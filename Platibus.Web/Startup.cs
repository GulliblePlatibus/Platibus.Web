using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices;
using Platibus.Web.Helpers;
using Platibus.Web.Registry;
using StructureMap;
using Platibus.Web.Pages;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace Platibus.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public static Guid subjectId { get; set; }
        
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _configuration = config;
            _environment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<BackendServerUrlConfiguration>(
                _configuration.GetSection(nameof(BackendServerUrlConfiguration)));
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Management.API Auth", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "application",
                    Description = "This API uses the Management.API login Oauth2 Client Credentials flow",
                    TokenUrl = "https://qa-auth-management-identity.azurewebsite.net/connect/token",
                    Scopes = new Dictionary<string, string> { { "scope.fullacces", "Acces to all api-endpoints" } }
                });
                c.SwaggerDoc("v1", new Info { Title = "Management Backend", Version = "v1", Description = "Management API for use with prior agreement" });
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IShift, Shift>();
            services.AddTransient<IUserDataService, UserDataService>();
            services.AddTransient<IShiftDataService, ShiftDataService>();
            services.AddTransient<IWorkScheduleDataService, WorkScheduleDataService>();
            services.AddTransient<IStaticWebResources, StaticWebResources>();
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(x =>
                {
                    x.DefaultScheme = "Cookies";
                    x.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", x =>
                {
                    x.SignInScheme = "Cookies";
                    x.SignOutScheme = "Cookies";
                    
                    x.Authority = "https://localhost:5001/";
                    x.RequireHttpsMetadata = false;
                    x.ClientId = "mvc";
                    x.SaveTokens = true;
                    x.Events = new OpenIdConnectEvents
                    {
                        OnTokenValidated = async ctx =>
                        {
                            ctx.ResolveClaims();
                        }
                    };
                });
            services.AddAuthorization(x =>
            {
                x.AddPolicy(UserRoles.Admin.ToString(), policy =>
                    policy.RequireClaim(ClaimTypes.Role, UserRoles.Admin.ToString()));
                x.AddPolicy(UserRoles.Administrative.ToString(), policy =>
                    policy.RequireClaim(ClaimTypes.Role, UserRoles.Admin.ToString(), UserRoles.Administrative.ToString()));
                x.AddPolicy(UserRoles.Manager.ToString(), policy =>
                    policy.RequireClaim(ClaimTypes.Role, UserRoles.Manager.ToString()));
                x.AddPolicy(UserRoles.Employee.ToString(), policy =>
                    policy.RequireClaim(ClaimTypes.Role, UserRoles.Employee.ToString()));
            });
            
            
            var container = new Container(new WebRegistry());
            container.Configure(config => config.Populate(services));
            
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomAuthLookupMiddleware();
            
            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //Enabled API to deliver swagger UI on http://{serverUrl}/swagger;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}

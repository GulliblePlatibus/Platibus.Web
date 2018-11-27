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
            
            
            var container = new Container(new WebRegistry());
            container.Configure(config => config.Populate(services));
            
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomAuthLookupMiddleware();
            
            
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

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.Helpers
{
    public static class WebExtensions
    {
        private const string TypeName = "name";

        //public static Guid SubjectId { get; private set; }

        public static TokenValidatedContext ResolveClaims(this TokenValidatedContext context)
        {
            //Get jwt token issued by identityProvider
            var jwt = context.SecurityToken;
            
            var subjectId = jwt.Subject;
            
            if (subjectId != null)
            {
                //SubjectId = Guid.Parse(subjectId);
                Startup.subjectId = Guid.Parse(subjectId);
            }
            
            //Extract claims from jwt token
            var claims = new List<Claim>(jwt.Claims);

            //Check if the claims contain a claim of the type 'name'
            if (claims.Exists(x => x.Type.Equals(TypeName)))
            {
                //Gets the claim typeOf 'name'
                var authLvl = claims.Where(x => x.Type.Equals(TypeName)).SingleOrDefault();
                
                //Try and parse the authLvl to a int value;

                if (authLvl.Value.Equals(UserRoles.Admin.ToString()))
                {
                    context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role, UserRoles.Admin.ToString())}));
                }

                if (authLvl.Value.Equals(UserRoles.Administrative.ToString()))
                {
                    context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role, UserRoles.Administrative.ToString())}));
                }

                if (authLvl.Value.Equals(UserRoles.Manager.ToString()))
                {
                    context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role, UserRoles.Employee.ToString())}));
                }

                if (authLvl.Value.Equals(UserRoles.Employee.ToString()))
                {
                    context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role,UserRoles.Unknown.ToString())}));
                }
            }
            
            return context;
        }

        public static Guid SubjectId(this HttpContext httpContext)
        {
            const string SUB = "sub";
            var user = httpContext.User;

            Guid subjectId = Guid.Empty;
            
            foreach (var claim in user.Claims)
            {
                
                if (claim.Type.Equals(SUB))
                {
                    //Try parse
                    if (Guid.TryParse(claim.Value, out subjectId))
                    {
                        return subjectId;
                    }
                }
            }
            return subjectId;
        }
    }
}
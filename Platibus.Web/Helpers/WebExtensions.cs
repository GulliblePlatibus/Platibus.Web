

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Platibus.Web.Helpers
{
    public static class WebExtensions
    {
        private const string TypeName = "name";
        
        public static TokenValidatedContext ResolveClaims(this TokenValidatedContext context)
        {
            //Get jwt token issued by identityProvider
            var jwt = context.SecurityToken;
            //Extract claims from jwt token
            var claims = new List<Claim>(jwt.Claims);

            //Check if the claims contain a claim of the type 'name'
            if (claims.Exists(x => x.Type.Equals(TypeName)))
            {
                //Gets the claim typeOf 'name'
                var authLvl = claims.Where(x => x.Type.Equals(TypeName)).SingleOrDefault();
                
                //Try and parse the authLvl to a int value;
                int authNiveau = 0;
                if (Int32.TryParse(authLvl.Value, out authNiveau))
                {
                    //Issue appropriate role
                    switch (authNiveau)
                    {
                        case 0:
                            context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role, UserRoles.Admin.ToString())}));
                            break;
                        case 1:
                            context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role, UserRoles.Manager.ToString())}));
                            break;
                        case 2:
                            context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role, UserRoles.Employee.ToString())}));
                            break;
                        default:
                            context.Principal.AddIdentity(new ClaimsIdentity(new[]{new Claim(ClaimTypes.Role,UserRoles.Unknown.ToString())}));
                            break;
                    }
                }
            }
            
            return context;
        }
    }
}
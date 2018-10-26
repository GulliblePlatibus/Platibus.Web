using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Platibus.Web.Pages.LoginLogout
{
    [Authorize]
    public class LoginLogout_IndexModel : PageModel
    {
        private const string Cookie = ".AspNetCore.Cookies";
        
        public void OnGet()
        {
        
        }

        public async void OnPost()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
    }
}
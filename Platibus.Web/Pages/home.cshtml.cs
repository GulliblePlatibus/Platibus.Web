using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages
{
 
    [Authorize]
    public class homeModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        public User user { get; set; }
        public string idString { get; set; }
        
        public homeModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }


        [Authorize]
        public async Task<IActionResult> OnGet()
        {
            var ui = HttpContext;

            var token = await HttpContext.GetTokenAsync("access_token");

            var a = HttpContext.Request.Headers.TryGetValue("access_token", out var token1);

            var id = HttpContext.SubjectId();
            try
            {
                user = await _userDataService.GetUserById(id);
            }
            catch (HttpRequestException ex)
            {
                return new RedirectToPageResult("Error");
            }

            switch (user.AccessLevel)
            {
                case UserRoles.Unknown:
                    return Redirect("/Home");
                case UserRoles.Employee:
                    return Redirect("/Employee/Overview");
                case UserRoles.Manager:
                    return Redirect("/Calendar/Calendar_Index");
                case UserRoles.Administrative:
                    return Redirect("/Analytics/Analytics_Index");
                case UserRoles.Admin:
                    return Redirect("/Administrative/Administrative_Index");
            }

            return Redirect("/Home");
        }
    }
}

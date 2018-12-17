using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.Helpers;

namespace Platibus.Web.Controllers
{
    
    [Route("")]
    public class ManageController : Controller
    {
        private readonly IUserDataService _userDataService;
        private readonly IOptions<IdentityServerConfiguration> _identityConfig;


        public ManageController(IUserDataService userDataService, IOptions<IdentityServerConfiguration> identityConfig)
        {
            _userDataService = userDataService;
            _identityConfig = identityConfig;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userid = HttpContext.SubjectId();

            var user = await _userDataService.GetUserById(userid);

            if (!userid.Equals(Guid.Empty))
            {
                if (user.Id.Equals(userid))
                {
                    return RedirectToPage("/home");
                }
            }
            else
            {
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> signOut()
        {
            
            HttpClient a = new HttpClient();

            await a.GetAsync($"{_identityConfig.Value.IdentityServerUrl}/account/logout");

            //Console.WriteLine();

            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
            HttpContext.Response.Cookies.Delete("idsrv");
            return Redirect("/");
        }
    } 
}
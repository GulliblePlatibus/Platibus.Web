using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.Helpers;

namespace Platibus.Web.Controllers
{
    
    [Route("")]
    public class ManageController : Controller
    {
        private readonly IUserDataService _userDataService;

        public ManageController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
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

            // await a.GetAsync("https://localhost:5001/account/logout");

            Console.WriteLine();

            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
            
            
            
            return Redirect("/");
        }
    } 
}
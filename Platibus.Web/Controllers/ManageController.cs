using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Platibus.Web.Acquaintance.IDataServices;

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
            var userid = Startup.subjectId;

            var user = await _userDataService.GetUserById(userid);

            if (!userid.Equals(Guid.Empty))
            
                if (user.Id.Equals(userid))
                {
                    RedirectToPage("/home");
                }
            
            
            return View();
        }
        
    }
    
    
}
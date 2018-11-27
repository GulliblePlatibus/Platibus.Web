using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.Helpers;

namespace Platibus.Web.ViewComponents
{
    public class AdminlteMainSidebarViewComponent : ViewComponent
    
    {
        private readonly IUserDataService _userDataService;

        public AdminlteMainSidebarViewComponent(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var id = HttpContext.SubjectId();

            var user = await _userDataService.GetUserById(id);
            
            
            return  View("AdminlteMainSidebar" , user);
        }
    }
}
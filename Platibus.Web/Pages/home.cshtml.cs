using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages
{
 
    [Authorize]
    public class homeModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {
           
        }
    }
}

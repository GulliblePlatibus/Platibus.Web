using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages
{
 
    [Authorize]
    public class homeModel : PageModel
    {
        public void OnGet()
        {
           
        }
    }
}

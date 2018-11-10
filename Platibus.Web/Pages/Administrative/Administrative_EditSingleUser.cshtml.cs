using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Administrative
{
    public class Administrative_EditSingleUserModel : PageModel
    {
private IUserDataService _userDataService;
        
        public User user { get; set; }

        public Administrative_EditSingleUserModel(IUserDataService _userDataService)
        {
           this._userDataService = _userDataService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Guid guid = Guid.Parse(id);
            
            user = await _userDataService.GetUserById(guid);

            return Page();
        }


    }
}
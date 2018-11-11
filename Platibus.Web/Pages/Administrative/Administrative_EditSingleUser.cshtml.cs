using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Administrative
{
    public class Administrative_EditSingleUserModel : PageModel
    {
private IUserDataService _userDataService;
        
        [BindProperty]
        public User user { get; set; }

        public List<SelectListItem> Items =>
        Enumerable.Range(1, 4).Select(x => new SelectListItem
        {
            Value = x.ToString(),
            Text = x.ToString()
        }).ToList();

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

        public async Task<IActionResult> OnPostUpdateUserAsync()
        {

            System.Diagnostics.Debug.WriteLine("Access: " + user.AccesLevel +
                "\nName: " + user.Name + "\nEmail: " + user.Email);

            return Page();
        }


    }
}
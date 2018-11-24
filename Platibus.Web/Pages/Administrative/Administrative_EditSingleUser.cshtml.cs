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

        public Guid guid { get; set; }

        //SelectedlistItem with range 1-4, because the view automatically interprets this as options, 
        //and can be easily bound to an attribute (in this case: user.AccessLevel)
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
            if (String.IsNullOrEmpty(id))
            {
                return new RedirectToPageResult("Error");
            }

            guid = Guid.Parse(id);
            
            user = await _userDataService.GetUserById(guid);

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateUserAsync(string id)
        {
            guid = Guid.Parse(id);
            
            var response = await _userDataService.UpdateUserById(guid, user);

            if (!response.IsSuccesfull)
            {
                return new RedirectToPageResult("Error");
            }

            return new RedirectToPageResult("./Administrative_EditUsers");
           
        }


    }
}
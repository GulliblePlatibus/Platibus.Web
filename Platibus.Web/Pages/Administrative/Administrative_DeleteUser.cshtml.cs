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
    public class Administrative_DeleteUserModel : PageModel
    {
        private IUserDataService _userDataService;
        public Guid guid { get; set; }
        [BindProperty]
        public User user { get; set; }

        public Administrative_DeleteUserModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new RedirectToPageResult("Error");
            }

            guid = Guid.Parse(id);

            user = await _userDataService.GetUserById(guid);

            if(user == null)
            {
                return new RedirectToPageResult("Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new RedirectToPageResult("Error");
            }

            guid = Guid.Parse(id);

            var response = await _userDataService.DeleteUserById(guid);

            if (response.IsSuccesfull)
            {
                return new RedirectToPageResult("./Administrative_EditUsers");
            }
            return new RedirectToPageResult("Error");
        }
    }
}
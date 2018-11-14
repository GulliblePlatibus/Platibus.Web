using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Personal
{
    public class EditInformationModel : PageModel
    {
        private IUserDataService _userDataService;
        [BindProperty] public User _user { get; set; }

        public EditInformationModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        

        public async Task<IActionResult> OnGetAsync(string id)
        {
            _user = await _userDataService.GetUserById(Guid.Parse(id));

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateUserAsync(string id)
        {
            var response = await _userDataService.UpdateUserById(Guid.Parse(id), _user);

            if (!response.IsSuccesfull)
            {
                return new RedirectToPageResult("Error");
            }

            return new RedirectToPageResult("./UserInformation");

        }
    }
}
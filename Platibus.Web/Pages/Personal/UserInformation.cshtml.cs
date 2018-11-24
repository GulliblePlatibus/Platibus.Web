using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages.User_Pages
{
    public class UserInformationModel : PageModel
    {
        private readonly IUserDataService _userDataService;

        [BindProperty] public User user { get; set; }
        [BindProperty] public string idString { get; set; }
        [BindProperty] public string accessString { get; set; }

        public UserInformationModel(IUserDataService dataService)
        {
            _userDataService = dataService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Change so it is the currently logged in User!!!
            //var id = "387eb00f-7420-45e9-abd9-a5229554115f";
            var id = WebExtensions.SubjectId;
            
            try
            {
               // user = await _userDataService.GetUserById(Guid.Parse(id));
                user = await _userDataService.GetUserById(id);
            }
            catch (HttpRequestException ex)
            {
                return new RedirectToPageResult("Error");
            }

           // idString = user.Id.ToString();

            switch (user.AccessLevel)
            {
                case 0:
                    idString = "Role undefined";
                    break;
                case 1:
                    idString = "Employee";
                    break;
                case 2:
                    idString = "Manager";
                    break;
                case 3:
                    idString = "Administrative Manager";
                    break;
                case 4:
                    idString = "Admin / Root";
                    break;
            }
            
            return Page();
        }

        


    }
}
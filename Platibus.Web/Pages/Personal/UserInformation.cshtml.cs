using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.Controllers;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages.User_Pages
{
    [Authorize]
    public class UserInformationModel :  PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IStaticWebResources _webResources;

        [BindProperty] public User user { get; set; }
        [BindProperty] public string idString { get; set; }
        [BindProperty] public string accessString { get; set; }
        
        

        public UserInformationModel(IUserDataService dataService , IStaticWebResources webResources)
        {
            _userDataService = dataService;
            _webResources = webResources;
        }

        
        
        [Authorize]
        public async Task<IActionResult> OnGetAsync()
        {
            //Change so it is the currently logged in User!!!
            //var id = "387eb00f-7420-45e9-abd9-a5229554115f";
            

            var ui = HttpContext;

            var token = await HttpContext.GetTokenAsync("access_token");

            var a = HttpContext.Request.Headers.TryGetValue("access_token", out var token1);

            var id = HttpContext.SubjectId();
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
                case UserRoles.Unknown:
                    idString = "Role undefined";
                    break;
                case UserRoles.Employee:
                    idString = "Employee";
                    break;
                case UserRoles.Manager:
                    idString = "Manager";
                    break;
                case UserRoles.Administrative:
                    idString = "Administrative Manager";
                    break;
                case UserRoles.Admin:
                    idString = "Admin / Root";
                    break;
            }
            
            return Page();
        }

        


    }
}
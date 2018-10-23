using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserDataService _userDataService;

        public IndexModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;

            _userDataService.CreateUser(new User());
        }

        public void OnGet()
        {

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Calendar
{
    public class Calendar_IndexModel : PageModel
    {
        public IUserDataService UserDataService { get; set; }
 
        [BindProperty]
        public IEnumerable<User> _Users { get; set; }
        public string test { get; set; } = "test";


        public Calendar_IndexModel(IUserDataService userDataService)
        {
            UserDataService = userDataService;
            _Users = new List<User>();

        }


        public async Task OnGetAsync()
        {

        }

        public async Task OnPost()
        {
            _Users = await UserDataService.ListUsersAsync(2, 2);


            foreach (var VARIABLE in _Users)
            {
                Console.WriteLine(VARIABLE.Email);    
            }
        }
    }
}
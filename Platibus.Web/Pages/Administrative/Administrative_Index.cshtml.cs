using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using System.Linq;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Administrative
{
    public class Administrative_IndexModel : PageModel
    {
        IUserDataService _userDataService;

        [BindProperty]
        public IEnumerable<User> _Users { get; set; }

        public Administrative_IndexModel(IUserDataService userDataService)
        {
            this._userDataService = userDataService;
            _Users = new List<User>();
        }

        public async Task OnGetAsync()
        {

        }

        public async Task OnPostGetUsersAsync()
        {
            _Users = await _userDataService.ListUsersAsync(2, 2);

            foreach (var VARIABLE in _Users)
            {
                Console.WriteLine(VARIABLE.Email);
            }
        }
    }
}
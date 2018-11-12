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
    public class Administrative_EditUsersModel : PageModel
    {
        IUserDataService _userDataService;
        public IEnumerable<User> _users { get; set; }
        [BindProperty] public List<User> _userList { get; set; }
        
        public Administrative_EditUsersModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
            _users = new List<User>();
            _userList = new List<User>();
        }

        public async Task OnGetAsync()
        {
            _users = await _userDataService.ListUsersAsync(0, 10);
            _userList = _users.ToList(); 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.Pages.Analytics
{
    public class Analytics_UserListModel : PageModel
    {

        public Analytics_UserListModel(IUserDataService userDataService)
        {
            
        }
        
        public void OnGet()
        {

        }
    }
}
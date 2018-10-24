using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.DataServices;

namespace Platibus.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserDataService userDataService;

        public RegisterModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
        }
        
        /*Below is the OnPost method for the different elements
         * In these methods, the two-way binding is used
         * To access the bound object, simply use the name specified in the .cshtml file
         */ 
        public void OnPost(string user_emailaddress)
        {
            
        }
        
    }
}
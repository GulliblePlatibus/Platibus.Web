using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserDataService _userDataService;

        public RegisterModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        /*Below is the OnPost method for the different elements
         * In these methods, the two-way binding is used
         * To access the bound object, simply use the name specified in the .cshtml file
         */ 
        public async void OnPost(string user_emailaddress)
        {
            //await _userDataService.Login();

            /*var response = await _userDataService.CreateUser(new User
            {
                Email = user_emailaddress,
                Name = "John Doe",
                Password = "1234"
            });

            if (response.IsSuccesfull)
            {
                Console.WriteLine("The request is successful");
            }
            else
            {
                Console.WriteLine(response.Message);
            }*/
        }
    }
}
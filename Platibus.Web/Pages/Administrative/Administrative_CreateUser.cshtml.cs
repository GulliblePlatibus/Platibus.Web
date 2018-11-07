using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Administrative
{
    public class Administrative_CreateUserModel : PageModel
    {
        /*This pagemodel is responsible for taking the input of its corresponding @page,
         *and using the IUserDataService (an instance of which is injected in the constructor
         *(which takes an IUser as an argument, hence the injected IUser).
         * The IUserDataService contacts the backend server which creates the specified user
         * in the system.
         */
        private IUserDataService userDataService;
        private User user;

        public Administrative_CreateUserModel(IUserDataService _userDataService)
        {
            this.userDataService = _userDataService;
            this.user = new User();
        }

        /*Note: The name contains 'createUser' because the input type=
         *submit annotation in the corresponding @page, has the asp page
         *handler "CreateUser", therefore this onPost is only called 
         * when the client presses the 'Create User' button.
         * The strings parameters 'user_realName, user_...' is similarly
         * named the same as the inputs in the @page, enabling the binding
         */
        public async Task OnPostCreateUserAsync
            (
            string user_realName, 
            string user_email, 
            string user_password
            )
        {
            if ((!String.IsNullOrEmpty(user_realName)) &&
               (!String.IsNullOrEmpty(user_email)) &&
               (!String.IsNullOrEmpty(user_password))
                ){
                user.Name = user_realName;
                user.Email = user_email;
                user.Password = user_password;

                await userDataService.CreateUser(this.user);
            }
            else
            {

            }
        }
    }
}
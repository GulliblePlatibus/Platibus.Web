using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages.Administrative
{
    [Authorize(Roles = nameof(UserRoles.Admin))]
    public class Administrative_CreateUserModel : PageModel
    {
        /*This pagemodel is responsible for taking the input of its corresponding @page,
         *and using the IUserDataService (an instance of which is injected in the constructor
         *(which takes an IUser as an argument, hence the injected IUser).
         * The IUserDataService contacts the backend server which creates the specified user
         * in the system.
         */
        private IUserDataService _userDataService;

        public bool UserCreationSuccesfull { get; set; } = true;
        public string UserCreationSuccesMSg { get; set; }
        
        public bool UserCreationError { get; set; } = true;
        public string UserCreationErrorMsg { get; set; }
        
        public Administrative_CreateUserModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        /*Note: The name contains 'createUser' because the input type=
         *submit annotation in the corresponding @page, has the asp page
         *handler "CreateUser", therefore this onPost is only called 
         * when the client presses the 'Create User' button.
         * The strings parameters 'user_realName, user_...' is similarly
         * named the same as the inputs in the @page, enabling the binding
         */
        public async Task OnPostCreateUserAsync(string user_realName, string user_email, string user_password, string authLvl)
        {
            UserCreationError = true;
            UserCreationErrorMsg = "";
            if (string.IsNullOrEmpty(user_realName) || string.IsNullOrEmpty(user_email) || string.IsNullOrEmpty(user_password))
            {
                //Show error messages in view
                UserCreationError = false;
                UserCreationErrorMsg = "Name, email and password may not be empty... Please try again";
                return;
            }

            if (string.IsNullOrEmpty(authLvl))
            {
                UserCreationError = false;
                UserCreationErrorMsg = "You must choose a role for user to be created";
                return;
            }

            var authNiveau = ResolveAuthNiveau(authLvl);

            if (authNiveau == 4)
            {
                UserCreationError = false;
                UserCreationErrorMsg = "The chosen role is not valid --> Valid roles are Admin, Employee, Manager";
                return;
            }
            
            var response = await _userDataService.CreateUser(new User
            {
                Email = user_email,
                Name = user_realName,
                Password = user_password,
                AccesLevel = authNiveau
            });

            if (!response.IsSuccesfull)
            {
                UserCreationError = false;
                UserCreationErrorMsg = response.Message;
                return;
            }
            
            //Show success messages in view
            UserCreationSuccesfull = false;
            UserCreationSuccesMSg =
                "Succesfully created user with email: " + user_email + " and password " + user_password;

        }

        private int ResolveAuthNiveau(string role)
        {
            if (role.Equals("Administrative"))
            {
                return 0;
            }

            if (role.Equals("Manager"))
            {
                return 1;
            }

            if (role.Equals("Employee"))
            {
                return 2;
            }

            return 4;
        }
    }
}
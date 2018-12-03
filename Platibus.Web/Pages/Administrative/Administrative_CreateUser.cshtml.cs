using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Helpers;

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
        public async Task OnPostCreateUserAsync(string user_realName, string user_email, string user_password, double user_wage, DateTime user_employmentDate, string authLvl)
        {
            UserCreationError = true;
            UserCreationErrorMsg = "";
            if (string.IsNullOrEmpty(user_realName) || string.IsNullOrEmpty(user_email) || string.IsNullOrEmpty(user_password) || double.IsNaN(user_wage))
            {
                //Show error messages in view
                UserCreationError = false;
                UserCreationErrorMsg = "Name, email, password, wage and employment start can not be empty... Please try again";
                return;
            }
            if (DateTime.Equals(DateTime.MinValue,DateTime.MinValue))
            {
                user_employmentDate = DateTime.Today;
            }
           

            if (string.IsNullOrEmpty(authLvl))
            {
                UserCreationError = false;
                UserCreationErrorMsg = "You must choose a role for user to be created";
                return;
            }

            var authNiveau = ResolveAuthNiveau(authLvl);

            if (authNiveau.Equals(UserRoles.Unknown))
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
                BaseWage = user_wage,
                EmploymentDate = user_employmentDate,
                AccessLevel = authNiveau
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
                "Succesfully created user with email: " + user_email + " and password " + user_password + " and wage " + user_wage + " and employment start at " + user_employmentDate + response;

        }

        private UserRoles ResolveAuthNiveau(string role)
        {
            if (role.Equals("Administrative"))
            {
                return UserRoles.Administrative;
            }

            if (role.Equals("Manager"))
            {
                return UserRoles.Manager;
            }

            if (role.Equals("Employee"))
            {
                return UserRoles.Employee;
            }

            return UserRoles.Unknown;
        }
    }
}

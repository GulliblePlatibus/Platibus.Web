using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Platibus.Web.Helpers;

namespace Platibus.Web.DataServices.Models.User
{
    public class UpdateUserModel
    {
        public UpdateUserModel(string name, string email, string password, UserRoles accessLevel)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles AccessLevel { get; set; }
    }
}

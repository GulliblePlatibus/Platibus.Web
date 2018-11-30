using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Platibus.Web.Helpers;

namespace Platibus.Web.DataServices.Models.User
{
    public class UpdateUserModel
    {
        public UpdateUserModel(string name, string email, string password, double wage, DateTime employmentDate, UserRoles accessLevel)
        {
            Name = name;
            Email = email;
            Password = password;
            Wage = wage;
            EmploymentDate = employmentDate;
            AccessLevel = accessLevel;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Wage { get; set; }
        public DateTime EmploymentDate { get; set; }
        public UserRoles AccessLevel { get; set; }
    }
}

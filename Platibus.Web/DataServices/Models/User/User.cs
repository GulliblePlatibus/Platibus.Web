using Newtonsoft.Json;
using Platibus.Web.Acquaintance.IDataServices;
using System;
using Platibus.Web.Helpers;

namespace Platibus.Web.DataServices.Models.User
{

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Wage { get; set; }
        public DateTime EmploymentDate { get; set; }
        public UserRoles AccessLevel { get; set; }
        public Guid Id { get; set; }
    }
}
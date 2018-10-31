using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.DataServices.Models.User
{

    public class User : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
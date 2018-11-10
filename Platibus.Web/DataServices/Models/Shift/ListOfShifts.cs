using System.Collections.Generic;

namespace Platibus.Web.DataServices.Models.Shift
{
    public class ListOfUsers
    {
        public List<User.User> UserList { get; set; }
        public User.User SelectedUser{ get; set; }
        public string UserName { get; set; }
    }
}


using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Documents;
using System;
using System.Threading.Tasks;

namespace Platibus.Web.Acquaintance.IDataServices
{
    public interface IUserDataService
    {
        Task<Response> CreateUser(IUser user);
        Task<User> GetUserById(Guid id);
    }

    public interface IUser
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }



}
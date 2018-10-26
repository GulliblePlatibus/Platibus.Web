using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Documents;

namespace Platibus.Web.DataServices
{
    public interface IUserDataService
    {
        Task<Response> CreateUser(User user);
        Task<User> GetUserById(Guid id);
    }
    
    public class UserDataService : BaseDataService, IUserDataService
    {
        public UserDataService(IOptions<BackendServerUrlConfiguration> config) : base(config)
        {
        }

        public async Task<Response> CreateUser(User user)
        {
            var baseurl = _serverUrl + "/api/users"; //<-- Endpoint on backend!!!

            var response = await PostAsync<User>(baseurl, user);

            if (!response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Response.Unsuccesfull(errorMsg);
                }
                return Response.Unsuccesfull(response.ReasonPhrase);
            }
            
            return Response.Succes();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var baseurl = _serverUrl + "/"; //<-- specific url here!

            var result = await GetAsync(baseurl);

            return await TryReadAsync<User>(result);
        }
    }
}
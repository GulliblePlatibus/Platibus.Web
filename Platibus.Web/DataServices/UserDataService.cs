using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Documents;
using Platibus.Web.Acquaintance.IDataServices;
using System.Collections.Generic;

namespace Platibus.Web.DataServices
{
    
    public class UserDataService : BaseDataService, IUserDataService
    {
        public UserDataService(IOptions<BackendServerUrlConfiguration> config) : base(config)
        {
        }

        public async Task<Response> CreateUser(IUser user)
        {
            var baseurl = _serverUrl + "/api/users"; //<-- Endpoint on backend!!!

            var response = await PostAsync<IUser>(baseurl, user);

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

        public async Task<IEnumerable<IUser>> ListUsersAsync(int page, int pageSize)
        {
            var baseurl = _serverUrl + '/'; //+specific url
            var result = await GetAsync(baseurl);

            return await TryReadAsync<IEnumerable<IUser>>(result);
        }
    }
}
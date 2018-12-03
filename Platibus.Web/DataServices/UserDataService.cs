using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Documents;
using Platibus.Web.Acquaintance.IDataServices;
using System.Collections.Generic;
using Platibus.Web.DataServices.Models.Salary;
using Platibus.Web.DataServices.Models.Salary.Entities;

namespace Platibus.Web.DataServices
{
    
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

        
        public async Task<Response> UpdateUserById(Guid id, User user)
        {
            var baseurl = _serverUrl + "/api/users/" + id;

            //Type argument specification NOT redundant, will fail to build!
            var response = await PutAsync<UpdateUserModel>(baseurl, new UpdateUserModel(user.Name, user.Email, user.Password, user.BaseWage, user.EmploymentDate, user.AccessLevel));

            if (!response.IsSuccessStatusCode)
            {
                if(response.Content != null)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Response.Unsuccesfull(errorMsg);
                }
                return Response.Unsuccesfull(response.ReasonPhrase);
            }
            return Response.Succes();
        }

        public async Task<Response> DeleteUserById(Guid id)
        {
            var baseurl = _serverUrl + "/api/users/" + id;

            var response = await DeleteAsync(baseurl);

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

        public async Task<List<ShiftPayment>> GetSalaryForUserPagedAsync(Guid userId, DateTime fromDate, DateTime toDate)
        {
            var baseurl = _serverUrl + $"/api/users/{userId}/salary?fromDate={fromDate.Ticks}&toDate={toDate.Ticks}";

            var result = await GetAsync(baseurl);

            return await TryReadAsync<List<ShiftPayment>>(result);
        }

       /* public async Task<ShiftPayment> GetSalaryByIdAsync(Guid id)
        {
            var baseurl = _serverUrl + "/api/users/salary/" + id;

            var result = await GetAsync(baseurl);

            return await TryReadAsync<ShiftPayment>(result); 
        }*/

        public async Task<User> GetUserById(Guid id)
        {
            var baseurl = _serverUrl + "/api/users/" + id; //<-- specific url here!

            var result = await GetAsync(baseurl);

            return await TryReadAsync<User>(result);
        }

        public async Task<IEnumerable<User>> ListUsersAsync(int page, int pageSize)
        {
            var baseurl = _serverUrl + "/api/users/"; //+specific url
            var result = await GetAsync(baseurl);

            var a = await TryReadAsync<IEnumerable<User>>(result);
            
            return a;
        }
    }
}
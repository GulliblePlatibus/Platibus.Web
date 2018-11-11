using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Documents;

namespace Platibus.Web.DataServices
{
    public class ShiftDataService : BaseDataService , IShiftDataService
    {
        public ShiftDataService(IOptions<BackendServerUrlConfiguration> config) : base(config)
        {
            
        }

        public async Task<Response> CreateShift(Shift shift)
        {
            var baseUrl = _serverUrl + "/api/shifts";

            var response = await PostAsync<Shift>(baseUrl, shift);
            
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
        
        public async Task<IEnumerable<Shift>> ListUsersAsync()
        {
            var baseurl = _serverUrl + "/api/shifts"; //+specific url
            var result = await GetAsync(baseurl);

            var a = await TryReadAsync<IEnumerable<Shift>>(result);
            
            return a;
        }

        public async Task<Response> AddEmployeeToShift(Shift shift)
        {
            var baseurl = _serverUrl + "/api/shifts/AddUser";

            
            var result = await PostAsync(baseurl , shift);

            if (!result.IsSuccessStatusCode)
            {
                return Response.Unsuccesfull();
                
            }
            
            return Response.Succes();
            
        }

        public async Task<Shift> GetShiftById(Guid id)
        {
            var baseurl = _serverUrl + string.Format("/api/shifts/{0}", id);
            var result = await GetAsync(baseurl);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await TryReadAsync<Shift>(result);
            

        }

        public async Task<Response> DeleteShiftById(Guid id)
        {
            var baseurl = _serverUrl + string.Format("/api/shifts/{0}", id);

            var result = await DeleteAsync(baseurl);

            if (!result.IsSuccessStatusCode)
            {
                return Response.Unsuccesfull();
            }
            
            return Response.Succes();
            
        }
    }
}
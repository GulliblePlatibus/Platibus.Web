using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.Documents;

namespace Platibus.Web.DataServices
{
    public class ShiftDataService : BaseDataService , IShiftDataService
    {
        public ShiftDataService(IOptions<BackendServerUrlConfiguration> config) : base(config)
        {
            
        }

        public async Task<Response> CreateShift(IShift shift)
        {
            var baseUrl = _serverUrl + "/api/shift";

            var response = await PostAsync<IShift>(baseUrl, shift);
            
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
    }
}
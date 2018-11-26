using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices.Models.WorkSchedule;

namespace Platibus.Web.DataServices
{
    public class WorkScheduleDataService : BaseDataService  , IWorkScheduleDataService
    {
        public WorkScheduleDataService(IOptions<BackendServerUrlConfiguration> options) : base(options)
        {
            
        }

        public async Task<IEnumerable<AllShiftsWithEmployees>> GetAllWorkSchedules()
        {
            var baseurl = _serverUrl + "/api/workschedule"; //+specific url
            var result = await GetAsync(baseurl);

            var a = await TryReadAsync<IEnumerable<AllShiftsWithEmployees>>(result);
            
            return a;
        }
    }
}
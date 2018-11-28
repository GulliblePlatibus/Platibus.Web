using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.WorkSchedule;

namespace Platibus.Web.Pages.Help
{
    public class Employee_IndexModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IWorkScheduleDataService _workScheduleDataService;
            
        [BindProperty]
        public List<UserShiftDetailed> UserShiftDetaileds { get; set; }

        public Employee_IndexModel(IUserDataService userDataService, IWorkScheduleDataService workScheduleDataService)
        {
            _userDataService = userDataService;
            _workScheduleDataService = workScheduleDataService;
        }
        
    public async Task OnGetAsync()
        {
            UserShiftDetaileds = await _workScheduleDataService.GetUserShiftDetailed() as List<UserShiftDetailed>;
        }
    }
}
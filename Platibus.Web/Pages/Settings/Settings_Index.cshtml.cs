using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.DataServices.Models.WorkSchedule;

namespace Platibus.Web.Pages.Settings
{
    public class Settings_IndexModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IWorkScheduleDataService _workScheduleDataService;

        [BindProperty]
        public List<User> allUsers { get; set; }
        
        [BindProperty]
        public List<AllShiftsWithEmployees> AllShifts { get; set; }
        
        [BindProperty]
        public List<UserShiftDetailed> UserShiftDetaileds { get; set; }

        public Settings_IndexModel(IUserDataService userDataService , IWorkScheduleDataService workScheduleDataService)
        {
            _userDataService = userDataService;
            _workScheduleDataService = workScheduleDataService;
        }
        
        public async Task OnGetAsync()
        {
            allUsers = await _userDataService.ListUsersAsync(2, 2) as List<User>;
            AllShifts = await _workScheduleDataService.GetAllWorkSchedules() as List<AllShiftsWithEmployees>;
            UserShiftDetaileds = await _workScheduleDataService.GetUserShiftDetailed() as List<UserShiftDetailed>;

            
        }
    }
}
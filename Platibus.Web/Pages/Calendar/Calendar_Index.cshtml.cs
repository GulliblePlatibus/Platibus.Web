using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.DataServices.Models.WorkSchedule;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages.Calendar
{
    public class Calendar_IndexModel : PageModel
    {
        
        
        private readonly IUserDataService _userDataService;
        private readonly IWorkScheduleDataService _workScheduleDataService;

        [BindProperty]
        public List<User> allUsers { get; set; }
        
        [BindProperty]
        public List<AllShiftsWithEmployees> AllShifts { get; set; }
        
        [BindProperty]
        public List<UserShiftDetailed> UserShiftDetaileds { get; set; }


        public Calendar_IndexModel(IUserDataService userDataService , IWorkScheduleDataService workScheduleDataService)
        {
            _userDataService = userDataService;
            _workScheduleDataService = workScheduleDataService;
            UserShiftDetaileds = new List<UserShiftDetailed>();
        }
        
        public async Task OnGetAsync()
        {
            allUsers = await _userDataService.ListUsersAsync(2, 2) as List<User>;
            AllShifts = await _workScheduleDataService.GetAllWorkSchedules() as List<AllShiftsWithEmployees>;
            UserShiftDetaileds= await _workScheduleDataService.GetUserShiftDetailed() as List<UserShiftDetailed>;

            

            
        }
    }
}
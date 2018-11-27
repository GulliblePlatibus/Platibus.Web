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

namespace Platibus.Web.Pages.Booking
{
    public class Booking_indexModel : PageModel
    {
       
        private readonly IWorkScheduleDataService _workScheduleDataService;
        private readonly IUserDataService _userDataService;
        private readonly IShiftDataService _shiftDataService;

        [BindProperty]
        public List<AllShiftsWithEmployees> _allWorkSchedule { get; set; }
        
        [BindProperty]
        public List<User> AllUsers { get; set; }
        
        

        public Booking_indexModel(IWorkScheduleDataService workScheduleDataService , IUserDataService userDataService , IShiftDataService shiftDataService)
        {
           
            _workScheduleDataService = workScheduleDataService;
            _userDataService = userDataService;
            _shiftDataService = shiftDataService;

            _allWorkSchedule = new List<AllShiftsWithEmployees>();
        }
        
        public async Task OnGetAsync()
        {
            _allWorkSchedule = await _workScheduleDataService.GetAllWorkSchedules() as List<AllShiftsWithEmployees>;
            AllUsers = await _userDataService.ListUsersAsync(2, 2) as List<User>;

        }

        public async Task<IActionResult> OnPostDeleteShiftAsync(Guid ShiftId)
        {
            var result = await _shiftDataService.DeleteShiftById(ShiftId);
            return RedirectToPage("/Booking/Booking_index");
            
        }
    }
}
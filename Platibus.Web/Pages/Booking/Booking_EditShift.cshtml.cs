using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;
using Platibus.Web.DataServices.Models.WorkSchedule;

namespace Platibus.Web.Pages.Booking
{
    public class Booking_EditShiftModel : PageModel
    {
        private readonly IShiftDataService _shiftDataService;
        private readonly IUserDataService _userDataService;

        [BindProperty]
        public Shift shift { get; set; }
        
        [BindProperty]
        public User user { get; set; }
        
        [BindProperty]
        public List<SelectListItem> UserList { get; set; }
        
        public Booking_EditShiftModel(IShiftDataService shiftDataService , IUserDataService userDataService)
        {
            _shiftDataService = shiftDataService;
            _userDataService = userDataService;
            UserList = new List<SelectListItem>();
            shift = new Shift();
        }
        

        public async Task OnGetAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                // do error stuff
            }

            shift.id = id;
            // get shift
            shift = await _shiftDataService.GetShiftById(id);
            var users = await _userDataService.ListUsersAsync(2, 2);
            UserList = users.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }


        public async Task<IActionResult> OnPostUpdateShiftWithUserAsync(User user , Shift shift)
        {
            if (shift.ShiftStart == null)
            {
                return null; // More check and redirect to error page
            }

            //shift.EmployeeOnShift = user.Id;
            var shiftWithEmployee = new AssignUserToShift
            {
                EmployeeOnShift = user.Id,
                id = shift.id,
                ShiftStart = shift.ShiftStart,
                ShiftEnd = shift.ShiftEnd
            };

            var result = await _shiftDataService.AddEmployeeToShift(shiftWithEmployee);
            
            return RedirectToPage("/Booking/Booking_index");
        }
    }
}
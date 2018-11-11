using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Booking
{
    public class Booking_indexModel : PageModel
    {
        private readonly IShiftDataService _shiftDataService;
        private readonly IUserDataService _userDataService;

        [BindProperty]
        public List<Shift> _allShifts { get; set; }
        [BindProperty]
        public List<User> AllUsers { get; set; }

        public Booking_indexModel(IShiftDataService shiftDataService , IUserDataService userDataService)
        {
            _shiftDataService = shiftDataService;
            _userDataService = userDataService;
            _allShifts = new List<Shift>();
            AllUsers = new List<User>();
        }
        
        public async Task OnGetAsync()
        {
            _allShifts = await _shiftDataService.ListUsersAsync() as List<Shift>;
            AllUsers = await _userDataService.ListUsersAsync(2, 2) as List<User>;
            Console.WriteLine("");
        }

        public async Task<IActionResult> OnPostDeleteShiftAsync(Guid ShiftId)
        {
            var result = await _shiftDataService.DeleteShiftById(ShiftId);
            return RedirectToPage("/Booking/Booking_index");
        }
    }
}
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

namespace Platibus.Web.Pages.Booking
{
    public class Booking_CreateShift : PageModel
    {
        private readonly IShiftDataService _shiftDataService;
        private readonly IUserDataService _userDataService;
        
        [BindProperty]
        public Shift shift { get; set; }
        
        [BindProperty]
        public List<SelectListItem> UserList { get; set; }
        
        [BindProperty]
        public User user { get; set; }

        public Booking_CreateShift(IShiftDataService shiftDataService , IUserDataService userDataService)
        {
            _shiftDataService = shiftDataService;
            _userDataService = userDataService;
        }
        

        public async Task OnGetAsync()
        {
           
            
            
            var users = await _userDataService.ListUsersAsync(2, 2);
            UserList = users.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            
            
        }

        public async Task<IActionResult> OnPostCreateShiftAsync(User user , Shift shift)
        {
            if (user.Id.Equals(Guid.Empty))
            {
                var result = _shiftDataService.CreateShift(shift);
            }
            else
            {
                var create = await _shiftDataService.CreateShift(shift);
                var result = _shiftDataService.AddEmployeeToShift(shift);
            }
            
            
            
            return RedirectToPage("/Booking/Booking_index");
        }


        
            
    }
}
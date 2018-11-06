using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Calendar
{
    public class Calendar_IndexModel : PageModel
    {
        public IUserDataService UserDataService { get; set; }
        public IShift Shift { get; set; }
        public IShiftDataService ShiftDataService { get; set; }

        [BindProperty]
        public IEnumerable<User> _Users { get; set; }
        public string test { get; set; } = "test";
        
        public IEnumerable<IShift> Shifts { get; set; }


        public Calendar_IndexModel(IUserDataService userDataService , IShift shift , IShiftDataService shiftDataService)
        {
            UserDataService = userDataService;
            Shift = shift;
            ShiftDataService = shiftDataService;
            _Users = new List<User>();
            Shifts = new List<IShift>();

        }


        public async Task OnGetAsync()
        {

        }

        public async Task OnPostGetUsersAsync()
        {
            
            _Users = await UserDataService.ListUsersAsync(2, 2);


            foreach (var VARIABLE in _Users)
            {
                Console.WriteLine(VARIABLE.Email);    
            }
        }

        public async Task OnPostCreateShiftAsync(DateTime ShiftStart , DateTime ShiftEnd )
        {
            Shift.ShiftStart = ShiftStart;
            Shift.ShiftEnd = ShiftEnd;
            var response = await ShiftDataService.CreateShift(Shift);

            
        }

        public async Task OnPostGetShiftsAsync()
        {
            Shifts = await ShiftDataService.ListUsersAsync();
            
            
        }
        
        
    }
}

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
    public class Booking_CreateShiftModel : PageModel
    {
        private readonly IShiftDataService _shiftDataService;
        private readonly IUserDataService _userDataService;
       
        [BindProperty]
        public Shift shift { get; set; }
       
        [BindProperty]
        public List<SelectListItem> UserList { get; set; }
       
        [BindProperty]
        public User user { get; set; }
       
        [BindProperty]
        public int numberOfShifts { get; set; }
 
        public Booking_CreateShiftModel(IShiftDataService shiftDataService , IUserDataService userDataService)
        {
            _shiftDataService = shiftDataService;
            _userDataService = userDataService;
        }
       
 
        public async Task OnGetAsync(DateTime dateTime)
        {
 
            Console.WriteLine();
           
            var users = await _userDataService.ListUsersAsync(2, 2);
            UserList = users.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
           
           
        }
 
        public async Task<IActionResult> OnPostCreateShiftAsync(User user , Shift shift , int numberOfShifts)
        {
 
            var list = new ListOfShifts();
            list.listOfShifts = new List<Shift>();
           
 
            for (int i = 0; i < numberOfShifts; i++)
            {
                list.listOfShifts.Add(new Shift{ShiftEnd = shift.ShiftEnd , ShiftStart = shift.ShiftStart});
               
               
            }
 
            var result = _shiftDataService.CreateManyShifts(list);
         
           
           
            return RedirectToPage("/Booking/Booking_index");
        }
 
       
        public async Task OnGetTestAsync(string date)
        {
           
 
        }
 
 
       
           
    }
}
 
 
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Booking
{
    public class Calender_CreateShiftModel : PageModel
    {
        private readonly IShiftDataService _shiftDataService;

        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        
        [BindProperty]
        public Guid Id { get; set; }
        
        public Calender_CreateShiftModel(IShiftDataService shiftDataService)
        {
            _shiftDataService = shiftDataService;
        }
        
        public void OnGet(string id , Guid UserId)
        {
            var date = DateTime.Parse(id);

            StartDate = date;
            EndDate = date;
            Id = UserId;

        }


        public async Task<IActionResult> OnPostCreateShiftAsync(DateTime StartDate , DateTime EndDate , TimeSpan StartTime , TimeSpan EndTime , Guid id)
        {

            var DateStart = StartDate.Add(StartTime);

            var DateEnd = EndDate.Add(EndTime);

            var newShift = new CreateShiftModel
            {
                EmployeeId = id,
                ShiftEnd = DateEnd,
                ShiftStart = DateStart
            };

             await _shiftDataService.CreateShift(newShift);
            
            return RedirectToPage("/Calendar/Calendar_Index");
        }
    }
}
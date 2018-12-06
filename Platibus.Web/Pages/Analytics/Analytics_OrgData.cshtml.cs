using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Salary;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Analytics
{
    public class Analytics_OrgDataModel : PageModel
    {
        private IUserDataService _userDataService;
        private IWorkScheduleDataService _workScheduleDataService;
        private IEnumerable<User> Users { get; set; }
        [BindProperty] public List<User> _userList { get; set; }
        private List<List<ShiftPayment>> AllShifts { get; set; }
        [BindProperty] public List<YearAndTotal> _yearAndTotals { get; set; }

        public Analytics_OrgDataModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
            _userList = new List<User>();
            Users = _userDataService.ListUsersAsync(0, 10).Result;
            _userList = Users.ToList();
            AllShifts = new List<List<ShiftPayment>>(); 
            _yearAndTotals = new List<YearAndTotal>
            {
                new YearAndTotal(DateTime.Now.AddYears(-4), 0),
                new YearAndTotal(DateTime.Now.AddYears(-3), 0),
                new YearAndTotal(DateTime.Now.AddYears(-2), 0),
                new YearAndTotal(DateTime.Now.AddYears(-1), 0),
                new YearAndTotal(DateTime.Now, 0)    
            };
        }
        
        public void OnGet()
        {
            foreach (var item in Users)
            {
                AllShifts.Add(_userDataService.GetSalaryForUserPagedAsync(item.Id, DateTime.Now.AddYears(-4), DateTime.Now)
                    .Result); 
            }

            foreach (var list in AllShifts)
            {
                foreach (var item in list)
                {
                    if (item.Shift.ShiftStart > DateTime.Now.AddYears(-5) && item.Shift.ShiftStart < DateTime.Now.AddYears(-4))
                    {
                        _yearAndTotals[0]._total += item.TotalPayment;
                    }   
                    if (item.Shift.ShiftStart > DateTime.Now.AddYears(-4) && item.Shift.ShiftStart < DateTime.Now.AddYears(-3))
                    {
                        _yearAndTotals[1]._total += item.TotalPayment;
                    } 
                    if (item.Shift.ShiftStart > DateTime.Now.AddYears(-3) && item.Shift.ShiftStart < DateTime.Now.AddYears(-2))
                    {
                        _yearAndTotals[2]._total += item.TotalPayment;
                    } 
                    if (item.Shift.ShiftStart > DateTime.Now.AddYears(-2) && item.Shift.ShiftStart < DateTime.Now.AddYears(-1))
                    {
                        _yearAndTotals[3]._total += item.TotalPayment;
                    } 
                    if (item.Shift.ShiftStart > DateTime.Now.AddYears(-1) && item.Shift.ShiftStart < DateTime.Now)
                    {
                        _yearAndTotals[4]._total += item.TotalPayment;
                    } 
                }
            }

            var la = "";
        }
    }
}
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
    public class Analytics_ProjectedModel : PageModel
    {
        private IUserDataService _userDataService;
        private IWorkScheduleDataService _workScheduleDataService;
        private IEnumerable<User> Users { get; set; }
        [BindProperty] public List<User> _userList { get; set; }
        [BindProperty] public List<UserNameAndSalary> UserNameAndSalariesListYear { get; set; }
        private int startInViewList { get; set; }



        public Analytics_ProjectedModel(IUserDataService userDataService, IWorkScheduleDataService workScheduleDataService)
        {
            _userDataService = userDataService;
            _userList = new List<User>();
            UserNameAndSalariesListYear = new List<UserNameAndSalary>();
            Users = _userDataService.ListUsersAsync(0, 10).Result;
            _userList = Users.ToList();
            UserNameAndSalariesListYear = populateYearList();
        }

        public async Task OnGetAsync()
        {

        }



        private List<UserNameAndSalary> populateYearList()
        {
            var yearList = new List<UserNameAndSalary>();
            foreach (var user in _userList)
            {
                yearList.Add(populateSalariesList(
                    _userDataService.GetSalaryForUserPagedAsync(user.Id, DateTime.Now.AddYears(-20), DateTime.Now.AddYears(10))
                        .Result, user.Name));
            }

            if (!yearList.Any())
            {
                return populateDefaultSalariesList();
            }

            return yearList;
        }

        private UserNameAndSalary populateSalariesList(List<ShiftPayment> shiftPayments, string name)
        {
            if (!shiftPayments.Any()) return new UserNameAndSalary(name, 0);

            return new UserNameAndSalary(name, GetAccumulatedSalary(shiftPayments));
        }

        private List<UserNameAndSalary> populateDefaultSalariesList()
        {
            var emptyList = new List<UserNameAndSalary>();
            for (int i = 0; i < 10; i++)
            {
                emptyList.Add(new UserNameAndSalary("", 0));
            }

            return emptyList;
        }

        private double GetAccumulatedSalary(List<ShiftPayment> shiftPayments)
        {
            double totalSalary = 0;
            foreach (var shift in shiftPayments)
            {
                totalSalary += shift.TotalPayment;
            }

            return Math.Truncate(totalSalary);
        }



    }
}
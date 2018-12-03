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
    public class Analytics_GenInfoModel : PageModel
    {
        private IUserDataService _userDataService;
        private IWorkScheduleDataService _workScheduleDataService;
        private IEnumerable<User> Users { get; set; }
        [BindProperty] public List<User> _userList { get; set; }
        [BindProperty] public List<UserNameAndSalary> UserNameAndSalariesList { get; set; }
        private int startInViewList { get; set; }
        
        

        public Analytics_GenInfoModel(IUserDataService userDataService, IWorkScheduleDataService workScheduleDataService)
        {
            _userDataService = userDataService;
            _workScheduleDataService = workScheduleDataService;
            _userList = new List<User>();
            UserNameAndSalariesList = new List<UserNameAndSalary>();
            //Populate the list with empty values, so it does not crash, if there is less than 10 employees registered in the system
            populateDefaultSalariesList();
            
        }
        
        public async Task OnGetAsync()
        {
            Users = await _userDataService.ListUsersAsync(0, 10);
            _userList = Users.ToList();
        }

        private List<UserNameAndSalary> PopulateUserViewList(List<User> users, int start)
        {
            int j = start;
            for (int i = 0; i < UserNameAndSalariesList.Count; i++)
            {
                if(_userList[j] == null){ UserNameAndSalariesList[i] = new UserNameAndSalary("", 0);}
                else
                {
                    UserNameAndSalariesList[i] = new UserNameAndSalary(_userList[j].Name, 30);
                    j++;
                }
            }

            return null;
        }

        private void populateDefaultSalariesList()
        {
            for (int i = 0; i < 10; i++)
            {
                this.UserNameAndSalariesList.Add(new UserNameAndSalary("", 0));
            }
        }

        
        
        
    }
}
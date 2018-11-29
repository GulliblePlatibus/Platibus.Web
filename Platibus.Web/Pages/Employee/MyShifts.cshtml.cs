using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.WorkSchedule;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages.Employee
{
    public class MyShiftsModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IWorkScheduleDataService _workScheduleDataService;
            
        [BindProperty]
        public List<UserShiftDetailed> UserShiftDetaileds { get; set; }

        public MyShiftsModel(IUserDataService userDataService, IWorkScheduleDataService workScheduleDataService)
        {
            _userDataService = userDataService;
            _workScheduleDataService = workScheduleDataService;
        }
        
        public async Task OnGetAsync()
        {
            var temp = await _workScheduleDataService.GetUserShiftDetailed() as List<UserShiftDetailed>;
            UserShiftDetaileds = getLoggedInShifts(temp);
        }


        public List<UserShiftDetailed> getLoggedInShifts(List<UserShiftDetailed> userShiftDetaileds)
        {
            List<UserShiftDetailed> list = new List<UserShiftDetailed>();
            foreach (var VARIABLE in userShiftDetaileds)
            {
                if (VARIABLE.Id.Equals(HttpContext.SubjectId()))
                {
                    list.Add(VARIABLE);
                }
            }

            return list;
        }
    }
}
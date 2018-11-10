using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.User;

namespace Platibus.Web.Pages.Calendar
{
    public class Calendar_IndexModel : PageModel
    {
        public IUserDataService UserDataService { get; set; }
        
        [BindProperty]
        public Shift Shift { get; set; }
        public IShiftDataService ShiftDataService { get; set; }

        public ListOfUsers UserList { get; set; }
        [BindProperty]
        public string name { get; set; }

        [BindProperty] public List<SelectListItem> listItems { get; set; }
        
        [BindProperty]
        public IEnumerable<User> _Users { get; set; }
        public string test { get; set; } = "test";
        
        public List<Shift> Shifts { get; set; }

        public WebGrid grid {get;set;} 
        
        public ObservableCollection<Shift> obs { get; set; }


        public Calendar_IndexModel(IUserDataService userDataService  , IShiftDataService shiftDataService)
        {
            UserDataService = userDataService;
            UserList = new ListOfUsers();
            ShiftDataService = shiftDataService;
            _Users = new List<User>();
            Shifts = new List<Shift>();
            listItems = new List<SelectListItem>();
            
           
            



        }


        public async Task OnGetAsync()
        {
            
            _Users = await UserDataService.ListUsersAsync(2, 2);
            Shifts = (List<Shift>) await ShiftDataService.ListUsersAsync();
            UserList.UserList = _Users.ToList();
            UserList.UserName = "test";
            test1();
            Console.WriteLine("test");
        }

        

        public async Task OnPostCreateShiftAsync(DateTime ShiftStart , DateTime ShiftEnd )
        {
            Shift.ShiftStart = ShiftStart;
            Shift.ShiftEnd = ShiftEnd;
            var response = await ShiftDataService.CreateShift(Shift);

        }

        public async Task OnPostTESTAsync(DateTime test)
        {
            Console.WriteLine(test);
        }

        public async Task OnPostGetShiftsAsync(string name)
        {
            _Users = await UserDataService.ListUsersAsync(2, 2);

            Console.WriteLine("test");
            
            
        }

        public async Task OnPostGetUserAsync(Calendar_IndexModel s)
        {
           
        }

        public async Task OnPostAssignEmployeeAsync(string name , Shift shift1 , Guid id, string shift)
        {

            _Users = await UserDataService.ListUsersAsync(2, 2);
            
            Shifts = (List<Shift>) await ShiftDataService.ListUsersAsync();

            
            foreach (var VARIABLE in _Users) 
            {
                if (VARIABLE.Name.Equals(name))
                {
                    
                }
            }

            await OnGetAsync();

        }

        public void test1()
        {
            foreach (var VARIABLE in _Users)
            {
                listItems.Add(new SelectListItem
                {
                    Text = VARIABLE.Name,
                    Value = VARIABLE.Name
                    
                   
                });
            }
          
        }
    }
}
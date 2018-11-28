

using Platibus.Web.DataServices.Models.User;
using Platibus.Web.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platibus.Web.DataServices.Models.Shift;
using Platibus.Web.DataServices.Models.WorkSchedule;

namespace Platibus.Web.Acquaintance.IDataServices
{
    public interface IUserDataService
    {
        Task<Response> CreateUser(User user);
        Task<User> GetUserById(Guid id);
        /// <summary>
        /// Returns a single page from the list of users.
        /// </summary>
        /// <param name="page">The page index to be shown from the list.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <returns>A list of users on the specific page.</returns>
        Task<IEnumerable<User>> ListUsersAsync(int page, int pageSize);

        Task<Response> UpdateUserById(Guid id, User user);
        Task<Response> DeleteUserById(Guid id);
    }


    public interface IShift
    {
        
         DateTime ShiftStart { get; set; }
         DateTime ShiftEnd { get; set; }
    }


    public interface IShiftDataService
    {
        Task<Response> CreateShift(Shift shift);
        Task<IEnumerable<Shift>> ListUsersAsync();
        Task<Response> CreateManyShifts(ListOfShifts shifts);
        Task<Response> AddEmployeeToShift(AssignUserToShift assignUserToShift);
        Task<Shift> GetShiftById(Guid id);
        Task<Response> DeleteShiftById(Guid id);
    }


    public interface IWorkScheduleDataService
    {
        Task<IEnumerable<AllShiftsWithEmployees>> GetAllWorkSchedules();
        Task<IEnumerable<UserShiftDetailed>> GetUserShiftDetailed();
    }
}
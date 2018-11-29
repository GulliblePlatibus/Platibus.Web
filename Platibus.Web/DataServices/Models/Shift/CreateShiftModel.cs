using System;

namespace Platibus.Web.DataServices.Models.User
{
    public class CreateShiftModel
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
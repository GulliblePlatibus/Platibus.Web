using System;

namespace Platibus.Web.DataServices.Models.User
{
    public class UpdateShiftModel
    {
        public Guid ShiftId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
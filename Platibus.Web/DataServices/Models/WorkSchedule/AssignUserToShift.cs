using System;

namespace Platibus.Web.DataServices.Models.WorkSchedule
{
    public class AssignUserToShift
    {
        public Guid id { get; set; }
        public Guid EmployeeOnShift { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
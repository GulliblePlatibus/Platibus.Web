using System;

namespace Platibus.Web.DataServices.Models.WorkSchedule
{
    public class AllShiftsWithEmployees
    {
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid Id { get; set; }
        public Guid EmployeeOnShift { get; set; }
    }
}
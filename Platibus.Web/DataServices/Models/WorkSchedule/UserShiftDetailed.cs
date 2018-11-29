using System;


namespace Platibus.Web.DataServices.Models.WorkSchedule
{
    public class UserShiftDetailed 
    {
        public string name { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public Guid Id { get; set; }
        public Guid shiftid { get; set; }
    }
}
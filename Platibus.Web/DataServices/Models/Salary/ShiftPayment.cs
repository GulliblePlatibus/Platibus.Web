using System;
using Platibus.Web.DataServices.Models.Salary.Entities;

namespace Platibus.Web.DataServices.Models.Salary
{
    public class ShiftPayment
    {
        public Guid UserId { get; set; }
        public double Seniority { get; set; }
        public double BaseWage { get; set; }
        public Shift.Shift Shift { get; set; }
        public SortedWorkHours SortedWorkHours { get; set; }
        public double TotalPayment { get; set; }
    }
}
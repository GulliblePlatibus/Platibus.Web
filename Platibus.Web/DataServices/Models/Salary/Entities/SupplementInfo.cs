using System;
using System.Collections.Generic;

namespace Platibus.Web.DataServices.Models.Salary.Entities
{
    public class SupplementInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Supplement Supplement { get; set; }
        public List<DayOfWeek> DayOfSupplement { get; set; }
        public List<HourInfo> TimeRanges { get; set; }
        
        public double Value { get; set; }

    }
}
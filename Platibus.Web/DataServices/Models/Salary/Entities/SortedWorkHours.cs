using System.Collections.Generic;

namespace Platibus.Web.DataServices.Models.Salary.Entities
{
    public class SortedWorkHours
    {
        public double Hours { get; set; }
        public List<SupplementInfo> SupplementHours { get; set; }
    }
}
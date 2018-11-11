using System;
using Microsoft.AspNetCore.Mvc;
using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.DataServices.Models.Shift
{
    [ModelBinder]
    public class Shift : IShift
    {
        [BindProperty]
        public Guid id { get; set; }
        
        public Guid EmployeeOnShift { get; set; }
        
        [BindProperty]
        public DateTime ShiftStart { get; set; }
        [BindProperty]
        public DateTime ShiftEnd { get; set; }
    }
}
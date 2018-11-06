using System;
using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.DataServices.Models.Shift
{
    public class Shift : IShift
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
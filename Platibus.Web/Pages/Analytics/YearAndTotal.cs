using System;

namespace Platibus.Web.Pages.Analytics
{
    public class YearAndTotal
    {
        public DateTime _time { get; set; }
        public double _total { get; set; }
        
        public YearAndTotal(DateTime time, double total)
        {
            _time = time;
            _total = total;
        }
    }
}
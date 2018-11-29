namespace Platibus.Web.Pages.Employee.Entities
{
    public class PieDataObject
    {
        public double Supplement { get; set; }
        public double Value { get; set; }
        public string Color { get; set; }
        public string Highlight { get; set; }
        public string Label { get; set; }

        public double TotalPayment
        {
            get { return Value * Supplement; }
        }
    } 
}
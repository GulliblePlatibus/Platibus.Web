using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Acquaintance.IDataServices;
using Platibus.Web.DataServices.Models.Salary;
using Platibus.Web.DataServices.Models.Salary.Entities;
using Platibus.Web.Helpers;
using Platibus.Web.Pages.Employee.Entities;

namespace Platibus.Web.Pages.Employee
{
    public class OverviewModel : PageModel
    {
        private readonly IUserDataService _userDataService;

        [BindProperty]
        public DateTime FromDate { get; set; }
        [BindProperty]
        public DateTime ToDate { get; set; }
        
        public List<ShiftPayment> WorkHourList { get; set; }
        public List<ShiftPayment> PayList { get; set; }

        [BindProperty]
        public string[] BarLabels { get; set; }
        public double[] BarValues { get; set; }
        
        public double TotalHours { get; set; }
        public PieDataObject[] PieDataSet { get; set; }

        public DataPeriod DataPeriod { get; set; }
        
        public OverviewModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;

        }

        public async Task OnGetAsync()
        {
            //SetDefaultTimePeriod
            DataPeriod = DataPeriod.Week;
            SetDefaultTimePeriod();
            
            //Search for user salary the last 7 days
            var result = await _userDataService.GetSalaryForUserPagedAsync(
                HttpContext.SubjectId(),
                FromDate,
                ToDate);
            
            //Sort data labels
            SortBarDataSet(result, FromDate, ToDate);
            SortPieDataSet(result);
            
            WorkHourList = result;
            PayList = result;
        }

        private void SortBarDataSet(List<ShiftPayment> data, DateTime fromDate, DateTime toDate)
        {
            var list = new List<DataObject>();
            
            switch (DataPeriod)
            {
                case DataPeriod.Week:

                    while (fromDate.CompareTo(toDate) < 0)
                    {
                        //Get data elements that match this day
                        var dayOfWeek = fromDate.DayOfWeek;
                        var dataObject = new DataObject
                        {
                            Label = dayOfWeek.ToString()
                        };
                        
                        foreach (var shiftPayment in data)
                        {
                            if (shiftPayment.Shift.ShiftStart.DayOfWeek.Equals(dayOfWeek))
                            {
                                dataObject.Value += shiftPayment.SortedWorkHours.Hours;
                            }
                        }
                        
                        list.Add(dataObject);

                        fromDate = fromDate.AddHours(24);
                    }
                    break;
            }

            var barLabels = new List<string>();
            var barValues = new List<double>();

            foreach (var dataObject in list)
            {
                barLabels.Add(dataObject.Label);
                barValues.Add(dataObject.Value);
            }

            BarLabels = barLabels.ToArray();
            BarValues = barValues.ToArray();

        }

        private void SortPieDataSet(List<ShiftPayment> data)
        {
            var list = new List<PieDataObject>();
            
            var totalHours = new PieDataObject
            {
                Value = 0,
                Color = "#F7464A",
                Highlight = "#FF5A5E",
                Label = "Basewage"
            };
            if (data.Any())
            {
                totalHours.Supplement = data.First().BaseWage;
            }
            list.Add(totalHours);
            
            var hashSet = new HashSet<SupplementInfo>();
            var dictionary = new Dictionary<Guid, double>();
            
            foreach (var shiftPayment in data)
            {
                //Add the total hours to totalHoursPieDataObject
                totalHours.Value += shiftPayment.SortedWorkHours.Hours;
                
                //Check if there are any supplements
                foreach (var supplementInfo in shiftPayment.SortedWorkHours.SupplementHours)
                {
                    hashSet.Add(supplementInfo);
                    if (dictionary.ContainsKey(supplementInfo.Id))
                    {
                        dictionary[supplementInfo.Id] += supplementInfo.Value;
                    }
                    else
                    {
                        dictionary.Add(supplementInfo.Id, supplementInfo.Value);
                    }
                }

                foreach (var d in dictionary)
                {
                    var supplement = hashSet.FirstOrDefault(x => x.Id.Equals(d.Key));

                    list.Add(new PieDataObject
                    {
                        Label = supplement.Description,
                        Color = "#333333",
                        Highlight = "#444444",
                        Value = d.Value,
                        Supplement = supplement.Supplement.Amount
                    });
                }
            }
            PieDataSet = list.ToArray();

        }
        
        

        private void SetDefaultTimePeriod()
        {
            switch (DataPeriod)
            {
                case DataPeriod.Week:
                    ToDate = DateTime.Now;
                    FromDate = DateTime.Now.Subtract(new TimeSpan(24 * 7, 0, 0));
                    break;
                case DataPeriod.ThisWeek:
                    break;
                case DataPeriod.Month:
                    break;
                case DataPeriod.ThisMonth:
                    break;
                case DataPeriod.Quarter:
                    break;
                case DataPeriod.HalfYear:
                    break;
                case DataPeriod.Year:
                    break;
            }
        }
    }
}
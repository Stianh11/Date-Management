using Date_Management_Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Date_Management_Project.ViewModels
{
    public class DateRangeViewModel
    {
        public DateRangeViewModel()
        {
            From = DateTime.Today;
            To = DateTime.Today;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string CountryCode { get; set; }

        // separate flags for each weekend day
        public bool IncludeSaturday { get; set; }
        public bool IncludeSunday { get; set; }

        // user picks which holidays they will work
        public List<int> WorkOnHolidayIds { get; set; } = new();

        // filled in controller, used to render checkbox list
        public List<Holiday> IncludedHolidays { get; set; } = new();

        public int? WorkingDays { get; set; }

        public List<SelectListItem> Countries { get; set; } = new()
        {
            new SelectListItem("Norway", "NO"),
            new SelectListItem("Sweden", "SE"),
            new SelectListItem("United Kingdom", "GB"),
            new SelectListItem("United States", "US"),
        };
    }
}

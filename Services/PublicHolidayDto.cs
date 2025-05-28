
namespace Date_Management_Project.Services
{
    /// <summary>
    /// Minimal DTO matching the fields we care about from https://date.nager.at.
    /// </summary>
    public class PublicHolidayDto
    {
        public DateTime Date { get; set; }
        public string LocalName { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public bool Fixed { get; set; }
        public bool Global { get; set; }
        public string[] Counties { get; set; }
        public int? LaunchYear { get; set; }
        public string Type { get; set; }
    }
}

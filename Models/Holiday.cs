namespace Date_Management_Project.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; } = "";
        public string CountryCode { get; set; } = "";
    }
}

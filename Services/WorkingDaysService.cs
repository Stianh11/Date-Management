using Date_Management_Project.Data;
using Date_Management_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Date_Management_Project.Services
{
    public class WorkingDaysService
    {
        private readonly AppDbContext _db;
        private readonly IHolidayApiService _api;

        public WorkingDaysService(AppDbContext db, IHolidayApiService api)
        {
            _db = db;
            _api = api;
        }

        public async Task<int> CountAsync(
            DateTime start,
            DateTime end,
            string countryCode,
            bool includeSaturday,
            bool includeSunday,
            IEnumerable<int>? workOnHolidayIds = null
        )
        {
            if (end < start)
                throw new ArgumentException("End must be on or after start date.");

            // 1) fetch public holidays once per year
            var years = Enumerable.Range(start.Year, end.Year - start.Year + 1);
            foreach (var year in years)
            {
                bool haveAny = await _db.Holidays
                  .AnyAsync(h => h.CountryCode == countryCode && h.StartDate.Year == year);
                if (haveAny) continue;

                var pubs = await _api.GetHolidaysAsync(year, countryCode);
                foreach (var ph in pubs)
                {
                    _db.Holidays.Add(new Holiday
                    {
                        StartDate = ph.Date,
                        EndDate = ph.Date,
                        Name = ph.Name,
                        CountryCode = countryCode
                    });
                }
                await _db.SaveChangesAsync();
            }

            // 2) load all in-range holidays
            var ranges = await _db.Holidays
              .Where(h =>
                  h.CountryCode == countryCode &&
                  h.StartDate <= end.Date &&
                  h.EndDate >= start.Date)
              .ToListAsync();

            // 3) count
            int count = 0;
            for (var day = start.Date; day <= end.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday && !includeSaturday) continue;
                if (day.DayOfWeek == DayOfWeek.Sunday && !includeSunday) continue;

                var hol = ranges.FirstOrDefault(h => day >= h.StartDate && day <= h.EndDate);
                if (hol != null)
                {
                    // if user chose to work this holiday, count it; otherwise skip
                    if (workOnHolidayIds?.Contains(hol.Id) == true)
                    { /* count */ }
                    else
                        continue;
                }

                count++;
            }

            return count;
        }

        public async Task<List<Holiday>> GetHolidaysInRangeAsync(
            DateTime start, DateTime end, string countryCode)
        {
            return await _db.Holidays
              .Where(h =>
                  h.CountryCode == countryCode &&
                  h.StartDate <= end.Date &&
                  h.EndDate >= start.Date)
              .ToListAsync();
        }
    }
}

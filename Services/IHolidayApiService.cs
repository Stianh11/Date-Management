using System.Collections.Generic;
using System.Threading.Tasks;

namespace Date_Management_Project.Services
{
    public interface IHolidayApiService
    {
        /// <summary>
        /// fetches the list of public holidays for a given year and country.
        /// </summary>
        Task<List<PublicHolidayDto>> GetHolidaysAsync(int year, string countryCode);
    }
}

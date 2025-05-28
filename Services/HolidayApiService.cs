
using Date_Management_Project.Services;

public class HolidayApiService : IHolidayApiService
{
    private readonly HttpClient _client;
    public HolidayApiService(HttpClient client) => _client = client;

    public async Task<List<PublicHolidayDto>> GetHolidaysAsync(int year, string countryCode)
    {
        // Nager API v3 URL
        var url = $"api/v3/PublicHolidays/{year}/{countryCode}";

        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return new List<PublicHolidayDto>();
        }

        //safely read JSON (or get null)
        var holidays = await response.Content
                                  .ReadFromJsonAsync<List<PublicHolidayDto>>();
        return holidays ?? new List<PublicHolidayDto>();
    }
}

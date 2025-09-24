using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Config;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class CityService : ICityService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public CityService(HttpClient httpClient, ApiSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }


        public async Task<List<string>> GetCitiesAsync(string country)
        {
            var json = JsonSerializer.Serialize(new { country });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_settings.CitiesApiUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var citiesResponse = JsonSerializer.Deserialize<CitiesResponse>(result);

            return citiesResponse?.data ?? new List<string>();
        }
    }
}

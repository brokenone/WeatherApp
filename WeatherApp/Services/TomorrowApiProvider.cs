using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Config;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class TomorrowApiProvider : IWeatherProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public TomorrowApiProvider(HttpClient httpClient, ApiSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<WeatherResult?> GetWeatherAsync(string city)
        {
            var url = $"{_settings.TomorrowApiUrl}?location={city}&apikey={_settings.TomorrowApiKey}";
            var response = await _httpClient.GetFromJsonAsync<TomorrowApiResponse>(url);
            if (response == null || response.data?.values == null) return null;

            return new WeatherResult
            {
                Source = "OpenWeatherApi",
                Description = "N/A",
                TemperatureC = response.data.values.temperature
            };
        }
    }
}

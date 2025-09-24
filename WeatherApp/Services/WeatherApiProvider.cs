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
    public class WeatherApiProvider : IWeatherProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public WeatherApiProvider(HttpClient httpClient, ApiSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<WeatherResult?> GetWeatherAsync(string city)
        {
            var url = $"{_settings.WeatherApiUrl}?key={_settings.WeatherApiKey}&q={city}";
            var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url);
            if (response == null || response.current == null) return null;

            return new WeatherResult
            {
                Source = "OpenWeatherApi",
                Description = response.current.condition?.text ?? "N/A",
                TemperatureC = response.current.temp_c
            };
        }
    }
}

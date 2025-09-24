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
    public class OpenWeatherProvider : IWeatherProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public OpenWeatherProvider(HttpClient httpClient, ApiSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<WeatherResult?> GetWeatherAsync(string city)
        {
            var url = $"{_settings.OpenWeatherApiUrl}?q={city}&appid={_settings.OpenWeatherApiKey}&units=metric";
            var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);
            if(response == null || response.main == null) return null;

            return new WeatherResult
            {
                Source = "OpenWeatherApi",
                Description = response.weather?.FirstOrDefault()?.description ?? "N/A",
                TemperatureC = response.main.temp
            };
        }
    }
}

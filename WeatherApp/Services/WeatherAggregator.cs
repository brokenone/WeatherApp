using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherAggregator : IWeatherService
    {
        private readonly IEnumerable<IWeatherProvider> _providers;

        public WeatherAggregator(IEnumerable<IWeatherProvider> providers)
        {
            _providers = providers;
        }

        public async Task<IEnumerable<WeatherResult>> GetWeatherAsync(string city)
        {
            var tasks = _providers.Select(p => p.GetWeatherAsync(city));
            var results = await Task.WhenAll(tasks);
            return results.Where(r => r != null)!;
        }
    }
}

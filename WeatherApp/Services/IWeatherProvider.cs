using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherProvider
    {
        Task<WeatherResult?> GetWeatherAsync(string city);
    }
}

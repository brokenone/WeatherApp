using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Config
{
    public class ApiSettings
    {
        public string OpenWeatherApiUrl { get; set; }
        public string WeatherApiUrl { get; set; }
        public string TomorrowApiUrl { get; set; }
        public string CitiesApiUrl { get; set; }

        public string OpenWeatherApiKey { get; set; }
        public string WeatherApiKey { get; set; }
        public string TomorrowApiKey { get; set; }
    }
}

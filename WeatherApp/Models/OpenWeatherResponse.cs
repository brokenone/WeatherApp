using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class OpenWeatherResponse
    {
        public List<WeatherDescription> weather {  get; set; }
        public MainInfo main {  get; set; }

        public class WeatherDescription
        {
            public string description { get; set; }
        }

        public class MainInfo
        {
            public double temp { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherResult
    {
        public string Source { get; set; }
        public string Description { get; set; }
        public double TemperatureC {  get; set; }
    }
}

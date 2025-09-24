using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class CitiesResponse
    {
        public bool Error { get; set; }
        public List<string> data { get; set; }
    }
}

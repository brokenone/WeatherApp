using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherApiResponse
    {
        public Current current {  get; set; }

        public class Current
        {
            public double temp_c {  get; set; }
            public Condition condition { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WeatherApp.Models
{
    public class TomorrowApiResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public Values values { get; set; }
        }

        public class Values
        {
            public double temperature { get; set; }
        }
    }
}

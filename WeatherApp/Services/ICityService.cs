using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public interface ICityService
    {
        Task<List<string>> GetCitiesAsync(string country);
    }
}

using System;
using System.IO;
using WeatherApp.Config;
using WeatherApp.Models;
using WeatherApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;

namespace WeatherApp
{
    public class Program
    {
        static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string enviroment = config["Enviroment"] ?? "Development";
            bool useDI = config.GetValue<bool>("useDI");
            var apiSettings = config.GetSection("ApiSettings").Get<ApiSettings>();

            Console.WriteLine($"Start Enviroment: {enviroment}");

            ICityService cityService;
            IWeatherService weatherService;

            if (useDI)
            {
                var services = new ServiceCollection();
                services.AddSingleton(apiSettings);
                services.AddHttpClient<IWeatherProvider, OpenWeatherProvider>((provider, httpClient) =>
                    new OpenWeatherProvider(httpClient, provider.GetRequiredService<ApiSettings>()));

                services.AddHttpClient<IWeatherProvider, WeatherApiProvider>((provider, httpClient) =>
                    new WeatherApiProvider(httpClient, provider.GetRequiredService<ApiSettings>()));

                services.AddHttpClient<IWeatherProvider, TomorrowApiProvider>((provider, httpClient) =>
                    new TomorrowApiProvider(httpClient, provider.GetRequiredService<ApiSettings>()));

                services.AddSingleton<IWeatherService, WeatherAggregator>();

                services.AddHttpClient<ICityService, CityService>((provider, httpClient) =>
                    new CityService(httpClient, provider.GetRequiredService<ApiSettings>()));

                var provider = services.BuildServiceProvider();

                cityService = provider.GetRequiredService<ICityService>();
                weatherService = provider.GetRequiredService<IWeatherService>();

                Console.WriteLine("Using Dependency Injection");
                Console.WriteLine();
            }
            else
            {
                var sharedHttp = new HttpClient();
                cityService = new CityService(sharedHttp, apiSettings);
                var providers = new List<IWeatherProvider> {
                    new OpenWeatherProvider(sharedHttp, apiSettings),
                    new WeatherApiProvider(sharedHttp, apiSettings),
                    new TomorrowApiProvider(sharedHttp, apiSettings)
                };

                weatherService = new WeatherAggregator(providers);
                Console.WriteLine("Using Manual Object Creation");
                Console.WriteLine();
            }

            Console.WriteLine("Enter Your Country");

            string country = Console.ReadLine();

            var cities = await cityService.GetCitiesAsync(country);

            if (cities.Count == 0)
            {
                Console.WriteLine("Cities Not Found");
                return;
            }

            Console.WriteLine($"Found {cities.Count} cities");

            for (int i = 0; i < cities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cities[i]}");
            }

            //Console.WriteLine("Enter City"); 

            //string city = Console.ReadLine(); 

            //var weathers = await weatherService.GetWeatherAsync(city); 

            //if (!weathers.Any()) 
            //{ 
            //    Console.WriteLine("No Weather Data Found"); 
            //}

            //Console.WriteLine($"Weather for {city}:"); 

            //foreach (var w in weathers) 
            //{ 
            //    Console.WriteLine($"{w.Source}: {w.Description}, {w.TemperatureC}°C"); 
            //}

            var openWeatherResults = new List<(string city, WeatherResult weather)>();

            for (int i = 0; i < 2; i++)
            {
                var city = cities[i];

                Console.WriteLine();
                Console.WriteLine($"=== {city} ===");

                var weathers = await weatherService.GetWeatherAsync(city);

                if (!weathers.Any())
                {
                    Console.WriteLine("No Weather Data Found");
                }
                else
                {
                    Console.WriteLine($"{"Source",-15} | {"Condition",-15} | Temp");
                    Console.WriteLine(new string('-', 45));

                    foreach (var w in weathers)
                    {
                        if (w.Source == "OpenWeatherApi")
                        {
                            openWeatherResults.Add((city, w));
                        }

                        Console.WriteLine($"{w.Source,-15} | {w.Description,-15} | {w.TemperatureC}°C");
                    }

                    Console.WriteLine(new string('-', 20)); Console.WriteLine();
                }

                // Request Delay
                await Task.Delay(1000);
            }

            if (openWeatherResults.Any())
            {
                var coldest = openWeatherResults.OrderBy(r => r.weather.TemperatureC).First();
                var hottest = openWeatherResults.OrderByDescending(r => r.weather.TemperatureC).First();

                Console.WriteLine();
                Console.WriteLine("=== Summary ===");
                Console.WriteLine($"Coldest: {coldest.city} with {coldest.weather.TemperatureC}°C (OpenWeatherApi)");
                Console.WriteLine($"Hottest: {hottest.city} with {hottest.weather.TemperatureC}°C (OpenWeatherApi)");
            }
        }
    }
}
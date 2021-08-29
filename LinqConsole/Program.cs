using Newtonsoft.Json;
using System;
using System.Linq;

namespace LinqConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var param = new WeatherForecastQuery()
            {
                DateMin = DateTime.Now,
                DateMax = DateTime.Now.AddDays(3)//,
                //Summary = "Cool"
            };

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).AsQueryable();
            result = result.WhereFilter(param);

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}

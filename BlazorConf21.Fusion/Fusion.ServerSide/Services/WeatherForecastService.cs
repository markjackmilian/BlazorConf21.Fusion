using System;
using System.Linq;
using System.Threading.Tasks;
using Fusion.ServerSide.Data;
using Stl.Async;
using Stl.Fusion;

namespace Fusion.ServerSide.Services
{
    [ComputeService]
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [ComputeMethod(AutoInvalidateTime = 1)]
        public virtual Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
        
        [ComputeMethod]
        public virtual Task<WeatherForecast[]> GetForecastAsync(string city)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Today.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

        public Task UpdateForecastForCity(string city)
        {
            using (Computed.Invalidate())
            {
                this.GetForecastAsync(city).Ignore();
            }

            return Task.CompletedTask;
        }
    }
}
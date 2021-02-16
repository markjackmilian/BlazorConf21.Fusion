using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stl.Async;
using Stl.Fusion;

namespace Fusion.ApiCache.Services
{
    [ComputeService]
    public class WeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        [ComputeMethod]
        public virtual async Task<IEnumerable<WeatherForecast>> Get()
        {
            await Task.Delay(1000);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
        
        [ComputeMethod]
        public virtual async Task<IEnumerable<WeatherForecast>> Get(string city)
        {
            await Task.Delay(1000);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        /// <summary>
        /// In this example this just invalidate the state
        /// </summary>
        public void AddNewForecast()
        {
            using (var context = Computed.Invalidate())
            {
                this.Get().Ignore();
            }
        }
        
        /// <summary>
        /// In this example this just invalidate the state
        /// </summary>
        public void AddNewForecast(string city)
        {
            using (var context = Computed.Invalidate())
            {
                this.Get(city).Ignore();
            }
        }
    }
}
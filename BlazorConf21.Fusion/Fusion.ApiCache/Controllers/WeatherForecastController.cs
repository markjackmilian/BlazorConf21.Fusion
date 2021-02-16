using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion.ApiCache.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stl.Fusion;

namespace Fusion.ApiCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _service;

        public WeatherForecastController(WeatherService service )
        {
            this._service = service;
        }

        [HttpGet]
        public virtual Task<IEnumerable<WeatherForecast>> Get()
        {
            return this._service.Get();
        }
        
        [HttpGet]
        [Route("getcity")]
        public Task<IEnumerable<WeatherForecast>> Get(string city)
        {
            return this._service.Get(city);
        }

        [HttpPost]
        public Task Add()
        {
            this._service.AddNewForecast();
            return Task.CompletedTask;
        }
        
        [HttpPost]
        [Route("addcity")]
        public Task Add(string city)
        {
            this._service.AddNewForecast(city);
            return Task.CompletedTask;
        }
    }
}
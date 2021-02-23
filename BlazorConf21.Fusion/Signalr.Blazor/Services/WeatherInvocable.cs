using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.AspNetCore.SignalR;
using Signalr.Blazor.Data;
using Signalr.Blazor.Hubs;

namespace Signalr.Blazor.Services
{
    public class WeatherInvocable : IInvocable
    {
        private readonly WeatherForecastService _weatherForecastService;
        private readonly IHubContext<WeatherHub> _weathHubContext;

        public WeatherInvocable(WeatherForecastService weatherForecastService, IHubContext<WeatherHub> weathHubContext)
        {
            this._weatherForecastService = weatherForecastService;
            this._weathHubContext = weathHubContext;
        }
        
        public async Task Invoke()
        {
            var forecast = await this._weatherForecastService.GetForecastAsync(DateTime.Today);
            await this._weathHubContext.Clients.All.SendAsync("NewForecast", forecast);
        }
    }
}
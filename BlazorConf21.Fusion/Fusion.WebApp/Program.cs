using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using Fusion.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stl.DependencyInjection;
using Stl.Fusion;
using Stl.Fusion.Client;

namespace Fusion.WebApp
{
    public class Program
    {
        public const string ClientSideScope = nameof(ClientSideScope);

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            ConfigureServices(builder.Services, builder);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            
            var host = builder.Build();
            var runTask = host.RunAsync();
            
            // Blazor host doesn't start IHostedService-s by default,
            // so let's start them "manually" here
            Task.Run(async () => {
                var hostedServices = host.Services.GetService<IEnumerable<IHostedService>>();
                foreach (var hostedService in hostedServices)
                    await hostedService.StartAsync(default);
            });

            await runTask;
        }

        private static void ConfigureServices(IServiceCollection services, WebAssemblyHostBuilder builder)
        {
            var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
            var apiBaseUri = new Uri("https://localhost:5001");
            
            var fusion = services.AddFusion();
            var fusionClient = fusion.AddRestEaseClient(
                (c, o) => {
                    o.BaseUri = baseUri;
                    o.MessageLogLevel = LogLevel.Information;
                }).ConfigureHttpClientFactory(
                (c, name, o) => {
                    var isFusionClient = (name ?? "").StartsWith("Stl.Fusion");
                    var clientBaseUri = isFusionClient ? baseUri : apiBaseUri;
                    o.HttpClientActions.Add(client => client.BaseAddress = clientBaseUri);
                });
            
            services.UseAttributeScanner(ClientSideScope).AddServicesFrom(Assembly.GetExecutingAssembly());
            services.UseAttributeScanner(ClientSideScope).AddServicesFrom(typeof(ISumService).Assembly);

            services.AddSingleton(c => new UpdateDelayer.Options() {
                // Default update delayer options 
                Delay = TimeSpan.FromSeconds(0.1),
            });
            
            ConfigureSharedServices(services);
        }

        private static void ConfigureSharedServices(IServiceCollection services)
        {
            // This method registers services marked with any of ServiceAttributeBase descendants, including:
            // [Service], [ComputeService], [RestEaseReplicaService], [LiveStateUpdater]
            services.UseAttributeScanner().AddServicesFrom(Assembly.GetExecutingAssembly());
        }
    }
}
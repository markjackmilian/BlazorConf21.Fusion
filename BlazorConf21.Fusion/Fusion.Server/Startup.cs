using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fusion.Server.Data;
using Stl.DependencyInjection;
using Stl.Fusion;
using Stl.Fusion.Bridge;
using Stl.Fusion.Client;
using Stl.Fusion.Server;

namespace Fusion.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Fusion services
            services.AddSingleton(new Publisher.Options() { 
                Id = "p-<uniqueId>", 
            });
            var fusion = services.AddFusion();
            var fusionServer = fusion.AddWebServer();
            var fusionClient = fusion.AddRestEaseClient();
            
            services.AddRouting();
            // Register Replica Service controllers
            services.AddMvc().AddApplicationPart(Assembly.GetExecutingAssembly());
            services.AddServerSideBlazor();
            
            // This method registers services marked with any of ServiceAttributeBase descendants, including:
            // [Service], [ComputeService], [RestEaseReplicaService], [LiveStateUpdater]
            services.UseAttributeScanner().AddServicesFrom(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseWebSockets(new WebSocketOptions() {
                KeepAliveInterval = TimeSpan.FromSeconds(30),
            });
            app.UseFusionSession();
            

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFusionWebSocketServer();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
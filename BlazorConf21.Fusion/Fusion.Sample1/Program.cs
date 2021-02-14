using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Stl.Fusion;

namespace Fusion.Sample1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddFusion(f => f.AddComputeService<CounterService>())
                .BuildServiceProvider();
            
            var service = services.GetRequiredService<CounterService>();

            var res = await service.GetCounterAsync();
            Console.WriteLine(res.Item1);
            var res1 = await service.GetCounterAsync();
            Console.WriteLine(res1.Item1);

            await service.IncrementCounterAsync();
            
            var res2 = await service.GetCounterAsync();
            Console.WriteLine(res2.Item1);
        }
    }
}
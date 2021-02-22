using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Signalr.Blazor.Hubs;

namespace Signalr.Blazor.Data
{
    public class CounterService
    {
        private readonly IHubContext<TestHub> _testHub;
        private static int _counter = 0;

        public CounterService(IHubContext<TestHub> testHub)
        {
            this._testHub = testHub;
        }

        public async Task AddOne()
        {
            ++_counter;
            await this._testHub.Clients.All.SendAsync("IncrementCount", _counter);
        }

        public int GetCount()
        {
            return _counter;
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Signalr.Blazor.Hubs
{
    public class TestHub : Hub
    {
        /// <summary>
        /// Notify last count state
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task SendCount(int count)
        {
            await Clients.All.SendAsync("IncrementCount", count);
        }
    }
}
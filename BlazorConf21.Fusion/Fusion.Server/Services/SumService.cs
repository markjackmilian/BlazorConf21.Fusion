using System.Threading.Tasks;
using Fusion.Shared;
using Stl.Async;
using Stl.Fusion;

namespace Fusion.Server.Services
{
    [ComputeService(typeof(ISumService))]
    public class SumService : ISumService
    {
        private static int _count = 5;

        public virtual async Task<int> GetValue()
        {
            await Task.Delay(1000);
            return _count;
        }

        public Task AddOne()
        {
            _count += 1;
            using (Computed.Invalidate())
            {
                this.GetValue().Ignore();
            }

            return Task.CompletedTask;
        }
    }
}
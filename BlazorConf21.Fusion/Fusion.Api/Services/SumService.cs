using System.Threading;
using System.Threading.Tasks;
using Fusion.Shared;
using mjm.nethelpers;
using Stl.Async;
using Stl.Fusion;

namespace Fusion.Api.Services
{
    [ComputeService(typeof(ISumService))]
    public class SumService : ISumService
    {
        private static int _count;

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
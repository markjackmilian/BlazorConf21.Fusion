using System.Threading.Tasks;
using Stl.Async;
using Stl.Fusion;

namespace Fusion.ServerSide.Services
{
    [ComputeService]
    public class CounterService
    {
        private static int _count = 0;

        [ComputeMethod]
        public virtual Task<int> GetValue()
        {
            return Task.FromResult(_count);
        }

        public Task AddOne()
        {
            ++_count;
            using (Computed.Invalidate())
            {
                this.GetValue().Ignore();
            }
            return Task.CompletedTask;
        }
    }
}
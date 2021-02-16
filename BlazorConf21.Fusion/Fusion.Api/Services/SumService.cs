using System.Threading;
using System.Threading.Tasks;
using Fusion.Shared;
using mjm.nethelpers;
using Stl.Async;
using Stl.Fusion;

namespace Fusion.Api.Services
{
    [ComputeService]
    public class SumService : ISumService
    {
        private readonly ThreadSafe<int> _count = ThreadSafe.On(0);

        public Task<int> GetValue(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(this._count.Use(i => i));
        }

        public Task Add(int value, CancellationToken cancellationToken = default)
        {
            this._count.Use(i => ++i);
            using var context = Computed.Invalidate();
            this.GetValue(cancellationToken).Ignore();

            return Task.CompletedTask;
        }
    }
}
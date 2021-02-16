using System;
using System.Threading;
using System.Threading.Tasks;
using Stl.Fusion;

namespace Fusion.Shared
{
    public interface ISumService
    {
        // Task ResetAsync(CancellationToken cancellationToken = default);
        // Task AccumulateAsync(double value, CancellationToken cancellationToken = default);

        [ComputeMethod]
        Task<int> GetValue(CancellationToken cancellationToken = default);
        [ComputeMethod]
        Task Add(int value, CancellationToken cancellationToken = default);
    }
}
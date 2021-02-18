using System;
using System.Threading;
using System.Threading.Tasks;
using Stl.Fusion;

namespace Fusion.Shared
{
    public interface ISumService
    {
        [ComputeMethod]
        Task<int> GetValue();
        Task AddOne();
    }
}
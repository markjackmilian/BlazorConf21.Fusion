using System.Threading.Tasks;
using Fusion.Shared;
using RestEase;
using Stl.Fusion;
using Stl.Fusion.Client;

namespace Fusion.WebApp.Services
{
    [RestEaseReplicaService(typeof(ISumService), Scope = Program.ClientSideScope)]
    [BasePath("sum")]
    public interface ISumClient 
    {
        [ComputeMethod(KeepAliveTime = 1)]
        [Get("getvalue")]
        Task<int> GetValue();
        
        [Post("add")]
        Task AddOne();
    }
}
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
        [ComputeMethod]
        [Get("getvalue")]
        Task<string> GetValue();
        
        [Post("add")]
        Task AddOne();
    }
}
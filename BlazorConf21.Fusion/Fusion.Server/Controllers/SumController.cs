using System.Threading.Tasks;
using Fusion.Shared;
using Microsoft.AspNetCore.Mvc;
using Stl.Fusion.Server;

namespace Fusion.Server.Controllers
{
    [Route("[controller]")]
    [ApiController, JsonifyErrors]
    public class SumController : Controller
    {
        // GET
        private readonly ISumService _sumService;

        public SumController(ISumService sumService) => this._sumService = sumService;

        // Publish ensures GetAsync output is published if publication was requested by the client:
        // - Publication is created
        // - Its Id is shared in response header.
        [Publish]
        [HttpGet("getvalue")]
        public Task<int> Get()
        {
            return this._sumService.GetValue();
        }

        [HttpPost("add")]
        public Task Sum()
        {
            return this._sumService.AddOne();
        }
    }
}
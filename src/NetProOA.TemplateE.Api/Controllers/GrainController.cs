using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;
using NetProOA.Framework.DataTransfer;
using NetProOA.TemplateE.Application.Grains;
using NetProOA.TemplateE.Application.ViewModels.ExampleProduct;
using System;
using System.Threading.Tasks;
using Thrift.Protocol;

namespace NetProOA.TemplateE.Api.Controllers
{
    [Route("templatee/grains")]
    [ApiController]
    [AllowAnonymous]
    public class GrainController : ControllerBase
    {
        private readonly IClusterClient _clusterClient;
        private readonly ILogger<GrainController> _logger;

        public GrainController(IClusterClient clusterClient, ILogger<GrainController> logger)
        {
            _clusterClient = clusterClient;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Get()
        {
            var testGrain = _clusterClient.GetGrain<ITestGrain>(Guid.NewGuid().ToString());
            var res = await testGrain.TestMethod("sas");
            return Ok(res);
        }
    }
}

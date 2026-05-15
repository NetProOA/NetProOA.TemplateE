using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProOA.Framework.DataTransfer;
using NetProOA.Framework.ES;
using NetProOA.TemplateE.EsDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Api.Controllers
{
    [Route("templatee/[controller]")]
    [ApiController]
    public class ElasticsearchController : ControllerBase
    {
        private readonly IElasticsearchHighLevelRepository _elasticsearchHighLevelRepository;

        public ElasticsearchController(IElasticsearchHighLevelRepository elasticsearchHighLevelRepository)
        {
            _elasticsearchHighLevelRepository = elasticsearchHighLevelRepository;
        }

        [AllowAnonymous]
        [HttpGet("anonymous")]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        //[ApiVersion("2.0")]
        public virtual async Task<IActionResult> Anonymous()
        {
            _elasticsearchHighLevelRepository.BulkInsert(new List<EsExampleProduct> { new EsExampleProduct { Name = "CK2" } });

            return Ok(new OkResponse());
        }
    }
}

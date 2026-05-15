using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProOA.Framework.DataTransfer;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.Application.Queries;
using NetProOA.TemplateE.Application.Queries.Criteria;
using NetProOA.TemplateE.Application.ViewModels.ExampleProduct;
using Microsoft.AspNetCore.Authorization;
using NetProOA.Framework.Authorization.Interceptor;
using Autofac.Extras.DynamicProxy;
using NetProOA.TemplateE.Web.Extensions;
using Microsoft.Extensions.Logging;
using NetProOA.Framework.Authorization;
using NetProOA.Framework.Consts.PermissionCodes;

namespace NetProOA.TemplateE.Web.Controllers
{
    [Route("templatee/exampleProducts")]
    [ApiController]
    [Authorize]
    [Intercept(typeof(AuthorizationUserInterceptor))]
    public class ExampleProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IExampleProductQueries _exampleProductQueries;
        private readonly ILogger<ExampleProductController> _logger;

        public ExampleProductController(IMediator mediator, IExampleProductQueries exampleProductQueries, ILogger<ExampleProductController> logger)
        {
            _mediator = mediator;
            _exampleProductQueries = exampleProductQueries;
            _logger = logger;
        }

        [HttpPost("pages")]
        [ProducesResponseType(typeof(PageResponse<IList<ExampleProductDto>>), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Get([FromBody] ExampleProductQueryCriteria criteria)
        {
            var pagedQueryResult = await _exampleProductQueries.GetExampleProductPageListAsync(criteria);
            return Ok(new PageResponse<IList<ExampleProductDto>> { TotalCount = pagedQueryResult.TotalRecords, Data = pagedQueryResult.Data });
        }

        [HttpPost]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Create([FromBody] CreateExampleProductCommand command)
        {
            await _mediator.Send(command);
            return Ok(new OkResponse());
        }

        [HttpPut]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Modify([FromBody] ModifyExampleProductCommand command)
        {
            await _mediator.Send(command);
            return Ok(new OkResponse());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SucceedResponse<ExampleProductDto>), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Get([FromRoute] string id)
        {
            var user = HttpContext.GetCurrentUser();
            var exampleProduct = await _exampleProductQueries.GetExampleProductAsync(id);
            return Ok(new SucceedResponse<ExampleProductDto> { Data = exampleProduct });
        }

        [HttpDelete]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Delete([FromBody] DeleteExampleProductCommand command)
        {
            await _mediator.Send(command);
            return Ok(new OkResponse());
        }
    }
}



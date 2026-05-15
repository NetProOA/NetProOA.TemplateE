using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProOA.Framework.DataTransfer;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using NetProOA.TemplateE.Web.Controllers;
using NetProOA.Framework.EventBus;
using NetProOA.TemplateE.Application.IntegrationEvents;
using NetProOA.Framework.MinioSdk;
using System.IO;
using NetProOA.TemplateE.Domain.Repositories;
using System.Linq;
using NetProOA.Framework.Domain.Core;
using NetProOA.Framework.ES;
using System.Collections.Generic;
using NetProOA.TemplateE.EsDomain;
using Nest;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;

namespace NetProOA.TemplateE.Api.Controllers
{
    [Route("templatee/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ICustomMinioClient _customMinioClient;
        private readonly IExampleProductRepository _exampleProductRepository;
        private readonly IDbContext _dbContext;
    
        public ValuesController(ILogger<ValuesController> logger, IIntegrationEventService integrationEventService, ICustomMinioClient customMinioClient, IExampleProductRepository exampleProductRepository, IDbContext dbContext)
        {
            _logger = logger;
            _integrationEventService = integrationEventService;
            _customMinioClient = customMinioClient;
            _exampleProductRepository = exampleProductRepository;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet("anonymous")]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        //[ApiVersion("2.0")]
        public virtual async Task<IActionResult> Anonymous()
        {
            try
            {
                var datas = _exampleProductRepository.Queryable().ToList();
                var cachDatas = _dbContext.GetSqlSugarClient().Queryable<ExampleProduct>().WithCache().ToList();
                await _integrationEventService.SaveInegrationEventAsync(new CreatedExampleProductInegrationEvent { Name = "demo" });
                //throw new DivideByZeroException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok(new OkResponse());
        }

        [AllowAnonymous]
        [HttpGet("file")]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> File()
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "NetProOA.TemplateE.Api.deps.json");
                await _customMinioClient.PutObjectAsync("saas", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok(new OkResponse());
        }
    }
}

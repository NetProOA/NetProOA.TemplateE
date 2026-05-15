using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using NetProOA.Framework.DataTransfer;
using NetProOA.Framework.Mongo.Domain.Core;
using NetProOA.TemplateE.MongoDomain.AggregatesModel.ExampleProductAggregate;
using NetProOA.TemplateE.MongoDomain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using NetProOA.Framework.Data.MongoDB;

namespace NetProOA.TemplateE.Api.Controllers
{
    [Route("templatee/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private readonly ILogger<MongoController> _logger;
        private readonly MongoDomain.Repositories.IExampleProductRepository _exampleProductMongoRepository;
        private readonly Framework.Mongo.Domain.Core.IDBContext _mongoDbContext;

        public MongoController(IExampleProductRepository exampleProductMongoRepository, IDBContext mongoDbContext, ILogger<MongoController> logger)
        {
            _exampleProductMongoRepository = exampleProductMongoRepository;
            _mongoDbContext = mongoDbContext;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("anonymous")]
        [ProducesResponseType(typeof(OkResponse), StatusCodes.Status200OK)]
        //[ApiVersion("2.0")]
        public virtual async Task<IActionResult> Anonymous()
        {
            try
            {
                _mongoDbContext.BeginTransaction();

                //var datas = _exampleProductMongoRepository.GetQueryable().Where(p => p.CreateTime >= DateTime.Now.Date).Select(p => new { Name = p.Name, ItemName = p.Item.ItemName }).ToList();

                ////分组
                //var groupPipeline = _exampleProductMongoRepository.GetCollection().Aggregate().Group(p => p.Name, g => new { Name = g.Key, Count = g.Count() }).SortBy(p => p.Name);
                //var groupPipelineStr = groupPipeline.ToString();
                //var groupResult = await groupPipeline.ToListAsync();

                ////查询1=查询2
                //var queryPipeline = _exampleProductMongoRepository.GetCollection().AsQueryable().Where(p => p.CreateTime >= DateTime.Now.Date).Select(p => new { Name = p.Name, ItemName = p.Item.ItemName });
                //var queryPipelineStr = queryPipeline.ToString();  //SaaS_Template.ExampleProducts.Aggregate([{ "$match" : { "CreateTime" : { "$gte" : ISODate("2023-12-16T16:00:00Z") } } }, { "$project" : { "Name" : "$Name", "ItemName" : "$Item.ItemName", "_id" : 0 } }])
                //var queryPipeline1Datas = queryPipeline.ToList();
                //if (queryPipeline1Datas.Any())
                //{
                //    var itemName = queryPipeline1Datas[0].ItemName;
                //}
                ////查询1=查询2(适合做成动态返回指定字段)
                //var queryPipeline2Datas = _exampleProductMongoRepository.GetCollection().Aggregate()
                //    .AppendStage(new JsonPipelineStageDefinition<MongoExampleProduct, MongoExampleProduct>("{ \"$match\" : { \"CreateTime\" : { \"$gte\" : ISODate(\"2023-12-16T16:00:00Z\") } } }"))
                //    .AppendStage(new JsonPipelineStageDefinition<MongoExampleProduct, JObject>("{ \"$project\" : { \"Name\" : \"$Name\", \"ItemName\" : \"$Item.ItemName\", \"_id\" : 0 } }", new JObjectSerializer()))
                //    .ToList();
                //if (queryPipeline2Datas.Any())
                //{
                //    var itemName = queryPipeline2Datas[0]["ItemName"].ToString();
                //}

                await _exampleProductMongoRepository.AddAsync(new MongoExampleProduct
                {
                    Name = "CFC",
                    CreateTime = DateTime.Now,
                    Item = new Item { ItemName = "item1" }
                });
                await _mongoDbContext.CommitAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok(new OkResponse());
        }
    }
}

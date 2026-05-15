using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Polly;
using NetProOA.Framework.Data.MongoDB;
using NetProOA.Framework.Data.MongoDB.Impl;
using NetProOA.TemplateE.MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Infrastructure.Autofac.Extensions
{
    /// <summary>
    /// Mongodb 数据库迁移相关的扩展
    /// </summary>
    public static class MongoDBMigrationExtensions
    {
        /// <summary>
        /// 初始化MongoDB的Collection
        /// </summary>
        public static IHost MigrateMongoDB(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var maongoDbManager = services.GetService<MongoDBManager>();
                var logger = services.GetRequiredService<ILogger<MongoDBManager>>();

                try
                {
                    logger.LogInformation($"Migrating database");

                    var retry = Policy.Handle<MongoException>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8),
                        });

                    retry.Execute(() =>
                    {
                        //创建集合
                        maongoDbManager.CreateCollections(typeof(MongoDomain.AggregatesModel.ExampleProductAggregate.MongoExampleProduct).Assembly);
                    });

                    //支持分片
                    maongoDbManager.InitMongodbShard();

                    MongoDBClassMap.Map();
                    MongoDBContext.RegisterConventions();

                    logger.LogInformation($"Migrated database associated");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database");
                }
            }

            return webHost;
        }

    }
}

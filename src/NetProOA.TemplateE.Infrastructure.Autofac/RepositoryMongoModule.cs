using Autofac.Core;
using Autofac;
using NetProOA.Framework.Data.MongoDB.Impl;
using NetProOA.Framework.Data.MongoDB;
using Microsoft.Extensions.Options;
using NetProOA.TemplateE.MongoRepository;
using NetProOA.TemplateE.MongoDomain.Repositories;

namespace NetProOA.TemplateE.Infrastructure.Autofac
{
    public class RepositoryMongoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDBManager>().SingleInstance();

            //默认用于读写
            builder.RegisterType<MongoDBContext>()
                .As<IMongoDBContext, Framework.Mongo.Domain.Core.IDBContext>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IOptions<MongoDBSettings>),
                    (pi, ctx) => Options.Create(ctx.Resolve<IOptionsMonitor<MongoDBSettings>>().Get("MongoDBForWrite"))))
                .InstancePerLifetimeScope();
            //用于只读
            builder.RegisterType<MongoDBContext>()
                .Named<IMongoDBContext>("ReadOnly")
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IOptions<MongoDBSettings>),
                    (pi, ctx) => Options.Create(ctx.Resolve<IOptionsMonitor<MongoDBSettings>>().Get("MongoDBForRead"))))
                .InstancePerLifetimeScope();

            builder.RegisterType<ExampleProductRepository>().As<IExampleProductRepository>().InstancePerLifetimeScope();
        }
    }
}

using Autofac;
using NetProOA.Framework.Domain.Core;
using NetProOA.Framework.Domain.Core.EF;
using NetProOA.TemplateE.Domain.Repositories;
using NetProOA.TemplateE.Domain.Repositories.ReadOnly;
using NetProOA.TemplateE.Repository;
using NetProOA.TemplateE.Repository.ReadOnly;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Infrastructure.Autofac
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                return ctx.Resolve<EFDBContext>();
            }).As<IEFDBContext, IDbContext>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ExampleProductRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}

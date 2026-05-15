using Autofac;
using NetProOA.Framework.EventBus;
using NetProOA.TemplateE.Application.Queries;
using NetProOA.TemplateE.Application.Queries.Impl;
using AutofacModule = Autofac.Module;

namespace NetProOA.TemplateE.Infrastructure.Autofac
{
    public class ApplicationModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ExampleProductQueries).Assembly)
                   .Where(t => t.Name.EndsWith("Queries")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}

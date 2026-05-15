using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Infrastructure.Autofac.Extensions
{
    public static class AutofacExtension
    {
        public static void AddCustomAutofac(this IServiceCollection services, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<CommonModule>();
            containerBuilder.RegisterModule<ApplicationModule>();
            containerBuilder.RegisterModule<RepositoryModule>();
            containerBuilder.RegisterModule<RepositoryMongoModule>();
        }
    }
}

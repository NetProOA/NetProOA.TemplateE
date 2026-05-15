using Autofac;
using Newtonsoft.Json;
using NetProOA.Framework.Authorization;
using NetProOA.Framework.EventBus;
using NetProOA.Framework.Infrastructure.AutoMapper;
using NetProOA.Framework.Infrastructure.Crosscutting.Adapter;
using NetProOA.Framework.Infrastructure.Crosscutting.Json;
using NetProOA.Framework.Infrastructure.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Infrastructure.Autofac
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewtonsoftJsonConverter>().As<IJsonConverter>()
                   .UsingConstructor(typeof(JsonSerializerSettings)).SingleInstance();
            builder.RegisterType<AutomapperTypeAdapter>().As<ITypeAdapter>().SingleInstance();

        }
    }
}

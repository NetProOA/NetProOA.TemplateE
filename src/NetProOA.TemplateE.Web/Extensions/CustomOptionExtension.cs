using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetProOA.TemplateE.Application.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Web.Extensions
{
    public static class CustomOptionExtension
    {
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiGatewaySetting>(configuration.GetSection("ApiGatewaySetting"));
            services.Configure<InnerApiGatewaySetting>(configuration.GetSection("InnerApiGatewaySetting"));
            services.Configure<AppSettings>(configuration);
            return services;
        }
    }
}

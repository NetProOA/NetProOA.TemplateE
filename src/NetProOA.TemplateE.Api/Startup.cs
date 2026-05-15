using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NetProOA.Framework.Authorization.Api;
using NetProOA.Framework.Domain.Core.EF;
using NetProOA.Framework.EventBus;
using NetProOA.Framework.ExceptionHandlerMiddleWare;
using NetProOA.Framework.Swagger;
using NetProOA.TemplateE.Web.Constants;
using NetProOA.TemplateE.Web.Extensions;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.Entity.Mapper;
using NetProOA.TemplateE.Infrastructure.Autofac.Extensions;
using NetProOA.TemplateE.Application.IntegrationEvents;
using Microsoft.IdentityModel.Logging;
using NetProOA.TemplateE.Application.IntegrationEventHandlers;
using NetProOA.Framework.CommandLine;
using NetProOA.TemplateE.Infrastructure._AutoMapper;
using MongoDB.Driver;
using NetProOA.Framework.Data.MongoDB;
using NetProOA.TemplateE.EsDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NetProOA.TemplateE.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection ServiceCollection { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            ServiceCollection = services;

            IdentityModelEventSource.ShowPII = true;
            services.AddCustomAuthentication(Configuration, AuthenticationConfig.API_NAME);
            if (NLoggerFactory.CurrentNLoggerType == NLoggerType.Mongo)
            {
                services.AddCustomEFMiniProfilerMongoStorage(Configuration, Configuration.GetSection("MongoDBMiniProfiler").GetValue<string>("ConnectionString"));
            }
            services.AddCustomApiVersioning();

            services.AddCustomMiniProfilerSwashbuckleSwagger(Configuration, SwaggerConfig.APPLICATION_NAME, new Dictionary<string, string> { { AuthenticationConfig.API_NAME, SwaggerConfig.APPLICATION_NAME } });

            services.AddCustomAutoMapper(typeof(ExampleProductProfile).Assembly);

            var mongoProfileConnectionString = Configuration.GetSection("MongoDBMiniProfiler").GetValue<string>("ConnectionString");
            services.AddCustomEFCore(options =>
            {
                options.WriteReadConnectionString = Configuration.GetConnectionString("MySqlConnectionString");
                options.ReadOnlyConnectionString = Configuration.GetConnectionString("MySqlReadOnlyConnectionString");

            }, typeof(Startup).Assembly, typeof(ExampleProductMap).Assembly);

            if (NLoggerFactory.CurrentNLoggerType == NLoggerType.Mongo)
            {
                services.AddCustomSqlTimingMongoStore(mongoProfileConnectionString);
            }
            else if (NLoggerFactory.CurrentNLoggerType == NLoggerType.Kafka)
            {
                services.AddCustomSqlTimingKafkaStore(options =>
                {
                    options.BootstrapServers = Configuration.GetValue<string>("Logging:NLog:KafkaAddress");
                });
            }
            else if (NLoggerFactory.CurrentNLoggerType == NLoggerType.Local)
            {
                services.AddCustomSqlTimingConsole();
            }

            services.AddCustomKafka(options =>
            {
                options.BootstrapServers = Configuration.GetValue<string>("Logging:NLog:KafkaAddress");
            });

            services.AddNullIntegrationEventService();

            services.AddCustomMediatR(typeof(CreateExampleProductCommand).Assembly);

            services.AddCustomCors();

            services.AddCustomAuditWebApi();

            services.AddCustomTracingMongoStorage(Configuration);

            services.AddCustomZipkinService(options =>
            {
                options.CollectorUrl = Configuration.GetValue<string>("Zipkin:CollectorUrl");
                options.ServiceName = Configuration.GetValue<string>("Zipkin:ServiceName");
                options.Enable = Configuration.GetValue<bool>("Zipkin:Enable");
            });

            services.AddCustomMetrics(options =>
            {
                options.Enable = Configuration.GetValue<bool>("AppMetrics:Enable");
                options.AppName = Configuration.GetValue<string>("AppMetrics:AppName");
                options.InfluxDB = new InfluxDB
                {
                    ConnectionString = Configuration.GetValue<string>("AppMetrics:InfluxDB:ConnectionString"),
                    DatabaseName = Configuration.GetValue<string>("AppMetrics:InfluxDB:DatabaseName")
                };
            });

            services.AddCustomNacosRegDiscovery(Configuration);

            services.AddCustomMinio(options =>
            {
                options.EndPoint = Configuration.GetValue<string>("Minio:EndPoint");
                options.AccessKey = Configuration.GetValue<string>("Minio:AccessKey");
                options.SecretKey = Configuration.GetValue<string>("Minio:SecretKey");
            });

            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBForWrite"));
            services.Configure<MongoDBSettings>("MongoDBForWrite", Configuration.GetSection("MongoDBForWrite"));
            services.Configure<MongoDBSettings>("MongoDBForRead", Configuration.GetSection("MongoDBForRead"));

            services.AddCustomPostmanService();

            services.AddControllers().AddCustomNewtonsoftJson(ServiceCollection, new List<JsonConverter> { new StringEnumConverter() }).AddControllersAsServices();//Aop����-AddControllersAsServices;

        }
   
        public void ConfigureContainer(ContainerBuilder builder)
        {
            ServiceCollection.AddCustomAutofac(builder);
            ServiceCollection.AddAuthorizationInterceptor(typeof(Startup).Assembly, builder);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseHttpsRedirection();

            app.UseCustomTracing(Configuration);

            app.UseMiddleware(typeof(SaaSExceptionHandlerMiddleWare));

            app.UseCustomApiVersioning();

            app.UseRouting();

            app.UseCustomCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomMiniProfiler();

            app.UseCustomAuditWebApi();

            app.UseCustomSwagger(Configuration, SwaggerConfig.APPLICATION_NAME, SwaggerConfig.CLIENT_ID, SwaggerConfig.APP_NAME);

            app.UseCustomConsul(Configuration, applicationLifetime);

            app.UseCustomZipkin();

            var enableRedisProfile = Configuration.GetValue<bool>("EnableRedisProfile");
            if (enableRedisProfile)
            {
                app.UseCustomRedisProfile();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapPostman();
            });
        }
    }
}

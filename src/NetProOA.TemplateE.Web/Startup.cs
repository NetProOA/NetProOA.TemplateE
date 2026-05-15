using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NetProOA.Framework.Authorization.Impl;
using NetProOA.Framework.Authorization.Services;
using NetProOA.Framework.Data.MongoDB;
using NetProOA.Framework.Document;
using NetProOA.Framework.Domain.Core.EF;
using NetProOA.Framework.EventBus;
using NetProOA.Framework.ExceptionHandlerMiddleWare;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.Entity.Mapper;
using NetProOA.TemplateE.EsDomain;
using NetProOA.TemplateE.Infrastructure._AutoMapper;
using NetProOA.TemplateE.Infrastructure.Autofac.Extensions;
using NetProOA.TemplateE.Web.Extensions;

namespace NetProOA.TemplateE.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection ServiceCollection { get; set; }
        public IHostEnvironment HostEnvironment { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceCollection = services;

            services.AddCustomTracingMongoStorage(Configuration);

            services.AddCustomAuthentication(Configuration);

            services.AddCustomDataProtection(options =>
            {
                options.AuthRedisConnectionString = Configuration.GetValue<string>("AuthRedisConfiguration");
                options.DirectoryPath = Configuration.GetValue<string>("DataProtectionKey");
                options.EnableAutomaticKeyGeneration = false;
            });

            services.AddCustomOptions(Configuration);

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

            services.AddCustomNullIntegrationEventService();

            services.AddCustomMediatR(typeof(CreateExampleProductCommand).Assembly);

            services.AddCustomCors();

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

            services.AddCustomDocument(Configuration);
            services.AddCustomIdentityClientSdk(Configuration);

            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBForWrite"));
            services.Configure<MongoDBSettings>("MongoDBForWrite", Configuration.GetSection("MongoDBForWrite"));
            services.Configure<MongoDBSettings>("MongoDBForRead", Configuration.GetSection("MongoDBForRead"));

            services.AddCustomRestClient(config => { });

            var mvcBuilder = services.AddControllersWithViews()
                     .AddControllersAsServices()
                     .AddCustomNewtonsoftJson(services, new List<JsonConverter> { new StringEnumConverter() });
            if (HostEnvironment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            ServiceCollection.AddCustomAutofac(builder);
            ServiceCollection.AddAuthorizationInterceptor(typeof(Startup).Assembly, builder);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering(); 
                await next();
            });

            app.UseMiddleware(typeof(SaaSExceptionHandlerMiddleWare));

            app.UseRouting();

            app.UseCustomTracing(Configuration);

            app.UseCustomCors();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomZipkin();

            VueRender.InitHtml();

            var enableRedisProfile = Configuration.GetValue<bool>("EnableRedisProfile");
            if (enableRedisProfile)
            {
                app.UseCustomRedisProfile();
            }
            app.UseCustomConsul(Configuration, applicationLifetime);
            app.UseEndpoints(configure =>
            {
                configure.MapDefaultControllerRoute();
            });
        }
    }
}

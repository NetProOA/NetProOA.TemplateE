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
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.DbMigrations.Constants;
using NetProOA.TemplateE.Entity.Mapper;
using NetProOA.TemplateE.Infrastructure._AutoMapper;
using NetProOA.TemplateE.Infrastructure.Autofac.Extensions;

namespace NetProOA.TemplateE.DbMigrations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection ServiceCollection { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceCollection = services;

            services.AddCustomAuthentication(Configuration, AuthenticationConfig.API_NAME);

            /*Swagger*/
            services.AddCustomSwashbuckleSwagger(Configuration, SwaggerConfig.APPLICATION_NAME, new Dictionary<string, string> { { AuthenticationConfig.API_NAME, SwaggerConfig.APPLICATION_NAME } });

            /*AutoMapper*/
            services.AddAutoMapper(typeof(ExampleProductProfile).Assembly);

            /*Db*/
            services.AddCustomEFCore(options =>
            {
                options.WriteReadConnectionString = Configuration.GetConnectionString("MySqlConnectionString");
                options.ReadOnlyConnectionString = Configuration.GetConnectionString("MySqlReadOnlyConnectionString");

            }, typeof(Startup).Assembly, typeof(ExampleProductMap).Assembly);

            services.AddCustomIntegrationEventService(Configuration, x =>
            {
                x.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"));
            },()=> { });

            /*MediatR*/
            services.AddCustomMediatR(typeof(CreateExampleProductCommand).Assembly);

            services.AddCustomCors();

            /*Api*/
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                var convers = options.SerializerSettings.Converters ?? new List<JsonConverter>();
                convers.Add(new StringEnumConverter());
                options.SerializerSettings.Converters = convers;
            }).AddCustomNewtonsoftJson(ServiceCollection, new List<JsonConverter> { new StringEnumConverter() }).AddControllersAsServices();//Aop����-AddControllersAsServices;

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            ServiceCollection.AddCustomAutofac(builder);
            ServiceCollection.AddAuthorizationInterceptor(typeof(Startup).Assembly, builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            app.UseCustomCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware(typeof(SaaSExceptionHandlerMiddleWare));

            app.UseAuthentication();
            app.UseAuthorization();

            /*Swagger*/
            app.UseCustomSwagger(Configuration, SwaggerConfig.APPLICATION_NAME, SwaggerConfig.CLIENT_ID, SwaggerConfig.APP_NAME);

            app.UseCustomConsul(Configuration, applicationLifetime);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

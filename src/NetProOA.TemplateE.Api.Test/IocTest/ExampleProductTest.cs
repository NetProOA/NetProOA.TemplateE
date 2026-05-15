using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bogus;
using MediatR;
using NetProOA.TemplateE.Api.Test.Infrastructure;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.Application.Queries;
using NetProOA.TemplateE.Application.Queries.Criteria;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetProOA.TemplateE.Api.Test.IocTest
{
    public class ExampleProductTest
    {
        public static ILifetimeScope Scope { get; private set; }
        public ExampleProductTest()
        {
            Scope = TestInitialize.Initialize<Startup>("Development");
        }

        [Fact]
        public async Task TestAddAsync()
        {
            var mediator = Scope.Resolve<IMediator>();
            var faker = new Faker("zh_CN");
            for (int i = 0; i < 100; i++)
            {
                var productName = faker.Commerce.ProductName();
                var result = await mediator.Send(new CreateExampleProductCommand { Name = productName, Price = i, ProcurementTime = DateTime.Now.AddHours(-i), ProcurementType = 1 });
                result.ShouldBe(true);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            var mediator = Scope.Resolve<IMediator>();
            var result = await mediator.Send(new ModifyExampleProductCommand { Id = "6551619d-b0d9-ca56-0d31-3a0df6561ed5", Name = "AB", Price = 1000, ProcurementTime = DateTime.Now, ProcurementType = 1 });
        }
    }
}

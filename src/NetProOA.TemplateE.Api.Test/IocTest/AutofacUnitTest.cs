using Autofac;
using Autofac.Extensions.DependencyInjection;
using NetProOA.TemplateE.Api.Test.Infrastructure;
using NetProOA.TemplateE.Application.Queries;
using NetProOA.TemplateE.Application.Queries.Criteria;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetProOA.TemplateE.Api.Test.IocTest
{
    public class AutofacUnitTest
    {
        public static ILifetimeScope Scope { get; private set; }
        public AutofacUnitTest()
        {
            Scope = TestInitialize.Initialize<Startup>("Development");
        }

        [Fact]
        public async Task TestAsync()
        {
            var exampleProductQueries = Scope.Resolve<IExampleProductQueries>();
            var result = await exampleProductQueries.GetExampleProductPageListAsync(new ExampleProductQueryCriteria { PageNumber = 1, PageSize = 20, IsAscending = true });
            Assert.True(result.Data.Any());
        }
    }
}

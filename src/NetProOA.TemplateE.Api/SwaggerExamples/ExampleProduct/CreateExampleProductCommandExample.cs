using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using Swashbuckle.AspNetCore.Filters;

namespace NetProOA.TemplateE.Api.SwaggerExamples.ExampleProduct
{
    public class CreateExampleProductCommandExample : IExamplesProvider<CreateExampleProductCommand>
    {
        public CreateExampleProductCommand GetExamples()
        {
            return new CreateExampleProductCommand
            {
                Name = "测试Example"
            };
        }
    }
}

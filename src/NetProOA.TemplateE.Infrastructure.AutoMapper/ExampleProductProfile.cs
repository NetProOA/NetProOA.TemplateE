using AutoMapper;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;

namespace NetProOA.TemplateE.Infrastructure._AutoMapper
{
    public class ExampleProductProfile : Profile
    {
        public ExampleProductProfile()
        {
            CreateMap<ExampleProduct, Application.ViewModels.ExampleProduct.ExampleProductDto>();
        }
    }
}


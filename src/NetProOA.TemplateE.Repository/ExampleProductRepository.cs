using NetProOA.Framework.Domain.Core.EF;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;
using NetProOA.TemplateE.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
namespace NetProOA.TemplateE.Repository
{
    public  class ExampleProductRepository : EFDBRepository<ExampleProduct,string>, IExampleProductRepository
    {
        public ExampleProductRepository(EFDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}


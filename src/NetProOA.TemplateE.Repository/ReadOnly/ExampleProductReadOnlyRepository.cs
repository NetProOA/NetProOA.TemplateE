using NetProOA.Framework.Domain.Core;
using NetProOA.Framework.Domain.Core.EF;
using NetProOA.TemplateE.Domain.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Text;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;
namespace NetProOA.TemplateE.Repository.ReadOnly
{
    public  class ExampleProductReadOnlyRepository: EFDBReadOnlyRepository<ExampleProduct,string>, IExampleProductReadOnlyRepository
    {
		 public ExampleProductReadOnlyRepository(EFReadOnlyDBContext context) : base(context)
        {
        }
    }
}

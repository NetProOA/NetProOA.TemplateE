using NetProOA.Framework.Domain.Core;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;
namespace NetProOA.TemplateE.Domain.Repositories.ReadOnly
{
    public  interface IExampleProductReadOnlyRepository: IReadOnlyRepository<ExampleProduct,string>
    {
    }
}

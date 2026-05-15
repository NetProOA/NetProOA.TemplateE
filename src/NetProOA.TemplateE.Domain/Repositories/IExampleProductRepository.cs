using NetProOA.Framework.Domain.Core;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;
namespace NetProOA.TemplateE.Domain.Repositories
{
    public  interface IExampleProductRepository: IRepository<ExampleProduct,string>
    {
    }
}

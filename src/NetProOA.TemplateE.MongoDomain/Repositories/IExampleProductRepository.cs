using NetProOA.Framework.Mongo.Domain.Core;
using NetProOA.TemplateE.MongoDomain.AggregatesModel.ExampleProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.MongoDomain.Repositories
{
    public interface IExampleProductRepository : IRepository<MongoExampleProduct>
    {
    }
}

using NetProOA.Framework.Data.MongoDB;
using NetProOA.Framework.Data.MongoDB.Impl;
using NetProOA.TemplateE.MongoDomain.AggregatesModel.ExampleProductAggregate;
using NetProOA.TemplateE.MongoDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.MongoRepository
{
    public class ExampleProductRepository : MongoDBRepository<MongoExampleProduct>, IExampleProductRepository
    {
        public ExampleProductRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}

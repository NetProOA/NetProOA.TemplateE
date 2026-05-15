using NetProOA.Framework.Mongo.Domain.Core.Specification;
using NetProOA.TemplateE.MongoDomain.AggregatesModel.ExampleProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetProOA.TemplateE.MongoDomain.AggregatesModel.ExampleProductAggregate.Specifications
{
    public class MatchExampleProductByNameSpecification : Specification<MongoExampleProduct>
    {
        public string Name { get; }

        public MatchExampleProductByNameSpecification(string name)
        {
            Name = name;
        }

        public override Expression<Func<MongoExampleProduct, bool>> GetExpression()
        {
            return p => p.Name == Name;
        }
    }
}

using NetProOA.Framework.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate.Specifications
{
    public class MatchExampleProductByIdsSpecification : Specification<ExampleProduct>
    {
        public List<string> Ids { get; }

        public MatchExampleProductByIdsSpecification(List<string> ids)
        {
            Ids = ids??new List<string>();
        }

        public override Expression<Func<ExampleProduct, bool>> GetExpression()
        {
            return p => Ids.Contains(p.Id);
        }
    }
}


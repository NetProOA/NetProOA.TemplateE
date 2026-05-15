using NetProOA.Framework.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Domain.Events.ExampleProduct
{
    public class CreatedExampleProductDomainEvent : IDomainEvent
    {
        public CreatedExampleProductDomainEvent(AggregateModels.ExampleProductAggregate.ExampleProduct exampleProduct)
        {
            ExampleProduct = exampleProduct;
        }

        public AggregateModels.ExampleProductAggregate.ExampleProduct ExampleProduct { get; }
    }
}
using NetProOA.Framework.Domain.Core.DomainEvents;
using NetProOA.Framework.EventBus;
using NetProOA.TemplateE.Application.IntegrationEvents;
using NetProOA.TemplateE.Domain.Events.ExampleProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.DomainEventHandlers.ExampleProduct
{
    public class CreatedExampleProductDomainEventHandler : IDomainEventHandler<CreatedExampleProductDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public CreatedExampleProductDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle(CreatedExampleProductDomainEvent notification, CancellationToken cancellationToken)
        {
           await _integrationEventService.SaveInegrationEventAsync(new CreatedExampleProductInegrationEvent { Name = notification.ExampleProduct.Name });
        }
    }
}

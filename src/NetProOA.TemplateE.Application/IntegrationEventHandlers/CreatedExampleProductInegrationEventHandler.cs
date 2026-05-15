using DotNetCore.CAP;
using NetProOA.Framework.EventBus;
using NetProOA.TemplateE.Application.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.IntegrationEventHandlers
{
    public class CreatedExampleProductInegrationEventHandler : IIntegrationEventHandler<CreatedExampleProductInegrationEvent>
    {
        public async Task Handle(CreatedExampleProductInegrationEvent @event)
        {
            await Task.CompletedTask;
        }
    }
}

using NetProOA.Framework.EventBus;
using NetProOA.Framework.EventBus.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Application.IntegrationEvents
{
    [IntegrationEvent(Name = nameof(CreatedExampleProductInegrationEvent))]
    public class CreatedExampleProductInegrationEvent : IntegrationEvent
    {
        public string Name { get; set; }
    }
}

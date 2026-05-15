using MediatR;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.JobHandlers
{
    public class ExampleProductJob
    {
        public readonly IMediator _mediator;

        public ExampleProductJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ExcuteAsync()
        {
            await _mediator.Send(new DeleteExampleProductCommand { Ids = new List<string> { } });
        }
    }
}

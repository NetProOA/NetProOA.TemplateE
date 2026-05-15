using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct
{
    public class DeleteExampleProductCommand : CommandBase, IRequest<bool>
    {
        public List<string> Ids { get; set; }
    }
}




using MediatR;
using Microsoft.Extensions.Logging;
using NetProOA.Framework.Domain.Core;
using NetProOA.TemplateE.Domain.Repositories;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DomainAggregate = NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct.Handlers
{
    public class CreateExampleProductCommandHandler : IRequestHandler<CreateExampleProductCommand, bool>
    {
        private readonly IDbContext _dbContext;
        private readonly IExampleProductRepository _exampleProductRepository;
        private readonly ILogger<CreateExampleProductCommandHandler> _logger;


        public CreateExampleProductCommandHandler(IDbContext dbContext, IExampleProductRepository exampleProductRepository, ILogger<CreateExampleProductCommandHandler> logger)
        {
            _dbContext = dbContext;
            _exampleProductRepository = exampleProductRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateExampleProductCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserIdentity;

            _dbContext.BeginTransaction();

            var exampleProduct = new DomainAggregate.ExampleProduct().PopulateWith(request);
            await _exampleProductRepository.AddAsync(exampleProduct);

            await _dbContext.CommitAsync();

            return true;
        }
    }
}




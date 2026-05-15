using MediatR;
using NetProOA.Framework.Domain.Core;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate.Specifications;
using NetProOA.TemplateE.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct.Handlers
{
    public class DeleteExampleProductCommandHandler : IRequestHandler<DeleteExampleProductCommand, bool>
    {
        private readonly IDbContext _dbContext;
        private readonly IExampleProductRepository _exampleProductRepository;

        public DeleteExampleProductCommandHandler(IDbContext dbContext, IExampleProductRepository exampleProductRepository)
        {
            _dbContext = dbContext;
            _exampleProductRepository = exampleProductRepository;
        }

        public async Task<bool> Handle(DeleteExampleProductCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserIdentity;

            _dbContext.BeginTransaction();

            var exampleProducts =await _exampleProductRepository.GetListAsync(new MatchExampleProductByIdsSpecification(request.Ids));
            foreach (var exampleProduct in exampleProducts)
            {
                await _exampleProductRepository.RemoveAsync(exampleProduct);
            }

            await _dbContext.CommitAsync();
            return true;
        }
    }
}


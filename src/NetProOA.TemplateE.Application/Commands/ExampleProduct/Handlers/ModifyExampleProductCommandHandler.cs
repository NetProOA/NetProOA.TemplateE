using MediatR;
using NetProOA.Framework.Domain.Core;
using NetProOA.TemplateE.Domain.Repositories;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct.Handlers
{
    public class ModifyExampleProductCommandHandler : IRequestHandler<ModifyExampleProductCommand, bool>
    {
        private readonly IDbContext _dbContext;
        private readonly IExampleProductRepository _exampleProductRepository;

        public ModifyExampleProductCommandHandler(IDbContext dbContext, IExampleProductRepository exampleProductRepository)
        {
            _dbContext = dbContext;
            _exampleProductRepository = exampleProductRepository;
        }

        public async Task<bool> Handle(ModifyExampleProductCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserIdentity;

            _dbContext.BeginTransaction();

            var exampleProduct = await _exampleProductRepository.GetByKeyAsync(request.Id);

            exampleProduct.Name = request.Name;
            exampleProduct.Price = request.Price;
            exampleProduct.ProcurementTime = request.ProcurementTime;
            exampleProduct.ProcurementType = request.ProcurementType;

            await _exampleProductRepository.UpdateAsync(exampleProduct);

            await _dbContext.CommitAsync();
            return true;
        }
    }
}




using NetProOA.Framework.DataTransfer.Queries;
using NetProOA.Framework.Infrastructure.Crosscutting.Adapter;
using NetProOA.Framework.Specifications;
using NetProOA.TemplateE.Application.Extensions;
using NetProOA.TemplateE.Application.Queries.Criteria;
using NetProOA.TemplateE.Application.ViewModels.ExampleProduct;
using NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate;
using NetProOA.TemplateE.Domain.Repositories;
using NetProOA.TemplateE.Domain.Repositories.ReadOnly;
using System;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Queries.Impl
{
    public class ExampleProductQueries : IExampleProductQueries
    {
        private readonly IExampleProductReadOnlyRepository _exampleProductReadOnlyRepository;
        private readonly IExampleProductRepository _exampleProductRepository;
        private readonly ITypeAdapter _typeAdapter;

        public ExampleProductQueries(IExampleProductReadOnlyRepository exampleProductReadOnlyRepository, IExampleProductRepository exampleProductRepository, ITypeAdapter typeAdapter)
        {
            _exampleProductReadOnlyRepository = exampleProductReadOnlyRepository;
            _exampleProductRepository = exampleProductRepository;
            _typeAdapter = typeAdapter;
        }

        public async Task<PagedQueryResult<ExampleProductDto>> GetExampleProductPageListAsync(ExampleProductQueryCriteria criteria)
        {
            var user = criteria.UserIdentity;

            var specification = Specification<ExampleProduct>.Eval(p => true);
            if (!criteria.Name.IsNullOrEmpty())
            {
                specification = Specification<ExampleProduct>.Eval(p => p.Name.Contains(criteria.Name));
            }
            if (criteria.PriceMin.HasValue && criteria.PriceMax.HasValue)
            {
                specification = Specification<ExampleProduct>.Eval(p => p.Price>=criteria.PriceMin&& p.Price<=criteria.PriceMax);
            }
            if (criteria.ProcurementTimeMin.HasValue && criteria.ProcurementTimeMax.HasValue)
            {
                specification = Specification<ExampleProduct>.Eval(p => p.ProcurementTime >= criteria.ProcurementTimeMin && p.ProcurementTime <= criteria.ProcurementTimeMax);
            }
            if (criteria.ProcurementType.HasValue)
            {
                specification = Specification<ExampleProduct>.Eval(p => p.ProcurementType== criteria.ProcurementType);
            }
            var pagedResult = await _exampleProductReadOnlyRepository.FindInPageAsync(specification, p => p.CreateTime, criteria.IsAscending, criteria.PageNumber, criteria.PageSize);
            var pagedResultDto = pagedResult.ToPagedQueryResultDto<ExampleProductDto, ExampleProduct>(_typeAdapter);

            return pagedResultDto;
        }

        public async Task<ExampleProductDto> GetExampleProductAsync(string id)
        {
            var exampleProduct = await _exampleProductRepository.GetByKeyAsync(id);
            return _typeAdapter.Adapt<ExampleProductDto>(exampleProduct);
        }
    }
}




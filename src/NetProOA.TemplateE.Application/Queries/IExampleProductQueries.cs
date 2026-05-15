using NetProOA.Framework.DataTransfer.Queries;
using NetProOA.TemplateE.Application.Queries.Criteria;
using NetProOA.TemplateE.Application.ViewModels.ExampleProduct;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Queries
{
    public interface IExampleProductQueries
    {
        Task<PagedQueryResult<ExampleProductDto>> GetExampleProductPageListAsync(ExampleProductQueryCriteria criteria);

        Task<ExampleProductDto> GetExampleProductAsync(string id);
    }
}




using NetProOA.Framework.DataTransfer.Queries;
using NetProOA.Framework.Domain.Core.Paged;
using NetProOA.Framework.Infrastructure.Crosscutting.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Extensions
{
    public static class PagedResultExtension
    {
        public static PagedQueryResult<TDto> ToPagedQueryResultDto<TDto, T>(this PagedResult<T> pagedResult, ITypeAdapter typeAdapter) where TDto : class, new()
        {
            return new PagedQueryResult<TDto>
            {
                TotalPages = pagedResult.TotalPages,
                TotalRecords = pagedResult.TotalRecords,
                Data = pagedResult.Data.Select(p => typeAdapter.Adapt<TDto>(p)).ToList()
            };
        }
    }
}

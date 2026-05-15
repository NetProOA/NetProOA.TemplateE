using NetProOA.Framework.DataTransfer;
using NetProOA.Framework.DataTransfer.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Web.Extensions
{
    public static class PagedQueryResultExtension
    {
        public static PageResponse<IList<T>> ToPageResponse<T>(this PagedQueryResult<T> pagedQueryResult)
        {
            return new PageResponse<IList<T>> { TotalCount = pagedQueryResult.TotalRecords, Data = pagedQueryResult.Data };
        }
    }
}

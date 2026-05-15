using NetProOA.Framework.DataTransfer;
using NetProOA.Framework.DataTransfer.Queries;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using NetProOA.TemplateE.Application.Queries.Criteria;
using NetProOA.TemplateE.Application.ViewModels.ExampleProduct;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace NetProOA.TemplateE.Api.SwaggerExamples.ExampleProduct
{
    public class PagedQueryResultExampleProductDtoExample : IExamplesProvider<PageResponse<IList<ExampleProductDto>>>
    {
        public PageResponse<IList<ExampleProductDto>> GetExamples()
        {
            return new PageResponse<IList<ExampleProductDto>>
            {
                Code = "OK",
                Succeed = true,
                Message = string.Empty,
                TotalCount = 1000,
                Data = new List<ExampleProductDto>
                {
                     new ExampleProductDto
                     {
                          Id=Guid.NewGuid().ToString()
                     }
                }
            };
        }
    }
    public class ExampleProductQueryCriteriaExample : IExamplesProvider<ExampleProductQueryCriteria>
    {
        public ExampleProductQueryCriteria GetExamples()
        {
            return new ExampleProductQueryCriteria
            {
                PageNumber = 1,
                PageSize = 50,
                Field = "Id",
                IsAscending = false,
            };
        }
    }

}

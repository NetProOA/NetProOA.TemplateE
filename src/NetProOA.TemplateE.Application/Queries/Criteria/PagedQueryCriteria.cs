using NetProOA.Framework.DataTransfer;

namespace NetProOA.TemplateE.Application.Queries.Criteria
{
    /// <summary>
    /// 分页查询
    /// </summary>
    public abstract class PagedQueryCriteria : IPagedQueryCriteria
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedQueryCriteria()
        {
            PageNumber = 1;
            PageSize = 25;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAscending { get; set; }

        /// <summary>
        /// Aop注入当前用户
        /// </summary>
        public UserIdentity UserIdentity { get; set; }
    }
}

using NetProOA.Framework.Domain.Core.Impl;
using NetProOA.TemplateE.Domain.Events.ExampleProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Domain.AggregateModels.ExampleProductAggregate
{
    public class ExampleProduct : AggregateRoot<string>, IHasRowVersion, IHasCreateModifyTime,IHasOperator
    {
        public ExampleProduct()
        {
        }

        public ExampleProduct(string name)
        {
            Name = name;
            AddDomainEvent(new CreatedExampleProductDomainEvent(this));
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime ProcurementTime { get; set; }

        /// <summary>
        /// 1:自主采购
        /// 2:推荐采购
        /// </summary>
        public int ProcurementType { get; set; }

        public byte[] RowVersion { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateEmplId { get; set; }
        public string CreateEmplName { get; set; }
        public string CreateDeptId { get; set; }
        public string CreateDeptName { get; set; }
        public string LastModifyEmplId { get; set; }
        public string LastModifyEmplName { get; set; }
        public string LastModifyDeptId { get; set; }
        public string LastModifyDeptName { get; set; }
    }
}

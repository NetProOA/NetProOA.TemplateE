using System;

namespace NetProOA.TemplateE.Application.Queries.Criteria
{
    public class ExampleProductQueryCriteria : PagedQueryCriteria
    {
        public string Name { get; set; }

        /// <summary>
        /// �۸�
        /// </summary>
        public decimal? PriceMin { get; set; }

        /// <summary>
        /// �۸�
        /// </summary>
        public decimal? PriceMax { get; set; }

        /// <summary>
        /// �ɹ�ʱ��
        /// </summary>
        public DateTime? ProcurementTimeMin { get; set; }

        /// <summary>
        /// �ɹ�ʱ��
        /// </summary>
        public DateTime? ProcurementTimeMax { get; set; }

        /// <summary>
        /// 1:�����ɹ�
        /// 2:�Ƽ��ɹ�
        /// </summary>
        public int? ProcurementType { get; set; }
    }
}




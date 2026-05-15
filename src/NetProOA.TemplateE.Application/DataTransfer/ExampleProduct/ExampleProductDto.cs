using System;

namespace NetProOA.TemplateE.Application.ViewModels.ExampleProduct
{
    public class ExampleProductDto
    {
		/// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �۸�
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// �ɹ�ʱ��
        /// </summary>
        public DateTime ProcurementTime { get; set; }

        /// <summary>
        /// 1:�����ɹ�
        /// 2:�Ƽ��ɹ�
        /// </summary>
        public int ProcurementType { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}




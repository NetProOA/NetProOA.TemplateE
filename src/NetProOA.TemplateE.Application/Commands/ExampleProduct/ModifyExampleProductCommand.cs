using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct
{
    public class ModifyExampleProductCommand : CommandBase, IRequest<bool>
    {
        public string Id { get; set; }

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
    }
}




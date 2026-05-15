using Nest;
using NetProOA.Framework.ES.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.EsDomain
{
    /// <summary>
    /// RelationName必须是小写
    /// </summary>
    [ElasticsearchType(RelationName = "es_example_products")]
    public class EsExampleProduct : ESDocument
    {
        public EsExampleProduct()
        {
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Keyword]
        public string Name { get;  set; }
    }
}

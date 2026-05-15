using NetProOA.Framework.Mongo.Domain.Core.Attributes;
using NetProOA.Framework.Mongo.Domain.Core.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.MongoDomain.AggregatesModel.ExampleProductAggregate
{
    [AggregateRootName("ExampleProducts")]
    public class MongoExampleProduct : AggregateRoot
    {
        public MongoExampleProduct()
        {
        }

        public MongoExampleProduct(string name)
        {
            Name = name;
        }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get;  set; }

        public DateTime CreateTime { get; set; }

        public Item  Item { get; set; }
    }

    public class Item
    {
        public string ItemName { get; set; }
    }
}

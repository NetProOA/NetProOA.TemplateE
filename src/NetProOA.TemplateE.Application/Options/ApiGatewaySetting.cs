using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Application.Options
{
    /// <summary>
    /// 外部网关
    /// </summary>
    public class ApiGatewaySetting
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string HostAddress { get; set; }

        /// <summary>
        /// IdentityService前缀
        /// </summary>
        public string IdentityServicePrefix { get; set; }

        /// <summary>
        /// IdentityService地址
        /// </summary>
        public string IdentityServiceHostAddress { get { return $"{HostAddress}/{IdentityServicePrefix}"; } }
    }

    public class InnerApiGatewaySetting
    {
        public string HostAddress { get; set; }
    }
}

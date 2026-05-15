using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.Framework.TemplateE.ClientSdk
{
    public abstract class ClientServiceBase
    {
        public AspnetcoreEnvironmentEnum GetAspnetcoreEnvironment()
        {
            var aspnetcoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
            if (aspnetcoreEnvironment == AspnetcoreEnvironmentEnum.Development.ToString())
            {
                return AspnetcoreEnvironmentEnum.Development;
            }
            else if (aspnetcoreEnvironment == AspnetcoreEnvironmentEnum.Production.ToString())
            {
                return AspnetcoreEnvironmentEnum.Production;
            }
            throw new Exception("未知环境");
        }
    }

    public enum AspnetcoreEnvironmentEnum
    {
        Development,
        Production
    }
}

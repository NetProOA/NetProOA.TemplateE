using Microsoft.Extensions.Logging;
using NetProOA.Framework.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.Framework.Identity.ClientSdk
{
    public interface ITemplateEClientService
    {
        Task<Response<SdkTemplateEInfo>> GetTemplateEInfoAsync(string id);
    }
}

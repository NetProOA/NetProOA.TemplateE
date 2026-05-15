using Microsoft.Extensions.Configuration;
using NetProOA.Framework.Infrastructure.Crosscutting.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProOA.Framework.DataTransfer;
using Microsoft.Extensions.Logging;
using NetProOA.Framework.TemplateE.ClientSdk;

namespace NetProOA.Framework.Identity.ClientSdk
{
    public class TemplateEClientService : ClientServiceBase, ITemplateEClientService
    {
        private readonly HttpClient _apiClient;

        private readonly string TemplateEClientUrl;
        private readonly string TemplateEInternalClientUrl;

        private readonly IJsonConverter _jsonConverter;

        private readonly ILogger<TemplateEClientService> _logger;

        public TemplateEClientService(HttpClient apiClient, IJsonConverter jsonConverter, IConfiguration configuration, ILogger<TemplateEClientService> logger)
        {
            _apiClient = apiClient;
            _jsonConverter = jsonConverter;
            TemplateEClientUrl = $"{configuration.GetSection("ApiGatewaySetting:HostAddress").Value}/templateE";
            switch (GetAspnetcoreEnvironment())
            {
                case AspnetcoreEnvironmentEnum.Development:
                    TemplateEInternalClientUrl = "http://192.168.31.132:90/templateeInternal";
                    break;
                case AspnetcoreEnvironmentEnum.Production:
                    TemplateEInternalClientUrl = "http://192.168.31.132:90/templateeInternal";
                    break;
            }
            _logger = logger;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<Response<SdkTemplateEInfo>> GetTemplateEInfoAsync(string id)
        {
            try
            {
                var url = $"{TemplateEClientUrl}/templatee/values/anonymous?id={id}";
                var httpResponseMessage = await _apiClient.GetAsync(url);
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseStr = await httpResponseMessage.Content.ReadAsStringAsync();
                    var response = _jsonConverter.DeserializeObject<Response<SdkTemplateEInfo>>(responseStr);
                    if (response.Succeed)
                    {
                        return response;
                    }
                }
                return new Response<SdkTemplateEInfo> { Succeed = false, Message = $"StatusCode:{httpResponseMessage.StatusCode.ToString()}" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new Response<SdkTemplateEInfo> { Succeed = false, Message = ex.Message };
            }
        }
    }
    public class SdkTemplateEInfo
    {
    }
}

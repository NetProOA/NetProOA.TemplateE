using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NetProOA.Framework.DataTransfer;
using NetProOA.TemplateE.Application.Commands.ExampleProduct;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetProOA.TemplateE.Api.Test.ApiTest
{
    public class HttpClientUnitTest :
    IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        static readonly string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZCN0FDQzUyMDMwNUJGREI0RjcyNTJEQUVCMjE3N0NDMDkxRkFBRTFSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJuYmYiOjE2OTQ2MTUzMzcsImV4cCI6MTY5NDY1ODUzNywiaXNzIjoiaHR0cDovLzE5Mi4xNjguMzEuMTMyOjcwMDAiLCJhdWQiOlsic2Fhcy5sb2dnZXIuYXBpIiwic2Fhcy5pZGVudGl0eS5hcGkiLCJzYWFzLmRvY3VtZW50LmFwaSJdLCJjbGllbnRfaWQiOiJtdmMtY2xpZW50Iiwic3ViIjoiOWViNDU0N2QtN2Q4Yy00M2Y1LTk5YzYtMzg1Nzg4Yzk5NTU2IiwiYXV0aF90aW1lIjoxNjk0NjE1MzM2LCJpZHAiOiJsb2NhbCIsInR5cGUiOiJlbXBsb3llZSIsInVzZXJpZCI6IjllYjQ1NDdkLTdkOGMtNDNmNS05OWM2LTM4NTc4OGM5OTU1NiIsImp0aSI6IjUwRkU1OUJGMzQzQzk4MDFGNEY4M0NENzdFRjZGQ0I4Iiwic2lkIjoiNTQ3OUNDMTlGQzE1NUE4MjQwMURBQzkyRUJFMUNBMzgiLCJpYXQiOjE2OTQ2MTUzMzcsInNjb3BlIjpbIm9wZW5pZCIsInByb2ZpbGUiLCJzYWFzLmlkZW50aXR5LmFwaSIsInNhYXMubG9nZ2VyLmFwaSIsInNhYXMuZG9jdW1lbnQuYXBpIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInB3ZCJdfQ.kjGEBrLxTIL4wErbte7-vMUAwRrLH1PEQiPlA8H05ilS_6AVvs2kkyxBmFilCkqKebNzhE9k8J2-JqPsUmBfIHtJNReSsrORkcN55AHmVrv4pAvGwv8sTN46tGrOminlSS40_J4NFY1xuoNRo_a75uqJcBwigdrR-eOfI08GpHM73W75Y4PsuoWvHcVTPpArGaY5Wzx9-G5OUS6pWfkipKNd8mIMT34C_h9rzR-k4PzCbPNJqtoHXtJ2WTWSLLlzVnUDBHRQSH5Se_z2YxuZHo3xg9Y-0-5v3Hn_fNL9EdtFTFL_W0NABGUoMxuwECwQnKOeNMiL0NaUKrqVaWyWHw";

        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public HttpClientUnitTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        [Fact]
        public async Task Test1Async()
        {
            var command = new CreateExampleProductCommand
            {
                UserIdentity = null,
                Name= "Test"
            };
            var httpResponseMessage = await _client.PostAsync("api/exampleProducts", command.ToStringContent());
            Assert.True(httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK);
            var jsonResult = await httpResponseMessage.Content.ReadAsStringAsync();

            var desObject = JsonConvert.DeserializeObject<OkResponse>(jsonResult);
            Assert.True(desObject.Succeed);
        }
    }

    public static class ObjectExtension
    {
        public static StringContent ToStringContent(this object obj)
        {
            var json = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            return json;
        }
    }
}

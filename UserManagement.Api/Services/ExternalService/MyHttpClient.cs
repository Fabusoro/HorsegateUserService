using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserManagement.Api.Services.Interface;

namespace UserManagement.Api.Services.ExternalService
{

    public class MyHttpClient
    {
        private readonly HttpClient _client;

        public MyHttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7212/");
        }

        public async Task<string> CallOtherServiceAsync(string id)
        {
            string endpoint = "api/Payment/Verify-Payment?userId=" + id;

            var requestBody = new { Property1 = "Value1" };
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpMethod method = HttpMethod.Get;

            HttpRequestMessage request = new HttpRequestMessage(method, endpoint);
            request.Content = content;

            HttpResponseMessage response = await _client.SendAsync(request);

            string responseBody = await response.Content.ReadAsStringAsync();
            

            return responseBody;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
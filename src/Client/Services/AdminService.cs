using System.Net.Http.Json;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Shared.Requests;

namespace RateMyManagementWASM.Client.Services
{
    public class AdminService : IAdminService
    {
        private HttpClient _httpClient;
        private string _relatePath = "api/admin";
        public AdminService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RateMyManagementWASM.ServerAPI");
            _httpClient.BaseAddress = new Uri(_httpClient.BaseAddress, _relatePath);
        }
        public async Task PopulateDb(PopulateDbRequest requestContent)
        {
            var requestUri = _httpClient.BaseAddress!.AbsoluteUri + "/populatedb";
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = JsonContent.Create(requestContent),
                RequestUri = new Uri(requestUri)
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}

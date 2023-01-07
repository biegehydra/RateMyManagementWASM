using RateMyManagementWASM.Client.IServices;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Services;

public class HttpCompanyService : EntityDbService<Company, CompanyDto>
{
    public HttpCompanyService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        AllIncludes = new[] {"Locations", "Locations.LocationReviews"};
    }

    public async Task<IEnumerable<Company>> Query(string query, string[] includes, bool startsWith = false)
    {
        var queryUrl = _httpClient.BaseAddress.AbsoluteUri + $"/query/{query}";
        queryUrl += startsWith ? "?startswith=true" : "?startswith=false";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(includes, new MediaTypeHeaderValue("application/json"), JsonOptions),
            RequestUri = new Uri(queryUrl)
        };
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var str = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<List<Company>>(str);
            return company;
        }
        else
        {
            return new List<Company>();
        }
    }
    public async Task<IEnumerable<CompanyWithRatingDto>> QueryWithRatings(string query, string[] includes, bool startsWith = false)
    {
        var queryUrl = _httpClient.BaseAddress.AbsoluteUri + $"/querywithrating/{query}";
        queryUrl += startsWith ? "?startswith=true" : "?startswith=false";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(includes, new MediaTypeHeaderValue("application/json"), JsonOptions),
            RequestUri = new Uri(queryUrl)
        };
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var str = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<List<CompanyWithRatingDto>>(str);
            return company;
        }
        else
        {
            return new List<CompanyWithRatingDto>();
        }
    }
}
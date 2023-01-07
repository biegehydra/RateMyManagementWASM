using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RateMyManagementWASM.Client.IServices;

public abstract class EntityDbService<TEntity, TDto>
{
    protected HttpClient _httpClient;
    private string _relatePath = "api/" + typeof(TEntity).Name;
    public static string[] AllIncludes { get; protected set; }
    private readonly string[] EmptyIncludes = new string[] {};

    protected readonly JsonSerializerOptions JsonOptions = new () { ReferenceHandler = ReferenceHandler.Preserve};
    public EntityDbService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("public");
        _httpClient.BaseAddress = new Uri(_httpClient.BaseAddress, _relatePath);
    }
    public async Task<TEntity> Get(string id, string[]? includes)
    {
        includes ??= EmptyIncludes;
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(includes),
            RequestUri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "/" + id),
            
        };
        var response = await _httpClient.SendAsync(message);
        if (response.IsSuccessStatusCode)
        {
            var str = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadAsAsync<TEntity>();
        }
        else
        {
            return default;
        }

    }
    public async Task Save(TDto entity)
    {
        var content = JsonConvert.SerializeObject(entity, new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All
        });
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Put,
            Content = new StringContent(content, Encoding.UTF8, "application/json"),
            RequestUri = _httpClient.BaseAddress
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateMany(IEnumerable<TDto> entities)
    {
        var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress.AbsoluteUri + "/createmany", entities, JsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<TEntity>> GetAll(string[]? includes)
    {
        includes ??= EmptyIncludes;
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = JsonContent.Create(includes),
            RequestUri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "/getall"),
        };
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<List<TEntity>>();
        }
        else
        {
            return new List<TEntity>();
        }
    }

    public async Task Delete(TDto entity)
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            Content = JsonContent.Create(entity, new MediaTypeHeaderValue("application/json"), JsonOptions ),
            RequestUri = _httpClient.BaseAddress,
        };
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
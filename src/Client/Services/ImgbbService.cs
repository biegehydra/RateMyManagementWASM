using System.Text.Json;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Client.Services;

public class ImgbbService : IImageService
{
    private readonly string _clientKey;
    private static readonly HttpClient _httpClient = new HttpClient();

    public ImgbbService()
    {
        _clientKey = "";
    }

    public async Task<ImgbbUploadResponse> UploadImageAsync(byte[] imageArray)
    {
        try
        {
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            var parameters = new Dictionary<string, string>()
                {
                    {"key", _clientKey},
                    {"image", base64ImageRepresentation},
                    {"name", "test"}
                };
            var req = new HttpRequestMessage(HttpMethod.Post, "https://api.imgbb.com/1/upload");
            req.Content = new FormUrlEncodedContent(parameters.ToArray());
            var response = await _httpClient.SendAsync(req);
            var obj = JsonSerializer.Deserialize<ImgbbUploadResponse>(await response.Content.ReadAsStringAsync());
            return obj;
        }
        catch (Exception e)
        {
            throw new Exception("Error uploading image");
        }
    }

    public async Task DeleteImageAsync(string deleteUrl)
    {
        // Deprecated
    }
}
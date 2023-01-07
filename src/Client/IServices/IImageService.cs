using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Client.IServices
{
    public interface IImageService
    {
        public Task<ImgbbUploadResponse> UploadImageAsync(byte[] image);
        public Task DeleteImageAsync(string deleteUrl);
    }
}

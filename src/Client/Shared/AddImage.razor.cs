using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Client.Shared
{
    public partial class AddImage
    {
        [Inject] private IImageService _imageService { get; set; }
        [Parameter] public EventCallback<ImgbbUploadResponse> ImageUploaded { get; set; }
        private ImgbbUploadResponse? _uploadResponse;
        private string? _displayUrl;
        private bool _error;
        private string _errorMessage = string.Empty;
        private async Task LoadFiles(InputFileChangeEventArgs args)
        {
            if (args.File.Name.Split(".")[1] == "heic")
            {
                return;
            }
            try
            {
                using var mem = new MemoryStream();
                await args.File.OpenReadStream(15000000L).CopyToAsync(mem);
                var result = await _imageService.UploadImageAsync(mem.ToArray());
                if (result.success)
                {
                    if (_uploadResponse != null)
                    {
                        await DeleteImage(_uploadResponse.data.delete_url);
                    }
                    _uploadResponse = result;
                    await ImageUploaded.InvokeAsync(_uploadResponse);
                }
                else
                {
                    _error = true;
                    StateHasChanged();
                    _errorMessage = "Something went wrong uploading the image";
                    await Task.Delay(3000);
                    _error = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured uploading image" + e.Message);
            }
        }
        private async Task DeleteImage(string url)
        {
            if (url != null)
            {
                await _imageService.DeleteImageAsync(url);

            }
        }

        public void SetImage(string url)
        {
            _displayUrl = string.IsNullOrWhiteSpace(url) ? null : url;
        }
        public async Task Reset(bool deleteImage)
        {
            if (deleteImage && _uploadResponse != null)
            {
                await DeleteImage(_uploadResponse.data.delete_url);
            }
            _uploadResponse = null;
        }
    }
}

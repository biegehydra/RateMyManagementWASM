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
        public Action<ImgbbUploadResponse> UploadEvent{ get; set; }
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
                    _uploadResponse = result;
                    UploadEvent.Invoke(_uploadResponse);
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

        public void SetImage(string url)
        {
            _displayUrl = string.IsNullOrWhiteSpace(url) ? null : url;
        }
        public async Task Reset(bool deleteImage)
        {
            _uploadResponse = null;
        }
    }
}

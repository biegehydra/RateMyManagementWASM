using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Client.Shared;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Pages
{
    public partial class CompanyPage : IDisposable
    {
        [Inject] private HttpLocationService _locationService { get; set; }
        [Inject] private HttpCompanyService _companyService { get; set; }
        [Inject] private NavigationManager _navManager { get; set; }

        [Parameter] public string? CompanyId { get; set; }
        private Company _company;
        private CompanyDto _companyDto;
        private List<Location> _queriedLocations = new List<Location>();
        private bool _creatingLocation;
        private bool _editing { get; set; }

        private AddImage? _addImage;

        private string _query = string.Empty;

        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                _queriedLocations = _company.Locations.Where(x => x.Address.Contains(_query) || x.City.Contains(_query)).ToList();
                if (string.IsNullOrWhiteSpace(_query))
                {
                    _queriedLocations = _company.Locations;
                }
            }
        }
        private string _locationAddress = String.Empty;
        private string _locationCity = String.Empty;

        protected override async Task OnInitializedAsync()
        {
            await GetCompanyAsync();
            _companyDto = new CompanyDto()
            {
                Description = _company.Description,
                Id = _company.Id,
                Industry = _company.Industry,
                LogoDeleteUrl = _company.LogoDeleteUrl,
                LogoUrl = _company.LogoUrl,
                Name = _company.Name
            };
            _queriedLocations = _company.Locations;
        }
        private async Task GetCompanyAsync()
        {
            if (CompanyId == null) return;
            _company = await _companyService.Get(CompanyId, HttpCompanyService.AllIncludes);
        }
        private async Task OnSubmitLocation()
        {
            if (string.IsNullOrWhiteSpace(_locationAddress) || string.IsNullOrWhiteSpace(_locationCity)) return;
            var locationDto = new LocationDto()
            {
                Id = Guid.NewGuid().ToString(),
                Address = _locationAddress,
                City = _locationCity,
                CompanyId = _company.Id
            };
            await _locationService.Save(locationDto);
            ResetLocation();
        }
        private void ResetLocation()
        {
            _locationAddress = string.Empty;
            _locationCity = string.Empty;
            _creatingLocation = false;
        }
        private void Navaway(string id)
        {
            var url = $"/company/location/{id}";
            _navManager.NavigateTo(url);
        }
        private async Task OnCompanyChangesSaved()
        {
            await _companyService.Save(_companyDto);
            _editing = !_editing;
            if (_addImage != null)
            {
                await _addImage.Reset(false);
            }
            _company.LogoUrl = _companyDto.LogoUrl;
        }
        private async Task OnBeginEditing()
        {
            _editing = !_editing;
            await Task.Delay(2);
            if (_addImage != null)
            {
                _addImage.UploadEvent += OnImageUploaded;
                _addImage?.SetImage(_company.LogoUrl ?? "");
            }
        }
        private async Task OnStopEditing()
        {
            _editing = !_editing;
            if (_addImage != null)
            {
                await _addImage.Reset(true);
            }
        }
        private void OnImageUploaded(ImgbbUploadResponse response)
        {
            _companyDto.LogoUrl = response.data.display_url;
            _companyDto.LogoDeleteUrl = response.data.delete_url;
        }

        private void ReleaseUnmanagedResources()
        {
            if (_addImage != null && _addImage.UploadEvent != null)
            {
                _addImage!.UploadEvent -= OnImageUploaded;
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~CompanyPage()
        {
            ReleaseUnmanagedResources();
        }
    }
}

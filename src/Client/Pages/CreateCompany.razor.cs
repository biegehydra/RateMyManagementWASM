using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Pages
{
    public partial class CreateCompany
    {
        [Inject] private HttpCompanyService _companyService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Parameter] public string query { get; set; }
        string _companyName = string.Empty;
        string _companyIndustry = string.Empty;
        string _companyDescription = string.Empty;
        string _companyLogoUrl { get; set; } = string.Empty;
        string _companyLogoDeleteUrl { get; set; } = string.Empty;
        string _errorMessage;
        bool _error;
        private async Task CreateCompanyAsync()
        {
            if (string.IsNullOrEmpty(_companyName) || string.IsNullOrEmpty(_companyDescription) ||
                string.IsNullOrEmpty(_companyIndustry))
            {
                return;
            }
            try
            {
                var company = new CompanyDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _companyName,
                    Industry = _companyIndustry,
                    Description = _companyDescription,
                    LogoDeleteUrl = _companyLogoDeleteUrl,
                    LogoUrl = _companyLogoUrl
                };
                await _companyService.Save(company);
                ResetForm();
                NavigateToNewCompany(company.Id);
            }
            catch (Exception e)
            {
                await DisplayError(e, 5000);
            }
        }

        private void NavigateToNewCompany(string id)
        {
            _navigationManager.NavigateTo($"company/{id}");
        }
        private async Task DisplayError(Exception e, int lengthMili)
        {
            _error = true;
            _errorMessage = "Something went wrong: " + e.Message;
            StateHasChanged();
            await Task.Delay(lengthMili);
            _error = false;
            StateHasChanged();
        }

        private void ResetForm()
        {
            _companyName = string.Empty;
            _companyIndustry = string.Empty;
            _companyDescription = string.Empty;
            _companyLogoDeleteUrl = string.Empty;
            _companyLogoUrl = string.Empty;
        }

        public void OnImageUploaded(ImgbbUploadResponse info)
        {
            _companyLogoUrl = info.data.display_url;
            _companyLogoDeleteUrl = info.data.delete_url;
        }
    }
}

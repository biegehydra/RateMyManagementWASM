using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Client.Pages
{
    public partial class CompanyQuery
    {
        [Inject] private HttpCompanyService companyService { get; set; }
        [Inject] private NavigationManager navManager { get; set; }
        [Parameter]
        public string? Query { get; set; }

        private bool _loading;
        public List<Company> CompaniesQueried { get; set; } = new List<Company>();
        protected override async Task OnParametersSetAsync()
        {
            CompaniesQueried = new List<Company>();
            await GetQueriedCompanies();
        }

        private async Task GetQueriedCompanies()
        {
            if (Query == null) return;
            _loading = true;
            var temp = await companyService.Query(Query, HttpCompanyService.AllIncludes);
            CompaniesQueried = temp.ToList();
            _loading = false;
        }

        private void NavigateToCompany(string companyId)
        {
            navManager.NavigateTo($"/company/{companyId}");
        }
    }
}

using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Pages
{
    public partial class CompanyQuery
    {
        [Inject] private HttpCompanyService companyService { get; set; }
        [Inject] private NavigationManager navManager { get; set; }
        [Parameter]
        public string? Query { get; set; }

        private bool _loading;
        public List<CompanyWithRatingDto> CompaniesQueried { get; set; } = new ();
        protected override async Task OnParametersSetAsync()
        {
            CompaniesQueried = new ();
            await GetQueriedCompanies();
        }

        private async Task GetQueriedCompanies()
        {
            if (Query == null) return;
            _loading = true;
            var temp = await companyService.QueryWithRatings(Query);
            CompaniesQueried = temp.ToList();
            _loading = false;
        }

        private void NavigateToCompany(string companyId)
        {
            navManager.NavigateTo($"/company/{companyId}");
        }
    }
}

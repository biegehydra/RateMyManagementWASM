using Microsoft.AspNetCore.Components;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Client.Paginiation;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Client.Shared.Layout;
using RateMyManagementWASM.Shared;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;
using RateMyManagementWASM.Shared.Requests;

namespace RateMyManagementWASM.Client.Pages
{
    public partial class Index
    {
        [Inject] private IAdminService _adminService { get; set; }
        [Inject] private HttpLocationService _locationService { get; set; }
        [Inject] private HttpCompanyService companyService { get; set; }
        [Inject] private NavigationManager navManager { get; set; }
        [CascadingParameter] private MainLayout? _layout { get; set; }
        private List<CompanyWithRatingDto> _currentPageCompanies = new ();
        private bool _loading;
        private PagingMetaData? _pagingMetaData = new PagingMetaData()
        {
            CurrentPage = 0,
            PageSize = 0, // Doesn't matter
            TotalCount = 0, // Doesn't matter
            TotalPages = 2
        };

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1);
            _layout?.DisableSearch();
            _layout?.DisableLogo();
        }

        private async Task OnPageSelected(string str)
        {
            try
            {
                _currentPageCompanies = new List<CompanyWithRatingDto>();
                _loading = true;
                var response = await companyService.QueryWithRatings(str, true);
                _currentPageCompanies = response.ToList();
            }
            catch (Exception exception)
            {
                var e = exception;
            }
            finally
            {
                _loading = false;
            }
        }
        private async Task PopulateSite()
        {
            var populateDbRequest = new PopulateDbRequest()
            {
                Companies = 100,
                LocationsPerCompany = 20,
                LocationReviewsPerLocation = 20
            };
            //TODO fix
            try
            {
                await _adminService.PopulateDb(populateDbRequest);
            }
            catch (Exception e)
            {
                var exc = e;
            }
        }
    }
}

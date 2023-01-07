using System.Globalization;
using Microsoft.AspNetCore.Components;
using Radzen;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Pages
{
    public partial class LocationPage
    {
        [Inject] HttpLocationService _locationService { get; set; }
        [Parameter] public string Id { get; set; }
        private Location _location { get; set; }
        private LocationDto _locationDto { get; set; }
        private bool _editing;

        protected override async Task OnInitializedAsync()
        {
            _location = await _locationService.Get(Id, HttpLocationService.AllIncludes);
            _locationDto = new LocationDto()
            {
                Id = _location.Id,
                City = _location.City,
                Address = _location.Address,
                CompanyId = _location.CompanyId
            };
            _location.LocationReviews = _location.LocationReviews.OrderByDescending(x => DateTime.Parse(x.SentDateAndTime, CultureInfo.InvariantCulture)).ToList();
        }
        private async Task OnSubmitReview()
        {
            await OnInitializedAsync();
        }
        private async Task OnSaveLocationChanges(LocationDto location)
        {
            await _locationService.Save(location);
            _editing = !_editing;
        }
        private void OnSortByClicked(MenuItemEventArgs args)
        {
            switch (args.Text)
            {
                case "Most Recent":
                    _location.LocationReviews = _location.LocationReviews.OrderByDescending(x => DateTime.Parse(x.SentDateAndTime, CultureInfo.InvariantCulture)).ToList();
                    break;
                case "Least Recent":
                    _location.LocationReviews = _location.LocationReviews.OrderBy(x => DateTime.Parse(x.SentDateAndTime, CultureInfo.InvariantCulture)).ToList();
                    break;
                case "Sender Username":
                    _location.LocationReviews = _location.LocationReviews.OrderBy(x => x.SenderUsername).ToList();
                    break;
                case "Employee Name":
                    _location.LocationReviews = _location.LocationReviews.OrderBy(x => x.ManagerName).ToList();
                    break;

            }

        }

    }
}

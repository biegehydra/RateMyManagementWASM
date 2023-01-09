using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using RateMyManagementWASM.Client.Services;
using RateMyManagementWASM.Shared;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Shared
{
    public partial class LocationReviewForm
    {
        [Inject] private HttpLocationReviewService _locationReviewService { get; set; }
        [Inject] private AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public LocationReviewDto LocationReviewDto { get; set; } = new LocationReviewDto();
        private string _locaionId;
        [Parameter] public string LocationId
        {
            get => _locaionId;
            set
            {
                _locaionId = value;
                LocationReviewDto.LocationId = value;
            }
        }
        [Parameter] public EventCallback SubmitCallback { get; set; }
        private IEnumerable<string> _multiplateAttributes = new List<string>();
        public IEnumerable<string> MultipleAttributes
        {
            get { return _multiplateAttributes; }
            set
            {
                _multiplateAttributes = value;
                string temp = string.Empty;
                foreach (var str in value)
                {
                    temp += str + ",";
                }
                if (temp.Length > 0)
                {
                    temp.TrimEnd(',');
                }
                LocationReviewDto.ManagerAttributes = temp;
            }
        }
        private bool _creatingReview;
        private List<string> ManagerTypes = ExtensionMethods.GetEnumNamesCorrected<ManagerType>();
        private List<string> ManagerAttributes = ExtensionMethods.GetEnumNamesCorrected<ManagerAttribute>();
        private ClaimsPrincipal? _claimsPrincipal;

        private string? _managerType;

        private int _reviewType;
        private int ReviewType
        {
            get { return _reviewType; }
            set
            {
                _reviewType = value;
                OnReviewTypeChange(_reviewType);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _claimsPrincipal = authState.User;
        }

        private void OnReviewTypeChange(int args)
        {
            LocationReviewDto.Type = args == 1 ? RateMyManagementWASM.Shared.Data.ReviewType.Employee.ToString() : RateMyManagementWASM.Shared.Data.ReviewType.Customer.ToString();
        }
        private void OnManagerTypeSelectionChange(object args)
        {
            var asString = args.ToString();
            ManagerType managerType;
            if (asString != null && asString.MatchesEnumItem(out managerType))
            {
                LocationReviewDto.ManagerType = managerType.ToString();
            }

        }

        private void OnCancelCreateReview()
        {
            ResetForm();
            _creatingReview = false;
        }
        private async Task OnSubmitReview()
        {
            if (_claimsPrincipal == null || _claimsPrincipal.Identity == null || _claimsPrincipal.Identity.Name == null) return;
            LocationReviewDto.SenderUsername = _claimsPrincipal.Identity.Name;
            LocationReviewDto.Id = Guid.NewGuid().ToString();
            LocationReviewDto.LocationId = LocationId;
            LocationReviewDto.ApplicationUserId = _claimsPrincipal.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            await _locationReviewService.Save(LocationReviewDto);
            ResetForm();
            _creatingReview = false;
            await SubmitCallback.InvokeAsync();
        }
        private void ResetForm()
        {
            LocationReviewDto = new LocationReviewDto();
            MultipleAttributes = new List<string>();
            _managerType = null;
            ReviewType = 0;
        }
    }
}

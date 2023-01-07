using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Services
{
    public class HttpLocationReviewService : EntityDbService<LocationReview, LocationReviewDto>
    {
        public HttpLocationReviewService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            AllIncludes = new[] {"Location", "Location.Company"};
        }
    }
}

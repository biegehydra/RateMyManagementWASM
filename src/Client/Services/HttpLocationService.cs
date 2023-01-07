using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Client.Services;

public class HttpLocationService : EntityDbService<Location, LocationDto>
{
    public HttpLocationService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        AllIncludes = new[] { "Company", "LocationReviews" };
    }
}
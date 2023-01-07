using AutoMapper;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Shared.Profiles
{
    public class LocationReviewProfile : Profile
    {
        public LocationReviewProfile()
        {
            CreateMap<LocationReview, LocationReviewDto>();
        }
    }
}

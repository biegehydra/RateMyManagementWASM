using AutoMapper;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Shared.ValueResolvers
{
    public class CompanyRatingResolver : IValueResolver<Company, CompanyWithRatingDto, float>
    {
        public float Resolve(Company source, CompanyWithRatingDto destination, float destMember, ResolutionContext context)
        {
            return source.Locations.Average(x => x.GetRating());
        }
    }
}

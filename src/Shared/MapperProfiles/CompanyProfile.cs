using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;
using RateMyManagementWASM.Shared.ValueResolvers;

namespace RateMyManagementWASM.Shared.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyWithRatingDto>()
                .ForMember(dest => dest.Rating, x => x.MapFrom<CompanyRatingResolver>())
                .ForMember(dest => dest.LogoUrl, x => x.MapFrom<CompanyLogoUrlResolver>());
        }
    }
}

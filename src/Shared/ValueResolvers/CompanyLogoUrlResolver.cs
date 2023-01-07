using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Shared.ValueResolvers
{
    public class CompanyLogoUrlResolver : IValueResolver<Company, CompanyWithRatingDto, string>
    {
        public string Resolve(Company source, CompanyWithRatingDto destination, string? destMember, ResolutionContext context)
        {
            return source.GetLogoUrl();
        }
    }
}

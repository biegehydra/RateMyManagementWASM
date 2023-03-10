using Microsoft.AspNetCore.Identity;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Server.Models
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        public List<LocationReview> Reviews { get; set; }
    }
}
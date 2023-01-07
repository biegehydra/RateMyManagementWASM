using Microsoft.AspNetCore.Authorization;
using RateMyManagementWASM.Client.Configuration.Requirements;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Client.Configuration
{
    public class AuthorizationOptionsConfigurer
    {
        public static void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(PolicyTypes.Admin.ToString(), policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new AdminRequirement());
            });
            options.AddPolicy(PolicyTypes.CompanyManager.ToString(), policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new IdMatchesCompanyRequirement());
            });
            options.AddPolicy(PolicyTypes.LocationManager.ToString(), policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new IdMatchesLocationRequirement());
            });
            options.AddPolicy(PolicyTypes.AuthorizedUser.ToString(), policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using RateMyManagementWASM.Client.Configuration.Requirements;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Client.Configuration.Handlers
{
    public class CompanyManagerHandler : AuthorizationHandler<IdMatchesCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdMatchesCompanyRequirement requirement)
        {
            if (context.Resource is Company company)
            {
                if (context.User.IsInRole("Administrator") || context.User.HasClaim(
                        claim => claim.Type == ClaimTypes.EditCompany.ToString()
                                 && claim.Value == company.Id))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail(new AuthorizationFailureReason(this, "Claim was not present"));
                }
            }
            else
            {
                context.Fail(new AuthorizationFailureReason(this, "Context missing company resource"));
            }
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using RateMyManagementWASM.Client.Configuration.Requirements;

namespace RateMyManagementWASM.Client.Configuration.Handlers
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var name = context.User.Identity.Name;
            var claims = context.User.Claims.ToList();
            //var other = context.User.Claims.Where(x => x.);
            if (context.User.IsInRole("Administrator"))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}

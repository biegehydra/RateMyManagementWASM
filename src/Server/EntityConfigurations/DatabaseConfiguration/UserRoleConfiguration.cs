using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RateMyManagementWASM.Server.EntityConfigurations.DatabaseConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "eb4e7559-826c-48d8-97e0-47926af096d0",
                    UserId = "0bbb2a21-f429-49d0-add1-f5b8fc0103f4"
                }
            );
        }
    }
}

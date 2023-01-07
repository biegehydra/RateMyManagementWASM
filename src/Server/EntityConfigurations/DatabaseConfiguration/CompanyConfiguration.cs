using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Server.EntityConfigurations.DatabaseConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasMany(x => x.Locations)
                .WithOne(x => x.Company);
            builder.HasKey(x => x.Id);
        }
    }
}
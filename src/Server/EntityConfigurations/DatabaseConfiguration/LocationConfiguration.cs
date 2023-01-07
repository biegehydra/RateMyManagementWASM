using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Server.EntityConfigurations.DatabaseConfiguration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");
            builder.HasOne(x => x.Company)
                .WithMany(x => x.Locations);
            builder.HasKey(x => x.Id);

        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMyManagementWASM.Shared.Data;

namespace RateMyManagementWASM.Server.EntityConfigurations.DatabaseConfiguration
{
    public class LocationReviewConfiguration : IEntityTypeConfiguration<LocationReview>
    {
        public void Configure(EntityTypeBuilder<LocationReview> builder)
        {
            builder.ToTable("LocationReviews");
            builder.HasOne(x => x.Location)
                .WithMany(x => x.LocationReviews);
            builder.HasKey(x => x.Id);
        }
    }
}

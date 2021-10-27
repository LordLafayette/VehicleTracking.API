using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Domains.VehicleProfiles;

namespace VehicleTracking.Infrastructure.Data.VehicleTrackingContexts.EntityTypeConfigurations
{
    public class VehicleProfileEntityTypeConfiguration : IEntityTypeConfiguration<VehicleProfile>
    {
        public void Configure(EntityTypeBuilder<VehicleProfile> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.LastLocationName)
                .HasMaxLength(256);

            builder.Property(e => e.LicensePlate)
                .HasMaxLength(10);

            builder.Property(e => e.VehicleType)
                .HasConversion<int>();

            builder.HasMany(e => e.VehicleHistories)
                .WithOne()
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(h => h.VehicleProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class VehicleHistoryEntityTypeConfiguration : IEntityTypeConfiguration<VehicleHistory>
    {
        public void Configure(EntityTypeBuilder<VehicleHistory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.LocationName)
                .HasMaxLength(256);
        }
    }
}

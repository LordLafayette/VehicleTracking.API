using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Domains;

namespace VehicleTracking.Infrastructure.Data.VehicleTrackingContexts.EntityTypeConfigurations
{
    public class DriverInfoEntityTypeConfiguration : IEntityTypeConfiguration<DriverInfo>
    {
        public void Configure(EntityTypeBuilder<DriverInfo> builder)
        {
            builder.HasKey(e => e.IdentityId);

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.LicenseNumber)
                .HasMaxLength(50);

            builder.HasMany(e => e.VehicleProfiles)
                .WithOne(v => v.DriverInfo)
                .HasForeignKey(e => e.DriverId)
                .HasPrincipalKey(e => e.IdentityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

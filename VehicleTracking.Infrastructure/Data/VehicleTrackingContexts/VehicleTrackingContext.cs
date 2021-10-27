using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Infrastructure.Data.VehicleTrackingContexts.EntityTypeConfigurations;

namespace VehicleTracking.Infrastructure.Data
{
    public class VehicleTrackingContext : IdentityDbContext
    {
        public VehicleTrackingContext(DbContextOptions<VehicleTrackingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new VehicleProfileEntityTypeConfiguration());
            builder.ApplyConfiguration(new DriverInfoEntityTypeConfiguration());

            Seed(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>(builder =>
            {
                builder.HasData(new IdentityRole
                {
                    Id = "1",
                    Name = "Driver",
                    ConcurrencyStamp = "1",
                    NormalizedName = "DRIVER"
                }, new IdentityRole
                {
                    Id = "2",
                    Name = "ADMIN",
                    ConcurrencyStamp = "2",
                    NormalizedName = "ADMIN"
                });
            });
        }
    }
}

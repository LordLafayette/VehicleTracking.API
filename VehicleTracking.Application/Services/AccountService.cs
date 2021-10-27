using EnsureThat;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.Resources;
using VehicleTracking.Core.Domains;
using VehicleTracking.Core.Domains.VehicleProfiles;
using VehicleTracking.Infrastructure.Data;
using VehicleTracking.Infrastructure.Repositories;
using VehicleTracking.Infrastructure.UnitOfWork;

namespace VehicleTracking.Application.Services
{
    public class AccountService : ApplicationService, IAccountService
    {
        private IVehicleTrackingUnitOfWork uow;
        private IVehicleTrackingRepository<DriverInfo> driverRepo;
        private UserManager<IdentityUser> userManager;
        public AccountService(
            UserManager<IdentityUser> userManager,
            IVehicleTrackingRepository<DriverInfo> driverRepo,
            IVehicleTrackingUnitOfWork uow
            )
        {
            this.userManager = userManager;
            this.driverRepo = driverRepo;
            this.uow=uow;
        }

        public async Task<IdentityResult> Register(RegisterRequest request)
        {
            EnsureArg.IsNotNull(request, nameof(request));
            request.Validate();
            var user = new IdentityUser(request.Username)
            {
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var driver = new DriverInfo(user.Id, request.Name, request.LastName, request.LicenseNumber);
                driver.AddVehicle(request.LicensePlate, VehicleType.Truck);
                driverRepo.Add(driver);
                await uow.SaveChange();
            }

            return result;
        }
    }
}

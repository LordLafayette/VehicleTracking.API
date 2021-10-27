using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.Resources;
using VehicleTracking.Core.Domains;
using VehicleTracking.Infrastructure.Repositories;

namespace VehicleTracking.Application.Services
{
    public class DriverService : ApplicationService, IDriverService
    {
        private IMapper mapper;
        private IVehicleTrackingRepository<DriverInfo> driverRepo;
        public DriverService(
            IMapper mapper,
            IVehicleTrackingRepository<DriverInfo> driverRepo)
        {
            this.mapper = mapper;
            this.driverRepo = driverRepo;
        }

        public async Task<DriverInformationDto> GetDriverInfomation(string driverId)
        {
            var driver = await driverRepo
                .GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(f=>f.IdentityId == driverId);

            return mapper.Map<DriverInformationDto>(driver);
        }

        public async Task<IEnumerable<VehicleProfileDto>> GetVehicleProfiles(string driverId)
        {
            var driver = await driverRepo
                .GetQueryable()
                .AsNoTracking()
                .Include(e => e.VehicleProfiles)
                .FirstOrDefaultAsync(f => f.IdentityId == driverId);

            if (driver == null)
                return null;

            return mapper.Map<IEnumerable<VehicleProfileDto>>(driver.VehicleProfiles);
        }

        public async Task UpdateVehicleStatus(string driverId, int vehicleId, UpdateVehicleInformationRequest request)
        {
            request.Validate();

            var driver = await driverRepo
                .GetQueryable()
                .AsNoTracking()
                .Include(e => e.VehicleProfiles.Where(m => m.Id == vehicleId))
                .FirstOrDefaultAsync(f => f.IdentityId == driverId);

            if (driver == null)
            {
                return;
            }

            var vehicle = driver.VehicleProfiles.FirstOrDefault();
            if(vehicle == null)
            {
                return;
            }

            vehicle.UpdateStatus(request.Lat.Value, request.Lng.Value, request.Fule, request.Speed, request.Location);
        }
    }
}

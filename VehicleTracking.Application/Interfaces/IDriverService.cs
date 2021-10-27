using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Resources;

namespace VehicleTracking.Application.Interfaces
{
    public interface IDriverService : IApplicationService
    {
        Task<DriverInformationDto> GetDriverInfomation(string driverId);
        Task<IEnumerable<VehicleProfileDto>> GetVehicleProfiles(string driverId);

        Task UpdateVehicleStatus(string driverId, int vehicleId, UpdateVehicleInformationRequest request);
    }
}

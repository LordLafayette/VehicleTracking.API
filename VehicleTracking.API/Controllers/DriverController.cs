using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.Resources;

namespace VehicleTracking.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DriverController : VehicleTrackingControllerBase
    {
        IDriverService driverService;
        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        #region Me

        [HttpGet("")]
        public Task<DriverInformationDto> GetMyDriverInfomation() => driverService.GetDriverInfomation(IdentityId);

        [HttpGet("Vehicle")]
        public Task<IEnumerable<VehicleProfileDto>> GetMyVehicleProfiles() => driverService.GetVehicleProfiles(IdentityId);

        [HttpPut("Vehicle/{id:int}")]
        public Task UpdateMyVehicleStatus(int id, [FromBody] UpdateVehicleInformationRequest request) => driverService.UpdateVehicleStatus(IdentityId, id, request);

        #endregion


        #region Admin



        #endregion
    }
}

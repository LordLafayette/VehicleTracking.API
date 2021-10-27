using Microsoft.AspNetCore.Mvc;

namespace VehicleTracking.API.Controllers
{
    public abstract class VehicleTrackingControllerBase : ControllerBase
    {
        public string IdentityId { get; set; }
        public string Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace VehicleTracking.IdentityServer.Resources
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

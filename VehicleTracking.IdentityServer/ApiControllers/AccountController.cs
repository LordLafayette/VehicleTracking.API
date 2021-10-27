using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleTracking.Application.Interfaces;
using VehicleTracking.Application.Resources;

namespace VehicleTracking.IdentityServer.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public Task<IdentityResult> Register([FromBody] RegisterRequest request) => accountService.Register(request);
    }
}

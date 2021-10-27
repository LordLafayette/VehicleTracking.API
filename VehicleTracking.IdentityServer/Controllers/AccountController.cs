using EnsureThat;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using VehicleTracking.IdentityServer.Resources;

namespace VehicleTracking.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> signInManager;
        public AccountController(SignInManager<IdentityUser> signInManager,IOptions<AuthenticationOptions> options)
        {
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, true, true);
            

            return Ok(result);
        }

    }
}

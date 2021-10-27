using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.API.Controllers;

namespace VehicleTracking.API.Filters
{
    public class RequestInformationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is VehicleTrackingControllerBase)
            {
                var con = (VehicleTrackingControllerBase)context.Controller;

                var identity = (ClaimsIdentity)context.HttpContext.User.Identity;
                if (identity.IsAuthenticated)
                {
                    con.IdentityId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    con.Role = identity.FindFirst(identity.RoleClaimType)?.Value;
                }
            }
        }
    }
}

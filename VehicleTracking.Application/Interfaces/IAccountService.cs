using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Resources;

namespace VehicleTracking.Application.Interfaces
{
    public interface IAccountService : IApplicationService
    {
        Task<IdentityResult> Register(RegisterRequest request);
    }
}

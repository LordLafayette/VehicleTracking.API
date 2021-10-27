using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Application.Interfaces;

namespace VehicleTracking.Application.Services
{
    public abstract class ApplicationService : IApplicationService
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}

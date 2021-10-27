using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Application.Interfaces
{
    public interface IApplicationService
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}

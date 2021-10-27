using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Interfaces;
using VehicleTracking.Infrastructure.Data;

namespace VehicleTracking.Infrastructure.UnitOfWork
{
    public class VehicleTrackingUnitOfWork : UnitOfWork, IVehicleTrackingUnitOfWork
    {
        public VehicleTrackingUnitOfWork(VehicleTrackingContext db) : base(db)
        {
            
        }

    }

    public interface IVehicleTrackingUnitOfWork : IUnitOfWork
    {

    }
}

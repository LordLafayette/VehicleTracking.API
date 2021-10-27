using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Interfaces;
using VehicleTracking.Infrastructure.Data;

namespace VehicleTracking.Infrastructure.Repositories
{
    public class VehicleTrackingRepository<TEntity> : Repository<TEntity>, IVehicleTrackingRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        public VehicleTrackingRepository(VehicleTrackingContext db) : base(db)
        {
        }
    }

    public interface IVehicleTrackingRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {

    }

}

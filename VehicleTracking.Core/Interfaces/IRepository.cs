using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> GetQueryable();
        void Add(TEntity entity);
    }
}

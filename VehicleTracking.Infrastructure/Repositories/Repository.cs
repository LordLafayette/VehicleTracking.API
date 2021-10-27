using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Interfaces;

namespace VehicleTracking.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        DbContext db;
        public Repository(DbContext db)
        {
            this.db = db;
        }
        public virtual void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity); ;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return db.Set<TEntity>().AsQueryable();
        }
    }
}

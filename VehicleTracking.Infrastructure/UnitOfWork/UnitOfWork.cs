using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Interfaces;

namespace VehicleTracking.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        DbContext db;
        public UnitOfWork(DbContext db)
        {
            this.db = db;
        }
        public virtual Task SaveChange()
        {
            return db.SaveChangesAsync();
        }
    }
}

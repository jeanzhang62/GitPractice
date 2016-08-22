using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutomobileMaintenanceTracker.Models
{
    public interface IAMTDb :IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        T Get<T>(int id) where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void SaveChanges();
    }
    public class AMTDb :DbContext, IAMTDb
    {
        public AMTDb():base("name=AMTConnection")
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<MaintenanceService> MaintenanceServices { get; set; }
        public DbSet<Technician> Technician { get; set; }
        public DbSet<CarType> CarTypes { get; set; }

       
        IQueryable<T> IAMTDb.Query<T>()
        {
            return Set<T>();
        }

        public T Get<T>(int id) where T : class
        {
            return Set<T>().Find(id);
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        void IAMTDb.SaveChanges()
        {
            SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Remove<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }
    }
}
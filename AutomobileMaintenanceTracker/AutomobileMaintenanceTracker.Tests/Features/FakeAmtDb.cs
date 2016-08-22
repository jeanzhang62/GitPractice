using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileMaintenanceTracker.Tests.Features
{
    class FakeAmtDb : IAMTDb
    {
        public void Add<T>(T entity) where T : class
        {
            Added.Add(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }

        public void AddSet<T>(IQueryable<T> objects)
        {
            Sets.Add(typeof(T), objects);
        }

        public void SaveChanges()
        {
            Saved = true;
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();

        public List<object> Added = new List<object>();

        public bool Saved = true;
    }
}

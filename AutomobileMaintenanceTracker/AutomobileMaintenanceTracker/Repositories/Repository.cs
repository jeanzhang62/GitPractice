using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;


namespace AutomobileMaintenanceTracker.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        internal AMTDb context;
        internal DbSet<TEntity> dbSet;

        public Repository(AMTDb context)
        {
            context.Configuration.ProxyCreationEnabled = false;
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }
    

        public virtual bool Add(TEntity entity)
        {
            dbSet.Add(entity);

            return true;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual bool Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return true;
        }
    }
}


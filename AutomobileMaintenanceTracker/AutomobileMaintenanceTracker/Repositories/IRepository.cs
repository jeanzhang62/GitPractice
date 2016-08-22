using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileMaintenanceTracker.Repositories
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        IList<T> SearchFor(Expression<Func<T, bool>> filter);
        IList<T> GetAll();
        T GetById(int id);
    }
}

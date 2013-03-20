using System;
using System.Linq;
using System.Linq.Expressions;
using Infra.Model;

namespace Infra.Interfaces
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        IQueryable<T> All(Expression<Func<T, Boolean>> predicate = null, params Expression<Func<T, Object>>[] includes);

        Boolean Any(Expression<Func<T, Boolean>> predicate = null);

        Int32 Count(Expression<Func<T, Boolean>> predicate = null);
        
        T One(Int32 id);

        T One(Expression<Func<T, Boolean>> predicate = null, params Expression<Func<T, Object>>[] includes);
        
        void Remove(T entity);
        
        T Save(T entity);
    }
}

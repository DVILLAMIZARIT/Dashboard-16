using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using Infra.Model;

namespace Infra.Interfaces
{
    [ContractClass(typeof(IRepositoryContract<>))]
    public interface IRepository<T> : IDisposable
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

    [ContractClassFor(typeof(IRepository<>))]
    abstract class IRepositoryContract<T> : IRepository<T>
        where T : class, IEntity
    {
        IQueryable<T> IRepository<T>.All(Expression<Func<T, Boolean>> predicate, params Expression<Func<T, Object>>[] includes)
        {
            Contract.Ensures(Contract.Result<IQueryable<T>>() != null);
            
            return Enumerable.Empty<T>().AsQueryable();
        }

        Boolean IRepository<T>.Any(Expression<Func<T, Boolean>> predicate)
        {
            Contract.Requires<ArgumentNullException>(predicate != null);
            
            return default(Boolean);
        }

        Int32 IRepository<T>.Count(Expression<Func<T, Boolean>> predicate)
        {
            Contract.Requires<ArgumentNullException>(predicate != null);
            Contract.Ensures(Contract.Result<Int32>() >= 0);

            return default(Int32);
        }

        T IRepository<T>.One(Int32 id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            
            return default(T);
        }

        T IRepository<T>.One(Expression<Func<T, Boolean>> predicate, params Expression<Func<T, Object>>[] includes)
        {
            Contract.Requires<ArgumentNullException>(predicate != null);

            return default(T);
        }

        void IRepository<T>.Remove(T entity)
        {
            Contract.Requires<ArgumentNullException>(entity != null);
        }

        T IRepository<T>.Save(T entity)
        {
            Contract.Requires<ArgumentNullException>(entity != null);

            return default(T);
        }

        #region IDisposable
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using Infra.Interfaces;
using Infra.Model;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected DataContext Context { get; private set; }

        #region Ctor

        public Repository()
            : this(new DataContextFactory().Create())
        {
        }
        public Repository(DataContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            this.Context = context;
        }

        #endregion

        #region IRepository<T>

        public virtual IQueryable<T> All(Expression<Func<T, Boolean>> predicate = null, params Expression<Func<T, Object>>[] includes)
        {
            var set = this.CreateIncludeSet(includes);

            return predicate != null ? set.Where(predicate) : set;
        }

        public virtual Boolean Any(Expression<Func<T, Boolean>> predicate = null)
        {
            var set = this.CreateSet();

            return predicate != null ? set.Any(predicate) : set.Any();
        }

        public virtual Int32 Count(Expression<Func<T, Boolean>> predicate = null)
        {
            var set = this.CreateSet();

            return predicate != null ? set.Count(predicate) : set.Count();
        }

        public virtual T One(Int32 id)
        {
            return this.CreateSet().Find(id);
        }

        public virtual T One(Expression<Func<T, Boolean>> predicate = null, params Expression<Func<T, Object>>[] includes)
        {
            var set = this.CreateIncludeSet(includes);

            return predicate != null ? set.FirstOrDefault(predicate) : set.FirstOrDefault();
        }

        public virtual void Remove(T entity)
        {
            entity = this.GetOrAttach(entity);

            var attachedEntity = this.GetOrAttach(entity);
            if (attachedEntity is IDeletableEntity)
            {
                ((IDeletableEntity)attachedEntity).IsDeleted = true;
                this.Save(entity);
            }
            else
            {
                this.CreateSet().Remove(attachedEntity);
            }
        }

        public virtual T Save(T entity)
        {
            if (entity.Id == default(Int32))
            {
                entity = this.CreateSet().Add(entity);
            }
            else
            {
                entity = this.GetOrAttach(entity);
                this.Context.MarkAsModified<T>(entity);
            }
            return entity;
        }

        #endregion

        #region Helpers

        private IDbSet<T> CreateIncludeSet(IEnumerable<Expression<Func<T, Object>>> includes)
        {
            var set = this.CreateSet();
            
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    set.Include(include);
                }
            }

            return set;
        }

        private IDbSet<T> CreateSet()
        {
            return this.Context.CreateSet<T>();
        }

        protected Boolean IsAttached(T entity)
        {
            Contract.Requires<ArgumentNullException>(entity != null);

            return this.Context.Set<T>().Local.Any(x => x == entity);
        }

        protected T GetOrAttach(T entity)
        {
            Contract.Requires<ArgumentNullException>(entity != null);

            if (this.IsAttached(entity))
            {
                return entity;
            }
            return this.Context.Set<T>().Attach(entity);
        }

        #endregion
    }
}

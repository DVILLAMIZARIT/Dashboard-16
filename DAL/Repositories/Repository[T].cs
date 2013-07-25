using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces;
using Infra.Interfaces;
using Infra.Model;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected DataContext Context { get; private set; }

        #region Ctor

        public Repository(IDataContextFactory contextFactory)
        {
            Contract.Requires<ArgumentNullException>(contextFactory != null);

            this.Context = contextFactory.Create();
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
            this.Context.SaveChanges();
            return entity;
        }

        #endregion

        #region IDisposable

        private Boolean disposed;

        void IDisposable.Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                this.disposed = true;
            }
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }

        ~Repository()
        {
            this.Dispose(false);
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
            //else
            //{
            //    IObjectContextAdapter adapter = this.Context as IObjectContextAdapter;
            //    EntityContainer container = adapter.ObjectContext.MetadataWorkspace.GetEntityContainer(adapter.ObjectContext.DefaultContainerName, DataSpace.CSpace);
            //    String entitySetName = container.BaseEntitySets.FirstOrDefault(x => x.ElementType.Name == typeof(T).Name).Name;
            //    String entityName = String.Join(".", container.Name, entitySetName);
                
            //    Object original;
            //    EntityKey entityKey = adapter.ObjectContext.CreateEntityKey(entityName, entity);
            //    if (adapter.ObjectContext.TryGetObjectByKey(entityKey, out original))
            //    {
            //        adapter.ObjectContext.ApplyCurrentValues<T>(entityKey.EntitySetName, entity);
            //    }
            //}
            return this.Context.Set<T>().Attach(entity);
        }

        #endregion
    }
}

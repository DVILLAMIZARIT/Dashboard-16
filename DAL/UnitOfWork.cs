using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Diagnostics.Contracts;
using System.Linq;
using DAL.Interfaces;

namespace DAL
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        public TContext Context { get; private set; }

        public Int32 PendingChanges
        {
            get
            {
                IObjectContextAdapter objContext = this.Context as IObjectContextAdapter;
                if (objContext != null)
                {
                    ObjectStateManager stateManager = objContext.ObjectContext.ObjectStateManager;
                    return stateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified).Count();
                }
                return -1;
            }
        }

        public UnitOfWork(TContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            this.Context = context;
        }

        public virtual void Commit()
        {
            this.Context.SaveChanges();
        }

        #region IDisposable

        ~UnitOfWork()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        private Boolean disposed;

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(Boolean disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                }
                this.disposed = true;
            }
        }

        #endregion
    }
}

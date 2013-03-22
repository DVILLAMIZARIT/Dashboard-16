using System;
using System.Diagnostics.Contracts;
using DAL.Interfaces;

namespace DAL
{
    public class DataContextFactory : IDataContextFactory
    {
        private DataContext context;

        private readonly String nameOrConnectionString;

        #region Ctor

        public DataContextFactory()
            : this("DataContext")
        {
        }
        public DataContextFactory(String nameOrConnectionString)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(nameOrConnectionString));

            this.nameOrConnectionString = nameOrConnectionString;
        }

        #endregion

        protected virtual DataContext CreateContext()
        {
            return new DataContext(this.nameOrConnectionString);
        }

        public virtual DataContext Create()
        {
            return context ?? (context = this.CreateContext());
        }

        #region IDisposable

        ~DataContextFactory()
        {
            this.Dispose(false);
        }

        private Boolean disposed = false;
        
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean isDisposing)
        {
            if (isDisposing && !this.disposed)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }

        #endregion
    }
}

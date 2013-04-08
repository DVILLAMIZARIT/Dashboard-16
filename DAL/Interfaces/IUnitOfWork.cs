using System;
using System.Data.Entity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        TContext Context { get; }

        Int32 PendingChanges { get; }

        void Commit();
    }
}

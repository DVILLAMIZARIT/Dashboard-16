using System;
using System.Data.Entity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        DbContext Context { get; }

        Int32 PendingChanges { get; }

        void Commit();
    }
}

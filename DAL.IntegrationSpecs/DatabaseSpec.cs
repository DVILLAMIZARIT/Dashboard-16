using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using Machine.Specifications;

namespace DAL.IntegrationSpecs
{
    public abstract class DatabaseSpec
    {
        protected static DataContextFactory<DataContext> contextFactory;
        protected static UnitOfWork<DataContext> unitOfWork;
        private static DbTransaction transaction;

        Establish context = () =>
        {
            contextFactory = new DataContextFactory<DataContext>("test");
            var context = contextFactory.GetContext();
            unitOfWork = new UnitOfWork<DataContext>(context);

            IObjectContextAdapter adapter = context;

            if ((adapter.ObjectContext.Connection.State & ConnectionState.Open) != ConnectionState.Open)
            {
                adapter.ObjectContext.Connection.Open();
            }

            transaction = adapter.ObjectContext.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        };

        Cleanup on_exit = () =>
        {
            transaction.Rollback();
            transaction.Dispose();

            contextFactory.Dispose();
        };
    }
}

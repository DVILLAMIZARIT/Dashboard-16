using System.Data.Entity;

namespace DAL
{
    public class NullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
        }
    }
}

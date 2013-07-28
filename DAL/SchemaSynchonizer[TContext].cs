using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace DAL
{
    public class SchemaSynchonizer<TContext>
        where TContext : DbContext
    {
        private readonly Func<Boolean> debugMode;
        private readonly IDatabaseInitializer<TContext> productionInitializer;

        public SchemaSynchonizer(Func<Boolean> debugMode, IDatabaseInitializer<TContext> productionInitializer = null)
        {
            Contract.Requires<ArgumentNullException>(debugMode != null);

            this.debugMode = debugMode;
            this.productionInitializer = productionInitializer;
        }

        public void Execute()
        {
            if (debugMode())
            {
                Database.SetInitializer<TContext>(new DropCreateDatabaseAlways<TContext>());
            }
            else
            {
                Database.SetInitializer<TContext>(productionInitializer);//new NullDatabaseInitializer<TContext>());
            }
        }
    }
}

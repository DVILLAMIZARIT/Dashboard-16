using System;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace DAL
{
    public class SchemaSynchonizer<TContext>
        where TContext : DbContext
    {
        private readonly Func<Boolean> debugMode;

        public SchemaSynchonizer(Func<Boolean> debugMode)
        {
            Contract.Requires<ArgumentNullException>(debugMode != null);

            this.debugMode = debugMode;
        }

        public void Execute()
        {
            if (debugMode())
            {
                Database.SetInitializer<TContext>(new DropCreateDatabaseAlways<TContext>());
            }
            else
            {
                Database.SetInitializer<TContext>(new NullDatabaseInitializer<TContext>());
            }
        }
    }
}

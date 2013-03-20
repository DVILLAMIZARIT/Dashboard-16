using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Reflection;
using DAL.Interfaces;
using Infra.Model;

namespace DAL
{
    public class DataContext : DbContext, IDataContext
    {
        private readonly IDictionary<MethodInfo, Object> configurations;

        public DbSet<User> Users { get; set; }

        #region Ctor
        
        public DataContext()
            : base("name=DataContext")
        {
        }
        public DataContext(String nameOrConnectionString, IDictionary<MethodInfo, Object> configurations)
            : base(nameOrConnectionString)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(nameOrConnectionString));
            Contract.Requires<ArgumentNullException>(configurations != null);

            this.configurations = configurations;
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Contract.Requires<ArgumentNullException>(modelBuilder != null);

            foreach (var config in configurations)
            {
                config.Key.Invoke(modelBuilder.Configurations, new[] { config.Value });
            }

            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<T> CreateSet<T>()
            where T : class
        {
            return this.Set<T>();
        }

        public static void Initialize()
        {
            Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, DAL.Migrations.Configuration>());
        }

        public virtual void MarkAsModified<T>(T entity)
            where T : class
        {
            this.Entry<T>(entity).State = System.Data.EntityState.Modified;
        }
    }
}

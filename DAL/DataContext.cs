﻿using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;
using System.Linq;
using Core.Extensions;
using DAL.Interfaces;
using Infra.Model;

namespace DAL
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        #region Ctor
        
        public DataContext()
            : base("DefaultConnection")
        {
        }
        public DataContext(String nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        //public DataContext(String nameOrConnectionString, IDictionary<MethodInfo, Object> configurations)
        //    : base(nameOrConnectionString)
        //{
        //    Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(nameOrConnectionString));
        //    Contract.Requires<ArgumentNullException>(configurations != null);

        //    this.configurations = configurations;
        //}

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Contract.Assume(modelBuilder != null);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name != "EntityFramework");
            foreach (var assembly in assemblies)
            {
                var configTypes = assembly.GetTypes()
                    .Where(t => t.BaseType != null && !t.IsAbstract && t.IsDerivedFromOpenGenericType(typeof(EntityTypeConfiguration<>)));
                foreach (var type in configTypes)
                {
                    // I use internal classes for configuration, so the second parameter of CreateInstance should be
                    // passed `true` to allow it to be instantiated.
                    dynamic configuration = Activator.CreateInstance(type, true /* !type.GetConstructors().Any(c => c.IsPublic) */);
                    modelBuilder.Configurations.Add(configuration);
                }
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

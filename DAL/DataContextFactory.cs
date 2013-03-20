using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace DAL
{
    public class DataContextFactory<TContext> : IDisposable
        where TContext : DataContext
    {
        #region Static members
        
        private static readonly Type complexType = typeof(ComplexTypeConfiguration<>);

        private static readonly Type entityType = typeof(EntityTypeConfiguration<>);

        private static readonly ConcurrentDictionary<String, IDictionary<MethodInfo, Object>> mappingConfigurations = new ConcurrentDictionary<String, IDictionary<MethodInfo, Object>>();

        #endregion

        private TContext context;

        private readonly String nameOrConnectionString;

        #region Ctor
        
        public DataContextFactory(String nameOrConnectionString)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(nameOrConnectionString));

            this.nameOrConnectionString = nameOrConnectionString;
        }

        #endregion

        private static IDictionary<MethodInfo, Object> BuildConfigurations()
        {
            var addMethods = typeof(ConfigurationRegistrar).GetMethods()
                .Where(m => m.Name.Equals("Add"))
                .ToList();

            var entityTypeMethod = addMethods.First(m =>
                m.GetParameters()
                 .First()
                 .ParameterType
                 .GetGenericTypeDefinition()
                 .IsAssignableFrom(entityType));

            var complexTypeMethod = addMethods.First(m =>
                m.GetParameters().First()
                 .ParameterType
                 .GetGenericTypeDefinition()
                 .IsAssignableFrom(complexType));

            var configurations = new Dictionary<MethodInfo, object>();

            var types = typeof(DataContextFactory<>).Assembly
                .GetExportedTypes()
                .Where(IsMappingType)
                .ToList();

            foreach (var type in types)
            {
                MethodInfo typedMethod;
                Type modelType;

                if (IsMatching(type, out modelType, t => entityType.IsAssignableFrom(t)))
                {
                    typedMethod = entityTypeMethod.MakeGenericMethod(modelType);
                }
                else if (IsMatching(type, out modelType, t => complexType.IsAssignableFrom(t)))
                {
                    typedMethod = complexTypeMethod.MakeGenericMethod(modelType);
                }
                else
                {
                    continue;
                }

                configurations.Add(typedMethod, Activator.CreateInstance(type));
            }

            return configurations;
        }

        private TContext CreateContext()
        {
            var configurations = mappingConfigurations.GetOrAdd(this.nameOrConnectionString, key => BuildConfigurations());
            var localContext = (TContext)Activator.CreateInstance(typeof(TContext), new Object[] { this.nameOrConnectionString, configurations });
            return localContext;
        }

        public virtual TContext GetContext()
        {
            return context ?? (context = this.CreateContext());
        }

        #region Static Helpers

        private static Boolean IsMappingType(Type matchingType)
        {
            if (!matchingType.IsClass || matchingType.IsAbstract)
            {
                return false;
            }
            Type temp;
            return IsMatching(matchingType, out temp, t => entityType.IsAssignableFrom(t) || complexType.IsAssignableFrom(t));
        }

        private static Boolean IsMatching(Type matchingType, out Type modelType, Predicate<Type> matcher)
        {
            modelType = null;
            while (matchingType != null)
            {
                if (matchingType.IsGenericType)
                {
                    var definitionType = matchingType.GetGenericTypeDefinition();
                    if (matcher(definitionType))
                    {
                        modelType = matchingType.GetGenericArguments().First();
                        return true;
                    }
                }
                matchingType = matchingType.BaseType;
            }
            return false;
        }

        #endregion

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

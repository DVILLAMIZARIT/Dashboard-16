using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Core.Extensions
{
    public static class TypeExtensions
    { // http://stackoverflow.com/a/6427201/298053
        public static bool IsDerivedFromOpenGenericType(this Type type, Type openGenericType)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(openGenericType != null);
            Contract.Requires<ArgumentException>(openGenericType.IsGenericTypeDefinition);

            return type.GetTypeHierarchy()
                       .Where(t => t.IsGenericType)
                       .Select(t => t.GetGenericTypeDefinition())
                       .Any(t => openGenericType.Equals(t));
        }

        public static IEnumerable<Type> GetTypeHierarchy(this Type type)
        {
            Contract.Requires(type != null);
            Type currentType = type;
            while (currentType != null)
            {
                yield return currentType;
                currentType = currentType.BaseType;
            }
        }
    }
}

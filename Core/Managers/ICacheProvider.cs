using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Managers
{
    public interface ICacheProvider
    {
        IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query);
        IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query, TimeSpan cacheDuraction);
        Boolean RemoveFromCache<T>(IQueryable<T> query);
    }
}

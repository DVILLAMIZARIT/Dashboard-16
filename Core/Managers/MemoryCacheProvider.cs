using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Core.Managers
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private static readonly Object lockObject = new Object();
        private static MemoryCacheProvider instance = new MemoryCacheProvider();

        public static MemoryCacheProvider Instance
        {
            get { return instance; }
            private set
            {
                lock (lockObject)
                {
                    instance = value;
                }
            }
        }

        private static ConcurrentDictionary<String, CacheItem> dictionary;

        static MemoryCacheProvider()
        {
            dictionary = new ConcurrentDictionary<String, CacheItem>();
        }

        private MemoryCacheProvider()
        {
        }

        public IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query)
        {
            String key = GetKey<T>(query);
            CacheItem cache = dictionary.GetOrAdd(key, keyToFind =>
            {
                return new CacheItem
                {
                    Content = query.ToList(),
                    DateAdded = DateTime.Now
                };
            });
            foreach (T item in ((List<T>)cache.Content))
            {
                yield return item;
            }
        }

        public IEnumerable<T> GetOrCreateCache<T>(IQueryable<T> query, TimeSpan cacheDuraction)
        {
            String key = GetKey<T>(query);
            CacheItem cache = dictionary.GetOrAdd(key, keyToFind =>
            {
                return new CacheItem
                {
                    Content = query.ToList(),
                    DateAdded = DateTime.Now
                };
            });
            if (DateTime.Now.Subtract(cache.DateAdded) > cacheDuraction)
            {
                cache = dictionary.AddOrUpdate(key, new CacheItem
                {
                    Content = cache.Content,
                    DateAdded = DateTime.Now
                }, (keyToFind, oldItem) =>
                {
                    return new CacheItem
                    {
                        Content = query.ToList(),
                        DateAdded = DateTime.Now
                    };
                });
            }
            foreach (T item in ((List<T>)cache.Content))
            {
                yield return item;
            }
        }

        public Boolean RemoveFromCache<T>(IQueryable<T> query)
        {
            String key = GetKey<T>(query);
            CacheItem cache = null;
            return dictionary.TryRemove(key, out cache);
        }

        private static String GetKey<T>(IQueryable<T> query)
        {
            return String.Join(Environment.NewLine, query.ToString(), typeof(T).AssemblyQualifiedName);
        }
    }
}

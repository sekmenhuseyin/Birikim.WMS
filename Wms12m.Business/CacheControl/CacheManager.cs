using System;
using System.Linq;
using System.Runtime.Caching;

namespace Wms12m.Business
{
    public sealed class CacheManager
    {
        private static object lockObject = new object();
        private static CacheManager cacheManager = null;
        private static ObjectCache dataCache = null;

        private CacheManager()
        {
        }

        public static CacheManager Current
        {
            get
            {
                if (cacheManager != null)
                {
                    return cacheManager;
                }
                else
                {
                    return Initialize();
                }
            }
        }

        private static CacheManager Initialize()
        {

            lock (lockObject)
            {
                cacheManager = new CacheManager();
                dataCache = MemoryCache.Default;
            }

            return cacheManager;
        }

        public void Add(string key, object value)
        {
            dataCache.Add(key, value, null);
        }

        public void Add(string key, object value, TimeSpan timeout)
        {
            dataCache.Add(key, value, new CacheItemPolicy() { SlidingExpiration = timeout });
        }

        public void Remove(string key)
        {
            var keys = dataCache.Where(c => c.Key.Contains(key)).Select(c => c.Key);
            foreach (var k in keys)
            {
                dataCache.Remove(k);
            }
        }

        public object Get(string key)
        {
            return dataCache.Get(key);
        }
    }
}

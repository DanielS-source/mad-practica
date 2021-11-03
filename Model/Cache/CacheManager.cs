using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.PracticaMaD.Model.Cache
{
    public static class CacheManager
    {
        static CacheManager()
        {
            cache = MemoryCache.Default;
            cacheTags = new ConcurrentDictionary<string, IList<string>>();
        }

        #region Properties

        private static MemoryCache cache;
        private static ConcurrentDictionary<string, IList<string>> cacheTags;

        #endregion Properties

        public static void Set(string cacheKey, object value, params string[] tags)
        {
            cache.Add(cacheKey, value, new CacheItemPolicy());

            foreach (string tag in tags)
            {
                cacheTags.AddOrUpdate(tag, x => new List<string> { cacheKey }, (x, list) =>
                {
                    lock (list)
                    {
                        if (!list.Contains(cacheKey))
                        {
                            list.Add(cacheKey);
                        }
                    }

                    return list;
                });
            }
        }

        public static bool Exists(string cacheKey)
        {
            return cache.Contains(cacheKey);
        }

        public static E Get<E>(string cacheKey)
        {
            return (E)cache.Get(cacheKey);
        }

        public static void Remove(string cacheKey)
        {
            cache.Remove(cacheKey);
        }

        public static void Dispose()
        {
            cache.Dispose();
            cacheTags.Clear();
        }

        public static void ExpireTag(params string[] tags)
        {
            foreach (string tag in tags)
            {
                if (cacheTags.TryRemove(tag, out IList<string> list))
                {
                    lock (list)
                    {
                        foreach (string item in list)
                        {
                            cache.Remove(item);
                        }
                    }
                }
            }
        }
    }
}

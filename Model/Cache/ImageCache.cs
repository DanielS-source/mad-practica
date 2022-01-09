using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.PracticaMaD.Model.Cache
{
    public static class ImageCache
    {

        static ImageCache()
        {
            cache = new MemoryCache("ImageCache");
        }

        #region Properties

        private static MemoryCache cache;
        private static List<string> keys = new List<string>();
        private static readonly int MAX_SIZE = 5;

        #endregion Properties

        public static void Add(string cacheKey, object value)
        {
            cache.Add(cacheKey, value, new CacheItemPolicy()); 
            keys.Add(cacheKey); 
            if (keys.Count > MAX_SIZE)
            {
                //Removes last item if cache size exceeds MAX_SIZE
                var key = keys[0];
                cache.Remove(key);
                keys.Remove(key);
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
            keys.Remove(cacheKey);
        }
        public static void Dispose()
        {
            cache.Dispose();
            keys.Clear();
        }

    }

}

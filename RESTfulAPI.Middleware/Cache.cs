using System;
using Microsoft.Extensions.Caching.Memory;

namespace RESTfulAPI.Middleware
{
    public class Cache
    {
        public static object GetCache(string key, object cacheDate)
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            if (cache.TryGetValue(key, out object result)) return result;
            var cacheTime = DateTimeOffset.Now.AddHours(1);
            cache.Set(key, cacheDate, cacheTime);
            return result;
        }

        public static object SetCache(string key, object cacheDate)
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            cache.Set(key, cacheDate);
            object result = GetCache(key);
            return result;
        }

        public static object GetCache(string key)
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            object result = cache.Get(key);
            return result;
        }
    }
}
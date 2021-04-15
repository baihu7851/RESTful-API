using Microsoft.Extensions.Caching.Memory;

namespace RESTfulAPI.Middleware
{
    public class Cache
    {
        private static readonly MemoryCache MemoryCache = new(new MemoryCacheOptions());

        public static void RemoveCache(string key)
        {
            MemoryCache.Remove(key);
        }

        public static void SetCache(string key, object cacheDate)
        {
            MemoryCache.Set(key, cacheDate);
        }

        public static object GetCache(string key)
        {
            object result = MemoryCache.Get(key);
            return result;
        }
    }
}
using System;
using Microsoft.Extensions.Caching.Memory;

namespace RESTfulAPI.Middleware
{
    public class Cache
    {
        public static MemoryCache MyCache = new(new MemoryCacheOptions());
    }
}
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreDemo.Framework.Infrastructures.Cache
{
    public class MemoryCacheObjectManager : IMemoryCacheObjectManager
    {
        private readonly IMemoryCache _cache;
        private const string CacheTestKey = nameof(CacheTestKey);
        public MemoryCacheObjectManager(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException();
        }
        
        public string CacheTest
        {
            get => _cache.Get<string>(CacheTestKey);
            set => _cache.Set(CacheTestKey, value,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        }
    }
}

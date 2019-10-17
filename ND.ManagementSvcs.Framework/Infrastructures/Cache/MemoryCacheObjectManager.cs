using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ND.ManagementSvcs.Framework.Infrastructures.Cache
{
    public class MemoryCacheObjectManager : IMemoryCacheObjectManager
    {
        private readonly IMemoryCache _cache;

        #region CacheKey
        private const string CacheTestKey = nameof(CacheTestKey);
        #endregion
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

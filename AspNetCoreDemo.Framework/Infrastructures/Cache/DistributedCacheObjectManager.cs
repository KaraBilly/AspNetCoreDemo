using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace ND.ManagementSvcs.Framework.Infrastructures.Cache
{
    public class DistributedCacheObjectManager
    {
        private readonly IDistributedCache _distributedCache;

        #region Distributed Keys
        private const string TestKey = nameof(TestKey);
        #endregion

        public DistributedCacheObjectManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetKey<T>(string key) where T : struct
        {
            var keyBytes = await _distributedCache.GetAsync(key);
            return (T) Convert.ChangeType(keyBytes, typeof(T));
        }

        public async Task SetKey<T>(string key, T value, DistributedCacheEntryOptions options = null) where T : struct
        {
            await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(value.ToString()), options);
        }
    }
}

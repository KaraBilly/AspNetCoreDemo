using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Framework.Infrastructures.Cache;
using AspNetCoreDemo.Framework.Repositories.Interfaces;

namespace AspNetCoreDemo.Framework.Repositories
{
    public class ValueRepositories : IValuesRepositories
    {
        private readonly IMemoryCacheObjectManager _memoryCacheObjectManager;
        public ValueRepositories(IMemoryCacheObjectManager memoryCacheObjectManager)
        {
            _memoryCacheObjectManager = memoryCacheObjectManager ?? throw new ArgumentNullException();
        }
        public string GetValues(int value)
        {
            var cache = _memoryCacheObjectManager.CacheTest;
            if (cache == null)
            {
                _memoryCacheObjectManager.CacheTest = value.ToString();
                cache = _memoryCacheObjectManager.CacheTest;
            }

            return cache;
        }
    }
}

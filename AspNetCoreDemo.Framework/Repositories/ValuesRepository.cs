using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Framework.Infrastructures.Cache;
using AspNetCoreDemo.Framework.Repositories.Entities;
using AspNetCoreDemo.Framework.Repositories.Interfaces;

namespace AspNetCoreDemo.Framework.Repositories
{
    public class ValueRepository : IValuesRepository
    {
        private readonly IMemoryCacheObjectManager _memoryCacheObjectManager;
        public ValueRepository(IMemoryCacheObjectManager memoryCacheObjectManager)
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

        public Detail GetDetail()
        {
            return new Detail
            {
                DetailIntTest = 1,
                DetailStrTest = "2"
            };
        }
    }
}

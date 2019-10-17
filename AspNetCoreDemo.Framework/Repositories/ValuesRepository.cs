using System;
using System.Collections.Generic;
using System.Text;
using ND.ManagementSvcs.Framework.Infrastructures.Cache;
using ND.ManagementSvcs.Framework.Repositories.Entities;
using ND.ManagementSvcs.Framework.Repositories.Interfaces;

namespace ND.ManagementSvcs.Framework.Repositories
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheInfra.Implement;
using KDSCache.Enums;
using KDSCache.Interfaces;

namespace KDSCache.Implement
{
    public class KDSCacheManager : SimpleCacheManager<CachedItems>, IKDSCacheManager
    {
    }
}

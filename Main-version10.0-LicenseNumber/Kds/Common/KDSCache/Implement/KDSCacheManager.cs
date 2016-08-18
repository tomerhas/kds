using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheInfra.Implement;
using KDSCommon.Interfaces;
using KDSCommon.Enums;

namespace KDSCache.Implement
{
    public class KDSCacheManager : SimpleCacheManager<CachedItems>, IKDSCacheManager
    {
    }
}

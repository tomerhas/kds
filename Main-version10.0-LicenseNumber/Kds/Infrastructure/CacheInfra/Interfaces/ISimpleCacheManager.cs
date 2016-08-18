using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheInfra.Interfaces
{
    public interface ISimpleCacheManager<TKey>
    {
        object GetCacheItemObject(TKey key);
        TItemType GetCacheItem<TItemType>(TKey key) where TItemType : class;
        void AddItem(TKey key, object item);

    }
}

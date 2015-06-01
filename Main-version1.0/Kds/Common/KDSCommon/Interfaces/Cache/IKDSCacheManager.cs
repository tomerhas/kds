using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;

namespace KDSCommon.Interfaces
{
    public interface IKDSCacheManager
    {
        object GetCacheItemObject(CachedItems key);
        TItemType GetCacheItem<TItemType>(CachedItems key) where TItemType : class;
        void AddItem(CachedItems key, object item);

    }
}

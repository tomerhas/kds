using System;
using CacheInfra.Implement.AgedQueue;

namespace CacheInfra.Interfaces
{
    interface IAgedQueueList<TKey, TValue>
     where TValue : class
    {
        void Init(int maxItems);
        void Add(TValue item, TKey key);
        TValue GetItem(TKey key);
    }
}

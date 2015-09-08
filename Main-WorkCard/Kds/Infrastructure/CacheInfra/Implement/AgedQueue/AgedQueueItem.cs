using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheInfra.Implement.AgedQueue
{
    internal class AgedQueue<TKey,TValue>
    {
        public DateTime LastUpdated { get; set; }
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using CacheInfra.Implement.AgedQueue;
using KDSCache.Interfaces;

namespace KDSCache.Implement
{
    public class KDSAgedQueueParameters : AgedQueueList<DateTime, clParameters>, IKDSAgedQueueParameters
    {
    }
}

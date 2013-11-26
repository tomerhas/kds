using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheInfra.Implement.AgedQueue;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;

namespace KDSCache.Implement
{
    public class KDSAgedQueueParameters : AgedQueueList<DateTime, clParametersDM>, IKDSAgedQueueParameters
    {
    }
}

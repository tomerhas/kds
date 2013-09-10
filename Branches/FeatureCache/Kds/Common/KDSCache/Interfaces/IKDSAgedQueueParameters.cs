using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;

namespace KDSCache.Interfaces
{
    public interface IKDSAgedQueueParameters
    {
        void Init(int maxItems);
        void Add(clParameters item, DateTime key);
        clParameters GetItem(DateTime key);
    }
}

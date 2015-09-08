using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;

namespace KDSCommon.Interfaces
{
    public interface IKDSAgedQueueParameters
    {
        void Init(int maxItems);
        void Add(clParametersDM item, DateTime key);
        clParametersDM GetItem(DateTime key);
    }
}

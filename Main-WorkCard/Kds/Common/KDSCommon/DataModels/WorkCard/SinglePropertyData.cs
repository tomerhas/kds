using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class SinglePropertyData<T>
    {
        public bool IsEnabled { get; set; }
        public T Value { get; set; }

        public SinglePropertyData()
        {
            IsEnabled = true;
        }
    }
}

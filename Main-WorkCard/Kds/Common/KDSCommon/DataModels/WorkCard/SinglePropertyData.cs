using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    /// <summary>
    /// SinglePropertyData is a wrapper for a single value that besides the value itself contains additional metadata related for the UI
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

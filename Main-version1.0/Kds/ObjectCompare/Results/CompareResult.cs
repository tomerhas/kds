using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCompare.Results
{
    /// <summary>
    /// Result data from comparing a single field
    /// </summary>
    public class CompareResult
    {
        public CompareResult()
        {
            CompareType = CompareTypes.Dual;
        }

        public CompareResult(CompareTypes compareType, string propPath, object oldObj, object newObj)
        {
            CompareType = compareType;
            PropertyPath = propPath;
            OldValue= oldObj!=null ?oldObj.ToString(): "";
            NewValue = newObj!= null ? newObj.ToString() : "";
        }

        public CompareTypes CompareType { get; set; }
        public string PropertyPath { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Header { get; set; }
        public string PropertyDisplayName 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Header))
                    return Header;
                return PropertyPath;
            }
        }
    }

    public enum CompareTypes
    { 
        Self,
        Dual
    }
}

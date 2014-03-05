using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCompare.Metadata
{
    /// <summary>
    /// Metadata extracted from a single field
    /// </summary>
    public class PropertyMetaData
    {
        public string PropertyName { get; set; }
        public string Header { get; set; }
        public Type PropertyType { get; set; }
        public bool IsPrimitive { get; set; }
        public bool CompareToString { get; set; }
    }
}

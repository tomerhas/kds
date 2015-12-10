using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    /// <summary>
    /// The selectItem is wrapper for a single item in the listbox
    /// </summary>
    public class SelectItem
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }
}

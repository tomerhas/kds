using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectCompare.Helper;

namespace ObjectCompare
{
    public class ModificationRecorder<TItem>  where TItem :class, new()
    {
        public ModificationRecorder(TItem item)
        {
            ContainedItem = item;
            _origItem =  CloneHelper.Clone(item);
        }
        public bool HasChanged()
        {
            ObjectComparer objComp = new ObjectComparer();
            var result =  objComp.Compare(_origItem, ContainedItem);
            if (result != null && result.CompareResultsList.Count > 0)
                return true;
            return false;
        }

        public TItem ContainedItem { get; set; }
        private TItem _origItem;

    }
}

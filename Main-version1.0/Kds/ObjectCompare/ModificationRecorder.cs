using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectCompare.Helper;

namespace ObjectCompare
{
    public class ModificationRecorder<TItem>  where TItem :class, new()
    {

        public ModificationRecorder(TItem item, bool startRecord) :this(item)
        {
            if (startRecord)
                StartRecord();
        }
        public ModificationRecorder(TItem item)
        {
            ContainedItem = item;
        }

        public void StartRecord()
        {
            _origItem = CloneHelper.Clone(ContainedItem);
        }
        public bool HasChanged()
        {
            ObjectComparer objComp = new ObjectComparer();
            var result =  objComp.Compare(_origItem, ContainedItem);
            if (result != null && result.CompareResultsList.Count > 0)
                return true;
            return false;
        }

        public bool HasChanged(Func<TItem, bool> predicate)
        {
            ObjectComparer objComp = new ObjectComparer();
            if (predicate(ContainedItem))
            {
                var result = objComp.Compare(_origItem, ContainedItem);
                if (result != null && result.CompareResultsList.Count > 0)
                    return true;
                return false;
            }
            return false; 
        }

        public TItem ContainedItem { get; set; }
        private TItem _origItem;

    }
}

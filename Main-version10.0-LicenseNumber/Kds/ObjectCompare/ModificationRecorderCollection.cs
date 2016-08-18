using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCompare
{
    public class ModificationRecorderCollection<T> : List<ModificationRecorder<T>> where T : class, new()
    {
    }
}

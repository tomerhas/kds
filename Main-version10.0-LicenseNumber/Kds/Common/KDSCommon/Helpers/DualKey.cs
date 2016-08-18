using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Helpers
{
    public class DualKey<Key1,Key2> 
    {
        public Key1 FirstKey { get; set; }
        public Key2 SecondKey { get; set; }

        public DualKey(Key1 key1, Key2 key2)
        {
            FirstKey = key1;
            SecondKey = key2;
        }
    }
}

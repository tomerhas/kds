using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.CalcParallel
{
    public class Meafyen
    {
        public bool IsExist { get; set; }
        public string Value { get; set; }
        public int IntValue
        {
            get
            {
                int val = -1;
                Int32.TryParse(Value, out val);
                return val;
            }
        }


        public Meafyen(bool isExist, string Val)
        {
            Value = Val;
            IsExist = isExist;
        }
    }
}

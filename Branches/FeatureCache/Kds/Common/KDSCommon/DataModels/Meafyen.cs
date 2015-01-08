using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels
{
    public class Meafyen
    {
        public int Kod { get; set; }
        public bool IsExist { get; set; }
        public string Value { get; set; }
        public string ErechIshi { get; set; }

        
        public int IntValue
        {
            get
            {
                int val = -1;
                Int32.TryParse(Value, out val);
                return val;
            }
        }

        public float FloatValue
        {
            get
            {
                float val = -1;
                float.TryParse(Value, out val);
                return val;
            }
        }
        public Meafyen(int kod)
        {
            Kod = kod;
        }
        public Meafyen(bool isExist, string Val, string erech_ishi)
        {
            Value = Val;
            IsExist = isExist;
            ErechIshi = erech_ishi == null ? "" : erech_ishi.Trim();  
        }
    }
}

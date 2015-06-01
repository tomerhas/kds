﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDSCommon.DataModels.BankShaot
{
    public class Param
    {
        public int Kod { get; set; }
        public bool IsExist { get; set; }
        public string Value { get; set; }
       // public string ErechIshi { get; set; }

        
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
        public Param(int kod)
        {
            Kod = kod;
        }
        public Param( string Val)
        {
           // Kod = kod;
            Value = Val;
        }
        public Param(bool isExist, string Val ) //, string erech_ishi)
        {
            Value = Val;
            IsExist = isExist;
          //  ErechIshi = erech_ishi == null ? "" : erech_ishi.Trim();  
        }
    }
}

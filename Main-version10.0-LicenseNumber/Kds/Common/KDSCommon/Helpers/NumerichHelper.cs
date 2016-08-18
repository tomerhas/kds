using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Helpers
{
    public class NumerichHelper
    {
        public static string Append0ToNumber(string val)
        {

            if (IsNumber(val))
                val = string.Format("{0:00}", int.Parse(val));
            return val;
        }

        public static bool IsNumber(string val)
        {
            int temp = 0;
            return int.TryParse(val, out temp);
        }

        public static bool IsLongNumber(string val)
        {
            long temp = 0;
            return long.TryParse(val, out temp);
        }
    }
}

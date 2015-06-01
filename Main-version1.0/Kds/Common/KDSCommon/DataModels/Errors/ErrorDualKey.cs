using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.Helpers;

namespace KDSCommon.DataModels.Errors
{
    public class ErrorDualKey : DualKey<ErrorTypes, ErrorSubLevel>, IEquatable<ErrorDualKey>
    {
        public ErrorDualKey(ErrorTypes errorType, ErrorSubLevel errorSubType)
            : base(errorType, errorSubType)
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstKey.ToString(), SecondKey.ToString());
        }

        public override int GetHashCode()
        {
            return this.FirstKey.GetHashCode() ^ this.SecondKey.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((ErrorDualKey)obj);
        }

        public bool Equals(ErrorDualKey other)
        {
            if (this.FirstKey == other.FirstKey && this.SecondKey == other.SecondKey)
                return true;
            return false;
        }
    }
}

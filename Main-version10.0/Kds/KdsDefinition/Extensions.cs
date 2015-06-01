using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.UI.SystemManager;

namespace KdsDefinition
{
    internal static class Extensions
    {
        internal static KdsListValue Clone(this KdsListValue source)
        {
            string serial = KdsLibrary.Utils.KdsExtensions.SerializeObject(source);
            return (KdsListValue)
                KdsLibrary.Utils.KdsExtensions.DeserializeObject(typeof(KdsListValue), serial);
        }

        internal static string[] DataTypes
        {
            get { return new string[] { "System.Int16", "System.Int32", "System.Int64", "System.DateTime", "System.Decimal", "System.String" }; }
        }
    }
}

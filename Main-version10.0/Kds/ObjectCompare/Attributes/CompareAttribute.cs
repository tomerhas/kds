using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCompare.Attributes
{
    /// <summary>
    /// Attribute to be placed over fields that are required to be compared
    /// </summary>
    public class CompareAttribute : Attribute
    {
        public CompareAttribute()
        {
            //Set default true
            CompareToString = false;
        }

        public CompareAttribute(string header)
        {
            //Set default true
            CompareToString = false;
            Header = header;
        }

        public bool CompareToString { get; set; }
        public string Header { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class AttributeField
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public AttributeField()
        {

        }

        public AttributeField(string nameAttr, string valAttr)
        {
            Name = nameAttr;
            Value = valAttr;
        }
    }
}

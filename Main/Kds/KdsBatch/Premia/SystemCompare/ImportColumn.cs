using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KdsBatch.SystemCompare
{
    /// <summary>
    /// Represents a column from table for import
    /// </summary>
    public class ImportColumn
    {
        [XmlAttribute("Name")]
        public string ColumnName { get; set; }
        [XmlAttribute("Type")]
        public string Type { get; set; }
        public Type ColumnType
        {
            get
            {
                if (String.IsNullOrEmpty(Type)) return typeof(String);
                switch (Type.ToLower())
                {
                    case "number": return typeof(Double);
                    default: return typeof(String);
                }
            }
        }
    }

    /// <summary>
    /// Represents defintion of fixed length column from text file for import
    /// </summary>
    public class FixedColumn : ImportColumn
    {
        [XmlAttribute("From")]
        public int DeclarationFrom { get; set; }
        [XmlAttribute("To")]
        public int DeclarationTo { get; set; }
        [XmlIgnore]
        public int From
        {
            get { return DeclarationFrom - 1; }
        }
        [XmlIgnore]
        public int To
        {
            get { return DeclarationTo - 1; }
        }

       
    }
    
}

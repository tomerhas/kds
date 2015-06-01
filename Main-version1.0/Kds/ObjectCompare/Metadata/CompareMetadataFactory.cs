using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ObjectCompare.Attributes;

namespace ObjectCompare.Metadata
{
    /// <summary>
    /// Class for creating all the metadata required from a type.
    /// Fills in a list of PropertyMetaData taken from the class type    /// 
    /// </summary>
    public static class CompareMetadataFactory
    {
        public static List<PropertyMetaData> Create<T>()
        {
            Type type = typeof(T);
            return Create(type);
        }

        public static List<PropertyMetaData> Create(Type type)
        {
            List<PropertyMetaData> attList = new List<PropertyMetaData>();
            var flags = BindingFlags.Instance | BindingFlags.Public ;
            //var properties = type.GetProperties().Where(prop => prop.(typeof(CompareAttribute), false));
            var properties = type.GetProperties(flags).Where(prop => !prop.IsDefined(typeof(DontCompareAttribute), false)); //type.GetProperties(flags).Select(prop => prop);
            foreach (var propInfo in properties)
            {
                PropertyMetaData propMetadata = new PropertyMetaData();
                propMetadata.PropertyName = propInfo.Name;
                //var att = propInfo.GetCustomAttributes(typeof(CompareAttribute), false)[0] as CompareAttribute;
                
                //if (propInfo.PropertyType == typeof(string) || propInfo.PropertyType == typeof(DateTime))
                //    propMetadata.IsPrimitive = true;
                //else
                //    propMetadata.IsPrimitive = propInfo.PropertyType.IsPrimitive;
                propMetadata.PropertyType = propInfo.PropertyType;
                //propMetadata.CompareToString = att.CompareToString;
                //propMetadata.Header = att.Header;
                attList.Add(propMetadata);

            }
            return attList;
        }

    }
}

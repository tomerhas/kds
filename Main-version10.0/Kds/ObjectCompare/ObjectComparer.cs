using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;
using ObjectCompare.Metadata;
using ObjectCompare.Results;

namespace ObjectCompare
{
    /// <summary>
    /// The logic class for comparing between two objects of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectComparer 
    {

        public ObjectComparer()
        {
        }

        public CompareResultContainer Compare<T>(T oldVal, T newVal) where T: class, new()
        {
            CompareResultContainer diffCollection = new CompareResultContainer();
            RecursiveComparer(oldVal, newVal, typeof(T), diffCollection,"");
            return diffCollection;
            
        }

        public CompareResultContainer Compare(Type objType, object oldVal, object newVal)
        {
            CompareResultContainer diffCollection = new CompareResultContainer();
            RecursiveComparer(oldVal, newVal, objType, diffCollection, "");
            return diffCollection;
        }

        public CompareResultContainer SelfCompare<T>(T newVal) where T: class, new()
        {
            CompareResultContainer diffCollection = new CompareResultContainer();
            RecursiveSelfCompare( newVal, typeof(T), diffCollection, "");
            return diffCollection;
        }

        public CompareResultContainer SelfCompare(Type objType, object newVal)
        {
            CompareResultContainer diffCollection = new CompareResultContainer();
            RecursiveSelfCompare(newVal, objType, diffCollection, "");
            return diffCollection;
        }

        private void RecursiveSelfCompare(object newVal, Type type, CompareResultContainer diffCollection, string parentProperty)
        { 
            var accessor = TypeAccessor.Create(type);
            var attList = CompareMetadataFactory.Create(type);
            foreach (var att in attList)
            {
                object newAccessor = accessor[newVal, att.PropertyName];
                //If the new obj property or old obj property are null - do nothing - currently not supporting new items 
                if (newAccessor != null)
                {
                    if (att.IsPrimitive == true || att.CompareToString == true)
                    {
                        string propName = string.Empty;
                        if (!string.IsNullOrEmpty(parentProperty))
                        {
                            propName = parentProperty + "." + att.PropertyName;
                        }
                        else
                        {
                            propName = att.PropertyName;
                        }
                        diffCollection.Add(new CompareResult() {CompareType=CompareTypes.Self, PropertyPath = propName,  
                            Header=att.Header, OldValue = "", NewValue = newAccessor.ToString() });

                    }
                    else
                    {
                        string pathName = string.Empty;
                        if (!string.IsNullOrEmpty(parentProperty))
                        {
                            pathName = parentProperty + "." + att.PropertyName;
                        }
                        else
                        {
                            pathName = att.PropertyName;
                        }
                        RecursiveSelfCompare(newAccessor, att.PropertyType, diffCollection, pathName);
                    }
                }
            }
        }

        /// <summary>
        /// A function for recursivly comparing complex types
        /// </summary>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <param name="type"></param>
        /// <param name="diffCollection"></param>
        /// <param name="parentProperty"></param>
        private void RecursiveComparer(object oldVal, object newVal, Type type, CompareResultContainer diffCollection, string parentProperty)
        {
            var accessor = TypeAccessor.Create(type);
            var attList = CompareMetadataFactory.Create(type);
            foreach (var att in attList)
            {
                object oldAccessor = accessor[oldVal, att.PropertyName];
                object newAccessor = accessor[newVal, att.PropertyName];
                //If the new obj property or old obj property are null - do nothing - currently not supporting new items 
                if (newAccessor != null || oldAccessor!=null)
                {
                    if ((oldAccessor == null && newAccessor != null) ||
                            (oldAccessor != null && newAccessor == null) ||
                            oldAccessor.ToString() != newAccessor.ToString())
                    {
                        diffCollection.Add(new CompareResult(CompareTypes.Dual, att.PropertyName, oldAccessor, newAccessor)); 
                    }
                    //if (att.IsPrimitive == true || att.CompareToString == true)
                    //{
                    //    if ((oldAccessor==null && newAccessor!=null) ||
                    //        (oldAccessor != null && newAccessor == null) ||
                    //        oldAccessor.ToString() != newAccessor.ToString())
                    //    {
                    //        string propName = string.Empty;
                    //        if (!string.IsNullOrEmpty(parentProperty))
                    //        {
                    //            propName = parentProperty + "." + att.PropertyName;
                    //        }
                    //        else
                    //        {
                    //            propName = att.PropertyName;
                    //        }
                    //        diffCollection.Add(new CompareResult(CompareTypes.Dual, propName, oldAccessor, newAccessor) { Header = att.Header });
                    //    }
                    //}
                    //else
                    //{
                    //    string pathName = string.Empty;
                    //    if (!string.IsNullOrEmpty(parentProperty))
                    //    {
                    //        pathName = parentProperty + "." + att.PropertyName;
                    //    }
                    //    else
                    //    {
                    //        pathName = att.PropertyName;
                    //    }
                    //    RecursiveComparer(oldAccessor, newAccessor, att.PropertyType, diffCollection, pathName);
                    //}
                }
            }
            
        }
    }
}

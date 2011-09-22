using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Defines Attribute for adding default values to controls located in
    /// insert template of KdsFormTemplate
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class KdsDefaultValueAttribute : Attribute
    {
        #region Fields
        private string _fieldName;
        private string _controlName;
        private string _propertyName; 
        #endregion

        #region Constractors
        public KdsDefaultValueAttribute(string field, string control)
            : this(field, control, "Text")
        {
        }

        public KdsDefaultValueAttribute(string field, string control,
            string property)
        {
            _fieldName = field;
            _controlName = control;
            _propertyName = property;
        } 
        #endregion

        #region Properties
        public string ControlName
        {
            get { return _controlName; }
            set { _controlName = value; }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        } 
        #endregion

        #region Methods
        public static void AddDefaultValuesFromAttributes(
            KdsSysManPageBase container)
        {
            Type type = container.GetType();
            KdsDefaultValueAttribute[] attrArray =
                (KdsDefaultValueAttribute[])type.GetCustomAttributes(typeof(KdsDefaultValueAttribute),
                    true);
            foreach (var attr in attrArray)
            {
                container.AddDefaultValue(attr.FieldName, attr.ControlName,
                    attr.PropertyName);
            }
        }
        #endregion
    }
    
    [Serializable]
    public class KdsDefaultValueHolder
    {
        #region Fields
        private string _fieldName;
        private string _controlName;
        private string _propertyName;
        #endregion
        #region Constractors
        public KdsDefaultValueHolder(string field, string control)
            : this(field, control, "Text")
        {
        }

        public KdsDefaultValueHolder(string field, string control,
            string property)
        {
            _fieldName = field;
            _controlName = control;
            _propertyName = property;
        } 
        #endregion
        #region Properties
        public string ControlName
        {
            get { return _controlName; }
            set { _controlName = value; }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }
        #endregion
    }

}

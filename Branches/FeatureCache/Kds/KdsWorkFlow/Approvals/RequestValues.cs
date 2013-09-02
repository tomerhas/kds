using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Represents a collection of Request Values as a part
    /// of Approval Request Key
    /// </summary>
    public class RequestValues
    {
        #region Fields
        public const string FIRST_REQUEST_VALUE_KEY = "erech_mevukash";
        public const string SECOND_REQUEST_VALUE_KEY = "erech_mevukash2";
        private Dictionary<string, RequestValue> _values = new Dictionary<string, RequestValue>(); 
        #endregion

        #region Methods
        public void SetValue(string key)
        {
            SetValue(key, RequestValue.DEFAULT_ADDITIONAL_FIELD_VALUE);
        }

        public void SetValue(string key, object value)
        {
            if (_values == null) _values = new Dictionary<string, RequestValue>();
            if (_values.ContainsKey(key))
                _values[key] = new RequestValue(key, value);
            else _values.Add(key, new RequestValue(key, value));
        }

        public bool IsEqual(RequestValues requestValues)
        {
            var enm = _values.GetEnumerator();
            bool equals = true;
            while (enm.MoveNext())
            {
                equals = enm.Current.Value.IsEqual(requestValues[enm.Current.Key]);
                if (!equals) break;
            }
            return equals;
        }

        public static RequestValues WithFirstRequestValues(object value)
        {
            var requestValue = RequestValues.Empty;
            requestValue.SetValue(FIRST_REQUEST_VALUE_KEY, value);
            return requestValue;
        }

        public static RequestValues WithBothRequestValues(object firstValue, object secondValue)
        {
            var requestValue = RequestValues.Empty;
            requestValue.SetValue(FIRST_REQUEST_VALUE_KEY, firstValue);
            requestValue.SetValue(SECOND_REQUEST_VALUE_KEY, secondValue);
            return requestValue;
        } 
        #endregion

        #region Properties
        public object this[string key]
        {
            get
            {
                if (!_values.ContainsKey(key))
                    SetValue(key);
                return _values[key].Value;
            }
        }

        public static RequestValues Empty
        {
            get
            {
                var requestVals = new RequestValues();
                requestVals.SetValue(FIRST_REQUEST_VALUE_KEY);
                requestVals.SetValue(SECOND_REQUEST_VALUE_KEY);
                return requestVals;
            }
        } 
        #endregion
    }

    /// <summary>
    /// Represents a Request Value of Approval Request.
    /// Default Value for Primary Key purposes is -999
    /// </summary>
    public class RequestValue
    {
        #region Fields
        internal const int DEFAULT_ADDITIONAL_FIELD_VALUE = -999;
        private object _value; 
        #endregion

        #region Constractors
        public RequestValue(string key)
            : this(key, DEFAULT_ADDITIONAL_FIELD_VALUE)
        {

        }
        public RequestValue(string key, object value)
        {
            Key = key;
            Value = value;

        } 
        #endregion

        #region Methods
        internal bool IsEqual(object compareValue)
        {
            return _value.ToString().Equals(compareValue.ToString());
        } 
        #endregion

        #region Properties
        public string Key { get; set; }

        public object Value
        {
            get { return _value; }
            set
            {
                if (value == null || value == DBNull.Value)
                    _value = DEFAULT_ADDITIONAL_FIELD_VALUE;
                else _value = value;
            }
        } 
        #endregion
    }
    
}

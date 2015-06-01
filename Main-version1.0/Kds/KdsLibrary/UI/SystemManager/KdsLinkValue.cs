using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using KdsLibrary.Utils;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Defines arguments 
    /// for DropDownList in GridView or DetailsView
    /// </summary>
    public class KdsListValue
    {
        #region Fields
        private string _selectMethod;
        private string _textField;
        private string _valueField;
        private List<KdsSelectParameter> _selectParams;
        private List<KdsUnboundValue> _unboundValues;
        #endregion

        #region Constractors
        public KdsListValue()
        {
        }

        #endregion

        #region Properties
        [XmlAttribute("Select")]
        public string SelectMethod
        {
            get { return _selectMethod; }
            set { _selectMethod = value; }
        }
        [XmlAttribute("ValueField")]
        public string ValueField
        {
            get { return _valueField; }
            set { _valueField = value; }
        }
        [XmlAttribute("TextField")]
        public string TextField
        {
            get { return _textField; }
            set { _textField = value; }
        }
        [XmlElement("SelectParameter", typeof(KdsSelectParameter))]
        public List<KdsSelectParameter> SelectParametersList
        {
            get { return _selectParams; }
            set { _selectParams = value; }
        }
        [XmlIgnore]
        public MatchNameList<KdsSelectParameter> SelectParameters
        {
            get { return new MatchNameList<KdsSelectParameter>(_selectParams); }
        }

        [XmlElement("UnboundValue", typeof(KdsUnboundValue))]
        public List<KdsUnboundValue> UnboundValues
        {
            get { return _unboundValues; }
            set { _unboundValues = value; }
        }

        public bool HasUnboundValues
        {
            get { return _unboundValues != null && _unboundValues.Count > 0; }
        }
        #endregion

        
    }

   
    public class KdsUnboundValue
    {
        [XmlAttribute("Value")]
        public int Value { get; set; }
        [XmlAttribute("Text")]
        public string Text { get; set; }
    }

}

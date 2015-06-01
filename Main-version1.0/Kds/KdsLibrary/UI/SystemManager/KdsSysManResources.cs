using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using KdsLibrary.Utils;

namespace KdsLibrary.UI.SystemManager
{
    public class KdsSysManResources
    {
        private List<KdsTextResource> _textResources;
        
        [XmlElement("Text", typeof(KdsTextResource))]
        public List<KdsTextResource> TextResourcesList
        {
            get { return _textResources; }
            set { _textResources = value; }
        }
        [XmlIgnore]
        public MatchNameList<KdsTextResource> TextResources
        {
            get { return new MatchNameList<KdsTextResource>(_textResources); }
        }
    }

    public class KdsTextResource:IMatchName
    {
        #region Fields
        private string _key;
        private string _value; 
        #endregion

        #region Properties
		[XmlAttribute("Key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        [XmlAttribute("Value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
	    #endregion    
    
        #region IMatchName Members

        public string Name
        {
            get { return _key; }
        }

        #endregion
    }
}

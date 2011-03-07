using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Web;
using KdsLibrary.UI.SystemManager;
using KdsLibrary.DAL;

namespace KdsTaskManager
{

    [XmlRoot("ReferenceDefinitions")]
    public class ReferenceDefinitions
    {
        private static ReferenceDefinitions oReferenceDefinitions ;
        [XmlElement("Reference", typeof(Reference))]
        public List<Reference> ReferenceList { get; set; }

        public static ReferenceDefinitions GetInstance()
        {
            if (oReferenceDefinitions == null)
            {
                oReferenceDefinitions = new ReferenceDefinitions();
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(@"ReferenceDefinitions.xml");
                oReferenceDefinitions = (ReferenceDefinitions)Utilities.DeserializeObject(typeof(KdsTaskManager.ReferenceDefinitions), xmlDoc.OuterXml);
            }
            return oReferenceDefinitions;
        }
    }

    public class Reference
    {
        public Reference() { }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("FullPath")]
        public string FullPath { get; set; }

    }

}

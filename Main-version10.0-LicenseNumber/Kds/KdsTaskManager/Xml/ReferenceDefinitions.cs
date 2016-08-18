using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Web;
using KdsLibrary.UI.SystemManager;

namespace KdsTaskManager
{

    [XmlRoot("ReferenceDefinitions")]
    public class ReferenceDefinitions
    {
        private static ReferenceDefinitions oReferenceDefinitions ;
        private static object syncRoot = new object();
        private static bool InstanceWasCreated = false;
        [XmlElement("Reference", typeof(Reference))]
        public List<Reference> ReferenceList { get; set; }

        public static ReferenceDefinitions GetInstance()
        {
            if (!InstanceWasCreated)
            {
                lock (syncRoot)
                {
                    if (!InstanceWasCreated)
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.Load(@"ReferenceDefinitions.xml");
                        oReferenceDefinitions = new ReferenceDefinitions();
                        oReferenceDefinitions = (ReferenceDefinitions)Utilities.DeserializeObject(typeof(KdsTaskManager.ReferenceDefinitions), xmlDoc.OuterXml);
                        InstanceWasCreated = true;
                    }
                }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Web;
using System.Xml;
using KdsLibrary.Utils;

namespace KdsLibrary.UI.SystemManager
{
    [XmlRoot("SystemManager")]
    public class KdsSysMan
    {
        private List<KdsDataSource> _dataSources;
        private KdsSysManResources _resources;

        [XmlElement("Table",typeof(KdsDataSource))]
        public List<KdsDataSource> DataSourcesList
        {
            get { return _dataSources; }
            set { _dataSources = value; }
        }

        [XmlElement("Resources", typeof(KdsSysManResources))]
        public KdsSysManResources Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }
        
        [XmlIgnore]
        public MatchNameList<KdsDataSource> DataSources
        {
            get { return new MatchNameList<KdsDataSource>(_dataSources); }
        }

        
        public static KdsSysMan GetKdsSysMan()
        {
            KdsSysMan sysMan = HttpContext.Current.GetApplicationStoredValue("KdsSysMan") as
                KdsSysMan;
            if (sysMan == null)
            {
                lock (HttpContext.Current.Application)
                {
                    var xSysMan = new XmlDocument();
                    xSysMan.Load(HttpContext.Current.Server.MapPath("~/Xml/SysMan.xml"));
                    sysMan = (KdsSysMan)KdsLibrary.Utils.KdsExtensions.DeserializeObject(
                        typeof(KdsSysMan), xSysMan.OuterXml);
                    HttpContext.Current.AddApplicationStoredValue("KdsSysMan", sysMan);
                }
            }
            return sysMan;
        }

        public override string ToString()
        {
            return KdsExtensions.SerializeObject(this);
        }
    }
}

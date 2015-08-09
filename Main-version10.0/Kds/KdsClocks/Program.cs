using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace KdsClocks
{
    class Program
    {
        static void Main(string[] args)
        {

           syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
            var xmlE = wsSy.SQLRecordSetToXML("Select * From Emp");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlE);
         //   DataSet DS = ConvertXMLToDataSet(xmlE);
        }

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
    }
}

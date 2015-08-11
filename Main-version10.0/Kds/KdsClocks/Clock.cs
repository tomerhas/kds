using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

namespace KdsClocks
{
    public class Clock// : IClocks
    {
        public void InsertMovemetRecords()
        {
            try
            {
                syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
                var xmlE = wsSy.SQLRecordSetToXML(ConfigurationManager.AppSettings["MOVMENTSQL"]);
                DataSet DsMovement = ConvertXMLToDataSet(xmlE);
                // להכין udt
                //לשלוח לשמירה
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertMovemetErrRecords()
        {
            try
            {
                syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
                var xmlE = wsSy.SQLRecordSetToXML(ConfigurationManager.AppSettings["ERRMOVESQL"]);
                DataSet DsMovement = ConvertXMLToDataSet(xmlE);
                // להכין udt
                //לשלוח לשמירה

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

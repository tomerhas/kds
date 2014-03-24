using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KdsBatch.Reports
{
    public class clReportParam
    {
        private string _sName;
        private string _sValue;

        public string Name
        {
            get { return _sName; }
            //  set { _sName = value; }
        }

        public string Value
        {
            get { return _sValue; }
            //  set { _sValue = value; }
        }

        public clReportParam(string name, string value)
        {
            _sName = name;
            _sValue = value;
        }

        public clReportParam(string name, DataSet Ds)
        {
            _sName = name;
            _sValue = getDsStrind(Ds);
        }

        private string getDsStrind(DataSet Ds)
        {
            System.IO.MemoryStream strm = new System.IO.MemoryStream();

            string tmpDs = null;

            Ds.WriteXml(strm, XmlWriteMode.WriteSchema);

            tmpDs = "<?xml version=" + "\"" + "1.0" + "\"" + " standalone=" + "\"" + "yes" + "\"" + "?><NewDataSet>";

            string am = "<?xml version=" + "\"" + "1.0" + "\"" + " encoding=" + "\"" + "utf-16" + "\"" + "?>";

            tmpDs = tmpDs + Ds.GetXmlSchema().Replace(am, "");

            tmpDs = tmpDs + Ds.GetXml().Replace("<NewDataSet>", "");


            if (tmpDs.IndexOf("</NewDataSet>") == -1)
            {
                tmpDs = tmpDs + "</NewDataSet>";

            }

            return tmpDs;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using KdsLibrary.Utils.Reports;

namespace KdsBatch.Reports
{
    public class clReportOnLine : clReport
    {
        protected ReportModule _RptModule;

        public clReportOnLine()
        {

        }
        public clReportOnLine(String rptName, eFormat sFormat)
        {
            RdlName = rptName;
            Extension = sFormat;
            ReportParams = new List<clReportParam>();
         //   SetParams();
            SetVersionDetails();
        }

        private void SetVersionDetails()
        {
            KdsLibrary.BL.clReport rep = new KdsLibrary.BL.clReport();
            DataTable dt = rep.GetReportDetails(((ReportName)Enum.Parse(typeof(ReportName), RdlName)).GetHashCode());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                RSVersion = dr["RS_VERSION"].ToString();
                UrlConfigKey = dr["URL_CONFIG_KEY"].ToString();
                ServiceUrlConfigKey = dr["SERVICE_URL_CONFIG_KEY"].ToString();
            }

        }

        public Byte[] CreateFile()
        {
            try
            {
                string nameFolderKds = ConfigurationSettings.AppSettings["RSFolderApplication"];
                byte[] fileReport;
                _RptModule = new ReportModule();
                             // string ParamForLog = "\n" ;
                for (int i = 0; i < ReportParams.Count; i++)
                {
                    //                    ParamForLog +=  "param:" + Report.ReportParams[i].Name + "=" + Report.ReportParams[i].Value + "\n";
                    if (RSVersion == "RS2012")
                        _RptModule.AddParameter2012(ReportParams[i].Name, ReportParams[i].Value);
                    else
                        _RptModule.AddParameter(ReportParams[i].Name, ReportParams[i].Value);
                }
                //                clGeneral.LogMessage("Create" + Report.Extension.ToString() + " file:" + nameFolderKds + Report.RdlName +ParamForLog , System.Diagnostics.EventLogEntryType.Information);
                fileReport = _RptModule.CreateReport(nameFolderKds + RdlName, Extension, RSVersion, ServiceUrlConfigKey);
                return fileReport;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

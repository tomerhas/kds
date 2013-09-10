﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KdsLibrary.ReportingServices;
using System.Configuration;
using System.Data;

namespace KdsLibrary.Utils.Reports
{
    public class ReportModule
    {
        private static ReportModule _Instance;
        private string format,encoding, mimeType, extension;
        private string historyID = null, devInfo ;
        private Byte[] CurrentReportByte ;
        Warning[] warnings = null;
        string[] streamIDs = null;

        public ReportModule()
        {
            devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
        }
        public static ReportModule GetInstance()
        {
            if (_Instance == null)
                _Instance = new ReportModule();
            return _Instance;
        }
        private ReportingServices.ParameterValue[] parameters = new ReportingServices.ParameterValue[0];
        /// <summary>
        /// This Function add automaticly a param  named P_DT with Now() value to cause an auto refresh of the report created .
        /// in this case, this P_DT param has to be defined in the reporting service .
        /// </summary>
        public Byte[] CreateReport(String rptName, eFormat sFormat,bool AutoRefresh)
        {
            int LengthParam;
            if (AutoRefresh)
            {
                LengthParam = parameters.Length;
                Array.Resize(ref parameters, LengthParam + 1);
                parameters[LengthParam] = new ReportingServices.ParameterValue();
                parameters[LengthParam].Name = "P_DT";
                parameters[LengthParam].Value = DateTime.Now.ToString();
            }
            return CreateReport(rptName, sFormat);
        }

        public Byte[] CreateReport(String rptName, eFormat sFormat)
        {
            format = sFormat.ToString();
            ReportExecutionService rs = new ReportExecutionService();
            try
            {
                rs.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["RSUserName"], ConfigurationSettings.AppSettings["RSPassword"], ConfigurationSettings.AppSettings["RSDomain"]);
                rs.Url = ConfigurationSettings.AppSettings["ReportingServices.reportExecution2005"];
                ExecutionInfo execInfo = new ExecutionInfo();
                ExecutionHeader execHeader = new ExecutionHeader();

                rs.ExecutionHeaderValue = execHeader;
                rs.Timeout = 1000000000; 
                execInfo = rs.LoadReport(rptName, historyID);
                rs.SetExecutionParameters(parameters, "he-IL");
                String SessionId = rs.ExecutionHeaderValue.ExecutionID;
               CurrentReportByte=  rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                return CurrentReportByte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rs.Dispose();
                parameters = null;
            }
        }
        public FileInfo CreateOutputFile(string path, string filename)
        {
            try
            {
                string sFileName = path + @"\" + filename + Extention;
                if (CurrentReportByte == null)
                    throw new Exception(sFileName + " can't be created , because the binary report still wasn't created ...");
                FileStream fs;
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                fs.Write(CurrentReportByte, 0, CurrentReportByte.Length);
                fs.Flush();
                fs.Close();
                return new FileInfo(sFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddParameter(string sName, string sValue)
        {
            try
            {
                if (parameters == null)
                { parameters = new ReportingServices.ParameterValue[0]; }
                int LengthParam;
                LengthParam = parameters.Length;
                Array.Resize(ref parameters, LengthParam + 1);
                parameters[LengthParam] = new ReportingServices.ParameterValue();
                parameters[LengthParam].Name = sName;
                parameters[LengthParam].Value = sValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddParameter(string Name, DataSet Ds)
        {
            if (parameters == null)
            { parameters = new ReportingServices.ParameterValue[0]; }
            int LengthParam;
            LengthParam = parameters.Length;
            Array.Resize(ref parameters, LengthParam + 1);

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
            parameters[LengthParam] = new ReportingServices.ParameterValue();
            parameters[LengthParam].Name = Name;
            parameters[LengthParam].Value = tmpDs;
        }

        private string Extention
        {
            get
            {
                string Ext = string.Empty;
                switch ((eFormat)Enum.Parse(typeof(eFormat), format))
                {
                    case eFormat.EXCEL: 
                        Ext = ".xls";
                        break;
                    case eFormat.PDF: 
                        Ext = ".pdf";
                        break;
                }
                return Ext; 
            }
        }


    }
    public enum eFormat
    {
        EXCEL,
        PDF
    }

}
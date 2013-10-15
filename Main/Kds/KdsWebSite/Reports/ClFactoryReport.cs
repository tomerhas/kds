using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using System.IO;
using KdsLibrary.UI;
using System.Configuration;
using KdsLibrary.Security;
using System.Windows.Forms;
using KdsLibrary.UDT;
namespace KdsBatch.Reports
{

    public abstract class ClFactoryReport
    {

        protected List<clReport> _Reports;
        protected List<clDestinationReport> _DestinationReports;
        protected ReportModule _RptModule;
        protected string _NameOfReportFile = string.Empty;
        protected KdsLibrary.BL.clReport _BlReport;
        protected int _loginUser;
        protected DataTable _dtDetailsReport, _dtReportDefinitions, _dtDestinations;
        protected clGeneral.enStatusRequest _EndProcesSucceed;
        protected clGeneral.enReportType _enTypeRepot;

        protected OBJ_RIKUZ_PDF oObjRikuzPdf;
        protected COLL_RIKUZ_PDF oCollRikuzPdf;

        public ClFactoryReport()
        {
            _BlReport = KdsLibrary.BL.clReport.GetInstance();
            oCollRikuzPdf = new COLL_RIKUZ_PDF();
        }
        protected enum DetailsReports
        {
            KOD_SUG_DOCH,
            SHEM_PARAM_BADOCH,
            ERECH
        }
        protected enum DestReports
        {
            KOD_SUG_DOCH,
            SHEM_TIKIYA,
            EMAIL,
            MISPAR_ISHI
        }


        protected abstract void CreateReports();

        protected abstract void FillReports();

        protected abstract string SetNameOfReportFile(clReport CurrentReport);

        protected abstract void GetDataFromDb();

        protected Byte[] CreateFile(clReport Report)
        {
            try
            {
                string nameFolderKds = ConfigurationSettings.AppSettings["RSFolderApplication"];
                byte[] fileReport;
                //                string ParamForLog = "\n" ;
                for (int i = 0; i < Report.ReportParams.Count; i++)
                {
                    //                    ParamForLog +=  "param:" + Report.ReportParams[i].Name + "=" + Report.ReportParams[i].Value + "\n";
                    if (Report.RSVersion=="RS2012")
                        _RptModule.AddParameter2012(Report.ReportParams[i].Name, Report.ReportParams[i].Value);
                    else
                         _RptModule.AddParameter(Report.ReportParams[i].Name, Report.ReportParams[i].Value);
                }
                //                clGeneral.LogMessage("Create" + Report.Extension.ToString() + " file:" + nameFolderKds + Report.RdlName +ParamForLog , System.Diagnostics.EventLogEntryType.Information);
                fileReport = _RptModule.CreateReport(nameFolderKds + Report.RdlName, Report.Extension, Report.RSVersion, Report.ServiceUrlConfigKey);
                return fileReport;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SendMailErrorEventArgs(string Path, string eMail, string teur)
        {
            try
            {
                // ReportMail rpt = new ReportMail();
                string body = "";// rpt.GetMessageBody(Path);
                clMail email = new clMail(eMail, teur, body);
                email.attachFile(Path);
                email.IsHtmlBody(true);
                email.SendMail();
                email.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected virtual void DeleteFile(FileInfo File)
        {
            if (File.Exists)
            {
                GC.Collect();
                File.Delete();
            }
        }



        public virtual void MakeReports(long iRequestId)
        {
            byte[] fileReport;
            string pathAda = string.Empty, path, name = string.Empty, ErrorMessage = string.Empty;
            int iStatus = 0, i = 0;
            bool flag = false;
            FileInfo info;
            _RptModule = new ReportModule();// ReportModule.GetInstance();
            try
            {
                path = ConfigurationSettings.AppSettings["PathFileReports"];
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //                clGeneral.LogMessage("MakeReports:Before ForEach ,nb of report:" + _Reports.Count.ToString(), System.Diagnostics.EventLogEntryType.Information);
                foreach (clReport drReport in _Reports)
                {
                    try
                    {
                        fileReport = CreateFile(drReport);
                    }
                    catch (Exception ex)
                    {
                        flag = true;
                        clLogBakashot.InsertErrorToLog(iRequestId, drReport.MisparIshi, "E", 0, null, "MakeReports: " + ex.Message);
                        fileReport = null;
                    }

                    if (fileReport != null)
                    {
                        name = SetNameOfReportFile(drReport);
                        info = _RptModule.CreateOutputFile(path, name);
                        List<clDestinationReport> tempDest = getDestinationReports(drReport);/* _DestinationReports.FindAll(delegate(clDestinationReport Dest)
                                    {
                                        if ((Dest.Kod == drReport.KodReport) & (Dest.MisparIshi == drReport.MisparIshi)) return true;
                                        else return false;
                                    });
                                    */
                        if (tempDest.Count == 0)
                        {
                            ErrorMessage = "MakeReports::No Destination found for report kod:" + drReport.KodReport + ",BakashaId:" + drReport.BakashaId;
                            clGeneral.LogMessage(ErrorMessage, System.Diagnostics.EventLogEntryType.Warning);
                            clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "W", 0, null, ErrorMessage);
                        }
                        foreach (clDestinationReport dest in tempDest)
                        {
                            if (dest.TypeSending == TypeSending.Folder)
                            {
                                if (!Directory.Exists(dest.Folder))
                                    Directory.CreateDirectory(dest.Folder);
                                info.CopyTo(dest.Folder + @"\" + info.Name, true);
                            }
                            else if (dest.TypeSending == TypeSending.EMail)
                                SendMailErrorEventArgs(info.FullName, dest.eMail, drReport.Teur);
                            else throw new Exception("Missing destination for report " + drReport.RdlName + "(" + drReport.KodReport + "):" + name);
                        }
                        info.Delete();
                        break;
                    }
                }

                if (flag)
                    clDefinitions.UpdateLogBakasha(iRequestId, DateTime.Now, clGeneral.enStatusRequest.PartEnded.GetHashCode());
                else
                    clDefinitions.UpdateLogBakasha(iRequestId, DateTime.Now, _EndProcesSucceed.GetHashCode());
            }
            catch (Exception ex)
            {
                clGeneral.LogMessage(ex.Message, System.Diagnostics.EventLogEntryType.Error, true);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clDefinitions.UpdateLogBakasha(iRequestId, DateTime.Now, iStatus);
                clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "E", 0, null, "MakeReports: " + ex.Message + ((name != string.Empty) ? " for report :" + name : ""));
            }
        }


        protected virtual List<clDestinationReport> getDestinationReports(clReport CurrentReport)
        {
            List<clDestinationReport> ListDest = _DestinationReports.FindAll(delegate(clDestinationReport Dest)
             {
                 if ((Dest.Kod == CurrentReport.KodReport) & (Dest.MisparIshi == CurrentReport.MisparIshi)) return true;
                 else return false;
             });
            return ListDest;
        }

        protected virtual void FillDestinations()
        {
            if (_dtReportDefinitions.Rows.Count == 0)
                throw new Exception("No actives reports are defined in the database .");

            DataRow[] drDest;//= new DataRow();
            try
            {
                foreach (clReport drReport in _Reports)
                {
                    drDest = _dtDestinations.Select("SHEM_TIKIYA IS NOT NULL AND KOD_SUG_DOCH=" + drReport.KodReport + " AND MISPAR_ISHI=" + drReport.MisparIshi);
                    for (int i = 0; i < drDest.Length; i++)
                    {
                        _DestinationReports.Add(new clDestinationReport(TypeSending.Folder,
                                                    clGeneral.GetIntegerValue(drDest[i].ItemArray[(int)DestReports.KOD_SUG_DOCH].ToString()),
                                                    drDest[i].ItemArray[(int)DestReports.SHEM_TIKIYA].ToString(),
                                                    clGeneral.GetIntegerValue(drDest[i].ItemArray[(int)DestReports.MISPAR_ISHI].ToString())));
                    }
                    drDest = _dtDestinations.Select("EMAIL IS NOT NULL AND KOD_SUG_DOCH=" + drReport.KodReport + " AND MISPAR_ISHI=" + drReport.MisparIshi);
                    for (int i = 0; i < drDest.Length; i++)
                    {
                        _DestinationReports.Add(new clDestinationReport(TypeSending.EMail,
                                                    clGeneral.GetIntegerValue(drDest[i].ItemArray[(int)DestReports.KOD_SUG_DOCH].ToString()),
                                                    drDest[i].ItemArray[(int)DestReports.EMAIL].ToString(),
                            clGeneral.GetIntegerValue(drDest[i].ItemArray[(int)DestReports.MISPAR_ISHI].ToString())));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}

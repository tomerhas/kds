using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using System.IO;
using KdsLibrary.UI;
using KdsLibrary.BL;
using System.Configuration;
using KdsLibrary.Security;
using System.Windows.Forms;
using KdsLibrary.UDT;

namespace KdsBatch.Reports
{
    public class clManagerRikuzim : ClFactoryReport
    {
        // private string _Month;
        private long _lBakashaIdForRikuzim;
        private int _NumOfProcess;

        //#region propterties 
        //private string Month
        //{
        //    get
        //    {
        //        return _Month;
        //    }
        //}
        //#endregion 

        public clManagerRikuzim(long iRequestId, long iRequestIdForRikuzim, int numOfProcess)
        {
            int iStatus = 0;
            try
            {
                _NumOfProcess = numOfProcess;
                _enTypeRepot = clGeneral.enReportType.Rikuz;
                //_Month = Period;
                //_loginUser = iUserId;
                _lBakashaIdForRikuzim = iRequestIdForRikuzim;
                _EndProcesSucceed = clGeneral.enStatusRequest.ToBeEnded;

                _Reports = new List<clReport>();
                _DestinationReports = new List<clDestinationReport>();
                GetDataFromDb();
                CreateReports();
                //FillDestinations();
            }
            catch (Exception ex)
            {
                clGeneral.LogMessage(ex.Message, System.Diagnostics.EventLogEntryType.Error, true);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clDefinitions.UpdateLogBakasha(iRequestId, DateTime.Now, iStatus);
                clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "E", 0, null, "clManagerReport: " + ex.Message);
            }

        }

        protected override void CreateReports()
        {
            try
            {
                foreach (DataRow dr in _dtDetailsReport.Rows)
                {
                    _Reports.Add(new clRikuz(_lBakashaIdForRikuzim,
                                clGeneral.GetIntegerValue(dr["MISPAR_ISHI"].ToString()),
                                DateTime.Parse(dr["TAARICH"].ToString()),
                                (dr["sug_chishuv"].ToString() != "" ? int.Parse(dr["sug_chishuv"].ToString()) : -1),
                                clGeneral.GetIntegerValue(dr["EZOR"].ToString()), clGeneral.GetIntegerValue(dr["MAAMAD"].ToString()), clGeneral.GetIntegerValue(dr["KOD_HEVRA"].ToString()), DateTime.Parse(dr["ZMAN_HATCHALA"].ToString())));

                    //    _Reports.Add(new clReport(_lBakashaIdForRikuzim,
                    //                 clGeneral.GetIntegerValue(dr["MISPAR_ISHI"].ToString()),
                    //                 DateTime.Parse(dr["TAARICH"].ToString()),
                    //                 (dr["sug_chishuv"].ToString() != "" ? int.Parse(dr["sug_chishuv"].ToString()) : -1),
                    //                 clGeneral.GetIntegerValue(dr["EZOR"].ToString()), clGeneral.GetIntegerValue(dr["MAAMAD"].ToString()), clGeneral.GetIntegerValue(dr["KOD_HEVRA"].ToString()), DateTime.Parse(dr["ZMAN_HATCHALA"].ToString())));
                }
                FillReports();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected override void GetDataFromDb()
        {
            _dtDetailsReport = _BlReport.getDetailsOvdimLeRikuzim(_lBakashaIdForRikuzim,_NumOfProcess);
        }

        protected override void FillReports()
        {
            DataRow[] drParams;
            clOvdim oOvdim = new clOvdim();

            try
            {
                foreach (clRikuz drRikuz in _Reports)
                {
                    drParams = _dtDetailsReport.Select("MISPAR_ISHI=" + drRikuz.MisparIshi + " and taarich=Convert('" + drRikuz.Month.ToShortDateString() + "', 'System.DateTime')");

                    if (drParams.Length > 0)
                    {
                        drRikuz.Add("P_MISPAR_ISHI", drRikuz.MisparIshi.ToString());
                        drRikuz.Add("P_TAARICH", drRikuz.Month.AddMonths(1).AddDays(-1).ToShortDateString());
                        drRikuz.Add("P_BAKASHA_ID", drRikuz.BakashaId.ToString());
                        drRikuz.Add("P_Tar_chishuv", drRikuz.TarChishuv.ToShortDateString());
                        if (drParams[0]["sug_chishuv"].ToString() != "")
                            drRikuz.Add("P_sug_chishuv", clGeneral.arrCalcType[int.Parse(drParams[0]["sug_chishuv"].ToString())]);
                        else drRikuz.Add("P_sug_chishuv", "");
                        drRikuz.Add("P_Oved_5_Yamim", getTeurWorkDay(drParams[0]["WorkDay"].ToString()));
                        drRikuz.Add("P_SIKUM_CHODSHI", "1");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getTeurWorkDay(string sWorkDay)
        {
            if (int.Parse(sWorkDay) == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
            { return "5 ימים חודשי"; }
            else if (int.Parse(sWorkDay) == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode())
            { return "5 ימים יומי"; }
            else if (int.Parse(sWorkDay) == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
            { return "6 ימים יומי"; }
            else { return "6 ימים חודשי"; }
        }

        protected override string SetNameOfReportFile(clReport CurrentRikuz)
        {
            return "Rikuz_" + ((clRikuz)CurrentRikuz).Month.ToString("yyMM") + CurrentRikuz.MisparIshi.ToString().PadLeft(5, '0'); //, CurrentReport.Month.Year.ToString().Substring(2, 2) +
            //  CurrentReport.Month.Month.ToString() + CurrentReport.MisparIshi;
        }

        protected override List<clDestinationReport> getDestinationReports(clReport CurrentReport)
        {
            List<clDestinationReport> ListDest = _DestinationReports.FindAll(delegate(clDestinationReport Dest)
            {
                if ((Dest.Kod == CurrentReport.KodReport) & (Dest.MisparIshi == CurrentReport.MisparIshi) & (Dest.Month == ((clRikuz)CurrentReport).Month)) return true;
                else return false;
            });
            return ListDest;
        }

        protected override void FillDestinations()
        {
            string PathFolderRikuzim = "";
            string PathFolderHefreshim = "";
            string PathFolder;
            DataRow[] drDest;
            try
            {

                CreateFoldersByMonth(ref PathFolderRikuzim, ref PathFolderHefreshim);
                foreach (clRikuz drRikuz in _Reports)
                {
                    drDest = _dtDestinations.Select("MISPAR_ISHI=" + drRikuz.MisparIshi + " and taarich=Convert('" + drRikuz.Month.ToShortDateString() + "', 'System.DateTime')");

                    if (drDest[0]["SUG_CHISHUV"].ToString() == "0")
                    {
                        PathFolder = PathFolderRikuzim;
                    }
                    else
                    {
                        PathFolder = PathFolderHefreshim; // ConfigurationSettings.AppSettings["fullPhysPathHefreshDoc"] + @"\" + drReport.Month.ToString("MMyyyy");
                    }

                    _DestinationReports.Add(new clDestinationReport(TypeSending.Folder,
                                            drRikuz.KodReport,
                                            PathFolder,
                                            drRikuz.MisparIshi,
                                            drRikuz.Month));

                    if (drDest.Length > 0 && !string.IsNullOrEmpty(drDest[0]["EMAIL"].ToString()))
                    {
                        _DestinationReports.Add(new clDestinationReport(TypeSending.EMail,
                                                drRikuz.KodReport,
                                               "meravn@egged.co.il", // drDest[0]["EMAIL"].ToString(),
                                                drRikuz.MisparIshi,
                                                drRikuz.Month));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateFoldersByMonth(ref string PathFolderRikuzim, ref string PathFolderHefreshim)
        {
            //  string PathFolderRikuzim; 
            DataRow[] drDest;
            try
            {
                drDest = _dtDestinations.Select("SUG_CHISHUV=0");
                if (drDest.Length > 0)
                {
                    PathFolderRikuzim = ConfigurationManager.AppSettings["fullPhysPathRikuzDoc"] + @"\\" + DateTime.Parse(drDest[0]["taarich"].ToString()).ToString("yyyyMM");
                    if (!Directory.Exists(PathFolderRikuzim))
                    {
                        Directory.CreateDirectory(PathFolderRikuzim);
                    }
                }

                drDest = _dtDestinations.Select("SUG_CHISHUV=1");
                if (drDest.Length > 0)
                {
                    PathFolderHefreshim = ConfigurationManager.AppSettings["fullPhysPathHefreshDoc"] + @"\\" + DateTime.Parse(drDest[0]["taarich"].ToString()).ToString("yyyyMM");
                    if (!Directory.Exists(PathFolderHefreshim))
                    {
                        Directory.CreateDirectory(PathFolderHefreshim);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void MakeReports(long iRequestId)
        {
            byte[] fileReport;
            string pathAda = string.Empty, path, name = string.Empty, ErrorMessage = string.Empty;
            int iStatus = 0, iCntDone = 0;
            bool flag = false;
            _RptModule = new ReportModule();
            try
            {
                pathAda = ConfigurationManager.AppSettings["PhysPathRikuzimAda"];
                if (!Directory.Exists(pathAda))
                    Directory.CreateDirectory(pathAda);
                path = ConfigurationManager.AppSettings["PathFileReports"];
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (_Reports.Count > 0)
                {
                    foreach (clRikuz drRikuz in _Reports)
                    {
                        try
                        {
                            fileReport = CreateFile(drRikuz);
                        }
                        catch (Exception ex)
                        {
                            flag = true;
                            string Msg = ex.Message + "\n" + ex.StackTrace + ((ex.InnerException != null)? ex.InnerException.ToString() : "");
                            clGeneral.LogMessage(Msg, System.Diagnostics.EventLogEntryType.Error, true);
                            clLogBakashot.InsertErrorToLog(iRequestId, drRikuz.MisparIshi, "E", 0, null, "MakeReports: " + ex.Message);
                            fileReport = null;
                        }

                        if (fileReport != null)
                        {
                            oObjRikuzPdf = new OBJ_RIKUZ_PDF();
                            oObjRikuzPdf.MISPAR_ISHI = drRikuz.MisparIshi;
                            oObjRikuzPdf.BAKASHA_ID = drRikuz.BakashaId;
                            oObjRikuzPdf.TAARICH = drRikuz.Month;
                            oObjRikuzPdf.SUG_CHISHUV = drRikuz.sug_chishuv;
                            oObjRikuzPdf.RIKUZ_PDF = fileReport;
                            oCollRikuzPdf.Add(oObjRikuzPdf);

                            TransferFileToAda(iRequestId, pathAda, drRikuz, fileReport);
                            iCntDone++;
                        }
                    }
                    if (oCollRikuzPdf.Value.Length > 0)
                        _BlReport.SaveRikuzmPdf(oCollRikuzPdf, oObjRikuzPdf.BAKASHA_ID, _NumOfProcess);
                }
                if (flag)
                    clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "I", 0, null, "Process " + _NumOfProcess + " finished his job :(" + iCntDone + "/" + _Reports.Count + " records) , with warning .");
                else
                    clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "I", 0, null, "Process " + _NumOfProcess + " finished his job (" + iCntDone + "/" + _Reports.Count + " records).");
            }
            catch (Exception ex)
            {
                clGeneral.LogMessage(ex.Message + "\n" + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, true);
                clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "E", 0, null, "MakeReports: " + ex.Message + ((name != string.Empty) ? " for report :" + name : ""));
            }
        }

        private void TransferFileToAda(long iRequestId, string path, clRikuz drRikuz, byte[] fileReport)
        {
            FileStream fs = null;
            string sFileName =string.Empty;
            try
            {
                sFileName = "RIKUZ";
                sFileName += drRikuz.Hevra.ToString().PadLeft(4, char.Parse("0"));
                sFileName += drRikuz.MisparIshi.ToString().PadLeft(5, char.Parse("0"));
                sFileName += drRikuz.Month.Year.ToString().PadLeft(4, char.Parse("0"));
                sFileName += drRikuz.Month.Month.ToString().PadLeft(2, char.Parse("0"));
                sFileName += drRikuz.TarChishuv.ToString("ddMMyyyy");
                if (drRikuz.sug_chishuv == -1)
                    sFileName += "3";
                else sFileName += ((drRikuz.sug_chishuv) + 1).ToString();
                sFileName += drRikuz.Ezor.ToString();
                sFileName += drRikuz.Maamad.ToString().PadLeft(3, char.Parse("0"));
                sFileName += ".PDF";

                fs = new FileStream(path + sFileName, FileMode.Create, FileAccess.Write);
                fs.Write(fileReport, 0, fileReport.Length);

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iRequestId, _loginUser, "E", 0, null, "TransferFileToAda:" + ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
            }
        }



    }


}

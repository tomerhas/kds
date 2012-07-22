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

namespace KdsBatch.Reports
{
    public class clManagerRikuzim : ClFactoryReport 
    {
       // private string _Month;
        private long _lBakashaIdForRikuzim;

        //#region propterties 
        //private string Month
        //{
        //    get
        //    {
        //        return _Month;
        //    }
        //}
        //#endregion 

        public clManagerRikuzim(long iRequestId, long iRequestIdForRikuzim)
        {
            int iStatus = 0;
            try
            {
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
                clGeneral.LogMessage(ex.Message, System.Diagnostics.EventLogEntryType.Error,true);
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
                    _Reports.Add(new clReport(_lBakashaIdForRikuzim,
                                 clGeneral.GetIntegerValue(dr["MISPAR_ISHI"].ToString()),
                                 DateTime.Parse(dr["TAARICH"].ToString()),
                                 (dr["sug_chishuv"].ToString() != "" ? int.Parse(dr["sug_chishuv"].ToString()): -1) ,
                                 clGeneral.GetIntegerValue(dr["EZOR"].ToString()), clGeneral.GetIntegerValue(dr["MAAMAD"].ToString()), clGeneral.GetIntegerValue(dr["KOD_HEVRA"].ToString()), DateTime.Parse(dr["ZMAN_HATCHALA"].ToString())));
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
           _dtDetailsReport = _BlReport.getDetailsOvdimLeRikuzim(_lBakashaIdForRikuzim);
           //--_dtDestinations = _BlReport.getEmailOvdimLeRikuzim(_lBakashaIdForRikuzim);
        }

        protected override void FillReports()
        {
            DataRow[] drParams ;
            clOvdim oOvdim = new clOvdim();

            try
            {
                foreach (clReport drReport in _Reports)
                {
                    drParams = _dtDetailsReport.Select("MISPAR_ISHI=" + drReport.MisparIshi + " and taarich=Convert('" + drReport.Month.ToShortDateString() + "', 'System.DateTime')");

                    if (drParams.Length > 0)
                    {
                        drReport.Add("P_MISPAR_ISHI", drReport.MisparIshi.ToString());
                        drReport.Add("P_TAARICH", drReport.Month.AddMonths(1).AddDays(-1).ToShortDateString());
                        drReport.Add("P_BAKASHA_ID", drReport.BakashaId.ToString());
                        drReport.Add("P_Tar_chishuv", drReport.TarChishuv.ToShortDateString());
                        if (drParams[0]["sug_chishuv"].ToString() != "")
                            drReport.Add("P_sug_chishuv", clGeneral.arrCalcType[int.Parse(drParams[0]["sug_chishuv"].ToString())]);
                        else drReport.Add("P_sug_chishuv", "");
                        drReport.Add("P_Oved_5_Yamim", getTeurWorkDay(drParams[0]["WorkDay"].ToString()));
                        drReport.Add("P_SIKUM_CHODSHI", "1");
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

        protected override string SetNameOfReportFile(clReport CurrentReport)
        {
            return "Rikuz_" + CurrentReport.Month.ToString("yyMM") + CurrentReport.MisparIshi.ToString().PadLeft(5,'0'); //, CurrentReport.Month.Year.ToString().Substring(2, 2) +
                //  CurrentReport.Month.Month.ToString() + CurrentReport.MisparIshi;
        }

        protected override List<clDestinationReport> getDestinationReports(clReport CurrentReport)
        {
            List<clDestinationReport> ListDest = _DestinationReports.FindAll(delegate(clDestinationReport Dest)
            {
                if ((Dest.Kod == CurrentReport.KodReport) & (Dest.MisparIshi == CurrentReport.MisparIshi) & (Dest.Month == CurrentReport.Month)) return true;
                else return false;
            });
            return ListDest;
        }

        protected override void FillDestinations()
        {
            string PathFolderRikuzim="";
            string PathFolderHefreshim="";
            string PathFolder;
            DataRow[] drDest;
            try
            {

                CreateFoldersByMonth(ref PathFolderRikuzim, ref PathFolderHefreshim);
                foreach (clReport drReport in _Reports)
                {
                    drDest = _dtDestinations.Select("MISPAR_ISHI=" + drReport.MisparIshi + " and taarich=Convert('" + drReport.Month.ToShortDateString() + "', 'System.DateTime')");

                    if (drDest[0]["SUG_CHISHUV"].ToString() == "0")
                    {
                        PathFolder = PathFolderRikuzim;
                    }
                    else
                    {
                        PathFolder = PathFolderHefreshim; // ConfigurationSettings.AppSettings["fullPhysPathHefreshDoc"] + @"\" + drReport.Month.ToString("MMyyyy");
                    }

                    _DestinationReports.Add(new clDestinationReport(TypeSending.Folder,
                                            drReport.KodReport,
                                            PathFolder,
                                            drReport.MisparIshi,
                                            drReport.Month));

                    if (drDest.Length > 0 && !string.IsNullOrEmpty(drDest[0]["EMAIL"].ToString()))
                    {
                        _DestinationReports.Add(new clDestinationReport(TypeSending.EMail,
                                                drReport.KodReport,
                                               "meravn@egged.co.il", // drDest[0]["EMAIL"].ToString(),
                                                drReport.MisparIshi,
                                                drReport.Month));
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
                    PathFolderRikuzim = ConfigurationSettings.AppSettings["fullPhysPathRikuzDoc"] + @"\\" + DateTime.Parse(drDest[0]["taarich"].ToString()).ToString("yyyyMM");
                    if (!Directory.Exists(PathFolderRikuzim))
                    {
                        Directory.CreateDirectory(PathFolderRikuzim);
                    }
                }

                drDest = _dtDestinations.Select("SUG_CHISHUV=1");
                if (drDest.Length > 0)
                {
                    PathFolderHefreshim = ConfigurationSettings.AppSettings["fullPhysPathHefreshDoc"] + @"\\" + DateTime.Parse(drDest[0]["taarich"].ToString()).ToString("yyyyMM");
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


      

    }

   
}

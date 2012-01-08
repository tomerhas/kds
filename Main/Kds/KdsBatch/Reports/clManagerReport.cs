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
using System.Configuration;
using KdsLibrary.Security;
using System.Windows.Forms;



namespace KdsBatch.Reports
{
    public class clManagerReport : ClFactoryReport 
    {
        private string _Month;
        private const string cStartDateParameter = "P_STARTDATE"; 
        private const string cEndDateParameter = "P_ENDDATE";
        private const string cMonthParameter = "P_PERIOD"; 
        private DateTime _StartDate, _EndDate; 

        #region propterties 
        private string StartDate
        {
            get
            {
                return _StartDate.ToString("dd/MM/yyyy");
            }
        }
        private string EndDate
        {
            get
            {
                return _EndDate.ToString("dd/MM/yyyy");
            }
        }
        private string Month
        {
            get
            {
                return _Month;
            }
        }
        #endregion 

        public clManagerReport(long iRequestId,string Period, int iUserId)
        {
            int iStatus = 0;
            try
            {
                _enTypeRepot = clGeneral.enReportType.ConstantReport;

                _Month = Period;
                _StartDate = DateTime.Parse("01/" + Period);
                _EndDate = _StartDate.AddMonths(1).AddDays(-1);
                _loginUser = iUserId;
                _EndProcesSucceed = clGeneral.enStatusRequest.ToBeEnded;

                _Reports = new List<clReport>();
                _DestinationReports = new List<clDestinationReport>();
                GetDataFromDb();
                CreateReports();
                FillDestinations();
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
                foreach (DataRow dr in _dtReportDefinitions.Rows)
                {
                    _Reports.Add(new clReport(dr["NAME"].ToString(),
                                 clGeneral.GetIntegerValue(dr["KOD"].ToString()),
                                 dr["TEUR_DOCH"].ToString(),
                                 clGeneral.GetIntegerValue(dr["TVACH_TAARICHIM"].ToString()),
                                 clGeneral.GetIntegerValue(dr["MISPAR_ISHI"].ToString())));
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
//            clGeneral.LogMessage("Get Data From Db", System.Diagnostics.EventLogEntryType.Information);
            _dtDetailsReport = _BlReport.getDetailsReports(0);
            _dtReportDefinitions = _BlReport.getDefinitionReports(0);
            _dtDestinations = _BlReport.getDestinationsReports(0);
        }

        protected override void FillReports()
        {
            DataRow[] drParams ;
            try
            {
                foreach (clReport drReport in _Reports)
                {
                    if (drReport.HasPeriodParameters)
                    {
                        drReport.Add(cStartDateParameter, StartDate);
                        drReport.Add(cEndDateParameter, EndDate);
                    }
                    else drReport.Add(cMonthParameter, Month);

                    drParams = _dtDetailsReport.Select("KOD_SUG_DOCH=" + drReport.KodReport +" AND MISPAR_ISHI=" + drReport.MisparIshi);//, "TAARICH_IDKUN_ACHARON ASC");
                    for (int i = 0; i < drParams.Length; i++)
                    {
                        drReport.Add(drParams[i].ItemArray[(int)DetailsReports.SHEM_PARAM_BADOCH].ToString(),
                                     drParams[i].ItemArray[(int)DetailsReports.ERECH].ToString());
                    }
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override string SetNameOfReportFile(clReport CurrentReport)
        {
            return "Report" + CurrentReport.KodReport + "_" + DateTime.Now.ToShortDateString().Replace("/", "") + "_" +
                                             DateTime.Now.ToShortTimeString().Replace(":", "");
        }


    }

   
}

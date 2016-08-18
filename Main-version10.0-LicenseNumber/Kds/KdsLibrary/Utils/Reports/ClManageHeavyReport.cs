using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.Utils.Reports;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Logs;

namespace KdsLibrary.Utils.Reports
{
    public class ClManageHeavyReport : ClFactoryReport
    {
        private long _lBakashaId;
        public ClManageHeavyReport(long iRequestId)
        {
            int iStatus = 0;
            try
            {
                _enTypeRepot = clGeneral.enReportType.HeavyReport;
                _loginUser = 0;
                _EndProcesSucceed = clGeneral.enStatusRequest.PartEnded;
                _lBakashaId = iRequestId;
                _Reports = new List<clReport>();
                _DestinationReports = new List<clDestinationReport>();
                GetDataFromDb();
                CreateReports();
                FillDestinations();
            }
            catch (Exception ex)
            {
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().UpdateLogBakasha(iRequestId, DateTime.Now, iStatus);
                clGeneral.LogMessage(ex.Message, System.Diagnostics.EventLogEntryType.Error, true);
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(iRequestId, "E", 0, "ClManageHeavyReport: " + ex.Message, _loginUser,null);
                throw;
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
                                              long.Parse(dr["BAKASHA_ID"].ToString()),
                                              (eFormat)Enum.Parse(typeof(eFormat),clGeneral.GetIntegerValue(dr["EXTENSION_TYPE"].ToString()).ToString()),
                                              clGeneral.GetIntegerValue(dr["MISPAR_ISHI"].ToString()),
                                              dr["RS_VERSION"].ToString(),
                                              dr["URL_CONFIG_KEY"].ToString(),
                                              dr["SERVICE_URL_CONFIG_KEY"].ToString()
                                            )
                                 
                                 );
                }
                FillReports();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected override string SetNameOfReportFile(clReport CurrentReport)
        {
            return CurrentReport.BakashaId.ToString();
        }
        protected override void FillReports()
        {
            DataRow[] drParams;
            try
            {
                foreach (clReport drReport in _Reports)
                {
                    drParams = _dtDetailsReport.Select("KOD_SUG_DOCH=" + drReport.KodReport);//, "TAARICH_IDKUN_ACHARON ASC");
                    for (int i = 0; i < drParams.Length; i++)
                        drReport.Add(drParams[i].ItemArray[(int)DetailsReports.SHEM_PARAM_BADOCH].ToString(),
                                     drParams[i].ItemArray[(int)DetailsReports.ERECH].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void GetDataFromDb()
        {
//            clGeneral.LogMessage("Get Data From Db", System.Diagnostics.EventLogEntryType.Information);
            _dtDetailsReport = _BlReport.getDetailsReports(_lBakashaId);
            _dtReportDefinitions = _BlReport.getDefinitionReports(_lBakashaId);
            _dtDestinations = _BlReport.getDestinationsReports(_lBakashaId);
        }



    }
}

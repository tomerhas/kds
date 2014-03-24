using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KdsBatch;
using System.Threading;
using KdsLibrary;
using KdsBatch.Reports;
using KdsBatch.Premia;
using KdsBatch.MonthlyMails;
using KdsDataImport;
using KdsLibrary.BL;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using KdsBatch.History;
using DalOraInfra.DAL;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Logs;
namespace KdsService
{
    public class BatchService : IBatchService
    {

        #region Methods
        private void RunExecuteInputDataAndErrorsThread(object param)
        {
            object[] args = param as object[];
            clGeneral.BatchRequestSource requestSource = (clGeneral.BatchRequestSource)args[0];
            clGeneral.BatchExecutionType execType = (clGeneral.BatchExecutionType)args[1];
            DateTime workDate = (DateTime)args[2];
            long btchRequest = (long)args[3];

            try
            {
                clBatchFactory.ExecuteInputDataAndErrors(requestSource, execType, workDate, btchRequest);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            LogThreadEnd("ExecuteInputDataAndErrors", btchRequest);
        }

        private void RunShinuyimVeShguimBatch(object param)
        {
            clUtils oUtils = new clUtils();
            string sArguments = "";
            int iStatus = 0;
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            DateTime dTaarich = (DateTime)args[1];
            string path, exfile;
            FileInfo KdsCalcul = null;
            var logger = ServiceLocator.Current.GetInstance<ILogBakashot>();
            clGeneral.enCalcType TypeShguyim = ((clGeneral.enCalcType)Enum.Parse(typeof(clGeneral.enCalcType), args[2].ToString()));
            clGeneral.BatchExecutionType ExecutionTypeShguim = ((clGeneral.BatchExecutionType)Enum.Parse(typeof(clGeneral.BatchExecutionType), args[3].ToString()));
            try
            {
                logger.InsertLog(lRequestNum, "I", 0, "START");
                int iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["ShinuimShguimsProcessesNb"]);
                path = ConfigurationManager.AppSettings["MultiProcessesAppPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsShinuimShguimsFileName"].ToString();
                KdsCalcul = new FileInfo(path + exfile);

                logger.InsertLog(lRequestNum, "I", 0, "KdsCalul will run from " + KdsCalcul.FullName);
                switch (TypeShguyim)
                {
                    case clGeneral.enCalcType.ShinuyimVeShguyim:
                        logger.InsertLog(lRequestNum, "I", 0, "clGeneral.enCalcType.ShinuyimVeShguyim");
                        oUtils.PrepareNetunimToShguyimBatch(dTaarich, clGeneral.BatchRequestSource.ImportProcess.GetHashCode(), iCntProcesses, lRequestNum);
                        break;
                    case clGeneral.enCalcType.ShinuyimVeSghuimHR:
                        logger.InsertLog(lRequestNum, "I", 0, "clGeneral.enCalcType.ShinuyimVeSghuimHR");
                        oUtils.PrepareNetunimToShguyimBatchHR(clGeneral.BatchRequestSource.ImportProcessForChangesInHR.GetHashCode(), iCntProcesses, lRequestNum);
                        break;
                    case clGeneral.enCalcType.ShinuyimVeSghuimPremiot:
                        logger.InsertLog(lRequestNum, "I", 0, "clGeneral.enCalcType.ShinuyimVeSghuimPremiot");
                        oUtils.PrepareNetunimToPremiotShguyimBatch(clGeneral.BatchRequestSource.ImportProcessForPremiot.GetHashCode(), iCntProcesses, lRequestNum);
                        break;
                }
                // oCalcDal.PrepareDataLeChishuv(dFrom, dAdChodesh, sMaamad, bRitzaGorefet, iCntProcesses);
                logger.InsertLog(lRequestNum, "I", 0, "Finish to prepoare the general data");
                     
                if (KdsCalcul.Exists)
                {
                    sArguments = TypeShguyim.GetHashCode() + " " + lRequestNum.ToString() + " " + ExecutionTypeShguim.GetHashCode();
                    iStatus = RunMultiProcesses(lRequestNum, KdsCalcul, sArguments, iCntProcesses);
                    //  iStatus = RunKdsCalcul(KdsCalcul, lRequestNum, dFrom, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet, iCntProcesses);
                }
                else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();

            }
            catch (Exception ex)
            {
                logger.InsertLog(lRequestNum, "I", 0, "Failed");
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                logger.InsertLog(lRequestNum, "E", 0, "RunSinuyimVeShguimBatch: " + ex.Message);

                throw ex;
            }
            finally
            {
                CheckProcessesTerminated(KdsCalcul, lRequestNum, iStatus);
            }
            //LogThreadEnd("CalcBatchParallel", lRequestNum);
        }

        private int RunMultiProcesses(long BakashaId, FileInfo FileToRun, string sArguments, int CountOfProcesses)
        {
            try
            {
                for (int i = 1; i <= CountOfProcesses; i++)
                {
                    Process _process = new Process();
                    _process.StartInfo.RedirectStandardOutput = false;
                    _process.StartInfo.FileName = FileToRun.FullName;
                    _process.StartInfo.UseShellExecute = false;
                    _process.StartInfo.WorkingDirectory = FileToRun.DirectoryName;
                    _process.StartInfo.RedirectStandardError = true;
                    _process.StartInfo.Arguments = sArguments + " " + i.ToString();
                    _process.Start();
                    _process.PriorityClass = ProcessPriorityClass.BelowNormal;
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(BakashaId, "I", 0, FileToRun.Name + " " + i.ToString() + " was started.");

                    _process.Dispose();
                }
                return clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex.StackTrace);
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(BakashaId, "E", 0, "RunMultiProcesses: " + ex.Message);

                return clGeneral.enStatusRequest.Failure.GetHashCode();
            }
        }

        private void CheckProcessesTerminated(FileInfo FileToRun, long BakashaID, int Status)
        {
            Process[] List;
            do
            {
                List = Process.GetProcessesByName(FileToRun.Name.Split('.')[0]);
                if (List.Count() == 0)
                {
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(BakashaID, "I", 0, "END");
                    clDefinitions.UpdateLogBakasha(BakashaID, DateTime.Now, Status);
                    break;
                }
                else Thread.Sleep(5000);
            } while (List.Count() > 0);
        }

        private void RunCalcBatchParallel(object param)
        {
            clUtils oUtils = new clUtils();
            clCalcDal oCalcDal = new clCalcDal();
            DateTime dFrom;
            DataTable dtParametrim;
            string sArguments = "";
            int result, resultK,iStatus = 0;
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            DateTime dAdChodesh = (DateTime)args[1];
            string sMaamad = args[2].ToString();
            bool bRitzatTest = (bool)args[3];
            bool bRitzaGorefet = (bool)args[4];
            int iCntProcesses;
            string path, exfile;
            FileInfo KdsCalcul = null;
            var logger = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                logger.InsertLog(lRequestNum, "I", 0, "START");             
                iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["CalculProcessesNb"]);
                path = ConfigurationManager.AppSettings["MultiProcessesAppPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsCalculFileName"].ToString();
                KdsCalcul = new FileInfo(path + exfile);

                logger.InsertLog(lRequestNum, "I", 0, "KdsCalul will run from " + KdsCalcul.FullName);
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                dFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths((int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString())) * -1);
                dAdChodesh = dAdChodesh.AddMonths(1).AddDays(-1);
                result = oCalcDal.PrepareDataLeChishuv(lRequestNum,dFrom, dAdChodesh, sMaamad, bRitzaGorefet, iCntProcesses);
                resultK = oCalcDal.FillKavimDetailsLechishuv(lRequestNum, dFrom, dAdChodesh, iCntProcesses);
                logger.InsertLog(lRequestNum, "I", 0, "Finish to prepare the general data");

                if (result > 0 && resultK > 0)
                {
                    if (KdsCalcul.Exists)
                    {
                        sArguments = clGeneral.enCalcType.MonthlyCalc.GetHashCode() + " " + lRequestNum.ToString() + " " + dFrom.ToShortDateString() + " " + dAdChodesh.ToShortDateString() + " " +
                                                 sMaamad + " " + bRitzatTest.GetHashCode().ToString() + " " + bRitzaGorefet.GetHashCode().ToString();
                        //  iStatus = oUtils.RunKdsCalcul(lRequestNum, KdsCalcul, sArguments, iCntProcesses);
                        iStatus = RunMultiProcesses(lRequestNum, KdsCalcul, sArguments, iCntProcesses);
                    }
                    else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                }
            }
            catch (Exception ex)
            {
                logger.InsertLog(lRequestNum, "I", 0, "Failed");
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                logger.InsertLog(lRequestNum, "E", 0, "RunCalcBatchParallel: " + ex.Message);
       
                throw ex;
            }
            finally
            {
                CheckProcessesTerminated(KdsCalcul, lRequestNum, iStatus);
            }
            //LogThreadEnd("CalcBatchParallel", lRequestNum);
        }

        private void RunInsetRecordsToHistory(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            ManagerTask oTaskM = new ManagerTask(lRequestNum);
            try
            {
                oTaskM.RunHistory();
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message);
            }
        } 


        private void RunCalcBatchPremiyot(object param)
        {
            clUtils oUtils = new clUtils();
            clCalcDal oCalcDal = new clCalcDal();
            int result, iStatus = 0;
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            string path, exfile, sArguments;
            FileInfo KdsCalcul = null;
            var logger = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                logger.InsertLog(lRequestNum, "I", 0, "START");             
                int iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["PremiotProcessesNb"]);
                path = ConfigurationManager.AppSettings["MultiProcessesAppPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsCalculFileName"].ToString();
                KdsCalcul = new FileInfo(path + exfile);
                logger.InsertLog(lRequestNum, "I", 0, "KdsCalul will run from " + KdsCalcul.FullName);        
                result = oCalcDal.PrepareDataLeChishuvPremiyot(iCntProcesses);
                logger.InsertLog(lRequestNum, "I", 0, "Finish to prepoare the general data");
                if (result > 0)
                {
                    if (KdsCalcul.Exists)
                    {
                        sArguments = clGeneral.enCalcType.PremiotCalc.GetHashCode() + " " + lRequestNum.ToString();
                        iStatus = RunMultiProcesses(lRequestNum, KdsCalcul, sArguments, iCntProcesses);
                    }
                    else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                }
            }
            catch (Exception ex)
            {
                logger.InsertLog(lRequestNum, "I", 0, "Failed");
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                logger.InsertLog(lRequestNum, "E", 0, "RunCalcBatchPremiyot: " + ex.Message);
                throw ex;
            }
            finally
            {
                CheckProcessesTerminated(KdsCalcul, lRequestNum, iStatus);
            }
            //LogThreadEnd("CalcBatchParallel", lRequestNum);
        }
        //private void RunCalcBatchThread(object param)
        //{
        //    object[] args = param as object[];
        //    long lRequestNum = (long)args[0];
        //    DateTime dAdChodesh = (DateTime)args[1];
        //    string sMaamad = args[2].ToString();
        //    bool bRitzatTest = (bool)args[3];
        //    bool bRitzaGorefet = (bool)args[4];
        //    clCalculation objCalc = new clCalculation();

        //    try
        //    {
        //        if (bRitzatTest)
        //        {
        //            objCalc.MainCalc(lRequestNum, dAdChodesh, sMaamad, bRitzaGorefet, clCalculation.TypeCalc.Test);
        //        }
        //        else
        //        {
        //            objCalc.MainCalc(lRequestNum, dAdChodesh, sMaamad, bRitzaGorefet, clCalculation.TypeCalc.Batch);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogError(ex);
        //    }
        //    LogThreadEnd("CalcBatch", lRequestNum);
        //}

        private void RunTransferToHilanThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            long lRequestNumToTransfer = (long)args[1];

            clTransferToHilan objTrans = new clTransferToHilan();
            try
            {
                objTrans.Transfer(lRequestNum, lRequestNumToTransfer);

            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            LogThreadEnd("TransferToHilan", lRequestNum);

        }

        private void RunBdikatChufshaRezifaThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            int iUserId = (int)args[1];
            int iStatus = 0;
            clBatch oBatch = new clBatch();
            try
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "START");
                oBatch.BdikatChufshaRezifa(lRequestNum, iUserId);
                iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunBdikatChufshaRezifaThread: " + ex.Message);
                throw ex;
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, iStatus);
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "END");          
            }
            LogThreadEnd("BdikatChufshaRezifa", lRequestNum);
        }

        private void RunBdikatYemeyMachalaThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            int iUserId = (int)args[1];
            int iStatus = 0;
            clBatch oBatch = new clBatch();
            var logger = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                logger.InsertLog(lRequestNum, "I", 0, "START");
                oBatch.BdikatYemeyMachala(lRequestNum, iUserId);
                iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                logger.InsertLog(lRequestNum, "E", 0, "RunBdikatYemeyMachalaThread: " + ex.Message);
                throw ex;
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, iStatus);
                logger.InsertLog(lRequestNum, "I", 0, "END");
            }
            LogThreadEnd("RunBdikatYemeyMachalaThread", lRequestNum);
        }
        private void RunYeziratRikuzimThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            long iRequestIdForRikuzim = (long)args[1];
            KdsLibrary.BL.clReport _ClReport= new KdsLibrary.BL.clReport();
            string path, exfile, sArguments;
            FileInfo KdsRikuzims = null;
            int iStatus = 0;
            bool result = false;
            clBatch oBatch = new clBatch();
            try
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "START");
               
                int iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["RikuzimsProcessesNb"]);
                path = ConfigurationManager.AppSettings["MultiProcessesAppPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsRikuzimsFileName"].ToString();
                KdsRikuzims = new FileInfo(path + exfile);
                 ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "KdsRikuzims will run from " + KdsRikuzims.FullName);
               
                if (KdsRikuzims.Exists)
                {
                    oBatch.DeleteRikuzimPdf(iRequestIdForRikuzim);
                    result = _ClReport.GetProPrepareOvdimRikuzim(lRequestNum,iRequestIdForRikuzim, iCntProcesses);
                    sArguments = clGeneral.enCalcType.Rikuzim.GetHashCode() + " " + lRequestNum.ToString() + " " + iRequestIdForRikuzim.ToString();
                    iStatus = RunMultiProcesses(lRequestNum, KdsRikuzims, sArguments, iCntProcesses);
                }
                else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunYeziratRikuzimThread: " + ex.Message);

                throw ex;
            }
            finally
            {
                CheckProcessesTerminated(KdsRikuzims, lRequestNum, iStatus);
            }
            LogThreadEnd("YeziratRikuzim", lRequestNum);
        }

        private void RunTransferTekenNehagimThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            long lRequestNumToTransfer = (long)args[1];
            int iStatus;
            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "START");

            clBatch oBatch = new clBatch();
            try
            {
                oBatch.InsertTekenNehagimToTnua(lRequestNumToTransfer);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunTransferTekenNehagimThread: " + ex.Message);

            }
            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "End");
            LogThreadEnd("TransferTekenNehagim", lRequestNum);
            clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, clGeneral.enStatusRequest.ToBeEnded.GetHashCode());
           
        }

        private void RunShlichatRikuzimMailThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            long iRequestIdForRikuzim = (long)args[1];

            clManagerRikuzimMail objReport = new clManagerRikuzimMail(lRequestNum, iRequestIdForRikuzim);
            try
            {
                objReport.SendMails(lRequestNum);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            LogThreadEnd("TransferToHilan", lRequestNum);
        }
        private void RunCreateConstantsReports(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            string sMonth = (string)args[1];
            int iUserId = (int)args[2];

            clManagerReport objReport = new clManagerReport(lRequestNum, sMonth, iUserId);
            try
            {
                objReport.MakeReports(lRequestNum);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            LogThreadEnd("CreateConstantsReports", lRequestNum);

        }

        private void RunCreateHeavyReports(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            try
            {
                ClManageHeavyReport objReport = new ClManageHeavyReport(lRequestNum);
                objReport.MakeReports(lRequestNum);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            LogThreadEnd("CreateHeavyReports", lRequestNum);

        }


        private void RunRefreshMatzavOvdim(object param)
        {
            ClKds oKDs = new ClKds();
            try
            {
                oKDs.RunRefreshRetroMatzav();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }


        private void RunTahalichHrChanges(object param)
        {
            //  object[] args = param as object[];
            //  int iSeq = int.Parse(args[0].ToString());
            ClKds oKDs = new ClKds();
            try
            {
                oKDs.RunThreadHrChainges(param);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }
        private void RunTahalichSadran(object param)
        {
            object[] args = param as object[];
            //DateTime taarich = DateTime.Parse(args[0].ToString());
            string taarich = args[0].ToString();
            ClKds oKDs = new ClKds();
            try
            {
                oKDs.RunSdrn(taarich);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }


        private void RunRefreshMeafyenim(object param)
        {
            ClKds oKDs = new ClKds();
            try
            {
                oKDs.refrsh_meafyenim();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }


        private void RunRefreshPirteyOvdim(object param)
        {
            //  ClKds oKDs = new ClKds();
            clDal oDal = new clDal();
            clBatch obatch = new clBatch();
            int iSeqNum;
            try
            {
                // refresh table pirtey_ovdim
                iSeqNum = obatch.InsertProcessLog(3, 5, RecordStatus.Wait, "start refresh New_Pirtey_Ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 5, 1, "start refresh New_Pirtey_Ovdim", "");
                oDal.ClearCommand();
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_Pirtey_Ovdim", ParameterDir.pdInput);
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv");
                obatch.UpdateProcessLog(iSeqNum, RecordStatus.Finish, "end ok refresh New_Pirtey_Ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 5, 2, "end ok refresh New_Pirtey_Ovdim", "");

                // refresh pivot_pirtey_ovdim
                //     oDal.ExecuteSQL("truncate table tmp_pirtey_ovdim");
                iSeqNum = obatch.InsertProcessLog(3, 8, RecordStatus.Wait, "start refresh pivot_pirtey_ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 8, 1, "start refresh tmp_pirtey_ovdim", "");
                oDal.ClearCommand();
                oDal.ExecuteSP("Create_pivot_Pirtey_Ovdim");
                obatch.UpdateProcessLog(iSeqNum, RecordStatus.Finish, "end ok refresh pivot_pirtey_ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 8, 2, "end ok refresh tmp_pirtey_ovdim", "");
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }


        private void RunTkinutMakatimThread(object param)
        {
            object[] args = param as object[];
            DateTime taarich = DateTime.Parse(args[0].ToString());
            clTkinutMakatim objMakat = new clTkinutMakatim();
            try
            {
                objMakat.CheckTkinutMakatim(taarich);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            // LogThreadEnd("RunTkinutMakatimThread", lRequestNum);

        }

        private void LogThreadEnd(string threadName, long btchRequest)
        {
            string message = String.Format("{0} Thread ended at {1} for batch request no. {2}",
                threadName,
                DateTime.Now,
                btchRequest);
            KdsLibrary.clGeneral.LogMessage(message,
                System.Diagnostics.EventLogEntryType.Information);
        }

        private void LogThreadStart(string threadName, long btchRequest)
        {
            string message = String.Format("{0} Thread started at {1} for batch request no. {2}",
                threadName,
                DateTime.Now,
                btchRequest);
            KdsLibrary.clGeneral.LogMessage(message,
                System.Diagnostics.EventLogEntryType.Information);
        }


        #endregion

        #region IBatchService Members

        //public void ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource requestSource,
        //    KdsBatch.BatchExecutionType execType, DateTime workDate, long btchRequest)
        public void ExecuteInputDataAndErrors(int requestSource,
            int execType, DateTime workDate, long btchRequest)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunExecuteInputDataAndErrorsThread));
            string message = String.Format("ExecuteInputDataAndErrors Thread started at {0} for batch request no. {1}",
                DateTime.Now,
                btchRequest);
            LogThreadStart("ExecuteInputDataAndErrors", btchRequest);
            runThread.Start(new object[] { (clGeneral.BatchRequestSource)requestSource, 
                (clGeneral.BatchExecutionType)execType, 
                workDate, btchRequest });

        }

        public void CalcBatchParallel(long lRequestNum, DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunCalcBatchParallel));
            LogThreadStart("CalcBatchParallel", lRequestNum);
            runThread.Start(new object[] { lRequestNum, dAdChodesh, sMaamad, 
                bRitzatTest, bRitzaGorefet });
        }


        public void InsetRecordsToHistory(long lRequestNum)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunInsetRecordsToHistory));
            LogThreadStart("InsetRecordsToHistory", lRequestNum);
            runThread.Start(new object[] { lRequestNum });
        }
        public void CalcBatchPremiyot(long lRequestNum)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunCalcBatchPremiyot));
            LogThreadStart("CalcBatchPremiyot", lRequestNum);
            runThread.Start(new object[] { lRequestNum });
        }
        //public void CalcBatch(long lRequestNum, DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
        //{
        //    Thread runThread = new Thread(
        //        new ParameterizedThreadStart(RunCalcBatchThread));
        //    LogThreadStart("CalcBatch", lRequestNum);
        //    runThread.Start(new object[] { lRequestNum, dAdChodesh, sMaamad, 
        //        bRitzatTest, bRitzaGorefet });
        //}

        public void TransferToHilan(long lRequestNum, long lRequestNumToTransfer)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunTransferToHilanThread));
            LogThreadStart("TransferToHilan", lRequestNum);
            runThread.Start(new object[] { lRequestNum, lRequestNumToTransfer });
        }

        public void BdikatChufshaRezifa(long lRequestNum, int iUserId)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunBdikatChufshaRezifaThread));
            LogThreadStart("BdikatChufshaRezifa", lRequestNum);
            runThread.Start(new object[] { lRequestNum, iUserId });
        }

        public void BdikatYemeyMachala(long lRequestNum, int iUserId)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunBdikatYemeyMachalaThread));
            LogThreadStart("BdikatYemeyMachala", lRequestNum);
            runThread.Start(new object[] { lRequestNum, iUserId });
        }
        public void YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunYeziratRikuzimThread));
            LogThreadStart("YeziratRikuzim", lRequestNum);
            runThread.Start(new object[] { lRequestNum, iRequestIdForRikuzim });
        }

        public void TransferTekenNehagim(long lRequestNum, long iRequestIdForTransfer)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunTransferTekenNehagimThread));
            LogThreadStart("TransferTekenNehagim", lRequestNum);
            runThread.Start(new object[] { lRequestNum, iRequestIdForTransfer });
        }

        public void ShlichatRikuzimMail(long lRequestNum, long iRequestIdForRikuzim)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunShlichatRikuzimMailThread));
            LogThreadStart("ShlichatRikuzimMail", lRequestNum);
            runThread.Start(new object[] { lRequestNum, iRequestIdForRikuzim });
        }
        //public void TahalichHarazatShguimBatch(long lRequestNum, DateTime dTaarich, int TypeShguim, int ExecutionType)
        //{
        //    Thread runThread = new Thread(
        //        new ParameterizedThreadStart(RunShinuyimVeShguimBatch));
        //    LogThreadStart("TahalichHarazatShguimBatch", lRequestNum);
        //    runThread.Start(new object[] { lRequestNum, dTaarich, TypeShguim, ExecutionType });
        //}
        public void CreateConstantsReports(long lRequestNum, string sMonth, int iUserId)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunCreateConstantsReports));
            LogThreadStart("CreateConstantsReports", lRequestNum);
            runThread.Start(new object[] { lRequestNum, sMonth, iUserId });
        }

        public void CreateHeavyReports(long lRequestNum)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunCreateHeavyReports));
            LogThreadStart("CreateHeavyReports", lRequestNum);
            runThread.Start(new object[] { lRequestNum });
        }

        public string CreatePremiaInputFile(long btchRequest, DateTime period, int userId,
            long processBtchNumber)
        {
            string result = null;
            var premiaFile = new PremiaFileCreator(btchRequest, period, userId, processBtchNumber);
            if (!premiaFile.Execute())
                result = premiaFile.ErrorMessage;
            return result;
        }

        public string RunPremiaCalculation(DateTime period, int userId, long processBtchNumber)
        {
            string result = null;
            var premiaCalc = new PremiaCalculation(period, userId, processBtchNumber);
            if (!premiaCalc.Execute())
                result = premiaCalc.ErrorMessage;
            return result;
        }

        public string StorePremiaCalculationOutput(long btchRequest, DateTime period, int userId,
            long processBtchNumber)
        {
            string result = null;
            var premiaFile = new PremiaFileImporter(btchRequest, period, userId, processBtchNumber);
            if (!premiaFile.Execute())
                result = premiaFile.ErrorMessage;
            return result;
        }

        public void RefreshMatzavOvdim()
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunRefreshMatzavOvdim));
            runThread.Start(new object[] { });
        }

        public void TahalichHrChanges(int iSeq)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunTahalichHrChanges));
            // LogThreadStart("CreateHeavyReports", lRequestNum);
            runThread.Start(new object[] { iSeq });
        }

        public void TahalichSadran(string taarich)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunTahalichSadran));
            // LogThreadStart("CreateHeavyReports", lRequestNum);
            runThread.Start(new object[] { taarich });
        }


        public void RefreshMeafyenim()
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunRefreshMeafyenim));
            runThread.Start(new object[] { });
        }

        public void RefreshPirteyOvdim()
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunRefreshPirteyOvdim));
            runThread.Start(new object[] { });
        }

        public void ShinuyimVeShguimBatch(long lRequestNum, DateTime dTaarich, clGeneral.enCalcType TypeShguyim, clGeneral.BatchExecutionType ExecutionTypeShguim)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunShinuyimVeShguimBatch));
            LogThreadStart("ShinuyimVeShguimBatch", lRequestNum);
            runThread.Start(new object[] { lRequestNum, dTaarich, TypeShguyim, ExecutionTypeShguim });
        }

        public void TkinutMakatimBatch(DateTime dTaarich)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunTkinutMakatimThread));
            runThread.Start(new object[] { dTaarich });
        }

        //public void SleepUntillProccessEnd(long lRequestNumTahalic,long lRequestNum)
        //{            
        //    Thread runThread = new Thread(new ParameterizedThreadStart(RunSleepUntillProccessEnd));
        //    runThread.Start(new object[] { lRequestNumTahalic,lRequestNum });
        //}
        
        #endregion
    }
}

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
using KdsDataImport;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.IO;
namespace KdsService
{
    public class BatchService : IBatchService
    {
        #region Methods
        private void RunExecuteInputDataAndErrorsThread(object param)
        {
            object[] args = param as object[];
            KdsBatch.BatchRequestSource requestSource = (KdsBatch.BatchRequestSource)args[0];
            KdsBatch.BatchExecutionType execType = (KdsBatch.BatchExecutionType)args[1];
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

        private void RunSinuyimVeShguimBatch(object param)
        {
             clUtils oUtils = new clUtils();
            string sArguments = "";
            int iStatus = 0;
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            DateTime dTaarich = (DateTime)args[1];
            string path, exfile;
            FileInfo KdsCalcul = null;
            clGeneral.enCalcType TypeShguyim = ((clGeneral.enCalcType)Enum.Parse(typeof(clGeneral.enCalcType), args[2].ToString()));
            BatchExecutionType ExecutionTypeShguim = ((BatchExecutionType)Enum.Parse(typeof(BatchExecutionType), args[3].ToString()));
            try
            {
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "START");
                int iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["CntOfprocesses"]);
                path = ConfigurationManager.AppSettings["KdsCalculPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsCalculFileName"].ToString();
                KdsCalcul = new FileInfo(path + exfile);
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "KdsCalul will run from " + KdsCalcul.FullName);

                switch (TypeShguyim)
                {
                    case clGeneral.enCalcType.ShinuyimVeShguyim:
                        oUtils.PrepareNetunimToShguyimBatch(dTaarich, BatchRequestSource.ImportProcess.GetHashCode(), iCntProcesses);
                        break;
                    case clGeneral.enCalcType.ShinuyimVeSghuimHR:
                        oUtils.PrepareNetunimToShguyimBatchHR(BatchRequestSource.ImportProcessForChangesInHR.GetHashCode(),iCntProcesses);
                        break;
                }
                // oCalcDal.PrepareDataLeChishuv(dFrom, dAdChodesh, sMaamad, bRitzaGorefet, iCntProcesses);
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "Finish to prepoare the general data");
                if (KdsCalcul.Exists)
                {
                    sArguments = TypeShguyim.GetHashCode() + " " + lRequestNum.ToString() + " " + ExecutionTypeShguim.GetHashCode();
                    iStatus = RunKdsCalcul(lRequestNum, KdsCalcul, sArguments, iCntProcesses);
                    //  iStatus = RunKdsCalcul(KdsCalcul, lRequestNum, dFrom, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet, iCntProcesses);
                }
                else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();

            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clLogBakashot.InsertErrorToLog(lRequestNum, "E", 0, "RunSinuyimVeShguimBatch: " + ex.Message);
                throw ex;
            }
            finally
            {
                CheckKdsCalculTerminated(KdsCalcul, lRequestNum, iStatus);
            }
            //LogThreadEnd("CalcBatchParallel", lRequestNum);
        }

       //private int RunKdsCalcul(FileInfo FileToRun, long BakashaId, DateTime FromDate, DateTime ToDate, string Maamad, bool RitzaTest, bool RitzaGarefet, int CountOfProcesses)
        private int RunKdsCalcul(long BakashaId, FileInfo FileToRun, string sArguments, int CountOfProcesses)
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
                    clLogBakashot.InsertErrorToLog(BakashaId, "I", 0, "KdsCalul was run for " + i.ToString() + " time(s)");
                    _process.Dispose();
                }
                return clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                clLogBakashot.InsertErrorToLog(BakashaId, "E", 0, "RunKdsCalcul: " + ex.Message);
                return clGeneral.enStatusRequest.Failure.GetHashCode();
            }
        }

        private void CheckKdsCalculTerminated(FileInfo KdsCalcul, long BakashaID, int Status)
        {
            Process[] List;
            do
            {
                List = Process.GetProcessesByName(KdsCalcul.Name.Split('.')[0]);
                if (List.Count() == 0)
                {
                    clLogBakashot.InsertErrorToLog(BakashaID, "I", 0, "END");
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
            int result, iStatus = 0;
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            DateTime dAdChodesh = (DateTime)args[1];
            string sMaamad = args[2].ToString();
            string path, exfile;
            bool bRitzatTest = (bool)args[3];
            bool bRitzaGorefet = (bool)args[4];
            FileInfo KdsCalcul = null;
            try
            {
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "START");
                int iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["CntOfprocesses"]);
                path = ConfigurationManager.AppSettings["KdsCalculPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsCalculFileName"].ToString();
                KdsCalcul = new FileInfo(path + exfile);
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "KdsCalul will run from "+KdsCalcul.FullName );
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                dFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()) * -1);
                dAdChodesh = dAdChodesh.AddMonths(1).AddDays(-1);
                result = oCalcDal.PrepareDataLeChishuv(dFrom, dAdChodesh, sMaamad, bRitzaGorefet, iCntProcesses);
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "Finish to prepoare the general data");
                if (result > 0) 
                {
                    if (KdsCalcul.Exists)
                    {
                        sArguments = clGeneral.enCalcType.MonthlyCalc.GetHashCode() + " " + lRequestNum.ToString() + " " + dFrom.ToShortDateString() + " " + dAdChodesh.ToShortDateString() + " " +
                                                 sMaamad + " " + bRitzatTest.GetHashCode().ToString() + " " + bRitzaGorefet.GetHashCode().ToString();
                        iStatus = RunKdsCalcul(lRequestNum,KdsCalcul, sArguments, iCntProcesses);
                      //  iStatus = RunKdsCalcul(KdsCalcul, lRequestNum, dFrom, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet, iCntProcesses);
                    }
                    else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                }
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clLogBakashot.InsertErrorToLog(lRequestNum, "E", 0, "RunCalcBatchParallel: " + ex.Message);
                throw ex;
            }
            finally
            {
                CheckKdsCalculTerminated(KdsCalcul, lRequestNum, iStatus);
            }
            //LogThreadEnd("CalcBatchParallel", lRequestNum);
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
            try
            {
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "START");
                int iCntProcesses = int.Parse((string)ConfigurationManager.AppSettings["CntOfprocesses"]);
                path = ConfigurationManager.AppSettings["KdsCalculPath"].ToString();
                exfile = (string)ConfigurationManager.AppSettings["KdsCalculFileName"].ToString();
                KdsCalcul = new FileInfo(path + exfile);
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "KdsCalul will run from " + KdsCalcul.FullName);
                result = oCalcDal.PrepareDataLeChishuvPremiyot(iCntProcesses);
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "Finish to prepoare the general data");
                if (result > 0)
                {
                    if (KdsCalcul.Exists)
                    {
                        sArguments = clGeneral.enCalcType.PremiotCalc.GetHashCode() + " "+ lRequestNum.ToString();
                        iStatus = RunKdsCalcul(lRequestNum, KdsCalcul, sArguments, iCntProcesses);
                    }
                    else iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                }
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clLogBakashot.InsertErrorToLog(lRequestNum, "E", 0, "RunCalcBatchPremiyot: " + ex.Message);
                throw ex;
            }
            finally
            {
                CheckKdsCalculTerminated(KdsCalcul, lRequestNum, iStatus);
            }
            //LogThreadEnd("CalcBatchParallel", lRequestNum);
        }
        private void RunCalcBatchThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            DateTime dAdChodesh = (DateTime)args[1];
            string sMaamad = args[2].ToString();
            bool bRitzatTest = (bool)args[3];
            bool bRitzaGorefet = (bool)args[4];
            clCalculation objCalc = new clCalculation();

            try
            {
                if (bRitzatTest)
                {
                    objCalc.MainCalc(lRequestNum, dAdChodesh, sMaamad, bRitzaGorefet, clCalculation.TypeCalc.Test);
                }
                else
                {
                    objCalc.MainCalc(lRequestNum, dAdChodesh, sMaamad, bRitzaGorefet, clCalculation.TypeCalc.Batch);
                }
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
            LogThreadEnd("CalcBatch", lRequestNum);
        }

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

        private void RunYeziratRikuzimThread(object param)
        {
            object[] args = param as object[];
            long lRequestNum = (long)args[0];
            long iRequestIdForRikuzim = (long)args[1];

            clManagerRikuzim objReport = new clManagerRikuzim(lRequestNum, iRequestIdForRikuzim);
            try
            {
                objReport.MakeReports(lRequestNum);
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
                oDal.AddParameter("shem_mvew", KdsLibrary.DAL.ParameterType.ntOracleVarchar, "New_Pirtey_Ovdim", KdsLibrary.DAL.ParameterDir.pdInput);
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
            runThread.Start(new object[] { (KdsBatch.BatchRequestSource)requestSource, 
                (KdsBatch.BatchExecutionType)execType, 
                workDate, btchRequest });

        }

        public void CalcBatchParallel(long lRequestNum, DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunCalcBatchParallel));
            LogThreadStart("CalcBatchParallel", lRequestNum);
            runThread.Start(new object[] { lRequestNum, dAdChodesh, sMaamad, 
                bRitzatTest, bRitzaGorefet });
        }

        public void CalcBatchPremiyot(long lRequestNum)
        {
            Thread runThread = new Thread(new ParameterizedThreadStart(RunCalcBatchPremiyot));
            LogThreadStart("CalcBatchPremiyot", lRequestNum);
            runThread.Start(new object[] { lRequestNum });
        }
        public void CalcBatch(long lRequestNum, DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunCalcBatchThread));
            LogThreadStart("CalcBatch", lRequestNum);
            runThread.Start(new object[] { lRequestNum, dAdChodesh, sMaamad, 
                bRitzatTest, bRitzaGorefet });
        }

        public void TransferToHilan(long lRequestNum, long lRequestNumToTransfer)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunTransferToHilanThread));
            LogThreadStart("TransferToHilan", lRequestNum);
            runThread.Start(new object[] { lRequestNum, lRequestNumToTransfer });
        }

        public void YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunYeziratRikuzimThread));
            LogThreadStart("YeziratRikuzim", lRequestNum);
            runThread.Start(new object[] { lRequestNum, iRequestIdForRikuzim });
        }

        public void TahalichHarazatShguimBatch(long lRequestNum, DateTime dTaarich, int TypeShguim, int ExecutionType)
        {
            Thread runThread = new Thread(
                new ParameterizedThreadStart(RunSinuyimVeShguimBatch));
            LogThreadStart("TahalichHarazatShguimBatch", lRequestNum);
            runThread.Start(new object[] { lRequestNum, dTaarich, TypeShguim, ExecutionType });
        }
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

        #endregion
    }
}

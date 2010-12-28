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

        private void RunTahalichSadran(object param)
        {
            object[] args = param as object[];
            DateTime taarich = DateTime.Parse(args[0].ToString());
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
            try{
                // refresh table pirtey_ovdim
                iSeqNum = obatch.InsertProcessLog(3, 5, RecordStatus.Wait, "start refresh New_Pirtey_Ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 5, 1, "start refresh New_Pirtey_Ovdim", "");
                oDal.ClearCommand();
                oDal.AddParameter("shem_mvew", KdsLibrary.DAL.ParameterType.ntOracleVarchar, "New_Pirtey_Ovdim", KdsLibrary.DAL.ParameterDir.pdInput);
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv");
                obatch.UpdateProcessLog(iSeqNum, RecordStatus.Finish, "end ok refresh New_Pirtey_Ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 5, 2, "end ok refresh New_Pirtey_Ovdim", "");

                // refresh pivot_pirtey_ovdim
                oDal.ExecuteSQL("truncate table tmp_pirtey_ovdim");
                iSeqNum = obatch.InsertProcessLog(3, 8, RecordStatus.Wait, "start refresh tmp_pirtey_ovdim", 0);
                //**oKDs.KdsWriteProcessLog(3, 8, 1, "start refresh tmp_pirtey_ovdim", "");
                oDal.ClearCommand();
                oDal.ExecuteSP("Create_Cursor_Pirtey_Ovdim");
                obatch.UpdateProcessLog(iSeqNum, RecordStatus.Finish, "end ok refresh tmp_pirtey_ovdim", 0);
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
            runThread.Start(new object[] { lRequestNum});
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
            runThread.Start(new object[]{});
        }

        public void TahalichSadran(DateTime taarich)
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
            runThread.Start(new object[]{});
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

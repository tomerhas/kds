using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.BL;
using System.Configuration;
using System.Data;


namespace KdsBatch.TaskManager
{
    public class Utils
    {
        public void RunShguimOfSdrn()
        {
           // clBatch oBatch = new clBatch();
            clUtils oUtils = new clUtils();
            long lRequestNum = 0;
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "RunShguimOfSdrn", -12);
             //   oUtils.RunSinuyimVeShguimBatch(lRequestNum, DateTime.Now.AddDays(-1), clGeneral.enCalcType.ShinuyimVeShguyim, clGeneral.BatchExecutionType.All);
                KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcess, clGeneral.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);
                
            }
            catch (Exception ex)
            {
                throw new Exception("RunShguimOfSdrn:" + ex.Message);
            }
        }
        public void RunCalcPremiyotMusachim()
        {
            MainCalc oCalc;// = new MainCalc(
            clBatch oBatch = new clBatch();
            long lRequestNum = 0;
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.CalculationForPremiaPopulation, "RunCalcPremiyotMusachim", -12);
                oCalc = new MainCalc(lRequestNum, 1);
                oCalc.PremiaCalc();//KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcess, KdsBatch.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);
            }
            catch (Exception ex)
            {
                throw new Exception("RunCalcPremiyotMusachim:" + ex.Message);
            }
        }
        public void RunIshurimOfSdrn()
        {
            try
            {
                string Environment = ConfigurationSettings.AppSettings["Environment"];
                KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(DateTime.Now.AddDays(-1), (Environment == "Production"));
            }
            catch (Exception ex)
            {
                throw new Exception("RunIshurimOfSdrn :" + ex.Message);
            }
        }
        public void ShguimHrChanges()
        {
            int iSeqThreadHr, iSeqNum, num;
            DateTime dTaarich;
            clBatch oBatch = new clBatch();
            clUtils oUtils = new clUtils();
            try
            {
                iSeqThreadHr = oBatch.InsertProcessLog(8, 3, KdsLibrary.BL.RecordStatus.Wait, "start RunThreadHrChainges", 0);
                //(0,0)=no record at all ->run
                num = oBatch.GetNumChangesHrToShguim();
                if ((num < 50000))
                {
                    long lRequestNum = 0;
                    iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before OpenBatchRequest hr", 0);
                    //'**KdsWriteProcessLog(8, 3, 1, "before OpenBatchRequest")
                    lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12);
                    dTaarich = DateTime.Now.AddDays(-1);
                    oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after OpenBatchRequest hr", 0);
                    //'** KdsWriteProcessLog(8, 3, 1, "after OpenBatchRequest before shguyim")
                    iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before shguyim hr", 0);
                   // oUtils.RunSinuyimVeShguimBatch(lRequestNum, dTaarich, clGeneral.enCalcType.ShinuyimVeSghuimHR, clGeneral.BatchExecutionType.All);
                    KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcessForChangesInHR, clGeneral.BatchExecutionType.All, dTaarich, lRequestNum);
                    oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after shguyim from hr", 0);
                    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Finish, "end RunThreadHrChainges", 0);
                    //'**KdsWriteProcessLog(8, 3, 2, "after shguyim from hr")
                }
                else
                {
                    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.PartialFinish, "ThreadHrChainges did not run.a lot of mispar_ishi: " + num.ToString(), 0);
                    //'**  KdsWriteProcessLog(8, 3, 4, "ThreadHrChainges did not run.a lot of mispar_ishi: " & num.ToString())
                    throw new Exception("ThreadHrChainges didn't run because a lot of records (" + num.ToString() +" workers)");
                }
            }
            catch
            {
                throw;
            }
        }

        public void RunShguimLechishuv()
        {
            clBatchManager oBatchManager = new clBatchManager();
            DataTable dtOvdim = new DataTable();
            clCalcDal oCalcDal = new clCalcDal();
            clBatch oBatch = new clBatch();
            int lRequestNum = 0;
            int numFaild=0;
            int numSucceeded = 0;
            bool bInpuDataResult;
            try
            {
                lRequestNum = oBatch.InsertProcessLog(77, 0, KdsLibrary.BL.RecordStatus.Wait, "start RunShguimLechishuv", 0);
                dtOvdim = oCalcDal.GetOvdimLeRizatShguim();
                for (int i = 0; i <dtOvdim.Rows.Count; i++)
                {
                    try
                    {
                        bInpuDataResult = true;
                        bInpuDataResult = oBatchManager.MainInputData(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));

                        if (bInpuDataResult)
                        {
                            bInpuDataResult = oBatchManager.MainOvedErrors(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));
                            numSucceeded += 1;
                        }
                        else
                        {
                            numFaild += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        numFaild += 1;
                    }
                }
                oBatch.UpdateProcessLog(lRequestNum, KdsLibrary.BL.RecordStatus.Finish, "end RunShguimLechishuv: Total Rows=" + dtOvdim.Rows.Count + "; numFaild=" + numFaild + ";  numSucceeded=" + numSucceeded, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("RunShguimLechishuv:" + ex.Message);
            }
            finally {
               oBatchManager = null;
               dtOvdim =null;
               oCalcDal =null;
               oBatch =null;
            }
        }
    }
}

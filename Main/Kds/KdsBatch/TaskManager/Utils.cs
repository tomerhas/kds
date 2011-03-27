using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.BL;

namespace KdsBatch.TaskManager
{
    public class Utils
    {
        public void ShguimHrChanges()
        {
            int iSeqThreadHr, iSeqNum, num;
            DateTime dTaarich;
            clBatch oBatch = new clBatch();
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
                    KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcessForChangesInHR, KdsBatch.BatchExecutionType.All, dTaarich, lRequestNum);
                    oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after shguyim from hr", 0);
                    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Finish, "end RunThreadHrChainges", 0);
                    //'**KdsWriteProcessLog(8, 3, 2, "after shguyim from hr")
                }
                else
                {
                    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.PartialFinish, "ThreadHrChainges did not run.a lot of mispar_ishi: " + num.ToString(), 0);
                    //'**  KdsWriteProcessLog(8, 3, 4, "ThreadHrChainges did not run.a lot of mispar_ishi: " & num.ToString())
                }
            }
            catch
            {
                throw;
            }


        }
    }
}

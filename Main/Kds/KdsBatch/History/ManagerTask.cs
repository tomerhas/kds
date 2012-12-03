using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using KdsLibrary;
using KdsLibrary.BL;
namespace KdsBatch.History
{
    public class ManagerTask
    {
       private long _lRequestNum;
        public ManagerTask(long lRequestNum)
        {
           _lRequestNum = lRequestNum;
        }
        public ManagerTask()
        {
            //   _lRequestNum = lRequestNum;
        }

        public void Run()
        {
            BaseTask oTask;
            int iStatus = 0;
           // long _lRequestNum;
            clBatch objBatch = new clBatch();
            try
            {
             //  _lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.HasavatNetuniToOracle, "נתוני אורקל", -12);
                oTask = new KdsBatch.History.TaskDay(_lRequestNum, ';');
                 oTask.Run();
                 GC.Collect();
                 oTask = null;
                 oTask = new KdsBatch.History.TaskSidur(_lRequestNum, ';');
                 oTask.Run();
                 GC.Collect();
                 oTask = null;
                 oTask = new KdsBatch.History.TaskPeilut(_lRequestNum, ';');
                 oTask.Run();
                 GC.Collect();
                 oTask = null;

                 iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
            }
            catch (Exception ex)
            {
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clGeneral.LogError("History Error:  " + ex);
                clLogBakashot.InsertErrorToLog(_lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message);
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(_lRequestNum, DateTime.Now, iStatus);
            }
        }
    }
}

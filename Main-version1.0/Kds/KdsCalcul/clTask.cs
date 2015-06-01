using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsBatch.Reports;

namespace KdsCalcul
{
    public class clTask
    {
        public void RunRikuzim(long RequestNum, long RequestIdForRikuzim,int NumOfProcess)
        {
            clManagerRikuzim objReport = new clManagerRikuzim(RequestNum, RequestIdForRikuzim, NumOfProcess);
            try
            {
                objReport.MakeReports(RequestNum);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }

        }
    }
}

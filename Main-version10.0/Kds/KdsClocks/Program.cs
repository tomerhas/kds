using KDSCommon.Interfaces.Logs;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace KdsClocks
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock oClock= new Clock();
            long lRequestNum=0;
            int iStatus;
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.Clocks, "RunClocksHarmony", -12);
                var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
                logManager.InsertLog(lRequestNum, "I", 0, "start clock , time=" + DateTime.Now.ToString());

                oClock.InsertMovemetRecords();
                oClock.InsertMovemetErrRecords();

               // iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
                logManager.InsertLog(lRequestNum, "I", 0, "end clock , time=" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
               // iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunClocksHarmony Faild: " + ex.Message);
            }
        }

      
    }
}

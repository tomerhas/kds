using KDSCommon.Helpers;
using KDSCommon.Interfaces.Logs;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            Bootstrapper b = new Bootstrapper();
            b.Init();
            
          //  Clock oClock= new Clock();
            long lRequestNum=0;
            int iStatus;
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.Clocks, "RunClocksHarmony", -12);
                var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
                logManager.InsertLog(lRequestNum, "I", 0, "start clock , time=" + DateTime.Now.ToString());

                InsertMovemetRecords();
                InsertMovemetErrRecords();
             
               // iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
                logManager.InsertLog(lRequestNum, "I", 0, "end clock , time=" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
               // iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunClocksHarmony Faild: " + ex.Message);
            }
        }

        private static void InsertMovemetRecords()
        {
            COLL_HARMONY_MOVMENT_ERR_MOV collHarmony = new COLL_HARMONY_MOVMENT_ERR_MOV();
            try
            {
                var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();

                syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
                var xmlE = wsSy.SQLRecordSetToXML(ConfigurationManager.AppSettings["MOVMENTSQL"]);
                DataSet DsMovement = StaticBL.ConvertXMLToDataSet(xmlE);

                clockManager.InsertToCollMovment(collHarmony, DsMovement.Tables[int.Parse(ConfigurationManager.AppSettings["NUMTABLEMOVMENT"])]);
                clockManager.SaveShaonimMovment(collHarmony);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void InsertMovemetErrRecords()
        {
            COLL_HARMONY_MOVMENT_ERR_MOV collHarmony = new COLL_HARMONY_MOVMENT_ERR_MOV();
            try
            {
                var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();

                syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
                var xmlE = wsSy.SQLRecordSetToXML(ConfigurationManager.AppSettings["ERRMOVESQL"]);
                DataSet DsMovementErr = StaticBL.ConvertXMLToDataSet(xmlE);

                clockManager.InsertToCollErrMovment(collHarmony, DsMovementErr.Tables[int.Parse(ConfigurationManager.AppSettings["NUMTABLEERRMOVE"])]);
                clockManager.SaveShaonimMovment(collHarmony);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

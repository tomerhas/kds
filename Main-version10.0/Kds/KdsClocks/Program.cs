using KdsBatch;
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
            DateTime StartClock = DateTime.Now;
            string LastHour;
            int status=0;
            try
            {
                   lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.Clocks, "RunClocks", -12);
                   var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
                   logManager.InsertLog(lRequestNum, "I", 0, "start clock , time=" + DateTime.Now.ToString());

                   var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();
                   logManager.InsertLog(lRequestNum, "I", 0, "before select to tb_harmony_movment");
                   clockManager.InsertTnuotShaon();
                   logManager.InsertLog(lRequestNum, "I", 0, "after select to tb_harmony_movment, before attend");
                   RunAttendShaonim(lRequestNum);
                   logManager.InsertLog(lRequestNum, "I", 0, "after  attend");
                ////   StartClock = DateTime.Now;
                ////   var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();
                ////   status = clGeneral.enStatusRequest.InProcess.GetHashCode();
                ////   clockManager.InsertControlClockRecord(StartClock, status, "Start");
                ////   LastHour = clockManager.GetLastHourClock();

                ////   InsertMovemetRecords(lRequestNum, LastHour);            
                //////   InsertMovemetErrRecords(lRequestNum,LastHour);
                ////   clockManager.UpdateControlClockRecord(StartClock, clGeneral.enStatusRequest.ToBeEnded.GetHashCode(), "End");
                   status = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
                   logManager.InsertLog(lRequestNum, "I", 0, "end clock , time=" + DateTime.Now.ToString());
                /***************************************/ 
            }
            catch (Exception ex)
            {
                status = clGeneral.enStatusRequest.Failure.GetHashCode();
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunClocksHarmony Faild: " + ex.Message);
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, status);
                ServiceLocator.Current.GetInstance<IClockManager>().UpdateControlClockRecord(StartClock, status, "End");
            }
        }
        //private static void RunAttandHarmony()
        //{
        //    long lRequestNum = 0;
        //    int status = 0;
        //    try
        //    {
        //        lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.Attend, "RunAttendHarmony", -12);
        //        var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
        //        logManager.InsertLog(lRequestNum, "I", 0, "start Attend , time=" + DateTime.Now.ToString());
        //        status = clGeneral.enStatusRequest.InProcess.GetHashCode();

        //        MatchAttendHarmony(lRequestNum);

        //        status = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
        //        logManager.InsertLog(lRequestNum, "I", 0, "end Attend , time=" + DateTime.Now.ToString());
        //    }   
        //    catch (Exception ex)
        //    {
        //        status = clGeneral.enStatusRequest.Failure.GetHashCode();
        //        ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunAttandHarmony Faild: " + ex.Message);
        //    }
        //    finally
        //    {
        //        clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, status);
        //    }
        //}

        //private static void InsertMovemetRecords(long lRequestNum,string LastHour)
        //{
        //    COLL_HARMONY_MOVMENT_ERR_MOV collHarmony = new COLL_HARMONY_MOVMENT_ERR_MOV();
        //    try
        //    {
        //        var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();

        //        syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
        //        var xmlE = wsSy.SQLRecordSetToXML("select * from movment where Rec_Enter >='" + LastHour.Trim() + "'");//ConfigurationManager.AppSettings["MOVMENTSQL"]);
        //        DataSet DsMovement = StaticBL.ConvertXMLToDataSet(xmlE);

        //        clockManager.InsertToCollMovment(collHarmony, DsMovement.Tables[int.Parse(ConfigurationManager.AppSettings["NUMTABLEMOVMENT"])]);
        //        clockManager.SaveShaonimMovment(lRequestNum,collHarmony);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private static void InsertMovemetErrRecords(long lRequestNum, string LastHour)
        //{
        //    COLL_HARMONY_MOVMENT_ERR_MOV collHarmony = new COLL_HARMONY_MOVMENT_ERR_MOV();
        //    try
        //    {
        //        var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();

        //        syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
        //        var xmlE = wsSy.SQLRecordSetToXML("select * from Err_mov where Rec_Enter >='" + LastHour.Trim() + "'");//ConfigurationManager.AppSettings["MOVMENTSQL"]);
        //        DataSet DsMovementErr = StaticBL.ConvertXMLToDataSet(xmlE);

        //        clockManager.InsertToCollErrMovment(collHarmony, DsMovementErr.Tables[int.Parse(ConfigurationManager.AppSettings["NUMTABLEERRMOVE"])]);
        //        clockManager.SaveShaonimMovment(lRequestNum,collHarmony);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private static void RunAttendShaonim(long lRequestNum)
      {
        DataSet dsNetunim = new DataSet();
        DataSet ds;
        DateTime taarich, taarichCtl;
        IClockManager clockManager; 
        int  SugRec=0 ,status = 0,mispar_ishi=0,isite_kod=0,pmispar_sidur=0,p24,isuk;
        string inaction_kod, intbl_num, istm, inShaa, outShaa, iclock_num_in_site,knisaH, teur = "AttendHarmony now";
        try
        {

            clockManager = ServiceLocator.Current.GetInstance<IClockManager>();

            if (clockManager.getLastCntrlAttend() != 0)
            {
                dsNetunim = clockManager.GetNetunimToAttend();
                if (dsNetunim.Tables.Count > 0 && dsNetunim.Tables[0].Rows.Count > 0)
                {
                    taarichCtl = DateTime.Now;
                    clockManager.InsertControlAttendRecord(taarichCtl, status, teur);

                    foreach (DataRow dr in dsNetunim.Tables[0].Rows)
                    {
                        try
                        {
                            mispar_ishi = int.Parse(dr["MISPAR_ISHI"].ToString());
                            taarich = DateTime.Parse(dr["TAARICH"].ToString());
                            inShaa = dr["Shaa"].ToString().Split(' ')[1].Substring(0, 5).Replace(":", "");
                            pmispar_sidur = int.Parse(dr["MISPAR_SIDUR"].ToString());
                            isuk = int.Parse(dr["ISUK"].ToString());
                            if (isuk == 420 && pmispar_sidur == 99001)
                                pmispar_sidur = 99224;
                            if (isuk == 422 && pmispar_sidur == 99001)
                                pmispar_sidur = 99225;
                            //inaction_kod = dr["action_kod"].ToString();
                            //intbl_num = dr["tbl_num"].ToString();

                            //iclock_num_in_site = dr["clock_num_in_site"].ToString().Trim();
                            //if (iclock_num_in_site.ToString().Length < 2)
                            //    iclock_num_in_site = iclock_num_in_site.ToString().PadLeft(2, (char)48);

                            //isite_kod = int.Parse(dr["site_kod"].ToString() + iclock_num_in_site);
                            //istm = dr["rec_time_stmp"].ToString();
                            istm = null;

                            SugRec = int.Parse(dr["SugRec"].ToString());

                            if (taarich.Year > DateTime.Now.Year)
                                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "שנה לא תקינה", mispar_ishi, taarich);

                            switch (SugRec)
                            {
                                case 1:
                                    p24 = 0;
                                    ds = clockManager.GetKnisaIfExists(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "0")
                                        clockManager.InsertKnisatShaon(mispar_ishi, taarich, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                    break;
                                case 2:
                                    p24 = 0;
                                    ds = clockManager.GetKnisaIfExists(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "0")
                                    {
                                        ds = clockManager.GetYetziaNull(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            if (ds.Tables[0].Rows[0]["gmar"].ToString().Length == 12)
                                                outShaa = ds.Tables[0].Rows[0]["gmar"].ToString().Substring(8, 4);
                                            else
                                                outShaa = ds.Tables[0].Rows[0]["gmar"].ToString().Substring(0, 4);

                                            clockManager.UpdateKnisaRecord(mispar_ishi, taarich, inShaa, outShaa, isite_kod, pmispar_sidur, istm, p24);
                                        }
                                        else
                                            clockManager.InsertKnisatShaon(mispar_ishi, taarich, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                    }

                                    break;
                                case 3:
                                    p24 = 0;
                                    ds = clockManager.GetYetziaIfExists(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "0")
                                    {
                                        ds = clockManager.GetKnisaNull(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            if (ds.Tables[0].Rows[0]["knisa"].ToString().Length == 12)
                                                knisaH = ds.Tables[0].Rows[0]["knisa"].ToString().Substring(8, 4);
                                            else
                                                knisaH = ds.Tables[0].Rows[0]["knisa"].ToString().Substring(0, 4);

                                            clockManager.UpdateYeziaRecord(mispar_ishi, taarich, knisaH, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                        }
                                        else
                                            clockManager.InsertYeziatShaon(mispar_ishi, taarich, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                    }
                                    break;
                                case 4:
                                    if (int.Parse(inShaa) < 400)
                                        p24 = 1;
                                    else p24 = 0;

                                    ds = clockManager.GetKnisaIfExists(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "0")
                                    {
                                        ds = clockManager.GetYetziaNull(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            if (ds.Tables[0].Rows[0]["gmar"].ToString().Length == 12)
                                                outShaa = ds.Tables[0].Rows[0]["gmar"].ToString().Substring(8, 4);
                                            else
                                                outShaa = ds.Tables[0].Rows[0]["gmar"].ToString().Substring(0, 4);

                                            clockManager.UpdateKnisaRecord(mispar_ishi, taarich, inShaa, outShaa, isite_kod, pmispar_sidur, istm, p24);
                                        }
                                        else
                                            clockManager.InsertKnisatShaon(mispar_ishi, taarich, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                    }
                                    break;
                                case 5:
                                    if (int.Parse(inShaa) < 400)
                                        p24 = 1;
                                    else p24 = 0;

                                    ds = clockManager.GetYetziaIfExists(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "0")
                                    {
                                        ds = clockManager.GetKnisaNull(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        {
                                            if (ds.Tables[0].Rows[0]["knisa"].ToString().Length == 12)
                                                knisaH = ds.Tables[0].Rows[0]["knisa"].ToString().Substring(8, 4);
                                            else
                                                knisaH = ds.Tables[0].Rows[0]["knisa"].ToString().Substring(0, 4);

                                            clockManager.UpdateYeziaRecord(mispar_ishi, taarich, knisaH, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                        }
                                        else
                                            clockManager.InsertYeziatShaon(mispar_ishi, taarich, inShaa, isite_kod, pmispar_sidur, istm, p24);
                                    }

                                    break;
                                default:
                                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "לא קיים קוד תנועת שעון", mispar_ishi, taarich);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, " בעיה בקליטת רשומה ", ex);
                        }
                    }
                    teur = "AttendHarmony finished";
                    clockManager.UpdateControlAttendRecord(taarichCtl, 2, teur);
                }
            }
            else
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "לא בוצעו הצמדות, ריצה קודמת לא סיימה");
            }

        }

        catch (Exception ex)
        {
            status = 3;
            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "E", 0, "RunAttandShaonim Faild: " + ex.Message);
        }
       

      }

    }
}

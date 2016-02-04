using KdsBatch;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces;
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
                   clockManager.InsertTnuotShaon(lRequestNum);
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
               // ServiceLocator.Current.GetInstance<IClockManager>().UpdateControlClockRecord(StartClock, status, "End");
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
        DateTime taarich=new DateTime(), taarichCtl;
        IClockManager clockManager; 
        ILogBakashot log;
        int  SugRec=0 ,status = 1,mispar_ishi=0,clockId=0,pmispar_sidur=0,p24,isuk,meafyen43;
        string  istm, inShaa, outShaa,knisaH, teur = "AttendHarmony now";
        try
        {

            clockManager = ServiceLocator.Current.GetInstance<IClockManager>();
            log = ServiceLocator.Current.GetInstance<ILogBakashot>();
            if (clockManager.getLastCntrlAttend() != 1)
            {
                dsNetunim = clockManager.GetNetunimToAttend();
                if (dsNetunim.Tables.Count > 0 && dsNetunim.Tables[0].Rows.Count > 0)
                {
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "כמות רשומות להצמדה: " + dsNetunim.Tables[0].Rows.Count);
                    taarichCtl = DateTime.Now;
                    clockManager.InsertControlAttendRecord(taarichCtl, status, teur);

               //     var cacheManager = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
               //     var pundakim = cacheManager.GetCacheItem<DataTable>(CachedItems.Pundakim);

                    foreach (DataRow dr in dsNetunim.Tables[0].Rows)
                    {
                        try
                        {
                            mispar_ishi = int.Parse(dr["MISPAR_ISHI"].ToString());
                            taarich = DateTime.Parse(dr["TAARICH"].ToString());
                            inShaa = dr["Shaa"].ToString().Split(' ')[1].Substring(0, 5).Replace(":", "");
                            pmispar_sidur = int.Parse(dr["MISPAR_SIDUR"].ToString());
                            meafyen43 = int.Parse(dr["meafyen43"].ToString());
                            if(dr["ISUK"].ToString().Trim() != "")
                            {
                                isuk = int.Parse(dr["ISUK"].ToString());
                                if (isuk == 420 && pmispar_sidur == 99001)
                                    pmispar_sidur = 99224;
                                if (isuk == 422 && pmispar_sidur == 99001)
                                    pmispar_sidur = 99225;

                                if (dr["code_shaon"].ToString().Trim() != "")
                                {
                                    clockId = int.Parse(dr["code_shaon"].ToString());
                                    var shaon=clockId.ToString().PadLeft(5,'0').Substring(0,3);
                                //    var p = pundakim.Select("MIKUM_PUNDAK=" + shaon + " and Convert('" + taarich.ToShortDateString() + "', 'System.DateTime') >= ME_TAARICH and Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')<= AD_TAARICH");
                                 //   if (p.Length > 0)
                                  //      IsPundak = true;
                                }
                                istm = null;

                                SugRec = int.Parse(dr["SugRec"].ToString());
                                if (taarich.Year > DateTime.Now.Year)
                                    log.InsertLog(lRequestNum, "E", 0, "שנה לא תקינה", mispar_ishi, taarich);
                                else
                                {
                                        switch (SugRec)
                                        {
                                            case 1:
                                                p24 = 0;
                                                ds = clockManager.GetKnisaIfExists(mispar_ishi, taarich, inShaa, pmispar_sidur, p24);
                                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0].ToString() == "0")
                                                    clockManager.InsertKnisatShaon(mispar_ishi, taarich, inShaa, clockId, pmispar_sidur, istm, p24);
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

                                                        clockManager.UpdateKnisaRecord(mispar_ishi, taarich, inShaa, outShaa, clockId, pmispar_sidur, istm, p24);
                                                    }
                                                    else
                                                        clockManager.InsertKnisatShaon(mispar_ishi, taarich, inShaa, clockId, pmispar_sidur, istm, p24);
                                                }

                                                break;
                                            case 3:
                                                p24 = 0;
                                                var val= int.Parse(inShaa);
                                             
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

                                                        clockManager.UpdateYeziaRecord(mispar_ishi, taarich, knisaH, inShaa, clockId, pmispar_sidur, istm, p24);
                                                    }
                                                    else
                                                    {
                                                        if (val < 401 || (val <801 && meafyen43==1))
                                                        {
                                                            p24 = 1;
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

                                                                     clockManager.UpdateYeziaRecord(mispar_ishi, taarich, knisaH, inShaa, clockId, pmispar_sidur, istm, p24);
                                                                 }
                                                                 else
                                                                 {
                                                                     p24 = 0;
                                                                     clockManager.InsertYeziatShaon(mispar_ishi, taarich, inShaa, clockId, pmispar_sidur, istm, p24);
                                                                 }
                                                             }
                                                        }
                                                        else clockManager.InsertYeziatShaon(mispar_ishi, taarich, inShaa, clockId, pmispar_sidur, istm, p24);
                                                    }
                                                    
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

                                                        clockManager.UpdateKnisaRecord(mispar_ishi, taarich, inShaa, outShaa, clockId, pmispar_sidur, istm, p24);
                                                    }
                                                    else
                                                        clockManager.InsertKnisatShaon(mispar_ishi, taarich, inShaa, clockId, pmispar_sidur, istm, p24);
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

                                                        clockManager.UpdateYeziaRecord(mispar_ishi, taarich, knisaH, inShaa, clockId, pmispar_sidur, istm, p24);
                                                    }
                                                    else
                                                        clockManager.InsertYeziatShaon(mispar_ishi, taarich, inShaa, clockId, pmispar_sidur, istm, p24);
                                                }

                                                break;
                                            case 6:
                                                clockManager.InsertHityazvutPundak(mispar_ishi, taarich, DateTime.Parse(dr["Shaa"].ToString()), clockId);
                                                break;
                                            default:
                                                log.InsertLog(lRequestNum, "E", 0, "לא קיים קוד תנועת שעון", mispar_ishi, taarich);
                                                break;
                                        }
                                }
                            }
                            else log.InsertLog(lRequestNum, "E", 0, " לא קיים עיסוק ", mispar_ishi, taarich);
                        }
                        catch (Exception ex)
                        {
                            log.InsertLog(lRequestNum, "E", 0, " בעיה בקליטת רשומה ", mispar_ishi, taarich, ex);
                        }
                    }
                    teur = "AttendHarmony finished";
                    clockManager.UpdateControlAttendRecord(taarichCtl, 2, teur);
                }
                else
                {
                    log.InsertLog(lRequestNum, "I", 0, "לא קיימים נתונים להצמדות");
                }
            }
            else
            {
                log.InsertLog(lRequestNum, "I", 0, "עודכנו רשומות בטבלה TB_HARMONY_MOVMENT לא בוצעו הצמדות. ריצה קודמת לא סיימה");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.DAL;
using System.IO;
using System.Configuration;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary.Utils;

namespace KdsBatch
{
    public class clTkinutMakatim
    {
        public void CheckTkinutMakatim(DateTime dTaarich)
        {
            StreamWriter oFileMakat=null;
            DataTable dtMakatim = new DataTable();
            DataTable dtKavim = new DataTable();
            clKavim oKavim = new clKavim();
            clBatch oBatch = new clBatch();
            int numFaild = 0, numSucceeded = 0, iMakatValid, invalidMakat=0;
            clKavim.enMakatType oMakatType;
            long lMakatNesia,lRequestNum = 0;
            DateTime dCardDate;
            string Line;
            string sPathFile = ConfigurationSettings.AppSettings["PathFilePrintReports"] + "Tkinut_Makatim.txt";
            string sPathFileMail = ConfigurationSettings.AppSettings["PathFileReportsTemp"] + "Tkinut_Makatim.txt";
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromUI, "CheckTkinutMakatim", -12);

                dtMakatim = oKavim.GetMakatimLeTkinut(dTaarich);

                oFileMakat = new StreamWriter(sPathFile , false, Encoding.Default);
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "count= " + dtMakatim.Rows.Count);
               
                for (int i = 0; i < dtMakatim.Rows.Count; i++)
                {
                    try
                    {
                        lMakatNesia = long.Parse(dtMakatim.Rows[i]["MAKAT_NESIA"].ToString());
                        dCardDate = DateTime.Parse(dtMakatim.Rows[i]["TAARICH"].ToString());
                        oMakatType = (clKavim.enMakatType)oKavim.GetMakatType(lMakatNesia);
                        switch (oMakatType)
                        {
                            case clKavim.enMakatType.mKavShirut:
                                 dtKavim = oKavim.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);    
                                break;
                            case clKavim.enMakatType.mEmpty:
                                 dtKavim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);         
                                break;
                            case clKavim.enMakatType.mNamak:
                                 dtKavim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                                break;
                            default: iMakatValid = 1;
                                break;
                         }
                        if (iMakatValid == 1) //שגוי
                        {
                            Line = lMakatNesia + " ,  " + dCardDate.ToShortDateString();
                            oFileMakat.WriteLine(Line);
                            oFileMakat.Flush();
                            invalidMakat++;
                            clLogBakashot.InsertErrorToLog(lRequestNum, 0, "E", 0, null, "invalid makat:" + Line);
                        }
                        if (i % 1000 == 0)
                            clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "Tkinut Makatim: Num=" + i);
                                 
                        numSucceeded += 1;      
                    }
                    catch (Exception ex)
                    {
                        numFaild += 1;
                    }
                }
                if (!(oFileMakat == null))
                {
                    oFileMakat.Close();
                }
                SendMail(sPathFileMail, dTaarich.ToString("MM/yyyy"), "meravn@Egged.co.il");
                SendMail(sPathFileMail, dTaarich.ToString("MM/yyyy"), "RutyBe@Egged.co.il");
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "end CheckTkinutMakatim: Total Makats=" + dtMakatim.Rows.Count + "; numFaildException=" + numFaild + "; InvalidMakat=" + invalidMakat + ";  numSucceeded=" + numSucceeded);
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Finish.GetHashCode());
            }
            catch (Exception ex)
            {
                if (!(oFileMakat == null))
                {
                    oFileMakat.Close();
                }
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "CheckTkinutMakatim:" + ex.Message);
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Faild.GetHashCode());
            } 
        }

        protected void SendMail(string Path,string sMonth,string sTo)
        {
            try
            {
                // ReportMail rpt = new ReportMail();
                //string body = "";// rpt.GetMessageBody(Path);
                clMail email = new clMail(sTo, " קובץ מקטים שגויים מהתנועה לחודש " + sMonth, "");
                email.attachFile(Path);
                email.IsHtmlBody(true);
                email.SendMail();
                email.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RunRefreshKnisot(DateTime dTaarich)
        {
            DataTable dtMakatim = new DataTable();
            clBatch oBatch = new clBatch();
            DataSet dsKavim = new DataSet();
            OBJ_PEILUT_OVDIM oObjPeilutOvdim;
            COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdim = new COLL_OBJ_PEILUT_OVDIM();
            DataRow PirteyKav;
         //   DateTime dCardDate;
            int iResult = 0,num;
            clKavim oKavim = new clKavim();
            long lMakatNesia, lRequestNum=0;
            int numFaild = 0;
            int numFaildEx = 0;
            int numSucceeded = 0;
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.RifreshKnisot, "RunRefreshKnisot", -12);
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "Taarich= " + dTaarich.ToShortDateString());
                dtMakatim = oBatch.GetMakatimToRefresh(dTaarich);
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "count makat av= " + dtMakatim.Rows.Count);

                for (int i = 0; i < dtMakatim.Rows.Count; i++)
                {
                    try
                    {
                        lMakatNesia = long.Parse(dtMakatim.Rows[i]["MAKAT_NESIA"].ToString());
                        num = int.Parse(dtMakatim.Rows[i]["num"].ToString()); 
                        if (num == 1)
                            dsKavim = oKavim.GetKavimDetailsFromTnuaDS(lMakatNesia, dTaarich, out iResult, 1);
                        if (iResult == 0)
                        {
                            if (dsKavim.Tables[0].Rows.Count > 0)
                            {
                                PirteyKav = dsKavim.Tables[0].Rows[0];
                                foreach (DataRow dr in dsKavim.Tables[1].Rows)
                                {
                                    try
                                    {
                                        oObjPeilutOvdim = new OBJ_PEILUT_OVDIM();

                                        oObjPeilutOvdim.MISPAR_ISHI = int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString());
                                        oObjPeilutOvdim.TAARICH = DateTime.Parse(dtMakatim.Rows[i]["TAARICH"].ToString());
                                        oObjPeilutOvdim.MISPAR_SIDUR = int.Parse(dtMakatim.Rows[i]["MISPAR_SIDUR"].ToString());
                                        oObjPeilutOvdim.SHAT_HATCHALA_SIDUR = DateTime.Parse(dtMakatim.Rows[i]["SHAT_HATCHALA_SIDUR"].ToString());
                                        oObjPeilutOvdim.SHAT_YETZIA = DateTime.Parse(dtMakatim.Rows[i]["SHAT_YETZIA"].ToString());
                                        oObjPeilutOvdim.MISPAR_KNISA = int.Parse(dr["SIDURI"].ToString());
                                        oObjPeilutOvdim.MAKAT_NESIA = (long)int.Parse(PirteyKav["MAKAT8"].ToString());
                                        oObjPeilutOvdim.OTO_NO = int.Parse(dtMakatim.Rows[i]["OTO_NO"].ToString());
                                        oObjPeilutOvdim.MISPAR_SIDURI_OTO = int.Parse(dtMakatim.Rows[i]["MISPAR_SIDURI_OTO"].ToString());
                                        if (dtMakatim.Rows[i]["SNIF_TNUA"].ToString() != "")
                                            oObjPeilutOvdim.SNIF_TNUA = int.Parse(dtMakatim.Rows[i]["SNIF_TNUA"].ToString());
                                        oObjPeilutOvdim.SUG_KNISA = int.Parse(dr["SUGKNISA"].ToString());
                                        oObjPeilutOvdim.TEUR_NESIA = dr["TEUR_KNISA"].ToString();

                                        oCollPeilutOvdim.Add(oObjPeilutOvdim);
                                    }
                                    catch (Exception ex)
                                    {
                                        numFaildEx += 1;
                                        clLogBakashot.InsertErrorToLog(lRequestNum, int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString()), "E", 0, null, "RunRefreshKnisot:" + ex.Message);
                                    }
                                }
                                numSucceeded += 1;
                            }
                            else
                            {
                                numFaild += 1;
                                clLogBakashot.InsertErrorToLog(lRequestNum, int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString()), "E", 0, null, "RunRefreshKnisot: dsKavim.Tables[0].Rows.Count=0");                                 
                            }

                            if (i % 100 == 0)
                                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "Refresh Knisot: Num=" + i);
                        }
                        else
                        {
                            numFaild += 1;
                            clLogBakashot.InsertErrorToLog(lRequestNum, int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString()), "E", 0, null, "RunRefreshKnisot: iResult=1");  
                        }
                    }
                    catch (Exception ex)
                    {
                        numFaildEx += 1;
                        clLogBakashot.InsertErrorToLog(lRequestNum, 0, "E", 0, null, "RunRefreshKnisot er: " + ex.Message);
                    }
                }
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "Num Knisot to insert=" + oCollPeilutOvdim.Count);
                oBatch.InsertKnisot(oCollPeilutOvdim);
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "End RunRefreshKnisot");
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Finish.GetHashCode());  
           }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "E", 0, null, "RunRefreshKnisot Faild:" + ex.Message);
                oBatch.UpdateProcessLog(int.Parse(lRequestNum.ToString()), KdsLibrary.BL.RecordStatus.Faild, "RunRefreshKnisot Faild: "+ ex.Message, 0);
              //  throw new Exception("RunRefreshKnisot:" + ex.Message);
            }
            finally
            {
                oBatch = null;
            }
        }
    }
}

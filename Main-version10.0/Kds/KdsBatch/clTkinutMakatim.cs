using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using System.IO;
using System.Configuration;
using KdsLibrary.BL;
using KdsLibrary.Utils;
using KDSCommon.Helpers;
using KDSCommon.Enums;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Logs;
using System.Net.Mail;
using KDSCommon.DataModels.Mails;

namespace KdsBatch
{
    public class clTkinutMakatim
    {
        public void CheckTkinutMakatim(DateTime dTaarich)
        {
            StreamWriter oFileMakat=null;
            DataTable dtMakatim = new DataTable();
            DataTable dtKavim = new DataTable();
            clBatch oBatch = new clBatch();
            int numFaild = 0, numSucceeded = 0, iMakatValid, invalidMakat=0;
            enMakatType oMakatType;
            long lMakatNesia,lRequestNum = 0;
            DateTime dCardDate;
            string Line;
            string sPathFile = ConfigurationSettings.AppSettings["PathFilePrintReports"] + "Tkinut_Makatim.txt";
            string sPathFileMail = ConfigurationSettings.AppSettings["PathFileReportsTemp"] + "Tkinut_Makatim.txt";
            var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromUI, "CheckTkinutMakatim", -12);
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                dtMakatim = kavimDal.GetMakatimLeTkinut(dTaarich);

                oFileMakat = new StreamWriter(sPathFile , false, Encoding.Default);
                logManager.InsertLog(lRequestNum, "I", 0, "count= " + dtMakatim.Rows.Count, 0, null);
               
                for (int i = 0; i < dtMakatim.Rows.Count; i++)
                {
                    try
                    {
                        lMakatNesia = long.Parse(dtMakatim.Rows[i]["MAKAT_NESIA"].ToString());
                        dCardDate = DateTime.Parse(dtMakatim.Rows[i]["TAARICH"].ToString());
                        oMakatType = (enMakatType)StaticBL.GetMakatType(lMakatNesia);
                        switch (oMakatType)
                        {
                            case enMakatType.mKavShirut:
                                dtKavim = kavimDal.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);    
                                break;
                            case enMakatType.mEmpty:
                                dtKavim = kavimDal.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);         
                                break;
                            case enMakatType.mNamak:
                                dtKavim = kavimDal.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
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
                            logManager.InsertLog(lRequestNum,"E", 0,  "invalid makat:" + Line);
                        }
                        if (i % 1000 == 0)
                            logManager.InsertLog(lRequestNum, "I", 0,  "Tkinut Makatim: Num=" + i);
                                 
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
                logManager.InsertLog(lRequestNum, "I", 0, "end CheckTkinutMakatim: Total Makats=" + dtMakatim.Rows.Count + "; numFaildException=" + numFaild + "; InvalidMakat=" + invalidMakat + ";  numSucceeded=" + numSucceeded, 0, null);
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Finish.GetHashCode());
            }
            catch (Exception ex)
            {
                if (!(oFileMakat == null))
                {
                    oFileMakat.Close();
                }
                logManager.InsertLog(lRequestNum,  "I", 0,  "CheckTkinutMakatim:" + ex.Message,0,null);
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Faild.GetHashCode());
            } 
        }

        protected void SendMail(string Path,string sMonth,string sTo)
        {
            try
            {
               
                var mailManager = ServiceLocator.Current.GetInstance<IMailManager>();

                MailMessageWrapper message = new MailMessageWrapper(sTo) { Subject = " קובץ מקטים שגויים מהתנועה לחודש " + sMonth };
                message.Attachments.Add(new Attachment(Path));

                mailManager.SendMessage(message);
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
            long lMakatNesia, lRequestNum=0;
            int numFaild = 0;
            int numFaildEx = 0;
            int numSucceeded = 0;
            var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.RifreshKnisot, "RunRefreshKnisot", -12);
                logManager.InsertLog(lRequestNum, "I", 0,  "Taarich= " + dTaarich.ToShortDateString(),0,null);
                dtMakatim = oBatch.GetMakatimToRefresh(dTaarich);
                logManager.InsertLog(lRequestNum,  "I", 0,  "count makat av= " + dtMakatim.Rows.Count,0,null);

                for (int i = 0; i < dtMakatim.Rows.Count; i++)
                {
                    try
                    {
                        lMakatNesia = long.Parse(dtMakatim.Rows[i]["MAKAT_NESIA"].ToString());
                        num = int.Parse(dtMakatim.Rows[i]["num"].ToString());
                        if (num == 1)
                        {
                            var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                            dsKavim = kavimDal.GetKavimDetailsFromTnuaDS(lMakatNesia, dTaarich, out iResult, 1);
                        }
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
                                        oObjPeilutOvdim.LICENSE_NUMBER = int.Parse(dtMakatim.Rows[i]["LICENSE_NUMBER"].ToString());
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
                                        logManager.InsertLog(lRequestNum,  "E", 0,  "RunRefreshKnisot:" + ex.Message, int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString()),null);
                                    }
                                }
                                numSucceeded += 1;
                            }
                            else
                            {
                                numFaild += 1;
                                logManager.InsertLog(lRequestNum,  "E", 0,  "RunRefreshKnisot: dsKavim.Tables[0].Rows.Count=0", int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString()),null);                                 
                            }

                            if (i % 100 == 0)
                                logManager.InsertLog(lRequestNum,  "I", 0,  "Refresh Knisot: Num=" + i);
                        }
                        else
                        {
                            numFaild += 1;
                            logManager.InsertLog(lRequestNum, "E", 0,  "RunRefreshKnisot: iResult=1" , int.Parse(dtMakatim.Rows[i]["MISPAR_ISHI"].ToString()),null);  
                        }
                    }
                    catch (Exception ex)
                    {
                        numFaildEx += 1;
                        logManager.InsertLog(lRequestNum, "E", 0,  "RunRefreshKnisot er: " + ex.Message);
                    }
                }
                logManager.InsertLog(lRequestNum,  "I", 0,  "Num Knisot to insert=" + oCollPeilutOvdim.Count);
                oBatch.InsertKnisot(oCollPeilutOvdim);
                logManager.InsertLog(lRequestNum,  "I", 0,  "End RunRefreshKnisot");
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Finish.GetHashCode());  
           }
            catch (Exception ex)
            {
              //++  logManager.InsertLog(lRequestNum,  "E", 0,  "RunRefreshKnisot Faild:" + ex.Message);
              //++  oBatch.UpdateProcessLog(int.Parse(lRequestNum.ToString()), KdsLibrary.BL.RecordStatus.Faild, "RunRefreshKnisot Faild: "+ ex.Message, 0);
              //  throw new Exception("RunRefreshKnisot:" + ex.Message);
                //var excep = clLogBakashot.SetError(lRequestNum, "E", 0, null, ex);//--
                //throw (excep);//--
                var excep = ServiceLocator.Current.GetInstance<ILogBakashot>().SetError(255, 75757, "E", 0, DateTime.Now, "", ex);
                throw excep;
            }
            finally
            {
                oBatch = null;
            }
        }
    }
}

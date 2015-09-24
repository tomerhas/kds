using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSBLLogic.Managers
{
    public class ClockManager : IClockManager
    {
        private IUnityContainer _container;
        public ClockManager(IUnityContainer container)
        {
            _container = container;
        }

        public void InsertControlClockRecord(DateTime taarich, int status, string teur)
        {
            _container.Resolve<IClockDAL>().InsertControlClockRecord(taarich, status, teur);
        }

        public void UpdateControlClockRecord(DateTime taarich, int status, string teur)
        {
            _container.Resolve<IClockDAL>().UpdateControlClockRecord(taarich, status, teur);
        }

        public string GetLastHourClock()
        {
            return _container.Resolve<IClockDAL>().GetLastHourClock();
        }

        public void SaveShaonimMovment(long BakashaId,COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment)
        {
            _container.Resolve<IClockDAL>().SaveHarmonyMovment(BakashaId,collHarmonyMovment);
        }

        public void InsertToCollMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt)
        {
            OBJ_Harmony_movment_Err_Mov objHarmony;
            try
            {
                foreach (DataRow drHarmony in dt.Rows)
                {
                    if (drHarmony["BADGE"].ToString() != "")
                    {
                        objHarmony = new OBJ_Harmony_movment_Err_Mov();


                        objHarmony.EMP_NO = int.Parse(drHarmony["EMP_NO"].ToString());
                        objHarmony.TAARICH = DateTime.Parse(drHarmony["DATE"].ToString());
                        objHarmony.HOUR = DateTime.Parse(drHarmony["TIME"].ToString());

                        if (drHarmony.Table.Columns.Contains("CODE"))
                            objHarmony.CODE = drHarmony["CODE"].ToString();
                        if (drHarmony.Table.Columns.Contains("OPER"))
                            objHarmony.OPER = drHarmony["OPER"].ToString();
                        if (drHarmony.Table.Columns.Contains("STATION"))
                            objHarmony.STATION = drHarmony["STATION"].ToString();
                        if (drHarmony.Table.Columns.Contains("STATION_W"))
                            objHarmony.STATION_W = drHarmony["STATION_W"].ToString();
                        if (drHarmony.Table.Columns.Contains("MACHINE"))
                            objHarmony.MACHINE = drHarmony["MACHINE"].ToString();
                        if (drHarmony.Table.Columns.Contains("MACHINE_W"))
                            objHarmony.MACHINE_W = drHarmony["MACHINE_W"].ToString();
                        if (drHarmony.Table.Columns.Contains("ACTION"))
                            objHarmony.ACTION = drHarmony["ACTION"].ToString();
                        if (drHarmony.Table.Columns.Contains("XLEVEL"))
                            objHarmony.XLEVEL = drHarmony["XLEVEL"].ToString();
                        if (drHarmony.Table.Columns.Contains("ALTERNATIVE"))
                            objHarmony.ALTERNATIVE = drHarmony["ALTERNATIVE"].ToString();
                        if (drHarmony.Table.Columns.Contains("PRODUCT"))
                            objHarmony.PRODUCT = drHarmony["PRODUCT"].ToString();
                        if (drHarmony.Table.Columns.Contains("GOOD"))
                            if (!string.IsNullOrEmpty(drHarmony["GOOD"].ToString()))
                                objHarmony.GOOD = int.Parse(drHarmony["GOOD"].ToString());
                        if (drHarmony.Table.Columns.Contains("BED"))
                            if (!string.IsNullOrEmpty(drHarmony["BED"].ToString()))
                                objHarmony.BED = int.Parse(drHarmony["BED"].ToString());
                        if (drHarmony.Table.Columns.Contains("EXCEPTION"))
                            objHarmony.EXCEP = drHarmony["EXCEPTION"].ToString();
                        if (drHarmony.Table.Columns.Contains("EXECUTED"))
                            objHarmony.EXECUTED = drHarmony["EXECUTED"].ToString();
                        if (drHarmony.Table.Columns.Contains("MARKED"))
                            objHarmony.MARKED = drHarmony["MARKED"].ToString();

                        //bit
                        objHarmony.FROM_DLL = drHarmony["FROM_DLL"].ToString().ToLower() == "true" ? 1 : 0;  //int.Parse(drHarmony["FROM_DLL"].ToString());
                        objHarmony.DUP_REC = drHarmony["DUP_REC"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["DUP_REC"].ToString());
                        objHarmony.CHANG_REC = drHarmony["CHANG_REC"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["CHANG_REC"].ToString());
                        objHarmony.GOOD_REC = drHarmony["GOOD_REC"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["GOOD_REC"].ToString());
                        objHarmony.L_PRESENT = drHarmony["L_PRESENT"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["L_PRESENT"].ToString());

                        if (drHarmony.Table.Columns.Contains("BADGE"))
                            objHarmony.BADGE = drHarmony["BADGE"].ToString();
                        if (drHarmony.Table.Columns.Contains("AUTHORIZED"))
                            if (!string.IsNullOrEmpty(drHarmony["AUTHORIZED"].ToString()))
                                objHarmony.AUTHORIZED = int.Parse(drHarmony["AUTHORIZED"].ToString());
                        if (drHarmony.Table.Columns.Contains("PARAM1"))
                            objHarmony.PARAM1 = drHarmony["PARAM1"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM2"))
                            objHarmony.PARAM2 = drHarmony["PARAM2"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM3"))
                            objHarmony.PARAM3 = drHarmony["PARAM3"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM4"))
                            objHarmony.PARAM4 = drHarmony["PARAM4"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM5"))
                            objHarmony.PARAM5 = drHarmony["PARAM5"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM6"))
                            objHarmony.PARAM6 = drHarmony["PARAM6"].ToString();
                        if (drHarmony.Table.Columns.Contains("LANCH"))
                            objHarmony.LANCH = drHarmony["LANCH"].ToString();

                        if (drHarmony.Table.Columns.Contains("UNITPROD"))
                            objHarmony.UNITPROD = drHarmony["UNITPROD"].ToString();
                        if (drHarmony.Table.Columns.Contains("HAND"))
                            objHarmony.HAND = drHarmony["HAND"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["HAND"].ToString());
                        if (drHarmony.Table.Columns.Contains("SHIFT"))
                            if (!string.IsNullOrEmpty(drHarmony["SHIFT"].ToString()))
                                objHarmony.SHIFT = float.Parse(drHarmony["SHIFT"].ToString());
                        if (drHarmony.Table.Columns.Contains("NO_ACTIVITY"))
                            if (!string.IsNullOrEmpty(drHarmony["NO_ACTIVITY"].ToString()))
                                objHarmony.NO_ACTIVITY = float.Parse(drHarmony["NO_ACTIVITY"].ToString());
                        if (drHarmony.Table.Columns.Contains("NO_TMP_ACTIVITY"))
                            if (!string.IsNullOrEmpty(drHarmony["NO_TMP_ACTIVITY"].ToString()))
                                objHarmony.NO_TMP_ACTIVITY = float.Parse(drHarmony["NO_TMP_ACTIVITY"].ToString());

                        objHarmony.ZIGNORE = drHarmony["ZIGNORE"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["ZIGNORE"].ToString());

                        if (drHarmony.Table.Columns.Contains("CLOCKID"))
                            if (!string.IsNullOrEmpty(drHarmony["CLOCKID"].ToString()))
                                objHarmony.CLOCKID = int.Parse(drHarmony["CLOCKID"].ToString());
                        if (drHarmony.Table.Columns.Contains("SITE"))
                            objHarmony.SITE = drHarmony["SITE"].ToString();

                        objHarmony.EXPORT_P = objHarmony.L_PRESENT = drHarmony["EXPORT_P"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["EXPORT_P"].ToString());

                        if (drHarmony.Table.Columns.Contains("MEAL_SUPP"))
                                objHarmony.MEAL_SUPP =  drHarmony["MEAL_SUPP"].ToString();
                        if (drHarmony.Table.Columns.Contains("MEAL_QUANT"))
                                objHarmony.MEAL_QUANT =  drHarmony["MEAL_QUANT"].ToString();
                        if (drHarmony.Table.Columns.Contains("REC_ENTER"))
                            if (!string.IsNullOrEmpty(drHarmony["REC_ENTER"].ToString()))
                                objHarmony.REC_ENTER = DateTime.Parse(drHarmony["REC_ENTER"].ToString());
                        if (drHarmony.Table.Columns.Contains("ABSCODE"))
                            objHarmony.ABSCODE = drHarmony["ABSCODE"].ToString();
                        if (drHarmony.Table.Columns.Contains("ABSTIME"))
                            if (!string.IsNullOrEmpty(drHarmony["ABSTIME"].ToString()))
                                objHarmony.ABSTIME = DateTime.Parse(drHarmony["ABSTIME"].ToString());
                        if (drHarmony.Table.Columns.Contains("CAR_BADGE"))
                            objHarmony.CAR_BADGE = drHarmony["CAR_BADGE"].ToString();
                        if (drHarmony.Table.Columns.Contains("CAR_NO"))
                            objHarmony.CAR_NO = drHarmony["CAR_NO"].ToString();

                        if (drHarmony.Table.Columns.Contains("BADGE_TYPE"))
                            if (!string.IsNullOrEmpty(drHarmony["BADGE_TYPE"].ToString()))
                                objHarmony.BADGE_TYPE = int.Parse(drHarmony["BADGE_TYPE"].ToString());
                        if (drHarmony.Table.Columns.Contains("TRAILER_BADGE"))
                            objHarmony.TRAILER_BADGE = drHarmony["TRAILER_BADGE"].ToString();
                        if (drHarmony.Table.Columns.Contains("TRAILER_NO"))
                            objHarmony.TRAILER_NO = drHarmony["TRAILER_NO"].ToString();

                        if (drHarmony.Table.Columns.Contains("LEVELORG"))
                            if (!string.IsNullOrEmpty(drHarmony["LEVELORG"].ToString()))
                                objHarmony.LEVELORG = float.Parse(drHarmony["LEVELORG"].ToString());

                        if (drHarmony.Table.Columns.Contains("CODEORG"))
                            objHarmony.CODEORG = drHarmony["CODEORG"].ToString();
                        if (drHarmony.Table.Columns.Contains("TRAILER1_BADGE"))
                            objHarmony.TRAILER1_BADGE = drHarmony["TRAILER1_BADGE"].ToString();
                        if (drHarmony.Table.Columns.Contains("TRAILER1_NO"))
                            objHarmony.TRAILER1_NO = drHarmony["TRAILER1_NO"].ToString();
                        objHarmony.MAKOR = "Movment";


                        collHarmony.Add(objHarmony);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertToCollErrMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt)
        {
            OBJ_Harmony_movment_Err_Mov objHarmony;
            string taarich,shaa;
            try
            {
                foreach (DataRow drHarmony in dt.Rows)
                {
                    if (drHarmony["BADGE"].ToString().ToString().Length == 13 && NumerichHelper.IsLongNumber(drHarmony["BADGE"].ToString().ToString()))
                    {
                        objHarmony = new OBJ_Harmony_movment_Err_Mov();

                    
                        objHarmony.EMP_NO = int.Parse(drHarmony["BADGE"].ToString().Substring(4, 5));

                        taarich = drHarmony["DATE"].ToString().Substring(0, 2) + "/" + drHarmony["DATE"].ToString().Substring(2, 2) + "/20" + drHarmony["DATE"].ToString().Substring(4);
                        objHarmony.TAARICH = DateTime.Parse(taarich);
                    
                        shaa = drHarmony["TIME"].ToString().Substring(0, 2) + ":" + drHarmony["TIME"].ToString().Substring(2, 2) + ":" + drHarmony["TIME"].ToString().Substring(4);
                        objHarmony.HOUR = DateTime.Parse(taarich + " " + shaa);
                        

                        objHarmony.BADGE = drHarmony["BADGE"].ToString();

                        if (drHarmony.Table.Columns.Contains("CODE"))
                            objHarmony.CODE = drHarmony["CODE"].ToString();
                        if (drHarmony.Table.Columns.Contains("OPER"))
                            objHarmony.OPER = drHarmony["OPER"].ToString();

                        if (drHarmony.Table.Columns.Contains("STATION"))
                            objHarmony.STATION = drHarmony["STATION"].ToString();
                        if (drHarmony.Table.Columns.Contains("STATION_W"))
                            objHarmony.STATION_W = drHarmony["STATION_W"].ToString();
                        if (drHarmony.Table.Columns.Contains("MACHINE"))
                            objHarmony.MACHINE = drHarmony["MACHINE"].ToString();
                        if (drHarmony.Table.Columns.Contains("MACHINE_W"))
                            objHarmony.MACHINE_W = drHarmony["MACHINE_W"].ToString();

                        if (drHarmony.Table.Columns.Contains("ACTION"))
                            objHarmony.ACTION = drHarmony["ACTION"].ToString();
                        if (drHarmony.Table.Columns.Contains("XLEVEL"))
                            objHarmony.XLEVEL = drHarmony["XLEVEL"].ToString();
                        if (drHarmony.Table.Columns.Contains("ALTERNATIVE"))
                            objHarmony.ALTERNATIVE = drHarmony["ALTERNATIVE"].ToString();
                        if (drHarmony.Table.Columns.Contains("PRODUCT"))
                            objHarmony.PRODUCT = drHarmony["PRODUCT"].ToString();
                        if (drHarmony.Table.Columns.Contains("GOOD"))
                            if (!string.IsNullOrEmpty(drHarmony["GOOD"].ToString()))
                                objHarmony.GOOD = int.Parse(drHarmony["GOOD"].ToString());
                        if (drHarmony.Table.Columns.Contains("BED"))
                            if (!string.IsNullOrEmpty(drHarmony["BED"].ToString()))
                                objHarmony.BED = int.Parse(drHarmony["BED"].ToString());


                        objHarmony.FROM_DLL = drHarmony["FROM_DLL"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["FROM_DLL"].ToString());  
                        objHarmony.L_PRESENT = drHarmony["L_PRESENT"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["L_PRESENT"].ToString());

                        if (drHarmony.Table.Columns.Contains("PARAM1"))
                            objHarmony.PARAM1 = drHarmony["PARAM1"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM2"))
                            objHarmony.PARAM2 = drHarmony["PARAM2"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM3"))
                            objHarmony.PARAM3 = drHarmony["PARAM3"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM4"))
                            objHarmony.PARAM4 = drHarmony["PARAM4"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM5"))
                            objHarmony.PARAM5 = drHarmony["PARAM5"].ToString();
                        if (drHarmony.Table.Columns.Contains("PARAM6"))
                            objHarmony.PARAM6 = drHarmony["PARAM6"].ToString();
                        if (drHarmony.Table.Columns.Contains("LANCH"))
                            objHarmony.LANCH = drHarmony["LANCH"].ToString();

                        if (drHarmony.Table.Columns.Contains("UNITPROD"))
                            objHarmony.UNITPROD = drHarmony["UNITPROD"].ToString();

                        if (drHarmony.Table.Columns.Contains("DATETRANSF"))
                            if (!string.IsNullOrEmpty(drHarmony["DATETRANSF"].ToString()))
                                objHarmony.DATETRANSF = DateTime.Parse(drHarmony["DATETRANSF"].ToString());
                        if (drHarmony.Table.Columns.Contains("CODE_ERR"))
                            if (!string.IsNullOrEmpty(drHarmony["CODE_ERR"].ToString()))
                                objHarmony.CODE_ERR = int.Parse(drHarmony["CODE_ERR"].ToString());
                        if (drHarmony.Table.Columns.Contains("DESCR"))
                            objHarmony.DESCR = drHarmony["DESCR"].ToString();
                        if (drHarmony.Table.Columns.Contains("CLOCKID"))
                            if (!string.IsNullOrEmpty(drHarmony["CLOCKID"].ToString()))
                                objHarmony.CLOCKID = int.Parse(drHarmony["CLOCKID"].ToString());
                        if (!string.IsNullOrEmpty(drHarmony["ERR_MOVID"].ToString()))
                            objHarmony.ERR_MOVID = int.Parse(drHarmony["ERR_MOVID"].ToString());

                        //if (drHarmony.Table.Columns.Contains("MEAL_SUPP"))
                        //    if (!string.IsNullOrEmpty(drHarmony["MEAL_SUPP"].ToString()))
                        //        objHarmony.MEAL_SUPP = int.Parse(drHarmony["MEAL_SUPP"].ToString());
                        //if (drHarmony.Table.Columns.Contains("MEAL_QUANT"))
                        //    if (!string.IsNullOrEmpty(drHarmony["MEAL_QUANT"].ToString()))
                        //        objHarmony.MEAL_QUANT = int.Parse(drHarmony["MEAL_QUANT"].ToString());

                        objHarmony.MAKOR = "Err_Mov";
                        collHarmony.Add(objHarmony);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetNetunimToAttend()
        {
            return _container.Resolve<IClockDAL>().GetNetunimToAttend();
        }

        public void InsertControlAttendRecord(DateTime taarich, int status, string teur)
        {
            _container.Resolve<IClockDAL>().InsertControlAttendRecord(taarich, status, teur);
        }

        public void UpdateControlAttendRecord(DateTime taarich, int status, string teur)
        {
            _container.Resolve<IClockDAL>().UpdateControlAttendRecord(taarich, status, teur);
        }

        public DataSet GetKnisaIfExists(int iMisparIshi, DateTime taarich, string inShaa, int mispar_sidur)
        {
            return _container.Resolve<IClockDAL>().GetKnisaIfExists(iMisparIshi,  taarich,  inShaa,  mispar_sidur);
        }

        public void InsertKnisatShaon(int mispar_ishi, DateTime taarich, string shaa, int site_kod, int mispar_sidur, string iStm)
        {
            _container.Resolve<IClockDAL>().InsertKnisatShaon( mispar_ishi,  taarich,  shaa,  site_kod,  mispar_sidur,  iStm);
        }

        public DataSet GetYetziaNull(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur)
        {
            return _container.Resolve<IClockDAL>().GetYetziaNull(mispar_ishi, taarich, shaa, mispar_sidur);
        }

        public void UpdateKnisaRecord(int mispar_ishi, DateTime taarich, string shaaK, string shaaY, int site_kod, int mispar_sidur, string iStm)
        {
            _container.Resolve<IClockDAL>().UpdateKnisaRecord(mispar_ishi, taarich, shaaK, shaaY, site_kod, mispar_sidur, iStm);
        }

        public DataSet GetYetziaIfExists(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24)
        {
            return _container.Resolve<IClockDAL>().GetYetziaIfExists(mispar_ishi, taarich, shaa, mispar_sidur, p24);
        }

        public DataSet GetKnisaNull(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24)
        {
            return _container.Resolve<IClockDAL>().GetKnisaNull(mispar_ishi, taarich, shaa, mispar_sidur, p24);
        }

        public void UpdateYeziaRecord(int mispar_ishi, DateTime taarich, string shaaK, string shaaY, int site_kod, int mispar_sidur, string iStm, int p24)
        {
            _container.Resolve<IClockDAL>().UpdateYeziaRecord(mispar_ishi, taarich, shaaK, shaaY, site_kod, mispar_sidur, iStm, p24);
        }

        public void InsertYeziatShaon(int mispar_ishi, DateTime taarich, string shaa, int site_kod, int mispar_sidur, string iStm, int p24)
        {
            _container.Resolve<IClockDAL>().InsertYeziatShaon(mispar_ishi, taarich, shaa, site_kod, mispar_sidur, iStm, p24);
        }
    }
}

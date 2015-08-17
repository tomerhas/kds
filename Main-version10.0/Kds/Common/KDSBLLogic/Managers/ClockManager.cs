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

        public void SaveShaonimMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment)
        {
             _container.Resolve<IClockDAL>().SaveHarmonyMovment(collHarmonyMovment);
        }


        public void InsertToCollMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt)
        {
            OBJ_Harmony_movment_Err_Mov objHarmony;
            try
            {
                foreach (DataRow drHarmony in dt.Rows)
                {
                    objHarmony = new OBJ_Harmony_movment_Err_Mov();

                    
                    objHarmony.EMP_NO = int.Parse( drHarmony["EMP_NO"].ToString());
                    objHarmony.TAARICH = DateTime.Parse(drHarmony["DATE"].ToString());
                    objHarmony.HOUR = DateTime.Parse(drHarmony["TIME"].ToString());

                    objHarmony.CODE = drHarmony["CODE"].ToString();
                    objHarmony.STATION = drHarmony["STATION"].ToString();
                    objHarmony.MACHINE = drHarmony["MACHINE"].ToString();
                    objHarmony.EXCEP = drHarmony["EXCEPTION"].ToString();
                    objHarmony.EXECUTED = drHarmony["EXECUTED"].ToString();
                    objHarmony.MARKED = drHarmony["MARKED"].ToString();

                    //bit
                    objHarmony.FROM_DLL = drHarmony["FROM_DLL"].ToString().ToLower()== "true" ? 1 : 0;  //int.Parse(drHarmony["FROM_DLL"].ToString());
                    objHarmony.DUP_REC = drHarmony["DUP_REC"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["DUP_REC"].ToString());
                    objHarmony.CHANG_REC = drHarmony["CHANG_REC"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["CHANG_REC"].ToString());
                    objHarmony.GOOD_REC = drHarmony["GOOD_REC"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["GOOD_REC"].ToString());
                    objHarmony.L_PRESENT = drHarmony["L_PRESENT"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["L_PRESENT"].ToString());


                    objHarmony.BADGE = drHarmony["BADGE"].ToString();
                    if (!string.IsNullOrEmpty(drHarmony["AUTHORIZED"].ToString()))
                        objHarmony.AUTHORIZED = int.Parse(drHarmony["AUTHORIZED"].ToString());
                    objHarmony.PARAM1 = drHarmony["PARAM1"].ToString();
                    objHarmony.PARAM2 = drHarmony["PARAM2"].ToString();
                    objHarmony.PARAM3 = drHarmony["PARAM3"].ToString();
                    objHarmony.PARAM4 = drHarmony["PARAM4"].ToString();
                    objHarmony.PARAM5 = drHarmony["PARAM5"].ToString();
                    objHarmony.PARAM6 = drHarmony["PARAM6"].ToString();
                    objHarmony.LANCH = drHarmony["LANCH"].ToString();

                    objHarmony.HAND =  drHarmony["HAND"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["HAND"].ToString());
                    objHarmony.ZIGNORE = drHarmony["ZIGNORE"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["ZIGNORE"].ToString());

                    if (!string.IsNullOrEmpty(drHarmony["CLOCKID"].ToString()))
                        objHarmony.CLOCKID = int.Parse(drHarmony["CLOCKID"].ToString());
                    objHarmony.EXPORT_P = objHarmony.L_PRESENT = drHarmony["EXPORT_P"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["EXPORT_P"].ToString());
                    if (!string.IsNullOrEmpty(drHarmony["MEAL_SUPP"].ToString()))
                        objHarmony.MEAL_SUPP = int.Parse(drHarmony["MEAL_SUPP"].ToString());
                    if (!string.IsNullOrEmpty(drHarmony["MEAL_QUANT"].ToString()))
                        objHarmony.MEAL_QUANT = int.Parse(drHarmony["MEAL_QUANT"].ToString());
                    if (!string.IsNullOrEmpty(drHarmony["REC_ENTER"].ToString()))
                        objHarmony.REC_ENTER = DateTime.Parse(drHarmony["REC_ENTER"].ToString());
                    objHarmony.ABSCODE = drHarmony["ABSCODE"].ToString();
                    if (!string.IsNullOrEmpty(drHarmony["ABSTIME"].ToString()))
                        objHarmony.ABSTIME = DateTime.Parse(drHarmony["ABSTIME"].ToString());
                    objHarmony.CAR_BADGE = drHarmony["CAR_BADGE"].ToString();
                    objHarmony.CAR_NO = drHarmony["CAR_NO"].ToString();

                    if (!string.IsNullOrEmpty(drHarmony["BADGE_TYPE"].ToString()))
                        objHarmony.BADGE_TYPE = int.Parse(drHarmony["BADGE_TYPE"].ToString());
                   
                    objHarmony.MAKOR =  "Movment";

                    ////objHarmony.SHIFT = int.Parse(drHarmony["SHIFT"].ToString());  
                    ////objHarmony.CODE_ERR = int.Parse(drHarmony["CODE_ERR"].ToString());
                    ////objHarmony.DATETRANSF = DateTime.Parse(drHarmony["DATETRANSF"].ToString());     
                    ////objHarmony.BED = int.Parse(drHarmony["BED"].ToString());          
                    ////objHarmony.ERR_MOVID = int.Parse(drHarmony["ERR_MOVID"].ToString());
                    ////objHarmony.NO_TMP_ACTIVITY = int.Parse(drHarmony["NO_TMP_ACTIVITY"].ToString());           
                    ////objHarmony.GOOD = int.Parse(drHarmony["GOOD"].ToString());
                    ////objHarmony.LEVELORG = int.Parse(drHarmony["LEVELORG"].ToString());    
                    ////objHarmony.NO_ACTIVITY = int.Parse(drHarmony["NO_ACTIVITY"].ToString());
                    ////objHarmony.CODEORG = drHarmony["CODEORG"].ToString();
                    ////objHarmony.ACTION = drHarmony["ACTION"].ToString();
                    ////objHarmony.ALTERNATIVE = drHarmony["ALTERNATIVE"].ToString();
                    ////objHarmony.DESCR = drHarmony["DESCR"].ToString();
                    ////objHarmony.TRAILER1_BADGE = drHarmony["TRAILER1_BADGE"].ToString();
                    ////objHarmony.MACHINE_W = drHarmony["MACHINE_W"].ToString();
                    ////objHarmony.SITE = drHarmony["SITE"].ToString();
                    ////objHarmony.TRAILER_BADGE = drHarmony["TRAILER_BADGE"].ToString();
                    ////objHarmony.OPER = drHarmony["OPER"].ToString();
                    ////objHarmony.TRAILER_NO = drHarmony["TRAILER_NO"].ToString();
                    ////objHarmony.XLEVEL = drHarmony["XLEVEL"].ToString();   
                    ////objHarmony.PRODUCT = drHarmony["PRODUCT"].ToString();
                    ////objHarmony.TRAILER1_NO = drHarmony["TRAILER1_NO"].ToString();
                    ////objHarmony.STATION_W = drHarmony["STATION_W"].ToString();
                    ////objHarmony.UNITPROD = drHarmony["UNITPROD"].ToString();




                    // objHarmony.MAKOR = "1";
                    //  objHarmony.TAARICH_MEADKEN_ACHARON = drHarmony["HOUR"]; ;
                    //  objHarmony.MEADKEN_ACHARON = drHarmony["HOUR"]; ;
                    collHarmony.Add(objHarmony);
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
                        objHarmony.CODE = drHarmony["CODE"].ToString();
                        objHarmony.OPER = drHarmony["OPER"].ToString();
                        objHarmony.STATION_W = drHarmony["STATION_W"].ToString();
                        objHarmony.MACHINE_W = drHarmony["MACHINE_W"].ToString();

                        objHarmony.ACTION = drHarmony["ACTION"].ToString();
                        objHarmony.XLEVEL = drHarmony["XLEVEL"].ToString();
                        objHarmony.ALTERNATIVE = drHarmony["ALTERNATIVE"].ToString();
                        if (!string.IsNullOrEmpty(drHarmony["GOOD"].ToString()))
                            objHarmony.GOOD = int.Parse(drHarmony["GOOD"].ToString());
                        if (!string.IsNullOrEmpty(drHarmony["BED"].ToString()))
                            objHarmony.BED = int.Parse(drHarmony["BED"].ToString());


                        objHarmony.FROM_DLL = drHarmony["FROM_DLL"].ToString().ToLower() == "true" ? 1 : 0;// int.Parse(drHarmony["FROM_DLL"].ToString());  
                        objHarmony.L_PRESENT = drHarmony["L_PRESENT"].ToString().ToLower() == "true" ? 1 : 0; //int.Parse(drHarmony["L_PRESENT"].ToString());

                        objHarmony.PARAM1 = drHarmony["PARAM1"].ToString();
                        objHarmony.PARAM2 = drHarmony["PARAM2"].ToString();
                        objHarmony.PARAM3 = drHarmony["PARAM3"].ToString();
                        objHarmony.PARAM4 = drHarmony["PARAM4"].ToString();
                        objHarmony.PARAM5 = drHarmony["PARAM5"].ToString();
                        objHarmony.PARAM6 = drHarmony["PARAM6"].ToString();
                        objHarmony.LANCH = drHarmony["LANCH"].ToString();

                        if (!string.IsNullOrEmpty(drHarmony["DATETRANSF"].ToString()))
                            objHarmony.DATETRANSF = DateTime.Parse(drHarmony["DATETRANSF"].ToString());
                        if (!string.IsNullOrEmpty(drHarmony["CODE_ERR"].ToString()))
                            objHarmony.CODE_ERR = int.Parse(drHarmony["CODE_ERR"].ToString());

                        objHarmony.DESCR = drHarmony["DESCR"].ToString();
                        if (!string.IsNullOrEmpty(drHarmony["CLOCKID"].ToString()))
                            objHarmony.CLOCKID = int.Parse(drHarmony["CLOCKID"].ToString());
                        if (!string.IsNullOrEmpty(drHarmony["ERR_MOVID"].ToString()))
                            objHarmony.ERR_MOVID = int.Parse(drHarmony["ERR_MOVID"].ToString());
                        //?  objHarmony.MEAL_SUPP = int.Parse(drHarmony["MEAL_SUPP"].ToString());
                        //?  objHarmony.MEAL_QUANT = int.Parse(drHarmony["MEAL_QUANT"].ToString());

                        objHarmony.MAKOR = "Err_Mov";
                        ////objHarmony.EXCEP = drHarmony["EXCEPTION"].ToString();
                        ////objHarmony.EXECUTED = drHarmony["EXECUTED"].ToString();
                        ////objHarmony.MARKED = drHarmony["MARKED"].ToString();
                        ////objHarmony.DUP_REC = int.Parse(drHarmony["DUP_REC"].ToString());    
                        ////objHarmony.CHANG_REC = int.Parse(drHarmony["CHANG_REC"].ToString());
                        ////objHarmony.GOOD_REC = int.Parse(drHarmony["GOOD_REC"].ToString());           
                        ////objHarmony.AUTHORIZED = int.Parse(drHarmony["AUTHORIZED"].ToString());
                        ////objHarmony.HAND = int.Parse(drHarmony["HAND"].ToString());
                        ////objHarmony.ZIGNORE = int.Parse(drHarmony["ZIGNORE"].ToString());     
                        ////objHarmony.EXPORT_P = int.Parse(drHarmony["EXPORT_P"].ToString());    
                        ////objHarmony.REC_ENTER = DateTime.Parse(drHarmony["REC_ENTER"].ToString());
                        ////objHarmony.ABSCODE = drHarmony["ABSCODE"].ToString();
                        ////objHarmony.ABSTIME = DateTime.Parse(drHarmony["ABSTIME"].ToString());
                        ////objHarmony.CAR_BADGE = drHarmony["CAR_BADGE"].ToString();
                        ////objHarmony.CAR_NO = drHarmony["CAR_NO"].ToString();
                        ////objHarmony.BADGE_TYPE = int.Parse(drHarmony["BADGE_TYPE"].ToString());
                        ////objHarmony.STATION = drHarmony["STATION"].ToString();
                        //// objHarmony.MACHINE = drHarmony["MACHINE"].ToString();
                        ////objHarmony.SHIFT = int.Parse(drHarmony["SHIFT"].ToString());      
                        ////objHarmony.NO_TMP_ACTIVITY = int.Parse(drHarmony["NO_TMP_ACTIVITY"].ToString());           
                        ////objHarmony.LEVELORG = int.Parse(drHarmony["LEVELORG"].ToString());    
                        ////objHarmony.NO_ACTIVITY = int.Parse(drHarmony["NO_ACTIVITY"].ToString());
                        ////objHarmony.CODEORG = drHarmony["CODEORG"].ToString();
                        ////objHarmony.TRAILER1_BADGE = drHarmony["TRAILER1_BADGE"].ToString(); 
                        ////objHarmony.SITE = drHarmony["SITE"].ToString();
                        ////objHarmony.TRAILER_BADGE = drHarmony["TRAILER_BADGE"].ToString();
                        ////objHarmony.TRAILER_NO = drHarmony["TRAILER_NO"].ToString();
                        ////objHarmony.PRODUCT = drHarmony["PRODUCT"].ToString();
                        ////objHarmony.TRAILER1_NO = drHarmony["TRAILER1_NO"].ToString();
                        ////objHarmony.UNITPROD = drHarmony["UNITPROD"].ToString();

                        collHarmony.Add(objHarmony);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

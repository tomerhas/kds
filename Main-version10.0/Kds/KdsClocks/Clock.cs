using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using KDSCommon.UDT;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;

namespace KdsClocks
{
    public class Clock 
    {
        public void InsertMovemetRecords()
        {
            COLL_HARMONY_MOVMENT_ERR_MOV collHarmony = new COLL_HARMONY_MOVMENT_ERR_MOV();
            try
            {
                syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
                var xmlE = wsSy.SQLRecordSetToXML(ConfigurationManager.AppSettings["MOVMENTSQL"]);
                DataSet DsMovement = ConvertXMLToDataSet(xmlE);
                InsertToCollMovment(collHarmony, DsMovement.Tables[6]);

                var clockManager = ServiceLocator.Current.GetInstance<IClockManager>();
                clockManager.SaveShaonimMovment(collHarmony);
               
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InsertToCollMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt)
        {
            OBJ_Harmony_movment_Err_Mov objHarmony;
            try
            {
                foreach (DataRow drHarmony in dt.Rows)
                {
                    objHarmony = new OBJ_Harmony_movment_Err_Mov();

                    objHarmony.NUMERATOR = int.Parse(drHarmony["NUMERATOR"].ToString());
                    objHarmony.EMP_NO = int.Parse(drHarmony["EMP_NO"].ToString());
                    objHarmony.TAARICH = DateTime.Parse(drHarmony["DATE"].ToString());
                    objHarmony.HOUR = DateTime.Parse(drHarmony["TIME"].ToString());

                    objHarmony.CODE = drHarmony["CODE"].ToString();
                    objHarmony.STATION = drHarmony["STATION"].ToString();
                    objHarmony.MACHINE = drHarmony["MACHINE"].ToString();
                    objHarmony.EXCEP = drHarmony["EXCEPTION"].ToString();
                    objHarmony.EXECUTED = drHarmony["EXECUTED"].ToString();
                    objHarmony.MARKED = drHarmony["MARKED"].ToString();

                    //bit
                    ////objHarmony.FROM_DLL = int.Parse(drHarmony["FROM_DLL"].ToString());  
                    ////objHarmony.DUP_REC = int.Parse(drHarmony["DUP_REC"].ToString());    
                    ////objHarmony.CHANG_REC = int.Parse(drHarmony["CHANG_REC"].ToString());
                    ////objHarmony.GOOD_REC = int.Parse(drHarmony["GOOD_REC"].ToString());
                    ////objHarmony.L_PRESENT = int.Parse(drHarmony["L_PRESENT"].ToString());


                    objHarmony.BADGE = drHarmony["BADGE"].ToString();
                    objHarmony.AUTHORIZED = int.Parse(drHarmony["AUTHORIZED"].ToString());
                    objHarmony.PARAM1 = drHarmony["PARAM1"].ToString();
                    objHarmony.PARAM2 = drHarmony["PARAM2"].ToString();
                    objHarmony.PARAM3 = drHarmony["PARAM3"].ToString();
                    objHarmony.PARAM4 = drHarmony["PARAM4"].ToString();
                    objHarmony.PARAM5 = drHarmony["PARAM5"].ToString();
                    objHarmony.PARAM6 = drHarmony["PARAM6"].ToString();
                    objHarmony.LANCH = drHarmony["LANCH"].ToString();

                    ////objHarmony.HAND = int.Parse(drHarmony["HAND"].ToString());
                    ////objHarmony.ZIGNORE = int.Parse(drHarmony["ZIGNORE"].ToString());

                    objHarmony.CLOCKID = int.Parse(drHarmony["CLOCKID"].ToString());
                    ////objHarmony.EXPORT_P = int.Parse(drHarmony["EXPORT_P"].ToString());
                    objHarmony.MEAL_SUPP = int.Parse(drHarmony["MEAL_SUPP"].ToString());
                    objHarmony.MEAL_QUANT = int.Parse(drHarmony["MEAL_QUANT"].ToString());
                    objHarmony.REC_ENTER = DateTime.Parse(drHarmony["REC_ENTER"].ToString());
                    objHarmony.ABSCODE = drHarmony["ABSCODE"].ToString();
                    objHarmony.ABSTIME = DateTime.Parse(drHarmony["ABSTIME"].ToString());
                    objHarmony.CAR_BADGE = drHarmony["CAR_BADGE"].ToString();
                    objHarmony.CAR_NO = drHarmony["CAR_NO"].ToString();
                    objHarmony.BADGE_TYPE = int.Parse(drHarmony["BADGE_TYPE"].ToString());


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
       // InsertToCollMovment
        public void InsertMovemetErrRecords()
        {
            try
            {
                syInterfaceWS.MalalClient wsSy = new syInterfaceWS.MalalClient();
                var xmlE = wsSy.SQLRecordSetToXML(ConfigurationManager.AppSettings["ERRMOVESQL"]);
                DataSet DsMovement = ConvertXMLToDataSet(xmlE);
                // להכין udt
                //לשלוח לשמירה

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
    }
}


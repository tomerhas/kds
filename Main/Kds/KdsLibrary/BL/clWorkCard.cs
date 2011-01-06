using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using System.Data;
using System.Reflection;

namespace KdsLibrary.BL
{
    public class clWorkCard
    {
        private const int ERR_DESCRIPTION = 1;
        private const int ERR_NUM = 2;
        private const int ERR_SIDUR_NUMBER = 3;
        private const int ERR_SIDUR_START = 4;
        private const int ERR_ACTIVITIY_START = 5;
        private const int ERR_ACTIVITIY_NUMBER = 6;
        private enum ErrorLevelItemNum
        {
            errYomAvoda = 3,
            errSidur = 5,
            errPeilut =7
        }
   
        public enum StatusColor
        {
            Red,
            White
        }
        public enum ErrorLevel
        {
            LevelYomAvoda = 1,
            LevelSidur = 2,
            LevelPeilut = 3
        }
        public static bool IsIdkunExists(int iMisparIshi, bool bProfileRashemet, ErrorLevel enLevel, int iPakadId, int iMisparSidur, 
                                         DateTime dShatHatchala, DateTime dShatYetiza, int iMisparKnisa, 
                                         ref DataTable dtIdkuneyRashemet)
        {            
            bool bIdkunExists = false;
            DataRow[] drResults;
            if (!(bProfileRashemet))
            {
                switch (enLevel)
                {
                    case ErrorLevel.LevelYomAvoda:
                        drResults = dtIdkuneyRashemet.Select("pakad_id=" + iPakadId);
                        break;
                    case ErrorLevel.LevelSidur:
                        drResults = dtIdkuneyRashemet.Select("pakad_id=" + iPakadId + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = Convert('" + dShatHatchala + "', 'System.DateTime')");
                        break;
                    case ErrorLevel.LevelPeilut:
                        drResults = dtIdkuneyRashemet.Select("pakad_id=" + iPakadId + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = Convert('" + dShatHatchala + "', 'System.DateTime')" + " and shat_yetzia= Convert('" + dShatYetiza + "', 'System.DateTime') and mispar_knisa=" + iMisparKnisa);
                        break;
                    default:
                        drResults = dtIdkuneyRashemet.Select("pakad_id=" + iPakadId);
                        break;
                }
                if (drResults != null)
                {
                    if (iMisparIshi > 0)
                    {//המשתמש מעדכן את הכרטיס של עצמו, לכן נבדוק אם בפעם האחרונה
                     //מי שעדכן את הכרטיס היה המשתמש או הרשמת  או כל גורם אחר
                      //אם מי שעדכן את הכרטיס היה כל גורם אחר מלבד הוא עצמו או עדכון מערכת )מספר ב-מינוס, לא נאפשר עדכון השדה
                        
                        if ((drResults.Length) > 0)
                        {
                            int iGoremMeasher;
                            for (int i=0; i < drResults.Length; i++)
                            {
                                iGoremMeasher = String.IsNullOrEmpty(drResults[i]["GOREM_MEADKEN"].ToString()) ? 0 : int.Parse(drResults[i]["GOREM_MEADKEN"].ToString());
                                if ((iMisparIshi != iGoremMeasher) && (iGoremMeasher >= 0))
                                {
                                    bIdkunExists = true;
                                    break;
                                }
                            }                            
                        }
                        else
                        {
                            bIdkunExists = false;
                        }
                    }
                    else
                    {
                        bIdkunExists = drResults.Length > 0;
                    }                   
                }
            }
            return bIdkunExists;            
        }
        public static bool IsErrorExists(DataTable dtErrorList, int iErrorNum, int iMisparIshi, DateTime dCardDate,
                             int iMisparSidur, DateTime dFullShatHatchala, DateTime dPeilutShatYetiza, int iMisparKnisa)                            
        {
            //שגיאה ברמת פעילות           
            DataRow[] drResults;
            //bool bApprovalExists=false;
            
            try
            {
                //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
                drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrorNum + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = '" + dFullShatHatchala + "' and shat_yetzia='" + dPeilutShatYetiza + "' and mispar_knisa=" + iMisparKnisa.ToString());
                //if (drResults.Length > 0)
                //{
                // //אם קיימת שגיאה, נבדוק אם קיים אישור לשגיאה, אם קיים אישור לא נציג את השגיאה
                // bApprovalExists = IsErrorApprovalExists(ErrorLevel.LevelPeilut, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, DateTime.Parse(dCardDate.ToShortDateString() + " " + sShatHatchala), dPeilutShatYetiza, iMisparKnisa);           
                //}

                return (drResults.Length > 0); //&& (!bApprovalExists));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool IsErrorExists(DataTable dtErrorList, int iErrorNum, int iMisparIshi, DateTime dCardDate,
                              int iMisparSidur, DateTime dFullShatHatchala)
        {
            //שגיאה ברמת סידור           
            DataRow[] drResults;
            //bool bApprovalExists=false;
           
            try
            {
                //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
                drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrorNum + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = '" + dFullShatHatchala + "'");
                //if (drResults.Length > 0)
                //{
                    //אם קיימת שגיאה, נבדוק אם קיים אישור לשגיאה, אם קיים אישור לא נציג את השגיאה
                  //  bApprovalExists = IsErrorApprovalExists(ErrorLevel.LevelSidur, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, DateTime.Parse(dCardDate.ToShortDateString() + " " + sShatHatchala), DateTime.MinValue, 0);
                //}
                return (drResults.Length > 0); //&& (!bApprovalExists));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public static bool IsErrorExists(int iErrorNum, int iMisparIshi, DateTime dCardDate)
        //{

        //    clBatchManager oBatchManager = new clBatchManager();
        //    DataRow[] drResults;
        //    bool bApprovalExists = false;
        //    //שגיאה ברמת יום עבודה
        //    try
        //    {
        //        //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
        //        drResults = oBatchManager.dtErrors.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrorNum);
        //        if (drResults.Length > 0)
        //        {
        //            //אם קיימת שגיאה, נבדוק אם קיים אישור לשגיאה, אם קיים אישור לא נציג את השגיאה
        //            bApprovalExists = IsErrorApprovalExists(ErrorLevel.LevelYomAvoda, iErrorNum, iMisparIshi, dCardDate, 0, DateTime.MinValue, DateTime.MinValue, 0);
        //        }
        //        return ((drResults.Length > 0) && (!bApprovalExists));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static bool IsErrorExists(DataTable dtErrorList, int iErrorNum, int iMisparIshi, DateTime dCardDate)                                                                       
        {
            //שגיאה ברמת יום עבודה         
            DataRow[] drResults;
            //bool bApprovalExists=false;
         
            try
            {
                //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
                drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrorNum );
                //if (drResults.Length > 0)
                //{                   
                //    //אם קיימת שגיאה, נבדוק אם קיים אישור לשגיאה, אם קיים אישור לא נציג את השגיאה
                //    bApprovalExists = IsErrorApprovalExists(ErrorLevel.LevelYomAvoda, iErrorNum, iMisparIshi, dCardDate, 0, DateTime.MinValue, DateTime.MinValue, 0);                 
                //}
                return (drResults.Length > 0);// && (!bApprovalExists));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static bool IsErrorExistsInSadotNosafim(DataTable dtErrorList, int iErrorNum, int iMisparIshi, DateTime dCardDate,
        //                      int iMisparSidur, DateTime dFullShatHatchala)
        //{
        //    //שגיאה ברמת סידור  - נוספים
        //    DataRow[] drResults;
        //    //bool bApprovalExists=false;

        //    try
        //    {
        //        //הרוטינה מחזיר אמת אם קיימת שגיאה למפתח המתקבל
        //        drResults = dtErrorList.Select("mispar_ishi=" + iMisparIshi + " and taarich=Convert('" + dCardDate.ToShortDateString() + "', 'System.DateTime')" + " and check_num=" + iErrorNum + " and mispar_sidur=" + iMisparSidur + " and shat_hatchala = '" + dFullShatHatchala + "'");
        //        //if (drResults.Length > 0)
        //        //{
        //        //אם קיימת שגיאה, נבדוק אם קיים אישור לשגיאה, אם קיים אישור לא נציג את השגיאה
        //        //  bApprovalExists = IsErrorApprovalExists(ErrorLevel.LevelSidur, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, DateTime.Parse(dCardDate.ToShortDateString() + " " + sShatHatchala), DateTime.MinValue, 0);
        //        //}
        //        return (drResults.Length > 0); //&& (!bApprovalExists));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static bool IsErrorApprovalExists(ErrorLevel oErrorLevel,int iErrorNum, int iMisparIshi, 
                                                 DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, 
                                                 DateTime dShatYetiza, int iKnisaNum)
        {
            //Return true if approval for error exists in table, else false.
            OBJ_SHGIOT_MEUSHAROT oObjShgiotMeusharot = new OBJ_SHGIOT_MEUSHAROT();
            
            try
            {
                oObjShgiotMeusharot.KOD_SHGIA = iErrorNum;
                oObjShgiotMeusharot.MISPAR_ISHI = iMisparIshi;
                oObjShgiotMeusharot.TAARICH = dCardDate;

                switch (oErrorLevel)
                {
                    case ErrorLevel.LevelYomAvoda:
                        break;
                    case ErrorLevel.LevelSidur:
                        oObjShgiotMeusharot.MISPAR_SIDUR = iMisparSidur;
                        oObjShgiotMeusharot.SHAT_HATCHALA = dShatHatchala;
                        break;
                    case ErrorLevel.LevelPeilut:
                        oObjShgiotMeusharot.MISPAR_SIDUR = iMisparSidur;
                        oObjShgiotMeusharot.SHAT_HATCHALA = dShatHatchala;
                        oObjShgiotMeusharot.SHAT_YETZIA = dShatYetiza;
                        oObjShgiotMeusharot.MISPAR_KNISA = iKnisaNum;
                        break;
                    default:
                        break;
                }
                clDal _Dal = new clDal();

                _Dal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_obj_shgiot_meusharot", ParameterType.ntOracleObject, oObjShgiotMeusharot, ParameterDir.pdInput, "OBJ_SHGIOT_MEUSHAROT");
                _Dal.AddParameter("p_level", ParameterType.ntOracleInteger, oErrorLevel.GetHashCode(), ParameterDir.pdInput);
                _Dal.ExecuteSP(clGeneral.cFnIsApprovalErrorExists);

                return int.Parse(_Dal.GetValParam("p_result").ToString()) > 0;        
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void SaveEmployeeCard(COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd, COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd,
                                     COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd, COLL_SIDURIM_OVDIM oCollSidurimOvdimIns,
                                     COLL_SIDURIM_OVDIM oCollSidurimOvdimDel, COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel,
                                     COLL_IDKUN_RASHEMET oCollIdkunRashemet)
                                     
        {
            clDal _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_coll_yamey_avoda_ovdim", ParameterType.ntOracleArray, oCollYameyAvodaUpd, ParameterDir.pdInput, "COLL_YAMEY_AVODA_OVDIM");
                _Dal.AddParameter("p_coll_sidurim_ovdim_upd", ParameterType.ntOracleArray, oCollSidurimOvdimUpd, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                _Dal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimUpd, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                _Dal.AddParameter("p_coll_sidurim_ovdim_ins", ParameterType.ntOracleArray, oCollSidurimOvdimIns, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                _Dal.AddParameter("p_coll_sidurim_ovdim_del", ParameterType.ntOracleArray, oCollSidurimOvdimDel, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                _Dal.AddParameter("p_coll_peilut_ovdim_del", ParameterType.ntOracleArray, oCollPeilutOvdimDel, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                _Dal.AddParameter("p_coll_idkun_rashemet", ParameterType.ntOracleArray, oCollIdkunRashemet, ParameterDir.pdInput, "COLL_IDKUN_RASHEMET");
                _Dal.ExecuteSP(clGeneral.cProSaveEmployeeCard);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public int GetCountYemeyAvodaOvdim(int iMisparIshi,DateTime dTarMe,DateTime dTarAd)
        {
            int iCountYemeyAvoda = 0;
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cFunGetCountYemeyAvodaLeoved);

                iCountYemeyAvoda = int.Parse(oDal.GetValParam("p_count").ToString());

                return iCountYemeyAvoda;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        public int GetIsCardExistsInYemeyAvodaOvdim(int iMisparIshi,DateTime dTaarich)
        {
            int iCountYemeyAvoda = 0;
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);                
                oDal.ExecuteSP(clGeneral.cFunIsCardExistsYemeyAvoda);

                iCountYemeyAvoda = int.Parse(oDal.GetValParam("p_count").ToString());

                return iCountYemeyAvoda;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        
        public DataTable GetSidurimLeoved(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dtSidurim=new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput );
                oDal.ExecuteSP(clGeneral.cProGetSidurimLeoved, ref dtSidurim);

                return dtSidurim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsYemeyAvodaWithSidurim(int iMisparIshi, DateTime dTaarich,DateTime dAdTaarich,DateTime dShatHatchala,DateTime dShatSiyum,DateTime dShatHatchalaDef,DateTime dShatSiyumDef,int iMisparSidur,int iUserId)
        {
            clTxDal oDal = new clTxDal();
            DateTime dTemp;
            try
            {
                oDal.TxBegin();

                InsUpdSidurimOvdim(ref oDal,iMisparIshi, dTaarich,iMisparSidur,dShatHatchala,dShatSiyum,iUserId);
              
                dTemp=dTaarich.AddDays(1);
                do
                {
                    InsYemeyAvodaLeoved(ref oDal,iMisparIshi, dTemp, clGeneral.enStatusTipul.Betipul.GetHashCode(), clGeneral.enMeasherOMistayeg.Measher.GetHashCode(), iUserId);
                    dShatHatchalaDef = dShatHatchalaDef.AddDays(1);
                    dShatSiyumDef = dShatSiyumDef.AddDays(1);
                    InsUpdSidurimOvdim(ref oDal,iMisparIshi,  dTemp, iMisparSidur, dShatHatchalaDef, dShatSiyumDef, iUserId);
                    dTemp = dTemp.AddDays(1);
                }
                while (dTemp <= dAdTaarich);
            
                oDal.TxCommit();

            }
            catch (Exception ex)
            {
                oDal.TxRollBack();
                throw ex;
            }
        }

        private void InsYemeyAvodaLeoved(ref clTxDal oDal, int iMisparIshi, DateTime dTaarich,int iStatus,int iMeasherOrMistayeg,int iUserId)
        {
             try
            {
                oDal.ClearCommand();
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_measher_mistayeg", ParameterType.ntOracleInteger, iMeasherOrMistayeg, ParameterDir.pdInput);
                oDal.AddParameter("p_status", ParameterType.ntOracleInteger, iStatus, ParameterDir.pdInput);
                oDal.AddParameter("p_meadken", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);
                
                oDal.ExecuteSP(clGeneral.cProInsYemeyAvodaLeoved);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsUpdSidurimOvdim(int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iUserId)
        {
            clTxDal oDal = new clTxDal();
            try
            {
                oDal.TxBegin();
                InsUpdSidurimOvdim(ref oDal,iMisparIshi,dTaarich,iMisparSidur,dShatHatchala,dShatGmar,iUserId);

                oDal.TxCommit();
            }
            catch (Exception ex)
            {
                oDal.TxRollBack();
                throw ex;
            }
        }

        public static DataTable GetShgiot(int iErrorNumber)
        {
            clDal _Dal = new clDal();
           // string sDescription = "";
            DataTable dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_error_code", ParameterType.ntOracleInteger, iErrorNumber, ParameterDir.pdInput);
                _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetCtbShgiot, ref dt);
            
                return dt;

            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }
        private void InsUpdSidurimOvdim(ref clTxDal oDal,int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iUserId)
        {
            try
            {
                 oDal.ClearCommand();
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_gmar", ParameterType.ntOracleDate, dShatGmar, ParameterDir.pdInput);
                oDal.AddParameter("p_meadken", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);

                oDal.ExecuteSP(clGeneral.cProInsUpdSidurimLeoved);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsApprovalErrors(OBJ_SHGIOT_MEUSHAROT objShgiotMeusharot)
        {
            clDal _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_obj_shgiot_meusharot", ParameterType.ntOracleObject, objShgiotMeusharot, ParameterDir.pdInput, "OBJ_SHGIOT_MEUSHAROT");
                _Dal.ExecuteSP(clGeneral.cProInsApprovalErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public int InsertToShgiotMeusharot(int iMisparIshi, DateTime dDateCard, string sErrorKey, string sGoremMeasher)
        {
            int iResult = 0;
            try
            {              
                OBJ_SHGIOT_MEUSHAROT oObjShgiotMeusharot = new OBJ_SHGIOT_MEUSHAROT();
                string[] arrResult = sErrorKey.Split((char.Parse("|"))); //hErrKey.Value.Split((char.Parse("|")));
                ErrorLevelItemNum oErrLevel;

                //נאתחל את האובייקט
                oObjShgiotMeusharot.MISPAR_ISHI = iMisparIshi;
                oObjShgiotMeusharot.TAARICH = dDateCard;
                oObjShgiotMeusharot.TAARICH_ISHUR = DateTime.Now;
                oObjShgiotMeusharot.KOD_SHGIA = decimal.Parse(arrResult[ERR_NUM]);
                oObjShgiotMeusharot.GOREM_MEASHER = int.Parse(sGoremMeasher);
                oErrLevel = (ErrorLevelItemNum)arrResult.Length;
                switch (oErrLevel)
                {
                    case ErrorLevelItemNum.errYomAvoda: //שגיאה ברמת יום עבודה
                        oObjShgiotMeusharot.MISPAR_SIDUR = 0;
                        oObjShgiotMeusharot.SHAT_HATCHALA = DateTime.MinValue;
                        oObjShgiotMeusharot.MISPAR_KNISA = 0;
                        oObjShgiotMeusharot.SHAT_YETZIA = DateTime.MinValue;
                        break;
                    case ErrorLevelItemNum.errSidur:// שגיאה ברמת סידור
                        oObjShgiotMeusharot.MISPAR_SIDUR = decimal.Parse(arrResult[ERR_SIDUR_NUMBER]);
                        oObjShgiotMeusharot.SHAT_HATCHALA = DateTime.Parse(arrResult[ERR_SIDUR_START]);
                        oObjShgiotMeusharot.MISPAR_KNISA = 0;
                        oObjShgiotMeusharot.SHAT_YETZIA = DateTime.MinValue;
                        break;
                    case ErrorLevelItemNum.errPeilut: //שגיאה ברמת פעילות
                        oObjShgiotMeusharot.MISPAR_SIDUR = decimal.Parse(arrResult[ERR_SIDUR_NUMBER]);
                        oObjShgiotMeusharot.SHAT_HATCHALA = DateTime.Parse(arrResult[ERR_SIDUR_START]);
                        oObjShgiotMeusharot.SHAT_YETZIA = DateTime.Parse(arrResult[ERR_ACTIVITIY_START]);
                        oObjShgiotMeusharot.MISPAR_KNISA = decimal.Parse(arrResult[ERR_ACTIVITIY_NUMBER]);
                        break;
                }
                InsApprovalErrors(oObjShgiotMeusharot);
                iResult = 1;
                return iResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetIdkuneyRashemet(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר עדכוני רשמת ליום עבודה של עובד:                 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetIdkuneyRashemet, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetSadotNosafimLesidur()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cGetTbSadotNosafimLesidur, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetMeafyeneySidur()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetMeafyeneySidurim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool IsOvedMusach(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            try
            {
                //check if oved musach
                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cFnIsOvedMusach);

                return int.Parse(oDal.GetValParam("p_result").ToString()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertNesiaReka(int iLoginUser,int iMisparIshi, string sCardDate, int iMisparSidur, string sSidurShatHatchala,
                                    long lMakat, long lCarNum, DateTime dPeilutShatYetiza, string sPeilutShatYetiza)
        {
            COLL_OBJ_PEILUT_OVDIM _collPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
            OBJ_PEILUT_OVDIM _objPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            string[] arrShatYetiza;

            //נקבע את שעת היציאה
            _objPeilutOvdimIns.SHAT_YETZIA = dPeilutShatYetiza;

            if (sPeilutShatYetiza.Length > 0)
            {
                if (sPeilutShatYetiza.Substring(sPeilutShatYetiza.Length - 1,1) == "|")
                    sPeilutShatYetiza = sPeilutShatYetiza.Substring(0, sPeilutShatYetiza.Length - 1);

                //נעבור על כל שעות היציאה של הפעילויות בין שתי הנסיעות בהן אנו מוסיפים את הנסיעה הריקה
                //אם כבר קיימת נסיעה ריקה בשעת יציאה הזהה לזו של הנסיעה שאנחנו מוסיפים, נכניס שעת יציאה NULL
                arrShatYetiza = sPeilutShatYetiza.Split('|');
                for (int i = 0; i < arrShatYetiza.Length; i++)
                {
                    if (dPeilutShatYetiza.Equals(DateTime.Parse(arrShatYetiza[i])))
                        _objPeilutOvdimIns.SHAT_YETZIA = DateTime.Parse("01/01/0001 00:00");
                }
            }
           
            _objPeilutOvdimIns.MISPAR_ISHI = iMisparIshi;
            _objPeilutOvdimIns.TAARICH = DateTime.Parse(sCardDate);
            _objPeilutOvdimIns.MISPAR_SIDUR = iMisparSidur;
            _objPeilutOvdimIns.SHAT_HATCHALA_SIDUR = DateTime.Parse(sCardDate +" " + sSidurShatHatchala);
            _objPeilutOvdimIns.MAKAT_NESIA = lMakat;
            _objPeilutOvdimIns.OTO_NO= lCarNum;
            _objPeilutOvdimIns.NEW_SHAT_YETZIA = _objPeilutOvdimIns.SHAT_YETZIA;
           
            _objPeilutOvdimIns.MISPAR_KNISA = 0;
            _objPeilutOvdimIns.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.AddByUser.GetHashCode();
            _objPeilutOvdimIns.TAARICH_IDKUN_ACHARON =DateTime.Parse(DateTime.Now.ToShortDateString());
            _objPeilutOvdimIns.MEADKEN_ACHARON = iLoginUser;
            _collPeilutOvdimIns.Add(_objPeilutOvdimIns);
            clOvdim.InsertPeilutOvdim(_collPeilutOvdimIns);
            
        }       
    }
   
   
   
    //public class WorkCardObj : IComparable
    //{
    //    int _EmpID;
    //    int _SidurID;        
    //    DateTime _CardDate;
    //    DateTime _StartHour;

    //    int _Lina;
    //    bool _IsLinaChanged;

    //    int _Hashlama;
    //    bool _IsHashlamaChanged;

    //    int _Halbasha;
    //    bool _IsHalbashaChanged;

    //    public int EmpID
    //    {
    //        get { return _EmpID; }
    //        set { _EmpID = value; }
    //    }
    //    public DateTime CardDate
    //    {
    //        get { return _CardDate; }
    //        set { _CardDate = value; }
    //    }
    //    public int SidurID
    //    {
    //        get { return _SidurID; }
    //        set { _SidurID = value; }
    //    }
    //    public DateTime StartHour
    //    {
    //        get { return _StartHour; }
    //        set { _StartHour = value; }
    //    }
    //    public int Lina
    //    {
    //        get { return _Lina; }
    //        set { _Lina = value; }
    //    }
    //    public bool IsLinaChanged
    //    {
    //        get { return _IsLinaChanged; }
    //        set { _IsLinaChanged = value; }
    //    }
    //    public int Hashlama
    //    {
    //        get { return _Hashlama; }
    //        set { _Hashlama = value; }
    //    }
    //    public bool IsHashlamaChanged
    //    {
    //        get { return _IsHashlamaChanged; }
    //        set { _IsHashlamaChanged = value; }
    //    }
    //    public int Halbasha
    //    {
    //        get { return _Halbasha; }
    //        set { _Halbasha = value; }
    //    }
    //    public bool IsHalbashaChanged
    //    {
    //        get { return _IsHalbashaChanged; }
    //        set { _IsHalbashaChanged = value; }
    //    }
    //    //Implement IComparable CompareTo 
    //    public int CompareTo(object obj)
    //    {        //    ((WorkCardObj)obj)._IsLinaChanged = ((this.Lina == ((WorkCardObj)obj).Lina));
    //    //    ((WorkCardObj)obj)._IsHalbashaChanged = ((this.Halbasha == ((WorkCardObj)obj).Halbasha));
    //    //    ((WorkCardObj)obj)._IsHashlamaChanged = ((this.Hashlama == ((WorkCardObj)obj).Hashlama));
    //    //   // ((WorkCardObj)obj)._IsLinaChanged = ((this.Lina == ((WorkCardObj)obj).Lina));
    //        Type _CurrObj = this.GetType();
    //        object _Prop;

    //        PropertyInfo[] WorkCards = _CurrObj.GetProperties(BindingFlags.Public);
    //        foreach (PropertyInfo pi in WorkCards)
    //        {
    //            _Prop = pi.GetValue(_CurrObj, null);
    //        }
    //        return 0;
    //        //car c = (car)obj;
    //        //return String.Compare(this.make, c.make);
    //    }

    //}
}

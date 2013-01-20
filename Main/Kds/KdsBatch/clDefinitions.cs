using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using System.Collections;
using KdsLibrary.UDT;
using KdsLibrary;
using KdsLibrary.BL;
using System.Web;
using System.Configuration;
using KdsLibrary.Utils;

namespace KdsBatch
{
    public class clDefinitions
    {
        public const string DefaultEventLogName = "KDS";

        public const string cSchema = "KDSADMIN";
        public const string cProGetOvedDetails = "pkg_errors.pro_get_oved_sidurim_peilut";
        //public const string cProGetLookUpTables = "pkg_errors.pro_get_lookup_tables";
        public const string cProGetOvedMatzav = "pkg_errors.pro_get_oved_matzav";
        public const string cProGetOvedYomAvodaDetails = "pkg_errors.pro_get_oved_yom_avoda_details";
        public const string cFnIsDuplicateShatYetiza = "pkg_errors.fn_is_duplicate_shat_yetiza";
        public const string cProGetSugSidurMeafyenim = "pkg_errors.pro_get_sug_sidur_meafyenim";
        public const string cProGetYemeyAvodaToOved = "pkg_calc.pro_get_yemey_avoda_to_oved";
   //*     public const string cProGetYamimMeyuchadim = "pkg_utils.pro_get_yamim_meyuchadim";
        public const string cProInsChishuv = "pkg_calc.pro_ins_chishuv";
        public const string cProInsChishuvTemp = "pkg_calc.pro_ins_chishuv_tmp";
   //*     public const string cProGetSugeyYamimMeyuchadim = "pkg_utils.pro_get_sugey_yamim_meyuchadim";
        
        public const string cProGetSidurimMeyuchadim = "pkg_sidurim.get_sidurim_meyuchadim_all";
        public const string cProGetTmpSidurimMeyuchadim = "pkg_sidurim.get_tmp_sidurim_meyuchadim";
        public const string cProGetSidurimMeyuchRechiv = "pkg_utils.pro_get_sidurim_meyuch_rechiv";
        public const string cProUpdYameyAvodaOvdim = "pkg_errors.pro_upd_yamey_avoda_ovdim";
        public const string cProUpdSidurimOvdim = "pkg_errors.pro_upd_sidurim_ovdim";
        public const string cProUpdPeilutOvdim = "pkg_errors.pro_upd_peilut_ovdim";
        public const string cProUpdApprovalErrors = "pkg_errors.pro_upd_approval_errors";

        public const string cProInsSidurimOvdim = "pkg_errors.pro_ins_sidurim_ovdim";
        public const string cProDelPeilutOvdim = "pkg_errors.pro_del_peilut_ovdim";
        public const string cProGetSugeySidurRechiv = "pkg_utils.pro_get_sug_sidur_rechiv";
        public const string cProUpdateSidurimLoLetashlum = "pkg_calc.pro_upd_sidurim_lo_letashlum";
        public const string cProGetPeilutLesidur = "pkg_calc.pro_get_peiluyot_lesidur";
        public const string cProGetMichsaYomit = "pkg_calc.pro_get_michsa_yomit";
        public const string cProGetTmpPirteyOved = "pkg_ovdim.pro_get_tmp_pirtey_oved";
        public const string cProGetPremyotView = "pkg_utils.pro_get_premyot_view";
        public const string cProGetOvdimLechishuv = "pkg_batch.pro_get_ovdim_lechishuv";
        public const string cProUpdBakasha = "pkg_batch.pro_upd_bakasha";
        public const string cProUpdBakashaAllfields = "pkg_batch.pro_upd_bakasha_all_fields";
        public const string cProShinuyKelet = "pkg_errors.pro_shinuy_kelet";
        public const string cProGetOvdimToTransfer = "pkg_batch.pro_get_ovdim_to_transfer";
        public const string cProCheckOvedPutar = "pkg_calc.pro_get_oved_putar";
        public const string cProGetAllYameiAvoda = "pkg_batch.pro_get_all_yamei_avoda";
        public const string cProUpdCardStatus = "pkg_errors.pro_upd_card_status";
        public const string cProGetChishuvYomiToOved = "pkg_batch.pro_get_chishuv_yomi";
        public const string cProGetRechivimChishuvYomi = "pkg_batch.pro_get_rechivim_chishuv_yomi";
        public const string cProDelChishuvAfterTransfer = "pkg_batch.pro_del_chishuv_after_transfer";
        public const string cProUpdStatusYameyAvoda = "pkg_batch.pro_upd_status_yamey_avoda";
        public const string cProUpdOvdimImShinuyHr = "Egd_Mafienim_New.Egd_Idkun_Ovdim_Im_Shinui";
        public const string cProGetMeafyeneyElements = "PKG_ELEMENTS.get_tmp_meafyeney_elementim";
        public const string cProUpdTarRitzatShgiot = "pkg_errors.pro_upd_tar_ritzat_shgiot";
        public const string cProGetApprovalToEmploee = "PKG_APPROVALS.get_approval_to_emploee";
        public const string cProIsDuplicateTravel = "pkg_errors.pro_is_duplicate_travel";
        public const string cProIsSidurChofef = "pkg_errors.pro_have_sidur_chofef";
        public const string cFunCountShgiotLetzuga = "pkg_errors.fn_count_shgiot_letzuga";
        public const string cProGetIdkuneyRashemet = "pkg_errors.get_idkuney_rashemet";
        public const string cProCheckHaveSidurGrira = "pkg_errors.pro_check_have_sidur_grira";
        public const string cProGetShgiotNoActive = "pkg_errors.pro_get_shgiot_no_active";
        public const string cProGetPremiaOvdimLechishuv = "pkg_batch.pro_get_ovdim_lehishuv_premiot";
        public const string cProUpdateChishuvPremia = "pkg_batch.pro_update_chishuv_premia";
        public const string cProInsertMisparIshiSugChishuv = "pkg_batch.pro_ins_misparishi_sug_chishuv";

        private static List<int> SpecialSidurim = new List<int> { 99200 };


        public static void UpdateCardStatus(int iMisparIshi, DateTime dCardDate, clGeneral.enCardStatus oCardStatus, int iUserId)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_status", ParameterType.ntOracleInteger, oCardStatus.GetHashCode(), ParameterDir.pdInput);
                oDal.AddParameter("p_user_id", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);
              
                oDal.ExecuteSP(clDefinitions.cProUpdCardStatus);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public static bool CheckShaaton(DataTable dtSugeyYamimMeyuchadim, int iSugYom, DateTime dTaarich)
        {
            if ((dTaarich.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
                return true;
            else if (dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom).Length > 0)
            {
                return (dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["Shbaton"].ToString() == "1") ? true : false;
            }
            else return false;
        }

        public DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateLogBakasha(long lRequestNum, int iHuavraLesachar, DateTime dTarHaavaraLesachar)
        {
            clDal oDal = new clDal();

            try
            {

                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lRequestNum, ParameterDir.pdInput);

                oDal.AddParameter("p_status", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                oDal.AddParameter("p_huavra_lesachar", ParameterType.ntOracleInteger, iHuavraLesachar, ParameterDir.pdInput);
                oDal.AddParameter("p_zman_siyum", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_haavara_lesachar", ParameterType.ntOracleDate, dTarHaavaraLesachar, ParameterDir.pdInput);

                oDal.ExecuteSP(cProUpdBakasha);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void UpdateLogBakasha(long lRequestNum, DateTime dZmanSiyum, int iStatus)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lRequestNum, ParameterDir.pdInput);
                oDal.AddParameter("p_status", ParameterType.ntOracleInteger, iStatus, ParameterDir.pdInput);
                oDal.AddParameter("p_huavra_lesachar", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                oDal.AddParameter("p_zman_siyum", ParameterType.ntOracleDate, dZmanSiyum, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_haavara_lesachar", ParameterType.ntOracleDate, null, ParameterDir.pdInput);

                oDal.ExecuteSP(cProUpdBakasha);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateLogBakasha(long lRequestNum, DateTime dZmanSiyum, int iStatus, int iHuavraLesachar, DateTime dTarHaavaraLesachar, int ishurHilan)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lRequestNum, ParameterDir.pdInput);

                if (dZmanSiyum != DateTime.MinValue)
                    oDal.AddParameter("p_zman_siyum", ParameterType.ntOracleDate, dZmanSiyum, ParameterDir.pdInput);
                else oDal.AddParameter("p_zman_siyum", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                
                if (iStatus > 0)
                    oDal.AddParameter("p_status", ParameterType.ntOracleInteger, iStatus, ParameterDir.pdInput);
                else oDal.AddParameter("p_status", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);

                if (iHuavraLesachar>-1)
                    oDal.AddParameter("p_huavra_lesachar", ParameterType.ntOracleInteger, iHuavraLesachar, ParameterDir.pdInput);
                else oDal.AddParameter("p_huavra_lesachar", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);

                if (dTarHaavaraLesachar != DateTime.MinValue)
                    oDal.AddParameter("p_tar_haavara_lesachar", ParameterType.ntOracleDate, dTarHaavaraLesachar, ParameterDir.pdInput);
                else oDal.AddParameter("p_tar_haavara_lesachar", ParameterType.ntOracleDate, null, ParameterDir.pdInput);

                if (ishurHilan>-1)
                    oDal.AddParameter("p_ishur_hilan", ParameterType.ntOracleInteger, ishurHilan, ParameterDir.pdInput);
                else
                    oDal.AddParameter("p_ishur_hilan", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);

                oDal.ExecuteSP(cProUpdBakashaAllfields);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetPeiluyotFromTnua(int iMisparIshi, DateTime dCardDate)
        {
            DataTable _Peiluyot;
            string sCacheKey = iMisparIshi + dCardDate.ToShortDateString();
            clKavim _Kavim = new clKavim();
            try
            {
                _Peiluyot = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
            }
            catch (Exception ex)
            {
                _Peiluyot = null;
            }

            if (_Peiluyot == null)
            {
                _Peiluyot = _Kavim.GetKatalogKavim(iMisparIshi, dCardDate, dCardDate);
                HttpRuntime.Cache.Insert(sCacheKey, _Peiluyot, null, DateTime.MaxValue, TimeSpan.FromMinutes(int.Parse((ConfigurationSettings.AppSettings["PeilyutCacheTimeOutMinutes"]))));
            }

            return _Peiluyot;
        }

        //public OrderedDictionary BuildSidurimPeiluyot(int _iMisparIshi, DateTime _dCardDate,ref int iLastMisaprSidur, out  OrderedDictionary _htSpecialEmployeeDetails) //, out  OrderedDictionary _htEmployeeDetailsWithCancled
        //{
        //    DataTable dtDetails;
        //    OrderedDictionary htEmployeeDetails= new OrderedDictionary();
        //    _htSpecialEmployeeDetails = new OrderedDictionary();
        //    //_htEmployeeDetailsWithCancled = new OrderedDictionary();
        //    //Get Oved Details
        //    dtDetails = GetOvedDetails(_iMisparIshi, _dCardDate);
        //    if (dtDetails.Rows.Count > 0)
        //    {
        //        //Insert Oved Details to Class
        //        htEmployeeDetails = InsertEmployeeDetails(false, dtDetails, _dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails);//, out  _htEmployeeDetailsWithCancled
        //    }
        //    return htEmployeeDetails;
        //}

        public OrderedDictionary InsertEmployeeDetails(bool bInsertToShguim, DataTable dtDetails, 
                                                       DateTime dCardDate, ref int iLastMisaprSidur, 
                                                       out OrderedDictionary htSpecialEmployeeDetails,
                                                       ref OrderedDictionary htFullSidurimDetails)
        {
            int iMisparSidur, iPeilutMisparSidur;
            int iKey = 0;
            int i = 1;
            int iMisparSidurPrev = 0;
            DateTime dShatHatchala=DateTime.MinValue;
            DateTime dShatHatchalaPrev = new DateTime();
            clSidur oSidur = new clSidur();
            //clSidur oSidurWithCanceld = new clSidur();
            clPeilut oPeilut = new clPeilut();
            OrderedDictionary htEmployeeDetails = new OrderedDictionary();
            htSpecialEmployeeDetails = new OrderedDictionary();
            //htEmployeeDetailsWithCancled = new OrderedDictionary();
            string sLastShilut="";
            DataTable dtPeiluyot;
            clKavim _Kavim = new clKavim();
            clKavim.enMakatType _MakatType;
           try
           {
           //נשלוף את נתוני הפעילויות לאותו יום
            dtPeiluyot = GetPeiluyotFromTnua(int.Parse(dtDetails.Rows[0]["Mispar_ishi"].ToString()), dCardDate);
             
            //HashTable-הכנסת כל הסידורים והפעילויות של עובד בתאריך הנתון ל
            foreach (DataRow dr in dtDetails.Rows)
            {
                
                    iMisparSidur = int.Parse(dr["Mispar_Sidur"].ToString());
                    dShatHatchala = DateTime.Parse(dr["shat_hatchala"].ToString());
                
                if ((iMisparSidur != iMisparSidurPrev) || (iMisparSidur == iMisparSidurPrev) && (dShatHatchala != dShatHatchalaPrev))
                {
                    iKey = 1;
                    i++;
                    //נתונים ברמת סידור
                   
                    oSidur = new clSidur();
                    oSidur.AddEmployeeSidurim(dr,true);
                   
                    //oSidurWithCanceld = new clSidur();
                    //oSidurWithCanceld.AddEmployeeSidurim(dr,false);
                    //oSidurWithCanceld.iSugSidurRagil = oSidur.iSugSidurRagil;
                    //oSidurWithCanceld.bSidurRagilExists = oSidur.bSidurRagilExists;

                    if (SpecialSidurim.Contains(iMisparSidur))
                    {
                        //htSpecialEmployeeDetails.Add(int.Parse(string.Concat(i, iMisparSidur)), oSidur);
                        htSpecialEmployeeDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
                    }
                    //else if (oSidur.iBitulOHosafa == 1 || oSidur.iBitulOHosafa == 3)
                    //{
                    //    htEmployeeDetailsWithCancled.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm").Replace(":", ""), iMisparSidur)), oSidurWithCanceld);
                    //}
                    else
                    {
                        if (!bInsertToShguim || (bInsertToShguim && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iLebdikaShguim == 1))))                        
                            //htEmployeeDetails.Add(int.Parse(string.Concat(i, iMisparSidur)), oSidur);
                            htEmployeeDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
                            //htEmployeeDetailsWithCancled.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"),dShatHatchala.ToString("HH:mm").Replace(":", ""), iMisparSidur)), oSidurWithCanceld);

                        htFullSidurimDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
                    }
                    iMisparSidurPrev = iMisparSidur;
                    dShatHatchalaPrev = dShatHatchala;


                   
                }
                //נתוני פעילויות  
               
                    iPeilutMisparSidur = (System.Convert.IsDBNull(dr["peilut_mispar_sidur"]) ? 0 : int.Parse(dr["peilut_mispar_sidur"].ToString()));
                    if (iPeilutMisparSidur > 0)
                    {
                        oPeilut = new clPeilut(dCardDate);
                        oPeilut.dCardDate = dCardDate;
                        oPeilut.AddEmployeePeilut(iKey, dr, dtPeiluyot);
                        _MakatType = (clKavim.enMakatType)_Kavim.GetMakatType(oPeilut.lMakatNesia);
                        if (_MakatType == clKavim.enMakatType.mKavShirut)                        
                            sLastShilut = oPeilut.sShilut;
                        else if (_MakatType == clKavim.enMakatType.mVisut)
                            oPeilut.sShilut= sLastShilut;
                        //if (!(oSidur.iBitulOHosafa == 1 || oSidur.iBitulOHosafa == 3) && !(oPeilut.iBitulOHosafa == 1 || oPeilut.iBitulOHosafa == 3))
                        //{
                            oSidur.htPeilut.Add(iKey, oPeilut);
                            //oSidurWithCanceld.htPeilut.Add(iKey, oPeilut);
                        //}
                        //else
                        //{
                        //    oSidurWithCanceld.htPeilut.Add(iKey, oPeilut);
                        //}

                        //אם לפחות אחד מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                        if (oPeilut.bPeilutEilat)                       
                            oSidur.bSidurEilat = true;
                        
                        //אם לפחות פעילות אחת לא ריקה, נגדיר את הסידור כסידור לא ריק
                        if (!oSidur.bSidurNotEmpty)                        
                            oSidur.bSidurNotEmpty = oPeilut.bPeilutNotRekea;                        

                        iKey++;
                    }
               
            }
            iLastMisaprSidur = int.Parse(dtDetails.Rows[dtDetails.Rows.Count - 1]["mispar_sidur"].ToString());
              
            return htEmployeeDetails;
            }
        catch (Exception ex)
            {
                throw new Exception("Error in InsertEmployeeDetails:"  + " " + ex.Message);
             
            }
        }

        public static void UpdateSidurimOvdim(COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd)
        {//COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd
            clDal oDal = new clDal();

            try
            {
                //COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd1 = new COLL_SIDURIM_OVDIM();
                //OBJ_SIDURIM_OVDIM oObjSidurimOvdim1 = new OBJ_SIDURIM_OVDIM();
                //oObjSidurimOvdim1.CHARIGA = 5;
                //oObjSidurimOvdim1.OUT_MICHSA = 6;
                //oCollSidurimOvdimUpd1.Add(oObjSidurimOvdim1);

                //OBJ_TEST oObjTest = new OBJ_TEST();
                //oObjTest.AA = 2;
                //oObjTest.BB = "5";
                //COLL_TEST oCollTest = new COLL_TEST();
                //oCollTest.Add(oObjTest);

                //OBJ_SIDURIM_OVDIM oObjSidurimOvdim = new OBJ_SIDURIM_OVDIM();
                //oObjSidurimOvdim.CHARIGA = 6;
                //oObjSidurimOvdim.OUT_MICHSA = 4;
                //COLL_SIDURIM_OVDIM oCollSidurimOvdim = new COLL_SIDURIM_OVDIM();
                //oCollSidurimOvdim.Add(oObjSidurimOvdim);

                // oObjTest.AA = 8;
                //oObjTest.BB = "1";
                //oCollTest.Value[oCollTest.Value.Length-1].AA = 8;
                // oCollTest.Add(oObjTest);
                // oCollSidurimOvdimUpd1.Add(oObjSidurimOvdim1);
                //oDal.TestRunSP(oCollSidurimOvdim);
                oDal.AddParameter("p_coll_sidurim_ovdim", ParameterType.ntOracleArray, oCollSidurimOvdimUpd, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                //oDal.AddParameter("p_coll_sidurim_ovdim", ParameterType.ntOracleObject, oObjSidurimOvdimUpd, ParameterDir.pdInput, "OBJ_SIDURIM_OVDIM");
                oDal.ExecuteSP(cProUpdSidurimOvdim);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateYameyAvodaOvdim(COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_coll_yamey_avoda_ovdim", ParameterType.ntOracleArray, oCollYameyAvodaUpd, ParameterDir.pdInput, "COLL_YAMEY_AVODA_OVDIM");
                oDal.ExecuteSP(cProUpdYameyAvodaOvdim);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertSidurimOvdim(COLL_SIDURIM_OVDIM oCollSidurimOvdimIns)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_coll_sidurim_ovdim", ParameterType.ntOracleArray, oCollSidurimOvdimIns, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.ExecuteSP(cProInsSidurimOvdim);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertPeilutOvdim(COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimIns, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.ExecuteSP(clGeneral.cProInsPeilutOvdim);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdatePeilutOvdim(COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimUpd, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.ExecuteSP(cProUpdPeilutOvdim);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeletePeilutOvdim(COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimDel, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.ExecuteSP(cProDelPeilutOvdim);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ShinuyKelet(COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd, COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd, COLL_SIDURIM_OVDIM oCollSidurimOvdimIns, COLL_SIDURIM_OVDIM oCollSidurimOvdimDel,
                                       COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd, COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns, COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel)
        {
            clDal oDal = new clDal();
            try
            {
                //EventLog kdsLog = new EventLog();
                //kdsLog.Source = "KDS";

                oDal.AddParameter("p_coll_yamey_avoda_ovdim", ParameterType.ntOracleArray, oCollYameyAvodaUpd, ParameterDir.pdInput, "COLL_YAMEY_AVODA_OVDIM");
                oDal.AddParameter("p_coll_sidurim_ovdim_upd", ParameterType.ntOracleArray, oCollSidurimOvdimUpd, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.AddParameter("p_coll_sidurim_ovdim_ins", ParameterType.ntOracleArray, oCollSidurimOvdimIns, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.AddParameter("p_coll_sidurim_ovdim_del", ParameterType.ntOracleArray, oCollSidurimOvdimDel, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.AddParameter("p_coll_obj_peilut_ovdim_upd", ParameterType.ntOracleArray, oCollPeilutOvdimUpd, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.AddParameter("p_coll_obj_peilut_ovdim_ins", ParameterType.ntOracleArray, oCollPeilutOvdimIns, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.AddParameter("p_coll_obj_peilut_ovdim_del", ParameterType.ntOracleArray, oCollPeilutOvdimDel, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                //kdsLog.WriteEntry("10.1", EventLogEntryType.Error);
                oDal.ExecuteSP(cProShinuyKelet);
                //kdsLog.WriteEntry("10.2", EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clMail omail;
                string[] RecipientsList = (ConfigurationManager.AppSettings["MailErrorWorkCard"].ToString()).Split(';');
                RecipientsList.ToList().ForEach(recipient =>
                {
                    omail = new clMail(recipient, "תקלה בשמירת נתונים למספר אישי: " + oCollYameyAvodaUpd.Value[0].MISPAR_ISHI + "  תאריך:" + oCollYameyAvodaUpd.Value[0].TAARICH.ToShortDateString(), ex.Message);
                    omail.SendMail();
                });
               
                throw ex;
            }
        }
        public static DataTable GetSugeySidur()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני סוג סידור:                 
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetSugSidurMeafyenim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetTmpSidurimMeyuchadim(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
              
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetTmpSidurimMeyuchadim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetApprovalToEmploee(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetApprovalToEmploee, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetIdkuneyRashemet(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetIdkuneyRashemet, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetApprovalErrors(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetApprovalErrors, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetTmpMeafyeneyElements(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetMeafyeneyElements, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static float GetSidurTimeInMinuts(clSidur oSidur)
        {
            float fSidurTime = 0;
            try
            {   //מחזיר את זמן הסידור בדקות

                if ((!(string.IsNullOrEmpty(oSidur.sShatGmar))) && (!(string.IsNullOrEmpty(oSidur.sShatHatchala))))
                {
                    fSidurTime = (float)(oSidur.dFullShatGmar - oSidur.dFullShatHatchala).TotalMinutes;
                }
                return fSidurTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetSugMishmeret(int iMisparIshi,DateTime Taarich,int  iSugYom,DateTime dShatHatchalaSidur,DateTime dShatGmarSidur,clParameters oParameters)
        {
            int iSugMishmeret;
            DateTime dTemp1,dTemp2;
            try { 
                      iSugMishmeret = clGeneral.enSugMishmeret.Boker.GetHashCode();
                      if (iSugYom== 1 || iSugYom == 3 || iSugYom == 4 || iSugYom == 5)
                      {
                  
                          dTemp1 = oParameters.dMinStartMishmeretMafilimChol;
                          dTemp2 = oParameters.dMaxStartMishmeretMafilimChol;
                          //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 01 או 03 או 04 או 05 וגם [שעת התחלה ראשונה] >= 11:00 וקטן מ- 17:00 וגם [שעת גמר אחרונה] > 18 אזי [סוג משמרת] = צהריים 
                          if (dShatHatchalaSidur >= dTemp1 && dShatHatchalaSidur < dTemp2)
                          {
                              if (dShatGmarSidur >oParameters.dMinEndMishmeretMafilimChol)
                              {
                                  iSugMishmeret = clGeneral.enSugMishmeret.Tzaharim.GetHashCode();
                              }
                          }
                          dTemp1 =oParameters.dMinEndMishmeretMafilimLilaChol1;
                          dTemp2 = oParameters.dMinStartMishmeretMafilimLilaChol;
                          //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 01 או 03 או 04 או 05 וגם [שעת התחלה ראשונה] >= 17:00 וגם [שעת גמר אחרונה] > 21:00 אזי [סוג משמרת] = לילה
                          if (dShatHatchalaSidur > dTemp2 && dShatGmarSidur > dTemp1)
                          {
                              iSugMishmeret = clGeneral.enSugMishmeret.Liyla.GetHashCode();
                           }
                      }
                      //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 11 או 13 או 14 או 15 או 16 או 17 או 18 וגם [שעת גמר אחרונה] > 13:00 אזי [סוג משמרת] = צהריים
                      if (iSugYom == 11 || iSugYom == 13 || (iSugYom >= 14 && iSugYom <= 15))
                      {
                          dTemp1 = oParameters.dMinEndMishmeretMafilimShishi;
                          if (dShatGmarSidur >= dTemp1)
                          {
                              iSugMishmeret = clGeneral.enSugMishmeret.Tzaharim.GetHashCode();
                          }
                      }

                      dTemp1 = oParameters.dMinEndMishmeretMafilimLilaChol2;
                      //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 01 או 03 או 04 או 05 וגם [שעת גמר אחרונה] >= 23:15 אזי [סוג משמרת] = לילה
                      if (dShatGmarSidur >= dTemp1)
                      {
                          iSugMishmeret = clGeneral.enSugMishmeret.Liyla.GetHashCode();
                      }
                 
                return iSugMishmeret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static float GetTimeBetweenTwoSidurimInMinuts(clSidur oPrevSidur, clSidur oSidur)
        {
            float fDiffSidurTime = 0;
            try
            {   //מחזיר הפרש זמן בין שני סידורים חופפים

                if ((!(string.IsNullOrEmpty(oPrevSidur.sShatGmar))) && (!(string.IsNullOrEmpty(oSidur.sShatHatchala))))
                {
                    fDiffSidurTime =(float) (oSidur.dFullShatHatchala - oPrevSidur.dFullShatGmar).TotalMinutes;
                }
                return fDiffSidurTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static int GetTimeInMinuts(string sTime)
        //{

        //    int iMinutes = 0;
        //    try
        //    {   //Get hour (string) XX:XX and return the time in minutes

        //        if ((!(string.IsNullOrEmpty(sTime))))
        //        {
        //            if (sTime.IndexOf(char.Parse(":")) > -1)
        //            {
        //                sTime = sTime.Split(' ')[0].Replace(":", "").PadLeft(4, char.Parse("0"));
        //            }
        //            iMinutes = (int.Parse(sTime.Substring(0, 2)) * 60) + int.Parse(sTime.Substring(2, 2));
        //        }
        //        return iMinutes;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static DataRow[] GetOneSugSidurMeafyen(int iSugSidur, DateTime dDate, DataTable dtSugSidur)
        {   //הפונקציה מקבלת קוד סוג סידור ותאריך ומחזירה את מאפייני סוג הסידור

            DataRow[] dr;


            dr = dtSugSidur.Select(string.Concat("sug_sidur=", iSugSidur.ToString(), " and Convert('", dDate.ToShortDateString(), "','System.DateTime') >= me_tarich and Convert('", dDate.ToShortDateString(), "', 'System.DateTime') <= ad_tarich"));
            // dr = dtSugSidur.Select(string.Concat("sug_sidur=", iSugSidur.ToString()));

            return dr;
        }

        public static DataRow[] GetOneSibotLedivuachYadani(int iKodSiba, ref DataTable drSidurSibotLedivuchYadani)
        {   //הפונקציה מקבלת קוד סיבה ומחזירה גורמים לביטול  

            DataRow[] dr;

            dr = drSidurSibotLedivuchYadani.Select(string.Concat("kod_siba=", iKodSiba.ToString()));

            return dr;
        }

        public static DateTime GetMinDate(DateTime Date1, DateTime Date2)
        {
            if (Date1 > Date2)
            {
                return Date2;
            }
            else return Date1;
        }

        public static string GetMasharCarNumbers(OrderedDictionary htEmployeeDetails)
        {
            string sCarNumbers = "";
            clPeilut oPeilut;
            clSidur oSidur;

            //נשרשר את כל מספרי הרכב, כדי לפנות למש"ר עם פחות נתונים
            for (int i = 0; i < htEmployeeDetails.Count; i++)
            {
                oSidur = (KdsBatch.clSidur)htEmployeeDetails[i];
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (clPeilut)oSidur.htPeilut[j];
                    sCarNumbers += oPeilut.lOtoNo.ToString() + ",";
                }
            }

            if (sCarNumbers.Length > 0)
            {
                sCarNumbers = sCarNumbers.Substring(0, sCarNumbers.Length - 1);
            }
            return sCarNumbers;
        }
        public static DataSet GetErrorsForFields(bool bProfileRashemet, int iMisparIshi, DateTime dCardDate,
                                                 int iMisparSidur, DateTime dFullShatHatchala, 
                                                 DateTime dPeilutShatYetiza, int iMisparKnisa, 
                                                 string sFieldName)
        {
            clBatchManager oBatchManager = new clBatchManager(iMisparIshi, dCardDate);
            DataSet ds;
            int iErrorNum;
            int iShgiotLeoved = bProfileRashemet ? 0 : 1; //אם םרופיל של רשמת/רשמת על/מנהל מערכת- לא נסנן שגיאות לפי שדה 'שגיאות לעובד' אחרת נראה שגיאות לעובד בלבד
            ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);

            //רוטינת שגויים
            oBatchManager.MainOvedErrors(iMisparIshi, dCardDate);
            //רמת פעילות
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                iErrorNum = int.Parse(dr["KOD_SHGIA"].ToString());
                if (KdsLibrary.BL.clWorkCard.IsErrorExists(oBatchManager.dtErrors, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, dFullShatHatchala, dPeilutShatYetiza, iMisparKnisa))
                {
                    dr["SHOW_ERROR"] = "1";
                    dr["ERR_KEY"] = string.Concat(dr["ERR_KEY"].ToString(), "|", iMisparSidur.ToString(), "|", dFullShatHatchala.ToString(), "|", dPeilutShatYetiza.ToString(), "|", iMisparKnisa.ToString());
                    dr["USER_PROFILE"] = iShgiotLeoved;
                }
            }

            return ds;
        }
        public static DataSet GetErrorsForFields(bool bProfileRashemet,DataTable dtErrors, int iMisparIshi, DateTime dCardDate,
                                                 int iMisparSidur, DateTime dFullShatHatchala, DateTime dPeilutShatYetiza, 
                                                 int iMisparKnisa, string sFieldName)
        {
            DataSet ds;
            int iErrorNum;
            int iShgiotLeoved = bProfileRashemet ? 0 : 1; //אם םרופיל של רשמת/רשמת על/מנהל מערכת- לא נסנן שגיאות לפי שדה 'שגיאות לעובד' אחרת נראה שגיאות לעובד בלבד
            //string sCacheKey = sFieldName + iShgiotLeoved;
            //try
            //{
            //    ds = (DataSet)HttpRuntime.Cache.Get(sCacheKey);
            //}
            //catch (Exception ex)
            //{
            //    ds = null;
            //}

            //if (ds == null)
            //{
            //    ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);
            //    HttpRuntime.Cache.Insert(sCacheKey, ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
            //}

            ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);
            //רמת פעילות
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                iErrorNum = int.Parse(dr["KOD_SHGIA"].ToString());
                if (KdsLibrary.BL.clWorkCard.IsErrorExists(dtErrors, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, dFullShatHatchala, dPeilutShatYetiza, iMisparKnisa))
                {
                    dr["SHOW_ERROR"] = "1";
                    dr["ERR_KEY"] = string.Concat(dr["ERR_KEY"].ToString(), "|", iMisparSidur.ToString(), "|", dFullShatHatchala.ToString(), "|", dPeilutShatYetiza.ToString(), "|", iMisparKnisa.ToString());
                    dr["USER_PROFILE"] = iShgiotLeoved;
                }
            }

            return ds;
        }
        public static DataSet GetErrorsForFields(bool bProfileRashemet, int iMisparIshi, DateTime dCardDate,
                                                 int iMisparSidur, DateTime dFullShatHatchala, 
                                                 string sFieldName, ref DataTable dtErr)
        {
            clBatchManager oBatchManager = new clBatchManager(iMisparIshi, dCardDate);
            DataSet ds;
            int iErrorNum;
            int iShgiotLeoved = bProfileRashemet ? 0 : 1;//אם םרופיל של רשמת/רשמת על/מנהל מערכת- לא נסנן שגיאות לפי שדה 'שגיאות לעובד' אחרת נראה שגיאות לעובד בלבד 
            ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);

            //רוטינת שגויים
            oBatchManager.MainOvedErrors(iMisparIshi, dCardDate);
            dtErr = oBatchManager.dtErrors;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                iErrorNum = int.Parse(dr["KOD_SHGIA"].ToString());
                if (KdsLibrary.BL.clWorkCard.IsErrorExists(oBatchManager.dtErrors, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, dFullShatHatchala))
                {
                    dr["SHOW_ERROR"] = "1";
                    dr["ERR_KEY"] = string.Concat(dr["ERR_KEY"].ToString(), "|", iMisparSidur.ToString(), "|", dFullShatHatchala.ToString());
                    dr["USER_PROFILE"] = iShgiotLeoved;
                }
            }

            return ds;
        }

        public static DataSet GetErrorsForFields(bool bProfileRashemet, DataTable dtErrors, int iMisparIshi, DateTime dCardDate,
                                                 int iMisparSidur, DateTime dFullShatHatchala, string sFieldName)
        {
            DataSet ds;
            int iErrorNum;
            int iShgiotLeoved = bProfileRashemet ? 0 : 1; //אם םרופיל של רשמת/רשמת על/מנהל מערכת- לא נסנן שגיאות לפי שדה 'שגיאות לעובד' אחרת נראה שגיאות לעובד בלבד
            //string sCacheKey = sFieldName + iShgiotLeoved;
            //try
            //{
            //    ds = (DataSet)HttpRuntime.Cache.Get(sCacheKey);
            //}
            //catch (Exception ex)
            //{
            //    ds = null;
            //}

            //if (ds == null)
            //{
            //    ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);
            //    HttpRuntime.Cache.Insert(sCacheKey, ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
            //}

            ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                iErrorNum = int.Parse(dr["KOD_SHGIA"].ToString());
                if (KdsLibrary.BL.clWorkCard.IsErrorExists(dtErrors, iErrorNum, iMisparIshi, dCardDate, iMisparSidur, dFullShatHatchala))
                {
                    dr["SHOW_ERROR"] = "1";
                    dr["ERR_KEY"] = string.Concat(dr["ERR_KEY"].ToString(), "|", iMisparSidur.ToString(), "|", dFullShatHatchala.ToString());
                    dr["USER_PROFILE"] = iShgiotLeoved; //0 - rashemet/rashemt al/administrator  1 - oved ragil
                }
            }

            return ds;
        }

        public static DataSet GetErrorsForFields(bool bProfileRashemet,DataTable dtErrors, int iMisparIshi, DateTime dCardDate, string sFieldName)
        {
            DataSet ds;
            int iErrorNum;
            int iShgiotLeoved = bProfileRashemet ? 0 : 1; //אם םרופיל של רשמת/רשמת על/מנהל מערכת- לא נסנן שגיאות לפי שדה 'שגיאות לעובד' אחרת נראה שגיאות לעובד בלבד
            //string sCacheKey = sFieldName + iShgiotLeoved;
            //try
            //{
            //    ds = (DataSet)HttpRuntime.Cache.Get(sCacheKey);
            //}
            //catch (Exception ex)
            //{
            //    ds = null;
            //}

            //if (ds == null)
            //{
            //    ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);
            //    HttpRuntime.Cache.Insert(sCacheKey, ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
            //}

            ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                iErrorNum = int.Parse(dr["KOD_SHGIA"].ToString());
                if (KdsLibrary.BL.clWorkCard.IsErrorExists(dtErrors, iErrorNum, iMisparIshi, dCardDate))
                {
                    dr["SHOW_ERROR"] = "1";
                    dr["USER_PROFILE"] = iShgiotLeoved;
                }
            }

            return ds;
        }

        public static DataSet GetErrorsForFields(bool bProfileRashemet, int iMisparIshi, DateTime dCardDate, string sFieldName)
        {
            clBatchManager oBatchManager = new clBatchManager(iMisparIshi, dCardDate);
            DataSet ds;
            int iErrorNum;
            int iShgiotLeoved = bProfileRashemet ? 0 :1; //אם םרופיל של רשמת/רשמת על/מנהל מערכת- לא נסנן שגיאות לפי שדה 'שגיאות לעובד' אחרת נראה שגיאות לעובד בלבד
            ds = GetErrorForFieldFromDB(sFieldName, iShgiotLeoved);

            //רוטינת שגויים
            oBatchManager.MainOvedErrors(iMisparIshi, dCardDate);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                iErrorNum = int.Parse(dr["KOD_SHGIA"].ToString());
                if (KdsLibrary.BL.clWorkCard.IsErrorExists(oBatchManager.dtErrors, iErrorNum, iMisparIshi, dCardDate))
                {
                    dr["SHOW_ERROR"] = "1";
                    dr["USER_PROFILE"] = iShgiotLeoved;
                }
            }

            return ds;
        }
        private static DataSet GetErrorForFieldFromDB(string sFieldName, int iShgiotLeoved)
        {
            clDal _Dal = new clDal();
            DataSet ds = new DataSet();
            try
            {
                _Dal.AddParameter("p_field_name", ParameterType.ntOracleVarchar, sFieldName, ParameterDir.pdInput, 100);
                _Dal.AddParameter("p_shgiot_leoved", ParameterType.ntOracleInteger, iShgiotLeoved, ParameterDir.pdInput);
                _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetErrorsForField, ref ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetErrorsNoActive()
        {
            clDal _Dal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(cProGetShgiotNoActive, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemet)
        {
            clDal Dal = new clDal();
            try
            {
                Dal.AddParameter("p_coll_idkun_rashemet", ParameterType.ntOracleArray, oCollIdkunRashemet, ParameterDir.pdInput, "COLL_IDKUN_RASHEMET");
                Dal.ExecuteSP(clGeneral.cProUpdIdkunRashemet);
            }
            catch (Exception ex)
            {
                clMail omail;
                string[] RecipientsList = (ConfigurationManager.AppSettings["MailErrorWorkCard"].ToString()).Split(';');
                RecipientsList.ToList().ForEach(recipient =>
                {
                    omail = new clMail(recipient, "תקלה בשמירת עדכוני רשמת למספר אישי: " + oCollIdkunRashemet.Value[0].MISPAR_ISHI + "  תאריך:" + oCollIdkunRashemet.Value[0].TAARICH.ToShortDateString(), ex.Message);
                    omail.SendMail();
                });

                throw ex;
            } 
        }

        public static void UpdateAprrovalErrors(COLL_SHGIOT_MEUSHAROT oCollShgiotMeusharot)
        {
            clDal Dal = new clDal();
            try
            {
                Dal.AddParameter("p_coll_shgiot_meusharot", ParameterType.ntOracleArray, oCollShgiotMeusharot, ParameterDir.pdInput, "COLL_SHGIOT_MEUSHAROT");
                Dal.ExecuteSP(clDefinitions.cProUpdApprovalErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static bool IsExceptionAllowedForSidurMyuchad(ref clSidur oSidur, ref string sCharigaType, clParameters KdsParameters)
        //{
        //    bool bExceptionAllowed = false;

        //    //ברמת הסידור - רק לסידורים מיוחדים שיש להם ערך 1 (זכאי) במאפיין 35 (זכאות לחריגה משעות כניסה ויציאה
        //    if ((oSidur.bSidurMyuhad) && (oSidur.sZakaiLeChariga == clGeneral.enMeafyenSidur35.enCharigaZakai.GetHashCode().ToString())
        //        && ((oSidur.sShaonNochachut == clGeneral.enShaonNochachut.enMinhal.GetHashCode().ToString())))
        //    {
        //        /*
        //        ג. בדיקת מאפייני העובד (רלוונטי רק לסידור שנוסף בכע) - מותר לעדכן את שדה חריגה במקרים הבאים: 
        //        לגבי כל התנאים המפורטים בהמשך רלוונטי רק לסידור מיוחד עם מאפיין 54 וערך 1 (מנהל)                
        //            ב. לעובד אין מאפיינים 3 ו- 4 
        //        ג.  יום חול וערב חג

        //        2. א.  לעובד אין מאפיינים 5 ו- 6 
        //        ב.  יום שישי (שישי, לא ערב חג)

        //        3. א.  לעובד אין מאפיינים 7 ו- 8 
        //        ב.  יום שבת/שבתון

        //         */
        //         if ((oSidur.sErevShishiChag.Equals("1")) || 
        //             ((oSidur.sSidurDay!= clGeneral.enDay.Shabat.GetHashCode().ToString()) &&
        //             (oSidur.sSidurDay!= clGeneral.enDay.Shishi.GetHashCode().ToString()) &&
        //             (!oSidur.sErevShishiChag.Equals("1")) && (!oSidur.sShabaton.Equals("1"))))

        //         if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
        //         if ((oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oSidur.sShabaton.Equals("1")))
                 
        //        ////יום שישי או ערב חג
        //        ////ביום שישי/ערב חג לעובד ללא מאפיינים 5 ו- 6 וגם TB_Sidurim_Ovedim.KOD_SIBA_LO_LETASHLUM=5   (עבודה בשישי ללא הרשאה).
        //        //if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
        //        //{
        //        //    if ((!MeafyenyOved.Meafyen5Exists) && (!MeafyenyOved.Meafyen6Exists) && (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkAtFridayWithoutPremission.GetHashCode()))
        //        //    {
        //        //        bExceptionAllowed = true;
        //        //        sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
        //        //    }
        //        //}
        //        ////ביום שבת/שבתון לעובד ללא מאפיינים 7 ו- 8 וגם TB_Sidurim_Ovedim.KOD_SIBA_LO_LETASHLUM=4  (עבודה בשבתון ללא הרשאה).
        //        //if ((oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oSidur.sShabaton.Equals("1")))
        //        //{
        //        //    if ((!MeafyenyOved.Meafyen7Exists) && (!MeafyenyOved.Meafyen8Exists) && (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkAtSaturdayWithoutPremission.GetHashCode()))
        //        //    {
        //        //        bExceptionAllowed = true;
        //        //        sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
        //        //    }
        //        //}
        //    }
        //    return bExceptionAllowed;
        //}

        public static bool IsExceptionAllowed(ref clSidur oSidur, ref string sCharigaType, clParameters KdsParameters)
        {
            bool bExceptionAllowed = false;

            //DateTime dShatHatchalaLetashlum= new DateTime();
            //DateTime dShatGmarLetashlum= new DateTime();
            //clBatchManager oBatchManager = new clBatchManager();

            //נאפשר חריגה רק במידה ושעת ההתחלה שהוזנה פחות שעת מאפיין שעת התחלה גדול מפרמטר 41
            try
            {
                //נקרא את מאפייני שעת גמר ושעת התחלה לתשלום           
                //ברמת הסידור - רק לסידורים מיוחדים שיש להם ערך 1 (זכאי) במאפיין 35 (זכאות לחריגה משעות כניסה ויציאה

                if ((oSidur.bSidurMyuhad) && (oSidur.sZakaiLeChariga == clGeneral.enMeafyenSidur35.enCharigaZakai.GetHashCode().ToString()))
                {
                    /* ברמת העובד -
                       א. עובד עם מאפיינים מתאימים ליום העבודה- 
                       יום חול -  מאפיינים 3 ו-  4 עבור יום חול (לכל העובדים)
                       ערב שישי/ערב חג  - מאפיינים 5 ו- 6 
                       עבור ערב שבת/שבתון  - מאפיינים 7 ו- 8 
                       1. שעת התחלה קטנה מהערך במאפיין  בשדה שעת התחלה לתשלום או שעת גמר גדולה מהערך בשדה במאפיין שעת גמר לתשלום (המאפיין המתאים לפי סוג היום) - אם שניהם לא מתקיימים, לחסום את השדה.
                    */
                    /*
                     . בדיקת שדה לא לתשלום (רלוונטי רק לסידור הנשלף מטבלת סידורים)  - 
                        הסידור לא לתשלום בגלל מאפיינים לא מתאימים ליום עבודה:
                        ניתן לעדכן חריגה לסידורים המסומנים לא לתשלום    1=Lo_letashlum וגם 
                        TB_Sidurim_Ovedim.KOD_SIBA_LO_LETASHLUM=4, 5, 10, 17
                        4  - עבודה בשבתון ללא הרשאה
                        5 - עבודה בשישי ללא הרשאה
                        17 -  עבודה ביום חול ללא הרשאה
                       10 -  סידור מחוץ לשעות מותרות לעובד
                     */

                    if ((oSidur.iLoLetashlum.Equals(1))
                        && ((oSidur.iKodSibaLoLetashlum.Equals(clGeneral.enLoLetashlum.SidurInNonePremissionHours.GetHashCode()))
                        || (oSidur.iKodSibaLoLetashlum.Equals(clGeneral.enLoLetashlum.WorkAtFridayWithoutPremission.GetHashCode()))
                        || (oSidur.iKodSibaLoLetashlum.Equals(clGeneral.enLoLetashlum.WorkAtSaturdayWithoutPremission.GetHashCode()))
                        || (oSidur.iKodSibaLoLetashlum.Equals(clGeneral.enLoLetashlum.WorkWitoutPremmision.GetHashCode()))
                        ))
                    {
                        bExceptionAllowed = true;
                        sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
                    }
                    else
                    {
                        //oBatchManager.GetOvedShatHatchalaGmar(oSidur.dFullShatGmar, MeafyenyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);
                        if (!String.IsNullOrEmpty(oSidur.sShatHatchala))
                        {
                            if (oSidur.dFullShatHatchala < oSidur.dFullShatHatchalaLetashlum)
                            {
                                /*אם תנאי 1 מתקיים, בודקים
                                    אם המרווח בין החתמת כניסה/יציאה לשעת התחלה/גמר לתשלום מאפייני שעת התחלה/גמר מותרת שווה או גדול מהערך המוגדר בפרמטר 41 (זמן חריגה התחלה / גמר על חשבון העובד). יש לבדוק את מאפייני שעת התחלה/גמר מותרת בהתאם לסוג היום.
                                    כמובן יש לבדוק שהמרווח הוא מ"הכיוון הנכון", כלומר עבור עובד שאמור להתחיל לעבוד בשעה 07:00, שעת התחלה לתשלום היא 07:20 החתים שעון בשעה 07:20, כמובן שאסור לבקש חריגה משעת התחלה. בדיקה דומה יש לבצע עבור בקשה חריגה משעת גמר. במידה וביקשו ערך שגוי, יש להציג הודעה מתאימה ב- pop-up : "אין חריגה משעת התחלה או משעת גמר 
                                */
                                if ((oSidur.dFullShatHatchalaLetashlum - oSidur.dFullShatHatchala).TotalMinutes >= KdsParameters.iZmanChariga)
                                {
                                    bExceptionAllowed = true;
                                    sCharigaType = clGeneral.enCharigaValue.CharigaKnisa.GetHashCode().ToString();//חריגה רק מהתחלה
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(oSidur.sShatGmar))
                        {
                            if (oSidur.dFullShatGmar > oSidur.dFullShatGmarLetashlum)
                            {
                                if ((oSidur.dFullShatGmar - oSidur.dFullShatGmarLetashlum).TotalMinutes >= KdsParameters.iZmanChariga)
                                {

                                    if (bExceptionAllowed)
                                        sCharigaType = clGeneral.enCharigaValue.CharigaKnisaYetiza.GetHashCode().ToString();  //חריגה  ומגמר מהתחלה                          
                                    else
                                    {
                                        bExceptionAllowed = true;
                                        sCharigaType = clGeneral.enCharigaValue.CharigaYetiza.GetHashCode().ToString();   //חריגה מגמר
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
                }


                return bExceptionAllowed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsHashlamaAllowed(ref clSidur oSidur, DataRow[] drSugSidur, clOvedYomAvoda OvedYomAvoda)
        {
            bool bHashlamaAllowed = true;

            //לחסום את השדה לסידורי מטלה
            if (oSidur.iMisparSidur < 1000)            
                bHashlamaAllowed = false;
            
            //לא נאפשר השלמה לעובד מאגד תעבורה
            if (OvedYomAvoda.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())            
                bHashlamaAllowed = false;            
            //סידור מיוחד - לסידור מאפיין 40        
            if (oSidur.bSidurMyuhad)
            {
                if (!oSidur.bHashlamaKodExists)                
                    bHashlamaAllowed = false;                
            }
            else //סידור רגיל  //40 (זכאי להשלמה עבור הסידור) בטבלת מאפיינים סידורים מיוחדים. 
            {
                if ((drSugSidur.Length) > 0)
                {
                    int iHashlamaAllowed = String.IsNullOrEmpty(drSugSidur[0]["ZAKAY_LEHASHLAMA_AVUR_SIDUR"].ToString()) ? 0 : int.Parse(drSugSidur[0]["ZAKAY_LEHASHLAMA_AVUR_SIDUR"].ToString());
                    if (!(iHashlamaAllowed > 0))                    
                        bHashlamaAllowed = false;                    
                }
            }
            if (bHashlamaAllowed)
                if (IsSidurTimeBigOrEquallToHashlamaTime(ref oSidur))
                    bHashlamaAllowed = false;
                        
            return bHashlamaAllowed;
        }
        public static bool IsSidurTimeBigOrEquallToHashlamaTime(ref clSidur oSidur)
        {
            float fSidurTime;
            bool bSidurTimeBigger = false;

            if ((oSidur.sShatHatchala == "") || (oSidur.sShatGmar == "") || (oSidur.sShatHatchala == null) || (oSidur.sShatGmar == null))
                bSidurTimeBigger = true;
            else
            {
                fSidurTime = clDefinitions.GetSidurTimeInMinuts(oSidur);
                if (fSidurTime > 0)
                    bSidurTimeBigger = (fSidurTime / 60 >= clGeneral.enSugHashlama.enHashlama.GetHashCode());
                else
                    bSidurTimeBigger = false;
            }

            return bSidurTimeBigger;
        }
    //    public static clParameters GetParamInstance(DateTime dCardDate, int iSugYom)
    //    {
    //        string sCacheKey = dCardDate.ToShortDateString() + iSugYom;
    //        clParameters _Params;
    //        HttpApplication _App = (HttpApplication)HttpContext.Current.ApplicationInstance; 
    //        try
    //        {                 
    //            _Params=(clParameters)_App.Application[sCacheKey];                
    //        }
    //        catch (Exception ex)
    //        {
    //            _Params = null;
    //        }

    //        if (_Params == null)
    //        {
    //            _Params = new clParameters(dCardDate, iSugYom);
    //            _App.Application[sCacheKey] = _Params;
    //        }

    //        return _Params;
    //    }
        public static void SetParamTableInApplicationObj()
        {
            clUtils _Utils = new clUtils();
            DataTable dtParameters;
            

            dtParameters = _Utils.GetKdsParametrs();
            foreach (DataRow drParam in dtParameters.Rows)
            {
                //SetParameters(dr["me_taarich"], iSugYom);
            }
            
            dtParameters.Dispose();
        }
        public static int GetDiffDays(DateTime dFromDate, DateTime dToDate)
        {
            //הפרש בימים בין תאריכים
            TimeSpan ts = dToDate - dFromDate;
            int iDays = ts.Days;
            return iDays;
        }
  }
}

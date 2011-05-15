using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;


using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;
namespace KdsBatch
{
    public class clCalcDal
    {
        private const string cProGetOvdimLechishuv = "Pkg_Calculation.pro_get_ovdim_lechishuv";
        private const string cProGetMichsaYomit = "Pkg_Calculation.pro_get_michsa_yomit";
        private const string cProGetSidurimMeyuchRechiv = "Pkg_Calculation.pro_get_sidurim_meyuch_rechiv";
        private const string cProGetSugeySidurRechiv = "Pkg_Calculation.pro_get_sug_sidur_rechiv";
        private const string cProGetSugeySidurFromTnua = "Pkg_Calculation.pro_get_sugey_sidur_tnua";
        private const string cProGetPremyotView = "Pkg_Calculation.pro_get_premyot_view";
        private const string cProGetPremiaYadanit = "Pkg_Calculation.pro_get_premia_yadanit";
        private const string cProGetBusesDetails = "Pkg_Calculation.pro_get_buses_details";
        private const string cProGetYemeyAvoda = "Pkg_Calculation.pro_get_yemey_avoda";
        private const string cGetKatalogKavim = "Pkg_Calculation.pro_get_kavim_details";
        private const string cProGetPirteyOvdim = "Pkg_Calculation.pro_get_pirtey_ovdim";
        private const string cProGetMeafyeneyBitua = "Pkg_Calculation.pro_get_meafyeney_ovdim";
        private const string cProGetSugYechida = "Pkg_Calculation.pro_get_sug_yechida";
        private const string cProGetPeiluyotOvdim = "Pkg_Calculation.pro_get_peiluyot_ovdim";
        private const string cProGetPremiaOvdimLechishuv = "Pkg_Calculation.pro_get_ovdim_lehishuv_premiot";

        private const string cGetNetunryChishuv = "Pkg_Calculation.pro_get_netunim_lechishuv";
        public DataTable GetOvdimLechishuv(DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                
                if (sMaamad.IndexOf(",") > 0)
                {
                    oDal.AddParameter("p_maamad", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_maamad", ParameterType.ntOracleInteger, sMaamad, ParameterDir.pdInput);
                }
                oDal.AddParameter("p_ritza_gorefet", ParameterType.ntOracleInteger, bRitzaGorefet.GetHashCode(), ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetOvdimLechishuv, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMichsaYomitLechodesh(DateTime dTarMe, DateTime dTarAd)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetMichsaYomit, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SetSidurimMeyuchaRechiv(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dtSidurimMeyuchaRechiv = new DataTable();
            try
            {   //מחזיר סידורים מיוחדים רכיב 
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetSidurimMeyuchRechiv, ref dtSidurimMeyuchaRechiv);

                return dtSidurimMeyuchaRechiv;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetSugeySidurRechiv(DateTime dTarMe, DateTime dTarAd)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetSugeySidurRechiv, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSugeySidur()
        {

            clDal oDal = new clDal();
            DataTable dtSugeySidur = new DataTable();
            try
            {   //מחזיר סוגי סידור לעובד בחודש מהתנועה
                //oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                //oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetSugeySidurFromTnua, ref dtSugeySidur);
                return dtSugeySidur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getPremyot()
        {
            clDal oDal = new clDal();
            DataTable dtPremyot = new DataTable();
            try
            {   //מחזיר פרמיות:  
                //oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetPremyotView, ref dtPremyot);

                return dtPremyot;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable getPremyotYadaniyot()
        {
            clDal oDal = new clDal();
            DataTable dtPremyot = new DataTable();
            try
            {   //ידניות מחזיר פרמיות:  
                //oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetPremiaYadanit, ref dtPremyot);

                return dtPremyot;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetBusesDetails()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מביא את נתוני מש"ר: 
                //oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetBusesDetails, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetYemeyAvoda( )
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר ימי עבודה לעובד:  
                //טבלת TB_Yamey_Avoda_Ovdim
                // Status-יכללו כל כרטיסי העבודה התקינים/הועברו לשכר (ערך 1/2 בשדה  
                //  Status_Tipul-בחודש הנבחר עבורם הסתיים טיפול – ערך "1" בשדה  
                //oDal.AddParameter("p_taarich_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_taarich_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                //if (iStatusTipul == 0)
                //{ 
                oDal.AddParameter("p_status_tipul", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                // }
                // else
                // {
                //     oDal.AddParameter("p_status_tipul", ParameterType.ntOracleInteger, iStatusTipul, ParameterDir.pdInput);
                //}
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetYemeyAvoda, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetKatalogKavim()
        {
            clTxDal _Dal = new clTxDal();
            DataTable dt = new DataTable();
            //הפרוצדורה מחזירה את כל נתוני הפעילויות מהתנועה, למספר אישי ולטווח תאריכים נתון
            try
            {
                _Dal.TxBegin();
               // _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                //_Dal.AddParameter("p_date_from", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput, 100);
                //_Dal.AddParameter("p_date_to", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput, 100);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(cGetKatalogKavim, ref dt);
                _Dal.TxCommit();
                return dt;
            }
            catch (Exception ex)
            {
                _Dal.TxRollBack();
                throw ex;
            }
        }

        public DataTable GetPirteyOvdim()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                // oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetPirteyOvdim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMeafyeneyBitzuaLeOvedAll(int iBreratMechdal)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה מאפייני ביצוע

                //oDal.AddParameter("p_me_taarich", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_ad_taarich", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_brerat_mechdal", ParameterType.ntOracleInteger, iBreratMechdal, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProGetMeafyeneyBitua, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSugYechida()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {

                //oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProGetSugYechida, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPeiluyotOvdim()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {

                //oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                //oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProGetPeiluyotOvdim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePremiaBakashaID(int iMisparIshi, long lBakashaId, DateTime startMonth)
        {

            clDal dal = new clDal();
            dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lBakashaId,
                ParameterDir.pdInput);
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi,
                ParameterDir.pdInput);
            dal.AddParameter("p_chodesh", ParameterType.ntOracleDate, startMonth,
                ParameterDir.pdInput);
            dal.ExecuteSP(clDefinitions.cProUpdateChishuvPremia);

        }

        public DataTable GetPremiaCalcPopulation(ref DateTime dTarMe, ref DateTime dTarAd)
        {
            DataTable dt = new DataTable();
            clDal dal = new clDal();
            try
            {
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null,  ParameterDir.pdOutput);
                dal.AddParameter("p_taarich_me", ParameterType.ntOracleDate, null, ParameterDir.pdOutput);
                dal.AddParameter("p_taarich_ad", ParameterType.ntOracleDate, null, ParameterDir.pdOutput);
                dal.ExecuteSP(cProGetPremiaOvdimLechishuv, ref dt);
                if (dal.GetValParam("p_taarich_me") !=null && dal.GetValParam("p_taarich_me") !="")
                    dTarMe = DateTime.Parse(dal.GetValParam("p_taarich_me"));
                if (dal.GetValParam("p_taarich_ad") != null && dal.GetValParam("p_taarich_ad") != "")
                    dTarAd = DateTime.Parse(dal.GetValParam("p_taarich_ad")).AddMonths(1).AddDays(-1);
            }
            catch (Exception ex)
            {
                //dt = null;
                throw (ex);
            }
            return dt;
        }

        public DataSet GetNetuneyChishuvDS1 (DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi)
        {
            DataSet ds = new DataSet();
            clDal dal = new clDal();
            string names;
            DataTable dt = new DataTable();
            try
            {
 

                dal.AddParameter("p_Cur_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names= "Ovdim";
                dal.AddParameter("p_Cur_Michsa_Yomit", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Michsa_Yomit";
                dal.AddParameter("p_Cur_SidurMeyuchadRechiv", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sidur_Meyuchad_Rechiv";
                dal.AddParameter("p_Cur_Sug_Sidur_Rechiv", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sug_Sidur_Rechiv";
                dal.AddParameter("p_Cur_Premiot_View", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Premiot_View";
                dal.AddParameter("p_Cur_Premiot_Yadaniot", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Premiot_Yadaniot";
                dal.AddParameter("p_Cur_Sug_Yechida", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sug_Yechida";
                dal.AddParameter("p_Cur_Yemey_Avoda", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Yemey_Avoda";
                dal.AddParameter("p_Cur_Pirtey_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Pirtey_Ovdim";
                dal.AddParameter("p_Cur_Meafyeney_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Meafyeney_Ovdim";
                dal.AddParameter("p_Cur_Peiluyot_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Peiluyot_Ovdim"; 
                dal.AddParameter("p_Cur_Buses_Details", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Buses_Details";
                 dal.AddParameter("p_Cur_Sugey_Sidur_Tnua", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sugey_Sidur_Tnua";
                dal.AddParameter("p_Cur_Kavim_Details", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Kavim_Details";

                dal.AddParameter("p_tar_me", ParameterType.ntOracleDate, TarMe, ParameterDir.pdInput);
                dal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, TarAd, ParameterDir.pdInput);

                if (sMaamad.IndexOf(",") > 0)
                {
                    dal.AddParameter("p_maamad", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_maamad", ParameterType.ntOracleInteger, sMaamad, ParameterDir.pdInput);
                }
                dal.AddParameter("p_ritza_gorefet", ParameterType.ntOracleInteger, rizaGorefet.GetHashCode(), ParameterDir.pdInput);
                dal.AddParameter("p_status_tipul", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                dal.AddParameter("p_brerat_mechdal", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput);
                dal.AddParameter("p_Mis_Ishi", ParameterType.ntOracleInteger, mis_ishi, ParameterDir.pdInput);


                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(cGetKatalogKavim, ref dt);
               //  dal.ExecuteSP(cGetNetunryChishuv, ref ds,names);
               
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public DataSet GetNetuneyChishuvDS(DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi)
        {
            DataSet ds = new DataSet();
            string names;
           // DataTable dt = new DataTable();
            clTxDal dal = new clTxDal();
 
            try
            {

                dal.TxBegin();
                dal.AddParameter("p_Cur_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names = "Ovdim";
                dal.AddParameter("p_Cur_Michsa_Yomit", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Michsa_Yomit";
                dal.AddParameter("p_Cur_SidurMeyuchadRechiv", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sidur_Meyuchad_Rechiv";
                dal.AddParameter("p_Cur_Sug_Sidur_Rechiv", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sug_Sidur_Rechiv";
                dal.AddParameter("p_Cur_Premiot_View", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Premiot_View";
                dal.AddParameter("p_Cur_Premiot_Yadaniot", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Premiot_Yadaniot";
                dal.AddParameter("p_Cur_Sug_Yechida", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sug_Yechida";
                dal.AddParameter("p_Cur_Yemey_Avoda", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Yemey_Avoda";
                dal.AddParameter("p_Cur_Pirtey_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Pirtey_Ovdim";
                dal.AddParameter("p_Cur_Meafyeney_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Meafyeney_Ovdim";
                dal.AddParameter("p_Cur_Peiluyot_Ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Peiluyot_Ovdim";
                dal.AddParameter("p_Cur_Mutamut", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Ctb_Mutamut";
                dal.AddParameter("p_Cur_Piturim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Ovdim_ShePutru";
                dal.AddParameter("p_Cur_Buses_Details", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Buses_Details";
                dal.AddParameter("p_Cur_Sugey_Sidur_Tnua", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Sugey_Sidur_Tnua";
                dal.AddParameter("p_Cur_Kavim_Details", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Kavim_Details";

                dal.AddParameter("p_tar_me", ParameterType.ntOracleDate, TarMe, ParameterDir.pdInput);
                dal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, TarAd, ParameterDir.pdInput);

                if (sMaamad.IndexOf(",") > 0)
                {
                    dal.AddParameter("p_maamad", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_maamad", ParameterType.ntOracleInteger, sMaamad, ParameterDir.pdInput);
                }
                dal.AddParameter("p_ritza_gorefet", ParameterType.ntOracleInteger, rizaGorefet.GetHashCode(), ParameterDir.pdInput);
                dal.AddParameter("p_status_tipul", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                dal.AddParameter("p_brerat_mechdal", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput);
                dal.AddParameter("p_Mis_Ishi", ParameterType.ntOracleInteger, mis_ishi, ParameterDir.pdInput);

                //dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                //dal.ExecuteSP(cGetKatalogKavim, ref dt);
                clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "before ExecuteSP");
                dal.ExecuteSP(cGetNetunryChishuv, ref ds, names);
                clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "After ExecuteSP");
                dal.TxCommit();
                clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "After Commit");
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

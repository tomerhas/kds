using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;

namespace KdsBatch.CalcNew
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
    }
}

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
using KdsBatch.Errors;
namespace KdsBatch.Entities
{
    public class EntitiesDal
    {
        private const string cProGetShgiotNoActive = "Pkg_Errors.pro_get_shgiot_active";
        private const string cProDeleteErrors = "Pkg_Errors.pro_Delete_Errors";
        private const string cProGetOvdimErrors = "PKG_CALCULATION.pro_Ovdim_Errors";

        public DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_last_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedYomAvodaDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedYomAvodaDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSidurimLeOved(int iMisparIshi, DateTime dCardDate)
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

        public DataTable GetErrorsActive()
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

        public DataTable GetOvedMatzav(int iMisparIshi, DateTime dDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר טבלת פרמטרים:                                
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedMatzav, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public bool IsSidurChofef(int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iParamChafifa, ref DataTable dt)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_gmar", ParameterType.ntOracleDate, dShatGmar, ParameterDir.pdInput);
                oDal.AddParameter("p_param_chafifa", ParameterType.ntOracleInteger, iParamChafifa, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProIsSidurChofef, ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  void UpdateCardStatus(int iMisparIshi, DateTime dCardDate, clGeneral.enCardStatus oCardStatus, int iUserId)
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

        public  DataTable GetTmpMeafyeneyElements(DateTime dTarMe, DateTime dTarAd)
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

        public bool IsDuplicateTravle(int iMisparIshi, DateTime dCardDate, long lMakatNesia, DateTime dShatYetzia, int iMisparKnisa, ref DataTable dt)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInt64, lMakatNesia, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate, dShatYetzia, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, iMisparKnisa, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProIsDuplicateTravel, ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSidur(string sidurPositionFirstLast, int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_sidur_position", ParameterType.ntOracleVarchar, sidurPositionFirstLast, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSidur, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteErrorsFromTbShgiot(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);

                oDal.ExecuteSP(cProDeleteErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertErrorsToTbShgiot(DateTime dCardDate)
        {
            //כתיבת שגיאות ל-TB_SHGIOT
            clDal oDal = new clDal();
            StringBuilder sbYeshut = new StringBuilder();
            DataSet ds = new DataSet();
            string[] ucols = new string[2];
            //DataTable dtErrors;
            try
            {
                 int i = 0;
                oDal.ArrayBindCount = GlobalData.CardErrors.Count; // dtErrors.Rows.Count;
                int[] arrMisparIshi = new int[GlobalData.CardErrors.Count];
                int[] arrKodShgia = new int[GlobalData.CardErrors.Count];
                string[] arrYeshutId = new string[GlobalData.CardErrors.Count];
                int[] arrMisparSidur = new int[GlobalData.CardErrors.Count];
                DateTime[] arrTaarich = new DateTime[GlobalData.CardErrors.Count];
                DateTime[] arrShatHatchala = new DateTime[GlobalData.CardErrors.Count];
                DateTime[] arrShatYetzia = new DateTime[GlobalData.CardErrors.Count];
                int[] arrMisparKnisa = new int[GlobalData.CardErrors.Count];
                string[] arrHeara = new string[GlobalData.CardErrors.Count];

                foreach (CardError ce in GlobalData.CardErrors)
                {
                    arrMisparIshi[i] = int.Parse(ce.mispar_ishi.ToString());
                    arrKodShgia[i] = int.Parse(ce.check_num.ToString());
                    sbYeshut.Remove(0, sbYeshut.Length);
                    sbYeshut.Append(string.IsNullOrEmpty(ce.taarich.ToString()) ? dCardDate.ToShortDateString() : DateTime.Parse(ce.taarich.ToString()).ToShortDateString());
                    //sbYeshut.Append(DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
                    sbYeshut.Append(",");
                    sbYeshut.Append(string.IsNullOrEmpty(ce.mispar_sidur.ToString()) ? "" : string.Concat(ce.mispar_sidur.ToString(), ","));
                    sbYeshut.Append(string.IsNullOrEmpty(ce.shat_hatchala.ToString()) ? "" : string.Concat(DateTime.Parse(ce.shat_hatchala.ToString()).ToString("HH:mm"), ","));
                    if (ce.shat_yetzia != DateTime.MinValue)
                        sbYeshut.Append(string.IsNullOrEmpty(ce.shat_yetzia.ToString()) ? "" : string.Concat(DateTime.Parse(ce.shat_yetzia.ToString()).ToString("HH:mm"), ","));
                    if(string.IsNullOrEmpty(ce.mispar_knisa.ToString()) || ce.mispar_knisa ==0)
                    sbYeshut.Append("");
                    else sbYeshut.Append(string.Concat(ce.mispar_knisa.ToString(), ","));

                    sbYeshut.Append(int.Parse(ce.check_num.ToString()));
                    sbYeshut.Append(",");
                    sbYeshut.Append(i.ToString());
                    //arrYeshutId[i]=sbYeshut.ToString().Remove(sbYeshut.ToString().Length-1,1);
                    arrYeshutId[i] = sbYeshut.ToString();

                    arrTaarich[i] = (string.IsNullOrEmpty(ce.taarich.ToString()) ? DateTime.MinValue : DateTime.Parse(ce.taarich.ToString()));
                    arrMisparSidur[i] = string.IsNullOrEmpty(ce.mispar_sidur.ToString()) ? 0 : (int)ce.mispar_sidur;
                    arrShatHatchala[i] = string.IsNullOrEmpty(ce.shat_hatchala.ToString()) ? DateTime.MinValue : DateTime.Parse(ce.shat_hatchala.ToString());
                    arrShatYetzia[i] = string.IsNullOrEmpty(ce.shat_yetzia.ToString()) ? DateTime.MinValue : DateTime.Parse(ce.shat_yetzia.ToString());
                    arrMisparKnisa[i] = string.IsNullOrEmpty(ce.mispar_knisa.ToString()) ? 0 : (int)ce.mispar_knisa;

                    //arrHeara[i] = dr["error_desc"].ToString();
                    i++;
                }

                oDal.AddParameter("MISPAR_ISHI", ParameterType.ntOracleInteger, arrMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("KOD_SHGIA", ParameterType.ntOracleInteger, arrKodShgia, ParameterDir.pdInput);
                oDal.AddParameter("YESHUT_ID", ParameterType.ntOracleVarchar, arrYeshutId, ParameterDir.pdInput);
                oDal.AddParameter("TAARICH", ParameterType.ntOracleDate, arrTaarich, ParameterDir.pdInput);
                oDal.AddParameter("MISPAR_SIDUR", ParameterType.ntOracleInteger, arrMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("SHAT_HATCHALA", ParameterType.ntOracleDate, arrShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("SHAT_YETZIA", ParameterType.ntOracleDate, arrShatYetzia, ParameterDir.pdInput);
                oDal.AddParameter("MISPAR_KNISA", ParameterType.ntOracleInteger, arrMisparKnisa, ParameterDir.pdInput);
                //oDal.AddParameter("HEARA", ParameterType.ntOracleVarchar, arrHeara, ParameterDir.pdInput);
                //// Set the command text on an OracleCommand object
                //oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,HEARA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:HEARA)");
                oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,MISPAR_KNISA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:MISPAR_KNISA)");
         
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRitzatShgiotDate(int iMisparIshi, DateTime dCardDate, bool bShgiotLetzuga)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                if (bShgiotLetzuga)
                {
                    oDal.AddParameter("p_shgiot_letzuga", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_shgiot_letzuga", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                oDal.ExecuteSP(clDefinitions.cProUpdTarRitzatShgiot);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckShgiotLetzuga(string sArrKodShgia)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_arr_kod_shgia", ParameterType.ntOracleVarchar, sArrKodShgia, ParameterDir.pdInput, 300);

                oDal.ExecuteSP(clDefinitions.cFunCountShgiotLetzuga);

                return int.Parse(oDal.GetValParam("p_result").ToString()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getOvdimForShguim()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
               
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetOvdimErrors, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  DataTable GetData(DateTime taarich)
        {
            DataTable dt = new DataTable();
           // errorMessage = String.Empty;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_date", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, 0, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProGetYameiAvodaMeshek, ref dt);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
              //  errorMessage = ex.ToString();
            }
            return dt;
        }
    }
}

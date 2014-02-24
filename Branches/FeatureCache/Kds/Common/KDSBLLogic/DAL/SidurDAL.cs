using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DalOraInfra.DAL;
using KDSCommon.Interfaces.DAL;
using System.Configuration;

namespace KdsLibrary.KDSLogic.DAL
{
    public class SidurDAL : ISidurDAL
    {
        public const string cProIsSidurChofef = "pkg_errors.pro_have_sidur_chofef";
        public const string cGetSidurDetails = "KDS_SIDUR_AVODA_PACK.GetSidurDetails";
        public const string cFunGetMeshechSidurForPeriod = "pkg_sidurim.fun_get_meshech_sidur_Toperiod";

        public bool IsSidurChofef(int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iParamChafifa, DataTable dt)
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

                oDal.ExecuteSP(cProIsSidurChofef, ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSidurAndPeiluyotFromTnua(int iMisparSidur, DateTime dDate, int? iKnisaVisut, out int iResult)
        {
            // int ?iKnisaVisut,    
            DataSet ds = new DataSet();
            clTxDal oTxDal = new clTxDal((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);

            try
            {//: מביא נתונים לסידור      
                oTxDal.TxBegin();
                oTxDal.AddParameter("p_date", ParameterType.ntOracleVarchar, dDate.ToShortDateString(), ParameterDir.pdInput, 100);
                oTxDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oTxDal.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTxDal.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTxDal.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTxDal.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, iKnisaVisut, ParameterDir.pdInput);

                oTxDal.ExecuteSP(cGetSidurDetails, ref ds);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                if (ds.Tables[0].Rows.Count == 0)
                {
                    iResult = 1;
                }
                else
                {
                    iResult = int.Parse(oTxDal.GetValParam("p_rc").ToString());
                }
                oTxDal.TxCommit();
                return ds;
            }
            catch (Exception ex)
            {
               // clGeneral.LogMessage("GetSidurAndPeiluyotFromTnua: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());

                oTxDal.TxRollBack();
                throw ex;
            }
        }

        public float GetMeshechSidur(int iMisparIshi, int iMisparSidur, DateTime taarich_me, DateTime taarich_ad)
        {
             clDal oDal = new clDal();
             float iMeshechSidur = 0;
             try
             {
                 //מחזיר את סטטוס קוד האישור ברמה הגבוהה ביותר
                 oDal.AddParameter("p_return_value", ParameterType.ntOracleDecimal, null, ParameterDir.pdReturnValue);

                 oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                 oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                 oDal.AddParameter("p_taarich_me", ParameterType.ntOracleDate, taarich_me, ParameterDir.pdInput);
                 oDal.AddParameter("p_taarich_ad", ParameterType.ntOracleDate, taarich_ad, ParameterDir.pdInput);

                 oDal.ExecuteSP(cFunGetMeshechSidurForPeriod);

                 iMeshechSidur = float.Parse(oDal.GetValParam("p_return_value"));

                 return iMeshechSidur;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
        }

    }
}

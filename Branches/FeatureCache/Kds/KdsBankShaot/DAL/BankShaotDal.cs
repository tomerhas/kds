using DalOraInfra.DAL;
using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdsBankShaot.DAL
{
    public class BankShaotDal
    {
        public const string cProGetYechidotLechishuv = "PKG_BANK_SHAOT.pro_get_yechidot_lechishuv";
        public const string cProGetTbparams = "PKG_BANK_SHAOT.pro_get_tb_params_bank";
        public const string cProGetpronetuneychishuv = "PKG_BANK_SHAOT.pro_get_netuney_chishuv";
        public const string cFunGetYemeyChol = "PKG_BANK_SHAOT.pro_get_yemey_chol";
        public const string cProSaveNetuneyBudgets = "PKG_BANK_SHAOT.pro_save_chishuv_budgets";
        public const string cProSaveBudgetEmployees = "PKG_BANK_SHAOT.pro_save_budget_employee";

        public DataTable GetYechidotLeChishuv(DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetYechidotLechishuv, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetParametrim(DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetTbparams, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNetuneyOvdimToYechida(int iKodYechida,DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_yechida", ParameterType.ntOracleInteger, iKodYechida, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetpronetuneychishuv, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetYemeyChol(DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה מאפיין לעובד
                
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cFunGetYemeyChol, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveNetuneyBudgets(COLL_BUDGET oCollBudgets)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_coll_budgets", ParameterType.ntOracleArray, oCollBudgets, ParameterDir.pdInput, "COLL_BUDGET");

                oDal.ExecuteSP(cProSaveNetuneyBudgets);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveEmployeesBudget(int iKodYechida, DateTime dTaarich, long iBakashaId, int iMeadken)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_yechida", ParameterType.ntOracleInteger, iKodYechida, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInteger, iBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_meadken", ParameterType.ntOracleInteger, iMeadken, ParameterDir.pdInput);

                oDal.ExecuteSP(cProSaveBudgetEmployees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        
    }
}

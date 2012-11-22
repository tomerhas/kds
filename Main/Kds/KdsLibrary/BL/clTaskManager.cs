using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
namespace KdsLibrary.BL
{
    public class clTaskManager
    {

        private static clTaskManager _Instance;

        public static clTaskManager GetInstance()
        {
            if (_Instance == null)
                _Instance = new clTaskManager();
            return _Instance;
        }

        public DataTable GetStuckGroup(int GroupId, int ActionId)
        {
            try
            {
                DataTable dt = new DataTable();
                clDal oDal = new clDal();
                oDal.AddParameter("p_GroupId", ParameterType.ntOracleInteger, GroupId, ParameterDir.pdInput);
                oDal.AddParameter("p_ActionId", ParameterType.ntOracleInteger, ActionId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cGetStuckGroup, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStausSdrn(string dTaarich)
        {
            try
            {
                DataTable dt = new DataTable();
                clDal oDal = new clDal();
                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetStatusSdrn, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RunSdrnWithDate(string dTaarich)
        {
            try
            {   
                clDal oDal = new clDal();
                oDal.AddParameter("p_date_str_yyyymmdd", ParameterType.ntOracleVarchar, dTaarich, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cProRunSdrnWithDate);
          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RunRetrospectSdrn(string dTaarich)
        {
            try
            {
                clDal oDal = new clDal();
                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, dTaarich, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cProRunRetrospectSdrn);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

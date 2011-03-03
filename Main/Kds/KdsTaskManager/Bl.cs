using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
namespace KdsTaskManager
{
    public class Bl
    {
        private static Bl _Instance;

        public static Bl GetInstance()
        {
            if (_Instance == null)
                _Instance = new Bl();
            return _Instance;
        }

        public DataTable GetGroupsDefinition()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(OracleSp.GetGroupsDefinition, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetTaskOfGroup(int CurrentGroup)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("GroupId", KdsLibrary.DAL.ParameterType.ntOracleInt64, CurrentGroup, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(OracleSp.GetTaskOfGroup, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetActionParameters(int CurrentGroup, int CurrentOrder)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_GroupId", KdsLibrary.DAL.ParameterType.ntOracleInt64, CurrentGroup, ParameterDir.pdInput);
                oDal.AddParameter("p_OrderId", KdsLibrary.DAL.ParameterType.ntOracleInt64, CurrentOrder, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(OracleSp.GetActionParameters, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public void UpdateProcessLog(Message message)
        {
            clDal oDal = new clDal();
            OBJ_LOGTASKMSG oLogMessage;
            try
            {
                oLogMessage = new OBJ_LOGTASKMSG(message.GroupId, message.IdOrder, (int)message.Status, message.Sequence, message.StartTime, message.EndTime, message.Remark);

                oDal.AddParameter("LOGTASKMSG", ParameterType.ntOracleArray, oLogMessage, ParameterDir.pdInput, "OBJ_LOGTASKMSG");
                oDal.ExecuteSP(OracleSp.UpdateLogTask);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertProcessLog(Message message)
        {
            clDal oDal = new clDal();
            OBJ_LOGTASKMSG oLogMessage;
            try
            {
                oLogMessage = new OBJ_LOGTASKMSG(message.GroupId, message.IdOrder, (int)message.Status, message.Sequence, message.StartTime, message.EndTime, message.Remark);

                oDal.AddParameter("LOGTASKMSG", ParameterType.ntOracleArray, oLogMessage, ParameterDir.pdInput, "OBJ_LOGTASKMSG");
                oDal.ExecuteSP(OracleSp.InsertLogTask);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

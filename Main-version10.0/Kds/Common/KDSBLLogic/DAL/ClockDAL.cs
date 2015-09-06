using DalOraInfra.DAL;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSBLLogic.DAL
{
    public class ClockDAL : IClockDAL
    {
        public const string cProSaveHarmonyMovment = "Pkg_Attendance.pro_ins_shaonim_movment";
        public const string cProInsControlClock = "Pkg_Attendance.pro_ins_control_clock";
        public const string cProUpdControlClock = "Pkg_Attendance.pro_upd_control_clock";
        public const string cFunGetLastHourClock = "Pkg_Attendance.pro_get_last_hour_clock";

        public void SaveHarmonyMovment(long BakashaId ,COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_bakasha", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_coll_Harmony_movment", ParameterType.ntOracleArray, collHarmonyMovment, ParameterDir.pdInput, "COLL_HARMONY_MOVMENT_ERR_MOV");   
                oDal.ExecuteSP(cProSaveHarmonyMovment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertControlClockRecord(DateTime  taarich, int status , string teur)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("p_status", ParameterType.ntOracleInteger, status, ParameterDir.pdInput);
                oDal.AddParameter("p_teur", ParameterType.ntOracleVarchar, teur, ParameterDir.pdInput);
                oDal.ExecuteSP(cProInsControlClock);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateControlClockRecord(DateTime taarich, int status, string teur)
        {
            clDal oDal = new clDal();
            try
            {
               oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
               oDal.AddParameter("p_status", ParameterType.ntOracleInteger, status, ParameterDir.pdInput);
               oDal.AddParameter("p_teur", ParameterType.ntOracleVarchar, teur, ParameterDir.pdInput);
               oDal.ExecuteSP(cProUpdControlClock);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetLastHourClock()
        {
            clDal oDal = new clDal();
            string sHour;
            DateTime dhour;
            try
            {
                oDal.AddParameter("p_hour", ParameterType.ntOracleVarchar, null, ParameterDir.pdReturnValue,50);
                oDal.ExecuteSP(cFunGetLastHourClock);


                dhour =oDal.GetValParam("p_hour") =="null"? DateTime.Now : DateTime.Parse(oDal.GetValParam("p_hour"));
                sHour = dhour.Year + "-" + dhour.Month.ToString().PadLeft(2, '0') + "-" + dhour.Day.ToString().PadLeft(2, '0') + " " + dhour.ToLongTimeString();
                return sHour;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

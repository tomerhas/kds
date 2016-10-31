using DalOraInfra.DAL;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Data;
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
        //**  Attend **//
        public const string cProInsertTnuotShaon = "Pkg_Attendance.pro_Insert_Tnuot_Shaon";
        public const string cProGetNetunimToAttend = "Pkg_Attendance.pro_fetch_AttendHarmony";
        public const string cProNewCntrlAttend = "Pkg_Attendance.pro_new_control_Attend";
        public const string cProUpdateCntrlAttend = "Pkg_Attendance.pro_Upd_control_Attend";
        public const string cFunGetLastAttendCntrl = "Pkg_Attendance.pro_get_last_attend_cntl";
        public const string cProCheckKnisaExists = "Pkg_Attendance.pro_check_In";
        public const string cProInsertKnisa = "Pkg_Attendance.pro_ins_In";
        public const string cProCheckYeziaNull = "Pkg_Attendance.pro_GetOutNull";
        public const string cProUpdateKnisaRecord = "Pkg_Attendance.pro_UpdKnisa";
        public const string cProCheckYeziaExists = "Pkg_Attendance.pro_check_Yetzia";
        public const string cProCheckKnisaNull = "Pkg_Attendance.pro_GetInNull";
        public const string cProUpdateYeziaRecord = "Pkg_Attendance.pro_UpdYetzia";
        public const string cProInsertYetzia = "Pkg_Attendance.pro_ins_Yetzia";
        public const string cProInsertHityazvutPundakim = "Pkg_Attendance.pro_ins_hityazvut_pundakim";

        //**//

        public void SaveHarmonyMovment(long BakashaId, COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment)
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

        public void InsertControlClockRecord(DateTime taarich, int status, string teur)
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
                oDal.AddParameter("p_hour", ParameterType.ntOracleVarchar, null, ParameterDir.pdReturnValue, 50);
                oDal.ExecuteSP(cFunGetLastHourClock);


                dhour = oDal.GetValParam("p_hour") == "null" ? DateTime.Now : DateTime.Parse(oDal.GetValParam("p_hour"));
                sHour = dhour.Year + "-" + dhour.Month.ToString().PadLeft(2, '0') + "-" + dhour.Day.ToString().PadLeft(2, '0') + " " + dhour.ToLongTimeString();
                return sHour;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetNetunimToAttend()
        {
            clDal oDal = new clDal();
            DataSet ds = new DataSet();
            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetNetunimToAttend, ref ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertControlAttendRecord(DateTime taarich, int status, string teur)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pSTATUS", ParameterType.ntOracleInteger, status, ParameterDir.pdInput);
                oDal.AddParameter("pteur", ParameterType.ntOracleVarchar, teur, ParameterDir.pdInput);
                oDal.ExecuteSP(cProNewCntrlAttend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertTnuotShaon(long lRequestNum)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_bakasha", ParameterType.ntOracleInteger, lRequestNum, ParameterDir.pdInput);
                oDal.ExecuteSP(cProInsertTnuotShaon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int getLastCntrlAttend()
        {
            clDal oDal = new clDal();
            int status;
            try
            {
                oDal.AddParameter("p_status", ParameterType.ntOracleVarchar, null, ParameterDir.pdReturnValue, 50);
                oDal.ExecuteSP(cFunGetLastAttendCntrl);


                status = int.Parse(oDal.GetValParam("p_status"));
                return status;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }


        public void UpdateControlAttendRecord(DateTime taarich, long LastNumerator, int status, string teur)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("p_last_Num", ParameterType.ntOracleInt64, LastNumerator, ParameterDir.pdInput); 
                oDal.AddParameter("pSTATUS", ParameterType.ntOracleInteger, status, ParameterDir.pdInput);
                oDal.AddParameter("pteur", ParameterType.ntOracleVarchar, teur, ParameterDir.pdInput);

                oDal.ExecuteSP(cProUpdateCntrlAttend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetKnisaIfExists(int iMisparIshi, DateTime taarich, string inShaa, int mispar_sidur, int p24)
        {
            clDal oDal = new clDal();
            DataSet ds = new DataSet();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProCheckKnisaExists, ref ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertKnisatShaon(int mispar_ishi, DateTime taarich, string shaa, int site_kod, int mispar_sidur, string iStm, int p24)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput); ;
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, shaa, ParameterDir.pdInput);
                oDal.AddParameter("pMikum", ParameterType.ntOracleInteger, site_kod, ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, iStm, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);

                oDal.ExecuteSP(cProInsertKnisa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetYetziaNull(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24)
        {
            clDal oDal = new clDal();
            DataSet ds = new DataSet();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, int.Parse(shaa.Substring(0, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, int.Parse(shaa.Substring(2, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProCheckYeziaNull, ref ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateKnisaRecord(int mispar_ishi, DateTime taarich, string shaaK, string shaaY, int site_kod, int mispar_sidur, string iStm, int p24)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, int.Parse(shaaK.Substring(0, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, int.Parse(shaaK.Substring(2, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pMIKUM", ParameterType.ntOracleInteger, site_kod, ParameterDir.pdInput);
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, int.Parse(shaaY.Substring(0, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, int.Parse(shaaY.Substring(2, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, iStm, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);

                oDal.ExecuteSP(cProUpdateKnisaRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetYetziaIfExists(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24)
        {
            clDal oDal = new clDal();
            DataSet ds = new DataSet();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, shaa, ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProCheckYeziaExists, ref ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public DataSet GetKnisaNull(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24)
        {
            clDal oDal = new clDal();
            DataSet ds = new DataSet();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, int.Parse(shaa.Substring(0, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, int.Parse(shaa.Substring(2, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProCheckKnisaNull, ref ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         public void UpdateYeziaRecord(int mispar_ishi, DateTime taarich, string shaaK, string shaaY, int site_kod, int mispar_sidur, string iStm, int p24)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, int.Parse(shaaK.Substring(0, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, int.Parse(shaaK.Substring(2, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pMIKUM", ParameterType.ntOracleInteger, site_kod, ParameterDir.pdInput);
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, int.Parse(shaaY.Substring(0, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, int.Parse(shaaY.Substring(2, 2)), ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                 oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput);
                 oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, iStm, ParameterDir.pdInput);

                oDal.ExecuteSP(cProUpdateYeziaRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
          public void InsertYeziatShaon(int mispar_ishi, DateTime taarich, string shaa, int site_kod, int mispar_sidur, string iStm, int p24)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput); ;
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, shaa, ParameterDir.pdInput);
                oDal.AddParameter("pMikum", ParameterType.ntOracleInteger, site_kod, ParameterDir.pdInput);
                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleInteger, mispar_sidur, ParameterDir.pdInput);
                oDal.AddParameter("p24", ParameterType.ntOracleInteger, p24, ParameterDir.pdInput);
                oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, iStm, ParameterDir.pdInput);

                oDal.ExecuteSP(cProInsertYetzia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
          public void InsertHityazvutPundak(int mispar_ishi, DateTime taarich, DateTime shaa, int site_kod)
          {
              clDal oDal = new clDal();
              try
              {
                  oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput); ;
                  oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                  oDal.AddParameter("p_shaa", ParameterType.ntOracleDate, shaa, ParameterDir.pdInput);
                  oDal.AddParameter("p_mikum", ParameterType.ntOracleInteger, site_kod, ParameterDir.pdInput);

                  oDal.ExecuteSP(cProInsertHityazvutPundakim);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
          }
    }
      
}

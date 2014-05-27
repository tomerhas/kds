using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KDSCommon.UDT;
using DalOraInfra.DAL;

namespace KdsLibrary.BL
{
    public class clOvdim 
    {
        private clDal _Dal;
        private static clOvdim _Instance;

        public static clOvdim GetInstance()
        {
            if (_Instance == null)
                _Instance = new clOvdim();
            return _Instance;
        }

        public DataTable GetSidur(string sidurPositionFirstLast, int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
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

        public DataTable GetSidurimAndPeilut(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvedSidurimPeilut, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOvedDetails(int iMisparIshi, DateTime dTo)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: שם מעמד סניף ואיזור
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_last_taarich", ParameterType.ntOracleDate, dTo, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvedDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetOvedMisparIshi(string sName)
        {
            clDal oDal = new clDal();

            try
            {   //מחזיר מספר עובד על פי שם
                oDal.AddParameter("p_name", ParameterType.ntOracleVarchar, sName, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput, 200);
                oDal.ExecuteSP(clGeneral.cProGetOvedMisparIshi);

                if (oDal.GetValParam("p_mispar_ishi")!="null")
                    return oDal.GetValParam("p_mispar_ishi");
                else return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvdimByName(string sPrefix, string sMisparimRange)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר שמות של עובדים
                oDal.AddParameter("p_prefix", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                oDal.AddParameter("p_misparim_range", ParameterType.ntOracleVarchar, sMisparimRange, ParameterDir.pdInput, 4000);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvedimByName, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvdimMisparIshi(string sPrefix, string sMisparimRange)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר מספרים אישיים
                oDal.AddParameter("p_prefix", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                if (sMisparimRange == null) { sMisparimRange = ""; }
                oDal.AddParameter("p_misparim_range", ParameterType.ntOracleVarchar, sMisparimRange, ParameterDir.pdInput, 4000);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvedimMisparIshi, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetActiveWorkers(string sPrefix, DateTime FromDate, DateTime ToDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_prefix", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                oDal.AddParameter("p_FromDate", ParameterType.ntOracleDate, FromDate, ParameterDir.pdInput);
                oDal.AddParameter("p_EndDate", ParameterType.ntOracleDate, ToDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetActiveWorkers, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public string GetOvedFullName(int iMisparIshi)
        {
            clDal oDal = new clDal();
            try
            {   //מחזיר שם משפחה + שם פרטי
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_full_name", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput, 200);
                oDal.ExecuteSP(clGeneral.cProGetOvedFullName);

                if (oDal.GetValParam("p_full_name") != "null")
                    return oDal.GetValParam("p_full_name");
                else return "";
            }
            //catch (Oracle.DataAccess.Client.OracleException ex)
            //{
            //    throw new Exception("מספר אישי לא קיים"); 
            //}

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetErrorOvdim(int iKodHevra, int iKodEzor, int iKodSnif, int iKodMaamad, int iKodStatus,string sShgiot, DateTime dFrom, DateTime dTo)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            //string sOvdimMisparIshi = "";

            try
            {
                //מביא מספרים אישים של עובדים בסניף ומעמד נתון
                //xxxxx, xxxxx, xxxxx
                if (iKodHevra == 0)
                {
                    oDal.AddParameter("p_kod_hevra", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_hevra", ParameterType.ntOracleInteger, iKodHevra, ParameterDir.pdInput);
                }

                if (iKodEzor == 0)
                {
                    oDal.AddParameter("p_kod_ezor", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_ezor", ParameterType.ntOracleInteger, iKodEzor, ParameterDir.pdInput);
                }

                if (iKodSnif == 0)
                {
                    oDal.AddParameter("p_kod_snif", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_snif", ParameterType.ntOracleInteger, iKodSnif, ParameterDir.pdInput);
                }

                if (iKodMaamad == 0)
                {
                    oDal.AddParameter("p_kod_maamad", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_maamad", ParameterType.ntOracleInteger, iKodMaamad, ParameterDir.pdInput);
                }

                if (iKodStatus == -1)
                {
                    oDal.AddParameter("p_kod_status", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_status", ParameterType.ntOracleInteger, iKodStatus, ParameterDir.pdInput);
                }

                if (sShgiot == "")
                {
                    oDal.AddParameter("p_kod_shgia", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_shgia", ParameterType.ntOracleVarchar, sShgiot, ParameterDir.pdInput);
                }
                

                oDal.AddParameter("p_from_date", ParameterType.ntOracleDate, dFrom, ParameterDir.pdInput);
                oDal.AddParameter("p_to_date", ParameterType.ntOracleDate, dTo, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetErrorOvdim, ref dt);

                return dt;
                /*foreach (DataRow dr in dt.Rows)
                {
                    sOvdimMisparIshi += dr["mispar_ishi"] + ",";
                }

                sOvdimMisparIshi = sOvdimMisparIshi.Substring(0,sOvdimMisparIshi.Length - 1);
                return sOvdimMisparIshi;*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedErrorsCards(int iMisparIshi, DateTime from, DateTime to, int iKodStatus, string sShgiot)
        {
            try
            {
                DataTable dt = new DataTable();
                clDal oDal = new clDal();

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_from", ParameterType.ntOracleDate, from, ParameterDir.pdInput);
                oDal.AddParameter("p_to", ParameterType.ntOracleDate, to, ParameterDir.pdInput);

                if (iKodStatus == -1)
                {
                    oDal.AddParameter("p_kod_status", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_status", ParameterType.ntOracleInteger, iKodStatus, ParameterDir.pdInput);
                }

                if (sShgiot == "")
                {
                    oDal.AddParameter("p_kod_shgia", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_shgia", ParameterType.ntOracleVarchar, sShgiot, ParameterDir.pdInput);
                }
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetOvedErrorsCards, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvdimToUser(string sPrefix, int iUserId)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר מספרים אישיים
                oDal.AddParameter("p_prefix", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                oDal.AddParameter("p_user_id", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvdimToUser, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvdimLefiRikuzim(string sPrefix, int userId)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר מספרים אישיים
                oDal.AddParameter("p_prefix", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                oDal.AddParameter("p_user_id", ParameterType.ntOracleInteger, userId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvdimLefiRikuzim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOvdimToUserByName(string sPrefix, int iUserId)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר מספרים אישיים
                oDal.AddParameter("p_prefix", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                oDal.AddParameter("p_user_id", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvdimByUserName, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPirteyOved(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מביא פירטי עובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetPirteyOved, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPirteyOvedAll(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מביא את כל נתוני פירטי עובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetPirteyOvedAll, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPirteyHitkashrut(int iMisparIshi)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP("pkg_ovdim.pro_get_pirtey_hitkashrut_oved", ref dt);//clGeneral.cProGetPirteyOvedAll, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetSugChishuv(int iMisparIshi, DateTime dTaarich, ref long iBakashaId, ref DateTime dTarChishuv)
        {
            clDal oDal = new clDal();

            try
            {   //מביא סוג חישוב ומספר בקשת חישוב
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_sug_chishuv", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_taarich_chishuv", ParameterType.ntOracleDate, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetSugChishuv);

                if (oDal.GetValParam("p_bakasha_id").Length > 0)
                {
                    iBakashaId = long.Parse(oDal.GetValParam("p_bakasha_id"));
                    dTarChishuv = DateTime.Parse(oDal.GetValParam("p_taarich_chishuv"));
                }
                return int.Parse(oDal.GetValParam("p_sug_chishuv"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPirteyHeadrut(int iMisparIshi, DateTime dTaarich, long iBakashaId)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //  פונקציה המחזירה פירטי חופשה והיעדרות לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iBakashaId, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetPirteyHeadrut, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetHeadruyot()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {

                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetHeadruyot, ref dt);
            }
            catch (Exception ex)
            {
                //TO DO: log exeption
            }

            return dt;
        }

        public DataTable GetPirteyHeadrutTemp(ref clTxDal oDal, int iMisparIshi, DateTime dTaarich, long iBakashaId)
        {

            DataTable dt = new DataTable();
            try
            {   //  פונקציה המחזירה פירטי חופשה והיעדרות לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iBakashaId, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetPirteyHeadrutTemp, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetRechivimChodshiyimTemp(ref clTxDal oDal, int iMisparIshi, DateTime dTaarich, long iBakashaId, int iTzuga)
        {
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה רכיבים חודשיים לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_tzuga", ParameterType.ntOracleInteger, iTzuga, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetRechivimChodshiyimTemp, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRechivimChodshiyim(int iMisparIshi, DateTime dTaarich, long iBakashaId, int iTzuga)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה רכיבים חודשיים לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_tzuga", ParameterType.ntOracleInteger, iTzuga, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetRechivimChodshiyim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetRikuzChodshiTemp(ref clTxDal oDal, int iMisparIshi, DateTime dTaarich, long iBakashaId, int iTzuga,float fErechRechiv45, ref DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref DataTable dtRikuz21To31,ref DataTable dtAllRikuz)
        {
            //DataTable dt = new DataTable();

            try
            {   //פונקציה המחזירה ריכוז חודשי לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_tzuga", ParameterType.ntOracleInteger, iTzuga, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetRikuzChodshiTemp, ref dtAllRikuz);

                ChangeRowToColumns(dtAllRikuz, ref  dtRikuz1To10, ref  dtRikuz11To20, ref  dtRikuz21To31, iTzuga,fErechRechiv45);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetRikuzChodshi(int iMisparIshi, DateTime dTaarich, long iBakashaId, int iTzuga,float fErechRechiv45, ref DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref DataTable dtRikuz21To31)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            DataColumn[] columnsDt = new DataColumn[16];

            try
            {   //פונקציה המחזירה ריכוז חודשי לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_tzuga", ParameterType.ntOracleInteger, iTzuga, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetRikuzChodshi, ref dt);

                ChangeRowToColumns(dt, ref  dtRikuz1To10, ref  dtRikuz11To20, ref  dtRikuz21To31, iTzuga, fErechRechiv45);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ChangeRowToColumns(DataTable dt, ref DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref DataTable dtRikuz21To31, int iTzuga, float fErechRechiv45)
        {
            DataRow RowRikuz, RowRikuzBsisi;
            int i, iKodRechiv, iTempDay, j;
            DateTime dYom;
            DataColumn[] columnsDt = new DataColumn[16];
            DataRow[] rowDays;
            float fTotal;
            try
            {   //פונקציה המחזירה ריכוז חודשי לעובד

                dtRikuz1To10 = new DataTable();
                dtRikuz1To10.Columns.Add(new DataColumn("TEUR_RECHIV", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("TOTAL", Type.GetType("System.Decimal")));
                dtRikuz1To10.Columns.Add(new DataColumn("TOTAL_IN_HOUR", Type.GetType("System.Decimal")));
                dtRikuz1To10.Columns.Add(new DataColumn("KIZUZ_MEAL_MICHSA", Type.GetType("System.Decimal")));
                dtRikuz1To10.Columns.Add(new DataColumn("TOTAL_TO_PAY", Type.GetType("System.Decimal")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom1", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom2", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom3", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom4", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom5", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom6", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom7", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom8", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom9", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom10", Type.GetType("System.String")));
                dtRikuz1To10.Columns.Add(new DataColumn("Yom11", Type.GetType("System.String")));

                dtRikuz11To20 = new DataTable();
                dtRikuz11To20 = dtRikuz1To10.Copy();
                dtRikuz21To31 = new DataTable();
                dtRikuz21To31 = dtRikuz1To10.Copy();

                if (dt.Rows.Count != 0)
                {
                    fTotal = 0;
                    iKodRechiv = -1;

                    RowRikuzBsisi = dtRikuz1To10.NewRow();
                    i = 0;

                    do
                    {
                        if (int.Parse(dt.Rows[i]["KOD_RECHIV"].ToString()) != 0)
                        {
                            if (iKodRechiv != int.Parse(dt.Rows[i]["KOD_RECHIV"].ToString()))
                            {
                                iKodRechiv = int.Parse(dt.Rows[i]["KOD_RECHIV"].ToString());

                                RowRikuz = dtRikuz1To10.NewRow();

                                RowRikuzBsisi = dtRikuz1To10.NewRow();
                                RowRikuzBsisi["TEUR_RECHIV"] = dt.Rows[i]["TEUR_RECHIV"];
                                if (!string.IsNullOrEmpty(dt.Rows[i]["KIZUZ_MEAL_MICHSA"].ToString()))
                                {
                                    RowRikuzBsisi["KIZUZ_MEAL_MICHSA"] = (double.Parse(dt.Rows[i]["KIZUZ_MEAL_MICHSA"].ToString())/60).ToString("0.00");
                                }
                                fTotal = float.Parse(dt.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + iKodRechiv).ToString());
                                if (fTotal > 0)
                                {
                                    RowRikuzBsisi["TOTAL"] = fTotal.ToString("0.00");
                                    RowRikuzBsisi["TOTAL_IN_HOUR"] = (fTotal / 60).ToString("N2");
                                }

          
                                if (!RowRikuzBsisi["KIZUZ_MEAL_MICHSA"].Equals(DBNull.Value))
                                {
                                    RowRikuzBsisi["TOTAL_TO_PAY"] = ((decimal.Parse(RowRikuzBsisi["TOTAL_IN_HOUR"].ToString()) - decimal.Parse(RowRikuzBsisi["KIZUZ_MEAL_MICHSA"].ToString()))).ToString("0.00");
                                }
                                else { RowRikuzBsisi["TOTAL_TO_PAY"] = RowRikuzBsisi["TOTAL_IN_HOUR"]; }

                                if (iKodRechiv == clGeneral.enRechivim.KamutGmulChisachon.GetHashCode())
                                    RowRikuzBsisi["TOTAL_TO_PAY"] = fErechRechiv45.ToString("0.00");

                                RowRikuz.ItemArray = RowRikuzBsisi.ItemArray;

                                rowDays = dt.Select("KOD_RECHIV=" + iKodRechiv + " AND  day_taarich>=1 and  day_taarich<=10");
                                for (j = 0; j <= rowDays.Length - 1; j++)
                                {
                                    dYom = DateTime.Parse(rowDays[j]["TAARICH"].ToString());
                                    iTempDay = dYom.Day;
                                   
                                    if (float.Parse(rowDays[j]["ERECH_RECHIV"].ToString()) > 0)
                                    {
                                        RowRikuz["YOM" + iTempDay] =double.Parse(rowDays[j]["ERECH_RECHIV"].ToString()).ToString("0.00");
                                        if (iKodRechiv == clGeneral.enRechivim.KamutGmulChisachon.GetHashCode() && iTzuga==1)
                                        { RowRikuz["YOM" + iTempDay] = Math.Ceiling(double.Parse(RowRikuz["YOM" + iTempDay].ToString())).ToString("0.00"); }
                                
                                    }
                                    else { RowRikuz["YOM" + iTempDay] = rowDays[j]["HEARA"]; }
                                   
                                }
                                   
                                dtRikuz1To10.Rows.Add(RowRikuz);
                                RowRikuz = dtRikuz11To20.NewRow();
                                RowRikuz.ItemArray = RowRikuzBsisi.ItemArray;

                                rowDays = dt.Select("KOD_RECHIV=" + iKodRechiv + " AND  day_taarich>=11 and  day_taarich<=20");
                                for (j = 0; j <= rowDays.Length - 1; j++)
                                {
                                    dYom = DateTime.Parse(rowDays[j]["TAARICH"].ToString());
                                    iTempDay = dYom.Day;


                                    iTempDay = iTempDay % 10;
                                    if (iTempDay == 0) { iTempDay = 10; }
                                    if (float.Parse(rowDays[j]["ERECH_RECHIV"].ToString()) > 0)
                                    {
                                        RowRikuz["YOM" + iTempDay] = double.Parse(rowDays[j]["ERECH_RECHIV"].ToString()).ToString("0.00");
                                        if (iKodRechiv == clGeneral.enRechivim.KamutGmulChisachon.GetHashCode() && iTzuga == 1)
                                        { RowRikuz["YOM" + iTempDay] = Math.Ceiling(double.Parse(RowRikuz["YOM" + iTempDay].ToString())).ToString("0.00"); }
                                     }
                                    else { RowRikuz["YOM" + iTempDay] = rowDays[j]["HEARA"]; }
                                
                                }
                                  
                                dtRikuz11To20.Rows.Add(RowRikuz);
                                RowRikuz = dtRikuz21To31.NewRow();


                                RowRikuz.ItemArray = RowRikuzBsisi.ItemArray;

                                rowDays = dt.Select("KOD_RECHIV=" + iKodRechiv + " AND  day_taarich>=21 and  day_taarich<=31");
                                for (j = 0; j <= rowDays.Length - 1; j++)
                                {
                                    dYom = DateTime.Parse(rowDays[j]["TAARICH"].ToString());
                                    iTempDay = dYom.Day;

                                    if (iTempDay == 31) { iTempDay = 11; }
                                    else
                                    {
                                        iTempDay = iTempDay % 10;
                                        if (iTempDay == 0) { iTempDay = 10; }
                                    }
                                     if (float.Parse(rowDays[j]["ERECH_RECHIV"].ToString()) > 0)
                                    {
                                        RowRikuz["YOM" + iTempDay] = double.Parse(rowDays[j]["ERECH_RECHIV"].ToString()).ToString("0.00");
                                        if (iKodRechiv == clGeneral.enRechivim.KamutGmulChisachon.GetHashCode() && iTzuga == 1)
                                        { RowRikuz["YOM" + iTempDay] = Math.Ceiling(double.Parse(RowRikuz["YOM" + iTempDay].ToString())).ToString("0.00"); }
                                
                                     }
                                    else { RowRikuz["YOM" + iTempDay] = rowDays[j]["HEARA"]; }
                                }
                                   
                                dtRikuz21To31.Rows.Add(RowRikuz);
                               
                            }
                        }
                        i += 1;
                    } while (i < dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMeafyenLeOved(int iMisparIshi, DateTime dTaarich, clGeneral.enMeafyeneyOved iKodMeafyen)
        {
            clDal oDal = new clDal();
            try
            {   //פונקציה המחזירה מאפיין לעובד
                oDal.AddParameter("p_erech", ParameterType.ntOracleVarchar, null, ParameterDir.pdReturnValue, 200);

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_meafyen", ParameterType.ntOracleInteger, iKodMeafyen.GetHashCode(), ParameterDir.pdInput);

                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.ExecuteSP(clGeneral.cFunGetMeafyenLeOved);

                return oDal.GetValParam("p_erech");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetOvedSnifAndUnit(int iMisparIshi, ref string sSnif, ref string sUnit)
        {
            //פונקציה המחזירה את תאור הסניף והיחידה של עובד
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_teur_snif", ParameterType.ntOracleVarchar, "", ParameterDir.pdOutput, 400);
                oDal.AddParameter("p_teur_unit", ParameterType.ntOracleVarchar, "", ParameterDir.pdOutput, 400);

                oDal.ExecuteSP(clGeneral.cProGetOvedSnifUnit);
                sSnif = oDal.GetValParam("p_teur_snif") == "null" ? "" : oDal.GetValParam("p_teur_snif");
                sUnit = oDal.GetValParam("p_teur_Unit") == "null" ? "" : oDal.GetValParam("p_teur_Unit");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedCards(int iMisparIshi, string sMonth)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_month", ParameterType.ntOracleVarchar, sMonth, ParameterDir.pdInput, 100);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetOvedCards, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedCardsInTipul(int iMisparIshi)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetOvedCardsTipul, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public DataTable GetMeafyeneyBitzuaLeOvedAll(int iMisparIshi, DateTime dMeTaarich, DateTime dAdTaarich, int iBreratMechdal)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה מאפייני ביצוע  לעובד

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_me_taarich", ParameterType.ntOracleDate, dMeTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_ad_taarich", ParameterType.ntOracleDate, dAdTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_brerat_mechdal", ParameterType.ntOracleInteger, iBreratMechdal, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetMeafyeneyBituaLeovedAll, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetHostoriatMeafyeneLeOved(int iMisparIshi, DateTime dFromTaarich, int iCodeMeafyen)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה היסטורית מאפיין ביצוע  לעובד

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_from_taarich", ParameterType.ntOracleDate, dFromTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_code_meafyen", ParameterType.ntOracleInteger, iCodeMeafyen, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetHistoriatMeafyenLeoved, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable GetMonthsToOved(int iMisparIshi)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה  12 חודשי עבודה האחרונים של העובד

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetMonthWorkToOved, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMonthsHuavarLesacharToOved(int iMisparIshi)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה  12 חודשי עבודה האחרונים של העובד

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetMonthHuavarLesacharToOved, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStatusToOved(int iMisparIshi)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה  שורות סטטוס לעובד

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetStatusToOved, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetHostoriatNatunLeOved(int iMisparIshi, DateTime dFromTaarich, int iCodeNatun)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה היסטורית נתוני עובד

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_from_taarich", ParameterType.ntOracleDate, dFromTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_code_natun", ParameterType.ntOracleInteger, iCodeNatun, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetHistoriatNatunLeoved, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable GetRitzotChishuv(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה ריצות חישוב לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetRitzotChishuv, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetShaotMealMichsa(int iMisparIshi, DateTime dTaarich, long lBakashaId)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה ריצות חישוב לעובד
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetShaotMealMichsa, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertBakashaMechutzLemichsa(int iMisparIshi, DateTime dTaarich, int iShaot, string sSiba, int iUserId)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi ", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_shaot", ParameterType.ntOracleInteger, iShaot, ParameterDir.pdInput);
                oDal.AddParameter("p_siba", ParameterType.ntOracleVarchar, sSiba, ParameterDir.pdInput, 30);
                oDal.AddParameter("p_user_id", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);

                oDal.ExecuteSP(clGeneral.cProInsBakashMechutzLemichsa);

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetIshurim(clGeneral.enDemandType DemandType, int MisparIshi, string Month, clGeneral.enMonthlyQuotaForm FormType, string Filter)
        {
            _Dal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                _Dal.AddParameter("p_TypeDemand", ParameterType.ntOracleInteger, (int)DemandType, ParameterDir.pdInput);
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, MisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_Month", ParameterType.ntOracleVarchar, Month, ParameterDir.pdInput);
                _Dal.AddParameter("p_StatusIsuk", ParameterType.ntOracleInteger, (int)FormType, ParameterDir.pdInput);
                _Dal.AddParameter("p_Filter", ParameterType.ntOracleVarchar, Filter, ParameterDir.pdInput);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetHourApproval, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRelevantMonthOfApproval(int MisparIshi)
        {
            _Dal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, MisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetRelevantMonthOfApproval, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetMontlyAndSharedQuota(int MisparIshi, string Period, ref int MontlyQuota, ref int SharedQuota)
        {
            _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, MisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_Period", ParameterType.ntOracleVarchar, Period, ParameterDir.pdInput);
                _Dal.AddParameter("p_MonthlyQuota", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                _Dal.AddParameter("p_SharedQuota", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetSharedAndMonthlyQuota);
                MontlyQuota = clGeneral.GetIntegerValue(_Dal.GetValParam("p_MonthlyQuota"));
                SharedQuota = clGeneral.GetIntegerValue(_Dal.GetValParam("p_SharedQuota"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetStatusIsuk(int FormType, int MisparIshi, string Month)
        {
            _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_KodAgaf", ParameterType.ntOracleInteger, MisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_Form", ParameterType.ntOracleInteger, FormType, ParameterDir.pdInput);
                _Dal.AddParameter("p_Month", ParameterType.ntOracleVarchar, Month, ParameterDir.pdInput);
                _Dal.AddParameter("p_Result", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetStatusIsuk);
                return clGeneral.GetIntegerValue(_Dal.GetValParam("p_Result"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateRecordInIshurim(int Bakasha_ID, int KodStatusIshur, int MisparIshi, int KodIshur, DateTime Taarich, int MisparSidur, DateTime StartTime, DateTime EndTime, int NumberOfEntry, int Level, int ConfirmedValue, int ValueToConfirm, string siba, int MainFactor, int SecondaryFactor)
        {
            _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_Bakasha_ID", ParameterType.ntOracleInteger, Bakasha_ID, ParameterDir.pdInput);
                _Dal.AddParameter("p_kod_status_ishur", ParameterType.ntOracleInteger, KodStatusIshur, ParameterDir.pdInput);
                _Dal.AddParameter("p_MISPAR_ISHI", ParameterType.ntOracleInteger, MisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_KOD_ISHUR", ParameterType.ntOracleInteger, KodIshur, ParameterDir.pdInput);
                _Dal.AddParameter("p_TAARICH", ParameterType.ntOracleDate, Taarich, ParameterDir.pdInput);
                _Dal.AddParameter("p_MISPAR_SIDUR", ParameterType.ntOracleInteger, MisparSidur, ParameterDir.pdInput);
                _Dal.AddParameter("p_SHAT_HATCHALA", ParameterType.ntOracleDate, StartTime, ParameterDir.pdInput);
                _Dal.AddParameter("p_SHAT_YETZIA", ParameterType.ntOracleDate, EndTime, ParameterDir.pdInput);
                _Dal.AddParameter("p_MISPAR_KNISA", ParameterType.ntOracleInteger, NumberOfEntry, ParameterDir.pdInput);
                _Dal.AddParameter("p_RAMA", ParameterType.ntOracleInteger, Level, ParameterDir.pdInput);
                _Dal.AddParameter("p_ERECH_MEUSHAR", ParameterType.ntOracleInteger, ConfirmedValue, ParameterDir.pdInput);
                _Dal.AddParameter("p_ERECH_MEVUKASH", ParameterType.ntOracleInteger, ValueToConfirm, ParameterDir.pdInput);
                _Dal.AddParameter("P_SIBA", ParameterType.ntOracleVarchar, siba, ParameterDir.pdInput);
                _Dal.AddParameter("p_MainFactor", ParameterType.ntOracleInteger, MainFactor, ParameterDir.pdInput);
                _Dal.AddParameter("p_SecondaryFactor", ParameterType.ntOracleInteger, SecondaryFactor, ParameterDir.pdInput);
                _Dal.AddParameter("p_Result", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProUptHourApproval);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (Int32.Parse(_Dal.GetValParam("p_Result")) >= 0) ? true : false;

        }

        public bool UpdatePremiaDetails(int MisparIshi, DateTime Taarich, int SugPremia, int DakotPremia, DateTime TaarichIdkun, int MeadkenAcharon, int MisparIshiTrail, DateTime TaarichIdkunTrail, int SugPeula)
        {
            _Dal = new clDal();
            try
            {
                _Dal.AddParameter("p_MISPAR_ISHI", ParameterType.ntOracleInteger, MisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_TAARICH", ParameterType.ntOracleDate, Taarich, ParameterDir.pdInput);
                _Dal.AddParameter("p_SUG_PREMIA", ParameterType.ntOracleInteger, SugPremia, ParameterDir.pdInput);
                _Dal.AddParameter("p_DAKOT_PREMIA", ParameterType.ntOracleInteger, DakotPremia, ParameterDir.pdInput);
                _Dal.AddParameter("p_TAARICH_IDKUN_ACHARON", ParameterType.ntOracleDate, TaarichIdkun, ParameterDir.pdInput);
                _Dal.AddParameter("p_MEADKEN_ACHARON", ParameterType.ntOracleInteger, MeadkenAcharon, ParameterDir.pdInput);
                _Dal.AddParameter("p_MISPAR_ISHI_TRAIL", ParameterType.ntOracleInteger, MisparIshiTrail, ParameterDir.pdInput);
                _Dal.AddParameter("p_TAARICH_IDKUN_TRAIL", ParameterType.ntOracleDate, TaarichIdkunTrail, ParameterDir.pdInput);
                _Dal.AddParameter("p_Result", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProUptPremia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (Int32.Parse(_Dal.GetValParam("p_Result")) >= 0) ? true : false;
        }

        public string GetSugYechidaLeoved(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            string sSugYechida;
            try
            {
                oDal.AddParameter("p_sug_yechida", ParameterType.ntOracleVarchar, null, ParameterDir.pdReturnValue, 60);

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cProGetSugYechidaOved);

                sSugYechida = oDal.GetValParam("p_sug_yechida");

                return sSugYechida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetLastUpdateData(int iMisparIshi, DateTime dTaarich)
        {
            DataTable dt = new DataTable();
            clDal _Dal = new clDal();
            try
            {
                //מביא לכרטיס עבודה, את תאריך והשם של המשתמש שעדכן את הטבלאות אחרון
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetLastUpdateData, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetMerkazEruaByKod(int iMisparIshi, int iKodErech,DateTime dateCard)
        {
            clDal oDal = new clDal();
            int iErech = 0;

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_natun", ParameterType.ntOracleInteger, iKodErech, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dateCard, ParameterDir.pdInput);
                oDal.AddParameter("p_Erech", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cproGetMerkazEruaByKod);
                iErech = Int32.Parse(oDal.GetValParam("p_Erech"));
                return iErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMikumShaonInOut(int iMisparIshi, DateTime dTarich, ref int iMikumShaonKnisa, ref int iMikumShaonYetzia)
        {
            //פונקציה המחזירה מיקום שעון  - סידור ראשון כניסה ומיקום שעון  - סידור אחרון  יציאה עפ"י מספר אישי ותאריך   
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_tarich", ParameterType.ntOracleDate, dTarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Mikum_Shaon_Knisa", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_Mikum_Shaon_Yetzia", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetMikumShaonInOut);
                iMikumShaonKnisa = Int32.Parse(oDal.GetValParam("p_Mikum_Shaon_Knisa")) / 100;
                iMikumShaonYetzia = Int32.Parse(oDal.GetValParam("p_Mikum_Shaon_Yetzia")) / 100;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetDakot(int iMerkazErua, int iMikumYaad, DateTime dTarich, ref int iDakot)
        {
            //  get Dakot From "ZMAN NESIAA MISTANE"
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_merkaz_erua", ParameterType.ntOracleInteger, iMerkazErua, ParameterDir.pdInput);
                oDal.AddParameter("p_mikum_yaad", ParameterType.ntOracleInteger, iMikumYaad, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTarich, ParameterDir.pdInput);

                oDal.AddParameter("p_dakot", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cproGetZmanNesiaaMistane);
                iDakot = Int32.Parse(oDal.GetValParam("p_Dakot"));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetZmanNesiaaOvdim(int iMisparIshi, DateTime dTarich, ref int iZmanNesiaHaloch, ref int iZmanNesiaHazor)
        {
            //  get   "ZMAN NESIAA Haloch ,  ZMAN NESIAA Hazor" from TB_YAMEY_AVODA_OVDIM

            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_tarich", ParameterType.ntOracleDate, dTarich, ParameterDir.pdInput);

                oDal.AddParameter("p_zman_nesia_haloch", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_zman_nesia_hazor", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cproGetZmanNesiaaOvdim);
                if (oDal.GetValParam("p_zman_nesia_haloch") != "null")
                    iZmanNesiaHaloch = Int32.Parse(oDal.GetValParam("p_zman_nesia_haloch"));
                else iZmanNesiaHaloch = 0;
                if (oDal.GetValParam("p_zman_nesia_hazor") != "null")
                    iZmanNesiaHazor = Int32.Parse(oDal.GetValParam("p_zman_nesia_hazor"));
                else iZmanNesiaHazor = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdZmanNesiaa(int iMisparIshi, DateTime dTarich, int izmaNesiaHaloch, int iZmanNesiaHazor, int meadken_acharon)
        {
            //  Update   "ZMAN NESIAA Haloch ,  ZMAN NESIAA Hazor" on  TB_YAMEY_AVODA_OVDIM

            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_tarich", ParameterType.ntOracleDate, dTarich, ParameterDir.pdInput);
                oDal.AddParameter("p_zman_nesia_haloch", ParameterType.ntOracleInteger, izmaNesiaHaloch, ParameterDir.pdInput);
                oDal.AddParameter("p_zman_nesia_hazor", ParameterType.ntOracleInteger, iZmanNesiaHazor, ParameterDir.pdInput);
                oDal.AddParameter("p_meadken_acharon", ParameterType.ntOracleInteger, meadken_acharon, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cproProUpdZmanNesiaa);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetLastUpdator(int iMisparIshi, DateTime dTarich)
        {
            //  Get list  of last  updates (Date ,Id ,Name)

            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_tarich", ParameterType.ntOracleDate, dTarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetLastUpdator, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetRechivim()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //  Get  all CTB_rechivim ( Kod_Rechiv ,TEUR_RECHIV ) 
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cproGetRechivim, ref dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string cstr(string p)
        {
            throw new NotImplementedException();
        }

        public DataTable GetPeiluyotLeOved(int iMisparIshi, int iMisparSidur,DateTime dTaarichWc, DateTime dShatHatchlaSidur)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarichWc, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchlaSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetPeiluyotLeOved, ref dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveSidurimLeoved(COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd,
                                      COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd,
                                      COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd)
        {
            clDal objDal = new clDal();
            try
            {
                objDal.AddParameter("p_coll_yamey_avoda_ovdim", ParameterType.ntOracleArray, oCollYameyAvodaUpd, ParameterDir.pdInput, "COLL_YAMEY_AVODA_OVDIM");
                objDal.AddParameter("p_coll_sidurim_ovdim", ParameterType.ntOracleArray, oCollSidurimOvdimUpd, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                objDal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimUpd, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
              
                objDal.ExecuteSP(clGeneral.cProUpdSadotNosafim);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLastUpdate(int iMisparIshi, DateTime dCardDate)
        {
            clDal _Dal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(clGeneral.cProGetMeadkenAcharon, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedDetailsByTkufa(int iMisparIshi, DateTime dFrom, DateTime dTo)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: שם מעמד סניף ואיזור
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_start_date", ParameterType.ntOracleDate, dFrom, ParameterDir.pdInput);
                oDal.AddParameter("p_end_date", ParameterType.ntOracleDate, dTo, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvedDetailsBeTkufa, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvdimToPeriodByCode(int Code, DateTime dFrom, DateTime dTo)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_code", ParameterType.ntOracleInteger, Code, ParameterDir.pdInput);
                oDal.AddParameter("p_start_date", ParameterType.ntOracleDate, dFrom, ParameterDir.pdInput);
                oDal.AddParameter("p_end_date", ParameterType.ntOracleDate, dTo, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvdimToPeriodByCode, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FillObjIdkuneyRashemet(ref OBJ_IDKUN_RASHEMET _ObjIdkunRashemet,ref DataTable dtPakadim, int iMisparIshi,
                                            DateTime dCardDate, int iLoginUser, int iStatus)
        {
            _ObjIdkunRashemet.TAARICH = dCardDate;
            _ObjIdkunRashemet.MISPAR_ISHI = iMisparIshi;
            _ObjIdkunRashemet.MISPAR_SIDUR = 0;
            _ObjIdkunRashemet.SHAT_HATCHALA = DateTime.MinValue;
            _ObjIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.MinValue;
            _ObjIdkunRashemet.SHAT_YETZIA = DateTime.MinValue;
            _ObjIdkunRashemet.NEW_SHAT_YETZIA = DateTime.MinValue;
            _ObjIdkunRashemet.MISPAR_KNISA = 0;
            _ObjIdkunRashemet.GOREM_MEADKEN =iLoginUser;
            if (iStatus.Equals(1))
                _ObjIdkunRashemet.PAKAD_ID = clUtils.GetPakadId(dtPakadim, "MEASHER");
            else
                _ObjIdkunRashemet.PAKAD_ID = clUtils.GetPakadId(dtPakadim, "MISTAYEG");
        }
        private void SaveIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemet)
        {
            clDal Dal = new clDal();
            try
            {
                Dal.AddParameter("p_coll_idkun_rashemet", ParameterType.ntOracleArray, oCollIdkunRashemet, ParameterDir.pdInput, "COLL_IDKUN_RASHEMET");
                Dal.ExecuteSP(clGeneral.cProUpdIdkunRashemet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateIdkunyRashemet(int iMisparIshi, DateTime dCardDate, ref DataTable dtPakadim, int iLoginUser, int iStatus)
        {
            COLL_IDKUN_RASHEMET oCollIdkunRashemet = new COLL_IDKUN_RASHEMET();
            OBJ_IDKUN_RASHEMET _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
            FillObjIdkuneyRashemet(ref _ObjIdkunRashemet,ref  dtPakadim, iMisparIshi, dCardDate, iLoginUser,iStatus);            
            oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            SaveIdkunRashemet(oCollIdkunRashemet);
        }
        public void SetMeasherOMistayeg(int iMisparIshi, DateTime dCardDate, int iMeasherOMistayeg)
        {
            clDal oDal = new clDal();
            try
            {
                //עדכון TB_YAMEY_OVODA_OVDIM
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_status", ParameterType.ntOracleInteger, iMeasherOMistayeg, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cProSaveMeasherOmistayeg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPakadIdForMasach(int iMasachId)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_masach_id", ParameterType.ntOracleInteger, iMasachId, ParameterDir.pdInput);                
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetPakadId, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public string GetSibaLoLetashlum(int iMisparIshi, DateTime dCardDate,int iMisparSidur, DateTime dSidurFullDate)
        {
            clDal _Dal = new clDal();
            string sDescription = "";

            try
            {
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                _Dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                _Dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleVarchar, dSidurFullDate.ToString(), ParameterDir.pdInput,20);
                _Dal.AddParameter("p_teur_siba", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput,200);
                _Dal.ExecuteSP(clGeneral.cProGetSibaLoLetashlum);

                sDescription = _Dal.GetValParam("p_teur_siba") == "null" ? "" : _Dal.GetValParam("p_teur_siba").ToString();

                return sDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool CheckPeilutExist(int misparSidur,int misparIshi,DateTime shatSidur, DateTime shatYezia)
        {
            clDal _Dal = new clDal();
            int count;

            try
            {
                _Dal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, misparSidur, ParameterDir.pdInput);
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, misparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, shatSidur, ParameterDir.pdInput);
                _Dal.AddParameter("p_shat_yezia", ParameterType.ntOracleDate, shatYezia, ParameterDir.pdInput);

                _Dal.ExecuteSP(clGeneral.cFunCheckPeilutExist);

                count = int.Parse(_Dal.GetValParam("p_count").ToString());
                if (count > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsCardWasUpdated(int iMisparIshi, DateTime dTaarich)
        {
            clDal _Dal = new clDal();
            int count;

            try
            {
                _Dal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                _Dal.ExecuteSP(clGeneral.cFunIsCardLastUpdate);

                count = int.Parse(_Dal.GetValParam("p_count").ToString());
                if (count > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
       public bool CheckSidurExist(int misSidur,int misparIshi,DateTime shatSidur){
            clDal _Dal = new clDal();
            int count;

            try
            {
                _Dal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, misSidur, ParameterDir.pdInput);
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, misparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, shatSidur, ParameterDir.pdInput);
               
                _Dal.ExecuteSP(clGeneral.cFunCheckSidurExist);

                count = int.Parse(_Dal.GetValParam("p_count").ToString());
                if (count > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public  void DeleteInsertKnisotLePeilut(int iMisparIshi, int iMisparSidur, DateTime dTaarich, DateTime dShatHatchala, DateTime dShatYezia, long lMakat,int iMeadken_Acharon, COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns)
       {
           clTxDal oTxDal = new clTxDal();

           try
           {
               oTxDal.TxBegin();

               oTxDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
               oTxDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
               oTxDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
               oTxDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);
               oTxDal.AddParameter("p_shat_yezia", ParameterType.ntOracleDate, dShatYezia, ParameterDir.pdInput);
               oTxDal.AddParameter("p_makat_nesia", ParameterType.ntOracleLong, lMakat, ParameterDir.pdInput);
               oTxDal.AddParameter("p_meadken_acharon", ParameterType.ntOracleInteger, iMeadken_Acharon, ParameterDir.pdInput);
               oTxDal.ExecuteSP(clGeneral.cProDelKnisotPeilut);

               oTxDal.ClearCommand();

               oTxDal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimIns, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
               oTxDal.ExecuteSP(clGeneral.cProInsPeilutOvdim);
               oTxDal.TxCommit();

            }
           catch (Exception ex)
           {
               oTxDal.TxRollBack();
               throw ex;
           }
       }

       public void DeleteHachanotMechonaInsertPeiluyot(int iMisparIshi, int iMisparSidur, DateTime dTaarich, DateTime dShatHatchala,  COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns)
       {
           clTxDal oTxDal = new clTxDal();

           try
           {
               oTxDal.TxBegin();

               oTxDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
               oTxDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
               oTxDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
               oTxDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);

               oTxDal.ExecuteSP(clGeneral.cProDelHachanotMechona);

               oTxDal.ClearCommand();

               oTxDal.AddParameter("p_coll_obj_peilut_ovdim", ParameterType.ntOracleArray, oCollPeilutOvdimIns, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
               oTxDal.ExecuteSP(clGeneral.cProInsPeilutOvdim);
               oTxDal.TxCommit();

           }
           catch (Exception ex)
           {
               oTxDal.TxRollBack();
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


       public DataTable GetArachimLeOved(int iMisparIshi, DateTime dCardDate)
       {
           clDal _Dal = new clDal();
           DataTable dt = new DataTable();
           try
           {
               _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
               _Dal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
               _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
               _Dal.ExecuteSP(clGeneral.cProGetArachimLeoved, ref dt);

               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable GetPirteyOvedLetkufa(int iMisparIshi, DateTime startDate, DateTime endDate)
       {
           clDal _Dal = new clDal();
           DataTable dt = new DataTable();
           try
           {
               _Dal.AddParameter("p_mispar_ishi ", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
               _Dal.AddParameter("p_start", ParameterType.ntOracleDate, startDate, ParameterDir.pdInput);
               _Dal.AddParameter("p_end", ParameterType.ntOracleDate, endDate, ParameterDir.pdInput);
               _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
               _Dal.ExecuteSP(clGeneral.cProGetPirteyOvedLetkufot, ref dt);

               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable GetMeafyenimLeovedLetkufa(int iMisparIshi, DateTime startDate, DateTime endDate)
       {
           clDal _Dal = new clDal();
           DataTable dt = new DataTable();
           try
           {
               _Dal.AddParameter("p_mispar_ishi ", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
               _Dal.AddParameter("p_start", ParameterType.ntOracleDate, startDate, ParameterDir.pdInput);
               _Dal.AddParameter("p_end", ParameterType.ntOracleDate, endDate, ParameterDir.pdInput);
               _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
               _Dal.ExecuteSP(clGeneral.cProGetMeafyenOvedLetkufot, ref dt);

               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }


       public DataTable getRikuzimLeOved(int iMisparIshi, DateTime taarich)
       {
           clDal _Dal = new clDal();
           DataTable dt = new DataTable();
           try
           {
               _Dal.AddParameter("p_mispar_ishi ", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
               _Dal.AddParameter("p_tarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
               _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
               _Dal.ExecuteSP(clGeneral.cProGetRikuzeyAvodaLeoved, ref dt);

               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public int GetCountWorkCardNoShaotLetashlum(DateTime dTarMe, DateTime dTarAd, string sMaamad)
       {
           clDal oDal = new clDal();
           int iCount;
           try
           {
               oDal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
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
               oDal.ExecuteSP(clGeneral.cProGetCountWorkCardNoShaotLetahlum);

               iCount= int.Parse(oDal.GetValParam("p_count"));

               return iCount;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public int GetCountWCLoLetashlumWithMeafyenim(DateTime dTarMe, DateTime dTarAd)
       {
           clDal oDal = new clDal();
           int iCount;
           try
           {
               oDal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
               oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
               oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);

               oDal.ExecuteSP(clGeneral.GetCountWCLoLetashlumWithMeafyenim);

               iCount= int.Parse(oDal.GetValParam("p_count"));

               return iCount;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable GetWorkCardNoShaotLetashlum(DateTime dTarMe, DateTime dTarAd, string sMaamad)
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
               oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
               oDal.ExecuteSP(clGeneral.cProGetWorkCardNoShaotLetahlum,ref dt);

              
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}




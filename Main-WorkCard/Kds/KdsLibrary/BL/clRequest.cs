﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DalOraInfra.DAL;

namespace KdsLibrary.BL
{
    public class clRequest
    {

        public DataTable GetMaakavBakashot(int iRequestType,int iStatus,int iRequestNum,string sChodesh)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה ריצות חישוב
                if (iRequestNum >-1)
                {
                    oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iRequestNum, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, null, ParameterDir.pdInput);
                }
                if (iRequestType > 0)
                {
                    oDal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, iRequestType, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                if (iStatus > 0)
                {
                    oDal.AddParameter("p_status", ParameterType.ntOracleInteger, iStatus, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_status", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (sChodesh != "-1")
                {
                    oDal.AddParameter("p_chodesh", ParameterType.ntOracleVarchar, sChodesh, ParameterDir.pdInput, 7);
                }
                else
                {
                    oDal.AddParameter("p_chodesh", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput, 7);
                }
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetRequests, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequestList(string sPrefix,string sMonth)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {  
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                oDal.AddParameter("p_month", ParameterType.ntOracleVarchar,  sMonth, ParameterDir.pdInput);
               
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetRequestsList, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetListOvdimFromLogBakashot(string sPrefix, string sMonth,int iBakashaId)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleVarchar, sPrefix, ParameterDir.pdInput);
                if (iBakashaId == 0)
                {
                    oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInteger, iBakashaId, ParameterDir.pdInput);
                }
                oDal.AddParameter("p_month", ParameterType.ntOracleVarchar, sMonth, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetListOvdimFromLogBakashot, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLogBakashot(int iRequestType, int iMisparIshi, long iRequestNum, string sChodesh,string TypeMessage)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה לוג  בקשות 
                if (iRequestNum > -1){
                    oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iRequestNum, ParameterDir.pdInput);
                }
                else{
                    oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, null, ParameterDir.pdInput);
                }
                if (iRequestType > 0){
                    oDal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, iRequestType, ParameterDir.pdInput);
                }
                else{
                    oDal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                if (iMisparIshi > 0){
                    oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                }
                else{
                    oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (sChodesh!="-1"){
                oDal.AddParameter("p_chodesh", ParameterType.ntOracleVarchar, sChodesh, ParameterDir.pdInput, 7);
                }
                else{  oDal.AddParameter("p_chodesh", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput, 7);
                }
                if (TypeMessage != "0")
                    oDal.AddParameter("p_Type_Message", ParameterType.ntOracleChar, TypeMessage, ParameterDir.pdInput);
                else
                    oDal.AddParameter("p_Type_Message", ParameterType.ntOracleChar, null, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetLogBakashot, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLogTahalich(DateTime dFromDate, DateTime dToDate, int iProcessCode,   int iStatus)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה לוג  תהליך 
                oDal.AddParameter("p_from_date", ParameterType.ntOracleDate, dFromDate, ParameterDir.pdInput);

                oDal.AddParameter("p_from_date", ParameterType.ntOracleDate, dToDate, ParameterDir.pdInput);

                if (iProcessCode > -1)
                {
                    oDal.AddParameter("p_process_code", ParameterType.ntOracleInteger, iProcessCode, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_process_code", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (iStatus > -1)
                {
                    oDal.AddParameter("p_status", ParameterType.ntOracleVarchar, iStatus, ParameterDir.pdInput, 7);
                }
                else
                {
                    oDal.AddParameter("p_status", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput, 7);
                }

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetLogTahalich, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckTahalichEnd(long p_bakasha_id)
        {
            clDal _Dal = new clDal();
            int count;

            try
            {
                _Dal.AddParameter("p_count", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInteger, p_bakasha_id, ParameterDir.pdInput);

                _Dal.ExecuteSP(clGeneral.cFunCheckTahalichEnd);

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

        public long get_max_bakasha_id(int p_sug_bakasha)
        {
            clDal _Dal = new clDal();
            long bakasha_id;

            try
            {
                _Dal.AddParameter("p_bakasha", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, p_sug_bakasha, ParameterDir.pdInput);

                _Dal.ExecuteSP(clGeneral.cFunGetMaxBakasaBySug);

                bakasha_id = long.Parse(_Dal.GetValParam("p_bakasha").ToString());
                if (bakasha_id > 0)
                    return bakasha_id;
                else return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
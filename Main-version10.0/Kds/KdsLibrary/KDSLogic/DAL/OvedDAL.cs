﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.DAL;
using System.Data;
using KdsLibrary.DAL;

namespace KdsLibrary.KDSLogic.DAL
{
    public class OvedDAL : IOvedDAL
    {
        private const string cProUpdYameyAvodaOvdim = "pkg_errors.pro_upd_yamey_avoda_ovdim";
        private const string cProGetOvedYomAvodaDetails = "pkg_errors.pro_get_oved_yom_avoda_details";
        public const string cProGetOvedDetails = "pkg_errors.pro_get_oved_sidurim_peilut";
        public const string cProGetOvedMatzav = "pkg_errors.pro_get_oved_matzav";

        public DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate)

        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetOvedDetails, ref dt);

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
                oDal.ExecuteSP(cProGetOvedYomAvodaDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMeafyeneyBitzuaLeOved(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה מאפייני ביצוע  לעובד
                //כולל ברירות מחדל
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cProGetMeafyeneyBituaLeoved, ref dt);


                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetZmanNesia(int iMerkazErua, int iMikumYaad, DateTime dDate)
        {
            clDal oDal = new clDal();
            int iDakot = 0;
            try
            {
                //מחזיר את זמן נסיעה
                oDal.AddParameter("p_merkaz_erua", ParameterType.ntOracleInteger, iMerkazErua, ParameterDir.pdInput);
                oDal.AddParameter("p_mikum_yaad", ParameterType.ntOracleInteger, iMikumYaad, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_dakot", ParameterType.ntOracleInteger, iDakot, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetZmanNesia);

                iDakot = Int32.Parse(oDal.GetValParam("p_dakot"));

                return iDakot;
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
                oDal.ExecuteSP(cProGetOvedMatzav, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
    }
}
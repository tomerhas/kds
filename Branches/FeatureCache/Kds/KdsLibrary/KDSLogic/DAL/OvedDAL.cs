using System;
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

        
    }
}

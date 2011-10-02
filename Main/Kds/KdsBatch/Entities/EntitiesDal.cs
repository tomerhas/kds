using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch.Entities
{
    public class EntitiesDal
    {
        private const string cProGetShgiotNoActive = "Pkg_Errors.pro_get_shgiot_active"; 
      
        public DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_last_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetOvedDetails, ref dt);

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
                oDal.ExecuteSP(clDefinitions.cProGetOvedYomAvodaDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSidurimLeOved(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetErrorsActive()
        {
            clDal _Dal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(cProGetShgiotNoActive, ref dt);

                return dt;
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
                oDal.ExecuteSP(clDefinitions.cProGetOvedMatzav, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

    }
}

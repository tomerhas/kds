using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.DAL;
using System.Configuration;
using DalOraInfra.DAL;

namespace KdsLibrary.KDSLogic.DAL
{
    public class PeilutDAL : IPeilutDAL
    {
        public const string cProIsDuplicateTravel = "pkg_errors.pro_is_duplicate_travel";
        public const string cProGetMeafyeneyElements = "PKG_ELEMENTS.get_tmp_meafyeney_elementim";

        private IUnityContainer _container;
        public PeilutDAL(IUnityContainer container)
        {
            _container = container;
        }

        public bool IsDuplicateTravle(int iMisparIshi, DateTime dCardDate, long lMakatNesia, DateTime dShatYetzia, int iMisparKnisa, ref DataTable dt)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInt64, lMakatNesia, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate, dShatYetzia, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, iMisparKnisa, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProIsDuplicateTravel, ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public  DataTable GetTmpMeafyeneyElements(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetMeafyeneyElements, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

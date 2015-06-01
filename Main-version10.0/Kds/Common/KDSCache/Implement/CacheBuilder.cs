using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheInfra.Interfaces;
using System.Data;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using DalOraInfra.DAL;


namespace KDSCache.Implement
{
    public class CacheBuilder : ICacheBuilder
    {
        public const string cProGetYamimMeyuchadim = "pkg_utils.pro_get_yamim_meyuchadim";
        public const string cProGetSugeyYamimMeyuchadim = "pkg_utils.pro_get_sugey_yamim_meyuchadim";
        public const string cProGetLookUpTables = "pkg_errors.pro_get_lookup_tables";
        public const string cProGetSugSidurMeafyenim = "pkg_errors.pro_get_sug_sidur_meafyenim";
        public const string cProGetMutamut = "pkg_utils.pro_get_ctb_mutamut";
        public const string cProGetSibotLedivuchYadani = "pkg_utils.pro_get_sibot_ledivuch_yadani";
        public const string cProGetShgiotNoActive = "pkg_errors.pro_get_shgiot_no_active";
        public const string cProGetCtbElementim = "PKG_UTILS.pro_get_ctb_elementim";

        private IKDSCacheManager _kdsCacheManager;

        public CacheBuilder(IKDSCacheManager kdsCacheManager) //, IUnityContainer container)
        {
            _kdsCacheManager = kdsCacheManager;
        }

        public void Init()
        {
             _kdsCacheManager.AddItem(CachedItems.YamimMeyuhadim, GetYamimMeyuchadim());
             _kdsCacheManager.AddItem(CachedItems.SugeyYamimMeyuchadim, GetSugeyYamimMeyuchadim());
             _kdsCacheManager.AddItem(CachedItems.LookUpTables, GetLookUpTables());
             _kdsCacheManager.AddItem(CachedItems.SugeySidur, GetSugeySidur());
             _kdsCacheManager.AddItem(CachedItems.Mutamut, GetCtbMutamut());
             _kdsCacheManager.AddItem(CachedItems.SibotLedivuchYadani, GetCtbSibotLedivuchYadani());
             _kdsCacheManager.AddItem(CachedItems.ErrorTable, GetErrorTable());
             _kdsCacheManager.AddItem(CachedItems.Elementim, GetElementim());
        }

        private DataTable GetYamimMeyuchadim()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetYamimMeyuchadim, ref dt);
            return dt;
        }

        private DataTable GetSugeyYamimMeyuchadim()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetSugeyYamimMeyuchadim, ref dt);
            return dt;  
        }

        private DataTable GetLookUpTables()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetLookUpTables, ref dt);
            return dt;
        }

        private DataTable GetSugeySidur()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetSugSidurMeafyenim, ref dt);
            return dt;
        }


        private DataTable GetCtbMutamut()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetMutamut, ref dt);
            return dt;
        }


        private DataTable GetCtbSibotLedivuchYadani()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetSibotLedivuchYadani, ref dt);
            return dt;
        }

        private DataTable GetErrorTable()
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

        private DataTable GetElementim()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            oDal.ExecuteSP(cProGetCtbElementim, ref dt);
            return dt;
        }
    }
}

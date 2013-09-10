using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCache.Interfaces;
using CacheInfra.Interfaces;
using KDSCache.Enums;
using System.Data;
using KdsLibrary.DAL;
using Microsoft.Practices.Unity;


namespace KDSCache.Implement
{
    public class CacheBuilder : ICacheBuilder
    {
        public const string cProGetYamimMeyuchadim = "pkg_utils.pro_get_yamim_meyuchadim";
        public const string cProGetSugeyYamimMeyuchadim = "pkg_utils.pro_get_sugey_yamim_meyuchadim";

        private IKDSCacheManager _kdsCacheManager;

        public CacheBuilder(IKDSCacheManager kdsCacheManager)
        {
            _kdsCacheManager = kdsCacheManager;
        }

        public void Init()
        {
             _kdsCacheManager.AddItem(CachedItems.YamimMeyuhadim, GetYamimMeyuchadim());
             _kdsCacheManager.AddItem(CachedItems.SugeyYamimMeyuchadim, GetSugeyYamimMeyuchadim());

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

    }
}

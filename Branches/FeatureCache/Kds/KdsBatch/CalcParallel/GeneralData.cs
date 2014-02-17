using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
namespace KdsBatch
{
    public class SingleGeneralData  
    {
        static bool _IsCreated = false;
        static GeneralData Instance;
        static object objlock;
      
        private SingleGeneralData()
        { 
        }
        public static GeneralData GetInstance()
        {
            return Instance;
        }

        public static GeneralData GetInstance(DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi, int numProcess)
        {
            objlock = new object();
            if (!_IsCreated)
            {
                lock (objlock)
                {
                    if (!_IsCreated)
                    {
                        Instance = new GeneralData(TarMe, TarAd, sMaamad, rizaGorefet, mis_ishi, numProcess);
                        _IsCreated = true;
                    }
                }
            }
            return Instance;
        }
        public static void ResetObject()
        {
            Instance = null;
        //    GC.GetTotalMemory(true);
         }


    }

    public class GeneralData 
    {
        public DateTime _TarMe, _TarAd, _dTaarichHefreshim;
        public DataSet dsNetuneyChishuv;

        public DataTable dtYamimMeyuchadim { get; set; }
        public DataTable dtSugeyYamimMeyuchadim { get; set; }
        public DataTable dtParameters { get; set; }

        public List<clParametersDM> ListParameters{ get; set; }
        //Me>Ad
        public DataTable dtOvdimLechishuv { get; set; }
        public DataTable dtMichsaYomitAll { get; set; }
        public DataTable dtMeafyeneySugSidurAll { get; set; }
        public DataTable dtSidurimMeyuchRechivAll { get; set; }
        public DataTable dtSugeySidurRechivAll { get; set; }
        public DataTable dtPremyotAll { get; set; }
        public DataTable dtPremyotNihulTnuaAll { get; set; }
        public DataTable dtPremyotYadaniyotAll { get; set; }
        public DataTable dtBusNumbersAll { get; set; }
        public DataTable dtYemeyAvodaAll { get; set; }
        public DataTable dtPeiluyotFromTnuaAll { get; set; }
        public DataTable dtPirteyOvdimAll { get; set; }
        public DataTable dtMutamutAll { get; set; }
        public DataTable dtMeafyenyOvedAll { get; set; }
        public DataTable dtSugeyYechidaAll { get; set; }
        public DataTable dtPeiluyotOvdimAll { get; set; }
        public DataTable dtMatzavOvdim { get; set; }

        public GeneralData(DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi,int numProcess)
        {
            DataRow[] drs;
            _TarMe = TarMe;
            _TarAd = TarAd;
            GetNetunimLechishuv(TarMe, TarAd, sMaamad, rizaGorefet, mis_ishi, numProcess);

             dtOvdimLechishuv = dsNetuneyChishuv.Tables["Ovdim"];
             if (mis_ishi == -1) //premiot
             {
                 if (dtOvdimLechishuv.Rows.Count > 0)
                 {
                     drs = dtOvdimLechishuv.Select("taarich>'01/01/0001'", "taarich ASC");
                     _TarMe = DateTime.Parse(drs[0]["taarich"].ToString());

                     drs = dtOvdimLechishuv.Select("taarich>'01/01/0001'", "taarich DESC");
                     _TarAd = DateTime.Parse(drs[0]["taarich"].ToString()).AddMonths(1).AddDays(-1);
                 }
             }


             if (mis_ishi > 0)
             {
                 _dTaarichHefreshim = GetTaarichHefreshim(_TarMe, mis_ishi);
                 if (_dTaarichHefreshim == DateTime.MinValue)
                     _dTaarichHefreshim = DateTime.Parse("01/" + (_TarAd.Date.ToString("MM/yyyy")));
                 else _dTaarichHefreshim = _dTaarichHefreshim.AddDays(1);
             }
             else
                 _dTaarichHefreshim = DateTime.Parse("01/" + (_TarAd.Date.ToString("MM/yyyy")));
                
            InitGeneralData();
        }
        private void InitGeneralData()
        {
            
            clCalcDal oCalcDal = new clCalcDal();
            clUtils oUtils = new clUtils();
            try
            {
                var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();

                dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);// clGeneral.GetYamimMeyuchadim();
                dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);// clGeneral.GetSugeyYamimMeyuchadim();
                dtParameters = oUtils.GetKdsParametrs();
                InitListParamObject();
                
                dtPremyotAll = dsNetuneyChishuv.Tables["Premiot_View"];
                dtPremyotNihulTnuaAll = dsNetuneyChishuv.Tables["Premiot_NihulTnua"];
                dtPremyotYadaniyotAll = dsNetuneyChishuv.Tables["Premiot_Yadaniot"]; 
                dtMichsaYomitAll = dsNetuneyChishuv.Tables["Michsa_Yomit"];
                dtMeafyeneySugSidurAll = oUtils.InitDtMeafyeneySugSidur(_TarMe, _TarAd);
                dtSidurimMeyuchRechivAll = dsNetuneyChishuv.Tables["Sidur_Meyuchad_Rechiv"]; 
                dtSugeySidurRechivAll = dsNetuneyChishuv.Tables["Sug_Sidur_Rechiv"]; 
                dtBusNumbersAll = dsNetuneyChishuv.Tables["Buses_Details"]; 
                dtYemeyAvodaAll = dsNetuneyChishuv.Tables["Yemey_Avoda"]; 
                if (dsNetuneyChishuv.Tables["Kavim_Details"] != null)
                    dtPeiluyotFromTnuaAll = dsNetuneyChishuv.Tables["Kavim_Details"]; 
                else dtPeiluyotFromTnuaAll = null;
                dtPirteyOvdimAll = dsNetuneyChishuv.Tables["Pirtey_Ovdim"]; 
                dtMutamutAll = dsNetuneyChishuv.Tables["Ctb_Mutamut"]; 
                dtPeiluyotOvdimAll = dsNetuneyChishuv.Tables["Peiluyot_Ovdim"]; 
                dtSugeyYechidaAll = dsNetuneyChishuv.Tables["Sug_Yechida"];
                dtMeafyenyOvedAll = dsNetuneyChishuv.Tables["Meafyeney_Ovdim"];
                dtMatzavOvdim = dsNetuneyChishuv.Tables["Matzav_Ovdim"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCalcDal = null;
                oUtils = null;
            }
        }

        private  void InitListParamObject()
        {
            clParametersDM itemParams;
            int sugYom;
            DateTime dTarMe = _TarMe;
            try
            { 
                ListParameters  = new List<clParametersDM>();
                while (dTarMe <= _TarAd)
                {
                    sugYom = clGeneral.GetSugYom(dtYamimMeyuchadim, dTarMe, dtSugeyYamimMeyuchadim);
                    //itemParams = new clParameters(dTarMe, sugYom,"Calc", dtParameters);
                    IParametersManager clPramsManager = ServiceLocator.Current.GetInstance<IParametersManager>();
                    itemParams = clPramsManager.CreateClsParametrs(dTarMe, sugYom, "Calc", dtParameters);
                    ListParameters.Add(itemParams);
                    dTarMe = dTarMe.AddDays(1);
                    itemParams = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetNetunimLechishuv(DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi,int numProcess)
        {
            clCalcDal oCalcDal = new clCalcDal();
            try
            {
                dsNetuneyChishuv = oCalcDal.GetNetuneyChishuvDS(TarMe, TarAd, sMaamad, rizaGorefet, mis_ishi, numProcess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCalcDal = null;
            }
        }

        private DateTime GetTaarichHefreshim(DateTime Taarich, int mis_ishi)
        {
            clCalcDal oCalcDal = new clCalcDal();
            try
            {
                return(oCalcDal.GetTaarichHefreshim(Taarich, mis_ishi));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCalcDal = null;
            }
        }
        
    }
}

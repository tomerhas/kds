using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;
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

        public static GeneralData GetInstance(DateTime TarMe , DateTime TarAd,string sMaamad,bool rizaGorefet,int mis_ishi)
        {
            objlock = new object();
            if (!_IsCreated)
            {
                lock (objlock)
                {
                    if (!_IsCreated)
                    {
                        Instance = new GeneralData(TarMe, TarAd, sMaamad, rizaGorefet, mis_ishi);
                        _IsCreated = true;
                    }
                }
            }
            return Instance;
        }
        public static void ResetObject()
        {
            _IsCreated = false;
            Instance = null;
        }
    }
    
    public class GeneralData
    {
        private clParameters objParameters;
        private DateTime _TarMe,_TarAd;
        private DataSet dsNetuneyChishuv;

        public DataTable _dtYamimMeyuchadim { get; set; }
        public DataTable _dtSugeyYamimMeyuchadim { get; set; }
        public DataTable _dtParameters { get; set; }

        public List<clParameters> ListParameters{ get; set; }
        //Me>Ad
        public DataTable _dtOvdimLechishuv { get; set; }
        public DataTable _dtMichsaYomitAll { get; set; }
        public DataTable _dtMeafyeneySugSidurAll { get; set; }
        public DataTable _dtSidurimMeyuchRechivAll { get; set; }
        public DataTable _dtSugeySidurRechivAll { get; set; }
        public DataTable _dtSugeySidurAll { get; set; }
        public DataTable _dtPremyotAll { get; set; }
        public DataTable _dtPremyotYadaniyotAll { get; set; }
        public DataTable _dtBusNumbersAll { get; set; }
        public DataTable _dtYemeyAvodaAll { get; set; }
        public DataTable _dtPeiluyotFromTnuaAll { get; set; }
        public DataTable _dtPirteyOvdimAll { get; set; }
        public DataTable _dtMutamutAll { get; set; }
        public DataTable _dtMeafyenyOvedAll { get; set; }
        public DataTable _dtSugeyYechidaAll { get; set; }
        public DataTable _dtPeiluyotOvdimAll;

        public GeneralData(DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi)
        {
            DataRow[] drs;
            _TarMe = TarMe;
            _TarAd = TarAd;
            GetNetunimLechishuv(TarMe, TarAd, sMaamad, rizaGorefet, mis_ishi);

             _dtOvdimLechishuv = dsNetuneyChishuv.Tables["Ovdim"]; 
            if (mis_ishi == -1) //premiot
            {
                if (_dtOvdimLechishuv.Rows.Count > 0)
                {
                    drs = _dtOvdimLechishuv.Select("CHODESH", "CHODESH ASC");
                    _TarMe = DateTime.Parse(drs[0]["CHODESH"].ToString());

                    drs = _dtOvdimLechishuv.Select("CHODESH", "CHODESH DESC");
                    _TarAd = DateTime.Parse(drs[0]["CHODESH"].ToString()).AddMonths(1).AddDays(-1);
                }
            }
            clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "before InitGeneralData");
            InitGeneralData();
            clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "After InitGeneralData");
        }
        private void InitGeneralData()
        {
            
            clCalcDal oCalcDal = new clCalcDal();
            clUtils oUtils = new clUtils();
            try
            {
                _dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                _dtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                _dtParameters = oUtils.GetKdsParametrs();
                 InitListParamObject();
                
                 _dtPremyotAll = dsNetuneyChishuv.Tables["Premiot_View"]; // oCalcDal.getPremyot();
                 _dtPremyotYadaniyotAll = dsNetuneyChishuv.Tables["Premiot_Yadaniot"]; // oCalcDal.getPremyotYadaniyot();
                 _dtMichsaYomitAll = dsNetuneyChishuv.Tables["Michsa_Yomit"]; //oCalcDal.GetMichsaYomitLechodesh(_TarMe, _TarAd);
                _dtMeafyeneySugSidurAll = oUtils.InitDtMeafyeneySugSidur(_TarMe, _TarAd);
                _dtSidurimMeyuchRechivAll = dsNetuneyChishuv.Tables["Sidur_Meyuchad_Rechiv"]; // oCalcDal.SetSidurimMeyuchaRechiv(_TarMe, _TarAd);
                _dtSugeySidurRechivAll = dsNetuneyChishuv.Tables["Sug_Sidur_Rechiv"]; //oCalcDal.GetSugeySidurRechiv(_TarMe, _TarAd);
                _dtSugeySidurAll = dsNetuneyChishuv.Tables["Sugey_Sidur_Tnua"]; // oCalcDal.GetSugeySidur();
                _dtBusNumbersAll = dsNetuneyChishuv.Tables["Buses_Details"]; //oCalcDal.GetBusesDetails();
                _dtYemeyAvodaAll = dsNetuneyChishuv.Tables["Yemey_Avoda"]; //oCalcDal.GetYemeyAvoda();
                if (dsNetuneyChishuv.Tables["Kavim_Details"] != null)
                    _dtPeiluyotFromTnuaAll = dsNetuneyChishuv.Tables["Kavim_Details"]; //oCalcDal.GetKatalogKavim();
                else _dtPeiluyotFromTnuaAll = null;
                _dtPirteyOvdimAll = dsNetuneyChishuv.Tables["Pirtey_Ovdim"]; //oCalcDal.GetPirteyOvdim();
                _dtMutamutAll = dsNetuneyChishuv.Tables["Ctb_Mutamut"]; 
                _dtPeiluyotOvdimAll = dsNetuneyChishuv.Tables["Peiluyot_Ovdim"]; //oCalcDal.GetPeiluyotOvdim();               
                _dtSugeyYechidaAll = dsNetuneyChishuv.Tables["Sug_Yechida"]; //oCalcDal.GetSugYechida();
                _dtMeafyenyOvedAll = dsNetuneyChishuv.Tables["Meafyeney_Ovdim"]; //oCalcDal.GetMeafyeneyBitzuaLeOvedAll(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private  void InitListParamObject()
        {
            clUtils oUtils = new clUtils();
            clParameters itemParams;
            int sugYom;
            DateTime dTarMe = _TarMe;
            try
            { 
                ListParameters  = new List<clParameters>();
                while (dTarMe <= _TarAd)
                {
                    sugYom = clGeneral.GetSugYom(_dtYamimMeyuchadim, dTarMe, _dtSugeyYamimMeyuchadim);
                    itemParams = new clParameters(dTarMe, sugYom,"Calc", _dtParameters);
                    ListParameters.Add(itemParams);
                    dTarMe = dTarMe.AddDays(1);
                }
            }
            catch (Exception ex)
            {
              //   clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }

        private void GetNetunimLechishuv(DateTime TarMe, DateTime TarAd, string sMaamad, bool rizaGorefet, int mis_ishi)
        {
            clCalcDal oCalcDal = new clCalcDal();
            try
            {
                dsNetuneyChishuv = oCalcDal.GetNetuneyChishuvDS(TarMe, TarAd, sMaamad, rizaGorefet, mis_ishi);
            }
            catch (Exception ex)
            {
                //   clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;
namespace KdsBatch.CalcNew
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

        public static GeneralData GetInstance(DateTime TarMe , DateTime TarAd)
        {
            objlock = new object();
            if (!_IsCreated)
            {
                lock (objlock)
                {
                    if (!_IsCreated)
                    {
                        Instance = new GeneralData(TarMe, TarAd);
                        _IsCreated = true;
                    }
                }
            }
            return Instance;
        }
    }
    public class GeneralData
    {
        private clParameters objParameters;
        private DateTime _TarMe,_TarAd;

        public DataTable _dtYamimMeyuchadim { get; set; }
        public DataTable _dtSugeyYamimMeyuchadim { get; set; }
        public DataTable _dtParameters { get; set; }

        public List<clParameters> ListParameters{ get; set; }
        //Me>Ad
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
        public DataTable _dtMeafyenyOvedAll { get; set; }
        public DataTable _dtSugeyYechidaAll { get; set; }
        public DataTable _dtPeiluyotOvdimAll;

        public GeneralData(DateTime  TarMe, DateTime TarAd)
        {
            _TarMe = TarMe;
            _TarAd = TarAd;
            InitGeneralData();
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
                _dtPremyotAll = oCalcDal.getPremyot();
                _dtPremyotYadaniyotAll = oCalcDal.getPremyotYadaniyot();
                _dtMichsaYomitAll = oCalcDal.GetMichsaYomitLechodesh(_TarMe, _TarAd);
                _dtMeafyeneySugSidurAll = oUtils.InitDtMeafyeneySugSidur(_TarMe, _TarAd);
                _dtSidurimMeyuchRechivAll = oCalcDal.SetSidurimMeyuchaRechiv(_TarMe, _TarAd);
                _dtSugeySidurRechivAll = oCalcDal.GetSugeySidurRechiv(_TarMe, _TarAd);          
                _dtSugeySidurAll = oCalcDal.GetSugeySidur();
                _dtBusNumbersAll = oCalcDal.GetBusesDetails();
                _dtYemeyAvodaAll = oCalcDal.GetYemeyAvoda();
                _dtPeiluyotFromTnuaAll = oCalcDal.GetKatalogKavim();
                _dtPirteyOvdimAll = oCalcDal.GetPirteyOvdim();
                _dtPeiluyotOvdimAll = oCalcDal.GetPeiluyotOvdim();               
                _dtSugeyYechidaAll = oCalcDal.GetSugYechida();
                _dtMeafyenyOvedAll = oCalcDal.GetMeafyeneyBitzuaLeOvedAll(1);
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
                //  clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }
        
    }
}

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
      

            Instance.dtYamimMeyuchadim= null;
            Instance.dtSugeyYamimMeyuchadim= null;
            Instance.dtParameters= null;
            Instance.dtOvdimLechishuv= null;
            Instance.dtMichsaYomitAll= null;
            Instance.dtMeafyeneySugSidurAll= null;
            Instance.dtSidurimMeyuchRechivAll= null;
            Instance.dtSugeySidurRechivAll= null;
          //  Instance.dtSugeySidurAll= null;
            Instance.dtPremyotAll= null;
            Instance.dtPremyotYadaniyotAll= null;
            Instance.dtBusNumbersAll= null;
            Instance.dtYemeyAvodaAll= null;
            Instance.dtPeiluyotFromTnuaAll= null;
            Instance.dtPirteyOvdimAll= null;
            Instance.dtMutamutAll= null;
            Instance.dtMeafyenyOvedAll= null;
            Instance.dtSugeyYechidaAll= null;
            Instance.dtPeiluyotOvdimAll= null;
            Instance.dtOvdimShePutru = null;
            Instance.ListParameters = null;
            Instance.dsNetuneyChishuv = null;
            _IsCreated = false;
            Instance = null;

          //  GC.Collect();
            GC.GetTotalMemory(true); 
        }


    }

    public class GeneralData 
    {
       // private clParameters objParameters;
        private DateTime _TarMe,_TarAd;
        public DataSet dsNetuneyChishuv;

        public DataTable dtYamimMeyuchadim { get; set; }
        public DataTable dtSugeyYamimMeyuchadim { get; set; }
        public DataTable dtParameters { get; set; }

        public List<clParameters> ListParameters{ get; set; }
        //Me>Ad
        public DataTable dtOvdimLechishuv { get; set; }
        public DataTable dtMichsaYomitAll { get; set; }
        public DataTable dtMeafyeneySugSidurAll { get; set; }
        public DataTable dtSidurimMeyuchRechivAll { get; set; }
        public DataTable dtSugeySidurRechivAll { get; set; }
       // public DataTable dtSugeySidurAll { get; set; }
        public DataTable dtPremyotAll { get; set; }
        public DataTable dtPremyotYadaniyotAll { get; set; }
        public DataTable dtBusNumbersAll { get; set; }
        public DataTable dtYemeyAvodaAll { get; set; }
        public DataTable dtPeiluyotFromTnuaAll { get; set; }
        public DataTable dtPirteyOvdimAll { get; set; }
        public DataTable dtMutamutAll { get; set; }
        public DataTable dtMeafyenyOvedAll { get; set; }
        public DataTable dtSugeyYechidaAll { get; set; }
        public DataTable dtPeiluyotOvdimAll { get; set; }
        public DataTable dtOvdimShePutru { get; set; }

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
            //clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "before InitGeneralData");
            InitGeneralData();
            //clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now.Date, "After InitGeneralData");
        }
        private void InitGeneralData()
        {
            
            clCalcDal oCalcDal = new clCalcDal();
            clUtils oUtils = new clUtils();
            try
            {
                dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                dtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                dtParameters = oUtils.GetKdsParametrs();
                 InitListParamObject();
                
                 dtPremyotAll = dsNetuneyChishuv.Tables["Premiot_View"]; // oCalcDal.getPremyot();
                 dtPremyotYadaniyotAll = dsNetuneyChishuv.Tables["Premiot_Yadaniot"]; // oCalcDal.getPremyotYadaniyot();
                 dtMichsaYomitAll = dsNetuneyChishuv.Tables["Michsa_Yomit"]; //oCalcDal.GetMichsaYomitLechodesh(_TarMe, _TarAd);
                dtMeafyeneySugSidurAll = oUtils.InitDtMeafyeneySugSidur(_TarMe, _TarAd);
                dtSidurimMeyuchRechivAll = dsNetuneyChishuv.Tables["Sidur_Meyuchad_Rechiv"]; // oCalcDal.SetSidurimMeyuchaRechiv(_TarMe, _TarAd);
                dtSugeySidurRechivAll = dsNetuneyChishuv.Tables["Sug_Sidur_Rechiv"]; //oCalcDal.GetSugeySidurRechiv(_TarMe, _TarAd);
               // dtSugeySidurAll = dsNetuneyChishuv.Tables["Sugey_Sidur_Tnua"]; // oCalcDal.GetSugeySidur();
                dtBusNumbersAll = dsNetuneyChishuv.Tables["Buses_Details"]; //oCalcDal.GetBusesDetails();
                dtYemeyAvodaAll = dsNetuneyChishuv.Tables["Yemey_Avoda"]; //oCalcDal.GetYemeyAvoda();
                if (dsNetuneyChishuv.Tables["Kavim_Details"] != null)
                    dtPeiluyotFromTnuaAll = dsNetuneyChishuv.Tables["Kavim_Details"]; //oCalcDal.GetKatalogKavim();
                else dtPeiluyotFromTnuaAll = null;
                dtPirteyOvdimAll = dsNetuneyChishuv.Tables["Pirtey_Ovdim"]; //oCalcDal.GetPirteyOvdim();
                dtMutamutAll = dsNetuneyChishuv.Tables["Ctb_Mutamut"]; 
                dtPeiluyotOvdimAll = dsNetuneyChishuv.Tables["Peiluyot_Ovdim"]; //oCalcDal.GetPeiluyotOvdim();               
                dtSugeyYechidaAll = dsNetuneyChishuv.Tables["Sug_Yechida"]; //oCalcDal.GetSugYechida();
                dtMeafyenyOvedAll = dsNetuneyChishuv.Tables["Meafyeney_Ovdim"]; //oCalcDal.GetMeafyeneyBitzuaLeOvedAll(1);
                dtOvdimShePutru = dsNetuneyChishuv.Tables["Ovdim_ShePutru"];
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
            clParameters itemParams;
            int sugYom;
            DateTime dTarMe = _TarMe;
            try
            { 
                ListParameters  = new List<clParameters>();
                while (dTarMe <= _TarAd)
                {
                    sugYom = clGeneral.GetSugYom(dtYamimMeyuchadim, dTarMe, dtSugeyYamimMeyuchadim);
                    itemParams = new clParameters(dTarMe, sugYom,"Calc", dtParameters);
                    ListParameters.Add(itemParams);
                    dTarMe = dTarMe.AddDays(1);
                    itemParams = null;
                }
            }
            catch (Exception ex)
            {
              //   clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
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
                //   clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
            finally
            {
                oCalcDal = null;
            }
        }
    }
}

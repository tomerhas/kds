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
    public class Oved : IDisposable
    {
        public int Mispar_ishi;
        public DateTime Month;
        public GeneralData oGeneralData;
        public long iBakashaId;
        public bool bChishuvYom { get; set; }
        public DateTime _dDay { get; set; }

        public List<clPirteyOved> PirteyOved { get; set; }
        public DataTable DtSugeyYechida { set; get; }
        public DataTable DtYemeyAvoda { set; get; }
        public DataTable DtPeiluyotFromTnua { set; get; }
        public DataTable DtPeiluyotOved { set; get; }
        public List<clMeafyenyOved> MeafyeneyOved { get; set; }
        public DataTable dtPremyotYadaniyot { set; get; }
        public DataTable dtPremyotNihulTnua { set; get; }
        public DataTable dtPremyot { set; get; }
        public DataTable dtMatzavOved { set; get; }
        public string sSugYechida { get; set; }
        public float fMekademNipuach { set; get; }
        public float fmichsatYom { get; set; }
        public DataSet _dsChishuv { set; get; }
        private DataTable _DtMonth; 
        private DataTable _DtDay;
        private DataTable _DtSidur;
        private DataTable _DtPeilut;
        public DateTime dTchilatAvoda;
        public string sMatazavOved;
        public int iKodHeadrut=0;
        public DateTime dSiyumAvoda;
        private float _fHashlamaAlCheshbonNosafot;
        public clParameters objParameters { get; set; }
        public clPirteyOved objPirteyOved { get; set; }
        public clMeafyenyOved objMeafyeneyOved { get; set; }

        public DateTime Taarich { get; set; }
        public int SugYom { get; set; }

        public DataTable DtYemeyAvodaYomi { set; get; }
        public DataTable DtPeiluyotYomi { set; get; }
        public DataTable DtPeiluyotTnuaYomi { set; get; }

        public float fTotalAruchatZaharimForDay { set; get; }
        public float fHashlamaAlCheshbonNosafot
        {
            set { _fHashlamaAlCheshbonNosafot = float.Parse(Math.Round(value, 3).ToString()); } 
           get { return _fHashlamaAlCheshbonNosafot; } }
        
        public Oved(int mis_ishi, DateTime month, DateTime tarMe, DateTime tarAd, long BakashaId)
        {
            if (BakashaId == 0 || BakashaId == 1)
                oGeneralData = new GeneralData(tarMe, tarAd, "", false, mis_ishi, 0); //SingleGeneralData.GetInstance(tarMe, tarAd, "", false, mis_ishi, 0);
            Mispar_ishi = mis_ishi;
            Month = month;
            iBakashaId = BakashaId;
        }
        public Oved(int mis_ishi, DateTime dDay, long BakashaId)
        {
            oGeneralData = new GeneralData(dDay, dDay, "", false, mis_ishi, 0); // SingleGeneralData.GetInstance(dDay, dDay, "", false, mis_ishi,0);
            Mispar_ishi = mis_ishi;
            Month = DateTime.Parse("01/" + dDay.Month + "/" + dDay.Year);
            _dDay = dDay;
            bChishuvYom = true;
            iBakashaId = BakashaId;
        }
        public void SetNetunimLeOved()
        {
            try
            {
                fmichsatYom = 0;
                if (iBakashaId > 1)
                    oGeneralData = SingleGeneralData.GetInstance();
               
                InitPremyotYadaniyot();
                InitPremyotNihulTnua();
                InitPremyot();
                InitPirteyOvedList();
                InitDtYemeyAvoda();
                InitDtPeiluyotFromTnua();
                InitDtPeiluyotLeOved();
                InitMeafyenyOved();
                InitSugeyYechida();

                InitDataSetChishuv();
                InitMatzavOved();
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "SetNetunimLeOved: " + ex.Message);
                throw ex;
            }
        }


        private void InitPremyotYadaniyot()
        {

            DataRow[] rows;
            dtPremyotYadaniyot = new DataTable();
            try
            {
              
                if (oGeneralData.dtPremyotYadaniyotAll != null && oGeneralData.dtPremyotYadaniyotAll.Rows.Count > 0)
                {
                    oGeneralData.dtPremyotYadaniyotAll.Select(null, "mispar_ishi");
                  
                    rows = oGeneralData.dtPremyotYadaniyotAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich = Convert('" + Month.ToShortDateString() + "' , 'System.DateTime') ");
                    if (rows.Length > 0)
                    {
                        dtPremyotYadaniyot = rows.CopyToDataTable();
                    }
                    else
                    {
                        dtPremyotYadaniyot = oGeneralData.dtPremyotYadaniyotAll.Clone();
                    }
                }
                else
                {
                    dtPremyotYadaniyot = oGeneralData.dtPremyotYadaniyotAll.Clone();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitPremyotYadaniyot: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }

        
       private void InitPremyotNihulTnua()
        {

            DataRow[] rows;
            dtPremyotNihulTnua = new DataTable();
            try
            {

                if (oGeneralData.dtPremyotNihulTnuaAll != null && oGeneralData.dtPremyotNihulTnuaAll.Rows.Count > 0)
                {
                    oGeneralData.dtPremyotNihulTnuaAll.Select(null, "mispar_ishi");

                    rows = oGeneralData.dtPremyotNihulTnuaAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich = Convert('" + Month.ToShortDateString() + "' , 'System.DateTime') ");
                    if (rows.Length > 0)
                    {
                        dtPremyotNihulTnua = rows.CopyToDataTable();
                    }
                    else
                    {
                        dtPremyotNihulTnua = oGeneralData.dtPremyotNihulTnuaAll.Clone();
                    }
                }
                else
                {
                    dtPremyotNihulTnua = oGeneralData.dtPremyotNihulTnuaAll.Clone();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitPremyotYadaniyot: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }

        private void InitPremyot()
        {

            DataRow[] rows;
            dtPremyot = new DataTable();
            try
            {
               
                if (oGeneralData.dtPremyotAll != null && oGeneralData.dtPremyotAll.Rows.Count > 0)
                {
                  oGeneralData.dtPremyotAll.Select(null, "mispar_ishi");
            
                    rows = oGeneralData.dtPremyotAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich = Convert('" + Month.ToShortDateString() + "' , 'System.DateTime') ");
                    if (rows.Length > 0)
                    {
                        dtPremyot = rows.CopyToDataTable();
                    }
                    else
                    {
                        dtPremyot = oGeneralData.dtPremyotAll.Clone();
                    }
                }
                else
                {
                    dtPremyot = oGeneralData.dtPremyotAll.Clone();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitPremyot: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }
        private void InitPirteyOvedList()
        {
            clPirteyOved itemPirteyOved;
            DateTime dTarMe = Month;
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            try
            {
                PirteyOved = new List<clPirteyOved>();
                oGeneralData.dtPirteyOvdimAll.Select(null, "mispar_ishi");

                rows = oGeneralData.dtPirteyOvdimAll.Select("mispar_ishi= " + Mispar_ishi,"ME_TARICH asc"); // + " and Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime') >= ME_TARICH and Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')<= AD_TARICH");
                   
                for(int i=0;i<rows.Length ;i++)
                {
                    itemPirteyOved = new clPirteyOved(rows[i], dTarMe);
                    PirteyOved.Add(itemPirteyOved);
                    itemPirteyOved = null;
                }
                if (rows.Length > 0)
                {
                    dTchilatAvoda = DateTime.Parse(rows[rows.Length - 1]["TCHILAT_AVODA"].ToString());
                    dSiyumAvoda = DateTime.Parse(rows[rows.Length - 1]["AD_TARICH"].ToString());
                }

                itemPirteyOved = null;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitPirteyOvedList: " + ex.Message);
                throw ex;
            }
        }

        private void InitDtYemeyAvoda()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            DtYemeyAvoda = new DataTable();
            try
            {
              
                if (oGeneralData.dtYemeyAvodaAll != null && oGeneralData.dtYemeyAvodaAll.Rows.Count > 0)
                {
                 oGeneralData.dtYemeyAvodaAll.Select(null, "mispar_ishi");
         
                    rows = oGeneralData.dtYemeyAvodaAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich >= Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and taarich <= Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime') ");
                    if (rows.Length > 0)
                    {
                        DtYemeyAvoda = rows.CopyToDataTable();
                    }
                    else
                    {
                        DtYemeyAvoda = oGeneralData.dtYemeyAvodaAll.Clone();
                    }
                }
                else
                {
                    DtYemeyAvoda = oGeneralData.dtYemeyAvodaAll.Clone();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitDtYemeyAvoda: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }

        private void InitDtPeiluyotFromTnua()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            DtPeiluyotFromTnua = new DataTable();
            try
            {
               
                if (oGeneralData.dtPeiluyotFromTnuaAll != null && oGeneralData.dtPeiluyotFromTnuaAll.Rows.Count > 0)
                {
                    oGeneralData.dtPeiluyotFromTnuaAll.Select(null, "mispar_ishi");
             
                    rows = oGeneralData.dtPeiluyotFromTnuaAll.Select("mispar_ishi= " + Mispar_ishi + " and activity_date >= Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and activity_date <= Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime') ");
                    if (rows.Length > 0)
                    {
                        DtPeiluyotFromTnua = rows.CopyToDataTable();
                    }
                    else
                    {
                        DtPeiluyotFromTnua = oGeneralData.dtPeiluyotFromTnuaAll.Clone();
                    }
                }
                else
                {
                    DtPeiluyotFromTnua = oGeneralData.dtPeiluyotFromTnuaAll;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitDtPeiluyotFromTnua: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }

        private void InitDtPeiluyotLeOved()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            DtPeiluyotOved = new DataTable();
            try
            {
               
                if (oGeneralData.dtPeiluyotOvdimAll.Rows.Count > 0)
                {
                 oGeneralData.dtPeiluyotOvdimAll.Select(null, "mispar_ishi");
                 
                    rows = oGeneralData.dtPeiluyotOvdimAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich >= Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and taarich <= Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime') ");
                    if (rows.Length > 0)
                    {
                        DtPeiluyotOved = rows.CopyToDataTable();
                    }
                    else
                    {
                        DtPeiluyotOved = oGeneralData.dtPeiluyotOvdimAll.Clone();
                    }
                }
                else
                {
                    DtPeiluyotOved = oGeneralData.dtPeiluyotOvdimAll.Clone();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitDtPeiluyotLeOved: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }
         
        public void InitMeafyenyOved()
        {
            clOvdim oOvdim = new clOvdim();
            clMeafyenyOved itemMeafyenyOved;
            DateTime dTarMe = Month;
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            string sQury = "";
            string sQuryMI = "";
            DataRow[] drMeafyn;
            DataTable MeafyenimLeYom = new DataTable();
            TimeSpan ts = new TimeSpan();
            DateTime StartTime;
            try
            {
                StartTime = DateTime.Now;
                MeafyeneyOved = new List<clMeafyenyOved>();
            
                if ( iBakashaId > 1){
                    oGeneralData.dtMeafyenyOvedAll.Select(null, "mispar_ishi");
                    sQuryMI = "mispar_ishi= " + Mispar_ishi + " and ";
                }
                
     
                while (dTarMe <= TarAd)
                {
                    sQury = sQuryMI + " Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')>= ME_TAARICH";
                    sQury += " and Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')<= AD_TAARICH";
                
                    drMeafyn = oGeneralData.dtMeafyenyOvedAll.Select(sQury);
                    if (drMeafyn.Length > 0)
                    {
                        MeafyenimLeYom = drMeafyn.CopyToDataTable();
                        itemMeafyenyOved = new clMeafyenyOved(Mispar_ishi, dTarMe, "Calc", MeafyenimLeYom);
                        MeafyeneyOved.Add(itemMeafyenyOved);
                    }
                    dTarMe = dTarMe.AddDays(1);
                    itemMeafyenyOved = null;
                    drMeafyn = null;
                    MeafyenimLeYom = null;
                }
                ts = DateTime.Now - StartTime;

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitMeafyenyOved: " + ex.Message);
                throw ex;
            }
        }

        private void InitSugeyYechida()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            DtSugeyYechida = new DataTable();
            try
            {
               oGeneralData.dtSugeyYechidaAll.Select(null, "mispar_ishi");

               rows = oGeneralData.dtSugeyYechidaAll.Select("mispar_ishi= " + Mispar_ishi + " and ((Convert('" + Month.ToShortDateString() + "', 'System.DateTime')>=me_tarich and  Convert('" + Month.ToShortDateString() + "', 'System.DateTime')<=ad_tarich)  or (Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime')>=me_tarich  and  Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime')<=ad_tarich) or  (me_tarich>=Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and ad_tarich<=Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime')))");
                if (rows.Length > 0)
                {
                    DtSugeyYechida = rows.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitSugeyYechida: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }
        }

        private void InitMatzavOved()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;

            try
            {
                oGeneralData.dtMatzavOvdim.Select(null, "mispar_ishi");

                rows = oGeneralData.dtMatzavOvdim.Select("mispar_ishi= " + Mispar_ishi + " and ((Convert('" + Month.ToShortDateString() + "', 'System.DateTime')>=TAARICH_ME and  Convert('" + Month.ToShortDateString() + "', 'System.DateTime')<=TAARICH_AD)  or (Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime')>=TAARICH_ME  and  Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime')<=TAARICH_AD) or  (TAARICH_ME>=Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and TAARICH_AD<=Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime')))");
                if (rows.Length > 0)
                {
                    dtMatzavOved = rows.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(iBakashaId, Mispar_ishi, "E", 0, Month, "InitMatzavOved: " + ex.Message);
                throw ex;
            }
            finally
            {
                rows = null;
            }

        }

        public void SetMatzavOved()
        {
             DataRow[] rows;
            string sKodMatzav = "0";
            try
            {

                if (dtMatzavOved.Rows.Count > 0)
                {
                    rows = dtMatzavOved.Select("mispar_ishi= " + Mispar_ishi + " and Convert('" + Taarich.ToShortDateString() + "', 'System.DateTime') >= taarich_me and Convert('" + Taarich.ToShortDateString() + "', 'System.DateTime')<= taarich_ad");
                    if (rows.Length > 0)
                    {
                        sKodMatzav = rows[0]["kod_matzav"].ToString();
                        if (rows[0]["kod_headrut"].ToString() != "")
                            iKodHeadrut = int.Parse(rows[0]["kod_headrut"].ToString());
                    }
                }

                sMatazavOved = sKodMatzav;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Initialize

        public void InitDataSetChishuv()
        {
            _dsChishuv = new DataSet();

            InitDtChishuvYom();
            InitDtChishuvPeilut();
            InitDtChishuvSidur();
            InitDtChishuvChodesh();
        }

        private void InitDtChishuvPeilut()
        {
            _DtPeilut = new DataTable("CHISHUV_PEILUT");
            _DtPeilut.Columns.Add("mispar_ishi", System.Type.GetType("System.Int32"));
            _DtPeilut.Columns.Add("bakasha_id", System.Type.GetType("System.Int32"));
            _DtPeilut.Columns.Add("mispar_sidur", System.Type.GetType("System.Int32"));
            _DtPeilut.Columns.Add("shat_hatchala", System.Type.GetType("System.DateTime"));
            _DtPeilut.Columns.Add("mispar_knisa", System.Type.GetType("System.Int32"));
            _DtPeilut.Columns.Add("shat_yetzia", System.Type.GetType("System.DateTime"));
            _DtPeilut.Columns.Add("taarich", System.Type.GetType("System.DateTime"));
            _DtPeilut.Columns.Add("kod_rechiv", System.Type.GetType("System.Int32"));
            _DtPeilut.Columns.Add("erech_rechiv", System.Type.GetType("System.Single"));
            _dsChishuv.Tables.Add(_DtPeilut);
        }

        public void InitDtChishuvYom()
        {
            _DtDay = new DataTable("CHISHUV_YOM");
            _DtDay.Columns.Add("mispar_ishi", System.Type.GetType("System.Int32"));
            _DtDay.Columns.Add("bakasha_id", System.Type.GetType("System.Int32"));
            _DtDay.Columns.Add("taarich", System.Type.GetType("System.DateTime"));
            _DtDay.Columns.Add("kod_rechiv", System.Type.GetType("System.Int32"));
            _DtDay.Columns.Add("erech_rechiv", System.Type.GetType("System.Single"));
            _DtDay.Columns.Add("erech_ezer", System.Type.GetType("System.Single"));
            _DtDay.Columns.Add("tkufa", System.Type.GetType("System.DateTime"));
            _dsChishuv.Tables.Add(_DtDay);
        }

        private void InitDtChishuvChodesh()
        {
            _DtMonth = new DataTable("CHISHUV_CHODESH");
            _DtMonth.Columns.Add("mispar_ishi", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("bakasha_id", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("taarich", System.Type.GetType("System.DateTime"));
            _DtMonth.Columns.Add("kod_rechiv", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("erech_rechiv", System.Type.GetType("System.Single"));

            _dsChishuv.Tables.Add(_DtMonth);
        }

        private void InitDtChishuvSidur()
        {
            _DtSidur = new DataTable("CHISHUV_SIDUR");
            _DtSidur.Columns.Add("mispar_ishi", System.Type.GetType("System.Int32"));
            _DtSidur.Columns.Add("bakasha_id", System.Type.GetType("System.Int32"));
            _DtSidur.Columns.Add("mispar_sidur", System.Type.GetType("System.Int32"));
            _DtSidur.Columns.Add("shat_hatchala", System.Type.GetType("System.DateTime"));
            _DtSidur.Columns.Add("taarich", System.Type.GetType("System.DateTime"));
            _DtSidur.Columns.Add("kod_rechiv", System.Type.GetType("System.Int32"));
            _DtSidur.Columns.Add("erech_rechiv", System.Type.GetType("System.Single"));
            _DtSidur.Columns.Add("out_michsa", System.Type.GetType("System.Int32"));
            _dsChishuv.Tables.Add(_DtSidur);
        }

        #endregion


        #region IDisposable Members



        #endregion


        public void Dispose()
        {
            DtSugeyYechida = null;
            DtYemeyAvoda = null;
            DtPeiluyotFromTnua = null;
            DtPeiluyotOved = null;
            dtPremyotYadaniyot = null;
            dtPremyotNihulTnua = null;
            DtYemeyAvodaYomi = null;
            DtPeiluyotYomi = null;
            DtPeiluyotTnuaYomi = null;
            dtPremyot = null;
            _dsChishuv = null;
            _DtMonth = null;
            _DtDay = null;
            _DtPeilut = null;
            _DtPeilut = null;
            _fHashlamaAlCheshbonNosafot=0;
            MeafyeneyOved = null;
            PirteyOved = null;
            oGeneralData = null;
           // GC.GetTotalMemory(true); 

        }
    }
}

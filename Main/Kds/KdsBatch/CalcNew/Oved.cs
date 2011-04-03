﻿using System;
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
    public class Oved
    {
        public int Mispar_ishi;
        public DateTime Month;
        private GeneralData oGeneralData;
        public long iBakashaId;

        public DataTable DtYamimMeyuchadim { get; set; }
        public DataTable DtSugeyYamimMeyuchadim { get; set; }
        public List<clParameters> Parameters { get; set; }
        public List<clPirteyOved> PirteyOved { get; set; }
        public DataTable DtMichsaYomit { set; get; }
        public DataTable DtBusNumbers { set; get; }
        public DataTable DtMeafyeneySugSidur { set; get; }
        public DataTable DtSugeySidur { set; get; }
        public DataTable DtSugeyYechida { set; get; }
        public DataTable DtYemeyAvoda { set; get; }
        public DataTable DtPeiluyotFromTnua { set; get; }
        public DataTable DtPeiluyotOved { set; get; }
        public DataTable DtSugeySidurRechiv { set; get; }
        public DataTable DtSidurimMeyuchRechiv { set; get; }
        public List<clMeafyenyOved>  MeafyeneyOved { get; set; }
        public DataTable dtPremyotYadaniyot { set; get; }
        public DataTable dtPremyot { set; get; }
        //public int iSugYom { get; set; }
        //public string sSugYechida { get; set; }
        //public float fMekademNipuach { get; set; }
        public  float fMekademNipuach { set; get; }
        public DataSet _dsChishuv { set; get; }
        public DataTable _DtMonth { set; get; }
        public DataTable _DtDay { set; get; }
        public DataTable _DtSidur { set; get; }
        public DataTable _DtPeilut { set; get; }

          public Oved(int mis_ishi, DateTime month, DateTime tarMe, DateTime tarAd)
        {
            oGeneralData = SingleGeneralData.GetInstance(tarMe, tarAd);
            Mispar_ishi = mis_ishi;
            Month = month;
            SetNetunimLeOved();
        }
        private void SetNetunimLeOved()
        {

            DtYamimMeyuchadim = oGeneralData._dtYamimMeyuchadim;
            DtSugeyYamimMeyuchadim = oGeneralData._dtSugeyYamimMeyuchadim;
            Parameters = oGeneralData.ListParameters;
            DtMichsaYomit = oGeneralData._dtMichsaYomitAll;
            DtMeafyeneySugSidur = oGeneralData._dtMeafyeneySugSidurAll;
            DtSugeySidur = oGeneralData._dtSugeySidurAll;
            DtBusNumbers = oGeneralData._dtBusNumbersAll;
            DtSugeySidurRechiv = oGeneralData._dtSugeySidurRechivAll;
            DtSidurimMeyuchRechiv = oGeneralData._dtSidurimMeyuchRechivAll;
            dtPremyotYadaniyot = oGeneralData._dtPremyotYadaniyotAll;
            dtPremyot = oGeneralData._dtPremyotAll;
            InitPirteyOvedList();
            InitDtYemeyAvoda();
            InitDtPeiluyotFromTnua();
            InitDtPeiluyotLeOved();
            InitMeafyenyOved();

            //* For *//
            //sSugYechida
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
                while (dTarMe <= TarAd)
                {
                    rows = oGeneralData._dtPirteyOvdimAll.Select("mispar_ishi= " + Mispar_ishi  + " and Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime') >= ME_TARICH and Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')<= AD_TARICH");
                    if (rows.Length > 0)
                    {
                        itemPirteyOved = new clPirteyOved(rows[0], dTarMe);
                        PirteyOved.Add(itemPirteyOved);
                    }
                    dTarMe = dTarMe.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
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
                rows = oGeneralData._dtYemeyAvodaAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich >= Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and taarich <= Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime') ");
                if (rows.Length > 0)
                {
                    DtYemeyAvoda=rows.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }

        private void InitDtPeiluyotFromTnua()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            DtPeiluyotFromTnua = new DataTable();
            try
            {
                rows = oGeneralData._dtPeiluyotFromTnuaAll.Select("mispar_ishi= " + Mispar_ishi + " and activity_date >= Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and activity_date <= Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime') ");
                if (rows.Length > 0)
                {
                    DtPeiluyotFromTnua = rows.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }

        private void InitDtPeiluyotLeOved()
        {
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            DataRow[] rows;
            DtPeiluyotOved = new DataTable();
            try
            {
                rows = oGeneralData._dtPeiluyotOvdimAll.Select("mispar_ishi= " + Mispar_ishi + " and taarich >= Convert('" + Month.ToShortDateString() + "', 'System.DateTime') and taarich <= Convert('" + TarAd.ToShortDateString() + "', 'System.DateTime') ");
                if (rows.Length > 0)
                {
                    DtPeiluyotOved = rows.CopyToDataTable();
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }

        public void InitMeafyenyOved()
        {
            clOvdim oOvdim = new clOvdim();
            clMeafyenyOved itemMeafyenyOved;
            DateTime dTarMe = Month;
            DateTime TarAd = (Month.AddMonths(1)).AddDays(-1);
            try
            {
                MeafyeneyOved = new List<clMeafyenyOved>();
                while (dTarMe <= TarAd)
                {
                    itemMeafyenyOved = new clMeafyenyOved(Mispar_ishi, dTarMe, "Calc", oGeneralData._dtMeafyenyOvedAll);
                    MeafyeneyOved.Add(itemMeafyenyOved);
                    dTarMe = dTarMe.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                throw ex;
            }
        }
        #region Initialize

        public DataSet GetDataSetChishuv()
        {
            _dsChishuv = new DataSet();

            InitDtChishuvYom();
            InitDtChishuvPeilut();
            InitDtChishuvSidur();
            InitDtChishuvChodesh();

            return _dsChishuv; 
        }

        private  void InitDtChishuvPeilut()
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

        public  void InitDtChishuvYom()
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

        private  void InitDtChishuvChodesh()
        {
            _DtMonth = new DataTable("CHISHUV_CHODESH");
            _DtMonth.Columns.Add("mispar_ishi", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("bakasha_id", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("taarich", System.Type.GetType("System.DateTime"));
            _DtMonth.Columns.Add("kod_rechiv", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("erech_rechiv", System.Type.GetType("System.Single"));

            _dsChishuv.Tables.Add(_DtMonth);
        }

        private  void InitDtChishuvSidur()
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

    }
}

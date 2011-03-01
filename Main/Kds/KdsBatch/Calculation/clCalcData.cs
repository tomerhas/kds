using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.DAL;
using KdsLibrary.BL;

namespace KdsBatch
{
    static class clCalcData
    {
        private static DataTable _dtMeafyeneySugSidur;
        private static DataTable _dtYamimMeyuchadim;
        private static DataTable _dtMichsaYomit;
        private static DataTable _dtSugeyYamimMeyuchadim;
        private static DataTable _dtSidurimMeyuchRechiv;
        private static DataTable _dtSugeySidurRechiv;
        private static DataTable _dtPremyot;
        private static DataTable _dtPremyotYadaniyot;
        private static DataTable _dtPeiluyotFromTnua;
        private static DataTable _dtYemeyAvoda;
        private static DataTable _dtSugeySidur;
        public static int iSugYom;
        public static string sSugYechida;
        public static float fMekademNipuach;
        static bool _InstanceCreated = false;
        static DataSet _dsChishuv;
        static DataTable _DtMonth, _DtDay, _DtSidur, _DtPeilut;

        #region Properties
        public static DataTable DtSidur
        {

            get { return _DtSidur; }

        }

        public static DataTable DtDay
        {

            get { return _DtDay; }

        }

        public static DataTable DtMonth
        {

            get { return _DtMonth; }

        }

        public static DataTable DtPeilut
        {

            get { return _DtPeilut; }

        }

        public static DataTable DtMichsaYomit
        {
            set { _dtMichsaYomit = value; }
            get { return _dtMichsaYomit; }
        }

        public static DataTable DtYemeyAvoda
        {
            set { _dtYemeyAvoda = value; }
            get { return _dtYemeyAvoda; }
        }

        public static DataTable DtSugeySidur
        {
            set { _dtSugeySidur = value; }
            get { return _dtSugeySidur; }
        }
        
        public static DataTable DtSugeySidurRechiv
        {
            set { _dtSugeySidurRechiv = value; }
            get { return _dtSugeySidurRechiv; }
        }

        public static DataTable DtSugeyYamimMeyuchadim
        {
            set { _dtSugeyYamimMeyuchadim = value; }
            get { return _dtSugeyYamimMeyuchadim; }
        }

        public static DataTable DtSidurimMeyuchRechiv
        {
            set { _dtSidurimMeyuchRechiv = value; }
            get { return _dtSidurimMeyuchRechiv; }
        }

        public static DataTable DtMeafyeneySugSidur
        {
            set { _dtMeafyeneySugSidur = value; }
            get { return _dtMeafyeneySugSidur; }
        }

        public static DataTable DtYamimMeyuchadim
        {
            set { _dtYamimMeyuchadim = value; }
            get { return _dtYamimMeyuchadim; }
        }

        public static DataTable DtPremyot
        {
            set { _dtPremyot = value; }
            get { return _dtPremyot; }
        }

        public static DataTable DtPremyotYadaniyot
        {
            set { _dtPremyotYadaniyot = value; }
            get { return _dtPremyotYadaniyot; }
        }

        public static DataTable DtPeiluyotFromTnua
        {
            set { _dtPeiluyotFromTnua = value; }
            get { return _dtPeiluyotFromTnua; }
        }
        #endregion

        #region Methods
       
        public static bool CheckErevChag(int iSugYom)
        {
            if (_dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom).Length > 0)
            {
                return (_dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["EREV_SHISHI_CHAG"].ToString() == "1") ? true : false;

            }
            else return false;
        }

        public static bool CheckYomShishi(int iSugYom)
        {
            if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode())
            {
                return true;
            }
            else return false;
        }

      

     
      

        public static int GetPremiaChodshit(int iSugPremia)
        {
            DataRow[] drPremia;

            drPremia = _dtPremyot.Select("Sug_premia=" + iSugPremia);
            if (drPremia.Length > 0)
            { return int.Parse(drPremia[0]["Sum_dakot"].ToString()); }
            else
            {
                return 0;
            }
        }

        public static int GetPremiaYadanit(int iSugPremia)
        {
            DataRow[] drPremia;

            drPremia = _dtPremyotYadaniyot.Select("Sug_premya=" + iSugPremia);
            if (drPremia.Length > 0)
            { return int.Parse(drPremia[0]["Dakot_premya"].ToString()); }
            else
            {
                return 0;
            }
        }

        public static float GetSumErechRechiv(object oErech)
        {
            if (oErech.Equals(System.DBNull.Value))
            {
                return 0;
            }
            else
            {
                return float.Parse(oErech.ToString());
            }
        }

       public static bool CheckOutMichsa( int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala,int iOutMichsa  )
       {
           bool bOutMichsa=true;

           if (iOutMichsa == 1 && CheckUshraBakasha(clGeneral.enKodIshur.OutMichsa.GetHashCode(), iMisparIshi, dTaarich,iMisparSidur,dShatHatchala))
               bOutMichsa = true;
           else bOutMichsa = false;
           return bOutMichsa;
       }

        public static bool CheckUshraBakasha(int iKodIshur, int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala)
        {
            clUtils objUtils = new clUtils();
            if (objUtils.CheckIshurToSidur(iMisparIshi, dTaarich, iKodIshur, iMisparSidur, dShatHatchala) == 1)
            { return true; }
            else { return false; }
        }

        public static bool CheckUshraBakasha(int iKodIshur, int iMisparIshi, DateTime dTaarich)
        {
            clUtils objUtils = new clUtils();
            if (objUtils.CheckIshur(iMisparIshi, dTaarich, iKodIshur) == 1)
            { return true; }
            else { return false; }
        }

        public static Boolean CheckOvedPutar(int iMispar_ishi, DateTime dTaarich)
        {
            Boolean bPutar = false;
            DateTime dTarMe, dTarAd;
            clDal oDal = new clDal();
            try
            {
                dTarMe = new DateTime(dTaarich.Year, dTaarich.Month, 1);
                dTarAd = dTarMe.AddMonths(1).AddDays(-1);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_chodesh_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_chodesh_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_putar", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProCheckOvedPutar);

                if (!string.IsNullOrEmpty(oDal.GetValParam("p_putar")))
                {
                    bPutar = (oDal.GetValParam("p_putar") == "1" ? true : false);
                }

                return bPutar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetNetunimMashar(string sKodRechev)
        {
            DataTable dtMashar;
            clKavim oKavim = new clKavim();
            try
            {
                dtMashar = oKavim.GetMasharData(sKodRechev);

                return dtMashar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Initialize

        public static DataSet GetInstance()
        {

            if (!_InstanceCreated)
            {

                _dsChishuv = new DataSet();

                InitDtChishuvYom();
                InitDtChishuvPeilut();
                InitDtChishuvSidur();
                InitDtChishuvChodesh();

                _InstanceCreated = true;

            }

            return _dsChishuv;

        }

        public static void ResetDataSet()
        {
            _dsChishuv = null;
            _InstanceCreated = false;
        }

        private static void InitDtChishuvPeilut()
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

        public static void InitDtChishuvYom()
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

        private static void InitDtChishuvChodesh()
        {
            _DtMonth = new DataTable("CHISHUV_CHODESH");
            _DtMonth.Columns.Add("mispar_ishi", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("bakasha_id", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("taarich", System.Type.GetType("System.DateTime"));
            _DtMonth.Columns.Add("kod_rechiv", System.Type.GetType("System.Int32"));
            _DtMonth.Columns.Add("erech_rechiv", System.Type.GetType("System.Single"));

            _dsChishuv.Tables.Add(_DtMonth);
        }

        private static void InitDtChishuvSidur()
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


    public class GlobalDataset
    {

        protected DataSet _dsChishuv;

        public GlobalDataset()
        {

            _dsChishuv = clCalcData.GetInstance();

        }



    }
}

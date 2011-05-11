﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using System.Data;


namespace KdsBatch
{
    class clCalcBL
    {
 

        public clCalcBL()
        {
             
        }
        public float CalcKm(long lMakat, float fGoremChishuvKm)
        {
            int iZmanPeilut;
            iZmanPeilut = int.Parse(lMakat.ToString().PadLeft(8).Substring(3, 3));
            return (iZmanPeilut /  fGoremChishuvKm);
        }
        public float GetSumErechRechiv(object oErech)
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

        public  bool CheckYomShishi(int iSugYom)
        {
            if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode())
            {
                return true;
            }
            else return false;
        }

        public  bool CheckErevChag(DataTable dtSugeyYamimMeyuchadim,int iSugYom)
        {
            try{
                if (dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom).Length > 0)
                {
                    return (dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["EREV_SHISHI_CHAG"].ToString() == "1") ? true : false;

                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  bool CheckOutMichsa(int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala, int iOutMichsa)
        {
            bool bOutMichsa = true;

            if (iOutMichsa == 1 && CheckUshraBakasha(clGeneral.enKodIshur.OutMichsa.GetHashCode(), iMisparIshi, dTaarich, iMisparSidur, dShatHatchala))
                bOutMichsa = true;
            else bOutMichsa = false;
            return bOutMichsa;
        }

        public  bool CheckUshraBakasha(int iKodIshur, int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala)
        {
            clUtils objUtils = new clUtils();
            try{
                if (objUtils.CheckIshurToSidur(iMisparIshi, dTaarich, iKodIshur, iMisparSidur, dShatHatchala) == 1)
                { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  bool CheckUshraBakasha(int iKodIshur, int iMisparIshi, DateTime dTaarich)
        {
            clUtils objUtils = new clUtils();
            try
            {
                if (objUtils.CheckIshur(iMisparIshi, dTaarich, iKodIshur) == 1)
                { return true; }
                else { return false; } 
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetSugYomLemichsa(Oved oOved, DateTime dTaarich, int iKodSectorIsuk, int iMeafyen56)
        {
            int iSugYom;
            try{
                if (oOved.oGeneralData.dtYamimMeyuchadim == null)
                {
                    oOved.oGeneralData.dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                }

                iSugYom = clGeneral.GetSugYom(oOved.Mispar_ishi, dTaarich, oOved.oGeneralData.dtYamimMeyuchadim, iKodSectorIsuk, oOved.oGeneralData.dtSugeyYamimMeyuchadim, iMeafyen56);
                return iSugYom;
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckIsurShaotNosafot(clPirteyOved objPirteyOved,DataTable dtMutamut)
        {
            clUtils objUtils = new clUtils();
           // DataTable dtMutamut;
            try{
                if (objPirteyOved.iMutamut > 0)
                {
                   // dtMutamut = objUtils.GetCtbMutamut();
                    if (dtMutamut.Select("KOD_MUTAMUT=" + objPirteyOved.iMutamut)[0]["Isur_Shaot_Nosafot"].ToString() == "1")
                    { return true; }
                    else { return false; }
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckMutamut(clPirteyOved objPirteyOved, DataTable dtMutamut)
        {
            
            clUtils objUtils = new clUtils();
          //  DataTable dtMutamut;
            try
            {
                if (objPirteyOved.iMutamut > 0)
                {
                   // dtMutamut = objUtils.GetCtbMutamut();
                    if (dtMutamut.Select("KOD_MUTAMUT=" + objPirteyOved.iMutamut)[0]["MEZAKE_GMUL"].ToString() == "1")
                    { return true; }
                    else { return false; }
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetMichsaYomit(Oved oOved, int iKodMichsa, ref int iSugYom, DateTime dTaarich, int iKodSectorIsuk, int iMeafyen56)
        {
            DataRow[] drMichsa;
            int iShvuaAvoda;
            try
            {
                if (iSugYom == 0)
                {
                    iSugYom = GetSugYomLemichsa(oOved, dTaarich, iKodSectorIsuk, iMeafyen56);
                }
                if ( iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() ||  iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                { iShvuaAvoda = 6; }
                else { iShvuaAvoda = 5; }

                drMichsa = oOved.oGeneralData.dtMichsaYomitAll.Select("Kod_Michsa=" + iKodMichsa + " and SHAVOA_AVODA=" + iShvuaAvoda + " and sug_yom=" + iSugYom + " and me_taarich<=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')" + " and ad_taarich>=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                if (drMichsa.Length > 0)
                { return int.Parse((float.Parse(drMichsa[0]["michsa"].ToString()) * 60).ToString()); }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetPremiaYadanit(DataTable dtPremyotYadaniyot, int iSugPremia)
        {
            DataRow[] drPremia;
            try
            {
                drPremia = dtPremyotYadaniyot.Select("Sug_premya=" + iSugPremia);
                if (drPremia.Length > 0)
                { return int.Parse(drPremia[0]["Dakot_premya"].ToString()); }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  int GetPremiaChodshit(DataTable dtPremyot,int iSugPremia)
        {
            DataRow[] drPremia;
            try{
                drPremia = dtPremyot.Select("Sug_premia=" + iSugPremia);
                if (drPremia.Length > 0)
                { return int.Parse(drPremia[0]["Sum_dakot"].ToString()); }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public   Boolean CheckOvedPutar(int iMispar_ishi, DateTime dTaarich)
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

        public string InitSugYechida(Oved oved,DateTime dDay)
        {
            DataRow[] drSugYechida;
            try
            {
                if (oved.DtSugeyYechida.Rows.Count > 0)
                {
                    drSugYechida = oved.DtSugeyYechida.Select(" Convert('" + dDay.ToShortDateString() + "', 'System.DateTime') >= me_tarich and " + " Convert('" + dDay.ToShortDateString() + "', 'System.DateTime') <= ad_tarich ");
                    if (drSugYechida.Length > 0)
                    { return drSugYechida[0]["SUG_YECHIDA"].ToString(); }
                    else
                    {
                        return "";
                    }
                }
                else return ""; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

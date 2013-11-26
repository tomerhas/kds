using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using System.Data;
using KDSCommon.DataModels;

namespace KdsBatch
{
    public class clCalcGeneral
    {
        public  clParametersDM objParameters;
        public  clMeafyenyOved objMeafyeneyOved;
        public  clPirteyOved objPirteyOved;
       

        public  bool CheckMutamut()
        {
            clUtils objUtils = new clUtils();
            DataTable dtMutamut;
            if (objPirteyOved.iMutamut > 0)
            {
                dtMutamut = objUtils.GetCtbMutamut();
                if (dtMutamut.Select("KOD_MUTAMUT=" + objPirteyOved.iMutamut)[0]["MEZAKE_GMUL"].ToString() == "1")
                { return true; }
                else { return false; }
            }
            else return false;
        }

        public  bool CheckIsurShaotNosafot()
        {
            clUtils objUtils = new clUtils();
            DataTable dtMutamut;
            if (objPirteyOved.iMutamut > 0)
            {
                dtMutamut = objUtils.GetCtbMutamut();
                if (dtMutamut.Select("KOD_MUTAMUT=" + objPirteyOved.iMutamut)[0]["Isur_Shaot_Nosafot"].ToString() == "1")
                { return true; }
                else { return false; }
            }
            else return false;
        }

        public  float CalcKm(long lMakat)
        {
            int iZmanPeilut;
            iZmanPeilut = int.Parse(lMakat.ToString().PadLeft(8).Substring(3, 3));
            return (iZmanPeilut / objParameters.fGoremChishuvKm);
        }

        public  int GetSugYomLemichsa(int iMisparIshi, DateTime dTaarich)
        {
            int iSugYom;
            if (clCalcData.DtYamimMeyuchadim == null)
            {
                clCalcData.DtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
            }

            iSugYom = clGeneral.GetSugYom(iMisparIshi, dTaarich, clCalcData.DtYamimMeyuchadim, objPirteyOved.iKodSectorIsuk, clCalcData.DtSugeyYamimMeyuchadim,objMeafyeneyOved.iMeafyen56);
            return iSugYom;
        }

        public  void CalcMekademNipuach(DateTime dTarMe, DateTime dTarAd, int iMisparIshi)
        {
            float fCountMichsa, fCountYomLeloChag;
            int iSugYom;
            fCountMichsa =clCalcData.DtDay.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " AND ERECH_RECHIV>0", "").Length;
            fCountYomLeloChag = 0;
            clCalcData.fMekademNipuach = 0;

            do
            {
                iSugYom = GetSugYomLemichsa(iMisparIshi, dTarMe);
                if (iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    fCountYomLeloChag += 1;
                }
                dTarMe = dTarMe.AddDays(1);
            }
            while (dTarMe <= dTarAd);

            clCalcData.fMekademNipuach = fCountYomLeloChag / fCountMichsa;

        }

        public  int GetMichsaYomit(int iMisparIshi, int iKodMichsa, ref int iSugYom, DateTime dTaarich)
        {
            DataRow[] drMichsa;
            int iShvuaAvoda;
            if (iSugYom == 0)
            {
                iSugYom = GetSugYomLemichsa(iMisparIshi, dTaarich);
            }
            if (objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
            { iShvuaAvoda = 6; }
            else { iShvuaAvoda = 5; }

            drMichsa = clCalcData.DtMichsaYomit.Select("Kod_Michsa=" + iKodMichsa + " and SHAVOA_AVODA=" + iShvuaAvoda + " and sug_yom=" + iSugYom + " and me_taarich<=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')" + " and ad_taarich>=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
            if (drMichsa.Length > 0)
            { return int.Parse((float.Parse(drMichsa[0]["michsa"].ToString()) * 60).ToString()); }
            else
            {
                return 0;
            }
        }

       
    }
}



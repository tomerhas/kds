using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.BL;
using System.Data;
using KDSCommon.Enums;
using KDSCommon.Helpers;


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
            return (iZmanPeilut / fGoremChishuvKm);
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
        public float GetSumErechRechiv(DataTable TableName, int kodRechiv)
        {
            try
            {
                float Res = (from c in TableName.AsEnumerable()
                             where c.Field<int>("KOD_RECHIV").Equals(kodRechiv)
                             select c.Field<float>("ERECH_RECHIV")).Sum();
                return (Res == null) ? 0 : float.Parse(Res.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetSumErechRechiv :" + ex.Message);
            }
        }

        public float GetSumErechRechiv(DataTable TableName, int kodRechiv, DateTime dTaarich)
        {
            try
            {
                float Res = (from c in TableName.AsEnumerable()
                             where c.Field<int>("KOD_RECHIV").Equals(kodRechiv)
                             && c.Field<DateTime>("taarich").Equals(dTaarich)
                             select c.Field<float>("ERECH_RECHIV")).Sum();
                return (Res == null) ? 0 : float.Parse(Res.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetSumErechRechiv :" + ex.Message);
            }
        }

        public float GetSumErechRechivChelki(DataTable Table, int kodRechiv,int KodRechivTnai, DateTime dTaarich)
        {
            DataRow[] sidurim, dr;
            int iMisparSidur;
            string sQury = "";
            DateTime dshatHatchala;
            float fErechChelki = 0;
            try
            {
                sidurim = Table.Select("ERECH_RECHIV>0  and KOD_RECHIV=" + KodRechivTnai.ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int i = 0; i < sidurim.Length; i++)
                {
                    iMisparSidur = int.Parse(sidurim[i]["mispar_sidur"].ToString());
                    dshatHatchala = DateTime.Parse(sidurim[i]["shat_hatchala"].ToString());

                    sQury = " MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                    sQury += "SHAT_HATCHALA=Convert('" + dshatHatchala.ToString() + "', 'System.DateTime')";

                    dr = Table.Select(sQury + " and KOD_RECHIV=" + kodRechiv.ToString());
                    if (dr.Length > 0)
                        fErechChelki += float.Parse(dr[0]["ERECH_RECHIV"].ToString());
               }
                return fErechChelki;
            }
            catch (Exception ex)
            {
                throw new Exception("GetSumErechRechiv :" + ex.Message);
            }
        }

        public float GetSumErechEzer(DataTable TableName, int kodRechiv, DateTime dTaarich)
        {
            try
            {
                float Res = (from c in TableName.AsEnumerable()
                             where c.Field<int>("KOD_RECHIV").Equals(kodRechiv)
                             && c.Field<DateTime>("taarich").Equals(dTaarich)
                             select c.Field<float>("ERECH_EZER")).Sum();
                return (Res == null) ? 0 : float.Parse(Res.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetSumErechRechiv :" + ex.Message);
            }
        }
        public Dictionary<int, float> GetSumsOfRechiv(DataTable TableName, DateTime dTaarich)
        {
            try
            {
                var List = from c in TableName.AsEnumerable()
                           where c.Field<DateTime>("taarich").Equals(dTaarich)
                           group c by c.Field<int>("KOD_RECHIV") into g
                           select new
                           {
                               Kod = g.Key,
                               Total = g.Sum(c => c.Field<float>("ERECH_RECHIV"))
                           };
                return List.ToDictionary(item => item.Kod, item => item.Total);
            }
            catch (Exception ex)
            {
                throw new Exception("GetSumsOfRechiv :" + ex.Message);
            }
        }

        public Dictionary<int, float> GetSumsOfRechiv(DataTable TableName)
        {
            try
            {
                var List = from c in TableName.AsEnumerable()
                           group c by c.Field<int>("KOD_RECHIV") into g
                           select new
                           {
                               Kod = g.Key,
                               Total = g.Sum(c => c.Field<float>("ERECH_RECHIV"))
                           };
                return List.ToDictionary(item => item.Kod, item => item.Total);
            }
            catch (Exception ex)
            {
                throw new Exception("GetSumsOfRechiv :" + ex.Message);
            }
        }


        public float GetSumErechRechiv(Dictionary<int, float> ListOfSum, clGeneral.enRechivim SugRechiv)
        {
            return (ListOfSum.ContainsKey(SugRechiv.GetHashCode())) ? ListOfSum[SugRechiv.GetHashCode()] : 0;
        }

        public bool CheckYomShishi(int iSugYom)
        {
            if (iSugYom == enSugYom.Shishi.GetHashCode())
            {
                return true;
            }
            else return false;
        }

        public bool CheckErevChag(DataTable dtSugeyYamimMeyuchadim, int iSugYom)
        {
            try
            {
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

        public bool CheckOutMichsa(int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala, int iOutMichsa)
        {
            bool bOutMichsa = true;

            if (iOutMichsa == 1 && CheckUshraBakasha(clGeneral.enKodIshur.OutMichsa.GetHashCode(), iMisparIshi, dTaarich, iMisparSidur, dShatHatchala))
                bOutMichsa = true;
            else bOutMichsa = false;
            return bOutMichsa;
        }

        public bool CheckUshraBakasha(int iKodIshur, int iMisparIshi, DateTime dTaarich, int iMisparSidur, DateTime dShatHatchala)
        {
            clUtils objUtils = new clUtils();
            try
            {
                if (objUtils.CheckIshurToSidur(iMisparIshi, dTaarich, iKodIshur, iMisparSidur, dShatHatchala) == 1)
                { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objUtils = null;
            }
        }

        public bool CheckUshraBakasha(int iKodIshur, int iMisparIshi, DateTime dTaarich)
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
            finally
            {
                objUtils = null;
            }
        }

        public int GetSugYomLemichsa(Oved oOved, DateTime dTaarich, int iKodSectorIsuk, int iMeafyen56)
        {
            int iSugYom;
            try
            {
                if (oOved.oGeneralData.dtYamimMeyuchadim == null)
                {
                    oOved.oGeneralData.dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                }

                iSugYom = DateHelper.GetSugYom(oOved.Mispar_ishi, dTaarich, oOved.oGeneralData.dtYamimMeyuchadim, iKodSectorIsuk, oOved.oGeneralData.dtSugeyYamimMeyuchadim, iMeafyen56);
                return iSugYom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckIsurShaotNosafot(clPirteyOved objPirteyOved, DataTable dtMutamut)
        {
            clUtils objUtils = new clUtils();
            // DataTable dtMutamut;
            try
            {
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
            finally
            {
                objUtils = null;
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
            finally
            {
                objUtils = null;
            }
        }

        public int GetMichsaYomit(Oved objOved, ref int iSugYom)//int iKodMichsa, ref int iSugYom, DateTime dTaarich, int iKodSectorIsuk, int GetMeafyen(56).IntValue)
        {
            DataRow[] drMichsa;
            int iShvuaAvoda;
            try
            {
                if (iSugYom == 0)
                {
                    iSugYom = GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                }
                if (objOved.objMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                { iShvuaAvoda = 6; }
                else { iShvuaAvoda = 5; }

                drMichsa = objOved.oGeneralData.dtMichsaYomitAll.Select("Kod_Michsa=" + int.Parse(objOved.objMeafyeneyOved.GetMeafyen(1).Value) + " and SHAVOA_AVODA=" + iShvuaAvoda + " and sug_yom=" + iSugYom + " and me_taarich<=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')" + " and ad_taarich>=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
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

        public float GetPremiaYadanit(DataTable dtPremyotYadaniyot, int iSugPremia)
        {
            DataRow[] drPremia;
            try
            {
                drPremia = dtPremyotYadaniyot.Select("Sug_premya=" + iSugPremia);
                if (drPremia.Length > 0)
                { return float.Parse(drPremia[0]["Dakot_premya"].ToString()); }
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

        public int GetPremiaChodshit(DataTable dtPremyot, int iSugPremia)
        {
            DataRow[] drPremia;
            try
            {
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

        public int GetPremiaNihulTnua(DataTable dtPremyot, int iSugPremia)
        {
            DataRow[] drPremia;
            try
            {
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
        //public   Boolean CheckOvedPutar(int iMispar_ishi, DateTime dTaarich)
        //{
        //    Boolean bPutar = false;
        //    DateTime dTarMe, dTarAd;
        //    clDal oDal = new clDal();
        //    try
        //    {
        //        dTarMe = new DateTime(dTaarich.Year, dTaarich.Month, 1);
        //        dTarAd = dTarMe.AddMonths(1).AddDays(-1);
        //        oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMispar_ishi, ParameterDir.pdInput);
        //        oDal.AddParameter("p_tar_chodesh_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
        //        oDal.AddParameter("p_tar_chodesh_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
        //        oDal.AddParameter("p_putar", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oDal.ExecuteSP(clDefinitions.cProCheckOvedPutar);

        //        if (!string.IsNullOrEmpty(oDal.GetValParam("p_putar")))
        //        {
        //            bPutar = (oDal.GetValParam("p_putar") == "1" ? true : false);
        //        }

        //        return bPutar;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public Boolean CheckOvedPutar(Oved oved)
        {
            Boolean bPutar = false;
            try
            {
                if (oved.sMatazavOved=="P")
                {
                    bPutar = true;
                }
               
                return bPutar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public string InitSugYechida(Oved oved, DateTime dDay)
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

        public bool CheckSugSidur(Oved objOved, int iMeafyen, int iErech, int iSugSidur)
        {   //הפונקציה מקבלת קוד מאפיין,סוג סידור,ערך מאפיין ותאריך ומחזירה האם קיים כזה סוג סידור

            DataRow[] dr;
            try
            {
                dr = objOved.oGeneralData.dtMeafyeneySugSidurAll.Select("kod_meafyen=" + iMeafyen.ToString() + " and erech=" + iErech + " and Convert('" + objOved.Taarich.ToShortDateString() + "','System.DateTime') >= me_taarich and Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') <= ad_taarich and sug_sidur=" + iSugSidur.ToString());

                return ((dr.Length > 0) ? true : false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr = null;
            }
        }
        public void ChishuvMichsatYom(Oved oved)
        {
            DateTime d_taarich = oved.Month;
            DateTime d_ad_taarich;
            //clMeafyenyOved objMeafyeneyOved = null;
            //clPirteyOved objPirteyOved = null;
            DataRow[] dr;
            //int iSugYom;
            try
            {
                dr = oved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString());
                if (dr.Length > 0)
                {
                    d_taarich = oved.dTchilatAvoda <= oved.Month ? oved.Month : oved.dTchilatAvoda;
                    d_ad_taarich = oved.Month.AddMonths(1).AddDays(-1) <= oved.dSiyumAvoda ? oved.Month.AddMonths(1).AddDays(-1) : oved.dSiyumAvoda;
                    for (int i = d_taarich.Day; i <= d_ad_taarich.Day; i++)
                    {
                        //objMeafyeneyOved = oved.MeafyeneyOved.Find(Meafyenim => Meafyenim._Taarich == d_taarich);
                        //objPirteyOved = oved.PirteyOved.Find(Pratim => (Pratim._TaarichMe <= d_taarich && Pratim._TaarichAd >= d_taarich));
                        //iSugYom = GetSugYomLemichsa(oved, d_taarich, objPirteyOved.iKodSectorIsuk, objMeafyeneyOved.GetMeafyen(56).IntValue);

                        //if (iSugYom == enSugYom.Chol.GetHashCode())
                        //{
                            dr = oved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + d_taarich.ToShortDateString() + "', 'System.DateTime')");
                            if (dr.Length > 0 && float.Parse(dr[0]["ERECH_RECHIV"].ToString()) > 0)
                            {
                                if(oved.fmichsatYom<float.Parse(dr[0]["ERECH_RECHIV"].ToString()))
                                    oved.fmichsatYom = float.Parse(dr[0]["ERECH_RECHIV"].ToString());  
                            }
                       // }
                        d_taarich = d_taarich.AddDays(1);
                    }
                    //while (oved.fmichsatYom == 0)
                    //{
                    //    objMeafyeneyOved = oved.MeafyeneyOved.Find(Meafyenim => Meafyenim._Taarich == d_taarich);
                    //    objPirteyOved = oved.PirteyOved.Find(Pratim => (Pratim._TaarichMe <= d_taarich && Pratim._TaarichAd >= d_taarich));
                    //    iSugYom = GetSugYomLemichsa(oved, d_taarich, objPirteyOved.iKodSectorIsuk, objMeafyeneyOved.GetMeafyen(56).IntValue);

                    //    if (iSugYom == enSugYom.Chol.GetHashCode())
                    //    {
                    //        dr = oved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + d_taarich.ToShortDateString() + "', 'System.DateTime')");
                    //        if (dr.Length > 0 && float.Parse(dr[0]["ERECH_RECHIV"].ToString()) > 0)
                    //        {
                    //            oved.fmichsatYom = float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                    //            break;
                    //        }
                    //    }
                    //    d_taarich = d_taarich.AddDays(1);
                    //}
                }
            }
            catch (Exception ex)
            {
                //clLogBakashot.SetError(oved.iBakashaId, oved.Mispar_ishi, "E", 0, oved.Month, "clCalcBL: " + ex.StackTrace + "\n message: " + ex.StackTrace + "\n message: " + ex.Message);
                throw ex;
            }
        }

        public float ChishuvTosefetGil(Oved objOved, float fMichsaYomit, float fNuchehutLepremia,DateTime dShatGmar)
        {
            bool bShishi, bErevChag,bChisuv;
            float fTosefetGil = 0;
            try
            {
                bChisuv = false;
                bShishi = CheckYomShishi(objOved.SugYom);
                bErevChag = CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom);

                if (objOved.objMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {
                    if (fMichsaYomit > 0 && (!bErevChag) || (bErevChag && dShatGmar < objOved.objParameters.dKnisatShabat))
                    {
                        bChisuv = true;
                    }
                }
                else
                {
                    if (fMichsaYomit > 0 && ((!bShishi && !bErevChag) || ((bShishi || bErevChag) && dShatGmar < objOved.objParameters.dKnisatShabat)))
                    {
                        bChisuv = true;
                    }
                }

                if (bChisuv)
                {
                    if (objOved.objPirteyOved.iGil == enKodGil.enKashish.GetHashCode())
                        fTosefetGil = (fNuchehutLepremia * 30) / objOved.objParameters.iChalukaTosefetGilKashish;
                    else if (objOved.objPirteyOved.iGil == enKodGil.enKshishon.GetHashCode())
                        fTosefetGil = (fNuchehutLepremia * 30) / objOved.objParameters.iChalukaTosefetGilKshishon;
                }

                return fTosefetGil;
            }
            catch (Exception ex)
            {
                //clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Taarich, "CalcDay:ChishuvTosefetGil " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
        }

        public float getMichsaYomit(Oved objOved)
        {
            int iSugYom=0;
            try
            {
                return getMichsaYomit(objOved, ref iSugYom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public float getMichsaYomit(Oved objOved, ref int iSugYom)
        {
            int iSugYomLemichsa;
            float fErechRechiv;
            try
            {
                iSugYomLemichsa = 0;
                fErechRechiv = GetMichsaYomit(objOved, ref iSugYomLemichsa);
                iSugYom = iSugYomLemichsa;
                if (iSugYom == enSugYom.Purim.GetHashCode() && objOved.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode())
                {
                    if (!CheckYomShishi(iSugYom))
                    {
                        iSugYomLemichsa = enSugYom.Chol.GetHashCode();
                        fErechRechiv = GetMichsaYomit(objOved, ref iSugYomLemichsa);
                    }
                    else
                    {
                        iSugYomLemichsa = enSugYom.Shishi.GetHashCode();
                        fErechRechiv = GetMichsaYomit(objOved, ref iSugYomLemichsa);
                    }
                }

                if (iSugYom == enSugYom.ShushanPurim.GetHashCode() && objOved.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode())
                {
                    iSugYomLemichsa = enSugYom.Purim.GetHashCode();
                    fErechRechiv = GetMichsaYomit(objOved, ref iSugYomLemichsa);
                }

                if (iSugYom == enSugYom.ShushanPurim.GetHashCode() && objOved.objPirteyOved.iEzor != clGeneral.enEzor.Yerushalim.GetHashCode())
                {
                    if (!CheckYomShishi(iSugYom))
                    {
                        iSugYomLemichsa = enSugYom.Chol.GetHashCode();
                        fErechRechiv = GetMichsaYomit(objOved, ref iSugYomLemichsa);
                    }
                    else
                    {
                        iSugYomLemichsa = enSugYom.Shishi.GetHashCode();
                        fErechRechiv = GetMichsaYomit(objOved, ref iSugYomLemichsa);
                    }
                }

                return fErechRechiv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsOvedWithMeafyen85YomMeyuchad(Oved objOved)
        {
            bool bflag = false;
            try
            {
                if ((objOved.SugYom == enSugYom.CholHamoedPesach.GetHashCode() || objOved.SugYom == enSugYom.CholHamoedSukot.GetHashCode() ||
                                    objOved.SugYom == enSugYom.ErevRoshHashna.GetHashCode() || objOved.SugYom == enSugYom.ErevYomKipur.GetHashCode() ||
                                    objOved.SugYom == enSugYom.ErevSukot.GetHashCode() || objOved.SugYom == enSugYom.ErevSimchatTora.GetHashCode() ||
                                    objOved.SugYom == enSugYom.ErevPesach.GetHashCode() || objOved.SugYom == enSugYom.ErevPesachSheni.GetHashCode() ||
                                    objOved.SugYom == enSugYom.ErevShavuot.GetHashCode())
                                   && objOved.objMeafyeneyOved.GetMeafyen(85).IntValue == 1)
                {
                    bflag = true;
                }

                return bflag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KDSCommon.DataModels;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.DAL;
using System.Collections.Specialized;
using KDSCommon.Enums;
using KDSCommon.Helpers;

namespace KDSBLLogic.Managers
{
    public class OvedManager : IOvedManager
    {
        private IUnityContainer _container;
        public OvedManager(IUnityContainer container)
        {
            _container = container;
        }

        public OvedYomAvodaDetailsDM CreateOvedDetails(int iMisparIshi, DateTime dDate)
        {
            var dtOvedCardDetails = _container.Resolve<IOvedDAL>().GetOvedYomAvodaDetails(iMisparIshi, dDate);
            if (dtOvedCardDetails.Rows.Count > 0)
            {
                OvedYomAvodaDetailsDM ovedYomAvodaDatails = new OvedYomAvodaDetailsDM();
                SetMeafyneyOved(ovedYomAvodaDatails, dtOvedCardDetails);
                ovedYomAvodaDatails.bOvedDetailsExists = true;
                return ovedYomAvodaDatails;
            }
            return new OvedYomAvodaDetailsDM();
        }

        private void SetMeafyneyOved(OvedYomAvodaDetailsDM ovedYomAvodaDatails, DataTable dtOvedCardDetails)
        {
            try
            {
                //נתונים כללים               
                //נוציא את שדה הלבשה ברמת יום עבודה
                if (dtOvedCardDetails.Rows[0]["halbasha"] != null)
                {
                    ovedYomAvodaDatails.sHalbasha = dtOvedCardDetails.Rows[0]["halbasha"].ToString();
                }
                if (dtOvedCardDetails.Rows[0]["Hamara"] != null)
                {
                    ovedYomAvodaDatails.sHamara = dtOvedCardDetails.Rows[0]["Hamara"].ToString();
                }

                //קוד מעמד
                ovedYomAvodaDatails.sKodMaamd = dtOvedCardDetails.Rows[0]["maamad"].ToString();
                if (!string.IsNullOrEmpty(ovedYomAvodaDatails.sKodMaamd))
                {
                    ovedYomAvodaDatails.sKodHaver = ovedYomAvodaDatails.sKodMaamd.Substring(0, 1);
                }

                ovedYomAvodaDatails.sBitulZmanNesiot = dtOvedCardDetails.Rows[0]["bitul_zman_nesiot"].ToString();
                ovedYomAvodaDatails.sTachograf = dtOvedCardDetails.Rows[0]["Tachograf"].ToString();
                ovedYomAvodaDatails.iKodHevra = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["kod_hevra"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["kod_hevra"].ToString());
                ovedYomAvodaDatails.iKodHevraHashala = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_HEVRA_HASHALA"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_HEVRA_HASHALA"].ToString());
                ovedYomAvodaDatails.sLina = dtOvedCardDetails.Rows[0]["lina"].ToString();
                ovedYomAvodaDatails.sMercazErua = dtOvedCardDetails.Rows[0]["mercaz_erua"].ToString();
                ovedYomAvodaDatails.bMercazEruaExists = !(String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["mercaz_erua"].ToString()));
                ovedYomAvodaDatails.sHashlamaLeyom = dtOvedCardDetails.Rows[0]["Hashlama_Leyom"].ToString();
                ovedYomAvodaDatails.iSibatHashlamaLeyom = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"].ToString());
                ovedYomAvodaDatails.iMisparIshi = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mispar_ishi"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mispar_ishi"].ToString());
                ovedYomAvodaDatails.iIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Isuk"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Isuk"].ToString());
                ovedYomAvodaDatails.iZmanMutamut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["dakot_mutamut"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["dakot_mutamut"].ToString());
                ovedYomAvodaDatails.sMutamut = dtOvedCardDetails.Rows[0]["mutaam"].ToString();
                ovedYomAvodaDatails.bMutamutExists = !String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["mutaam"].ToString());
                ovedYomAvodaDatails.sSidurDay = dtOvedCardDetails.Rows[0]["iDay"].ToString();
                ovedYomAvodaDatails.sShabaton = dtOvedCardDetails.Rows[0]["shbaton"].ToString();
                ovedYomAvodaDatails.sErevShishiChag = dtOvedCardDetails.Rows[0]["erev_shishi_chag"].ToString();
                ovedYomAvodaDatails.sRishyonAutobus = dtOvedCardDetails.Rows[0]["rishyon_autobus"].ToString().Trim();
                ovedYomAvodaDatails.sShlilatRishayon = dtOvedCardDetails.Rows[0]["shlilat_rishayon"].ToString();
                ovedYomAvodaDatails.iDirug = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["dirug"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["dirug"].ToString());
                ovedYomAvodaDatails.iDarga = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["darga"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["darga"].ToString());   
                ovedYomAvodaDatails.iZmanNesiaHaloch = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"].ToString());
                ovedYomAvodaDatails.iZmanNesiaHazor = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"].ToString());
                ovedYomAvodaDatails.iStatus = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Status"]) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["Status"].ToString());
                ovedYomAvodaDatails.iStatusTipul = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["status_tipul"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["status_tipul"].ToString());
                ovedYomAvodaDatails.sStatusCardDesc = dtOvedCardDetails.Rows[0]["teur_status_kartis"].ToString();
                ovedYomAvodaDatails.sDayTypeDesc = dtOvedCardDetails.Rows[0]["teur_yom"].ToString();
                ovedYomAvodaDatails.iMeasherOMistayeg = String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString()) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString());
                ovedYomAvodaDatails.iSnifAv = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["snif_av"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["snif_av"].ToString());
                ovedYomAvodaDatails.iKodSectorIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"].ToString());
                ovedYomAvodaDatails.iSnifTnua = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Snif_Tnua"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Snif_Tnua"].ToString());
                ovedYomAvodaDatails.iBechishuvSachar = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["BECHISHUV_SACHAR"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["BECHISHUV_SACHAR"].ToString());
                ovedYomAvodaDatails.iMikumYechida = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mikum_yechida"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mikum_yechida"].ToString());
                ovedYomAvodaDatails.iMikumYechidaLenochechut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_MIKUM_YECHIDA_AV_LENOCH"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_MIKUM_YECHIDA_AV_LENOCH"].ToString());
                ovedYomAvodaDatails.dTaarichMe = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["me_tarich"]) ? DateTime.MinValue : DateTime.Parse(dtOvedCardDetails.Rows[0]["me_tarich"].ToString());
                ovedYomAvodaDatails.dTaarichAd = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["ad_tarich"]) ? DateTime.MinValue : DateTime.Parse(dtOvedCardDetails.Rows[0]["ad_tarich"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate)
        {
            return _container.Resolve<IOvedDAL>().GetOvedDetails(iMisparIshi, dCardDate);
        }

        public MeafyenimDM CreateMeafyenyOved(int iMisparIshi, DateTime dDate, DataTable meafyenim)
        {

            if (meafyenim!=null && meafyenim.Rows.Count > 0)
            {
                Dictionary<int, Meafyen> dict = PrepareMeafyenim(meafyenim);
                var meafyenimDM = new MeafyenimDM(dict);
                meafyenimDM.Taarich = dDate;
                return meafyenimDM;
            }
            return null;
        }

        public MeafyenimDM CreateMeafyenyOved(int iMisparIshi, DateTime dDate)
        {
            var dtMeafyenyOved = _container.Resolve<IOvedDAL>().GetMeafyeneyBitzuaLeOved(iMisparIshi, dDate);
            if (dtMeafyenyOved.Rows.Count > 0)
            {
                Dictionary<int, Meafyen> dict = PrepareMeafyenim(dtMeafyenyOved);
                var meafyenimDM = new MeafyenimDM(dict);
                meafyenimDM.Taarich = dDate;
                return meafyenimDM;
            }
            return null;
        }

        private Dictionary<int, Meafyen> PrepareMeafyenim(DataTable dtMeafyenyOved)
        {
            try
            {
                var List = from c in dtMeafyenyOved.AsEnumerable()
                           select new
                           {
                               kod = Int32.Parse(c.Field<string>("kod_meafyen").ToString()),
                               exist = Int32.Parse(c.Field<string>("source_meafyen").ToString()),
                               value = c.Field<string>("value_erech_ishi"),
                               erech_ishi = c.Field<string>("erech_ishi_partany")
                           };
                Dictionary<int, Meafyen> Meafyenim = List.ToDictionary(item => item.kod, item =>
                {
                    return new Meafyen((item.exist == 1), item.value,item.erech_ishi);
                }
                    );
                return Meafyenim;

            }
            catch (Exception ex)
            {
                throw new Exception("PrepareMeafyenim :" + ex.Message);
            }
        }

        /// <summary>
        /// Need to review code and validate if relenvant.
        /// Copied from clDefinitions.InsertEmployeeDetails with minor modifications
        /// </summary>
        /// <param name="dtDetails"></param>
        /// <param name="dCardDate"></param>
        /// <param name="misparIshi"></param>
        /// <param name="iLastMisaprSidur"></param>
        /// <returns></returns>
        //public OrderedDictionary InsertEmployeeDetails (DataTable dtDetails, DateTime dCardDate, int misparIshi, out int iLastMisaprSidur, out OrderedDictionary htSpecialEmployeeDetails, out OrderedDictionary htFullSidurimDetails)
        //{
        //    int iMisparSidur, iPeilutMisparSidur;
        //    int iKey = 0;
        //    int i = 1;
        //    int iMisparSidurPrev = 0;
        //    DateTime dShatHatchala = DateTime.MinValue;
        //    DateTime dShatHatchalaPrev = new DateTime();
        //    SidurDM oSidur = null;
        //    PeilutDM oPeilut = null;
        //    OrderedDictionary htEmployeeDetails = new OrderedDictionary();
        //    //OrderedDictionary htSpecialEmployeeDetails = new OrderedDictionary();
        //    string sLastShilut = "";
        //    DataTable dtPeiluyot;
        //    enMakatType _MakatType;
        //    try
        //    {
        //        htSpecialEmployeeDetails = new OrderedDictionary();
        //        htFullSidurimDetails = new OrderedDictionary();
              
        //        //נשלוף את נתוני הפעילויות לאותו יום
        //        var kavimManager = _container.Resolve<IKavimManager>();
        //        dtPeiluyot = kavimManager.GetKatalogKavim(misparIshi, dCardDate, dCardDate);

        //        //HashTable-הכנסת כל הסידורים והפעילויות של עובד בתאריך הנתון ל
        //        foreach (DataRow dr in dtDetails.Rows)
        //        {

        //            iMisparSidur = int.Parse(dr["Mispar_Sidur"].ToString());
        //            dShatHatchala = DateTime.Parse(dr["shat_hatchala"].ToString());

        //            if ((iMisparSidur != iMisparSidurPrev) || (iMisparSidur == iMisparSidurPrev) && (dShatHatchala != dShatHatchalaPrev))
        //            {
        //                iKey = 1;
        //                i++;
        //                //נתונים ברמת סידור

        //                var sidurManager = _container.Resolve<ISidurManager>();
        //                oSidur = sidurManager.CreateClsSidurFromEmployeeDetails(dr);

        //                List<int> SpecialSidurim = new List<int> { 99200 };
        //                if (SpecialSidurim.Contains(iMisparSidur))
        //                {
        //                    htSpecialEmployeeDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
        //                }
        //                else
        //                {
        //                    if (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iLebdikaShguim == 1))
        //                    {
        //                        htEmployeeDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
        //                    }

        //                    htFullSidurimDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
        //                }
        //                iMisparSidurPrev = iMisparSidur;
        //                dShatHatchalaPrev = dShatHatchala;

        //            }
        //            //נתוני פעילויות  

        //            iPeilutMisparSidur = (System.Convert.IsDBNull(dr["peilut_mispar_sidur"]) ? 0 : int.Parse(dr["peilut_mispar_sidur"].ToString()));
        //            if (iPeilutMisparSidur > 0)
        //            {
        //                var peilutManager = _container.Resolve<IPeilutManager>();
        //                oPeilut = peilutManager.CreateEmployeePeilut(dCardDate, iKey, dr, dtPeiluyot);
        //                _MakatType = (enMakatType)StaticBL.GetMakatType(oPeilut.lMakatNesia);
        //                if (_MakatType == enMakatType.mKavShirut)
        //                    sLastShilut = oPeilut.sShilut;
        //                else if (_MakatType == enMakatType.mVisut)
        //                    oPeilut.sShilut = sLastShilut;
        //                oSidur.htPeilut.Add(iKey, oPeilut);

        //                //אם לפחות אחד מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
        //                if (oPeilut.bPeilutEilat)
        //                    oSidur.bSidurEilat = true;

        //                //אם לפחות פעילות אחת לא ריקה, נגדיר את הסידור כסידור לא ריק
        //                if (!oSidur.bSidurNotEmpty)
        //                    oSidur.bSidurNotEmpty = oPeilut.bPeilutNotRekea;

        //                iKey++;
        //            }

        //        }

        //        if (dtDetails.Rows.Count > 0)
        //            iLastMisaprSidur = int.Parse(dtDetails.Rows[dtDetails.Rows.Count - 1]["mispar_sidur"].ToString());
        //        else 
        //            iLastMisaprSidur = 0;

        //        return htEmployeeDetails;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in InsertEmployeeDetails:" + " " + ex.Message);

        //    }
        //}


        public OrderedDictionary GetEmployeeDetails(bool bInsertToShguim, DataTable dtDetails,
                                               DateTime dCardDate, int misparIshi, out int iLastMisaprSidur,
                                               out OrderedDictionary htSpecialEmployeeDetails,
                                               out OrderedDictionary htFullSidurimDetails)
        {
            int iMisparSidur, iPeilutMisparSidur;
            int iKey = 0;
            int i = 1;
            int iMisparSidurPrev = 0;
            DateTime dShatHatchala = DateTime.MinValue;
            DateTime dShatHatchalaPrev = new DateTime();
            SidurDM oSidur = null;
            PeilutDM oPeilut = null;
            OrderedDictionary htEmployeeDetails = new OrderedDictionary();
            htSpecialEmployeeDetails = new OrderedDictionary();
            //htEmployeeDetailsWithCancled = new OrderedDictionary();
            string sLastShilut = "";
            DataTable dtPeiluyot;
            enMakatType _MakatType;
            try
            {
                htSpecialEmployeeDetails = new OrderedDictionary();
                htFullSidurimDetails = new OrderedDictionary();

                //נשלוף את נתוני הפעילויות לאותו יום
                var kavimManager = _container.Resolve<IKavimManager>();
                dtPeiluyot = kavimManager.GetKatalogKavim(misparIshi, dCardDate, dCardDate);

                //HashTable-הכנסת כל הסידורים והפעילויות של עובד בתאריך הנתון ל
                foreach (DataRow dr in dtDetails.Rows)
                {

                    iMisparSidur = int.Parse(dr["Mispar_Sidur"].ToString());
                    dShatHatchala = DateTime.Parse(dr["shat_hatchala"].ToString());

                    if ((iMisparSidur != iMisparSidurPrev) || (iMisparSidur == iMisparSidurPrev) && (dShatHatchala != dShatHatchalaPrev))
                    { 
                        iKey = 1;
                        i++;
                        //נתונים ברמת סידור

                        var sidurManager = _container.Resolve<ISidurManager>();
                        oSidur = sidurManager.CreateClsSidurFromEmployeeDetails(dr);

                        List<int> SpecialSidurim = new List<int> { 99200 };

                        if (SpecialSidurim.Contains(iMisparSidur))
                        {
                           htSpecialEmployeeDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
                        }
                        else
                        {
                            if (!bInsertToShguim || (bInsertToShguim && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iLebdikaShguim == 1))))
                                htEmployeeDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
                            
                            htFullSidurimDetails.Add(long.Parse(string.Concat(dShatHatchala.ToString("ddMM"), dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur)), oSidur);
                        }
                        iMisparSidurPrev = iMisparSidur;
                        dShatHatchalaPrev = dShatHatchala;
                    }
                    //נתוני פעילויות  
                    iPeilutMisparSidur = (System.Convert.IsDBNull(dr["peilut_mispar_sidur"]) ? 0 : int.Parse(dr["peilut_mispar_sidur"].ToString()));
                    if (iPeilutMisparSidur > 0)
                    {
                        var peilutManager = _container.Resolve<IPeilutManager>();
                        oPeilut = peilutManager.CreateEmployeePeilut(dCardDate, iKey, dr, dtPeiluyot);
                        _MakatType = (enMakatType)StaticBL.GetMakatType(oPeilut.lMakatNesia);
                        if (_MakatType == enMakatType.mKavShirut)
                            sLastShilut = oPeilut.sShilut;
                        else if (_MakatType == enMakatType.mVisut)
                            oPeilut.sShilut = sLastShilut;
                        oSidur.htPeilut.Add(iKey, oPeilut);

                        //אם לפחות אחד מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                        if (oPeilut.bPeilutEilat)
                            oSidur.bSidurEilat = true;

                        //אם לפחות פעילות אחת לא ריקה, נגדיר את הסידור כסידור לא ריק
                        if (!oSidur.bSidurNotEmpty)
                            oSidur.bSidurNotEmpty = oPeilut.bPeilutNotRekea;

                        iKey++;
                    }
                   

                }
               
                if (dtDetails.Rows.Count > 0)
                    iLastMisaprSidur = int.Parse(dtDetails.Rows[dtDetails.Rows.Count - 1]["mispar_sidur"].ToString());
                else
                    iLastMisaprSidur = 0;

                return htEmployeeDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in InsertEmployeeDetails:" + " " + ex.Message);

            }
        }



        public void UpdateCardStatus(int iMisparIshi, DateTime dCardDate, CardStatus oCardStatus, int iUserId)
        {
            _container.Resolve<IOvedDAL>().UpdateCardStatus(iMisparIshi, dCardDate, oCardStatus,iUserId);
        }

        public bool IsOvedMatzavExists(string sKodMatzav, DataTable dtMatzavOved)
        {
            DataRow[] dr;
            bool bOvedMatzavExists;

            try
            {
                sKodMatzav = NumerichHelper.Append0ToNumber(sKodMatzav);

                dr = dtMatzavOved.Select(string.Concat("kod_matzav='", sKodMatzav + "'"));
                bOvedMatzavExists = dr.Length > 0;
                return bOvedMatzavExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}


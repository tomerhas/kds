using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using KDSCommon.DataModels;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Enums;
using KDSCommon.Helpers;

namespace KdsLibrary.KDSLogic.Managers
{
    public class SidurManager : ISidurManager 
    {
        private const int SIDUR_RETIZVUT99500 = 99500;
        private const int SIDUR_RETIZVUT99501 = 99501;
        
        private IUnityContainer _container;
        
        public SidurManager(IUnityContainer container)
        {
            _container = container;
        }

        public SidurDM CreateClsSidurFromSidurayGrira(DataRow dr)
        {
            SidurDM cls = new SidurDM();

            cls.iMisparIshi = int.Parse(dr["Mispar_Ishi"].ToString());
            cls.iMisparSidur = (System.Convert.IsDBNull(dr["Mispar_Sidur"]) ? 0 : int.Parse(dr["Mispar_Sidur"].ToString()));
            cls.sShatGmar = (System.Convert.IsDBNull(dr["Shat_gmar"]) ? "" : DateTime.Parse(dr["Shat_gmar"].ToString()).ToString("HH:mm"));
            if (String.IsNullOrEmpty(cls.sShatGmar))
            {
                cls.dFullShatGmar = DateTime.MinValue;
                cls.dOldFullShatGmar = cls.dFullShatGmar;
            }
            else
            {
                cls.dFullShatGmar = DateTime.Parse(dr["Shat_gmar"].ToString());
                cls.dOldFullShatGmar = cls.dFullShatGmar;
            }

            cls.dFullShatHatchala = DateTime.Parse(dr["Shat_Hatchala"].ToString());
            cls.sShatHatchala = (cls.dFullShatHatchala.Year < DateHelper.cYearNull ? "" : DateTime.Parse(dr["Shat_Hatchala"].ToString()).ToString("HH:mm"));

            cls.sShatHatchalaLetashlum = (System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? "" : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString()).ToString("HH:mm"));

            cls.dFullShatHatchalaLetashlum = System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString());
            if (cls.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                cls.sShatHatchalaLetashlum = "";

            cls.sShatGmarLetashlum = (System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? "" : DateTime.Parse(dr["shat_gmar_letashlum"].ToString()).ToString("HH:mm"));

            cls.dFullShatGmarLetashlum = System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum"].ToString());
            cls.dOldFullShatGmarLetashlum = cls.dFullShatGmarLetashlum;
            cls.dSidurDate = (DateTime)dr["taarich"];

            cls.sVisa = dr["yom_visa"].ToString();
            cls.sChariga = dr["Chariga"].ToString();
            cls.sOldChariga = cls.sChariga;
            cls.sPitzulHafsaka = dr["Pitzul_Hafsaka"].ToString();
            cls.sOldPitzulHafsaka = cls.sPitzulHafsaka;
            cls.sOutMichsa = dr["out_michsa"].ToString();
            cls.sOldOutMichsa = cls.sOutMichsa;
            cls.sHashlama = dr["hashlama"].ToString();
            cls.sOldHashlama = cls.sHashlama;
            cls.iSugHashlama = (System.Convert.IsDBNull(dr["sug_hashlama"]) ? 0 : int.Parse(dr["sug_hashlama"].ToString()));
            cls.bHashlamaExists = !(String.IsNullOrEmpty(dr["Hashlama"].ToString()));
            cls.iLoLetashlum = (System.Convert.IsDBNull(dr["Lo_Letashlum"]) ? 0 : int.Parse(dr["Lo_Letashlum"].ToString()));
            cls.iOldLoLetashlum = cls.iLoLetashlum;
            cls.iShayahLeyomKodem = (System.Convert.IsDBNull(dr["shayah_leyom_kodem"]) ? 0 : int.Parse(dr["shayah_leyom_kodem"].ToString()));
            cls.iMisparShiureyNehiga = (System.Convert.IsDBNull(dr["mispar_shiurey_nehiga"]) ? 0 : int.Parse(dr["mispar_shiurey_nehiga"].ToString()));

            cls.sMikumShaonKnisa = dr["mikum_shaon_knisa"].ToString();
            cls.sMikumShaonYetzia = dr["mikum_shaon_yetzia"].ToString();

            cls.iKodSibaLedivuchYadaniIn = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_in"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_in"].ToString());
            cls.iOldKodSibaLedivuchYadaniIn = cls.iKodSibaLedivuchYadaniIn;

            cls.iKodSibaLedivuchYadaniOut = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_out"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_out"].ToString());
            cls.iOldKodSibaLedivuchYadaniOut = cls.iKodSibaLedivuchYadaniOut;

            cls.iKodSibaLoLetashlum = System.Convert.IsDBNull(dr["kod_siba_lo_letashlum"]) ? 0 : int.Parse(dr["kod_siba_lo_letashlum"].ToString());
            cls.iMezakeNesiot = System.Convert.IsDBNull(dr["mezake_nesiot"]) ? 0 : int.Parse(dr["mezake_nesiot"].ToString());
            cls.iMezakeHalbasha = System.Convert.IsDBNull(dr["mezake_halbasha"]) ? 0 : int.Parse(dr["mezake_halbasha"].ToString());

            cls.lMeadkenAcharon = System.Convert.IsDBNull(dr["meadken_acharon"]) ? 0 : int.Parse(dr["meadken_acharon"].ToString());
            cls.iBitulOHosafa = System.Convert.IsDBNull(dr["bitul_o_hosafa"]) ? 0 : int.Parse(dr["bitul_o_hosafa"].ToString());
            cls.sHeara = dr["Heara"].ToString();
            cls.iNidreshetHitiatzvut = System.Convert.IsDBNull(dr["nidreshet_hitiatzvut"]) ? 0 : int.Parse(dr["nidreshet_hitiatzvut"].ToString());
            cls.dShatHitiatzvut = System.Convert.IsDBNull(dr["shat_hitiatzvut"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hitiatzvut"].ToString());
            cls.iPtorMehitiatzvut = System.Convert.IsDBNull(dr["ptor_mehitiatzvut"]) ? 0 : int.Parse(dr["ptor_mehitiatzvut"].ToString());
            cls.dTaarichIdkunAcharon = System.Convert.IsDBNull(dr["taarich_idkun_acharon"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon"].ToString());
            cls.iHachtamaBeatarLoTakin = System.Convert.IsDBNull(dr["Hachtama_Beatar_Lo_Takin"]) ? 0 : int.Parse(dr["Hachtama_Beatar_Lo_Takin"].ToString());
            cls.iMenahelMusachMeadken = System.Convert.IsDBNull(dr["MENAHEL_MUSACH_MEADKEN"]) ? 0 : int.Parse(dr["MENAHEL_MUSACH_MEADKEN"].ToString());
            cls.dShatHatchalaMenahelMusach = System.Convert.IsDBNull(dr["shat_hatchala_letashlum_musach"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum_musach"].ToString());
            cls.dShatGmarMenahelMusach = System.Convert.IsDBNull(dr["shat_gmar_letashlum_musach"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum_musach"].ToString());

            return cls;
        }

        public SidurDM CreateClsSidurFromEmployeeDetails( DataRow dr)
        {
            SidurDM cls = new SidurDM();
            int iResult;
            DataTable dsSidurim;

            //נתונים ברמת סידור            
            cls.iMisparIshi = int.Parse(dr["Mispar_Ishi"].ToString());
            cls.iMisparSidur = (System.Convert.IsDBNull(dr["Mispar_Sidur"]) ? 0 : int.Parse(dr["Mispar_Sidur"].ToString()));
            cls.iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));
            cls.sShatGmar = (System.Convert.IsDBNull(dr["Shat_gmar"]) ? "" : DateTime.Parse(dr["Shat_gmar"].ToString()).ToString("HH:mm"));
            if (String.IsNullOrEmpty(cls.sShatGmar))
            {
                cls.dFullShatGmar = DateTime.MinValue;
                cls.dOldFullShatGmar = cls.dFullShatGmar;
            }
            else
            {
                cls.dFullShatGmar = DateTime.Parse(dr["Shat_gmar"].ToString());
                cls.dOldFullShatGmar = cls.dFullShatGmar;
            }
            //שעת גמר 00:00 זה חצות, במקרה של NULL יהיה ערך ריק
            //if (!string.IsNullOrEmpty(sShatGmar))
            //{
            //    if (int.Parse(sShatGmar.Remove(2, 1)) == 0)
            //    {
            //        sShatGmar = "";
            //    }
            //}

            cls.dFullShatHatchala = DateTime.Parse(dr["Shat_Hatchala"].ToString());
            cls.dOldFullShatHatchala = cls.dFullShatHatchala;
            cls.sShatHatchala = (cls.dFullShatHatchala.Year < DateHelper.cYearNull ? "" : DateTime.Parse(dr["Shat_Hatchala"].ToString()).ToString("HH:mm"));
            //if (!string.IsNullOrEmpty(sShatHatchala))
            //{
            //    if (int.Parse(sShatHatchala.Remove(2, 1)) == 0)
            //    {
            //        sShatHatchala = "";
            //    }
            //}
            cls.sShatHatchalaLetashlum = (System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? "" : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString()).ToString("HH:mm"));

            cls.dFullShatHatchalaLetashlum = System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString());
            cls.dOldFullShatHatchalaLetashlum = cls.dFullShatHatchalaLetashlum;
            cls.sShatGmarLetashlum = (System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? "" : DateTime.Parse(dr["shat_gmar_letashlum"].ToString()).ToString("HH:mm"));

            if (cls.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                cls.sShatHatchalaLetashlum = "";

            cls.dFullShatGmarLetashlum = System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum"].ToString());
            cls.dOldFullShatGmarLetashlum = cls.dFullShatGmarLetashlum;
            cls.dShatHatchalaMenahelMusach = System.Convert.IsDBNull(dr["shat_hatchala_letashlum_musach"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum_musach"].ToString());
            cls.dShatGmarMenahelMusach = System.Convert.IsDBNull(dr["shat_gmar_letashlum_musach"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum_musach"].ToString());

            cls.dSidurDate = (DateTime)dr["taarich"];
            cls.sSidurDay = dr["iDay"].ToString();
            cls.sVisa = dr["yom_visa"].ToString();
            cls.sChariga = dr["Chariga"].ToString();
            cls.sOldChariga = cls.sChariga;
            cls.sPitzulHafsaka = dr["Pitzul_Hafsaka"].ToString();//(System.Convert.IsDBNull(dr["Pitzul_Hafsaka"]) ? 0 : int.Parse(dr["Pitzul_Hafsaka"].ToString()));
            cls.sOldPitzulHafsaka = cls.sPitzulHafsaka;
            cls.iZakayLepizul = (System.Convert.IsDBNull(dr["zakay_lepizul"]) ? 0 : int.Parse(dr["zakay_lepizul"].ToString()));
            cls.sOutMichsa = dr["out_michsa"].ToString();
            cls.sOldOutMichsa = cls.sOutMichsa;
            cls.sHashlama = dr["hashlama"].ToString();//(System.Convert.IsDBNull(dr["hashlama"]) ? 0 : int.Parse(dr["Hashlama"].ToString()));
            cls.sOldHashlama = cls.sHashlama;
            cls.bHashlamaKodExists = !(String.IsNullOrEmpty(dr["hashlama"].ToString()));
            cls.iSugHashlama = (System.Convert.IsDBNull(dr["sug_hashlama"]) ? 0 : int.Parse(dr["sug_hashlama"].ToString()));
            cls.bHashlamaExists = !(String.IsNullOrEmpty(dr["Hashlama"].ToString()));
            //iKmVisaLepremia = (System.Convert.IsDBNull(dr["Km_Visa_Lepremia"]) ? 0 : int.Parse(dr["Km_Visa_Lepremia"].ToString()));
            cls.iLoLetashlum = (System.Convert.IsDBNull(dr["Lo_Letashlum"]) ? 0 : int.Parse(dr["Lo_Letashlum"].ToString()));
            cls.iOldLoLetashlum = cls.iLoLetashlum;
            cls.iShayahLeyomKodem = (System.Convert.IsDBNull(dr["shayah_leyom_kodem"]) ? 0 : int.Parse(dr["shayah_leyom_kodem"].ToString()));
            cls.iMisparShiureyNehiga = (System.Convert.IsDBNull(dr["mispar_shiurey_nehiga"]) ? 0 : int.Parse(dr["mispar_shiurey_nehiga"].ToString()));
            //iMikumShaonKnisa =(dr["Mikum_ShaonKnisa"] == null ? 0 : int.Parse(dr["Mikum_ShaonKnisa"].ToString()));
            cls.sSectorAvoda = dr["sector_avoda"].ToString();
            cls.bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
            cls.iSectorVisa = (System.Convert.IsDBNull(dr["sector_visa"]) ? 0 : int.Parse(dr["sector_visa"].ToString()));
            cls.bSectorVisaExists = !String.IsNullOrEmpty(dr["sector_visa"].ToString());
            cls.iSidurLoChosem = (System.Convert.IsDBNull(dr["sidur_lo_chosem"]) ? 0 : int.Parse(dr["sidur_lo_chosem"].ToString()));
            cls.bSidurLoChosemExists = !String.IsNullOrEmpty(dr["sidur_lo_chosem"].ToString());
            cls.iSidurLoNibdakSofShavua = (System.Convert.IsDBNull(dr["sidur_lo_nivdak_sofash"]) ? 0 : int.Parse(dr["sidur_lo_nivdak_sofash"].ToString()));
            cls.iMivtzaVisa = (System.Convert.IsDBNull(dr["mivtza_visa"]) ? 0 : int.Parse(dr["mivtza_visa"].ToString()));
            cls.iTafkidVisa = (System.Convert.IsDBNull(dr["tafkid_visa"]) ? 0 : int.Parse(dr["tafkid_visa"].ToString()));
            cls.iElement1Hova = (System.Convert.IsDBNull(dr["element1_hova"]) ? 0 : int.Parse(dr["element1_hova"].ToString()));
            cls.iElement2Hova = (System.Convert.IsDBNull(dr["element2_hova"]) ? 0 : int.Parse(dr["element2_hova"].ToString()));
            cls.iElement3Hova = (System.Convert.IsDBNull(dr["element3_hova"]) ? 0 : int.Parse(dr["element3_hova"].ToString()));
            cls.bElement1HovaExists = !(String.IsNullOrEmpty(dr["element1_hova"].ToString()));
            cls.bElement2HovaExists = !(String.IsNullOrEmpty(dr["element2_hova"].ToString()));
            cls.bElement3HovaExists = !(String.IsNullOrEmpty(dr["element3_hova"].ToString()));
            cls.sHalbashKod = dr["Halbash_Kod"].ToString();
            cls.bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
            cls.sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
            cls.bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
            cls.sShatGmarMuteret = dr["shat_gmar_muteret"].ToString();
            cls.bShatGmarMuteretExists = !(String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()));
            cls.sNoPeilotKod = dr["No_Peilot_Kod"].ToString();
            cls.bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
            cls.sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
            cls.bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
            cls.sZakaiLehamara = dr["zakay_lehamara"].ToString();
            cls.bZakaiLehamaraExists = !(String.IsNullOrEmpty(dr["zakay_lehamara"].ToString()));
            cls.sZakaiLeChariga = dr["zakay_lechariga"].ToString();
            cls.bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
            cls.iZakaiLelina = System.Convert.IsDBNull(dr["zakay_lelina"]) ? 0 : int.Parse(dr["zakay_lelina"].ToString());
            cls.sTokefHatchala = dr["tokef_hatchala"].ToString();
            cls.sTokefSiyum = dr["tokef_siyum"].ToString();


            cls.sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
            cls.bZakayLezamanNesiaExists = !(String.IsNullOrEmpty(dr["zakay_leaman_nesia"].ToString()));
            cls.sShabaton = dr["shbaton"].ToString();
            cls.sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();

            cls.sErevShishiChag = dr["erev_shishi_chag"].ToString();
            cls.iSugYom = (System.Convert.IsDBNull(dr["Sug_Yom"]) ? 0 : int.Parse(dr["Sug_Yom"].ToString()));
            //iKodMatzav =(dr["Kod_Matzav"] == null ? 0 : int.Parse(dr["Kod_Matzav"].ToString()));
            //sBitulZmanNesiot = dr["Bitul_Zman_Nesiot"].ToString();
            //            iMisparSiduriOto = (System.Convert.IsDBNull(dr["mispar_siduri_oto"]) ? 0 : int.Parse(dr["mispar_siduri_oto"].ToString()));
            cls.sSidurInSummer = dr["sidur_in_summer"].ToString();
            cls.bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
            cls.sNoOtoNo = dr["no_oto_no"].ToString();
            cls.bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
            cls.sHashlamaKod = dr["hashlama_kod"].ToString();
            cls.sShaonNochachut = dr["shaon_nochachut"].ToString();
            cls.bShaonNochachutExists = !(String.IsNullOrEmpty(dr["shaon_nochachut"].ToString()));
            cls.sLoLetashlumAutomati = dr["lo_letashlum_automati"].ToString();
            cls.bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
            cls.sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
            cls.bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));

            cls.sNitanLedaveachAdTaarich = dr["nitan_ledaveach_ad_taarich"].ToString();
            cls.bNitanLedaveachAdTaarichExists = !(String.IsNullOrEmpty(dr["nitan_ledaveach_ad_taarich"].ToString()));

            cls.sSugAvoda = dr["sug_avoda"].ToString();
            cls.bSugAvodaExists = !(String.IsNullOrEmpty(dr["sug_avoda"].ToString()));
            cls.sMikumShaonKnisa = dr["mikum_shaon_knisa"].ToString();
            cls.sMikumShaonYetzia = dr["mikum_shaon_yetzia"].ToString();
            cls.sPeilutRequiredKod = dr["peilut_required_kod"].ToString();
            cls.bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
            cls.sSidurNotValidKod = dr["sidur_not_valid_kod"].ToString();
            cls.bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));

            cls.sRashaiLedaveach = dr["rashai_ledaveach"].ToString();
            cls.bRashaiLedaveachExists = !(String.IsNullOrEmpty(dr["rashai_ledaveach"].ToString()));

            //sSidurNetzerKod = dr["sidur_netzer_kod"].ToString();
            cls.sSidurNotInShabtonKod = dr["sidur_not_in_shabton_kod"].ToString();
            cls.bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
            cls.bSidurMyuhad = IsSidurMyuhad(cls.iMisparSidur.ToString());
            cls.sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
            cls.iKodSibaLedivuchYadaniIn = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_in"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_in"].ToString());
            cls.iOldKodSibaLedivuchYadaniIn = cls.iKodSibaLedivuchYadaniIn;
            cls.iKodSibaLedivuchYadaniOut = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_out"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_out"].ToString());
            cls.iOldKodSibaLedivuchYadaniOut = cls.iKodSibaLedivuchYadaniOut;
            cls.iKodSibaLoLetashlum = System.Convert.IsDBNull(dr["kod_siba_lo_letashlum"]) ? 0 : int.Parse(dr["kod_siba_lo_letashlum"].ToString());
            cls.iMezakeNesiot = System.Convert.IsDBNull(dr["mezake_nesiot"]) ? 0 : int.Parse(dr["mezake_nesiot"].ToString());
            cls.iMezakeHalbasha = System.Convert.IsDBNull(dr["mezake_halbasha"]) ? 0 : int.Parse(dr["mezake_halbasha"].ToString());
            cls.iLebdikaShguim = System.Convert.IsDBNull(dr["LEBDIKAT_SHGUIM"]) ? 0 : int.Parse(dr["LEBDIKAT_SHGUIM"].ToString());
            cls.iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());

            cls.sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
            cls.bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
            cls.iTosefetGrira = System.Convert.IsDBNull(dr["tosefet_grira"]) ? 0 : int.Parse(dr["tosefet_grira"].ToString());
            cls.iAchuzKnasLepremyatVisa = System.Convert.IsDBNull(dr["achuz_knas_lepremyat_visa"]) ? 0 : int.Parse(dr["achuz_knas_lepremyat_visa"].ToString());
            cls.iAchuzVizaBesikun = System.Convert.IsDBNull(dr["achuz_viza_besikun"]) ? 0 : int.Parse(dr["achuz_viza_besikun"].ToString());
            //iSugVisa = System.Convert.IsDBNull(dr["sug_visa"]) ? 0 : int.Parse(dr["sug_visa"].ToString());
            cls.iMisparMusachOMachsan = System.Convert.IsDBNull(dr["mispar_musach_o_machsan"]) ? 0 : int.Parse(dr["mispar_musach_o_machsan"].ToString());
            cls.iSugHazmanatVisa = System.Convert.IsDBNull(dr["sug_hazmanat_visa"]) ? 0 : int.Parse(dr["sug_hazmanat_visa"].ToString());
            // iButal = System.Convert.IsDBNull(dr["butal"]) ? 0 : int.Parse(dr["butal"].ToString());
            cls.lMeadkenAcharon = System.Convert.IsDBNull(dr["meadken_acharon"]) ? 0 : int.Parse(dr["meadken_acharon"].ToString());
            cls.iBitulOHosafa = System.Convert.IsDBNull(dr["bitul_o_hosafa"]) ? 0 : int.Parse(dr["bitul_o_hosafa"].ToString());
            cls.sHeara = dr["Heara"].ToString();
            cls.iNidreshetHitiatzvut = System.Convert.IsDBNull(dr["nidreshet_hitiatzvut"]) ? 0 : int.Parse(dr["nidreshet_hitiatzvut"].ToString());
            cls.iHachtamaBeatarLoTakin = System.Convert.IsDBNull(dr["Hachtama_Beatar_Lo_Takin"]) ? 0 : int.Parse(dr["Hachtama_Beatar_Lo_Takin"].ToString());
            cls.dShatHitiatzvut = System.Convert.IsDBNull(dr["shat_hitiatzvut"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hitiatzvut"].ToString());
            cls.iPtorMehitiatzvut = System.Convert.IsDBNull(dr["ptor_mehitiatzvut"]) ? 0 : int.Parse(dr["ptor_mehitiatzvut"].ToString());
            cls.dTaarichIdkunAcharon = System.Convert.IsDBNull(dr["taarich_idkun_acharon"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon"].ToString());
            cls.sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
            cls.bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString()));
            cls.sSidurDescription = dr["teur_sidur_meychad"].ToString();
            cls.iMenahelMusachMeadken = System.Convert.IsDBNull(dr["MENAHEL_MUSACH_MEADKEN"]) ? 0 : int.Parse(dr["MENAHEL_MUSACH_MEADKEN"].ToString());
            cls.iMikumAvKnisa = int.Parse(dr["mikum_av_knisa"].ToString());
            cls.iMikumAvYetzia = int.Parse(dr["mikum_av_yatzia"].ToString());
            cls.bIsKnisaTkina_err197 = bool.Parse(dr["knisa_tkina_err197"].ToString());
            cls.bIsYetziaTkina_err198 = bool.Parse(dr["yatzia_tkina_err198"].ToString());
            cls.bSidurAsurBeyomShishi = !(String.IsNullOrEmpty(dr["sidur_asur_beyom_shishi"].ToString()));
            cls.bMatalaKlalitLeloRechev = !(String.IsNullOrEmpty(dr["matala_klalit_lelo_rechev"].ToString()));
            cls.iSidurLebdikatRezefMachala = System.Convert.IsDBNull(dr["lebdikat_rezef_machala"]) ? 0 : int.Parse(dr["lebdikat_rezef_machala"].ToString());

            if (cls.bSidurMyuhad)
            {
                cls.bSidurRetizfut = IsSidurRetzifut(cls.iMisparSidur);
                cls.bSidurNahagut = IsSidurNahagut(cls);
                if (!cls.bSidurNahagut)
                { cls.bSidurTafkid = IsSidurTafkid(cls); }
            }
            else if (dr["SUG_SIDUR"] != null && dr["SUG_SIDUR"].ToString() != "")
                cls.iSugSidurRagil = int.Parse(dr["SUG_SIDUR"].ToString());
            //לכל סידור רגיל נפנה לתנועה לקבלת פרטים נוספים על הסידור
            // נקבל את סוג הסידור ואם הוא קיים במפת התכנון
            //  bSidurRagilExists = false;
            //if (!bSidurMyuhad && bGetSidurDetails)
            //{
            //    dsSidurim = GetSidurRagilDetails(dSidurDate, iMisparSidur, iShayahLeyomKodem, out iResult);
            //    if (iResult == 0)
            //    {
            //        //if (dsSidurim.Tables[0].Rows.Count > 0)
            //        if (dsSidurim.Rows.Count > 0)
            //        {
            //         //   bSidurRagilExists = true;
            //            //iSugSidurRagil = int.Parse(dsSidurim.Tables[0].Rows[0]["SUG_SIDUR"].ToString());
            //            iSugSidurRagil = int.Parse(dsSidurim.Rows[0]["SUGSIDUR"].ToString());
            //         //   if (dsSidurim.Rows[0]["PREMIUM"].ToString() != "")
            //          //      iPremium = int.Parse(dsSidurim.Rows[0]["PREMIUM"].ToString());
            //        }
            //     }
            //  }

            return cls;
        }

        public SidurDM CreateClsSidurFromSidurMeyuchad(SidurDM oSidurKodem, DateTime dTaarich, int iMisparSidurNew, DataRow dr)
        {
            SidurDM cls = new SidurDM();
            int iResult;
            DataTable dsSidurim;

            //נתונים ברמת סידור            
            cls.iMisparIshi = oSidurKodem.iMisparIshi;
            cls.iMisparSidur = iMisparSidurNew;

            cls.sShatGmar = oSidurKodem.dFullShatGmar.ToString("HH:mm");
            cls.dFullShatGmar = oSidurKodem.dFullShatGmar;
            cls.dOldFullShatGmar = cls.dFullShatGmar;

            cls.dFullShatHatchala = oSidurKodem.dFullShatHatchala;
            cls.sShatHatchala = oSidurKodem.dFullShatHatchala.ToString("HH:mm");
            cls.sShatHatchalaLetashlum = oSidurKodem.sShatHatchalaLetashlum;
            cls.dFullShatHatchalaLetashlum = oSidurKodem.dFullShatHatchalaLetashlum;
            cls.dOldFullShatHatchalaLetashlum = cls.dFullShatHatchalaLetashlum;
            cls.sShatGmarLetashlum = oSidurKodem.dFullShatGmarLetashlum.ToString("HH:mm");
            cls.dFullShatGmarLetashlum = oSidurKodem.dFullShatGmarLetashlum;
            cls.dOldFullShatGmarLetashlum = cls.dFullShatGmarLetashlum;
            cls.dSidurDate = dTaarich;
            cls.sSidurDay = (dTaarich.GetHashCode() + 1).ToString();

            cls.sVisa = oSidurKodem.sVisa;
            cls.sChariga = oSidurKodem.sChariga;
            cls.sOldChariga = cls.sChariga;
            cls.sPitzulHafsaka = oSidurKodem.sPitzulHafsaka;
            cls.sOldPitzulHafsaka = cls.sPitzulHafsaka;
            cls.iZakayLepizul = oSidurKodem.iZakayLepizul;
            cls.sOutMichsa = oSidurKodem.sOutMichsa;
            cls.sOldOutMichsa = cls.sOutMichsa;
            cls.sHashlama = oSidurKodem.sHashlama;
            cls.sOldHashlama = cls.sHashlama;
            cls.bHashlamaExists = oSidurKodem.bHashlamaExists;
            cls.iLoLetashlum = oSidurKodem.iLoLetashlum;
            cls.iOldLoLetashlum = cls.iLoLetashlum;
            cls.iShayahLeyomKodem = oSidurKodem.iShayahLeyomKodem;
            cls.iMisparShiureyNehiga = oSidurKodem.iMisparShiureyNehiga;
            cls.iSugYom = oSidurKodem.iSugYom;
            cls.sShabaton = oSidurKodem.sShabaton;
            cls.sErevShishiChag = oSidurKodem.sErevShishiChag;
            cls.sHeara = oSidurKodem.sHeara;
            cls.dTaarichIdkunAcharon = oSidurKodem.dTaarichIdkunAcharon;
            cls.lMeadkenAcharon = oSidurKodem.lMeadkenAcharon;
            cls.iBitulOHosafa = oSidurKodem.iBitulOHosafa;
            cls.iNidreshetHitiatzvut = oSidurKodem.iNidreshetHitiatzvut;
            cls.iHachtamaBeatarLoTakin = oSidurKodem.iHachtamaBeatarLoTakin;
            cls.dShatHitiatzvut = oSidurKodem.dShatHitiatzvut;
            cls.iPtorMehitiatzvut = oSidurKodem.iPtorMehitiatzvut;
            cls.iSectorVisa = oSidurKodem.iSectorVisa;
            cls.iMivtzaVisa = oSidurKodem.iMivtzaVisa;
            cls.iSidurLoChosem = oSidurKodem.iSidurLoChosem;
            cls.bSectorVisaExists = oSidurKodem.bSectorVisaExists;
            cls.iKodSibaLedivuchYadaniIn = oSidurKodem.iKodSibaLedivuchYadaniIn;
            cls.iKodSibaLedivuchYadaniOut = oSidurKodem.iKodSibaLedivuchYadaniOut;
            cls.iMezakeNesiot = oSidurKodem.iMezakeNesiot;
            cls.iMezakeHalbasha = oSidurKodem.iMezakeHalbasha;
            cls.iKodSibaLoLetashlum = oSidurKodem.iKodSibaLoLetashlum;
            cls.iAchuzKnasLepremyatVisa = oSidurKodem.iAchuzKnasLepremyatVisa;
            cls.iAchuzVizaBesikun = oSidurKodem.iAchuzVizaBesikun;
            cls.iTosefetGrira = oSidurKodem.iTosefetGrira;
            cls.iMisparMusachOMachsan = oSidurKodem.iMisparMusachOMachsan;
            cls.iSugHazmanatVisa = oSidurKodem.iSugHazmanatVisa;
            cls.iSidurLoNibdakSofShavua = oSidurKodem.iSidurLoNibdakSofShavua;
            cls.iMenahelMusachMeadken = oSidurKodem.iMenahelMusachMeadken;
            cls.dShatHatchalaMenahelMusach = oSidurKodem.dShatHatchalaMenahelMusach;
            cls.dShatGmarMenahelMusach = oSidurKodem.dShatGmarMenahelMusach;

            cls.bSidurMyuhad = IsSidurMyuhad(cls.iMisparSidur.ToString());

            if (cls.bSidurMyuhad)
            {
                cls.iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));

                cls.sSectorAvoda = dr["sector_avoda"].ToString();
                cls.bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
                cls.sHalbashKod = dr["Halbash_Kod"].ToString();
                cls.bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
                cls.sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
                cls.bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
                cls.sShatGmarMuteret = dr["shat_gmar_muteret"].ToString();
                cls.bShatGmarMuteretExists = !(String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()));
                cls.sNoPeilotKod = dr["No_Peilot_Kod"].ToString();
                cls.bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
                cls.sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
                cls.bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
                cls.sZakaiLehamara = dr["zakay_lehamara"].ToString();
                cls.bZakaiLehamaraExists = !(String.IsNullOrEmpty(dr["zakay_lehamara"].ToString()));
                cls.sZakaiLeChariga = dr["zakay_lechariga"].ToString();
                cls.bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
                cls.sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
                cls.bZakayLezamanNesiaExists = !(String.IsNullOrEmpty(dr["zakay_leaman_nesia"].ToString()));
                cls.iZakaiLelina = System.Convert.IsDBNull(dr["zakay_lelina"]) ? 0 : int.Parse(dr["zakay_lelina"].ToString());
                cls.sTokefHatchala = dr["tokef_hatchala"].ToString();
                cls.sTokefSiyum = dr["tokef_siyum"].ToString();

                cls.sSidurInSummer = dr["sidur_in_summer"].ToString();
                cls.bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
                cls.sNoOtoNo = dr["no_oto_no"].ToString();
                cls.bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
                cls.sHashlamaKod = dr["hashlama_kod"].ToString();
                cls.sShaonNochachut = dr["shaon_nochachut"].ToString();
                cls.sLoLetashlumAutomati = dr["lo_letashlum_automati"].ToString();
                cls.bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
                cls.sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
                cls.bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));
                cls.sSugAvoda = dr["sug_avoda"].ToString();
                cls.bSugAvodaExists = !(String.IsNullOrEmpty(dr["sug_avoda"].ToString()));
                cls.sPeilutRequiredKod = dr["peilut_required_kod"].ToString();
                cls.bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
                cls.sSidurNotValidKod = dr["sidur_not_valid_kod"].ToString();
                cls.bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));
                cls.sSidurNotInShabtonKod = dr["sidur_not_in_shabton_kod"].ToString();
                cls.bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
                cls.iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());
                cls.sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();
                cls.sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
                cls.sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
                cls.bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
                cls.sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
                cls.bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString()));
                cls.bSidurAsurBeyomShishi = !(String.IsNullOrEmpty(dr["sidur_asur_beyom_shishi"].ToString()));
                cls.bMatalaKlalitLeloRechev = !(String.IsNullOrEmpty(dr["matala_klalit_lelo_rechev"].ToString()));
                cls.iSidurLebdikatRezefMachala = System.Convert.IsDBNull(dr["lebdikat_rezef_machala"]) ? 0 : int.Parse(dr["lebdikat_rezef_machala"].ToString());

                cls.bSidurRetizfut = IsSidurRetzifut(cls.iMisparSidur);
                cls.bSidurNahagut = IsSidurNahagut(cls);
                if (!cls.bSidurNahagut)
                { cls.bSidurTafkid = IsSidurTafkid(cls); }

            }
            else if (dr["SUG_SIDUR"] != null && dr["SUG_SIDUR"].ToString() != "")
                cls.iSugSidurRagil = int.Parse(dr["SUG_SIDUR"].ToString());
            //לכל סידור רגיל נפנה לתנועה לקבלת פרטים נוספים על הסידור
            // נקבל את סוג הסידור ואם הוא קיים במפת התכנון
            //bSidurRagilExists = false;
            //if (!bSidurMyuhad)
            //{
            //    dsSidurim = GetSidurRagilDetails(dSidurDate, iMisparSidur, iShayahLeyomKodem, out iResult);
            //    if (iResult == 0)
            //    {
            //        //if (dsSidurim.Tables[0].Rows.Count > 0)
            //        if (dsSidurim.Rows.Count > 0)
            //        {
            //            bSidurRagilExists = true;
            //            //iSugSidurRagil = int.Parse(dsSidurim.Tables[0].Rows[0]["SUG_SIDUR"].ToString());
            //            iSugSidurRagil = int.Parse(dsSidurim.Rows[0]["SUGSIDUR"].ToString());
            //            if (dsSidurim.Rows[0]["PREMIUM"].ToString() != "")
            //                 iPremium = int.Parse(dsSidurim.Rows[0]["PREMIUM"].ToString());
            //        }
            //    }
            //}

            cls.htPeilut = oSidurKodem.htPeilut;

            return cls;
        }

        public bool IsSidurChofef(int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iParamChafifa, DataTable dt)
        { 
            return _container.Resolve<ISidurDAL>().IsSidurChofef(iMisparIshi, dCardDate, iMisparSidur, dShatHatchala, dShatGmar, iParamChafifa, dt);
        }

        public float GetMeshechSidur(int iMisparIshi, int iMisparSidur, DateTime taarich_me, DateTime taarich_ad)
        {
            return _container.Resolve<ISidurDAL>().GetMeshechSidur(iMisparIshi, iMisparSidur, taarich_me, taarich_ad);
        }
        //public DataSet GetSidurAndPeiluyotFromTnua(int iMisparSidur, DateTime dDate, int? iKnisaVisut, out int iResult)
        //{
        //    return _container.Resolve<ISidurDAL>().GetSidurAndPeiluyotFromTnua(iMisparSidur, dDate, iKnisaVisut,out iResult);
        //}
        private bool IsSidurNahagut(SidurDM cls)
        {
            //מחזיר אם סידור הוא מסוג נהגות
            bool bSidurNahagut = false;

            if (cls.bSectorAvodaExists)
            {
                bSidurNahagut = (int.Parse(cls.sSectorAvoda) == enSectorAvoda.Nahagut.GetHashCode());
            }
            return bSidurNahagut;
        }

        private bool IsSidurTafkid(SidurDM cls)
        {
            //מחזיר אם סידור הוא מסוג תפקיד
            bool bSidurTafkid = false;

            if (cls.bSectorAvodaExists)
            {
                bSidurTafkid = (int.Parse(cls.sSectorAvoda) == enSectorAvoda.Tafkid.GetHashCode());
            }
            return bSidurTafkid;
        }
        private bool IsSidurRetzifut(int iSidurNum)
        {
            return ((iSidurNum == SIDUR_RETIZVUT99500) || (iSidurNum == SIDUR_RETIZVUT99501));

        }


        

        private bool IsSidurMyuhad(string sMisparSidur)
        {
            if (sMisparSidur.Length > 1)
                return (sMisparSidur.Substring(0, 2) == "99");
            else
            {
                return false;
            }
        }


        public double CalculatePremya(OrderedDictionary htPeilut, out double dElementsHamtanaReshut)
        {
            double dSumMazanTashlum = 0;
            double dSumMazanElementim = 0;
            double dTempPremia = 0;
            int iTypeMakat;
            PeilutDM oPeilut;
            dElementsHamtanaReshut = 0;
            try
            {
                for (int i = 0; i < htPeilut.Values.Count; i++)
                {
                    oPeilut = ((PeilutDM)htPeilut[i]);
                    iTypeMakat = oPeilut.iMakatType;
                    if (oPeilut.iMisparKnisa == 0 && (iTypeMakat == enMakatType.mKavShirut.GetHashCode() || iTypeMakat == enMakatType.mEmpty.GetHashCode() || iTypeMakat == enMakatType.mNamak.GetHashCode()))
                    {
                        dSumMazanTashlum += oPeilut.iMazanTashlum;
                    }
                    else if (iTypeMakat == enMakatType.mElement.GetHashCode())
                    {
                        if (oPeilut.sElementInMinutes == "1" && oPeilut.sKodLechishuvPremia.Trim() == "1:1")
                        {
                            dSumMazanElementim += Int32.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));

                        }
                        if (oPeilut.bElementLershutExists || oPeilut.bElementLershutExists)
                        {
                            dElementsHamtanaReshut += Int32.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                        }
                    }
                }

                dSumMazanElementim = dSumMazanElementim - (dSumMazanElementim * 0.3);

                dTempPremia = (dSumMazanTashlum * 1.333) + dSumMazanElementim;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dTempPremia;

        }


        //private DataTable GetSidurRagilDetails(DateTime dCardDate, int iMisparSidur, int iShayahLeyomKodem, out int iResult)
        //{
        //    //סידורים רגילים
        //    //נבדוק מול התנועה
        //    string sCacheKey = iMisparSidur + dCardDate.ToShortDateString();
        //    DataTable dsSidurim = new DataTable();
        //    clKavim oKavim = new clKavim();
        //    DateTime dSidurDate = dCardDate;
        //    //if (iShayahLeyomKodem == 1)
        //    //{   //אם הסידור שייך מבחינת תכנון ליום הקודם (בודקת האם בשדה Shayah_LeYom_Kodem בטבלת "סידורים עובדים" קיים ערך 1) . אם כן, יש לפנות  לממשק עם תאריך של יום קודם.
        //    //    dSidurDate = dSidurDate.AddDays(-1);
        //    //}
        //    iResult = 0;
        //    try
        //    {
        //        dsSidurim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
        //    }
        //    catch (Exception ex)
        //    {
        //        dsSidurim = null;
        //    }
        //    if (dsSidurim == null)
        //    {
        //        dsSidurim = oKavim.GetSidurDetailsFromTnua(iMisparSidur, dSidurDate, out iResult);
        //        HttpRuntime.Cache.Insert(sCacheKey, dsSidurim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
        //    }

        //    return dsSidurim;
        //}


        //public clSidur(int iMispar_Ishi,DateTime dTaarich, int iMisparSidurNew, DataRow dr)
        //{
        //    int iResult;
        //    DataTable dsSidurim;

        //    //נתונים ברמת סידור            
        //    iMisparIshi = iMispar_Ishi;
        //    iMisparSidur = iMisparSidurNew;

        //    dSidurDate = dTaarich;
        //    sSidurDay = (dTaarich.GetHashCode() + 1).ToString();
        //    sPitzulHafsaka = "0";
        //    sOldPitzulHafsaka = "0";
        //    bSidurMyuhad = IsSidurMyuhad(iMisparSidur.ToString());

        //    if (bSidurMyuhad)
        //    {
        //        iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));

        //        sSectorAvoda = dr["sector_avoda"].ToString();
        //        iSidurLoNibdakSofShavua = (System.Convert.IsDBNull(dr["sidur_lo_nivdak_sofash"]) ? 0 : int.Parse(dr["sidur_lo_nivdak_sofash"].ToString()));

        //        bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
        //        sHalbashKod = dr["Halbash_Kod"].ToString();
        //        bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
        //        sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
        //        bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
        //        sShatGmarMuteret = dr["shat_gmar_muteret"].ToString();
        //        sTokefHatchala = dr["tokef_hatchala"].ToString();
        //        sTokefSiyum = dr["tokef_siyum"].ToString();
        //        bShatGmarMuteretExists = !(String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()));
        //        sNoPeilotKod = dr["No_Peilot_Kod"].ToString();
        //        bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
        //        sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
        //        bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
        //        sZakaiLehamara = dr["zakay_lehamara"].ToString();
        //        bZakaiLehamaraExists = !(String.IsNullOrEmpty(dr["zakay_lehamara"].ToString()));
        //        sZakaiLeChariga = dr["zakay_lechariga"].ToString();
        //        bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
        //        sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
        //        bZakayLezamanNesiaExists = !(String.IsNullOrEmpty(dr["zakay_leaman_nesia"].ToString()));
        //        sSidurInSummer = dr["sidur_in_summer"].ToString();
        //        bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
        //        iZakaiLelina = System.Convert.IsDBNull(dr["zakay_lelina"]) ? 0 : int.Parse(dr["zakay_lelina"].ToString());

        //        sNoOtoNo = dr["no_oto_no"].ToString();
        //        bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
        //        sHashlamaKod = dr["hashlama_kod"].ToString();

        //        sShaonNochachut = dr["shaon_nochachut"].ToString();
        //        sLoLetashlumAutomati = dr["lo_letashlum_automati"].ToString();
        //        bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
        //        sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
        //        bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));
        //        sSugAvoda = dr["sug_avoda"].ToString();
        //        bSugAvodaExists = !(String.IsNullOrEmpty(dr["sug_avoda"].ToString()));
        //        sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();

        //        sPeilutRequiredKod = dr["peilut_required_kod"].ToString();
        //        bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
        //        sSidurNotValidKod = dr["sidur_not_valid_kod"].ToString();
        //        bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));
        //        sSidurNotInShabtonKod = dr["sidur_not_in_shabton_kod"].ToString();
        //        bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
        //        iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());

        //        sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
        //           sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
        //        bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
        //        sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
        //        bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString())); 

        //            bSidurRetizfut = IsSidurRetzifut(iMisparSidur);
        //            bSidurNahagut = IsSidurNahagut();
        //            if (!bSidurNahagut)
        //            { bSidurTafkid = IsSidurTafkid(); }

        //    }
        //    else if(dr["SUG_SIDUR"] != null &&  dr["SUG_SIDUR"].ToString() != "")
        //        iSugSidurRagil = int.Parse(dr["SUG_SIDUR"].ToString());

        //    iMenahelMusachMeadken = System.Convert.IsDBNull(dr["MENAHEL_MUSACH_MEADKEN"]) ? 0 : int.Parse(dr["MENAHEL_MUSACH_MEADKEN"].ToString());
        //    dShatHatchalaMenahelMusach = System.Convert.IsDBNull(dr["shat_hatchala_letashlum_musach"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum_musach"].ToString());
        //    dShatGmarMenahelMusach = System.Convert.IsDBNull(dr["shat_gmar_letashlum_musach"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum_musach"].ToString()); 

        //    //לכל סידור רגיל נפנה לתנועה לקבלת פרטים נוספים על הסידור
        //    // נקבל את סוג הסידור ואם הוא קיים במפת התכנון
        //    //bSidurRagilExists = false;
        //    //if (!bSidurMyuhad)
        //    //{
        //    //    dsSidurim = 
        //    //        GetSidurRagilDetails(dSidurDate, iMisparSidur, iShayahLeyomKodem, out iResult);
        //    //    if (iResult == 0)
        //    //    {
        //    //        //if (dsSidurim.Tables[0].Rows.Count > 0)
        //    //        if (dsSidurim.Rows.Count > 0)
        //    //        {
        //    //            bSidurRagilExists = true;
        //    //            //iSugSidurRagil = int.Parse(dsSidurim.Tables[0].Rows[0]["SUG_SIDUR"].ToString());
        //    //            iSugSidurRagil = int.Parse(dsSidurim.Rows[0]["SUGSIDUR"].ToString());
        //    //            if (dsSidurim.Rows[0]["PREMIUM"].ToString() != "")
        //    //                iPremium = int.Parse(dsSidurim.Rows[0]["PREMIUM"].ToString());
        //    //        }
        //    //    }
        //    //}
        //}

    }
}


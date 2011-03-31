using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using KdsLibrary.BL;
using KdsLibrary;
using System.Diagnostics;
using System.Web;
namespace KdsBatch
{

    public class clSidur
    {
        //נתוני עובד
        public int iMisparIshi;
        public int iMisparSidur;
        public int iMisparSidurMyuhad;
        public string sShatGmar;
        public DateTime dFullShatHatchala;
        public DateTime dOldFullShatHatchala;
        public DateTime dFullShatGmar;
        public DateTime dOldFullShatGmar;
        public string sShatHatchala;
        public DateTime  dSidurDate;
        public string sSidurDay;
        public string sShatHatchalaLetashlum;
        public String sShatGmarLetashlum;
        public DateTime dFullShatHatchalaLetashlum;
        public DateTime dFullShatGmarLetashlum;
        public DateTime dOldFullShatHatchalaLetashlum;
        public DateTime dOldFullShatGmarLetashlum;
        public string sVisa;
        public string sChariga;
        public string sOldChariga;
        public string sPitzulHafsaka;
        public string sOldPitzulHafsaka;

        public int iZakayLepizul;
        public string sOutMichsa;
        public string sOldOutMichsa;
        public string sHashlama;
        public string sOldHashlama;
        public int iSugHashlama;
        public int iMivtzaVisa;
        public int iTafkidVisa;
        public string sLidroshKodMivtza;
        public bool bHashlamaExists;
        //public int iKmVisaLepremia;
        public int iLoLetashlum;
        public int iOldLoLetashlum;
        public int iMisparShiureyNehiga;
        public string sShatHatchalaMuteret;
        public bool bShatHatchalaMuteretExists;
        public string sShatGmarMuteret;
        public bool bShatGmarMuteretExists;
        public string sHalbashKod;
        public bool bHalbashKodExists;
        public string sNoPeilotKod;
        public bool bNoPeilotKodExists;
        public string sSidurVisaKod;
        public bool bSidurVisaKodExists;
        public string sSectorAvoda;//-5העדרות, -1נהגות,תפקיד
        public bool bSectorAvodaExists;
        public int iSectorVisa;
        public bool bSectorVisaExists;
        public string sZakaiLehamara;
        public bool bZakaiLehamaraExists;
        public string sZakaiLeChariga;
        public bool bZakaiLeCharigaExists;
        public string sZakayLezamanNesia;
        public bool bZakayLezamanNesiaExists;
        public string sHashlamaKod;       
        public bool bHashlamaKodExists;
        public string sShaonNochachut;
        public bool bShaonNochachutExists;
        public string sLoLetashlumAutomati;
        public bool bLoLetashlumAutomatiExists;
        //public int iMisparSiduriOto;
        public string sSidurInSummer;
        public bool bSidurInSummerExists;
        public string sNoOtoNo;
        public bool bNoOtoNoExists;
        public string sHeadrutTypeKod; //מאפיין 53
        public bool bHeadrutTypeKodExists; //מאפיין 53
        public string sNitanLedaveachAdTaarich; //מאפיין 27
        public bool bNitanLedaveachAdTaarichExists; //מאפיין 27
        public string sSugAvoda;//מאפיין 52
        public bool bSugAvodaExists;//מאפיין 52
        //public string sSugSidurType3; //מאפיין 3 בטבלת סוגי מאפיינים
        public string sMikumShaonKnisa;
        public string sMikumShaonYetzia;
        public string sPeilutRequiredKod;
        public bool bPeilutRequiredKodExists;
        public string sSidurNotValidKod;
        public bool bSidurNotValidKodExists;

        public string sRashaiLedaveach; //מאפיין 99
        public bool bRashaiLedaveachExists;

        //public string sSidurNetzerKod;
        public string sSidurNotInShabtonKod;
        public bool bSidurNotInShabtonKodExists;
        public int iElement1Hova; //מאפיין 93
        public int iElement2Hova;//מאפיין 94
        public int iElement3Hova; //מאפיין 95
        public bool bElement1HovaExists; //מאפיין 93
        public bool bElement2HovaExists; //מאפיין 94
        public bool bElement3HovaExists; //מאפיין 95
        public int iNitanLedaveachBemachalaAruca;
        public int iShayahLeyomKodem;
        public bool bSidurRagilExists;
        public bool bSidurMyuhad;
        public bool bSidurEilat;
        public bool bSidurNotEmpty;
        public bool bSidurNahagut;
        public bool bSidurRetizfut;
        public bool bSidurTafkid = false;
        public int iSugSidurRagil;
        public int iKodSibaLedivuchYadaniIn;
        public int iOldKodSibaLedivuchYadaniIn;
        public int iKodSibaLedivuchYadaniOut;
        public int iOldKodSibaLedivuchYadaniOut;
        public int iKodSibaLoLetashlum;
        public int iMezakeNesiot;
        public string sKizuzAlPiHatchalaGmar;
        public bool bKizuzAlPiHatchalaGmarExists;
        public int iBitulOHosafa;
        public int iTosefetGrira;
        public int iAchuzKnasLepremyatVisa;
        public int iAchuzVizaBesikun;
        public string sHovatHityatzvut;
        public int iSidurLoNibdakSofShavua;
        //public int iSugVisa;
        public int iMisparMusachOMachsan;
        public int iSugHazmanatVisa;
        public bool bHovaMisparMachsan;
        //public int iButal;
        public long lMeadkenAcharon;
        public string sHeara;
        public DateTime dTaarichIdkunAcharon;
        //נתוני עובד
        //public int iLina;
        //public int iHalbasha;
        //public int iBitulZmanNeziot;
        //public char cTachograf;
        public string sShabaton, sErevShishiChag;
        public int iSugYom;
        //public int iKodMatzav;
        //public string sBitulZmanNesiot;
        public int iNidreshetHitiatzvut;
        public int iHachtamaBeatarLoTakin;
        public DateTime dShatHitiatzvut;
        public int iPtorMehitiatzvut;
        public int iMezakeHalbasha;
        public int iLebdikaShguim;
        public string sSidurDescription;
        public string sZakayMichutzLamichsa; //מאפיין 25
        private const int SIDUR_RETIZVUT99500 = 99500;
        private const int SIDUR_RETIZVUT99501 = 99501;
        public int iPremium=0;
        //נתוני פעילות
        public OrderedDictionary htPeilut = new OrderedDictionary();

        //public enum enSidurType
        //{
        //    stTafkid = 1, //סידור תפקיד
        //    stHeadrut = 9, //סידור העדרות
        //    stDriver = 5 //סידור נהגות
        //}
        public void AddEmployeeSidurim(DataRow dr,bool bGetSidurDetails)
        {
            int iResult;
            DataTable dsSidurim;

            //נתונים ברמת סידור            
            iMisparIshi = int.Parse(dr["Mispar_Ishi"].ToString());
            iMisparSidur = (System.Convert.IsDBNull(dr["Mispar_Sidur"]) ? 0 : int.Parse(dr["Mispar_Sidur"].ToString()));
            iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));
            sShatGmar = (System.Convert.IsDBNull(dr["Shat_gmar"]) ? "" : DateTime.Parse(dr["Shat_gmar"].ToString()).ToString("HH:mm"));
            if (String.IsNullOrEmpty(sShatGmar)){            
                dFullShatGmar = DateTime.MinValue;
                dOldFullShatGmar = dFullShatGmar; 
            }
            else{            
                dFullShatGmar = DateTime.Parse(dr["Shat_gmar"].ToString());
                dOldFullShatGmar = dFullShatGmar; 
            }
            //שעת גמר 00:00 זה חצות, במקרה של NULL יהיה ערך ריק
            //if (!string.IsNullOrEmpty(sShatGmar))
            //{
            //    if (int.Parse(sShatGmar.Remove(2, 1)) == 0)
            //    {
            //        sShatGmar = "";
            //    }
            //}

            dFullShatHatchala = DateTime.Parse(dr["Shat_Hatchala"].ToString());
            dOldFullShatHatchala = dFullShatHatchala;
            sShatHatchala = (dFullShatHatchala.Year < clGeneral.cYearNull ? "" : DateTime.Parse(dr["Shat_Hatchala"].ToString()).ToString("HH:mm"));
            //if (!string.IsNullOrEmpty(sShatHatchala))
            //{
            //    if (int.Parse(sShatHatchala.Remove(2, 1)) == 0)
            //    {
            //        sShatHatchala = "";
            //    }
            //}
            sShatHatchalaLetashlum = (System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? "" : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString()).ToString("HH:mm"));

            dFullShatHatchalaLetashlum = System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString());
            dOldFullShatHatchalaLetashlum=dFullShatHatchalaLetashlum; 
            sShatGmarLetashlum = (System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? "" : DateTime.Parse(dr["shat_gmar_letashlum"].ToString()).ToString("HH:mm"));

            if (dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
                sShatHatchalaLetashlum = "";

            dFullShatGmarLetashlum = System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum"].ToString());
            dOldFullShatGmarLetashlum = dFullShatGmarLetashlum;

            dSidurDate = (DateTime)dr["taarich"];
            sSidurDay = dr["iDay"].ToString();
            sVisa = dr["yom_visa"].ToString();
            sChariga = dr["Chariga"].ToString();
            sOldChariga = sChariga;
            sPitzulHafsaka = dr["Pitzul_Hafsaka"].ToString();//(System.Convert.IsDBNull(dr["Pitzul_Hafsaka"]) ? 0 : int.Parse(dr["Pitzul_Hafsaka"].ToString()));
            sOldPitzulHafsaka = sPitzulHafsaka;
            iZakayLepizul = (System.Convert.IsDBNull(dr["zakay_lepizul"]) ? 0 : int.Parse(dr["zakay_lepizul"].ToString()));
            sOutMichsa = dr["out_michsa"].ToString();
            sOldOutMichsa = sOutMichsa;
            sHashlama = dr["hashlama"].ToString();//(System.Convert.IsDBNull(dr["hashlama"]) ? 0 : int.Parse(dr["Hashlama"].ToString()));
            sOldHashlama = sHashlama;
            bHashlamaKodExists = !(String.IsNullOrEmpty(dr["hashlama"].ToString()));
            iSugHashlama = (System.Convert.IsDBNull(dr["sug_hashlama"]) ? 0 : int.Parse(dr["sug_hashlama"].ToString()));
            bHashlamaExists = !(String.IsNullOrEmpty(dr["Hashlama"].ToString()));
            //iKmVisaLepremia = (System.Convert.IsDBNull(dr["Km_Visa_Lepremia"]) ? 0 : int.Parse(dr["Km_Visa_Lepremia"].ToString()));
            iLoLetashlum = (System.Convert.IsDBNull(dr["Lo_Letashlum"]) ? 0 : int.Parse(dr["Lo_Letashlum"].ToString()));
            iOldLoLetashlum = iLoLetashlum;
            iShayahLeyomKodem = (System.Convert.IsDBNull(dr["shayah_leyom_kodem"]) ? 0 : int.Parse(dr["shayah_leyom_kodem"].ToString()));
            iMisparShiureyNehiga = (System.Convert.IsDBNull(dr["mispar_shiurey_nehiga"]) ? 0 : int.Parse(dr["mispar_shiurey_nehiga"].ToString()));
            //iMikumShaonKnisa =(dr["Mikum_ShaonKnisa"] == null ? 0 : int.Parse(dr["Mikum_ShaonKnisa"].ToString()));
            sSectorAvoda = dr["sector_avoda"].ToString();
            bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
            iSectorVisa = (System.Convert.IsDBNull(dr["sector_visa"]) ? 0: int.Parse(dr["sector_visa"].ToString()));
            bSectorVisaExists = !String.IsNullOrEmpty(dr["sector_visa"].ToString());
            iSidurLoNibdakSofShavua = (System.Convert.IsDBNull(dr["sidur_lo_nivdak_sofash"]) ? 0 : int.Parse(dr["sidur_lo_nivdak_sofash"].ToString()));
            iMivtzaVisa = (System.Convert.IsDBNull(dr["mivtza_visa"]) ? 0 : int.Parse(dr["mivtza_visa"].ToString()));
            iTafkidVisa = (System.Convert.IsDBNull(dr["tafkid_visa"]) ? 0 : int.Parse(dr["tafkid_visa"].ToString()));
            iElement1Hova = (System.Convert.IsDBNull(dr["element1_hova"]) ? 0 : int.Parse(dr["element1_hova"].ToString()));
            iElement2Hova = (System.Convert.IsDBNull(dr["element2_hova"]) ? 0 : int.Parse(dr["element2_hova"].ToString()));
            iElement3Hova = (System.Convert.IsDBNull(dr["element3_hova"]) ? 0 : int.Parse(dr["element3_hova"].ToString()));
            bElement1HovaExists = !(String.IsNullOrEmpty(dr["element1_hova"].ToString()));
            bElement2HovaExists = !(String.IsNullOrEmpty(dr["element2_hova"].ToString()));
            bElement3HovaExists = !(String.IsNullOrEmpty(dr["element3_hova"].ToString()));
            sHalbashKod = dr["Halbash_Kod"].ToString();
            bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
            sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
            bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
            sShatGmarMuteret = dr["shat_gmar_muteret"].ToString();
            bShatGmarMuteretExists = !(String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()));
            sNoPeilotKod = dr["No_Peilot_Kod"].ToString();
            bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
            sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
            bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
            sZakaiLehamara = dr["zakay_lehamara"].ToString();
            bZakaiLehamaraExists = !(String.IsNullOrEmpty(dr["zakay_lehamara"].ToString()));
            sZakaiLeChariga = dr["zakay_lechariga"].ToString();
            bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
            sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
            bZakayLezamanNesiaExists = !(String.IsNullOrEmpty(dr["zakay_leaman_nesia"].ToString()));
            sShabaton = dr["shbaton"].ToString();
            sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();
            
            sErevShishiChag = dr["erev_shishi_chag"].ToString();
            iSugYom = (System.Convert.IsDBNull(dr["Sug_Yom"]) ? 0 : int.Parse(dr["Sug_Yom"].ToString()));
            //iKodMatzav =(dr["Kod_Matzav"] == null ? 0 : int.Parse(dr["Kod_Matzav"].ToString()));
            //sBitulZmanNesiot = dr["Bitul_Zman_Nesiot"].ToString();
            //            iMisparSiduriOto = (System.Convert.IsDBNull(dr["mispar_siduri_oto"]) ? 0 : int.Parse(dr["mispar_siduri_oto"].ToString()));
            sSidurInSummer = dr["sidur_in_summer"].ToString();
            bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
            sNoOtoNo = dr["no_oto_no"].ToString();
            bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
            sHashlamaKod = dr["hashlama_kod"].ToString();           
            sShaonNochachut = dr["shaon_nochachut"].ToString();
            bShaonNochachutExists = !(String.IsNullOrEmpty(dr["shaon_nochachut"].ToString()));
            sLoLetashlumAutomati = dr["lo_letashlum_automati"].ToString();
            bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
            sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
            bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));
            
            sNitanLedaveachAdTaarich = dr["nitan_ledaveach_ad_taarich"].ToString();
            bNitanLedaveachAdTaarichExists = !(String.IsNullOrEmpty(dr["nitan_ledaveach_ad_taarich"].ToString()));

            sSugAvoda = dr["sug_avoda"].ToString();
            bSugAvodaExists = !(String.IsNullOrEmpty(dr["sug_avoda"].ToString()));
            sMikumShaonKnisa = dr["mikum_shaon_knisa"].ToString();
            sMikumShaonYetzia = dr["mikum_shaon_yetzia"].ToString();
            sPeilutRequiredKod = dr["peilut_required_kod"].ToString();
            bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
            sSidurNotValidKod = dr["sidur_not_valid_kod"].ToString();
            bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));

            sRashaiLedaveach = dr["rashai_ledaveach"].ToString();
            bRashaiLedaveachExists = !(String.IsNullOrEmpty(dr["rashai_ledaveach"].ToString()));

            //sSidurNetzerKod = dr["sidur_netzer_kod"].ToString();
            sSidurNotInShabtonKod = dr["sidur_not_in_shabton_kod"].ToString();
            bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
            bSidurMyuhad = IsSidurMyuhad(iMisparSidur.ToString());
            sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
            iKodSibaLedivuchYadaniIn = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_in"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_in"].ToString());
            iOldKodSibaLedivuchYadaniIn = iKodSibaLedivuchYadaniIn;
            iKodSibaLedivuchYadaniOut = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_out"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_out"].ToString());
            iOldKodSibaLedivuchYadaniOut = iKodSibaLedivuchYadaniOut;
            iKodSibaLoLetashlum = System.Convert.IsDBNull(dr["kod_siba_lo_letashlum"]) ? 0 : int.Parse(dr["kod_siba_lo_letashlum"].ToString());
            iMezakeNesiot = System.Convert.IsDBNull(dr["mezake_nesiot"]) ? 0 : int.Parse(dr["mezake_nesiot"].ToString());
            iMezakeHalbasha = System.Convert.IsDBNull(dr["mezake_halbasha"]) ? 0 : int.Parse(dr["mezake_halbasha"].ToString());
            iLebdikaShguim = System.Convert.IsDBNull(dr["LEBDIKAT_SHGUIM"]) ? 0 : int.Parse(dr["LEBDIKAT_SHGUIM"].ToString());
            iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());
            
            sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
            bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
            iTosefetGrira = System.Convert.IsDBNull(dr["tosefet_grira"]) ? 0 : int.Parse(dr["tosefet_grira"].ToString());
            iAchuzKnasLepremyatVisa = System.Convert.IsDBNull(dr["achuz_knas_lepremyat_visa"]) ? 0 : int.Parse(dr["achuz_knas_lepremyat_visa"].ToString());
            iAchuzVizaBesikun = System.Convert.IsDBNull(dr["achuz_viza_besikun"]) ? 0 : int.Parse(dr["achuz_viza_besikun"].ToString());
            //iSugVisa = System.Convert.IsDBNull(dr["sug_visa"]) ? 0 : int.Parse(dr["sug_visa"].ToString());
            iMisparMusachOMachsan = System.Convert.IsDBNull(dr["mispar_musach_o_machsan"]) ? 0 : int.Parse(dr["mispar_musach_o_machsan"].ToString());
            iSugHazmanatVisa = System.Convert.IsDBNull(dr["sug_hazmanat_visa"]) ? 0 : int.Parse(dr["sug_hazmanat_visa"].ToString());
            // iButal = System.Convert.IsDBNull(dr["butal"]) ? 0 : int.Parse(dr["butal"].ToString());
            lMeadkenAcharon = System.Convert.IsDBNull(dr["meadken_acharon"]) ? 0 : int.Parse(dr["meadken_acharon"].ToString());
            iBitulOHosafa = System.Convert.IsDBNull(dr["bitul_o_hosafa"]) ? 0 : int.Parse(dr["bitul_o_hosafa"].ToString());
            sHeara = dr["Heara"].ToString();
            iNidreshetHitiatzvut = System.Convert.IsDBNull(dr["nidreshet_hitiatzvut"]) ? 0 : int.Parse(dr["nidreshet_hitiatzvut"].ToString());
            iHachtamaBeatarLoTakin = System.Convert.IsDBNull(dr["Hachtama_Beatar_Lo_Takin"]) ? 0 : int.Parse(dr["Hachtama_Beatar_Lo_Takin"].ToString());
            dShatHitiatzvut = System.Convert.IsDBNull(dr["shat_hitiatzvut"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hitiatzvut"].ToString());
            iPtorMehitiatzvut = System.Convert.IsDBNull(dr["ptor_mehitiatzvut"]) ? 0 : int.Parse(dr["ptor_mehitiatzvut"].ToString());
            dTaarichIdkunAcharon = System.Convert.IsDBNull(dr["taarich_idkun_acharon"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon"].ToString());
            sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
            bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString()));
            sSidurDescription = dr["teur_sidur_meychad"].ToString();
            if (bSidurMyuhad)
            {
                bSidurRetizfut = IsSidurRetzifut(iMisparSidur);
                bSidurNahagut = IsSidurNahagut();
                if (!bSidurNahagut)
                { bSidurTafkid = IsSidurTafkid(); }
            }

            //לכל סידור רגיל נפנה לתנועה לקבלת פרטים נוספים על הסידור
            // נקבל את סוג הסידור ואם הוא קיים במפת התכנון
            bSidurRagilExists = false;
            if (!bSidurMyuhad && bGetSidurDetails)
            {
                dsSidurim = GetSidurRagilDetails(dSidurDate, iMisparSidur, iShayahLeyomKodem, out iResult);
                if (iResult == 0)
                {
                    //if (dsSidurim.Tables[0].Rows.Count > 0)
                    if (dsSidurim.Rows.Count > 0)
                    {
                        bSidurRagilExists = true;
                        //iSugSidurRagil = int.Parse(dsSidurim.Tables[0].Rows[0]["SUG_SIDUR"].ToString());
                        iSugSidurRagil = int.Parse(dsSidurim.Rows[0]["SUGSIDUR"].ToString());
                        if (dsSidurim.Rows[0]["PREMIUM"].ToString() != "")
                            iPremium = int.Parse(dsSidurim.Rows[0]["PREMIUM"].ToString());
                    }
                }
            }
        }

        private bool IsSidurNahagut()
        {
            //מחזיר אם סידור הוא מסוג נהגות
            bool bSidurNahagut = false;

            if (bSectorAvodaExists)
            {
                bSidurNahagut = (int.Parse(sSectorAvoda) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
            }
            return bSidurNahagut;
        }

        private bool IsSidurTafkid()
        {
            //מחזיר אם סידור הוא מסוג תפקיד
            bool bSidurTafkid = false;

            if (bSectorAvodaExists)
            {
                bSidurTafkid = (int.Parse(sSectorAvoda) == clGeneral.enSectorAvoda.Tafkid.GetHashCode());
            }
            return bSidurTafkid;
        }
        private bool IsSidurRetzifut(int iSidurNum)
        {
            return ((iSidurNum == SIDUR_RETIZVUT99500) || (iSidurNum == SIDUR_RETIZVUT99501));

        }

        public bool IsLongEilatTrip(DateTime dTaarich, out clPeilut oPeilutEilat, float fPaar)
        {
            clPeilut peilutEilat = this.htPeilut.Values.Cast<clPeilut>().ToList().FirstOrDefault(peilut => peilut.bPeilutEilat); //&& peilut.fKm > fPaar
            oPeilutEilat = peilutEilat;
            if (!string.IsNullOrEmpty(fPaar.ToString())
                && peilutEilat != null)
            {
                return true;
            }

            return false;
        }

        private DataTable GetSidurRagilDetails(DateTime dCardDate, int iMisparSidur, int iShayahLeyomKodem, out int iResult)
        {            
            //סידורים רגילים
            //נבדוק מול התנועה
            string sCacheKey = iMisparSidur + dCardDate.ToShortDateString();
            DataTable dsSidurim = new DataTable();
            clKavim oKavim = new clKavim();
            DateTime dSidurDate = dCardDate;
            //if (iShayahLeyomKodem == 1)
            //{   //אם הסידור שייך מבחינת תכנון ליום הקודם (בודקת האם בשדה Shayah_LeYom_Kodem בטבלת "סידורים עובדים" קיים ערך 1) . אם כן, יש לפנות  לממשק עם תאריך של יום קודם.
            //    dSidurDate = dSidurDate.AddDays(-1);
            //}
            iResult = 0;
            try
            {
                dsSidurim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
            }
            catch (Exception ex)
            {
                dsSidurim = null;
            }
            if (dsSidurim == null)
            {
                dsSidurim = oKavim.GetSidurDetailsFromTnua(iMisparSidur, dSidurDate, out iResult);
                HttpRuntime.Cache.Insert(sCacheKey, dsSidurim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
            }
            
            return dsSidurim;
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


        public double CalculatePremya( OrderedDictionary htPeilut,out double dElementsHamtanaReshut)
        {
            double dSumMazanTashlum = 0;
            double dSumMazanElementim = 0;
            double dTempPremia = 0;
            int iTypeMakat;
            clPeilut oPeilut;
            dElementsHamtanaReshut = 0;
            try
            {
                for (int i = 0; i < htPeilut.Values.Count; i++)
                {
                    oPeilut = ((clPeilut)htPeilut[i]);
                    iTypeMakat = oPeilut.iMakatType;
                    if (oPeilut.iMisparKnisa==0 && (iTypeMakat == clKavim.enMakatType.mKavShirut.GetHashCode() || iTypeMakat == clKavim.enMakatType.mEmpty.GetHashCode() || iTypeMakat == clKavim.enMakatType.mNamak.GetHashCode()))
                    {
                         dSumMazanTashlum += oPeilut.iMazanTashlum;
                    }
                    else if (iTypeMakat == clKavim.enMakatType.mElement.GetHashCode())
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
 

    public clSidur()
    {

    }

    public clSidur(DataRow dr)
    {
        iMisparIshi = int.Parse(dr["Mispar_Ishi"].ToString());
        iMisparSidur = (System.Convert.IsDBNull(dr["Mispar_Sidur"]) ? 0 : int.Parse(dr["Mispar_Sidur"].ToString()));
        sShatGmar = (System.Convert.IsDBNull(dr["Shat_gmar"]) ? "" : DateTime.Parse(dr["Shat_gmar"].ToString()).ToString("HH:mm"));
        if (String.IsNullOrEmpty(sShatGmar))
        {
            dFullShatGmar = DateTime.MinValue;
            dOldFullShatGmar = dFullShatGmar; 
        }
        else
        {
            dFullShatGmar = DateTime.Parse(dr["Shat_gmar"].ToString());
            dOldFullShatGmar = dFullShatGmar; 
        }
    
        dFullShatHatchala = DateTime.Parse(dr["Shat_Hatchala"].ToString());
        sShatHatchala = (dFullShatHatchala.Year < clGeneral.cYearNull ? "" : DateTime.Parse(dr["Shat_Hatchala"].ToString()).ToString("HH:mm"));
   
        sShatHatchalaLetashlum = (System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? "" : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString()).ToString("HH:mm"));

        dFullShatHatchalaLetashlum = System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString());
        if (dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
            sShatHatchalaLetashlum = "";

        sShatGmarLetashlum = (System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? "" : DateTime.Parse(dr["shat_gmar_letashlum"].ToString()).ToString("HH:mm"));

        dFullShatGmarLetashlum = System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum"].ToString());
        dOldFullShatGmarLetashlum =dFullShatGmarLetashlum;
        dSidurDate = (DateTime)dr["taarich"];
        
        sVisa = dr["yom_visa"].ToString();
        sChariga = dr["Chariga"].ToString();
        sOldChariga = sChariga;
        sPitzulHafsaka = dr["Pitzul_Hafsaka"].ToString();
        sOldPitzulHafsaka = sPitzulHafsaka;
        sOutMichsa = dr["out_michsa"].ToString();
        sOldOutMichsa = sOutMichsa;
        sHashlama = dr["hashlama"].ToString();
        sOldHashlama = sHashlama;
        iSugHashlama = (System.Convert.IsDBNull(dr["sug_hashlama"]) ? 0 : int.Parse(dr["sug_hashlama"].ToString()));
        bHashlamaExists = !(String.IsNullOrEmpty(dr["Hashlama"].ToString()));
        iLoLetashlum = (System.Convert.IsDBNull(dr["Lo_Letashlum"]) ? 0 : int.Parse(dr["Lo_Letashlum"].ToString()));
        iOldLoLetashlum = iLoLetashlum;
        iShayahLeyomKodem = (System.Convert.IsDBNull(dr["shayah_leyom_kodem"]) ? 0 : int.Parse(dr["shayah_leyom_kodem"].ToString()));
        iMisparShiureyNehiga = (System.Convert.IsDBNull(dr["mispar_shiurey_nehiga"]) ? 0 : int.Parse(dr["mispar_shiurey_nehiga"].ToString()));
      
        sMikumShaonKnisa = dr["mikum_shaon_knisa"].ToString();
        sMikumShaonYetzia = dr["mikum_shaon_yetzia"].ToString();
       
        iKodSibaLedivuchYadaniIn = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_in"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_in"].ToString());
        iOldKodSibaLedivuchYadaniIn = iKodSibaLedivuchYadaniIn;

        iKodSibaLedivuchYadaniOut = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_out"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_out"].ToString());
        iOldKodSibaLedivuchYadaniOut = iKodSibaLedivuchYadaniOut;

        iKodSibaLoLetashlum = System.Convert.IsDBNull(dr["kod_siba_lo_letashlum"]) ? 0 : int.Parse(dr["kod_siba_lo_letashlum"].ToString());
        iMezakeNesiot = System.Convert.IsDBNull(dr["mezake_nesiot"]) ? 0 : int.Parse(dr["mezake_nesiot"].ToString());
        iMezakeHalbasha = System.Convert.IsDBNull(dr["mezake_halbasha"]) ? 0 : int.Parse(dr["mezake_halbasha"].ToString());
        
        lMeadkenAcharon = System.Convert.IsDBNull(dr["meadken_acharon"]) ? 0 : int.Parse(dr["meadken_acharon"].ToString());
        iBitulOHosafa = System.Convert.IsDBNull(dr["bitul_o_hosafa"]) ? 0 : int.Parse(dr["bitul_o_hosafa"].ToString());
        sHeara = dr["Heara"].ToString();
        iNidreshetHitiatzvut = System.Convert.IsDBNull(dr["nidreshet_hitiatzvut"]) ? 0 : int.Parse(dr["nidreshet_hitiatzvut"].ToString());
        dShatHitiatzvut = System.Convert.IsDBNull(dr["shat_hitiatzvut"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hitiatzvut"].ToString());
        iPtorMehitiatzvut = System.Convert.IsDBNull(dr["ptor_mehitiatzvut"]) ? 0 : int.Parse(dr["ptor_mehitiatzvut"].ToString());
        dTaarichIdkunAcharon = System.Convert.IsDBNull(dr["taarich_idkun_acharon"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon"].ToString());
        iHachtamaBeatarLoTakin = System.Convert.IsDBNull(dr["Hachtama_Beatar_Lo_Takin"]) ? 0 : int.Parse(dr["Hachtama_Beatar_Lo_Takin"].ToString());
           
           
    }

    public clSidur(clSidur oSidurKodem, DateTime dTaarich, int iMisparSidurNew, DataRow dr)
      {
            int iResult;
            DataTable dsSidurim;

            //נתונים ברמת סידור            
            iMisparIshi = oSidurKodem.iMisparIshi;
            iMisparSidur = iMisparSidurNew;

            sShatGmar = oSidurKodem.dFullShatGmar.ToString("HH:mm");
            dFullShatGmar = oSidurKodem.dFullShatGmar;
            dOldFullShatGmar = dFullShatGmar; 

            dFullShatHatchala = oSidurKodem.dFullShatHatchala;
            sShatHatchala = oSidurKodem.dFullShatHatchala.ToString("HH:mm");
            sShatHatchalaLetashlum = oSidurKodem.sShatHatchalaLetashlum;
            dFullShatHatchalaLetashlum = oSidurKodem.dFullShatHatchalaLetashlum;
            dOldFullShatHatchalaLetashlum = dFullShatHatchalaLetashlum;  
            sShatGmarLetashlum = oSidurKodem.dFullShatGmarLetashlum.ToString("HH:mm");
            dFullShatGmarLetashlum = oSidurKodem.dFullShatGmarLetashlum;
            dOldFullShatGmarLetashlum = dFullShatGmarLetashlum;
            dSidurDate = dTaarich;
            sSidurDay = (dTaarich.GetHashCode() + 1).ToString();

            sVisa = oSidurKodem.sVisa;
            sChariga = oSidurKodem.sChariga;
            sOldChariga = sChariga;
            sPitzulHafsaka = oSidurKodem.sPitzulHafsaka;
            sOldPitzulHafsaka = sPitzulHafsaka;
            iZakayLepizul = oSidurKodem.iZakayLepizul;
            sOutMichsa = oSidurKodem.sOutMichsa;
            sOldOutMichsa = sOutMichsa;
            sHashlama = oSidurKodem.sHashlama;
            sOldHashlama = sHashlama;
            bHashlamaExists = oSidurKodem.bHashlamaExists;
            iLoLetashlum = oSidurKodem.iLoLetashlum;
            iOldLoLetashlum = iLoLetashlum;
            iShayahLeyomKodem = oSidurKodem.iShayahLeyomKodem;
            iMisparShiureyNehiga =oSidurKodem.iMisparShiureyNehiga;
            iSugYom = oSidurKodem.iSugYom;
            sShabaton = oSidurKodem.sShabaton;
            sErevShishiChag = oSidurKodem.sErevShishiChag;
            sHeara = oSidurKodem.sHeara;
            dTaarichIdkunAcharon = oSidurKodem.dTaarichIdkunAcharon;
            lMeadkenAcharon = oSidurKodem.lMeadkenAcharon;
            iBitulOHosafa = oSidurKodem.iBitulOHosafa;
            iNidreshetHitiatzvut = oSidurKodem.iNidreshetHitiatzvut;
            iHachtamaBeatarLoTakin = oSidurKodem.iHachtamaBeatarLoTakin;
            dShatHitiatzvut = oSidurKodem.dShatHitiatzvut;
            iPtorMehitiatzvut = oSidurKodem.iPtorMehitiatzvut;
            iSectorVisa = oSidurKodem.iSectorVisa;
            iMivtzaVisa = oSidurKodem.iMivtzaVisa;
            bSectorVisaExists = oSidurKodem.bSectorVisaExists;
            iKodSibaLedivuchYadaniIn = oSidurKodem.iKodSibaLedivuchYadaniIn;
            iKodSibaLedivuchYadaniOut = oSidurKodem.iKodSibaLedivuchYadaniOut;
            iMezakeNesiot = oSidurKodem.iMezakeNesiot;
            iMezakeHalbasha = oSidurKodem.iMezakeHalbasha;
            iKodSibaLoLetashlum = oSidurKodem.iKodSibaLoLetashlum;
            iAchuzKnasLepremyatVisa = oSidurKodem.iAchuzKnasLepremyatVisa;
            iAchuzVizaBesikun = oSidurKodem.iAchuzVizaBesikun;
            iTosefetGrira = oSidurKodem.iTosefetGrira;
            iMisparMusachOMachsan = oSidurKodem.iMisparMusachOMachsan;
            iSugHazmanatVisa = oSidurKodem.iSugHazmanatVisa;
            iSidurLoNibdakSofShavua = oSidurKodem.iSidurLoNibdakSofShavua;

            bSidurMyuhad = IsSidurMyuhad(iMisparSidur.ToString());

            if (bSidurMyuhad)
            {
                iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));
            
                sSectorAvoda = dr["sector_avoda"].ToString();
                bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
                sHalbashKod = dr["Halbash_Kod"].ToString();
                bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
                sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
                bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
                sShatGmarMuteret = dr["shat_gmar_muteret"].ToString();
                bShatGmarMuteretExists = !(String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()));
                sNoPeilotKod = dr["No_Peilot_Kod"].ToString();
                bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
                sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
                bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
                sZakaiLehamara = dr["zakay_lehamara"].ToString();
                bZakaiLehamaraExists = !(String.IsNullOrEmpty(dr["zakay_lehamara"].ToString()));
                sZakaiLeChariga = dr["zakay_lechariga"].ToString();
                bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
                sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
                bZakayLezamanNesiaExists = !(String.IsNullOrEmpty(dr["zakay_leaman_nesia"].ToString()));
                sSidurInSummer = dr["sidur_in_summer"].ToString();
                bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
                sNoOtoNo = dr["no_oto_no"].ToString();
                bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
                sHashlamaKod = dr["hashlama_kod"].ToString();               
                sShaonNochachut = dr["shaon_nochachut"].ToString();
                sLoLetashlumAutomati = dr["lo_letashlum_automati"].ToString();
                bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
                sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
                bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));
                sSugAvoda = dr["sug_avoda"].ToString();
                bSugAvodaExists = !(String.IsNullOrEmpty(dr["sug_avoda"].ToString()));
               sPeilutRequiredKod = dr["peilut_required_kod"].ToString();
                bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
                sSidurNotValidKod = dr["sidur_not_valid_kod"].ToString();
                bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));
                sSidurNotInShabtonKod = dr["sidur_not_in_shabton_kod"].ToString();
                bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
                iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());
                sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();
                sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
                sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
                bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
                sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
                bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString())); 
           
                bSidurRetizfut = IsSidurRetzifut(iMisparSidur);
                bSidurNahagut = IsSidurNahagut();
                if (!bSidurNahagut)
                { bSidurTafkid = IsSidurTafkid(); }
            
            }
            //לכל סידור רגיל נפנה לתנועה לקבלת פרטים נוספים על הסידור
            // נקבל את סוג הסידור ואם הוא קיים במפת התכנון
            bSidurRagilExists = false;
            if (!bSidurMyuhad)
            {
                dsSidurim = GetSidurRagilDetails(dSidurDate, iMisparSidur, iShayahLeyomKodem, out iResult);
                if (iResult == 0)
                {
                    //if (dsSidurim.Tables[0].Rows.Count > 0)
                    if (dsSidurim.Rows.Count > 0)
                    {
                        bSidurRagilExists = true;
                        //iSugSidurRagil = int.Parse(dsSidurim.Tables[0].Rows[0]["SUG_SIDUR"].ToString());
                        iSugSidurRagil = int.Parse(dsSidurim.Rows[0]["SUGSIDUR"].ToString());
                        if (dsSidurim.Rows[0]["PREMIUM"].ToString() != "")
                             iPremium = int.Parse(dsSidurim.Rows[0]["PREMIUM"].ToString());
                    }
                }
            }

            htPeilut = oSidurKodem.htPeilut;
        }


    public clSidur(int iMispar_Ishi,DateTime dTaarich, int iMisparSidurNew, DataRow dr)
    {
        int iResult;
        DataTable dsSidurim;

        //נתונים ברמת סידור            
        iMisparIshi = iMispar_Ishi;
        iMisparSidur = iMisparSidurNew;

        dSidurDate = dTaarich;
        sSidurDay = (dTaarich.GetHashCode() + 1).ToString();
        sPitzulHafsaka = "0";
        sOldPitzulHafsaka = "0";
        bSidurMyuhad = IsSidurMyuhad(iMisparSidur.ToString());

        if (bSidurMyuhad)
        {
            iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));
            
            sSectorAvoda = dr["sector_avoda"].ToString();
            iSidurLoNibdakSofShavua = (System.Convert.IsDBNull(dr["sidur_lo_nivdak_sofash"]) ? 0 : int.Parse(dr["sidur_lo_nivdak_sofash"].ToString()));
            
            bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
            sHalbashKod = dr["Halbash_Kod"].ToString();
            bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
            sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
            bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
            sShatGmarMuteret = dr["shat_gmar_muteret"].ToString();
            bShatGmarMuteretExists = !(String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()));
            sNoPeilotKod = dr["No_Peilot_Kod"].ToString();
            bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
            sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
            bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
            sZakaiLehamara = dr["zakay_lehamara"].ToString();
            bZakaiLehamaraExists = !(String.IsNullOrEmpty(dr["zakay_lehamara"].ToString()));
            sZakaiLeChariga = dr["zakay_lechariga"].ToString();
            bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
            sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
            bZakayLezamanNesiaExists = !(String.IsNullOrEmpty(dr["zakay_leaman_nesia"].ToString()));
            sSidurInSummer = dr["sidur_in_summer"].ToString();
            bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
            sNoOtoNo = dr["no_oto_no"].ToString();
            bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
            sHashlamaKod = dr["hashlama_kod"].ToString();
            
            sShaonNochachut = dr["shaon_nochachut"].ToString();
            sLoLetashlumAutomati = dr["lo_letashlum_automati"].ToString();
            bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
            sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
            bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));
            sSugAvoda = dr["sug_avoda"].ToString();
            bSugAvodaExists = !(String.IsNullOrEmpty(dr["sug_avoda"].ToString()));
            sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();

            sPeilutRequiredKod = dr["peilut_required_kod"].ToString();
            bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
            sSidurNotValidKod = dr["sidur_not_valid_kod"].ToString();
            bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));
            sSidurNotInShabtonKod = dr["sidur_not_in_shabton_kod"].ToString();
            bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
            iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());
            
            sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
               sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
            bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
            sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
            bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString())); 
           
                bSidurRetizfut = IsSidurRetzifut(iMisparSidur);
                bSidurNahagut = IsSidurNahagut();
                if (!bSidurNahagut)
                { bSidurTafkid = IsSidurTafkid(); }
        
        }
        //לכל סידור רגיל נפנה לתנועה לקבלת פרטים נוספים על הסידור
        // נקבל את סוג הסידור ואם הוא קיים במפת התכנון
        bSidurRagilExists = false;
        if (!bSidurMyuhad)
        {
            dsSidurim = GetSidurRagilDetails(dSidurDate, iMisparSidur, iShayahLeyomKodem, out iResult);
            if (iResult == 0)
            {
                //if (dsSidurim.Tables[0].Rows.Count > 0)
                if (dsSidurim.Rows.Count > 0)
                {
                    bSidurRagilExists = true;
                    //iSugSidurRagil = int.Parse(dsSidurim.Tables[0].Rows[0]["SUG_SIDUR"].ToString());
                    iSugSidurRagil = int.Parse(dsSidurim.Rows[0]["SUGSIDUR"].ToString());
                    if (dsSidurim.Rows[0]["PREMIUM"].ToString() != "")
                        iPremium = int.Parse(dsSidurim.Rows[0]["PREMIUM"].ToString());
                }
            }
        }
    }

}
}


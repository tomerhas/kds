﻿using System;
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

namespace KdsBatch.Entities
{
    public class Sidur
    {
        public int iMisparIshi;
        public int iMisparSidur;
        public int iMisparSidurMyuhad;
        public string sShatGmar;
        public DateTime dFullShatHatchala;
        public DateTime dFullShatGmar;
        public string sShatHatchala;
        public DateTime dSidurDate;
        public string sSidurDay = "";
        public string sShatHatchalaLetashlum;
        public String sShatGmarLetashlum;
        public DateTime dFullShatHatchalaLetashlum;
        public DateTime dFullShatGmarLetashlum;
        public string sVisa;
        public string sChariga;
        public string sPitzulHafsaka;
        public string sOutMichsa;
        public string sHashlama;
        public int iSugHashlama;
        public int iMivtzaVisa;
        public int iTafkidVisa;
        public string sLidroshKodMivtza;
        public bool bHashlamaExists;
        public int iLoLetashlum;
        public int iMisparShiureyNehiga;
        public string sShatHatchalaMuteret;
        public bool bShatHatchalaMuteretExists;
        public string sShatGmarMuteret;
        public string sHalbashKod;//מאפיין 15
        public bool bHalbashKodExists;
        public bool bNoPeilotKodExists;
        public string sSidurVisaKod;
        public bool bSidurVisaKodExists;
        public string sSectorAvoda;//-5העדרות, -1נהגות,תפקיד
        public int iSectorVisa;
        public bool bSectorVisaExists;
        public string sZakaiLehamara;
        public string sZakaiLeChariga;
        public bool bZakaiLeCharigaExists;
        public string sZakayLezamanNesia;
        public string sHashlamaKod;
        public bool bHashlamaKodExists;
        public string sShaonNochachut;
        public bool bLoLetashlumAutomatiExists;
        public string sSidurInSummer;
        public bool bSidurInSummerExists;
        public string sNoOtoNo;
        public bool bNoOtoNoExists;
        public string sHeadrutTypeKod; //מאפיין 53
        public bool bHeadrutTypeKodExists; //מאפיין 53
        public string sSugAvoda;//מאפיין 52
        public string sMikumShaonKnisa;
        public string sMikumShaonYetzia;
        public bool bPeilutRequiredKodExists;
        public bool bSidurNotValidKodExists;
        public bool bSidurNotInShabtonKodExists;

        public int iNitanLedaveachBemachalaAruca;
        public int iShayahLeyomKodem;
        public bool bSidurMyuhad;
        public bool bSidurEilat;
        public bool bSidurNotEmpty;
        public bool bSidurNahagut;
        public bool bSidurTafkid = false;
        public int iSugSidurRagil;
        public int iKodSibaLedivuchYadaniIn;
        public int iKodSibaLedivuchYadaniOut;
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
        public int iMisparMusachOMachsan;
        public int iSugHazmanatVisa;
        public bool bHovaMisparMachsan;
        public string sHeara;
        public DateTime dTaarichIdkunAcharon;

        public string sShabaton = "";
        public string sErevShishiChag = "";

        public string sBitulZmanNesiot;
        public int iNidreshetHitiatzvut;
        public int iHachtamaBeatarLoTakin;
        public DateTime dShatHitiatzvut;
        public int iPtorMehitiatzvut;
        public int iMezakeHalbasha;
        public int iLebdikaShguim;
        public string sZakayMichutzLamichsa; //מאפיין 25
        private const int SIDUR_RETIZVUT99500 = 99500;
        private const int SIDUR_RETIZVUT99501 = 99501;

        //נתוני פעילות
        public List<Peilut> Peiluyot; // = new List<Peilut>();
        public Peilut oPeilutEilat;
        //costructors
        public Sidur() {}

        public Sidur(DataRow dr)
        {
            iMisparIshi = int.Parse(dr["Mispar_Ishi"].ToString());
            iMisparSidur = (System.Convert.IsDBNull(dr["Mispar_Sidur"]) ? 0 : int.Parse(dr["Mispar_Sidur"].ToString()));
            sShatGmar = (System.Convert.IsDBNull(dr["Shat_gmar"]) ? "" : DateTime.Parse(dr["Shat_gmar"].ToString()).ToString("HH:mm"));
            if (String.IsNullOrEmpty(sShatGmar))
            {
                dFullShatGmar = DateTime.MinValue;
            }
            else
            {
                dFullShatGmar = DateTime.Parse(dr["Shat_gmar"].ToString());
            }
    
            dFullShatHatchala = DateTime.Parse(dr["Shat_Hatchala"].ToString());
            sShatHatchala = (dFullShatHatchala.Year < clGeneral.cYearNull ? "" : DateTime.Parse(dr["Shat_Hatchala"].ToString()).ToString("HH:mm"));
   
            sShatHatchalaLetashlum = (System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? "" : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString()).ToString("HH:mm"));

            dFullShatHatchalaLetashlum = System.Convert.IsDBNull(dr["shat_hatchala_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala_letashlum"].ToString());
            if (dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
                sShatHatchalaLetashlum = "";

            sShatGmarLetashlum = (System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? "" : DateTime.Parse(dr["shat_gmar_letashlum"].ToString()).ToString("HH:mm"));

            dFullShatGmarLetashlum = System.Convert.IsDBNull(dr["shat_gmar_letashlum"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_gmar_letashlum"].ToString());
            dSidurDate = (DateTime)dr["taarich"];
        
            sVisa = dr["yom_visa"].ToString();
            sChariga = dr["Chariga"].ToString();
            sPitzulHafsaka = dr["Pitzul_Hafsaka"].ToString();
            sOutMichsa = dr["out_michsa"].ToString();
            sHashlama = dr["hashlama"].ToString();
            iSugHashlama = (System.Convert.IsDBNull(dr["sug_hashlama"]) ? 0 : int.Parse(dr["sug_hashlama"].ToString()));
            bHashlamaExists = !(String.IsNullOrEmpty(dr["Hashlama"].ToString()));
            iLoLetashlum = (System.Convert.IsDBNull(dr["Lo_Letashlum"]) ? 0 : int.Parse(dr["Lo_Letashlum"].ToString()));
            iShayahLeyomKodem = (System.Convert.IsDBNull(dr["shayah_leyom_kodem"]) ? 0 : int.Parse(dr["shayah_leyom_kodem"].ToString()));
            iMisparShiureyNehiga = (System.Convert.IsDBNull(dr["mispar_shiurey_nehiga"]) ? 0 : int.Parse(dr["mispar_shiurey_nehiga"].ToString()));
      
            sMikumShaonKnisa = dr["mikum_shaon_knisa"].ToString();
            sMikumShaonYetzia = dr["mikum_shaon_yetzia"].ToString();
       
            iKodSibaLedivuchYadaniIn = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_in"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_in"].ToString());

            iKodSibaLedivuchYadaniOut = System.Convert.IsDBNull(dr["kod_siba_ledivuch_yadani_out"]) ? 0 : int.Parse(dr["kod_siba_ledivuch_yadani_out"].ToString());

            iKodSibaLoLetashlum = System.Convert.IsDBNull(dr["kod_siba_lo_letashlum"]) ? 0 : int.Parse(dr["kod_siba_lo_letashlum"].ToString());
            iMezakeNesiot = System.Convert.IsDBNull(dr["mezake_nesiot"]) ? 0 : int.Parse(dr["mezake_nesiot"].ToString());
            iMezakeHalbasha = System.Convert.IsDBNull(dr["mezake_halbasha"]) ? 0 : int.Parse(dr["mezake_halbasha"].ToString());
        
            iBitulOHosafa = System.Convert.IsDBNull(dr["bitul_o_hosafa"]) ? 0 : int.Parse(dr["bitul_o_hosafa"].ToString());
            sHeara = dr["Heara"].ToString();
            iNidreshetHitiatzvut = System.Convert.IsDBNull(dr["nidreshet_hitiatzvut"]) ? 0 : int.Parse(dr["nidreshet_hitiatzvut"].ToString());
            dShatHitiatzvut = System.Convert.IsDBNull(dr["shat_hitiatzvut"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_hitiatzvut"].ToString());
            iPtorMehitiatzvut = System.Convert.IsDBNull(dr["ptor_mehitiatzvut"]) ? 0 : int.Parse(dr["ptor_mehitiatzvut"].ToString());
            dTaarichIdkunAcharon = System.Convert.IsDBNull(dr["taarich_idkun_acharon"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon"].ToString());
            iHachtamaBeatarLoTakin = System.Convert.IsDBNull(dr["Hachtama_Beatar_Lo_Takin"]) ? 0 : int.Parse(dr["Hachtama_Beatar_Lo_Takin"].ToString());

        }

        public Sidur(Sidur oSidurKodem, DateTime dTaarich, int iMisparSidurNew, DataRow dr)
        {
            //נתונים ברמת סידור            
            iMisparIshi = oSidurKodem.iMisparIshi;
            iMisparSidur = iMisparSidurNew;

            sShatGmar = oSidurKodem.dFullShatGmar.ToString("HH:mm");
            dFullShatGmar = oSidurKodem.dFullShatGmar;

            dFullShatHatchala = oSidurKodem.dFullShatHatchala;
            sShatHatchala = oSidurKodem.dFullShatHatchala.ToString("HH:mm");
            sShatHatchalaLetashlum = oSidurKodem.sShatHatchalaLetashlum;
            dFullShatHatchalaLetashlum = oSidurKodem.dFullShatHatchalaLetashlum;
            sShatGmarLetashlum = oSidurKodem.dFullShatGmarLetashlum.ToString("HH:mm");
            dFullShatGmarLetashlum = oSidurKodem.dFullShatGmarLetashlum;
            dSidurDate = dTaarich;
            sSidurDay = (dTaarich.GetHashCode() + 1).ToString();

            sVisa = oSidurKodem.sVisa;
            sChariga = oSidurKodem.sChariga;
            sPitzulHafsaka = oSidurKodem.sPitzulHafsaka;
            sOutMichsa = oSidurKodem.sOutMichsa;
            sHashlama = oSidurKodem.sHashlama;
            bHashlamaExists = oSidurKodem.bHashlamaExists;
            iLoLetashlum = oSidurKodem.iLoLetashlum;
            iShayahLeyomKodem = oSidurKodem.iShayahLeyomKodem;
            iMisparShiureyNehiga =oSidurKodem.iMisparShiureyNehiga;
            sShabaton = oSidurKodem.sShabaton;
            sErevShishiChag = oSidurKodem.sErevShishiChag;
            sHeara = oSidurKodem.sHeara;
            dTaarichIdkunAcharon = oSidurKodem.dTaarichIdkunAcharon;
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

            bSidurMyuhad = iMisparSidur.ToString().Substring(0, 2) == "99" ? true : false;

            if (bSidurMyuhad)
            {
                iMisparSidurMyuhad = (System.Convert.IsDBNull(dr["mispar_sidur_myuhad"]) ? 0 : int.Parse(dr["mispar_sidur_myuhad"].ToString()));
            
                sSectorAvoda = dr["sector_avoda"].ToString();
                //  bSectorAvodaExists = !String.IsNullOrEmpty(dr["sector_avoda"].ToString());
                sHalbashKod = dr["Halbash_Kod"].ToString();
                bHalbashKodExists = !(String.IsNullOrEmpty(dr["Halbash_Kod"].ToString()));
                sShatHatchalaMuteret = dr["shat_hatchala_muteret"].ToString();
                bShatHatchalaMuteretExists = !(String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()));
                sShatGmarMuteret = dr["shat_gmar_muteret"].ToString(); 
                bNoPeilotKodExists = !(String.IsNullOrEmpty(dr["No_Peilot_Kod"].ToString()));
                sSidurVisaKod = dr["Sidur_Visa_kod"].ToString();
                bSidurVisaKodExists = !(String.IsNullOrEmpty(dr["Sidur_Visa_kod"].ToString()));
                sZakaiLehamara = dr["zakay_lehamara"].ToString();
                sZakaiLeChariga = dr["zakay_lechariga"].ToString();
                bZakaiLeCharigaExists = !(String.IsNullOrEmpty(dr["zakay_lechariga"].ToString()));
                sZakayLezamanNesia = dr["Zakay_Leaman_Nesia"].ToString();
                sSidurInSummer = dr["sidur_in_summer"].ToString();
                bSidurInSummerExists = !(String.IsNullOrEmpty(dr["sidur_in_summer"].ToString()));
                sNoOtoNo = dr["no_oto_no"].ToString();
                bNoOtoNoExists = !(String.IsNullOrEmpty(dr["no_oto_no"].ToString()));
                sHashlamaKod = dr["hashlama_kod"].ToString();               
                sShaonNochachut = dr["shaon_nochachut"].ToString();
                bLoLetashlumAutomatiExists = !(String.IsNullOrEmpty(dr["lo_letashlum_automati"].ToString()));
                sHeadrutTypeKod = dr["headrut_type_kod"].ToString();
                bHeadrutTypeKodExists = !(String.IsNullOrEmpty(dr["headrut_type_kod"].ToString()));
                sSugAvoda = dr["sug_avoda"].ToString();
                bPeilutRequiredKodExists = !(String.IsNullOrEmpty(dr["peilut_required_kod"].ToString()));
                bSidurNotValidKodExists = !(String.IsNullOrEmpty(dr["sidur_not_valid_kod"].ToString()));
                bSidurNotInShabtonKodExists = !(String.IsNullOrEmpty(dr["sidur_not_in_shabton_kod"].ToString()));
                iNitanLedaveachBemachalaAruca = System.Convert.IsDBNull(dr["nitan_ledaveach_bmachala_aruc"]) ? 0 : int.Parse(dr["nitan_ledaveach_bmachala_aruc"].ToString());
                sLidroshKodMivtza = dr["lidrosh_kod_mivtza"].ToString();
                sZakayMichutzLamichsa = dr["zakay_michutz_lamichsa"].ToString(); //מאפיין 25
                sKizuzAlPiHatchalaGmar = dr["kizuz_al_pi_hatchala_gmar"].ToString();
                bKizuzAlPiHatchalaGmarExists = !(String.IsNullOrEmpty(dr["kizuz_al_pi_hatchala_gmar"].ToString()));
                sHovatHityatzvut = dr["hovat_hityazvut"].ToString();
                bHovaMisparMachsan = !(String.IsNullOrEmpty(dr["hova_ledaveach_mispar_machsan"].ToString()));

                bSidurNahagut = IsSidurNahagut();
                if (!bSidurNahagut)
                { bSidurTafkid = IsSidurTafkid(); }

            }
            else if (dr["SUG_SIDUR"] != null &&  dr["SUG_SIDUR"].ToString() !="")
                    iSugSidurRagil = int.Parse(dr["SUG_SIDUR"].ToString());
       
             Peiluyot = oSidurKodem.Peiluyot;
        }

        //global functions - sudur level
        public double CalculatePremya(out double dElementsHamtanaReshut)
        {
            double dSumMazanTashlum = 0;
            double dSumMazanElementim = 0;
            double dTempPremia = 0;
            int iTypeMakat;
            Peilut oPeilut;
            dElementsHamtanaReshut = 0;
            try
            {
                for (int i = 0; i < Peiluyot.Count; i++)
                {
                    oPeilut = Peiluyot[i];
                    iTypeMakat = oPeilut.iMakatType;
                    if (oPeilut.iMisparKnisa == 0 && (iTypeMakat == clKavim.enMakatType.mKavShirut.GetHashCode() || iTypeMakat == clKavim.enMakatType.mEmpty.GetHashCode() || iTypeMakat == clKavim.enMakatType.mNamak.GetHashCode()))
                    {
                        dSumMazanTashlum += oPeilut.iMazanTashlum;
                    }
                    else if (iTypeMakat == clKavim.enMakatType.mElement.GetHashCode())
                    {
                        if (oPeilut.sElementInMinutes == "1" && oPeilut.sKodLechishuvPremia.Trim() == "1:1")
                        {
                            dSumMazanElementim += Int32.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));

                        }
                        if (oPeilut.bElementLershutExists )
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

        private bool IsSidurNahagut()
        {
            //מחזיר אם סידור הוא מסוג נהגות
            bool bSidurNahagut = false;

            if (!String.IsNullOrEmpty(sSectorAvoda))
            {
                bSidurNahagut = (int.Parse(sSectorAvoda) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
            }
            return bSidurNahagut;
        }

        private bool IsSidurTafkid()
        {
            //מחזיר אם סידור הוא מסוג תפקיד
            bool bSidurTafkid = false;

            if (!String.IsNullOrEmpty(sSectorAvoda))
            {
                bSidurTafkid = (int.Parse(sSectorAvoda) == clGeneral.enSectorAvoda.Tafkid.GetHashCode());
            }
            return bSidurTafkid;
        }
       
        private bool IsSidurRetzifut(int iSidurNum)
        {
            return ((iSidurNum == SIDUR_RETIZVUT99500) || (iSidurNum == SIDUR_RETIZVUT99501));
        }

        public bool IsLongEilatTrip()
        {
            oPeilutEilat = this.Peiluyot.FirstOrDefault(peilut => peilut.bPeilutEilat); 
            if (!string.IsNullOrEmpty("fPaar")//להוסיף טיפול בפרמטר 149
                && oPeilutEilat != null)
            {
                return true;
            }

            return false;
        }
       }

    
}

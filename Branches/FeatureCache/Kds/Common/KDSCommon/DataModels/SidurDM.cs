using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace KDSCommon.DataModels
{
    public class SidurDM
    {
        //נתוני עובד
        public int iMisparIshi;
        public int iMisparSidur;
        public int iMisparSidurMyuhad;
        public bool bSadotNosafim = false;
        public string sShatGmar;
        public DateTime dFullShatHatchala;
        public DateTime dOldFullShatHatchala;
        public DateTime dFullShatGmar;
        public DateTime dOldFullShatGmar;
        public string sShatHatchala;
        public DateTime dSidurDate;
        public string sSidurDay = "";
        public string sShatHatchalaLetashlum;
        public String sShatGmarLetashlum;
        public DateTime dFullShatHatchalaLetashlum;
        public DateTime dFullShatGmarLetashlum;
        public DateTime dOldFullShatHatchalaLetashlum;
        public DateTime dOldFullShatGmarLetashlum;
        public string sVisa;
        public string sChariga;
        public string sOldChariga = "0";
        public string sPitzulHafsaka;
        public string sOldPitzulHafsaka = "0";

        public int iZakayLepizul;
        public string sOutMichsa;
        public string sOldOutMichsa = "0";
        public string sHashlama;
        public string sOldHashlama = "0";
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
        public string sHalbashKod;//מאפיין 15
        public bool bHalbashKodExists;
        public string sNoPeilotKod;
        public bool bNoPeilotKodExists;
        public string sSidurVisaKod;
        public bool bSidurVisaKodExists;
        public string sSectorAvoda;//-5העדרות, -1נהגות,תפקיד
        public bool bSectorAvodaExists;
        public int iSectorVisa;
        public bool bSectorVisaExists;
        public int iSidurLoChosem;//מאפיין 90
        public bool bSidurLoChosemExists;
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
        public bool bSidurAsurBeyomShishi;//102
        public int iSidurLebdikatRezefMachala;//103
        public bool bMatalaKlalitLeloRechev;//101
        public int iNitanLedaveachBemachalaAruca;
        public int iShayahLeyomKodem;
        // public bool bSidurRagilExists;
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
        public bool bCancelSidurDisabled;
        public int iZakaiLelina;
        public int iTosefetGrira;
        public int iAchuzKnasLepremyatVisa;
        public int iAchuzVizaBesikun;
        public string sHovatHityatzvut;
        public int iSidurLoNibdakSofShavua;
        //public int iSugVisa;
        public int iMisparMusachOMachsan;
        public int iSugHazmanatVisa;
        public int iMenahelMusachMeadken;
        public bool bHovaMisparMachsan;
        //public int iButal;
        public long lMeadkenAcharon;
        public string sHeara;
        public DateTime dTaarichIdkunAcharon;
        public int iMikumAvKnisa;
        public int iMikumAvYetzia;
        //נתוני עובד
        //public int iLina;
        //public int iHalbasha;
        //public int iBitulZmanNeziot;
        //public char cTachograf;
        public string sShabaton = "";
        public string sErevShishiChag = "";
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
        public DateTime dShatHatchalaMenahelMusach;
        public DateTime dShatGmarMenahelMusach;
        public string sZakayMichutzLamichsa; //מאפיין 25
       // private const int SIDUR_RETIZVUT99500 = 99500;
      //  private const int SIDUR_RETIZVUT99501 = 99501;
        public bool bIsKnisaTkina_err197;
        public bool bIsYetziaTkina_err198;
        //public int iPremium=0;
        public enSidurStatus oSidurStatus;
        public string sTokefHatchala, sTokefSiyum;
        public enum enSidurStatus
        {
            enUpdate,
            enNew,
            enDelete
        }

        public OrderedDictionary htPeilut = new OrderedDictionary();

        public bool IsLongEilatTrip(out PeilutDM oPeilutEilat, float fPaar)
        {
            PeilutDM peilutEilat = htPeilut.Values.Cast<PeilutDM>().ToList().FirstOrDefault(peilut => peilut.bPeilutEilat && peilut.fKm > fPaar);
            oPeilutEilat = peilutEilat;
            if (!string.IsNullOrEmpty(fPaar.ToString())
                && peilutEilat != null)
            {
                return true;
            }

            return false;
        }

    }
}

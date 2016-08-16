using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KDSCommon.DataModels
{
    public class PeilutDM
    {
        public int iKisuyTor = 0;
        public int iOldKisuyTor = 0;
        public int iKisuyTorMap = 0;
        public long lMakatNesia = 0;
        public long lOldMakatNesia = 0;
        public string sShatYetzia = "";
        public DateTime dFullShatYetzia;
        public DateTime dOldFullShatYetzia;
        public long lOtoNo = 0;
        public long lOldOtoNo = 0;
        public long lOldLicenseNumber = 0;
        public long lLicenseNumber = 0;
        public long lMisparSiduriOto;
        public long iElementLeYedia = 0;//לשנות לטקסט?
        public long iErechElement = 0;
        public int iPeilutMisparSidur = 0;
        public string sBusNumberMust = "";
        public bool bBusNumberMustExists;
        public string sElementHamtana = "";
        public bool bElementHamtanaExists;
        public string sElementLershut = "";
        public bool bElementLershutExists;
        public string sElementIgnoreHafifaBetweenNesiot = "";
        public bool bElementIgnoreHafifaBetweenNesiotExists;
        public string sElementIgnoreReka = "";
        public bool bElementIgnoreReka;
        public int iSectorZviraZmanEelement;//מאפיין 14
       // public bool bSectorZviraZmanEelement;
        

        public string sElementZviraZman = "";
        public string sElementInMinutes = "";
        public string sElementNesiaReka;
        public string sBitulBiglalIchurLasidur = "";
        public bool bBitulBiglalIchurLasidurExists;
        public long lMisparVisa = 0;
        public int iMakatType = 0;
        public int iMakatValid;
        public int iXyMokedTchila;
        public int iXyMokedSiyum;
        public int iMisparKnisa = 0;
        //public int iSugKnisa = 0; //נקרא מהקטלוג
        public int iMisparSidurMatalotTnua;
        public bool bMisparSidurMatalotTnuaExists = false;
        public string sDivuchInSidurVisa = "";
        public string sDivuchInSidurMeyuchad = "";
        public bool bPeilutEilat;
        public bool bPeilutNotRekea = true;
        public bool bElementHachanatMechona = false;
        public int iMazanTichnun;
        public int iMazanTashlum;
        public int iBitulOHosafa;
        public int iOnatiyut;
        public int iDakotBafoal; //דקות בפועל
        public int iOldDakotBafoal; //דקות בפועל
        public bool bKnisaNeeded; //כניסה לפי צורך
        public int iKmVisa;
        public float fKm;
        public int iEilatTrip;
        public string sSnifTnua = "";
        public string sLoNitzbarLishatGmar;
        public int iSnifAchrai;
        public DateTime dCardLastUpdate;
        public DateTime dCardDate;
        public string sMakatDescription;
        public string sShilut;
        public string sSugShirutName;
        public long lMisparMatala;
        public bool bImutNetzer;
        private DataTable dtKavim;
        public DataTable dtElementim;
        private String sMakatNesia;
        public String sHeara;
        public String sShilutNetzer;
        public DateTime dShatYetziaNetzer;
        public DateTime dShatBhiratNesiaNetzer;
        public int iMisparSidurNetzer;
        public string sMikumBhiratNesiaNetzer;
        public long lMakatNetzer;
        public long lOtoNoNetzer;
        public int iKodShinuyPremia;
        public string sKodLechishuvPremia;
        public int iElementLeShatGmar;
        public enPeilutStatus oPeilutStatus;
        public string sHamtanaEilat;

        public enum enPeilutStatus
        {
            enUpdate,
            enNew,
            enDelete
        }
    }
}

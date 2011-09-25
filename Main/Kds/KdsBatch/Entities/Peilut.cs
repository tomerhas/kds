using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KdsBatch.Entities
{
    class Peilut
    {
        public int iKisuyTor = 0;
        public int iKisuyTorMap = 0;
        public long lMakatNesia = 0;
        public string sShatYetzia = "";
        public DateTime dFullShatYetzia;
        public long lOtoNo = 0;
        public long lMisparSiduriOto;
        public long iElementLeYedia = 0;//לשנות לטקסט?
        public long iErechElement = 0;
        public int iPeilutMisparSidur = 0;
        public bool bBusNumberMustExists;
        public bool bElementHamtanaExists;
        public string sElementZviraZman = "";
        public string sElementInMinutes = "";
        public string sElementNesiaReka;
        public bool bBitulBiglalIchurLasidurExists;
        public long lMisparVisa = 0;
        public int iMakatType = 0;
        public int iMakatValid;
        public int iXyMokedTchila;
        public int iMisparKnisa = 0;
        public int iMisparSidurMatalotTnua;
        public bool bMisparSidurMatalotTnuaExists = false;
        public string sDivuchInSidurVisa = "";
        public string sDivuchInSidurMeyuchad = "";
        public bool bPeilutEilat;
        public bool bPeilutNotRekea = true;
        public int iMazanTichnun;
        public int iMazanTashlum;
        public int iBitulOHosafa;
        public int iOnatiyut;
        public int iDakotBafoal; //דקות בפועל
        public int iKmVisa;
        public string sSnifTnua = "";
        public string sLoNitzbarLishatGmar;
        public int iSnifAchrai;
        public DateTime dCardDate;
        public string sMakatDescription;
        public string sShilut;
        public long lMisparMatala;
        public bool bImutNetzer;
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

    }
}

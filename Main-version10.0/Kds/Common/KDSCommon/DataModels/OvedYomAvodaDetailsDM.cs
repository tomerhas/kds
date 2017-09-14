using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels
{
    public class OvedYomAvodaDetailsDM
    {
        public string sKodMaamd;
        public string sHalbasha = "";
        public string sHamara = "";
        public string sBitulZmanNesiot;
        public string sTachograf = "";
        public string sKodHaver = "";
        public int iKodHevra;
        public int iGil;
        public int iKodHevraHashala;
        public string sMutamut = "";
        public bool bMutamutExists;
        public string sMercazErua = "";
        public bool bMercazEruaExists;
        public int iZmanMutamut = 0;
        public int iZmanNesiaHaloch;
        public int iZmanNesiaHazor;
        public int iStatus;
        public int iDirug;
        public int iDarga;
        public int iStatusTipul;
        public string sStatusCardDesc;
        public string sDayTypeDesc; //תאור סוג יום
        public int iMeasherOMistayeg; //מאשר או מסתייג, אם התקבל ערך NULL ייכנס -1

        public string sHashlamaLeyom = "";
        public int iSibatHashlamaLeyom;
        public string sLina = "";
        public int iMisparIshi;
        public int iIsuk;
        public int iSnifAv;
        public int iMikumYechida;
        public int iMikumYechidaLenochechut;
        public int iKodSectorIsuk;
        public string sSidurDay;
        public string sShabaton;
        public string sErevShishiChag;
        public string sRishyonAutobus;
        public string sShlilatRishayon;
        public int iSnifTnua;
        public int iBechishuvSachar;
        public DateTime dTaarichMe;
        public DateTime dTaarichAd;
        public bool bOvedDetailsExists = false;
    }
}

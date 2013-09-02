using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;
using System.Configuration;
using System.Web;

namespace KdsBatch
{
    public class clPeilut
    {
        public int iKisuyTor = 0;
        public int iOldKisuyTor  = 0;
        public int iKisuyTorMap = 0;
        public long lMakatNesia = 0;
        public long lOldMakatNesia = 0;
        public string sShatYetzia = "";
        public DateTime dFullShatYetzia;
        public DateTime dOldFullShatYetzia;
        public long lOtoNo = 0;
        public long lOldOtoNo = 0;
        public long lMisparSiduriOto;
        public long iElementLeYedia = 0;//לשנות לטקסט?
        public long iErechElement = 0;
        public int iPeilutMisparSidur = 0;
        public string sBusNumberMust = "";
        public bool bBusNumberMustExists;
        public string sElementHamtana = "";
        public bool bElementHamtanaExists;
        public string sElementLershut= "";
        public bool bElementLershutExists;
        public string sElementIgnoreHafifaBetweenNesiot = "";        
        public bool bElementIgnoreHafifaBetweenNesiotExists;
        public string sElementIgnoreReka = "";        
        public bool bElementIgnoreReka;
       
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
        private DataTable dtElementim;
        private String sMakatNesia;        
        public String sHeara;
        public String sShilutNetzer;
        public DateTime  dShatYetziaNetzer;
        public DateTime  dShatBhiratNesiaNetzer;
        public int iMisparSidurNetzer;
        public string sMikumBhiratNesiaNetzer;
        public long   lMakatNetzer;
        public long  lOtoNoNetzer;
        public int iKodShinuyPremia;
        public string sKodLechishuvPremia;
        public int iElementLeShatGmar;
        public enPeilutStatus oPeilutStatus;
        public string sHamtanaEilat;

        private const string COL_TRIP_EMPTY = "ריקה";
        private const string COL_TRIP_NAMAK = "נמ'ק";
        private const string COL_TRIP_ELEMENT = "אלמנט";
        private const string COL_TRIP_VISUT = "ויסות";
        private const string COL_TRIP_KNISA = "כניסה";
        private const string MAKAT_KNISA = "לפי צורך";


        public enum enPeilutStatus
        {
            enUpdate,
            enNew,             
            enDelete
        }
        public clPeilut(DateTime dCardDate)
        {            
            clUtils oUtils = new clUtils();
            string sCacheKey = "ElementsTable";

            try
            {
                dtElementim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
                if (dtElementim == null)
                {
                    dtElementim = oUtils.GetCtbElementim();
                    HttpRuntime.Cache.Insert(sCacheKey, dtElementim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                }
                //dtElementim = oUtils.GetCtbElementim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public clPeilut(){}

        public clPeilut(int iMisparIshi,DateTime dDateCard,clPeilut oPeilutOld,long lMakatNesiaNew,DataTable dtMeafyeneyElements) 
        {
            DataTable dtPeiluyot;
            clUtils oUtils = new clUtils();
            string sCacheKey = "ElementsTable";
                
            try
            {
                dtElementim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
                if (dtElementim == null)
                {
                    dtElementim = oUtils.GetCtbElementim();
                    HttpRuntime.Cache.Insert(sCacheKey, dtElementim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                }
           //     dtElementim = oUtils.GetCtbElementim();
            lMakatNesia = lMakatNesiaNew;
            dCardDate = dDateCard;
            iPeilutMisparSidur = oPeilutOld.iPeilutMisparSidur;
            iKisuyTor = oPeilutOld.iKisuyTor;
            iOldKisuyTor = iKisuyTor; 
            dFullShatYetzia =oPeilutOld.dFullShatYetzia;
            dOldFullShatYetzia = oPeilutOld.dFullShatYetzia;
            sShatYetzia = oPeilutOld.sShatYetzia;
            
            lOtoNo =oPeilutOld.lOtoNo;
            lOldOtoNo = lOtoNo;

            lMisparSiduriOto =oPeilutOld.lMisparSiduriOto;

            lMisparVisa = oPeilutOld.lMisparVisa;
            iMisparKnisa = oPeilutOld.iMisparKnisa;
            bImutNetzer = oPeilutOld.bImutNetzer;
            iBitulOHosafa = oPeilutOld.iBitulOHosafa;
            iDakotBafoal = oPeilutOld.iDakotBafoal;
            iOldDakotBafoal = iDakotBafoal;
            iKmVisa = oPeilutOld.iKmVisa;
            dCardLastUpdate = oPeilutOld.dCardLastUpdate;
            sSnifTnua = oPeilutOld.sSnifTnua;
            lMisparSiduriOto = oPeilutOld.lMisparSiduriOto;
            lMisparMatala = oPeilutOld.lMisparMatala;

            sHeara=oPeilutOld.sHeara;
            sShilutNetzer=oPeilutOld.sShilutNetzer;
            dShatYetziaNetzer=oPeilutOld.dShatBhiratNesiaNetzer;
            dShatBhiratNesiaNetzer=oPeilutOld.dShatBhiratNesiaNetzer;
            iMisparSidurNetzer=oPeilutOld.iMisparSidurNetzer;
            sMikumBhiratNesiaNetzer = oPeilutOld.sMikumBhiratNesiaNetzer;
            lMakatNetzer=oPeilutOld.lMakatNetzer;
            lOtoNoNetzer=oPeilutOld.lOtoNoNetzer;
            iKodShinuyPremia=oPeilutOld.iKodShinuyPremia;
            sMakatDescription = oPeilutOld.sMakatDescription;

            dtPeiluyot = clDefinitions.GetPeiluyotFromTnua(iMisparIshi, dDateCard);

            SetKavDetails(dtPeiluyot, lMakatNesia);

            if ((clKavim.enMakatType)iMakatType == clKavim.enMakatType.mElement)
            {
                DataRow[] dr = dtMeafyeneyElements.Select("kod_element=" + int.Parse(lMakatNesia.ToString().Substring(1, 2)));
                DataRow drMeafyeneyElements;

                if (dr.Length > 0)
                {
                    drMeafyeneyElements = dr[0];
                    iElementLeYedia = (System.Convert.IsDBNull(drMeafyeneyElements["element_for_yedia"]) ? 0 : int.Parse(drMeafyeneyElements["element_for_yedia"].ToString()));
                    iErechElement = (System.Convert.IsDBNull(drMeafyeneyElements["erech_element"]) ? 0 : int.Parse(drMeafyeneyElements["erech_element"].ToString()));

                    iMisparSidurMatalotTnua = (System.Convert.IsDBNull(drMeafyeneyElements["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString()));
                    bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString());
                    sBusNumberMust = drMeafyeneyElements["bus_number_must"].ToString();
                    bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
                    sElementHamtana = drMeafyeneyElements["element_hamtana"].ToString();
                    bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
                    sElementIgnoreHafifaBetweenNesiot = drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString();
                    bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString());

                    sElementIgnoreReka = drMeafyeneyElements["lehitalem_beitur_reyka"].ToString();
                    bElementIgnoreReka = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_beitur_reyka"].ToString());

                    sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
                    sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
                    sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
                    sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
                    sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();
                    sHamtanaEilat = drMeafyeneyElements["hamtana_eilat"].ToString();
                    
                    sElementLershut = drMeafyeneyElements["Lershut"].ToString();
                    bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
                    iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());
            
                    sBitulBiglalIchurLasidur = drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString();
                    bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString());
                    sDivuchInSidurVisa = drMeafyeneyElements["divuch_in_sidur_visa"].ToString();
                    sDivuchInSidurMeyuchad = drMeafyeneyElements["divuach_besidur_meyuchad"].ToString();
                    
                }
            }
          }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clPeilut(int iMisparIshi, DateTime dDateCard, KdsLibrary.UDT.OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, DataTable dtMeafyeneyElements)
        {
            DataTable dtPeiluyot;
            clUtils oUtils = new clUtils();
            string sCacheKey = "ElementsTable";
           
            try
            {
                dtElementim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
            }
            catch (Exception ex)
            {
                dtElementim = null;
            }

            if (dtElementim == null)
            {
                dtElementim = oUtils.GetCtbElementim();
                HttpRuntime.Cache.Insert(sCacheKey, dtElementim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
            }

            try
            {
               

                lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                lOldMakatNesia = lMakatNesia;
                dCardDate = dDateCard;
                iPeilutMisparSidur = oObjPeilutOvdimIns.MISPAR_SIDUR;
                dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                dOldFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                sShatYetzia = dFullShatYetzia.ToString("HH:mm");

                lMisparVisa = oObjPeilutOvdimIns.MISPAR_VISA;
                iMisparKnisa = oObjPeilutOvdimIns.MISPAR_KNISA;
                lMisparSiduriOto = oObjPeilutOvdimIns.MISPAR_SIDURI_OTO;
                lMisparMatala = oObjPeilutOvdimIns.MISPAR_MATALA;
                lOtoNo = oObjPeilutOvdimIns.OTO_NO;
                lOldOtoNo = lOtoNo;
                dtPeiluyot = clDefinitions.GetPeiluyotFromTnua(iMisparIshi, dDateCard);

                SetKavDetails(dtPeiluyot, lMakatNesia);

                if ((clKavim.enMakatType)iMakatType == clKavim.enMakatType.mElement)
                {
                    DataRow[] dr = dtMeafyeneyElements.Select("kod_element=" + int.Parse(lMakatNesia.ToString().Substring(1, 2)));
                    DataRow drMeafyeneyElements;

                    if (dr.Length > 0)
                    {
                        drMeafyeneyElements = dr[0];
                        iElementLeYedia = (System.Convert.IsDBNull(drMeafyeneyElements["element_for_yedia"]) ? 0 : int.Parse(drMeafyeneyElements["element_for_yedia"].ToString()));
                        iErechElement = (System.Convert.IsDBNull(drMeafyeneyElements["erech_element"]) ? 0 : int.Parse(drMeafyeneyElements["erech_element"].ToString()));

                        iMisparSidurMatalotTnua = (System.Convert.IsDBNull(drMeafyeneyElements["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString()));
                        bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString());
                        sBusNumberMust = drMeafyeneyElements["bus_number_must"].ToString();
                        bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
                        sElementHamtana = drMeafyeneyElements["element_hamtana"].ToString();
                        bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
                        sElementIgnoreHafifaBetweenNesiot = drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString();
                        bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString());
                        sElementIgnoreReka = drMeafyeneyElements["lehitalem_beitur_reyka"].ToString();
                        bElementIgnoreReka = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_beitur_reyka"].ToString());

                        sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
                        sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
                        sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
                        sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
                        sElementLershut = drMeafyeneyElements["Lershut"].ToString();
                        sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();
                        sHamtanaEilat = drMeafyeneyElements["hamtana_eilat"].ToString();
                        bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
                        iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());
            
                        sBitulBiglalIchurLasidur = drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString();
                        bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString());
                        sDivuchInSidurVisa = drMeafyeneyElements["divuch_in_sidur_visa"].ToString();
                        sDivuchInSidurMeyuchad = drMeafyeneyElements["divuach_besidur_meyuchad"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddEmployeePeilut(int iKey, DataRow dr, DataTable dtPeiluyot)
        {            
            //נתוני פעילויות       
            iPeilutMisparSidur = (System.Convert.IsDBNull(dr["peilut_mispar_sidur"]) ? 0 : int.Parse(dr["peilut_mispar_sidur"].ToString()));            
            iKisuyTor = (System.Convert.IsDBNull(dr["Kisuy_Tor"])? 0 : int.Parse(dr["Kisuy_Tor"].ToString()));
            iOldKisuyTor = iKisuyTor;
            lMakatNesia = (System.Convert.IsDBNull(dr["Makat_Nesia"]) ? 0 : long.Parse(dr["Makat_Nesia"].ToString()));
            lOldMakatNesia = lMakatNesia; 
            dFullShatYetzia = (System.Convert.IsDBNull(dr["Shat_Yetzia"]) ? new DateTime(199,1,1) : DateTime.Parse(dr["Shat_Yetzia"].ToString()));
            dOldFullShatYetzia = dFullShatYetzia;
            sShatYetzia = (System.Convert.IsDBNull(dr["Shat_Yetzia"]) ? "" : DateTime.Parse(dr["Shat_Yetzia"].ToString()).ToString("HH:mm"));          
            lOtoNo = (System.Convert.IsDBNull(dr["Oto_No"])  ? 0 : long.Parse(dr["Oto_No"].ToString()));
            lOldOtoNo = lOtoNo;
            lMisparSiduriOto = (System.Convert.IsDBNull(dr["mispar_siduri_oto"]) ? 0 : int.Parse(dr["mispar_siduri_oto"].ToString()));
            iElementLeYedia =(System.Convert.IsDBNull(dr["element_for_yedia"]) ? 0 : int.Parse(dr["element_for_yedia"].ToString()));
            iErechElement = (System.Convert.IsDBNull(dr["erech_element"]) ? 0 : int.Parse(dr["erech_element"].ToString()));
                
            iMisparSidurMatalotTnua = (System.Convert.IsDBNull(dr["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(dr["mispar_sidur_matalot_tnua"].ToString()));
            bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(dr["mispar_sidur_matalot_tnua"].ToString());
            sBusNumberMust = dr["bus_number_must"].ToString();
            bBusNumberMustExists = !(String.IsNullOrEmpty(dr["bus_number_must"].ToString()));
            sElementHamtana = dr["element_hamtana"].ToString();
            bElementHamtanaExists =!String.IsNullOrEmpty(dr["element_hamtana"].ToString());
            sElementIgnoreHafifaBetweenNesiot = dr["lehitalem_hafifa_bein_nesiot"].ToString();
            bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(dr["lehitalem_hafifa_bein_nesiot"].ToString());
            sElementIgnoreReka = dr["lehitalem_beitur_reyka"].ToString();
            bElementIgnoreReka = !String.IsNullOrEmpty(dr["lehitalem_beitur_reyka"].ToString());

            sElementZviraZman = dr["element_zvira_zman"].ToString();
            sElementNesiaReka = dr["nesia_reika"].ToString();
            sElementInMinutes = dr["element_in_minutes"].ToString();
            sKodLechishuvPremia = dr["kod_lechishuv_premia"].ToString();
            sElementLershut= dr["Lershut"].ToString();
            sLoNitzbarLishatGmar= dr["lo_nizbar_leshat_gmar"].ToString();
            sHamtanaEilat = dr["hamtana_eilat"].ToString();
            bElementLershutExists = !String.IsNullOrEmpty(dr["Lershut"].ToString());
            lMisparMatala = (System.Convert.IsDBNull(dr["mispar_matala"]) ? 0 : int.Parse(dr["mispar_matala"].ToString()));
            sBitulBiglalIchurLasidur = dr["bitul_biglal_ichur_lasidur"].ToString();
            bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(dr["bitul_biglal_ichur_lasidur"].ToString());
            lMisparVisa = (System.Convert.IsDBNull(dr["mispar_visa"]) ? 0 : long.Parse(dr["mispar_visa"].ToString()));
            sDivuchInSidurVisa = dr["divuch_in_sidur_visa"].ToString();
            sDivuchInSidurMeyuchad = dr["divuach_besidur_meyuchad"].ToString();
            iMisparKnisa = (System.Convert.IsDBNull(dr["mispar_knisa"]) ? 0 : int.Parse(dr["mispar_knisa"].ToString()));
            bImutNetzer = System.Convert.IsDBNull(dr["imut_netzer"]) ? false : true;
            iBitulOHosafa = System.Convert.IsDBNull(dr["peilut_bitul_o_hosafa"]) ? 0 : int.Parse(dr["peilut_bitul_o_hosafa"].ToString());
            iDakotBafoal = System.Convert.IsDBNull(dr["DAKOT_BAFOAL"]) ? 0 : int.Parse(dr["DAKOT_BAFOAL"].ToString());
            iOldDakotBafoal = iDakotBafoal;
            iKmVisa = System.Convert.IsDBNull(dr["KM_VISA"]) ? 0 : int.Parse(dr["KM_VISA"].ToString());
            dCardLastUpdate = System.Convert.IsDBNull(dr["taarich_idkun_acharon_peilut"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon_peilut"].ToString());
            sSnifTnua = dr["snif_tnua"].ToString();
            sHeara = dr["heara_peilut"].ToString();
             sShilutNetzer = dr["shilut_netzer"].ToString();
             dShatYetziaNetzer = (System.Convert.IsDBNull(dr["shat_yetzia_netzer"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_yetzia_netzer"].ToString()));
             dShatBhiratNesiaNetzer = (System.Convert.IsDBNull(dr["shat_bhirat_nesia_netzer"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_bhirat_nesia_netzer"].ToString()));
           iMisparSidurNetzer = System.Convert.IsDBNull(dr["mispar_sidur_netzer"]) ? 0 : int.Parse(dr["mispar_sidur_netzer"].ToString());
           sMikumBhiratNesiaNetzer = dr["mikum_bhirat_nesia_netzer"].ToString();
            lMakatNetzer = System.Convert.IsDBNull(dr["makat_netzer"]) ? 0 : long.Parse(dr["makat_netzer"].ToString());
            lOtoNoNetzer = System.Convert.IsDBNull(dr["oto_no_netzer"]) ? 0 : long.Parse(dr["oto_no_netzer"].ToString());
            iElementLeShatGmar = System.Convert.IsDBNull(dr["peilut_mashmautit"]) ? 0 : int.Parse(dr["peilut_mashmautit"].ToString());
            
            iKodShinuyPremia = System.Convert.IsDBNull(dr["kod_shinuy_premia"]) ? 0 : int.Parse(dr["kod_shinuy_premia"].ToString());
            
            //נבדוק מה סוג הפעילות )שירות,נמק,ריקה,אלמנט,וסות) ונשלח בהתאם לתנועה
            SetKavDetails(dtPeiluyot,lMakatNesia);
            if ((iMisparKnisa > 0) || ((iPeilutMisparSidur < 1000) && (lMakatNesia==0)))
            {
                sMakatDescription = dr["teur_nesia"].ToString();
                if (iMisparKnisa > 0)
                {
                    bKnisaNeeded = sMakatDescription.Replace("-"," ").IndexOf(MAKAT_KNISA) > 0 ? true : false;
                    sSugShirutName = COL_TRIP_KNISA;
                }
            }
        }
        private DataTable GetCatalog(long lMakatNasia, DateTime dCardDate, clKavim.enMakatType oMakatType, ref int iMakatValid)
        {
            string sCacheKey;
            DataTable dtKavim;
            clKavim oKavim = new clKavim();
            sCacheKey = string.Concat(lMakatNesia.ToString(), " ", dCardDate.ToShortDateString());
            //DataSet dsKavim;
            try
            {
                //dtKavim = (DataTable)HttpContext.Current.Cache.Get(sCacheKey);
                dtKavim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
            }
            catch (Exception ex)
            {
                dtKavim = null;
            }

            try
            {
                switch (oMakatType)
                {
                    case clKavim.enMakatType.mKavShirut:                       
                        if (dtKavim == null)
                        {
                            dtKavim = oKavim.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);
                            HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                        }
                        break;
                    case clKavim.enMakatType.mEmpty:                       
                        if (dtKavim == null)
                        {
                            dtKavim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                            HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                            //HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.Now.AddDays(1), TimeSpan.FromMinutes(1440));
                        }
                        break;
                    case clKavim.enMakatType.mNamak:                       
                        if (dtKavim == null)
                        {
                            dtKavim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                            HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                            //HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.Now.AddDays(1), TimeSpan.FromMinutes(1440));
                        }
                        break;
                }
                //if (HttpContext.Current != null)
                //{                   
                    //HttpContext.Current.Cache.Insert(sCacheKey, dtKavim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                //}
                return dtKavim;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetKavDetails(DataTable dtPeiluyot, long lMakatNesia)
        {
            clKavim _Kavim = new clKavim();
            DataRow[] dr;            
            iMakatType = _Kavim.GetMakatType(lMakatNesia);
            clKavim.enMakatType oMakatType;
            oMakatType = (clKavim.enMakatType)iMakatType;
            DataTable dtVisut= new DataTable();
            switch (oMakatType)
            {
                case clKavim.enMakatType.mKavShirut:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            if (dr[0]["EILAT_TRIP"] != null)
                            {
                                if (dr[0]["EILAT_TRIP"].ToString() == "1")                               
                                    //אם לפחות אחת מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                                    bPeilutEilat = true;                                
                            }
                            iEilatTrip = (!System.Convert.IsDBNull(dr[0]["EILAT_TRIP"])) ? int.Parse(dr[0]["EILAT_TRIP"].ToString()) : 0;
                            iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                            iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            sShilut = dr[0]["SHILUT"].ToString();                           
                            sSugShirutName = dr[0]["SUG_SHIRUT_NAME"].ToString();
                            iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));
                           
                            fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                            iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                        }
                        else                       
                            iMakatValid = 1;                        
                    }
                   
                     break;
                case clKavim.enMakatType.mEmpty:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                            iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                            sShilut = "";
                            sSugShirutName = COL_TRIP_EMPTY;
                            iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));
                           
                            fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                        }
                        else                        
                            iMakatValid = 1;                          
                    }
                    bPeilutNotRekea = false;
                          
                    break;
                case clKavim.enMakatType.mNamak:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                            iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                            sShilut = dr[0]["SHILUT"].ToString();
                            iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));
                           
                            sSugShirutName = COL_TRIP_NAMAK;
                            fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                        }
                        else                       
                            iMakatValid =1;                          
                    }
                    break;
                case clKavim.enMakatType.mElement:
                    int iKodElement;
                    DataRow[] drElementim;

                    iMazanTichnun = int.Parse(lMakatNesia.ToString().Substring(3, 3));
                    iMazanTashlum = int.Parse(lMakatNesia.ToString().Substring(3, 3));                        
                    iKodElement = int.Parse(lMakatNesia.ToString().Substring(1, 2));
                    drElementim = dtElementim.Select(string.Concat("KOD_ELEMENT=", iKodElement));
                  
                    if (iKodElement != 0)                    
                        iMakatValid = ((drElementim.Length > 0) ? 0 : 1);

                    sMakatNesia = lMakatNesia.ToString().Substring(0, 3);                     
                    sMakatDescription = ((drElementim.Length > 0)?drElementim[0]["teur_element"].ToString():"");
                    bElementHachanatMechona = ((sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString())));

                    sSugShirutName = "";
                    break;
                case clKavim.enMakatType.mVisut:
                    dtVisut = _Kavim.GetVisutDetails(lMakatNesia);
                    if (dtVisut.Rows.Count>0)
                        sMakatDescription = dtVisut.Rows[0]["teur_visut"].ToString();
                    
                    sSugShirutName = COL_TRIP_VISUT;                    
                    iMakatType =clKavim.enMakatType.mElement.GetHashCode(); //5-Element  
                    break;
                default:
                    iMakatValid = 1;
                    break;
            }
 }

        private void GetKavDetails(long lMakatNesia)
        {
            clKavim oKavim = new clKavim();
            int iKodElement;
            DataRow[] drElementim;
            
            try
            {
                iMakatType = oKavim.GetMakatType(lMakatNesia);
                clKavim.enMakatType oMakatType;
                oMakatType = (clKavim.enMakatType)iMakatType;
                switch (oMakatType)
                {                    
                    case clKavim.enMakatType.mKavShirut:
                        dtKavim = GetCatalog(lMakatNesia, dCardDate, oMakatType, ref iMakatValid);        
                        if (dtKavim.Rows.Count > 0)
                        {
                            //if (dsKavim.Tables[0].Rows[0]["EilatTrip"]!=null)
                            if (dtKavim.Rows[0]["EilatTrip"] != null)
                            {
                                //if (dsKavim.Tables[0].Rows[0]["EilatTrip"].ToString()=="1")
                                if (dtKavim.Rows[0]["EilatTrip"].ToString() == "1")
                                {
                                    //אם לפחות אחד מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                                    bPeilutEilat = true;
                                }
                            }
                            iMazanTichnun = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTichnun"])) ? int.Parse(dtKavim.Rows[0]["MazanTichnun"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTashlum"])) ? int.Parse(dtKavim.Rows[0]["MazanTashlum"].ToString()) : 0;
                            //iSugKnisa = int.Parse(dtKavim.Rows[0]["SugKnisa"].ToString());
                            sMakatDescription = dtKavim.Rows[0]["Description"].ToString();
                            sShilut = dtKavim.Rows[0]["Shilut"].ToString();
                            sSugShirutName = dtKavim.Rows[0]["SugShirutName"].ToString();
                            fKm = float.Parse(dtKavim.Rows[0]["KM"].ToString());
                            iOnatiyut = (System.Convert.IsDBNull(dtKavim.Rows[0]["Onatiut"]) ? 0 : int.Parse(dtKavim.Rows[0]["Onatiut"].ToString()));
                            
                        }
                        break;
                    case clKavim.enMakatType.mEmpty:
                        dtKavim = GetCatalog(lMakatNesia, dCardDate, oMakatType, ref iMakatValid); 
                        if (dtKavim.Rows.Count > 0)
                        {
                            iMazanTichnun = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTichnun"])) ? int.Parse(dtKavim.Rows[0]["MazanTichnun"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTashlum"])) ? int.Parse(dtKavim.Rows[0]["MazanTashlum"].ToString()) : 0;
                            //iSugKnisa = int.Parse(dtKavim.Rows[0]["SugKnisa"].ToString());
                            sMakatDescription = dtKavim.Rows[0]["Description"].ToString();
                            sShilut = "";
                            sSugShirutName = COL_TRIP_EMPTY;//dtKavim.Rows[0]["SugShirutName"].ToString();
                            fKm = float.Parse(dtKavim.Rows[0]["KM"].ToString());
                            iOnatiyut = (System.Convert.IsDBNull(dtKavim.Rows[0]["Onatiut"]) ? 0 : int.Parse(dtKavim.Rows[0]["Onatiut"].ToString()));
                           
                        }
                        bPeilutNotRekea = false;
                         
                        break;
                    case clKavim.enMakatType.mNamak:
                        dtKavim = GetCatalog(lMakatNesia, dCardDate, oMakatType, ref iMakatValid); 
                        if (dtKavim.Rows.Count > 0)
                        {
                            iMazanTichnun = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTichnun"])) ? int.Parse(dtKavim.Rows[0]["MazanTichnun"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTashlum"])) ? int.Parse(dtKavim.Rows[0]["MazanTashlum"].ToString()) : 0;
                            //iSugKnisa = int.Parse(dtKavim.Rows[0]["SugKnisa"].ToString());
                            sMakatDescription = dtKavim.Rows[0]["Description"].ToString();
                            sShilut = dtKavim.Rows[0]["Shilut"].ToString();
                            sSugShirutName = COL_TRIP_NAMAK;//dtKavim.Rows[0]["SugShirutName"].ToString();
                            fKm = float.Parse(dtKavim.Rows[0]["KM"].ToString());
                            iOnatiyut = (System.Convert.IsDBNull(dtKavim.Rows[0]["Onatiut"]) ? 0 : int.Parse(dtKavim.Rows[0]["Onatiut"].ToString()));
                           
                        }
                        break;
                    case clKavim.enMakatType.mElement:
                        iKodElement = int.Parse(lMakatNesia.ToString().Substring(1,2));
                        drElementim=dtElementim.Select(string.Concat("KOD_ELEMENT=", iKodElement));
                        iMakatValid = ((drElementim.Length > 0) ? 0 : 1);
                        sMakatNesia = lMakatNesia.ToString().Substring(0, 3);
                        sMakatDescription = drElementim[0]["teur_element"].ToString();
                        bElementHachanatMechona = ((sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString()))
                                                    || (sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                    || (sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString())));

                        sSugShirutName = COL_TRIP_ELEMENT;
                        break;
                    default:
                        iMakatValid = 0;
                        break;
                }                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsMustBusNumber(int iVisutMustRechevWC)
        {
          
            try
            {
                return ((iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || iMakatType == clKavim.enMakatType.mEmpty.GetHashCode()
                   || (iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (bBusNumberMustExists || (lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700" && iVisutMustRechevWC==1)) && !bElementHachanatMechona)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }    
 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using System.Data;
using System.Web;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces;
using KDSCommon.UDT;

namespace KdsLibrary.KDSLogic.Managers
{
    public class PeilutManager : IPeilutManager 
    {
        
        private const string COL_TRIP_EMPTY = "ריקה";
        private const string COL_TRIP_NAMAK = "נמ'ק";
        private const string COL_TRIP_ELEMENT = "אלמנט";
        private const string COL_TRIP_VISUT = "ויסות";
        private const string COL_TRIP_KNISA = "כניסה";
        private const string MAKAT_KNISA = "לפי צורך";
        private IUnityContainer _container;

        public PeilutManager(IUnityContainer container)
        {
            _container = container;
        }

        //public PeilutDM CreatePeilutFromOldPeilut(int iMisparIshi, DateTime dDateCard, PeilutDM oPeilutOld, long lMakatNesiaNew, DataTable dtMeafyeneyElements)
        //{
        //    PeilutDM cls = new PeilutDM();

        //    DataTable dtPeiluyot;
        //    string sCacheKey = "ElementsTable";

        //    try
        //    {
        //        cls.dtElementim = GetDTElementim();
        //        //cls.dtElementim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
        //        //if (dtElementim == null)
        //        //{
        //        //    dtElementim = oUtils.GetCtbElementim();
        //        //    HttpRuntime.Cache.Insert(sCacheKey, dtElementim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
        //        //}
        //        //     dtElementim = oUtils.GetCtbElementim();
        //        cls.lMakatNesia = lMakatNesiaNew;
        //        cls.dCardDate = dDateCard;
        //        cls.iPeilutMisparSidur = oPeilutOld.iPeilutMisparSidur;
        //        cls.iKisuyTor = oPeilutOld.iKisuyTor;
        //        cls.iOldKisuyTor = cls.iKisuyTor;
        //        cls.dFullShatYetzia = oPeilutOld.dFullShatYetzia;
        //        cls.dOldFullShatYetzia = oPeilutOld.dFullShatYetzia;
        //        cls.sShatYetzia = oPeilutOld.sShatYetzia;

        //        cls.lOtoNo = oPeilutOld.lOtoNo;
        //        cls.lOldOtoNo = cls.lOtoNo;

        //        cls.lMisparSiduriOto = oPeilutOld.lMisparSiduriOto;

        //        cls.lMisparVisa = oPeilutOld.lMisparVisa;
        //        cls.iMisparKnisa = oPeilutOld.iMisparKnisa;
        //        cls.bImutNetzer = oPeilutOld.bImutNetzer;
        //        cls.iBitulOHosafa = oPeilutOld.iBitulOHosafa;
        //        cls.iDakotBafoal = oPeilutOld.iDakotBafoal;
        //        cls.iOldDakotBafoal = cls.iDakotBafoal;
        //        cls.iKmVisa = oPeilutOld.iKmVisa;
        //        cls.dCardLastUpdate = oPeilutOld.dCardLastUpdate;
        //        cls.sSnifTnua = oPeilutOld.sSnifTnua;
        //        cls.lMisparSiduriOto = oPeilutOld.lMisparSiduriOto;
        //        cls.lMisparMatala = oPeilutOld.lMisparMatala;

        //        cls.sHeara = oPeilutOld.sHeara;
        //        cls.sShilutNetzer = oPeilutOld.sShilutNetzer;
        //        cls.dShatYetziaNetzer = oPeilutOld.dShatBhiratNesiaNetzer;
        //        cls.dShatBhiratNesiaNetzer = oPeilutOld.dShatBhiratNesiaNetzer;
        //        cls.iMisparSidurNetzer = oPeilutOld.iMisparSidurNetzer;
        //        cls.sMikumBhiratNesiaNetzer = oPeilutOld.sMikumBhiratNesiaNetzer;
        //        cls.lMakatNetzer = oPeilutOld.lMakatNetzer;
        //        cls.lOtoNoNetzer = oPeilutOld.lOtoNoNetzer;
        //        cls.iKodShinuyPremia = oPeilutOld.iKodShinuyPremia;
        //        cls.sMakatDescription = oPeilutOld.sMakatDescription;

        //        //TODO - need to try get from cahce and also save to peilut to cache
        //        var kavimManager = _container.Resolve<IKavimManager>();
        //        dtPeiluyot = kavimManager.GetKatalogKavim(iMisparIshi, dDateCard, dDateCard);

        //        SetKavDetails(cls, dtPeiluyot, cls.lMakatNesia);

        //        if ((enMakatType)cls.iMakatType == enMakatType.mElement)
        //        {
        //            DataRow[] dr = dtMeafyeneyElements.Select("kod_element=" + int.Parse(cls.lMakatNesia.ToString().Substring(1, 2)));
        //            DataRow drMeafyeneyElements;

        //            if (dr.Length > 0)
        //            {
        //                drMeafyeneyElements = dr[0];
        //                cls.iElementLeYedia = (System.Convert.IsDBNull(drMeafyeneyElements["element_for_yedia"]) ? 0 : int.Parse(drMeafyeneyElements["element_for_yedia"].ToString()));
        //                cls.iErechElement = (System.Convert.IsDBNull(drMeafyeneyElements["erech_element"]) ? 0 : int.Parse(drMeafyeneyElements["erech_element"].ToString()));

        //                cls.iMisparSidurMatalotTnua = (System.Convert.IsDBNull(drMeafyeneyElements["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString()));
        //                cls.bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString());
        //                cls.sBusNumberMust = drMeafyeneyElements["bus_number_must"].ToString();
        //                cls.bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
        //                cls.sElementHamtana = drMeafyeneyElements["element_hamtana"].ToString();
        //                cls.bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
        //                cls.sElementIgnoreHafifaBetweenNesiot = drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString();
        //                cls.bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString());

        //                cls.sElementIgnoreReka = drMeafyeneyElements["lehitalem_beitur_reyka"].ToString();
        //                cls.bElementIgnoreReka = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_beitur_reyka"].ToString());

        //                cls.sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
        //                cls.sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
        //                cls.sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
        //                cls.sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
        //                cls.sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();
        //                cls.sHamtanaEilat = drMeafyeneyElements["hamtana_eilat"].ToString();

        //                cls.sElementLershut = drMeafyeneyElements["Lershut"].ToString();
        //                cls.bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
        //                cls.iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());

        //                cls.sBitulBiglalIchurLasidur = drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString();
        //                cls.bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString());
        //                cls.sDivuchInSidurVisa = drMeafyeneyElements["divuch_in_sidur_visa"].ToString();
        //                cls.sDivuchInSidurMeyuchad = drMeafyeneyElements["divuach_besidur_meyuchad"].ToString();

        //            }
        //        }

        //        return cls;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void UpdatePeilutFromOldPeilut(int iMisparIshi, DateTime dDateCard, PeilutDM cls, long lMakatNesiaNew, DataTable dtMeafyeneyElements)
        {
      
            DataTable dtPeiluyot;
            string sCacheKey = "ElementsTable";

            try
            {
                cls.dtElementim = GetDTElementim(dDateCard);
                cls.lMakatNesia = lMakatNesiaNew;
                cls.dCardDate = dDateCard;
                cls.iOldKisuyTor = cls.iKisuyTor;

                cls.lOldOtoNo = cls.lOtoNo;
                cls.lOldLicenseNumber = cls.lLicenseNumber;

                cls.iOldDakotBafoal = cls.iDakotBafoal;


                //TODO - need to try get from cahce and also save to peilut to cache
                var kavimManager = _container.Resolve<IKavimManager>();
                dtPeiluyot = kavimManager.GetKatalogKavim(iMisparIshi, dDateCard, dDateCard);

                SetKavDetails(cls, dtPeiluyot, cls.lMakatNesia);

                if ((enMakatType)cls.iMakatType == enMakatType.mElement)
                {
                    DataRow[] dr = dtMeafyeneyElements.Select("kod_element=" + int.Parse(cls.lMakatNesia.ToString().Substring(1, 2)));
                    DataRow drMeafyeneyElements;

                    if (dr.Length > 0)
                    {
                        drMeafyeneyElements = dr[0];
                        cls.iElementLeYedia = (System.Convert.IsDBNull(drMeafyeneyElements["element_for_yedia"]) ? 0 : int.Parse(drMeafyeneyElements["element_for_yedia"].ToString()));
                        cls.iErechElement = (System.Convert.IsDBNull(drMeafyeneyElements["erech_element"]) ? 0 : int.Parse(drMeafyeneyElements["erech_element"].ToString()));

                        cls.iMisparSidurMatalotTnua = (System.Convert.IsDBNull(drMeafyeneyElements["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString()));
                        cls.bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString());
                        cls.sBusNumberMust = drMeafyeneyElements["bus_number_must"].ToString();
                        cls.bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
                        cls.sElementHamtana = drMeafyeneyElements["element_hamtana"].ToString();
                        cls.bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
                        cls.sElementIgnoreHafifaBetweenNesiot = drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString();
                        cls.bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString());

                        cls.sElementIgnoreReka = drMeafyeneyElements["lehitalem_beitur_reyka"].ToString();
                        cls.bElementIgnoreReka = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_beitur_reyka"].ToString());

                        cls.sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
                        cls.sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
                        cls.sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
                        cls.sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
                        cls.sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();
                        cls.sHamtanaEilat = drMeafyeneyElements["hamtana_eilat"].ToString();

                        cls.sElementLershut = drMeafyeneyElements["Lershut"].ToString();
                        cls.bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
                        cls.iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());


                        cls.sBitulBiglalIchurLasidur = drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString();
                        cls.bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString());
                        cls.sDivuchInSidurVisa = drMeafyeneyElements["divuch_in_sidur_visa"].ToString();
                        cls.sDivuchInSidurMeyuchad = drMeafyeneyElements["divuach_besidur_meyuchad"].ToString();

                        cls.iSectorZviraZmanEelement = String.IsNullOrEmpty(drMeafyeneyElements["element_zvira_zman"].ToString()) ? 0 : int.Parse(drMeafyeneyElements["element_zvira_zman"].ToString());


                    }
                }

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PeilutDM CreateClsPeilut(int iMisparIshi, DateTime dDateCard, OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, DataTable dtMeafyeneyElements)
        {
            PeilutDM cls = new PeilutDM();
            DataTable dtPeiluyot;
            string sCacheKey = "ElementsTable";

            try
            {

                cls.dtElementim = GetDTElementim(dDateCard);
     
                cls.lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                cls.lOldMakatNesia = cls.lMakatNesia;
                cls.dCardDate = dDateCard;
                cls.iPeilutMisparSidur = oObjPeilutOvdimIns.MISPAR_SIDUR;
                cls.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                cls.dOldFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                cls.sShatYetzia = cls.dFullShatYetzia.ToString("HH:mm");

                cls.lMisparVisa = oObjPeilutOvdimIns.MISPAR_VISA;
                cls.iMisparKnisa = oObjPeilutOvdimIns.MISPAR_KNISA;
                cls.lMisparSiduriOto = oObjPeilutOvdimIns.MISPAR_SIDURI_OTO;
                cls.lMisparMatala = oObjPeilutOvdimIns.MISPAR_MATALA;
                cls.lOtoNo = oObjPeilutOvdimIns.OTO_NO;
                cls.lOldOtoNo = cls.lOtoNo;
                cls.lLicenseNumber = oObjPeilutOvdimIns.LICENSE_NUMBER;
                cls.lOldLicenseNumber = cls.lLicenseNumber;



                var kavimManager = _container.Resolve<IKavimManager>();
                dtPeiluyot = kavimManager.GetKatalogKavim(iMisparIshi, dDateCard, dDateCard);

                SetKavDetails(cls, dtPeiluyot, cls.lMakatNesia);

                if ((enMakatType)cls.iMakatType == enMakatType.mElement)
                {
                    DataRow[] dr = dtMeafyeneyElements.Select("kod_element=" + int.Parse(cls.lMakatNesia.ToString().Substring(1, 2)));
                    DataRow drMeafyeneyElements;

                    if (dr.Length > 0)
                    {
                        drMeafyeneyElements = dr[0];
                        cls.iElementLeYedia = (System.Convert.IsDBNull(drMeafyeneyElements["element_for_yedia"]) ? 0 : int.Parse(drMeafyeneyElements["element_for_yedia"].ToString()));
                        cls.iErechElement = (System.Convert.IsDBNull(drMeafyeneyElements["erech_element"]) ? 0 : int.Parse(drMeafyeneyElements["erech_element"].ToString()));

                        cls.iMisparSidurMatalotTnua = (System.Convert.IsDBNull(drMeafyeneyElements["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString()));
                        cls.bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(drMeafyeneyElements["mispar_sidur_matalot_tnua"].ToString());
                        cls.sBusNumberMust = drMeafyeneyElements["bus_number_must"].ToString();
                        cls.bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
                        cls.sElementHamtana = drMeafyeneyElements["element_hamtana"].ToString();
                        cls.bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
                        cls.sElementIgnoreHafifaBetweenNesiot = drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString();
                        cls.bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_hafifa_bein_nesiot"].ToString());
                        cls.sElementIgnoreReka = drMeafyeneyElements["lehitalem_beitur_reyka"].ToString();
                        cls.bElementIgnoreReka = !String.IsNullOrEmpty(drMeafyeneyElements["lehitalem_beitur_reyka"].ToString());

                        cls.sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
                        cls.sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
                        cls.sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
                        cls.sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
                        cls.sElementLershut = drMeafyeneyElements["Lershut"].ToString();
                        cls.sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();
                        cls.sHamtanaEilat = drMeafyeneyElements["hamtana_eilat"].ToString();
                        cls.bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
                        cls.iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());

                        cls.sBitulBiglalIchurLasidur = drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString();
                        cls.bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(drMeafyeneyElements["bitul_biglal_ichur_lasidur"].ToString());
                        cls.sDivuchInSidurVisa = drMeafyeneyElements["divuch_in_sidur_visa"].ToString();
                        cls.sDivuchInSidurMeyuchad = drMeafyeneyElements["divuach_besidur_meyuchad"].ToString();
                        cls.iSectorZviraZmanEelement = String.IsNullOrEmpty(drMeafyeneyElements["element_zvira_zman"].ToString()) ? 0 : int.Parse(drMeafyeneyElements["element_zvira_zman"].ToString());

                    }
                }
                return cls;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PeilutDM CreateEmployeePeilut(DateTime CardDate,int iKey, DataRow dr, DataTable dtPeiluyot)
        {
            //נתוני פעילויות       
            PeilutDM cls = new PeilutDM();
            cls.dtElementim = GetDTElementim(CardDate);
            cls.dCardDate = CardDate;
            cls.iPeilutMisparSidur = (System.Convert.IsDBNull(dr["peilut_mispar_sidur"]) ? 0 : int.Parse(dr["peilut_mispar_sidur"].ToString()));
            cls.iKisuyTor = (System.Convert.IsDBNull(dr["Kisuy_Tor"]) ? 0 : int.Parse(dr["Kisuy_Tor"].ToString()));
            cls.iOldKisuyTor = cls.iKisuyTor;
            cls.lMakatNesia = (System.Convert.IsDBNull(dr["Makat_Nesia"]) ? 0 : long.Parse(dr["Makat_Nesia"].ToString()));
            cls.lOldMakatNesia = cls.lMakatNesia;
            cls.dFullShatYetzia = (System.Convert.IsDBNull(dr["Shat_Yetzia"]) ? new DateTime(199, 1, 1) : DateTime.Parse(dr["Shat_Yetzia"].ToString()));
            cls.dOldFullShatYetzia = cls.dFullShatYetzia;
            cls.sShatYetzia = (System.Convert.IsDBNull(dr["Shat_Yetzia"]) ? "" : DateTime.Parse(dr["Shat_Yetzia"].ToString()).ToString("HH:mm"));
            cls.lOtoNo = (System.Convert.IsDBNull(dr["Oto_No"]) ? 0 : long.Parse(dr["Oto_No"].ToString()));
            cls.lLicenseNumber = (System.Convert.IsDBNull(dr["LICENSE_NUMBER"]) ? 0 : long.Parse(dr["LICENSE_NUMBER"].ToString()));
            cls.lOldOtoNo = cls.lOtoNo;
            cls.lOldLicenseNumber = cls.lLicenseNumber;
            cls.lMisparSiduriOto = (System.Convert.IsDBNull(dr["mispar_siduri_oto"]) ? 0 : int.Parse(dr["mispar_siduri_oto"].ToString()));
            cls.iElementLeYedia = (System.Convert.IsDBNull(dr["element_for_yedia"]) ? 0 : int.Parse(dr["element_for_yedia"].ToString()));
            cls.iErechElement = (System.Convert.IsDBNull(dr["erech_element"]) ? 0 : int.Parse(dr["erech_element"].ToString()));

            cls.iMisparSidurMatalotTnua = (System.Convert.IsDBNull(dr["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(dr["mispar_sidur_matalot_tnua"].ToString()));
            cls.bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(dr["mispar_sidur_matalot_tnua"].ToString());
            cls.sBusNumberMust = dr["bus_number_must"].ToString();
            cls.bBusNumberMustExists = !(String.IsNullOrEmpty(dr["bus_number_must"].ToString()));
            cls.sElementHamtana = dr["element_hamtana"].ToString();
            cls.bElementHamtanaExists = !String.IsNullOrEmpty(dr["element_hamtana"].ToString());
            cls.sElementIgnoreHafifaBetweenNesiot = dr["lehitalem_hafifa_bein_nesiot"].ToString();
            cls.bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(dr["lehitalem_hafifa_bein_nesiot"].ToString());
            cls.sElementIgnoreReka = dr["lehitalem_beitur_reyka"].ToString();
            cls.bElementIgnoreReka = !String.IsNullOrEmpty(dr["lehitalem_beitur_reyka"].ToString());

            cls.sElementZviraZman = dr["element_zvira_zman"].ToString();
            cls.sElementNesiaReka = dr["nesia_reika"].ToString();
            cls.sElementInMinutes = dr["element_in_minutes"].ToString();
            cls.sKodLechishuvPremia = dr["kod_lechishuv_premia"].ToString();
            cls.sElementLershut = dr["Lershut"].ToString();
            cls.sLoNitzbarLishatGmar = dr["lo_nizbar_leshat_gmar"].ToString();
            cls.sHamtanaEilat = dr["hamtana_eilat"].ToString();
            cls.bElementLershutExists = !String.IsNullOrEmpty(dr["Lershut"].ToString());
            cls.lMisparMatala = (System.Convert.IsDBNull(dr["mispar_matala"]) ? 0 : int.Parse(dr["mispar_matala"].ToString()));
            cls.sBitulBiglalIchurLasidur = dr["bitul_biglal_ichur_lasidur"].ToString();
            cls.bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(dr["bitul_biglal_ichur_lasidur"].ToString());
            cls.lMisparVisa = (System.Convert.IsDBNull(dr["mispar_visa"]) ? 0 : long.Parse(dr["mispar_visa"].ToString()));
            cls.sDivuchInSidurVisa = dr["divuch_in_sidur_visa"].ToString();
            cls.sDivuchInSidurMeyuchad = dr["divuach_besidur_meyuchad"].ToString();
            cls.iMisparKnisa = (System.Convert.IsDBNull(dr["mispar_knisa"]) ? 0 : int.Parse(dr["mispar_knisa"].ToString()));
            cls.bImutNetzer = System.Convert.IsDBNull(dr["imut_netzer"]) ? false : true;
            cls.iBitulOHosafa = System.Convert.IsDBNull(dr["peilut_bitul_o_hosafa"]) ? 0 : int.Parse(dr["peilut_bitul_o_hosafa"].ToString());
            cls.iDakotBafoal = System.Convert.IsDBNull(dr["DAKOT_BAFOAL"]) ? 0 : int.Parse(dr["DAKOT_BAFOAL"].ToString());
            cls.iOldDakotBafoal = cls.iDakotBafoal;
            cls.iKmVisa = System.Convert.IsDBNull(dr["KM_VISA"]) ? 0 : int.Parse(dr["KM_VISA"].ToString());
            cls.dCardLastUpdate = System.Convert.IsDBNull(dr["taarich_idkun_acharon_peilut"]) ? DateTime.MinValue : DateTime.Parse(dr["taarich_idkun_acharon_peilut"].ToString());
            cls.sSnifTnua = dr["snif_tnua"].ToString();
            cls.sHeara = dr["heara_peilut"].ToString();
            cls.sShilutNetzer = dr["shilut_netzer"].ToString();
            cls.dShatYetziaNetzer = (System.Convert.IsDBNull(dr["shat_yetzia_netzer"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_yetzia_netzer"].ToString()));
            cls.dShatBhiratNesiaNetzer = (System.Convert.IsDBNull(dr["shat_bhirat_nesia_netzer"]) ? DateTime.MinValue : DateTime.Parse(dr["shat_bhirat_nesia_netzer"].ToString()));
            cls.iMisparSidurNetzer = System.Convert.IsDBNull(dr["mispar_sidur_netzer"]) ? 0 : int.Parse(dr["mispar_sidur_netzer"].ToString());
            cls.sMikumBhiratNesiaNetzer = dr["mikum_bhirat_nesia_netzer"].ToString();
            cls.lMakatNetzer = System.Convert.IsDBNull(dr["makat_netzer"]) ? 0 : long.Parse(dr["makat_netzer"].ToString());
            cls.lOtoNoNetzer = System.Convert.IsDBNull(dr["oto_no_netzer"]) ? 0 : long.Parse(dr["oto_no_netzer"].ToString());
            cls.iElementLeShatGmar = System.Convert.IsDBNull(dr["peilut_mashmautit"]) ? 0 : int.Parse(dr["peilut_mashmautit"].ToString());

            cls.iKodShinuyPremia = System.Convert.IsDBNull(dr["kod_shinuy_premia"]) ? 0 : int.Parse(dr["kod_shinuy_premia"].ToString());
            cls.iSectorZviraZmanEelement = String.IsNullOrEmpty(dr["element_zvira_zman"].ToString()) ? 0 : int.Parse(dr["element_zvira_zman"].ToString());

            //נבדוק מה סוג הפעילות )שירות,נמק,ריקה,אלמנט,וסות) ונשלח בהתאם לתנועה
            SetKavDetails(cls, dtPeiluyot, cls.lMakatNesia);
            if ((cls.iMisparKnisa > 0) || ((cls.iPeilutMisparSidur < 1000) && (cls.lMakatNesia == 0)))
            {
                cls.sMakatDescription = dr["teur_nesia"].ToString();
                if (cls.iMisparKnisa > 0)
                {
                    cls.bKnisaNeeded = cls.sMakatDescription.Replace("-", " ").IndexOf(MAKAT_KNISA) > 0 ? true : false;
                    cls.sSugShirutName = COL_TRIP_KNISA;
                }
            }
            cls.dtElementim = GetDTElementim(CardDate);

            return cls;
        }

        public bool IsMustBusNumber(PeilutDM cls, int iVisutMustRechevWC)
        {

            try
            {
                return ((cls.iMakatType == enMakatType.mVisa.GetHashCode() || cls.iMakatType == enMakatType.mKavShirut.GetHashCode() || cls.iMakatType == enMakatType.mNamak.GetHashCode() || cls.iMakatType == enMakatType.mEmpty.GetHashCode()
                   || (cls.iMakatType == enMakatType.mElement.GetHashCode() && (cls.bBusNumberMustExists || (cls.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700" && iVisutMustRechevWC == 1)) && !cls.bElementHachanatMechona)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetCatalog(long lMakatNesia, DateTime dCardDate, enMakatType oMakatType, ref int iMakatValid)
        {
            string sCacheKey;
            DataTable dtKavim;
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
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                switch (oMakatType)
                {
                    case enMakatType.mKavShirut:
                        if (dtKavim == null)
                        {
                            dtKavim = kavimDal.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);
                            HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                        }
                        break;
                    case enMakatType.mEmpty:
                        if (dtKavim == null)
                        {
                            
                            dtKavim = kavimDal.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                            HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                            //HttpRuntime.Cache.Insert(sCacheKey, dtKavim, null, DateTime.Now.AddDays(1), TimeSpan.FromMinutes(1440));
                        }
                        break;
                    case enMakatType.mNamak:
                        if (dtKavim == null)
                        {
                            dtKavim = kavimDal.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
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

        private void SetKavDetails(PeilutDM cls, DataTable dtPeiluyot, long lMakatNesia)
        {
            DataRow[] dr;
            cls.iMakatType = StaticBL.GetMakatType(lMakatNesia);
            enMakatType oMakatType;
            oMakatType = (enMakatType)cls.iMakatType;
            DataTable dtVisut = new DataTable();
            string sMakatNesia;
            switch (oMakatType)
            {
                case enMakatType.mKavShirut:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            if (dr[0]["EILAT_TRIP"] != null)
                            {
                                if (dr[0]["EILAT_TRIP"].ToString() == "1")
                                    //אם לפחות אחת מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                                    cls.bPeilutEilat = true;
                            }
                            cls.iEilatTrip = (!System.Convert.IsDBNull(dr[0]["EILAT_TRIP"])) ? int.Parse(dr[0]["EILAT_TRIP"].ToString()) : 0;
                            cls.iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            cls.iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            cls.iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                            cls.iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            cls.iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            cls.sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            cls.sShilut = dr[0]["SHILUT"].ToString();
                            cls.sSugShirutName = dr[0]["SUG_SHIRUT_NAME"].ToString();
                            cls.iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));

                            cls.fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                            cls.iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                        }
                        else
                            cls.iMakatValid = 1;
                    }

                    break;
                case enMakatType.mEmpty:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            cls.iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                            cls.iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            cls.iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            cls.iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            cls.sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            cls.iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            cls.iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                            cls.sShilut = "";
                            cls.sSugShirutName = COL_TRIP_EMPTY;
                            cls.iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));

                            cls.fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                        }
                        else
                            cls.iMakatValid = 1;
                    }
                    cls.bPeilutNotRekea = false;

                    break;
                case enMakatType.mNamak:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            cls.iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                            cls.iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            cls.iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            cls.iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            cls.sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            cls.iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            cls.iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                            cls.sShilut = dr[0]["SHILUT"].ToString();
                            cls.iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));

                            cls.sSugShirutName = COL_TRIP_NAMAK;
                            cls.fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                        }
                        else
                            cls.iMakatValid = 1;
                    }
                    break;
                case enMakatType.mElement:
                    int iKodElement;
                    DataRow[] drElementim;

                    cls.iMazanTichnun = int.Parse(lMakatNesia.ToString().Substring(3, 3));
                    cls.iMazanTashlum = int.Parse(lMakatNesia.ToString().Substring(3, 3));
                    iKodElement = int.Parse(lMakatNesia.ToString().Substring(1, 2));
                    drElementim = cls.dtElementim.Select(string.Concat("KOD_ELEMENT=", iKodElement));

                    if (iKodElement != 0)
                        cls.iMakatValid = ((drElementim.Length > 0) ? 0 : 1);

                    sMakatNesia = lMakatNesia.ToString().Substring(0, 3);
                    cls.sMakatDescription = ((drElementim.Length > 0) ? drElementim[0]["teur_element"].ToString() : "");
                    cls.bElementHachanatMechona = ((sMakatNesia.Equals(enElementHachanatMechona.Element701.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(enElementHachanatMechona.Element712.GetHashCode().ToString())));

                    cls.sSugShirutName = "";
                    break;
                case enMakatType.mVisut:
                    var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                    dtVisut = kavimDal.GetVisutDetails(lMakatNesia);
                    if (dtVisut.Rows.Count > 0)
                        cls.sMakatDescription = dtVisut.Rows[0]["teur_visut"].ToString();

                    cls.sSugShirutName = COL_TRIP_VISUT;
                    cls.iMakatType = enMakatType.mElement.GetHashCode(); //5-Element  
                    break;
                default:
                    cls.iMakatValid = 1;
                    break;
            }
        }

        private void GetKavDetails(PeilutDM cls, long lMakatNesia)
        {
            int iKodElement;
            DataRow[] drElementim;
            DataTable dtKavim;
            string sMakatNesia; 
            try
            {
                cls.iMakatType = StaticBL.GetMakatType(lMakatNesia);
                enMakatType oMakatType;
                oMakatType = (enMakatType)cls.iMakatType;
                switch (oMakatType)
                {
                    case enMakatType.mKavShirut:
                        dtKavim = GetCatalog(lMakatNesia, cls.dCardDate, oMakatType, ref cls.iMakatValid);
                        if (dtKavim.Rows.Count > 0)
                        {
                            //if (dsKavim.Tables[0].Rows[0]["EilatTrip"]!=null)
                            if (dtKavim.Rows[0]["EilatTrip"] != null)
                            {
                                //if (dsKavim.Tables[0].Rows[0]["EilatTrip"].ToString()=="1")
                                if (dtKavim.Rows[0]["EilatTrip"].ToString() == "1")
                                {
                                    //אם לפחות אחד מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                                    cls.bPeilutEilat = true;
                                }
                            }
                            cls.iMazanTichnun = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTichnun"])) ? int.Parse(dtKavim.Rows[0]["MazanTichnun"].ToString()) : 0;
                            cls.iMazanTashlum = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTashlum"])) ? int.Parse(dtKavim.Rows[0]["MazanTashlum"].ToString()) : 0;
                            //iSugKnisa = int.Parse(dtKavim.Rows[0]["SugKnisa"].ToString());
                            cls.sMakatDescription = dtKavim.Rows[0]["Description"].ToString();
                            cls.sShilut = dtKavim.Rows[0]["Shilut"].ToString();
                            cls.sSugShirutName = dtKavim.Rows[0]["SugShirutName"].ToString();
                            cls.fKm = float.Parse(dtKavim.Rows[0]["KM"].ToString());
                            cls.iOnatiyut = (System.Convert.IsDBNull(dtKavim.Rows[0]["Onatiut"]) ? 0 : int.Parse(dtKavim.Rows[0]["Onatiut"].ToString()));

                        }
                        break;
                    case enMakatType.mEmpty:
                        dtKavim = GetCatalog(lMakatNesia, cls.dCardDate, oMakatType, ref cls.iMakatValid);
                        if (dtKavim.Rows.Count > 0)
                        {
                            cls.iMazanTichnun = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTichnun"])) ? int.Parse(dtKavim.Rows[0]["MazanTichnun"].ToString()) : 0;
                            cls.iMazanTashlum = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTashlum"])) ? int.Parse(dtKavim.Rows[0]["MazanTashlum"].ToString()) : 0;
                            //iSugKnisa = int.Parse(dtKavim.Rows[0]["SugKnisa"].ToString());
                            cls.sMakatDescription = dtKavim.Rows[0]["Description"].ToString();
                            cls.sShilut = "";
                            cls.sSugShirutName = COL_TRIP_EMPTY;//dtKavim.Rows[0]["SugShirutName"].ToString();
                            cls.fKm = float.Parse(dtKavim.Rows[0]["KM"].ToString());
                            cls.iOnatiyut = (System.Convert.IsDBNull(dtKavim.Rows[0]["Onatiut"]) ? 0 : int.Parse(dtKavim.Rows[0]["Onatiut"].ToString()));

                        }
                        cls.bPeilutNotRekea = false;

                        break;
                    case enMakatType.mNamak:
                        dtKavim = GetCatalog(lMakatNesia, cls.dCardDate, oMakatType, ref cls.iMakatValid);
                        if (dtKavim.Rows.Count > 0)
                        {
                            cls.iMazanTichnun = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTichnun"])) ? int.Parse(dtKavim.Rows[0]["MazanTichnun"].ToString()) : 0;
                            cls.iMazanTashlum = (!System.Convert.IsDBNull(dtKavim.Rows[0]["MazanTashlum"])) ? int.Parse(dtKavim.Rows[0]["MazanTashlum"].ToString()) : 0;
                            //iSugKnisa = int.Parse(dtKavim.Rows[0]["SugKnisa"].ToString());
                            cls.sMakatDescription = dtKavim.Rows[0]["Description"].ToString();
                            cls.sShilut = dtKavim.Rows[0]["Shilut"].ToString();
                            cls.sSugShirutName = COL_TRIP_NAMAK;//dtKavim.Rows[0]["SugShirutName"].ToString();
                            cls.fKm = float.Parse(dtKavim.Rows[0]["KM"].ToString());
                            cls.iOnatiyut = (System.Convert.IsDBNull(dtKavim.Rows[0]["Onatiut"]) ? 0 : int.Parse(dtKavim.Rows[0]["Onatiut"].ToString()));

                        }
                        break;
                    case enMakatType.mElement:
                        iKodElement = int.Parse(lMakatNesia.ToString().Substring(1, 2));
                        drElementim = cls.dtElementim.Select(string.Concat("KOD_ELEMENT=", iKodElement));
                        cls.iMakatValid = ((drElementim.Length > 0) ? 0 : 1);
                        sMakatNesia = lMakatNesia.ToString().Substring(0, 3);
                        cls.sMakatDescription = drElementim[0]["teur_element"].ToString();
                        cls.bElementHachanatMechona = ((sMakatNesia.Equals(enElementHachanatMechona.Element701.GetHashCode().ToString()))
                                                    || (sMakatNesia.Equals(enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                    || (sMakatNesia.Equals(enElementHachanatMechona.Element712.GetHashCode().ToString())));

                        cls.sSugShirutName = COL_TRIP_ELEMENT;
                        break;
                    default:
                        cls.iMakatValid = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetDTElementim(DateTime dTaarich)
        {
            //DataTable dtElementim = new DataTable();
            //var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
            //dtElementim = kavimDal.GetMeafyeneyElementByKod(0, dTaarich);
            //return dtElementim;

            var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
            return  cache.GetCacheItem<DataTable>(CachedItems.Elementim);
        }

        public bool IsDuplicateTravle(int iMisparIshi, DateTime dCardDate, long lMakatNesia, DateTime dShatYetzia, int iMisparKnisa, ref DataTable dt)
        {
            return _container.Resolve<IPeilutDAL>().IsDuplicateTravle(iMisparIshi, dCardDate, lMakatNesia, dShatYetzia, iMisparKnisa, ref dt);
        }


        public DataTable GetTmpMeafyeneyElements(DateTime dTarMe, DateTime dTarAd)
        {
            return _container.Resolve<IPeilutDAL>().GetTmpMeafyeneyElements(dTarMe, dTarAd);
        }

    }
}

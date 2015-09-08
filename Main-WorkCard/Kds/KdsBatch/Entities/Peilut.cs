﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;
using System.Configuration;
using System.Web;
using KdsBatch.Errors;
using KDSCommon.Helpers;
using KDSCommon.Enums;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;

namespace KdsBatch.Entities
{
    public class Peilut : BasicErrors
    {
        public int iMispar_siduri;
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
        public bool bElementLershutExists;
        public enMakatType MakatType;
        public bool bElementHachanatMechona = false;
        public Sidur objSidur;

        public Peilut() : base(OriginError.Peilut) { }

        public Peilut(DataRow dr, Sidur oSidur)  : base(OriginError.Peilut)
        {
            objSidur = oSidur;
            dCardDate = oSidur.dSidurDate;
            //נתוני פעילויות       
            iPeilutMisparSidur = (System.Convert.IsDBNull(dr["peilut_mispar_sidur"]) ? 0 : int.Parse(dr["peilut_mispar_sidur"].ToString()));
            iKisuyTor = (System.Convert.IsDBNull(dr["Kisuy_Tor"]) ? 0 : int.Parse(dr["Kisuy_Tor"].ToString()));
            lMakatNesia = (System.Convert.IsDBNull(dr["Makat_Nesia"]) ? 0 : long.Parse(dr["Makat_Nesia"].ToString()));
            dFullShatYetzia = (System.Convert.IsDBNull(dr["Shat_Yetzia"]) ? new DateTime(199, 1, 1) : DateTime.Parse(dr["Shat_Yetzia"].ToString()));
            sShatYetzia = (System.Convert.IsDBNull(dr["Shat_Yetzia"]) ? "" : DateTime.Parse(dr["Shat_Yetzia"].ToString()).ToString("HH:mm"));
            lOtoNo = (System.Convert.IsDBNull(dr["Oto_No"]) ? 0 : long.Parse(dr["Oto_No"].ToString()));
            lMisparSiduriOto = (System.Convert.IsDBNull(dr["mispar_siduri_oto"]) ? 0 : int.Parse(dr["mispar_siduri_oto"].ToString()));
            iElementLeYedia = (System.Convert.IsDBNull(dr["element_for_yedia"]) ? 0 : int.Parse(dr["element_for_yedia"].ToString()));
            iErechElement = (System.Convert.IsDBNull(dr["erech_element"]) ? 0 : int.Parse(dr["erech_element"].ToString()));

            iMisparSidurMatalotTnua = (System.Convert.IsDBNull(dr["mispar_sidur_matalot_tnua"]) ? 0 : int.Parse(dr["mispar_sidur_matalot_tnua"].ToString()));
            bMisparSidurMatalotTnuaExists = !String.IsNullOrEmpty(dr["mispar_sidur_matalot_tnua"].ToString());
         //   sBusNumberMust = dr["bus_number_must"].ToString();
            bBusNumberMustExists = !(String.IsNullOrEmpty(dr["bus_number_must"].ToString()));
           // sElementHamtana = dr["element_hamtana"].ToString();
            bElementHamtanaExists = !String.IsNullOrEmpty(dr["element_hamtana"].ToString());
            //sElementIgnoreHafifaBetweenNesiot = dr["lehitalem_hafifa_bein_nesiot"].ToString();
            //bElementIgnoreHafifaBetweenNesiotExists = !String.IsNullOrEmpty(dr["lehitalem_hafifa_bein_nesiot"].ToString());
            //sElementIgnoreReka = dr["lehitalem_beitur_reyka"].ToString();
            //bElementIgnoreReka = !String.IsNullOrEmpty(dr["lehitalem_beitur_reyka"].ToString());

            sElementZviraZman = dr["element_zvira_zman"].ToString();
            sElementNesiaReka = dr["nesia_reika"].ToString();
            sElementInMinutes = dr["element_in_minutes"].ToString();
            sKodLechishuvPremia = dr["kod_lechishuv_premia"].ToString();
            sLoNitzbarLishatGmar = dr["lo_nizbar_leshat_gmar"].ToString();
            bElementLershutExists = !String.IsNullOrEmpty(dr["Lershut"].ToString());
            lMisparMatala = (System.Convert.IsDBNull(dr["mispar_matala"]) ? 0 : int.Parse(dr["mispar_matala"].ToString()));
            //sBitulBiglalIchurLasidur = dr["bitul_biglal_ichur_lasidur"].ToString();
            bBitulBiglalIchurLasidurExists = !String.IsNullOrEmpty(dr["bitul_biglal_ichur_lasidur"].ToString());
            lMisparVisa = (System.Convert.IsDBNull(dr["mispar_visa"]) ? 0 : long.Parse(dr["mispar_visa"].ToString()));
            sDivuchInSidurVisa = dr["divuch_in_sidur_visa"].ToString();
            sDivuchInSidurMeyuchad = dr["divuach_besidur_meyuchad"].ToString();
            iMisparKnisa = (System.Convert.IsDBNull(dr["mispar_knisa"]) ? 0 : int.Parse(dr["mispar_knisa"].ToString()));
            bImutNetzer = System.Convert.IsDBNull(dr["imut_netzer"]) ? false : true;
            iBitulOHosafa = System.Convert.IsDBNull(dr["peilut_bitul_o_hosafa"]) ? 0 : int.Parse(dr["peilut_bitul_o_hosafa"].ToString());
            iDakotBafoal = System.Convert.IsDBNull(dr["DAKOT_BAFOAL"]) ? 0 : int.Parse(dr["DAKOT_BAFOAL"].ToString());
            iKmVisa = System.Convert.IsDBNull(dr["KM_VISA"]) ? 0 : int.Parse(dr["KM_VISA"].ToString());
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
            SetKavDetails();
            InitializeErrors();
        }
        public Peilut(int iMisparIshi, DateTime dDateCard, Peilut oPeilutOld, long lMakatNesiaNew, DataTable dtMeafyeneyElements)
            : base(OriginError.Peilut)
        {
            DataTable dtPeiluyot;
            clUtils oUtils = new clUtils();
            try
            {
            lMakatNesia = lMakatNesiaNew;
            dCardDate = dDateCard;
            iPeilutMisparSidur = oPeilutOld.iPeilutMisparSidur;
            iKisuyTor = oPeilutOld.iKisuyTor;
            dFullShatYetzia =oPeilutOld.dFullShatYetzia;
            sShatYetzia = oPeilutOld.sShatYetzia;
            
            lOtoNo =oPeilutOld.lOtoNo;
            lMisparSiduriOto =oPeilutOld.lMisparSiduriOto;

            lMisparVisa = oPeilutOld.lMisparVisa;
            iMisparKnisa = oPeilutOld.iMisparKnisa;
            bImutNetzer = oPeilutOld.bImutNetzer;
            iBitulOHosafa = oPeilutOld.iBitulOHosafa;
            iDakotBafoal = oPeilutOld.iDakotBafoal;
            iKmVisa = oPeilutOld.iKmVisa;
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

            SetKavDetails();

            if ((enMakatType)iMakatType == enMakatType.mElement)
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
                    bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
                    bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
                   
                    sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
                    sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
                    sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
                    sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
                    sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();    
                       
                    bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
                    iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());
            
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

        public Peilut(int iMisparIshi, DateTime dDateCard, OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, DataTable dtMeafyeneyElements)
            : base(OriginError.Peilut)
        {
            DataTable dtPeiluyot;
            clUtils oUtils = new clUtils();
            string sCacheKey = "ElementsTable";
           
            //try
            //{
            //    dtElementim = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
            //}
            //catch (Exception ex)
            //{
            //    dtElementim = null;
            //}

            //if (dtElementim == null)
            //{
            //    dtElementim = oUtils.GetCtbElementim();
            //    HttpRuntime.Cache.Insert(sCacheKey, dtElementim, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
            //}

            try
            {
               

                lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                dCardDate = dDateCard;
                iPeilutMisparSidur = oObjPeilutOvdimIns.MISPAR_SIDUR;
                dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                sShatYetzia = dFullShatYetzia.ToString("HH:mm");

                lMisparVisa = oObjPeilutOvdimIns.MISPAR_VISA;
                iMisparKnisa = oObjPeilutOvdimIns.MISPAR_KNISA;
                lMisparSiduriOto = oObjPeilutOvdimIns.MISPAR_SIDURI_OTO;
                lMisparMatala = oObjPeilutOvdimIns.MISPAR_MATALA;
                lOtoNo = oObjPeilutOvdimIns.OTO_NO;
                dtPeiluyot = clDefinitions.GetPeiluyotFromTnua(iMisparIshi, dDateCard);

                SetKavDetails();

                if ((enMakatType)iMakatType == enMakatType.mElement)
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
                        bBusNumberMustExists = !(String.IsNullOrEmpty(drMeafyeneyElements["bus_number_must"].ToString()));
                        bElementHamtanaExists = !String.IsNullOrEmpty(drMeafyeneyElements["element_hamtana"].ToString());
                      
                        sElementZviraZman = drMeafyeneyElements["element_zvira_zman"].ToString();
                        sElementNesiaReka = drMeafyeneyElements["nesia_reika"].ToString();
                        sElementInMinutes = drMeafyeneyElements["element_in_minutes"].ToString();
                        sKodLechishuvPremia = drMeafyeneyElements["kod_lechishuv_premia"].ToString();
                        sLoNitzbarLishatGmar = drMeafyeneyElements["lo_nizbar_leshat_gmar"].ToString();    
                        bElementLershutExists = !String.IsNullOrEmpty(drMeafyeneyElements["Lershut"].ToString());
                        iElementLeShatGmar = System.Convert.IsDBNull(drMeafyeneyElements["peilut_mashmautit"]) ? 0 : int.Parse(drMeafyeneyElements["peilut_mashmautit"].ToString());
            
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

        private DataTable GetCatalog(long lMakatNasia, DateTime dCardDate, enMakatType oMakatType, ref int iMakatValid)
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

        public bool IsMustBusNumber()
        {
            try
            {
                return ((iMakatType == enMakatType.mVisa.GetHashCode() || iMakatType == enMakatType.mKavShirut.GetHashCode() || iMakatType == enMakatType.mNamak.GetHashCode() || iMakatType == enMakatType.mEmpty.GetHashCode()
                   || (iMakatType == enMakatType.mElement.GetHashCode() && (bBusNumberMustExists || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700") && !bElementHachanatMechona)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetKavDetails()
        {
            DataRow[] dr;
            iMakatType = StaticBL.GetMakatType(lMakatNesia);
            enMakatType oMakatType;
            oMakatType = (enMakatType)iMakatType;
            DataTable dtVisut = new DataTable();
            string sMakatNesia;
            
            if(iMakatType !=0)
                MakatType = (enMakatType)Enum.Parse(typeof(enMakatType), iMakatType.ToString());

            switch (oMakatType)
            {
                case enMakatType.mKavShirut:
                    dr = objSidur.objDay.oOved.dtPeiluyotTnua.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            if (dr[0]["EILAT_TRIP"] != null)
                            {
                                if (dr[0]["EILAT_TRIP"].ToString() == "1")
                                {
                                    //אם לפחות אחת מהפעילויות היא פעילות אילת, נסמן את הסידור כסידור אילת
                                    bPeilutEilat = true;
                                    objSidur.bSidurEilat = true;
                                }
                            }
                          //  iEilatTrip = (!System.Convert.IsDBNull(dr[0]["EILAT_TRIP"])) ? int.Parse(dr[0]["EILAT_TRIP"].ToString()) : 0;
                            iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                         //   iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            sShilut = dr[0]["SHILUT"].ToString();
                          //  sSugShirutName = dr[0]["SUG_SHIRUT_NAME"].ToString();
                            iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));

                          //  fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                            iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                        }
                        else
                            iMakatValid = 1;
                    }

                    break;
                case enMakatType.mEmpty:
                    dr = objSidur.objDay.oOved.dtPeiluyotTnua.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                         //   iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                            sShilut = "";
                          //  sSugShirutName = COL_TRIP_EMPTY;
                            iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));

                          //  fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                        }
                        else
                            iMakatValid = 1;
                    }
                    bPeilutNotRekea = false;

                    break;
                case enMakatType.mNamak:
                    dr = objSidur.objDay.oOved.dtPeiluyotTnua.Select("MAKAT8=" + lMakatNesia);
                    if (dr.Length > 0)
                    {
                        if (dr[0]["STATUS"].ToString() == "0")
                        {
                            iXyMokedTchila = (System.Convert.IsDBNull(dr[0]["xy_moked_tchila"]) ? 0 : int.Parse(dr[0]["xy_moked_tchila"].ToString()));
                          //  iXyMokedSiyum = (System.Convert.IsDBNull(dr[0]["xy_moked_siyum"]) ? 0 : int.Parse(dr[0]["xy_moked_siyum"].ToString()));
                            iMazanTichnun = (!System.Convert.IsDBNull(dr[0]["MAZAN_TICHNUN"])) ? int.Parse(dr[0]["MAZAN_TICHNUN"].ToString()) : 0;
                            iMazanTashlum = (!System.Convert.IsDBNull(dr[0]["MAZAN_TASHLUM"])) ? int.Parse(dr[0]["MAZAN_TASHLUM"].ToString()) : 0;
                            sMakatDescription = dr[0]["DESCRIPTION"].ToString();
                            iSnifAchrai = (System.Convert.IsDBNull(dr[0]["snif"]) ? 0 : int.Parse(dr[0]["snif"].ToString()));
                            iKisuyTorMap = (!System.Convert.IsDBNull(dr[0]["kisui_tor"])) ? int.Parse(dr[0]["kisui_tor"].ToString()) : 0;
                            sShilut = dr[0]["SHILUT"].ToString();
                            iOnatiyut = (System.Convert.IsDBNull(dr[0]["Onatiut"]) ? 0 : int.Parse(dr[0]["Onatiut"].ToString()));

                        //    sSugShirutName = COL_TRIP_NAMAK;
                        //    fKm = (!System.Convert.IsDBNull(dr[0]["KM"])) ? float.Parse(dr[0]["KM"].ToString()) : 0;
                        }
                        else
                            iMakatValid = 1;
                    }
                    break;
                case enMakatType.mElement:
                  
                    int iKodElement;
                    DataRow[] drElementim;
                    iMazanTichnun = int.Parse(lMakatNesia.ToString().Substring(3, 3));
                    iMazanTashlum = int.Parse(lMakatNesia.ToString().Substring(3, 3));
                    iKodElement = int.Parse(lMakatNesia.ToString().Substring(1, 2));
                    drElementim = GlobalData.dtElementim.Select(string.Concat("KOD_ELEMENT=", iKodElement));

                    if (iKodElement != 0)
                        iMakatValid = ((drElementim.Length > 0) ? 0 : 1);

                    sMakatNesia = lMakatNesia.ToString().Substring(0, 3);
                    sMakatDescription = ((drElementim.Length > 0) ? drElementim[0]["teur_element"].ToString() : "");
                    bElementHachanatMechona = ((sMakatNesia.Equals(enElementHachanatMechona.Element701.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(enElementHachanatMechona.Element712.GetHashCode().ToString())));

                  //  sSugShirutName = "";
                    break;
                case enMakatType.mVisut:
                    var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                    dtVisut = kavimDal.GetVisutDetails(lMakatNesia);
                    if (dtVisut.Rows.Count > 0)
                        sMakatDescription = dtVisut.Rows[0]["teur_visut"].ToString();

                 //   sSugShirutName = COL_TRIP_VISUT;
                    iMakatType = enMakatType.mElement.GetHashCode(); //5-Element  
                    break;
                default:
                    iMakatValid = 1;
                    break;
            }
        }
    }
}
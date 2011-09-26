using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;
using System.Configuration;
using System.Web;

namespace KdsBatch.Entities
{
    public class Peilut
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
        public bool bElementLershutExists;
        private DataTable dtElementim;
        public bool bElementHachanatMechona = false;
       
        public Peilut(){}

        public Peilut(int iMisparIshi,DateTime dDateCard,Peilut oPeilutOld,long lMakatNesiaNew,DataTable dtMeafyeneyElements) 
        {
            DataTable dtPeiluyot;
            clUtils oUtils = new clUtils();
            try
            {
            dtElementim = oUtils.GetCtbElementim();
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

        public Peilut(int iMisparIshi, DateTime dDateCard, KdsLibrary.UDT.OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, DataTable dtMeafyeneyElements)
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

        public bool IsMustBusNumber()
        {
            try
            {
                return ((iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || iMakatType == clKavim.enMakatType.mEmpty.GetHashCode()
                   || (iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (bBusNumberMustExists || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700") && !bElementHachanatMechona)));
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
            DataTable dtVisut = new DataTable();
            string sMakatNesia;       
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
                case clKavim.enMakatType.mEmpty:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
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
                case clKavim.enMakatType.mNamak:
                    dr = dtPeiluyot.Select("MAKAT8=" + lMakatNesia);
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
                    sMakatDescription = ((drElementim.Length > 0) ? drElementim[0]["teur_element"].ToString() : "");
                    bElementHachanatMechona = ((sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                || (sMakatNesia.Equals(KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString())));

                  //  sSugShirutName = "";
                    break;
                case clKavim.enMakatType.mVisut:
                    dtVisut = _Kavim.GetVisutDetails(lMakatNesia);
                    if (dtVisut.Rows.Count > 0)
                        sMakatDescription = dtVisut.Rows[0]["teur_visut"].ToString();

                 //   sSugShirutName = COL_TRIP_VISUT;
                    iMakatType = clKavim.enMakatType.mElement.GetHashCode(); //5-Element  
                    break;
                default:
                    iMakatValid = 1;
                    break;
            }
        }
    }
}

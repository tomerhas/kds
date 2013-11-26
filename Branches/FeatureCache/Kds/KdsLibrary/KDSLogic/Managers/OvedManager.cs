using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KDSCommon.DataModels;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.DAL;

namespace KdsLibrary
{
    public class OvedManager : IOvedManager
    {
        private IUnityContainer _container;
        public OvedManager(IUnityContainer container)
        {
            _container = container;
        }

        public OvedYomAvodaDetailsDM CreateOvedDetails(int iMisparIshi, DateTime dDate)
        {
            var dtOvedCardDetails = _container.Resolve<IOvedDAL>().GetOvedYomAvodaDetails(iMisparIshi, dDate);
            if (dtOvedCardDetails.Rows.Count > 0)
            {
                OvedYomAvodaDetailsDM ovedYomAvodaDatails = new OvedYomAvodaDetailsDM();
                SetMeafyneyOved(ovedYomAvodaDatails, dtOvedCardDetails);
                return ovedYomAvodaDatails;
            }
            return null;
        }

        private void SetMeafyneyOved(OvedYomAvodaDetailsDM ovedYomAvodaDatails, DataTable dtOvedCardDetails)
        {
            try
            {
                //נתונים כללים               
                //נוציא את שדה הלבשה ברמת יום עבודה
                if (dtOvedCardDetails.Rows[0]["halbasha"] != null)
                {
                    ovedYomAvodaDatails.sHalbasha = dtOvedCardDetails.Rows[0]["halbasha"].ToString();
                }
                if (dtOvedCardDetails.Rows[0]["Hamara"] != null)
                {
                    ovedYomAvodaDatails.sHamara = dtOvedCardDetails.Rows[0]["Hamara"].ToString();
                }

                //קוד מעמד
                ovedYomAvodaDatails.sKodMaamd = dtOvedCardDetails.Rows[0]["maamad"].ToString();
                if (!string.IsNullOrEmpty(ovedYomAvodaDatails.sKodMaamd))
                {
                    ovedYomAvodaDatails.sKodHaver = ovedYomAvodaDatails.sKodMaamd.Substring(0, 1);
                }

                ovedYomAvodaDatails.sBitulZmanNesiot = dtOvedCardDetails.Rows[0]["bitul_zman_nesiot"].ToString();
                ovedYomAvodaDatails.sTachograf = dtOvedCardDetails.Rows[0]["Tachograf"].ToString();
                ovedYomAvodaDatails.iKodHevra = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["kod_hevra"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["kod_hevra"].ToString());
                ovedYomAvodaDatails.sLina = dtOvedCardDetails.Rows[0]["lina"].ToString();
                ovedYomAvodaDatails.sMercazErua = dtOvedCardDetails.Rows[0]["mercaz_erua"].ToString();
                ovedYomAvodaDatails.bMercazEruaExists = !(String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["mercaz_erua"].ToString()));
                ovedYomAvodaDatails.sHashlamaLeyom = dtOvedCardDetails.Rows[0]["Hashlama_Leyom"].ToString();
                ovedYomAvodaDatails.iSibatHashlamaLeyom = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"].ToString());
                ovedYomAvodaDatails.iMisparIshi = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mispar_ishi"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mispar_ishi"].ToString());
                ovedYomAvodaDatails.iIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Isuk"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Isuk"].ToString());
                ovedYomAvodaDatails.iZmanMutamut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["dakot_mutamut"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["dakot_mutamut"].ToString());
                ovedYomAvodaDatails.sMutamut = dtOvedCardDetails.Rows[0]["mutaam"].ToString();
                ovedYomAvodaDatails.bMutamutExists = !String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["mutaam"].ToString());
                ovedYomAvodaDatails.sSidurDay = dtOvedCardDetails.Rows[0]["iDay"].ToString();
                ovedYomAvodaDatails.sShabaton = dtOvedCardDetails.Rows[0]["shbaton"].ToString();
                ovedYomAvodaDatails.sErevShishiChag = dtOvedCardDetails.Rows[0]["erev_shishi_chag"].ToString();
                ovedYomAvodaDatails.sRishyonAutobus = dtOvedCardDetails.Rows[0]["rishyon_autobus"].ToString().Trim();
                ovedYomAvodaDatails.sShlilatRishayon = dtOvedCardDetails.Rows[0]["shlilat_rishayon"].ToString();
                ovedYomAvodaDatails.iZmanNesiaHaloch = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"].ToString());
                ovedYomAvodaDatails.iZmanNesiaHazor = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"].ToString());
                ovedYomAvodaDatails.iStatus = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Status"]) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["Status"].ToString());
                ovedYomAvodaDatails.iStatusTipul = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["status_tipul"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["status_tipul"].ToString());
                ovedYomAvodaDatails.sStatusCardDesc = dtOvedCardDetails.Rows[0]["teur_status_kartis"].ToString();
                ovedYomAvodaDatails.sDayTypeDesc = dtOvedCardDetails.Rows[0]["teur_yom"].ToString();
                ovedYomAvodaDatails.iMeasherOMistayeg = String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString()) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString());
                ovedYomAvodaDatails.iSnifAv = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["snif_av"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["snif_av"].ToString());
                ovedYomAvodaDatails.iKodSectorIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"].ToString());
                ovedYomAvodaDatails.iSnifTnua = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Snif_Tnua"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Snif_Tnua"].ToString());
                ovedYomAvodaDatails.iBechishuvSachar = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["BECHISHUV_SACHAR"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["BECHISHUV_SACHAR"].ToString());
                ovedYomAvodaDatails.iMikumYechida = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mikum_yechida"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mikum_yechida"].ToString());
                ovedYomAvodaDatails.iMikumYechidaLenochechut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_MIKUM_YECHIDA_AV_LENOCH"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_MIKUM_YECHIDA_AV_LENOCH"].ToString());
                ovedYomAvodaDatails.dTaarichMe = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["me_tarich"]) ? DateTime.MinValue : DateTime.Parse(dtOvedCardDetails.Rows[0]["me_tarich"].ToString());
                ovedYomAvodaDatails.dTaarichAd = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["ad_tarich"]) ? DateTime.MinValue : DateTime.Parse(dtOvedCardDetails.Rows[0]["ad_tarich"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate)
        {
            return _container.Resolve<IOvedDAL>().GetOvedDetails(iMisparIshi, dCardDate);
        }
    }
}


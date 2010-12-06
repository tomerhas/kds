using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;

namespace KdsBatch
{
    public class clOvedYomAvoda
    {
        public string sKodMaamd;
        public string sHalbasha = "";
        public string sHamara = "";
        public string sBitulZmanNesiot;
        public string sTachograf = "";
        public string sKodHaver = "";
        public int iKodHevra;
        public string sMutamut = "";
        public bool bMutamutExists;
        public string sMercazErua = "";
        public bool bMercazEruaExists;
        public int iZmanMutamut = 0;
        public int iZmanNesiaHaloch;
        public int iZmanNesiaHazor;
        public int iStatus;
        public int iStatusTipul;
        public string sStatusCardDesc;
        public string sDayTypeDesc; //תאור סוג יום
        public int iMeasherOMistayeg; //מאשר או מסתייג, אם התקבל ערך NULL ייכנס -1

        //public string sMeafyen3 = "";
        //public string sMeafyen4 = "";
        //public string sMeafyen7 = "";
        //public string sMeafyen61 = "";
        //public string sMeafyen51 = "";
        //public string sMeafyen64 = "";
        //public string sMeafyen72 = "";
        public string sHashlamaLeyom = "";
        public int iSibatHashlamaLeyom;
        public string sLina = "";
        public int iMisparIshi;
        public int iIsuk;
        public int iSnifAv;
        public int iKodSectorIsuk;
        public string sSidurDay;
        public string sShabaton;
        public string sErevShishiChag;
        public string sRishyonAutobus;
        public string sShlilatRishayon;
        public int iSnifTnua;

        private DataTable dtOvedCardDetails;
        private bool _bOvedDetailsExists = false;

        public clOvedYomAvoda(int iMisparIshi, DateTime dDate)
        {
            dtOvedCardDetails = GetOvedYomAvodaDetails(iMisparIshi, dDate);
            if (dtOvedCardDetails.Rows.Count > 0)
            {
                SetMeafyneyOved();
                _bOvedDetailsExists = true;
            }

        }


        public bool OvedDetailsExists
        {
            get { return _bOvedDetailsExists; }
        }

        private void SetMeafyneyOved()
        {
            try
            {
                //נתונים כללים               
                //נוציא את שדה הלבשה ברמת יום עבודה
                if (dtOvedCardDetails.Rows[0]["halbasha"] != null)
                {
                    sHalbasha = dtOvedCardDetails.Rows[0]["halbasha"].ToString();
                }
                if (dtOvedCardDetails.Rows[0]["Hamara"] != null)
                {
                    sHamara = dtOvedCardDetails.Rows[0]["Hamara"].ToString();
                }

                //קוד מעמד
                sKodMaamd = dtOvedCardDetails.Rows[0]["maamad"].ToString();
                if (!string.IsNullOrEmpty(sKodMaamd))
                {
                    sKodHaver = sKodMaamd.Substring(0, 1);
                }

                //sMeafyen3 = dtOvedCardDetails.Rows[0]["meafyen3"].ToString();
                //sMeafyen4 = dtOvedCardDetails.Rows[0]["meafyen4"].ToString();
                //sMeafyen7 = dtOvedCardDetails.Rows[0]["meafyen7"].ToString();
                //sMeafyen61 = dtOvedCardDetails.Rows[0]["meafyen61"].ToString();
                //sMeafyen51 = dtOvedCardDetails.Rows[0]["meafyen51"].ToString();
                ////עובד במרכז נ.צ.ר
                //sMeafyen64 = dtOvedCardDetails.Rows[0]["meafyen64"].ToString();
                //sMeafyen72 = dtOvedCardDetails.Rows[0]["meafyen72"].ToString();
                sBitulZmanNesiot = dtOvedCardDetails.Rows[0]["bitul_zman_nesiot"].ToString();
                sTachograf = dtOvedCardDetails.Rows[0]["Tachograf"].ToString();
                iKodHevra = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["kod_hevra"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["kod_hevra"].ToString());
                sLina = dtOvedCardDetails.Rows[0]["lina"].ToString();
                sMercazErua = dtOvedCardDetails.Rows[0]["mercaz_erua"].ToString();
                bMercazEruaExists = !(String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["mercaz_erua"].ToString()));
                sHashlamaLeyom = dtOvedCardDetails.Rows[0]["Hashlama_Leyom"].ToString();
                iSibatHashlamaLeyom = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"].ToString());
                iMisparIshi = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mispar_ishi"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mispar_ishi"].ToString());
                iIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Isuk"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Isuk"].ToString());
                iZmanMutamut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["dakot_mutamut"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["dakot_mutamut"].ToString());
                sMutamut = dtOvedCardDetails.Rows[0]["mutaam"].ToString();
                bMutamutExists = !String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["mutaam"].ToString());
                sSidurDay = dtOvedCardDetails.Rows[0]["iDay"].ToString();
                sShabaton = dtOvedCardDetails.Rows[0]["shbaton"].ToString();
                sErevShishiChag = dtOvedCardDetails.Rows[0]["erev_shishi_chag"].ToString();
                sRishyonAutobus = dtOvedCardDetails.Rows[0]["rishyon_autobus"].ToString().Trim();
                sShlilatRishayon = dtOvedCardDetails.Rows[0]["shlilat_rishayon"].ToString();
                iZmanNesiaHaloch = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"].ToString());
                iZmanNesiaHazor = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"].ToString());
                iStatus = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Status"]) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["Status"].ToString());
                iStatusTipul = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["status_tipul"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["status_tipul"].ToString());
                sStatusCardDesc = dtOvedCardDetails.Rows[0]["teur_status_kartis"].ToString();
                sDayTypeDesc = dtOvedCardDetails.Rows[0]["teur_yom"].ToString();
                iMeasherOMistayeg = String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString()) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString());
                iSnifAv = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["snif_av"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["snif_av"].ToString());
                iKodSectorIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"].ToString());
                iSnifTnua = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Snif_Tnua"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Snif_Tnua"].ToString());
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private DataTable GetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedYomAvodaDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetOvedYomAvodaDetails(DateTime taarich, int mispar_ishi, TimeSpan shat_hatchala, TimeSpan shat_gmar, int meadken_acharon)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleInteger, shat_hatchala, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_gmar", ParameterType.ntOracleDate, shat_gmar, ParameterDir.pdInput);
                oDal.AddParameter("p_meadken_acharon", ParameterType.ntOracleInteger, meadken_acharon, ParameterDir.pdInput);
                oDal.ExecuteSP(clDefinitions.cProUpdYameyAvodaOvdim);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


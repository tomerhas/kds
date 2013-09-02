using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KdsBatch.Entities
{
    public class Oved
    {
        public string sKodMaamd;
        public string sKodHaver = "";
        public int iZmanMutamut = 0;
        public int iKodHevra;
        public string sMutamut = "";
        public bool bMutamutExists;
        public string sMercazErua = "";
        public bool bMercazEruaExists;
        public int iMisparIshi;
        public int iIsuk;
        public int iSnifAv;
        public int iKodSectorIsuk;
        public int iSnifTnua;
        public int iMatzavOved = 0;
        public string sRishyonAutobus;
        public string sShlilatRishayon;
        public DateTime dCardDate;

        public DataTable dtIdkuneyRashemet;
      //  public DataTable dtTmpMeafyeneyElements;
        public DataTable dtOvedDetails;
        public DataTable dtSidurimVePeiluyot;
        public clMeafyenyOved oMeafyeneyOved;
        public DataTable dtPeiluyotTnua;
        public bool bOvedDetailsExists = false;
        public bool bSidurimExists = false;
        public Oved() { }

        public Oved(int iMisparIshi, DateTime dDate)
        {
            EntitiesDal oDal = new EntitiesDal();

            dCardDate = dDate;
            dtOvedDetails = oDal.GetOvedDetails(iMisparIshi, dDate);
            if (dtOvedDetails.Rows.Count > 0)
            {
                SetMeafyneyOved();
                bOvedDetailsExists = true;
            }
        }

        private void SetMeafyneyOved()
        {
            try
            {
                //קוד מעמד
                sKodMaamd = dtOvedDetails.Rows[0]["maamad"].ToString();
                if (!string.IsNullOrEmpty(sKodMaamd))
                {
                    sKodHaver = sKodMaamd.Substring(0, 1);
                }

                iKodHevra = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["kod_hevra"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["kod_hevra"].ToString());
                sMercazErua = dtOvedDetails.Rows[0]["mercaz_erua"].ToString();
                bMercazEruaExists = !(String.IsNullOrEmpty(dtOvedDetails.Rows[0]["mercaz_erua"].ToString()));
                iMisparIshi = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["mispar_ishi"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["mispar_ishi"].ToString());
                iIsuk = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["Isuk"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["Isuk"].ToString());
                iZmanMutamut = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["dakot_mutamut"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["dakot_mutamut"].ToString());
                sMutamut = dtOvedDetails.Rows[0]["mutaam"].ToString();
                bMutamutExists = !String.IsNullOrEmpty(dtOvedDetails.Rows[0]["mutaam"].ToString());
                iSnifAv = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["snif_av"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["snif_av"].ToString());
                iKodSectorIsuk = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["KOD_SECTOR_ISUK"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["KOD_SECTOR_ISUK"].ToString());
                iSnifTnua = System.Convert.IsDBNull(dtOvedDetails.Rows[0]["Snif_Tnua"]) ? 0 : int.Parse(dtOvedDetails.Rows[0]["Snif_Tnua"].ToString());
                sRishyonAutobus = dtOvedDetails.Rows[0]["rishyon_autobus"].ToString().Trim();
                sShlilatRishayon = dtOvedDetails.Rows[0]["shlilat_rishayon"].ToString();
               
                oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);
               

                SetDataTables();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetDataTables()
        {
            EntitiesDal oDal = new EntitiesDal();
            DataTable dtMatzavOved;
            dtIdkuneyRashemet = clDefinitions.GetIdkuneyRashemet(iMisparIshi, dCardDate);
            //dtTmpMeafyeneyElements = clDefinitions.GetTmpMeafyeneyElements(dCardDate, dCardDate);
            dtMatzavOved = oDal.GetOvedMatzav(iMisparIshi, dCardDate);
            if (dtMatzavOved.Rows.Count > 0)
            {
                int.TryParse(dtMatzavOved.Rows[0]["kod_matzav"].ToString(), out iMatzavOved);
                //iMatzavOved = int.Parse(dtMatzavOved.Rows[0]["kod_matzav"].ToString());
            }
            dtSidurimVePeiluyot = oDal.GetSidurimLeOved(iMisparIshi, dCardDate);
            if (dtSidurimVePeiluyot.Rows.Count>0)
                bSidurimExists=true;
            dtPeiluyotTnua = clDefinitions.GetPeiluyotFromTnua(iMisparIshi, dCardDate);
       
        }


        public bool IsOvedZakaiLZmanNesiaLaAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.Meafyen61Exists && oMeafyeneyOved.sMeafyen61.Substring(0, 1) == "1")
                   ||
                   (oMeafyeneyOved.Meafyen51Exists && oMeafyeneyOved.sMeafyen51.Substring(0, 1) == "1"));
        }

        public bool IsOvedZakaiLZmanNesiaMeAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 2 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.Meafyen61Exists && oMeafyeneyOved.sMeafyen61.Substring(0, 1) == "2")
                   ||
                   (oMeafyeneyOved.Meafyen51Exists && oMeafyeneyOved.sMeafyen51.Substring(0, 1) == "2"));
        }

        public bool IsOvedZakaiLZmanNesiaLeMeAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 3 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.Meafyen61Exists && oMeafyeneyOved.sMeafyen61.Substring(0, 1) == "3")
                   ||
                   (oMeafyeneyOved.Meafyen51Exists && oMeafyeneyOved.sMeafyen51.Substring(0, 1) == "3"));
        }

        public bool IsOvedInMatzav(string sMatzavim)
        {
            bool result = false;
            try
            {
                //return result;
                result = sMatzavim.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                               .Any(matzav =>int.Parse(matzav) == iMatzavOved);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool CheckIdkunRashemet(string sFieldToChange)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try
            {
                drIdkunim = dtIdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "'");
                if (drIdkunim.Length > 0)
                    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }
    }
}

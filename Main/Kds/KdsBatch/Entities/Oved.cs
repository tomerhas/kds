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

        public DataTable dtOvedDetails;
        public bool _bOvedDetailsExists = false;

        public bool OvedDetailsExists
        {
            get { return _bOvedDetailsExists; }
        }
        public Oved() { }

        public Oved(int iMisparIshi, DateTime dDate)
        {
            EntitiesDal oDal = new EntitiesDal();
            dtOvedDetails = oDal.GetOvedDetails(iMisparIshi, dDate);
            if (dtOvedDetails.Rows.Count > 0)
            {
                SetMeafyneyOved();
                _bOvedDetailsExists = true;
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}

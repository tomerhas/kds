using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;

namespace KdsBatch
{
   public class clPirteyOved
    {
        public int iMutamBitachon;
        public int iIsuk;
        public int iEzor;
        public int iGil = 0;
        public int iMutamut = 0;
        public int iZmanMutamut = 0;
        public int iKodMaamdRashi; 
        public int iKodMaamdMishni;
        public int iKodSectorIsuk;
        public int iYechidaIrgunit;
        public int iMikumYechida;
        public int iDirug;
        public int iDarga;
        public int iAchsana;
        public int iSnifAv;
        public bool bIsInShlila = false;

         private DataTable dtOvedCardDetails;

         public clPirteyOved(int iMisparIshi, DateTime dDate)
        {
            dtOvedCardDetails = GetPirteyOved(iMisparIshi, dDate);
            if (dtOvedCardDetails.Rows.Count > 0)
            {
                SetMeafyneyOved();
            }
            dtOvedCardDetails.Dispose();
        }

        private void SetMeafyneyOved()
        {
            try
            {
                //נתונים כללים               
               
                //קוד מעמד
                iKodMaamdRashi = int.Parse(dtOvedCardDetails.Rows[0]["maamad"].ToString().Substring(0,1));
                iKodMaamdMishni = int.Parse(dtOvedCardDetails.Rows[0]["maamad"].ToString().Substring(1, 2));

               iIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Isuk"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["Isuk"].ToString());
                iZmanMutamut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["dakot_mutamut"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["dakot_mutamut"].ToString());
                iMutamut = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mutaam"])? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mutaam"].ToString());
                iGil = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["gil"]) ? 0 : Int32.Parse(dtOvedCardDetails.Rows[0]["gil"].ToString());
               iMutamBitachon = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["mutaam_bitachon"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["mutaam_bitachon"].ToString());
                iEzor = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["ezor"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["ezor"].ToString());
                iKodSectorIsuk = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["KOD_SECTOR_ISUK"].ToString());
                iYechidaIrgunit = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["YECHIDA_IRGUNIT"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["YECHIDA_IRGUNIT"].ToString());
                iDirug = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["dirug"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["dirug"].ToString());
                iDarga = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["darga"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["darga"].ToString());
                iAchsana = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["achsana"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["achsana"].ToString());
                iSnifAv = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["snif_av"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["snif_av"].ToString());
                bIsInShlila = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["SHLILAT_RISHAYON"]) ? false : true;
                iMikumYechida = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["MIKUM_YECHIDA"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["MIKUM_YECHIDA"].ToString());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

         }


        private DataTable GetPirteyOved(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {//מחזיר נתוני עובד: 
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetTmpPirteyOved, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

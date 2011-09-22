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
        // public DateTime _Taarich;

         public DateTime _TaarichMe;
         public DateTime _TaarichAd;
         public clPirteyOved() { }
       
         public clPirteyOved(int iMisparIshi, DateTime dDate)
        {
            dtOvedCardDetails = GetPirteyOved(iMisparIshi, dDate);
            if (dtOvedCardDetails.Rows.Count > 0)
            {
                SetMeafyneyOved(dtOvedCardDetails.Rows[0]);
            }
            dtOvedCardDetails.Dispose();
            dtOvedCardDetails = null;
        }

         public clPirteyOved(int iMisparIshi, DateTime dDate,string type)
         {
              DataRow[] rows;
              try
              {
                  rows = clCalcData.DtPirteyOvedForMonth.Select("Convert('" + dDate.ToShortDateString() + "', 'System.DateTime') >= ME_TARICH and Convert('" + dDate.ToShortDateString() + "', 'System.DateTime')<= AD_TARICH");
                  if (rows.Length > 0)
                  {
                      SetMeafyneyOved(rows[0]);
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
         }

         public clPirteyOved(DataRow dr, DateTime dTaarich)
         {
             try
             {
                // _Taarich = dTaarich;
               
                 SetMeafyneyOved(dr);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
        private void SetMeafyneyOved(DataRow drPratim)
        {
            try
            {
                //נתונים כללים               
               
                //קוד מעמד
                iKodMaamdRashi = int.Parse(drPratim["maamad"].ToString().Substring(0, 1));
                iKodMaamdMishni = int.Parse(drPratim["maamad"].ToString().Substring(1, 2));

                iIsuk = System.Convert.IsDBNull(drPratim["Isuk"]) ? 0 : int.Parse(drPratim["Isuk"].ToString());
                iZmanMutamut = System.Convert.IsDBNull(drPratim["dakot_mutamut"]) ? 0 : int.Parse(drPratim["dakot_mutamut"].ToString());
                iMutamut = System.Convert.IsDBNull(drPratim["mutaam"]) ? 0 : int.Parse(drPratim["mutaam"].ToString());
                iGil = System.Convert.IsDBNull(drPratim["gil"]) ? 0 : Int32.Parse(drPratim["gil"].ToString());
                iMutamBitachon = System.Convert.IsDBNull(drPratim["mutaam_bitachon"]) ? 0 : int.Parse(drPratim["mutaam_bitachon"].ToString());
                iEzor = System.Convert.IsDBNull(drPratim["ezor"]) ? 0 : int.Parse(drPratim["ezor"].ToString());
                iKodSectorIsuk = System.Convert.IsDBNull(drPratim["KOD_SECTOR_ISUK"]) ? 0 : int.Parse(drPratim["KOD_SECTOR_ISUK"].ToString());
                iYechidaIrgunit = System.Convert.IsDBNull(drPratim["YECHIDA_IRGUNIT"]) ? 0 : int.Parse(drPratim["YECHIDA_IRGUNIT"].ToString());
                iDirug = System.Convert.IsDBNull(drPratim["dirug"]) ? 0 : int.Parse(drPratim["dirug"].ToString());
                iDarga = System.Convert.IsDBNull(drPratim["darga"]) ? 0 : int.Parse(drPratim["darga"].ToString());
                iAchsana = System.Convert.IsDBNull(drPratim["achsana"]) ? 0 : int.Parse(drPratim["achsana"].ToString());
                iSnifAv = System.Convert.IsDBNull(drPratim["snif_av"]) ? 0 : int.Parse(drPratim["snif_av"].ToString());
                bIsInShlila = System.Convert.IsDBNull(drPratim["SHLILAT_RISHAYON"]) ? false : true;
                iMikumYechida = System.Convert.IsDBNull(drPratim["MIKUM_YECHIDA"]) ? 0 : int.Parse(drPratim["MIKUM_YECHIDA"].ToString());

                _TaarichMe = DateTime.Parse(drPratim["ME_TARICH"].ToString());
                _TaarichAd = DateTime.Parse(drPratim["AD_TARICH"].ToString());

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

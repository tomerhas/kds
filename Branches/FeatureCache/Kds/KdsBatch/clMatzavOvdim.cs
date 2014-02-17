
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KdsBatch
{
   public class clMatzavOvdim
   {
      public string sKod_Matzav;
      public int iKod_Headrut;
      public DateTime _TaarichMe;
      public DateTime _TaarichAd;
         
      public clMatzavOvdim() { }

      public clMatzavOvdim(DataRow dr)
      {
            try
            {
                SetMatzavOved(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
       }
        private void SetMatzavOved(DataRow drMatzav)
        {
            try
            {
                sKod_Matzav =drMatzav["kod_matzav"].ToString();
                _TaarichMe = DateTime.Parse(drMatzav["TAARICH_ME"].ToString());
                _TaarichAd = DateTime.Parse(drMatzav["TAARICH_AD"].ToString());
                if (drMatzav["kod_headrut"].ToString() != "")
                    iKod_Headrut = int.Parse(drMatzav["kod_headrut"].ToString());
                else iKod_Headrut = 0;
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }

         }

   }
}
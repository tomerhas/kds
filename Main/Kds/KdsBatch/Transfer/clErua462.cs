using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua462:clErua
    {
      public clErua462(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,462)
      {
           _sBody = SetBody();
          PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua462 = new StringBuilder();
          float fErech = 0;
          try
          {
              
              fErech = clCalcData.GetSumErechRechiv(_dtDetailsChishuv.Compute("count(MISPAR_ISHI)", "MISPAR_ISHI=" + _iMisparIshi + " AND KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode() + " and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')"));

              sErua462.Append(FormatNumber(fErech, 4, 0));

              sErua462.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode()), 4, 0));
              sErua462.Append(FormatNumber((GetErechRechiv( clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode()) / 60), 4, 0));

              sErua462.Append(GetBlank(60));


           ListErua.Add(sErua462.ToString());
           return ListErua;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua462: " + ex.Message);
               throw ex;
           }
      }
    }
}

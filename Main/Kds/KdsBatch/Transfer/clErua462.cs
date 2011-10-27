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
      private DataTable dtChishuvYomi;
      public clErua462(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv,DataTable dtChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,462)
      {
          dtChishuvYomi = dtChishuv;
           _sBody = SetBody();
          if (_sBody!=null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua462 = new StringBuilder();
          DataRow[] drYamim;
        //  float fErech = 0;
          try
          {

            //  fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("count(TAARICH)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode() + " and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')"));
              drYamim = dtChishuvYomi.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode() + " and taarich>=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime') and taarich<=Convert('" + _dMonth.AddMonths(1).AddDays(-1).ToShortDateString() + "', 'System.DateTime')");
              sErua462.Append(FormatNumber(drYamim.Length, 4, 2));

              sErua462.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(),"erech_rechiv_a")/60), 4, 1));
              sErua462.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), "erech_rechiv_a") / 60), 4, 1));

              sErua462.Append(GetBlank(61));

              if (!IsEmptyErua(sErua462.ToString()))
              {
                  ListErua.Add(sErua462.ToString());
                  return ListErua;
              }else return null;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua462: " + ex.Message);
               throw ex;
           }
      }
    }
}

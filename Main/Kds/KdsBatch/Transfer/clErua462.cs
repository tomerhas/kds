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
          string sErech;
          try
          {

            //  fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("count(TAARICH)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode() + " and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')"));
              drYamim = dtChishuvYomi.Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode() + " and taarich>=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime') and taarich<=Convert('" + _dMonth.AddMonths(1).AddDays(-1).ToShortDateString() + "', 'System.DateTime')");
              sErua462.Append(FormatNumber(drYamim.Length, 4, 2));

              sErech = FormatNumber((GetErechRechiv(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), "erech_rechiv_a") / 60), 4, 1);
              sErech = ChangeLastSign(sErech);
              sErua462.Append(sErech);

              sErech = FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), "erech_rechiv_a") / 60), 4, 1);
              sErech = ChangeLastSign(sErech);
              sErua462.Append(sErech);
             
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

      private string ChangeLastSign(string sErech)
      {
        int iLastDigit;
        
        if (sErech.Trim() !="" && sErech.Length>0){
            iLastDigit = int.Parse(sErech.Substring(sErech.Length - 1, 1));
            sErech = sErech.Substring(0, sErech.Length - 1);
            sErech += GetSiman(iLastDigit);
        }
        return sErech;
      }

      private string GetSiman(int iLastDigit)
      {
          switch (iLastDigit)
          {
              case 0: return "{";
              case 1: return "A";
              case 2: return "B";
              case 3: return "C";
              case 4: return "D";
              case 5: return "E";
              case 6: return "F";
              case 7: return "G";
              case 8: return "H";
              case 9: return "I";
          }
          return "";
      }

    }
}

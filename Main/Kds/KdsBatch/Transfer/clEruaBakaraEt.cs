using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clEruaBakaraEt:clErua
    {
      public clEruaBakaraEt(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,162)
      {
          _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override void SetHeader()
      {
          StringBuilder sHeader = new StringBuilder();
          sHeader.Append("162;");
          sHeader.Append(_dMonth.Year.ToString().Substring(2, 2) + _dMonth.Month.ToString().PadLeft(2, char.Parse("0")) + ";");
          sHeader.Append(_iMisparIshi.ToString().PadLeft(9, char.Parse("0")) + ";");
          sHeader.Append(_drPirteyOved["TEUDAT_ZEHUT"].ToString().PadLeft(9, char.Parse("0")) + ";");
          _sHeader = sHeader.ToString();
      }

      protected override void SetFooter()
      {
          _sFooter = "";
      }
      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sBakaraEt = new StringBuilder();
          float fErech;
          try
          {
              sBakaraEt.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode()), 3, 0) + ";");
              fErech = GetErechRechiv(clGeneral.enRechivim.ShaotShabat100.GetHashCode());
              fErech += GetErechRechiv(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode());
              sBakaraEt.Append(FormatNumberWithPoint((fErech / 60), 6, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint((GetErechRechiv(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode())/60), 6, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint((GetErechRechiv(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode())/60), 6, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint((GetErechRechiv(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode())/60), 6, 2) + ";");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv( clGeneral.enRechivim.PremyaRegila.GetHashCode()), 11, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv( clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode()), 7, 2) + ";");
              sBakaraEt.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachPitzul.GetHashCode()), 2, 0) + ";");
              fErech = GetErechRechiv( clGeneral.enRechivim.ZmanLailaEgged.GetHashCode());
              fErech += GetErechRechiv( clGeneral.enRechivim.ZmanLailaChok.GetHashCode());
              sBakaraEt.Append(FormatNumberWithPoint((fErech/60), 5, 1) + ";");
              sBakaraEt.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode()), 2, 0) + ";");

              if (!IsEmptyErua(sBakaraEt.ToString()))
              {
                  ListErua.Add(sBakaraEt.ToString());
                  return ListErua;
              }
              else return null;
           }
           catch (Exception ex)
           {
               WriteError("clEruaBakaraEt: " + ex.Message);
               throw ex;
           }
      }
    }
}

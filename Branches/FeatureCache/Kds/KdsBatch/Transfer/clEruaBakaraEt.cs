using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;
using System.IO;

namespace KdsBatch
{
   
  public  class clEruaBakaraEt:clErua
    {
      protected string sFileName;

      public int TypeFile;
      public enum enTypeFile
      {
          Ragil = 1,
          Hefreshim = 2
      }

      public clEruaBakaraEt(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv, string sChodeshIbud)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,162)
      {
          if (sChodeshIbud == DateTime.Parse(_drPirteyOved["taarich"].ToString()).ToString("MM/yyyy"))
              TypeFile = enTypeFile.Ragil.GetHashCode();
          else TypeFile = enTypeFile.Hefreshim.GetHashCode();

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
              sBakaraEt.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), "erech_rechiv_a"), 3, 0) + ";");
              fErech = GetErechRechiv(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), "erech_rechiv_a");
              fErech += GetErechRechiv(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), "erech_rechiv_a");
              sBakaraEt.Append(FormatNumberWithPoint((fErech / 60), 6, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint((GetErechRechiv(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), "erech_rechiv_a")/60), 6, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint((GetErechRechiv(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), "erech_rechiv_a") / 60), 6, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint((GetErechRechiv(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), "erech_rechiv_a") / 60), 6, 2) + ";");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append("000.00;");
              sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(clGeneral.enRechivim.PremyaRegila.GetHashCode(), "erech_rechiv_a"), 11, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), "erech_rechiv_a"), 7, 2) + ";");
              sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(clGeneral.enRechivim.SachPitzul.GetHashCode(), "erech_rechiv_a"),4, 1) + ";");
              fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), "erech_rechiv_a");
              fErech += GetErechRechiv(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), "erech_rechiv_a");
              sBakaraEt.Append(FormatNumberWithPoint((fErech/60), 5, 1) + ";");
              sBakaraEt.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), "erech_rechiv_a"), 2, 0) + ";");
              sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(clGeneral.enRechivim.ETPaarBetweenMichsaRegilaAndMuktenet.GetHashCode(), "erech_rechiv_a")/60, 6, 2));

              if (!IsEmptyErua(sBakaraEt.ToString().Replace(";", "").Replace(".", "")) )
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

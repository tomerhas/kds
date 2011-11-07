using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua413:clErua
    {
      private List<string> _ListErua;

      public clErua413(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,413)
      {
          _sBody = SetBody();
          if (_sBody != null)
          PrepareLines();
      }

      protected override void SetHeader()
      {
          StringBuilder sStart = new StringBuilder();
         
          sStart.Append("413");
          sStart.Append(_drPirteyOved["mifal"].ToString().PadLeft(4, char.Parse("0")));
          sStart.Append("000");
          sStart.Append(_drPirteyOved["mispar_ishi"].ToString().PadLeft(5, char.Parse("0")));
          sStart.Append(_drPirteyOved["sifrat_bikoret"].ToString());
          sStart.Append(_drPirteyOved["shem_mish"].ToString().PadLeft(10).Substring(0, 10));
          sStart.Append(_drPirteyOved["shem_prat"].ToString().PadLeft(7).Substring(0, 7));
          sStart.Append("000000");
          sStart.Append(GetBlank(6));
          _sHeader = sStart.ToString();

      }

      protected override void SetFooter()
      {
          StringBuilder sEnd = new StringBuilder();

          sEnd.Append(_dMonth.Month.ToString().PadLeft(2, char.Parse("0")));
          sEnd.Append(_dMonth.Year.ToString());
          sEnd.Append(GetBlank(37));
          _sFooter = sEnd.ToString();      
      }

      protected override List<string> SetBody()
      {
          _ListErua = new List<string>();
         
          try
          {
              
              CreateData413( "224", clGeneral.enRechivim.Kizuz100.GetHashCode(),  7, 2);
              CreateData413( "221", clGeneral.enRechivim.Kizuz125.GetHashCode(),  7, 2);
              CreateData413( "222", clGeneral.enRechivim.Kizuz150.GetHashCode(),  7, 2);
              CreateData413( "223", clGeneral.enRechivim.Kizuz200.GetHashCode(),  7, 2);
              CreateData413( "321", clGeneral.enRechivim.PremiaManasim.GetHashCode(),  7,2);


              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {

                  CreateData413( "311", clGeneral.enRechivim.PremiaLariushum.GetHashCode(),  7, 2);
                  CreateData413( "321", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(),  7, 2);
                  CreateData413( "311", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatDfus.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatGifur.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(),  7,2);
                  CreateData413( "331", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremiaMeshek.GetHashCode(),  7, 2);
                  CreateData413( "331", clGeneral.enRechivim.PremiaGrira.GetHashCode(),  7, 2);

                  CreateData413( "303", clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(),  7, 2);

              }
              else
              {

                  CreateData413( "389", clGeneral.enRechivim.PremiaLariushum.GetHashCode(),  7, 2);
                  CreateData413( "321", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(),  7, 2);
                  CreateData413( "389", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatDfus.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatGifur.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremiaMeshek.GetHashCode(),  7, 2);
                  CreateData413( "322", clGeneral.enRechivim.PremiaGrira.GetHashCode(),  7, 2);

              }

              if (_ListErua.Count > 0)
                  return _ListErua;
              else return null;

          }
           catch (Exception ex)
           {
               WriteError("CreateErua413: " + ex.Message);
               throw ex;
           }
      }

      private void CreateData413(string sSaifHilan, int iKodRechiv, int iLen, int iNumDigit)
      {
          float fErech;
          int iLastDigit;
          bKayamEfreshBErua = false;
          fErech = GetErechRechiv(iKodRechiv);
          if (fErech > 0)
          {
              StringBuilder sErua413 = new StringBuilder();
          
              sErua413.Append(sSaifHilan.PadLeft(4, char.Parse("0")));
              sErua413.Append(GetBlank(17));
              sErua413.Append(FormatNumber(fErech, iLen, iNumDigit));
              sErua413.Append(GetBlank(12));

              if (bKayamEfreshBErua)
              {
                  iLastDigit = int.Parse(_sFooter.Substring(_sFooter.Length - 1, 1));
                  _sFooter = _sFooter.Substring(0, _sFooter.Length - 1);
                  _sFooter += GetSimanEfresh(iLastDigit);
              }
              if (!IsEmptyErua(sErua413.ToString()))
              {
                  _ListErua.Add(_sHeader + sErua413.ToString() + _sFooter);
              }
          }
      }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.DAL;
using System.Data;

namespace KdsBatch
{
  public  class clErua589:clErua
    {
      private DataTable dtChishuvYomi;
      public clErua589(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv,DataTable dtChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,589)
      {
          dtChishuvYomi = dtChishuv;
          _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua589 = new StringBuilder();
          DataTable dtChishuv;
          DateTime dTarMe, dTarAd;
          float fErech;
          int iSugYom, iCountDays,iDay;

          try
          {
              
              iCountDays = 0;
              dTarMe = new DateTime(_dMonth.Year,_dMonth.Month,1);
              dTarAd = dTarMe.AddMonths(1).AddDays(-1);
              do
              {
                  iCountDays += 1;
                  fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                  if (fErech > 0)
                  {
                      sErua589.Append("R");
                  }
                  else
                  {
                      fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                      if (fErech > 0)
                      {
                          sErua589.Append("G");
                      }
                      else
                      {
                          fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                          if (fErech > 0)
                          {
                              sErua589.Append("D");
                          }
                          else
                          {
                              fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                              if (fErech > 0)
                              {
                                  sErua589.Append("V");
                              }
                              else
                              {
                                  fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                  if (fErech > 0)
                                  {
                                      sErua589.Append("N");
                                  }
                                  else
                                  {
                                      fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                      if (fErech > 0)
                                      {
                                          sErua589.Append("M");
                                      }
                                      else
                                      {
                                          fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                          if (fErech > 0)
                                          {
                                              sErua589.Append("I");
                                          }
                                          else
                                          {
                                              fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                              if (fErech > 0)
                                              {
                                                  sErua589.Append("T");
                                              }
                                              else
                                              {
                                                  fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                                  if (fErech > 0)
                                                  {
                                                      sErua589.Append("A");
                                                  }
                                                  else
                                                  {
                                                      fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                                      if (fErech > 0)
                                                      {
                                                          sErua589.Append("F");
                                                      }
                                                      else
                                                      {
                                                          fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTipatChalav.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                                          if (fErech > 0)
                                                          {
                                                              sErua589.Append("H");
                                                          }
                                                          else
                                                          {
                                                              fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                                              if (fErech > 0)
                                                              {
                                                                  sErua589.Append("W");
                                                              }
                                                              else
                                                              {
                                                                  fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                                                  if (fErech > 0)
                                                                  {
                                                                      sErua589.Append("Z");
                                                                  }
                                                                  else
                                                                  {
                                                                      fErech = clCalcData.GetSumErechRechiv(dtChishuvYomi.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
                                                                      if (fErech > 0)
                                                                      {
                                                                          sErua589.Append("א");
                                                                      }
                                                                      else
                                                                      {
                                                                          //switch ((dTarMe.DayOfWeek.GetHashCode() + 1))
                                                                          //{
                                                                          //    case 7:
                                                                          //        { iSugYom = 20; break; }
                                                                          //    case 6:
                                                                          //        { iSugYom = 10; break; }
                                                                          //    default:
                                                                          //        { iSugYom = 1; break; }
                                                                          //}
                                                                          //if (iSugYom == 10)
                                                                          //{ sErua589.Append("ו"); }
                                                                          //else if (iSugYom == 20)
                                                                          //{ sErua589.Append("ש"); }
                                                                          //else if (iSugYom > 20)
                                                                          //{ sErua589.Append("ג"); }
                                                                          //else 
                                                                          if (_iMushhe > 0)
                                                                          { sErua589.Append("C"); }
                                                                          else { sErua589.Append("."); }
                                                                      }
                                                                  }
                                                              }
                                                          }
                                                      }
                                                  }
                                              }
                                          }

                                      }
                                  }
                              }
                          }
                      }
                  }

                  dTarMe = dTarMe.AddDays(1);
              }
              while (dTarMe <= dTarAd);
              
              iDay=dTarAd.Day;
              while (iDay < 31)
              {
                  iDay++;
                  sErua589.Append(".");  
              }
              sErua589.Append(GetBlank(31));
        //   sErua589.Append(GetBlank(74-iCountDays));
              if (!IsEmptyErua(sErua589.ToString()) && !bKayamEfreshBErua)
           {
               ListErua.Add(sErua589.ToString());
               return ListErua;
           }
           else return null;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua589: " + ex.Message);
               return null;
           //    throw ex;
           }

      }

      protected override void SetHeader()
      {
          StringBuilder sStart = new StringBuilder();

          sStart.Append("589");
          sStart.Append(_drPirteyOved["mifal"].ToString().PadLeft(4, char.Parse("0")));
          sStart.Append("000");
          sStart.Append(_drPirteyOved["mispar_ishi"].ToString().PadLeft(5, char.Parse("0")));
          sStart.Append(_drPirteyOved["sifrat_bikoret"].ToString());
          sStart.Append(_drPirteyOved["shem_mish"].ToString().PadLeft(10).Substring(0, 10));
          sStart.Append(_drPirteyOved["shem_prat"].ToString().PadLeft(7).Substring(0, 7));
          sStart.Append("0000");
          sStart.Append(GetBlank(2));
          sStart.Append(GetBlank(1));
          sStart.Append("011");
          sStart.Append("01");
          _sHeader = sStart.ToString();

      }

      protected override void SetFooter()
      {
          StringBuilder sEnd = new StringBuilder();
          DateTime startDate =DateTime.Parse("01/"+_dMonth.Month.ToString()+"/" + _dMonth.Year.ToString());

          if (_sadeLeloErech)
          {
              sEnd.Append(GetBlank(8));
              sEnd.Append(GetBlank(8));
          }
          else
          {
              sEnd.Append(startDate.ToString("ddMMyyyy"));
              sEnd.Append(startDate.AddMonths(1).AddDays(-1).ToString("ddMMyyyy"));
          }
          sEnd.Append(GetBlank(5));

          _sFooter = sEnd.ToString();
      }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
    public class clEruaDataEt : clErua
    {
        private List<string> _ListErua;
        private DataTable dtChishuvYomi;
        public clEruaDataEt(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv,DataTable dtChishuv)
            : base(lBakashaId, drPirteyOved, dtDetailsChishuv,162)
      {
          dtChishuvYomi = dtChishuv;
           _sBody = SetBody();
           if (_sBody != null)
            PrepareLines();
      }

      protected override void SetHeader()
      {
          StringBuilder sHeader = new StringBuilder();
          sHeader.Append("162");
          sHeader.Append(_dMonth.Year.ToString().Substring(2, 2) + _dMonth.Month.ToString().PadLeft(2, char.Parse("0")));
          sHeader.Append(_iMisparIshi.ToString().PadLeft(9,char.Parse("0")));
          sHeader.Append(_drPirteyOved["TEUDAT_ZEHUT"].ToString().PadLeft(9, char.Parse("0")));
          sHeader.Append(" ");
          sHeader.Append("0"); 
          _sHeader = sHeader.ToString();
      }

      protected override void SetFooter()
      {
          StringBuilder sFooter = new StringBuilder();
          sFooter.Append("000");
          sFooter.Append("      ");
          sFooter.Append("1");

          _sFooter = sFooter.ToString();
      }

      protected override List<string> SetBody()
      {
          _ListErua = new List<string>();
         
          StringBuilder sDataEt = new StringBuilder();
          float fErech;
          string sMeafyen53;
          DataRow[] drYamim;
          try
          {
              fErech = GetErechRechiv( clGeneral.enRechivim.YemeyAvoda.GetHashCode());
              if (fErech > 0)
              {
                  CreateDataEtToRechiv("100", fErech, 0);
               
              }

              fErech = GetErechRechiv( clGeneral.enRechivim.ShaotShabat100.GetHashCode());
              if (fErech > 0)
              {
                   CreateDataEtToRechiv("001", fErech, 0);
                 
              }

              if (_drPirteyOved["isuk"].ToString().Substring(0, 1) == "5")
              {
                  fErech = GetErechRechiv( clGeneral.enRechivim.PremyaRegila.GetHashCode());
                  if (fErech > 0)
                  {
                      if (fErech > 500) { fErech = 500; }
                    CreateDataEtToRechiv( "020", 1, fErech);
                     
                  }


                  fErech = GetErechRechiv( clGeneral.enRechivim.SachPitzul.GetHashCode());
                  if (fErech > 0)
                  {
                      CreateDataEtToRechiv( "063", fErech, 0);
                     
                  }
              }

              sMeafyen53 =_drPirteyOved["meafyen53"].ToString();
              if (sMeafyen53 != "")
              {
                  if (sMeafyen53.Substring(0, 1) == "1")
                  {
                      fErech = GetErechRechiv(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode());
                      if (fErech > 0)
                      {
                        //  fKamut = float.Parse(dtChishuvYomi.Compute("count(MISPAR_ISHI)", "MISPAR_ISHI=" + _iMisparIshi + " AND ERECH_RECHIV>0 AND KOD_RECHIV=" + clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode() + " and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')").ToString());
                          drYamim = dtChishuvYomi.Select("ERECH_RECHIV>0 AND  KOD_RECHIV=" + clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode() + " and taarich>=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime') and taarich<=Convert('" + _dMonth.AddMonths(1).AddDays(-1).ToShortDateString() + "', 'System.DateTime')");
                          CreateDataEtToRechiv("004", drYamim.Length, fErech);

                      }
                  }
                  else if (sMeafyen53.Substring(0, 1) == "2")
                  {
                      fErech = GetErechRechiv(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode());
                      if (fErech > 0)
                      {
                          CreateDataEtToRechiv("004", 1, fErech);
                      }
                  }
                  //else { CreateDataEtToRechiv("004", 0, fErech); }
              }

              fErech = GetErechRechiv( clGeneral.enRechivim.ZmanLailaEgged.GetHashCode());
              fErech += GetErechRechiv( clGeneral.enRechivim.ZmanLailaChok.GetHashCode());

              if (fErech > 0)
              {
                   CreateDataEtToRechiv( "078", fErech, 0);
                 
              }


              fErech = GetErechRechiv( clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode());
              if (fErech > 0)
              {
                   CreateDataEtToRechiv( "079", fErech, 0);
                  
              }


              fErech = GetErechRechiv( clGeneral.enRechivim.Shaot125Letashlum.GetHashCode());
              if (fErech > 0)
              {
                 CreateDataEtToRechiv( "007", fErech, 0);
                 
              }

              fErech = GetErechRechiv( clGeneral.enRechivim.Shaot150Letashlum.GetHashCode());
              if (fErech > 0)
              {
                  CreateDataEtToRechiv( "008", fErech, 0);
                 
              }

              fErech = GetErechRechiv( clGeneral.enRechivim.Shaot200Letashlum.GetHashCode());
              if (fErech > 0)
              {
                 CreateDataEtToRechiv( "048", fErech, 0);
                 
              }
          
            if (_ListErua.Count>0)
                return _ListErua;
            return null;
           }
           catch (Exception ex)
           {
               WriteError("clEruaDataEt: " + ex.Message);
               throw ex;
           }
      }

      private void CreateDataEtToRechiv(string sSemelNatun, float fKamut, float fMechir)
      {
          string sSimanMechir, sSimanKamut;
          StringBuilder sDataEt = new StringBuilder();
          try
          {
              if (fKamut < 0)
              {
                  sSimanKamut = "-";
              }
              else { sSimanKamut = "+"; }
              if (fMechir < 0)
              {
                  sSimanMechir = "-";
              }
              else { sSimanMechir = "+"; }

              sDataEt.Append(sSemelNatun);
              sDataEt.Append(FormatNumber(fKamut, 10, 2));
              sDataEt.Append(sSimanKamut);
              sDataEt.Append(FormatNumber(fMechir, 10, 2));
              sDataEt.Append(sSimanMechir);

              if (!IsEmptyErua(sDataEt.ToString()))
              {
                  _ListErua.Add(sDataEt.ToString());
              }
            
          }
          catch (Exception ex)
          {
              WriteError("clEruaDataEt: " + ex.Message);
              throw ex;
          }
      }
    }
}

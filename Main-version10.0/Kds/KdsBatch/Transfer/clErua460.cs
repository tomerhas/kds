using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua460:clErua
    {
      private DataTable _dtPrem;
      private DataTable _dtChufsha;
    //  private DateTime dChodeshIbud;
      public clErua460(long lBakashaId, DataRow drPirteyOved, DataSet dsNetunim)
          : base(lBakashaId, drPirteyOved, dsNetunim, 460)
      {
          _dtPrem = dsNetunim.Tables[2];
          _dtChufsha = dsNetunim.Tables[4];
         //dChodeshIbud = DateTime.Parse(drPirteyOved["chodesh_ibud"].ToString());
          _sBody = SetBody();
          if (_sBody != null)
             PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua460 = new StringBuilder();
          float fErech;
          DataRow[] dr;
          try
          {
              dr = _dtChufsha.Select("mispar_ishi = " + _iMisparIshi);
              if (dr.Length > 0 && _dChodeshIbud == _dMonth)
                  sErua460.Append(FormatNumber(1, 4, 2));
              else sErua460.Append(GetBlank(4));

              fErech = GetErechRechiv(clGeneral.enRechivim.NochehutByameyMiluim.GetHashCode());
              sErua460.Append(FormatNumber(fErech, 4, 2));
           //   sErua460.Append(GetBlank(8));

              //if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              //{
              //  //  sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaGrira.GetHashCode(), "erech_rechiv_a"), 4, 0));
              //    sErua460.Append(FormatNumber(GetErechRechivPremiyaFriends(clGeneral.enRechivim.PremiaGrira.GetHashCode()), 4, 0));
              //}
              //else 
              if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  fErech=GetErechRechivPremiya(clGeneral.enRechivim.PremiaGrira.GetHashCode(), _dtPrem);
                  fErech += GetErechRechiv(clGeneral.enRechivim.PremyatGriraYadanit.GetHashCode());
                  sErua460.Append(FormatNumber(fErech, 4, 0));
              }
              else sErua460.Append(GetBlank(4));

              if (MaamadDorB())
              {
                  sErua460.Append(FormatNumber(GetErechRechivDorB(clGeneral.enRechivim.YomMachalatYeledHadHori.GetHashCode()), 4, 2));
              }
              else  if (_iMaamadRashi != clGeneral.enMaamad.Friends.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachalatYeledHadHori.GetHashCode()), 4, 2));
              }
              else sErua460.Append(GetBlank(4));

              if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  fErech = GetErechRechiv(clGeneral.enRechivim.YomMiluim.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMiluimChelki.GetHashCode());
                  sErua460.Append(FormatNumber(fErech, 4, 2));
              }
              else
              {
                  sErua460.Append(GetBlank(4));
              }


              fErech = GetErechRechiv(clGeneral.enRechivim.MachalatYeledImMugbalutMaavid.GetHashCode());
              sErua460.Append(FormatNumber(fErech, 4, 2));

              if (MaamadDorB())
              {
                  fErech = GetErechRechivDorB(clGeneral.enRechivim.MachalatYeledImMugbalutOved.GetHashCode());
                  sErua460.Append(FormatNumber(fErech, 4, 2));
              }
              else
              {
                  fErech = GetErechRechiv(clGeneral.enRechivim.MachalatYeledImMugbalutOved.GetHashCode());
                  sErua460.Append(FormatNumber(fErech, 4, 2));
              }

              fErech = GetErechRechiv(clGeneral.enRechivim.MachalatYeledImMugbalutOved.GetHashCode());
              sErua460.Append(FormatNumber(fErech, 4, 2));

              fErech = GetErechRechiv(clGeneral.enRechivim.YomMachalaMisradHaBitachon.GetHashCode());
              sErua460.Append(FormatNumber(fErech, 4, 2));
              //sErua460.Append(GetBlank(4));//פוזיציה 8
              sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode()), 4, 2));


              if (_iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode() && 
                  _iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() && 
                  _iDirug != 11 && _iDirug != 63 && !(_iDirug == 92 && _iDarga == 2))
              {
                  fErech = GetErechRechiv(clGeneral.enRechivim.Shaot100LetashlumNahagut.GetHashCode()) / 60;
                  sErua460.Append(FormatNumber(fErech, 4, 1));

                  fErech = GetErechRechiv(clGeneral.enRechivim.Shaot125LetashlumNahagut.GetHashCode()) / 60;
                  sErua460.Append(FormatNumber(fErech, 4, 1));


                  fErech = GetErechRechiv(clGeneral.enRechivim.Shaot150LetashlumNahagut.GetHashCode()) / 60;
                  sErua460.Append(FormatNumber(fErech, 4, 1));


                  fErech = GetErechRechiv(clGeneral.enRechivim.Shaot200LetashlumNahagut.GetHashCode()) / 60;
                  sErua460.Append(FormatNumber(fErech, 4, 1));


                  fErech = GetErechRechiv(clGeneral.enRechivim.Shaot100Nahagut.GetHashCode()) / 60;
                  sErua460.Append(FormatNumber(fErech, 4, 1));

                  fErech = GetErechRechiv(clGeneral.enRechivim.DakotNehigaHashlamaLeyomAvoda.GetHashCode())/60;
                  sErua460.Append(FormatNumber(fErech, 4, 1));

                  sErua460.Append(GetBlank(13));
              } 
              else 
                sErua460.Append(GetBlank(37));
              if (!IsEmptyErua(sErua460.ToString()))
              {
                  ListErua.Add(sErua460.ToString());
                  return ListErua;
              }
              else return null;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua460: " + ex.Message);
               return null;
              // throw ex;
           }
      }
    }
}

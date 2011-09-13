using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua415:clErua
    {
      public clErua415(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,415)
      {
          _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      {
          List<string> ListErua = new List<string>();
          StringBuilder sErua415;
          float fErech;
          sErua415 = new StringBuilder();
          try
          {
              //if (_iDirug != 61)
              //{
                  //if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                  //{
                  //    sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode()) / 60), 4, 1));
                  //}
                  //else { sErua415.Append(GetBlank(4)); }
                 
                  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode()) / 60), 4, 1));
                  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotRegilot.GetHashCode()) / 60), 4, 1));
                  sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.ShaotHeadrut.GetHashCode()), 4, 1));
                  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.Nosafot100.GetHashCode()) / 60), 4, 1));
              //}
              //else { sErua415.Append(GetBlank(16)); }

              sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode())/60 ), 4, 1));
              sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode())/60), 4, 1));
              sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode())/60), 4, 1));

              if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
               //   fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode());
                  fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaChok.GetHashCode());
                  sErua415.Append(FormatNumber((fErech/60), 4, 1));
              }
              else { sErua415.Append(GetBlank(4)); }

              //if (_iDirug != 61)
              //{
                  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.ShaotShabat100.GetHashCode())/60), 4, 1));

                  sErua415.Append(GetBlank(4));

                  sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode()), 4, 1));
                  fErech =GetErechRechiv(clGeneral.enRechivim.DakotNehigaChol.GetHashCode()) ;
                  fErech += GetErechRechiv(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode());
                  sErua415.Append(FormatNumber((fErech / 60), 4, 1));
                  sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.Shaot25.GetHashCode()), 4, 1));
                  sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.Shaot50.GetHashCode()), 4, 1));
                //  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode()) / 60), 4, 1));

                  fErech = GetErechRechiv(clGeneral.enRechivim.Nosafot100.GetHashCode())*60;
                  fErech += GetErechRechiv(clGeneral.enRechivim.Nosafot125.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.Nosafot150.GetHashCode());
                  sErua415.Append(FormatNumber((fErech / 60), 4, 1));

                  fErech = GetErechRechiv(clGeneral.enRechivim.SachLina.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.SachLinaKfula.GetHashCode());
                  sErua415.Append(FormatNumber(fErech, 4, 1));

                  if (_iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() &&
                      _iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode() && 
                      _iMaamadRashi != clGeneral.enMaamad.Friends.GetHashCode())
                  {
                      sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode()), 4, 2));
                  }
                  else { sErua415.Append(GetBlank(4)); }
              //}
              //else { sErua415.Append(GetBlank(36)); }

                  if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                  {
                      sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode())/60),5, 1));
                  }
                  else  sErua415.Append(GetBlank(5));

              if (!IsEmptyErua(sErua415.ToString()))
              {
                  ListErua.Add(sErua415.ToString());
                  return ListErua;
              }
              else return null;
          }
           catch (Exception ex)
           {
               WriteError("CreateErua415: " + ex.Message);
               throw ex;
           }
      }
    }
}

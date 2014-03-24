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
      public clErua415(long lBakashaId, DataRow drPirteyOved, DataSet dsNetunim)
          : base(lBakashaId, drPirteyOved, dsNetunim, 415)
      {
           _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      {
          List<string> ListErua = new List<string>();
          StringBuilder sErua415;
          float fErech ,fXtemp,fYTemp;
          sErua415 = new StringBuilder();
          string sMeafyen83 = "";
          try
          {
              sMeafyen83 = _drPirteyOved["meafyen83"].ToString(); 
              //if (_iDirug != 61)
              //{
                  //if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                  //{
                  //    sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode()) / 60), 4, 1));
                  //}
                  //else { sErua415.Append(GetBlank(4)); }

                  fErech = GetErechRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                  fErech -= (GetErechRechiv(clGeneral.enRechivim.SachKizuzShaotNosafot.GetHashCode())*60);
                  sErua415.Append(FormatNumber((fErech / 60), 4, 1));

                  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotRegilot.GetHashCode()) / 60), 4, 1));
                  
                  fErech = GetErechRechiv(clGeneral.enRechivim.ShaotHeadrut.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.ShaotChofesh.GetHashCode());
                  sErua415.Append(FormatNumber(fErech, 4, 1));

                  sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode()), 4, 1));
              //}
              //else { sErua415.Append(GetBlank(16)); }

             // 
              fErech = GetErechRechiv(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode());
              if (sMeafyen83.Trim() == "1")
                  fErech = 0;
              sErua415.Append(FormatNumber((fErech / 60), 4, 1));

              fErech = GetErechRechiv(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode());
             if (sMeafyen83.Trim() == "1")
                  fErech = 0;
              sErua415.Append(FormatNumber((fErech/60), 4, 1));

              fErech = GetErechRechiv(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode());
              sErua415.Append(FormatNumber((fErech / 60), 4, 1));

              if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
               //   fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode());
                  fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaChok.GetHashCode());
                  sErua415.Append(FormatNumber((fErech/60), 4, 1));
              }
              else if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() && (_drPirteyOved["isuk"].ToString()==clGeneral.enIsukOved.Mafil.GetHashCode().ToString() || _drPirteyOved["isuk"].ToString()==clGeneral.enIsukOved.AchraaiMishmeretMachshev.GetHashCode().ToString() ||_drPirteyOved["isuk"].ToString()==clGeneral.enIsukOved.MetaemTikshoret.GetHashCode().ToString()))
              {
                  //   fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode());
                  fErech = GetErechRechiv(clGeneral.enRechivim.ZmanLailaChok.GetHashCode());
                  sErua415.Append(FormatNumber((fErech / 60), 4, 1));
              }
              else { sErua415.Append(GetBlank(4)); }

              //if (_iDirug != 61)
              //{
             fXtemp = GetErech(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), "erech_rechiv_a") - GetErech(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), "erech_rechiv_a");
             fYTemp = GetErech(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), "erech_rechiv_b") - GetErech(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), "erech_rechiv_b");
             if (CheckBakashatHefreshExists(clGeneral.enRechivim.ShaotShabat100.GetHashCode()) || CheckBakashatHefreshExists(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode()))
                 if (!bKayamEfreshBErua && fXtemp != fYTemp)
                     bKayamEfreshBErua = true; 
             fXtemp = fXtemp > 0 ? fXtemp : 0;
             fYTemp = fYTemp > 0 ? fYTemp : 0;
             fErech = fXtemp - fYTemp;
             sErua415.Append(FormatNumber(fErech / 60, 4, 1));
           //  fErech = GetErechRechiv(clGeneral.enRechivim.ShaotShabat100.GetHashCode());
           // fErech -= GetErechRechiv(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode());
             //if (fErech>0)
             //    sErua415.Append(FormatNumber(fErech/60, 4, 1));
             //else
             //    sErua415.Append(GetBlank(4));

            sErua415.Append(GetBlank(4));

            sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode())/60), 4, 1));
            fErech =GetErechRechiv(clGeneral.enRechivim.DakotNehigaChol.GetHashCode()) ;
            fErech += GetErechRechiv(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode());
            fErech += GetErechRechiv(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode());
            fErech += GetErechRechiv(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode());
            sErua415.Append(FormatNumber((fErech / 60), 4, 1));
            sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.Shaot25.GetHashCode()), 4, 1));
            sErua415.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.Shaot50.GetHashCode()), 4, 1));
        //  sErua415.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode()) / 60), 4, 1));

            //fErech = GetErechRechiv(clGeneral.enRechivim.Nosafot100.GetHashCode())*60;
            //fErech += GetErechRechiv(clGeneral.enRechivim.Nosafot125.GetHashCode());
            //fErech += GetErechRechiv(clGeneral.enRechivim.Nosafot150.GetHashCode());
            //sErua415.Append(FormatNumber((fErech / 60), 4, 1));

            sErua415.Append(GetBlank(4));

            fErech = GetErechRechiv(clGeneral.enRechivim.SachLina.GetHashCode());
            fErech += GetErechRechiv(clGeneral.enRechivim.SachLinaKfula.GetHashCode());
            sErua415.Append(FormatNumber(fErech, 4, 1));

            if (MaamadDorB())
            {
                sErua415.Append(FormatNumber(GetErechRechivDorB(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode()), 4, 2));
            }
            else if ((_iMaamad == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.SachirZmani.GetHashCode())
                || (_iMaamad == clGeneral.enKodMaamad.Sachir12.GetHashCode() && _dMonth >= dTakanonSoziali))
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
               return null;
              // throw ex;
           }
      }
    }
}

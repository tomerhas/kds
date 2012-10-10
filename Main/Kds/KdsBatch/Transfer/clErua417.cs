using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua417:clErua
    {
      private DataTable _dtPrem;
      public clErua417(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv, DataTable dtPrem)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,417)
      {
          _dtPrem = dtPrem;
          _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
           StringBuilder sErua417;
           float fErech;
           sErua417 = new StringBuilder();
           try
           {
              
               fErech = GetErechRechiv( clGeneral.enRechivim.KamutGmulChisachon.GetHashCode());
               //fErech += GetErechRechiv( clGeneral.enRechivim.KamutGmulChisachonMeyuchad.GetHashCode());
               sErua417.Append(FormatNumber(fErech,4,1));
               
               sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode()),4,1));

               if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
               {
                   fErech = GetErechRechiv(clGeneral.enRechivim.ShaotHeadrut.GetHashCode());
                   fErech += GetErechRechiv(clGeneral.enRechivim.ShaotChofesh.GetHashCode());
                   sErua417.Append(FormatNumber((fErech), 4, 1));
               }
               else { sErua417.Append(GetBlank(4)); }

               sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachPitzul.GetHashCode()),4,0));
               sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachPitzulKaful.GetHashCode()),4,0));
               sErua417.Append(GetBlank(4));          //sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelBoker.GetHashCode()),4,0));
               sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaTichnunTnua.GetHashCode()), 4, 0));
               sErua417.Append(GetBlank(4));            // sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelErev.GetHashCode()),4,0));
               sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode()),4,0));


              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {
                  sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode()), 4, 0));
              }
              else if (_iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
              {
                  if (_iMaamad == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() ||
                   _iMaamad == clGeneral.enKodMaamad.SachirZmani.GetHashCode())
                  {
                      sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachalatHorim.GetHashCode()), 4, 2));
                  }
                  else { sErua417.Append(GetBlank(4)); }
              }
           //פרמיה משק - 115
            if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
            {
               //sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaMeshek.GetHashCode(),"erech_rechiv_a"), 4, 0));
                sErua417.Append(FormatNumber(GetErechRechivPremiyaFriends(clGeneral.enRechivim.PremiaMeshek.GetHashCode()), 4, 0));
            }
            else if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechivPremiya(clGeneral.enRechivim.PremiaMeshek.GetHashCode(),_dtPrem), 4, 0));
            }
            else sErua417.Append(GetBlank(4));
            //פרמיה פקחים - 117
            if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaPakachim.GetHashCode(), "erech_rechiv_a"), 4, 0));
            }
            else if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechivPremiya(clGeneral.enRechivim.PremiaPakachim.GetHashCode(), _dtPrem), 4, 0));
            }
            else sErua417.Append(GetBlank(4));
            //פרמיה סדרנים - 116
            if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaSadranim.GetHashCode(), "erech_rechiv_a"), 4, 0));
            }
            else if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechivPremiya(clGeneral.enRechivim.PremiaSadranim.GetHashCode(), _dtPrem), 4, 0));
            }
            else sErua417.Append(GetBlank(4));
            //פרמיה רכזים - 118
            if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaRakazim.GetHashCode(), "erech_rechiv_a"), 4, 0));      
            }
            else if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
            {
                sErua417.Append(FormatNumber(GetErechRechivPremiya(clGeneral.enRechivim.PremiaRakazim.GetHashCode(), _dtPrem), 4, 0));
            }
            else sErua417.Append(GetBlank(4)); 
           
            fErech = GetErechRechiv(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode());
            fErech += GetErechRechiv(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode());
            sErua417.Append(FormatNumber(fErech, 4,0));
            sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode()), 4, 0));
            sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.DakotPremiaShabat.GetHashCode()),4,0));
            fErech = GetErechRechiv(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode());
            fErech += GetErechRechiv(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode());
            sErua417.Append(FormatNumber(fErech, 5, 0));

              if (!IsEmptyErua(sErua417.ToString()))
              {
                  ListErua.Add(sErua417.ToString());
                  return ListErua;
              }
              else return null;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua417: " + ex.Message);
               return null;
             //  throw ex;
           }
      }
    }
}

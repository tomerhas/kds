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
      public clErua417(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,417)
      {
          _sBody = SetBody();
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
               sErua417.Append(FormatNumber(fErech,4,2));
               
               sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode()),4,2));
                         
               if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
               {
                   fErech = GetErechRechiv(clGeneral.enRechivim.YomHeadrut.GetHashCode());
                   fErech += GetErechRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode());
               sErua417.Append(FormatNumber((fErech/60),4,2));
               }
               else { sErua417.Append(GetBlank(4)); }

               sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachPitzul.GetHashCode()),4,1));
              sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachPitzulKaful.GetHashCode()),4,1));
              sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelBoker.GetHashCode()),4,1));
              sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode()),4,1));
              sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelErev.GetHashCode()),4,1));
              sErua417.Append(FormatNumber((GetErechRechiv( clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode())/60),4,1));


              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {
                  sErua417.Append(FormatNumber((GetErechRechiv(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode()) / 60), 4, 1));
              }
              else
              {
                if (_iMaamad != clGeneral.enKodMaamad.ChaverSofi.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode()&& _iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
                  {
                      sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachalatHorim.GetHashCode()), 4, 2));
                  }
                  else { sErua417.Append(GetBlank(4)); }
              }

                if (_iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
               {
                   sErua417.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode()), 4, 0));
               }
               else { sErua417.Append(GetBlank(4)); }

                sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.PremiaPakachim.GetHashCode()),4,0));
                sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.PremiaSadranim.GetHashCode()),4,0));
                sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.PremiaRakazim.GetHashCode()),4,0));
                sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.PremyaNamlak.GetHashCode()),4,0));
                sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode()),4,0));
                sErua417.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.DakotPremiaShabat.GetHashCode()),4,0));

              sErua417.Append("-----");

               ListErua.Add(sErua417.ToString());
               return ListErua;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua417: " + ex.Message);
               throw ex;
           }
      }
    }
}

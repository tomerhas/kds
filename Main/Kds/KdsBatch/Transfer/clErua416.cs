using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua416:clErua
    {
      public clErua416(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,416)
      {
          _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
           StringBuilder sErua416;
           float fErech;
           sErua416 = new StringBuilder();
           try
           {
           
           sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode()),4,2));
           sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YemeyAvoda.GetHashCode()),4,2));

           fErech = GetErechRechiv(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode());
          if (_iMaamad == clGeneral.enKodMaamad.SachirZmani.GetHashCode() && fErech>0)
           {
               fErech += GetErechRechiv(clGeneral.enRechivim.YomHeadrut.GetHashCode());

               sErua416.Append(FormatNumber(fErech,4,2));
           }
           else if (_iMaamad == clGeneral.enKodMaamad.OvedBechoze.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
           {
               fErech = GetErechRechiv(clGeneral.enRechivim.YomHeadrut.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaBoded.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaYeled.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatHorim.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomTeuna.GetHashCode());

               sErua416.Append(FormatNumber(fErech,4,2));
           }
           else if (_iMaamad == clGeneral.enKodMaamad.Shtachim.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.Aray.GetHashCode())
           {
               fErech = GetErechRechiv( clGeneral.enRechivim.YomHeadrut.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaBoded.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaYeled.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatHorim.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomTeuna.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode());

               sErua416.Append(FormatNumber(fErech,4,2));
           }
           else
           {
               sErua416.Append(GetBlank(4));
           }
          
           if(_iMaamad == clGeneral.enKodMaamad.Shtachim.GetHashCode())
           {
               sErua416.Append(GetBlank(4));
           }
           else if (_iMaamad == clGeneral.enKodMaamad.Sachir12.GetHashCode())
           {
               fErech = GetErechRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMiluim.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaYeled.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatHorim.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode());
               fErech += GetErechRechiv(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode());

               sErua416.Append(FormatNumber(fErech,4,2));
           }
           else 
           {
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode()),4,2));
           }

           if (_iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode())
           {
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.ChofeshZchut.GetHashCode()),4,2));
               sErua416.Append(GetBlank(4));
               sErua416.Append(GetBlank(4));
               if (_iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode())
               {
                   sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachla.GetHashCode()),4,2));
               }
               else
               {
                   sErua416.Append(GetBlank(4));
               }
               if (_iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
               {
                   sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachalaBoded.GetHashCode()),4,2));
               }
               else
               {
                   sErua416.Append(GetBlank(4));
               }
               if (_iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode())
               {
                   sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMachalaYeled.GetHashCode()),4,2));
               }
               else
               {
                   sErua416.Append(GetBlank(4));
               }

               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode()),4,2));
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMiluimChelki.GetHashCode()), 4, 2));
           }
           else
           {
               sErua416.Append(GetBlank(32));
           }

           if (_iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
           {
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMiluim.GetHashCode()),4,2));
           }
           else
           {
               sErua416.Append(GetBlank(4));
           }
           if (_iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode())
           {
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomTipatChalav.GetHashCode()),4,2));
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomTeuna.GetHashCode()),4,2));
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomHadracha.GetHashCode()),4,2));
               if (_iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode() && _iMaamadRashi != clGeneral.enMaamad.Friends.GetHashCode())
               {
                   sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomEvel.GetHashCode()),4,2));
               }
               else
               {
                   sErua416.Append(GetBlank(4));
               }
               sErua416.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode()),5,1));
           }
           else 
           {
               sErua416.Append(GetBlank(21));
           }

           if (!IsEmptyErua(sErua416.ToString()))
           {
               ListErua.Add(sErua416.ToString());
               return ListErua;
           }
           else return null;
           
           }
           catch (Exception ex)
           {
               WriteError("CreateErua416: " + ex.Message);
               throw ex;
           }
      }
    }
}

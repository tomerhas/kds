using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua419:clErua
    {
      public clErua419(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,419)
      {
          _sBody = SetBody();
          if (_sBody != null)
          PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua419;
          float fErech;
          sErua419 = new StringBuilder();
          try
          {
             
              sErua419.Append(GetBlank(32));
              if (_iMaamadRashi != clGeneral.enMaamad.Friends.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode() && _iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode())
              {
                  sErua419.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode()), 4, 2));
              }
              else { sErua419.Append(GetBlank(4)); }
              
              sErua419.Append(GetBlank(8));
              
              sErua419.Append(GetBlank(4)); 

              sErua419.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.PremiaMachsenaim.GetHashCode()), 4, 0));

              //if (_iMaamad == clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
              //{
              //    fErech = GetErechRechiv( clGeneral.enRechivim.YomMachla.GetHashCode());
              //    fErech += GetErechRechiv( clGeneral.enRechivim.YomMachalaYeled.GetHashCode());

              //    sErua419.Append(FormatNumber(fErech, 4, 2));
              //}
              //else { sErua419.Append(GetBlank(4)); }
              sErua419.Append(GetBlank(4));

              sErua419.Append(GetBlank(4));
              sErua419.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode()), 4, 2));
              sErua419.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode()), 4, 2));
              sErua419.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode()), 5, 2));
             // sErua419.Append(GetBlank(5));

              if (!IsEmptyErua(sErua419.ToString()))
              {
                  ListErua.Add(sErua419.ToString());
                  return ListErua;
              }
              else return null;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua419: " + ex.Message);
               return null;
            //   throw ex;
           }
      }
    }
}

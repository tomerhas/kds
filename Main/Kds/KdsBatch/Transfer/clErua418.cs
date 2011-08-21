using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua418:clErua
    {
      public clErua418(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,418)
      {
          _sBody = SetBody();
          if (_sBody != null)
            PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua418;
          sErua418 = new StringBuilder();
          try
          {
              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {
                  sErua418.Append(GetBlank(61));
                  sErua418.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode()), 4, 2));
                  sErua418.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode()), 4, 2));
                  sErua418.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode()), 5, 2));
              }
              if (!IsEmptyErua(sErua418.ToString()))
              {
                  ListErua.Add(sErua418.ToString());
                  return ListErua;
              }
              else return null;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua418: " + ex.Message);
               throw ex;
           }
      }
    }
}

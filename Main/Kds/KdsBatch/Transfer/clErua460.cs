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
      public clErua460(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,460)
      {
          _sBody = SetBody();
          if (_sBody != null)
             PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua460 = new StringBuilder();
          try
          {
              sErua460.Append(GetBlank(8));

              if (_iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaGrira.GetHashCode()), 4, 0));
              }
              else
              {
                  sErua460.Append(GetBlank(4));
              }

              if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode()), 4, 2));
              }
              else
              {
                  sErua460.Append(GetBlank(4));
              }
              sErua460.Append(GetBlank(57));
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
               throw ex;
           }
      }
    }
}

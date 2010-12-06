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
          PrepareLines();
      }

      protected override List<string> SetBody()
      { 
          List<string> ListErua=new List<string>();
          StringBuilder sErua460 = new StringBuilder();
          try
          {
              sErua460.Append(GetBlank(8));
              sErua460.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.PremiaGrira.GetHashCode()), 4, 0));
              if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechiv( clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode()), 4, 2));
              }
              else
              {
                  sErua460.Append(GetBlank(4));
              }
              sErua460.Append(GetBlank(57));

           ListErua.Add(sErua460.ToString());
           return ListErua;
           }
           catch (Exception ex)
           {
               WriteError("CreateErua460: " + ex.Message);
               throw ex;
           }
      }
    }
}

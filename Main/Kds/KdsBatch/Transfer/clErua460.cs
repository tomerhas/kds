﻿using System;
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
      public clErua460(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv, DataTable dtPrem)
          : base(lBakashaId, drPirteyOved, dtDetailsChishuv,460)
      {
          _dtPrem = dtPrem;
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
              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.PremiaGrira.GetHashCode(), "erech_rechiv_a"), 4, 0));
              }
              else if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechivPremiya(clGeneral.enRechivim.PremiaGrira.GetHashCode(), _dtPrem), 4, 0));
              }
              else sErua460.Append(GetBlank(4));

              sErua460.Append(GetBlank(4));

              if (_iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomMiluim.GetHashCode()), 4, 2));
              }
              else
              {
                  sErua460.Append(GetBlank(4));
              }
              sErua460.Append(GetBlank(12));//פוזיציות 6,7,8
              sErua460.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode()), 4, 2));

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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua419:clErua
    {
      private DataTable _dtPrem;
      public clErua419(long lBakashaId, DataRow drPirteyOved, DataSet dsNetunim)
          : base(lBakashaId, drPirteyOved, dsNetunim, 419)
      {
          _dtPrem = dsNetunim.Tables[2];
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
              if (MaamadDorB())
              {
                  sErua419.Append(FormatNumber(GetErechRechivDorB(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode()), 4, 2));
              }
              else  if (_iMaamad == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() ||
                  _iMaamad == clGeneral.enKodMaamad.SachirZmani.GetHashCode() ||
                  (_iMaamad == clGeneral.enKodMaamad.Sachir12.GetHashCode() && _dMonth >= dTakanonSoziali))
              {
                  sErua419.Append(FormatNumber(GetErechRechiv(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode()), 4, 2));
              }
              else { sErua419.Append(GetBlank(4)); }
              
              sErua419.Append(GetBlank(8));
              
              sErua419.Append(GetBlank(4));

              if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                sErua419.Append(FormatNumber(GetErechRechivPremiya(clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), _dtPrem), 4, 0));
              else sErua419.Append(GetBlank(4));

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

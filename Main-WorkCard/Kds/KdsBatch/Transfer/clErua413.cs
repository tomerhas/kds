﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;

namespace KdsBatch
{
  public  class clErua413:clErua
    {
      private DataTable _dtPrem;
      private List<string> _ListErua;

      public clErua413(long lBakashaId, DataRow drPirteyOved, DataSet dsNetunim)
          : base(lBakashaId, drPirteyOved, dsNetunim, 413)
      {
          _dtPrem = dsNetunim.Tables[2];
          _sBody = SetBody();
          if (_sBody != null)
          PrepareLines();
      }

      protected override void SetHeader()
      {
          StringBuilder sStart = new StringBuilder();
         
          sStart.Append("413");
          sStart.Append(_drPirteyOved["mifal"].ToString().PadLeft(4, char.Parse("0")));
          sStart.Append("000");
          sStart.Append(_drPirteyOved["mispar_ishi"].ToString().PadLeft(5, char.Parse("0")));
          sStart.Append(_drPirteyOved["sifrat_bikoret"].ToString());
          sStart.Append(_drPirteyOved["shem_mish"].ToString().PadLeft(10).Substring(0, 10));
          sStart.Append(_drPirteyOved["shem_prat"].ToString().PadLeft(7).Substring(0, 7));
          sStart.Append("000000");
          sStart.Append(GetBlank(6)); 
          _sHeader = sStart.ToString();

      }

      protected override void SetFooter()
      {
          StringBuilder sEnd = new StringBuilder();

          if (_sadeLeloErech)
          {
              sEnd.Append(GetBlank(2));
              sEnd.Append(GetBlank(4));
          }
          else
          {
              sEnd.Append(_dMonth.Month.ToString().PadLeft(2, char.Parse("0")));
              sEnd.Append(_dMonth.Year.ToString());
          }
          sEnd.Append(GetBlank(37));
          _sFooter = sEnd.ToString();      
      }

      protected override List<string> SetBody()
      {
          _ListErua = new List<string>();
          
          try
          {
              
              CreateData413( "224", clGeneral.enRechivim.Kizuz100.GetHashCode(),"erech_rechiv",  7, 2);
              CreateData413( "221", clGeneral.enRechivim.Kizuz125.GetHashCode(),"erech_rechiv",  7, 2);
              CreateData413( "222", clGeneral.enRechivim.Kizuz150.GetHashCode(),"erech_rechiv",  7, 2);
              CreateData413( "223", clGeneral.enRechivim.Kizuz200.GetHashCode(),"erech_rechiv",  7, 2);
         //     CreateData413( "321", clGeneral.enRechivim.PremiaManasim.GetHashCode(),"erech_rechiv",  7,2);


              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {
                  
                  CreatePremiyaData413("303", clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), 7, 2);
                  CreateData413("310", clGeneral.enRechivim.PremiaLariushum.GetHashCode(),"erech_rechiv", 7, 2);
                  //CreateData413("311", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode() +
                  //               ',' + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(),"erech_rechiv", 7, 2);
                  //CreateData413("312", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),"erech_rechiv", 7, 2);
                  //CreateData413("313", clGeneral.enRechivim.DakotPremiaVisa.GetHashCode() +
                  //               ',' + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(),"erech_rechiv", 7, 2);
                 // CreateData413("314", clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(),"erech_rechiv", 7, 2);
                //  CreateData413("315", clGeneral.enRechivim.PremiaPakachim.GetHashCode(), "erech_rechiv_a", 7, 2);
                //  CreateData413("316", clGeneral.enRechivim.PremiaSadranim.GetHashCode(), "erech_rechiv_a", 7, 2);
                //  CreateData413("317", clGeneral.enRechivim.PremiaRakazim.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreatePremiyaData413("321", clGeneral.enRechivim.PremiaManasim.GetHashCode(), 7, 2);
                  //CreateData413("330", clGeneral.enRechivim.PremiaGrira.GetHashCode(), "erech_rechiv_a", 7, 2);
               //   CreateData413("331", clGeneral.enRechivim.PremiaMeshek.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreateData413("339", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("340", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(),"erech_rechiv", 7, 2);
            //      CreateData413("324", clGeneral.enRechivim.PremiaTichnunTnua.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatDfus.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatGifur.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("331", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(),"erech_rechiv",  7,2);
                  CreateData413("331", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(),"erech_rechiv",  7, 2);
                  CreateData413("365", clGeneral.enRechivim.PremyaMenahel.GetHashCode(),"erech_rechiv", 7, 2);
                  if (_dMonth < dTakanonSoziali)
                      CreateData413Seif435("435", "erech_rechiv", 7, 2);

              }
              else
              {
                 // CreateData413("303", clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreateData413("310", clGeneral.enRechivim.PremiaLariushum.GetHashCode(),"erech_rechiv", 7, 2);
                  //CreateData413("311", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode() +
                  //               ',' + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(),"erech_rechiv", 7, 2);
                 // CreateData413("312", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),"erech_rechiv", 7, 2);
                  //CreateData413("313", clGeneral.enRechivim.DakotPremiaVisa.GetHashCode() +
                  //               ',' + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(),"erech_rechiv", 7, 2);
                 // CreateData413("314", clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(),"erech_rechiv", 7, 2);
                  //CreateData413("315", clGeneral.enRechivim.PremiaPakachim.GetHashCode(), "erech_rechiv_a", 7, 2);
                  //CreateData413("316", clGeneral.enRechivim.PremiaSadranim.GetHashCode(), "erech_rechiv_a", 7, 2);
                  //CreateData413("317", clGeneral.enRechivim.PremiaRakazim.GetHashCode(), "erech_rechiv_a", 7, 2);
                // CreateData413("319", clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreatePremiyaData413("321", clGeneral.enRechivim.PremiaManasim.GetHashCode(), 7, 2);
               //   CreateData413("322", clGeneral.enRechivim.PremiaMeshek.GetHashCode(), "erech_rechiv_a", 7, 2);
                 // CreateData413("330", clGeneral.enRechivim.PremiaGrira.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreateData413("339", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("340", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(),"erech_rechiv", 7, 2);
                //  CreateData413("324", clGeneral.enRechivim.PremiaTichnunTnua.GetHashCode(), "erech_rechiv_a", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatDfus.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatGifur.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("322", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(),"erech_rechiv", 7, 2);
                  CreateData413("389", clGeneral.enRechivim.PremyaMenahel.GetHashCode(),"erech_rechiv", 7, 2);

                  if (_iMaamad == clGeneral.enKodMaamad.Sachir12.GetHashCode() && _dMonth < dTakanonSoziali)
                      CreateData413Seif435("435", "erech_rechiv", 7, 2);
                  if ((_iMaamad == clGeneral.enKodMaamad.OvedBechoze.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.OvedChadshKavua.GetHashCode() || _iMaamad == clGeneral.enKodMaamad.Aray.GetHashCode()) && _dMonth < dTakanonSoziali)
                      CreateData413("438", clGeneral.enRechivim.YomChofesh.GetHashCode(), "erech_rechiv", 7, 2);


              }

              if (_ListErua.Count > 0)
                  return _ListErua;
              else return null;

          }
           catch (Exception ex)
           {
               WriteError("CreateErua413: " + ex.Message);
               throw ex;
           }
      }

      private void CreateData413(string sSaifHilan, int iKodRechiv,string col, int iLen, int iNumDigit)
      {
          float fErech=0;
          try
          {
              //if ((iKodRechiv == clGeneral.enRechivim.PremiaMachsenaim.GetHashCode() || iKodRechiv == clGeneral.enRechivim.PremiaManasim.GetHashCode()) && _iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              //{
              //   fErech = GetErechRechivPremiyaFriends(iKodRechiv);
              //}
              //else
              //{
                  fErech = GetErechRechiv(iKodRechiv, col);
              //}

              if (sSaifHilan == "223")
              { fErech = fErech / 60; }

              if ((sSaifHilan != "221" && sSaifHilan != "222" && sSaifHilan != "223" && sSaifHilan != "224")
                  || ((sSaifHilan == "221" || sSaifHilan == "222" || sSaifHilan == "223" || sSaifHilan == "224") && !bKayamEfreshBErua))
              {
                  CreateData(sSaifHilan, fErech, iLen, iNumDigit);
              }
          }
          catch (Exception ex)
          {
              WriteError("CreateData413: " + ex.Message); 
             // throw ex;
          }
      }



      private void CreatePremiyaData413(string sSaifHilan, int iKodRechiv,  int iLen, int iNumDigit)
      {
          float fErech=0;
          try
          {
              if (_iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
              {
                  fErech = GetErechRechivPremiya(iKodRechiv, _dtPrem);

                  if (iKodRechiv == clGeneral.enRechivim.PremiaMachsenaim.GetHashCode())
                  { fErech += GetErechRechiv(clGeneral.enRechivim.PremyatMachsanimYadanit.GetHashCode()); }

                  if (iKodRechiv == clGeneral.enRechivim.PremiaManasim.GetHashCode())
                  { fErech += GetErechRechiv(clGeneral.enRechivim.PremyatManasYadani.GetHashCode()); }

              }
             
              CreateData(sSaifHilan,  fErech,  iLen, iNumDigit);
          }
          catch (Exception ex)
          {
              WriteError("CreateData413: " + ex.Message);
             // throw ex;
          }
      }


      private void CreateData(string sSaifHilan, float fErech, int iLen, int iNumDigit)
      {
         // int iLastDigit;
          bKayamEfreshBErua = false;
          string sErech = "";
        //  bool flag =false;
          try
          {
              StringBuilder sErua413 = new StringBuilder();

              sErua413.Append(sSaifHilan.PadLeft(4, char.Parse("0")));
              //if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode()) // && sSaifHilan != "221" && sSaifHilan != "222" && sSaifHilan != "223" && sSaifHilan != "224" && sSaifHilan != "303" && sSaifHilan != "321")
              //{
                  sErua413.Append(_dMonth.Month.ToString().PadLeft(2, char.Parse("0")));
                  sErua413.Append(_dMonth.Year.ToString());
                  //flag = true;
                  sErech = GetBlank(11);
              //}
              //else sErech = GetBlank(17);

              sErech += FormatNumber(fErech, iLen, iNumDigit);
              sErech += GetBlank(12);

              SetFooter();
              //if (bKayamEfreshBErua && !(flag) && sSaifHilan != "438")
              //{
              //    iLastDigit = int.Parse(_sFooter.Substring(1, 1)); //int.Parse(_sFooter.Substring(_sFooter.Trim().Length - 1, 1));
              //    _sFooter = _sFooter.Substring(0, 1) + GetSimanEfresh(iLastDigit) + _sFooter.Substring(2, _sFooter.Length - 2);
              //    // _sFooter += GetSimanEfresh(iLastDigit);
              //}
              if (!IsEmptyErua(sErech.ToString()))
              {
                  sErua413.Append(sErech);
                  _ListErua.Add(_sHeader + sErua413.ToString() + _sFooter);
              }
          }
          catch (Exception ex)
          {
              WriteError("CreateData413: " + ex.Message);
              // throw ex;
          }
      }

      private void CreateData413Seif435(string sSaifHilan, string col, int iLen, int iNumDigit)
      {
          bKayamEfreshBErua = false;
          string sErech = "";
          float fErech = 0;
          try
          {
              StringBuilder sErua413 = new StringBuilder();

              sErua413.Append(sSaifHilan.PadLeft(4, char.Parse("0")));

              //if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode()) // && sSaifHilan != "221" && sSaifHilan != "222" && sSaifHilan != "223" && sSaifHilan != "224" && sSaifHilan != "303" && sSaifHilan != "321")
              //{
              //    sErua413.Append(_dMonth.Month.ToString().PadLeft(2, char.Parse("0")));
              //    sErua413.Append(_dMonth.Year.ToString());
              //    sErech = GetBlank(11);
              //}
              //else 
              sErech = GetBlank(17);

              if (_iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
              {
                  fErech = GetErechRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomTeuna.GetHashCode());
              }
              else
              {
                  fErech = GetErechRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaBoded.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalaYeled.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatHorim.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode());
                  fErech += GetErechRechiv(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode());
              }
              sErech += FormatNumber(fErech, iLen, iNumDigit);
              sErech += GetBlank(12);

              SetFooter();
            
              if (!IsEmptyErua(sErech.ToString()))
              {
                  sErua413.Append(sErech);
                  _ListErua.Add(_sHeader + sErua413.ToString() + _sFooter);
              }
          }
          catch (Exception ex)
          {
              WriteError("CreateData413: " + ex.Message);
              // throw ex;
          }
      }
    }
}
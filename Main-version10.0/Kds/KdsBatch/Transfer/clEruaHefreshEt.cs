using KDSCommon.DataModels;
using KdsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KdsBatch.Transfer
{
    public class clEruaHefreshEt : clErua
    {
   //     DataRow[] hereshim;
        public clEruaHefreshEt(long lBakashaId, DataRow drPirteyOved, DataSet dsNetunim)//,DataRow[] drsHefreshimET)
            : base(lBakashaId, drPirteyOved, dsNetunim,0)
         {
          //  hereshim = drsHefreshimET;
            oErueyHefreshEt = new List<EtHefreshLineDM>();
           _sBody = SetBody();
            if (_sBody != null)
                PrepareLines();
        }

        protected override void SetHeader()
        {
           
        }

        protected override void SetFooter()
        {
            
        }

        private void SetConstantValues(EtHefreshLineDM EtHefresh)
        {
            EtHefresh.MisparErua = 90226;
            EtHefresh.MisparHevra = 1705;
            EtHefresh.Mn = 0;
            //EtHefresh.OvedName = ""; _drPirteyOved["shem_mish"].ToString() +" "+ _drPirteyOved["shem_prat"].ToString();
            EtHefresh.KodIdkun = 1;
            EtHefresh.Tz = int.Parse(_drPirteyOved["TEUDAT_ZEHUT"].ToString());
            EtHefresh.TokefMe = _dMonth;
            EtHefresh.TokefAd = _dMonth.AddMonths(1).AddDays(-1);

        }
        protected override List<string> SetBody()
        {
           
            decimal fErech, kamut, fSumB, fSumA;
            int isuk=0;
            string sMeafyen53;

            if (_drPirteyOved["isuk"].ToString() != "")
                isuk = int.Parse(_drPirteyOved["isuk"].ToString());
            foreach (int semel in SmalimHefreshET)
            { 
                EtHefreshLineDM EtHefresh = new EtHefreshLineDM();
                SetConstantValues(EtHefresh);
                EtHefresh.Semel = semel;
                fErech = 0;
              
                switch (semel)
                {
                    case 100:
                       fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), "erech_rechiv"), 2);
                       EtHefresh.Kamut = fErech;
                       break;
                    case 1:
                        fErech = (decimal)GetErechRechiv(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), "erech_rechiv");
                        fErech += (decimal)GetErechRechiv(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), "erech_rechiv");
                        EtHefresh.Kamut = decimal.Round(fErech/60,2);
                        break;
                    case 20:
                     
                        if (isuk >= 500 && isuk < 600)
                        {
                            fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.PremyaRegila.GetHashCode(), "erech_rechiv"), 2);
                            if (fErech != 0)
                            {
                                EtHefresh.Schum = fErech;
                            }
                        }

                        break;
                    case 4:
                        sMeafyen53 = _drPirteyOved["meafyen53"].ToString();
                        if (sMeafyen53 != "")
                        {
                            if (sMeafyen53.Substring(0, 1) == "1")
                            {
                                fSumA = (decimal)GetErechRechiv(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), "erech_rechiv_a")* (decimal)GetErechRechiv(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), "erech_rechiv_a");
                                fSumB = (decimal)GetErechRechiv(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), "erech_rechiv_b") * (decimal)GetErechRechiv(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), "erech_rechiv_b");
                                if (fSumA != fSumB)
                                {
                                    fErech = fSumA - fSumB;
                                    EtHefresh.Schum = fErech;
                                    if (!bKayamEfreshBErua)
                                        bKayamEfreshBErua = true;
                                }
                            
                             }
                            else if (sMeafyen53.Substring(0, 1) == "2")
                            {
                                fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), "erech_rechiv"), 2);
                                if (fErech !=0)
                                    EtHefresh.Schum = fErech;
                            }
                        }

                       break;
                    case 63:
                        if (isuk >= 500 && isuk < 600)
                        {
                            fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.SachPitzul.GetHashCode(), "erech_rechiv"), 2);
                            EtHefresh.Kamut = fErech;
                        }
                        break;
                    case 78:
                        fErech = (decimal)GetErechRechiv(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), "erech_rechiv");
                        fErech += (decimal)GetErechRechiv(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), "erech_rechiv");
                        EtHefresh.Kamut = decimal.Round(fErech/60, 2);
                        break;
                    case 79:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), "erech_rechiv"), 2);
                        EtHefresh.Kamut = fErech;
                        break;
                    case 7:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), "erech_rechiv")/60, 2);
                        EtHefresh.Kamut = fErech;
                        break;
                    case 8:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), "erech_rechiv")/60, 2);
                        EtHefresh.Kamut = fErech;
                        break;
                    case 48:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), "erech_rechiv")/60, 2);
                        EtHefresh.Kamut = fErech;
                        break;
                    case 125:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.ETPaarBetweenMichsaRegilaAndMuktenet.GetHashCode(), "erech_rechiv")/60, 2);
                        EtHefresh.Kamut = fErech;
                        break;
                    case 85:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), "erech_rechiv")/60, 2);
                        EtHefresh.Kamut = fErech;
                        break;
                    case 77:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.NesiaBerechevMuganET.GetHashCode(), "erech_rechiv"), 2);
                        EtHefresh.Kamut = fErech;
                        break;
                  
                    case 84:
                        fErech = decimal.Round((decimal)GetErechRechiv(clGeneral.enRechivim.NesiaBerechevMuganEvenET.GetHashCode(), "erech_rechiv"), 2);
                        EtHefresh.Kamut = fErech;
                        break;


                }
               if(fErech!=0)
                oErueyHefreshEt.Add(EtHefresh);
            }


            return null;
        }
    }
}

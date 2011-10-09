using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.UDT;
using KdsLibrary;
using KdsLibrary.DAL;
using KdsLibrary.BL;

namespace KdsBatch
{
    class clCalcMonth : GlobalDataset
    {
        private DataTable _dtChishuvChodesh;
        private clCalcDay oDay;
        private int _iMisparIshi;
        private long _lBakashaId;
        private DateTime _dTaarichChishuv;
        private clCalcGeneral _oGeneralData;
        public clCalcMonth(int iMisparIshi, long lBakashaId)
        {

            try
            {
                _lBakashaId = lBakashaId;
                _iMisparIshi = iMisparIshi;
                _dtChishuvChodesh = clCalcData.DtMonth;
                _oGeneralData = new clCalcGeneral();
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId,iMisparIshi ,"E", 0,null, "CalcMonth: " + ex.Message);
               
                throw ex;
            }
        }

        public DataSet CalcMonth(int iMisparIshi,DateTime dTarMe, DateTime dTarAd)
        {
            DateTime dTaarich,dTarChishuv;
            int iSugYom;//, iStatusTipul;
            bool bChishuvYom = false;
            clUtils oUtils = new clUtils();
            clOvdim oOvdim = new clOvdim();
            clKavim oKavim = new clKavim();
            try
            {
                //iStatusTipul = clGeneral.enStatusTipul.HistayemTipul.GetHashCode();
                oDay = new clCalcDay(_iMisparIshi, _lBakashaId,_oGeneralData);
                dTarChishuv = dTarMe;
                    
                //מצב של חישוב יום בודד לצורץ ריצת שינויי קלט
                if (dTarMe == dTarAd)
                {
                    dTarMe = new DateTime(dTarMe.Year, dTarMe.Month, 1);

                    dTarAd = dTarMe.AddMonths(1).AddDays(-1);
                    bChishuvYom = true;
                    //iStatusTipul = clGeneral.enStatusTipul.Betipul.GetHashCode();
                }
              
                ////שליפת כרטיסי עבודה לחישוב
                 clCalcData.DtYemeyAvoda = GetYemeyAvodaToOved(_iMisparIshi, dTarMe, dTarAd);
                 AddRowZmanLeloHafsaka();
                 clCalcData.DtPremyot = InitDtPremyot(dTarMe, _iMisparIshi);
                 clCalcData.DtPremyotYadaniyot = oUtils.InitDtPremyotYadaniyot(_iMisparIshi,dTarMe);
                 clCalcData.sSugYechida = InitSugYechida(_iMisparIshi, dTarAd);
                 clCalcData.DtPeiluyotFromTnua = oKavim.GetKatalogKavim(iMisparIshi, dTarMe, dTarAd);
                 clCalcData.DtPirteyOvedForMonth = oUtils.GetPirteyOvedForTkufa(iMisparIshi, dTarMe, dTarAd);
                 //**clCalcData.DtParameters = oUtils.GetKdsParametrs();
                 clCalcData.DtSidurimMeyuchRechiv = SetSidurimMeyuchaRechiv(dTarMe, dTarAd);
                 clCalcData.DtSugeySidurRechiv = GetSugeySidurRechiv(dTarMe, dTarAd);
                 clCalcData.InitListMeafyenyOvedObject(iMisparIshi, dTarMe, dTarAd);
                 _dTaarichChishuv = dTarMe;
                               
                if (!bChishuvYom)
                {
                    SimunSidurimLoLetashlum();
                    ////סימון "לא לתשלום" עבור סידורי רציפות
                    //SimunLoLetashlumRetzifut();
                }

                dTaarich = dTarMe;
                 
                while (dTaarich <= dTarAd)
                {
                    if (IsDayExist(dTaarich)){
                    _oGeneralData.objMeafyeneyOved = clCalcData.ListMeafyeneyOvedMonth.Find(delegate(clMeafyenyOved Meafyenim)
                                                                                            {
                                                                                                return (Meafyenim._Taarich == dTaarich);
                                                                                            });
                   // _oGeneralData.objMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dTaarich);

                 //   iSugYom = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, dTaarich, clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                    
                    _oGeneralData.objPirteyOved = new clPirteyOved(iMisparIshi, dTaarich,"Calc");
                    _oGeneralData.objParameters = clCalcData.ListParametersMonth.Find(delegate(clParameters Params)
                                                                                    {
                                                                                        return (Params._Taarich == dTaarich);
                                                                                    });
                   // _oGeneralData.objParameters = new clParameters(dTaarich, iSugYom, "Calc");
                    
                    oDay.CalcRechiv126(dTaarich);
                    }
                    dTaarich = dTaarich.AddDays(1);
                }

                CalcMekademNipuach(dTarMe, dTarAd, iMisparIshi);

                if (bChishuvYom)
                {
                    dTarMe = dTarChishuv;
                    dTarAd = dTarChishuv;
                }

                 dTaarich = dTarMe;
                                  
                 while (dTaarich <= dTarAd)
                 {
                     if (IsDayExist(dTaarich)){
                         clCalcData.iSugYom = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, dTaarich, clCalcData.DtSugeyYamimMeyuchadim);
                         _oGeneralData.objMeafyeneyOved = clCalcData.ListMeafyeneyOvedMonth.Find(delegate(clMeafyenyOved Meafyenim)
                         {
                             return (Meafyenim._Taarich == dTaarich);
                         });
                        // _oGeneralData.objMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dTaarich);
                       //  iSugYom = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, dTaarich, clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                         _oGeneralData.objParameters = clCalcData.ListParametersMonth.Find(delegate(clParameters Params)
                         {
                             return (Params._Taarich == dTaarich);
                         });
                                
                       //  clCalcData.iSugYom = iSugYom;
                        // _oGeneralData.objParameters = new clParameters(dTaarich, iSugYom,"Calc");
                         _oGeneralData.objPirteyOved = new clPirteyOved(iMisparIshi, dTaarich,"Calc");
                        // clCalcData.DtSidurimMeyuchRechiv=SetSidurimMeyuchaRechiv(dTaarich);
                       //  clCalcData.DtSugeySidurRechiv = GetSugeySidurRechiv(dTaarich);
                         clCalcData.fTotalAruchatZaharimForDay = 0;
                         oDay.CalcRechivim(dTaarich);
                     }
                     dTaarich = dTaarich.AddDays(1);
                 }

                 if (!bChishuvYom)
                 {
                      CalcRechivimInMonth(dTarMe, dTarAd);
                      ChangingChofeshFromShaotNosafot();
                 }
                 return _dsChishuv;
            }
             catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, iMisparIshi,"E",0,null, "CalcMonth: " + ex.Message);
                throw ex;
            }
        }
        private bool IsDayExist(DateTime dDay)
        {
            DataRow[] dr;
            try
            {
                dr = clCalcData.DtYemeyAvoda.Select(" taarich=Convert('" + dDay.ToShortDateString() + "', 'System.DateTime')");
                if (dr.Length > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        private void AddRowZmanLeloHafsaka()
        {
            float zmanHafsaka,zmanSidur;
            clCalcPeilut oPeilut;
            DateTime taarich = DateTime.Now;
            try
            {
                oPeilut = new clCalcPeilut(_iMisparIshi, _lBakashaId, _oGeneralData);

                clCalcData.DtYemeyAvoda.Columns.Add("ZMAN_LELO_HAFSAKA", System.Type.GetType("System.Single"));
                clCalcData.DtYemeyAvoda.Columns.Add("ZMAN_HAFSAKA_BESIDUR", System.Type.GetType("System.Single"));
                for (int i = 0; i < clCalcData.DtYemeyAvoda.Rows.Count; i++)
                {
                    try
                    {
                        if (clCalcData.DtYemeyAvoda.Rows[i]["shat_hatchala_sidur"].ToString() != "")
                        {
                            taarich = DateTime.Parse(clCalcData.DtYemeyAvoda.Rows[i]["taarich"].ToString());
                            zmanHafsaka = oPeilut.getZmanHafsakaBesidur(int.Parse(clCalcData.DtYemeyAvoda.Rows[i]["mispar_sidur"].ToString()), DateTime.Parse(clCalcData.DtYemeyAvoda.Rows[i]["shat_hatchala_sidur"].ToString()));
                            zmanSidur = float.Parse((DateTime.Parse(clCalcData.DtYemeyAvoda.Rows[i]["shat_gmar_letashlum"].ToString()) - DateTime.Parse(clCalcData.DtYemeyAvoda.Rows[i]["shat_hatchala_letashlum"].ToString())).TotalMinutes.ToString());
                            clCalcData.DtYemeyAvoda.Rows[i]["ZMAN_LELO_HAFSAKA"] = zmanSidur - zmanHafsaka;
                            clCalcData.DtYemeyAvoda.Rows[i]["ZMAN_HAFSAKA_BESIDUR"] = zmanHafsaka;
                        }
                    }
                    catch (Exception ex)
                    {
                        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", 0, taarich, "AddRowZmanLeloHafsaka: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcMekademNipuach(DateTime dTarMe, DateTime dTarAd, int iMisparIshi)
        {
            float fCountMichsa, fCountYomLeloChag;
            int iSugYom;
            fCountMichsa = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " AND ERECH_RECHIV>0", "").Length;
            fCountYomLeloChag = 0;
            clCalcData.fMekademNipuach = 0;

            do
            {
                iSugYom = _oGeneralData.GetSugYomLemichsa(iMisparIshi, dTarMe);
                if (iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    fCountYomLeloChag += 1;
                }
                dTarMe = dTarMe.AddDays(1);
            }
            while (dTarMe <= dTarAd);

            clCalcData.fMekademNipuach = fCountYomLeloChag / fCountMichsa;

        }

        private void ChangingChofeshFromShaotNosafot()
        {
            //טיפול בחופש ע"ח שעות נוספות 
            DataRow[] drSidurimToChange, drMichsaYomit, drNosafot100, drChofesh, drDakotNochehut;
            DateTime dTaarich, dShatHatchalaSidur;
            float fMichsaYomit, fNosafot100LeOved;
            int I, iSachSidurimKuzezu, iOutMichsa, iMisparSidur;
            try 
            {
                iSachSidurimKuzezu = 0;
                drNosafot100 = _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString());
                if (drNosafot100.Length > 0)
                {
                   
                    drSidurimToChange = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=99822", "taarich asc");
                    for (I = 0; I < drSidurimToChange.Length; I++)
                    {
                        dTaarich = (DateTime)(drSidurimToChange[I]["taarich"]);
                        fMichsaYomit = 0;
                        fNosafot100LeOved = 0;
                        drMichsaYomit = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                        if (drMichsaYomit.Length > 0)
                        {
                            fMichsaYomit = (float)(drMichsaYomit[0]["ERECH_RECHIV"]);
                            fNosafot100LeOved = (float)(drNosafot100[0]["ERECH_RECHIV"]) / 60;
                            iOutMichsa = int.Parse(drSidurimToChange[I]["out_michsa"].ToString());
                            iMisparSidur = int.Parse(drSidurimToChange[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = (DateTime)(drSidurimToChange[I]["shat_hatchala_sidur"]);

                            if (fMichsaYomit <= (fNosafot100LeOved*60) && (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa) || iSachSidurimKuzezu < _oGeneralData.objParameters.iMaxYamimHamaratShaotNosafot))
                            {
                                fNosafot100LeOved = fNosafot100LeOved - (fMichsaYomit / 60);
                                drNosafot100[0]["ERECH_RECHIV"] = fNosafot100LeOved;
                                
                                // -	לעדכן את רכיב 67 כדלקמן: 
                                //•	ברמת יום עבודה – לבטל את הרשומה של הרכיב ביום העבודה אליו שייך הסידור.
                                drChofesh = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                if (drChofesh.Length > 0)
                                { drChofesh[0].Delete(); }
                                //•	ברמת החודש – ערך הרכיב פחות 1.
                                drChofesh = _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                                if (drChofesh.Length > 0)
                                { drChofesh[0]["ERECH_RECHIV"] = (float)(drChofesh[0]["ERECH_RECHIV"]) - 1; }

                                //-	לעדכן את רכיב 221 כדלקמן: 
                                drChofesh = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                //•	ברמת יום עבודה – לבטל את הרשומה של הרכיב ביום העבודה אליו שייך הסידור.
                                if (drChofesh.Length > 0)
                                { drChofesh[0].Delete(); }
                                //•	ברמת החודש – ערך הרכיב פחות מכסה יומית מחושבת (רכיב 126) ברמת יום העבודה אליו שייך הסידור
                                drChofesh = _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString());
                                if (drChofesh.Length > 0)
                                { drChofesh[0]["ERECH_RECHIV"] = (float)(drChofesh[0]["ERECH_RECHIV"]) - fMichsaYomit; }
                                
                                
                                //-	לעדכן רכיב 219:
                                //•	ברמת יום עבודה –  לבטל את הרשומה של הרכיב ביום העבודה אליו שייך הסידור
                                drChofesh = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.ShaotChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                if (drChofesh.Length > 0)
                                {
                                    drChofesh[0].Delete();
                                }
                                //	ברמת החודש – ערך רכיב 221 מעודכן חלקי 60.
                                drChofesh = _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.ShaotChofesh.GetHashCode().ToString());
                                if (drChofesh.Length > 0)
                                { drChofesh[0]["ERECH_RECHIV"] = (float)(drChofesh[0]["ERECH_RECHIV"]) - (clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString())) / 60); }

                                //-	לעדכן רכיב 1:
                                //•	ברמת יום עבודה – ערך הרכיב = ערך הרכיב הקודם + מכסה יומית מחושבת (רכיב 126) של יום העבודה
                                drDakotNochehut = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                if (drDakotNochehut.Length > 0)
                                {
                                    drDakotNochehut[0]["ERECH_RECHIV"] = (float)(drDakotNochehut[0]["ERECH_RECHIV"]) + fMichsaYomit;
                                }
                                //•	ברמת החודש - = ערך הרכיב הקודם + מכסה יומית מחושבת (רכיב 126) של יום העבודה
                                drDakotNochehut = _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString());
                                if (drDakotNochehut.Length > 0)
                                {
                                    drDakotNochehut[0]["ERECH_RECHIV"] = (float)(drDakotNochehut[0]["ERECH_RECHIV"]) + fMichsaYomit;
                                }

                                iSachSidurimKuzezu += 1;

                            }
                            else
                            {
                                drSidurimToChange[I]["Lo_letashlum"] = 1;
                            }
                        }
                        else
                        {
                            drSidurimToChange[I]["Lo_letashlum"] = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", 0,null,"ChangingChofeshFromShaotNosafot: " + ex.Message);
                throw ex;
            }
        }

        private int GetSachShabatonimInMonth(DateTime dMonth)
        {
            int iSachShabatonim, iSugYom;
            DateTime dTarMe, dTarAd;
            iSachShabatonim = 0;
            dTarMe = new DateTime(dMonth.Year, dMonth.Month,1);
            dTarAd = dTarMe.AddMonths(1).AddDays(-1);
            do
            {
                iSugYom = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, dTarMe, clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTarMe))
                {
                    iSachShabatonim = iSachShabatonim + 1;
                }
                dTarMe = dTarMe.AddDays(1);
            }
            while (dTarMe <= dTarAd);
            return iSachShabatonim;
        }

        private DataTable SetSidurimMeyuchaRechiv(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dtSidurimMeyuchaRechiv = new DataTable();
            try
            {   //מחזיר סידורים מיוחדים רכיב 
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetSidurimMeyuchRechiv, ref dtSidurimMeyuchaRechiv);

                return  dtSidurimMeyuchaRechiv;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private DataTable GetSugeySidurRechiv(DateTime dTarMe, DateTime dTarAd)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetSugeySidurRechiv, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void SimunLoLetashlumRetzifut()
        //{
        //    DataRow[] drSidurim, drSidur;
        //    int I, iMisparSidur, J, iSugYom;
        //    float fSumDakotNichehut = 0, fSumDakotNichehutChodshi = 0;
        //    clCalcSidur oSidur;
        //    clCalcPeilut oPeilut;
        //    DateTime dTaarich, dTaarichNext;
        //    dTaarichNext = DateTime.MinValue;
        //    try
        //    {
        //        oSidur = new clCalcSidur(_iMisparIshi, _lBakashaId,_oGeneralData);
        //        oPeilut = new clCalcPeilut(_iMisparIshi, _lBakashaId, _oGeneralData);

        //        drSidurim = clCalcData.DtYemeyAvoda.Select("mispar_sidur in(99500, 99501) and Lo_letashlum =0", "TAARICH ASC,MISPAR_SIDUR ASC");
        //        for (I = 0; I < drSidurim.Length; I++)
        //        {
        //            if (int.Parse(drSidurim[I]["Lo_letashlum"].ToString()) == 0)
        //            {
        //                iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
        //                dTaarich = (DateTime)drSidurim[I]["taarich"];
        //                _oGeneralData.objMeafyeneyOved = new clMeafyenyOved(_iMisparIshi, dTaarich);
                    
        //                iSugYom = clDefinitions.GetSugYom(clCalcData.DtYamimMeyuchadim, dTaarich, clCalcData.DtSugeyYamimMeyuchadim, _oGeneralData.objMeafyeneyOved.iMeafyen56);
        //                clCalcData.iSugYom = iSugYom;
        //                _oGeneralData.objParameters = new clParameters(dTaarich, iSugYom);
        //                if (fSumDakotNichehutChodshi >= _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum)
        //                {
        //                    drSidurim[I]["Lo_letashlum"] = 1;
        //                }

        //                if (!(dTaarich == dTaarichNext))
        //                {

        //                    drSidur = clCalcData.DtYemeyAvoda.Select("mispar_sidur in(99500, 99501) and Lo_letashlum =0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "TAARICH ASC");
        //                    for (J = 0; J < drSidur.Length; J++)
        //                    {
        //                        if (fSumDakotNichehut >= _oGeneralData.objParameters.iMaxRetzifutYomiLetashlum)
        //                        {
        //                            drSidur[J]["Lo_letashlum"] = 1;
        //                        }
        //                        oSidur.dTaarich = (DateTime)drSidur[J]["taarich"];
        //                        oPeilut.dTaarich =oSidur.dTaarich;
        //                        fSumDakotNichehut += oSidur.CalcRechiv1BySidur(drSidur[J], 0, oPeilut);
        //                    }
        //                    fSumDakotNichehut = 0;
        //                }
        //                oSidur.dTaarich = dTaarich;
        //                oPeilut.dTaarich=oSidur.dTaarich;
        //                fSumDakotNichehutChodshi += oSidur.CalcRechiv1BySidur(drSidurim[I], 0, oPeilut);
        //                if ((I + 1) < drSidurim.Length)
        //                {
        //                    dTaarichNext = (DateTime)drSidurim[I]["taarich"];
        //                }
        //            }
        //        }
              
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        private void SimunSidurimLoLetashlum()
        {
            DataRow[] drSidurim, drSidur;
            int I, J, iMisparSidur, iMisparSidurNext,iOutMichsa;
            float fSumDakotNichehut;
            DateTime dTaarich,dShatHatchala;
            fSumDakotNichehut = 0;
            iMisparSidurNext = 0;
            clCalcSidur oSidur;
            clCalcPeilut oPeilut;
            try
            {
                oSidur = new clCalcSidur(_iMisparIshi, _lBakashaId,_oGeneralData);
                oPeilut = new clCalcPeilut(_iMisparIshi, _lBakashaId, _oGeneralData);

                //יש לסכום לתוך X את דקות הנוכחות לתשלום (רכיב 1) של הסידורים המיוחדים לתשלום (TB_Sidurim_Ovedim.Lo_letashlum = 0) ושאינם מחוץ למכסה (( 0=TB_Sidurim_Ovedim.Out_michsa עד אשר X >= מכסה חודשית לסידור כל סידור מעבר לכך מסוג זה שאינו מחוץ למכסה (0=TB_Sidurim_Ovedim.Out_michsa) יסומן לא לתשלום TB_Sidurim_Ovedim.Lo_letashlum = 1. 

                drSidurim = clCalcData.DtYemeyAvoda.Select("SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and not Michsat_shaot_chodshit is null and Lo_letashlum =0", "MISPAR_SIDUR ASC");
                for (I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchala = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());
                    if (!clCalcData.CheckOutMichsa(_iMisparIshi, _dTaarichChishuv, iMisparSidur, dShatHatchala, iOutMichsa))
                    {
                        if (I > 0 && ((I + 1) < drSidurim.Length))
                        {
                            iMisparSidurNext = int.Parse(drSidurim[I + 1]["mispar_sidur"].ToString());
                        }
                        else if ((I + 1) == drSidurim.Length) { iMisparSidurNext = 0; }

                        if (!(iMisparSidur == iMisparSidurNext))
                        {
                            //יש לסכום לתוך X את דקות הנוכחות לתשלום (רכיב 1) של הסידורים המיוחדים לתשלום (TB_Sidurim_Ovedim.Lo_letashlum = 0) ושאינם מחוץ למכסה (( 0=TB_Sidurim_Ovedim.Out_michsa עד אשר X >= מכסה חודשית לסידור כל סידור מעבר לכך מסוג זה שאינו מחוץ למכסה (0=TB_Sidurim_Ovedim.Out_michsa) יסומן לא לתשלום TB_Sidurim_Ovedim.Lo_letashlum = 1. 

                            drSidur = clCalcData.DtYemeyAvoda.Select("mispar_sidur=" + iMisparSidur + " and not Michsat_shaot_chodshit is null and Lo_letashlum =0", "TAARICH ASC");
                            for (J = 0; J < drSidur.Length; J++)
                            {
                                dTaarich = DateTime.Parse(drSidur[J]["taarich"].ToString());
                                dShatHatchala = DateTime.Parse(drSidur[J]["shat_hatchala_sidur"].ToString());
                                oSidur.dTaarich = dTaarich;
                                iOutMichsa = int.Parse(drSidur[J]["out_michsa"].ToString());

                                if (!clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchala, iOutMichsa))
                                {
                                   if (!HaveSidurimNosafim(iMisparSidur,dTaarich))
                                    {
                                        drSidur[J]["Lo_letashlum"] = -1;
                                        if (fSumDakotNichehut >= float.Parse(drSidur[J]["Michsat_shaot_chodshit"].ToString()))
                                        {
                                            drSidur[J]["Lo_letashlum"] = 1;
                                        }
                                        else
                                        {
                                            oPeilut.dTaarich = dTaarich;
                                            fSumDakotNichehut += oSidur.CalcRechiv1BySidur(drSidur[J], 0, oPeilut);
                                        }
                                    }
                                }

                                drSidur = clCalcData.DtYemeyAvoda.Select("mispar_sidur=" + iMisparSidur + " and not Michsat_shaot_chodshit is null and Lo_letashlum =0", "TAARICH ASC");

                                for (J = 0; J < drSidur.Length; J++)
                                {
                                    dTaarich = DateTime.Parse(drSidur[J]["taarich"].ToString());
                                    oSidur.dTaarich = dTaarich;

                                    dShatHatchala = DateTime.Parse(drSidur[J]["shat_hatchala_sidur"].ToString());
                                    iOutMichsa = int.Parse(drSidur[J]["out_michsa"].ToString());

                                    if (!clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchala, iOutMichsa))
                                    {

                                        if (!HaveSidurimNosafim(iMisparSidur,dTaarich))
                                        {
                                            drSidur[J]["Lo_letashlum"] = -1;
                                            if (fSumDakotNichehut >= float.Parse(drSidur[J]["Michsat_shaot_chodshit"].ToString()))
                                            {
                                                drSidur[J]["Lo_letashlum"] = 1;
                                            }
                                            else
                                            {
                                                oPeilut.dTaarich = dTaarich;

                                                fSumDakotNichehut += oSidur.CalcRechiv1BySidur(drSidur[J], 0, oPeilut);
                                            }
                                        }
                                    }
                                }
                            }
                            fSumDakotNichehut = 0;
                        }
                    }
                }

               
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=-1", "");
                for (I = 0; I < drSidurim.Length; I++)
                {
                    drSidurim[I]["Lo_letashlum"] =0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool HaveSidurimNosafim(int iMisparSidur, DateTime dTaarich)
        {
            DataRow[] drSidurimNosafim;
            int i;
            bool bHaveSidurim = false;
            DateTime dShatHatchala;
            int iOutMichsa;
            try
            {
                drSidurimNosafim = clCalcData.DtYemeyAvoda.Select("mispar_sidur<>" + iMisparSidur + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and not Michsat_shaot_chodshit is null and Lo_letashlum =0");
                for (i = 0; i < drSidurimNosafim.Length; i++)
                {
                    dShatHatchala = DateTime.Parse(drSidurimNosafim[i]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurimNosafim[i]["out_michsa"].ToString());

                    if (!clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchala, iOutMichsa))
                    {
                        bHaveSidurim = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveSidurim;
        }

        //שליפת ימי עבודה לעובד
        private DataTable GetYemeyAvodaToOved(int iMisparIshi, DateTime dFromDate, DateTime dToDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר ימי עבודה לעובד:  
                //טבלת TB_Yamey_Avoda_Ovdim
                // Status-יכללו כל כרטיסי העבודה התקינים/הועברו לשכר (ערך 1/2 בשדה  
                //  Status_Tipul-בחודש הנבחר עבורם הסתיים טיפול – ערך "1" בשדה  
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich_me", ParameterType.ntOracleDate, dFromDate, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich_ad", ParameterType.ntOracleDate, dToDate, ParameterDir.pdInput);
                //if (iStatusTipul == 0)
                //{ 
                    oDal.AddParameter("p_status_tipul", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
               // }
               // else
               // {
               //     oDal.AddParameter("p_status_tipul", ParameterType.ntOracleInteger, iStatusTipul, ParameterDir.pdInput);
               //}
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetYemeyAvodaToOved, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        private DataTable InitDtPremyot(DateTime dTarMe,int iMisparIshi)
        {
            clDal oDal = new clDal();
            DataTable dtPremyot = new DataTable();
            int iTkufa;
            try
            {   //מחזיר פרמיות:  
                iTkufa = int.Parse(dTarMe.Year.ToString() + dTarMe.Month.ToString("00"));
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_tkufa", ParameterType.ntOracleInteger, iTkufa, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetPremyotView, ref dtPremyot);

                return dtPremyot;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private string InitSugYechida(int iMisparIshi, DateTime dTarMonth)
        {
            KdsLibrary.BL.clOvdim oOvdim = new KdsLibrary.BL.clOvdim();
            string sSugYechida;
            try {
                sSugYechida= oOvdim.GetSugYechidaLeoved(iMisparIshi,dTarMonth);
               return sSugYechida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalcRechivimInMonth(DateTime dTarMe,DateTime dTarAd)
        {
            //יש משמעות לסדר חישוב הרכיבים , אין להזיז מיקום!
            try
            {
                // מכסה יומית מחושבת רכיב 126 
                CalcRechiv126();

                // מכסה חודשית (רכיב 111)
                //CalcRechiv111();

                // פרמיה גרירה ( רכיב 112 )
                CalcRechiv112();

                //פרמיה מחסנאים ( רכיב 113)
                CalcRechiv113();

                //פרמיה משק ( רכיב 115): 
                CalcRechiv115();

                //דקות 1:1 אלמנטים(רכיב 267
                CalcRechiv267();

                //דקות נסיעה לפרמיה  (רכיב 268
                CalcRechiv268();

                //דקות 1:1 (רכיב 52
                CalcRechiv52();
                              
                // דקות בנהיגה בימי חול ( רכיב 2)
                CalcRechiv2();

                //דקות בניהול תנועה בימי חול ( רכיב 3)
                CalcRechiv3();

                //דקות בתפקיד בימי חול ( רכיב 4)
                CalcRechiv4();

                 //נוכחות חול ( רכיב 194): 
                CalcRechiv194();

                //  דקות זיכוי חופש (רכיב 7) 
                CalcRechiv7();

             //  CalcRechiv19();

               

                //  דקות לתוספת משק  (רכיב 12) 
                CalcRechiv12();

                //דקות מחליף משלח (רכיב 13) 
                CalcRechiv13();

                //דקות מחליף סדרן (רכיב 14) 
                CalcRechiv14();

                //דקות מחליף פקח(רכיב 15) 
                CalcRechiv15();

                //דקות מחליף קופאי (רכיב 16
                CalcRechiv16();

                //דקות מחליף רכז (רכיב 17) 
                CalcRechiv17();

                //דקות נוכחות בפועל(רכיב 18 
                CalcRechiv18();

                //דקות מחליף תנועה (רכיב 129): 
                CalcRechiv129();

                //CalcRechiv20();

               // CalcRechiv21();

                //דקות סיכון (רכיב 23)
                CalcRechiv23();

                // דקות בנהיגה בימי שבתון ( רכיב 35)
                CalcRechiv35();

                // דקות בניהול תנועה בימי שבתון ( רכיב 36)
                CalcRechiv36();

                // דקות בתפקיד בימי שבת ( רכיב 37)
                CalcRechiv37();


                //נוכחות שבת ( רכיב 196): 
                CalcRechiv196();

                //סהכ ק"מ  (רכיב 215)
                CalcRechiv215();

                //סה"כ לאשל בוקר (רכיב 38) 
         //       CalcRechiv38();

                //סה"כ לאשל ערב (רכיב 40)
         //       CalcRechiv40();

                //סה"כ לינה (רכיב 47 
                CalcRechiv47();

                //סה"כ לינה  כפולה (רכיב 48)
                CalcRechiv48();

                //סה"כ פיצול כפול (רכיב 50
                CalcRechiv50();

                //זמן לילה ראשון  (רכיב 54) 
                CalcRechiv54();

                //יום אבל  (רכיב 56) :
                 CalcRechiv56();

                 //יום הדרכה רכיב 57 :  
                 CalcRechiv57();

                 //יום ללא דיווח (רכיב 59) 
                 CalcRechiv59();

                 //יום מחלה (רכיב 60) :
                 CalcRechiv60();

                 //יום מחלה בודד (רכיב 61) :
                 CalcRechiv61();

                //יום מילואים (רכיב 62) :
                 CalcRechiv62();

                 //יום עבודה בחו"ל (רכיב 63): 
                 CalcRechiv63();

                 //יום תאונה  (רכיב 64) : 
                 CalcRechiv64();

                  //יום שמירת הריון בת-זוג  (רכיב 65):
                 CalcRechiv65();

                //יום טיפת חלב  (רכיב 68) :
                 CalcRechiv68();

                 //יום מחלת בן-זוג  (רכיב 69) :
                 CalcRechiv69();

                 //יום מחלת הורים (רכיב 70) :
                 CalcRechiv70();

                  //יום מחלת ילד (רכיב 71) :
                 CalcRechiv71();

                 //יום הסבה לקו (רכיב 72) : 
                 CalcRechiv72();

                //  CalcRechiv74();

                 //ימי נוכחות לעובד (רכיב 75) 
                 CalcRechiv75();

                //דקות מחוץ למכסה תפקיד חול (רכיב 80):
                CalcRechiv80();

                 //קיזוז דקות התחלה-גמר  (רכיב 86):
                 CalcRechiv86();

                 //מחוץ למכסה תפקיד בשבת (רכיב 186): 
                 CalcRechiv186();

                 //מחוץ למכסה ניהול תנועה בשבת (רכיב 187): 
                 CalcRechiv187();

                 //סה"כ דקות מחוץ למכסה בשבת (רכיב 81) : 
                 CalcRechiv81();

                //סה"כ לפיצול (רכיב 49)
                CalcRechiv49();

                //שעות 25% (רכיב 91
                CalcRechiv91();

                //שעות 50% (רכיב 92
                CalcRechiv92();

                //זמן לילה שני אחרי 2200 (רכיב 55)
                CalcRechiv55();
               
                // קיזוז זכות חופש (רכיב 8
                CalcRechiv8();

                //זמן להמרת שעות שבת (רכיב 53) 
                CalcRechiv53();

                //חופש זכות (רכיב 132):
                CalcRechiv132();

                //סהכ דקות הסתגלות (רכיב 214)
                CalcRechiv214();

                //דקות פרמיה בשבת  (רכיב 26 ) 
               CalcRechiv26();

                //דקות פרמיה יומית  (רכיב 30)
                CalcRechiv30();

                //פרמיה רגילה (רכיב 133): 
                CalcRechiv133();

                //סהכ ק"מ ויזה לפרמיה  (רכיב 216
                CalcRechiv216();

                //דקות פרמיה ויזה (רכיב 28) 
                CalcRechiv28();

                //דקות פרמיה ויזה בשבת (רכיב 29) 
                CalcRechiv29();

                // CalcRechiv39();

                //פרמיה נמל"ק (רכיב 134):
                CalcRechiv134();

                //דקות פרמיה בתוך המכסה  (רכיב 27) 
                CalcRechiv27();

                //חישוב קיזוז עבור סידורים מיוחדים בעלי מכסה חודשית. 
                CalcRechiv135();
                CalcRechiv136();
                CalcRechiv137();
                CalcRechiv138();
                CalcRechiv139();
                CalcRechiv140();
                CalcRechiv141();
                 CalcRechiv145();
                CalcRechiv185();

                //קיזוז נוכחות ( רכיב 142): 
                CalcRechiv142();

                //דקות נוכחות לתשלום( רכיב 1)
                CalcRechiv1();

                //ימי נוכחות לעובד (רכיב 109)
                CalcRechiv109();

                //משמרת שנייה במשק (רכיב 125) 
                CalcRechiv125();
                
                //CalcRechiv87();

                //נוכחות לפרמיה - רישום ( רכיב 209): 
                CalcRechiv209();

                //פרמיה לרישום ( רכיב 204
                CalcRechiv204();

                //נוספות שישי ( רכיב 198)
                CalcRechiv198();

                //נוספות שבת (רכיב 78) 
                CalcRechiv78();

                //זמן  ארוחת צהרים  רכיב 88
                CalcRechiv88();

                //קיזוז זמן בויזות (רכיב 89) 
                CalcRechiv89();

                //קיזוז לעובד מותאם (רכיב 90
                CalcRechiv90();

                //תוספת זמן הלבשה (רכיב 93)
                CalcRechiv93();

                //תוספת זמן השלמה – רכיב 94
                CalcRechiv94();

                //תוספת זמן נסיעה – רכיב 95  
                CalcRechiv95();

                //תוספת רציפות 1-1(נהגות)   - רכיב 96:  
                CalcRechiv96();

                //תוספת רציפות תפקיד   - רכיב 97:  
                //CalcRechiv97(); בוטל

                //רציפות לזמן לילה    - רכיב 272:  
                CalcRechiv272();

                //סה"כ קיזוזים  (רכיב 83):
                CalcRechiv83();

                //סה"כ תוספת רציפות (רכיב 85)
                CalcRechiv85();

                //זמן גרירות (רכיב 128 ) 
                CalcRechiv128();

                //CalcRechiv144();

                //דקות חופש (רכיב 221) 
                CalcRechiv221();

                //שעות חופש (רכיב 219) 
               CalcRechiv219();

                //שבת/שעות 100% (רכיב 131): 
                CalcRechiv131();

                //שעות % 100 לתשלום – (רכיב 100)
                CalcRechiv100();

                //דקות שבת ( רכיב 33)
                CalcRechiv33();

                //דקות היעדרות (רכיב 220) 
                CalcRechiv220();

                // שעות היעדרות ( רכיב 5) 
                CalcRechiv5();

                //דקות רגילות (רכיב 32
                CalcRechiv32();

                //סה"כ לאשל צהריים (רכיב 42)
          //      CalcRechiv42();

                //כמות גמול חסכון (רכיב 22) 
                CalcRechiv22();

                //סה"כ לאשל בוקר למבקרים בדרכים (רכיב 39) 
                CalcRechiv39();

                //סה"כ לאשל ערב למבקרים בדרכים (רכיב 41)
                CalcRechiv41();

                //סה"כ לאשל צהריים למבקרים בדרכים   (רכיב 43 )
                CalcRechiv43();

                // מכסת שעות נוספות תפקיד חול (רכיב 143
                CalcRechiv143(dTarMe,dTarAd);

                //דקות לתוספת מיוחדת בתפקיד  - תמריץ (רכיב 10)
                CalcRechiv10();
                  
                //השלמה ע"ח פרמיה (רכיב 149): 
                //CalcRechiv149();

                ////CalcRechiv150();
                ////CalcRechiv151();
                ////CalcRechiv152();
                ////CalcRechiv153();
                ////CalcRechiv154();
                ////CalcRechiv155();
                ////CalcRechiv156();
                ////CalcRechiv157();
                ////CalcRechiv158();
                ////CalcRechiv159();

                ////דקות מחוץ למכסה פקח (רכיב 164): 
                //CalcRechiv164();

                ////דקות מחוץ למכסה סדרן (רכיב 165
                //CalcRechiv165();

                ////דקות מחוץ למכסה רכז (רכיב 166)
                //CalcRechiv166();

                ////דקות מחוץ למכסה משלח (רכיב 167):  
                //CalcRechiv167();

                ////CalcRechiv168();

                ////קיזוז נוספות פקח (רכיב 169): 
                //CalcRechiv169();

                ////קיזוז נוספות סדרן (רכיב 170 
                //CalcRechiv170();

                ////קיזוז נוספות רכז (רכיב 171
                //CalcRechiv171();

                ////קיזוז נוספות משלח (רכיב 172): 
                //CalcRechiv172();

                //חישוב קיזוז עבור סידורים מיוחדים בעלי מכסה חודשית. 
                CalcRechiv174();
                CalcRechiv175();
                CalcRechiv176();
                CalcRechiv177();
                CalcRechiv178();

                //קיזוז מחליף תנועה (רכיב 130): 
                CalcRechiv130();

                //סהכ דקות פקח (רכיב 179):  
                CalcRechiv179();

                //סהכ דקות סדרן (רכיב 180)
                CalcRechiv180();

                //סהכ דקות רכז (רכיב 181
                CalcRechiv181();

                //סהכ דקות משלח (רכיב 182):
                CalcRechiv182();

                //סהכ דקות קופאי (רכיב 183)
                CalcRechiv183();

                //דקות נהגות בשישי ( רכיב 189): 
                CalcRechiv189();

                //סהכ נהגות (רכיב 188): 
                CalcRechiv188();

                // דקות תפקיד בשישי ( רכיב 193)
               CalcRechiv193();

                //סהכ תפקיד (רכיב 192):  
                CalcRechiv192();

                // דקות ניהול תנועה בשישי ( רכיב 191): 
                CalcRechiv191();

                //נוכחות שישי ( רכיב 195)
                CalcRechiv195();

                //נוספות 125% (רכיב 76): 
                CalcRechiv76();

                //נוספות 150% (רכיב 77): 
                CalcRechiv77();

                //שעות % 125 לתשלום – (רכיב 101) 
                CalcRechiv101();

                //שעות % 150 לתשלום – (רכיב 102) 
                CalcRechiv102();

                //סהכ ניהול תנועה (רכיב 190): 
                CalcRechiv190();

                //דקות נוספות בנהגות (רכיב 19)/ דקות נוספות בניהול תנועה (רכיב 20)/ דקות נוספות בתפקיד (רכיב 21): 
                CalcRechiv19_20_21();

                //סה"כ שעות נוספות חול רכיב 82:
                CalcRechiv82();

                //נוספות 100% לעובד חודשי 
                CalcRechiv146();

               
                // CalcRechiv148();

                // נוספות תנועה חול (רכיב 160)קיזוז
                CalcRechiv160();

                //CalcRechiv162();
               

                //ימי עבודה  ללא מיוחדים (רכיב 110
                CalcRechiv110();

                //דקות מחוץ למכסה ניהול תנועה חול (רכיב 184)
                CalcRechiv184();

                //מחוץ למכסה חול ( רכיב 200): 
                CalcRechiv200();

                //נוכחות לפרמיה – מחסן כרטיסים ( רכיב 223 
                CalcRechiv223();

                //פרמית מחסן כרטיסים (רכיב 206)
                CalcRechiv206();

                //מחוץ למכסה בתפקיד שישי (רכיב 207): 
                CalcRechiv207();

                //נוספות תפקיד חול ושישי לאחר קיזוז (רכיב 250) 
                CalcRechiv250();

                // קיזוז נוספות תפקיד חול (רכיב 147)
                CalcRechiv147();

                //קיזוז שעות 100% (רכיב 122):  קיזוז שעות 125% (רכיב 119): קיזוז שעות 150% (רכיב 120):
                CalcRechiv119_120_122();

                //כמות גמול חסכון נוספות (רכיב 44) 
                CalcRechiv44();

                //סה''כ גמול חסכון  (רכיב 45) 
                CalcRechiv45();

                //מחוץ למכסה בניהול תנועה שישי (רכיב 208): 
                CalcRechiv208();

                //מכסת שנ ניהול תנועה (רכיב 253): 
                CalcRechiv253();

                //מכסת שנ תפקיד בשבת (רכיב 254): 
                CalcRechiv254();

                //מכסת שנ ניהול תנועה בשבת (רכיב 255): 
                CalcRechiv255();

                //קיזוז שעות 200% (רכיב 121)/ קיזוז נוספות תפקיד שבת (רכיב 161)/ קיזוז נוספות תנועה שבת (רכיב 163): 
                CalcRechiv121_161_163();

                //שעות % 200 לתשלום – (רכיב 103)  
                CalcRechiv103();

                //סה"כ קיזוז שעות נוספות (רכיב 108)
                CalcRechiv108();

                //נוספות תנועה חול ושישי לאחר קיזוז (רכיב 251) 
                CalcRechiv251();

                //נוספות נהגות חול ושישי לאחר קיזוז (רכיב 252) 
                CalcRechiv252();

                //סה"כ שעות נוספות – (רכיב 105) 
                CalcRechiv105();

                //יום היעדרות  (רכיב 66) 
                CalcRechiv66();

                //יום חופש  (רכיב 67) 
                CalcRechiv67();

                UpdateRechiv146();

                //דקות לתוספת מיוחדת בנהגת  - תמריץ (רכיב 11) 
                CalcRechiv11();

                //נוכחות לפרמיה – משק מוסכים (רכיב 211) 
                CalcRechiv211();

                //נוכחות לפרמיה – משק אחסנה (רכיב 212)
                CalcRechiv212();

                //נוכחות לפרמיה – משק גרירה (276)
                CalcRechiv276();

                //נוכחות לפרמיה – משק כוננות גרירה (277)
                CalcRechiv277();

                //מחוץ למכסה שישי ( רכיב 201): 
                CalcRechiv201();

                //דקות פרמיה בשישי  (רכיב 202 ) 
                CalcRechiv202();

                //דקות פרמיה ויזה בשישי (רכיב 203) :
                CalcRechiv203();

                //סהכ נסיעות (רכיב 213)
                CalcRechiv213();

                //דקות הגדרה (רכיב 217) :
                CalcRechiv217();

                //דקות כיסוי תור (רכיב 218
                CalcRechiv218();

                //נוכחות לפרמיית נהגי טנדרים (רכיב 235)
                CalcRechiv235();

                //פרמית רשת ביטחון למשמרת שנייה (רכיב 242)
                CalcRechiv242();

                //פרמית פירוק ושיפוץ מכללים (רכיב 243) 
                CalcRechiv243();

                //דמי נסיעה לאגד תעבורה (רכיב 244) ) 
                CalcRechiv244();

                //אש"ל לאגד תעבורה (רכיב 245) 
                CalcRechiv245();

                //נוכחות לפרמיה – מבקרים בדרכים ( רכיב 210): 
                CalcRechiv210();

                //פרמית מבקרים בדרכים (רכיב 224
                CalcRechiv224();

                //פרמית מפעל ייצור (רכיב 225) 
                CalcRechiv225();

                //פרמית נהגי תובלה (רכיב 226) :
                CalcRechiv226();

                //פרמית נהגי טנדרים (רכיב 227) 
                CalcRechiv227();

                //פרמית דפוס (רכיב 228
                CalcRechiv228();

                //פרמית מסגרות/אחזקה/נגר/רפד (רכיב 229) 
                CalcRechiv229();

                //פרמית גיפור (רכיב 230) :
                CalcRechiv230();

                //פרמית מוסך ראשלצ (רכיב 231)
                CalcRechiv231();
                               
                //פרמית טכנאי ייצור (רכיב 232
                CalcRechiv232();

                //נוכחות לפרמיית מפעל ייצור (רכיב 233
                CalcRechiv233();

                //נוכחות לפרמיית נהגי תובלה (רכיב 234) 
                CalcRechiv234();

                //נוכחות לפרמיית דפוס (רכיב 236) 
                CalcRechiv236();

                //נוכחות לפרמיית אחזקה (רכיב 237) 
                CalcRechiv237();

                //נוכחות לפרמיית גיפור (רכיב 238) 
                CalcRechiv238();

                //נוכחות לפרמיית מוסך ראשלצ (רכיב 239) 
                CalcRechiv239();

                //נוכחות לפרמיית טכנאי ייצור (רכיב 240) 
                CalcRechiv240();

                //נוכחות לפרמיית פירוק ושיפוץ מכללים (רכיב 241) 
                CalcRechiv241();

                //נוכחות לפרמית מנהל בית ספר לנהיגה ( רכיב 246): 
                CalcRechiv246();

                //פרמיה מנהל בית ספר לנהיגה ( רכיב 247): 
                CalcRechiv247();

                //יום חופש עקב אי דיווח (רכיב 248) : 
                CalcRechiv248();

                //יום היעדרות עקב אי דיווח (רכיב 249) : 
                CalcRechiv249();

                //מחלה יום מלא (רכיב 261) 
                CalcRechiv261();

                //מחלה יום חלקי (רכיב 262) : 
                CalcRechiv262();

                //נוכחות לפרמיית מנ"סים (רכיב 256) 
                CalcRechiv256();

                //נוכחות לפרמיית מתכנן תנועה (רכיב 257)  
                CalcRechiv257();

                //נוכחות לפרמיית סדרן (רכיב 258) 
                CalcRechiv258();

                //נוכחות לפרמיית רכז (רכיב 259)  
                CalcRechiv259();

                //נוכחות לפרמיית פקח (רכיב 260) 
                CalcRechiv260();

                //השלמה בנהגות – רכיב 263 
                CalcRechiv263();

                //השלמה בניהול תנועה – רכיב 264 :  
                CalcRechiv264();

                //השלמה בתפקיד – רכיב 265 :  
                CalcRechiv265();

                //יום מילואים חלקי (רכיב 266) 
                CalcRechiv266();

                //דקות חופש/היעדרות (רכיב 269) 
                CalcRechiv269();

                //ימי חופש/היעדרות (רכיב 270) :
                CalcRechiv270();

                //זמן לילה סידורי בוקר (רכיב 271)
                CalcRechiv271();

                //רכיב 931 – הלבשה תחילת יום
                CalcRechiv931();

                //רכיב 932 - הלבשה סוף יום
                CalcRechiv932();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalcRechiv1()
        {
            try
            {
            float fSumDakotRechiv;//,fKizuzKaytanaTafkid,fKizuzKaytanaYeshivatTzevet,fKizuzKaytanaBniaPeruk,fKizuzKaytanaShivuk;

            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode()));
            
            //fKizuzKaytanaTafkid= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode().ToString()));
            //fKizuzKaytanaYeshivatTzevet= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode().ToString()));
            //fKizuzKaytanaBniaPeruk= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode().ToString()));
            //fKizuzKaytanaShivuk = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode().ToString()));

            //fSumDakotRechiv = fSumDakotRechiv -(fKizuzKaytanaTafkid + fKizuzKaytanaYeshivatTzevet + fKizuzKaytanaBniaPeruk + fKizuzKaytanaShivuk);
            addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fSumDakotRechiv);
           
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv2()
        {
            float fSumDakotRechiv;
            try
            {
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode()));
            addRowToTable(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNehigaChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv3()
        {
            float fSumDakotRechiv;
            try
            {
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv4()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotTafkidChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv5()
        {
            float fSumDakotRechiv;
            try
            {
               // fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotHeadrut.GetHashCode().ToString()));
               //if (_oGeneralData.objMeafyeneyOved.iMeafyen56==clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
               //{
               //    fSumDakotRechiv = fSumDakotRechiv * clCalcGeneral.CalcMekademNipuach(_dTaarichChishuv,_iMisparIshi);
               //}
                 fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHeadrut.GetHashCode().ToString()));
                 fSumDakotRechiv = fSumDakotRechiv / 60;
                addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ShaotHeadrut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv7()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString()));
                if (fSumDakotRechiv != 0)
                {
                    addRowToTable(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
         }

        private void CalcRechiv8()
        {
            //•	X (קיזוז נוספות תנועה בשבת) = [דקות שבת בתנועה ללא מחוץ למכסה] מינוס מכסת תנועה בשבת (סך שבתונים בחודש [זיהוי שבתון (תאריך)] * 6).
            //•	Y (קיזוז נוספות תפקיד בשבת) =  [דקות שבת בתפקיד ללא מחוץ למכסה] מינוס מכסת תפקיד בשבת (סך שבתונים בחודש [זיהוי שבתון (תאריך)] * 6).
            //יש לכלול בחישוב רק סידורים עבורם שדה מחוץ למכסה [[TB_Sidurim_Ovedim.Out_michsa = 0 או Null.
            float fSumDakotRechiv, fTempX, fTempY, fSachDakot36, fSachDakot37,fMichsatShabat;
            int iSachShabatonim;
            try
            {
            iSachShabatonim =GetSachShabatonimInMonth(_dTaarichChishuv);

            fSachDakot37 = oDay.GetRechiv37OutMichsa();
            fMichsatShabat = (_oGeneralData.objMeafyeneyOved.iMeafyen16>-1 ? _oGeneralData.objMeafyeneyOved.iMeafyen16 : 0);
                if (!_oGeneralData.objMeafyeneyOved.Meafyen16Exists)
                {
                    fMichsatShabat = iSachShabatonim * 6 * 60;
                }

                fTempY = fSachDakot37 - fMichsatShabat;

            fSachDakot36 = oDay.GetRechiv36OutMichsa();
            fMichsatShabat = (_oGeneralData.objMeafyeneyOved.iMeafyen17>-1? _oGeneralData.objMeafyeneyOved.iMeafyen17 : 0);
            if (!_oGeneralData.objMeafyeneyOved.Meafyen17Exists)
            {
                fMichsatShabat = iSachShabatonim * 6 * 60;
            }


            fTempX = fSachDakot36 - fMichsatShabat;

            if ((fTempY + fTempX) > 0)
            {
                fSumDakotRechiv = fTempY + fTempX;
                if (fSumDakotRechiv != 0)
                {
                    addRowToTable(clGeneral.enRechivim.KizuzZchutChofesh.GetHashCode(), fSumDakotRechiv);
                }
            }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzZchutChofesh.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv10()
        {
            float fSumDakotRechiv,fNosafotMichutzLamichsa, fTempX, fTempY, fTempZ, fTempW, fDakotTamritzTafkid, fMichsatNosafotTafkid;
            float fZmanHalbasha, fZmanNesiot, fZmanRetzifut;
            try
            {
          
                if (_oGeneralData.objMeafyeneyOved.iMeafyen54 > 0)
                {
                    //	X  [סה"כ ש"נ לחודש] = סכום ערך הרכיב של כל הימים בחודש חלקי 60 (כדי לקבל ערך בשעות) 
                    fDakotTamritzTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode().ToString()));
                    fMichsatNosafotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode().ToString()));
                    fTempX=(fDakotTamritzTafkid / 60);
                    if (fTempX > fMichsatNosafotTafkid)
                    {
                       // Z [ש"נ מחוץ למכסה] =  (סכום ערך הרכיב עבור כל הסידורים בחודש המסומנים מחוץ למכסה TB_Sidurim_Ovedim.Out_michsa = 1) חלקי 60 (לחלק ב-60 לקבלת נתון בשעות)
                        fNosafotMichutzLamichsa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "OUT_MICHSA=0 AND  KOD_RECHIV=" + clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode().ToString()));

                        fTempZ = fNosafotMichutzLamichsa/60;

                        //Y [קיזוז ש"נ]  = הגבוה מבין (0,  X פחות מכסת נוספות בתפקיד (רכיב 143) פחות Z)
                        fTempY = Math.Max(0,( fTempX -fMichsatNosafotTafkid- fTempZ));

                        //	W = X פחות Y [ש"נ אחרי קיזוז]  + זמן נסיעות (רכיב 95) + זמן הלבשה (רכיב 93) +  זמן רציפות (רכיב 97).
                        fZmanHalbasha = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHalbasha.GetHashCode().ToString()));
                        fZmanNesiot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString()));
                        fZmanRetzifut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString()));

                        fTempW = fTempX - fTempY + fZmanHalbasha + fZmanNesiot + fZmanRetzifut;

                        if (fTempW > _oGeneralData.objParameters.iTamrizNosafotLoLetashlum)
                        {
                            fSumDakotRechiv = Math.Min(_oGeneralData.objParameters.iMaxNosafotTafkid, (fTempW - _oGeneralData.objParameters.iTamrizNosafotLoLetashlum));
                            addRowToTable(clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), fSumDakotRechiv);
                        }
                       
                    }
              
            }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv11()
        {
            float fSumDakotRechiv, fDakotNosafotNahagut, fDakotNosafot100;
            try
            {

                if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                {

                    if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == 52 || _oGeneralData.objMeafyeneyOved.iMeafyen56 == 62)
                    {
                        fDakotNosafot100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));
                        if (fDakotNosafot100 > _oGeneralData.objParameters.iTamrizNosafotLoLetashlum)
                        {
                            fSumDakotRechiv = Math.Min(_oGeneralData.objParameters.iMaxNosafotNahagut, fDakotNosafot100 - _oGeneralData.objParameters.iTamrizNosafotLoLetashlum);
                            addRowToTable(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), fSumDakotRechiv);
                        }
                    }
                    else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == 51 || _oGeneralData.objMeafyeneyOved.iMeafyen56 == 61)
                    {
                        fDakotNosafotNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode().ToString()));
                        if (fDakotNosafotNahagut > _oGeneralData.objParameters.iTamrizNosafotLoLetashlum)
                        {
                            fSumDakotRechiv = Math.Min(_oGeneralData.objParameters.iMaxNosafotNahagut, (fDakotNosafotNahagut/60) - _oGeneralData.objParameters.iTamrizNosafotLoLetashlum);
                            addRowToTable(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), fSumDakotRechiv);
                        }

                    }
                       ////if (_oGeneralData.objMeafyeneyOved.iMeafyen54 > 0)
                       // //{
                       //     fDakotNosafotNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode().ToString()));
                       //     if ((fDakotNosafotNahagut / 60) > _oGeneralData.objParameters.iTamrizNosafotLoLetashlum)
                       //     {
                       //         fSumDakotRechiv = Math.Min(_oGeneralData.objParameters.iMaxNosafotNahagut, (fDakotNosafotNahagut / 60) - 30);
                       //         addRowToTable(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), fSumDakotRechiv);

                       //     }
                       // //}
                   
                }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv12()
        {
            float fSumDakotRechiv;
            try
            {
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode().ToString()));
           addRowToTable(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv13()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), fSumDakotRechiv);
            } 
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
                
        private void CalcRechiv14()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifSadran.GetHashCode().ToString()));
              addRowToTable(clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv15()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifPakach.GetHashCode().ToString()));
             addRowToTable(clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv16()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifKupai.GetHashCode().ToString()));
              addRowToTable(clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv17()
        {
            float fSumDakotRechiv;

            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode().ToString()));
            
           addRowToTable(clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv18()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv19_20_21()
        {
            float fSumDakotRechiv19, fSumDakotRechiv20, fSumDakotRechiv21;
            //float fTempY, fTempZ, fShaotNihulLeloMichutz, fNosafotMichutz, fMichsatTafkidChol, fDakotMichutzChol, fSumDakotRechiv, fShaotTafkidLeloMichutz;
            //int iMichsatDakotNosafot;
            //float fMichsatNosafot;
            //fTempZ =0;
           try{
            fSumDakotRechiv19 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode().ToString()));
            fSumDakotRechiv20 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNihul.GetHashCode().ToString()));
            fSumDakotRechiv21 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString()));

          //  //עבור דקות נוספות בנהגות (רכיב 19): אין קיזוז 
          //  if (_oGeneralData.objMeafyeneyOved.Meafyen11Exists)
          //  {
          //      if (_oGeneralData.objMeafyeneyOved.iMeafyen11 == -1)
          //      {
          //          throw new Exception("null מאפיין 11 התקבל עם ערך");
          //      }
          //      iMichsatDakotNosafot = _oGeneralData.objMeafyeneyOved.iMeafyen11 * 60;
          //      fSumDakotRechiv = Math.Min(fSumDakotRechiv19, iMichsatDakotNosafot);
          //      addRowToTable(clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), fSumDakotRechiv);
          //  }

          //  //עבור דקות נוספות בניהול תנועה (רכיב 20):
         
          //  fSumDakotRechiv = fSumDakotRechiv20 / 60;

          //  fNosafotMichutz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString()));
            
          // if (_oGeneralData.objPirteyOved.iGil==clGeneral.enKodGil.enKshishon.GetHashCode())
          // {
          //     fTempZ = 3;
          // }
          // else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
          // {
          //     fTempZ = 6;
          // }

          
          // if (_oGeneralData.objMeafyeneyOved.iMeafyen13 == -1)
          // {
          //     throw new Exception("null מאפיין 13 התקבל עם ערך");
          //}
          // else
          //  {
          //      fMichsatNosafot = _oGeneralData.objMeafyeneyOved.iMeafyen13 + fTempZ;
          // }

          // fShaotNihulLeloMichutz = (fSumDakotRechiv - fNosafotMichutz) / 60;
          // if (fShaotNihulLeloMichutz > fMichsatNosafot)
          // {
          //     fTempY = fNosafotMichutz;
          // }
          // else { fTempY = fShaotNihulLeloMichutz; }

          //  fSumDakotRechiv = (60 + fNosafotMichutz) * fTempY;

          //  addRowToTable(clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), fSumDakotRechiv);
           

          //  //עבור דקות נוספות בתפקיד (רכיב 21):
          //  fSumDakotRechiv21 = fSumDakotRechiv21 / 60;
          //  fDakotMichutzChol= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString()));
          //  fMichsatTafkidChol= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode().ToString()));

          //  fShaotTafkidLeloMichutz = (fSumDakotRechiv21 - fDakotMichutzChol) / 60;
          //  if (fShaotTafkidLeloMichutz > fMichsatTafkidChol)
          //  {
          //      fSumDakotRechiv = fMichsatTafkidChol;
          //  }
          //  else
          //  { 
          //      fSumDakotRechiv = fShaotTafkidLeloMichutz;
          //  }

          //  fSumDakotRechiv = (fSumDakotRechiv * 60 )+ fDakotMichutzChol;
            addRowToTable(clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), fSumDakotRechiv20);
            addRowToTable(clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), fSumDakotRechiv19);
            addRowToTable(clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), fSumDakotRechiv21);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv22()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachon.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv23()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotSikun.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv26()
        {
            float fSumDakotRechiv;
            try
            {
             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv27()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv28()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv29()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv30()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString()));
               addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv32()
        {
            float fDakotNochehut,fMichsaChodhit;
            float fSumDakotRechiv=0;
            try{
                if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotRegilot.GetHashCode().ToString()));
                }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                    
                {
                    fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString())); ;
                    fMichsaChodhit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString())); ;
                    
                    if (fDakotNochehut>fMichsaChodhit)
                    { 
                        fSumDakotRechiv = fMichsaChodhit;
                    }
                    else
                    {
                        fSumDakotRechiv = fDakotNochehut;
                    }
                }
               addRowToTable(clGeneral.enRechivim.DakotRegilot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotRegilot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv33()
        {
            //יש לפתוח רשומה לרכיב רק אם סכום שלושת הרכיבים > 0
            //ערך הרכיב = דקות שבת בנהגות (רכיב 35)  +  דקות שבת בניהול תנועה (רכיב 36)  +  דקות שבת בתפקיד (רכיב 37).

            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString()));

             addRowToTable(clGeneral.enRechivim.DakotShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
      }

        private void CalcRechiv35()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv36()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv37()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv =clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv38()
        {
            float fSumDakotRechiv;
            try
            {
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelBoker.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachEshelBoker.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachEshelBoker.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv39()
        {
            float fSumDakotRechiv;
            try
            {
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode().ToString()));
             addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }  
        }

        private void CalcRechiv40()
        {
            float fSumDakotRechiv;
            try
            {
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelErev.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachEshelErev.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachEshelErev.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv41()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode().ToString()));

            addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv42()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv43()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode().ToString()));
             addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv44()
        {
            float fSumDakotRechiv,fTempY,fTempW,fTempZ;
           
           //א.	אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך = 25 אזי:
            //Y = ערך רכיב דקות נוספות תפקיד (רכיב 21)
            //אחרת
            //Y = סכימת ערך הרכיב עבור כל ימי העבודה בחודש. 
            try{
          
             fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode().ToString()));
           
            //ב.	W =  מקדם ניפוח 0.83 [שליפת פרמטר (קוד פרמטר = 177)] רלוונטי עבור עובדי 6 ימים בלבד [שליפת מאפיין ביצוע (קוד מאפיין=56)] עם ערך = 61 אחרת W = 1 (לעובדי 5 ימים).
            if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
            {
                fTempW = clCalcData.fMekademNipuach;
            }
            else
            {
                fTempW = 1;
            }

            if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode())
            {
                fTempZ = _oGeneralData.objParameters.iGmulNosafotTzair;
            }
            else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
            {
                fTempZ = _oGeneralData.objParameters.iGmulNosafotKshishon;
            }
              else
            {
                fTempZ = _oGeneralData.objParameters.iGmulNosafotKashish;
            }

            fSumDakotRechiv = (fTempY / fTempZ) * fTempW;
            if (fSumDakotRechiv > 0)
            {
                addRowToTable(clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), float.Parse(fSumDakotRechiv.ToString("00")));
            }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv45()
        {
            float fSumDakotRechiv, fKamutGmulNosafot, fKamutGmulChisachon;
            try
            {
                fKamutGmulChisachon = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachon.GetHashCode().ToString()));
                fKamutGmulNosafot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode().ToString()));

                fSumDakotRechiv = fKamutGmulChisachon + fKamutGmulNosafot;
                addRowToTable(clGeneral.enRechivim.SachGmulChisachon.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachGmulChisachon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv47()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachLina.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachLina.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachLina.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }


        private void CalcRechiv48()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachLinaKfula.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachLinaKfula.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachLinaKfula.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }


        private void CalcRechiv49()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachPitzul.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachPitzul.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachLinaKfula.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv50()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachPitzulKaful.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachPitzulKaful.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv52()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv53()
        {
            float fSumDakotRechiv, fZchutChofesh;
            try{
            
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode().ToString()));
                fZchutChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzZchutChofesh.GetHashCode().ToString()));

                fSumDakotRechiv = (fSumDakotRechiv - fZchutChofesh) / 60;

                addRowToTable(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), fSumDakotRechiv);
           }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv54()
        {
            float fSumDakotRechiv;
            try{
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLailaChok.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanLailaChok.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv55()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLailaEgged.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv56()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomEvel.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomEvel.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv57()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHadracha.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomHadracha.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomHadracha.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv59()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomLeloDivuach.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomLeloDivuach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv60()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMachla.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomMachla.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv61()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomMachalaBoded.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv62()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMiluim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomMiluim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv63()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomAvodaBechul.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomAvodaBechul.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv64()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomTeuna.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomTeuna.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv65()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv66()
        {
            float fSumDakotRechiv, fMichsaYomit, fNochehutChodshit, fShaotNosafot, fMichsaChodshit, fSachKizuzMeheadrut, fNosafot100;
            float fMichsaChodshitChelkit, fNochehutChodshitChelkit, fHashlama, fmichsatYom;
            DataRow[] days;
            DataRow[] dr;
            DateTime taarich;
            try
            {
                fSumDakotRechiv = 0; fmichsatYom = 0;
                fNochehutChodshitChelkit = 0;
                fMichsaChodshitChelkit = 0;
                fNochehutChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString()));
                fMichsaChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                if (_oGeneralData.objMeafyeneyOved.iMeafyen83 == 1 && _oGeneralData.objMeafyeneyOved.iMeafyen33==1)
                {
                    fSachKizuzMeheadrut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_EZER)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString()));
                    fShaotNosafot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachShaotNosafot.GetHashCode().ToString()));
                   fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                    if (fShaotNosafot > 0 && fSachKizuzMeheadrut > fShaotNosafot)
                    { fSachKizuzMeheadrut = fShaotNosafot; }
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString()));
                    fSumDakotRechiv = (fSumDakotRechiv - fSachKizuzMeheadrut) * 60 / fMichsaYomit;
                    IpusRechivim();
                }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString()));
                }
               else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit<fMichsaChodshit)
                {

                    days = _dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and ERECH_RECHIV<1 and KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                    for (int i = 0; i < days.Length; i++)
                    {
                        taarich = DateTime.Parse(days[i]["taarich"].ToString());
                        if (fmichsatYom == 0)
                            fmichsatYom = float.Parse((_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());

                        dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (dr.Length > 0)
                            fNochehutChodshitChelkit += float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                        dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (dr.Length > 0)
                            fMichsaChodshitChelkit += float.Parse(dr[0]["ERECH_RECHIV"].ToString());

                    }
                    fNosafot100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));

                    fHashlama = Math.Min((fMichsaChodshitChelkit - fNochehutChodshitChelkit) / fmichsatYom, fNosafot100);
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString()));
                    fSumDakotRechiv = fSumDakotRechiv - fHashlama;
                }
                
                addRowToTable(clGeneral.enRechivim.YomHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomHeadrut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv67()
        {

            float fSumDakotRechiv, fNochehutChodshit, fMichsaChodshit,  fSachKizuzMeheadrut, fNosafot100;
            float fMichsaChodshitChelkit, fNochehutChodshitChelkit, fHashlama, fmichsatYom;
            DataRow[] days;
            DataRow[] dr;
            DateTime taarich;
            float fShaotNosafot, fMichsaYomit;
            try
            {
                fSumDakotRechiv = 0; fmichsatYom = 0;
                fNochehutChodshitChelkit = 0;
                fMichsaChodshitChelkit = 0;
                fNochehutChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString()));
                fMichsaChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                if (_oGeneralData.objMeafyeneyOved.iMeafyen83 == 1 && _oGeneralData.objMeafyeneyOved.iMeafyen33 == 0 )
                {
                    fSachKizuzMeheadrut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_EZER)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString()));
                    fShaotNosafot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachShaotNosafot.GetHashCode().ToString()));
                    fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                    if (fShaotNosafot > 0 && fSachKizuzMeheadrut > fShaotNosafot)
                    { fSachKizuzMeheadrut = fShaotNosafot; }
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString()));
                    fSumDakotRechiv = (fSumDakotRechiv - fSachKizuzMeheadrut) * 60 / fMichsaYomit;
                    IpusRechivim();
                }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString()));
                }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit < fMichsaChodshit)
                {
                    days = _dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and ERECH_RECHIV<1 and KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                    for (int i = 0; i < days.Length; i++)
                    {
                        taarich = DateTime.Parse(days[i]["taarich"].ToString());
                        if (fmichsatYom == 0)
                            fmichsatYom = float.Parse((_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());


                        dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (dr.Length > 0)
                            fNochehutChodshitChelkit += float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                        dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (dr.Length > 0)
                            fMichsaChodshitChelkit += float.Parse(dr[0]["ERECH_RECHIV"].ToString());  
                    }
                    fNosafot100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));

                    fHashlama = Math.Min((fMichsaChodshitChelkit - fNochehutChodshitChelkit) / fmichsatYom, fNosafot100);
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString()));
                    fSumDakotRechiv = fSumDakotRechiv - fHashlama;
                }
                

                addRowToTable(clGeneral.enRechivim.YomChofesh.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomChofesh.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void UpdateRechiv146()
        {
            float fSumDakotRechiv, fHashlama, fNochehutChodshitChelkit, fMichsaChodshitChelkit, fNosafot100;
            DataRow[] days;
            DataRow[] dr;
            DateTime taarich;
            try
            {
                fSumDakotRechiv = 0;
                fNochehutChodshitChelkit = 0;
                fMichsaChodshitChelkit = 0;
                days = _dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and ERECH_RECHIV<1 and KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                for (int i = 0; i < days.Length; i++)
                {
                    taarich = DateTime.Parse(days[i]["taarich"].ToString());
                    dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (dr.Length > 0)
                        fNochehutChodshitChelkit += float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                    dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (dr.Length > 0)
                        fMichsaChodshitChelkit += float.Parse(dr[0]["ERECH_RECHIV"].ToString());  
             
                }
                fNosafot100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));
                fHashlama = Math.Min((fMichsaChodshitChelkit - fNochehutChodshitChelkit) / 60, fNosafot100);
                fSumDakotRechiv = fNosafot100 - fHashlama;
                addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E",  clGeneral.enRechivim.YomChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv68()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTipatChalav.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomTipatChalav.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomTipatChalav.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv69()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv70()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomMachalatHorim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv71()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomMachalaYeled.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv72()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv75()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv76()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Nosafot125.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv77()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Nosafot150.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv78()
        {
            float fSumDakotRechiv;
            try{
            //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NosafotShabat.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.NosafotShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NosafotShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv80()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

         private void CalcRechiv81()
        {
            float fSumDakotRechiv;
            try
            {
                //מחוץ למכסה בתפקיד בשבת (רכיב 186) + מחוץ למכסה ניהול תנועה בשבת (רכיב 187)
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString()));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv82()
        {
            float fSumDakotRechiv, fNosafotNahagut, fNosafotTnua, fNosafotTafkid;
            try{
            //ערך הרכיב = (דקות נוספות בנהגות (רכיב 19)   + דקות נוספות בניהול תנועה (רכיב 20)  + דקות נוספות בתפקיד (רכיב 21)) חלקי 60
            fNosafotNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode().ToString()));
            fNosafotTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNihul.GetHashCode().ToString()));
            fNosafotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString()));

            fSumDakotRechiv = (fNosafotNahagut + fNosafotTnua + fNosafotTafkid) / 60;
            addRowToTable(clGeneral.enRechivim.SachNosafotChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv83()
        {
            float fSumDakotRechiv, fKizuzHatchalaGmar, fKizuzVisa, fKizuzMutam, fZmanAruchatTzaharayim;
            try{
            fKizuzHatchalaGmar = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode().ToString()));
            fKizuzVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzBevisa.GetHashCode().ToString()));
            fKizuzMutam = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzOvedMutam.GetHashCode().ToString()));
            fZmanAruchatTzaharayim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString()));

            fSumDakotRechiv = (fKizuzHatchalaGmar + fKizuzVisa + fKizuzMutam + fZmanAruchatTzaharayim) / 60;
            addRowToTable(clGeneral.enRechivim.SachKizuzim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachKizuzim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv85()
        {
            float fSumDakotRechiv, fSumRetxzigfutNehiga, fSumRetzifutTafkid;
            try{
            fSumRetxzigfutNehiga = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString()));
            fSumRetzifutTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString()));

            if ((fSumRetxzigfutNehiga + fSumRetzifutTafkid) > _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha)
            {
                fSumDakotRechiv = _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha;
            }
            else {
                fSumDakotRechiv = (fSumRetxzigfutNehiga + fSumRetzifutTafkid);
            }

            addRowToTable(clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv86()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv88()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv89()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzBevisa.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.KizuzBevisa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzBevisa.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv90()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzOvedMutam.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv / 60;
            addRowToTable(clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv91()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv / 60;   
            addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Shaot25.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv92()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv / 60;
            addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Shaot50.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

       
        private void CalcRechiv93()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHalbasha.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanHalbasha.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv94()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ZmanHashlama.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanHashlama.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv95()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ZmanNesia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanNesia.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv96()
        {
            float fSumDakotRechiv=0;
            try
            {
                
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString()));
            
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv97()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv272()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv273()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv100()
        {
            float fSumDakotRechiv;
            try{

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv101()
        {
            float fSumDakotRechiv, fNosafot125;
            try{
            //	ברמה חודשית: ערך הרכיב = נוספות 125% (רכיב 76) )
            fNosafot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString()));
           
            fSumDakotRechiv = fNosafot125;
            addRowToTable(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv102()
        {
            float fSumDakotRechiv, fNosafot150, fKizuz150;
            try{
            //	ברמה חודשית : ערך הרכיב = נוספות 150% (רכיב 77)).
            fNosafot150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString()));
            fKizuz150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Kizuz150.GetHashCode().ToString()));

            fSumDakotRechiv = fNosafot150 - fKizuz150;
            addRowToTable(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }


        private void CalcRechiv103()
        {
            float fSumDakotRechiv, fNosafot200, fKizuz200;
            try{
           // 	ברמה חודשית : ערך הרכיב = נוספות 150% (רכיב 77) פחות קיזוז שעות 150% (רכיב 120).
            fNosafot200 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NosafotShabat.GetHashCode().ToString()));
            fKizuz200 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Kizuz200.GetHashCode().ToString()));

            fSumDakotRechiv = fNosafot200 - fKizuz200;
            addRowToTable(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv105()
        {
            float fSumDakotRechiv, fNosafotNahagut, fNosafotTnua,fNosafotTafkid,fShaot100;
            try{
            //ערך הרכיב =  (דקות נוספות נהגות (רכיב 19) + דקות נוספות תנועה (רכיב 20) + דקות נוספות תפקיד (רכיב 21)) חלקי 60  + שעות 100 לתשלום (רכיב 100).
                fNosafotNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode().ToString()));
                fNosafotTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode().ToString()));
                fNosafotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode().ToString()));
                fShaot100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString()));

            fSumDakotRechiv = (fNosafotNahagut + fNosafotTnua + fNosafotTafkid) / 60 + fShaot100;
            addRowToTable(clGeneral.enRechivim.SachShaotNosafot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachShaotNosafot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }


        private void CalcRechiv108()
        {
            float fSumDakotRechiv, fKizuzTafkidChol, fKizuzTnuaChol, fKizuzTafkidShanat, fKizuzTnuaShabat;//, fKizuzNahagutShabat , fKizuzNahagutChol;
            try{
            fKizuzTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode().ToString()));
            fKizuzTnuaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode().ToString()));
            //fKizuzNahagutChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotNahagutChol.GetHashCode().ToString()));
           fKizuzTafkidShanat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidShabat.GetHashCode().ToString()));
            //fKizuzNahagutShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotNahgutShabat.GetHashCode().ToString()));
            fKizuzTnuaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode().ToString()));

            fSumDakotRechiv = fKizuzTafkidChol + fKizuzTnuaChol + fKizuzTafkidShanat + fKizuzTnuaShabat;// +fKizuzNahagutShabat+ fKizuzNahagutChol ;
            addRowToTable(clGeneral.enRechivim.SachKizuzShaotNosafot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachKizuzShaotNosafot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }


        private void CalcRechiv109()
        {
            float fSumDakotRechiv, fDakotNechehut, fMichsaChodshit;
            try
            {
            
                if (_oGeneralData.objMeafyeneyOved.iMeafyen56== clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyAvoda.GetHashCode().ToString()));
                    addRowToTable(clGeneral.enRechivim.YemeyAvoda.GetHashCode(), fSumDakotRechiv);
                }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyAvoda.GetHashCode().ToString()));
                    addRowToTable(clGeneral.enRechivim.YemeyAvoda.GetHashCode(), fSumDakotRechiv);
           }
        }
        catch (Exception ex)
        {
             clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YemeyAvoda.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
            throw (ex);
        }
        }

        private void CalcRechiv110()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        //private void CalcRechiv111()
        //{
        //    float fSumDakotRechiv=0;
        //    try{
        //        if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
        //        {
        //            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

        //        }
        //        else if (_oGeneralData.objMeafyeneyOved.Meafyen2Exists)
        //        {
        //            if (_oGeneralData.objMeafyeneyOved.iMeafyen2 > 0)
        //            {
        //                fSumDakotRechiv = _oGeneralData.objMeafyeneyOved.iMeafyen2;
        //            }
        //            else 
        //            {
        //                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
        //            }
        //        }
        //       else if (_oGeneralData.objMeafyeneyOved.iMeafyen56==clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
        //           {
        //           fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
                
        //           }
               
        //    addRowToTable(clGeneral.enRechivim.MichsaChodshit.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichsaChodshit.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }       
        //}

        private void CalcRechiv112()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaChodshit(clGeneral.enSugPremia.Grira.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiaGrira.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiaGrira.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv113()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaChodshit(clGeneral.enSugPremia.Machsenaim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv115()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaChodshit(clGeneral.enSugPremia.Meshek.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiaMeshek.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiaMeshek.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv119_120_122()
        {
            float fErech,fSumDakotRechiv122, fSumDakotRechiv119, fSumDakotRechiv120, fShaot125, fTempX;
            float fShaot100,fKizuzNosafotTnua, fKizuzNosafotTafkid; //, fKizuzMachlifTnua, fKizuzVaadOvdim, fKizuzMishpatChaverim;
            try{
            fSumDakotRechiv122=0;
             fSumDakotRechiv119=0;
             fSumDakotRechiv120 = 0;

            fKizuzNosafotTnua=clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode().ToString()));
            fKizuzNosafotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode().ToString()));
            //fKizuzVaadOvdim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode().ToString()));
            //fKizuzMishpatChaverim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode().ToString()));
            //fKizuzMachlifTnua= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifTnua.GetHashCode().ToString()));

            fTempX = fKizuzNosafotTnua + fKizuzNosafotTafkid; // +fKizuzVaadOvdim + fKizuzMachlifTnua + fKizuzMishpatChaverim;

            fShaot100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()));
            fShaot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString()));

            if (fTempX <= fShaot100)
            {
                fSumDakotRechiv122 = fTempX;
                if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()).Length > 0)
                {
                    _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = float.Parse(_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) - fTempX;
                }
            }
            else if (fTempX <= (fShaot100 + fShaot125))
            {
                fSumDakotRechiv119 = fTempX - fShaot100;
                fSumDakotRechiv122 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));
                if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()).Length > 0)
                {
                    _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = 0;
                }
                fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString())) - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Kizuz125.GetHashCode().ToString()));
                if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString()).Length > 0)
                {
                    _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = fErech;
                }
            }
            else
            {
                fSumDakotRechiv120 = fTempX - (fShaot100 + fShaot125);
                fSumDakotRechiv119 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString()));
                fSumDakotRechiv122 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));
                if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()).Length > 0)
                {
                    _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = 0;
                }
                if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString()).Length > 0)
                {
                    _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = 0;
                }
                fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString())) - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Kizuz150.GetHashCode().ToString()));
                if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot150Letashlum.GetHashCode().ToString()).Length > 0)
                {
                    _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot150Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = fErech;
                }
            }

            addRowToTable(clGeneral.enRechivim.Kizuz100.GetHashCode(), fSumDakotRechiv122);
            addRowToTable(clGeneral.enRechivim.Kizuz150.GetHashCode(), fSumDakotRechiv120);
            addRowToTable(clGeneral.enRechivim.Kizuz125.GetHashCode(), fSumDakotRechiv119);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Kizuz100.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv121_161_163()
        {
            float  fNosafotShabat, fTempX, fTempY,fDakotShabat,fDakotMichutLamichsa;
            float fShaotLeloMichutz, fTempZ, fTempW, fTempM;
            //int iSachShabatonim;
            try
            {
                fNosafotShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NosafotShabat.GetHashCode().ToString()));
                if (fNosafotShabat > 0)
                {
                    //iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);
                        
                    //fTempX = (_oGeneralData.objMeafyeneyOved.iMeafyen16>-1?  _oGeneralData.objMeafyeneyOved.iMeafyen16:0);
                    //if (!_oGeneralData.objMeafyeneyOved.Meafyen16Exists)
                    //{
                    //    fTempX = iSachShabatonim * 6;
                    //}

                    //fTempY = (_oGeneralData.objMeafyeneyOved.iMeafyen17>-1 ? _oGeneralData.objMeafyeneyOved.iMeafyen17: 0);
                    //if (!_oGeneralData.objMeafyeneyOved.Meafyen17Exists)
                    //{
                    //    fTempY = iSachShabatonim * 6;
                    //}

                    fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode().ToString()));

                    fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode().ToString()));
               
                    //ה.	ערך רכיב קיזוז נוספות תפקיד שבת 
                    fDakotShabat= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString()));
                    fDakotMichutLamichsa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString()));
                    fShaotLeloMichutz = fDakotShabat - fDakotMichutLamichsa;
                    fTempZ = 0;
                    if (fTempX < (fShaotLeloMichutz / 60))
                    {
                        fTempZ = (fShaotLeloMichutz / 60) - fTempX;
                    }

                    addRowToTable(clGeneral.enRechivim.KizuzNosafotTafkidShabat.GetHashCode(), fTempZ);

                    //ח.	ערך רכיב קיזוז נוספות תנועה שבת 
                    fDakotShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString()));
                    fDakotMichutLamichsa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString()));
                    fShaotLeloMichutz = fDakotShabat - fDakotMichutLamichsa;
                    fTempW = 0;
                    if (fTempY < (fShaotLeloMichutz / 60))
                    {
                        fTempW = (fShaotLeloMichutz / 60) - fTempY;
                    }

                    addRowToTable(clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode(), fTempW);


                    fTempM = (fTempW + fTempZ)*60;
                    if (fTempM < fNosafotShabat)
                    {
                        addRowToTable(clGeneral.enRechivim.Kizuz200.GetHashCode(), fTempM);

                    }
                }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv125()
        {
            float fSumDakotRechiv;
            try{
            //יש לבצע את חישוב הרכיב רק עבור עובדים עם [שליפת מאפיין ביצוע (קוד מאפיין = 42, מ.א., תאריך)] עם ערך 1, אחרת אין לפתוח רשומה לרכיב זה בשום רמה.
            if (_oGeneralData.objMeafyeneyOved.sMeafyen42 == "1")
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), fSumDakotRechiv);
            }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

       private void CalcRechiv126()
        {
            float fSumDakotRechiv;
           try{
               if (_oGeneralData.objMeafyeneyOved.iMeafyen2 > 0)
               {
                   fSumDakotRechiv = _oGeneralData.objMeafyeneyOved.iMeafyen2;
               }
               else
               {
                   fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
               }
               addRowToTable(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), fSumDakotRechiv);
           }
           catch (Exception ex)
           {
                clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
               throw (ex);
           }
      }

        private void CalcRechiv128()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.ZmanGrirot.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ZmanGrirot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv129()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifTnua.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv130()
        {
            float fSumDakotRechiv, fKizuzMachlifPakach, fKizuzMachlifSadran, fKizuzMachlifMeshaleach, fKizuzMachlifKupai, fKizuzMachlifRakaz;
             try{          
            fKizuzMachlifPakach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode().ToString()));
            fKizuzMachlifSadran = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode().ToString()));
            fKizuzMachlifMeshaleach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode().ToString()));
            fKizuzMachlifKupai = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode().ToString()));
            fKizuzMachlifRakaz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode().ToString()));

            fSumDakotRechiv = fKizuzMachlifPakach + fKizuzMachlifSadran + fKizuzMachlifMeshaleach + fKizuzMachlifKupai + fKizuzMachlifRakaz;
            
            addRowToTable(clGeneral.enRechivim.KizuzMachlifTnua.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                  clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifTnua.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                 throw (ex);
             }
        }

        private void CalcRechiv131()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ShaotShabat100.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv132()
        {
            float fSumDakotRechiv,fZmanHamaraShabat,fDakotZikuyChofesh;
            try
            {
                fZmanHamaraShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode().ToString()));
                fDakotZikuyChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString()));

                fSumDakotRechiv = fZmanHamaraShabat + fDakotZikuyChofesh;
                addRowToTable(clGeneral.enRechivim.ChofeshZchut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ChofeshZchut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv133()
        {
            float fSumDakotRechiv, fPremiaShabat, fPremiaYomit,fMichsaChodshit, fPremiaShishi, fDakotNochehut;
            try
            {
                fSumDakotRechiv = 0;
                if (_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30)
                {
                    fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString()));
                    fMichsaChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
                    if (fMichsaChodshit>0)
                        fSumDakotRechiv = Math.Min((fDakotNochehut * _oGeneralData.objParameters.fBasisLechishuvPremia) / Math.Min(_oGeneralData.objParameters.fMichsatSaotChodshitET * 60, fMichsaChodshit), _oGeneralData.objParameters.fMaxPremiatNehiga);
                }
                else
                {
                    fPremiaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString()));
                    fPremiaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString()));
                    fPremiaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString()));

                    fSumDakotRechiv = fPremiaShabat + fPremiaYomit + fPremiaShishi;
                }
                addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyaRegila.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv134()
        {
            float fSumDakotRechiv, fDakotPremiaVisaShabat, fDakotPremiaVisa, fDakotPremiaVisaShishi;
            try
            {
                fDakotPremiaVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString()));
                fDakotPremiaVisaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode().ToString()));
                fDakotPremiaVisaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode().ToString()));

                fSumDakotRechiv = fDakotPremiaVisa + fDakotPremiaVisaShabat + fDakotPremiaVisaShishi;
                addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyaNamlak.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv135()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv136()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv137()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv138()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv139()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv140()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv141()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv142()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNochehut.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.KizuzNochehut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzNochehut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv143(DateTime dTarMe,DateTime dTarAd)
        {
            float fMichsaYomit,fDakotNochehut,fDakotNochehutBefoal,fHadracha,fSumDakotRechiv, fTempX, fTempY, fTemp;
            int  iSugYom;
             DateTime dTaarich;
             Boolean bPutar = false;
            Boolean bLechishuv = false;
            fTempX = 0;
            try{
             if ((_oGeneralData.objMeafyeneyOved.iMeafyen14 <= 0) && _oGeneralData.objMeafyeneyOved.iMeafyen12>0)
             {
                 dTaarich = dTarMe;
                 bPutar = clCalcData.CheckOvedPutar(_iMisparIshi, dTaarich);
                 while (dTaarich <= dTarAd)
                 {
                    fDakotNochehut=clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)","KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    iSugYom = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, dTaarich, clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                    bLechishuv = false;
                    if ((iSugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode()) && _oGeneralData.objMeafyeneyOved.iMeafyen85 == 1 && fDakotNochehut == 0 && fMichsaYomit>0 && bPutar)
                    {
                        bLechishuv = true;
                    }
                     if (iSugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode() && _oGeneralData.objMeafyeneyOved.sMeafyen63.Length>0)
                    {
                        bLechishuv = true;
                    }
                    if (dTaarich.Day>=24)
                     {
                        //בדיקה אם קיימים אחד מן הרכיבים הללו באותו יום:
                      //  יום אבל (רכיב 56), מחלה (רכיב 60), מחלה בודד (רכיב 61), תאונה (רכיב 64)
                        // הריון בת זוג (רכיב 65), מחלת בן זוג (רכיב 69), מחלת הורים (רכיב 70)., מחלת ילד (רכיב 71), מילואים (רכיב 74).

                         if (_dsChishuv.Tables["CHISHUV_YOM"].Select("kod_rechiv in(56,60,61,64,65,69,70,71,74) and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                         {
                             bLechishuv = true;
                         }
                     }

                    if (bLechishuv == false)
                    { 
                        fHadracha=  clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)","KOD_RECHIV=" + clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        fDakotNochehutBefoal=  clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)","KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        if ((fMichsaYomit > 0 && fDakotNochehutBefoal >= 240) || (fHadracha == 1 && fDakotNochehut >= 240))
                        {
                          bLechishuv = true;
                        } 
                     }

                    if (bLechishuv)
                    { 
                        fTempX += 1; 
                    }

                     dTaarich = dTaarich.AddDays(1);
                 }  
 
             }

             if ((_oGeneralData.objMeafyeneyOved.iMeafyen14>0))
             {
                 fSumDakotRechiv = _oGeneralData.objMeafyeneyOved.iMeafyen14;
             }
                else
                {
                 fTempY = _dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString(),"").Length;
                 if (_oGeneralData.objMeafyeneyOved.iMeafyen12 == -1)
                 {
                     throw new Exception("ערך null במאפיין 12");
                 }
                 
                if (_oGeneralData.objMeafyeneyOved.iMeafyen84 == -1)
                   {
                       throw new Exception("ערך null במאפיין 84");
                   }
                fTemp =_oGeneralData.objMeafyeneyOved.iMeafyen12 + _oGeneralData.objMeafyeneyOved.iMeafyen84;
                 fTemp = _oGeneralData.objMeafyeneyOved.iMeafyen12; 

                fSumDakotRechiv = fTemp * fTempX / fTempY;
              
            }
           
                    
            addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv145()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv146()
        {
            float fSumDakotRechiv;//, fDakotNochehut, fMichsaChodshit;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot100.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
                //fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString()));
                //fMichsaChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                //if (fMichsaChodshit>0 && fDakotNochehut > fMichsaChodshit)
                //{
                //    fSumDakotRechiv = (fDakotNochehut - fMichsaChodshit) / 60;
                //    addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
                //}
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.Nosafot100.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv147()
        {
            float fTempX, fSumDakotRechiv, fNosafotTafkid, fDakotTafkidChol, fOutMichsaShishi, fShaotNosafotTafkidChol;
            try
            {
                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode().ToString()))/60;

                fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString())); 
                fOutMichsaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode().ToString())); 
                fNosafotTafkid = fTempX - (float.Parse(((fDakotTafkidChol + fOutMichsaShishi) / 60).ToString()));
                fShaotNosafotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode().ToString())); 

                if (fNosafotTafkid > fShaotNosafotTafkidChol)
                    fSumDakotRechiv = fNosafotTafkid - fShaotNosafotTafkidChol;
                else fSumDakotRechiv = 0;

                addRowToTable(clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
      
        private void CalcRechiv149()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode().ToString()));

                addRowToTable(clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv160()
        {
            float fTempX, fSumDakotRechiv, fNosafotNihulTnua, fDakotNohulTnua, fOutMichsaShishi, fShaotNihulTnua;
            try
            {
                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode().ToString())) / 60;

                fDakotNohulTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString())); 
                fOutMichsaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode().ToString()));
                fNosafotNihulTnua = fTempX - ((fDakotNohulTnua + fOutMichsaShishi) / 60);

                fShaotNihulTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode().ToString())); 

                if (fNosafotNihulTnua > fShaotNihulTnua)
                    fSumDakotRechiv = fNosafotNihulTnua - fShaotNihulTnua;
                else fSumDakotRechiv = 0;

                addRowToTable(clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        
        //private void CalcRechiv164()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode().ToString()));
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv165()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode().ToString()));
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv166()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode().ToString()));
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv167()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode().ToString()));
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv169()
        //{
        //    float fSumDakotRechiv,fMichsatNosafotPakach,fSachDakotLeloMichutzLemichsa,fNosafotPakach,fNosafotMachlif,fNosafotMichutz;
        //    try{
        //    fMichsatNosafotPakach = _oGeneralData.objParameters.iMaxDakotNosafotPakach;

        //    fNosafotPakach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotPakach.GetHashCode().ToString()));
        //    fNosafotMachlif = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifPakach.GetHashCode().ToString()));
        //    fNosafotMichutz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode().ToString()));
        //    fSachDakotLeloMichutzLemichsa = (fNosafotPakach + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotPakach)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fMichsatNosafotPakach;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotPakach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv170()
        //{
        //    float fSumDakotRechiv, fMichsatNosafotSadran, fSachDakotLeloMichutzLemichsa, fNosafotSadran, fNosafotMachlif, fNosafotMichutz;
        //    try{
        //    fMichsatNosafotSadran = _oGeneralData.objParameters.iMaxDakotNosafotSadran;

        //    fNosafotSadran = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotSadran.GetHashCode().ToString()));
        //    fNosafotMachlif = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifSadran.GetHashCode().ToString()));
        //    fNosafotMichutz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode().ToString()));
        //    fSachDakotLeloMichutzLemichsa = (fNosafotSadran + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotSadran)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fMichsatNosafotSadran;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotSadran.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotSadran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv171()
        //{
        //    float fSumDakotRechiv, fMichsatNosafotRakaz, fSachDakotLeloMichutzLemichsa, fNosafotRakaz, fNosafotMachlif, fNosafotMichutz;
        //    try{
        //    fMichsatNosafotRakaz = _oGeneralData.objParameters.iMaxDakotNosafotRakaz;

        //    fNosafotRakaz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotRakaz.GetHashCode().ToString()));
        //    fNosafotMachlif = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifRakaz.GetHashCode().ToString()));
        //    fNosafotMichutz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode().ToString()));
        //    fSachDakotLeloMichutzLemichsa = (fNosafotRakaz + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotRakaz)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fMichsatNosafotRakaz;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotRakaz.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv172()
        //{
        //    float fSumDakotRechiv, fMichsatNosafotMeshaleach, fSachDakotLeloMichutzLemichsa, fNosafotMeshaleach, fNosafotMachlif, fNosafotMichutz;
        //    try{
        //    fMichsatNosafotMeshaleach = _oGeneralData.objParameters.iMaxDakotNosafotRakaz;

        //    fNosafotMeshaleach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMeshaleach.GetHashCode().ToString()));
        //    fNosafotMachlif = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifMeshaleach.GetHashCode().ToString()));
        //    fNosafotMichutz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode().ToString()));
        //    fSachDakotLeloMichutzLemichsa = (fNosafotMeshaleach + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotMeshaleach)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fNosafotMeshaleach;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotMeshaleach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotMeshaleach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        private void CalcRechiv174()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv175()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv176()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
         }

        private void CalcRechiv177()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
         }

        private void CalcRechiv178()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv179()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotPakach.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachDakotPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
      }

        private void CalcRechiv180()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotSadran.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachDakotSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotSadran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv181()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotRakaz.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv182()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotMeshalech.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
            }

        private void CalcRechiv183()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotKupai.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.SachDakotKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotKupai.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

      
        private void CalcRechiv184()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv185()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv186()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv187()
        {
            float fSumDakotRechiv;
            try{
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv188()
        {
            float fSumDakotRechiv, fDakotNehigaChol, fDakotShabatBenahagut, fDakotNehigaShishi;
            try{
            fDakotNehigaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString()));
            fDakotShabatBenahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString()));
            fDakotNehigaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString()));

            fSumDakotRechiv = fDakotNehigaChol + fDakotShabatBenahagut + fDakotNehigaShishi;
            addRowToTable(clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotNahagut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv189()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv190()
        {
            float fSumDakotRechiv, fDakotNihulChol, fDakotNihulShabat, fDakotNihulShishi;
            try{
            fDakotNihulChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString()));
            fDakotNihulShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString()));
            fDakotNihulShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString()));

            fSumDakotRechiv = fDakotNihulChol + fDakotNihulShabat + fDakotNihulShishi;
            addRowToTable(clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv191()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv192()
        {
            float fSumDakotRechiv, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
            try{
            fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString()));
            fDakotTafkidShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString()));
            fDakotTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString()));

            fSumDakotRechiv = fDakotTafkidChol + fDakotTafkidShabat + fDakotTafkidShishi;
            addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotTafkid.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv193()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv194()
        {
            float fSumDakotRechiv, fNehigaChol, fNihulTnua, fTafkidChol;
            try
            {
                fNehigaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString()));
                fNihulTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString()));
                 fTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString()));

                fSumDakotRechiv = fNehigaChol + fNihulTnua + fTafkidChol;
                addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv195()
        {
            float fSumDakotRechiv, fNehigaShishi, fNihulShishi, fTafkidShishi;
            try
            {
                fNehigaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString()));
                fNihulShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString()));
                fTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString()));

                fSumDakotRechiv = fNehigaShishi + fNihulShishi + fTafkidShishi;
                addRowToTable(clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutBeshishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv196()
        {
            float fSumDakotRechiv, fNehigaShabat, fNihulShabat, fTafkidShabat;
            try
            {
                fNehigaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString()));
                fNihulShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString()));
                fTafkidShabat= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString()));
                 
                fSumDakotRechiv = fNehigaShabat + fNihulShabat + fTafkidShabat;
                addRowToTable(clGeneral.enRechivim.NochehutShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutShabat.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv198()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NosafotShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NosafotShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NosafotShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv200()
        {
            float fSumDakotRechiv,fDakotNihul,fDakotTafkid;
            try
            {
                fDakotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString()));
                fDakotNihul = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString()));

                fSumDakotRechiv = fDakotNihul + fDakotTafkid;
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv201()
        {
            float fSumDakotRechiv, fDakotNihul, fDakotTafkid;
            try
            {
                fDakotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode().ToString()));
                fDakotNihul = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode().ToString()));

                fSumDakotRechiv = fDakotNihul + fDakotTafkid;
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv202()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString()));

                addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv203()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv204()
        {
            float fSumDakotRechiv, fNochehutPremia;
            int iMutaam;
            try
            {
                if (_oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.Rasham.GetHashCode() || _oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.SganMenahel.GetHashCode() || _oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMachlaka.GetHashCode())
                {
                    if (_oGeneralData.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumArtzi.GetHashCode() || _oGeneralData.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumBameshek.GetHashCode())
                    {
                        iMutaam = _oGeneralData.objPirteyOved.iMutamut;
                        if (iMutaam != clGeneral.enMutaam.enMutaam1.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam3.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam5.GetHashCode()&& iMutaam != clGeneral.enMutaam.enMutaam7.GetHashCode())
                        {
                            if (_oGeneralData.objMeafyeneyOved.iMeafyen60<=0)
                            {
                                fNochehutPremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode().ToString()));
                                fSumDakotRechiv = (_oGeneralData.objParameters.fAchuzPremiaRishum *fNochehutPremia )/100;
                                addRowToTable(clGeneral.enRechivim.PremiaLariushum.GetHashCode(), fSumDakotRechiv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiaLariushum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv206()
        {
            float fSumDakotRechiv, fNochehutLepremia;
            try
            {
               fNochehutLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode().ToString()));

                fSumDakotRechiv = (_oGeneralData.objParameters.fAchuzPremiatMachsanKartisim * fNochehutLepremia) / 100;
                addRowToTable(clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(), fSumDakotRechiv);
            
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv207()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv208()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv209()
        {
            float fSumDakotRechiv;
             try
            {
             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode().ToString()));
            addRowToTable(clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv210()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv211()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv212()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv276()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv277()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv213()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachNesiot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachNesiot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv214()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotHistaglut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv215()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKM.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachKM.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv216()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv217()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotHagdara.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv218()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv219()
        {
            float fSumDakotRechiv, fDakotChofesh;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fDakotChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString()));
                fSumDakotRechiv = fDakotChofesh / 60;          
                addRowToTable(clGeneral.enRechivim.ShaotChofesh.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.ShaotChofesh.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv220()
        {
            float fSumDakotRechiv, fNochehutChodshit, fMichsaChodshit;
            try
            {
                fNochehutChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString()));
                fMichsaChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
                fSumDakotRechiv = 0;

                if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit<fMichsaChodshit)
                {
                   // fYemeyAvoda = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyAvoda.GetHashCode().ToString()));

                    fSumDakotRechiv =( fMichsaChodshit - fNochehutChodshit);//(fMichsaChodshit/fYemeyAvoda);
                 }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHeadrut.GetHashCode().ToString()));
             
                }
                addRowToTable(clGeneral.enRechivim.DakotHeadrut.GetHashCode(), fSumDakotRechiv);
              
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotHeadrut.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv221()
        {
            float fSumDakotRechiv, fNochehutChodshit, fMichsaChodshit;//, fYemeyAvoda;
            try
            {
                fNochehutChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString()));
                fMichsaChodshit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
                fSumDakotRechiv = 0;

                if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit < fMichsaChodshit)
                {
                    //fYemeyAvoda = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyAvoda.GetHashCode().ToString()));

                    fSumDakotRechiv = (fMichsaChodshit - fNochehutChodshit);// / (fMichsaChodshit / fYemeyAvoda);
                }
                else if (_oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oGeneralData.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString()));

                }
                addRowToTable(clGeneral.enRechivim.DakotChofesh.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DakotChofesh.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv223()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode().ToString()));

                addRowToTable(clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv224()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.MevakrimBadrachim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv225()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.MifalYetzur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv226()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.NehageyTovala.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv227()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.NehageyTenderim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv228()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.Dfus.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatDfus.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatDfus.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv229()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.MisgarotAchzaka.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMisgarot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv230()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.Gifur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatGifur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatGifur.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv231()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.MusachRishonLetzyon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv232()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.TechnayYetzur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv233()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv234()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv235()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv236()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv237()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv238()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv239()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv240()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv241()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv242()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.ReshetBitachon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv243()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = clCalcData.GetPremiaYadanit(clGeneral.enSugPremia.PerukVeshiputz.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv244()
        {
            float fSumDakotRechiv,fYemeyAvoda;
            try
            {
                fSumDakotRechiv = 0;
                if (_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && _oGeneralData.objMeafyeneyOved.iMeafyen53>0)
                {
                    if (_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Substring(0,1)=="1")
                    {
                      //  fYemeyAvoda = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyAvoda.GetHashCode().ToString()));
                       // fSumDakotRechiv = (fYemeyAvoda * int.Parse(_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Substring(_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Length-4, 3))) / 100;
                        fSumDakotRechiv = float.Parse(_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Substring(_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Length - 3, 3))/10;
                    }
                    else if (_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Substring(0, 1) == "2")
                    {
                        fSumDakotRechiv = float.Parse(_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Substring(_oGeneralData.objMeafyeneyOved.iMeafyen53.ToString().Length - 3, 3));
                    }
                    addRowToTable(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), fSumDakotRechiv);
                    
                }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv245()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv246()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv247()
        {
            float fSumDakotRechiv, fNochehutPremia;
            try
            {
                if (_oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMoreNehiga.GetHashCode())
                {
                  
                    if (_oGeneralData.objMeafyeneyOved.iMeafyen60 <= 0)
                    {
                        fNochehutPremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode().ToString()));
                        fSumDakotRechiv = (_oGeneralData.objParameters.fAchuzPremiaMenahel * fNochehutPremia) / 100;
                        addRowToTable(clGeneral.enRechivim.PremyaMenahel.GetHashCode(), fSumDakotRechiv);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                 clLogBakashot.SetError(_lBakashaId,_iMisparIshi, "E", clGeneral.enRechivim.PremyaMenahel.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv248()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv249()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv250()
        {
            float fSumDakotRechiv, fTempX,fTempY, fShaotTafkidLeloMichutz;
            float fDakotMichutzChol, fMichsatTafkidChol, fSumDakotRechiv207;
            try
            {
                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode().ToString())) / 60;
                
                fDakotMichutzChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString()));
                fSumDakotRechiv207 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode().ToString()));
                fShaotTafkidLeloMichutz = fTempX - float.Parse(((fDakotMichutzChol + fSumDakotRechiv207) / 60).ToString());

                fMichsatTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode().ToString()));
                fTempY = fShaotTafkidLeloMichutz > fMichsatTafkidChol ? fMichsatTafkidChol : fShaotTafkidLeloMichutz;

                fSumDakotRechiv = ((fTempY*60) + fDakotMichutzChol + fSumDakotRechiv207)/60;

                addRowToTable(clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv251()
        {
            float fSumDakotRechiv, fTempX, fNosafotMichutz;
            float fMichsatNosafot, fSumDakotRechiv208, fTempY;
            try
            {
                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode().ToString()))/60;
                
                fNosafotMichutz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString()));
                fSumDakotRechiv208 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode().ToString()));
                fNosafotMichutz = fTempX - ((fNosafotMichutz + fSumDakotRechiv208) / 60);

                fMichsatNosafot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode().ToString()));
                fTempY = fNosafotMichutz > fMichsatNosafot ? fMichsatNosafot : fNosafotMichutz;

                fSumDakotRechiv = ((fTempY * 60) + fNosafotMichutz + fSumDakotRechiv208) / 60;

                addRowToTable(clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv252()
        {
            float fSumDakotRechiv, fTempX,  iMichsatDakotNosafot;
            try
            {
                fSumDakotRechiv = 0;

                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode().ToString()));

                if (_oGeneralData.objMeafyeneyOved.iMeafyen11 == -1)
                {
                    throw new Exception("null מאפיין 11 התקבל עם ערך");
                }

                iMichsatDakotNosafot = _oGeneralData.objMeafyeneyOved.iMeafyen11 ;
                fSumDakotRechiv = Math.Min(fTempX, (iMichsatDakotNosafot* 60));
                addRowToTable(clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), fSumDakotRechiv);
            


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv253()
        {
            float fSumDakotRechiv, fTempZ;
            try
            {
                fTempZ = 0;
                if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                {
                    fTempZ = 3;
                }
                else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                {
                    fTempZ = 6;
                }

                if (_oGeneralData.objMeafyeneyOved.iMeafyen13 == -1)
                {
                    throw new Exception("null מאפיין 13 התקבל עם ערך");
                }
                else
                {
                    fSumDakotRechiv = _oGeneralData.objMeafyeneyOved.iMeafyen13 + fTempZ;
                }

                addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv254()
        {
            float fSumDakotRechiv, iSachShabatonim;
            try
            {
                iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);

                fSumDakotRechiv = (_oGeneralData.objMeafyeneyOved.iMeafyen16 > -1 ? _oGeneralData.objMeafyeneyOved.iMeafyen16 : 0);
                if (!_oGeneralData.objMeafyeneyOved.Meafyen16Exists)
                {
                    fSumDakotRechiv = iSachShabatonim * 6;
                }

               addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv255()
        {
            float fSumDakotRechiv, iSachShabatonim;
            try
            {
                iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);

                fSumDakotRechiv = (_oGeneralData.objMeafyeneyOved.iMeafyen17 > -1 ? _oGeneralData.objMeafyeneyOved.iMeafyen17: 0);
                if (!_oGeneralData.objMeafyeneyOved.Meafyen17Exists)
                {
                    fSumDakotRechiv = iSachShabatonim * 6;
                }

                addRowToTable(clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv256()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv257()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv258()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv259()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv260()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv261()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MachalaYomMale.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.MachalaYomMale.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MachalaYomMale.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv262()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MachalaYomChelki.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv263()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv264()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv265()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBetafkid.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv266()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluimChelki.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv267()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotElementim.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotElementim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv268()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv269()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv270()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv271()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv931()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv932()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString()));
                addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void IpusRechivim()
        { 
            DataRow[] drRowToRemove;
            string sRechivim;

            sRechivim = sRechivim = clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + "," + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() +
                                      "," + clGeneral.enRechivim.NosafotShabat.GetHashCode().ToString() + "," + clGeneral.enRechivim.SachNosafotChol.GetHashCode().ToString() +
                                      "," + clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode().ToString() + "," + clGeneral.enRechivim.DakotNosafotNihul.GetHashCode().ToString() +
                                       "," + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString() + "," + clGeneral.enRechivim.DakotShabat.GetHashCode().ToString() +
                                       "," + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + "," + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() +
            "," + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + "," + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() +
               "," + clGeneral.enRechivim.Shaot150Letashlum.GetHashCode().ToString() + "," + clGeneral.enRechivim.Shaot200Letashlum.GetHashCode().ToString() +
              "," + clGeneral.enRechivim.SachShaotNosafot.GetHashCode().ToString() + 
               "," + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + "," + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString();
                              
            drRowToRemove = _dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV IN(" + sRechivim + ")");
            for (int i = 0; i < drRowToRemove.Length - 1; i++)
            {
                drRowToRemove[0].Delete();
            }
            drRowToRemove = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV IN(" + sRechivim + ")");
            for (int i = 0; i < drRowToRemove.Length - 1; i++)
            {
                drRowToRemove[0].Delete();
            }
            drRowToRemove = _dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV IN(" + sRechivim + ")");
            for (int i = 0; i < drRowToRemove.Length - 1; i++)
            {
                drRowToRemove[0].Delete();
            }


        }
        private void addRowToTable(int iKodRechiv, float fErechRechiv)
        {
            DataRow drChishuv;

            if (_dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + iKodRechiv.ToString()).Length == 0)
            {
                if (fErechRechiv > 0)
                {
                    drChishuv = _dtChishuvChodesh.NewRow();
                    drChishuv["BAKASHA_ID"] = _lBakashaId;
                    drChishuv["MISPAR_ISHI"] = _iMisparIshi;
                    drChishuv["TAARICH"] = _dTaarichChishuv;
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    _dtChishuvChodesh.Rows.Add(drChishuv);

                }
            }
            else
            {
                UpdateRowInTable(iKodRechiv, fErechRechiv);
            }

        }

        private void UpdateRowInTable(int iKodRechiv, float fErechRechiv)
        {
            DataRow drChishuv;
            drChishuv = _dtChishuvChodesh.Select("KOD_RECHIV=" + iKodRechiv)[0];
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
           
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch
{
    class CalcMonth
    {
        private DataTable _dtChishuvChodesh;
        private CalcDay oDay;
        private DateTime _dTaarichChishuv;
        private Oved objOved;
        private clCalcBL oCalcBL;


        public CalcMonth(Oved oOved)
        {
            try
            {
                objOved = oOved;
                _dtChishuvChodesh =objOved._dsChishuv.Tables["CHISHUV_CHODESH"]; //objOved._DtMonth;
                oCalcBL = new clCalcBL();
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", 0, null, "CalcMonth: " + ex.Message);

                throw ex;
            }
        }

        public void CalcMonthOved() //int Mispar_ishi, DateTime dTarMe, DateTime dTarAd)
        {
            DateTime dTaarich, dTarMe, dTarAd;
         
            try
            {
                //iStatusTipul = clGeneral.enStatusTipul.HistayemTipul.GetHashCode();
                if (objOved.DtYemeyAvoda.Rows.Count > 0)
                {
                    if (objOved.bChishuvYom)
                    {
                        dTarMe = objOved._dDay;
                        dTarAd = objOved._dDay;
                    }
                    else
                    {
                        dTarMe = objOved.Month;
                        dTarAd = objOved.Month.AddMonths(1).AddDays(-1);
                    }

                    _dTaarichChishuv = objOved.Month;

                    if (!objOved.bChishuvYom)
                    {
                        SimunSidurimLoLetashlum();
                        ////סימון "לא לתשלום" עבור סידורי רציפות
                        //SimunLoLetashlumRetzifut();
                    }

                    
                    oDay = new CalcDay(objOved);
                    dTaarich = dTarMe;
                    while (dTaarich <= dTarAd)
                    {
                        if (IsDayExist(dTaarich))
                        {
                            objOved.Taarich = dTaarich;
                            objOved.objParameters = objOved.oGeneralData.ListParameters.Find(Params => (Params._Taarich == dTaarich));
                            objOved.objPirteyOved = objOved.PirteyOved.Find(Pratim => (Pratim._TaarichMe <= dTaarich && Pratim._TaarichAd >= dTaarich));
                            objOved.objMeafyeneyOved = objOved.MeafyeneyOved.Find(Meafyenim => (Meafyenim._Taarich == dTaarich));
                            SetNetunimLeYom();

                            oDay.CalcRechiv126(dTaarich);

                            objOved.DtYemeyAvodaYomi = null;
                            objOved.DtPeiluyotYomi = null;
                            objOved.DtPeiluyotTnuaYomi = null;
                        }
                        dTaarich = dTaarich.AddDays(1);
                    }
                    
                    CalcMekademNipuach(dTarMe, dTarAd, objOved.Mispar_ishi);

                    dTaarich = dTarMe;
                    while (dTaarich <= dTarAd)
                    {
                        if (IsDayExist(dTaarich))
                        {
                            objOved.Taarich = dTaarich;
                            objOved.SugYom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dTaarich, objOved.oGeneralData.dtSugeyYamimMeyuchadim);
                            objOved.objParameters = objOved.oGeneralData.ListParameters.Find(Params => (Params._Taarich == dTaarich));
                            objOved.objPirteyOved = objOved.PirteyOved.Find(Pratim => (Pratim._TaarichMe <= dTaarich && Pratim._TaarichAd >= dTaarich));
                            objOved.objMeafyeneyOved = objOved.MeafyeneyOved.Find(Meafyenim => (Meafyenim._Taarich == dTaarich));
                            objOved.sSugYechida = oCalcBL.InitSugYechida(objOved, dTaarich);
                            // oDay.SugYom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dTaarich, objOved.oGeneralData.dtSugeyYamimMeyuchadim);
                            SetNetunimLeYom();
                            objOved.fTotalAruchatZaharimForDay = 0;
                            oDay.CalcRechivim();

                            objOved.DtYemeyAvodaYomi = null;
                            objOved.DtPeiluyotYomi = null;
                            objOved.DtPeiluyotTnuaYomi = null;
                        }
                        dTaarich = dTaarich.AddDays(1);
                    }


                    if (!objOved.bChishuvYom )
                    {
                        CalcRechivimInMonth(dTarMe, dTarAd);
                        ChangingChofeshFromShaotNosafot();
                    }
                
                    oDay.SetNullObjects();
                    oDay = null;
                }
                oCalcBL = null;
              //  return objOved._dsChishuv;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", 0, null, "CalcMonth: " + ex.Message);
                throw ex;
            }
        }

        private bool IsDayExist(DateTime dDay)
        {
            DataRow[] dr;
            try
            {
                dr = objOved.DtYemeyAvoda.Select(" taarich=Convert('" + dDay.ToShortDateString() + "', 'System.DateTime')");
                if (dr.Length > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetNetunimLeYom()
        {
            DataRow[] drs;
            try
            {
                drs = objOved.DtYemeyAvoda.Select("taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                if (drs.Length > 0)
                    objOved.DtYemeyAvodaYomi = drs.CopyToDataTable();
                else objOved.DtYemeyAvodaYomi = objOved.DtYemeyAvoda.Clone();
                drs = null;

                drs = objOved.DtPeiluyotOved.Select("taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                if (drs.Length > 0)
                    objOved.DtPeiluyotYomi = drs.CopyToDataTable();
                else objOved.DtPeiluyotYomi = objOved.DtPeiluyotOved.Clone();
                drs = null;

                if (objOved.DtPeiluyotFromTnua != null)
                {
                    drs = objOved.DtPeiluyotFromTnua.Select("activity_date=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (drs.Length > 0)
                        objOved.DtPeiluyotTnuaYomi = drs.CopyToDataTable();
                    else objOved.DtPeiluyotTnuaYomi = objOved.DtPeiluyotFromTnua.Clone();
                    drs = null;
                }
                AddRowZmanLeloHafsaka();
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        private void AddRowZmanLeloHafsaka()
        {
            float zmanHafsaka, zmanSidur;
            CalcPeilut oPeilut;
            DateTime taarich = DateTime.Now;
            DataRow dr;
            try
            {
                oPeilut = new CalcPeilut(objOved);

                objOved.DtYemeyAvodaYomi.Columns.Add("ZMAN_LELO_HAFSAKA", System.Type.GetType("System.Single"));
                objOved.DtYemeyAvodaYomi.Columns.Add("ZMAN_HAFSAKA_BESIDUR", System.Type.GetType("System.Single"));
                for (int i = 0; i < objOved.DtYemeyAvodaYomi.Rows.Count; i++)
                {
                    try
                    {
                        dr = objOved.DtYemeyAvodaYomi.Rows[i];
                        if (dr["shat_hatchala_sidur"].ToString() != "")
                        {

                            taarich = DateTime.Parse(dr["taarich"].ToString());
                            zmanHafsaka = oPeilut.getZmanHafsakaBesidur(int.Parse(dr["mispar_sidur"].ToString()), DateTime.Parse(dr["shat_hatchala_sidur"].ToString()));
                            zmanSidur = float.Parse((DateTime.Parse(dr["shat_gmar_letashlum"].ToString()) - DateTime.Parse(dr["shat_hatchala_letashlum"].ToString())).TotalMinutes.ToString());
                            objOved.DtYemeyAvodaYomi.Rows[i]["ZMAN_LELO_HAFSAKA"] = zmanSidur - zmanHafsaka;
                            objOved.DtYemeyAvodaYomi.Rows[i]["ZMAN_HAFSAKA_BESIDUR"] = zmanHafsaka;
                        }
                        //else clCalcData.DtYemeyAvoda.Rows[i]["ZMAN_LELO_HAFSAKA"] = 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcMekademNipuach(DateTime dTarMe, DateTime dTarAd, int Mispar_ishi)
        {
            float fCountMichsa, fCountYomLeloChag;
            int iSugYom;
            objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
            fCountMichsa = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " AND ERECH_RECHIV>0", "").Length;
            fCountYomLeloChag = 0;
            objOved.fMekademNipuach = 0;

            do
            {
                iSugYom = oCalcBL.GetSugYomLemichsa(objOved, dTarMe, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56);
                if (iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    fCountYomLeloChag += 1;
                }
                dTarMe = dTarMe.AddDays(1);
            }
            while (dTarMe <= dTarAd);

            objOved.fMekademNipuach = fCountYomLeloChag / fCountMichsa;

        }

        private void ChangingChofeshFromShaotNosafot()
        {
            //טיפול בחופש ע"ח שעות נוספות 
            DataRow[] drSidurimToChange, drMichsaYomit, drShaot100, drChofesh, drDakotNochehut;
            DateTime dTaarich, dShatHatchalaSidur;
            float fMichsaYomit, fShaot100Letashlum;
            int I, iSachSidurimKuzezu, iOutMichsa, iMisparSidur;
            try
            {
                iSachSidurimKuzezu = 0;
                objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                drShaot100 = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString());
                if (drShaot100.Length > 0)
                {

                    drSidurimToChange = objOved.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=99822", "taarich asc");
                    for (I = 0; I < drSidurimToChange.Length; I++)
                    {
                        dTaarich = (DateTime)(drSidurimToChange[I]["taarich"]);
                        fMichsaYomit = 0;
                        fShaot100Letashlum = 0;
                        objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                        drMichsaYomit = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                        if (drMichsaYomit.Length > 0)
                        {
                            fMichsaYomit = (float)(drMichsaYomit[0]["ERECH_RECHIV"]);
                            fShaot100Letashlum = (float)(drShaot100[0]["ERECH_RECHIV"]) / 60;
                            iOutMichsa = int.Parse(drSidurimToChange[I]["out_michsa"].ToString());
                            iMisparSidur = int.Parse(drSidurimToChange[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = (DateTime)(drSidurimToChange[I]["shat_hatchala_sidur"]);

                            if (fMichsaYomit <= fShaot100Letashlum && (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa) || iSachSidurimKuzezu < objOved.objParameters.iMaxYamimHamaratShaotNosafot))
                            {
                                fShaot100Letashlum = fShaot100Letashlum - (fMichsaYomit / 60);
                                drShaot100[0]["ERECH_RECHIV"] = fShaot100Letashlum;

                                // -	לעדכן את רכיב 67 כדלקמן: 
                                //•	ברמת יום עבודה – לבטל את הרשומה של הרכיב ביום העבודה אליו שייך הסידור.
                                objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                                drChofesh = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                if (drChofesh.Length > 0)
                                { drChofesh[0].Delete(); }
                                //•	ברמת החודש – ערך הרכיב פחות 1.
                                objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                                drChofesh = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                                if (drChofesh.Length > 0)
                                { drChofesh[0]["ERECH_RECHIV"] = (float)(drChofesh[0]["ERECH_RECHIV"]) - 1; }

                                //-	לעדכן את רכיב 221 כדלקמן: 
                                objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                                drChofesh = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                //•	ברמת יום עבודה – לבטל את הרשומה של הרכיב ביום העבודה אליו שייך הסידור.
                                if (drChofesh.Length > 0)
                                { drChofesh[0].Delete(); }
                                //•	ברמת החודש – ערך הרכיב פחות מכסה יומית מחושבת (רכיב 126) ברמת יום העבודה אליו שייך הסידור
                                objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                                drChofesh = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString());
                                if (drChofesh.Length > 0)
                                { drChofesh[0]["ERECH_RECHIV"] = (float)(drChofesh[0]["ERECH_RECHIV"]) - fMichsaYomit; }


                                //-	לעדכן רכיב 219:
                                //•	ברמת יום עבודה –  לבטל את הרשומה של הרכיב ביום העבודה אליו שייך הסידור
                                objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                                drChofesh = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.ShaotChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                if (drChofesh.Length > 0)
                                {
                                    drChofesh[0].Delete();
                                }
                                //	ברמת החודש – ערך רכיב 221 מעודכן חלקי 60.
                                objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                                drChofesh = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.ShaotChofesh.GetHashCode().ToString());
                                if (drChofesh.Length > 0)
                                { drChofesh[0]["ERECH_RECHIV"] = (float)(drChofesh[0]["ERECH_RECHIV"]) - (oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotChofesh.GetHashCode()) / 60); }

                                //-	לעדכן רכיב 1:
                                //•	ברמת יום עבודה – ערך הרכיב = ערך הרכיב הקודם + מכסה יומית מחושבת (רכיב 126) של יום העבודה
                                objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                                drDakotNochehut = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                if (drDakotNochehut.Length > 0)
                                {
                                    drDakotNochehut[0]["ERECH_RECHIV"] = (float)(drDakotNochehut[0]["ERECH_RECHIV"]) + fMichsaYomit;
                                }
                                //•	ברמת החודש - = ערך הרכיב הקודם + מכסה יומית מחושבת (רכיב 126) של יום העבודה
                                objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                                drDakotNochehut = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString());
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", 0, null, "ChangingChofeshFromShaotNosafot: " + ex.Message);
                throw ex;
            }
            finally
            {
                drSidurimToChange=null;
                drMichsaYomit=null;
                drShaot100=null;
                drChofesh=null;
                drDakotNochehut = null;
            }
        }

        private int GetSachShabatonimInMonth(DateTime dMonth)
        {
            int iSachShabatonim, iSugYom;
            DateTime dTarMe, dTarAd;
            iSachShabatonim = 0;
            dTarMe = new DateTime(dMonth.Year, dMonth.Month, 1);
            dTarAd = dTarMe.AddMonths(1).AddDays(-1);
            do
            {
                iSugYom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dTarMe,objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.iMeafyen56);
                if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom, dTarMe))
                {
                    iSachShabatonim = iSachShabatonim + 1;
                }
                dTarMe = dTarMe.AddDays(1);
            }
            while (dTarMe <= dTarAd);
            return iSachShabatonim;
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
        //        oSidur = new clCalcSidur(objOved.Mispar_ishi, objOved.iBakashaId,_oGeneralData);
        //        oPeilut = new clCalcPeilut(objOved.Mispar_ishi, objOved.iBakashaId, _oGeneralData);

        //        drSidurim = objOved.DtYemeyAvoda.Select("mispar_sidur in(99500, 99501) and Lo_letashlum =0", "TAARICH ASC,MISPAR_SIDUR ASC");
        //        for (I = 0; I < drSidurim.Length; I++)
        //        {
        //            if (int.Parse(drSidurim[I]["Lo_letashlum"].ToString()) == 0)
        //            {
        //                iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
        //                dTaarich = (DateTime)drSidurim[I]["taarich"];
        //                objOved.objMeafyeneyOved = new clMeafyenyOved(objOved.Mispar_ishi, dTaarich);

        //                iSugYom = clDefinitions.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dTaarich,objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.objMeafyeneyOved.iMeafyen56);
        //                xxx.iSugYom = iSugYom;
        //                objOved.objParameters = new clParameters(dTaarich, iSugYom);
        //                if (fSumDakotNichehutChodshi >= objOved.objParameters.iMaxRetzifutChodshitLetashlum)
        //                {
        //                    drSidurim[I]["Lo_letashlum"] = 1;
        //                }

        //                if (!(dTaarich == dTaarichNext))
        //                {

        //                    drSidur = objOved.DtYemeyAvoda.Select("mispar_sidur in(99500, 99501) and Lo_letashlum =0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "TAARICH ASC");
        //                    for (J = 0; J < drSidur.Length; J++)
        //                    {
        //                        if (fSumDakotNichehut >= objOved.objParameters.iMaxRetzifutYomiLetashlum)
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
            int I, J, iMisparSidur, iMisparSidurNext, iOutMichsa;
            float fSumDakotNichehut;
            DateTime dTaarich, dShatHatchala;
            fSumDakotNichehut = 0;
            iMisparSidurNext = 0;
            CalcSidur oSidur;
            CalcPeilut oPeilut;
            try
            {
                oSidur = new CalcSidur(objOved);
                oPeilut = new CalcPeilut(objOved);

                //יש לסכום לתוך X את דקות הנוכחות לתשלום (רכיב 1) של הסידורים המיוחדים לתשלום (TB_Sidurim_Ovedim.Lo_letashlum = 0) ושאינם מחוץ למכסה (( 0=TB_Sidurim_Ovedim.Out_michsa עד אשר X >= מכסה חודשית לסידור כל סידור מעבר לכך מסוג זה שאינו מחוץ למכסה (0=TB_Sidurim_Ovedim.Out_michsa) יסומן לא לתשלום TB_Sidurim_Ovedim.Lo_letashlum = 1. 

                drSidurim = objOved.DtYemeyAvoda.Select("SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and not Michsat_shaot_chodshit is null and Lo_letashlum =0", "MISPAR_SIDUR ASC");
                for (I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchala = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());
                    if (!oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, _dTaarichChishuv, iMisparSidur, dShatHatchala, iOutMichsa))
                    {
                        if (I > 0 && ((I + 1) < drSidurim.Length))
                        {
                            iMisparSidurNext = int.Parse(drSidurim[I + 1]["mispar_sidur"].ToString());
                        }
                        else if ((I + 1) == drSidurim.Length) { iMisparSidurNext = 0; }

                        if (!(iMisparSidur == iMisparSidurNext))
                        {
                            //יש לסכום לתוך X את דקות הנוכחות לתשלום (רכיב 1) של הסידורים המיוחדים לתשלום (TB_Sidurim_Ovedim.Lo_letashlum = 0) ושאינם מחוץ למכסה (( 0=TB_Sidurim_Ovedim.Out_michsa עד אשר X >= מכסה חודשית לסידור כל סידור מעבר לכך מסוג זה שאינו מחוץ למכסה (0=TB_Sidurim_Ovedim.Out_michsa) יסומן לא לתשלום TB_Sidurim_Ovedim.Lo_letashlum = 1. 

                            drSidur = objOved.DtYemeyAvoda.Select("mispar_sidur=" + iMisparSidur + " and not Michsat_shaot_chodshit is null and Lo_letashlum =0", "TAARICH ASC");
                            for (J = 0; J < drSidur.Length; J++)
                            {
                                dTaarich = DateTime.Parse(drSidur[J]["taarich"].ToString());
                                dShatHatchala = DateTime.Parse(drSidur[J]["shat_hatchala_sidur"].ToString());
                                objOved.Taarich = dTaarich;
                                iOutMichsa = int.Parse(drSidur[J]["out_michsa"].ToString());

                                if (!oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, dTaarich, iMisparSidur, dShatHatchala, iOutMichsa))
                                {
                                    if (!HaveSidurimNosafim(iMisparSidur, dTaarich))
                                    {
                                        drSidur[J]["Lo_letashlum"] = -1;
                                        if (fSumDakotNichehut >= float.Parse(drSidur[J]["Michsat_shaot_chodshit"].ToString()))
                                        {
                                            drSidur[J]["Lo_letashlum"] = 1;
                                        }
                                        else
                                        {
                                            //oPeilut.dTaarich = dTaarich;
                                            objOved.Taarich = dTaarich;
                                            SetNetunimLeYom();
                                            fSumDakotNichehut += oSidur.CalcRechiv1BySidur(drSidur[J], 0, oPeilut);
                                        }
                                    }
                                }

                                drSidur = objOved.DtYemeyAvoda.Select("mispar_sidur=" + iMisparSidur + " and not Michsat_shaot_chodshit is null and Lo_letashlum =0", "TAARICH ASC");

                                for (J = 0; J < drSidur.Length; J++)
                                {
                                    dTaarich = DateTime.Parse(drSidur[J]["taarich"].ToString());
                                    //oSidur.dTaarich = dTaarich;

                                    dShatHatchala = DateTime.Parse(drSidur[J]["shat_hatchala_sidur"].ToString());
                                    iOutMichsa = int.Parse(drSidur[J]["out_michsa"].ToString());

                                    if (!oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, dTaarich, iMisparSidur, dShatHatchala, iOutMichsa))
                                    {

                                        if (!HaveSidurimNosafim(iMisparSidur, dTaarich))
                                        {
                                            drSidur[J]["Lo_letashlum"] = -1;
                                            if (fSumDakotNichehut >= float.Parse(drSidur[J]["Michsat_shaot_chodshit"].ToString()))
                                            {
                                                drSidur[J]["Lo_letashlum"] = 1;
                                            }
                                            else
                                            {
                                                //oPeilut.dTaarich = dTaarich;
                                                objOved.Taarich = dTaarich;
                                                SetNetunimLeYom();
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

                drSidurim = null;
                objOved.DtYemeyAvoda.Select(null, "Lo_letashlum");
                drSidurim = objOved.DtYemeyAvoda.Select("Lo_letashlum=-1", "");
                for (I = 0; I < drSidurim.Length; I++)
                {
                    drSidurim[I]["Lo_letashlum"] = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSidur = null;
                oPeilut = null;
                drSidurim = null; 
                drSidur = null;
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
                drSidurimNosafim = objOved.DtYemeyAvoda.Select("mispar_sidur<>" + iMisparSidur + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and not Michsat_shaot_chodshit is null and Lo_letashlum =0");
                for (i = 0; i < drSidurimNosafim.Length; i++)
                {
                    dShatHatchala = DateTime.Parse(drSidurimNosafim[i]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurimNosafim[i]["out_michsa"].ToString());

                    if (!oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, dTaarich, iMisparSidur, dShatHatchala, iOutMichsa))
                    {
                        bHaveSidurim = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                drSidurimNosafim = null;
            }
            return bHaveSidurim;
        }

      
        private void CalcRechivimInMonth(DateTime dTarMe, DateTime dTarAd)
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

                // CalcRechiv19();

             

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
                CalcRechiv143(dTarMe, dTarAd);

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

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());

                //fKizuzKaytanaTafkid= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode());
                //fKizuzKaytanaYeshivatTzevet= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode());
                //fKizuzKaytanaBniaPeruk= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode());
                //fKizuzKaytanaShivuk = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode());

                //fSumDakotRechiv = fSumDakotRechiv -(fKizuzKaytanaTafkid + fKizuzKaytanaYeshivatTzevet + fKizuzKaytanaBniaPeruk + fKizuzKaytanaShivuk);
                addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv2()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv3()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv4()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv5()
        {
            float fSumDakotRechiv;
            try
            {
                // fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ShaotHeadrut.GetHashCode());
                //if (objOved.objMeafyeneyOved.iMeafyen56==clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                //{
                //    fSumDakotRechiv = fSumDakotRechiv * clCalcGeneral.CalcMekademNipuach(_dTaarichChishuv,objOved.Mispar_ishi);
                //}
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotHeadrut.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv / 60;
                addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv7()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode());
                if (fSumDakotRechiv != 0)
                {
                    addRowToTable(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv8()
        {
            //•	X (קיזוז נוספות תנועה בשבת) = [דקות שבת בתנועה ללא מחוץ למכסה] מינוס מכסת תנועה בשבת (סך שבתונים בחודש [זיהוי שבתון (תאריך)] * 6).
            //•	Y (קיזוז נוספות תפקיד בשבת) =  [דקות שבת בתפקיד ללא מחוץ למכסה] מינוס מכסת תפקיד בשבת (סך שבתונים בחודש [זיהוי שבתון (תאריך)] * 6).
            //יש לכלול בחישוב רק סידורים עבורם שדה מחוץ למכסה [[TB_Sidurim_Ovedim.Out_michsa = 0 או Null.
            float fSumDakotRechiv, fTempX, fTempY, fSachDakot36, fSachDakot37, fMichsatShabat;
            int iSachShabatonim;
            try
            {
                iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);

                fSachDakot37 = oDay.GetRechiv37OutMichsa();
                fMichsatShabat = (objOved.objMeafyeneyOved.iMeafyen16 > -1 ? objOved.objMeafyeneyOved.iMeafyen16 : 0);
                if (!objOved.objMeafyeneyOved.Meafyen16Exists)
                {
                    fMichsatShabat = iSachShabatonim * 6 * 60;
                }

                fTempY = fSachDakot37 - fMichsatShabat;

                fSachDakot36 = oDay.GetRechiv36OutMichsa();
                fMichsatShabat = (objOved.objMeafyeneyOved.iMeafyen17 > -1 ? objOved.objMeafyeneyOved.iMeafyen17 : 0);
                if (!objOved.objMeafyeneyOved.Meafyen17Exists)
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzZchutChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv10()
        {
            float fSumDakotRechiv, fNosafotMichutzLamichsa, fTempX, fTempY, fTempZ, fTempW, fDakotTamritzTafkid, fMichsatNosafotTafkid;
            float fZmanHalbasha, fZmanNesiot, fZmanRetzifut;
            try
            {

                if (objOved.objMeafyeneyOved.iMeafyen54 > 0)
                {

                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                    //	X  [סה"כ ש"נ לחודש] = סכום ערך הרכיב של כל הימים בחודש חלקי 60 (כדי לקבל ערך בשעות) 
                    fDakotTamritzTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode());
                    fMichsatNosafotTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsatShaotNosafotTafkidChol);// oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode());
                    fTempX = (fDakotTamritzTafkid / 60);
                    if (fTempX > fMichsatNosafotTafkid)
                    {
                        // Z [ש"נ מחוץ למכסה] =  (סכום ערך הרכיב עבור כל הסידורים בחודש המסומנים מחוץ למכסה TB_Sidurim_Ovedim.Out_michsa = 1) חלקי 60 (לחלק ב-60 לקבלת נתון בשעות)
                        fNosafotMichutzLamichsa = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "OUT_MICHSA=0 AND  KOD_RECHIV=" + clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode().ToString()));

                        fTempZ = fNosafotMichutzLamichsa / 60;

                        //Y [קיזוז ש"נ]  = הגבוה מבין (0,  X פחות מכסת נוספות בתפקיד (רכיב 143) פחות Z)
                        fTempY = Math.Max(0, (fTempX - fMichsatNosafotTafkid - fTempZ));

                        //	W = X פחות Y [ש"נ אחרי קיזוז]  + זמן נסיעות (רכיב 95) + זמן הלבשה (רכיב 93) +  זמן רציפות (רכיב 97).
                        fZmanHalbasha = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHalbasha);// oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanHalbasha.GetHashCode());
                        fZmanNesiot = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanNesia);//oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanNesia.GetHashCode());
                        fZmanRetzifut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid);//oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode());

                        fTempW = fTempX - fTempY + fZmanHalbasha + fZmanNesiot + fZmanRetzifut;

                        if (fTempW > objOved.objParameters.iTamrizNosafotLoLetashlum)
                        {
                            fSumDakotRechiv = Math.Min(objOved.objParameters.iMaxNosafotTafkid, (fTempW - objOved.objParameters.iTamrizNosafotLoLetashlum));
                            addRowToTable(clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), fSumDakotRechiv);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv11()
        {

            float fSumDakotRechiv, fDakotNosafotNahagut, fDakotNosafot100;

            try
            {

                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    if (objOved.objMeafyeneyOved.iMeafyen56 == 52 || objOved.objMeafyeneyOved.iMeafyen56 == 62)
                    {
                        fDakotNosafot100 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot100.GetHashCode()); 
                        if (fDakotNosafot100 > objOved.objParameters.iTamrizNosafotLoLetashlum)
                        {
                            fSumDakotRechiv = Math.Min(objOved.objParameters.iMaxNosafotNahagut, fDakotNosafot100 - objOved.objParameters.iTamrizNosafotLoLetashlum);
                            addRowToTable(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), fSumDakotRechiv);
                        }
                    }
                    else if (objOved.objMeafyeneyOved.iMeafyen56 == 51 || objOved.objMeafyeneyOved.iMeafyen56 == 61)
                    {
                        fDakotNosafotNahagut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode()); 
                        if (fDakotNosafotNahagut > objOved.objParameters.iTamrizNosafotLoLetashlum)
                        {
                            fSumDakotRechiv = Math.Min(objOved.objParameters.iMaxNosafotNahagut, (fDakotNosafotNahagut/60) - objOved.objParameters.iTamrizNosafotLoLetashlum);
                            addRowToTable(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), fSumDakotRechiv);
                        }

                    }
                    ////if (objOved.objMeafyeneyOved.iMeafyen54 > 0)
                    ////{
                    //fDakotNosafotNahagut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode());
                    //if ((fDakotNosafotNahagut / 60) > objOved.objParameters.iTamrizNosafotLoLetashlum)
                    //{
                    //    fSumDakotRechiv = Math.Min(objOved.objParameters.iMaxNosafotNahagut, (fDakotNosafotNahagut / 60) - 30);
                    //    addRowToTable(clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), fSumDakotRechiv);

                    //}
                    ////}

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv12()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv13()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv14()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMachlifSadran.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv15()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMachlifPakach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv16()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMachlifKupai.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv17()
        {
            float fSumDakotRechiv;

            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode());

                addRowToTable(clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv18()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
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
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"]);

                fSumDakotRechiv19 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNosafotNahagut); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode());
                fSumDakotRechiv20 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNosafotNihul); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNihul.GetHashCode());
                fSumDakotRechiv21 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNosafotTafkid); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode());

                //  //עבור דקות נוספות בנהגות (רכיב 19): אין קיזוז 
                //  if (objOved.objMeafyeneyOved.Meafyen11Exists)
                //  {
                //      if (objOved.objMeafyeneyOved.iMeafyen11 == -1)
                //      {
                //          throw new Exception("null מאפיין 11 התקבל עם ערך");
                //      }
                //      iMichsatDakotNosafot = objOved.objMeafyeneyOved.iMeafyen11 * 60;
                //      fSumDakotRechiv = Math.Min(fSumDakotRechiv19, iMichsatDakotNosafot);
                //      addRowToTable(clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), fSumDakotRechiv);
                //  }

                //  //עבור דקות נוספות בניהול תנועה (רכיב 20):

                //  fSumDakotRechiv = fSumDakotRechiv20 / 60;

                //  fNosafotMichutz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode());

                // if (objOved.objPirteyOved.iGil==clGeneral.enKodGil.enKshishon.GetHashCode())
                // {
                //     fTempZ = 3;
                // }
                // else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                // {
                //     fTempZ = 6;
                // }


                // if (objOved.objMeafyeneyOved.iMeafyen13 == -1)
                // {
                //     throw new Exception("null מאפיין 13 התקבל עם ערך");
                //}
                // else
                //  {
                //      fMichsatNosafot = objOved.objMeafyeneyOved.iMeafyen13 + fTempZ;
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
                //  fDakotMichutzChol= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode());
                //  fMichsatTafkidChol= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode());

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv22()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv23()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotSikun.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotSikun.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv26()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv27()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv28()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaVisa.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv29()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv30()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaYomit.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv32()
        {
            float fDakotNochehut, fMichsaChodhit;
            float fSumDakotRechiv = 0;
            try
            {
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotRegilot.GetHashCode());
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode()); 
                    fMichsaChodhit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode()); 

                    if (fDakotNochehut > fMichsaChodhit)
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotRegilot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv33()
        {
            //יש לפתוח רשומה לרכיב רק אם סכום שלושת הרכיבים > 0
            //ערך הרכיב = דקות שבת בנהגות (רכיב 35)  +  דקות שבת בניהול תנועה (רכיב 36)  +  דקות שבת בתפקיד (רכיב 37).

            float fSumDakotRechiv;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode());

                addRowToTable(clGeneral.enRechivim.DakotShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv35()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv36()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv37()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv38()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachEshelBoker.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachEshelBoker.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelBoker.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv39()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv40()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachEshelErev.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachEshelErev.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelErev.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv41()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode());

                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv42()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv43()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv44()
        {
            float fSumDakotRechiv, fTempY, fTempW, fTempZ;

            //א.	אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך = 25 אזי:
            //Y = ערך רכיב דקות נוספות תפקיד (רכיב 21)
            //אחרת
            //Y = סכימת ערך הרכיב עבור כל ימי העבודה בחודש. 
            try
            {
            
                fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode());
                
                //ב.	W =  מקדם ניפוח 0.83 [שליפת פרמטר (קוד פרמטר = 177)] רלוונטי עבור עובדי 6 ימים בלבד [שליפת מאפיין ביצוע (קוד מאפיין=56)] עם ערך = 61 אחרת W = 1 (לעובדי 5 ימים).
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fTempW = objOved.fMekademNipuach;
                }
                else
                {
                    fTempW = 1;
                }

                if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode())
                {
                    fTempZ = objOved.objParameters.iGmulNosafotTzair;
                }
                else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                {
                    fTempZ = objOved.objParameters.iGmulNosafotKshishon;
                }
                else
                {
                    fTempZ = objOved.objParameters.iGmulNosafotKashish;
                }

                fSumDakotRechiv = (fTempY / fTempZ) * fTempW;
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), float.Parse(fSumDakotRechiv.ToString("00")));
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv45()
        {
            float fSumDakotRechiv, fKamutGmulNosafot, fKamutGmulChisachon;
            try
            {
                fKamutGmulChisachon = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode());
                fKamutGmulNosafot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode());

                fSumDakotRechiv = fKamutGmulChisachon + fKamutGmulNosafot;
                addRowToTable(clGeneral.enRechivim.SachGmulChisachon.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachGmulChisachon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv47()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachLina.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachLina.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachLina.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv48()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachLinaKfula.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachLinaKfula.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachLinaKfula.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv49()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachPitzul.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachPitzul.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachLinaKfula.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv50()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachPitzulKaful.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv52()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotLepremia.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv53()
        {
            float fSumDakotRechiv, fZchutChofesh;
            try
            {

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode());
                fZchutChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzZchutChofesh.GetHashCode());

                fSumDakotRechiv = (fSumDakotRechiv - fZchutChofesh) / 60;

                addRowToTable(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv54()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanLailaChok.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv55()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanLailaEgged.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv56()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomEvel.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomEvel.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomEvel.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv57()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHadracha.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomHadracha.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomHadracha.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv59()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomLeloDivuach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv60()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachla.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachla.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv61()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalaBoded.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv62()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMiluim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMiluim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMiluim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv63()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomAvodaBechul.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv64()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomTeuna.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomTeuna.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomTeuna.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv65()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomShmiratHerayon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv66()
        {
            float fSumDakotRechiv, fMichsaYomit, fNochehutChodshit, fShaotNosafot, fMichsaChodshit, fNosafot100 , fSachKizuzMeheadrut,fmichsatYom;
            float fMichsaChodshitChelkit, fNochehutChodshitChelkit,fHashlama;
            DataRow[] days;
            DateTime taarich;
            try
            {
                fSumDakotRechiv = 0; fmichsatYom = 0;
                fNochehutChodshitChelkit = 0;
                fMichsaChodshitChelkit = 0;

                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fNochehutChodshit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                fMichsaChodshit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                if (objOved.objMeafyeneyOved.iMeafyen83 == 1 && objOved.objMeafyeneyOved.iMeafyen33 == 1)
                {
                    fSachKizuzMeheadrut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_EZER)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString()));
                    fShaotNosafot = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachShaotNosafot); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachShaotNosafot.GetHashCode());
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                    if (fShaotNosafot > 0 && fSachKizuzMeheadrut > fShaotNosafot)
                    { fSachKizuzMeheadrut = fShaotNosafot; }
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrut.GetHashCode());
                    fSumDakotRechiv = (fSumDakotRechiv - fSachKizuzMeheadrut) * 60 / fMichsaYomit;
                    IpusRechivim();
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrut.GetHashCode());
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit < fMichsaChodshit)
                {
                    objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                    days = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and ERECH_RECHIV<1 and KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                    for (int i = 0; i < days.Length; i++)
                    {
                        taarich = DateTime.Parse(days[i]["taarich"].ToString());
                        if (fmichsatYom == 0)
                            fmichsatYom = float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());
                        fNochehutChodshitChelkit += float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());
                        fMichsaChodshitChelkit += float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());

                    }
                    fNosafot100 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot100); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                    fHashlama = Math.Min((fMichsaChodshitChelkit - fNochehutChodshitChelkit) / fmichsatYom, fNosafot100);
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrut.GetHashCode());
                    fSumDakotRechiv = fSumDakotRechiv - fHashlama;
              
                 }
                addRowToTable(clGeneral.enRechivim.YomHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv67()
        {

            float fSumDakotRechiv, fNochehutChodshit, fMichsaChodshit, fNosafot100,fSachKizuzMeheadrut;
            float fShaotNosafot, fMichsaYomit, fmichsatYom;
            float fMichsaChodshitChelkit, fNochehutChodshitChelkit,fHashlama;
            DataRow[] days;
            DateTime taarich;
            try
            {
                fSumDakotRechiv = 0; fmichsatYom = 0;
                fNochehutChodshitChelkit = 0;
                fMichsaChodshitChelkit = 0;
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fNochehutChodshit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                fMichsaChodshit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                if (objOved.objMeafyeneyOved.iMeafyen83 == 1 && objOved.objMeafyeneyOved.iMeafyen33 == 0)
                {
                    fSachKizuzMeheadrut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_EZER)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString()));
                    fShaotNosafot = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachShaotNosafot); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachShaotNosafot.GetHashCode());
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                    if (fShaotNosafot > 0 && fSachKizuzMeheadrut > fShaotNosafot)
                    { fSachKizuzMeheadrut = fShaotNosafot; }
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofesh.GetHashCode());
                    fSumDakotRechiv = (fSumDakotRechiv - fSachKizuzMeheadrut) * 60 / fMichsaYomit;
                    IpusRechivim();
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofesh.GetHashCode());
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit < fMichsaChodshit)
                {
                    objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                    days = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and ERECH_RECHIV<1 and KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                    for (int i = 0; i < days.Length; i++)
                    {
                        taarich = DateTime.Parse(days[i]["taarich"].ToString());
                        if (fmichsatYom==0)
                            fmichsatYom = float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());
                        fNochehutChodshitChelkit += float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());
                        fMichsaChodshitChelkit += float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());

                    }
                    fNosafot100 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot100); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                    fHashlama = Math.Min((fMichsaChodshitChelkit - fNochehutChodshitChelkit) / fmichsatYom, fNosafot100 );
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofesh.GetHashCode());
                    fSumDakotRechiv = fSumDakotRechiv - fHashlama;
                }


                addRowToTable(clGeneral.enRechivim.YomChofesh.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void UpdateRechiv146()
        {
            float fSumDakotRechiv,fHashlama, fNochehutChodshitChelkit, fMichsaChodshitChelkit, fNosafot100;
            DataRow[] days;
            DateTime taarich;
            try
            {
                fSumDakotRechiv = 0;
                fNochehutChodshitChelkit=0;
                fMichsaChodshitChelkit = 0;
                days = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and ERECH_RECHIV<1 and KOD_RECHIV="+ clGeneral.enRechivim.YomChofesh.GetHashCode().ToString());
                for (int i=0;i<days.Length;i++)
                {
                    taarich =DateTime.Parse(days[i]["taarich"].ToString());
                    fNochehutChodshitChelkit += float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());
                    fMichsaChodshitChelkit += float.Parse((objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich.ToShortDateString() + "', 'System.DateTime')"))[0]["ERECH_RECHIV"].ToString());

                }
                fNosafot100 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot100.GetHashCode());
                fHashlama = Math.Min((fMichsaChodshitChelkit - fNochehutChodshitChelkit)/60, fNosafot100);
                fSumDakotRechiv = fNosafot100 - fHashlama;
                addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv68()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomTipatChalav.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomTipatChalav.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomTipatChalav.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv69()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalatBenZug.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv70()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalatHorim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv71()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalaYeled.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv72()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv75()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv76()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode());
                addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Nosafot125.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv77()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot150.GetHashCode());
                addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Nosafot150.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv78()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NosafotShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NosafotShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NosafotShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv80()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv81()
        {
            float fSumDakotRechiv;
            try
            {
                //מחוץ למכסה בתפקיד בשבת (רכיב 186) + מחוץ למכסה ניהול תנועה בשבת (רכיב 187)
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv82()
        {
            float fSumDakotRechiv, fNosafotNahagut, fNosafotTnua, fNosafotTafkid;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                //ערך הרכיב = (דקות נוספות בנהגות (רכיב 19)   + דקות נוספות בניהול תנועה (רכיב 20)  + דקות נוספות בתפקיד (רכיב 21)) חלקי 60
                fNosafotNahagut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNosafotNahagut); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode());
                fNosafotTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNosafotNihul); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotNihul.GetHashCode());
                fNosafotTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNosafotTafkid); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode());

                fSumDakotRechiv = (fNosafotNahagut + fNosafotTnua + fNosafotTafkid) / 60;
                addRowToTable(clGeneral.enRechivim.SachNosafotChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv83()
        {
            float fSumDakotRechiv, fKizuzHatchalaGmar, fKizuzVisa, fKizuzMutam, fZmanAruchatTzaharayim;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fKizuzHatchalaGmar = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzDakotHatchalaGmar); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode());
                fKizuzVisa = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzBevisa); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzBevisa.GetHashCode());
                fKizuzMutam = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzOvedMutam); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzOvedMutam.GetHashCode());
                fZmanAruchatTzaharayim = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanAruchatTzaraim); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode());

                fSumDakotRechiv = (fKizuzHatchalaGmar + fKizuzVisa + fKizuzMutam + fZmanAruchatTzaharayim) / 60;
                addRowToTable(clGeneral.enRechivim.SachKizuzim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachKizuzim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv85()
        {
            float fSumDakotRechiv, fSumRetxzigfutNehiga, fSumRetzifutTafkid;
            try
            {
                fSumRetxzigfutNehiga = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode());
                fSumRetzifutTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode());

                if ((fSumRetxzigfutNehiga + fSumRetzifutTafkid) > objOved.objParameters.iMaxRetzifutChodshitImGlisha)
                {
                    fSumDakotRechiv = objOved.objParameters.iMaxRetzifutChodshitImGlisha;
                }
                else
                {
                    fSumDakotRechiv = (fSumRetxzigfutNehiga + fSumRetzifutTafkid);
                }

                addRowToTable(clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv86()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv88()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv89()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzBevisa.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzBevisa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzBevisa.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv90()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzOvedMutam.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv / 60;
                addRowToTable(clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv91()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv / 60;
                addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot25.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv92()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode());
                fSumDakotRechiv = fSumDakotRechiv / 60;
                addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot50.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv93()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHalbasha.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv94()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanHashlama.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanHashlama.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv95()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanNesia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanNesia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv96()
        {
            float fSumDakotRechiv = 0;
            try
            {

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode());

                addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv97()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv272()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv273()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv100()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot100Letashlum.GetHashCode());
                addRowToTable(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), fSumDakotRechiv);    
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv101()
        {
            float fSumDakotRechiv, fNosafot125;
            try
            {
                //	ברמה חודשית: ערך הרכיב = נוספות 125% (רכיב 76) )
                fNosafot125 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot125.GetHashCode());

                fSumDakotRechiv = fNosafot125;
                addRowToTable(clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv102()
        {
            float fSumDakotRechiv, fNosafot150, fKizuz150;
            try
            {
                //	ברמה חודשית : ערך הרכיב = נוספות 150% (רכיב 77)).
                fNosafot150 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot150.GetHashCode());
                fKizuz150 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Kizuz150.GetHashCode());

                fSumDakotRechiv = fNosafot150 - fKizuz150;
                addRowToTable(clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv103()
        {
            float fSumDakotRechiv, fNosafot200, fKizuz200;
            try
            {
                // 	ברמה חודשית : ערך הרכיב = נוספות 150% (רכיב 77) פחות קיזוז שעות 150% (רכיב 120).
                fNosafot200 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.NosafotShabat.GetHashCode());
                fKizuz200 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Kizuz200.GetHashCode());

                fSumDakotRechiv = fNosafot200 - fKizuz200;
                addRowToTable(clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv105()
        {
            float fSumDakotRechiv, fNosafotNahagut, fNosafotTnua, fNosafotTafkid, fShaot100;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                //ערך הרכיב =  (דקות נוספות נהגות (רכיב 19) + דקות נוספות תנועה (רכיב 20) + דקות נוספות תפקיד (רכיב 21)) חלקי 60  + שעות 100 לתשלום (רכיב 100).
                fNosafotNahagut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachNosafotNahagutCholVeshishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode());
                fNosafotTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachNosafotTnuaCholVeshishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode());
                fNosafotTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachNosafotTafkidCholVeshishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode());
                fShaot100 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ShaotShabat100); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Shaot100Letashlum.GetHashCode());

                fSumDakotRechiv = (fNosafotNahagut + fNosafotTnua + fNosafotTafkid) / 60 + fShaot100;
                addRowToTable(clGeneral.enRechivim.SachShaotNosafot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachShaotNosafot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv108()
        {
            float fSumDakotRechiv, fKizuzTafkidChol, fKizuzTnuaChol, fKizuzTafkidShanat, fKizuzTnuaShabat;//, fKizuzNahagutShabat , fKizuzNahagutChol;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fKizuzTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzNosafotTafkidChol); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode());
                fKizuzTnuaChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzNosafotTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode());
                //fKizuzNahagutChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotNahagutChol.GetHashCode());
                fKizuzTafkidShanat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzNosafotTafkidShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotTafkidShabat.GetHashCode());
                //fKizuzNahagutShabat = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotNahgutShabat.GetHashCode());
                fKizuzTnuaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzNosafotTnuaShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode());

                fSumDakotRechiv = fKizuzTafkidChol + fKizuzTnuaChol + fKizuzTafkidShanat + fKizuzTnuaShabat;// +fKizuzNahagutShabat+ fKizuzNahagutChol ;
                addRowToTable(clGeneral.enRechivim.SachKizuzShaotNosafot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachKizuzShaotNosafot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv109()
        {
            float fSumDakotRechiv, fDakotNechehut, fMichsaChodshit;
            try
            {

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {

                   fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YemeyAvoda.GetHashCode());
                   addRowToTable(clGeneral.enRechivim.YemeyAvoda.GetHashCode(), fSumDakotRechiv);
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YemeyAvoda.GetHashCode());
                    addRowToTable(clGeneral.enRechivim.YemeyAvoda.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyAvoda.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv110()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        //private void CalcRechiv111()
        //{
        //    float fSumDakotRechiv=0;
        //    try{
        //        if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
        //        {
        //            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

        //        }
        //        else if (objOved.objMeafyeneyOved.Meafyen2Exists)
        //        {
        //            if (objOved.objMeafyeneyOved.iMeafyen2 > 0)
        //            {
        //                fSumDakotRechiv = objOved.objMeafyeneyOved.iMeafyen2;
        //            }
        //            else 
        //            {
        //                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());
        //            }
        //        }
        //       else if (objOved.objMeafyeneyOved.iMeafyen56==clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
        //           {
        //           fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

        //           }

        //    addRowToTable(clGeneral.enRechivim.MichsaChodshit.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsaChodshit.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }       
        //}

        private void CalcRechiv112()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaChodshit(objOved.dtPremyot, clGeneral.enSugPremia.Grira.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiaGrira.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiaGrira.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv113()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaChodshit(objOved.dtPremyot, clGeneral.enSugPremia.Machsenaim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv115()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaChodshit(objOved.dtPremyot, clGeneral.enSugPremia.Meshek.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiaMeshek.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiaMeshek.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv119_120_122()
        {
            float fErech, fSumDakotRechiv122, fSumDakotRechiv119, fSumDakotRechiv120, fShaot125, fTempX;
            float fShaot100, fKizuzNosafotTnua, fKizuzNosafotTafkid; //, fKizuzMachlifTnua, fKizuzVaadOvdim, fKizuzMishpatChaverim;
            try
            {
                fSumDakotRechiv122 = 0;
                fSumDakotRechiv119 = 0;
                fSumDakotRechiv120 = 0;

                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fKizuzNosafotTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzNosafotTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode());
                fKizuzNosafotTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzNosafotTafkidChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode());
                //fKizuzVaadOvdim = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode());
                //fKizuzMishpatChaverim = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode());
                //fKizuzMachlifTnua= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMachlifTnua.GetHashCode());

                fTempX = fKizuzNosafotTnua + fKizuzNosafotTafkid; // +fKizuzVaadOvdim + fKizuzMachlifTnua + fKizuzMishpatChaverim;

                fShaot100 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot100Letashlum); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Shaot100Letashlum.GetHashCode());
                fShaot125 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot125Letashlum); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Shaot125Letashlum.GetHashCode());

                if (fTempX <= fShaot100)
                {
                    fSumDakotRechiv122 = fTempX;
                    objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                    if (objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()).Length > 0)
                    {
                        objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = float.Parse(objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"].ToString()) - fTempX;
                    }
                }
                else if (fTempX <= (fShaot100 + fShaot125))
                {
                    fSumDakotRechiv119 = fTempX - fShaot100;
                    fSumDakotRechiv122 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot100); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot100.GetHashCode());
                    objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                    if (objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()).Length > 0)
                    {
                        objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = 0;
                    }
                    fErech = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot125) - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Kizuz125);  // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot125.GetHashCode()) - oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Kizuz125.GetHashCode());
                    if (objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString()).Length > 0)
                    {
                        objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = fErech;
                    }
                }
                else
                {
                    objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
                    fSumDakotRechiv120 = fTempX - (fShaot100 + fShaot125);
                    fSumDakotRechiv119 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot125.GetHashCode());
                    fSumDakotRechiv122 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot100.GetHashCode());
                    if (objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString()).Length > 0)
                    {
                        objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot100Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = 0;
                    }
                    if (objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString()).Length > 0)
                    {
                        objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot125Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = 0;
                    }
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Nosafot150.GetHashCode()) - oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.Kizuz150.GetHashCode());
                    if (objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot150Letashlum.GetHashCode().ToString()).Length > 0)
                    {
                        objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Shaot150Letashlum.GetHashCode().ToString())[0]["ERECH_RECHIV"] = fErech;
                    }
                }

                addRowToTable(clGeneral.enRechivim.Kizuz100.GetHashCode(), fSumDakotRechiv122);
                addRowToTable(clGeneral.enRechivim.Kizuz150.GetHashCode(), fSumDakotRechiv120);
                addRowToTable(clGeneral.enRechivim.Kizuz125.GetHashCode(), fSumDakotRechiv119);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Kizuz100.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv121_161_163()
        {
            float fNosafotShabat, fTempX, fTempY, fDakotShabat, fDakotMichutLamichsa;
            float fShaotLeloMichutz, fTempZ, fTempW, fTempM;
            //int iSachShabatonim;
            try
            {
                fNosafotShabat = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.NosafotShabat.GetHashCode());
                if (fNosafotShabat > 0)
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                    //iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);

                    //fTempX = (objOved.objMeafyeneyOved.iMeafyen16>-1?  objOved.objMeafyeneyOved.iMeafyen16:0);
                    //if (!objOved.objMeafyeneyOved.Meafyen16Exists)
                    //{
                    //    fTempX = iSachShabatonim * 6;
                    //}

                    //fTempY = (objOved.objMeafyeneyOved.iMeafyen17>-1 ? objOved.objMeafyeneyOved.iMeafyen17: 0);
                    //if (!objOved.objMeafyeneyOved.Meafyen17Exists)
                    //{
                    //    fTempY = iSachShabatonim * 6;
                    //}

                    fTempX = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode());

                    fTempY = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsatShaotNoafotNihulShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode());

                    //ה.	ערך רכיב קיזוז נוספות תפקיד שבת 
                    fDakotShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode());
                    fDakotMichutLamichsa = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichutzLamichsaTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode());
                    fShaotLeloMichutz = fDakotShabat - fDakotMichutLamichsa;
                    fTempZ = 0;
                    if (fTempX < (fShaotLeloMichutz / 60))
                    {
                        fTempZ = (fShaotLeloMichutz / 60) - fTempX;
                    }

                    addRowToTable(clGeneral.enRechivim.KizuzNosafotTafkidShabat.GetHashCode(), fTempZ);

                    //ח.	ערך רכיב קיזוז נוספות תנועה שבת 
                    fDakotShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode());
                    fDakotMichutLamichsa = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichutzLamichsaNihulShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode());
                    fShaotLeloMichutz = fDakotShabat - fDakotMichutLamichsa;
                    fTempW = 0;
                    if (fTempY < (fShaotLeloMichutz / 60))
                    {
                        fTempW = (fShaotLeloMichutz / 60) - fTempY;
                    }

                    addRowToTable(clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode(), fTempW);


                    fTempM = (fTempW + fTempZ) * 60;
                    if (fTempM < fNosafotShabat)
                    {
                        addRowToTable(clGeneral.enRechivim.Kizuz200.GetHashCode(), fTempM);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotTnuaShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv125()
        {
            float fSumDakotRechiv;
            try
            {
                //יש לבצע את חישוב הרכיב רק עבור עובדים עם [שליפת מאפיין ביצוע (קוד מאפיין = 42, מ.א., תאריך)] עם ערך 1, אחרת אין לפתוח רשומה לרכיב זה בשום רמה.
                if (objOved.objMeafyeneyOved.sMeafyen42 == "1")
                {
                    //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode());
                    addRowToTable(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv126()
        {
            float fSumDakotRechiv;
            try
            {
                if (objOved.objMeafyeneyOved.iMeafyen2 > 0)
                {
                    fSumDakotRechiv = objOved.objMeafyeneyOved.iMeafyen2;
                }
                else
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());
                }
                addRowToTable(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv128()
        {
            float fSumDakotRechiv;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"]);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanGrirot); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanGrirot.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanGrirot.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.TosefetGririoTchilatSidur); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.TosefetGrirotSofSidur); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanGrirot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv129()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMachlifTnua.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv130()
        {
            float fSumDakotRechiv, fKizuzMachlifPakach, fKizuzMachlifSadran, fKizuzMachlifMeshaleach, fKizuzMachlifKupai, fKizuzMachlifRakaz;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fKizuzMachlifPakach = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzMachlifPakach); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode());
                fKizuzMachlifSadran = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzMachlifSadran); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode());
                fKizuzMachlifMeshaleach = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzMachlifMeshaleach); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode());
                fKizuzMachlifKupai = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzMachlifKupai); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode());
                fKizuzMachlifRakaz = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.KizuzMachlifRakaz); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode());

                fSumDakotRechiv = fKizuzMachlifPakach + fKizuzMachlifSadran + fKizuzMachlifMeshaleach + fKizuzMachlifKupai + fKizuzMachlifRakaz;

                addRowToTable(clGeneral.enRechivim.KizuzMachlifTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv131()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ShaotShabat100.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ShaotShabat100.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv132()
        {
            float fSumDakotRechiv, fZmanHamaraShabat, fDakotZikuyChofesh;
            try
            {
                fZmanHamaraShabat = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode());
                fDakotZikuyChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode());

                fSumDakotRechiv = fZmanHamaraShabat + fDakotZikuyChofesh;
                addRowToTable(clGeneral.enRechivim.ChofeshZchut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ChofeshZchut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv133()
        {
            float fSumDakotRechiv, fPremiaShabat, fPremiaYomit, fMichsaChodshit, fPremiaShishi, fDakotNochehut;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);
                fSumDakotRechiv = 0;
                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                {
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutBefoal); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode());
                    fMichsaChodshit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());
                    if (fMichsaChodshit>0)
                        fSumDakotRechiv = Math.Min((fDakotNochehut * objOved.objParameters.fBasisLechishuvPremia) / Math.Min(objOved.objParameters.fMichsatSaotChodshitET * 60, fMichsaChodshit), objOved.objParameters.fMaxPremiatNehiga);
                }
                else
                {
                    fPremiaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotPremiaShabat.GetHashCode());
                    fPremiaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaYomit); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotPremiaYomit.GetHashCode());
                    fPremiaShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaBeShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode());

                    fSumDakotRechiv = fPremiaShabat + fPremiaYomit + fPremiaShishi;
                }
                addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyaRegila.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv134()
        {
            float fSumDakotRechiv, fDakotPremiaVisaShabat, fDakotPremiaVisa, fDakotPremiaVisaShishi;
            try
            {
                fDakotPremiaVisa = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotPremiaVisa.GetHashCode());
                fDakotPremiaVisaShabat = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode());
                fDakotPremiaVisaShishi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode());

                fSumDakotRechiv = fDakotPremiaVisa + fDakotPremiaVisaShabat + fDakotPremiaVisaShishi;
                addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyaNamlak.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv135()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv136()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv137()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv138()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv139()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv140()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv141()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv142()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzNochehut.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzNochehut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNochehut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv143(DateTime dTarMe, DateTime dTarAd)
        {
            float fMichsaYomit, fDakotNochehut, fDakotNochehutBefoal, fHadracha, fSumDakotRechiv, fTempX, fTempY, fTemp;
            int iSugYom;
            DateTime dTaarich;
            Boolean bPutar = false;
            Boolean bLechishuv = false;
            fTempX = 0;
            try
            {
                if ((objOved.objMeafyeneyOved.iMeafyen14 <= 0) && objOved.objMeafyeneyOved.iMeafyen12 > 0)
                {
                    dTaarich = dTarMe;
                    bPutar = oCalcBL.CheckOvedPutar(objOved);
                    while (dTaarich <= dTarAd)
                    {
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dTaarich);
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), dTaarich);

                        iSugYom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dTaarich,objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.iMeafyen56);
                        bLechishuv = false;
                        if ((iSugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode()) && objOved.objMeafyeneyOved.iMeafyen85 == 1 && fDakotNochehut == 0 && fMichsaYomit > 0 && bPutar)
                        {
                            bLechishuv = true;
                        }
                        if (iSugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode() && objOved.objMeafyeneyOved.sMeafyen63.Length > 0)
                        {
                            bLechishuv = true;
                        }
                        if (dTaarich.Day >= 24)
                        {
                            //בדיקה אם קיימים אחד מן הרכיבים הללו באותו יום:
                            //  יום אבל (רכיב 56), מחלה (רכיב 60), מחלה בודד (רכיב 61), תאונה (רכיב 64)
                            // הריון בת זוג (רכיב 65), מחלת בן זוג (רכיב 69), מחלת הורים (רכיב 70)., מחלת ילד (רכיב 71), מילואים (רכיב 74).

                            if (objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("kod_rechiv in(56,60,61,64,65,69,70,71,74) and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                            {
                                bLechishuv = true;
                            }
                        }

                        if (bLechishuv == false)
                        {
                            fHadracha = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHadracha.GetHashCode() , dTaarich );
                            fDakotNochehutBefoal = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode() , dTaarich);
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

                if ((objOved.objMeafyeneyOved.iMeafyen14 > 0))
                {
                    fSumDakotRechiv = objOved.objMeafyeneyOved.iMeafyen14;
                }
                else
                {
                    objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                    fTempY = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 and KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString(), "").Length;
                    if (objOved.objMeafyeneyOved.iMeafyen12 == -1)
                    {
                        throw new Exception("ערך null במאפיין 12");
                    }

                    if (objOved.objMeafyeneyOved.iMeafyen84 == -1)
                    {
                        throw new Exception("ערך null במאפיין 84");
                    }
                    fTemp = objOved.objMeafyeneyOved.iMeafyen12 + objOved.objMeafyeneyOved.iMeafyen84;
                    fTemp = objOved.objMeafyeneyOved.iMeafyen12;

                    fSumDakotRechiv = fTemp * fTempX / fTempY;

                }


                addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv145()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv146()
        {
            float fSumDakotRechiv;//, fDakotNochehut, fMichsaChodshit;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot100.GetHashCode());
                addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
                //fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                //fMichsaChodshit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());

                //if (fMichsaChodshit > 0 && fDakotNochehut > fMichsaChodshit)
                //{
                //    fSumDakotRechiv = (fDakotNochehut - fMichsaChodshit) / 60;
                //    addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Nosafot100.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv147()
        {
            float fSumDakotRechiv, fRechivChodshi, fRechivYomi;
            try
            {
                fRechivYomi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode());
                fRechivChodshi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode());

                fSumDakotRechiv = fRechivYomi - fRechivChodshi;

                addRowToTable(clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv149()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode());

                addRowToTable(clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv160()
        {
            float fSumDakotRechiv, fRechiv251Chodshi, fRechiv251Yomi;
            try
            {
                fRechiv251Yomi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode());
                fRechiv251Chodshi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode());

                fSumDakotRechiv = fRechiv251Yomi - fRechiv251Chodshi;

                addRowToTable(clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        //private void CalcRechiv164()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode());
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv165()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode());
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv166()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode());
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv167()
        //{
        //    float fSumDakotRechiv;
        //    try{
        //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode());
        //    addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv169()
        //{
        //    float fSumDakotRechiv,fMichsatNosafotPakach,fSachDakotLeloMichutzLemichsa,fNosafotPakach,fNosafotMachlif,fNosafotMichutz;
        //    try{
        //    fMichsatNosafotPakach = objOved.objParameters.iMaxDakotNosafotPakach;

        //    fNosafotPakach = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotPakach.GetHashCode());
        //    fNosafotMachlif = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotMachlifPakach.GetHashCode());
        //    fNosafotMichutz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode());
        //    fSachDakotLeloMichutzLemichsa = (fNosafotPakach + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotPakach)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fMichsatNosafotPakach;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotPakach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotPakach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv170()
        //{
        //    float fSumDakotRechiv, fMichsatNosafotSadran, fSachDakotLeloMichutzLemichsa, fNosafotSadran, fNosafotMachlif, fNosafotMichutz;
        //    try{
        //    fMichsatNosafotSadran = objOved.objParameters.iMaxDakotNosafotSadran;

        //    fNosafotSadran = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotSadran.GetHashCode());
        //    fNosafotMachlif = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotMachlifSadran.GetHashCode());
        //    fNosafotMichutz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode());
        //    fSachDakotLeloMichutzLemichsa = (fNosafotSadran + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotSadran)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fMichsatNosafotSadran;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotSadran.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotSadran.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv171()
        //{
        //    float fSumDakotRechiv, fMichsatNosafotRakaz, fSachDakotLeloMichutzLemichsa, fNosafotRakaz, fNosafotMachlif, fNosafotMichutz;
        //    try{
        //    fMichsatNosafotRakaz = objOved.objParameters.iMaxDakotNosafotRakaz;

        //    fNosafotRakaz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotRakaz.GetHashCode());
        //    fNosafotMachlif = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotMachlifRakaz.GetHashCode());
        //    fNosafotMichutz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode());
        //    fSachDakotLeloMichutzLemichsa = (fNosafotRakaz + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotRakaz)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fMichsatNosafotRakaz;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotRakaz.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotRakaz.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv172()
        //{
        //    float fSumDakotRechiv, fMichsatNosafotMeshaleach, fSachDakotLeloMichutzLemichsa, fNosafotMeshaleach, fNosafotMachlif, fNosafotMichutz;
        //    try{
        //    fMichsatNosafotMeshaleach = objOved.objParameters.iMaxDakotNosafotRakaz;

        //    fNosafotMeshaleach = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotMeshaleach.GetHashCode());
        //    fNosafotMachlif = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNosafotMachlifMeshaleach.GetHashCode());
        //    fNosafotMichutz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode());
        //    fSachDakotLeloMichutzLemichsa = (fNosafotMeshaleach + fNosafotMachlif) - fNosafotMichutz;

        //    if (fSachDakotLeloMichutzLemichsa > fMichsatNosafotMeshaleach)
        //    {
        //        fSumDakotRechiv = fSachDakotLeloMichutzLemichsa - fNosafotMeshaleach;
        //        addRowToTable(clGeneral.enRechivim.KizuzNosafotMeshaleach.GetHashCode(), fSumDakotRechiv);
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         clLogBakashot.SetError(objOved.iBakashaId,objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotMeshaleach.GetHashCode(),_dTaarichChishuv ,"CalcMonth: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        private void CalcRechiv174()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv175()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv176()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv177()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv178()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv179()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotPakach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotPakach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv180()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotSadran.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotSadran.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv181()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotRakaz.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv182()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotMeshalech.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv183()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotKupai.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotKupai.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv184()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv185()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv186()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv187()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode());
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv188()
        {
            float fSumDakotRechiv, fDakotNehigaChol, fDakotShabatBenahagut, fDakotNehigaShishi;
            try
            {
                fDakotNehigaChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode());
                fDakotShabatBenahagut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode());
                fDakotNehigaShishi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode());

                fSumDakotRechiv = fDakotNehigaChol + fDakotShabatBenahagut + fDakotNehigaShishi;
                addRowToTable(clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv189()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv190()
        {
            float fSumDakotRechiv, fDakotNihulChol, fDakotNihulShabat, fDakotNihulShishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fDakotNihulChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode());
                fDakotNihulShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode());
                fDakotNihulShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNihulShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode());

                fSumDakotRechiv = fDakotNihulChol + fDakotNihulShabat + fDakotNihulShishi;
                addRowToTable(clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv191()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv192()
        {
            float fSumDakotRechiv, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fDakotTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode());
                fDakotTafkidShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode());
                fDakotTafkidShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotTafkidShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode());

                fSumDakotRechiv = fDakotTafkidChol + fDakotTafkidShabat + fDakotTafkidShishi;
                addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv193()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv194()
        {
            float fSumDakotRechiv, fNehigaChol, fNihulTnua, fTafkidChol;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fNehigaChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaChol); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode());
                fNihulTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode());
                fTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode());

                fSumDakotRechiv = fNehigaChol + fNihulTnua + fTafkidChol;
                addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv195()
        {
            float fSumDakotRechiv, fNehigaShishi, fNihulShishi, fTafkidShishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fNehigaShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNehigaShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode());
                fNihulShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNihulShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode());
                fTafkidShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotTafkidShishi); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode());

                fSumDakotRechiv = fNehigaShishi + fNihulShishi + fTafkidShishi;
                addRowToTable(clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv196()
        {
            float fSumDakotRechiv, fNehigaShabat, fNihulShabat, fTafkidShabat;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fNehigaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode());
                fNihulShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode());
                fTafkidShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode());

                fSumDakotRechiv = fNehigaShabat + fNihulShabat + fTafkidShabat;
                addRowToTable(clGeneral.enRechivim.NochehutShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv198()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NosafotShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NosafotShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NosafotShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv200()
        {
            float fSumDakotRechiv, fDakotNihul, fDakotTafkid;
            try
            {
                fDakotTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode());
                fDakotNihul = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode());

                fSumDakotRechiv = fDakotNihul + fDakotTafkid;
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv201()
        {
            float fSumDakotRechiv, fDakotNihul, fDakotTafkid;
            try
            {
                fDakotTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode());
                fDakotNihul = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode());

                fSumDakotRechiv = fDakotNihul + fDakotTafkid;
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv202()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode());

                addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv203()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv204()
        {
            float fSumDakotRechiv, fNochehutPremia;
            int iMutaam;
            try
            {
                if (objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.Rasham.GetHashCode() || objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.SganMenahel.GetHashCode() || objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMachlaka.GetHashCode())
                {
                    if (objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumArtzi.GetHashCode() || objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumBameshek.GetHashCode())
                    {
                        iMutaam = objOved.objPirteyOved.iMutamut;
                        if (iMutaam != clGeneral.enMutaam.enMutaam1.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam3.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam5.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam7.GetHashCode())
                        {
                            if (objOved.objMeafyeneyOved.iMeafyen60 <= 0)
                            {
                                fNochehutPremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode());
                                fSumDakotRechiv = (objOved.objParameters.fAchuzPremiaRishum * fNochehutPremia) / 100;
                                addRowToTable(clGeneral.enRechivim.PremiaLariushum.GetHashCode(), fSumDakotRechiv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiaLariushum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv206()
        {
            float fSumDakotRechiv, fNochehutLepremia;
            try
            {
                fNochehutLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode());

                fSumDakotRechiv = (objOved.objParameters.fAchuzPremiatMachsanKartisim * fNochehutLepremia) / 100;
                addRowToTable(clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv207()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv208()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode());
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv209()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv210()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv211()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv212()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv213()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachNesiot.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachNesiot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNesiot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv214()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotHistaglut.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotHistaglut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv215()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachKM.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachKM.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv216()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode());
                addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv217()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotHagdara.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotHagdara.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv218()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotKisuiTor.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv219()
        {
            float fSumDakotRechiv, fDakotChofesh;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fDakotChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotChofesh.GetHashCode());
                fSumDakotRechiv = fDakotChofesh / 60;
                addRowToTable(clGeneral.enRechivim.ShaotChofesh.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ShaotChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv220()
        {
            float fSumDakotRechiv, fNochehutChodshit, fMichsaChodshit;
            try
            {
                fNochehutChodshit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                fMichsaChodshit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());
                fSumDakotRechiv = 0;

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit < fMichsaChodshit)
                {
                    // fYemeyAvoda = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.YemeyAvoda.GetHashCode());

                    fSumDakotRechiv = (fMichsaChodshit - fNochehutChodshit);//(fMichsaChodshit/fYemeyAvoda);
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotHeadrut.GetHashCode());

                }
                addRowToTable(clGeneral.enRechivim.DakotHeadrut.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv221()
        {
            float fSumDakotRechiv, fNochehutChodshit, fMichsaChodshit;//, fYemeyAvoda;
            try
            {
                fNochehutChodshit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                fMichsaChodshit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode());
                fSumDakotRechiv = 0;

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fNochehutChodshit < fMichsaChodshit)
                {
                    //fYemeyAvoda = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.YemeyAvoda.GetHashCode());

                    fSumDakotRechiv = (fMichsaChodshit - fNochehutChodshit);// / (fMichsaChodshit / fYemeyAvoda);
                }
                else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotChofesh.GetHashCode());

                }
                addRowToTable(clGeneral.enRechivim.DakotChofesh.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotChofesh.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv223()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode());

                addRowToTable(clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv224()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.MevakrimBadrachim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv225()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.MifalYetzur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv226()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.NehageyTovala.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv227()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.NehageyTenderim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv228()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.Dfus.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatDfus.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatDfus.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv229()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot,clGeneral.enSugPremia.MisgarotAchzaka.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMisgarot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv230()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.Gifur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatGifur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatGifur.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv231()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot,clGeneral.enSugPremia.MusachRishonLetzyon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv232()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot,clGeneral.enSugPremia.TechnayYetzur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv233()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv234()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv235()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv236()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv237()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv238()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv239()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv240()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv241()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv242()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot, clGeneral.enSugPremia.ReshetBitachon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv243()
        {
            float fSumDakotRechiv;
            try
            {
                fSumDakotRechiv = oCalcBL.GetPremiaYadanit(objOved.dtPremyotYadaniyot,clGeneral.enSugPremia.PerukVeshiputz.GetHashCode());
                addRowToTable(clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv244()
        {
            float fSumDakotRechiv, fYemeyAvoda;
            try
            {
                fSumDakotRechiv = 0;
                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && objOved.objMeafyeneyOved.iMeafyen53 > 0)
                {
                    if (objOved.objMeafyeneyOved.iMeafyen53.ToString().Substring(0, 1) == "1")
                    {
                        //fYemeyAvoda = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.YemeyAvoda.GetHashCode());
                        //fSumDakotRechiv = (fYemeyAvoda * int.Parse(objOved.objMeafyeneyOved.iMeafyen53.ToString().Substring(objOved.objMeafyeneyOved.iMeafyen53.ToString().Length - 4, 3))) / 100;
                        fSumDakotRechiv =  float.Parse(objOved.objMeafyeneyOved.iMeafyen53.ToString().Substring(objOved.objMeafyeneyOved.iMeafyen53.ToString().Length - 3, 3))/10;
                    }
                    else if (objOved.objMeafyeneyOved.iMeafyen53.ToString().Substring(0, 1) == "2")
                    {
                        fSumDakotRechiv = float.Parse(objOved.objMeafyeneyOved.iMeafyen53.ToString().Substring(objOved.objMeafyeneyOved.iMeafyen53.ToString().Length - 3, 3));
                    }
                    addRowToTable(clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), fSumDakotRechiv);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv245()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode());
                addRowToTable(clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv246()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv247()
        {
            float fSumDakotRechiv, fNochehutPremia;
            try
            {
                if (objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMoreNehiga.GetHashCode())
                {

                    if (objOved.objMeafyeneyOved.iMeafyen60 <= 0)
                    {
                        fNochehutPremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"], clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode());
                        fSumDakotRechiv = (objOved.objParameters.fAchuzPremiaMenahel * fNochehutPremia) / 100;
                        addRowToTable(clGeneral.enRechivim.PremyaMenahel.GetHashCode(), fSumDakotRechiv);
                    }

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyaMenahel.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv248()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv249()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv250()
        {
            float fSumDakotRechiv, fTempX, fShaotTafkidLeloMichutz;
            float fDakotMichutzChol, fMichsatTafkidChol, fSumDakotRechiv207, fTempY;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode())/60; 

                fDakotMichutzChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMichutzTafkidChol);
                fSumDakotRechiv207 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichutzLamichsaTafkidShishi); 
                fShaotTafkidLeloMichutz = fTempX - ((fDakotMichutzChol + fSumDakotRechiv207) / 60);

                fMichsatTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsatShaotNosafotTafkidChol); 
                fTempY = fShaotTafkidLeloMichutz > fMichsatTafkidChol ? fMichsatTafkidChol : fShaotTafkidLeloMichutz;

                fSumDakotRechiv = ((fTempY * 60) + fDakotMichutzChol + fSumDakotRechiv207) / 60;

                addRowToTable(clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv251()
        {
            float fSumDakotRechiv, fTempX, fNosafotMichutz;
            float fMichsatNosafot, fSumDakotRechiv208, fTempY;
            try
            {

                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_CHODESH"]);

                fTempX = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachNosafotTnuaCholVeshishi);

                fNosafotMichutz = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua);
                fSumDakotRechiv208 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichutzLamichsaTnuaShishi);
                fNosafotMichutz = fTempX - ((fNosafotMichutz + fSumDakotRechiv208) / 60);

                fMichsatNosafot = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsatShaotNosafotNihul);
                fTempY = fNosafotMichutz > fMichsatNosafot ? fMichsatNosafot : fNosafotMichutz;

                fSumDakotRechiv = ((fTempY * 60) + fNosafotMichutz + fSumDakotRechiv208) / 60;

                addRowToTable(clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), fSumDakotRechiv);
          }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv252()
        {
            float fSumDakotRechiv, fTempX, iMichsatDakotNosafot;
            try
            {
                fSumDakotRechiv = 0;

                fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode()); // clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_CHODESH"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode().ToString())) / 60;

                if (objOved.objMeafyeneyOved.iMeafyen11 == -1)
                {
                    throw new Exception("null מאפיין 11 התקבל עם ערך");
                }

                iMichsatDakotNosafot = objOved.objMeafyeneyOved.iMeafyen11;
                fSumDakotRechiv = Math.Min(fTempX,( iMichsatDakotNosafot * 60));
                addRowToTable(clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), fSumDakotRechiv);
            
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv253()
        {
            float fSumDakotRechiv, fTempZ;
            try
            {
                fTempZ = 0;
                if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                {
                    fTempZ = 3;
                }
                else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                {
                    fTempZ = 6;
                }

                if (objOved.objMeafyeneyOved.iMeafyen13 == -1)
                {
                    throw new Exception("null מאפיין 13 התקבל עם ערך");
                }
                else
                {
                    fSumDakotRechiv = objOved.objMeafyeneyOved.iMeafyen13 + fTempZ;
                }

                addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsatShaotNosafotNihul.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv254()
        {
            float fSumDakotRechiv, iSachShabatonim;
            try
            {
                iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);

                fSumDakotRechiv = (objOved.objMeafyeneyOved.iMeafyen16 > -1 ? objOved.objMeafyeneyOved.iMeafyen16 : 0);
                if (!objOved.objMeafyeneyOved.Meafyen16Exists)
                {
                    fSumDakotRechiv = iSachShabatonim * 6;
                }

                addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsatShaotNosafotTafkidShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv255()
        {
            float fSumDakotRechiv, iSachShabatonim;
            try
            {
                iSachShabatonim = GetSachShabatonimInMonth(_dTaarichChishuv);

                fSumDakotRechiv = (objOved.objMeafyeneyOved.iMeafyen17 > -1 ? objOved.objMeafyeneyOved.iMeafyen17 : 0);
                if (!objOved.objMeafyeneyOved.Meafyen17Exists)
                {
                    fSumDakotRechiv = iSachShabatonim * 6;
                }

                addRowToTable(clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsatShaotNoafotNihulShabat.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv256()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv257()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv258()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv259()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv260()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode());
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv261()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MachalaYomMale.GetHashCode());
                addRowToTable(clGeneral.enRechivim.MachalaYomMale.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MachalaYomMale.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv262()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MachalaYomChelki.GetHashCode());
                addRowToTable(clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv263()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenahagut.GetHashCode());
                addRowToTable(clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv264()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode());
                addRowToTable(clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv265()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBetafkid.GetHashCode());
                addRowToTable(clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv266()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMiluimChelki.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv267()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotElementim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotElementim.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv268()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv269()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode());
                addRowToTable(clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv270()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv271()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode());
                addRowToTable(clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv931()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode());
                addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv932()
        {
            float fSumDakotRechiv;
            try
            {
                //ערך הרכיב = סכימת ערך הרכיב לכל הימים בחודש  
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HalbashaSofYom.GetHashCode());
                addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), _dTaarichChishuv, "CalcMonth: " + ex.Message);
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

            drRowToRemove = objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV IN(" + sRechivim + ")");
            for (int i = 0; i < drRowToRemove.Length - 1; i++)
            {
                drRowToRemove[0].Delete();
            }
            drRowToRemove = null;
            drRowToRemove = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV IN(" + sRechivim + ")");
            for (int i = 0; i < drRowToRemove.Length - 1; i++)
            {
                drRowToRemove[0].Delete();
            }
            drRowToRemove = null;
            drRowToRemove = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV IN(" + sRechivim + ")");
            for (int i = 0; i < drRowToRemove.Length - 1; i++)
            {
                drRowToRemove[0].Delete();
            }
            drRowToRemove = null;

        }
        private void addRowToTable(int iKodRechiv, float fErechRechiv)
        {
            DataRow drChishuv;
//            DataRow[] drChishuvRows;
//            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select(null, "KOD_RECHIV");
//            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_CHODESH"].Select("KOD_RECHIV=" + iKodRechiv.ToString());
            if ( CountOfRecords( iKodRechiv)== 0 ) // instead of (drChishuvRows.Length == 0)
            {
                if (fErechRechiv > 0)
                {
                    drChishuv = _dtChishuvChodesh.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = _dTaarichChishuv;
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    _dtChishuvChodesh.Rows.Add(drChishuv);
                    drChishuv = null;
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
            _dtChishuvChodesh.Select(null, "KOD_RECHIV");
            drChishuv = _dtChishuvChodesh.Select("KOD_RECHIV=" + iKodRechiv)[0];
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
            drChishuv = null;
        }
        private int CountOfRecords(int iKodRechiv)
        {
            try
            {
                return (from c in objOved._dsChishuv.Tables["CHISHUV_CHODESH"].AsEnumerable()
                        where c.Field<int>("KOD_RECHIV").Equals(iKodRechiv)
                        select c).Count();
            }
            catch (Exception ex)
            {
                throw new Exception("CountOfRecords:" + ex.Message);
            }
        }


    }
}

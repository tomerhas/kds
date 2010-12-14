using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch
{
    class clCalcPeilut : GlobalDataset
    {
        private DataTable _dtChishuvPeilut;
        public DateTime dTaarich;
        private int _iMisparIshi;
        private long _lBakashaId;
        private clCalcGeneral _oGeneralData;

        public clCalcPeilut(int iMisparIshi, long lBakashaId, clCalcGeneral oGeneralData)
        {
            _iMisparIshi = iMisparIshi;
            _lBakashaId = lBakashaId;
            
            _dtChishuvPeilut = clCalcData.DtPeilut;

            _oGeneralData = oGeneralData;

        }


         public void CalcRechiv1(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int  iMakat, iMisparKnisa,iDakotBefoal;
             DateTime dShatHatchla, dShatYetzia;
             float fErech;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);

                 drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3) in (719,720,791,744,785)");
              
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());

                     dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                     iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());
                     fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                    
                     addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);
                 }
                 
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }

         }

         public void CalcRechiv23(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             DataRow drDetailsPeilut;
             DataRow[] drPeiluyot;
             int iHagdaraLegmar, iMakat,iMisparKnisa;
             float fAchuzSikun,fDakotSikun;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);

                 drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1) in(0,1,2,3,4,8,9)");
                 if (drPeiluyot.Length > 0)
                 {
                     for (int J = 0; J < drPeiluyot.Length; J++)
                     {
                         drDetailsPeilut = GetDetailsFromCatalaog(dTaarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));
                         
                         iHagdaraLegmar = 0;
                         if (!String.IsNullOrEmpty(drDetailsPeilut["Mazan_Tashlum"].ToString()) )
                             iHagdaraLegmar = int.Parse(drDetailsPeilut["Mazan_Tashlum"].ToString());

                         if (!String.IsNullOrEmpty(drDetailsPeilut["Migun"].ToString()))
                                 {
                                     switch (int.Parse(drDetailsPeilut["Migun"].ToString()))
                                     {
                                         case 1:
                                             fAchuzSikun =  _oGeneralData.objParameters.fAchuzSikun1;
                                             break;
                                         case 2: fAchuzSikun =  _oGeneralData.objParameters.fAchuzSikun2;
                                             break;
                                         case 3: fAchuzSikun =  _oGeneralData.objParameters.fAchuzSikun3;
                                             break;
                                         case 4: fAchuzSikun =  _oGeneralData.objParameters.fAchuzSikun4;
                                             break;
                                         default: fAchuzSikun = 0; break;
                                     }
                                     fDakotSikun = fAchuzSikun / 100 * iHagdaraLegmar;

                                     if (fDakotSikun > 0)
                                     {
                                         dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                                         dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                                         iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                                         addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fDakotSikun);
                                     }
                                 }
                            
                     }
                 }

                 drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3)=764");
                 if (drPeiluyot.Length > 0)
                 {
                     for (int J = 0; J < drPeiluyot.Length; J++)
                     {
                         iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                         fDakotSikun =  _oGeneralData.objParameters.fAchuzSikunLehamtana / 100 * iMakat;
                         if (fDakotSikun > 0)
                         {
                             dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                             dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                             iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                             addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fDakotSikun);
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotSikun.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
           
         }

         public int CalcElementReka(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             //משך אלמנטים מסוג ריקה
             DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMakat;
             int iErech=0;
            
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);

                 drPeiluyot = dtPeiluyot.Select("nesia_reika is not null");

                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                     iErech += int.Parse(iMakat.ToString().Substring(3, 3));
                    
                 }

                 return iErech;
             }
             catch (Exception ex)
             {
                  throw (ex);
             }

         }

         public float CalcRechiv213(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             float fErech=0;
             DataTable dtPeiluyot;
             int iMisparKnisa=0;
             string sMakat;
            DateTime dShatYetzia = DateTime.MinValue;
             try 
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 for (int J = 0; J < dtPeiluyot.Rows.Count; J++)
                 {
                     iMisparKnisa=int.Parse(dtPeiluyot.Rows[J]["Mispar_Knisa"].ToString());
                     sMakat = dtPeiluyot.Rows[J]["makat_nesia"].ToString();
                     dShatYetzia = DateTime.Parse(dtPeiluyot.Rows[J]["shat_yetzia"].ToString());

                     if ((sMakat.Substring(0, 1) != "5" && sMakat.Substring(0, 1) != "6" && sMakat.Substring(0, 1) != "7") && iMisparKnisa == 0)
                     {
                         fErech+=1;
                     }
                     else if (sMakat.Substring(0,3)=="843" && sMakat.Substring(5,3)=="044")
                     {
                         fErech += int.Parse(sMakat.Substring(2, 2));
                     }
                 }

                 return fErech;
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachNesiot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv214(int iMisparSidur,  DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMakat, iMisparKnisa, iDakotBefoal;
             DateTime dShatHatchla, dShatYetzia;
             DataTable dtMashar;
             string sCarNumbers = "";
             float fErech,fHistaglutMifraki , fHistaglutEilat;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             DataRow drDetailsPeilut;
             int iSugAuto;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 drPeiluyot = dtPeiluyot.Select("not SUBSTRING(makat_nesia,1,1)in(5,7)");
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     sCarNumbers += drPeiluyot[J]["oto_no"].ToString() + ",";
                  }

                 if (sCarNumbers.Length > 0)
                 {
                     sCarNumbers = sCarNumbers.Substring(0, sCarNumbers.Length - 1);

                     dtMashar = clCalcData.GetNetunimMashar(sCarNumbers);

                     for (int J = 0; J < drPeiluyot.Length; J++)
                     {
                         fHistaglutMifraki = 0;
                         fHistaglutEilat = 0;
                         iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                         dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                         dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                         iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                         iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());

                         iSugAuto=0;
                         drDetailsPeilut = GetDetailsFromCatalaog(dTaarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));

                         if (!string.IsNullOrEmpty(drDetailsPeilut["sug_auto"].ToString()))
                                iSugAuto = int.Parse(drDetailsPeilut["sug_auto"].ToString());

                         if ((iSugAuto == 4 || iSugAuto == 5 ) && dtMashar.Select("bus_number=" + drPeiluyot[J]["oto_no"].ToString() + " and SUBSTRING(convert(Vehicle_Type,'System.String'),3,2) in(22,31,37,38,48)").Length > 0)
                         {
                             fHistaglutMifraki = (_oGeneralData.objParameters.fAchuzHistaglutPremyaMifraki / 100) * CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                         }

                         if (int.Parse(drPeiluyot[J]["Kod_shinuy_premia"].ToString()) == 1)
                         { fHistaglutEilat = (float.Parse("20") / 100 )* CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa); }
                         else if (int.Parse(drPeiluyot[J]["Kod_shinuy_premia"].ToString()) == 2)
                         { fHistaglutEilat = (float.Parse("50")/100) * CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa); }

                         fErech = fHistaglutMifraki + fHistaglutEilat;

                         addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);
                         
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotHistaglut.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv215(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             int  iMisparKnisa,iMakat1;
             float  fKm;
             DataRow drDetailsPeilut;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 fKm = 0;
                 for (int J = 0; J < dtPeiluyot.Rows.Count; J++)
                 {
                     iMakat1 = int.Parse(dtPeiluyot.Rows[J]["makat_nesia"].ToString().Substring(0, 1));
                     //ב.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia = 5 (זוהי ויזה) אזי: ק"מ = TB_peilut_Ovdim.Km_visa.
                     fKm = 0;
                     if (iMakat1 == 5)
                     {
                         fKm = float.Parse(dtPeiluyot.Rows[J]["km_visa"].ToString());
                     }
                     //ג.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia = 7 (זהו אלמנט) וגם האלמנט הוא מסוג נסיעה [שליפת מאפיינים (מס' אלמנט, קוד מאפיין = 35, תאריך)] אזי: ק"מ = [חישוב קמ לפי זמן נסיעה (מק"ט פעילות)] 
                     else if (iMakat1 == 7 && dtPeiluyot.Rows[J]["nesia"].ToString() == "1")
                     {
                         fKm = _oGeneralData.CalcKm(long.Parse(dtPeiluyot.Rows[J]["makat_nesia"].ToString()));
                     }
                     //א.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia <> 5 או 7 (זו נסיעה מקטלוג נסיעות) אזי: ק"מ = Km מקטלוג הנסיעות לפי מק"ט הפעילות ותאריך יום העבודה. 
                     else if (iMakat1 != 5 && iMakat1 != 7)
                     {
                         drDetailsPeilut = GetDetailsFromCatalaog(dTaarich, long.Parse(dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString()));

                         if (!string.IsNullOrEmpty(drDetailsPeilut["km"].ToString()))
                         {
                             fKm = float.Parse(drDetailsPeilut["km"].ToString());
                         }
                     }

                     if (fKm > 0)
                     {
                         dShatHatchla = DateTime.Parse(dtPeiluyot.Rows[J]["shat_hatchala_sidur"].ToString());
                         dShatYetzia = DateTime.Parse(dtPeiluyot.Rows[J]["shat_yetzia"].ToString());
                         iMisparKnisa = int.Parse(dtPeiluyot.Rows[J]["mispar_knisa"].ToString());
                         addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fKm);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachKM.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
            
          }


         public void CalcRechiv216(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             int iMisparKnisa, iMakat1;
             float fKm;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 fKm = 0;
                 for (int J = 0; J < dtPeiluyot.Rows.Count; J++)
                 {
                     iMakat1 = int.Parse(dtPeiluyot.Rows[J]["makat_nesia"].ToString().Substring(0, 1));
                     //ב.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia = 5 (זוהי ויזה) אזי: ק"מ = TB_peilut_Ovdim.Km_visa.
                     fKm = 0;
                     if (iMakat1 == 5)
                     {
                         fKm = float.Parse(dtPeiluyot.Rows[J]["km_visa"].ToString());
                     }
                     
                     if (fKm > 0)
                     {
                         dShatHatchla = DateTime.Parse(dtPeiluyot.Rows[J]["shat_hatchala_sidur"].ToString());
                         dShatYetzia = DateTime.Parse(dtPeiluyot.Rows[J]["shat_yetzia"].ToString());
                         iMisparKnisa = int.Parse(dtPeiluyot.Rows[J]["mispar_knisa"].ToString());
                         addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fKm);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv217(int iMisparSidur, DateTime dShatHatchalaSidur, bool bFirstSidur)
         {
             DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMakat, iMakatFirst,iMisparKnisa, iMisparKnisaFirst, iDakotBefoal;
             DateTime dShatHatchla, dShatYetzia;
             float fErech;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             DateTime dShatYetziaFirst = DateTime.MinValue;
             float fHagdaraLetashlum;
             bool bNoCalc = false;
             iMakatFirst = 0;
             iMisparKnisaFirst = 0;
             try
             {
                 
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 if (bFirstSidur)
                 {
                     drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3)<>701", "shat_yetzia asc");
                     if (drPeiluyot.Length > 0)
                     {
                         iMakatFirst = int.Parse(drPeiluyot[0]["MAKAT_NESIA"].ToString());
                         dShatYetziaFirst = DateTime.Parse(drPeiluyot[0]["shat_yetzia"].ToString());
                         iMisparKnisaFirst = int.Parse(drPeiluyot[0]["mispar_knisa"].ToString());
                     }
                 }

                 drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1)<>5");
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     if (drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) != "7" || ((drPeiluyot[J]["kod_lechishuv_premia"].ToString().Trim() == "3:1" || drPeiluyot[J]["kod_lechishuv_premia"].ToString().Trim() == "3:0") && drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "7"))
                     {
                         bNoCalc = false;
                         iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                         dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                         dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                         iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                         iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());
                         fHagdaraLetashlum = CalcHagdaraLetashlumPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);

                         fErech = _oGeneralData.objParameters.fMekademTosefetZmanLefiRechev * fHagdaraLetashlum;
                        
                         if (drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "6" || drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 3) == "791")
                         {
                             if (iMakatFirst == iMakat && dShatYetziaFirst == dShatYetzia && iMisparKnisaFirst == iMisparKnisa)
                             {
                                 if (fHagdaraLetashlum < 7 && dShatYetzia < clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date))
                                     bNoCalc = true;
                             }
                         }

                         if (!bNoCalc)
                            addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotHagdara.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv218(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
              int iMakat, iMisparKnisa, iKisuyTor;
             DateTime dShatHatchla, dShatYetzia;
             float fErech;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 for (int J = 0; J < dtPeiluyot.Rows.Count; J++)
                 {
                     iMakat = int.Parse(dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString());
                     dShatHatchla = DateTime.Parse(dtPeiluyot.Rows[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(dtPeiluyot.Rows[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(dtPeiluyot.Rows[J]["mispar_knisa"].ToString());
                     iKisuyTor = 0;
                     if (!string.IsNullOrEmpty(dtPeiluyot.Rows[J]["KISUY_TOR"].ToString()))
                        iKisuyTor = int.Parse(dtPeiluyot.Rows[J]["KISUY_TOR"].ToString());

                     fErech = iKisuyTor;

                     addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv267(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             int iMakat, iMisparKnisa, iDakotBefoal;
             float fErech;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);

                 for (int J = 0; J < dtPeiluyot.Rows.Count; J++)
                 {
                     iMakat = int.Parse(dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString());
                     dShatHatchla = DateTime.Parse(dtPeiluyot.Rows[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(dtPeiluyot.Rows[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(dtPeiluyot.Rows[J]["mispar_knisa"].ToString());
                     iDakotBefoal = int.Parse(dtPeiluyot.Rows[J]["Dakot_bafoal"].ToString());

                     fErech = 0;
                     if ((dtPeiluyot.Rows[J]["kod_lechishuv_premia"].ToString().Trim() == "1:1" && dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "7"))
                     {
                         fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                     }

                     if (J == 1)
                     {//א.	כאשר הפעילות השנייה בסידור הינה נסיעה ריקה 
                         if ( (dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "7" && dtPeiluyot.Rows[J]["kupai"].ToString() == "1" && dtPeiluyot.Rows[J - 1]["MAKAT_NESIA"].ToString().Substring(0, 3) == "701"))
                         {
                             if (DateTime.Parse(dtPeiluyot.Rows[J]["SHAT_YETZIA"].ToString()) <= clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date) || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < _oGeneralData.objParameters.iMaxZmanRekaAdShmone)
                                 {
                                     fErech = 0;
                                 }
                             }
                             else if (DateTime.Parse(dtPeiluyot.Rows[J]["SHAT_YETZIA"].ToString()) > clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date) || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < _oGeneralData.objParameters.iMaxZmanRekaAchreyShmone)
                                 {
                                     fErech = 0;
                                 }

                             }
                         }
                     }
                    
                     addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotElementim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }

         }

         public void CalcRechiv268(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataTable dtPeiluyot;
             int iMakat, iMisparKnisa, iDakotBefoal;
             float fErech;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);

                 for (int J = 0; J < dtPeiluyot.Rows.Count; J++)
                 {
                     iMakat = int.Parse(dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString());
                     dShatHatchla = DateTime.Parse(dtPeiluyot.Rows[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(dtPeiluyot.Rows[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(dtPeiluyot.Rows[J]["mispar_knisa"].ToString());
                     iDakotBefoal = int.Parse(dtPeiluyot.Rows[J]["Dakot_bafoal"].ToString());

                     fErech = 0;
                     if (dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(0, 1) != "5" || dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(0, 1) != "7")
                     {
                         fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                     }

                     if (J == 1)
                     {//א.	כאשר הפעילות השנייה בסידור הינה נסיעה ריקה 
                         if (dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "6"  && dtPeiluyot.Rows[J - 1]["MAKAT_NESIA"].ToString().Substring(0, 3) == "701")
                         {
                             if (DateTime.Parse(dtPeiluyot.Rows[J]["SHAT_YETZIA"].ToString()) <= clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date) || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < _oGeneralData.objParameters.iMaxZmanRekaAdShmone)
                                 {
                                     fErech = 0;
                                 }
                             }
                             else if (DateTime.Parse(dtPeiluyot.Rows[J]["SHAT_YETZIA"].ToString()) > clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date) || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < _oGeneralData.objParameters.iMaxZmanRekaAchreyShmone)
                                 {
                                     fErech = 0;
                                 }

                             }
                         }
                     }
                     if (dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "8" && dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString().Substring(6, 2) == "41")
                     {
                         fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, dtPeiluyot.Rows[J]["MAKAT_NESIA"].ToString(), int.Parse(dtPeiluyot.Rows[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) * _oGeneralData.objParameters.fMekademTosefetZmanLefiRechev;
                     }


                     addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }

         }

         public DataRow GetDetailsFromCatalaog(DateTime dSidurDate, long lMakatNesia)
         {
             try
             {
                 DataRow[] drNetunim;
               
                 //פרטי נסיעה מהתנועה
                 //DataTable dsSidurim = new DataTable();
                 //clKavim oKavim = new clKavim();
                 //int iMakatStart;

                 //iMakatStart = int.Parse(lMakatNesia.ToString().Substring(0, 1));
                 //if (iMakatStart == 6)
                 //{
                 //    dsSidurim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dSidurDate, out iResult);
                 //}
                 //else if (iMakatStart == 8 || iMakatStart == 9)
                 //{
                 //    dsSidurim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dSidurDate, out iResult);
                 //}
                 //else
                 //{
                 //    dsSidurim = oKavim.GetKavimDetailsFromTnuaDT(lMakatNesia, dSidurDate, out iResult);
                 //}
                 drNetunim = clCalcData.DtPeiluyotFromTnua.Select("MAKAT8=" + lMakatNesia + " AND ACTIVITY_DATE=Convert('" + dSidurDate.ToShortDateString() + "', 'System.DateTime')");
                 
                 return  drNetunim[0];
               
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", 0,dTaarich, "CalcPeilut: " + ex.Message);
                 throw (ex);
             }
         }

        public DataTable GetPeiluyLesidur(int iMisparSidur,DateTime dShatHatchalaSidur)
         {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר פעילויות לסידור:  
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, _iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala_sidur", ParameterType.ntOracleDate, dShatHatchalaSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetPeilutLesidur, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         private float CalcHagdaraLetichnunPeilut(int iDakotBefoal,string sMakat,int iElement14,int iMisparKnisa)
         {
             float fHagdara=0;
             DataRow drDetailsPeilut;
            
             try
             {
                 if (iDakotBefoal > 0)
                 {
                     fHagdara = iDakotBefoal;
                 }
                 else
                 {
                     if (sMakat.Substring(0, 1) != "5" && sMakat.Substring(0, 1) != "7" && iMisparKnisa==0)
                     {
                         drDetailsPeilut = GetDetailsFromCatalaog(dTaarich, long.Parse(sMakat));
                         if (drDetailsPeilut["Mazan_Tichnun"].ToString().Length > 0)
                         {
                             fHagdara = int.Parse(drDetailsPeilut["Mazan_Tichnun"].ToString());
                         }

                     }
                     else if ((sMakat.Substring(0, 1) == "7") && iElement14 == 5)
                     {
                         fHagdara = float.Parse(sMakat.Substring(3, 3));
                     }
                 }
                 return fHagdara;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         private float CalcHagdaraLetashlumPeilut(int iDakotBefoal, string sMakat, int iElement14, int iMisparKnisa)
         {
             float fHagdara = 0;
             DataRow drDetailsPeilut;

             try
             {
                 if (iDakotBefoal > 0)
                 {
                     fHagdara = iDakotBefoal;
                 }
                 else
                 {
                     if (sMakat.Substring(0, 1) != "5" && sMakat.Substring(0, 1) != "7" && iMisparKnisa == 0)
                     {
                         drDetailsPeilut = GetDetailsFromCatalaog(dTaarich, long.Parse(sMakat));
                         if (drDetailsPeilut["Mazan_Tashlum"].ToString().Length > 0)
                         {
                             fHagdara = int.Parse(drDetailsPeilut["Mazan_Tashlum"].ToString());
                         }

                     }
                     else if ((sMakat.Substring(0, 1) == "7") && iElement14 == 5)
                     {
                         fHagdara = float.Parse(sMakat.Substring(3, 3));
                     }
                 }
                 return fHagdara;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void CalcZmaneyAruchot(DataTable dtPeiluyot,int iMakatLerechiv, DateTime dShatHatchalaLetaslum, DateTime dShatGmarLetashlum, out float fZmanAruchatBoker, out float fZmanAruchatTzharim, out float fZmanAruchatErev)
        { 
            float fTempY = 0;
            DateTime dTempM1, dTempM2,dShatHatchla,dShatYetzia;
            DataRow[] drPeiluyot;
             int iMakat,iZmanElement;
            try
            {
                fZmanAruchatBoker=0;
                fZmanAruchatTzharim = 0;
                fZmanAruchatErev = 0;
                drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3)='" + iMakatLerechiv+ "'","");
              for (int i = 0; i < drPeiluyot.Length; i++)
              {
                  iMakat = int.Parse(drPeiluyot[i]["MAKAT_NESIA"].ToString());
                  dShatHatchla = DateTime.Parse(drPeiluyot[i]["shat_hatchala_sidur"].ToString());
                  dShatYetzia = DateTime.Parse(drPeiluyot[i]["shat_yetzia"].ToString());
                  iZmanElement = int.Parse(iMakat.ToString().PadLeft(8).Substring(4, 3));
                  
                  dTempM1 = clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date);
                  dTempM2 = clGeneral.GetDateTimeFromStringHour("09:30", dTaarich.Date);
                  fTempY = 0;

                  if (dShatYetzia <= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM1)
                  { fTempY = float.Parse((dShatYetzia.AddMinutes(iZmanElement) - dTempM1).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia <= dTempM2 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM2)
                  { fTempY = float.Parse((dTempM2 - dShatYetzia).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) <= dTempM2)
                  { fTempY = iZmanElement; }
                  fZmanAruchatBoker += fTempY;


                  dTempM1 = _oGeneralData.objParameters.dStartAruchatTzaharayim;
                  dTempM2 = _oGeneralData.objParameters.dEndAruchatTzaharayim;
                  fTempY = 0;

                  if (dShatYetzia <= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM1)
                  { fTempY = float.Parse((dShatYetzia.AddMinutes(iZmanElement) - dTempM1).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia <= dTempM2 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM2)
                  { fTempY = float.Parse((dTempM2 - dShatYetzia).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) <= dTempM2)
                  { fTempY = iZmanElement; }
                  fZmanAruchatTzharim += fTempY;


                  dTempM1 = clGeneral.GetDateTimeFromStringHour("18:00", dTaarich.Date);
                  dTempM2 = clGeneral.GetDateTimeFromStringHour("19:30", dTaarich.Date);
                  fTempY = 0;
                  if (dShatYetzia <= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM1)
                  { fTempY = float.Parse((dShatYetzia.AddMinutes(iZmanElement) - dTempM1).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia <= dTempM2 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM2)
                  { fTempY = float.Parse((dTempM2 - dShatYetzia).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) <= dTempM2)
                  { fTempY = iZmanElement; }
                  fZmanAruchatErev += fTempY;

              }

              if (fZmanAruchatBoker > 20)
              { fZmanAruchatBoker = 20; }
              if (fZmanAruchatTzharim > 30)
              { fZmanAruchatTzharim = 30; }
              if (fZmanAruchatErev > 20)
              { fZmanAruchatErev = 20; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void addRowToTable(int iKodRechiv, DateTime dShatHatchala, DateTime dShatYetzia, int iMisparSidur,int iMisparKnisa, float fErechRechiv)
        {
            DataRow drChishuv;
            drChishuv = _dtChishuvPeilut.NewRow();
            drChishuv["BAKASHA_ID"] = _lBakashaId;
            drChishuv["MISPAR_ISHI"] = _iMisparIshi;
            drChishuv["TAARICH"] = dTaarich;
            drChishuv["MISPAR_SIDUR"] = iMisparSidur;
            drChishuv["SHAT_HATCHALA"] = dShatHatchala;
            drChishuv["SHAT_YETZIA"] = dShatYetzia;
            drChishuv["MISPAR_KNISA"] = iMisparKnisa;
            drChishuv["KOD_RECHIV"] = iKodRechiv;
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
            _dtChishuvPeilut.Rows.Add(drChishuv);
        }
    }
}

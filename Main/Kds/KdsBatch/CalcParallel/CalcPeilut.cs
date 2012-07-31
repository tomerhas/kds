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
    class CalcPeilut
    {
        private Oved objOved;
        private DataTable _dtChishuvPeilut;
       // public DateTime objOved.Taarich { get; set; }
       // public int iSugYom { get; set; }
        public CalcPeilut(Oved oOved)
        {
            objOved = oOved;
            _dtChishuvPeilut = objOved._dsChishuv.Tables["CHISHUV_PEILUT"];// objOved._DtPeilut;    
        }
        //public void SetNetunim(int SugYom,DateTime Taarich)
        //{
        // //   objOved.Taarich = Taarich;
        // //   iSugYom = SugYom;
        //}

        public float getZmanHafsakaBesidur(int iMisparSidur, DateTime dShatHatchalaSidur)
        {
            float fZmanHafsaka = 0;
            DataRow[] drPeiluyot;
            int iMakat;
            string sQury;
            try
            {
                sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime')";
                sQury += " and (SUBSTRING(makat_nesia,1,3)='790')";

                drPeiluyot = objOved.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");
              //  drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, " (SUBSTRING(makat_nesia,1,3)='790')");
                for (int J = 0; J < drPeiluyot.Length; J++)
                {
                    iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());

                    fZmanHafsaka += int.Parse(iMakat.ToString().Substring(3, 3));
                }
                return fZmanHafsaka;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public float getZmanHamtanaEilat(int iMisparSidur, DateTime dShatHatchalaSidur)
        {
            float fZmanHamtana = 0;
            DataRow[] drPeiluyot;
            int iMakat;
            string sQury;
            try
            {
                sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime')";
                sQury += " and (SUBSTRING(makat_nesia,1,3)='735')";

                drPeiluyot = objOved.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");
                for (int J = 0; J < drPeiluyot.Length; J++)
                {
                    iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());

                    fZmanHamtana += int.Parse(iMakat.ToString().Substring(3, 3));
                }
                return fZmanHamtana;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
         public void CalcRechiv1(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataRow[] drPeiluyot;
             int  iMakat, iMisparKnisa,iDakotBefoal;
             DateTime dShatHatchla, dShatYetzia;
             float fErech;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3) in (719,720,791,744,785))");
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());

                     dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                     iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());
                     fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);

                     addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);
                 }

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich , iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }

         }

         public bool CheckHafasakaLast(int iMisparSidur, DateTime dShatHatchalaSidur, ref DateTime shatHatchalaHafsakaLast)
         {
             try
             {
                 DataRow[] drPeiluyot;
                 int iMakat;
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");
                 if (drPeiluyot.Length > 0)
                 {
                     iMakat = int.Parse(drPeiluyot[drPeiluyot.Length - 1]["MAKAT_NESIA"].ToString());
                     if (iMakat.ToString().Substring(0, 3) == "790")
                     {
                         shatHatchalaHafsakaLast = DateTime.Parse(drPeiluyot[drPeiluyot.Length - 1]["shat_yetzia"].ToString());
                         return true;
                     }
                 }
                 return false;
             }
             catch (Exception ex)
             {
                 throw (ex);
             }
         }
         private DataRow[] getPeiluyot(int iMisparSidur, DateTime dShatHatchalaSidur,string sCondition)
         {
             DataRow[] drPeiluyot;
             string sQury;
             try
             {
                 sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime')";
                 if (sCondition != "")
                     sQury += " and " + sCondition;
                 drPeiluyot = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");
                 return drPeiluyot;
             }
             catch (Exception ex)
             {
               //  clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
         }
     
        public void CalcRechiv23(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
           //  DataTable dtPeiluyot;
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
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "mispar_knisa=0 and(SUBSTRING(makat_nesia,1,1) in(0,1,2,3,4,8,9))");
                
                 if (drPeiluyot.Length > 0)
                 {
                     for (int J = 0; J < drPeiluyot.Length; J++)
                     {
                         drDetailsPeilut = GetDetailsFromCatalaog(objOved.Taarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));
                         
                         iHagdaraLegmar = 0;
                         if (!String.IsNullOrEmpty(drDetailsPeilut["Mazan_Tashlum"].ToString()) )
                             iHagdaraLegmar = int.Parse(drDetailsPeilut["Mazan_Tashlum"].ToString());

                         if (!String.IsNullOrEmpty(drDetailsPeilut["Migun"].ToString()))
                                 {
                                     switch (int.Parse(drDetailsPeilut["Migun"].ToString()))
                                     {
                                         case 1:
                                             fAchuzSikun =  objOved.objParameters.fAchuzSikun1;
                                             break;
                                         case 2: fAchuzSikun =  objOved.objParameters.fAchuzSikun2;
                                             break;
                                         case 3: fAchuzSikun =   objOved.objParameters.fAchuzSikun3;
                                             break;
                                         case 4: fAchuzSikun =  objOved.objParameters.fAchuzSikun4;
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
                 drPeiluyot = null;
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)=764)");
                 if (drPeiluyot.Length > 0)
                 {
                     for (int J = 0; J < drPeiluyot.Length; J++)
                     {
                         iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                         fDakotSikun =   objOved.objParameters.fAchuzSikunLehamtana / 100 * iMakat;
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
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotSikun.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }
           
         }

         public int CalcElementReka(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             //משך אלמנטים מסוג ריקה
            // DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMakat;
             int iErech=0;
            
             try
             {
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(nesia_reika is not null)");
                
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
             finally
             {
                 drPeiluyot = null;
             }
         }

         public float CalcRechiv213(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             float fErech=0;
            // DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMisparKnisa=0;
             string sMakat, sQury;
            DateTime dShatYetzia = DateTime.MinValue;

            clKavim _Kavim = new clKavim();
            int iMakatType; //= _Kavim.GetMakatType(lMakatNesia);
            clKavim.enMakatType oMakatType;

            try
            {
                // dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");

                for (int J = 0; J < drPeiluyot.Length; J++)
                {
                    iMisparKnisa = int.Parse(drPeiluyot[J]["Mispar_Knisa"].ToString());
                    sMakat = drPeiluyot[J]["makat_nesia"].ToString();
                    dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                    iMakatType = _Kavim.GetMakatType(int.Parse(sMakat));
                    oMakatType = (clKavim.enMakatType)iMakatType;

                    if ((oMakatType == clKavim.enMakatType.mNamak && !(sMakat.Substring(0, 1) == "8" && sMakat.Substring(6, 2) == "41")) || (oMakatType == clKavim.enMakatType.mKavShirut && iMisparKnisa == 0))
                    {
                        fErech += 1;
                    }
                    else if (sMakat.Substring(0, 3) == "843" && sMakat.Substring(5, 3) == "044")
                    {
                        fErech += int.Parse(sMakat.Substring(2, 2));
                    }
                }

                return fErech;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachNesiot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                _Kavim = null;
                drPeiluyot = null;
            }
         }

         public void CalcRechiv214(int iMisparSidur,  DateTime dShatHatchalaSidur)
         {
            // DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMakat, iMisparKnisa, iDakotBefoal;
             DateTime dShatHatchla, dShatYetzia,dMeTaarich;
             clKavim oKavim = new clKavim();
             float fErech,fHistaglutMifraki , fHistaglutEilat;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             DataRow drDetailsPeilut;
             string sCarNumbers = "" ;
             int iSugAuto;
             int J = 0;
             bool SidurMifraki = false;
             try
             {
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur,"");
                  
                 for (J = 0; J < drPeiluyot.Length; J++)
                 {
                     sCarNumbers += drPeiluyot[J]["oto_no"].ToString() + ",";
                 }
                 
                 if (sCarNumbers.Length > 0)
                 {
                     if (objOved.oGeneralData.dtBusNumbersAll == null)
                     {
                         dMeTaarich = DateTime.Parse("01/" + objOved.Taarich.Month + "/" + objOved.Taarich.Year);
                         objOved.oGeneralData.dtBusNumbersAll = oKavim.GetBusesDetailsLeOvedForMonth(dMeTaarich, dMeTaarich.AddMonths(1).AddDays(-1), objOved.Mispar_ishi);
                     }
                     if (objOved.oGeneralData.dtBusNumbersAll.Rows.Count > 0)
                     {
                         J = 0;
                          while (!SidurMifraki && J < drPeiluyot.Length)
                          {
                              iSugAuto = 0;
                              drDetailsPeilut = GetDetailsFromCatalaog(objOved.Taarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));
                              if (!string.IsNullOrEmpty(drDetailsPeilut["sug_auto"].ToString()))
                                 iSugAuto = int.Parse(drDetailsPeilut["sug_auto"].ToString());

                              if ((iSugAuto == 4 || iSugAuto == 5))
                                  SidurMifraki = true;
                              J++;
                          }
                          if (SidurMifraki)
                          {
                              for (J = 0; J < drPeiluyot.Length; J++)
                              {
                                  fHistaglutMifraki = 0;
                                  fHistaglutEilat = 0;
                                  iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                                  dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                                  dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                                  iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                                  iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());

                                  if (objOved.oGeneralData.dtBusNumbersAll.Select("bus_number=" + drPeiluyot[J]["oto_no"].ToString() + " and SUBSTRING(convert(Vehicle_Type,'System.String'),3,2) in(61,22,31,37,38,48)").Length > 0)
                                  {
                                      fHistaglutMifraki = (objOved.objParameters.fAchuzHistaglutPremyaMifraki / 100) * CalcHagdaraLetashlumPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                                  }

                                  if (int.Parse(drPeiluyot[J]["Kod_shinuy_premia"].ToString()) == 2)
                                  { fHistaglutEilat = (float.Parse("20") / 100) * CalcHagdaraLetashlumPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa); }
                                  else if (int.Parse(drPeiluyot[J]["Kod_shinuy_premia"].ToString()) == 3)
                                  { fHistaglutEilat = (float.Parse("10") / 100) * CalcHagdaraLetashlumPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa); }

                                  fErech = fHistaglutMifraki + fHistaglutEilat;

                                  addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                              }
                          }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotHistaglut.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 oKavim = null;
                 drPeiluyot = null;
             }
         }

         public void CalcRechiv215(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
           //  DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int  iMisparKnisa,iMakat1;
             float  fKm;
             DataRow drDetailsPeilut;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             string sQury = "";
             clCalcBL oCalcBL;
             try
             {
                 oCalcBL = new clCalcBL();
                // dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");
               
                 fKm = 0;
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat1 = int.Parse(drPeiluyot[J]["makat_nesia"].ToString().Substring(0, 1));
                     //ב.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia = 5 (זוהי ויזה) אזי: ק"מ = TB_peilut_Ovdim.Km_visa.
                     fKm = 0;
                     if (iMakat1 == 5)
                     {
                         fKm = float.Parse(drPeiluyot[J]["km_visa"].ToString());
                     }
                     //ג.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia = 7 (זהו אלמנט) וגם האלמנט הוא מסוג נסיעה [שליפת מאפיינים (מס' אלמנט, קוד מאפיין = 35, תאריך)] אזי: ק"מ = [חישוב קמ לפי זמן נסיעה (מק"ט פעילות)] 
                     else if (iMakat1 == 7 && drPeiluyot[J]["nesia"].ToString() == "1")
                     {
                         fKm = oCalcBL.CalcKm(long.Parse(drPeiluyot[J]["makat_nesia"].ToString()), objOved.objParameters.fGoremChishuvKm);
                     }
                     //א.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia <> 5 או 7 (זו נסיעה מקטלוג נסיעות) אזי: ק"מ = Km מקטלוג הנסיעות לפי מק"ט הפעילות ותאריך יום העבודה. 
                     else if (iMakat1 != 5 && iMakat1 != 7)
                     {
                         drDetailsPeilut = GetDetailsFromCatalaog(objOved.Taarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));

                         if (!string.IsNullOrEmpty(drDetailsPeilut["km"].ToString()))
                         {
                             fKm = float.Parse(drDetailsPeilut["km"].ToString());
                         }
                     }

                     if (fKm > 0)
                     {
                         dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                         dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                         iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                         addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fKm);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachKM.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 oCalcBL = null;
                 drPeiluyot = null;
             }
          }


         public void CalcRechiv216(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
           //  DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMisparKnisa, iMakat1;
             float fKm;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
       
             try
             {
                 //dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");
                 fKm = 0;
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat1 = int.Parse(drPeiluyot[J]["makat_nesia"].ToString().Substring(0, 1));
                     //ב.	אם הספרה הראשונה של מק"ט הפעילות TB_peilut_Ovdim.Makat_nesia = 5 (זוהי ויזה) אזי: ק"מ = TB_peilut_Ovdim.Km_visa.
                     fKm = 0;
                     if (iMakat1 == 5)
                     {
                         fKm = float.Parse(drPeiluyot[J]["km_visa"].ToString());
                     }
                     
                     if (fKm > 0)
                     {
                         dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                         dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                         iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                         addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fKm);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }
         }

         public void CalcRechiv217(int iMisparSidur, DateTime dShatHatchalaSidur, bool bFirstSidur)
         {
            // DataTable dtPeiluyot;
             DataRow[] drPeiluyot;
             int iMakat, iMakatFirst,iMisparKnisa, iMisparKnisaFirst, iDakotBefoal;
             DateTime dShatHatchla, dShatYetzia;
             float fErech;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             DateTime dShatYetziaFirst = DateTime.MinValue;
             float fHagdaraLetashlum;
             string sQury;
             bool bNoCalc = false;
             iMakatFirst = 0;
             iMisparKnisaFirst = 0;
             try
             {
                 
                 if (bFirstSidur)
                 {
                     drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>701)");
                 //   drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3)<>701", "shat_yetzia asc");
 
                     if (drPeiluyot.Length > 0)
                     {
                         iMakatFirst = int.Parse(drPeiluyot[0]["MAKAT_NESIA"].ToString());
                         dShatYetziaFirst = DateTime.Parse(drPeiluyot[0]["shat_yetzia"].ToString());
                         iMisparKnisaFirst = int.Parse(drPeiluyot[0]["mispar_knisa"].ToString());
                     }
                 }

               //  drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1)<>5");
                 drPeiluyot = null;
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,1)<>5)");
                   
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

                         fErech =  objOved.objParameters.fMekademTosefetZmanLefiRechev * fHagdaraLetashlum;
                        
                         if (drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "6" || drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 3) == "791")
                         {
                             if (iMakatFirst == iMakat && dShatYetziaFirst == dShatYetzia && iMisparKnisaFirst == iMisparKnisa)
                             {
                                 if (fHagdaraLetashlum < 7 && dShatYetzia < clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date))
                                     bNoCalc = true;
                             }
                         }
                         if (drPeiluyot[J]["MAKAT_NESIA"].ToString().PadLeft(8, '0').Substring(0, 1) == "8" && drPeiluyot[J]["MAKAT_NESIA"].ToString().PadLeft(8, '0').Substring(6, 2) == "41")
                             bNoCalc = true;
                         
                         if (!bNoCalc)
                            addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotHagdara.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }
         }

         public void CalcRechiv218(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
          //   DataTable dtPeiluyot;
              int iMakat, iMisparKnisa, iKisuyTor;
             DateTime dShatHatchla, dShatYetzia;
             float fErech;
          
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             DataRow[] drPeiluyot;
             try
             {
                // dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);

                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                     dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                     iKisuyTor = 0;
                     if (!string.IsNullOrEmpty(drPeiluyot[J]["KISUY_TOR"].ToString()))
                         iKisuyTor = int.Parse(drPeiluyot[J]["KISUY_TOR"].ToString());

                     fErech = iKisuyTor;

                     addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotKisuiTor.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }
         }

         public void CalcRechiv235(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataRow[] drPeiluyot;
             float fErech;
             int iMakat, iMisparKnisa;
             DateTime dShatYetzia = DateTime.MinValue;
             iMisparKnisa = 0;
             try
             {
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur," (SUBSTRING(makat_nesia,1,3)='737')");

                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                     dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                     
                     fErech = int.Parse(iMakat.ToString().Substring(3, 3));
                     addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), dShatHatchalaSidur, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }
         }
         public void CalcRechiv267(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
            // DataTable dtPeiluyot;
            
             DataRow[] drPeiluyot;
             int iMisparKnisa, iDakotBefoal;
             clKavim.enMakatType oMakatType;
             clKavim _Kavim = new clKavim();
             float fErech;
             string sMakat;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
             try
             {
                // dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");
                
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                    // iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                     dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                     iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());
                     
                     sMakat = drPeiluyot[J]["MAKAT_NESIA"].ToString();
                     oMakatType = (clKavim.enMakatType)(_Kavim.GetMakatType(int.Parse(sMakat)));

                     fErech = 0;
                     if ((drPeiluyot[J]["kod_lechishuv_premia"].ToString().Trim() == "1:1" && sMakat.Substring(0, 1) == "7")
                         || (oMakatType  == clKavim.enMakatType.mNamak && sMakat.Substring(0, 1) == "8" && sMakat.Substring(6, 2) == "41"))
                     {
                         fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                     }

                     if (J == 1)
                     {//א.	כאשר הפעילות השנייה בסידור הינה נסיעה ריקה 
                         if ((drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "7" && drPeiluyot[J]["kupai"].ToString() == "1" && drPeiluyot[J - 1]["MAKAT_NESIA"].ToString().Substring(0, 3) == "701"))
                         {
                             if (DateTime.Parse(drPeiluyot[J]["SHAT_YETZIA"].ToString()) <= clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date) || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < objOved.objParameters.iMaxZmanRekaAdShmone)
                                 {
                                     fErech = 0;
                                 }
                             }
                             else if (DateTime.Parse(drPeiluyot[J]["SHAT_YETZIA"].ToString()) > clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date) || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < objOved.objParameters.iMaxZmanRekaAchreyShmone)
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
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotElementim.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
             }
         }

         public void CalcRechiv268(int iMisparSidur, DateTime dShatHatchalaSidur)
         {
             DataRow[] drPeiluyot;
          //   DataTable dtPeiluyot;
             int iMakat, iMisparKnisa, iDakotBefoal;
             float fErech;
             DateTime dShatHatchla, dShatYetzia;
             iMisparKnisa = 0;
             dShatHatchla = DateTime.MinValue;
             dShatYetzia = DateTime.MinValue;
          
             try
             {
                // dtPeiluyot = GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                 drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "");
                 for (int J = 0; J < drPeiluyot.Length; J++)
                 {
                     iMakat = int.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString());
                     dShatHatchla = DateTime.Parse(drPeiluyot[J]["shat_hatchala_sidur"].ToString());
                     dShatYetzia = DateTime.Parse(drPeiluyot[J]["shat_yetzia"].ToString());
                     iMisparKnisa = int.Parse(drPeiluyot[J]["mispar_knisa"].ToString());
                     iDakotBefoal = int.Parse(drPeiluyot[J]["Dakot_bafoal"].ToString());

                     fErech = 0;
                     if (drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) != "5" || drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) != "7")
                     {
                         fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa);
                     }

                     if (J == 1)
                     {//א.	כאשר הפעילות השנייה בסידור הינה נסיעה ריקה 
                         if (drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "6" && drPeiluyot[J - 1]["MAKAT_NESIA"].ToString().Substring(0, 3) == "701")
                         {
                             if (DateTime.Parse(drPeiluyot[J]["SHAT_YETZIA"].ToString()) <= clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date) || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < objOved.objParameters.iMaxZmanRekaAdShmone)
                                 {
                                     fErech = 0;
                                 }
                             }
                             else if (DateTime.Parse(drPeiluyot[J]["SHAT_YETZIA"].ToString()) > clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date) || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                             {
                                 if (CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) < objOved.objParameters.iMaxZmanRekaAchreyShmone)
                                 {
                                     fErech = 0;
                                 }

                             }
                         }
                     }
                     if (drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(0, 1) == "8" && drPeiluyot[J]["MAKAT_NESIA"].ToString().Substring(6, 2) == "41")
                     {
                         fErech = CalcHagdaraLetichnunPeilut(iDakotBefoal, drPeiluyot[J]["MAKAT_NESIA"].ToString(), int.Parse(drPeiluyot[J]["sector_zvira_zman_haelement"].ToString()), iMisparKnisa) * objOved.objParameters.fMekademTosefetZmanLefiRechev;
                     }


                     addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), dShatHatchla, dShatYetzia, iMisparSidur, iMisparKnisa, fErech);

                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.Message, null);
                 throw (ex);
             }
             finally
             {
                 drPeiluyot = null;
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
                 drNetunim = objOved.DtPeiluyotTnuaYomi.Select("MAKAT8=" + lMakatNesia);
                 
                 return  drNetunim[0];
               
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(objOved.iBakashaId,  objOved.Mispar_ishi, "E", 0,objOved.Taarich, "CalcPeilut: " + ex.Message);
                 throw (ex);
             }
         }

        public DataTable GetPeiluyLesidur(int iMisparSidur,DateTime dShatHatchalaSidur)
         {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר פעילויות לסידור:  
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,  objOved.Mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, objOved.Taarich, ParameterDir.pdInput);
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
                         drDetailsPeilut = GetDetailsFromCatalaog(objOved.Taarich, long.Parse(sMakat));
                         if (drDetailsPeilut["MAZAN_TICHNUN"].ToString().Length > 0)
                         {
                             fHagdara = int.Parse(drDetailsPeilut["MAZAN_TICHNUN"].ToString());
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
                         drDetailsPeilut = GetDetailsFromCatalaog(objOved.Taarich, long.Parse(sMakat));
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

         public void CalcZmaneyAruchot(DataRow[] drPeiluyot, DateTime dShatHatchalaLetaslum, DateTime dShatGmarLetashlum, out float fZmanAruchatTzharim, int iMaxDakot)
        { 
            float fTempY = 0;
            DateTime dTempM1, dTempM2,dShatHatchla,dShatYetzia;
          //  DataRow[] drPeiluyot;
             int iMakat,iZmanElement;
            try
            {
                fZmanAruchatTzharim = 0;
               // drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3)='" + iMakatLerechiv+ "'","");
              for (int i = 0; i < drPeiluyot.Length; i++)
              {
                  iMakat = int.Parse(drPeiluyot[i]["MAKAT_NESIA"].ToString());
                  dShatHatchla = DateTime.Parse(drPeiluyot[i]["shat_hatchala_sidur"].ToString());
                  dShatYetzia = DateTime.Parse(drPeiluyot[i]["shat_yetzia"].ToString());
                  iZmanElement = int.Parse(iMakat.ToString().PadLeft(8).Substring(3, 3));
                  
                  //dTempM1 = clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date);
                  //dTempM2 = clGeneral.GetDateTimeFromStringHour("09:30", objOved.Taarich.Date);
                  //fTempY = 0;

                  //if (dShatYetzia <= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM1)
                  //{ fTempY = float.Parse((dShatYetzia.AddMinutes(iZmanElement) - dTempM1).TotalMinutes.ToString()); }
                  //else if (dShatYetzia >= dTempM1 && dShatYetzia <= dTempM2 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM2)
                  //{ fTempY = float.Parse((dTempM2 - dShatYetzia).TotalMinutes.ToString()); }
                  //else if (dShatYetzia >= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) <= dTempM2)
                  //{ fTempY = iZmanElement; }
                  //fZmanAruchatBoker += fTempY;


                  dTempM1 = objOved.objParameters.dStartAruchatTzaharayim;
                  dTempM2 = objOved.objParameters.dEndAruchatTzaharayim;
                  fTempY = 0;

                  if (dShatYetzia <= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM1)
                  { fTempY = float.Parse((dShatYetzia.AddMinutes(iZmanElement) - dTempM1).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia <= dTempM2 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM2)
                  { fTempY = float.Parse((dTempM2 - dShatYetzia).TotalMinutes.ToString()); }
                  else if (dShatYetzia >= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) <= dTempM2)
                  { fTempY = iZmanElement; }
                  fZmanAruchatTzharim += fTempY;


                  //dTempM1 = clGeneral.GetDateTimeFromStringHour("18:00", objOved.Taarich.Date);
                  //dTempM2 = clGeneral.GetDateTimeFromStringHour("19:30", objOved.Taarich.Date);
                  //fTempY = 0;
                  //if (dShatYetzia <= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM1)
                  //{ fTempY = float.Parse((dShatYetzia.AddMinutes(iZmanElement) - dTempM1).TotalMinutes.ToString()); }
                  //else if (dShatYetzia >= dTempM1 && dShatYetzia <= dTempM2 && dShatYetzia.AddMinutes(iZmanElement) >= dTempM2)
                  //{ fTempY = float.Parse((dTempM2 - dShatYetzia).TotalMinutes.ToString()); }
                  //else if (dShatYetzia >= dTempM1 && dShatYetzia.AddMinutes(iZmanElement) <= dTempM2)
                  //{ fTempY = iZmanElement; }
                  //fZmanAruchatErev += fTempY;

              }

              fZmanAruchatTzharim = fZmanAruchatTzharim > iMaxDakot ? iMaxDakot : fZmanAruchatTzharim;
     
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
            drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
            drChishuv["MISPAR_ISHI"] =  objOved.Mispar_ishi;
            drChishuv["TAARICH"] = objOved.Taarich;
            drChishuv["MISPAR_SIDUR"] = iMisparSidur;
            drChishuv["SHAT_HATCHALA"] = dShatHatchala;
            drChishuv["SHAT_YETZIA"] = dShatYetzia;
            drChishuv["MISPAR_KNISA"] = iMisparKnisa;
            drChishuv["KOD_RECHIV"] = iKodRechiv;
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
            _dtChishuvPeilut.Rows.Add(drChishuv);
            drChishuv = null;
        }
    }
}

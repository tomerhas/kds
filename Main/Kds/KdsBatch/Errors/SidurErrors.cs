using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.BL;
using System.Data;
using KdsBatch.Entities;
using KdsLibrary;

namespace KdsBatch.Errors
{
    public class SidurError9 : BasicChecker
    {
        public SidurError9(object CurrentInstance)
        {
            Comment = "סידור לא קיים";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((SidurInstance.bSidurMyuhad && SidurInstance.iMisparSidurMyuhad == 0) || SidurInstance.iMisparSidur.ToString().Length < 4)
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError15 : BasicChecker
    {
        public SidurError15(object CurrentInstance)
        {
            Comment = "חסרה שעת התחלה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.dFullShatHatchala.Year < clGeneral.cYearNull)
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError174 : BasicChecker
    {
        public SidurError174(object CurrentInstance)
        {
            Comment = "חסרה שעת גמר";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (string.IsNullOrEmpty(SidurInstance.sShatGmar))
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError33 : BasicChecker
    {
        public SidurError33(object CurrentInstance)
        {
            Comment = "זמן החתמת שעון לא מזכה בחריגה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;                   
            string sLookUp;
            DateTime dMeafyenStartDate=DateTime.MinValue;
            DateTime dMeafyenEndDate = DateTime.MinValue;
            bool bCheckChariga = false;
           try
           {
               //בדיקה ברמת סידור
               //השגיאה רלוונטית רק עבור עובד שיש לו מאפייני עבודה מתאימים ליום העבודה:
               //יום חול - מאפיינים 3, 4, שישי/ערב חג -  מאפיינים 5, 6 שבת/שבתון -  מאפיינים 7, 8
               if (clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim,SidurInstance.objDay.iSugYom, SidurInstance.dSidurDate))
               {
                   if (SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen7Exists && SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen8Exists)
                   {
                       bCheckChariga = true;
                   }
               }
               else if ((SidurInstance.sErevShishiChag == "1") || (SidurInstance.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
               {
                   if (SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen5Exists && SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen6Exists)
                   {
                       bCheckChariga = true;
                    }
               }
               else
               {
                   if (SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen3Exists && SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen4Exists)
                   {
                       bCheckChariga = true;
                   }
               }

               if (!(SidurInstance.iLoLetashlum == 1 && (SidurInstance.iKodSibaLoLetashlum == 4 || SidurInstance.iKodSibaLoLetashlum == 5 || SidurInstance.iKodSibaLoLetashlum == 10)))
               {
                   if (bCheckChariga)
                   {
                       dMeafyenStartDate = SidurInstance.dFullShatHatchalaLetashlum;
                       dMeafyenEndDate = SidurInstance.dFullShatGmarLetashlum;
                       if (string.IsNullOrEmpty(SidurInstance.sChariga))
                       {
                           bError = true;
                       }
                       else
                       {
                           sLookUp = GlobalData.GetLookUpKods("ctb_divuch_hariga_meshaot");
                           //אם ערך חריגה לא נמצא בטבלה
                           if (sLookUp.IndexOf(SidurInstance.sChariga) != -1)
                           {
                               clGeneral.enCharigaValue oCharigaValue;
                               oCharigaValue = (clGeneral.enCharigaValue)int.Parse((SidurInstance.sChariga));
                               switch (oCharigaValue)
                               {
                                   case clGeneral.enCharigaValue.CharigaKnisa:
                                       //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
                                       if (!string.IsNullOrEmpty(SidurInstance.sShatHatchala))
                                       {

                                           if (SidurInstance.dFullShatHatchala < dMeafyenStartDate)
                                           {
                                               if ((dMeafyenStartDate - SidurInstance.dFullShatHatchala).TotalMinutes < SidurInstance.objDay.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       break;
                                   case clGeneral.enCharigaValue.CharigaYetiza:
                                       if (!string.IsNullOrEmpty(SidurInstance.sShatGmar))
                                       {
                                           if (SidurInstance.dFullShatGmar > dMeafyenEndDate)
                                           {
                                               if ((SidurInstance.dFullShatGmar - dMeafyenEndDate).TotalMinutes < SidurInstance.objDay.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       break;
                                   case clGeneral.enCharigaValue.CharigaKnisaYetiza:
                                       //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
                                       if (!string.IsNullOrEmpty(SidurInstance.sShatHatchala))
                                       {
                                           if (SidurInstance.dFullShatHatchala < dMeafyenStartDate)
                                           {
                                               if ((dMeafyenStartDate - SidurInstance.dFullShatHatchala).TotalMinutes < SidurInstance.objDay.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       if (!string.IsNullOrEmpty(SidurInstance.sShatGmar))
                                       {
                                           if (SidurInstance.dFullShatGmar > dMeafyenEndDate)
                                           {
                                               if ((SidurInstance.dFullShatGmar - dMeafyenEndDate).TotalMinutes < SidurInstance.objDay.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                        }
                                       break;
                               }
                           }
                           if (SidurInstance.bSidurMyuhad && SidurInstance.sZakaiLeChariga == "3")
                               bError=false; 
                       }
                   }
               }
               return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError20 : BasicChecker
    {
        public SidurError20(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError137 : BasicChecker
    {
        public SidurError137(object CurrentInstance)
        {
            Comment = "ערך השלמה שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            DataRow drNew;
            string sLookUp = "";
            bool bError = false;
            try
            {
                if (!SidurInstance.bHashlamaExists && string.IsNullOrEmpty(SidurInstance.sHashlama))
                {
                    bError = true;
                }
                else
                {
                    sLookUp = "0,1,2,9";
                    if (sLookUp.IndexOf(SidurInstance.sHashlama) == -1)
                    {
                        bError = true;
                    }
                }
              
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError23 : BasicChecker
    {
        public SidurError23(object CurrentInstance)
        {
            Comment =  "ערך פיצול הפסקה ביום שבתון שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, SidurInstance.objDay.iSugYom, SidurInstance.dSidurDate) &&
                    (!string.IsNullOrEmpty(SidurInstance.sPitzulHafsaka)) && SidurInstance.sPitzulHafsaka != "0")
                {
                    bError = true;
                }             
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError96 : BasicChecker
    {
        public SidurError96(object CurrentInstance)
        {
            Comment ="חסר קמ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurVisaKodExists)
                {
                    SidurInstance.
                        Peiluyot.
                        ForEach (peilut =>
                                {
                                    if (peilut.MakatType == clKavim.enMakatType.mVisa
                                        && peilut.iKmVisa <= 0)
                                    {
                                        bError = true;
                                    }
                                }
                                );
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError118 : BasicChecker
    {
        public SidurError118(object CurrentInstance)
        {
            Comment =  "מחוץ למכסה שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (((SidurInstance.sOutMichsa != "0") && (SidurInstance.sOutMichsa != "1")) || (string.IsNullOrEmpty(SidurInstance.sOutMichsa)))
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError40 : BasicChecker
    {
        public SidurError40(object CurrentInstance)
        {
            Comment = "מחוץ למכסה בסדור שאסור ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.sOutMichsa == "1" && SidurInstance.sSectorAvoda == clGeneral.enSectorAvoda.Headrut.GetHashCode().ToString())
                {
                    bError = true;
                }            
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError136 : BasicChecker
    {
        public SidurError136(object CurrentInstance)
        {
            Comment =  "שיעור נהיגה לא בסידור הוראת נהיגה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iCountShiureyNehiga = 0;
            DataRow[] drSugSidur;
            try
            {
                if (!SidurInstance.bSidurMyuhad)
                {//סידורים רגילים
                    drSugSidur = GlobalData.GetOneSugSidurMeafyen(SidurInstance.iSugSidurRagil, SidurInstance.dSidurDate);
                    if (drSugSidur.Length > 0)
                    {   //עבור סידורים רגילים, נבדוק במאפייני סידורים אם סוג סידור נהגות.
                        if ((drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Nahagut.GetHashCode().ToString()))
                        {
                            iCountShiureyNehiga = SidurInstance.Peiluyot.ToList().Count(Peilut => Peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "842" && Peilut.lMakatNesia.ToString().PadLeft(8).Substring(5, 3) == "044");

                            if (iCountShiureyNehiga == 0)
                            {
                                bError = true;
                            }
                        }
                    }
                }

                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError34 : BasicChecker
    {
        public SidurError34(object CurrentInstance)
        {
            Comment = "סידור אינו זכאי לחריגה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            string sLookUp;
            int iShatGmar;
            try
            {
                if (SidurInstance.bSidurMyuhad)
                {//סידורים מיוחדים
                    if (!string.IsNullOrEmpty(SidurInstance.sChariga) && Int32.Parse(SidurInstance.sChariga) > 0)
                    {
                        if (!(string.IsNullOrEmpty(SidurInstance.sShatGmar)))
                        {
                            sLookUp = GlobalData.GetLookUpKods("ctb_divuch_hariga_meshaot");
                            iShatGmar = int.Parse(SidurInstance.sShatGmar.Remove(2, 1).Substring(0, 2));
                            //אם ערך חריגה תקין, אבל אין זכאות לחריגה נעלה שגיאה
                            if (((sLookUp.IndexOf(SidurInstance.sChariga)) != -1) && (!SidurInstance.bZakaiLeCharigaExists) && (iShatGmar < 28))  //לא קיים מאפיין 35
                            {
                                bError = true;
                            }
                        }
                    }
                }
                else
                {//סדורים רגילים
                    if (!string.IsNullOrEmpty(SidurInstance.sChariga) && Int32.Parse(SidurInstance.sChariga) > 0)
                    {
                        bError = true;
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError48 : BasicChecker
    {
        public SidurError48(object CurrentInstance)
        {
            Comment = "ערך השלמה לסידור שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            float fSidurTime;
            try
            {
                if (!string.IsNullOrEmpty(SidurInstance.sHashlama))
                {
                    if ((int.Parse(SidurInstance.sHashlama)) > 0)
                    {
                        if ((!(string.IsNullOrEmpty(SidurInstance.sShatGmar))) && (SidurInstance.dFullShatHatchala.Year > clGeneral.cYearNull))
                        {
                            fSidurTime = float.Parse((!string.IsNullOrEmpty(SidurInstance.sShatGmar) && !string.IsNullOrEmpty(SidurInstance.sShatHatchala) ? (SidurInstance.dFullShatGmar - SidurInstance.dFullShatHatchala).TotalMinutes : 0).ToString()); 

                            if (fSidurTime / 60 > int.Parse(SidurInstance.sHashlama))
                            {
                                bError = true;
                            }
                        }
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError25 : BasicChecker
    {
        public SidurError25(object CurrentInstance)
        {
            Comment = "פצול מיוחד ולא זכאי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (!string.IsNullOrEmpty(SidurInstance.sPitzulHafsaka))
                {
                    if ((SidurInstance.objDay.oOved.sKodHaver == "1") && (SidurInstance.sPitzulHafsaka == "2")) //קוד מעמד שמתחיל ב- 1 - חבר
                    {
                        bError = true;
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError57 : BasicChecker
    {
        public SidurError57(object CurrentInstance)
        {
            Comment = "סדור ויזה ללא סימון";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            string sLookUp = "";
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurVisaKodExists)
                {
                    sLookUp = GlobalData.GetLookUpKods("CTB_YOM_VISA");
                    int tmpVisaCode = 0;
                    Int32.TryParse(SidurInstance.sVisa, out tmpVisaCode);
                    if ((sLookUp.IndexOf(SidurInstance.sVisa) == -1) || (string.IsNullOrEmpty(SidurInstance.sVisa)) || tmpVisaCode == 0)
                    {
                        bError = true;  
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError22 : BasicChecker
    {
        public SidurError22(object CurrentInstance)
        {
            Comment = "פצול/הפסקה בסדור בודד אחרון";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((SidurInstance.objDay.Sidurim.Count == 1) || (SidurInstance == SidurInstance.objDay.Sidurim[SidurInstance.objDay.Sidurim.Count-1]))
                {
                    if (!string.IsNullOrEmpty(SidurInstance.sPitzulHafsaka) && Int32.Parse(SidurInstance.sPitzulHafsaka) > 0)
                    {
                        bError = true;
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError58 : BasicChecker
    {
        public SidurError58(object CurrentInstance)
        {
            Comment = "קיים סימון ויזה בסידור רגיל";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((!SidurInstance.bSidurVisaKodExists) && (!String.IsNullOrEmpty(SidurInstance.sVisa)) && Int32.Parse(SidurInstance.sVisa) > 0)
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError14 : BasicChecker
    {
        public SidurError14(object CurrentInstance)
        {
            Comment = "שעת ההתחלה לסידור מיוחד שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            DateTime dStartLimitHour, dEndLimitHour;
            DateTime dSidurStartHour;
            bool bSidurNihulTnua = false;
            bool bError = false;
            try
            {
                dStartLimitHour = SidurInstance.objDay.oParameters.dSidurStartLimitHourParam1;
                dEndLimitHour = SidurInstance.objDay.oParameters.dSidurEndLimitShatHatchala;

                dSidurStartHour = SidurInstance.dFullShatHatchala;

             
                bSidurNihulTnua = SidurInstance.IsSidurNihulTnua();

                if (SidurInstance.bSidurNahagut || bSidurNihulTnua)
                {
                    dStartLimitHour = SidurInstance.objDay.oParameters.dSidurStartLimitHourParam1;
                    dEndLimitHour = SidurInstance.objDay.oParameters.dShatHatchalaNahagutNihulTnua;
                }


                if (SidurInstance.bSidurMyuhad)
                {

                    if ((SidurInstance.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(SidurInstance.sShatHatchalaMuteret))) //קיים מאפיין
                    {
                        dStartLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(SidurInstance.sShatHatchalaMuteret).ToString("HH:mm"), SidurInstance.dSidurDate);
                    }

                    if ((SidurInstance.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(SidurInstance.sShatGmarMuteret))) //קיים מאפיין
                    {
                        dEndLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(SidurInstance.sShatGmarMuteret).ToString("HH:mm"), SidurInstance.dSidurDate.AddDays(1));
                    }
                }


                if ((!string.IsNullOrEmpty(SidurInstance.sShatHatchala) && dSidurStartHour < dStartLimitHour) && (dStartLimitHour.Year != clGeneral.cYearNull) ||
                    (!string.IsNullOrEmpty(SidurInstance.sShatHatchala) && dSidurStartHour > dEndLimitHour) && (dEndLimitHour.Year != clGeneral.cYearNull))
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError173 : BasicChecker
    {
        public SidurError173(object CurrentInstance)
        {
            Comment = "שעת סיום לסידור מיוחד שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            DateTime dEndLimitHour, dStartLimitHour;
            DateTime dSidurEndHour;
            try
            {
                bool isSidurNahagut = false;
                bool isSidurNihulTnua = false;
                DataRow[] drSugSidur = GlobalData.GetOneSugSidurMeafyen(SidurInstance.iSugSidurRagil,SidurInstance.dSidurDate );
   
                if (!SidurInstance.bSidurNahagut) { isSidurNihulTnua = SidurInstance.IsSidurNihulTnua(); }

                dStartLimitHour =SidurInstance.objDay.oParameters.dSidurStartLimitHourParam1;
                dEndLimitHour = (isSidurNahagut || isSidurNihulTnua) ? SidurInstance.objDay.oParameters.dNahagutLimitShatGmar : SidurInstance.objDay.oParameters.dSidurEndLimitHourParam3;

                if (SidurInstance.bSidurMyuhad && !string.IsNullOrEmpty(SidurInstance.sShaonNochachut) && (SidurInstance.objDay.oOved.iIsuk == 122 || SidurInstance.objDay.oOved.iIsuk == 123 || SidurInstance.objDay.oOved.iIsuk == 124 || SidurInstance.objDay.oOved.iIsuk == 127))
                    dEndLimitHour = SidurInstance.objDay.oParameters.dSidurLimitShatGmarMafilim;


                if ((SidurInstance.objDay.oOved.iIsuk != 122 && SidurInstance.objDay.oOved.iIsuk != 123 && SidurInstance.objDay.oOved.iIsuk != 124 && SidurInstance.objDay.oOved.iIsuk != 127) && SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen43Exists)
                    dEndLimitHour = SidurInstance.objDay.oParameters.dSiyumLilaLeovedLoMafil;

                dSidurEndHour = SidurInstance.dFullShatGmar;
                if (SidurInstance.bSidurMyuhad)
                {
                    if ((SidurInstance.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(SidurInstance.sShatHatchalaMuteret))) //קיים מאפיין
                    {
                        dStartLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(SidurInstance.sShatHatchalaMuteret).ToString("HH:mm"),SidurInstance.dSidurDate);
                    }

                    if ((SidurInstance.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(SidurInstance.sShatGmarMuteret))) //קיים מאפיין
                    {
                        dEndLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(SidurInstance.sShatGmarMuteret).ToString("HH:mm"),SidurInstance.dSidurDate.AddDays(1));
                    }
                }

                if ((!string.IsNullOrEmpty(SidurInstance.sShatGmar) && dSidurEndHour < dStartLimitHour) && (dStartLimitHour.Year != clGeneral.cYearNull) ||
                    (!string.IsNullOrEmpty(SidurInstance.sShatGmar) && dSidurEndHour > dEndLimitHour) && (dEndLimitHour.Year != clGeneral.cYearNull) ||
                    (!string.IsNullOrEmpty(SidurInstance.sShatGmar) && !string.IsNullOrEmpty(SidurInstance.sShatHatchala) && SidurInstance.dFullShatHatchala >= SidurInstance.dFullShatGmar))
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError49 : BasicChecker
    {
        public SidurError49(object CurrentInstance)
        {
            Comment =  "השלמת הזמנה אסורה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            float fSidurTime;
            int iZmanMinimum = 0;

            try
            {
                if (!string.IsNullOrEmpty(SidurInstance.sHashlama))
                {
                    if ((SidurInstance.bSidurMyuhad) && (SidurInstance.iMisparSidurMyuhad > 0))
                    {
                        if (SidurInstance.sHashlamaKod != "1")
                        {
                            if ((SidurInstance.bHashlamaExists) && (!String.IsNullOrEmpty(SidurInstance.sHashlama)))
                            {
                                if (int.Parse(SidurInstance.sHashlama) > 0)
                                {
                                    bError = true;
                                }
                            }
                        }
                    }
                    else //סידור רגיל
                    {
                        if ((!SidurInstance.bSidurMyuhad) && (int.Parse(SidurInstance.sHashlama) > 0))
                        {
                            if (clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, SidurInstance.objDay.iSugYom, SidurInstance.dSidurDate))
                            {
                                iZmanMinimum = SidurInstance.objDay.oParameters.iHashlamaShabat;
                            }
                            else
                            {
                                if ((SidurInstance.sErevShishiChag == "1") || (SidurInstance.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                                {
                                    iZmanMinimum = SidurInstance.objDay.oParameters.iHashlamaShisi;
                                }
                                else
                                {
                                    iZmanMinimum = SidurInstance.objDay.oParameters.iHashlamaYomRagil;
                                }
                            }
                            fSidurTime = float.Parse((!string.IsNullOrEmpty(SidurInstance.sShatGmar) && !string.IsNullOrEmpty(SidurInstance.sShatHatchala) ? (SidurInstance.dFullShatGmar - SidurInstance.dFullShatHatchala).TotalMinutes : 0).ToString()); 

                            if (fSidurTime > iZmanMinimum)
                            {
                                bError = true;
                            }
                        }
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError106 : BasicChecker
    {
        public SidurError106(object CurrentInstance)
        {
            Comment ="סדור ויזה מחייב סוג ויזה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurMyuhad)
                {//בסידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים עם ערך 2 ) חובה לדווח סוג ויזה. אם שדה ריק - שגוי. 
                    if (SidurInstance.sSidurVisaKod == "2" && SidurInstance.iSugHazmanatVisa == 0)
                    {
                        bError = true;
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError178 : BasicChecker
    {
        public SidurError178(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurMyuhad)
                {//בסידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים עם ערך 2 ) חובה לדווח קוד מבצע ויזה. אם שדה ריק - שגוי. 
                    if (SidurInstance.sSidurVisaKod == "2" && SidurInstance.iMivtzaVisa == 0 && SidurInstance.sLidroshKodMivtza != "")
                    {
                        bError=true;
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError127 : BasicChecker
    {
        public SidurError127(object CurrentInstance)
        {
            Comment =  "חובה לפחות פעילות אחת";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (((SidurInstance.bSidurMyuhad) && (SidurInstance.bPeilutRequiredKodExists) && (SidurInstance.iMisparSidurMyuhad > 0)) || (!(SidurInstance.bSidurMyuhad)))
                {
                    //אם אין פעילויות נעלה שגיאה
                    if (SidurInstance.Peiluyot.Count == 0)
                    {
                        bError = true;
                    }
                }      
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError148 : BasicChecker
    {
        public SidurError148(object CurrentInstance)
        {
            Comment = "סדור אסור לעובד בדרוג 85";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            clGeneral.enEmployeeType enEmployeeType; 
            try
            {
                enEmployeeType = (clGeneral.enEmployeeType)(SidurInstance.objDay.oOved.iKodHevra);
                if (enEmployeeType == clGeneral.enEmployeeType.enEggedTaavora)
                {
                    if ((SidurInstance.bSidurMyuhad) && (SidurInstance.bSidurNotValidKodExists))
                    {
                        bError = true; 
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError124 : BasicChecker
    {
        public SidurError124(object CurrentInstance)
        {
            Comment = "לעובד אסור סידור נ.צ.ר";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                //עובדים במרכז נ.צ.ר רשאים לעבוד שם רק לאחר שעברו הכשרה. בסיומה מדווחים להם מאפיין 64. מזהים סידור נ.צ.ר לפי מאפיין 52 ערך 11(סידור נצר) בטבלת סידורים מיוחדים.
                if ((SidurInstance.bSidurMyuhad) && (SidurInstance.sSugAvoda == clGeneral.enSugAvoda.Netzer.GetHashCode().ToString()) && 
                    (!SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen64Exists))
                {
                    bError = true;
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError160 : BasicChecker
    {
        public SidurError160(object CurrentInstance)
        {
            Comment =  "סידור עבודה לא חוקי לחודש זה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurMyuhad)
                {//קיים מאפיין 73 
                    if (SidurInstance.bSidurInSummerExists)
                    {
                      //  dSidurDate =SidurInstance.dSidurDate;
                        if ((SidurInstance.sSidurInSummer == "1") || (SidurInstance.sSidurInSummer == "2"))
                        {
                            if (SidurInstance.objDay.oParameters.dStartNihulVShivik.Year != clGeneral.cYearNull)
                            {
                                bError = (SidurInstance.dSidurDate < SidurInstance.objDay.oParameters.dStartNihulVShivik);
                            }

                            if (!bError)
                            {
                                if (SidurInstance.objDay.oParameters.dEndNihulVShivik.Year != clGeneral.cYearNull)
                                {
                                    bError = (SidurInstance.dSidurDate > SidurInstance.objDay.oParameters.dEndNihulVShivik);
                                }
                            }
                        }
                        else
                        {
                            bError = (((SidurInstance.dSidurDate < SidurInstance.objDay.oParameters.dStartTiful) && (SidurInstance.objDay.oParameters.dStartTiful.Year != clGeneral.cYearNull)) ||
                                ((SidurInstance.dSidurDate > SidurInstance.objDay.oParameters.dEndTiful) && (SidurInstance.objDay.oParameters.dEndTiful.Year != clGeneral.cYearNull)));
                        }
                        if (bError)
                        {
                            bError = true;
                        }
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError50 : BasicChecker
    {
        public SidurError50(object CurrentInstance)
        {
            Comment = "סדור אסור בשבתון ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurMyuhad) //וסידור מיוחד
                {   //אם שבתון וקיים מאפיין 9, כלומר סידור אזור בשבתון, אז נעלה שגיאה
                    if (clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, SidurInstance.objDay.iSugYom, SidurInstance.dSidurDate) 
                        && SidurInstance.bSidurNotInShabtonKodExists)
                    {//היום הוא יום שבתון ולסידור יש מאפיין אסור בשבתון, לכן נעלה שגיאה
                        bError = true;
                    }
                }        
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError32 : BasicChecker
    {
        public SidurError32(object CurrentInstance)
        {
            Comment = "חריגה שגוי";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            string sLookUp = ""; 
            try
            {    //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28
                if (string.IsNullOrEmpty(SidurInstance.sChariga))
                {
                    bError = true;
                }
                else
                {
                    sLookUp =GlobalData.GetLookUpKods("ctb_divuch_hariga_meshaot");
                    //אם ערך חריגה לא נמצא בטבלה
                    if (sLookUp.IndexOf(SidurInstance.sChariga) == -1)
                    {
                        bError = true;
                    }
                }
                 
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError164 : BasicChecker
    {
        public SidurError164(object CurrentInstance)
        {
            Comment = "סידור של ארועי קיץ לעובד 5 ימים";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.bSidurMyuhad)
                {
                    if (SidurInstance.bSidurInSummerExists && (SidurInstance.sSidurInSummer != "1" && SidurInstance.sSidurInSummer != "2" && SidurInstance.sSidurInSummer != "3" && SidurInstance.sSidurInSummer != "4"))
                    {
                        //סידור של ארועי קיץ חייב להיות לעובד אשר הוגדר עובד 6 ימים (מזהים לפי ערך 61, 62) במאפיין 56 במאפייני עובדים. סידור של ארועי קיץ = סידור מיוחד עם מאפיין 73
                        if (((!SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen56Exists)) || 
                            ((SidurInstance.objDay.oOved.oMeafyeneyOved.Meafyen56Exists)  && (SidurInstance.objDay.oOved.oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (SidurInstance.objDay.oOved.oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                        {
                            bError = true;
                        }
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError161 : BasicChecker
    {
        public SidurError161(object CurrentInstance)
        {
            Comment = "לעובד אסור לבצע סידור נהיגה";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            DataRow[] drSugSidur;
            try
            {
                if (SidurInstance.bSidurMyuhad)
                {
                    if (SidurInstance.sSugAvoda != clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
                    {
                        if (SidurInstance.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        {
                            bError = SidurInstance.CheckConditionsAllowSidur();
                        }
                    }
                }
                else
                {//סידור רגיל
                    drSugSidur = GlobalData.GetOneSugSidurMeafyen(SidurInstance.iSugSidurRagil, SidurInstance.dSidurDate);
                   
                    if (drSugSidur.Length > 0)
                    {
                        if (drSugSidur[0]["sug_Avoda"].ToString() != clGeneral.enSugAvoda.Grira.GetHashCode().ToString() && drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        {
                            bError =SidurInstance.CheckConditionsAllowSidur();
                        }
                    }
                }
              
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError16 : BasicChecker
    {
        public SidurError16(object CurrentInstance)
        {
            Comment ="קיימת חפיפה בשעות סידורים";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            Sidur oPrevSidur; 
            DateTime dShatHatchalaSidur = SidurInstance.dFullShatHatchala;
            bool bError = false;
            try
            {
                if (SidurInstance.iMispar_Siduri > 0)
                {
                    oPrevSidur = (Sidur)SidurInstance.objDay.Sidurim[SidurInstance.iMispar_Siduri - 1];
                    if (dShatHatchalaSidur != DateTime.MinValue && oPrevSidur.dFullShatGmar != DateTime.MinValue)
                    {
                        DateTime dPrevTime = new DateTime(oPrevSidur.dFullShatGmar.Year, oPrevSidur.dFullShatGmar.Month, oPrevSidur.dFullShatGmar.Day, int.Parse(oPrevSidur.dFullShatGmar.ToString("HH:mm").Substring(0, 2)), int.Parse(oPrevSidur.dFullShatGmar.ToString("HH:mm").Substring(3, 2)), 0);
                        DateTime dCurrTime = new DateTime(dShatHatchalaSidur.Year, dShatHatchalaSidur.Month, dShatHatchalaSidur.Day, int.Parse(dShatHatchalaSidur.ToString("HH:mm").Substring(0, 2)), int.Parse(dShatHatchalaSidur.ToString("HH:mm").Substring(3, 2)), 0);
                        //אם גם הסידור הקודם וגם הסידור הנוכחי הם לתשלום, נבצע את הבדיקה
                        if ((SidurInstance.iLoLetashlum == 0 || (SidurInstance.iLoLetashlum == 1 && SidurInstance.iKodSibaLoLetashlum == 1)) && (oPrevSidur.iLoLetashlum == 0 || (oPrevSidur.iLoLetashlum == 1 && oPrevSidur.iKodSibaLoLetashlum == 1)))
                        {
                            if (dCurrTime < dPrevTime)
                            {
                                bError = true;
                            }
                        }
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError24 : BasicChecker
    {
        public SidurError24(object CurrentInstance)
        {
            Comment = " עבודה בערב שבת/חג לאחר פצול";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            Sidur oPrevSidur;
            DateTime dSidurPrevShatGmar;
            DateTime dSidurShatHatchala; 
            int iSidurPrevPitzulHafsaka;

            try
            {
                if (SidurInstance.iMispar_Siduri > 0)
                {
                    oPrevSidur = (Sidur)SidurInstance.objDay.Sidurim[SidurInstance.iMispar_Siduri - 1];
                    iSidurPrevPitzulHafsaka= string.IsNullOrEmpty(oPrevSidur.sPitzulHafsaka) ? 0 : int.Parse(oPrevSidur.sPitzulHafsaka);

                    if (!(string.IsNullOrEmpty(SidurInstance.sShatHatchala)))
                    {
                        //אם  יום שישי או ערב חג אבל  לא בשבתון
                        if ((((SidurInstance.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || ((SidurInstance.sErevShishiChag == "1") && (SidurInstance.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString())))) && (iSidurPrevPitzulHafsaka > 0))
                        {
                            //נקרא את שעת כניסת השבת                   
                            //אם ביום שהוא ערב שבת/חג יש סידור אחד שמסתיים לפני כניסת שבת ויש לו סימון בשדה פיצול והסידור העוקב אחריו התחיל אחרי כניסת השבת  - זו שגיאה. (מצב תקין הוא שהסידור העוקב התחיל לפני כניסת שבת וגלש/לא גלש לשבת). 
                            //if (((int.Parse(sSidurPrevShatGmar.Remove(2, 1)) > iShabatStart)) || (int.Parse(SidurInstance.sShatHatchala.Remove(2, 1)) <= iShabatStart))
                            dSidurPrevShatGmar = clGeneral.GetDateTimeFromStringHour(oPrevSidur.sShatGmar, SidurInstance.dSidurDate);
                            dSidurShatHatchala = clGeneral.GetDateTimeFromStringHour(SidurInstance.sShatHatchala, SidurInstance.dSidurDate);
                            if ((dSidurPrevShatGmar <= SidurInstance.objDay.oParameters.dKnisatShabat) && (dSidurShatHatchala > SidurInstance.objDay.oParameters.dKnisatShabat))
                            {
                                //נציג את הסידור השני כשגוי
                                bError = true;
                            }
                            if ((dSidurPrevShatGmar > SidurInstance.objDay.oParameters.dKnisatShabat) && (dSidurShatHatchala > SidurInstance.objDay.oParameters.dKnisatShabat))
                            {
                                //נציג את שני הסידורים כשגויים
                                bError = true;
                            }
                        }
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError45 : BasicChecker
    {
        public SidurError45(object CurrentInstance)
        {
            Comment = "השלמה ליום עבודה בסידור היעדרות / סידור לא לתשלום";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (SidurInstance.objDay.sHashlamaLeyom == "1")
                {
                    if ((SidurInstance.objDay.Sidurim.Count == 1))
                    {
                        if (SidurInstance.bSidurMyuhad)
                        {
                            if ((SidurInstance.bHeadrutTypeKodExists) || (SidurInstance.iLoLetashlum > 0 && SidurInstance.iKodSibaLoLetashlum != 1))
                            {
                                bError = true;
                            }
                        }
                        else
                        {
                            if (SidurInstance.iLoLetashlum > 0 && SidurInstance.iKodSibaLoLetashlum != 1)
                            {
                                bError = true;
                            }
                        }
                    }
                    if (bError)
                    {
                        bError = true;
                    }
                }
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError142 : BasicChecker
    {
        public SidurError142(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError156 : BasicChecker
    {
        public SidurError156(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError143 : BasicChecker
    {
        public SidurError143(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError152 : BasicChecker
    {
        public SidurError152(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError153 : BasicChecker
    {
        public SidurError153(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError154 : BasicChecker
    {
        public SidurError154(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError168 : BasicChecker
    {
        public SidurError168(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError175 : BasicChecker
    {
        public SidurError175(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError176 : BasicChecker
    {
        public SidurError176(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError180 : BasicChecker
    {
        public SidurError180(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SidurError181 : BasicChecker
    {
        public SidurError181(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

      public class SidurError55 : BasicChecker
    {
        public SidurError55(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

      public class SidurError86 : BasicChecker
    {
        public SidurError86(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Sidur);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

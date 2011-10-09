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
    public class PeilutError81 : BasicChecker
    {
        public PeilutError81(object CurrentInstance)
        {
            Comment = "קוד נסיעה לא קיים";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (PeilutInstance.lMakatNesia.ToString().Length < 6)
                    bError = true;
                else if (PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || PeilutInstance.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || PeilutInstance.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                {
                    if (PeilutInstance.iMakatValid != 0)
                        bError = true;
                }
                else if (PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && PeilutInstance.lMakatNesia.ToString().Substring(0, 3) != "700" && (PeilutInstance.lMakatNesia.ToString().Length < 8 || PeilutInstance.iMakatValid != 0))
                    bError = true;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError81: " + ex.Message);
                PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }

    public class PeilutError139 : BasicChecker
    {
        public PeilutError139(object CurrentInstance)
        {
            Comment = "  נסיעה ללא רכב סידורי ";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && PeilutInstance.iMisparKnisa == 0 &&
                   PeilutInstance.iOnatiyut == 71 && PeilutInstance.lMisparSiduriOto == 0 && PeilutInstance.bPeilutEilat)
                {
                    bError = true;
                }    
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errMisparSiduriOtoNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError139: " + ex.Message);
                PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError129 : BasicChecker
    {
        public PeilutError129(object CurrentInstance)
        {
            Comment = "משך זמן האלמנט גדול ממשך זמן הסידור";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            DataRow drNew;
            long lMakatNesia = PeilutInstance.lMakatNesia;
            clKavim oKavim = new clKavim();
            int iElementTime;
            float fSidurTime;
            try
            {
                if ((PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (PeilutInstance.sElementZviraZman != clGeneral.enSectorZviratZmanForElement.ElementZviratZman.GetHashCode().ToString()) && (PeilutInstance.sElementInMinutes == "1"))
                {
                    fSidurTime = float.Parse((!string.IsNullOrEmpty(PeilutInstance.objSidur.sShatGmar) && !string.IsNullOrEmpty(PeilutInstance.objSidur.sShatHatchala) ? (PeilutInstance.objSidur.dFullShatGmar - PeilutInstance.objSidur.dFullShatHatchala).TotalMinutes : 0).ToString()); //clDefinitions.GetSidurTimeInMinuts(PeilutInstance.objSidur);
                    iElementTime = int.Parse(lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                    if (iElementTime > (fSidurTime))
                    {
                        bError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError129: " + ex.Message);
                PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError121 : BasicChecker
    {
        public PeilutError121(object CurrentInstance)
        {
            Comment = "שעת פעילות נמוכה משעת התחלת הסידור";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((clKavim.enMakatType)PeilutInstance.iMakatType == clKavim.enMakatType.mElement
                    && PeilutInstance.iElementLeYedia == 2 && PeilutInstance.lMakatNesia > 0)
                {
                    return bError;
                }

                //12עקב שינוי שעת התחלה של סידור יכול להיווצר מצב בו יש פעילות המתחילה לפני שעת התחלת הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה. פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים121-
                //122עקב שינוי שעת סיום של סידור יכול להיווצר מצב בו יש פעילות המתחילה אחרי שעת סיום הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה.  פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים122-
              
                if (!(string.IsNullOrEmpty(PeilutInstance.sShatYetzia)))
                {
                    if ((!((PeilutInstance.iMakatType == (long)clKavim.enMakatType.mElement.GetHashCode()) && (PeilutInstance.iElementLeYedia == 2))) && (PeilutInstance.lMakatNesia > 0))
                    {
                        if (PeilutInstance.objSidur.dFullShatHatchala.Year > clGeneral.cYearNull)
                        {//בדיקה 121
                            if (PeilutInstance.dFullShatYetzia < PeilutInstance.objSidur.dFullShatHatchala)
                            {
                                bError = true;
                                Comment = "שעת פעילות נמוכה משעת התחלת הסידור";
                            }
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError121: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }

    public class PeilutError122 : BasicChecker
    {
        public PeilutError122(object CurrentInstance)
        {
            Comment = "שעת פעילות גדולה משעת סיום הסידור";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((clKavim.enMakatType)PeilutInstance.iMakatType == clKavim.enMakatType.mElement
                    && PeilutInstance.iElementLeYedia == 2 && PeilutInstance.lMakatNesia > 0)
                {
                    return bError;
                }

                //12עקב שינוי שעת התחלה של סידור יכול להיווצר מצב בו יש פעילות המתחילה לפני שעת התחלת הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה. פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים121-
                //122עקב שינוי שעת סיום של סידור יכול להיווצר מצב בו יש פעילות המתחילה אחרי שעת סיום הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה.  פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים122-

                if (!(string.IsNullOrEmpty(PeilutInstance.sShatYetzia)))
                {
                    if ((!((PeilutInstance.iMakatType == (long)clKavim.enMakatType.mElement.GetHashCode()) && (PeilutInstance.iElementLeYedia == 2))) && (PeilutInstance.lMakatNesia > 0))
                    {
                        if (PeilutInstance.objSidur.dFullShatGmar != DateTime.MinValue)
                        {//בדיקה 122
                            if (PeilutInstance.dFullShatYetzia > PeilutInstance.objSidur.dFullShatGmar)
                            {
                                bError = true;
                                Comment = "שעת פעילות גדולה משעת סיום הסידור";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError122: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError84 : BasicChecker
    {
        public PeilutError84(object CurrentInstance)
        {
            Comment =  "פעילות אסורה בסדור תפקיד";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iPeilutMisparSidur;
            try
            {
                if ((PeilutInstance.objSidur.bSidurMyuhad) && (PeilutInstance.objSidur.bNoPeilotKodExists))
                {
                    //נבדוק אם קיימות לסידור פעילויות
                    //foreach (DictionaryEntry dePeilut in PeilutInstance.objSidur.htPeilut)
                    //{
                    iPeilutMisparSidur = PeilutInstance.iPeilutMisparSidur;
                    //sMakatNesia = htEmployeeDetails["MakatNesia"].ToString();
                    if (iPeilutMisparSidur > 0)
                    {
                        if (PeilutInstance.objSidur.Peiluyot.Count == 1)
                        {
                            if (PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && PeilutInstance.bMisparSidurMatalotTnuaExists && PeilutInstance.iMisparSidurMatalotTnua == iPeilutMisparSidur)
                                bError = true;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError84: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError69 : BasicChecker
    {
        public PeilutError69(object CurrentInstance)
        {
            Comment = "מספר רכב לא תקין/חסר מספר רכב";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            clKavim oKavim = new clKavim();
            try
            {
                clKavim.enMakatType oMakatType = (clKavim.enMakatType)PeilutInstance.iMakatType;
                if (((oMakatType == clKavim.enMakatType.mKavShirut) || (oMakatType == clKavim.enMakatType.mEmpty) || (oMakatType == clKavim.enMakatType.mNamak) || (oMakatType == clKavim.enMakatType.mElement && PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")) || ((PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700" && (PeilutInstance.bBusNumberMustExists) && (PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "701") && (PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "712") && (PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "711")))
                {
                    //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                    //בדיקת מספר רכב מול מש"ר

                    if (PeilutInstance.lOtoNo > 0)
                    {
                        if (!(oKavim.IsBusNumberValid(PeilutInstance.lOtoNo,PeilutInstance.dCardDate)))
                        {
                            bError = true;
                        }
                    }
                    else //חסר מספר רכב
                    {//שגיאה 69
                        //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                        bError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError69: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError31 : BasicChecker
    {
        public PeilutError31(object CurrentInstance)
        {
           // Comment = "";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iLastMisaprSidur;
            try
            {
                iLastMisaprSidur = PeilutInstance.objSidur.objDay.Sidurim[PeilutInstance.objSidur.objDay.Sidurim.Count - 1].iMisparSidur;
           
                if (!string.IsNullOrEmpty(PeilutInstance.objSidur.objDay.sLina))
                {
                    if ((PeilutInstance.objSidur.iMisparSidur == iLastMisaprSidur) && (int.Parse(PeilutInstance.objSidur.objDay.sLina) > 0) && (PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (PeilutInstance.bElementHamtanaExists))
                    {
                        bError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError31: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError68 : BasicChecker
    {
        public PeilutError68(object CurrentInstance)
        {
            Comment = "מספר רכב בסידור תפקיד";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (PeilutInstance.lOtoNo > 0)
                {
                    //TB_Sidurim_Meyuchadim נבדוק אם קיים מאפיין 43: לסידורים מיוחדים נבדוק בטבלת 
                    //לסידורים רגילים נבדוק את המאפיין מהתנועה
                    if (PeilutInstance.objSidur.bSidurMyuhad)
                    {
                        if ((PeilutInstance.objSidur.bNoOtoNoExists) && (PeilutInstance.objSidur.sNoOtoNo == "1"))
                        {
                            bError = true;
                        }
                    }
                    else //סידור רגיל
                    {
                        DataRow[] drSugSidur = GlobalData.GetOneSugSidurMeafyen(PeilutInstance.objSidur.iSugSidurRagil, PeilutInstance.objSidur.dSidurDate);

                        if (drSugSidur.Length > 0)
                        {
                            if ((!String.IsNullOrEmpty(drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString())) && (drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString() == "1"))
                            {
                                bError = true;
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError68: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError52 : BasicChecker
    {
        public PeilutError52(object CurrentInstance)
        {
            Comment = "תעודת נסיעה לא בסדור ויזה";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((!(PeilutInstance.objSidur.bSidurMyuhad && (PeilutInstance.objSidur.bSidurVisaKodExists))) && (PeilutInstance.lMisparVisa > 0))
                {
                    bError = true;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError52: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError87 : BasicChecker
    {
        public PeilutError87(object CurrentInstance)
        {
            Comment = "כסוי תור מעל המותר";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && PeilutInstance.iMisparKnisa == 0) || PeilutInstance.iMakatType == clKavim.enMakatType.mNamak.GetHashCode())
                {
                    if (PeilutInstance.iKisuyTor > PeilutInstance.iKisuyTorMap)
                        bError = true;

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError87: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError123 : BasicChecker
    {
        public PeilutError123(object CurrentInstance)
        {
            Comment = "אלמנט אסור בסדור מיוחד";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (PeilutInstance.objSidur.bSidurMyuhad)
                {
                    if (PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && PeilutInstance.sDivuchInSidurMeyuchad == "1")
                    {
                        bError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError123: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError125 : BasicChecker
    {
        public PeilutError125(object CurrentInstance)
        {
            Comment = "נסיעה אסורה בסידור ויזה";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (PeilutInstance.objSidur.bSidurVisaKodExists)
                {
                    if (!(PeilutInstance.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || (PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && PeilutInstance.sDivuchInSidurVisa == "2")))
                    {
                        bError = true;
                    }
                }        
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError125: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError166 : BasicChecker
    {
        public PeilutError166(object CurrentInstance)
        {
            Comment = "עיכוב ארוך מעל המותר ";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iTimeInMinutes = 0;
            bool bCurrSidurEilat = false;
            bool bhaveHamtana = false;
            int iParamHamtana = 0;
            try
            {
                if (PeilutInstance.objSidur.bSidurEilat && PeilutInstance.objSidur.IsLongEilatTrip())
                {
                    bCurrSidurEilat = true;
                }
                if (PeilutInstance.lMakatNesia.ToString().Length > 5)
                    iTimeInMinutes = int.Parse(PeilutInstance.lMakatNesia.ToString().Substring(3, 3));

                //1. אם בסידור אליו משויכת פעילות ההמתנה קיימת נסיעת אילת ארוכה ומשך ההמתנה (הערך בפוזיציות 4-6) הוא מעל הערך בפרמטר 148 - שגוי.
                //2. אם בסידור אליו משויכת פעילות ההמתנה לא קיימת נסיעת אילת ארוכה ומשך ההמתנה (הערך בפוזיציות 4-6) הוא מעל הערך בפרמטר 161 - שגוי.
                if (bCurrSidurEilat && PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && PeilutInstance.bElementHamtanaExists)
                {
                    iParamHamtana = PeilutInstance.objSidur.objDay.oParameters.iMaxZmanHamtanaEilat;
                    bhaveHamtana = true;
                }
                else if (!bCurrSidurEilat && PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && PeilutInstance.bElementHamtanaExists)
                {
                    iParamHamtana = PeilutInstance.objSidur.objDay.oParameters.iMaximumHmtanaTime;
                    bhaveHamtana = true;
                }

                if (bhaveHamtana && iTimeInMinutes > iParamHamtana)
                {
                    bError = true;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError166: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError13 : BasicChecker
    {
        public PeilutError13(object CurrentInstance)
        {
            Comment = "סדור נמלק ללא תעודת נסיעה";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((PeilutInstance.objSidur.bSidurMyuhad) && (PeilutInstance.objSidur.bSidurVisaKodExists) && (PeilutInstance.objSidur.iMisparSidurMyuhad > 0))
                {
                    if ((PeilutInstance.lMisparVisa == 0) && (PeilutInstance.lMakatNesia > 0) && PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(0, 1) == "5")  //אין תעודת נסיעה
                    {
                        bError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError13: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError162 : BasicChecker
    {
        public PeilutError162(object CurrentInstance)
        {
            Comment = "פעילות נבלעת בתוך פעילות קודמת ";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            Peilut oPrevPeilut;
            DateTime dCurrEndPeilut = new DateTime();
            DateTime dCurrStartPeilut = new DateTime();
            DateTime dPrevEndPeilut = new DateTime();
            DateTime dPrevStartPeilut = new DateTime();
            byte bCheck = 0;
            double dblCurrTimeInMinutes = 0;
            double dblPrevTimeInMinutes = 0; 
            try
            {
                if (PeilutInstance.iMispar_siduri > 0 && PeilutInstance.iMisparKnisa == 0)//לא נבצע את הבדיקה לפעילות הראשונה 
                {
                    oPrevPeilut = (Peilut)PeilutInstance.objSidur.Peiluyot[PeilutInstance.iMispar_siduri - 1];
                    if ((PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode()) || (PeilutInstance.iMakatType == clKavim.enMakatType.mNamak.GetHashCode()))
                    {
                        dCurrStartPeilut = PeilutInstance.dFullShatYetzia;
                        if (PeilutInstance.iDakotBafoal > 0)
                            dblCurrTimeInMinutes = PeilutInstance.iDakotBafoal;
                        else dblCurrTimeInMinutes = PeilutInstance.iMazanTichnun;
                        bCheck = 1;
                    }
                    

                    //זמן תחילת פעילות לאחר זמן תחילת הפעילות הקודמת לה וזמן סיום הפעילות קודם לזמן סיום הפעילות הקודמת לה. זיהוי זמן הפעילות (זיהוי סוג פעילות לפי רוטינת זיהוי מק"ט) :עבור קו שירות, נמ"ק, , יש לפנות לקטלוג נסיעות כדי לקבל את הזמן. עבור אלמנט, במידה וזה אלמנט זמן (לפי ערך 1 במאפיין 4 בטבלת מאפייני אלמנטים), הזמן נלקח מפוזיציות 4-6 של האלמנט. בבדיקה זו אין  להתייחס לפעילות המתנה (מזהים פעילות המתנה (מסוג אלמנט) לפי מאפיין 15 בטבלת מאפייני אלמנטים).           
                    if ((oPrevPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode()) || (oPrevPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode()))
                    {
                        dPrevStartPeilut = oPrevPeilut.dFullShatYetzia;
                        if (oPrevPeilut.iDakotBafoal > 0)
                            dblPrevTimeInMinutes = oPrevPeilut.iDakotBafoal;
                        else dblPrevTimeInMinutes = oPrevPeilut.iMazanTichnun;
                        // dblPrevTimeInMinutes = oPrevPeilut.iMazanTichnun;
                        bCheck &= 1;
                    }
                   
                    if (bCheck == 1)
                    {
                        dCurrEndPeilut = dCurrStartPeilut.AddMinutes(dblCurrTimeInMinutes);
                        dPrevEndPeilut = dPrevStartPeilut.AddMinutes(dblPrevTimeInMinutes);
                        if ((dCurrStartPeilut >= dPrevStartPeilut) && (dCurrEndPeilut < dPrevEndPeilut))
                        {
                            bError = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError162: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError151 : BasicChecker
    {
        public PeilutError151(object CurrentInstance)
        {
            Comment = "נסיעה כפולה בין עובדים שונים";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            DataTable dtDuplicate = new DataTable();
            EntitiesDal oDal = new EntitiesDal();
            try
            {
                if (PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode())
                {
                    if (oDal.IsDuplicateTravle(PeilutInstance.objSidur.iMisparIshi, PeilutInstance.dCardDate, PeilutInstance.lMakatNesia, PeilutInstance.dFullShatYetzia, PeilutInstance.iMisparKnisa, ref dtDuplicate))
                    {
                        bError = true;

                        for (int i = 0; i < dtDuplicate.Rows.Count; i++)
                        {
                            //if (!CheckApprovalToEmploee((int)dtDuplicate.Rows[i]["mispar_ishi"],(DateTime)dtDuplicate.Rows[i]["taarich"],"25", PeilutInstance.objSidur.iMisparSidur, PeilutInstance.objSidur.dFullShatHatchala, PeilutInstance.iMisparKnisa, PeilutInstance.dFullShatYetzia))
                            clDefinitions.UpdateCardStatus((int)dtDuplicate.Rows[i]["mispar_ishi"], (DateTime)dtDuplicate.Rows[i]["taarich"], clGeneral.enCardStatus.Error, PeilutInstance.objSidur.objDay.iUserId);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError151: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError179 : BasicChecker
    {
        public PeilutError179(object CurrentInstance)
        {
            Comment = "ערך דקות בפועל גבוה מזמן לגמר או מהזמן המותר לכניסה ";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && PeilutInstance.iMisparKnisa == 0) || PeilutInstance.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || PeilutInstance.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                {
                    if (PeilutInstance.iDakotBafoal > PeilutInstance.iMazanTashlum)
                        bError = true;

                }

                if (PeilutInstance.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && PeilutInstance.iMisparKnisa > 0)
                {
                    if (PeilutInstance.iDakotBafoal > PeilutInstance.objSidur.objDay.oParameters.iMaxMinutsForKnisot)
                        bError = true;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError179: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
    public class PeilutError86 : BasicChecker
    {
        public PeilutError86(object CurrentInstance)
        {
            Comment = "הכנת מכונה מעל המותר ";
            SetInstance(CurrentInstance, OriginError.Peilut);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iElementType =0;
            int iElementTime =0;
            try
            {
                //זמן הכנת מכונה באלמנט הוא מוגבל בזמן הזמן משתנה אם זו הכנת מכונה ראשונה (אלמנט 701xxx00) ביום או נוספת (711xxx00). זיהוי הכנה ראשונה/נוספת ביום - אם הסידור בו מדווחת הכנת מכונה התחיל עד 8 בבוקר (לא כולל) זוהי הכנת מכונה ראשונה. כל הכנת מכונה נוספת/מאוחרת משעה 8 בבוקר (כולל) נחשבת להכנת מכונה נוספת. זמן תקין להכנת מכונה  ראשונה הוא עד הערך בפרמטר 120 (זמן הכנת מכונה ראשונה), זמן תקין להכנת מכונה נוספת הוא עד הערך בפרמטר 121 (זמן הכנת מכונה נוספת). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה נוספות, זמן תקין לפי פרמטר 122 (מכסימום יומי להכנות מכונה נוספות דקות). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), זמן תקין לפי פרמטר 123 (מכסימום יומי להכנות מכונה  דקות).      מקסימום הכנות מכונה מותר בסידור         יש מקסימום למספר הכנות מכונה מותרות בסידור, נבדק לפי פרמטר 124 (מכסימום הכנות מכונה בסידור אחד), לא משנה מה הסוג שלהן.      
                if ((PeilutInstance.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (!String.IsNullOrEmpty(PeilutInstance.sShatYetzia)))
                {
                    iElementType = int.Parse(PeilutInstance.lMakatNesia.ToString().Substring(0, 3));
                    if ((iElementType == 701) || (iElementType == 711))
                    {
                        iElementTime = int.Parse(PeilutInstance.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                        if ((iElementType == 701) && PeilutInstance.dFullShatYetzia < PeilutInstance.dFullShatYetzia.Date.AddHours(8) || clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, PeilutInstance.objSidur.objDay.iSugYom, PeilutInstance.objSidur.dSidurDate))
                        {
                            //מכונה ראשונה ביום- נשווה לפרמטר 120
                            bError = (iElementTime > PeilutInstance.objSidur.objDay.oParameters.iPrepareFirstMechineMaxTime);
                            //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
                            PeilutInstance.objSidur.objDay.iTotalTimePrepareMechineForDay += iElementTime;
                            //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
                            //iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
                            PeilutInstance.objSidur.iTotalTimePrepareMechineForSidur += 1;
                        }
                        else if ((iElementType == 701) && PeilutInstance.dFullShatYetzia >= PeilutInstance.dFullShatYetzia.Date.AddHours(8) && !clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, PeilutInstance.objSidur.objDay.iSugYom, PeilutInstance.objSidur.dSidurDate))
                        {
                            //מכונות נוספות נשווה לפרמטר 121
                            bError = (iElementTime > PeilutInstance.objSidur.objDay.oParameters.iPrepareOtherMechineMaxTime);
                            //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
                            PeilutInstance.objSidur.objDay.iTotalTimePrepareMechineForDay +=  iElementTime;
                            //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
                            //iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
                            PeilutInstance.objSidur.iTotalTimePrepareMechineForSidur += 1;

                            if (iElementType == 711)
                            {
                                //צבירת זמן כל המכונות הנוספות - נשווה בסוף מול פרמטר 122
                                PeilutInstance.objSidur.objDay.iTotalTimePrepareMechineForOtherMechines += iElementTime;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(PeilutInstance.objSidur.objDay.btchRequest, PeilutInstance.objSidur.objDay.oOved.iMisparIshi, "E", TypeCheck.errKodNesiaNotExists.GetHashCode(), PeilutInstance.objSidur.objDay.dCardDate, "PeilutError86: " + ex.Message);
                  PeilutInstance.objSidur.objDay.bSuccsess = false;
            }
            return bError;
        }
    }
}

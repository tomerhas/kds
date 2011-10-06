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
    public class DayError1 : BasicChecker
    {
        public DayError1(object CurrentInstance)
        {
          //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                bError = !(DayInstance.oOved.IsOvedInMatzav("1,3,4,5,6,7,8"));

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errHrStatusNotValid.GetHashCode(), DayInstance.dCardDate, "DayError1: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            
           return bError;    
        }
    }
    
    public class DayError27: BasicChecker
    {
        public DayError27(object CurrentInstance)
        {
            //  Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            string sLookUp = "";
            try
            {
                sLookUp = GlobalData.GetLookUpKods("ctb_zmaney_nesiaa");
                if (!(string.IsNullOrEmpty(DayInstance.sBitulZmanNesiot)))
                {   //נעלה שגיאה אם ערך לא קיים בטבלת פענוח
                    if (sLookUp.IndexOf(DayInstance.sBitulZmanNesiot) == -1)
                    {
                        bError = true;
                    }
                }
                else
                {
                    bError = true;
                }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errSimunNesiaNotValid.GetHashCode(), DayInstance.dCardDate, "DayError27: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError30 : BasicChecker
    {
        public DayError30(object CurrentInstance)
        {
           // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            string sLookUp = "";
            try
            {
                if (string.IsNullOrEmpty(DayInstance.sLina))
                {
                    bError = true;
                }
                else
                {
                    sLookUp = GlobalData.GetLookUpKods("ctb_lina");
                    bError = (sLookUp.IndexOf(DayInstance.sLina) == -1);
                }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errLinaValueNotValid.GetHashCode(), DayInstance.dCardDate, "DayError30: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError36 : BasicChecker
    {
        public DayError36(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            string sLookUp = "";
            try
            {
                sLookUp = GlobalData.GetLookUpKods("ctb_zmaney_halbasha");
                if (string.IsNullOrEmpty(DayInstance.sHalbasha))
                {
                    bError = true;
                }
                else
                {
                    if ((sLookUp.IndexOf(DayInstance.sHalbasha) == -1) || DayInstance.sHalbasha == ZmanHalbashaType.CardError.GetHashCode().ToString())
                    {
                        bError = true;
                    }
                }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errHalbashaNotvalid.GetHashCode(), DayInstance.dCardDate, "DayError36: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError47: BasicChecker
    {
        public DayError47(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (DayInstance.sHashlamaLeyom == "1" && clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, DayInstance.iSugYom, DayInstance.dCardDate))
                {
                    bError= true;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errShbatHashlamaNotValid.GetHashCode(), DayInstance.dCardDate, "DayError47: " + ex.Message);
                 DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError103: BasicChecker
    {
        public DayError103(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            Dictionary<string, Peilut> peiluyot = new Dictionary<string, Peilut>();
            bool shouldProcess = true;
            clKavim oKavim = new clKavim();
            DataTable dtMeafyenim = null;
            try
            {
                DayInstance.Sidurim.ForEach(sidur =>
                                            {
                                                sidur.Peiluyot.ForEach(peilut =>
                                                        {
                                                            if (!bError)
                                                            {
                                                                if ((clKavim.enMakatType)peilut.iMakatType == clKavim.enMakatType.mElement)
                                                                {
                                                                    dtMeafyenim = oKavim.GetMeafyeneyElementByKod(peilut.lMakatNesia, peilut.dCardDate);
                                                                    if (dtMeafyenim.Select("KOD_MEAFYEN = 9") != null)
                                                                    {
                                                                        shouldProcess = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        shouldProcess = true;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    shouldProcess = true;
                                                                }

                                                                if (shouldProcess)
                                                                {
                                                                    if (!peiluyot.ContainsKey(peilut.dFullShatYetzia.ToString()))
                                                                    {
                                                                        peiluyot.Add(peilut.dFullShatYetzia.ToString(), peilut);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (peilut.iMisparKnisa == 0 && peiluyot[peilut.dFullShatYetzia.ToString()].iMisparKnisa == 0)
                                                                        {
                                                                            bError = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                      );
                                            }
                                    );
               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errDuplicateShatYetiza.GetHashCode(), DayInstance.dCardDate, "DayError103: " + ex.Message);
                 DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError132 : BasicChecker
    {
        public DayError132(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iCountSidurim;
            try
            {
                iCountSidurim = DayInstance.Sidurim.Count(Sidur => Sidur.iNitanLedaveachBemachalaAruca == 0);
                if ((DayInstance.oOved.iMatzavOved == 5) && (iCountSidurim > 0))
                {
                    bError = true;
                }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errOvdaInMachalaNotAllowed.GetHashCode(), DayInstance.dCardDate, "DayError132: " + ex.Message);
                 DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError167 : BasicChecker
    {
        public DayError167(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            EntitiesDal oDal = new EntitiesDal();
            DateTime shatHatchalaOfPrevDay = DateTime.MinValue;
            DateTime shatGmarOfPrevDay = DateTime.MinValue;
            DateTime shatHatchalaOfNextDay = DateTime.MinValue;
            DateTime shatGmarOfNextDay = DateTime.MinValue;
            bool bError = false;
            DataTable dtSidur;
            try
            {
                dtSidur = oDal.GetSidur("last", DayInstance.oOved.iMisparIshi, DayInstance.dCardDate.AddDays(-1));
                if (dtSidur != null && dtSidur.Rows.Count > 0)
                {
                    DateTime.TryParse(dtSidur.Rows[0]["shat_hatchala"].ToString(), out shatHatchalaOfPrevDay);
                    DateTime.TryParse(dtSidur.Rows[0]["shat_gmar"].ToString(), out shatGmarOfPrevDay);
                }
                if (DayInstance.Sidurim.Count > 0)
                {
                    Sidur firstSidurOfTheDay = DayInstance.Sidurim[0] as Sidur;
                    Sidur lastSidurOfTheDay = DayInstance.Sidurim[DayInstance.Sidurim.Count - 1] as Sidur;
                    dtSidur = oDal.GetSidur("first", DayInstance.oOved.iMisparIshi, DayInstance.dCardDate.AddDays(1));
                    if (dtSidur != null && dtSidur.Rows.Count > 0)
                    {
                        DateTime.TryParse(dtSidur.Rows[0]["shat_hatchala"].ToString(), out shatHatchalaOfNextDay);
                        DateTime.TryParse(dtSidur.Rows[0]["shat_gmar"].ToString(), out shatGmarOfNextDay);
                    }

                    if (shatGmarOfPrevDay != DateTime.MinValue &&
                        shatGmarOfPrevDay.Day == firstSidurOfTheDay.dFullShatHatchala.Day &&
                        (shatGmarOfPrevDay - firstSidurOfTheDay.dFullShatHatchala) > TimeSpan.Zero)
                    {
                        bError = true;
                        //   hafifaDescription = "חפיפה עם יום קודם";
                    }

                    if (shatHatchalaOfNextDay != DateTime.MinValue &&
                        lastSidurOfTheDay.dFullShatGmar != DateTime.MinValue &&
                        lastSidurOfTheDay.dFullShatGmar.Day == shatHatchalaOfNextDay.Day &&
                        (lastSidurOfTheDay.dFullShatGmar - shatHatchalaOfNextDay) > TimeSpan.Zero)
                    {
                        bError = true;
                        //  hafifaDescription = "חפיפה עם יום עוקב";
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errHafifaBetweenSidurim.GetHashCode(), DayInstance.dCardDate, "DayError167: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError171 : BasicChecker
    {
        public DayError171(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            bool hasSidurEilat = false;
            bool hasVisa = false;
            bool isLongNesiaToEilat = false;
            Peilut tmpPeilut = null;
            try
            {
                DayInstance.Sidurim.ForEach(
                                            sidur =>
                                            {
                                                if (sidur.bSidurEilat)
                                                {
                                                    hasSidurEilat = true;

                                                    if (sidur.IsLongEilatTrip())
                                                        isLongNesiaToEilat = true;
                                                }

                                                if (sidur.bSidurVisaKodExists) hasVisa = true;
                                            }
                                        );
                if (hasSidurEilat && isLongNesiaToEilat && hasVisa)
                {
                    bError = true;
                }
              
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errHasBothSidurEilatAndSidurVisa.GetHashCode(), DayInstance.dCardDate, "DayError171: " + ex.Message);
                 DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError172 : BasicChecker
    {
        public DayError172(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (DayInstance.oOved.IsOvedInMatzav("5,6,8") && DayInstance.Sidurim.Any(sidur => !sidur.IsSidurHeadrut()))
                {
                    bError = true;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errOvedPeilutNotValid.GetHashCode(), DayInstance.dCardDate, "DayError172: " + ex.Message);
                 DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError165 : BasicChecker
    {
        public DayError165(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if (!DayInstance.oOved.CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                {
                    if ((DayInstance.oOved.oMeafyeneyOved.Meafyen51Exists) || (DayInstance.oOved.oMeafyeneyOved.Meafyen61Exists))
                    {
                       if( DayInstance.Sidurim.Any(sidur => sidur.bSidurNahagut))
                           bError = true;
                    }
                }  
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errAvodatNahagutNotValid.GetHashCode(), DayInstance.dCardDate, "DayError165: " + ex.Message);
                 DayInstance.bSuccsess = false;
            }
            return bError;
        }
    }

    public class DayError150 : BasicChecker
    {
        public DayError150(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            int iZmanNesiaHaloch, iZmanNesiaChazor;
            try
            {
                iZmanNesiaHaloch = -1;
                iZmanNesiaChazor = -1;
                if (DayInstance.oOved.oMeafyeneyOved.Meafyen61Exists) // אם קיים מאפיין 61, לעובד מגיע זמן נסיעה משתנה
                {
                    if (DayInstance.oOved.bMercazEruaExists)
                    {
                        if (DayInstance.oOved.IsOvedZakaiLZmanNesiaLaAvoda() || DayInstance.oOved.IsOvedZakaiLZmanNesiaLeMeAvoda())
                        {
                            iZmanNesiaHaloch = DayInstance.GetZmanNesiaMeshtana(0, 1, DayInstance.dCardDate);
                        }

                        if (DayInstance.oOved.IsOvedZakaiLZmanNesiaMeAvoda() || DayInstance.oOved.IsOvedZakaiLZmanNesiaLeMeAvoda())
                        {
                            iZmanNesiaChazor = DayInstance.GetZmanNesiaMeshtana(DayInstance.Sidurim.Count - 1, 1, DayInstance.dCardDate);
                        }

                        if (iZmanNesiaChazor == -1 || iZmanNesiaHaloch == -1)
                        {
                            bError = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errNesiaMeshtanaNotDefine.GetHashCode(), DayInstance.dCardDate, "DayError150: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            return bError;   
        }

      
    }

    public class DayError86 : BasicChecker   
    {
        public DayError86(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            bool bError = false;
            try
            {
                if ((DayInstance.iTotalTimePrepareMechineForDay > DayInstance.oParameters.iPrepareAllMechineTotalMaxTimeInDay) ||
                    (DayInstance.iTotalTimePrepareMechineForOtherMechines > DayInstance.oParameters.iPrepareOtherMechineTotalMaxTime))
                {
                    bError = true;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(DayInstance.btchRequest, DayInstance.oOved.iMisparIshi, "E", TypeCheck.errTimeForPrepareMechineNotValid.GetHashCode(), DayInstance.dCardDate, "DayError86: " + ex.Message);
                DayInstance.bSuccsess = false;
            }
            return bError;   
        }
    }  

   
}

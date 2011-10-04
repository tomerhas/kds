using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.BL;
using System.Data;
using KdsBatch.Entities;

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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                if (DayInstance.sHashlamaLeyom == "1" && clDefinitions.CheckShaaton(GlobalData.dtSugeyYamimMeyuchadim, DayInstance.iSugYom, DayInstance.dCardDate))
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            return true;
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                if (DayInstance.oOved.IsOvedInMatzav("5,6,8") && DayInstance.Sidurim.Any(sidur => sidur.bSidurMyuhad && !string.IsNullOrEmpty(sidur.sHeadrutTypeKod)))
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            return bError;     
        }

      
    }

    public class DayError86 : BasicChecker  // x2
    {
        public DayError86(object CurrentInstance)
        {
            // Comment = " נתונים ";
            SetInstance(CurrentInstance, OriginError.Day);
        }
        protected override bool IsCorrect()
        {
            return true;
        }
    }  

   
}

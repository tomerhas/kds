using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Errors
{
    public enum OriginError
    {
        Day,
        Sidur,
        Peilut
    }
    public enum TypeCheck
    {
        errHrStatusNotValid = 1,
        errSidurNotExists = 9,
        errSidurNamlakWithoutNesiaCard = 13,
        errSidurHourStartNotValid = 14,//לבדוק שינויים            
        errStartHourMissing = 15,
        errSidurimHoursNotValid = 16,
        errPizulHafsakaValueNotValid = 20,
        errPizulValueNotValid = 22, 
        errShabatPizulValueNotValid = 23,
        errPitzulSidurInShabat = 24,
        errPitzulMuchadValueNotValid = 25,
        errLoZakaiLeNesiot  = 26,
        errSimunNesiaNotValid = 27,
        errLinaValueNotValid = 30,
        errLoZakaiLLina = 31,
        errCharigaValueNotValid = 32,
        errCharigaZmanHachtamatShaonNotValid = 33,
        errZakaiLeCharigaValueNotValid = 34,  
        errSidurExistsInShlila =35,//לקחת את הערך לפי עץ ניהולי
        errHalbashaNotvalid = 36, 
        errHalbashaInSidurNotValid = 37, 
        errHamaraNotValid = 39,
        errOutMichsaInSidurHeadrutNotValid = 40,
        errHamaratShabatNotValid = 42,
        errHamaratShabatNotAllowed=43,
        errZakaiLehamaratShabat = 44,//להוסיף לסידורים רגילים
        errHashlamaForComputerWorkerAndAccident = 45,            
        errShbatHashlamaNotValid = 47,
        errHashlamaForSidurNotValid = 48,
        errHahlamatHazmanaNotValid = 49,
        errSidurNotAllowedInShabaton = 50,
        errTeoodatNesiaNotInVisa =52,
        errSidurEilatNotValid = 55,
        errYomVisaNotValid = 56,
        errSimunVisaNotValid = 57,
        errSidurVisaNotValid = 58,
        //errOtoNoNotValid = 67,
        errOtoNoExists = 68,//להוסיף לסידורים רגילים
        errOtoNoNotExists = 69,//לבדוק באיפיון מה הכוונה מספר פעילות המתחילה ב7-
        errKodNesiaNotExists = 81,
        errPeilutForSidurNonValid = 84,
        errTimeForPrepareMechineNotValid = 86,
        errHighValueKisuyTor=87,
        errNesiaTimeNotValid = 91,
        errShatYetizaNotExist = 92,
        errKmNotExists = 96,
        //errKisuyTorNotValid = 117,
        errDuplicateShatYetiza = 103,
        errMissingSugVisa=106,
        errHafifaBecauseOfHashlama = 108,
        errPeilutShatYetizaNotValid = 113,
        errOutMichsaNotValid = 118,
        errShatPeilutSmallerThanShatHatchalaSidur = 121,
        errShatPeilutBiggerThanShatGmarSidur = 122,
        errElementInSpecialSidurNotAllowed=123,
        errSidurNetzerNotValidForOved = 124,
        errNesiaInSidurVisaNotAllowed = 125,
        errAtLeastOnePeilutRequired = 127,
        errElementTimeBiggerThanSidurTime = 129,
        errOvdaInMachalaNotAllowed = 132,
        errDriverLessonsNumberNotValid = 136,
        errHashlamaNotValid = 137,
        errMisparSiduriOtoNotExists = 139,  
        errMisparSiduriOtoNotInSidurEilat = 140,
        errNotAllowedKodsForEggedTaavora = 141,
        errTotalHashlamotBiggerThanAllow = 142,
        errMissingNumStore=143,
        errSidurTafkidWithOutApprove = 145,
        errNotAllowedSidurForEggedTaavora = 148,
        errNesiaMeshtanaNotDefine  = 150,
        errDuplicateTravle=151,
        errChafifaBesidurNihulTnua = 152,
        errHighPremya = 153,
        errNegativePremya = 154,
        errMiluimAndAvoda=156,
        errSidurAvodaNotValidForMonth = 160,
        errOvedNotAllowedToDoSidurNahagut = 161,
        errCurrentPeilutInPrevPeilut = 162,
        errHashlamaLeYomAvodaNotAllowed = 163,
        errSidurSummerNotValidForOved = 164,
        errAvodatNahagutNotValid = 165,
        errHmtanaTimeNotValid = 166,
        errHafifaBetweenSidurim = 167,
        errCurrentSidurInPrevSidur = 168,
        errOvedNotExists = 169,
        errVisaNotValid = 170,
        errHasBothSidurEilatAndSidurVisa = 171,
        errOvedPeilutNotValid = 172,
        errSidurHourEndNotValid = 173,
        errEndHourMissing=174,
        errHachtamaYadanitKnisa = 175,
        errHachtamaYadanitYetzia = 176,
        errSidurGriraNotValid=177,
        errMissingKodMevatzaVisa=178,
        errHightValueDakotBefoal=179,
        IsShatHatchalaLetashlumNull = 180,
        IsShatGmarLetashlumNull = 181 
    }

    public enum ZmanHalbashaType
    {
        ZakaiKnisa = 1,
        ZakaiYetiza = 2,
        ZakaiKnisaYetiza = 3,
        LoZakai = 4,
        CardError = 5 //  שגוי סטטוס 
    }
}

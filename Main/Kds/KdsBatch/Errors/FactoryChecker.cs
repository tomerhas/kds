using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Errors
{
    public class FactoryChecker
    {

        public BasicChecker GetInstance(TypeCheck Error, object CurrentInstance, OriginError origion)
        {
            BasicChecker creator = null;
 
            if (creator == null)
            {
                switch (Error)
                {
                        //Day
                    case TypeCheck.errHrStatusNotValid:
                        creator = new DayError1(CurrentInstance);
                        break;
                    case TypeCheck.errSimunNesiaNotValid:
                        creator = new DayError27(CurrentInstance);
                        break;
                    case TypeCheck.errLinaValueNotValid:
                        creator = new DayError30(CurrentInstance);
                        break;
                    case TypeCheck.errHalbashaNotvalid:
                        creator = new DayError36(CurrentInstance);
                        break;
                    case TypeCheck.errShbatHashlamaNotValid:
                        creator = new DayError47(CurrentInstance);
                        break;
                    case TypeCheck.errDuplicateShatYetiza:
                        creator = new DayError103(CurrentInstance);
                        break;
                    case TypeCheck.errOvdaInMachalaNotAllowed:
                        creator = new DayError132(CurrentInstance);
                        break;
                    case TypeCheck.errHafifaBetweenSidurim:
                        creator = new DayError167(CurrentInstance);
                        break;
                    case TypeCheck.errHasBothSidurEilatAndSidurVisa:
                        creator = new DayError171(CurrentInstance);
                        break;
                    case TypeCheck.errOvedPeilutNotValid:
                        creator = new DayError172(CurrentInstance);
                        break;
                    case TypeCheck.errAvodatNahagutNotValid:
                        creator = new DayError165(CurrentInstance);
                        break;
                    case TypeCheck.errNesiaMeshtanaNotDefine:
                        creator = new DayError150(CurrentInstance);
                        break;
                    case TypeCheck.errTimeForPrepareMechineNotValid:
                        if (origion == OriginError.Day)
                            creator = new DayError86(CurrentInstance);
                        else if (origion == OriginError.Sidur)
                            creator = new SidurError86(CurrentInstance);
                        else  creator = new PeilutError86(CurrentInstance);
                        break;
                       
                    //Sidur

                    case TypeCheck.errSidurNotExists:
                        creator = new SidurError9(CurrentInstance);
                        break;
                    case TypeCheck.errStartHourMissing:
                        creator = new SidurError15(CurrentInstance);
                        break;
                    case TypeCheck.errEndHourMissing:
                        creator = new SidurError174(CurrentInstance);
                        break;
                    case TypeCheck.errCharigaZmanHachtamatShaonNotValid:
                        creator = new SidurError33(CurrentInstance);
                        break;
                    case TypeCheck.errPizulHafsakaValueNotValid:
                        creator = new SidurError20(CurrentInstance);
                        break;
                    case TypeCheck.errHashlamaNotValid:
                        creator = new SidurError137(CurrentInstance);
                        break;
                    case TypeCheck.errShabatPizulValueNotValid:
                        creator = new SidurError23(CurrentInstance);
                        break;
                    case TypeCheck.errKmNotExists:
                        creator = new SidurError96(CurrentInstance);
                        break;
                    case TypeCheck.errOutMichsaNotValid:
                        creator = new SidurError118(CurrentInstance);
                        break;
                    case TypeCheck.errOutMichsaInSidurHeadrutNotValid:
                        creator = new SidurError40(CurrentInstance);
                        break;
                    case TypeCheck.errDriverLessonsNumberNotValid:
                        creator = new SidurError136(CurrentInstance);
                        break;
                    case TypeCheck.errZakaiLeCharigaValueNotValid:
                        creator = new SidurError34(CurrentInstance);
                        break;
                    case TypeCheck.errHashlamaForSidurNotValid:
                        creator = new SidurError48(CurrentInstance);
                        break;
                    case TypeCheck.errPitzulMuchadValueNotValid:
                        creator = new SidurError25(CurrentInstance);
                        break;
                    case TypeCheck.errSimunVisaNotValid:
                        creator = new SidurError57(CurrentInstance);
                        break;
                    case TypeCheck.errPizulValueNotValid:
                        creator = new SidurError22(CurrentInstance);
                        break;
                    case TypeCheck.errSidurVisaNotValid:
                        creator = new SidurError58(CurrentInstance);
                        break;
                    case TypeCheck.errSidurHourStartNotValid:
                        creator = new SidurError14(CurrentInstance);
                        break;
                    case TypeCheck.errSidurHourEndNotValid:
                        creator = new SidurError173(CurrentInstance);
                        break;
                    case TypeCheck.errHahlamatHazmanaNotValid:
                        creator = new SidurError49(CurrentInstance);
                        break;
                    case TypeCheck.errMissingSugVisa:
                        creator = new SidurError106(CurrentInstance);
                        break;
                    case TypeCheck.errMissingKodMevatzaVisa:
                        creator = new SidurError178(CurrentInstance);
                        break;
                    case TypeCheck.errAtLeastOnePeilutRequired:
                        creator = new SidurError127(CurrentInstance);
                        break;
                    case TypeCheck.errNotAllowedSidurForEggedTaavora:
                        creator = new SidurError148(CurrentInstance);
                        break;
                    case TypeCheck.errSidurNetzerNotValidForOved:
                        creator = new SidurError124(CurrentInstance);
                        break;
                    case TypeCheck.errSidurAvodaNotValidForMonth:
                        creator = new SidurError160(CurrentInstance);
                        break;
                    case TypeCheck.errSidurNotAllowedInShabaton:
                        creator = new SidurError50(CurrentInstance);
                        break;
                    case TypeCheck.errCharigaValueNotValid:
                        creator = new SidurError32(CurrentInstance);
                        break;
                    case TypeCheck.errSidurSummerNotValidForOved:
                        creator = new SidurError164(CurrentInstance);
                        break;
                    case TypeCheck.errOvedNotAllowedToDoSidurNahagut:
                        creator = new SidurError161(CurrentInstance);
                        break;
                    case TypeCheck.errSidurimHoursNotValid:
                        creator = new SidurError16(CurrentInstance);
                        break;
                    case TypeCheck.errPitzulSidurInShabat:
                        creator = new SidurError24(CurrentInstance);
                        break;
                    case TypeCheck.errHashlamaForComputerWorkerAndAccident:
                        creator = new SidurError45(CurrentInstance);
                        break;
                    case TypeCheck.errTotalHashlamotBiggerThanAllow:
                        creator = new SidurError142(CurrentInstance);
                        break;
                    case TypeCheck.errMiluimAndAvoda:
                        creator = new SidurError156(CurrentInstance);
                        break;
                    case TypeCheck.errMissingNumStore:
                        creator = new SidurError143(CurrentInstance);
                        break;
                    case TypeCheck.errChafifaBesidurNihulTnua:
                        creator = new SidurError152(CurrentInstance);
                        break;
                    case TypeCheck.errHighPremya:
                        creator = new SidurError153(CurrentInstance);
                        break;
                    case TypeCheck.errNegativePremya:
                        creator = new SidurError154(CurrentInstance);
                        break;
                    case TypeCheck.errCurrentSidurInPrevSidur:
                        creator = new SidurError168(CurrentInstance);
                        break;
                    case TypeCheck.errHachtamaYadanitKnisa:
                        creator = new SidurError175(CurrentInstance);
                        break;
                    case TypeCheck.errHachtamaYadanitYetzia:
                        creator = new SidurError176(CurrentInstance);
                        break;
                    case TypeCheck.IsShatHatchalaLetashlumNull:
                        creator = new SidurError180(CurrentInstance);
                        break;
                    case TypeCheck.IsShatGmarLetashlumNull:
                        creator = new SidurError181(CurrentInstance);
                        break;
                    case TypeCheck.errSidurEilatNotValid:
                        creator = new SidurError55(CurrentInstance);
                        break;
                    //case TypeCheck.errTimeForPrepareMechineNotValid:
                    //    creator = new SidurError86(CurrentInstance);
                    //    break;   
                 
                    // Peilut

                    case TypeCheck.errKodNesiaNotExists:
                        creator = new PeilutError81(CurrentInstance);
                        break;
                    case TypeCheck.errMisparSiduriOtoNotExists:
                        creator = new PeilutError139(CurrentInstance);
                        break;
                    case TypeCheck.errElementTimeBiggerThanSidurTime:
                        creator = new PeilutError129(CurrentInstance);
                        break;
                    case TypeCheck.errShatPeilutSmallerThanShatHatchalaSidur:
                        creator = new PeilutError121(CurrentInstance);
                        break;
                    case TypeCheck.errShatPeilutBiggerThanShatGmarSidur:
                        creator = new PeilutError122(CurrentInstance);
                        break;
                    case TypeCheck.errPeilutForSidurNonValid:
                        creator = new PeilutError84(CurrentInstance);
                        break;
                    case TypeCheck.errOtoNoNotExists:
                        creator = new PeilutError69(CurrentInstance);
                        break;
                    case TypeCheck.errLoZakaiLLina:
                        creator = new PeilutError31(CurrentInstance);
                        break;
                    case TypeCheck.errOtoNoExists:
                        creator = new PeilutError68(CurrentInstance);
                        break;
                    case TypeCheck.errTeoodatNesiaNotInVisa:
                        creator = new PeilutError52(CurrentInstance);
                        break;
                    case TypeCheck.errHighValueKisuyTor:
                        creator = new PeilutError87(CurrentInstance);
                        break;
                    case TypeCheck.errElementInSpecialSidurNotAllowed:
                        creator = new PeilutError123(CurrentInstance);
                        break;
                    case TypeCheck.errNesiaInSidurVisaNotAllowed:
                        creator = new PeilutError125(CurrentInstance);
                        break;
                    case TypeCheck.errHmtanaTimeNotValid:
                        creator = new PeilutError166(CurrentInstance);
                        break;
                    case TypeCheck.errSidurNamlakWithoutNesiaCard:
                        creator = new PeilutError13(CurrentInstance);
                        break;
                    case TypeCheck.errCurrentPeilutInPrevPeilut:
                        creator = new PeilutError162(CurrentInstance);
                        break;
                    case TypeCheck.errDuplicateTravle:
                        creator = new PeilutError151(CurrentInstance);
                        break;
                    case TypeCheck.errHightValueDakotBefoal:
                        creator = new PeilutError179(CurrentInstance);
                        break;                
                    //case TypeCheck.errTimeForPrepareMechineNotValid:
                    //    creator = new SidurError86(CurrentInstance);
                    //    break;   

                }
                creator.Error = Error;
            }
            if (creator == null)
                throw new Exception("Error " + Error + " doesn't not defined");
            return creator;
        }
    }
}

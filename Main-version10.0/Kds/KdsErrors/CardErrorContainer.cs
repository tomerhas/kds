using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;
using KdsErrors.ErrosrImpl.DayErrors;
using KdsErrors.ErrosrImpl.SidurErrors ;
using KdsErrors.ErrosrImpl.PeilutErrors;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors
{
    public class CardErrorContainer : ICardErrorContainer
    {
        private Dictionary<ErrorDualKey, ICardError> _errorDic;
        private IUnityContainer _container;

        public CardErrorContainer(IUnityContainer container)
        {
            _container = container;
            _errorDic = new Dictionary<ErrorDualKey, ICardError>();
        }

        public void Add(ICardError cardError)
        {
            var key = new ErrorDualKey(cardError.CardErrorType, cardError.CardErrorSubLevel);
            _errorDic.Add(key, cardError);
        }

        public ICardError this[ErrorDualKey index]
        {
            get { return _errorDic[index]; }
        }

        public void Init()
        {
            Add(_container.Resolve<DayErrorHrStatusValid01>());
            Add(_container.Resolve<DayErrorIsSidurLina30>());
            Add(_container.Resolve<DayErrorIsSimunNesiaValid27>());
            Add(_container.Resolve<DayErrorIsShbatHashlamaValid47>());
            Add(_container.Resolve<DayErrorIsHalbashValid36>());
            Add(_container.Resolve<DayErrorDuplicateShatYetiza103>());
            Add(_container.Resolve<DayErrorIsOvodaInMachalaAllowed132>());
            Add(_container.Resolve<DayErrorIsMatzavOvedNoValidFirstDay192>());
            Add(_container.Resolve<DayErrorHasHafifa167>());
            Add(_container.Resolve<DayErrorHasBothSidurEilatAndSidurVisa171>());
            Add(_container.Resolve<dayErrorIsOvedPeilutValid172>());
            Add(_container.Resolve<DayErrorCheckNumGririotInDay203>());
            Add(_container.Resolve<DayErrorAvodatNahagutValid165>());
            Add(_container.Resolve<DayErrorTimeMechineNosefetInDayNotValid182>());
            Add(_container.Resolve<DayErrorTimeAllMechineInDayNotValid183>());
            Add(_container.Resolve<DayErrorIsNesiaMeshtanaDefine150>());
            Add(_container.Resolve<DayErrorMutamNochechutLessMichsa217>());


            Add(_container.Resolve<SidurErrorSidurNotExists9>());
            Add(_container.Resolve<SidurErrorStartHourMissing15>());
            Add(_container.Resolve<SidurErrorEndHourMissing174>());
            Add(_container.Resolve<SidurErrorSidurCharigaNotValid33>());
            Add(_container.Resolve<SidurErrorPitzulHafsakaNotValid20>());
            Add(_container.Resolve<SidurErrorHashlamaNotValid137>());
            Add(_container.Resolve<SidurErrorShabatPizulVNotValid23>());
            Add(_container.Resolve<SidurErrorKmNotExists96>());
            Add(_container.Resolve<SidurErrorOutMichsaNotValid118>());
            Add(_container.Resolve<SidurErrorOutMichsaNotValid40>());
            Add(_container.Resolve<SidurErrorDriverLessonsNumberValid136>());
            Add(_container.Resolve<SidurErrorZakaiLeChariga34>());
            Add(_container.Resolve<SidurErrorHashlamaForSidurValid48>());
            Add(_container.Resolve<SidurerrorPitzulAndNotZakai25>());
            Add(_container.Resolve<SidurErrorSidurVisaNotValid57>());
            Add(_container.Resolve<SidurErrorOneSidurNotValid22>());
            Add(_container.Resolve<SidurErrorVisaInSidurRagil58>());
            Add(_container.Resolve<SidurErrorSidurStartHourValid14>());
            Add(_container.Resolve<SidurErrorSidurEndHourValid173>());
            Add(_container.Resolve<SidurErrorHashlamatHazmanaValid49>());
            Add(_container.Resolve<SidurErrorSidurVisaMissingSugVisa106>());
            Add(_container.Resolve<SidurErrorMissingKodMevatzeaVisa178>());
            Add(_container.Resolve<SidurErrorOnePeilutExists127>());
            Add(_container.Resolve<SidurErrorSidurAllowedForEggedTaavora148>());
            Add(_container.Resolve<SidurErrorSidurNetzerNotValidForOved124>());
            Add(_container.Resolve<SidurErrorSidurAvodaValidForTaarich160>());
            Add(_container.Resolve<SidurErrorSidurValidInShabaton50>());
            Add(_container.Resolve<SidurErrorCharigaNotValid32>());
            Add(_container.Resolve<SidurErrorSidurSummerValid164>());
            Add(_container.Resolve<SidurErrorSidurNAhagutValid161>());
            Add(_container.Resolve<SidurErrorFirstDayShlilatRishayon195>());
            Add(_container.Resolve<SidurErrorSidurimHoursNotValid16>());
            Add(_container.Resolve<SidirErrorPitzulSidurInShabatValid24>());
            Add(_container.Resolve<SidurErrorHashlamaForComputerAndAccidentValid45>());
            Add(_container.Resolve<SidurErrorTotalHashlamotInCardValid142>());
            Add(_container.Resolve<SidurErrorSidurMiluimAndAvoda156>());
            Add(_container.Resolve<SidurErrorSidurMissingNumStore143>());
            Add(_container.Resolve<SidurErrorChafifaBesidurNihulTnua152>());
            Add(_container.Resolve<SidurErrorHighPremya153>());
            Add(_container.Resolve<SidurErrorNegativePremya154>());
            Add(_container.Resolve<SidurErrorMutamLoBeNahagut186>());
            Add(_container.Resolve<SidurErrorKupaiWithNihulTnua187>());
            Add(_container.Resolve<SidurErrorChofeshAlCheshbonShaotNosafot188>());
            Add(_container.Resolve<SidurErrorCurrentSidurInPrevSidur168>());
            Add(_container.Resolve<SidurErrorHachtamaYadanitKnisaMissing175>());
            Add(_container.Resolve<SidurErrorHachtamaYadanitYetziaMissing176>());
            Add(_container.Resolve<SidurErrorShatHatchalaLetashlumNull180>());
            Add(_container.Resolve<SidurErrorShatGmarLetashlumNull181>());
            Add(_container.Resolve<SidurErrorSidurLoTakefLetarich190>());
            Add(_container.Resolve<SidurErrorIsukNahagImSidurTafkidNoMefyen191>());
            Add(_container.Resolve<SidurErrorDivuachSidurLoMatimLeisuk193>());
            Add(_container.Resolve<SidurErrorDivuachSidurLoMatimLeisuk194>());
            Add(_container.Resolve<SidurErrorHachtamatKnisaLoBmakomHasaka197>());
            Add(_container.Resolve<SidurErrorHachtamatYetziaLoBmakomHasaka198>());
            Add(_container.Resolve<SidurErrorAvodaByemeyTeuna199>());
            Add(_container.Resolve<SidurErrorAvodaByemeyEvel200>());
            Add(_container.Resolve<SidurErrorAvodaByemeyMachala201>());
            Add(_container.Resolve<SidurErrorMachalaLeloIshurwithSidurLetashlum202>());
            Add(_container.Resolve<SidurErrorSidurAsurBeShisiLeoved5Yamim204>());
            Add(_container.Resolve<SidurErrorTipatChalavMealMichsa205>());
            Add(_container.Resolve<SidurErrorOvedMutaamLeloShaotNosafot206>());
            Add(_container.Resolve<SidurErrorShatHatchalaBiggerShatYetzia207>());
            Add(_container.Resolve<SidurErrorMushalETWithSidurNotAllowET208>());
            Add(_container.Resolve<SidurErrorMisparElementimMealHamutar185>());
            Add(_container.Resolve<SidurErrorSidurEilatValid55>());
            Add(_container.Resolve<SidurErrorCntMechineInSidurNotValid184>());
            Add(_container.Resolve<SidurErrorShatHatchalaLetashlumBiggerShatGmar209>());
            Add(_container.Resolve<SidurErrorMichsatMachalaYeledImMugbalut210>());
            Add(_container.Resolve<SidurErrorMachalaMisradHaBitachonLoYachid212>());
            Add(_container.Resolve<SidurErrorMachalatYeledMugbal214>());
            Add(_container.Resolve<SidurErrorOvedLoMushalWithTaavura215>());
            Add(_container.Resolve<SidurErrorOvedLeloHeterEmptyBus216>());
            Add(_container.Resolve<SidurErrorHoursLeTaslumNotValid219>());
            


            Add(_container.Resolve<PeilutErrorIsKodNesiaExists81>());
            Add(_container.Resolve<PeilutErorMisparSiduriOtoNotExists139>());
            Add(_container.Resolve<PeilutErrorIsElementTimeValid129>());
            Add(_container.Resolve<PeilutErrorIsShatPeilutNotValid121>());
            Add(_container.Resolve<PeilutErrorIsShatPeilutNotValid122>());
            Add(_container.Resolve<PeilutErrorIsPeilutInSidurValid84>());
            Add(_container.Resolve<PeilutErrorIsOtoNoValid69>());
            Add(_container.Resolve<PeilutErrorIsZakaiLina31>());
            Add(_container.Resolve<PeilutErrorIsOtoNoExists68>());
            Add(_container.Resolve<PeilutErrorIsTeoodatNesiaValid52>());
            Add(_container.Resolve<PeilutErrorHighValueKisuyTor87>());
            Add(_container.Resolve<PeilutErrorElementInSpecialSidurNotAllowed123>());
            Add(_container.Resolve<PeilutErrorIsNesiaInSidurVisaAllowed125>());
            Add(_container.Resolve<PeilutErrorIsHmtanaTimeValid166>());
            Add(_container.Resolve<PeilutErrorIsSidurNamlakWithoutNesiaCard13>());
            Add(_container.Resolve<PeilutErrorIsCurrentPeilutInPrevPeilut162>());
            Add(_container.Resolve<PeilutErrorIsTimeForPrepareMechineValid86>());
            Add(_container.Resolve<PeilutErrorIsDuplicateTravel151>());
            Add(_container.Resolve<PeilutErrorHightValueDakotBefoal179>());
            Add(_container.Resolve<PeilutErrorKisuyTorLifneyHatchalatSidur189>());
            Add(_container.Resolve<PeilutErrorIsRechevMushbat211>());
            Add(_container.Resolve<PeilutErrorElementNehigaForMatalaLoNahagut213>());
            Add(_container.Resolve<SidurErrorCheckHityzvutMissingLater218>());
            Add(_container.Resolve<PeilutErrorElementNotAllowedInSidurNihulTnua220>());



        }
    }
}

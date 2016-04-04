using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace KdsErrors.FlowManagers.SubFlowManagers
{
    public class OvedSidurErrorFlowManager : BaseErrorFlowManager
    {
        private IUnityContainer _container;
        public OvedSidurErrorFlowManager(IUnityContainer container)
            : base(container)
        {
            _container = container;
        }

        public override void ExecuteFlow(ErrorInputData input, int stage)
        {
            ICardErrorContainer errContainer = _container.Resolve<ICardErrorContainer>();
            switch (stage)
            {
                case 1:
                    ErrorValidationBeforePeilut(input, errContainer);
                    break;
                case 2:
                    ErrorValidationAfterPeilut(input, errContainer);
                    break;
                default:
                    throw new Exception("OvedSidurErrorFlowManager supports only 2 stages");

            }

        }

        private void ErrorValidationBeforePeilut(ErrorInputData input, ICardErrorContainer errContainer)
        {
           
            List<ErrorTypes> errorTypeList = new List<ErrorTypes>();

            errorTypeList.Add(ErrorTypes.errSidurNotExists);//9
            errorTypeList.Add(ErrorTypes.errStartHourMissing);//15
            errorTypeList.Add(ErrorTypes.errEndHourMissing);//174
            errorTypeList.Add(ErrorTypes.errCharigaZmanHachtamatShaonNotValid);//33
            errorTypeList.Add(ErrorTypes.errPizulHafsakaValueNotValid);//20
            errorTypeList.Add(ErrorTypes.errHashlamaNotValid);//137
            errorTypeList.Add(ErrorTypes.errShabatPizulValueNotValid);//23
            errorTypeList.Add(ErrorTypes.errKmNotExists);//96
            errorTypeList.Add(ErrorTypes.errOutMichsaNotValid);//118
            errorTypeList.Add(ErrorTypes.errOutMichsaInSidurHeadrutNotValid);//40
            errorTypeList.Add(ErrorTypes.errDriverLessonsNumberNotValid);//136
            errorTypeList.Add(ErrorTypes.errZakaiLeCharigaValueNotValid);//34
            errorTypeList.Add(ErrorTypes.errHashlamaForSidurNotValid);//48
            errorTypeList.Add(ErrorTypes.errPitzulMuchadValueNotValid);//25
            errorTypeList.Add(ErrorTypes.errSimunVisaNotValid);//57
            errorTypeList.Add(ErrorTypes.errPizulValueNotValid);//22
            errorTypeList.Add(ErrorTypes.errSidurVisaNotValid);//58
            errorTypeList.Add(ErrorTypes.errSidurHourStartNotValid);//14
            errorTypeList.Add(ErrorTypes.errSidurHourEndNotValid);//173
            errorTypeList.Add(ErrorTypes.errShatHatchalaBiggerShatYetzia);//207
            errorTypeList.Add(ErrorTypes.errHahlamatHazmanaNotValid);//49
            errorTypeList.Add(ErrorTypes.errMissingSugVisa);//106
            errorTypeList.Add(ErrorTypes.errMissingKodMevatzaVisa);//178
            errorTypeList.Add(ErrorTypes.errAtLeastOnePeilutRequired);//127
            errorTypeList.Add(ErrorTypes.errNotAllowedSidurForEggedTaavora);//148
            errorTypeList.Add(ErrorTypes.errSidurNetzerNotValidForOved);//124
            errorTypeList.Add(ErrorTypes.errSidurAvodaNotValidForMonth);//160
            errorTypeList.Add(ErrorTypes.errSidurNotAllowedInShabaton);//50
            errorTypeList.Add(ErrorTypes.errCharigaValueNotValid);//32
            errorTypeList.Add(ErrorTypes.errSidurSummerNotValidForOved);//164
            errorTypeList.Add(ErrorTypes.errOvedNotAllowedToDoSidurNahagut);//161
            errorTypeList.Add(ErrorTypes.errFirstDayShlilatRishayon195); //195
            errorTypeList.Add(ErrorTypes.errSidurimHoursNotValid);//16
            errorTypeList.Add(ErrorTypes.errPitzulSidurInShabat);//24
            errorTypeList.Add(ErrorTypes.errHashlamaForComputerWorkerAndAccident);//45
            errorTypeList.Add(ErrorTypes.errTotalHashlamotBiggerThanAllow);//142
            errorTypeList.Add(ErrorTypes.errMiluimAndAvoda); //156
            errorTypeList.Add(ErrorTypes.errMissingNumStore);//143
            errorTypeList.Add(ErrorTypes.errChafifaBesidurNihulTnua);//152
            errorTypeList.Add(ErrorTypes.errHighPremya);//153
            errorTypeList.Add(ErrorTypes.errNegativePremya);//154
            errorTypeList.Add(ErrorTypes.errMutamLoBeNahagutBizeaNahagut); //186
            errorTypeList.Add(ErrorTypes.errKupaiWithNihulTnua);//187
            errorTypeList.Add(ErrorTypes.errChofeshAlCheshbonShaotNosafot);//188
            errorTypeList.Add(ErrorTypes.errCurrentSidurInPrevSidur);//168
            errorTypeList.Add(ErrorTypes.errHachtamaYadanitKnisa);//175
            errorTypeList.Add(ErrorTypes.errHachtamaYadanitYetzia); //176
            errorTypeList.Add(ErrorTypes.IsShatHatchalaLetashlumNull);//180
            errorTypeList.Add(ErrorTypes.IsShatGmarLetashlumNull);//181
            errorTypeList.Add(ErrorTypes.errSidurLoTakefLetaarich);//190
            errorTypeList.Add(ErrorTypes.errIsukNahagImSidurTafkidNoMefyen);//191
            errorTypeList.Add(ErrorTypes.errDivuachSidurLoMatimLeisuk420); //193


            errorTypeList.Add(ErrorTypes.errDivuachSidurLoMatimLeisuk422); //194
            errorTypeList.Add(ErrorTypes.errHachtamatKnisaLoBmakomHasaka197);//197
            errorTypeList.Add(ErrorTypes.errHachtamatYetziaLoBmakomHasaka198);//198
            errorTypeList.Add(ErrorTypes.errAvodaByemeyTeuna199);//199
            errorTypeList.Add(ErrorTypes.errAvodaByemeyEvel200);//200
            errorTypeList.Add(ErrorTypes.errAvodaByemeyMachala201); //201
            errorTypeList.Add(ErrorTypes.errMachalaLeloIshur202);//202
            errorTypeList.Add(ErrorTypes.errSidurAsurBeyomShishiLeoved5Yamim204);//204
            errorTypeList.Add(ErrorTypes.errTipatChalavMealMichsa205);//205
            errorTypeList.Add(ErrorTypes.errOvedMutaamLeloShaotNosafot206);//206
            errorTypeList.Add(ErrorTypes.errMushalETWithSidurNotAllowET);//208
            errorTypeList.Add(ErrorTypes.errShatHatchalaLetashlumBiggerShatGmar);//209
            errorTypeList.Add(ErrorTypes.errMichsatMachalaYeledImMugbalut);//210
            errorTypeList.Add(ErrorTypes.errMachalaMisradHaBitachonLoYachid);//212
            errorTypeList.Add(ErrorTypes.errMachalatYeledMugbal214);//214
            errorTypeList.Add(ErrorTypes.errOvedLoMushalWithTaavura215);//215
            errorTypeList.Add(ErrorTypes.errOvedLeloHeterEmptyBus216);//215


            ExecuteListOfErrors(errContainer, input, errorTypeList, ErrorSubLevel.Sidur);

        }

        private void ErrorValidationAfterPeilut(ErrorInputData input, ICardErrorContainer errContainer)
        {
            List<ErrorTypes> errorTypeList = new List<ErrorTypes>();

            errorTypeList.Add(ErrorTypes.ErrMisparElementimMealHamutar); //185
            errorTypeList.Add(ErrorTypes.errSidurEilatNotValid);//55
            errorTypeList.Add(ErrorTypes.errCntMechineInSidurNotValid);//184

            ExecuteListOfErrors(errContainer, input, errorTypeList, ErrorSubLevel.Sidur);
        }
    }
}

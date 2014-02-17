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
            int iHashlama = 0;

            List<ErrorTypes> errorTypeList = new List<ErrorTypes>();
            errorTypeList.Add(ErrorTypes.errSidurNotExists);
            errorTypeList.Add(ErrorTypes.errStartHourMissing);
            errorTypeList.Add(ErrorTypes.errEndHourMissing);
            errorTypeList.Add(ErrorTypes.errCharigaZmanHachtamatShaonNotValid);
            errorTypeList.Add(ErrorTypes.errPizulHafsakaValueNotValid);
            errorTypeList.Add(ErrorTypes.errHashlamaNotValid);
            errorTypeList.Add(ErrorTypes.errShabatPizulValueNotValid);
            errorTypeList.Add(ErrorTypes.errKmNotExists);
            errorTypeList.Add(ErrorTypes.errOutMichsaNotValid);
            errorTypeList.Add(ErrorTypes.errOutMichsaInSidurHeadrutNotValid);
            errorTypeList.Add(ErrorTypes.errDriverLessonsNumberNotValid);
            errorTypeList.Add(ErrorTypes.errZakaiLeCharigaValueNotValid);
            errorTypeList.Add(ErrorTypes.errHashlamaForSidurNotValid);
            errorTypeList.Add(ErrorTypes.errPitzulMuchadValueNotValid);
            errorTypeList.Add(ErrorTypes.errSimunVisaNotValid);

            errorTypeList.Add(ErrorTypes.errPizulValueNotValid);
            errorTypeList.Add(ErrorTypes.errSidurVisaNotValid);
            errorTypeList.Add(ErrorTypes.errSidurHourStartNotValid);
            errorTypeList.Add(ErrorTypes.errSidurHourEndNotValid);
            errorTypeList.Add(ErrorTypes.errHahlamatHazmanaNotValid);
            errorTypeList.Add(ErrorTypes.errMissingSugVisa);
            errorTypeList.Add(ErrorTypes.errMissingKodMevatzaVisa);
            errorTypeList.Add(ErrorTypes.errAtLeastOnePeilutRequired);
            errorTypeList.Add(ErrorTypes.errNotAllowedSidurForEggedTaavora);
            errorTypeList.Add(ErrorTypes.errSidurNetzerNotValidForOved);
            errorTypeList.Add(ErrorTypes.errSidurAvodaNotValidForMonth);
            
            
            ExecuteListOfErrors(errContainer, input, errorTypeList, ErrorSubLevel.Sidur);
            ICardError err;
         
            //Err 50
            err = errContainer[new ErrorDualKey(ErrorTypes.errSidurNotAllowedInShabaton, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 32
            err = errContainer[new ErrorDualKey(ErrorTypes.errCharigaValueNotValid, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 164
            err = errContainer[new ErrorDualKey(ErrorTypes.errSidurSummerNotValidForOved, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 161
            err = errContainer[new ErrorDualKey(ErrorTypes.errOvedNotAllowedToDoSidurNahagut, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 195
            err = errContainer[new ErrorDualKey(ErrorTypes.errFirstDayShlilatRishayon195, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            if (input.iSidur > 0)//לא נבצע את הבדיקה לסידור הראשון
            {
                //Err 16
                err = errContainer[new ErrorDualKey(ErrorTypes.errSidurimHoursNotValid, ErrorSubLevel.Sidur)];
                err.IsCorrect(input);
                //Err 24
                err = errContainer[new ErrorDualKey(ErrorTypes.errPitzulSidurInShabat, ErrorSubLevel.Sidur)];
                err.IsCorrect(input);
            }
            else
            {
                //Err 45
                err = errContainer[new ErrorDualKey(ErrorTypes.errHashlamaForComputerWorkerAndAccident, ErrorSubLevel.Sidur)];
                err.IsCorrect(input);
            }

            iHashlama = string.IsNullOrEmpty(input.curSidur.sHashlama) ? 0 : int.Parse(input.curSidur.sHashlama);
            if (iHashlama > 0)
            {
                input.iTotalHashlamotForSidur += 1;
                //Err 142
                err = errContainer[new ErrorDualKey(ErrorTypes.errTotalHashlamotBiggerThanAllow, ErrorSubLevel.Sidur)];
                err.IsCorrect(input);
            }

            //Err 156
            err = errContainer[new ErrorDualKey(ErrorTypes.errMiluimAndAvoda, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 143
            err = errContainer[new ErrorDualKey(ErrorTypes.errMissingNumStore, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 152
            err = errContainer[new ErrorDualKey(ErrorTypes.errChafifaBesidurNihulTnua, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 153
            err = errContainer[new ErrorDualKey(ErrorTypes.errHighPremya, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 154
            err = errContainer[new ErrorDualKey(ErrorTypes.errNegativePremya, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 186
            err = errContainer[new ErrorDualKey(ErrorTypes.errMutamLoBeNahagutBizeaNahagut, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 187
            err = errContainer[new ErrorDualKey(ErrorTypes.errKupaiWithNihulTnua, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 188
            err = errContainer[new ErrorDualKey(ErrorTypes.errChofeshAlCheshbonShaotNosafot, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 168
            err = errContainer[new ErrorDualKey(ErrorTypes.errCurrentSidurInPrevSidur, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 175
            err = errContainer[new ErrorDualKey(ErrorTypes.errHachtamaYadanitKnisa, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 176
            err = errContainer[new ErrorDualKey(ErrorTypes.errHachtamaYadanitYetzia, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 180
            err = errContainer[new ErrorDualKey(ErrorTypes.IsShatHatchalaLetashlumNull, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 181
            err = errContainer[new ErrorDualKey(ErrorTypes.IsShatGmarLetashlumNull, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 190
            err = errContainer[new ErrorDualKey(ErrorTypes.errSidurLoTakefLetaarich, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 191
            err = errContainer[new ErrorDualKey(ErrorTypes.errIsukNahagImSidurTafkidNoMefyen, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 193
            err = errContainer[new ErrorDualKey(ErrorTypes.errDivuachSidurLoMatimLeisuk420, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 194
            err = errContainer[new ErrorDualKey(ErrorTypes.errDivuachSidurLoMatimLeisuk422, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 197
            err = errContainer[new ErrorDualKey(ErrorTypes.errHachtamatKnisaLoBmakomHasaka197, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 198
            err = errContainer[new ErrorDualKey(ErrorTypes.errHachtamatYetziaLoBmakomHasaka198, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 199
            err = errContainer[new ErrorDualKey(ErrorTypes.errAvodaByemeyTeuna199, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 200
            err = errContainer[new ErrorDualKey(ErrorTypes.errAvodaByemeyEvel200, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 201
            err = errContainer[new ErrorDualKey(ErrorTypes.errAvodaByemeyMachala201, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            //Err 202
            err = errContainer[new ErrorDualKey(ErrorTypes.errMachalaLeloIshur202, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            ////Err 204
            //err = errContainer[new ErrorDualKey(ErrorTypes.errSidurAsurBeyomShishiLeoved5Yamim204, ErrorSubLevel.Sidur)];
            //err.IsCorrect(input);

            ////Err 205
            //err = errContainer[new ErrorDualKey(ErrorTypes.errTipatChalavMealMichsa205, ErrorSubLevel.Sidur)];
            //err.IsCorrect(input);

            //Err 206
            //err = errContainer[new ErrorDualKey(ErrorTypes.errHachtamatYetziaLoBmakomHasaka198, ErrorSubLevel.Sidur)];
            //err.IsCorrect(input);

            //peiluyot



        }

        private void ErrorValidationAfterPeilut(ErrorInputData input, ICardErrorContainer errContainer)
        {
            //Err 185
            ICardError err = errContainer[new ErrorDualKey(ErrorTypes.ErrMisparElementimMealHamutar, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);

            if (input.iSidur > 0)//לא נבצע את הבדיקה לסידור הראשון
            {
                //Err 55
                err = errContainer[new ErrorDualKey(ErrorTypes.errSidurEilatNotValid, ErrorSubLevel.Sidur)];
                err.IsCorrect(input);
            }

            //Err 184
            err = errContainer[new ErrorDualKey(ErrorTypes.errCntMechineInSidurNotValid, ErrorSubLevel.Sidur)];
            err.IsCorrect(input);
        }
    }
}

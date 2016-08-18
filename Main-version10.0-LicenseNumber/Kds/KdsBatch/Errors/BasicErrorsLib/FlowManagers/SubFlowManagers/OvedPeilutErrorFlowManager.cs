using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace KdsBatch.Errors.BasicErrorsLib.FlowManagers.SubFlowManagers
{
    public class OvedPeilutErrorFlowManager : BaseErrorFlowManager
    {
        private IUnityContainer _container;
        public OvedPeilutErrorFlowManager(IUnityContainer container)
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
                    ErrorValidation(input, errContainer);
                    break;
                default:
                    throw new Exception("OvedPeilutErrorFlowManager supports only 1 stage");
            }
        }

        private void ErrorValidation(ErrorInputData input, ICardErrorContainer errContainer)
        {
            //Err 81
            ICardError err = errContainer[new ErrorDualKey(ErrorTypes.errKodNesiaNotExists, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 139
            err = errContainer[new ErrorDualKey(ErrorTypes.errMisparSiduriOtoNotExists, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 129
            err = errContainer[new ErrorDualKey(ErrorTypes.errElementTimeBiggerThanSidurTime, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 121
            err = errContainer[new ErrorDualKey(ErrorTypes.errShatPeilutSmallerThanShatHatchalaSidur, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 84
            err = errContainer[new ErrorDualKey(ErrorTypes.errPeilutForSidurNonValid, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 69
            err = errContainer[new ErrorDualKey(ErrorTypes.errOtoNoNotExists, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 31
            err = errContainer[new ErrorDualKey(ErrorTypes.errLoZakaiLLina, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 68
            err = errContainer[new ErrorDualKey(ErrorTypes.errOtoNoExists, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 52
            err = errContainer[new ErrorDualKey(ErrorTypes.errTeoodatNesiaNotInVisa, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 87
            err = errContainer[new ErrorDualKey(ErrorTypes.errHighValueKisuyTor, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 123
            err = errContainer[new ErrorDualKey(ErrorTypes.errElementInSpecialSidurNotAllowed, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 125
            err = errContainer[new ErrorDualKey(ErrorTypes.errNesiaInSidurVisaNotAllowed, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 166
            err = errContainer[new ErrorDualKey(ErrorTypes.errHmtanaTimeNotValid, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 13
            err = errContainer[new ErrorDualKey(ErrorTypes.errSidurNamlakWithoutNesiaCard, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 162
            err = errContainer[new ErrorDualKey(ErrorTypes.errCurrentPeilutInPrevPeilut, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 86
            err = errContainer[new ErrorDualKey(ErrorTypes.errTimeMechineInPeilutNotValid, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 151
            err = errContainer[new ErrorDualKey(ErrorTypes.errDuplicateTravle, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 179
            err = errContainer[new ErrorDualKey(ErrorTypes.errHightValueDakotBefoal, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);

            //Err 189
            err = errContainer[new ErrorDualKey(ErrorTypes.errKisuyTorLifneyHatchalatSidur, ErrorSubLevel.Peilut)];
            err.IsCorrect(input);
        }
    }
}
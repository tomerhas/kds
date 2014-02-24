using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace KdsErrors.FlowManagers.SubFlowManagers
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
            List<ErrorTypes> errorTypeList = new List<ErrorTypes>();

            errorTypeList.Add(ErrorTypes.errKodNesiaNotExists); //81
            errorTypeList.Add(ErrorTypes.errMisparSiduriOtoNotExists); //139
            errorTypeList.Add(ErrorTypes.errElementTimeBiggerThanSidurTime);//129
            errorTypeList.Add(ErrorTypes.errShatPeilutSmallerThanShatHatchalaSidur);//121
            errorTypeList.Add(ErrorTypes.errPeilutForSidurNonValid); //84
            errorTypeList.Add(ErrorTypes.errOtoNoNotExists); //69
            errorTypeList.Add(ErrorTypes.errLoZakaiLLina);//31
            errorTypeList.Add(ErrorTypes.errOtoNoExists);//68
            errorTypeList.Add(ErrorTypes.errTeoodatNesiaNotInVisa); //52
            errorTypeList.Add(ErrorTypes.errHighValueKisuyTor);//87
            errorTypeList.Add(ErrorTypes.errElementInSpecialSidurNotAllowed);//123
            errorTypeList.Add(ErrorTypes.errNesiaInSidurVisaNotAllowed); //125
            errorTypeList.Add(ErrorTypes.errHighValueKisuyTor);//166
            errorTypeList.Add(ErrorTypes.errSidurNamlakWithoutNesiaCard);//13
            errorTypeList.Add(ErrorTypes.errCurrentPeilutInPrevPeilut); //162
            errorTypeList.Add(ErrorTypes.errTimeMechineInPeilutNotValid);//86
            errorTypeList.Add(ErrorTypes.errDuplicateTravle);//151
            errorTypeList.Add(ErrorTypes.errHightValueDakotBefoal);//179
            errorTypeList.Add(ErrorTypes.errKisuyTorLifneyHatchalatSidur);//189

            ExecuteListOfErrors(errContainer, input, errorTypeList, ErrorSubLevel.Peilut);

        }
    }
}
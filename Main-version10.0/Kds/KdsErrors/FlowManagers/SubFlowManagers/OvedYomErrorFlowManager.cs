using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace KdsErrors.FlowManagers.SubFlowManagers
{
    public class OvedYomErrorFlowManager : BaseErrorFlowManager
    {

        private IUnityContainer _container;

        public OvedYomErrorFlowManager(IUnityContainer container)
            : base(container)
        {
            _container = container;
        }

        public override void ExecuteFlow(ErrorInputData inputData, int stage)
        {

            ICardErrorContainer errContainer = _container.Resolve<ICardErrorContainer>();

            switch (stage)
            {
                case 1:
                    ErrorValidationBeforeSidur(inputData, errContainer);
                    break;
                case 2:
                    ErrorValidationAfterSidur(inputData, errContainer);
                    break;
                default:
                    throw new Exception("stage Yom supports only 2 stages");
            }

        }

        private void ErrorValidationBeforeSidur(ErrorInputData inputData, ICardErrorContainer errContainer)
        {
            List<ErrorTypes> errorTypeList = new List<ErrorTypes>();

            errorTypeList.Add(ErrorTypes.errHrStatusNotValid); //1
            errorTypeList.Add(ErrorTypes.errLinaValueNotValid); //30
            errorTypeList.Add(ErrorTypes.errSimunNesiaNotValid);//27
            errorTypeList.Add(ErrorTypes.errShbatHashlamaNotValid);//47
    
            ExecuteListOfErrors(errContainer, inputData, errorTypeList, ErrorSubLevel.Yomi);
           

            if (inputData.htEmployeeDetails.Count > 0)
            {
                errorTypeList = new List<ErrorTypes>();

                errorTypeList.Add(ErrorTypes.errHalbashaNotvalid); //36
                errorTypeList.Add(ErrorTypes.errDuplicateShatYetiza); //103
                errorTypeList.Add(ErrorTypes.errOvdaInMachalaNotAllowed);//132
                errorTypeList.Add(ErrorTypes.errMatzavOvedNotValidFirstDay);//192
                errorTypeList.Add(ErrorTypes.errHafifaBetweenSidurim); //167
                errorTypeList.Add(ErrorTypes.errHasBothSidurEilatAndSidurVisa); //171
                errorTypeList.Add(ErrorTypes.errOvedPeilutNotValid);//172
                errorTypeList.Add(ErrorTypes.errConenutGriraMealHamutar);//203

                ExecuteListOfErrors(errContainer, inputData, errorTypeList, ErrorSubLevel.Yomi);
            }
        }

        private void ErrorValidationAfterSidur(ErrorInputData inputData, ICardErrorContainer errContainer)
        {
            if (inputData.htEmployeeDetails.Count > 0)
            {

                List<ErrorTypes> errorTypeList = new List<ErrorTypes>();

                //if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))//165
                errorTypeList.Add(ErrorTypes.errAvodatNahagutNotValid); //165
                errorTypeList.Add(ErrorTypes.errTimeMechineNosefetInDayNotValid); //182
                errorTypeList.Add(ErrorTypes.errTimeAllMechineInDayNotValid);//183
                errorTypeList.Add(ErrorTypes.errNesiaMeshtanaNotDefine);//150
                errorTypeList.Add(ErrorTypes.errMutamNahagutNochechutLessMichsa);//217

                ExecuteListOfErrors(errContainer, inputData, errorTypeList, ErrorSubLevel.Yomi);
  
            }
        }


    }
}

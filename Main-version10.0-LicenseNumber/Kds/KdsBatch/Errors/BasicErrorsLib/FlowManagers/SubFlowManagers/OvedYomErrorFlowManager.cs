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

namespace KdsBatch.Errors.BasicErrorsLib.FlowManagers.SubFlowManagers
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
            //Err 1
            ICardError err = errContainer[new ErrorDualKey(ErrorTypes.errHrStatusNotValid, ErrorSubLevel.Yomi)];
            err.IsCorrect(inputData);
            //Err 30
            err = errContainer[new ErrorDualKey(ErrorTypes.errLinaValueNotValid, ErrorSubLevel.Yomi)];
            err.IsCorrect(inputData);
            //Err 27
            err = errContainer[new ErrorDualKey(ErrorTypes.errSimunNesiaNotValid, ErrorSubLevel.Yomi)];
            err.IsCorrect(inputData);
            //Err 47
            err = errContainer[new ErrorDualKey(ErrorTypes.errShbatHashlamaNotValid, ErrorSubLevel.Yomi)];
            err.IsCorrect(inputData);

            if (inputData.htEmployeeDetails.Count > 0)
            {

                //Err 36
                err = errContainer[new ErrorDualKey(ErrorTypes.errHalbashaNotvalid, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 103
                err = errContainer[new ErrorDualKey(ErrorTypes.errDuplicateShatYetiza, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 132
                err = errContainer[new ErrorDualKey(ErrorTypes.errOvdaInMachalaNotAllowed, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 192
                err = errContainer[new ErrorDualKey(ErrorTypes.errMatzavOvedNotValidFirstDay, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 167
                err = errContainer[new ErrorDualKey(ErrorTypes.errHafifaBetweenSidurim, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 171
                err = errContainer[new ErrorDualKey(ErrorTypes.errHasBothSidurEilatAndSidurVisa, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 172
                err = errContainer[new ErrorDualKey(ErrorTypes.errOvedPeilutNotValid, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 203
                err = errContainer[new ErrorDualKey(ErrorTypes.errConenutGriraMealHamutar, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);



            }
        }

        private void ErrorValidationAfterSidur(ErrorInputData inputData, ICardErrorContainer errContainer)
        {
            if (inputData.htEmployeeDetails.Count > 0)
            {
                //if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                //{
                //Err 165

                ICardError err = errContainer[new ErrorDualKey(ErrorTypes.errAvodatNahagutNotValid, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);
                //}

                //Err 182
                err = errContainer[new ErrorDualKey(ErrorTypes.errTimeMechineNosefetInDayNotValid, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);

                //Err 183
                err = errContainer[new ErrorDualKey(ErrorTypes.errTimeAllMechineInDayNotValid, ErrorSubLevel.Yomi)];
                err.IsCorrect(inputData);


                if (inputData.htEmployeeDetails.Count > 0)
                {
                    //Err 150
                    err = errContainer[new ErrorDualKey(ErrorTypes.errNesiaMeshtanaNotDefine, ErrorSubLevel.Yomi)];
                    err.IsCorrect(inputData);
                }
            }
        }


    }
}

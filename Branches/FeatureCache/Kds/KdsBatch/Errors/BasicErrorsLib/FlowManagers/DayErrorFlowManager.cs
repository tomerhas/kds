using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.Managers.FlowManagers;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.FlowManagers
{
    public class DayErrorFlowManager : IDayErrorFlowManager
    {
        private IUnityContainer _container;
        public DayErrorFlowManager(IUnityContainer container)
        {
            _container = container;
        }

        public void ValidateDayErrors(ErrorInputData input)
        {
            var ovedDetails = _container.Resolve<IOvedManager>().GetOvedDetails(input.iMisparIshi, input.CardDate);

            var cardErrorContainer = _container.Resolve<ICardErrorContainer>();

            cardErrorContainer[ErrorTypes.errHrStatusNotValid].IsCorrect(input);
            cardErrorContainer[ErrorTypes.errSimunNesiaNotValid].IsCorrect(input);


        }
    }
}

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
    public abstract class BaseErrorFlowManager : ISubErrorFlowManager
    {
        private IUnityContainer _container;
        public BaseErrorFlowManager(IUnityContainer container)
        {
            _container = container;
        }

        protected void ExecuteListOfErrors(ICardErrorContainer errContainer, ErrorInputData input, List<ErrorTypes> errorList, ErrorSubLevel subLevelType)
        {
            errorList.ForEach(x => 
            {
                ICardError err = errContainer[new ErrorDualKey(x, subLevelType)];
                err.IsCorrect(input);
            });
        }

        public abstract void ExecuteFlow(ErrorInputData input, int stage);
        
    }
}

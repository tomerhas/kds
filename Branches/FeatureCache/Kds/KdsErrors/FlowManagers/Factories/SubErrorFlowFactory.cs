using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsErrors.FlowManagers.SubFlowManagers;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.FlowManagers.Factories
{
    public class SubErrorFlowFactory : ISubErrorFlowFactory
    {
        private IUnityContainer _container;
        public SubErrorFlowFactory(IUnityContainer container)
        {
            _container = container;
        }

        public ISubErrorFlowManager GetFlowManager(SubFlowManagerTypes managerType)
        {
            switch (managerType)
            {
                case SubFlowManagerTypes.OvedYomFlowManager:
                    return _container.Resolve<OvedYomErrorFlowManager>();
                case SubFlowManagerTypes.OvedSidurErrorFlowManager:
                    return _container.Resolve<OvedSidurErrorFlowManager>();
                case SubFlowManagerTypes.OvedPeilutErrorFlowManager:
                    return _container.Resolve<OvedPeilutErrorFlowManager>();
                default:
                    throw new Exception("This type of SubFlowManagerTypes is not supported in SubErrorFlowFactory");
            }
        }
    }
}

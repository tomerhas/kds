using KDSBLLogic.DAL;
using KDSBLLogic.Logs;
using KDSBLLogic.Managers;
using KDSCache.Implement;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Logs;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Managers.Security;
using KDSCommon.Interfaces.Shinuyim;
using KdsLibrary;
using KdsLibrary.KDSLogic.DAL;
using KdsLibrary.KDSLogic.Managers;
using KdsShinuyim.FlowManager;
using KdsWorkCard.Managers;
using KDSWorkCardTester.Mocks;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSWorkCardTester.Managers
{
    [TestClass]
    public class SidurManagerTester
    {

        IUnityContainer container;
        [TestInitialize]
        public void Init()
        {
            container = new UnityContainer();
            InitServiceLocator(container);

            container.RegisterInstance<IKDSCacheManager>(container.Resolve<KDSCacheManager>());
            var manager = new KDSAgedQueueParameters();
            manager.Init(20);
            container.RegisterInstance<IKDSAgedQueueParameters>(manager);

            container.RegisterType<IOvedManager, OvedManager>();
            container.RegisterType<ISidurManager, SidurManager>();
            container.RegisterType<IPeilutManager, PeilutManager>();
            container.RegisterType<IWorkCardSidurManager, WorkCardSidurManager>();
            container.RegisterType<IParametersManager, ParametersManager>();
            container.RegisterType<IKavimManager, KavimManager>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<ILogBakashot, LogBakashot>();
            container.RegisterType<IShinuyimManager, ShinuyimManager>();
            container.RegisterType<IShinuyimFlowManager, ShinuyimFlowManager>();

            //DAL
            container.RegisterType<IOvedDAL, OvedDAL>();
            container.RegisterType<IKavimDAL, KavimDAL>();
            container.RegisterType<ISidurDAL, SidurDAL>();
            container.RegisterType<IPeilutDAL, PeilutDAL>();
            container.RegisterType<IPeilutDAL, PeilutDAL>();
            container.RegisterType<IShinuyimDAL, ShinuyimDAL>();
            container.RegisterType<IParametersDAL, ParametersDAL>();
            container.RegisterType<ILogDAL, LogDAL>();
            InitCacheItems(container);

            //Mocks
            container.RegisterType<ILoginUserManager, LoggedInUserMock>();
        }

        private void InitServiceLocator(IUnityContainer container)
        {
            UnityServiceLocator serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private void InitCacheItems(IUnityContainer container)
        {
            var builder = container.Resolve<CacheBuilder>();
            builder.Init();
        }

        //[TestMethod]
        //public void GetWorkCardSidurimTester()
        //{ 
        //    int misparIshi = 65929;
        //    DateTime cardDate = new DateTime(2014,6,5);
        //    var sidurim = container.Resolve<IWorkCardSidurManager>().GetSidurim(misparIshi, cardDate);
        //    string json = JsonConvert.SerializeObject(sidurim);
        //    string res = json;
        //}

        

    }
}

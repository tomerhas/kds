using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSBLLogic.DAL;
using KDSBLLogic.Logs;
using KDSBLLogic.Managers;
using KDSCache.Implement;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Logs;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Shinuyim;
using KdsErrors;
using KdsErrors.FlowManagers;
using KdsErrors.FlowManagers.Factories;
using KdsLibrary;
using KdsLibrary.KDSLogic.DAL;
using KdsLibrary.KDSLogic.Managers;
using KdsShinuyim.FlowManager;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KDSBatchTester
{
    public class Bootstraper
    {
        public IUnityContainer Init()
        {
            IUnityContainer container = new UnityContainer();
            //Register interfaces

            //container.RegisterInstance<ISimpleCacheManager<CachedItems>>(new SimpleCacheManager<CachedItems>());//the object will be singelton

            container.RegisterInstance<IKDSCacheManager>(container.Resolve<KDSCacheManager>());
            var manager = new KDSAgedQueueParameters();
            manager.Init(20);

            container.RegisterInstance<IKDSAgedQueueParameters>(manager);
            container.RegisterInstance<IMailManager>(container.Resolve<MailManager>());
            //container.RegisterInstance<ICacheBuilder>(container.Resolve<CacheBuilder>());

            //Managers
            container.RegisterType<IParametersManager, ParametersManager>();
            container.RegisterType<IOvedManager, OvedManager>();
            container.RegisterType<ISidurManager, SidurManager>();
            container.RegisterType<IPeilutManager, PeilutManager>();
            container.RegisterType<IKavimManager, KavimManager>();
            container.RegisterType<IErrorFlowManager, ErrorFlowManager>();
            container.RegisterType<ISubErrorFlowFactory, SubErrorFlowFactory>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IShinuyimManager, ShinuyimManager>();
            container.RegisterType<IShinuyimFlowManager, ShinuyimFlowManager>();
            container.RegisterType<ILogBakashot, LogBakashot>();
            container.RegisterType<IClockManager, ClockManager>();

            //Containers

            //DAL
            container.RegisterType<IOvedDAL, OvedDAL>();
            container.RegisterType<IKavimDAL, KavimDAL>();
            container.RegisterType<ISidurDAL, SidurDAL>();
            container.RegisterType<IPeilutDAL, PeilutDAL>();
            container.RegisterType<IPeilutDAL, PeilutDAL>();
            container.RegisterType<IShinuyimDAL, ShinuyimDAL>();
            container.RegisterType<IParametersDAL, ParametersDAL>();
            container.RegisterType<ILogDAL, LogDAL>();
            container.RegisterType<IClockDAL, ClockDAL>();

            //var manager = container.Resolve<ISimpleCacheManager<int>>();
            //var item = container.Resolve<ISimpleCacheManager<string>>();
            InitServiceLocator(container);
            InitCacheItems(container);

            //Init card error
            ICardErrorContainer cardErrorContainer = container.Resolve<CardErrorContainer>();
            container.RegisterInstance<ICardErrorContainer>(cardErrorContainer);
            cardErrorContainer.Init();

            return container;

        }

        private void InitCacheItems(IUnityContainer container)
        {
            var builder = container.Resolve<CacheBuilder>();
            builder.Init();
        }

        private void InitServiceLocator(IUnityContainer container)
        {
            UnityServiceLocator serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }
    }
}

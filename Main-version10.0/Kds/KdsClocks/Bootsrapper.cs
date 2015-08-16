
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using CacheInfra.Interfaces;
using CacheInfra.Implement;
using KDSCommon.Enums;
//using KDSCache.Implement;
using KDSCommon.Interfaces;
using KdsLibrary;
using KDSCommon.Interfaces.Managers;
//using KdsBatch;
using KDSCommon.Interfaces.DAL;
using KdsLibrary.KDSLogic.DAL;
using KDSCommon.Interfaces.Managers.FlowManagers;
//using KdsErrors;
using KdsLibrary.KDSLogic.Managers;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Logs;
using KDSBLLogic.Logs;
using KDSCommon.Interfaces.Shinuyim;
using KDSBLLogic.DAL;
using KDSBLLogic.Managers;
using KdsShinuyim;
//using KdsShinuyim.FlowManager;
using KDSCache.Implement;


/// <summary>
/// Summary description for Bootstrapper
/// </summary>
/// 
namespace KdsClocks
{

    public class Bootstrapper
    {
        public Bootstrapper()
        {

        }

        public void Init()
        {
            IUnityContainer container = new UnityContainer();
            //Register interfaces

            //container.RegisterInstance<ISimpleCacheManager<CachedItems>>(new SimpleCacheManager<CachedItems>());//the object will be singelton

            container.RegisterInstance<IKDSCacheManager>(container.Resolve<KDSCacheManager>());
            var manager = new KDSAgedQueueParameters();
            manager.Init(20);

          //  container.RegisterInstance<IKDSAgedQueueParameters>(manager);
           // container.RegisterInstance<IMailManager>(container.Resolve<MailManager>());
            //container.RegisterInstance<ICacheBuilder>(container.Resolve<CacheBuilder>());

            //Managers
            container.RegisterType<IParametersManager, ParametersManager>();
            container.RegisterType<IOvedManager, OvedManager>();
            container.RegisterType<ISidurManager, SidurManager>();
            container.RegisterType<IPeilutManager, PeilutManager>();
            container.RegisterType<IKavimManager, KavimManager>();
          //  container.RegisterType<IErrorFlowManager, ErrorFlowManager>();
          //  container.RegisterType<ISubErrorFlowFactory, SubErrorFlowFactory>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IShinuyimManager, ShinuyimManager>();
          //  container.RegisterType<IShinuyimFlowManager, ShinuyimFlowManager>();
            container.RegisterType<ILogBakashot, LogBakashot>();
            container.RegisterType<IClockManager, ClockManager>();
            //Containers

            //DAL
            //container.RegisterType<IOvedDAL, OvedDAL>();
            //container.RegisterType<IKavimDAL, KavimDAL>();
            //container.RegisterType<ISidurDAL, SidurDAL>();
            //container.RegisterType<IPeilutDAL, PeilutDAL>();
            //container.RegisterType<IPeilutDAL, PeilutDAL>();
            //container.RegisterType<IShinuyimDAL, ShinuyimDAL>();
            //container.RegisterType<IParametersDAL, ParametersDAL>();
            container.RegisterType<ILogDAL, LogDAL>();
            container.RegisterType<IClockDAL, ClockDAL>();

            //var manager = container.Resolve<ISimpleCacheManager<int>>();
            //var item = container.Resolve<ISimpleCacheManager<string>>();
            InitServiceLocator(container);
            InitCacheItems(container);

            //Init card error
            //ICardErrorContainer cardErrorContainer = container.Resolve<CardErrorContainer>();
            //container.RegisterInstance<ICardErrorContainer>(cardErrorContainer);
            //cardErrorContainer.Init();

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

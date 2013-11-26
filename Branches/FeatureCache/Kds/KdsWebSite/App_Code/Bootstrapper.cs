using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using CacheInfra.Interfaces;
using CacheInfra.Implement;
using KDSCommon.Enums;
using KDSCache.Implement;
using KDSCommon.Interfaces;
using KdsLibrary;
using KDSCommon.Interfaces.Managers;
using KdsBatch;
using KDSCommon.Interfaces.DAL;
using KdsLibrary.KDSLogic.DAL;
using KDSCommon.Interfaces.Managers.FlowManagers;
using KdsBatch.Errors.BasicErrorsLib.FlowManagers;
using KdsBatch.Errors.BasicErrorsLib;


/// <summary>
/// Summary description for Bootstrapper
/// </summary>
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
        IKDSCacheManager kdsManager = container.Resolve<KDSCacheManager>();

        //container.RegisterInstance<IKDSCacheManager>(new KDSCacheManager());
        container.RegisterInstance<IKDSCacheManager>(kdsManager);
        var manager= new KDSAgedQueueParameters();
        manager.Init(20);
        container.RegisterInstance<IKDSAgedQueueParameters>(manager);
        //container.RegisterInstance<ICacheBuilder>(container.Resolve<CacheBuilder>());

        //Managers
        container.RegisterType<IParametersManager, ParametersManager>();
        container.RegisterType<IOvedManager, OvedManager>();

        container.RegisterType<IDayErrorFlowManager, DayErrorFlowManager>();
        //Containers
        ICardErrorContainer cardErrorContainer = container.Resolve<CardErrorContainer>();
        container.RegisterInstance<ICardErrorContainer>(cardErrorContainer);
        cardErrorContainer.Init();
        //DAL
        container.RegisterType<IOvedDAL, OvedDAL>();
        
        //var manager = container.Resolve<ISimpleCacheManager<int>>();
        //var item = container.Resolve<ISimpleCacheManager<string>>();
        InitServiceLocator(container);

        InitCacheItems(container);
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
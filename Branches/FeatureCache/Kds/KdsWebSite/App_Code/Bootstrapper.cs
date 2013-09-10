using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using CacheInfra.Interfaces;
using CacheInfra.Implement;
using KDSCommon.Enums;
using KDSCache.Interfaces;
using KDSCache.Implement;


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
        container.RegisterInstance<IKDSCacheManager>(new KDSCacheManager());
        var manager= new KDSAgedQueueParameters();
        manager.Init(3);
        container.RegisterInstance<IKDSAgedQueueParameters>(manager);

        container.RegisterType<ICacheBuilder,CacheBuilder>();
        
        
        //var manager = container.Resolve<ISimpleCacheManager<int>>();
        //var item = container.Resolve<ISimpleCacheManager<string>>();
        InitServiceLocator(container);

        InitCacheItems(container);
    }

    private void InitCacheItems(IUnityContainer container)
    {
        var builder = container.Resolve<ICacheBuilder>();
        builder.Init();
    }

    private void InitServiceLocator(IUnityContainer container)
    {
        UnityServiceLocator serviceLocator = new UnityServiceLocator(container);
        ServiceLocator.SetLocatorProvider(() => serviceLocator);
    }
}
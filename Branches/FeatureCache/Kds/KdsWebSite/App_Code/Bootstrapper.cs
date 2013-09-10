using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using CacheInfra.Interfaces;
using CacheInfra.Implement;


/// <summary>
/// Summary description for Bootstrapper
/// </summary>
public class Bootstrapper
{
    public Bootstrapper()
    {

    }

    public void Run()
    {
        IUnityContainer container = new UnityContainer();
        //Register interfaces
        container.RegisterInstance<ISimpleCacheManager<int>>(new SimpleCacheManager<int>());

        //var item = container.Resolve<ISimpleCacheManager<string>>();
        InitServiceLocator(container);
    }

    private void InitServiceLocator(IUnityContainer container)
    {
        UnityServiceLocator serviceLocator = new UnityServiceLocator(container);
        ServiceLocator.SetLocatorProvider(() => serviceLocator);
    }
}
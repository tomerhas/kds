﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheInfra.Interfaces;
using System.Runtime.Caching;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;

namespace CacheInfra.Implement
{
    public class SimpleCacheManager<TKey> : ISimpleCacheManager<TKey>
    {
        private ObjectCache _cache = null; 
        public SimpleCacheManager()
        {
            _cache = MemoryCache.Default;
            //var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
        }

        public object GetCacheItemObject(TKey key)
        {
            return _cache.Get(key.ToString());
        }

        public TItemType GetCacheItem<TItemType>(TKey key) where TItemType: class
        {
            return _cache.Get(key.ToString()) as TItemType;
        }

        public void AddItem(TKey key, object item)
        {
            _cache.Set(key.ToString(), item, new CacheItemPolicy());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheInfra.Interfaces;

namespace CacheInfra.Implement.AgedQueue    
{
    public class AgedQueueList<TKey,TValue> : IAgedQueueList<TKey,TValue> where TValue :class 
    {
        private List<AgedQueue<TKey, TValue>> _agedList;
        private int _maxItems;
        private object _syncObj = new object();

        public AgedQueueList()
        {
            _agedList = new List<AgedQueue<TKey, TValue>>();
            _maxItems = 20;
        }

        public void Init(int maxItems)
        {
            _maxItems = maxItems;
        }
        
        public void Add(TValue item, TKey key)
        {
            lock (_syncObj)
            {
                if (_agedList.Count < _maxItems)
                {
                    CreateAgedItem(item, key);
                }
                else
                {
                    AgedQueue<TKey, TValue> agedItem = FindCandidateForRemove();
                    _agedList.Remove(agedItem);
                    CreateAgedItem(item, key);
                }
            }
        }

        private AgedQueue<TKey, TValue> FindCandidateForRemove()
        {
            return _agedList.OrderBy(x => x.LastUpdated).First();
        }

        private void CreateAgedItem(TValue item, TKey key)
        {
            AgedQueue<TKey, TValue> agedItem = new AgedQueue<TKey, TValue>();
            agedItem.Value = item;
            agedItem.Key = key;
            agedItem.LastUpdated = DateTime.Now;
            _agedList.Add(agedItem);
        }

        public TValue GetItem(TKey key) 
        {
            lock (_syncObj)
            {
                var item = _agedList.SingleOrDefault(x => x.Key.Equals(key));
                if (item == null)
                    return null;
                return item.Value; 
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsLibrary.Utils
{
    /// <summary>
    /// Defines value of Type Int32 to be matched 
    /// in List<> by MatchKeyList object 
    /// </summary>
    public interface IMatchKey
    {
        int Key { get; }
    }

    /// <summary>
    /// Defines value of Type String to be matched 
    /// in List<> by MatchNameList object 
    /// </summary>
    public interface IMatchName
    {
        string Name { get; }
    }
    
    /// <summary>
    /// Extends Generics List<> providing match by key or name using indexer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class MatchList<T> 
    {
        private List<T> _list;
        protected int _searchKey;
        protected string _searchName;
        public MatchList(List<T> list)
	    {
            _list = list;
	    }
        protected abstract bool MatchItem(T item);
       
        public T FindItem(int itemId)
        {
            _searchKey = itemId;
            return _list.Find(MatchItem);
        }
        public T FindItem(string itemName)
        {
            _searchName = itemName;
            return _list.Find(MatchItem);
        }
        public T this[int itemId]
        {
            get { return FindItem(itemId); }
        }
        public T this[string itemName]
        {
            get { return FindItem(itemName); }
        }
        public List<T> Items
        {
            get { return _list; }
        }
    }

    /// <summary>
    /// Extends Generics List<> providing match by value of Type Int32 using indexer
    /// </summary>
    /// <typeparam name="T">class implements IMatchKey interface</typeparam>
    [Serializable]
    public class MatchKeyList<T> : MatchList<T> where T : IMatchKey
    {
        public MatchKeyList(List<T> list)
            : base(list)
        {
        }

        protected override bool MatchItem(T item)
        {
            return item.Key.ToString().Equals(_searchKey.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
    }

    /// <summary>
    /// Extends Generics List<> providing match by value of Type String using indexer
    /// </summary>
    /// <typeparam name="T">class implements IMatchName interface</typeparam>
    [Serializable]
    public class MatchNameList<T> : MatchList<T> where T : IMatchName
    {
        public MatchNameList(List<T> list)
            : base(list)
        {
        }
        protected override bool MatchItem(T item)
        {
            return item.Name.Equals(_searchName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

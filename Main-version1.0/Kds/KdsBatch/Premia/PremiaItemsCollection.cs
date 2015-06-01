using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Premia
{
    /// <summary>
    /// Represents a collection of PremiaItem instances
    /// </summary>
    public class PremiaItemsCollection
    {
        #region Fields
        private List<PremiaItem> _items; 
        #endregion

        #region Constractor
        public PremiaItemsCollection()
        {
            _items = new List<PremiaItem>();
        } 
        #endregion

        #region Properties
        public IEnumerable<PremiaItem> Items { get { return _items; } } 
        #endregion

        #region Methods
        public void AddItem(PremiaItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(PremiaItem item)
        {
            _items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        #endregion
        
    }
}

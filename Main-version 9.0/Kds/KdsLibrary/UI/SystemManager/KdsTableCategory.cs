using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.Utils;
using KdsLibrary.DAL;
using System.Data;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Represents Category of System tables
    /// for navigation purposes
    /// </summary>
    public class KdsTableCategory 
    {
        #region Fields
        private int _categoryID;
        private string _categoryName;
        private MatchNameList<TableCategoryItem> _items; 
        #endregion

        #region Constractor
        public KdsTableCategory(int categoryID)
        {
            _categoryID = categoryID;
            _categoryName = String.Empty;
            Init();
        } 
        #endregion

        #region Methods
        private void Init()
        {
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_kod_kategoria", ParameterType.ntOracleInteger,
                _categoryID, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_SYSMAN.get_kategoria_tavla", ref dt);
            List<TableCategoryItem> list =
                new List<TableCategoryItem>(dt.Rows.Count);
            if (dt.Rows.Count > 0) _categoryName = dt.Rows[0]["TEUR_KATEGORIA"].ToString();
            foreach (DataRow dr in dt.Rows)
            {
                TableCategoryItem item = new TableCategoryItem();
                item.TableName = dr["shem_tavla_db"].ToString().Trim();
                item.TableTitle = dr["teur_tavla"].ToString();
                list.Add(item);
            }
            _items = new MatchNameList<TableCategoryItem>(list);
        } 
        #endregion

        #region Properties
        public int CategoryID
        {
            get { return _categoryID; }
        }

        public string CategoryName
        {
            get { return _categoryName; }
        }

        public MatchNameList<TableCategoryItem> TableList
        {
            get { return _items; }
        } 
        #endregion
    }

    /// <summary>
    /// Represents a System table 
    /// for navigation purposes
    /// </summary>
    public class TableCategoryItem:IMatchName
    {
        #region Properties
        public string TableName { get; set; }
        public string TableTitle { get; set; } 
        #endregion

        #region IMatchName Members

        public string Name
        {
            get { return TableName; }
        }

        #endregion
    }
}

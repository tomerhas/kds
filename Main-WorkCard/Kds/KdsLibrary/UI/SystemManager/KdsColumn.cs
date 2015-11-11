using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using KdsLibrary.Utils;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using DalOraInfra.DAL;
using KDSCommon.Interfaces;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Defines Column in GridView or DetailsView
    /// </summary>
    public class KdsColumn:IMatchName
    {
        #region Fields
        private string _columnName;
        private string _headerText;
        private bool _isMust;
        private int _maxLength;
        private bool _showInGrid;
        private bool _showInDetails;
        private KdsListValue _listValue;
        private KdsColumnType _columnType;
        private string _format;
        private bool _isKey;
        private string _dataType;
        private bool _isLocked;
        private int _order;
        #endregion

        #region Constractor
        public KdsColumn()
        {
        }

        //public KdsColumn(XmlNode columnNode)
        //{
        //    _columnName = columnNode.Attributes["Name"].Value;
        //    _columnType = (KdsColumnType)Enum.Parse(typeof(KdsColumnType),
        //        columnNode.Attributes["Type"].Value);
        //    _headerText = columnNode.Attributes["HeaderText"].Value;
        //    _isMust = bool.Parse(columnNode.Attributes["IsMust"].Value);
        //    XmlNode linkValueNode = columnNode.SelectSingleNode("LinkTable");
        //    if (linkValueNode != null)
        //        _linkValue = new KdsLinkValue(linkValueNode);
        //    _maxLength = int.Parse(columnNode.Attributes["MaxLength"].Value);
        //    _showInDetails = bool.Parse(
        //        columnNode.Attributes["ShowInDetails"].Value);
        //    _showInGrid = bool.Parse(columnNode.Attributes["ShowInGrid"].Value);
        //} 
        #endregion
        
        #region Properties
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
        [XmlAttribute("Title")]
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }
        [XmlAttribute("Must")]
        public bool IsMust
        {
            get { return _isMust; }
            set { _isMust = value; }
        }
        [XmlAttribute("MaxLength")]
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
        [XmlAttribute("ShowInGrid")]
        public bool ShowInGrid
        {
            get { return _showInGrid; }
            set { _showInGrid = value; }
        }
        [XmlAttribute("ShowInDetails")]
        public bool ShowInDetails
        {
            get { return _showInDetails; }
            set { _showInDetails = value; }
        }
        [XmlElement("ListValue", typeof(KdsListValue))]
        public KdsListValue ListValue
        {
            get { return _listValue; }
            set { _listValue = value; }
        }
        [XmlAttribute("ViewType")]
        public KdsColumnType ColumnType
        {
            get { return _columnType; }
            set { _columnType = value; }
        }
        [XmlAttribute("DataFormat")]
        public string DataFormat
        {
            get { return _format; }
            set { _format = value; }
        }
        [XmlAttribute("Key")]
        public bool IsKey
        {
            get { return _isKey; }
            set { _isKey = value; }
        }
        [XmlAttribute("DataType")]
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }
        [XmlAttribute("Locked")]
        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }
        [XmlAttribute("Order")]
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }
        [XmlIgnore]
        public string FullHeaderText
        {
            get
            {
                return String.Format("{0}{1}", _isMust ? "<font color=red>*</font>" : String.Empty,
                    _headerText);
            }

        }
        [XmlIgnore]
        public bool ShowLabel
        {
            get { return _columnType != KdsColumnType.HiddenField; }
        }
        #endregion

        #region Methods
        internal DataControlField GetDataControlField()
        {
            DataControlField field = null;
            KdsColumnType myColumnType = _columnType;
            switch (_columnType)
            {
                case KdsColumnType.CheckBox:
                    field = new KdsCheckBoxField();
                    ((KdsCheckBoxField)field).DataField = _columnName;
                    break;
                case KdsColumnType.Calendar:
                    field = new BoundField();
                    SetBoundFieldProperties(field);
                    break;
                case KdsColumnType.DropDown:
                    field = new BoundField();
                    if (!_listValue.HasUnboundValues)
                        ((BoundField)field).DataField = _listValue.TextField;
                    else
                        ((BoundField)field).DataField = _columnName;
                    break;
                case KdsColumnType.TextField:
                    field = new BoundField();
                    SetBoundFieldProperties(field);
                    break;
                case KdsColumnType.HiddenField:
                    field = new BoundField();
                    SetBoundFieldProperties(field);
                    field.Visible = false;
                    break;
                default:
                    field = new BoundField();
                    SetBoundFieldProperties(field);
                    break;
            }
            
            field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            field.HeaderText = _headerText;
            field.SortExpression = _columnName;
            field.Visible = _showInGrid;
            field.ItemStyle.CssClass = "ItemRow";
           
            return field;
        }
        private void SetBoundFieldProperties(DataControlField field)
        {
            BoundField boundField = (BoundField)field;
            boundField.DataField = _columnName;
            if (!String.IsNullOrEmpty(_format))
                boundField.DataFormatString = _format;
        }

        internal object GetListValueDataSource(KdsDataSource parent,
            Control container)
        {
            DataTable dt = new DataTable();
            if (!_listValue.HasUnboundValues)
            {
                clDal db = new clDal();
                parent.AddSelectParams(db, container,
                    _listValue.SelectParametersList);
                db.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                db.ExecuteSP(_listValue.SelectMethod, ref dt);
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Value", typeof(String));
                dt.Columns.Add("Text", typeof(String));
                _listValue.TextField = "Text";
                _listValue.ValueField = "Value";
                _listValue.UnboundValues.ForEach(
                    delegate(KdsUnboundValue unbound)
                    {
                        DataRow row = dt.NewRow();
                        row["Value"] = unbound.Value;
                        row["Text"] = unbound.Text;
                        dt.Rows.Add(row);
                    });
            }
            if (!_isMust)
            {
                DataRow emptyRow = dt.NewRow();
                if (!_listValue.HasUnboundValues)
                    emptyRow[_listValue.ValueField] = DBNull.Value;
                else emptyRow[_listValue.ValueField] = String.Empty;
                emptyRow[_listValue.TextField] = String.Empty;
                dt.Rows.InsertAt(emptyRow, 0);
            }
            return dt;
        } 
        #endregion

        
    }

    public class KdsCheckBoxField : CheckBoxField
    {
        protected override object GetValue(Control controlContainer)
        {
            object o = base.GetValue(controlContainer);
            if (o is decimal)
            {
                // Cope with the fact that oracle returns booleans as decimal due to
                // the fact that oracle does not have boolean columns
                decimal d = (decimal)o;
                if (d == 1)
                {
                    return true;
                }
                else if (d == 0)
                {
                    return false;
                }
            }
            else if (o is short)
            {
                short d = (short)o;
                if (d == 1)
                {
                    return true;
                }
                else if (d == 0)
                {
                    return false;
                }
            }
            else if (o is int)
            {
                int d = (int)o;
                if (d == 1)
                {
                    return true;
                }
                else if (d == 0)
                {
                    return false;
                }
            }
            else if (o is string)
            {
                string s = (string)o;
                return s.Equals("1");
            }
           
            return o;
        }
    }

    public enum KdsColumnType
    {
        TextField,
        Calendar,
        CheckBox,
        DropDown,
        HiddenField
    }
}

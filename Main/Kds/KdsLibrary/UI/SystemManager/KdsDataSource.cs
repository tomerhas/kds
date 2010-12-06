using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using KdsLibrary.Utils;
using System.Data;
using KdsLibrary.DAL;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Defines DataSource for GridView or DetailsView
    /// </summary>
    public class KdsDataSource : IMatchName
    {
        #region Fields
        private string _selectMethod;
        private string _insertMethod;
        private string _updateMethod;
        private string _deleteMethod;
        private List<KdsColumn> _columns;
        private bool _isReadOnly;
        private string _roles;
        private string _updateRoles;
        private string _name;
        private string _title;
        private List<KdsSelectParameter> _selectParams;
        private string _sortColumn;
        #endregion

        #region Constractor
        public KdsDataSource()
        {
        }

        #endregion
        
        #region Properties
        [XmlAttribute("Select")]
        public string SelectMethod
        {
            get { return _selectMethod; }
            set { _selectMethod = value; }
        }
        [XmlAttribute("Insert")]
        public string InsertMethod
        {
            get { return _insertMethod; }
            set { _insertMethod = value; }
        }
        [XmlAttribute("Update")]
        public string UpdateMethod
        {
            get { return _updateMethod; }
            set { _updateMethod = value; }
        }
        [XmlAttribute("Delete")]
        public string DeleteMethod
        {
            get { return _deleteMethod; }
            set { _deleteMethod = value; }
        }
        [XmlElement("Column", typeof(KdsColumn))]
        public List<KdsColumn> ColumnsList
        {
            get { return _columns; }
            set { _columns = value; }
        }
        [XmlAttribute("ReadOnly")]
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }
        [XmlAttribute("Roles")]
        public string Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
        [XmlAttribute("UpdateRoles")]
        public string UpdateRoles
        {
            get { return _updateRoles; }
            set { _updateRoles = value; }
        }
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [XmlAttribute("Title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        [XmlAttribute("SortColumn")]
        public string SortColumn
        {
            get { return _sortColumn; }
            set { _sortColumn = value; }
        }
        [XmlElement("SelectParameter", typeof(KdsSelectParameter))]
        public List<KdsSelectParameter> SelectParametersList
        {
            get { return _selectParams; }
            set { _selectParams = value; }
        }
        [XmlIgnore]
        public MatchNameList<KdsColumn> Columns
        {
            get { return new MatchNameList<KdsColumn>(_columns); }
        }
        [XmlIgnore]
        public List<KdsColumn> OrderedColumns
        {
            get 
            {
                bool applyOrder = false;
                var orderedCols = new List<KdsColumn>(_columns.Count);
                _columns.ForEach(delegate(KdsColumn col)
                {
                    orderedCols.Add(col);
                    if (!applyOrder) applyOrder = col.Order > 0;
                });
                if (applyOrder)
                    orderedCols.Sort(delegate(KdsColumn first, KdsColumn second)
                    {
                        return first.Order.CompareTo(second.Order);
                    });
                return orderedCols;
            }
        }
        [XmlIgnore]
        public MatchNameList<KdsSelectParameter> SelectParameters
        {
            get { return new MatchNameList<KdsSelectParameter>(_selectParams); }
        }
        [XmlIgnore]
        public bool AllowEdit
        {
            get { return !String.IsNullOrEmpty(_updateMethod); }
        }
        [XmlIgnore]
        public bool AllowInsert
        {
            get { return !String.IsNullOrEmpty(_insertMethod); }
        }
        [XmlIgnore]
        public bool AllowDelete
        {
            get { return !String.IsNullOrEmpty(_deleteMethod); }
        }
        #endregion

        #region Methods
        public static KdsDataSource GetKdsDataSource(string page)
        {
            var sysman = KdsSysMan.GetKdsSysMan();
            return sysman.DataSources[page];
        }
        
        public DataTable GetData(KdsSysManPageBase container)
        {
            DAL.clDal dal = new KdsLibrary.DAL.clDal();
            DataTable dt = new  DataTable();
            AddSelectParams(dal, container);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            dal.ExecuteSP(_selectMethod, ref dt);
            return dt;
        }

        internal void AddSelectParams(clDal dal, KdsSysManPageBase container)
        {
            AddSelectParams(dal, container, _selectParams);
        }
        internal void AddSelectParams(clDal dal, KdsSysManPageBase container,
            List<KdsSelectParameter> selectParams)
        {
            selectParams.ForEach(delegate(KdsSelectParameter param)
            {
                dal.AddParameter(param.ControlName, GetParameterType(param.DataType),
                    GetParameterValue(param.GetValue(container), param.DataType), ParameterDir.pdInput);
            });
        }
        internal void AddSelectParams(clDal dal, Control container,
            List<KdsSelectParameter> selectParams)
        {
            selectParams.ForEach(delegate(KdsSelectParameter param)
            {
                dal.AddParameter(param.ControlName, GetParameterType(param.DataType),
                    GetParameterValue(param.GetValue(container), param.DataType), ParameterDir.pdInput);
            });
        }
        private clDal GetCommand(System.Collections.Specialized.IOrderedDictionary Values)
        {
            clDal dal = new clDal();
            _columns.ForEach(delegate(KdsColumn column)
            {
                if (Values[column.Name] != null)
                {
                    dal.AddParameter(column.Name, GetParameterType(column.DataType),
                        GetParameterValue(Values[column.Name],column.DataType),
                        ParameterDir.pdInput);
                }
            });
            dal.AddParameter("Taarich_Idkun_Acharon", ParameterType.ntOracleDate,
                DateTime.Now, ParameterDir.pdInput);
            dal.AddParameter("Meadken_Acharon", ParameterType.ntOracleInteger,
                KdsLibrary.Security.LoginUser.GetLoginUser().UserInfo.EmployeeNumber,
                ParameterDir.pdInput);
            return dal;
        }

        private clDal GetDeleteCommand(System.Collections.Specialized.IOrderedDictionary Values)
        {
            clDal dal = new clDal();
            _columns.FindAll(delegate(KdsColumn column)
                {
                    return column.IsKey;
                }).ForEach(delegate(KdsColumn column)
                {
                    if (Values[column.Name] != null)
                    {
                        dal.AddParameter(column.Name, GetParameterType(column.DataType),
                            GetParameterValue(Values[column.Name], column.DataType),
                            ParameterDir.pdInput);
                    }
                });
            
            return dal;
        }

        internal ParameterType GetParameterType(string dataType)
        {
            switch (dataType)
            {
                case "System.Int16":
                    return ParameterType.ntOracleInteger;
                case "System.Int32":
                    return ParameterType.ntOracleInteger;
                case "System.Int64":
                    return ParameterType.ntOracleInt64;
                case "System.DateTime":
                    return ParameterType.ntOracleDate;
                case "System.Decimal":
                    return ParameterType.ntOracleDecimal;
                case "System.String":
                    return ParameterType.ntOracleVarchar;
                default:
                    return ParameterType.ntOracleVarchar;
            }
        }

        internal object GetParameterValue(object formValue, string dataType)
        {
            if (formValue == null || String.IsNullOrEmpty(formValue.ToString()))
                return null;
            switch (dataType)
            {
                case "System.Int16":
                    return int.Parse(formValue.ToString());
                case "System.Int32":
                    return int.Parse(formValue.ToString());
                case "System.Int64":
                    return long.Parse(formValue.ToString());
                case "System.DateTime":
                    return DateTime.Parse(formValue.ToString());
                case "System.Decimal":
                    return float.Parse(formValue.ToString());
                case "System.String":
                    return formValue.ToString();
                default:
                    return formValue.ToString();
            }
        }

        public bool DeleteItem(System.Collections.Specialized.IOrderedDictionary newValues,
            out string errorMessage)
        {
            if (!AllowDelete)
                errorMessage = "Delete is not allowed";
            else
            {
                errorMessage = String.Empty;
                DAL.clDal dal = GetDeleteCommand(newValues);
                try
                {
                    dal.ExecuteSP(_deleteMethod);
                }
                catch (Exception exc)
                {
                    errorMessage = exc.Message;
                }
            }
            return String.IsNullOrEmpty(errorMessage);
        }

        public bool SaveItem(System.Collections.Specialized.IOrderedDictionary newValues, string commnadName, out string errorMessage)
        {
            string method = String.Empty;
            switch (commnadName)
            {
                case "Update":
                    method = _updateMethod;
                    break;
                case "Insert":
                    method = _insertMethod;
                    break;
                default:
                    throw new Exception(String.Format("Command {0} is not implemented",
                        commnadName));
            }

            errorMessage = String.Empty;
            DAL.clDal dal = GetCommand(newValues);
            try
            {
                dal.ExecuteSP(method);
            }
            catch (Exception exc)
            {
                if (exc.Message.ToLower().Contains("ora-00001"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["PrimaryKeyViolation"].Value;
                else if (exc.Message.ToLower().Contains("me_ad_date_check"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["DateRangeViolation"].Value;
                else if (exc.Message.ToLower().Contains("ora-20021"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NoSugErech"].Value;
                else if (exc.Message.ToLower().Contains("ora-20022"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["DateOverlappingViolation"].Value;
                else if (exc.Message.ToLower().Contains("ora-20023"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NoTime"].Value;
                else if (exc.Message.ToLower().Contains("ora-20024"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NoPeriodDDMM"].Value;
                else if (exc.Message.ToLower().Contains("ora-20025"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NoDate"].Value;
                else if (exc.Message.ToLower().Contains("ora-20026"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NoTypeInCTB"].Value;
                else if (exc.Message.ToLower().Contains("ora-20027"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NotNumeric"].Value;
                else if (exc.Message.ToLower().Contains("ora-20028"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["NoDecimalsAllowed"].Value;
                else if (exc.Message.ToLower().Contains("ora-20029"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["TimeWrongFormat"].Value;
                else if (exc.Message.ToLower().Contains("ora-20030"))
                    errorMessage =
                        KdsSysMan.GetKdsSysMan().Resources.TextResources["DecimalMissing"].Value;
                else errorMessage = exc.Message;
            }
            return String.IsNullOrEmpty(errorMessage);
        }

        public bool IsRoleAllowedToModify(Security.SecurityManager securityManager)
        {
            if (String.IsNullOrEmpty(_updateRoles))
                return true;
            return securityManager.IsRoleExistsIn(_updateRoles);
        }
        #endregion
    }

    public class KdsSelectParameter : IMatchName
    {
        #region Fields
        private string _ctlName;
        private string _dataType; 
        #endregion

        #region Properties
        [XmlAttribute("ControlName")]
        public string ControlName
        {
            get { return _ctlName; }
            set { _ctlName = value; }
        }

        [XmlAttribute("DataType")]
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        } 
        #endregion

        #region IMatchName Members

        public string Name
        {
            get { return _ctlName; }
        }

        #endregion

        #region Methods
        internal object GetValue(KdsSysManPageBase container)
        {
            Control ctl = GetControl(container);
            return GetControlValue(ctl);
        }
        
        internal object GetValue(Control container)
        {
            Control ctl = GetControl(container);
            return GetControlValue(ctl);
        }

        private object GetControlValue(Control ctl)
        {
            if (ctl == null) return null;
            switch (ctl.GetType().ToString())
            {
                case "System.Web.UI.WebControls.TextBox":
                    return ((TextBox)ctl).Text;
                case "System.Web.UI.WebControls.DropDownList":
                    return ((DropDownList)ctl).SelectedValue;
                case "System.Web.UI.WebControls.CheckBox":
                    return ((CheckBox)ctl).Checked;
                case "System.Web.UI.WebControls.HiddenField":
                    return ((HiddenField)ctl).Value;
                case "System.Web.UI.WebControls.Label":
                    return ((Label)ctl).Text;
                case "Egged.WebCustomControls.wccCalendar":
                    return ((Egged.WebCustomControls.wccCalendar)ctl).Date;
                default: return null;
            }

        }

        private Control GetControl(KdsSysManPageBase container)
        {
            Control ctl = null;
            if (container.MasterPageContentPlaceHolder != null)
                ctl =
                    container.MasterPageContentPlaceHolder.FindControl(_ctlName);
            else ctl = container.FindControl(_ctlName);
            return ctl;
        }

        private Control GetControl(Control container)
        {
            Control ctl = container.FindControl(_ctlName);
            if (ctl != null) return ctl;
            foreach (Control child in container.Controls)
            {
                ctl = GetControl(child);
                if (ctl != null) break;
            }
            return ctl;
        } 
        #endregion
    }

}


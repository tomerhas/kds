using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Egged.WebCustomControls;
using System.Collections;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Represents base class for pages with GridView or FormView
    /// to view/edit/insert data in tables
    /// </summary>
    public abstract class KdsSysManPageBase:KdsPage
    {
        #region Fields
        private GridView _gridView;
        private FormView _formView;
        protected KdsDataSource _dataSource;
        protected string _dataSourceName;
        private List<KdsDefaultValueHolder> _defaultValues;
        private bool _deletePrompt;
        #endregion

        #region Events
		protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _defaultValues = new List<KdsDefaultValueHolder>();
            _dataSourceName = Request.QueryString["table"];
            _dataSource = KdsSysMan.GetKdsSysMan().DataSources[_dataSourceName];

            _gridView = GridViewControl;
            _formView = FormViewControl;
            if (IsViewOnly)
            {
                _gridView.Sorting += new GridViewSortEventHandler(gridView_Sorting);
                _gridView.PageIndexChanging += new GridViewPageEventHandler(gridView_PageIndexChanging);
                _gridView.RowDataBound += new GridViewRowEventHandler(gridView_RowDataBound);
                _gridView.SelectedIndexChanged += new EventHandler(gridView_SelectedIndexChanged);
                _gridView.RowCommand += new GridViewCommandEventHandler(gridView_RowCommand);
                _gridView.DataBound += new EventHandler(gridView_DataBound);
                _gridView.RowCreated += new GridViewRowEventHandler(gridView_RowCreated);
                _gridView.RowDeleting += new GridViewDeleteEventHandler(gridView_RowDeleting);
                NewButton.Click += new EventHandler(NewButton_Click);
                RefreshButton.Click += new EventHandler(RefreshButton_Click);
            }
            else
            {
                _formView.EditItemTemplate = new KdsFormTemplate(_dataSource, ScriptManager);
                _formView.EmptyDataTemplate = new KdsFormTemplate(_dataSource, ScriptManager);
                _formView.ItemUpdating += new FormViewUpdateEventHandler(formView_ItemUpdating);
                _formView.ItemInserting += new FormViewInsertEventHandler(formView_ItemInserting);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsViewOnly)
            {
                if (String.IsNullOrEmpty(Request.QueryString["sourceID"]) ||
                    Session[Request.QueryString["sourceID"]] == null)
                    Response.Redirect(String.Format("~/{0}", NotAuthorizedRedirectPage));
                
            }

            RegisterTargetQueryStringControl();
            RegisterBodyOnloadEvent();
            if (IsViewOnly)
                hdEditorQueryString.Value = String.Empty;

            if (_dataSource == null)
            {
                throw new Exception(String.Format("Table {0} is not defined.",
                    _dataSourceName));
            }
            if (!IsPostBack)
            {
                SecurityManager.CheckIfRoleExistsIn(_dataSource.Roles); 
                BuildDataBoundControl();
                if (IsViewOnly)
                {
                    NewButton.Visible = IsAllowedToInsert;
                    PageHeader = GetCombinedTitle();
                    SetHeader(PageHeader);
                }
                if (Master != null)
                    LoadMessages((DataList)Master.FindControl("lstMessages"));
            }
            Title = _dataSource.Title;  
        }
        
        protected virtual void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            _gridView.PageIndex = e.NewPageIndex;
            _gridView.DataSource = ViewState["DataSource"];
            _gridView.DataBind();
            _gridView.SelectedIndex = -1;

        }
        
        protected virtual void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            _gridView.SelectedIndex = -1;
            DataTable dataTable = ViewState["DataSource"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = GetSortArgs(e);
                _gridView.PageIndex = 0;
                _gridView.DataSource = dataView;
                _gridView.DataBind();
            }
        }
        
        protected virtual void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    if (IsAllowedToUpdate)
                    {
                        e.Row.Cells[2].Visible = false;
                    }
                    else e.Row.Cells[1].Visible = false;
                    SetSortImage(e);
                    e.Row.Cells[0].Visible = IsAllowedToDelete;
                    break;
                case DataControlRowType.DataRow:
                    if (IsAllowedToUpdate)
                    {
                        LinkButton lb = e.Row.Cells[1].Controls[1] as LinkButton;
                        lb.Text = e.Row.Cells[2].Text.ToString();
                        e.Row.Cells[2].Visible = false;
                    }
                    else e.Row.Cells[1].Visible = false;
                    e.Row.Cells[0].Visible = IsAllowedToDelete;
                    if (IsAllowedToDelete)
                    {
                        DictionaryEntry[] keyVals = new DictionaryEntry[_gridView.DataKeyNames.Length];
                        _gridView.DataKeys[e.Row.RowIndex].Values.CopyTo(keyVals, 0);
                        StringBuilder sb = new StringBuilder(
                            KdsSysMan.GetKdsSysMan().Resources.TextResources["ConfirmDelete"].Value);
                        for(int i=0;i<keyVals.Length;++i)
                        {
                            sb.AppendFormat(" {0}:{1}",
                                _dataSource.Columns[keyVals[i].Key.ToString()].HeaderText,
                                GetValuesForDisplay(keyVals[i].Value,
                                     _dataSource.Columns[keyVals[i].Key.ToString()].DataFormat));
                        }
                        sb.Append("?");
                        string deletePrompt = sb.ToString();
                        
                        var cb = e.Row.FindControl("cbDelete")
                            as AjaxControlToolkit.ConfirmButtonExtender;
                        if (cb != null) cb.ConfirmText = deletePrompt;
                        Panel popup = e.Row.FindControl("pnlConfirm") as Panel;
                        if (popup != null)
                        {
                            Label prompt = popup.FindControl("lblPrompt") as Label;
                            if (prompt != null) prompt.Text = deletePrompt;
                            Button btnOK = popup.FindControl("OkConfButton") as Button;
                            if (btnOK != null)
                                btnOK.Text = 
                                    KdsSysMan.GetKdsSysMan().Resources.TextResources["Yes"].Value;
                            Button btnCancel = popup.FindControl("CancelConfButton") as Button;
                            if(btnCancel!=null)
                                btnCancel.Text = 
                                    KdsSysMan.GetKdsSysMan().Resources.TextResources["No"].Value;
                        }
                        
                    }
                    break;
            }
        }

        protected virtual void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (_deletePrompt)
                    _gridView.DeleteRow(_gridView.SelectedIndex);
                else
                    OpenEditor();
            
        }
        
        protected virtual void gridView_DataBound(object sender, EventArgs e)
        {
            
        }
        
        protected virtual void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected virtual void gridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                IGridViewPager gridPager = e.Row.FindControl("ucGridPager")
                as IGridViewPager;
                if (gridPager != null)
                {
                    gridPager.PageIndexChanged += delegate(object pagerSender,
                        GridViewPageEventArgs pagerArgs)
                    {
                        ChangeGridPage(pagerArgs.NewPageIndex);
                    };
                }
            }
        }

        protected virtual void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteItem(e.Values);
        }

        protected virtual void formView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            StoreItem(e.Values, "Insert");
        }
        
        protected virtual void formView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            StoreItem(e.NewValues, "Update");
        }
        
        protected virtual void NewButton_Click(object sender, EventArgs e)
        {
            OpenNew();
        }
        
        protected virtual void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion        
        
        #region Methods
        private string GetSortArgs(GridViewSortEventArgs e)
        {
            string sortExpr = String.Empty;
            SortDirection sortDir;
            sortExpr = e.SortExpression;
            if (ViewState["SortExpression"] == null)
                sortDir = e.SortDirection;
            else
            {
                if (ViewState["SortExpression"].ToString().Equals(e.SortExpression))
                {
                    SortDirection prevDir = (SortDirection)ViewState["SortDirection"];
                    sortDir = prevDir == SortDirection.Ascending ? SortDirection.Descending :
                        SortDirection.Ascending;
                }
                else
                    sortDir = SortDirection.Ascending;
            }
            ViewState["SortExpression"] = sortExpr;
            ViewState["SortDirection"] = sortDir;
            return String.Format("{0} {1}", sortExpr, ConvertSortDirectionToSql(sortDir));
        }

        private void SetSortImage(GridViewRowEventArgs e)
        {
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            int iColSort = 0;
            iColSort = GetCurrentColSort();
            lbl.Text = " ";
            e.Row.Cells[iColSort].Controls.Add(lbl);
            if (ViewState["SortDirection"] != null)
            {
                if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
                {
                    System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                    ImageSort.ID = "imgDescSort";
                    ImageSort.ImageUrl = "~/Images/DescSort.gif";
                    e.Row.Cells[iColSort].Controls.Add(ImageSort);
                }
                else
                {
                    System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                    ImageSort.ID = "imgAscSort";
                    ImageSort.ImageUrl = "~/Images/AscSort.gif";
                    e.Row.Cells[iColSort].Controls.Add(ImageSort);
                }
            }
            int i = 0;
            object sortHeader = null;

            for (i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Controls.Count > 0)
                {
                    sortHeader = e.Row.Cells[i].Controls[0];
                    ((LinkButton)(sortHeader)).Style.Add("color", "white");
                    ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
                }
            }
        }

        private int GetCurrentColSort()
        {
            string sSortExp = (string)ViewState["SortExpression"];
            int iColNum = -1;
            try
            {
                foreach (DataControlField dc in _gridView.Columns)
                {
                    iColNum++;
                    if (dc.SortExpression.Equals(sSortExp)) { break; }
                }

                return iColNum;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        private string GetCombinedTitle()
        {
            DataTable dtSource = _gridView.DataSource as DataTable;
            DateTime lastUpdate = DateTime.MinValue;
            String formattedLastUpdate = String.Empty;
            int rowCount = 0;
            if (dtSource != null)
            {
                rowCount = dtSource.Rows.Count;
                if (dtSource.Rows.Count > 0 && 
                    dtSource.Columns.Contains("LastUpdate"))
                {
                    DataView dv = dtSource.DefaultView;
                    dv.Sort = "LastUpdate DESC";
                    lastUpdate = DateTime.Parse(dv[0]["LastUpdate"].ToString());
                    formattedLastUpdate = String.Format(" - {0}:{1} {2} {3}",
                        "תאריך עדכון אחרון", lastUpdate.ToString("dd/MM/yyyy"),
                        "בשעה", lastUpdate.ToString("HH:mm"));
                }
            }
            return String.Format("{0}({1} {2}){3}",
                _dataSource.Title, rowCount, "רשומות", formattedLastUpdate);
        }

        private void BuildDataBoundControl()
        {
            if(IsViewOnly) BuildGridView();
            else BuildFormView();
        }

        private void BuildFormView()
        {
            if (_formView != null)
            {
                BindDetailsDataControl();
                ResizeWindow();
            }
        }

        private void ResizeWindow()
        {
            var detailsColumns = _dataSource.ColumnsList.FindAll(delegate(KdsColumn column)
            {
                return column.ShowInDetails && column.ShowLabel;
            });
            //int height = detailsColumns.Count * 50;
            //if (height < 200) height = 200;
            //calculate offset for wccCalendar frame
            int offset = 0;
            if (detailsColumns.Count(delegate(KdsColumn value)
            {
                return value.ColumnType == KdsColumnType.Calendar;
            }) > 0)
            {
                KdsColumn calendarCol = detailsColumns.Last(delegate(KdsColumn column)
                {
                    return column.ColumnType == KdsColumnType.Calendar;
                });
                int index = detailsColumns.IndexOf(calendarCol);
                if (index >= 0)
                {
                    if (detailsColumns.Count - index < 5)
                        offset = (detailsColumns.Count - index) * 20;

                }
            }
            //height += offset;
            ClientScript.RegisterStartupScript(GetType(), "Resize",
                String.Format("var offset={0};", offset > 0 ? 200 : 100), true);
            

        }

        private void BuildGridView()
        {
            if (_gridView != null)
            {
                if (!String.IsNullOrEmpty(_dataSource.SortColumn))
                {
                    ViewState["SortExpression"] = _dataSource.SortColumn;
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }
                BuildColumns(_gridView);
                if (BindDataOnLoad) BindGridDataControl();
            }
            else
            {
                throw new Exception(
                    "GridView Control does not exist on page.");
            }
        }

        private void BindDetailsDataControl()
        {
            DataView dv = (DataView)Session[Request.QueryString["sourceID"]];
            if (dv.Count == 0) _formView.ChangeMode(FormViewMode.Insert);
            _formView.DataSource = dv;
            _formView.DataBind();
        }

        private string GetRowFilter()
        {
            StringBuilder sbFilter = new StringBuilder();
            if (_gridView.SelectedDataKey != null)
            {
                foreach (String keyName in _gridView.DataKeyNames)
                {
                    if (sbFilter.Length > 0) sbFilter.Append(" and ");
                    sbFilter.AppendFormat("{0}={1}", keyName.ToUpper(),
                        GetValuesForFilter(_gridView.SelectedDataKey[keyName]));
                }
            }
            return sbFilter.ToString();
        }

        private string GetNewRowFilter()
        {
            StringBuilder sbFilter = new StringBuilder();
            
            foreach (String keyName in _gridView.DataKeyNames)
            {
                if (sbFilter.Length > 0) sbFilter.Append(" and ");
                sbFilter.AppendFormat("{0}=Null", keyName.ToUpper());
            }
            
            return sbFilter.ToString();
        }

        private string GetValuesForFilter(object value)
        {
            if (value.GetType() == typeof(DateTime))
                return "#" + ((DateTime)value).ToString(DateTimeFormatInfo.InvariantInfo) + "#";
            else return value.ToString();
        }

        private string GetValuesForDisplay(object value, string format)
        {
            if (value.GetType() == typeof(DateTime))
                return ((DateTime)value).ToString(
                    format.Replace("{0:", String.Empty).Replace("}", String.Empty));
            else return value.ToString();
        }

        protected void BindGridDataControl()
        {
            DataTable dt = _dataSource.GetData(this);
            _gridView.DataSource = dt;
            ViewState["DataSource"] = dt;
            _gridView.DataBind();
        }

        private void BuildColumns(CompositeDataBoundControl dataControl)
        {
            bool isDataEditor = dataControl.GetType() == typeof(DetailsView);
            DataControlFieldCollection fields = GetDataControlFieldCollection(dataControl);
            if (fields == null)
                throw new Exception("CompositeDataBound Control does not have DataControlFieldCollection"
                    );
            SetSelectCommandField();
            _dataSource.OrderedColumns.ForEach(delegate(KdsColumn kdsColumn)
            {
                DataControlField field =
                    kdsColumn.GetDataControlField();
                fields.Add(field);
            });
        }

        private void SetSelectCommandField()
        {
            if (_dataSource.ColumnsList.Count > 0 && IsAllowedToUpdate)
            {
                int index = _gridView.Columns.Count > 1 ? 1 : 0;
                _gridView.Columns[index].SortExpression =
                    _dataSource.OrderedColumns[0].Name;
                _gridView.Columns[index].HeaderText =
                    _dataSource.OrderedColumns[0].HeaderText;
            }
            List<KdsColumn> keyColumns = _dataSource.ColumnsList.FindAll(
                delegate(KdsColumn column)
                { return column.IsKey; });

            _gridView.DataKeyNames = keyColumns.ConvertAll<String>(delegate(KdsColumn column)
            {
                return column.Name;
            }).ToArray();
        }

        private DataControlFieldCollection GetDataControlFieldCollection(CompositeDataBoundControl dataControl)
        {
            switch (dataControl.GetType().ToString())
            {
                case "System.Web.UI.WebControls.GridView":
                    return ((GridView)dataControl).Columns;
                case "System.Web.UI.WebControls.DetailsView":
                    return ((DetailsView)dataControl).Fields;
                case "KdsLibrary.UI.SystemManager.KdsDetailsView":
                    return ((DetailsView)dataControl).Fields;

                default: return null;
            }
        }

        protected virtual void OpenEditor()
        {
            Guid guid = Guid.NewGuid();
            DataTable dt = (ViewState["DataSource"] as DataTable);
            DataView dv = new DataView(dt);
            dv.RowFilter = GetRowFilter();
            Session[guid.ToString()] = dv;
            hdEditorQueryString.Value = String.Format("{0}?table={1}&sourceID={2}",
                GetRelativePath(), _dataSourceName, guid);
        }

        private string GetRelativePath()
        {
            //return String.Format("{0}/SystemManager/DataEntryEdit.aspx",
            //    Request.ApplicationPath);
            string UrlRoot = string.Empty;
            UrlRoot = Request.Url.AbsoluteUri.Replace(Request.RawUrl, (Request.Url.Host == "localhost") ? Request.ApplicationPath : "");
            
            return String.Format("{0}/SystemManager/DataEntryEdit.aspx",
                UrlRoot);
        }

        protected virtual void OpenNew()
        {
            Guid guid = Guid.NewGuid();
            DataTable dt = (ViewState["DataSource"] as DataTable);
            DataView dv = new DataView(dt);
            dv.RowFilter = GetNewRowFilter();
            Session[guid.ToString()] = dv;
            hdEditorQueryString.Value = String.Format("{0}?table={1}&sourceID={2}&isEmpty=true",
               GetRelativePath(), _dataSourceName, guid);
            KdsDefaultValueAttribute.AddDefaultValuesFromAttributes(this);
            hdEditorQueryString.Value += GetQueryStringFromDefaultValues();
                //KdsDefaultValueAttribute.GetDefaultValues(this);
        }

        private string GetQueryStringFromDefaultValues()
        {
            StringBuilder sbQS = new StringBuilder();
            _defaultValues.ForEach(delegate(KdsDefaultValueHolder val)
            {
                Control ctl = MasterPageContentPlaceHolder.FindControl(val.ControlName);
                if (ctl != null)
                {
                    Type ctlType = ctl.GetType();
                    System.Reflection.PropertyInfo pInfo = ctlType.GetProperty(val.PropertyName);
                    if (pInfo != null)
                    {
                        object ctlValue = pInfo.GetValue(ctl, null);
                        sbQS.AppendFormat("&{0}={1}", val.FieldName, ctlValue.ToString());
                    }
                }
            });
            return sbQS.ToString();
        }

        protected virtual void RegisterTargetQueryStringControl()
        {
            if(IsViewOnly)
                Page.ClientScript.RegisterClientScriptBlock(GetType(),
                    "targetQueryStringControl",
                    String.Format("var queryStringTargetControlID='{0}';",
                    hdEditorQueryString.ClientID), true);
        }

        protected virtual void RegisterBodyOnloadEvent()
        {
            HtmlGenericControl body = null;
            if(Master!=null)
                body = (HtmlGenericControl)Page.Master.FindControl("MasterBody");
            if (body != null) body.Attributes.Add("onload", "load()");
        }

        protected virtual void FillValues(System.Collections.Specialized.IOrderedDictionary newValues)
        {
            _dataSource.ColumnsList.FindAll(delegate(KdsColumn column)
           {
               return column.ShowInDetails;
           }).ForEach(delegate(KdsColumn dc)
           {
               Control ctl = _formView.FindControl(dc.Name);
               if (ctl != null)
               {
                   newValues[dc.Name] = GetControlValue(ctl, dc);
               }
           });
        }

        protected virtual void FillDeleteValues(System.Collections.Specialized.IOrderedDictionary Values)
        {
            if (_gridView.SelectedDataKey != null)
            {
                foreach (String keyName in _gridView.DataKeyNames)
                {
                    Values[keyName] = _gridView.SelectedDataKey[keyName];
                }
            }
        }
        private object GetControlValue(Control ctl, KdsColumn dc)
        {
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
                case "System.Web.UI.WebControls.RadioButtonList":
                    return ((RadioButtonList)ctl).SelectedItem.Value;
                default: return ((TextBox)ctl).Text;
            }
        }

        private void StoreItem(System.Collections.Specialized.IOrderedDictionary newValues,
            string commnadName)
        {
            ErrorLabel.Text = String.Empty;
            if (Page.IsValid)
            {
                string errorMessage = String.Empty;
                FillValues(newValues);
                if (_dataSource.SaveItem(newValues, commnadName, out errorMessage))
                    //close page
                    hdEditorState.Value = "OK";
                else ErrorLabel.Text = errorMessage;
            }
        }

        private void DeleteItem(System.Collections.Specialized.IOrderedDictionary values)
        {
            ErrorLabel.Text = String.Empty;
            string errorMessage = String.Empty;
            FillDeleteValues(values);
            if (_dataSource.DeleteItem(values, out errorMessage))
                RefreshData();
            else ErrorLabel.Text = errorMessage;
           
        }

        protected virtual void RefreshData()
        {
            ErrorLabel.Text = String.Empty;
            BindGridDataControl();
            PageHeader = GetCombinedTitle();
            SetHeader(PageHeader);
        }

        protected void ChangeGridPage(int pageIndex)
        {
            DataTable dataTable = ViewState["DataSource"] as DataTable;
            ChangeGridPage(pageIndex, _gridView, new DataView(dataTable), "SortDirection",
                "SortExpression");
        }
        protected void ChangeGridPage(int pageIndex, GridView grid, DataView dataView, 
            string sortDirViewStateKey, string sortExprViewStateKey)
        {
            //DataTable dataTable = ViewState["DataSource"] as DataTable;
            //if (dataTable != null)
            //{
                grid.PageIndex = pageIndex;
                //var dataView = new DataView(dataTable);
                string sortExpr = String.Empty;
                SortDirection sortDir = SortDirection.Ascending;
                if (ViewState[sortExprViewStateKey] != null)
                {
                    sortExpr = ViewState[sortExprViewStateKey].ToString();
                    if (ViewState[sortDirViewStateKey] != null)
                        sortDir = (SortDirection)ViewState[sortDirViewStateKey];
                    dataView.Sort = String.Format("{0} {1}", sortExpr,
                        ConvertSortDirectionToSql(sortDir));
                }
                grid.DataSource = dataView;
                grid.DataBind();
            //}
        }

        protected internal void AddDefaultValue(string field, string control,
            string property)
        {
            _defaultValues.Add(
                new KdsDefaultValueHolder(field, control, property));
        }
        
        protected internal void AddDefaultValue(string field, string control)
        {
            _defaultValues.Add(
                new KdsDefaultValueHolder(field, control));
        }

        protected virtual void PromptDelete(object sender, EventArgs e)
        {
            _deletePrompt = true;
        }
        #endregion

        #region Properties
        protected virtual GridView GridViewControl
        {
            get 
            {
                if (MasterPageContentPlaceHolder == null) return null;
                return (GridView)MasterPageContentPlaceHolder.FindControl("GridView1"); 
            }
        }
       
        protected virtual FormView FormViewControl
        {
            get { return (FormView)FindControl("FormView1"); }
        }
        
        protected internal virtual ContentPlaceHolder MasterPageContentPlaceHolder
        {
            get 
            {
                if (Master == null) return null;
                return (ContentPlaceHolder)Master.FindControl("KdsContent"); 
            }
        }
        
        protected virtual ScriptManager ScriptManager
        {
            get 
            { 
                if(Master!=null)
                    return (ScriptManager)Master.FindControl("ScriptManagerKds"); 
                else
                    return (ScriptManager)FindControl("ScriptManagerKds"); 
            }
        }

        protected bool IsViewOnly
        {
            get { return _formView == null; }
        }

        protected virtual bool BindDataOnLoad
        {
            get { return true; }
        }

        protected virtual HiddenField hdEditorQueryString
        {
            get { return (HiddenField)MasterPageContentPlaceHolder.FindControl("hdQueryString"); }
        }

        protected virtual Button NewButton
        {
            get 
            {
                return (Button)MasterPageContentPlaceHolder.FindControl("btnNew");
            }
        }

        protected virtual Label ErrorLabel
        {
            get
            {
                if (MasterPageContentPlaceHolder == null) return (Label)FindControl("lblError");
                return (Label)MasterPageContentPlaceHolder.FindControl("lblError"); 
            }
        }
        
        protected virtual HiddenField hdEditorState
        {
            get { return (HiddenField)FindControl("hdState"); }
        }

        protected virtual Button RefreshButton
        {
            get 
            { 
                return (Button)MasterPageContentPlaceHolder.FindControl("btnRefresh"); 
            }
        }

        protected bool IsAllowedToInsert
        {
            get
            {
                return _dataSource.AllowInsert &&
                      _dataSource.IsRoleAllowedToModify(SecurityManager);
            }
        }
        
        protected bool IsAllowedToUpdate
        {
            get
            {
                return _dataSource.AllowEdit &&
                    _dataSource.IsRoleAllowedToModify(SecurityManager);
            }
        }

        protected bool IsAllowedToDelete
        {
            get
            {
                return _dataSource.AllowDelete &&
                    _dataSource.IsRoleAllowedToModify(SecurityManager);
            }
        }
        #endregion

    }

    public interface IGridViewPager
    {
        event GridViewPageEventHandler PageIndexChanged;
    }
}


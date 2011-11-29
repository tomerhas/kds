using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using KdsLibrary.Utils;
using KdsLibrary.Controls;
using System.Web;

namespace KdsLibrary.UI.SystemManager
{
    /// <summary>
    /// Class for creating a Template for FormView Control
    /// </summary>
     public class KdsFormTemplate : System.Web.UI.ITemplate, INamingContainer
     {
        private const int MAX_WIDTH = 315;
        private const int MAX_DROPDOWN_WIDTH = 320;
        private const int CALENDAR_MAX_WIDTH = 130;
        private KdsDataSource _dataSource;
        private ScriptManager _scriptManager;
        private KdsSysManResources _resources;
        private System.Collections.Hashtable Fields;

        public KdsFormTemplate(KdsDataSource source,  ScriptManager scriptManager)
        {
            _dataSource = source;
            _scriptManager = scriptManager;
            _resources = KdsSysMan.GetKdsSysMan().Resources;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            Fields = new System.Collections.Hashtable();
            //create a table within the ItemTemplate
            Table tbl = new Table();
            tbl.CssClass = "title";
            TableRow tr = null;
            TableCell td = null;
            container.Controls.Add(tbl);

            tr = new TableRow();
            tbl.Rows.Add(tr);
            td = new TableCell();
            td.ColumnSpan = 2;
            td.CssClass = "GridHeader";
            td.Text = IsForUpdate ?  _resources.TextResources["UpdateTitle"].Value : _resources.TextResources["InsertTitle"].Value;
            tr.Cells.Add(td);

            _dataSource.OrderedColumns.FindAll(delegate(KdsColumn column)
            {
                return column.ShowInDetails;
            }).ForEach(delegate(KdsColumn dc)
            {
                if (dc.Name != null)
                {
                    //for each DataColum create a TableRow with 2 cells
                    tr = new TableRow();
                    tbl.Rows.Add(tr);
                    td = new TableCell();
                    tr.Cells.Add(td);
                    if (IsLabelNeeded(dc))
                    {
                        Label lbl = new Label();
                        td.Controls.Add(lbl);
                        lbl.CssClass = "InternalLabel";
                        if (IsForUpdate && dc.IsKey)
                            lbl.Text = dc.HeaderText;
                        else
                            lbl.Text = dc.FullHeaderText;
                        if (!lbl.Text.EndsWith(":"))
                            lbl.Text = String.Concat(lbl.Text, ":");
                    }
                    td.CssClass = "LabelColumn";
                    
                    td = new TableCell();
                    tr.Cells.Add(td);
                    SetAColumn(container, td, dc);
                }
            });
            AddButton(tbl, IsForUpdate ? "Update" : "Insert",
                _resources.TextResources["Save"].Value);
            //AddButton(tbl, "Cancel",
            //    _resources.TextResources["Close"].Value);
            
        }

        private bool IsLabelNeeded(KdsColumn kdsColumn)
        {
            if (IsForUpdate) return kdsColumn.ShowLabel;
            else return kdsColumn.ShowLabel; //&& !kdsColumn.IsLocked;
        }

        private void AddButton(Table tbl,string buttonCommandName,
            string buttonText)
        {
            var tr = new TableRow();
            tbl.Rows.Add(tr);
            var td = new TableCell();
            tr.Cells.Add(td);
            td = new TableCell();
            tr.Cells.Add(td);
            var button = new Button();
            button.Text = buttonText;
            td.Attributes.Add("align", "left");
            button.CommandName = buttonCommandName;
            if(buttonText.Equals("Cancel"))
                button.OnClientClick = "javascript:window.close();";
            button.CssClass = "ImgButtonSearch";
            td.Controls.Add(button);
            
        }

        private void SetAColumn(System.Web.UI.Control container, TableCell td, KdsColumn kdsColumn)
        {
            TextBox txt = null;

            if ((IsForUpdate && kdsColumn.IsKey && kdsColumn.ShowLabel &&
                 kdsColumn.ColumnType == KdsColumnType.Calendar) ||
               (kdsColumn.IsLocked && kdsColumn.ShowLabel))
            {
                txt = new TextBox();
                txt.ID = kdsColumn.Name;
                Fields.Add(txt.ID, kdsColumn.Name);
                td.Controls.Add(txt);
                txt.Width = Unit.Pixel(MAX_WIDTH);
                txt.Text = GetDefaultValue(kdsColumn);
                txt.Enabled = false;
                txt.DataBinding += new EventHandler(txt_DataBinding);
            }
            else
            {
                KdsColumnType columnType = GetRelevantColumnType(kdsColumn);

                switch (columnType)
                {
                    case KdsColumnType.CheckBox:
                        RadioButtonList rbl = new RadioButtonList();
                        rbl.ID = kdsColumn.Name;
                        Fields.Add(rbl.ID, kdsColumn.Name);
                        System.Web.UI.WebControls.ListItem li =
                            new System.Web.UI.WebControls.ListItem(
                                _resources.TextResources["Yes"].Value,
                                "1");
                        rbl.Items.Add(li);
                        rbl.RepeatLayout = RepeatLayout.Flow;
                        rbl.RepeatDirection = RepeatDirection.Horizontal;
                        li = new System.Web.UI.WebControls.ListItem(
                            _resources.TextResources["No"].Value,
                            "0");
                        rbl.Items.Add(li);
                        rbl.SelectedValue = GetDefaultValue(kdsColumn);
                        if (String.IsNullOrEmpty(rbl.SelectedValue))
                            rbl.SelectedValue = "1";
                        rbl.DataBound += new EventHandler(rbl_DataBound);
                        td.Controls.Add(rbl);
                        break;

                    case KdsColumnType.TextField:
                        txt = new TextBox();
                        txt.ID = kdsColumn.Name;
                        Fields.Add(txt.ID, kdsColumn.Name);
                        if (kdsColumn.MaxLength > 0)
                        {
                            txt.MaxLength = kdsColumn.MaxLength;
                        }
                        txt.Width = Unit.Pixel(MAX_WIDTH);
                        txt.Text = GetDefaultValue(kdsColumn);
                        td.Controls.Add(txt);
                        txt.DataBinding += new EventHandler(txt_DataBinding);
                        AddValidators(txt, kdsColumn, td);
                        break;

                    case KdsColumnType.Calendar:
                        KdsCalendar calendar = new KdsCalendar();
                        calendar.Attributes.Add("dir", "rtl");
                        td.Attributes.Add("dir", "ltr");
                        td.Attributes.Add("align", "right");
                        calendar.ID = kdsColumn.Name;
                        calendar.Width = Unit.Pixel(CALENDAR_MAX_WIDTH);
                        calendar.Text = GetDefaultValue(kdsColumn);
                        Fields.Add(calendar.CalendarId, kdsColumn.Name);
                        td.Controls.Add(calendar);
                        calendar.DataBinding += new EventHandler(calendar_DataBinding);
                        td.Controls.Add(calendar);
                        //KdsCalendar.AddValidators(calendar, kdsColumn, td);
                        break;

                    case KdsColumnType.DropDown:
                        DropDownList dropDown = new DropDownList();
                        dropDown.ID = kdsColumn.Name;
                        dropDown.AutoPostBack = false;
                        dropDown.Width = Unit.Pixel(MAX_DROPDOWN_WIDTH);
                        dropDown.DataSource = kdsColumn.GetListValueDataSource(
                            _dataSource, container);
                        dropDown.DataTextField = kdsColumn.ListValue.TextField;
                        dropDown.DataValueField = kdsColumn.ListValue.ValueField;
                        dropDown.DataBind();
                        td.Controls.Add(dropDown);
                        string defaultVal = GetDefaultValue(kdsColumn);
                        if (!String.IsNullOrEmpty(defaultVal))
                            dropDown.SelectedValue = defaultVal;
                        dropDown.DataBinding += new EventHandler(dropDown_DataBinding);
                        Fields.Add(dropDown.ID, kdsColumn.Name);
                        AddValidators(dropDown, kdsColumn, td);
                        
                        var depends = from c in _dataSource.ColumnsList
                                      where c.ShowInDetails == true
                                      && c.ColumnType == KdsColumnType.DropDown
                                      && c.ListValue != null
                                      && (c.ListValue.SelectParametersList.Find(delegate(KdsSelectParameter param)
                                      {
                                          return param.ControlName.ToLower().Equals(kdsColumn.Name.ToLower());
                                      }) != null)
                                      select c.ListValue;
                        if (depends.ToArray().Length > 0)
                        {
                            dropDown.AutoPostBack = true;
                            dropDown.SelectedIndexChanged += new EventHandler(dropDown_SelectedIndexChanged);
                        }
                        break;

                    case KdsColumnType.HiddenField:
                        HiddenField hidden = new HiddenField();
                        hidden.ID = kdsColumn.Name;
                        hidden.Value = GetDefaultValue(kdsColumn);
                        Fields.Add(hidden.ID, kdsColumn.Name);
                        td.Controls.Add(hidden);
                        hidden.DataBinding += new EventHandler(hidden_DataBinding);
                        break;
                    default:
                        txt = new TextBox();
                        txt.ID = kdsColumn.Name;
                        Fields.Add(txt.ID, kdsColumn.Name);
                        td.Controls.Add(txt);
                        if (txt.MaxLength > 0)
                        {
                            txt.MaxLength = kdsColumn.MaxLength;
                        }
                        txt.Width = Unit.Pixel(MAX_WIDTH);
                        txt.Text = GetDefaultValue(kdsColumn);
                        txt.DataBinding += new EventHandler(txt_DataBinding);
                        break;
                }
                if (kdsColumn.IsKey && IsForUpdate && td.Controls.Count > 0 &&
                    kdsColumn.ColumnType != KdsColumnType.Calendar)
                {
                    WebControl ctl = td.Controls[0] as WebControl;//td.FindControl(kdsColumn.Name) as WebControl;
                    if (ctl != null) ctl.Enabled = false;
                }

            }
           
        }

        void dropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddParent = sender as DropDownList;
            if (ddParent != null)
            {
                var depends = from c in _dataSource.ColumnsList
                              where c.ShowInDetails == true
                              && c.ColumnType == KdsColumnType.DropDown
                              && c.ListValue != null
                              && (c.ListValue.SelectParametersList.Find(delegate(KdsSelectParameter param)
                              {
                                  return param.ControlName.ToLower().Equals(ddParent.ID.ToLower());
                              }) != null)
                              select c.Name;
                FormView container = ddParent.NamingContainer as FormView;

                foreach (string childCol in depends.ToArray())
                {
                    DropDownList ddChild = container.FindControl(childCol) as DropDownList;
                    var kdsChildCol = _dataSource.Columns[childCol];
                    ddChild.DataSource = kdsChildCol.GetListValueDataSource(
                            _dataSource, container);
                    ddChild.DataBind();
                }

            }
        }

        private string GetDefaultValue(KdsColumn kdsColumn)
        {
            string defaultVal = String.Empty;
            if (HttpContext.Current.Request.QueryString[kdsColumn.Name] != null)
            {
                defaultVal = 
                    HttpContext.Current.Request.QueryString[kdsColumn.Name];
            }
            return defaultVal;
        }
        private KdsColumnType GetRelevantColumnType(KdsColumn kdsColumn)
        {
            if (IsForUpdate) return kdsColumn.ColumnType;
            else
            {
                if (kdsColumn.IsLocked) return KdsColumnType.HiddenField;
                else return kdsColumn.ColumnType;
            }
        }

        void hidden_DataBinding(object sender, EventArgs e)
        {
            HiddenField hidden = (HiddenField)sender;
            DataRowView drv = null;
            KdsColumn kdsColumn = _dataSource.Columns[Fields[hidden.ID].ToString()];
            drv = (DataRowView)((FormView)hidden.NamingContainer).DataItem;
            if (drv != null && drv[kdsColumn.Name] != null)
                hidden.Value = drv[kdsColumn.Name].ToString();
        }

        void dropDown_DataBinding(object sender, EventArgs e)
        {
            DropDownList dropDown = (DropDownList)sender;
            DataRowView drv = null;
            KdsColumn kdsColumn = _dataSource.Columns[Fields[dropDown.ID].ToString()];
            drv = (DataRowView)((FormView)dropDown.NamingContainer).DataItem;
            if (drv != null)
            {
                try
                {
                    dropDown.SelectedValue = drv[kdsColumn.Name].ToString();
                }
                catch (ArgumentOutOfRangeException) { }
            }
            if (dropDown.AutoPostBack)
                dropDown_SelectedIndexChanged(dropDown, new EventArgs());
        }

        private void AddValidators(Control ctl, KdsColumn kdsColumn, TableCell td)
        {
            RangeValidator rangeVal;
            RegularExpressionValidator rexpval;
            CompareValidator comVal;
            if (!(ctl is DropDownList))
            {
                switch (kdsColumn.DataType)
                {
                    case "System.Int16":
                        rangeVal = new RangeValidator();
                        rangeVal.ControlToValidate = ctl.ID;
                        rangeVal.MaximumValue = "32767";
                        rangeVal.MinimumValue = "-32767";
                        rangeVal.Display = ValidatorDisplay.Dynamic;
                        rangeVal.Text = _resources.TextResources["IntegerMessage"].Value;
                        rangeVal.Type = ValidationDataType.Integer;
                        td.Controls.Add(rangeVal);
                        break;
                    case "System.Int32":
                        rangeVal = new RangeValidator();
                        rangeVal.ControlToValidate = ctl.ID;
                        rangeVal.MaximumValue = "2147483647";
                        rangeVal.MinimumValue = "-2147483648";
                        rangeVal.Display = ValidatorDisplay.Dynamic;
                        rangeVal.Text = _resources.TextResources["IntegerMessage"].Value;
                        rangeVal.Type = ValidationDataType.Integer;
                        td.Controls.Add(rangeVal);
                        break;
                    case "System.Int64":
                        rangeVal = new RangeValidator();
                        rangeVal.ControlToValidate = ctl.ID;
                        rangeVal.MaximumValue = "9223372036854775807";
                        rangeVal.MinimumValue = "-9223372036854775808";
                        rangeVal.Display = ValidatorDisplay.Dynamic;
                        rangeVal.Text = _resources.TextResources["IntegerMessage"].Value;
                        rangeVal.Type = ValidationDataType.Integer;
                        td.Controls.Add(rangeVal);
                        break;
                    case "System.DateTime":
                        comVal = new CompareValidator();
                        comVal.Type = ValidationDataType.Date;
                        comVal.ControlToValidate = ((KdsCalendar)ctl).CalendarId;
                        comVal.Display = ValidatorDisplay.Dynamic;
                        comVal.Operator = ValidationCompareOperator.DataTypeCheck;
                        comVal.Text = _resources.TextResources["DateTimeMessage"].Value;
                        td.Controls.Add(comVal);
                        break;
                    case "System.Decimal":
                        comVal = new CompareValidator();
                        comVal.Type = ValidationDataType.Double;
                        comVal.ControlToValidate = ctl.ID;
                        comVal.Display = ValidatorDisplay.Dynamic;
                        comVal.Operator = ValidationCompareOperator.DataTypeCheck;
                        comVal.Text = _resources.TextResources["NumberMessage"].Value;
                        td.Controls.Add(comVal);
                        break;
                }
            }
            if (kdsColumn.IsMust)
            {
                RequiredFieldValidator reqVal = new RequiredFieldValidator();
                reqVal.ControlToValidate = ctl.ID;
                reqVal.Display = ValidatorDisplay.Dynamic;
                reqVal.Text = String.Concat(_resources.TextResources["RequiredMessage"].Value, " ",
                    kdsColumn.HeaderText);
                td.Controls.Add(reqVal);
            }
                
        }

        void calendar_DataBinding(object sender, EventArgs e)
        {
            KdsCalendar calendar = (KdsCalendar)sender;
            DataRowView drv = null;
            KdsColumn _dc = _dataSource.Columns[Fields[calendar.ID].ToString()];
            drv = (DataRowView)((FormView)calendar.NamingContainer).DataItem;
            if (drv != null && drv[_dc.Name] != null && !String.IsNullOrEmpty(drv[_dc.Name].ToString()))
                calendar.Text = ((DateTime)drv[_dc.Name]).ToString("dd/MM/yyyy");
        }


        void txt_DataBinding(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            DataRowView drv = null;
            KdsColumn kdsColumn = _dataSource.Columns[Fields[txt.ID].ToString()];
            drv = (DataRowView)((FormView)txt.NamingContainer).DataItem;
            if (drv != null && drv[kdsColumn.Name] != null)
            {
                if (kdsColumn.ListValue != null && kdsColumn.ColumnType == KdsColumnType.DropDown)
                    if(!kdsColumn.ListValue.HasUnboundValues)
                        txt.Text = drv[kdsColumn.ListValue.TextField].ToString();
                    else txt.Text = drv[kdsColumn.Name].ToString();
                else
                    txt.Text = GetTextFormat(drv, kdsColumn);
            }
        }


        void rbl_DataBound(object sender, EventArgs e)
        {
            RadioButtonList rbl = ((RadioButtonList)sender);
            DataRowView drv = null;
            KdsColumn kdsColumn = _dataSource.Columns[Fields[rbl.ID].ToString()];
            drv = (DataRowView)((FormView)rbl.NamingContainer).DataItem;
            if (drv != null && drv[kdsColumn.Name] != null)
            {
                if (!String.IsNullOrEmpty(drv[kdsColumn.Name].ToString()))
                    rbl.SelectedValue = drv[kdsColumn.Name].ToString();
                else rbl.SelectedValue = "0";
            }
        }

        void lbl_DataBinding(object sender, EventArgs e)
        {
            Label lbl = ((Label)sender);
            DataRowView drv = null;
            drv = (DataRowView)((FormView)lbl.NamingContainer).DataItem;
            KdsColumn kdsColumn = _dataSource.Columns[Fields[lbl.ID].ToString()];
            if (drv != null && drv[kdsColumn.Name] != null)
                if (kdsColumn.ListValue != null && kdsColumn.ColumnType == KdsColumnType.DropDown)
                    lbl.Text = drv[kdsColumn.ListValue.TextField].ToString();
                else lbl.Text = GetTextFormat(drv, kdsColumn);
        }

        private string GetTextFormat(DataRowView rowView, KdsColumn kdsColumn)
        {
            //change the data format based on the data if the datacolumn is not a primary key field
            if (rowView[kdsColumn.Name] == DBNull.Value) return String.Empty;
            switch (kdsColumn.DataType)
            {
                case "System.DateTime":
                    return ((DateTime)rowView[kdsColumn.Name]).ToString("dd/MM/yyyy");
                case "System.Decimal":
                    return Convert.ToDecimal(rowView[kdsColumn.Name]).ToString("#0.00");
                case "System.Boolean":
                    return Convert.ToBoolean(rowView[kdsColumn.Name]) ? _resources.TextResources["Yes"].Value : _resources.TextResources["No"].Value;
                default:
                    return rowView[kdsColumn.Name].ToString();
            }
        }
        /// <summary>
        /// Gets the value true if the Template is for Update and
        /// false if the Template is for Insert
        /// </summary>
        private bool IsForUpdate
        {
            get { return HttpContext.Current.Request.QueryString["isEmpty"] == null; }
        }
    }

}




   


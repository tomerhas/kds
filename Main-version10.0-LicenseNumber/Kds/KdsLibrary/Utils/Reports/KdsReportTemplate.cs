using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using KdsLibrary.Utils.Reports;
using KdsLibrary.UI.SystemManager;
using KdsLibrary.Controls; 
using System.Reflection;
using AjaxControlToolkit.Design;
using DalOraInfra.DAL;
using Saplin.Controls;



namespace KdsLibrary.Utils.Reports
{
    public class PanelFilters : Panel
    {
        private const int CALENDAR_MAX_WIDTH = 130; 
        private KdsReport _Report;
        private KdsSysManResources _Resources;
        private List<KdsFilter> _KdsFilters;
        private int _MaxOfColumnInLine = 3;
        private int _MarginWidth = 50;
        private int _HeigthOfLine = 40;
        private Table _Table;
        private TableRow _Tr;
        private TableCell _Td;
        private List<string> _Controls;
        private ScriptManager _scriptManager;

        public PanelFilters( KdsReport Report,KdsSysManResources resources,  ScriptManager scriptManager)
        {
            ID = "PanelFilters";
            _scriptManager = scriptManager;
            _Resources = resources;
            _Controls = new List<string>();
            _Report = Report;
            _KdsFilters = _Report.FilterList;
            _Table = new Table();
            _Table.Attributes.Add("cellpadding", "1");
            //_Table.GridLines = GridLines.Both;
            //_Table.Width = Unit.Percentage(100);
            _MaxOfColumnInLine = _Report.FilterByLine;

        }

        public void FillControls()
        {
            try
            {
                if (_Report.FilterList.Count > 0)
                {
                    this.Controls.Add(_Table);
                    BuildPanel();
                }
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }

        public List<string> ControlsBoxes
        {
            get { return _Controls; }
        }

        private void BuildPanel()
        {
            int TdCounter = 0;
            object _Control;
            string CurrentClass = string.Empty;
            try
            {
                List<string> Classes = GetClassesDistincted();
                foreach (string FilterClassed in Classes)
                {
                    if ((CurrentClass != FilterClassed) && (FilterClassed != ""))
                    {
                        CurrentClass = FilterClassed;
                        AddHeaderOfClass(CurrentClass);
                        TdCounter = 0;
                    }
                   AddRowToTable();
                    foreach (KdsFilter Filter in _KdsFilters)
                    {
                        if (Filter.ClassName == CurrentClass)
                        {
                            if (TdCounter == _MaxOfColumnInLine)
                            {
                                TdCounter = 0;
                                AddRowToTable();
                            }
                            if (Filter.BoxeType!= KdsBoxeType.Button)
                            AddLabel(Filter);
                            _Td = new TableCell();
                            _Td.Attributes.Add("Align", "Right");
                            _Td.Attributes.Add("vAlign", "Top");
                            _Td.Attributes.Add("dir", "ltr");
                            _Control = GetControlBoxe(Filter);
                            if (_Control != null)
                            {
                                _Td.Controls.Add((System.Web.UI.Control)_Control);
                                if (Filter.Required)
                                    AddValidator(ref _Td, Filter, (System.Web.UI.Control)_Control);
                                if (Filter.BoxeType == KdsBoxeType.TextBox && Filter.DropDownList != null)
                                    if (Filter.DropDownList.QueryType == KdsQueryType.AjaxAutoComplete)
                                        AddAutoComplete(ref _Td, Filter);
                                _Tr.Cells.Add(_Td);
                                _Tr.Cells.Add(MarginCell());
                                TdCounter++;
                            }
                        }
                    }
                }
                AddValidationSummary();
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        private void AddLabel(KdsFilter Filter)
        {
            _Td = new TableCell();
            _Td.Attributes.Add("Align", "Right");
            _Td.Attributes.Add("vAlign", "Top");
            if ((Filter.BoxeType == KdsBoxeType.ListBoxExtended) || (Filter.BoxeType == KdsBoxeType.RadioButtonList) || (Filter.BoxeType == KdsBoxeType.DropDownCheckBoxes)) //**
                _Td.Style.Add("padding-top", "10px");
            Label Lb = new Label();
            Lb.Text = Filter.Caption + ":";
            Lb.ID = "LBL_" + Filter.ParameterName;
            Lb.Attributes.Add("runat", "server");
            _Td.Wrap = false;
            _Td.Controls.Add(Lb);
            _Tr.Cells.Add(_Td);
        }
        private void AddHeaderOfClass(string CurrentClass)
        {
            AddRowToTable();
            _Td = new TableCell();
            _Td.ColumnSpan = _MaxOfColumnInLine * 3;
            _Td.Font.Bold = true;
            _Td.Text = CurrentClass;
            _Td.Style.Add("border-bottom-style", "solid");
            _Tr.Cells.Add(_Td);
            _Table.Rows.Add(_Tr);

        }
        private List<string> GetClassesDistincted()
        {
            List<string> Classes=  new List<string>();
            foreach (KdsFilter Filter in _KdsFilters)
            {
                if (Classes.Find(delegate(string keyInList)
                {
                    return keyInList == Filter.ClassName;
                }) == null)
                {
                    Classes.Add(Filter.ClassName);
                }
            }
            return Classes;
        }

        private void AddAutoComplete(ref TableCell TdAutoComplete,KdsFilter Filter )  
        {
            AjaxControlToolkit.AutoCompleteExtender Ext = new AjaxControlToolkit.AutoCompleteExtender();
       
            Ext.ID = "AutoComplete_" +Filter.ParameterName ;
            Ext.TargetControlID = Filter.ParameterName;
            Ext.UseContextKey = true;
            Ext.MinimumPrefixLength = 1;
            Ext.ServiceMethod = Filter.DropDownList.SelectMethod;
            Ext.ServicePath = Filter.DropDownList.LibraryName;
            Ext.EnableCaching = true;
            Ext.CompletionListCssClass="ACLst";
            Ext.CompletionListHighlightedItemCssClass="ACLstItmSel";
            Ext.CompletionListItemCssClass = "ACLstItmE";
            TdAutoComplete.Controls.Add((System.Web.UI.Control)Ext);
        }
        private void AddValidationSummary()
        {
            _Tr = new TableRow();
            _Table.Rows.Add(_Tr);
            _Td = new TableCell();
            UpdatePanel Panel = new UpdatePanel();
            Panel.RenderMode = UpdatePanelRenderMode.Inline;
            Panel.UpdateMode = UpdatePanelUpdateMode.Always;
            Panel.ID = "UpdatePnlSummary";
            ValidationSummary Sum = new ValidationSummary();
            Sum.ID = "ValidationSummary";
            Panel.ContentTemplateContainer.Controls.Add(Sum);
            _Td.Controls.Add((System.Web.UI.Control)Panel);
            _Td.ColumnSpan = _MaxOfColumnInLine * 2;
            _Tr.Cells.Add(_Td);
        }
        private void AddValidator(ref TableCell CurrentTd, KdsFilter Filter, Control ControlToValidate)
        {
            int Counter = 0;
            if ((Filter.FilterValidator != null) &&
                (Filter.FilterValidator.Count > 0))
            {
                foreach (FilterValidator FValidator in Filter.FilterValidator)
                {
                    CustomValidator Validator = new CustomValidator();
                    Validator.ErrorMessage = FValidator.ErrorMessageValidator;
                    Validator.ID = ControlToValidate.ID + "_Validator_" + Counter.ToString("00");
                    Validator.Text = FValidator.ErrorMessageValidator;
                    Validator.EnableClientScript = true;
                    Validator.ClientValidationFunction = FValidator.ValidationFunction;
                    Validator.Enabled = true;
                    Validator.ValidateEmptyText = true;
                    if (ControlToValidate.GetType().ToString() != "System.Web.UI.WebControls.CheckBoxList")
                        if (ControlToValidate.GetType().ToString() == "KdsLibrary.Controls.ListBoxExtended")
                        {
                            Validator.ControlToValidate = ((ListBoxExtended)ControlToValidate).ControlToValidateId;
                        }
                        else
                            Validator.ControlToValidate = ControlToValidate.ID;
                    Validator.Display = ValidatorDisplay.None;
                    CurrentTd.Controls.Add((System.Web.UI.Control)Validator);
                    Counter++;
                }
            }
        }

        private void AddRowToTable()
        {
            _Tr = new TableRow();
        //    _Tr.Style.Add("height", _HeigthOfLine.ToString() + "px");
            _Table.Rows.Add(_Tr);
        }

        private TableCell MarginCell()
        {
            TableCell Td = new TableCell();
            Td.Width = Unit.Pixel(_MarginWidth);
            return Td;
        }
        private object GetControlBoxe(KdsFilter Filter)
        {
            object _ObjReturned = null;

            switch (Filter.BoxeType)
            {
                case KdsBoxeType.Calendar:
                    _ObjReturned = (object)GetCalendarControl(Filter);
                    break;
                case KdsBoxeType.CheckBoxList:
                    _ObjReturned = (object)GetCheckboxListControl(Filter);
                    break;
                case KdsBoxeType.DropDown:
                    _ObjReturned = (object)GetDropdownListControl(Filter);
                    break;
                case KdsBoxeType.ListBox:
                    _ObjReturned = (object)GetListboxControl(Filter);
                    break;
                case KdsBoxeType.TextBox:
                    _ObjReturned = (object)GetTextBoxControl(Filter);
                    break;
                case KdsBoxeType.ListBoxExtended :
                    _ObjReturned = (object)GetListBoxExtendedControl(Filter);
                    break;
                case KdsBoxeType.Button :
                    _ObjReturned = (object)GetButtonControl(Filter);
                    break;
                case KdsBoxeType.RadioButtonList :
                    _ObjReturned = (object)GetRadioButtonList(Filter);
                    break;
                case KdsBoxeType.DropDownCheckBoxes:
                    _ObjReturned = (object)GetDropDownCheckBoxesControl(Filter);
                    break;
            }
            return _ObjReturned;
        }
        private KdsCalendar GetCalendarControl(KdsFilter filter)
        {
            DateTime cDate;
            KdsCalendar calendar = new KdsCalendar();
            calendar.Width = Unit.Pixel(CALENDAR_MAX_WIDTH);
            calendar.Text = (DateTime.TryParse(filter.DefaultValue, out cDate)) ? cDate.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
            calendar.ID = filter.ParameterName;
           // calendar.Attributes.Add("dir", "rtl");
            calendar.Attributes.Add("runat", "server");
            calendar.LabelName = filter.Caption;
            calendar.CalloutMessageDisplayed = false;
           // calendar.PopupPositionCallOut = AjaxControlToolkit.ValidatorCalloutPosition.BottomRight;
            if (filter.RunAtServer)
            {
                calendar.Attributes.Add("OnChange", "FireControlChanged()");
                calendar.AutoPostBack = true;
            }
            if (filter.IsParent)
            {
                calendar.AutoPostBack = true;
                calendar.TextChanged += new KdsCalendar.TextChangeEventHandler(Control_TextChanged);
            }
            _Controls.Add(calendar.ID);
            return calendar;
        }
        private DropDownList GetDropdownListControl(KdsFilter filter)
        {
            DropDownList Ddl = new DropDownList();
            Ddl.ID = filter.ParameterName;
            Ddl.DataTextField = filter.DropDownList.TextField;
            Ddl.DataValueField = filter.DropDownList.ValueField;
            Ddl.Attributes.Add("dir", "rtl");
            if (filter.RunAtServer)
            {
                Ddl.Attributes.Add("OnChange", "FireControlChanged()");
                Ddl.Attributes.Add("runat", "server");
            }
            if (filter.IsParent)
            {
                Ddl.AutoPostBack = true;
                Ddl.TextChanged += new EventHandler(Control_TextChanged);
            }
            Ddl.DataSource = GetListValueDataSource(filter);
            Ddl.DataBind();
            _Controls.Add(Ddl.ID);
            return Ddl; 
        }
        private DropDownCheckBoxes GetDropDownCheckBoxesControl(KdsFilter filter)
        {
            DropDownCheckBoxes Ddl = new DropDownCheckBoxes();
            Ddl.ID = filter.ParameterName;
            Ddl.DataTextField = filter.DropDownList.TextField;
            Ddl.DataValueField = filter.DropDownList.ValueField;
           Ddl.Attributes.Add("dir", "rtl");
            Ddl.Style.SelectBoxWidth = 250;
            Ddl.Style.DropDownBoxBoxWidth = 250;// DropDownBoxBoxHeight='150'"; 
            Ddl.Texts.SelectBoxCaption = "";
          //  Ddl.TextAlign = TextAlign.Right;
           Ddl.Texts.SelectAllNode = "הכל";
         //  Ddl.Texts.SelectAllStyle = "style='text-align:left'";
            Ddl.UseSelectAllNode = true;
           
            if (filter.IsParent)
            {
                Ddl.AutoPostBack = true;
                Ddl.TextChanged += new EventHandler(Control_TextChanged);
            }
            Ddl.DataSource = GetListValueDataSource(filter);
            Ddl.DataBind();
            if (filter.RunAtServer)
            //{
            //    Ddl.Attributes.Add("onblur", "FireControlChanged()");
            //    Ddl.AutoPostBack = true;
            //}
           // if (filter.RunAtServer)
            //{

              //  Ddl.Attributes.Add("onchange", "FireControlChanged()");
               Ddl.Attributes.Add("runat", "server");
            
           // }
            _Controls.Add(Ddl.ID);

            return Ddl;
        }
        
        private ListBox GetListboxControl(KdsFilter filter)
        {
            ListBox List = new ListBox();
            List.ID = filter.ParameterName;
            if (filter.RunAtServer)
            {
                List.Attributes.Add("OnChange", "FireControlChanged()");
                List.Attributes.Add("runat", "server");
            }
            List.SelectionMode = ListSelectionMode.Multiple;
            List.DataTextField = filter.DropDownList.TextField;
            List.DataValueField = filter.DropDownList.ValueField;
            List.Attributes.Add("dir", "rtl");
            if (filter.IsParent)
            {
                List.AutoPostBack = true;
                List.TextChanged += new EventHandler(Control_TextChanged);
            }
            List.DataSource = GetListValueDataSource(filter);
            List.DataBind();
            _Controls.Add(List.ID);
            return List;

        }
        private CheckBoxList GetCheckboxListControl(KdsFilter filter)
        {
            CheckBoxList List = new CheckBoxList();
            List.ID = filter.ParameterName;
            List.Attributes.Add("runat", "server");
            if (filter.RunAtServer)
            {
                List.Attributes.Add("OnClick", "FireControlChanged()");
            }
            List.DataTextField = filter.DropDownList.TextField;
            List.DataValueField = filter.DropDownList.ValueField;
            List.Attributes.Add("dir", "rtl");
            if (filter.IsParent)
            {
                List.AutoPostBack = true;
                List.TextChanged += new EventHandler(Control_TextChanged);
            }
            List.DataSource = GetListValueDataSource(filter);
            List.DataBind();
            _Controls.Add(List.ID);
            return List;
        }

        private RadioButtonList GetRadioButtonList(KdsFilter filter)
        {
            RadioButtonList List = new RadioButtonList();
            List.ID = filter.ParameterName;
            List.Attributes.Add("runat", "server");
            if (filter.RunAtServer)
            {
//                List.AutoPostBack = true;
                List.Attributes.Add("OnClick", "FireControlChanged()");
            }
            List.DataTextField = filter.DropDownList.TextField;
            List.DataValueField = filter.DropDownList.ValueField;
            List.Attributes.Add("dir", "rtl");
            if (filter.IsParent)
            {
                List.AutoPostBack = true;
                List.TextChanged += new EventHandler(Control_TextChanged);
            }
            List.DataSource = GetListValueDataSource(filter);
            List.DataBind();
            _Controls.Add(List.ID);
            return List;
        }
        private TextBox GetTextBoxControl(KdsFilter filter)
        {
            TextBox TxtBox = new TextBox();
            if (filter.IsParent)
            {
                TxtBox.AutoPostBack = true;
                TxtBox.TextChanged += new EventHandler(Control_TextChanged);
            }
            TxtBox.Text = filter.DefaultValue;
            TxtBox.ID = filter.ParameterName;
            TxtBox.Attributes.Add("runat", "server");
            TxtBox.Attributes.Add("dir", "rtl");
            _Controls.Add(TxtBox.ID);
            return TxtBox;
        }
        private ListBoxExtended GetListBoxExtendedControl(KdsFilter filter)
        {
            ListBoxExtended ListBoxExt ;
            if ((filter.DropDownList != null ) && (filter.DropDownList.QueryType== KdsQueryType.AjaxAutoComplete))
                ListBoxExt = new ListBoxExtended(filter.DropDownList.SelectMethod, filter.DropDownList.LibraryName,
                    filter.ParameterName , _scriptManager );
            else ListBoxExt = new ListBoxExtended(_scriptManager,filter.ParameterName);
            ListBoxExt.FillControls();
            ListBoxExt.ID = filter.ParameterName;
            _Controls.Add(ListBoxExt.ID);
            return ListBoxExt;
        }

        private HtmlInputButton GetButtonControl(KdsFilter filter)
        {
            HtmlInputButton Btn = new HtmlInputButton();
            Btn.ID = filter.ParameterName;
            Btn.Value= filter.Caption;
            Btn.Attributes.Add("runat", "server");
            Btn.Attributes.Add("OnClick", "FireControlChanged()");
            Btn.Attributes.Add("Class", _Resources.TextResources["ButtonCssClass"].Value);
            return Btn;

        }


        private void Control_TextChanged(object sender, EventArgs e)
        {
            Control CtrlParent = (Control)sender;
            Control CtrlChild = null;
            List<KdsFilter> ParentsOfChildFilter = new List<KdsFilter>();
            DataTable dt = new DataTable();
            clDal db = null;
            try
            {
                List<KdsFilter> ChildsOfParentFilter = FindChildsOfParentFilter(CtrlParent.ID);
                ChildsOfParentFilter.ForEach(delegate(KdsFilter ChildFilter)
                {
                    db = new clDal();
                    ChildFilter.DropDownList.ConditionFilters.ForEach(delegate(ConditionFilter CFilter)
                    {
                        CtrlChild = _Table.FindControl(ChildFilter.ParameterName);
                        CtrlParent = _Table.FindControl(CFilter.ControlReference);
                        db.AddParameter(CFilter.Name, CFilter.ParameterType, GetControlObject(CtrlParent), ParameterDir.pdInput);
                    });
                    db.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                    db.ExecuteSP(ChildFilter.DropDownList.SelectMethod, ref dt);
                    db = null;
                    BindControl(ChildFilter.BoxeType, CtrlChild, dt);
                    dt.Clear();
                });
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }

        }
        private void BindControl(KdsBoxeType BoxType, Control Ctrl, DataTable Dt)
        {
            switch  (BoxType)
                {
                case KdsBoxeType.DropDown :
                        ((DropDownList)Ctrl).DataSource = Dt;
                        ((DropDownList)Ctrl).DataBind();
                        break;
                case KdsBoxeType.ListBox:
                        ((ListBox)Ctrl).DataSource = Dt;
                        ((ListBox)Ctrl).DataBind();
                        break;
                }
        }

        private object GetListValueDataSource(KdsFilter Filter)
        {
            DataTable dt = new DataTable();
            try
            {

                clDal db = new clDal();
                switch (Filter.DropDownList.QueryType)
                {
                    case KdsQueryType.Select:
                        db.ExecuteSQL(Filter.DropDownList.SelectMethod, ref dt);
                        break;
                    case KdsQueryType.Function:
                        Type thisType = Type.GetType(Filter.DropDownList.LibraryName);
                        MethodInfo theMethod = thisType.GetMethod(Filter.DropDownList.SelectMethod);
                        dt = (DataTable)theMethod.Invoke(this, GetParametersOfFunction(Filter));
                        break;
                    default:
                        if ((Filter.DropDownList != null) &&
                            (Filter.DropDownList.ConditionFilters != null) &&
                            (Filter.DropDownList.ConditionFilters.Count > 0))
                        {
                            Filter.DropDownList.ConditionFilters.ForEach(delegate(ConditionFilter Cf)
                            {
                                Control CtrlParent = _Table.FindControl(Cf.ControlReference);
                                if (CtrlParent != null)
                                    db.AddParameter(Cf.Name, Cf.ParameterType, GetControlObject(CtrlParent), ParameterDir.pdInput);
                            });
                        }
                        if ((Filter.DropDownList != null) &&
                            (Filter.DropDownList.ParameterOfFunctions != null) &&
                            (Filter.DropDownList.ParameterOfFunctions.Count > 0))
                        {
                            Filter.DropDownList.ParameterOfFunctions.ForEach(delegate(ParameterOfFunction Param)
                            {
                                object[] ObjParam = GetParametersOfFunction(Filter);
                                foreach (object obj in ObjParam)
                                    db.AddParameter(Param.Name, Param.ParameterType, obj, ParameterDir.pdInput);
                            });
                        }
                        db.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                        db.ExecuteSP(Filter.DropDownList.SelectMethod, ref dt);
                        break;
                }
                return dt;
            }
            catch
            {
                throw new Exception("Problem in the definition of the Filter " + Filter.ParameterName +
                    " or in his DropDownList.Verify if his QueryType defined correctly.");
            }
        }
        private object[] GetParametersOfFunction(KdsFilter Filter)
        {
            object[] parameters = new object[Filter.DropDownList.ParameterOfFunctions.Count];
            int Counter = 0;
            Filter.DropDownList.ParameterOfFunctions.ForEach(delegate(ParameterOfFunction Param)
            {
                parameters[Counter] = GetValueOfParam(Param);
                Counter++;
            });
            return parameters; 
        }

        private object GetValueOfParam(ParameterOfFunction Param)
        {
            try
            {
                Type TypeOfParam = Type.GetType(Param.DataType);
                if (Param.Type == ParameterFunctionType.ByValue)
                    return Convert.ChangeType(Param.GetObjectValue, TypeOfParam);
                else if (Param.Type == ParameterFunctionType.ByReference) // Property or Function 
                {
                    if (Param.ReferenceType == ParameterReferenceType.Property) // Property 
                        return Convert.ChangeType(clGeneral.GetValueOfProperty(Param.Value.ToString()), TypeOfParam);
                    else if (Param.ReferenceType == ParameterReferenceType.Function)
                    {
                        if ((Param.ParameterOfFunctions != null) &&
                            (Param.ParameterOfFunctions.Count > 0))// has a child ... 
                        {
                            object[] parameters = new object[Param.ParameterOfFunctions.Count];
                            int Counter = 0;
                            Param.ParameterOfFunctions.ForEach(delegate(ParameterOfFunction SubParam)
                            {
                                parameters[Counter] = GetValueOfParam(SubParam);
                                Counter++;
                            });
                            Type thisType = Type.GetType(Param.LibraryName);
                            MethodInfo theMethod = thisType.GetMethod(Param.Value);
                            return theMethod.Invoke(this, parameters);
                        }
                        else return Convert.ChangeType(clGeneral.GetValueOfMethod(Param.Value), TypeOfParam);
                    }
                }
                return null;
            }
            catch ( Exception ex) 
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, "Problem in the definition of the Param " + Param.Name, true);
                return null;
            }
        }
        private object GetControlObject(Control Ctrol)
        {
            object ParamValue = null;
            ParamValue = clGeneral.GetControlValue(Ctrol);
            if (Ctrol.GetType().ToString() == "Egged.WebCustomControls.wccCalendar")
                ParamValue = DateTime.Parse((string)ParamValue);
            return ParamValue; 

        }

        public List<KdsFilter> FindChildsOfParentFilter(string Parent)
        {
            List<KdsFilter> ListOfChilds = new List<KdsFilter>();
            _KdsFilters.ForEach(delegate(KdsFilter Filter)
            {
                if ((Filter.DropDownList != null) && (Filter.DropDownList.ConditionFilters != null)
                    && (Filter.DropDownList.ConditionFilters.Count > 0))
                {
                    Filter.DropDownList.ConditionFilters.ForEach(delegate(ConditionFilter CFilter)
                    {
                        if (CFilter.ControlReference == Parent)
                            ListOfChilds.Add(Filter);
                    });
                }
            });
            return ListOfChilds;
        }
    }
}

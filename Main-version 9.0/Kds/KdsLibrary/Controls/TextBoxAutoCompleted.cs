using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;

namespace KdsLibrary.Controls
{

    public class TextBoxAutoCompleted : KdsPanel
    {
        private TextBox _Textbox;
        private AjaxControlToolkit.AutoCompleteExtender _AutoCompleteExtender;
        private string _SelectMethod, _LibraryName;
        private bool _isAutoComplete = false;

        public TextBoxAutoCompleted(ScriptManager scriptManager, string IdName)
            : base(scriptManager,IdName)
        {
            try
            {
                _Textbox = new TextBox();
                _Textbox.ID = _IdName + "TxtbxAutoComplete";
                _Textbox.Width = 100;
                _Textbox.Attributes.Add("dir", "rtl");
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        public TextBoxAutoCompleted(string SelectMethod, string LibraryName, string IdName, ScriptManager scriptManager)
            : this(scriptManager, IdName) 
        {
            try
            {
                _isAutoComplete = true; 
                _SelectMethod = SelectMethod;
                _LibraryName = LibraryName;
                _AutoCompleteExtender = new AjaxControlToolkit.AutoCompleteExtender();
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        protected override void BuildPanel()
        {
            try
            {
                _Tr = new TableRow();
                _Td = new TableCell();
                _Td.Controls.Add(_Textbox);
                if (_isAutoComplete)
                {
                    SetAutoComplete(_SelectMethod, _LibraryName);
                    _Td.Controls.Add((System.Web.UI.Control)_AutoCompleteExtender);
                }
                _Tr.Cells.Add(_Td);
                _Table.Rows.Add(_Tr);
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        private void SetAutoComplete(string SelectMethod , string LibraryName)
        {
            try
            {
                _AutoCompleteExtender.ID = "AutoComplete_" + _Textbox.ID;
                _AutoCompleteExtender.TargetControlID = _Textbox.ID;
                _AutoCompleteExtender.UseContextKey = true;
                _AutoCompleteExtender.MinimumPrefixLength = 1;
                _AutoCompleteExtender.CompletionInterval = 0;
                _AutoCompleteExtender.CompletionSetCount = 25;
                _AutoCompleteExtender.ServiceMethod = SelectMethod;
                _AutoCompleteExtender.ServicePath = LibraryName;
                _AutoCompleteExtender.EnableCaching = true;
                _AutoCompleteExtender.EnableViewState = true;
                _AutoCompleteExtender.CompletionListCssClass = "autocomplete_completionListElement";
                _AutoCompleteExtender.CompletionListHighlightedItemCssClass = "autocomplete_completionListItemElement_Select";
                _AutoCompleteExtender.CompletionListItemCssClass = "autocomplete_completionListItemElement";
                _AutoCompleteExtender.FirstRowSelected = true;
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        #region properties 
        public bool SelectionRequired
        {
            get
            {
                return _isAutoComplete;
            }
        }
        public TextBox Textboxe
        {
            get {return _Textbox;}
        }
        public string OnClientItemSelected
        {
            set
            {
                _AutoCompleteExtender.OnClientItemSelected = value;
            }
        }
        public string ContextKey
        {
            get
            {

                return (_isAutoComplete) ? _AutoCompleteExtender.ContextKey : "";
            }
            set
            {
                if (_isAutoComplete)
                    _AutoCompleteExtender.ContextKey = value;
            }
        }
        #endregion 
    }
}

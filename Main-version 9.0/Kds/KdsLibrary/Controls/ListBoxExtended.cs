using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace KdsLibrary.Controls
{
    public class ListBoxExtended :KdsPanel 
    {
        TextBoxAutoCompleted _TextBoxAutoComplete ;
        HtmlInputButton _BtnAdd, _BtnDel;
        ListBox _List;
        TextBox _ListOfValues,_ValidText;
        private void SetListBoxExtended()
        {
            this.Width = 100;
            _BtnAdd = new HtmlInputButton();
            _BtnAdd.ID = _IdName + "BtnAdd";
            _BtnAdd.Disabled = true;
            _BtnAdd.Value = "הוסף";
            _BtnDel = new HtmlInputButton();
            _BtnDel.ID = _IdName + "BtnDel";
            _BtnDel.Disabled = true;
            _BtnDel.Value = "הסר";
            _List = new ListBox();
            _List.ID = _IdName + "ListBoxExtended";
            _List.Width = Unit.Percentage(100);
            _List.BorderWidth = Unit.Pixel(1);
            _List.SelectionMode = ListSelectionMode.Multiple;
            _List.EnableViewState = false;
            _List.Attributes.Add("dir", "rtl");
            _ListOfValues = new TextBox();
            _ListOfValues.ID = _IdName + "ListOfValues";
            _ValidText = new TextBox();
            _ValidText.ID = _IdName + "ValidText";
            GridLine = true;
        }

        public ListBoxExtended(ScriptManager scriptManager,string IdName)
            : base(scriptManager,IdName)
        {
            _TextBoxAutoComplete = new TextBoxAutoCompleted(scriptManager,IdName);
            SetListBoxExtended();
        }
        public ListBoxExtended(string SelectMethod, string LibraryName,string IdName, ScriptManager scriptManager)
            : this(scriptManager,IdName)
        {
            _TextBoxAutoComplete = new TextBoxAutoCompleted(SelectMethod, LibraryName,IdName, scriptManager);
            SetListBoxExtended();
        }
        public void SetDefaultValue(string Value)
        {
            _ListOfValues.Text += (_ListOfValues.Text == "") ? Value : _ListOfValues.Text.ToString() + "," + Value;
        }
        protected override void BuildPanel()
        {
            try
            {
                AddTextBoxe();
                _TextBoxAutoComplete.FillControls();
                AddButtons();
                AddListbox();
                _List.Attributes.Add("onchange", "EnableButtonDel(this,'" + _BtnDel.ID.ToString() + "')");
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.BuildError(_scriptManager.Page, ex.Message, true);
            }
        }
        public void AddAttributes()
        {
            _TextBoxAutoComplete.Textboxe.Attributes.Add("BtnAdd", _BtnAdd.ClientID);
            if (_TextBoxAutoComplete.SelectionRequired)
            {
                _TextBoxAutoComplete.Textboxe.Attributes.Add("ValidText", _ValidText.ClientID);
                _TextBoxAutoComplete.OnClientItemSelected = "EnableButtonAdd";
                _TextBoxAutoComplete.Textboxe.Attributes.Add("onkeydown", "ClearValidText('" + _TextBoxAutoComplete.Textboxe.ClientID + "')");
            }
            else
            {
                _TextBoxAutoComplete.Textboxe.Attributes.Add("onkeyup", "SetButtonAdd('" + _TextBoxAutoComplete.Textboxe.ClientID + "')");
            }
        }
        private void AddTextBoxe()
        {
                _Tr = new TableRow();
                _Td = new TableCell();
                _Td.ColumnSpan = 3;
                _Td.Controls.Add(_TextBoxAutoComplete);
                _Tr.Cells.Add(_Td);
                _Table.Rows.Add(_Tr);
        }
        private void AddButtons()
        {
            _BtnDel.Attributes.Add("onclick", "DeleteSelectedItems(this,'" + _List.ID + "','" + _ListOfValues.ID + "')");
            _BtnAdd.Attributes.Add("onclick", "PutValueInListItem(this,'" + _TextBoxAutoComplete.Textboxe.ID + "','" + _List.ID + "','" + _ListOfValues.ID + "')");
            _Tr = new TableRow();

            _Td = new TableCell();
            _Td.Attributes.Add("align", "center");
            _Td.Controls.Add(_BtnAdd);
            _Tr.Cells.Add(_Td);

            _Td = new TableCell();
            _ListOfValues.Style.Add("display", "none");
            _Td.Controls.Add(_ListOfValues);
            _ValidText.Style.Add("display", "none");
            _Td.Controls.Add(_ValidText);
            _Tr.Cells.Add(_Td);

            _Td = new TableCell();
            _Td.Attributes.Add("align", "center");
            _Td.Controls.Add(_BtnDel);
            _Tr.Cells.Add(_Td);
            _Table.Rows.Add(_Tr);
        }
        private void AddListbox()
        {
            _Tr = new TableRow();
            _Td = new TableCell();
            _Td.ColumnSpan = 3;
            _Td.Controls.Add(_List);
            _Tr.Cells.Add(_Td);
            _Table.Rows.Add(_Tr);
        }

        #region properties 
        public string ControlToValidateId
        {
            get { return _ListOfValues.ID; }
        }
        public string ContextKey
        {
            get
            {
                return _TextBoxAutoComplete.ContextKey;
            }
            set
            {
                _TextBoxAutoComplete.ContextKey = value;
            }
        }
        public string ListOfValues 
        {
            get{ return _ListOfValues.Text.ToString();}
        }

        public ListBox List
        {
            get { return _List; }
        }
        #endregion 

    }
}

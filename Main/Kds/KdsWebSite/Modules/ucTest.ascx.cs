using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KdsLibrary.BL;
using KdsBatch;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;
using System.Collections.Specialized;
using KdsLibrary.UDT;
//using KdsBatch.InputData;
using KdsBatch;
public partial class Modules_ucTest : System.Web.UI.UserControl
{
    private DataTable _dtSnifim;
    protected void Page_Load(object sender, EventArgs e)
    {
        BuildPage();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        
    }
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    private void BuildPage()
    {
        //  HtmlTableCell oTableCell = new HtmlTableCell();
        HtmlTable hTable = new HtmlTable();
        HtmlTableRow hRow = new HtmlTableRow();
        HtmlTableCell hCell = new HtmlTableCell();

        hTable.Rows.Add(hRow);

        TextBox oTxt = new TextBox();
        oTxt.ID = "txtTest";
        oTxt.Text = "vered";

        ListItem Item = new ListItem();
        DropDownList ddl = new DropDownList();

        Item.Text = "";
        Item.Value = "-1";
        ddl.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "אין צורך בטכוגרף";
        Item.Value = "0";
        ddl.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "חסר טכוגרף";
        Item.Value = "1";
        ddl.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "צירף טכוגרף";
        Item.Value = "2";
        ddl.ID = "ddlVered";
        ddl.Items.Add(Item);


        hCell.Controls.Add(oTxt);
        hCell.Controls.Add(ddl);

        hTable.Rows[0].Cells.Add(hCell);
        //pnlHeader.Controls.Add(hTable);

        Panel pnlHeader;
        Panel pnlContent;

        for (int i = 0; i < 3; i++)
        {
            pnlHeader = new Panel();
            pnlHeader.ID = "pnlHeader" +i ;
       
            TableRow tRow = new TableRow();
            TableCell tCell = new TableCell();
            tRow.ID = "Row" + i;
            tCell.ID = "Cell" + i;
            tRow.Cells.Add(tCell);
            tbSidurim.Rows.Add(tRow);

            tCell.Controls.Add(pnlHeader);
            pnlContent = new Panel();
            pnlContent.ID = "pnlContent" + i;

            tCell.Controls.Add(pnlContent);

            GridView grdPeiluyot = new GridView();
            BoundField boundGridField;
            TemplateField tGridField = new TemplateField();
            GridControls.enControlToAdd[] arrControlToAdd;
            int iArrControlSize = 0;
            // DataTable dtTest;


            GridControls oGridControls;


            grdPeiluyot.EnableViewState = true;
            grdPeiluyot.GridLines = GridLines.None;
            //  grdPeiluyot.ID = "grdPeiluyot" + iIndex;
            grdPeiluyot.ShowHeader = true;
            grdPeiluyot.AllowPaging = false;
            grdPeiluyot.AutoGenerateColumns = false;
            grdPeiluyot.AllowSorting = true;
            grdPeiluyot.Width = Unit.Pixel(900);

            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "");
            iArrControlSize = 0;
            //Initialize the HeaderText field value.
            arrControlToAdd = new GridControls.enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = GridControls.enControlToAdd.TextBox;
            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "code", 0);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.ItemStyle.Width = Unit.Pixel(120);
            grdPeiluyot.RowDataBound += new GridViewRowEventHandler(grdPeiluyot_RowDataBound);
            grdPeiluyot.Columns.Add(tGridField);

            grdPeiluyot.DataSource = dtSnifim;
            grdPeiluyot.DataBind();
            //pnlHeader.Controls.Add(grdPeiluyot);
            pnlContent.Controls.Add(grdPeiluyot);
        
        }
    }
    protected void grdPeiluyot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           (((TextBox)(e.Row.Cells[0].Controls[0]))).Text = "vered";

        }
    }

    public class GridControls
    {
        private enControlToAdd[] _gcControlType;
        private string _sMessage;
        private string _sControlID;
        private string _sClientValidationFunction;
        private string _TargetID;
        private ListItemType _Type;
        private string _sColName;
        private int _iIndex;
        private DataTable _Mashar;


        public enum enControlToAdd
        {
            TextBox,
            MaskedTimeEditExtender,
            MaskedNumberEditExtender,
            label,
            ImageButton,
            CustomValidator,
            CustomValidatorExtender
        }

        public GridControls(enControlToAdd[] gcControlType, string sMessage, string sControlID,
                            string sClientValidationFunction, string sTargetID,
                            ListItemType Type, string sColName, int iIndex)
        {
            _gcControlType = gcControlType;
            _sMessage = sMessage;
            _sControlID = sControlID;
            _sClientValidationFunction = sClientValidationFunction;
            _TargetID = sTargetID;
            _Type = Type;
            _sColName = sColName;
            _iIndex = iIndex;
        }
        public enControlToAdd[] gcControlType
        {
            set
            {
                _gcControlType = value;
            }
            get
            {
                return _gcControlType;
            }
        }
        public string Message
        {
            set
            {
                _sMessage = value;
            }
            get
            {
                return _sMessage;
            }
        }
        public string sControlID
        {
            set
            {
                _sControlID = value;
            }
            get
            {
                return _sControlID;
            }
        }
        public string ClientValidationFunction
        {
            set
            {
                _sClientValidationFunction = value;
            }
            get
            {
                return _sClientValidationFunction;
            }
        }
        public string TargetID
        {
            set
            {
                _TargetID = value;
            }
            get
            {
                return _TargetID;
            }
        }
        public ListItemType Type
        {
            set
            {
                _Type = value;
            }
            get
            {
                return _Type;
            }
        }
        public string ColName
        {
            set
            {
                _sColName = value;
            }
            get
            {
                return _sColName;
            }
        }
        public int Index
        {
            set
            {
                _iIndex = value;
            }
            get
            {
                return _iIndex;
            }
        }

    }

    //protected void textTst_TextChanged(object sender, EventArgs e)
    //{

    //}
    //protected void vldTst_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = false;
    //}

    protected void txtDummy_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtDummy_TextChanged1(object sender, EventArgs e)
    {
    }
    public void ClearControl()
    {
        //tbSidurim.Controls.Clear();
        //tbSidurimHeader.Controls.Clear();
    }
    public class GridViewTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;
        //A variable to hold the column name.
        string _columnName;
        GridControls.enControlToAdd[] _arrControlToAdd;
        string _sMessage;
        string _sControlID;
        string _sClientValidationFunction;
        string _sTargetID;
        int _iIndex = 0;

        //Constructor where we define the template type and column name.
        public GridViewTemplate(GridControls oGridControls)
        {
            //Stores the template type.
            _templateType = oGridControls.Type;

            //Stores the column name.
            _columnName = oGridControls.ColName;
            _arrControlToAdd = oGridControls.gcControlType;
            _sClientValidationFunction = oGridControls.ClientValidationFunction;
            _sMessage = oGridControls.Message;
            _sTargetID = oGridControls.TargetID;
            _sControlID = oGridControls.sControlID;
            _iIndex = oGridControls.Index;
        }
        public GridViewTemplate(ListItemType type, string colname)
        {
            //Stores the template type.
            _templateType = type;

            //Stores the column name.
            _columnName = colname;
        }

        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            //enControlToAdd oControlToAdd;
            TextBox tbItem;
            AjaxControlToolkit.MaskedEditExtender MaskTextBox;
            ImageButton Image;
            string sTargetId = "";
            CustomValidator vldCustom;

            switch (_templateType)
            {
                case ListItemType.Header:
                    //Creates a new label control and add it to the container.
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                    break;
                case ListItemType.Item:
                    //Creates a new text box control and add it to the container.
                    if (_arrControlToAdd.Length > 0)
                    {
                        foreach (GridControls.enControlToAdd Item in _arrControlToAdd)
                        {
                            switch (Item)
                            {
                                case GridControls.enControlToAdd.TextBox:
                                    tbItem = AddTextBox(_columnName);
                                    tbItem.DataBinding += new EventHandler(tbItem_DataBinding);
                                    container.Controls.Add(tbItem);
                                    sTargetId = tbItem.ClientID;
                                    break;
                                case GridControls.enControlToAdd.MaskedTimeEditExtender:
                                    //if (!String.IsNullOrEmpty(sTargetId))
                                    //{
                                    //    MaskTextBox = AddTimeMaskedEditExtender(sTargetId);
                                    //    container.Controls.Add(MaskTextBox);
                                    //}
                                    break;
                                case GridControls.enControlToAdd.MaskedNumberEditExtender:
                                    //if (!String.IsNullOrEmpty(sTargetId))
                                    //{
                                    //    MaskTextBox = AddNumberMaskedEditExtender(sTargetId);
                                    //    container.Controls.Add(MaskTextBox);
                                    //}
                                    break;
                                case GridControls.enControlToAdd.ImageButton:
                                    Image = AddImageButton();
                                    container.Controls.Add(Image);
                                    break;
                                case GridControls.enControlToAdd.CustomValidator:
                                    //if (!String.IsNullOrEmpty(sTargetId))
                                    //{
                                    //    vldCustom = AddCustomValidator(sTargetId);
                                    //    container.Controls.Add(vldCustom);
                                    //}
                                    break;
                                //case enControlToAdd.label:
                                //    AddLabel();
                            }
                        }
                    }
                    //Creates a column with size 4.                    
                    //Adds the newly created textbox to the container.
                    break;
                case ListItemType.EditItem:
                    //As, I am not using any EditItem, I didnot added any code here.
                    break;
                case ListItemType.Footer:
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + _columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }


        void tbItem_DataBinding(object sender, EventArgs e)
        {
            TextBox txtdata = (TextBox)sender;
            GridViewRow container = (GridViewRow)txtdata.NamingContainer;

            object dataValue = DataBinder.Eval(container.DataItem, _columnName);

            if (dataValue != DBNull.Value)
            {
                txtdata.Text = dataValue.ToString();
            }
        }
        protected TextBox AddTextBox(string _columnName)
        {
            TextBox tb1 = new TextBox();                            //Allocates the new text box object.
            //tb1.ID = "txt" + _columnName+_iIndex;
            //Attaches the data binding event.
            tb1.Columns = 4;
            return tb1;
        }
        protected AjaxControlToolkit.MaskedEditExtender AddTimeMaskedEditExtender(string sTargetControlId)
        {
            AjaxControlToolkit.MaskedEditExtender MaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
            MaskTextBox.TargetControlID = sTargetControlId;
            MaskTextBox.Mask = "99:99";
            MaskTextBox.MessageValidatorTip = true;
            MaskTextBox.OnFocusCssClass = "MaskedEditFocus";
            MaskTextBox.OnInvalidCssClass = "MaskedEditError";
            MaskTextBox.MaskType = AjaxControlToolkit.MaskedEditType.Time;
            MaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
            MaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.ErrorTooltipEnabled = true;

            return MaskTextBox;
        }
        protected AjaxControlToolkit.MaskedEditExtender AddNumberMaskedEditExtender(string sTargetControlId)
        {
            AjaxControlToolkit.MaskedEditExtender MaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
            MaskTextBox.TargetControlID = sTargetControlId;
            MaskTextBox.Mask = "999999999";
            MaskTextBox.MessageValidatorTip = true;
            MaskTextBox.OnFocusCssClass = "MaskedEditFocus";
            MaskTextBox.OnInvalidCssClass = "MaskedEditError";
            MaskTextBox.MaskType = AjaxControlToolkit.MaskedEditType.Number;
            MaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
            MaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.ErrorTooltipEnabled = true;

            return MaskTextBox;
        }
        protected CustomValidator AddCustomValidator(string sTargetId)
        {
            CustomValidator oVld = new CustomValidator();

            oVld.ErrorMessage = _sMessage;
            oVld.ControlToValidate = sTargetId;
            oVld.ID = _sControlID;
            oVld.Display = ValidatorDisplay.None;
            oVld.EnableClientScript = true;
            oVld.ClientValidationFunction = _sClientValidationFunction;
            return oVld;
        }
        protected ImageButton AddImageButton()
        {
            ImageButton oImage = new ImageButton();
            oImage.ImageUrl = "~/Images/allscreens-cancle-btn.jpg";
            oImage.Click += new ImageClickEventHandler(oImage_Click);
            return oImage;
        }
        protected void AddLabel()
        {
        }
        protected void oImage_Click(object sender, EventArgs e)
        {

        }
    }
    public DataTable dtSnifim
    {
        set
        {
            _dtSnifim = value;
        }
        get
        {
            return _dtSnifim;
        }
    }
}

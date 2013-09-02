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
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.UI.SystemManager;

public partial class DataEntryView : KdsLibrary.UI.SystemManager.KdsSysManPageBase//System.Web.UI.Page
{
    private clGeneral.enDynamicFormType CurrentFormType;

    private enum  GridType {Data,History};

    

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ((ScriptManager)Master.FindControl("ScriptManagerKds")).EnableScriptGlobalization = true;
    }

    protected override bool BindDataOnLoad
    {
        get {
            CurrentFormType = (clGeneral.enDynamicFormType)ViewState["CurrentFormType"];
            if (CurrentFormType == clGeneral.enDynamicFormType.Parameters)
                return true;
            else return false; 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage mp = (MasterPage)Page.Master;

        SetCurrentFormType();
        SetDefaultValueOfDataEntryEdit();
        SetFilterKodDescription();
        EnableButtons();
        EnableRefreshTable();
        
        mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);

        if (!Page.IsPostBack)
        {
            clGeneral.LoadDateCombo(ddlMonth, 15);
            rdoFilterKod.Attributes.Add("OnClick", "InitTextBox()");
            LoadMessages((DataList)Master.FindControl("lstMessages"));
            ServicePath = "~/Modules/WebServices/wsDynamicForms.asmx";
            //    SetFixedHeaderGrid("ctl00_KdsContent_PnlGridHistory", mp.HeadPage);
            if ((CurrentFormType == clGeneral.enDynamicFormType.ComponentSidurim) || (CurrentFormType == clGeneral.enDynamicFormType.ComponentTypeSidur))
                PnlKodDescription.Visible = false;
        }
    }
    void ImagePrintClick_Click(object sender, ImageClickEventArgs e)
    {
        btnPrint_Click(sender, e);
    }

    protected void btnRefreshPrint_click(object sender, EventArgs e)
    {
        RefreshAftePrint();
    }
    private void EnableButtons()
    {
        if (CurrentFormType == clGeneral.enDynamicFormType.Parameters)
            {   
            btnDisplay.Enabled = true ;
            btnNew.Enabled= true ; 
            }
            else 
            {
                btnDisplay.Enabled= (txtFilterKod.Text=="") ? false  : true;
                btnNew.Enabled = (txtFilterKod.Text == "") ? false : true;
            }
            btnDisplay.CssClass = btnDisplay.Enabled? "ImgButtonSearch" : "ImgButtonSearchDisable" ;
            btnNew.CssClass = btnNew.Enabled? "ImgButtonSearch" : "ImgButtonSearchDisable"; 
    }
    private void EnableRefreshTable()
    {
        if ((CurrentFormType == clGeneral.enDynamicFormType.SpecialSidur) ||
            (CurrentFormType == clGeneral.enDynamicFormType.SugSidur) ||
            (CurrentFormType == clGeneral.enDynamicFormType.Elements) )
            DivRefreshTable.Style.Add("Display","block");
    }

    #region Setting Page Load

    private void SetCurrentFormType()
    {
        CurrentFormType = (clGeneral.enDynamicFormType)Enum.Parse(typeof(clGeneral.enDynamicFormType), _dataSourceName);
        ViewState["CurrentFormType"] = CurrentFormType;
        txtFormType.Text = ((int)CurrentFormType).ToString();
        AutoCompleteKod.ContextKey = ((int)CurrentFormType).ToString();
        AutoCompleteDescription.ContextKey = ((int)CurrentFormType).ToString();
    }


    private void SetFilterKodDescription()
    {
        switch (CurrentFormType)
        {
            case clGeneral.enDynamicFormType.SpecialSidur:
            case clGeneral.enDynamicFormType.ComponentSidurim:
                rdoFilterKod.Text = "מספר סידור מיוחד";
                break;
            case clGeneral.enDynamicFormType.SugSidur:
            case clGeneral.enDynamicFormType.ComponentTypeSidur:
                rdoFilterKod.Text = "סוג סידור";
                break;
            case clGeneral.enDynamicFormType.Elements:
                rdoFilterKod.Text = "מספר אלמנט";
                break;
            case clGeneral.enDynamicFormType.Parameters:
                rdoFilterKod.Text = "מספר פרמטר";
                break;
        }
    }

    private void SetDefaultValueOfDataEntryEdit()
    {
        switch (CurrentFormType)
        {
            case clGeneral.enDynamicFormType.SpecialSidur:
            case clGeneral.enDynamicFormType.ComponentSidurim:
                AddDefaultValue("MISPAR_SIDUR", "txtFilterKod");
                break;
            case clGeneral.enDynamicFormType.SugSidur:
            case clGeneral.enDynamicFormType.ComponentTypeSidur:
                AddDefaultValue("SUG_SIDUR", "txtFilterKod");
                break;
            case clGeneral.enDynamicFormType.Elements:
                AddDefaultValue("KOD_ELEMENT", "txtFilterKod");
                break;
        }
        
    }

    #endregion 

    #region GridView1 Setting 
    
    private void SetStyleOfGridView()
    {
        for (int i = 0; i < GridView1.Rows.Count; i = i + 2)
        {
            GridView1.Rows[i].CssClass = "GridRow";
            if (i < GridView1.Rows.Count - 1)
                GridView1.Rows[i + 1].CssClass = "GridAltRow";
        }
    }

    private void SetStyleColumn(GridViewRow Row, GridType Type)
    {
        switch (CurrentFormType)
        {
            case clGeneral.enDynamicFormType.Elements:
            case clGeneral.enDynamicFormType.SpecialSidur:
            case clGeneral.enDynamicFormType.SugSidur:
                if (Type == GridType.Data)
                {
                    Row.Cells[1].Style["text-align"] = "right"; // property
                    Row.Cells[4].Style["text-align"] = "center";// FromDate
                    Row.Cells[5].Style["text-align"] = "center";// ToDate
                    Row.Cells[6].Style["text-align"] = "center"; // Value
                    Row.Cells[7].Style["text-align"] = "center"; // LastDate
                    Row.Cells[8].Style["text-align"] = "right"; // Comment
                }
                else
                {
                    Row.Cells[0].Style["text-align"] = "right"; // property
                    Row.Cells[1].Style["text-align"] = "center";// FromDate
                    Row.Cells[2].Style["text-align"] = "center";// ToDate

                }
                break;
            case clGeneral.enDynamicFormType.Parameters:
                if (Type == GridType.Data)
                {
                    Row.Cells[1].Style["text-align"] = "right"; // property
                    Row.Cells[3].Style["text-align"] = "center";// FromDate
                    Row.Cells[4].Style["text-align"] = "center";// ToDate
                    Row.Cells[5].Style["text-align"] = "center"; // Value
                    Row.Cells[6].Style["text-align"] = "center"; // LastDate
                    Row.Cells[7].Style["text-align"] = "right"; // Comment
                }
                else
                {
                    Row.Cells[0].Style["text-align"] = "right"; // property
                    Row.Cells[1].Style["text-align"] = "center";// FromDate
                    Row.Cells[2].Style["text-align"] = "center";// ToDate
                }
                break;
            case clGeneral.enDynamicFormType.ComponentSidurim:
            case clGeneral.enDynamicFormType.ComponentTypeSidur:
                if (Type == GridType.Data)
                {
                    Row.Cells[0].Style["text-align"] = "right";// property
                    Row.Cells[1].Style["text-align"] = "center";// FromDate
                    Row.Cells[2].Style["text-align"] = "center"; // ToDate
                    Row.Cells[3].Style["text-align"] = "center"; // Value
                }
                else
                {
                    Row.Cells[1].Style["text-align"] = "right";// property
                    Row.Cells[2].Style["text-align"] = "center";// FromDate
                    Row.Cells[3].Style["text-align"] = "center"; // ToDate
                }
                break;

        }
    }

    private void SetWidthColumn(GridViewRow Row, GridType Type)
    {
        switch (CurrentFormType)
        {
            case clGeneral.enDynamicFormType.Elements:
            case clGeneral.enDynamicFormType.SpecialSidur:
            case clGeneral.enDynamicFormType.SugSidur:
                if (Type == GridType.Data)
                {
                    Row.Cells[1].Width = 400; // property
                    Row.Cells[4].Width = 80; // FromDate
                    Row.Cells[5].Width = 80;   // ToDate
                    Row.Cells[6].Width = 90;    // Value
                    Row.Cells[8].Width = 130; // LastDate
                    Row.Cells[9].Width = 180; // Comment
                }
                else
                {
                    Row.Cells[0].Width = 120; // property
                    Row.Cells[1].Width = 120; // FromDate
                    Row.Cells[2].Width = 120;   // ToDate
                }
                break;
            case clGeneral.enDynamicFormType.Parameters:
                if (Type == GridType.Data)
                {
                    Row.Cells[1].Width = 350; // property
                    Row.Cells[3].Width = 80; // FromDate
                    Row.Cells[4].Width = 80;   // ToDate
                    Row.Cells[5].Width = 90;    // Value
                    Row.Cells[6].Width = 130; // LastDate
                    Row.Cells[8].Width = 180; // Comment
                }
                else
                {
                    Row.Cells[0].Width = 120; // property
                    Row.Cells[1].Width = 120; // FromDate
                    Row.Cells[2].Width = 120;   // ToDate
                }
                break;
            case clGeneral.enDynamicFormType.ComponentSidurim:
            case clGeneral.enDynamicFormType.ComponentTypeSidur:
                if (Type == GridType.Data)
                {
                    Row.Cells[0].Width = (Type == GridType.Data) ? 450 : 430; // property
                    Row.Cells[1].Width = (Type == GridType.Data) ? 120 : 90; // FromDate
                    Row.Cells[2].Width = 150;   // ToDate
                    Row.Cells[3].Width = 140;   // Last Update
                }
                else
                {
                    Row.Cells[1].Width = (Type == GridType.Data) ? 450 : 430; // property
                    Row.Cells[2].Width = (Type == GridType.Data) ? 120 : 90; // FromDate
                    Row.Cells[3].Width = 150;   // ToDate
                }
                break;
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        DivData.Visible = true ;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CurrentFormType = (clGeneral.enDynamicFormType)ViewState["CurrentFormType"];
        string CurrentKod = string.Empty;
        //GridView1.AutoGenerateColumns = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell Cell in e.Row.Cells)
            {
                if ((Cell.Controls.Count ==0) ||(Cell.Controls[1].ID != "lbDelete"))
                    Cell.Attributes.Add("OnClick", "javascript:CheckChanged(" + e.Row.RowIndex + ");");
            }
            SetStyleColumn((GridViewRow)e.Row, GridType.Data);
        }
        if (e.Row.RowType == DataControlRowType.Header)// || e.Row.RowType == DataControlRowType.DataRow)
            SetWidthColumn((GridViewRow)e.Row,GridType.Data);
        if (e.Row.RowIndex == 0)
        {
            SetVariableForHistory(e.Row);
            SetHistoryTable();
        }

    }

    #endregion


    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        ViewState["CurrentKod"] = "";
        ViewState["CurrentDescription"] = "";
        ViewState["HistoryAtDate"] = "";
        RefreshData();
        SetHistoryTable();
        PnlGridHistory.ScrollBars = (grdHistory.Rows.Count > 2) ? ScrollBars.Vertical : ScrollBars.None;
    }
    protected void BntRefreshTable_Click(object sender, EventArgs e)
    {
        try
        {
            clDynamicForms oSidurimBl = clDynamicForms.GetInstance();
            oSidurimBl.RefreshTable(CurrentFormType);
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }

    #region History Code 


    protected void btnGetHistory_Click(object sender, EventArgs e)
    {
        try
        {
            SetStyleOfGridView();
            int iRowId = int.Parse(TxtSelectedRow.Text.ToString());
            GridView1.Rows[iRowId].CssClass = "SelectedGridRow";
            SetVariableForHistory(GridView1.Rows[iRowId]);
            if ((CurrentFormType != clGeneral.enDynamicFormType.ComponentSidurim) && (CurrentFormType != clGeneral.enDynamicFormType.ComponentTypeSidur))
                SetHistoryTable();
            PnlGridHistory.ScrollBars = (grdHistory.Rows.Count > 2) ? ScrollBars.Vertical : ScrollBars.None;

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }
    private void SetVariableForHistory(GridViewRow CurrentRow )
    {
        string[] sLabel = CurrentRow.Cells[2].Text.Split('-');
        ViewState["CurrentKod"] = sLabel[0];
        ViewState["CurrentDescription"] = sLabel[1];
        CurrentFormType = (clGeneral.enDynamicFormType)ViewState["CurrentFormType"]; CurrentFormType = (clGeneral.enDynamicFormType)ViewState["CurrentFormType"];
        if (CurrentFormType == clGeneral.enDynamicFormType.Parameters) 
        ViewState["HistoryAtDate"] = CurrentRow.Cells[3].Text.ToString();
        else         ViewState["HistoryAtDate"] = CurrentRow.Cells[4].Text.ToString();
    }
    private void SetHistoryTable()
    {
        DataTable dt;
        DataView dv;
        try
        {
            TxtKodHistory.Text = (string)ViewState["CurrentKod"];
            TxtDescriptionHistory.Text = (string)ViewState["CurrentDescription"];
            if ((CurrentFormType == clGeneral.enDynamicFormType.ComponentSidurim) |
                (CurrentFormType == clGeneral.enDynamicFormType.ComponentTypeSidur))
                dt = GetDataHistory((string)TxtKodHistory.Text, (string)ddlMonth.Text);
            else
                dt = GetDataHistory((string)TxtKodHistory.Text, (string)ViewState["HistoryAtDate"]);
            dv = new DataView(dt);
            grdHistory.PageIndex = 0;
            AddBoundFieldToColumn(dt);
            ViewState["SortHistoryDirection"] = SortDirection.Ascending;
            ViewState["SortExp"] = "ME_TAARICH";
            Session["DvHistory"] = dv;
            BindGridHistory();

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }
    private void AddBoundFieldToColumn(DataTable Dt)
    {
        try
        {
            if (grdHistory.Columns.Count == 0)
            {
                foreach (DataColumn col in Dt.Columns)
                {
                    BoundField bField = new BoundField();
                    bField.DataField = col.ColumnName;
                    bField.SortExpression = (col.ColumnName== "DETAILSCOMPONENT") ? "KOD_RECHIV" : col.ColumnName;
                    bField.HeaderText = col.Caption;
                    SetFormatBoundField(bField);
                    grdHistory.Columns.Add(bField);
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }
    private void SetFormatBoundField(BoundField Bf)
    {
        if ((Bf.DataField== "ME_TAARICH") || (Bf.DataField== "AD_TAARICH"))
                Bf.DataFormatString = "{0:dd/MM/yyyy}";
    }

    private DataTable GetDataHistory(string Kod,string ToDate)
    {
        clDynamicForms oSidurimBl = clDynamicForms.GetInstance();
        DataTable dt = null;
        try
        {
            CurrentFormType = (clGeneral.enDynamicFormType)ViewState["CurrentFormType"];
            dt = oSidurimBl.GetHistory(txtFilterKod.Text.ToString(), Kod, ToDate, CurrentFormType);
            SetCaptionGridHistory(dt);
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
        return dt;
    }
    private void SetCaptionGridHistory(DataTable Dt)
    {
        if ((CurrentFormType == clGeneral.enDynamicFormType.ComponentSidurim) | (CurrentFormType== clGeneral.enDynamicFormType.ComponentTypeSidur))
        {
            Dt.Columns[1].Caption = "פרטי הרכיב";
            Dt.Columns[2].Caption = "מתאריך";
            Dt.Columns[3].Caption = "עד תאריך";
        }
        else
        {
            Dt.Columns[0].Caption = "מתאריך";
            Dt.Columns[1].Caption = "עד תאריך";
            Dt.Columns[2].Caption = "ערך";
        }
    }
    protected void grdHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHistory.PageIndex = e.NewPageIndex;
        BindGridHistory();
    }

    protected void grdHistory_Sorting(object sender, GridViewSortEventArgs e)
    {
        if ((string.Empty != (string)ViewState["SortExp"]) && (string.Compare(e.SortExpression, (string)ViewState["SortExp"], true) == 0))
        {
            if ((SortDirection)ViewState["SortHistoryDirection"] == SortDirection.Ascending)
                ViewState["SortHistoryDirection"] = SortDirection.Descending;
            else
                ViewState["SortHistoryDirection"] = SortDirection.Ascending;
        }
        else ViewState["SortHistoryDirection"] = SortDirection.Ascending;
        ViewState["SortExp"] = e.SortExpression;

        if (Session["DvHistory"] != null)
        {
            if ((SortDirection)ViewState["SortHistoryDirection"] == SortDirection.Ascending)
                ((DataView)Session["DvHistory"]).Sort = string.Concat(e.SortExpression, " ASC");
            else ((DataView)Session["DvHistory"]).Sort = string.Concat(e.SortExpression, " DESC");
            BindGridHistory();
        }
    }


    protected void grdHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();

            iColSort = GetCurrentColSort();
            lbl.Text = " ";
            e.Row.Cells[iColSort].Controls.Add(lbl);
            if ((SortDirection)ViewState["SortHistoryDirection"] == SortDirection.Descending)
            {
                System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                ImageSort.ID = "imgDescSort";
                ImageSort.ImageUrl = "../../Images/DescSort.gif";
                e.Row.Cells[iColSort].Controls.Add(ImageSort);
            }
            else
            {
                System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                ImageSort.ID = "imgAsccSort";
                ImageSort.ImageUrl = "../../Images/AscSort.gif";
                e.Row.Cells[iColSort].Controls.Add(ImageSort);
            }

            int i = 0;
            object sortHeader = null;

            for (i = 0; i < e.Row.Cells.Count; i++)
            {
                sortHeader = e.Row.Cells[i].Controls[0];
                ((LinkButton)(sortHeader)).Style.Add("color", "white");
                ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
            }
            SetWidthColumn((GridViewRow)e.Row, GridType.History);
            SetStyleColumn((GridViewRow)e.Row, GridType.History);
            if ((CurrentFormType == clGeneral.enDynamicFormType.ComponentSidurim) || (CurrentFormType == clGeneral.enDynamicFormType.ComponentTypeSidur))
                e.Row.Cells[0].Style.Add("display", "none");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SetWidthColumn((GridViewRow)e.Row, GridType.History);
            SetStyleColumn((GridViewRow)e.Row, GridType.History);
            if ((CurrentFormType == clGeneral.enDynamicFormType.ComponentSidurim) || (CurrentFormType == clGeneral.enDynamicFormType.ComponentTypeSidur))
                e.Row.Cells[0].Style.Add("display", "none");

        }

    }
    private void BindGridHistory()
    {
        try 
        {
        grdHistory.DataSource = (DataView)Session["DvHistory"];
        grdHistory.DataBind();
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }


    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdHistory.Columns)
            {
                iColNum++;
                if (dc.SortExpression.Equals(sSortExp)) { break; }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
        return iColNum;
    }
    protected void grdHistory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            IGridViewPager gridPager = e.Row.FindControl("ucGridHistoryPager")
            as IGridViewPager;
            if (gridPager != null)
            {
                gridPager.PageIndexChanged += delegate(object pagerSender,
                    GridViewPageEventArgs pagerArgs)
                {
                    ChangeGridPage(pagerArgs.NewPageIndex, grdHistory,
                        (DataView)Session["DvHistory"], "SortHistoryDirection",
                        "SortExp");
                };
            }
        }
    }

    //private void ChangeGridPage(int pageIndex)
    //{
    //    grdHistory.PageIndex = pageIndex;
    //    var dataView = (DataView)Session["DvHistory"];
    //    string sortExpr = String.Empty;
    //    SortDirection sortDir = SortDirection.Ascending;
    //    if (ViewState["SortExp"] != null)
    //    {
    //        sortExpr = ViewState["SortExp"].ToString();
    //        if (ViewState["SortHistoryDirection"] != null)
    //            sortDir = (SortDirection)ViewState["SortHistoryDirection"];
    //        dataView.Sort = String.Format("{0} {1}", sortExpr,
    //            ConvertSortDirectionToSql(sortDir));
    //    }
    //    grdHistory.DataSource = dataView;
    //    grdHistory.DataBind();
    //}

    #endregion


}

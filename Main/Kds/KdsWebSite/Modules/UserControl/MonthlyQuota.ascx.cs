using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.Utils;
using KdsLibrary.UI;
using KdsLibrary.UI.SystemManager;
using KdsWorkFlow.Approvals;

public partial class Modules_UserControl_MonthlyQuota : System.Web.UI.UserControl
{
    private int _iQuotaDemand = 0;
    private string _EmployeeNumber = string.Empty;
    private clGeneral.enMonthlyQuotaForm _FormType; 
 //   public event EventHandler ErrorRaised ; 

    protected enum enColumn
    {
        ApprovalAll = 0,
        Kod_Status_Ishur = 2,
        Rama = 3,
        Mispar_Ishi = 4,
        Shem = 5,
        Agaf = 6,
        Snif = 7,
        Maamad = 8,
        Isuk = 9,
        TaarichMMYYYY = 10,
        Mevukash = 11,
        Siba = 12,
        Meushar_MenahelYashir = 13,
        Meushar_Agafit = 14,
        UavarLeIshurVaad = 15,
        UsharAlVaad = 16,
        Bakasha_ID = 17,
        Kod_Ishur = 18,
        Taarich = 19,
        Mispar_Sidur = 20,
        Shat_Hatchala = 21,
        Shat_Yetzia = 22,
        Mispar_Knisa = 23 ,
        Original_Status_Ishur = 24 
    }
    public void Init(string EmployeeNumber, clGeneral.enMonthlyQuotaForm FormType)
    {
        _EmployeeNumber = EmployeeNumber;   
        _FormType = FormType;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                SetViewStateVariable();
                rdoDemandsInTreatment.Attributes.Add("OnClick", "SetAutoCompleteExtender()");
                rdoTreatedDemands.Attributes.Add("OnClick", "SetAutoCompleteExtender()");
                EnableSearchWorker();
                SetValuesOfDdlMonth();
            }
            rdoDemandsInTreatment.AutoPostBack = (_FormType == clGeneral.enMonthlyQuotaForm.VaadatPnim);
            rdoTreatedDemands.AutoPostBack = (_FormType == clGeneral.enMonthlyQuotaForm.VaadatPnim);
  //          if (_FormType == clGeneral.enMonthlyQuotaForm.Agafit)
    //        {
                rdoDemandsInTreatment.AutoPostBack = true;
                rdoTreatedDemands.AutoPostBack = true;
                rdoDemandsInTreatment.CheckedChanged += new EventHandler(DemandTypeChanged);
                rdoTreatedDemands.CheckedChanged += new EventHandler(DemandTypeChanged);
      //      }
            SetScreenByWorkerType();
            SetAutoCompleteExtender();
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }

    }
    protected void DemandTypeChanged(object sender, EventArgs e)
    {
        SetValuesOfDdlMonth();
    }

    private void SetValuesOfDdlMonth()
    {
        DataTable dtParametrim = new DataTable();
        clUtils oUtils = new clUtils();
        try
        {
            ddlMonth.Items.Clear();
            dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());

            if (_FormType == clGeneral.enMonthlyQuotaForm.VaadatPnim)
            {
                ddlMonth.Items.Add("הכל");
                if (DemandType == clGeneral.enDemandType.Treated)
                    clGeneral.LoadDateCombo(ddlMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
                // clGeneral.LoadDateCombo(ddlMonth, 10, DateTime.Now.AddMonths(-1));
            }
            else
            {
                SetMontlyAndSharedQuota();
                clGeneral.LoadDateCombo(ddlMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
                //  clGeneral.LoadDateCombo(ddlMonth, 10, DateTime.Now.AddMonths(-1));
                if (DemandType == clGeneral.enDemandType.InTreatment)
                {
                    clOvdim _ovdim = clOvdim.GetInstance();
                    DataTable dt = new DataTable();
                    DataRow[] Drs;
                    dt = _ovdim.GetRelevantMonthOfApproval(iMisparIshi);
                    foreach (ListItem item in ddlMonth.Items)
                    {
                        Drs = dt.Select("RelevantMonths ='" + item.Value + "'");
                        if (Drs.Length > 0)
                            item.Text = item.Text + "*";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private void EnableSearchWorker()
    {
        btnSearchWorker.Enabled = (txtId.Text == "") ? false : true;
        btnSearchWorker.ControlStyle.CssClass = (txtId.Text == "") ? "ImgButtonSearchDisable" : "ImgButtonSearch";
        if (txtId.Text != "")
        {
            txtId.Text = "";
            rdoId.Checked = true;
            txtName.Text = "";
            txtName.Enabled = false;
        }
    }
    private void SetAutoCompleteExtender()
    {
        AutoCompleteExtenderByName.ContextKey = "Shem," + (int)DemandType + "," + _EmployeeNumber.ToString() + "," + DdlMonth + "," + (int)_FormType;
        AutoCompleteExtenderID.ContextKey = "mispar_ishi," + (int)DemandType + "," + _EmployeeNumber.ToString() + "," + DdlMonth + "," + (int)_FormType;
    }
    private void SetViewStateVariable()
    {
        try
        {
            if (_EmployeeNumber != "")
            {
                TxtUserID.Text = _EmployeeNumber;
                clOvdim oOvdim = clOvdim.GetInstance();
                TxtStatusIsuk.Text = oOvdim.GetStatusIsuk((int)_FormType, iMisparIshi, DdlMonth).ToString();
            }
            else clGeneral.BuildError(Page, "הנך משתמש לא מוגדר מערכת, ולכן יתכננו שיבושים ", false);
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
            //EventArgs evArgs = new EventArgs() ;
            //ErrorRaised(this, evArgs);

        }
    }
    private void SetScreenByWorkerType()
    {
        if (_FormType == clGeneral.enMonthlyQuotaForm.VaadatPnim)
        {
            DivQuotaSumAndBalance.Style.Add("Display", "none");
            DivQuotaDetails.Style.Add("Display", "none");
        }
        else
        {
            if (DemandType == clGeneral.enDemandType.Treated)
                DivQuotaSumAndBalance.Style.Add("Display", "none");
            else DivQuotaSumAndBalance.Style.Add("Display", "block");
            DivQuotaDetails.Style.Add("Display", "block");
        }
    }

    #region private properties

    private int iMisparIshi
    {
        get
        {
            return clGeneral.GetIntegerValue(_EmployeeNumber.ToString());
        }
    }
    private clGeneral.enDemandType DemandType
    {
        get
        {
            return (rdoDemandsInTreatment.Checked) ? clGeneral.enDemandType.InTreatment : clGeneral.enDemandType.Treated;
        }
    }
    private string DdlMonth
    {
        get
        {
            return (ddlMonth.Text.ToString() != "הכל") ? ddlMonth.Text.ToString() : "";
        }
    }


    #endregion


    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        try
        {
            EnableSearchWorker();
            SetScreenByWorkerType();
            if (_FormType != clGeneral.enMonthlyQuotaForm.VaadatPnim)
                SetMontlyAndSharedQuota();
            DivData.Style.Add("Display", "block");
            GetIshurim();
            TxtSumMevukash.Text = "0";
            TxtQuotaBalanceGrid.Text = txtBalanceQuota.Text.ToString();
            

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }

    }
    private void GetIshurim()
    {
        clOvdim oOvdim = clOvdim.GetInstance();
        DataTable dt = null;
        DataView dv;
        try
        {
            dt = oOvdim.GetIshurim(DemandType, iMisparIshi, DdlMonth, _FormType, "");
            dv = new DataView(dt);
            Session["DvIshurim"] = dv;
            grdIshurim.PageIndex = 0;
            grdIshurim.DataSource = dv;
            grdIshurim.DataBind();
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }


    private void SetMontlyAndSharedQuota()
    {
        int MonthlyQuota = 0, SharedQuota = 0;
        try
        {
            clOvdim oOvdim = clOvdim.GetInstance();
            oOvdim.GetMontlyAndSharedQuota(iMisparIshi, DdlMonth, ref MonthlyQuota, ref SharedQuota);
            txtMonthlyQuota.Text = MonthlyQuota.ToString();
            txtSharedQuota.Text = SharedQuota.ToString();
            txtBalanceQuota.Text = (MonthlyQuota - SharedQuota).ToString();
            TxtQuotaBalanceGrid.CssClass = ((MonthlyQuota - SharedQuota) >= 0) ? "TextPositiveValue" : "TextNegativeValue";
//            SetBalanceQuotaGrid();
            TdBtnUpdate.Width = (DemandType == clGeneral.enDemandType.InTreatment) ? "470px" : "700px";
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }

    protected void grdIshurim_RowBound(object sender, EventArgs e)
    {
        GridView Gv = (GridView)sender;
        try
        {
            if (Gv.Rows.Count > 0)
            {
                DivDataQuotaUpdate.Style.Add("Display", "block");
                PnlSearchWorker.Enabled = true;
            }
            else
            {
                DivDataQuotaUpdate.Style.Add("Display", "none");
                PnlSearchWorker.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }

    protected void grdIshurim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
                SetUnvisibleColumns(e.Row);
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                RadioButton rdoApproved = (RadioButton)e.Row.FindControl("rdoApproved");
                rdoApproved.Checked = (e.Row.Cells[(int)enColumn.Kod_Status_Ishur].Text == "1") ? true : false;
                RadioButton rdoNotApproved = (RadioButton)e.Row.FindControl("rdoNotApproved");
                rdoNotApproved.Checked = (e.Row.Cells[(int)enColumn.Kod_Status_Ishur].Text == "2") ? true : false;
                SetUnvisibleColumns(e.Row);
                if (_FormType == clGeneral.enMonthlyQuotaForm.VaadatPnim)
                {
                    if (rdoDemandsInTreatment.Checked)
                        PrepareLinkOfCell(enColumn.UavarLeIshurVaad, e.Row);
                    else if (rdoTreatedDemands.Checked)
                        PrepareLinkOfCell(enColumn.UsharAlVaad, e.Row);
                }
                else
                {
                    if (e.Row.Cells[(int)enColumn.UsharAlVaad].Text == "&nbsp;")
                    {
                        if (rdoDemandsInTreatment.Checked)
                            PrepareLinkOfCell(enColumn.Mevukash, e.Row);
                        else if (rdoTreatedDemands.Checked)
                            PrepareLinkOfCell(enColumn.Meushar_Agafit, e.Row);
                    }
                    else
                    {
                        rdoApproved.Enabled = false;
                        rdoNotApproved.Enabled = false;
                    }
                }
                PrepareLinkOfCell(enColumn.Mispar_Ishi, e.Row);
                CreateTooltip(enColumn.Siba, e.Row);
                CreateTooltip(enColumn.Snif, e.Row);
                CreateTooltip(enColumn.Agaf, e.Row);
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }

    }
    public void StatusIshur_Click(object sender, EventArgs e)
    {
        DataView Dv = (DataView)Session["DvIshurim"];
        SetChangesOfGridInDataview(grdIshurim, ref Dv);
        SetSums(Dv);
        SetEnableBtnUpdate();
        grdIshurim.DataSource = Dv;
        grdIshurim.DataBind();
    }
    public void HplApprovalAll_Click(object sender, EventArgs e)
    {
        SetApprovalAll(true);
    }
    public void HplApprovalClear_Click(object sender, EventArgs e)
    {
        SetApprovalAll(false);
    }
    private void SetApprovalAll(bool Checked)
    {
        DataView Dv = (DataView)Session["DvIshurim"];
        bool bNotApproval = false;
        SetChangesOfGridInDataview(grdIshurim, ref Dv);
        for (int index = 0; index < Dv.Table.Rows.Count; index++)
        {
            bNotApproval = (bool) (Dv.Table.Rows[index][(int)enColumn.ApprovalAll].ToString()== "2") ;
            if (Checked)
                Dv.Table.Rows[index][(int)enColumn.ApprovalAll] = "1" ;
            if (!bNotApproval && !(Checked))
                Dv.Table.Rows[index][(int)enColumn.ApprovalAll] = "0";
        }
    //    SetChangesOfGridInDataview(grdIshurim, ref Dv);
        SetSums(Dv);
        SetEnableBtnUpdate();
        grdIshurim.DataSource = Dv;
        grdIshurim.DataBind();
    }
    private void SetSums(DataView Dv)
    {
        int Sum = 0, BalanceQuota = 0;
        string ColumnToSum = (DemandType == clGeneral.enDemandType.InTreatment) ? enColumn.Mevukash.ToString() : enColumn.Meushar_Agafit.ToString();
        for (int index = 0; index < Dv.Table.Rows.Count; index++)
            if ((bool)(Dv.Table.Rows[index][(int)enColumn.ApprovalAll].ToString() == "1"))
                Sum += clGeneral.GetIntegerValue(Dv.Table.Rows[index][ColumnToSum].ToString());
        TxtSumMevukash.Text = Sum.ToString();
        Sum = clGeneral.GetIntegerValue(TxtSumMevukash.Text);
        BalanceQuota = (clGeneral.GetIntegerValue(txtBalanceQuota.Text) - Sum);
        TxtQuotaBalanceGrid.Text = BalanceQuota.ToString();
        TxtQuotaBalanceGrid.CssClass = (BalanceQuota > 0) ? "TextPositiveValue" : "TextNegativeValue";
    }
    private void SetEnableBtnUpdate()
    {
        if (_FormType == clGeneral.enMonthlyQuotaForm.Agafit)
        {
            btnUpdate.Enabled = (clGeneral.GetIntegerValue(TxtQuotaBalanceGrid.Text) > 0);
            btnUpdate.CssClass = ((clGeneral.GetIntegerValue(TxtQuotaBalanceGrid.Text) > 0)) ? "ImgButtonSearch" : "ImgButtonSearchDisable";
        }
    }
    protected void grdIshurim_RowCreated(object sender, GridViewRowEventArgs e)
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
                    ChangeGridPage(pagerArgs.NewPageIndex, grdIshurim,
                        (DataView)Session["DvIshurim"], "SortDirection",
                        "SortExp");
                };
            }
        }
    }
    private void ChangeGridPage(int pageIndex, GridView grid, DataView dataView,
    string sortDirViewStateKey, string sortExprViewStateKey)
    {
        SetChangesOfGridInDataview(grid, ref dataView);
        grid.PageIndex = pageIndex;
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
    }

    private void SetChangesOfGridInDataview(GridView Grid,ref  DataView Dv)
    {
        int PureIndex = 0;
        for (int index = 0 ; index <  Grid.Rows.Count ; index++)
        {
            if (Grid.PageIndex == 0)
                PureIndex = index;
            else
                PureIndex = ((Grid.PageIndex) * Grid.PageSize) + index ;
            RadioButton rdoApproved = (RadioButton)Grid.Rows[index].FindControl("rdoApproved");
            RadioButton rdoNotApproved = (RadioButton)Grid.Rows[index].FindControl("rdoNotApproved");
            if (rdoApproved.Checked == true)
                Dv.Table.Rows[PureIndex][(int)enColumn.ApprovalAll] = "1";
            else if (rdoNotApproved.Checked == true)
                Dv.Table.Rows[PureIndex][(int)enColumn.ApprovalAll] = "2";
        }

    }



    private string ConvertSortDirectionToSql(SortDirection sortDirection)
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
      
    private void CreateTooltip(enColumn ColumnOfCell, GridViewRow row)
    {
        const int MaxLength = 15;
        TableCell CurrentCell = new TableCell();
        try
        {
            CurrentCell = row.Cells[(int)ColumnOfCell];
            CurrentCell.Text = CurrentCell.Text.Replace("&quot;", @"""");
            if (CurrentCell.Text.Length > MaxLength)
            {
                CurrentCell.ToolTip = CurrentCell.Text;
                CurrentCell.Text = CurrentCell.Text.Remove(MaxLength) + "..";
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private void CalcSumQuotaDemand(GridViewRow row)
    {
        TableCell CellToSum = new TableCell();
        int CurrentQuota = 0;
        try
        {
            switch (DemandType)
            {
                case clGeneral.enDemandType.InTreatment:
                    CellToSum = row.Cells[(int)enColumn.Mevukash];
                    break;
                case clGeneral.enDemandType.Treated:
                    CellToSum = row.Cells[(int)enColumn.Meushar_Agafit];
                    break;
            }
            if (_FormType == clGeneral.enMonthlyQuotaForm.VaadatPnim)
            {
                Int32.TryParse(CellToSum.Text.ToString(), out CurrentQuota);
                if (row.Cells[(int)enColumn.Kod_Status_Ishur].Text == "1")
                    _iQuotaDemand += CurrentQuota;
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private void PrepareLinkOfCell(enColumn ColumnOfCell, GridViewRow row)
    {
        TableCell CurrentCell = new TableCell();
        try
        {
            CurrentCell = row.Cells[(int)ColumnOfCell];
            if (CurrentCell.Text != "&nbsp;")
            {
                CurrentCell.Style.Add("cursor", "pointer");
                CurrentCell.Style.Add("text-decoration", "underline");
                CurrentCell.Style.Add("Color", "Blue");

                switch (ColumnOfCell)
                {
                    case enColumn.Mispar_Ishi:
                        CurrentCell.Attributes.Add("onclick", "OpenDialogBakashatTashlum('" +
                          CurrentCell.Text.ToString() +
                          "','" + row.Cells[(int)enColumn.Bakasha_ID].Text +
                          "','" + row.Cells[(int)enColumn.TaarichMMYYYY].Text + "');");
                        break;
                    case enColumn.Mevukash:
                        CurrentCell.Attributes.Add("onclick", "OpenDialogApprovalMonthlyQuota(" +
                            (int)_FormType + "," + row.RowIndex + ",'" +
                            row.Cells[(int)enColumn.Mispar_Ishi].Text + "','" + row.Cells[(int)enColumn.Mevukash].Text +
                            "','" + row.Cells[(int)enColumn.Mevukash].Text + "','0','0');");
                        break;
                    case enColumn.Meushar_Agafit:
                        CurrentCell.Attributes.Add("onclick", "OpenDialogApprovalMonthlyQuota(" +
                            (int)_FormType + "," + row.RowIndex + ",'" +
                            row.Cells[(int)enColumn.Mispar_Ishi].Text + "','" + row.Cells[(int)enColumn.Mevukash].Text +
                            "','" + row.Cells[(int)enColumn.Meushar_Agafit].Text +
                            "','" + row.Cells[(int)enColumn.UavarLeIshurVaad].Text +
                            "','0','','');");
                        break;
                    case enColumn.UavarLeIshurVaad:
                    case enColumn.UsharAlVaad:
                        CurrentCell.Attributes.Add("onclick", "OpenDialogApprovalMonthlyQuota(" +
                            (int)_FormType + "," + row.RowIndex + ",'" +
                            row.Cells[(int)enColumn.Mispar_Ishi].Text + "','" + row.Cells[(int)enColumn.Mevukash].Text +
                            "','" + row.Cells[(int)enColumn.Meushar_Agafit].Text +
                            "','" + row.Cells[(int)enColumn.UavarLeIshurVaad].Text +
                            "','" + row.Cells[(int)enColumn.UsharAlVaad].Text +
                            "','" + row.Cells[(int)enColumn.Agaf].Text.Replace("&nbsp;", "") +
                            "','" + row.Cells[(int)enColumn.TaarichMMYYYY].Text +
                            "');");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }

    private void SetUnvisibleColumns(GridViewRow CurrentRow)
    {
        CurrentRow.Cells[(int)enColumn.Kod_Status_Ishur].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Bakasha_ID].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Taarich].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Original_Status_Ishur].Style.Add("display", "none");
        if (_FormType == clGeneral.enMonthlyQuotaForm.Agafit)
        {
            CurrentRow.Cells[(int)enColumn.TaarichMMYYYY].Style.Add("display", "none");
            CurrentRow.Cells[(int)enColumn.Maamad].Style.Add("display", "none");
            CurrentRow.Cells[(int)enColumn.Agaf].Style.Add("display", "none");
        }
        CurrentRow.Cells[(int)enColumn.Mispar_Sidur].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Shat_Hatchala].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Shat_Yetzia].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Mispar_Knisa].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Rama].Style.Add("display", "none");
        CurrentRow.Cells[(int)enColumn.Kod_Ishur].Style.Add("display", "none");
    }

    protected void btnSearchWorker_Click(object sender, EventArgs e)
    {
        DataView Dv = new DataView();
        SetStyleOfGridView();
        Dv = (DataView)Session["DvIshurim"];
        grdIshurim.PageIndex = GetPageOfMisparIshi(Dv, txtId.Text);
        grdIshurim.DataSource = Dv;
        grdIshurim.DataBind();
        foreach (GridViewRow gr in grdIshurim.Rows)
        {
            if (gr.Cells[(int)enColumn.Mispar_Ishi].Text == txtId.Text)
                gr.CssClass = "SelectedGridRow";
        }
    }

    private int GetPageOfMisparIshi(DataView Dv, string MisparIshi)
    {
        int iPos =0 , iPageIndex;
        DataTable Dt = Dv.Table;
        foreach (DataRow Dr in Dt.Rows)
        {
            iPos += 1;
            if (Dr[enColumn.Mispar_Ishi.ToString()].ToString() == MisparIshi)
                break;
        }
        iPageIndex = iPos / grdIshurim.PageSize;
        if ((iPos % grdIshurim.PageSize) == 0)
            iPageIndex = iPageIndex - 1;
        return iPageIndex;
    }

    private void SetStyleOfGridView()
    {
        for (int i = 0; i < grdIshurim.Rows.Count; i = i + 2)
        {
            grdIshurim.Rows[i].CssClass = "GridRow";
            if (i < grdIshurim.Rows.Count - 1)
                grdIshurim.Rows[i + 1].CssClass = "GridAltRow";
        }
    }

    protected void BtnUpdateRow_Click(object sender, EventArgs e)
    {
        bool Result = false;
        int ConfirmedValueUnit, ValueToConfirm;
        try
        {

            if (_FormType == clGeneral.enMonthlyQuotaForm.Agafit)
            {
                ConfirmedValueUnit = clGeneral.GetIntegerValue(TxtConfirmedValueUnitReturned.Text.ToString());
                ValueToConfirm = clGeneral.GetIntegerValue(TxtValueUnitToConfirmReturned.Text.ToString());
            }
            else
            {
                ConfirmedValueUnit = clGeneral.GetIntegerValue(TxtConfirmedValueVaadReturned.Text.ToString());
                ValueToConfirm = 0;
            }
            Result = UpdateRecordInIshurim(ConfirmedValueUnit, ValueToConfirm);
            SetMontlyAndSharedQuota();
            GetIshurim();
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private bool UpdateRecordInIshurim(int ConfirmedValue, int ValueToConfirm)
    {
        bool Result = false;
        int selectedRow = Int32.Parse(TxtSelectedRow.Text.ToString());
        int MainFactor = 0, SecondaryFactor = 0;
        try
        {
            DataView Dv = (DataView)Session["DvIshurim"];
            DataRow Dr = ((DataTable)(Dv.Table)).Rows[selectedRow];
            clOvdim _Ovdim = clOvdim.GetInstance();
            int Mispar_Ishi = clGeneral.GetIntegerValue(Dr[enColumn.Mispar_Ishi.ToString()].ToString());
            int Kod_Ishur = clGeneral.GetIntegerValue(Dr[enColumn.Kod_Ishur.ToString()].ToString());
            DateTime Taarich = (DateTime)Dr[enColumn.Taarich.ToString()];
            int Mispar_Sidur = clGeneral.GetIntegerValue(Dr[enColumn.Mispar_Sidur.ToString()].ToString());
            DateTime Shat_Hatchala = (DateTime)Dr[enColumn.Shat_Hatchala.ToString()];
            DateTime Shat_Yetzia = (DateTime)Dr[enColumn.Shat_Yetzia.ToString()];
            int Mispar_Knisa = clGeneral.GetIntegerValue(Dr[enColumn.Mispar_Knisa.ToString()].ToString());
            int Rama = clGeneral.GetIntegerValue(Dr[enColumn.Rama.ToString()].ToString());
            int Bakasha_ID = clGeneral.GetIntegerValue(Dr[enColumn.Bakasha_ID.ToString()].ToString());
            string Siba = Dr[enColumn.Siba.ToString()].ToString();
            WorkCard Wc = SetWorkCard(Mispar_Knisa, Mispar_Sidur, Shat_Hatchala, Shat_Yetzia, Taarich);
            GetFactorFromHr(Wc,Mispar_Ishi, Kod_Ishur, ref MainFactor, ref  SecondaryFactor);


            Result = _Ovdim.UpdateRecordInIshurim(Bakasha_ID, 1, Mispar_Ishi, Kod_Ishur, Taarich, Mispar_Sidur, Shat_Hatchala,
                                        Shat_Yetzia, Mispar_Knisa, Rama, ConfirmedValue, ValueToConfirm, Siba, MainFactor, SecondaryFactor);
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message, true);
        }
        return Result;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool Result = false;
        RadioButton rdoApproved, rdoNotApproved;
        int Bakasha_ID, Kod_Status_Ishur, Previous_Kod_Status_Ishur, Mispar_Ishi, Kod_Ishur, Mispar_Sidur, Mispar_Knisa, Rama, ConfirmedValueUnit, ValueToConfirm;
        DateTime Taarich, Shat_Hatchala, Shat_Yetzia;
        string Siba = string.Empty;
        clOvdim _Ovdim = clOvdim.GetInstance();


        foreach (GridViewRow Gvr in grdIshurim.Rows)
        {
            rdoApproved = (RadioButton)Gvr.FindControl("rdoApproved");
            rdoNotApproved = (RadioButton)Gvr.FindControl("rdoNotApproved");
            Kod_Status_Ishur = (rdoApproved.Checked) ? 1 : (rdoNotApproved.Checked) ? 2 : 0;
            Previous_Kod_Status_Ishur = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Original_Status_Ishur].Text.ToString());
            Bakasha_ID = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Bakasha_ID].Text.ToString());
            Mispar_Ishi = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Mispar_Ishi].Text.ToString());
            Kod_Ishur = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Kod_Ishur].Text.ToString());
            Taarich = DateTime.Parse(Gvr.Cells[(int)enColumn.Taarich].Text.ToString());
            Mispar_Sidur = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Mispar_Sidur].Text.ToString());
            Shat_Hatchala = DateTime.Parse(Gvr.Cells[(int)enColumn.Shat_Hatchala].Text.ToString());
            Shat_Yetzia = DateTime.Parse(Gvr.Cells[(int)enColumn.Shat_Yetzia].Text.ToString());
            Mispar_Knisa = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Mispar_Knisa].Text.ToString());
            Rama = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Rama].Text.ToString());
            Siba = Gvr.Cells[(int)enColumn.Siba].Text.ToString();
            if (_FormType == clGeneral.enMonthlyQuotaForm.Agafit)
            {
                ConfirmedValueUnit = clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.Mevukash].Text.ToString());
                ValueToConfirm = 0;
            }
            else
            {
                ConfirmedValueUnit = (Kod_Status_Ishur == 2) ? 0 : clGeneral.GetIntegerValue(Gvr.Cells[(int)enColumn.UavarLeIshurVaad].Text.ToString());
                ValueToConfirm = ConfirmedValueUnit;
            }

            if (((rdoApproved.Checked) || (rdoNotApproved.Checked)) & (Previous_Kod_Status_Ishur != Kod_Status_Ishur))
                Result = _Ovdim.UpdateRecordInIshurim(Bakasha_ID, Kod_Status_Ishur, Mispar_Ishi, Kod_Ishur, Taarich, Mispar_Sidur, Shat_Hatchala,
                                            Shat_Yetzia, Mispar_Knisa, Rama, ConfirmedValueUnit, ValueToConfirm, Siba, 0, 0);
        }
        SetMontlyAndSharedQuota();
        GetIshurim();
    }
    private WorkCard SetWorkCard(int EntryNumber, int SidurNumber, DateTime StartTime, DateTime EndTime, DateTime Taarich)
    {
        WorkCard Wc = new WorkCard();
        Wc.SidurNumber = SidurNumber;
        Wc.SidurStart = StartTime;
        Wc.ActivityStart = EndTime;
        Wc.WorkDate = Taarich;
        Wc.ActivityNumber = EntryNumber;
        return Wc;
    }

    private void GetFactorFromHr(WorkCard Wc,int Mispar_Ishi, int KodIshur, ref int MainFactor, ref int SecondaryFactor)
    {
        ApprovalRequest AppRequest;
        AppRequest = ApprovalRequest.CreateApprovalRequest(Mispar_Ishi.ToString(), KodIshur, Wc, RequestValues.Empty, 
            true, null);
        MainFactor = clGeneral.GetIntegerValue(AppRequest.MainFactor.EmployeeNumber.ToString());
        SecondaryFactor = clGeneral.GetIntegerValue(AppRequest.SecondaryFactor.EmployeeNumber.ToString());
    }

    protected void grdIshurim_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataView Dv = (DataView)Session["DvIshurim"];
        grdIshurim.PageIndex = e.NewPageIndex;
        grdIshurim.DataSource = Dv;
        grdIshurim.DataBind();
    }

}

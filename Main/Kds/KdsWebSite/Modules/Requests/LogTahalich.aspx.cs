using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.UI;
using System.Data;
using KdsLibrary.UI.SystemManager;
public partial class Modules_Requests_LogTahalich :KdsPage
{
    private const int COL_TAARICH = 0;
    private const int COL_TEUR_TAHALICH = 1;
    private const int COL_TEUR_PEILUT_BE_TAHALICH =2;
    private const int COL_TEUR_STATUS_BAKASHA = 3;
    private const int COL_TEUR_TAKALA = 4;
    private const int COL_TEUR_TECH = 5;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grdLogTahalich.RowCreated += new GridViewRowEventHandler(grdLogTahalich_RowCreated);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageHeader = "לוג תהליך";
            LoadMessages((DataList)Master.FindControl("lstMessages"));
            divNetunim.Visible = false;
            LoadCombo();
            clnFromDate.Text = DateTime.Now.ToShortDateString();
            clnToDate.Text = DateTime.Now.ToShortDateString(); 
        }
    }

    private void LoadCombo()
    {
        clUtils oUtils = new clUtils();
        try
        {
            ddlProcess.DataSource = oUtils.GetCombo(clGeneral.cProGetTahalichKlita, "הכל");
            ddlProcess.DataTextField = "DESCRIPTION";
            ddlProcess.DataValueField = "CODE";
            ddlProcess.DataBind();

            ddlStatus.DataSource = oUtils.GetCombo(clGeneral.cProGetStatusRequest, "הכל");
            ddlStatus.DataTextField = "DESCRIPTION";
            ddlStatus.DataValueField = "CODE";
            ddlStatus.DataBind();

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                divNetunim.Visible = true;
                ViewState["SortDirection"] = SortDirection.Descending;
                ViewState["SortExp"] = "TAARICH";
                GetLogTahalich();
                ((DataView)Session["LogTahalich"]).Sort = "TAARICH DESC";
                grdLogTahalich.DataSource = (DataView)Session["LogTahalich"];
                grdLogTahalich.DataBind();
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }


    private void GetLogTahalich()
    {

        clRequest objRequest = new clRequest();
        DataView dvTahalich;
        DataTable dtLogTahalich;
        DateTime dFromDate = DateTime.MinValue;
        DateTime dToDate = DateTime.MaxValue;

        if (clnFromDate.Text.Length > 0)
        { dFromDate = DateTime.Parse(clnFromDate.Text); }
        if (clnToDate.Text.Length > 0)
        { dToDate = DateTime.Parse(clnToDate.Text); }

        dtLogTahalich = objRequest.GetLogTahalich(dFromDate, dToDate, int.Parse(ddlProcess.SelectedValue), int.Parse(ddlStatus.SelectedValue));

        dvTahalich = new DataView(dtLogTahalich);

        Session["LogTahalich"] = dvTahalich;


    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdLogTahalich.Columns)
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



    protected void grdLogTahalich_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;
        try
        {

            if ((string.Empty != (string)ViewState["SortExp"]) && (string.Compare(e.SortExpression, (string)ViewState["SortExp"], true) == 0))
            {
                if ((SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
                {
                    sDirection = "DESC";
                    ViewState["SortDirection"] = SortDirection.Descending;
                }
                else
                {
                    sDirection = "ASC";
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }
            }
            else
            {
                sDirection = "ASC";
                ViewState["SortDirection"] = SortDirection.Ascending;
            }
            ViewState["SortExp"] = e.SortExpression;

            if (Session["LogTahalich"] == null)
            {
                GetLogTahalich();
            }
            ((DataView)Session["LogTahalich"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
            grdLogTahalich.DataSource = (DataView)Session["LogTahalich"];
            grdLogTahalich.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdLogTahalich_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();

            iColSort = GetCurrentColSort();
            lbl.Text = " ";
            e.Row.Cells[iColSort].Controls.Add(lbl);
            if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
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
            for (i = 0; i <= e.Row.Cells.Count - 1; i++)
            {
                sortHeader = e.Row.Cells[i].Controls[0];
                ((LinkButton)(sortHeader)).Style.Add("color", "white");
                ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
        {
            e.Row.Cells[COL_TAARICH].Attributes.Add("dir", "ltr");
            //e.Row.Cells[COL_TEUR].Attributes.Add("dir", "ltr");
            //e.Row.Cells[COL_SHAT_HATCHALA_SIDUR].Attributes.Add("dir", "ltr");
            //e.Row.Cells[COL_SHAT_PEILUT].Attributes.Add("dir", "ltr");
            //e.Row.Cells[COL_TARICH_IDKUN].Attributes.Add("dir", "ltr");
            //if (e.Row.Cells[COL_SHAT_HATCHALA_SIDUR].Text == DateTime.MinValue.ToString("dd/MM/yyyy  HH:mm"))
            //{ e.Row.Cells[COL_SHAT_HATCHALA_SIDUR].Text = ""; }
        }

    }

    protected void grdLogTahalich_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLogTahalich.PageIndex = e.NewPageIndex;
        grdLogTahalich.DataSource = (DataView)Session["LogTahalich"];
        grdLogTahalich.DataBind();
    }

    //*******Pager Functions*********/
    void grdLogTahalich_RowCreated(object sender, GridViewRowEventArgs e)
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
                    ChangeGridPage(pagerArgs.NewPageIndex, grdLogTahalich,
                       (DataView)Session["LogTahalich"], "SortDirection",
                       "SortExp");
                };
            }
        }
    }
    private void ChangeGridPage(int pageIndex, GridView grid, DataView dataView,
                                string sortDirViewStateKey, string sortExprViewStateKey)
    {
        //   SetChangesOfGridInDataview(grid, ref dataView);
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
    /********************/
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;
using System.Data;
using KdsLibrary.BL;
using KdsLibrary.UI.SystemManager;
public partial class Modules_Requests_MaakavBakashot : KdsPage
{
    private const int COL_STATUS = 0;
    private const int COL_BAKASHA_ID = 1;
    private const int COL_ZMAN_HATCHALA = 4;
    private const int COL_ZMAN_SIYUM= 5;
    private const int COL_HAAVARA_LESACHAR = 7;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grdRequest.RowCreated += new GridViewRowEventHandler(grdRequest_RowCreated);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "מעקב בקשות";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
               
                MasterPage mp = (MasterPage)Page.Master;
                SetFixedHeaderGrid(pnlgrdRequest.ClientID, mp.HeadPage);
                divNetunim.Visible = false;
                LoadCombo();
                if (Request.QueryString["RequestId"] != null)
                {
                    txtRequestNum.Text = Request.QueryString["RequestId"];
                    divNetunim.Visible = true;
                    ViewState["SortDirection"] = SortDirection.Descending;
                    ViewState["SortExp"] = "ZMAN_HATCHALA";
                    GetRequests();
                    ((DataView)Session["Requests"]).Sort = "ZMAN_HATCHALA DESC";
                    grdRequest.DataSource = (DataView)Session["Requests"];
                    grdRequest.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    private void LoadCombo()
    {
        clUtils oUtils = new clUtils();
        try
        {
           ddlRequestType.DataSource= oUtils.GetCombo(clGeneral.cProGetTypeRequest, "הכל");
           ddlRequestType.DataTextField = "DESCRIPTION";
           ddlRequestType.DataValueField = "CODE";
            ddlRequestType.DataBind();

           ddlStatus.DataSource = oUtils.GetCombo(clGeneral.cProGetStatusRequest, "הכל");
           ddlStatus.DataTextField = "DESCRIPTION";
           ddlStatus.DataValueField = "CODE";
           ddlStatus.DataBind();

           ddlMonth.DataSource = oUtils.GetCombo(clGeneral.cProGetMonthsFromRequest, "הכל");
           ddlMonth.DataTextField = "DESCRIPTION";
           ddlMonth.DataValueField = "CODE";
           ddlMonth.DataBind();
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
                ViewState["SortExp"] = "ZMAN_HATCHALA";
                GetRequests();
                ((DataView)Session["Requests"]).Sort = "ZMAN_HATCHALA DESC";
                grdRequest.DataSource = (DataView)Session["Requests"];
                grdRequest.DataBind();
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }


    private void GetRequests()
    {
      
            clRequest objRequest = new clRequest();
            DataView dvRequest;
            DataTable dtRequest;
            int iStatus,iRequestType,iRequestNum;
            iStatus = int.Parse(ddlStatus.SelectedValue);
             iRequestType = int.Parse(ddlRequestType.SelectedValue);
            iRequestNum=-1;
            if (clGeneral.IsNumeric(txtRequestNum.Text))
            {
            iRequestNum=int.Parse(txtRequestNum.Text);
            }

            dtRequest = objRequest.GetMaakavBakashot(iRequestType, iStatus, iRequestNum, ddlMonth.SelectedValue);
            dvRequest = new DataView(dtRequest);

            Session["Requests"] = dvRequest;
                   
    
    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdRequest.Columns)
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



    protected void grdRequest_Sorting(object sender, GridViewSortEventArgs e)
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

                if (Session["Requests"] == null)
                {
                   GetRequests();
                }
                ((DataView)Session["Requests"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
                grdRequest.DataSource = (DataView)Session["Requests"];
                grdRequest.DataBind();
                
            }
     
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdRequest_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Cells[COL_ZMAN_HATCHALA].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_ZMAN_SIYUM].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_HAAVARA_LESACHAR].Attributes.Add("dir", "ltr");
            ((HyperLink)e.Row.Cells[COL_BAKASHA_ID].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenLogBakashot(", e.Row.RowIndex, ")"));
      
        }

    }

    //*******Pager Functions*********/
    void grdRequest_RowCreated(object sender, GridViewRowEventArgs e)
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
                    ChangeGridPage(pagerArgs.NewPageIndex, grdRequest,
                       (DataView)Session["Requests"], "SortDirection",
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

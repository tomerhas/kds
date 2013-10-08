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
using System.IO;
using KdsLibrary.Security;
using System.Configuration;
using KdsLibrary.UI.SystemManager;
public partial class Modules_Reports_ShowHeavyReportsList : KdsPage
{

    private const int COL_BAKASHA_ID = 0;
    private const int COL_ZMAN_HATCHALA = 2;
    private const int COL_ZMAN_SIYUM= 3;
    private const int COL_SHEM_TIKIYA = 4;
    private const int COL_EXTENSION_TYPE = 5;
    private const int COL_KOVEZ =6;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grdReports.RowCreated += new GridViewRowEventHandler(grdReports_RowCreated);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                PageHeader = "רשימת דוחו''ת מוכנים";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
               
                MasterPage mp = (MasterPage)Page.Master;
                SetFixedHeaderGrid("ctl00_KdsContent_pnlgrdReports", mp.HeadPage);
                ShowReports();
              
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void ShowReports()
    {
        clReport objReport = new clReport();
        DataView dvReports;
        DataTable dtReports;
        try
        {
            dtReports = objReport.GetPrepareReports(int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber),"2,4");
            dvReports = new DataView(dtReports);
            grdReports.DataSource = dvReports;
            grdReports.DataBind();
            Session["Reports"] = dvReports;
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }
    protected void grdReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string url = ConfigurationManager.AppSettings["HeavyReportsUrl"].ToString();
        if (e.Row.RowType != DataControlRowType.EmptyDataRow && e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[COL_SHEM_TIKIYA].Style.Add("display", "none");
            e.Row.Cells[COL_EXTENSION_TYPE].Style.Add("display", "none");
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
        {
            e.Row.Cells[COL_ZMAN_HATCHALA].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_ZMAN_SIYUM].Attributes.Add("dir", "ltr");
            url = url + e.Row.Cells[COL_BAKASHA_ID].Text + "."; 
            if ((KdsLibrary.Utils.Reports.eFormat)int.Parse(e.Row.Cells[COL_EXTENSION_TYPE].Text) == KdsLibrary.Utils.Reports.eFormat.EXCEL)
            {
                ((HyperLink)e.Row.Cells[COL_KOVEZ].Controls[1]).ImageUrl = "../../Images/icon-excel.jpg";
                url = url + "xls";
            }
            else if ((KdsLibrary.Utils.Reports.eFormat)int.Parse(e.Row.Cells[COL_EXTENSION_TYPE].Text) == KdsLibrary.Utils.Reports.eFormat.EXCELOPENXML)
                {
                    ((HyperLink)e.Row.Cells[COL_KOVEZ].Controls[1]).ImageUrl = "../../Images/icon-excel.jpg";
                    url = url + "xlsx";
                }
                else 
                    {
                        ((HyperLink)e.Row.Cells[COL_KOVEZ].Controls[1]).ImageUrl = "../../Images/icon-pdf.jpg";
                        url = url + "pdf";
                    }
            string sScript = "window.showModalDialog('" + url + "','','dialogwidth:1200px;dialogheight:800px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
            sScript = "window.open('"+url +"')";
            ((HyperLink)e.Row.Cells[COL_KOVEZ].Controls[1]).Attributes.Add("OnClick", sScript);  
        }
    }

    //*******Pager Functions*********/
    void grdReports_RowCreated(object sender, GridViewRowEventArgs e)
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
                    ChangeGridPage(pagerArgs.NewPageIndex, grdReports,
                       (DataView)Session["Reports"], "SortDirection",
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


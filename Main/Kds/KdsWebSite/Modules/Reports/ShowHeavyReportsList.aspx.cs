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
public partial class Modules_Reports_ShowHeavyReportsList : KdsPage
{

    private const int COL_BAKASHA_ID = 0;
    private const int COL_ZMAN_HATCHALA = 2;
    private const int COL_ZMAN_SIYUM= 3;
    private const int COL_SHEM_TIKIYA = 4;
    private const int COL_EXTENSION_TYPE = 5;
    private const int COL_KOVEZ =6;          

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                PageHeader = "רשימת דוחו''ת מוכנים";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
               
                MasterPage mp = (MasterPage)Page.Master;
                SetFixedHeaderGrid("ctl00_KdsContent_pnlgrdRequest", mp.HeadPage);
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
            grdRequest.DataSource = dvReports;
            grdRequest.DataBind();
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }
    protected void grdRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string url = ConfigurationManager.AppSettings["HeavyReportsUrl"].ToString();
        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
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
}


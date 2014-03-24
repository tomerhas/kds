using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.BL;
using System.Data;
using KdsLibrary.Security;
using KdsLibrary.Utils.Reports;

public partial class Modules_Reports_ReportsList : KdsPage
{
    private const int SHEM_DOCH_BAKOD = 0;
    private const int TEUR_DOCH = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string Strprofil="";
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "רשימת דו''חות";
                LoadMessages((DataList)Master.FindControl("lstMessages"));

                MasterPage mp = (MasterPage)Page.Master;
                SetFixedHeaderGrid(pnlgrdReports.ClientID, mp.HeadPage);
                foreach (UserProfile Profile in LoginUser.UserProfiles) {
                    Strprofil = Strprofil + "," + Profile.Role.ToString();
                }
                if (Strprofil != "") 
                    Strprofil = Strprofil.Substring(1);

                GetReports(Strprofil);      
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    private void GetReports(string profil)
    {

        clReport objReports = clReport.GetInstance();
        DataView dvReport;
        DataTable dtReport;
        KdsDynamicReport _ConstDynamicReportParam;
      //  _ConstDynamicReportParam.ReportList = new List<KdsReport>();
        KdsReport _ConstReportParam;
        _ConstDynamicReportParam = new KdsDynamicReport();
        _ConstDynamicReportParam.ReportList = new List<KdsReport>();
        dtReport = objReports.GetListReports(profil);
        foreach (DataRow row in dtReport.Rows)
        {
            _ConstReportParam= new KdsReport();

            _ConstReportParam.PageHeader = row["TEUR_DOCH_2"].ToString();
            _ConstReportParam.RSVersion = row["RS_VERSION"].ToString();
            _ConstReportParam.URL_CONFIG_KEY = row["URL_CONFIG_KEY"].ToString();
            _ConstReportParam.SERVICE_URL_CONFIG_KEY = row["SERVICE_URL_CONFIG_KEY"].ToString();
            _ConstReportParam.EXTENSION = row["EXTENSION"].ToString();
            _ConstReportParam.RdlName = row["SHEM_DOCH_BAKOD"].ToString();

            _ConstDynamicReportParam.ReportList.Add(_ConstReportParam);
        }

        Session["ConstDynamicReportParam"] = _ConstDynamicReportParam;
        clReport _Report = new clReport();
        dvReport = new DataView(dtReport);

        grdReports.DataSource = dvReport;
        grdReports.DataBind();


    }


    protected void grdReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            e.Row.Cells[SHEM_DOCH_BAKOD].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
        {
             ((HyperLink)e.Row.Cells[TEUR_DOCH].Controls[0]).NavigateUrl = "ReportFilters.aspx?RdlName=" + e.Row.Cells[SHEM_DOCH_BAKOD].Text;
             e.Row.Cells[SHEM_DOCH_BAKOD].Style.Add("display", "none");
        }
    }
}

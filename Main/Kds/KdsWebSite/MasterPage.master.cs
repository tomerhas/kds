using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using KdsLibrary.UI;
using KdsLibrary.BL;
using KdsLibrary;


//using System.Linq;
//using System.Xml.Linq;
using KdsLibrary.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public int UserId;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        MenuMain.DataBound += new EventHandler(MenuMain_DataBound);
    }

    void MenuMain_DataBound(object sender, EventArgs e)
    {
        KdsPage kdsPage = this.Page as KdsPage;
        if (kdsPage != null)
        {
            kdsPage.ProvideMenuForRole();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ImageExit.Attributes.Add("onClick","if (window.confirm(' ?האם אתה בטוח שאתה רוצה לצאת מהמערכת ')){this.focus();self.opener = this;self.close();}");
        ImageExit.Style.Add("cursor", "hand");
        KdsPage kdsPage = this.Page as KdsPage;
        if (clGeneral.IsNumeric(LoginUser.GetLoginUser().UserInfo.EmployeeNumber))
            UserId = int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber);
        else UserId = 0;
        if (kdsPage != null)
        {
            if (ConfigurationManager.AppSettings["ImpersonateUser"] == "true")
                kdsPage.SetImpersonatePanel();
        }
        if (kdsPage != null)
            ShowMessageReports(kdsPage.PureUrlRoot);
    }
    private void ShowMessageReports(string PureUrlRoot)
    {
        clReport objReport = new clReport();
        DataTable dtReports;
        string sScript;
        dtReports = objReport.GetPrepareReports(clGeneral.GetIntegerValue(LoginUser.GetLoginUser().UserInfo.EmployeeNumber), "4");
        if (dtReports.Rows.Count > 0)
        {
            objReport.updatePrepareReports(int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber));
            sScript = "ShowMessageReport('" + PureUrlRoot + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Report", sScript, true);
        }
    }

    public ImageButton ImageExcelClick 
    { 
        get { return ImageExcel; }
    }

    public ImageButton ImagePrintClick
    {
        get { return ImagePrint; }
    }

    public  HtmlHead HeadPage
    {
        get { return Head1; }
    }

    public bool DisabledMenuAndToolBar
    {
        set
        {
            imgTopBanner.Visible = !value;
            MenuMain.Visible = !value;
            ImgHelp.Visible = !value;
            ImageHome.Visible = !value;
            ImageExit.Visible = !value;
            ImageExcel.Visible = !value;
        }
    }

    protected void ImageHome_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main.aspx");
    }
}

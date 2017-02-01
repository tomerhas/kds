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
using KDSCommon.Interfaces.Managers;
using KDSCommon.DataModels.Mails;
using Microsoft.Practices.ServiceLocation;

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

        string ServerName = Environment.MachineName;
        string VersionNumber = ConfigurationManager.AppSettings["VersionNumber"].ToString();
        lblGirsa.Text = " מספר גרסה: " + VersionNumber;
        lblServerName.Text = " שם שרת: " + ServerName;

        if (kdsPage != null)
        {
            KdsSecurityLevel iSecurity = kdsPage.PageModule.SecurityLevel;
            if (iSecurity == KdsSecurityLevel.ViewAll)
                UserId=0;
            else if (clGeneral.IsNumeric(LoginUser.GetLoginUser().UserInfo.EmployeeNumber))
                UserId =int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber);
            else UserId = 0;

            //if (ConfigurationManager.AppSettings["ImpersonateUser"] == "true")
            //    kdsPage.SetImpersonatePanel();
        }
        if (kdsPage != null)
            ShowMessageReports(kdsPage.PureUrlRoot);
    }
    private void ShowMessageReports(string PureUrlRoot)
    {
        clReport objReport = new clReport();
        DataTable dtReports;
        string sScript;
        int num;
        dtReports = objReport.GetPrepareReports(clGeneral.GetIntegerValue(LoginUser.GetLoginUser().UserInfo.EmployeeNumber), "4");
        if (dtReports.Rows.Count > 0)
        {
            num= int.Parse(dtReports.Rows[0]["MailDeviationRows"].ToString());
            if (num > 0)
            {
                IMailManager mailManager = ServiceLocator.Current.GetInstance<IMailManager>();
                string RecipientsList = dtReports.Rows[0]["email"].ToString();
                if (RecipientsList == "")
                    RecipientsList = LoginUser.GetLoginUser().UserInfo.Username.Split('\\')[1] + "@Egged.co.il";
                RecipientsList+= ";"+ ConfigurationManager.AppSettings["RecipientsMailList"].ToString(); 
                string subject = "חריגת שורות בדו''ח";
                string body = "משתמש יקר, הדוח שביקשת להפיק חרג מכמות רשומות מקסימלית.";
                body += "\n" + "מספר בקשה:  " + dtReports.Rows[0]["Bakasha_ID"].ToString();
                body += "\n" +"תאור בקשה:  " + dtReports.Rows[0]["Teur"].ToString();
                mailManager.SendMessage(new MailMessageWrapper(RecipientsList) { Subject = subject, Body =body });
            }

            objReport.updatePrepareReports(int.Parse(LoginUser.GetLoginUser().UserInfo.EmployeeNumber));
            sScript = "ShowMessageReport('" + PureUrlRoot + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Report", sScript, true);
        }
    }

    //protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    //{
    //    ScriptManagerKds.AsyncPostBackErrorMessage = e.Exception.ToString();
    //}

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
            ImagePrint.Visible = !value;
            ImageHome.Visible = !value;
            ImageExit.Visible = !value;
            ImageExcel.Visible = !value;
        }
    }

    protected void ImageHome_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main.aspx", false);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary.Security;
using KdsLibrary.Utils.Reports;
using KdsLibrary;
using System.Configuration;
using System.IO;


public partial class Modules_Reports_PresenceReport : KdsPage
{
    private string[] arrParams;
    private int iMisparIshiKiosk;

    protected override void CreateUser()
    {
        if (!Page.IsPostBack)
            Session["arrParams"] = null;
        if (Request.QueryString["Key"] != null && Session["arrParams"] == null)
        {
            DriverStation.WSkds wsDriverStation = new DriverStation.WSkds();

            try
            {
                wsDriverStation.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                arrParams = wsDriverStation.getZihuiUser(Request.QueryString["Key"].ToString());
                Session["arrParams"] = arrParams;
                SetUserKiosk(arrParams);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wsDriverStation.Dispose();
            }

        }
        else if (Session["arrParams"] != null && Request.QueryString["Key"] != null)
        {
            arrParams = (string[])Session["arrParams"];
            SetUserKiosk(arrParams);
        }
        else { base.CreateUser(); }
    }

    private void SetUserKiosk(string[] arrParamsKiosk)
    {
        iMisparIshiKiosk = int.Parse(arrParamsKiosk[0].ToString());

        LoginUser = LoginUser.GetLimitedUser(iMisparIshiKiosk.ToString());
        LoginUser.InjectEmployeeNumber(iMisparIshiKiosk.ToString());
        MasterPage mp = (MasterPage)Page.Master;
        mp.DisabledMenuAndToolBar = true;
        DisplayDivMessages = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageHeader = "דוח נוכחות";
        DateHeader = DateTime.Today.ToShortDateString();

        MasterPage mp = (MasterPage)Page.Master;
        mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);

        if (!Page.IsPostBack)
        {
             CreatReport();
        }
    }

    private void CreatReport()
    {
        byte[] s;
        ReportModule Report = new ReportModule();// ReportModule.GetInstance();
        DateTime dFromDate, dToDate;
        try
        {
            dFromDate = new DateTime(int.Parse(arrParams[3].ToString().Substring(0, 4)), int.Parse(arrParams[3].ToString().Substring(4, 2)), 1);
            dToDate = dFromDate.AddMonths(1).AddDays(-1);

            Report.AddParameter("P_STARTDATE", dFromDate.ToShortDateString());
            Report.AddParameter("P_ENDDATE", dToDate.ToShortDateString());
            Report.AddParameter("P_MISPAR_ISHI", LoginUser.UserInfo.EmployeeNumber);

            s = Report.CreateReport("/KdsReports/Presence", eFormat.PDF, true);
            Session["BinaryResult"] = s;
            Session["Report"] = s;
            Session["TypeReport"] = "PDF";
            Session["FileName"] = "Presence";

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    void ImagePrintClick_Click(object sender, ImageClickEventArgs e)
    {
        string sScript;
        byte[] s;
        string sIp = "";
        string sPathFilePrint = ConfigurationManager.AppSettings["PathFileReportsTemp"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
        
        if (LoginUser.IsLimitedUser && arrParams[2].ToString()=="1")
        {
            string sFileName, sPathFile;
            FileStream fs;
            sIp = "";//arrParams[1];
            s = (byte[])Session["Report"];
            sFileName = "PresenceReport.pdf";
            sPathFile = ConfigurationManager.AppSettings["PathFileReports"] + LoginUser.UserInfo.EmployeeNumber + @"\";
            if (!Directory.Exists(sPathFile))
                Directory.CreateDirectory(sPathFile);
            fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
            fs.Write(s, 0, s.Length);
            fs.Flush();
            fs.Close();
          
            sScript = "PrintDoc('" + sIp + "' ,'" + sPathFilePrint + sFileName + "')";

            MasterPage mp = (MasterPage)Page.Master;

            ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintPdf", sScript, true);

        }
    }

}

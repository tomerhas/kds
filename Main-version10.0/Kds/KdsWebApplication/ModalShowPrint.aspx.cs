using KdsLibrary.Security;
using KdsLibrary.UI;
using KdsLibrary.Utils.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ModalShowPrint : KdsPage
{
    private string[] arrParams;
    private int iMisparIshiKiosk;
    protected override void CreateUser()
    {
        if (((Session["arrParams"] != null) && (Request.QueryString["Page"] != null)) || ((((string)Session["hidSource"]) == "1") && (Session["arrParams"] != null)))
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
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] s;
        string sFileName = "", sPathFile, src;
        FileStream fs;
        string format=".pdf";
        try
        {
            if (!Page.IsPostBack)
            {
                switch ((eFormat)Enum.Parse(typeof(eFormat), (string)Session["TypeReport"]))
                {
                    case eFormat.PDF:
                        format =".pdf";
                        break;
                    case eFormat.EXCEL:
                        sFileName = ".xlsx";
                        break;
                }

                s = (byte[])Session["BinaryResult"];
                src = Session["FileName"].ToString();

                switch (src)
                {
                    case "Presence":
                        sFileName = "Shaonim" + format;
                        break;
                    case "PrintWorkCard":
                        sFileName = "WorkCard" + format;
                        break;
                    default:
                        sFileName = "TempFile" + format;

                        //case "RikuzAvodaChodshi":
                        //    sFileName = "RikuzAvodaChodshi.pdf";
                        break; 
                }


                sPathFile = ConfigurationManager.AppSettings["PathFileReportsSave"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
                if (!Directory.Exists(sPathFile))
                    Directory.CreateDirectory(sPathFile);
                fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
                idpath.Value = ConfigurationManager.AppSettings["PathFileReportsOut"] + LoginUser.UserInfo.EmployeeNumber + @"\\" + sFileName;
                fs.Write(s, 0, s.Length);
                fs.Flush();
                fs.Close();
      
            }
        }
        catch (Exception ex)
        { throw ex; }
    }

}

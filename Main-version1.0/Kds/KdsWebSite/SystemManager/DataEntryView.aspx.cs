using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class DataEntryView : KdsLibrary.UI.SystemManager.KdsSysManPageBase//System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ((ScriptManager)Master.FindControl("ScriptManagerKds")).EnableScriptGlobalization = true;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage mp = (MasterPage)Page.Master;
        mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);

    }

    void ImagePrintClick_Click(object sender, ImageClickEventArgs e)
    {
        //string sScript;
        //sScript = "window.showModalDialog('../Modules/Ovdim/PrintTable.aspx','','dialogwidth:800px;dialogheight:750px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;');";
        //MasterPage mp = (MasterPage)Page.Master;

        //ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintPdf", sScript, true);
        btnPrint_Click(sender, e);
    }
    protected void btnRefreshPrint_click(object sender, EventArgs e)
    {
        RefreshAftePrint();
    }
   
    
}

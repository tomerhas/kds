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
        
    }
    
    
    
   
    
}

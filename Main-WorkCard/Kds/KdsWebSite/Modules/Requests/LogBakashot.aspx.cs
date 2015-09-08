using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;

public partial class Modules_Requests_LogBakashot : KdsPage
{
  

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "לוג בקשות";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
               
                MasterPage mp = (MasterPage)Page.Master;
                //SetFixedHeaderGrid(ucLogBakashot.GetPannelGridId, mp.HeadPage);
               
             }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.UI;

public partial class Modules_Requests_LogBakashotModal :KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //SetFixedHeaderGrid(ucLogBakashot.GetPannelGridId, head1);
        }
    }
}

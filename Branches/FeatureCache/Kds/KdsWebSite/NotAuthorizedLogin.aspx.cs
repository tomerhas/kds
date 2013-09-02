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

public partial class NotAuthorizedLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Add some logging code here if required
        //Server.Transfer(String.Format("{0}/ErrorPage.aspx",
        //        HttpContext.Current.Request.ApplicationPath), false);
        Server.Transfer(String.Format("{0}/ErrorPage.aspx",
             "~"), false);
    }
}

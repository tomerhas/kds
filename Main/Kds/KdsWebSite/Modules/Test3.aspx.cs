using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Test3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] sArr = new string[4];
        sArr[0] = "75290";
        sArr[1] = "133.1.40.14";
        sArr[2] = "1";
        sArr[3] = "201004";
        Session["arrParams"] = sArr;
    }
}

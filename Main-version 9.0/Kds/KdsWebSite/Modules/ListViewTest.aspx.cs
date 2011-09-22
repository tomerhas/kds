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
using KdsLibrary.BL;

public partial class Modules_ListViewTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lstSidurimTest.DataSource = GetSidurimAndPeilut(74480, new DateTime(2009, 5, 26));
        //lstSidurim.DataSource = GetSidurimAndPeilut(74480, new DateTime(2009, 5, 26));
       
    }
    protected DataTable GetSidurimAndPeilut(int iMisparIshi, DateTime dCardDate)
    {
        //Return Sidurim and Peilut for Employee
        DataTable dt;
        clOvdim oOvdim = new clOvdim();
        try
        {
            dt = oOvdim.GetSidurimAndPeilut(iMisparIshi, dCardDate);
            return dt; 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

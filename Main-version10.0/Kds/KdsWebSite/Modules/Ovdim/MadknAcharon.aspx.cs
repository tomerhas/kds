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
using KdsBatch;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;

public partial class Modules_Ovdim_MadknAcharon : KdsLibrary.UI.KdsPage
{
  private int iMisparIshi;
  private DateTime dTarich;
  private DataTable dtUpdtes;
    protected void Page_Load(object sender, EventArgs e)
    {


    //iMisparIshi = 77690;
    //dTarich = new DateTime(2009, 06, 11);

        string sDate;

        iMisparIshi = Int32.Parse(Request.QueryString["id".ToLower()]);
        sDate = Request.QueryString["date".ToLower()].ToString();
        dTarich = new DateTime(int.Parse(sDate.Substring(6, 4)), int.Parse(sDate.Substring(3, 2)), int.Parse(sDate.Substring(0, 2)));

        clOvdim oOvdim = new clOvdim();
        try
        {
            dtUpdtes = oOvdim.GetLastUpdator(iMisparIshi, dTarich);
            grdUpdators.DataSource = dtUpdtes;
            grdUpdators.DataBind();        
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

}



      








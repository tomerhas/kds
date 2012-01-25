using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KdsLibrary.BL;
public partial class Modules_test5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt;
        DataView dv;
        clOvdim oOvdim = new clOvdim();

       
        dt = oOvdim.GetOvedCardsInTipul(78856);

        dv = new DataView(dt);
        grdEmployee.DataSource = dv;
        grdEmployee.DataBind();   
    }
    protected void grdEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
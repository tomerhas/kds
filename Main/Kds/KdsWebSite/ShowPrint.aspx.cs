using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
           Response.Clear();

           Page.EnableViewState = false;

           Response.ClearHeaders();  
           Response.AppendHeader("content-length", ((byte[])Session["BinaryResult"]).Length.ToString());
           if (Session["TypeReport"].ToString() == "PDF")
           {
               Response.ContentType = "application/pdf";
               Response.AddHeader("Content-Disposition", "inline:Filename=" + Session["FileName"].ToString() + ".pdf");
           }
           else
           {
               Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("Content-Disposition", "attachment; Filename=" + Session["FileName"].ToString() + ".xls");

           }

         Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.BinaryWrite((byte[])Session["BinaryResult"]);
           
            Response.Flush();

            Session["BinaryResult"] = null;
            Session["TypeReport"] = null;
            Session["FileName"] = null;
            Response.End();
           
        }
        catch (Exception ex)
        { throw ex; }
    }
}

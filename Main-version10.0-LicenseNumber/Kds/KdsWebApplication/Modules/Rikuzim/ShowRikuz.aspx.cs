using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.BL;
using System.Configuration;
using System.IO;
using KdsLibrary.Utils.Reports;

public partial class Modules_Rikuzim_ShowRikuz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Request.QueryString["EmpID"] != null)
         {
             ShowRikuz(int.Parse(Request.QueryString["EmpID"]), int.Parse(Request.QueryString["bakashaID"]), DateTime.Parse(Request.QueryString["taarich"]));
         }
    }

    protected void ShowRikuz(int EmpID, int bakashaID,DateTime taarich)
    {
        KdsLibrary.BL.clReport BlReport = new KdsLibrary.BL.clReport();
        byte[] bPdf;
        //string path,name;
        ////ReportModule _RptModule = ReportModule.GetInstance();
        //FileStream fs;

        string name;

        bPdf = BlReport.getRikuzPdf(EmpID, taarich, bakashaID);
        //path = ConfigurationSettings.AppSettings["PathFileReports"];
        //name = "Rikuz_" + taarich.Year + taarich.Month.ToString().PadLeft(2, '0') + EmpID.ToString().PadLeft(5, '0') + ".pdf";
        //fs = new FileStream(path + name, FileMode.Create, FileAccess.Write);

        //fs.Write(x, 0, x.Length);
        //fs.Flush();
        //fs.Close();


        //Session["BinaryResult"] = x;
        //Session["TypeReport"] = "PDF";
        name = "Rikuz_" + taarich.Year + taarich.Month.ToString().PadLeft(2, '0') + EmpID.ToString().PadLeft(5, '0');// +".pdf";

        try
        {
            Response.Clear();
            Page.EnableViewState = false;

            Response.ClearHeaders();
            Response.AppendHeader("content-length", bPdf.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; Filename=" + name + ".pdf");
          

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.BinaryWrite(bPdf);
            Response.Flush();
            Response.End();
            //Session["BinaryResult"] = null;
            //Session["TypeReport"] = null;
            //Session["FileName"] = null;

        }
        catch (Exception ex)
        { throw ex; }

       // Response.Redirect("~/modalshowprint.aspx");
        //// sScript = "window.showModalDialog('../../modalshowprint.aspx','','dialogwidth:800px;dialogheight:750px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;'); ";
        ////// MasterPage mp = (MasterPage)Page.Master;

        //// ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPdf", sScript, true);
    }


                        
}
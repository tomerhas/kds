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
using KdsLibrary.Utils;
using System.Text;

public partial class ErrorPage : System.Web.UI.Page
{
    protected string SecurityDisplay = String.Empty;
    protected string ErrorDisplay = String.Empty;
    protected string NoResourceDisplay = String.Empty;
    

    protected void Page_Load(object sender, EventArgs e)
    {
            //Display message for 404 page not found
        if (Request.QueryString["ErrorCode"] != null)
        {
            SetDisplayStyle(ref ErrorDisplay, false);
            SetDisplayStyle(ref SecurityDisplay, false);
            SetDisplayStyle(ref NoResourceDisplay, true);
            this.Title = "Resource not Found";
            lblDetails.Text = Request.QueryString["LastError"];
            cpe.ExpandedSize = 20;
            cpe.ScrollContents = false;
        }
            //Display message for General Exception ocurred
        else if (Request.QueryString["LastError"] != null)
        {
            SetDisplayStyle(ref ErrorDisplay, true);
            SetDisplayStyle(ref SecurityDisplay, false);
            SetDisplayStyle(ref NoResourceDisplay, false);
            this.Title = "Error";
            if (HttpContext.Current.Session != null)
                lblDetails.Text =
                    ((Exception)HttpContext.Current.Session["LastError"]).ToString();
            else lblDetails.Text = Request.QueryString["LastError"];
            cpe.ExpandedSize = 200;
            cpe.ScrollContents = true;
        }
            //Display message when trying to open not authorized page
        else
        {
            SetDisplayStyle(ref ErrorDisplay, false);
            SetDisplayStyle(ref SecurityDisplay, true);
            SetDisplayStyle(ref NoResourceDisplay, false);
            this.Title = "Not Authorized";
        }
        
    }

    private void SetDisplayStyle(ref string DisplayStyle, bool visible)
    {
        DisplayStyle = visible ? "block" : "none";
    }

    protected string DetailsDisplay
    {
        get 
        {
            if (ErrorDisplay.Equals("none") && NoResourceDisplay.Equals("none"))
                return "none";
            else return "block";
        }
    }

    protected string GetImagePath()
    {
        StringBuilder sbPath = new StringBuilder();
        string sep = "../";
        string folder = Request.FilePath.Substring(
            Request.ApplicationPath.Length + 1);
        while (folder.IndexOf("/") >= 0)
        {
            int index = folder.IndexOf("/");
            folder = folder.Remove(index, 1);
            sbPath.Append(sep);
        }
        return sbPath.ToString();
    }
}

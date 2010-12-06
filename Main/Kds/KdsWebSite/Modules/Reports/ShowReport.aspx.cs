using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Net;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Utils.Reports;
using Microsoft.Reporting.WebForms;
using System.Reflection;

public partial class Modules_Reports_ShowReport : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetReportViewerDefaultValues();
        }
        SetObjectParameter();
    }

    private void SetReportViewerDefaultValues()
    {
        try
        {
            KdsReport _Report = (KdsReport)Session["Report"];
            RptViewer.Width = (_Report.Orientation == KdsLibrary.Utils.Reports.Orientation.Portrait) ? 790 : 1160;
            RptViewer.PromptAreaCollapsed = true;
            RptViewer.ShowBackButton = false;
            RptViewer.ShowToolBar = true;
            RptViewer.ShowDocumentMapButton = false;
            RptViewer.ShowParameterPrompts = false;
            RptViewer.ShowZoomControl = true;
            RptViewer.ShowRefreshButton = false;
            RptViewer.ShowFindControls = false;
            RptViewer.ZoomPercent = _Report.ZoomPercent;
            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
         if (userAgent.Contains("MSIE 7.0"))
                RptViewer.Attributes.Add("style", "margin-bottom: 30px;");

            RptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            RptViewer.ServerReport.ReportServerUrl = new System.Uri(ConfigurationSettings.AppSettings["ServerReports"]);
            RptViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials(ConfigurationSettings.AppSettings["RSUserName"], ConfigurationSettings.AppSettings["RSPassword"], ConfigurationSettings.AppSettings["RSDomain"]);
            RptViewer.ServerReport.ReportPath = ConfigurationSettings.AppSettings["RSFolderApplication"] + RdlName;
            RptViewer.SizeToReportContent = false;
            SetListRenderingExtensions();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }


    private void SetObjectParameter()
    {
        try
        {
            List<ReportParameter> objParameters;
            Dictionary<string, string> ControlsList = (Dictionary<string, string>)Session["ReportParameters"];
            objParameters = new List<ReportParameter>(ControlsList.Count);
            foreach (KeyValuePair<string, string> Control in ControlsList)
                objParameters.Add(new ReportParameter(Control.Key, Control.Value));
            objParameters.Add(new ReportParameter("P_DT", DateTime.Now.ToString()));
            RptViewer.ServerReport.SetParameters(objParameters);
            SetStyleExportGroup();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    
    private void SetStyleExportGroup()
    {
        FieldInfo InfoExportButton;
        foreach (Control Ctrl in RptViewer.Controls)
        {
            if (Ctrl.GetType().ToString() == "Microsoft.Reporting.WebForms.ToolbarControl")
            {
                foreach (Control CtrlSub in Ctrl.Controls)
                {
                    if (CtrlSub.GetType().ToString() == "Microsoft.Reporting.WebForms.ExportGroup")
                    {
                        foreach (Control CtrlSub2 in CtrlSub.Controls)
                        {
                            if (CtrlSub2.GetType().ToString() == "Microsoft.Reporting.WebForms.TextButton")
                            {
                                InfoExportButton = CtrlSub2.GetType().GetField("m_text", BindingFlags.Instance | BindingFlags.NonPublic);
                                InfoExportButton.SetValue(CtrlSub2, "יצוא");
                            }
                            //else // System.Web.UI.WebControls.DropDownList
                            //{
                            //    InfoExportButton = CtrlSub2.GetType().GetField("SelectedIndex", BindingFlags.Instance | BindingFlags.Public);
                            //    Ddl = (DropDownList)CtrlSub2;
                            //}
                        }
                    }
                }
            }
        }
    }

    private string RdlName
    {
        get
        {
            return (Request.QueryString["RdlName"] == null) ? "Sample1" : Request.QueryString["RdlName"].ToString();
        }
    }

    private void ShowError(string Message)
    {
        ErrorMsg.Style.Add("Display", "Block");
        ErrorMsg.InnerText = Message;
    }

    public void SetListRenderingExtensions()
    {
        FieldInfo infoVisible , infoName;
        foreach (RenderingExtension extension in RptViewer.ServerReport.ListRenderingExtensions())
        {
            infoVisible =  extension.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);
            infoName = extension.GetType().GetField("m_localizedName", BindingFlags.NonPublic | BindingFlags.Instance);
            if ((extension.Name != "EXCEL") && (extension.Name != "PDF") && (infoVisible != null) )
                    infoVisible.SetValue(extension, false);
            if ((extension.Name == "EXCEL") && (infoName != null))
                    infoName.SetValue(extension, "אקסל - Excel");
            if ((extension.Name == "PDF") && (infoName != null))
                infoName.SetValue(extension, "Pdf");
        }
    }
}

/// <summary>
/// Local implementation of IReportServerCredentials
/// </summary>
public class ReportServerCredentials : IReportServerCredentials
{
    private string _userName;
    private string _password;
    private string _domain;

    public ReportServerCredentials(string userName, string password, string domain)
    {
        _userName = userName;
        _password = password;
        _domain = domain;
    }

    public WindowsIdentity ImpersonationUser
    {
        get
        {
            // Use default identity.
            return null;
        }
    }

    public ICredentials NetworkCredentials
    {
        get
        {
            // Use default identity.
            return new NetworkCredential(_userName, _password, _domain);
        }
    }

    public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
    {
        // Do not use forms credentials to authenticate.
        authCookie = null;
        user = password = authority = null;
        return false;
    }
}




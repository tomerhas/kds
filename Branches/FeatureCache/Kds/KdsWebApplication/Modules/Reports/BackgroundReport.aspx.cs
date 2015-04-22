using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.BL;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using KdsBatch.Reports;
using KDSCommon.UDT;

public partial class Modules_Reports_BackgroundReport : System.Web.UI.Page
{
    private string _DestinationFolder ;
    private int _Extension;
    private long _BakashaId; 
    private bool _SendToMail;
    protected void Page_Load(object sender, EventArgs e)
    {
        _DestinationFolder = (string)ConfigurationSettings.AppSettings["HeavyReportsPath"];
        DisplayEmailLabel();
    }

    private void DisplayEmailLabel()
    {
        clOvdim oOvdim = clOvdim.GetInstance();
        DataTable Dt = new DataTable();
        Dt = oOvdim.GetPirteyHitkashrut(UserId);
        //if ((Dt.Rows.Count > 0) && (Dt.Rows[0]["EMAIL"].ToString() != ""))
        //    DivSendMail.Style.Add("Display", "block");
    }




    protected void btnSend_Click(object sender, EventArgs e)
    {
        KdsReport _Report = (KdsReport)Session["Report"];
        string sMessage = string.Empty;
        clBatch objBatch = new clBatch();
        KdsLibrary.BL.clReport oReport = KdsLibrary.BL.clReport.GetInstance();
        COLL_REPORT_PARAM ColUdt = new COLL_REPORT_PARAM();
        Dictionary<string, string> ControlsList = (Dictionary<string, string>)Session["ReportParameters"];
        try
        {
            _Extension = (rdPdfType.Checked) ? (int)eFormat.PDF : (_Report.EXTENSION == "EXCELOPENXML" ? (int)eFormat.EXCELOPENXML : (int)eFormat.EXCEL);
            if (ControlsList.ContainsKey("p_type_rpt")) 
            {
                ControlsList["p_type_rpt"] = _Extension.ToString();
            }
            
            foreach (KeyValuePair<string, string> Control in ControlsList)
                ColUdt.Add(new OBJ_REPORT_PARAM(Control.Key, Control.Value));

            _SendToMail = CkSendInEmail.Checked;
            _BakashaId = objBatch.RunReportsBatch(clGeneral.enGeneralBatchType.CreateHeavyReports, PageHeader + ":" + TxtDescription.Text, clGeneral.enStatusRequest.InProcess, UserId, ColUdt, ReportName, _Extension, _DestinationFolder, _SendToMail);
            sMessage = " בקשתך נשלחה לביצוע ומספרה הוא: " + _BakashaId;
        }
        catch (Exception ex)
        {
            sMessage = ex.Message;
        }
        finally
        {
            lblMessage.Text = sMessage;
            ScriptManager.RegisterStartupScript(btnSend, this.GetType(), "Run", "wsBatch.CreateHeavyReports(" + _BakashaId + ");", true);
            btnShowMessage_Click(this, new EventArgs());
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        ModalPopupEx.Hide();
    }

    protected void btnShowMessage_Click(object sender, EventArgs e)
    {
        ModalPopupEx.Show();
    }
    private string ReportName
    {
        get
        {
            return Request.QueryString["ReportName"].ToString();
        }
    }

    private string PageHeader
    {
        get
        {
            return Server.UrlDecode(Request.QueryString["PageHeader"].ToString());
        }
    }
    private int UserId
    {
        get
        {
            return clGeneral.GetIntegerValue(Request.QueryString["UserId"].ToString());
        }
    }

}

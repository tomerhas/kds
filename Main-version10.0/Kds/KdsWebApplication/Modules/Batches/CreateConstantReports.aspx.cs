using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.UI;
using System.Data;
public partial class Modules_Batches_CreateConstantReports : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtParametrim = new DataTable();
        clUtils oUtils = new clUtils();
        try
        {
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsBatch.asmx";
                PageHeader = "הפקת דוחו''ת קבועים";
                
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                clGeneral.LoadDateCombo(ddlToMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
                ddlToMonth.SelectedIndex = 1;
                LoadMessages((DataList)Master.FindControl("lstMessages"));

                txtDescription.Attributes.Add("maxLength", txtDescription.MaxLength.ToString());
                txtDescription.Attributes.Add("onkeypress", "doKeypress(this);");
                txtDescription.Attributes.Add("onbeforepaste", "doBeforePaste(this);");
                txtDescription.Attributes.Add("onpaste", "doPaste(this);");
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        int iUserId;
        string sMessage;
        long iRequestId;

        clBatch objBatch = new clBatch();

        iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        iRequestId = 0;
       // clManagerReport objManagerReport = new clManagerReport(iRequestId,"01/2010");//
        try
        {

          iRequestId = objBatch.RunReportsBatch(clGeneral.enGeneralBatchType.CreateConstantReprts, txtDescription.Text, clGeneral.enStatusRequest.InProcess, iUserId, ddlToMonth.SelectedValue);
            ViewState["iRequestId"] = iRequestId;

      //     objManagerReport.MakeReports(iRequestId);
          ScriptManager.RegisterStartupScript(btnRun, this.GetType(), "Run", "RunReports(" + iRequestId +",'" + ddlToMonth.SelectedValue + "'," +iUserId + ");", true);

            sMessage = " בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
            lblMessage.Text = sMessage;

            btnShowMessage_Click(this, new EventArgs());

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        ModalPopupEx.Hide();
        Page.Response.Redirect("~/Modules/Requests/MaakavBakashot.aspx?RequestId=" + ViewState["iRequestId"], false);
    }


    protected void btnShowMessage_Click(object sender, EventArgs e)
    {
        ModalPopupEx.Show();
    }
}

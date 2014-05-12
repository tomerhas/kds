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
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.UI;


public partial class Modules_Batches_HasavatNetuneyMFtoOracle : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsBatch.asmx";
                PageHeader = "הפעלת הסבת נתונים לאורקל";
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
        int  iUserId;
        string sMessage;
        long iRequestId;

        clBatch objBatch = new clBatch();

        iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        iRequestId = 0;
        try
        {
            iRequestId = objBatch.RunErrorBatch(clGeneral.enGeneralBatchType.HasavatNetuniToOracle, txtDescription.Text, clGeneral.enStatusRequest.InProcess, iUserId);
            ViewState["iRequestId"] = iRequestId;
            ScriptManager.RegisterStartupScript(btnRun, this.GetType(), "Run", "RunHasava(" + iRequestId + ");", true);
            
            
            sMessage=" בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
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
        Page.Response.Redirect("~/Modules/Requests/MaakavBakashot.aspx?RequestId=" + ViewState["iRequestId"]);
    }


  protected void btnShowMessage_Click(object sender, EventArgs e)
  {
      ModalPopupEx.Show();
  }
}

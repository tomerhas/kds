using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KdsLibrary.BL;
using KdsLibrary.UI;
using KdsLibrary;
using KdsBatch;

public partial class Modules_Batches_RunCalcBatch : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dtParametrim = new DataTable();
            clUtils oUtils = new clUtils();
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsBatch.asmx";
                PageHeader = "ריצת חישוב";
                divRizaGorefet.Style.Add("display", "none");
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                clGeneral.LoadDateCombo(ddlToMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
           //  clGeneral.LoadDateCombo(ddlToMonth,11);
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



    protected void btnRunShinuyKelet_Click(object sender, EventArgs e)
    {
        clOvdim objOvdim = new clOvdim();
        string sMaamad = "";
        DateTime dFrom, dTo;
        DataTable dtParametrim,dt;
        clUtils oUtils = new clUtils();
        int i;
        bool nextStep = false;
        string sMessage;
        try
        {
            if (chkFriends.Checked) { sMaamad = clGeneral.enMaamad.Friends.GetHashCode().ToString(); }

            if (chkSalarieds.Checked)
            {
                if (sMaamad.Length > 0) { sMaamad += ","; }
                sMaamad += clGeneral.enMaamad.Salarieds.GetHashCode().ToString();
            }
            dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
            dFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths((int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()) - 1) * -1);
            dFrom = dFrom >= DateTime.Parse("01/07/2012") ? dFrom : DateTime.Parse("01/07/2012");
            dTo = (DateTime.Parse(ddlToMonth.SelectedValue)).AddMonths(1).AddDays(-1);
          
            dt = objOvdim.GetWorkCardNoShaotLetashlum(dFrom, dTo, sMaamad);
            for (i = 0; i < dt.Rows.Count; i++)
            {
                clBatchManager btchMan = new clBatchManager(0, clGeneral.enSugeyMeadkenShinuyim.MasachChishuvBatch.GetHashCode());

                try
                {
                    nextStep = btchMan.MainInputData(int.Parse(dt.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dt.Rows[i]["taarich"].ToString()));

                    if (nextStep)
                    {
                        nextStep = btchMan.MainOvedErrors(int.Parse(dt.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dt.Rows[i]["taarich"].ToString()));
                    }
                 }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    btchMan.Dispose();
                }
            }
            sMessage = "ההרצה בוצעה בהצלחה";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Err", "alert('" + sMessage + "');", true);
         }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnCount_Click(object sender, EventArgs e)
    {
          clOvdim objOvdim = new clOvdim();
         string sMaamad = "";
         DateTime dFrom, dTo;
         DataTable dtParametrim;
         clUtils oUtils = new clUtils();
         int iCount;
         string sMessage;
        try
        {
             if (chkFriends.Checked) { sMaamad = clGeneral.enMaamad.Friends.GetHashCode().ToString(); }

            if (chkSalarieds.Checked)
            {
                if (sMaamad.Length > 0) { sMaamad += ","; }
                sMaamad += clGeneral.enMaamad.Salarieds.GetHashCode().ToString();
            }
            dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
            dFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString())  * -1);
            dFrom = dFrom >= DateTime.Parse("01/07/2012") ? dFrom : DateTime.Parse("01/07/2012");
            dTo = (DateTime.Parse(ddlToMonth.SelectedValue)).AddMonths(1).AddDays(-1);
           iCount= objOvdim.GetCountWorkCardNoShaotLetashlum(dFrom, dTo, sMaamad);
           sMessage = "מספר כרטיסי עבודה עם שעת התחלה.גמר לתשלום חסרה: " + iCount;

           ScriptManager.RegisterStartupScript(this,this.GetType(), "Err", "alert('" + sMessage + "');", true);

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }


    protected void btnCountMeafyen_Click(object sender, EventArgs e)
    {
          clOvdim objOvdim = new clOvdim();
         string sMaamad = "";
         DateTime dFrom, dTo;
         DataTable dtParametrim;
         clUtils oUtils = new clUtils();
         int iCount;
         string sMessage;
        try
        {
             if (chkFriends.Checked) { sMaamad = clGeneral.enMaamad.Friends.GetHashCode().ToString(); }

            if (chkSalarieds.Checked)
            {
                if (sMaamad.Length > 0) { sMaamad += ","; }
                sMaamad += clGeneral.enMaamad.Salarieds.GetHashCode().ToString();
            }
            dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
            dFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString())  * -1);
            dFrom = dFrom >= DateTime.Parse("01/07/2012") ? dFrom : DateTime.Parse("01/07/2012");
            dTo = (DateTime.Parse(ddlToMonth.SelectedValue)).AddMonths(1).AddDays(-1);
           iCount= objOvdim.GetCountWCLoLetashlumWithMeafyenim(dFrom, dTo);
           sMessage = "כמות כע סידור לא לתשלום ולעובד מאפייני התחלה/גמר: " + iCount;

           ScriptManager.RegisterStartupScript(this,this.GetType(), "Err", "alert('" + sMessage + "');", true);

        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        int iUserId,iRunAll;
        long  iRequestId;
        string sMessage, sMaamad;
        clBatch objBatch = new clBatch();

        iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        iRunAll=0;
        sMaamad = "";
        try
        {
            if (chkFriends.Checked) { sMaamad = clGeneral.enMaamad.Friends.GetHashCode().ToString(); }

            if (chkSalarieds.Checked)
            {
                if (sMaamad.Length > 0) { sMaamad += ","; }
                sMaamad += clGeneral.enMaamad.Salarieds.GetHashCode().ToString();
            }

            if (chkRunAll.Checked) { iRunAll = 1; }
           
            iRequestId = objBatch.RunCalcBatch(clGeneral.enGeneralBatchType.Calculation, txtDescription.Text, clGeneral.enStatusRequest.InProcess, iUserId, sMaamad, ddlToMonth.SelectedValue, iRunAll,chkTest.Checked.GetHashCode());
            ViewState["iRequestId"] = iRequestId;
            ScriptManager.RegisterStartupScript(btnRun, this.GetType(), "Run", "RunCalc(" + iRequestId + ",'" + ddlToMonth.SelectedValue + "','" + sMaamad + "','" + chkTest.Checked + "','" + chkRunAll.Checked + "');", true);
         

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
      Page.Response.Redirect("~/Modules/Requests/MaakavBakashot.aspx?RequestId=" + ViewState["iRequestId"]);
    
    }


    protected void btnShowMessage_Click(object sender, EventArgs e)
    {
        ModalPopupEx.Show();
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            clBatch oBatch = new clBatch();
            oBatch.RefreshTable("TB_PREM");
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    //protected void btnRunShguiim_Click(object sender, EventArgs e)
    //{
    //    int iUserId, iRunAll;
    //    long iRequestId;
    //    string sMessage, sMaamad;
    //    clBatch objBatch = new clBatch();

    //    iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
    //    iRunAll = 0;
    //    sMaamad = "";
    //    try
    //    {

    //        ServicePath = "~/Modules/WebServices/wsBatch.asmx";
    //        iRequestId = objBatch.RunCalcBatch(clGeneral.enGeneralBatchType.Calculation, txtDescription.Text, clGeneral.enStatusRequest.InProcess, iUserId, sMaamad, ddlToMonth.SelectedValue, iRunAll, chkTest.Checked.GetHashCode());
    //        ViewState["iRequestId"] = iRequestId;
    //        ScriptManager.RegisterStartupScript(btnRun, this.GetType(), "Run", "RunShguyim(" + iRequestId + ",'" + DateTime.Now.ToShortDateString() + "','5','1');", true);


    //        sMessage = " בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
    //        lblMessage.Text = sMessage;
    //        btnShowMessage_Click(this, new EventArgs());

    //    }
    //    catch (Exception ex)
    //    {
    //        clGeneral.BuildError(Page, ex.Message);
    //    }

    //}

//    <asp:Button Text="שגויים" ID="Button1" runat="server" CssClass="ImgButtonSearch" 
//                        onclick="btnRunShguiim_Click"  /> 

    

//function RunShguyim(iRequestId, dChodesh, iCalcType, iBatchExecutionType) {
//    wsBatch.RunShinuimVeShguim(iRequestId, dChodesh, iCalcType, iBatchExecutionType); //, RunCalcSucceeded);
//}
}

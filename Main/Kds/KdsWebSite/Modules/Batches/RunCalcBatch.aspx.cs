﻿using System;
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

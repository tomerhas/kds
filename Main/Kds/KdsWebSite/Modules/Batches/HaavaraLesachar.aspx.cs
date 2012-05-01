using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.BL;
using System.Data;
using KdsLibrary;
using KdsBatch;
public partial class Modules_Batches_HaavaraLesachar :KdsPage
{

    private enum enGrdRitzot
    {
        ZMAN_HATCHALA = 0,
        bakasha_id,
        TEUR,
        auchlusia,
        tkufa,
        ritza_gorfet,
        HUAVRA_LESACHAR,
        ISHUR_HILAN,
        btns_kvazim,
        status_haavara_lesachar,
        status,
        btn_Rikuzim,
        status_yezirat_rikuzim,
        btn_send,
        rizot_zehot,
        btns_ishur_hilan
    }


 

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsBatch.asmx";
                PageHeader = "מסך העברה לשכר";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
                clnToDate.Text = DateTime.Now.ToShortDateString();
                clnFromDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                divNetunim.Visible = false;
                MasterPage mp = (MasterPage)Page.Master;
                SetFixedHeaderGrid(pnlgrdRitzot.ClientID, mp.HeadPage);
                //inputHiddenMinDate.Value = DateTime.Now.AddMonths(-1).ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

 protected void btnShow_Click(object sender, EventArgs e)
    {
     try
     {
         if (Page.IsValid)
         {
             if (DateTime.Parse(clnFromDate.Text) < DateTime.Parse(DateTime.Now.AddMonths(-1).ToShortDateString()) ||
                 DateTime.Parse(clnFromDate.Text) > DateTime.Now ||
                 DateTime.Parse(clnToDate.Text) < DateTime.Parse(DateTime.Now.AddMonths(-1).ToShortDateString()) ||
                 DateTime.Parse(clnToDate.Text) > DateTime.Now)
             {
                 ScriptManager.RegisterStartupScript(btnShow, this.GetType(), "err", "alert('לא ניתן להגדיר טווח תאריכים מעבר לחודש אחורה מתאריך נוכחי');", true);
             }
             else
             {
                 divNetunim.Visible = true;
                 ViewState["SortDirection"] = SortDirection.Descending;
                 ViewState["SortExp"] = "ZMAN_HATCHALA";
                 GetRitzot();
             }
            

         }
     }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

 }


 private void GetRitzot()
 {
 
     clBatch objBatch = new clBatch();
     DataView dvRitzot;
     DataTable dtRitzot;

     dtRitzot = objBatch.GetPirteyRitzotChishuv(DateTime.Parse(clnFromDate.Text), DateTime.Parse(clnToDate.Text), rdoRitzotAll.Checked);
     dvRitzot = new DataView(dtRitzot);

     dvRitzot.Sort = "ZMAN_HATCHALA DESC";

     grdRitzot.DataSource = dvRitzot;
     grdRitzot.DataBind();
     Session["Ritzot"] = dvRitzot;
  
 }

 private int GetCurrentColSort()
 {
     string sSortExp = (string)ViewState["SortExp"];
     int iColNum = -1;
     try
     {
         foreach (DataControlField dc in grdRitzot.Columns)
         {
             iColNum++;
             if (dc.SortExpression.Equals(sSortExp)) { break; }
         }

         return iColNum;
     }

     catch (Exception ex)
     {
         throw ex;
     }
 }


 protected void grdRitzot_RowDataBound(object sender, GridViewRowEventArgs e)
 {
     int iColSort;
     string status;
     if (e.Row.RowType == DataControlRowType.Header)
     {
         System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
         e.Row.Cells[enGrdRitzot.HUAVRA_LESACHAR.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.ISHUR_HILAN.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.status_haavara_lesachar.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.status_yezirat_rikuzim.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.rizot_zehot.GetHashCode()].Style.Add("display", "none");

         iColSort = GetCurrentColSort();
         lbl.Text = " ";
         e.Row.Cells[iColSort].Controls.Add(lbl);
         if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
         {
             System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
             ImageSort.ID = "imgDescSort";
             ImageSort.ImageUrl = "../../Images/DescSort.gif";
             e.Row.Cells[iColSort].Controls.Add(ImageSort);
         }
         else
         {
             System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
             ImageSort.ID = "imgAsccSort";
             ImageSort.ImageUrl = "../../Images/AscSort.gif";
             e.Row.Cells[iColSort].Controls.Add(ImageSort);
         }

         int i = 0;
         object sortHeader = null;

         for (i = 0; i < e.Row.Cells.Count-1; i++)
         {
             if (e.Row.Cells[i].Controls.Count >0)
             {
                 sortHeader = e.Row.Cells[i].Controls[0];
                 ((LinkButton)(sortHeader)).Style.Add("color", "white");
                 ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
             }
         }
     }
     else if (e.Row.RowType == DataControlRowType.DataRow)
     {
         e.Row.Cells[enGrdRitzot.HUAVRA_LESACHAR.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.ISHUR_HILAN.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.status_haavara_lesachar.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.status_yezirat_rikuzim.GetHashCode()].Style.Add("display", "none");
         e.Row.Cells[enGrdRitzot.rizot_zehot.GetHashCode()].Style.Add("display", "none");

         switch (e.Row.Cells[enGrdRitzot.status_haavara_lesachar.GetHashCode()].Text.Trim())
         {
             case "1": e.Row.Cells[enGrdRitzot.status.GetHashCode()].Text ="ממתין";
                 break;
             case "2": e.Row.Cells[enGrdRitzot.status.GetHashCode()].Text = "הסתיים";
                 break;
             case "3": e.Row.Cells[enGrdRitzot.status.GetHashCode()].Text = "נכשל";
                 break;
         }

         if (e.Row.Cells[enGrdRitzot.HUAVRA_LESACHAR.GetHashCode()].Text.Trim() != "1")
         {
             ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[1]).Style["display"] = "inline";
             ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[3]).Style["display"] = "none";
             if (e.Row.Cells[enGrdRitzot.status_haavara_lesachar.GetHashCode()].Text.Trim() == "1")
                 ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[1]).Enabled = false;
             else
                 ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[1]).Enabled = true;
         }
         else
         {
             ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[1]).Style["display"] = "none";
             ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[3]).Style["display"] = "inline";
             if (e.Row.Cells[enGrdRitzot.ISHUR_HILAN.GetHashCode()].Text == "1")
                 ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[3]).Enabled = false;
             else
                 ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[3]).Enabled = true;
         }
         
         //if (e.Row.Cells[enGrdRitzot.HUAVRA_LESACHAR.GetHashCode()].Text == "1")
         //((Button)e.Row.Cells[enGrdRitzot.btn_Rikuzim.GetHashCode()].Controls[1]).Enabled = true;

         if (e.Row.Cells[enGrdRitzot.status_yezirat_rikuzim.GetHashCode()].Text == "2" && e.Row.Cells[enGrdRitzot.HUAVRA_LESACHAR.GetHashCode()].Text == "1")
             ((Button)e.Row.Cells[enGrdRitzot.btn_send.GetHashCode()].Controls[1]).Enabled = true;

         if (e.Row.Cells[enGrdRitzot.ISHUR_HILAN.GetHashCode()].Text == "1")
         {
             ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[1]).Enabled = false;
             ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[3]).Enabled = true;    
         }
         else
         {
             ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[1]).Enabled = true;
             ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[3]).Enabled = false;  
         }
         if (e.Row.Cells[enGrdRitzot.HUAVRA_LESACHAR.GetHashCode()].Text != "1")
         {
             ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[1]).Enabled = false;
             ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[3]).Enabled = false;  
         }
         ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[1]).CommandArgument = e.Row.Cells[enGrdRitzot.bakasha_id.GetHashCode()].Text + "," + e.Row.Cells[enGrdRitzot.rizot_zehot.GetHashCode()].Text ;
         ((Button)e.Row.Cells[enGrdRitzot.btns_kvazim.GetHashCode()].Controls[3]).CommandArgument = e.Row.Cells[enGrdRitzot.bakasha_id.GetHashCode()].Text;
         ((Button)e.Row.Cells[enGrdRitzot.btn_Rikuzim.GetHashCode()].Controls[1]).CommandArgument = e.Row.Cells[enGrdRitzot.bakasha_id.GetHashCode()].Text;
         ((Button)e.Row.Cells[enGrdRitzot.btn_send.GetHashCode()].Controls[1]).CommandArgument = e.Row.Cells[enGrdRitzot.bakasha_id.GetHashCode()].Text;
         ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[1]).CommandArgument = e.Row.Cells[enGrdRitzot.bakasha_id.GetHashCode()].Text;
         ((Button)e.Row.Cells[enGrdRitzot.btns_ishur_hilan.GetHashCode()].Controls[3]).CommandArgument = e.Row.Cells[enGrdRitzot.bakasha_id.GetHashCode()].Text;
          
     }
 }

 protected void grdRitzot_Sorting(object sender, GridViewSortEventArgs e)
 {
     string sDirection;
     try
     {
         if ((string.Empty != (string)ViewState["SortExp"]) && (string.Compare(e.SortExpression, (string)ViewState["SortExp"], true) == 0))
         {
             if ((SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
             {
                 sDirection = "DESC";
                 ViewState["SortDirection"] = SortDirection.Descending;
             }
             else
             {
                 sDirection = "ASC";
                 ViewState["SortDirection"] = SortDirection.Ascending;
             }
         }
         else
         {
             sDirection = "ASC";
             ViewState["SortDirection"] = SortDirection.Ascending;
         }
         ViewState["SortExp"] = e.SortExpression;

         if (Session["Ritzot"] != null)
         {
             ((DataView)Session["Ritzot"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
             grdRitzot.DataSource = (DataView)Session["Ritzot"];
             grdRitzot.DataBind();
         }
         else
         {
           
             GetRitzot();
         }
     }
     catch (Exception ex)
     {
         throw ex;
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

 protected void TransferRitza(object sender, EventArgs e)
 {
     string RizotZehot;
     string[] commandArgsAccept;
     
    clBatch objBatch = new clBatch();
     try
     {
         commandArgsAccept = ((Button)sender).CommandArgument.ToString().Split(new char[] { ',' });
         inputHiddenBakasha.Value = commandArgsAccept[0]; 
         if (((Button)sender).ClientID.IndexOf("btnNo") > -1)
         {
             inputSourceBtnHilan.Value = "No";
             ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMesssage", "ShowMessageHilan('ריצת החישוב מסומנת כאושרה בחילן, האם ברצונך לבטל את האישור?');", true);
            
         }
         else if (((Button)sender).ClientID.IndexOf("btnYes") > -1)
         {
             inputSourceBtnHilan.Value = "Yes";
             ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMesssage", "ShowMessageHilan('ריצת החישוב טרם אושרה בחיל, האם ברצונך לסמן אותה כאושרה בחילן?');", true);
         }
         else
         {
             RizotZehot = commandArgsAccept[1].ToString();
             if (RizotZehot.Trim() != "&nbsp;" && RizotZehot.Trim() != "")
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMesssage", "ShowMessage('" + RizotZehot + "');", true);
             }
             else Transfer_Click(sender, e);
         }
         //iRequestId = objBatch.RunTransferToSachar(clGeneral.enGeneralBatchType.TransferToPayment, "", clGeneral.enStatusRequest.InProcess, iUserId, iRequestToTransfer);
         //ViewState["iRequestId"] = iRequestId;
         //ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Run", "TransetToSachar(" + iRequestId + "," + iRequestToTransfer + ");", true);
         
         //sMessage = " בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
         //lblMessage.Text = sMessage;
         //btnShowMessage_Click(this, new EventArgs());
     
     }
     catch (Exception ex)
     {
         clGeneral.BuildError(Page, ex.Message);
     }
 }



 protected void IshurHilan_Click(object sender, EventArgs e)
 {
     long iRequestToTransfer;
     string source;
     clBatch objBatch = new clBatch();
     try
     {
         source = inputSourceBtnHilan.Value;
         iRequestToTransfer = long.Parse(inputHiddenBakasha.Value);
         if (source =="Yes")
            clDefinitions.UpdateBakashaParams(iRequestToTransfer,1);
         else
             clDefinitions.UpdateBakashaParams(iRequestToTransfer, 0);

         GetRitzot();
     }
     catch (Exception ex)
     {
         clGeneral.BuildError(Page, ex.Message);
     }
 }
 protected void Transfer_Click(object sender, EventArgs e)
 {
     long iRequestId, iRequestToTransfer;
     string sMessage;
     int iUserId;
     clBatch objBatch = new clBatch();
     try
     {
         iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        // commandArgsAccept = ((Button)sender).CommandArgument.ToString().Split(new char[] { ',' });
         iRequestToTransfer = long.Parse(inputHiddenBakasha.Value);  // long.Parse(((Button)sender).CommandArgument);
         iRequestId = objBatch.RunTransferToSachar(clGeneral.enGeneralBatchType.TransferToPayment, "", clGeneral.enStatusRequest.InProcess, iUserId, iRequestToTransfer);
         ViewState["iRequestId"] = iRequestId;
         ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Run", "TransetToSachar(" + iRequestId + "," + iRequestToTransfer + ");", true);

         sMessage = " בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
         lblMessage.Text = sMessage;
         btnShowMessage_Click(this, new EventArgs());

     }
     catch (Exception ex)
     {
         clGeneral.BuildError(Page, ex.Message);
     }
 }

 protected void CancelRitza(object sender, EventArgs e)
 {
     long iRequestToTransfer;
     try
     {
           iRequestToTransfer = long.Parse(((Button)sender).CommandArgument);
           clDefinitions.UpdateLogBakasha(iRequestToTransfer, 2, DateTime.Now);
     }
     catch (Exception ex)
     {
         clGeneral.BuildError(Page, ex.Message);
     }
 }
 protected void YeziratRikuzim(object sender, EventArgs e)
 {
     long iRequestId, iRequestIdForRikuzim;
     string sMessage;
     int iUserId;

     clBatch objBatch = new clBatch();
     try
     {
         iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
         iRequestIdForRikuzim = long.Parse(((Button)sender).CommandArgument);
         iRequestId = objBatch.YeziratBakashatRikuzim(clGeneral.enGeneralBatchType.YeziratRikuzim, "", clGeneral.enStatusRequest.InProcess, iUserId, iRequestIdForRikuzim);
         ViewState["iRequestId"] = iRequestId;
         ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Run", "YeziratRikuzim(" + iRequestId + "," + iRequestIdForRikuzim + ");", true);

         sMessage = " בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
         lblMessage.Text = sMessage;
         btnShowMessage_Click(this, new EventArgs());

     }
     catch (Exception ex)
     {
         clGeneral.BuildError(Page, ex.Message);
     }
 }

 protected void ShlichatRikuzimMail(object sender, EventArgs e)
 {
     long iRequestId, iRequestIdForRikuzim;
     string sMessage;
     int iUserId;

     clBatch objBatch = new clBatch();
     try
     {
         iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
         iRequestIdForRikuzim = long.Parse(((Button)sender).CommandArgument);
         iRequestId = objBatch.YeziratBakashatRikuzim(clGeneral.enGeneralBatchType.SendRikuzimMail, "", clGeneral.enStatusRequest.InProcess, iUserId, iRequestIdForRikuzim);
         ViewState["iRequestId"] = iRequestId;
         
         ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Run", "ShlichatRikuzimMail(" + iRequestId + "," + iRequestIdForRikuzim + ");", true);

         sMessage = " בקשתך נשלחה לביצוע באצווה מספרה הוא: " + iRequestId;
         lblMessage.Text = sMessage;
         btnShowMessage_Click(this, new EventArgs());

     }
     catch (Exception ex)
     {
         clGeneral.BuildError(Page, ex.Message);
     }
 }


}

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

public partial class Modules_Batches_HaavaraLesachar :KdsPage
{
    private const int COL_REQUEST_ID  = 1;
    private const int COL_AVAR_LASACHAR = 6;
    private const int COL_BUTTON = 7;
    private const int COL_BUTTON_RIKUZ = 8;

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
     
     if (e.Row.RowType == DataControlRowType.Header)
     {
         System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
         e.Row.Cells[(int)COL_AVAR_LASACHAR].Style.Add("display", "none");
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

         for (i = 0; i < e.Row.Cells.Count-3; i++)
         {
             sortHeader = e.Row.Cells[i].Controls[0];
             ((LinkButton)(sortHeader)).Style.Add("color", "white");
             ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
         }
     }
     else if (e.Row.RowType == DataControlRowType.DataRow)
     {
         e.Row.Cells[(int)COL_AVAR_LASACHAR].Style.Add("display", "none");
         if (e.Row.Cells[(int)COL_AVAR_LASACHAR].Text == "1")
             ((Button)e.Row.Cells[COL_BUTTON_RIKUZ].Controls[1]).Enabled = true;
         ((Button)e.Row.Cells[COL_BUTTON].Controls[1]).CommandArgument = e.Row.Cells[COL_REQUEST_ID].Text;
         ((Button)e.Row.Cells[COL_BUTTON_RIKUZ].Controls[1]).CommandArgument = e.Row.Cells[COL_REQUEST_ID].Text;
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
     long iRequestId, iRequestToTransfer;
     string sMessage;
     int iUserId;
     
    clBatch objBatch = new clBatch();
     try
     {
         iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
         iRequestToTransfer = long.Parse(((Button)sender).CommandArgument);
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

 protected void YeziratRicuzim(object sender, EventArgs e)
 {
     long iRequestId, iRequestIdForRikuzim;
     string sMessage;
     int iUserId;

     clBatch objBatch = new clBatch();
     try
     {
         iUserId = int.Parse(LoginUser.UserInfo.EmployeeNumber);
         iRequestIdForRikuzim = long.Parse(((Button)sender).CommandArgument);
         iRequestId = objBatch.RunTransferToSachar(clGeneral.enGeneralBatchType.TransferToPayment, "", clGeneral.enStatusRequest.InProcess, iUserId, iRequestIdForRikuzim);
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
}

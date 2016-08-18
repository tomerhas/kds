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
using KdsLibrary.UI;
using KdsLibrary.Security;
using KdsLibrary.BL;
using KdsLibrary.Utils.Reports;
using KdsBatch;
using KdsLibrary.UI.SystemManager;

public partial class Modules_Rikuzim_ViewRikuzim   : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MasterPage mp = (MasterPage)Page.Master;
            // mp.ImageExcelClick.Click += new ImageClickEventHandler(ImageExcelClick_Click);
            // mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);
            ((ScriptManager)mp.FindControl("ScriptManagerKds")).AsyncPostBackTimeout = 360;

            if (!Page.IsPostBack)
            {
                PageHeader = "מסך ריכוזי עבודה שהועברו לשכר";
                DateHeader = DateTime.Today.ToShortDateString();
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
               // btnShow.Enabled = false;
                LoadMessages((DataList)Master.FindControl("lstMessages"));

                // divNetunim.Visible = false;

                // SetFixedHeaderGrid(pnlTotalMonthly.ClientID, mp.HeadPage);
                //  SetFixedHeaderGrid(pnlMonthlyComponents.ClientID, mp.HeadPage);

                divNetunim.Style.Add("overflow-y", "hidden");
                grdRikuzim.AllowPaging = false;
                divNetunim.Style["height"] = "250px";

              //  AutoCompleteExtenderByName.ContextKey = "";
               // AutoCompleteExtenderID.ContextKey = "";
               // txtEmpId.Focus();

               // LoadDdlMonth();
               // LoadDdlYears();
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        clUtils oUtils = new clUtils();
        DataTable dtRikuzim;
        DataView dv;
        try
        {
            if (txtRequestNum.Text != "")
            {
                dtRikuzim = oUtils.GetOvdimLeBakashatChishuv((long)int.Parse(txtRequestNum.Text));
                dv = new DataView(dtRikuzim);
                grdRikuzim.DataSource = dv;
                grdRikuzim.DataBind();
                divNetunim.Visible = true;
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void grdRikuzim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //int bakasha_id,iColSort;
        //string taarich;
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    //e.Row.Cells[enGrdRikuz.BAKASHA_ID.GetHashCode()].Style.Add("display", "none");
            
        //    //System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();

        //    //iColSort = GetCurrentColSort();
        //    //lbl.Text = " ";
        //    e.Row.Cells[iColSort].Controls.Add(lbl);
        //    if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
        //    {
        //        System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
        //        ImageSort.ID = "imgDescSort";
        //        ImageSort.ImageUrl = "../../Images/DescSort.gif";
        //        e.Row.Cells[iColSort].Controls.Add(ImageSort);
        //    }
        //    else
        //    {
        //        System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
        //        ImageSort.ID = "imgAsccSort";
        //        ImageSort.ImageUrl = "../../Images/AscSort.gif";
        //        e.Row.Cells[iColSort].Controls.Add(ImageSort);
        //    }
        //    int i = 0;
        //    object sortHeader = null;
        //    for (i = 1; i <= e.Row.Cells.Count - 2; i++)
        //    {
        //        sortHeader = e.Row.Cells[i].Controls[0];
        //        ((LinkButton)(sortHeader)).Style.Add("color", "white");
        //        ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
        //    }
        //}
        //else   if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[enGrdRikuz.BAKASHA_ID.GetHashCode()].Style.Add("display", "none");

        //    if (e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text == "0")
        //        e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text = "רגילה";
        //    else e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text = "הפרש";

        //    ((HyperLink)e.Row.Cells[enGrdRikuz.PDF.GetHashCode()].Controls[1]).ImageUrl = "../../Images/icon-pdf.jpg";
            
        //    bakasha_id = int.Parse(e.Row.Cells[enGrdRikuz.BAKASHA_ID.GetHashCode()].Text);
        //    taarich = "01" + "/" + e.Row.Cells[enGrdRikuz.TAARICH.GetHashCode()].Text;
        //    ((HyperLink)e.Row.Cells[enGrdRikuz.PDF.GetHashCode()].Controls[1]).Attributes.Add("onclick", "onclick_ShowReport(" + int.Parse(txtEmpId.Text) + ", " + bakasha_id + ", '" + taarich + "')");
        // }
      }
}
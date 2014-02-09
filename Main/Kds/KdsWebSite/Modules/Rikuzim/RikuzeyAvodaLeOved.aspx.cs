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
public partial class Modules_Ovdim_RikuzeyAvodaLeOved   : KdsPage
{
    private enum enGrdRikuz
    {
        BAKASHA_ID =0,
        TAARICH,
        SUG_CHISHUV,
        HUAVRA_LESACHAR,
        TAARICH_HAAVARA_LESACHAR,
        PDF
    }

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    grdRikuzim.RowCreated += new GridViewRowEventHandler(grdRikuzim_RowCreated);
    //}
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
                PageHeader = "מסך ריכוזי עבודה";
                DateHeader = DateTime.Today.ToShortDateString();
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                btnShow.Enabled = false;
                LoadMessages((DataList)Master.FindControl("lstMessages"));

                // divNetunim.Visible = false;

                // SetFixedHeaderGrid(pnlTotalMonthly.ClientID, mp.HeadPage);
                //  SetFixedHeaderGrid(pnlMonthlyComponents.ClientID, mp.HeadPage);

                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                AutoCompleteExtenderID.ContextKey = "";
                if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
                {
                    AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                }

                divNetunim.Style.Add("overflow-y", "hidden");
                grdRikuzim.AllowPaging = false;
                divNetunim.Style["height"] = "250px";

                //AutoCompleteExtenderByName.ContextKey = "";
               // AutoCompleteExtenderID.ContextKey = "";
                txtEmpId.Focus();

                LoadDdlMonth();
                LoadDdlYears();
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }


    void LoadDdlMonth()
    {
        ListItem Item;
        for (int i = 1; i < 13; i++)
        {
            Item = new ListItem();
            Item.Text = i.ToString();
            Item.Value = i.ToString();
            ddlMonth.Items.Add(Item);
          
        }
        ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
    }

    void LoadDdlYears()
    {
        ListItem Item;
        DateTime taarich = DateTime.Now;
        for (int i = 0; i < 20; i++)
        {
            Item = new ListItem();
            Item.Text = taarich.Year.ToString();
            Item.Value = taarich.Year.ToString();
            ddlYears.Items.Add(Item);
            taarich = taarich.AddYears(-1);
        }
        ddlYears.SelectedValue = DateTime.Now.AddMonths(-1).Year.ToString();
    }



    protected void btnShow_Click(object sender, EventArgs e)
    {
        clOvdim oOvdim = new clOvdim();
        DataTable dtRikuzim;
        DateTime taarich;
        DataView dv;
        try
        {
            taarich = DateTime.Parse("01/" + ddlMonth.SelectedValue + "/" + ddlYears.SelectedValue);
            dtRikuzim = oOvdim.getRikuzimLeOved(int.Parse(txtEmpId.Text), taarich);

            dv = new DataView(dtRikuzim);
            grdRikuzim.DataSource = dv;

            ViewState["SortDirection"] = SortDirection.Descending;
            ViewState["SortExp"] = "TAARICH";

            Session["Rikuzim"] = dv;
            grdRikuzim.DataBind();

            if (grdRikuzim.Rows.Count > (grdRikuzim.PageSize + 1))
                divNetunim.Style["overflow-y"] = "scroll";
            else divNetunim.Style["overflow-y"] = "hidden";

            divNetunim.Visible = true;
            btnShow.Enabled = true;
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void grdRikuzim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int bakasha_id,iColSort;
        string taarich;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[enGrdRikuz.BAKASHA_ID.GetHashCode()].Style.Add("display", "none");
            
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();

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
            for (i = 1; i <= e.Row.Cells.Count - 2; i++)
            {
                sortHeader = e.Row.Cells[i].Controls[0];
                ((LinkButton)(sortHeader)).Style.Add("color", "white");
                ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
            }
        }
        else   if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[enGrdRikuz.BAKASHA_ID.GetHashCode()].Style.Add("display", "none");

            if (e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text == "0")
                e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text = "רגילה";
            else if(e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text == "1")
                  e.Row.Cells[enGrdRikuz.SUG_CHISHUV.GetHashCode()].Text = "הפרש";

            ((HyperLink)e.Row.Cells[enGrdRikuz.PDF.GetHashCode()].Controls[1]).ImageUrl = "../../Images/icon-pdf.jpg";
            
            bakasha_id = int.Parse(e.Row.Cells[enGrdRikuz.BAKASHA_ID.GetHashCode()].Text);
            taarich = "01" + "/" + e.Row.Cells[enGrdRikuz.TAARICH.GetHashCode()].Text;
            ((HyperLink)e.Row.Cells[enGrdRikuz.PDF.GetHashCode()].Controls[1]).Attributes.Add("onclick", "onclick_ShowReport(" + int.Parse(txtEmpId.Text) + ", " + bakasha_id + ", '" + taarich + "')");
         }
      }


    //////*******Pager Functions*********/
    ////void grdRikuzim_RowCreated(object sender, GridViewRowEventArgs e)
    ////{
    ////    if (e.Row.RowType == DataControlRowType.Pager)
    ////    {
    ////        IGridViewPager gridPager = e.Row.FindControl("ucGridPager")
    ////        as IGridViewPager;
    ////        if (gridPager != null)
    ////        {
    ////            gridPager.PageIndexChanged += delegate(object pagerSender,
    ////                GridViewPageEventArgs pagerArgs)
    ////            {
    ////                ChangeGridPage(pagerArgs.NewPageIndex, grdRikuzim,
    ////                   (DataView)Session["Rikuzim"], "SortDirection",
    ////                   "SortExp");
    ////            };
    ////        }
    ////    }
    ////}
    ////private void ChangeGridPage(int pageIndex, GridView grid, DataView dataView,
    ////                            string sortDirViewStateKey, string sortExprViewStateKey)
    ////{
    ////    //   SetChangesOfGridInDataview(grid, ref dataView);
    ////    grid.PageIndex = pageIndex;
    ////    string sortExpr = String.Empty;
    ////    SortDirection sortDir = SortDirection.Ascending;
    ////    if (ViewState[sortExprViewStateKey] != null)
    ////    {
    ////        sortExpr = ViewState[sortExprViewStateKey].ToString();
    ////        if (ViewState[sortDirViewStateKey] != null)
    ////            sortDir = (SortDirection)ViewState[sortDirViewStateKey];
    ////        dataView.Sort = String.Format("{0} {1}", sortExpr,
    ////            ConvertSortDirectionToSql(sortDir));
    ////    }
    ////    grid.DataSource = dataView;
    ////    grid.DataBind();
    ////}
    ////private string ConvertSortDirectionToSql(SortDirection sortDirection)
    ////{
    ////    string newSortDirection = String.Empty;

    ////    switch (sortDirection)
    ////    {
    ////        case SortDirection.Ascending:
    ////            newSortDirection = "ASC";
    ////            break;

    ////        case SortDirection.Descending:
    ////            newSortDirection = "DESC";
    ////            break;
    ////    }

    ////    return newSortDirection;
    ////}
    /********************/

    /*************** sorting ******************/
    protected void grdRikuzim_Sorting(object sender, GridViewSortEventArgs e)
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

           
            ((DataView)Session["Rikuzim"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
            grdRikuzim.DataSource = (DataView)Session["Rikuzim"];
            grdRikuzim.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdRikuzim.Columns)
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

    /******************************************/
}


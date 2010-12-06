using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.BL;
using System.Data;

public partial class Modules_UserControl_ucLogBakashot : System.Web.UI.UserControl
{
    private const int COL_BAKASHA_ID = 1;
    private const int COL_TEUR = 3;
    private const int COL_TARICH_IDKUN = 4;
    private const int COL_TAARICH = 7;
    private const int COL_SHAT_HATCHALA_SIDUR = 9;
    private const int COL_SHAT_PEILUT = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            divNetunim.Visible = false;
            LoadCombo();
            if (Request.QueryString["BakashaId"] != null)
            {
                txtRequestNum.Text = Request.QueryString["BakashaId"];
                divNetunim.Visible = true;
                ViewState["SortDirection"] = SortDirection.Descending;
                ViewState["SortExp"] = "TAARICH_IDKUN_ACHARON";
       
                GetLogBakashot();
                ((DataView)Session["LogBakashot"]).Sort = "TAARICH_IDKUN_ACHARON DESC";
                grdLogRequest.DataSource = (DataView)Session["LogBakashot"];
                grdLogRequest.DataBind();
            }
            else
            {
                ddlMonth.SelectedValue = DateTime.Today.Month.ToString().PadLeft(2,'0') + "/" + DateTime.Today.Year;
            }
        }
    }

    //public string GetPannelGridId
    //{
    //   get { return pnlgrdRequest.ClientID;}
    //}

    private void LoadCombo()
    {
        clUtils oUtils = new clUtils();
        try
        {
            ddlRequestType.DataSource = oUtils.GetCombo(clGeneral.cProGetTypeRequest, "הכל");
            ddlRequestType.DataTextField = "DESCRIPTION";
            ddlRequestType.DataValueField = "CODE";
            ddlRequestType.DataBind();

            ddlMonth.DataSource = oUtils.GetCombo(clGeneral.cProGetMonthsFromRequest, "הכל");
            ddlMonth.DataTextField = "DESCRIPTION";
            ddlMonth.DataValueField = "CODE";
            ddlMonth.DataBind();

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
                divNetunim.Visible = true;
                ViewState["SortDirection"] = SortDirection.Descending;
                ViewState["SortExp"] = "bakasha_id, TAARICH_IDKUN_ACHARON";
                GetLogBakashot();
                ((DataView)Session["LogBakashot"]).Sort = "bakasha_id DESC, TAARICH_IDKUN_ACHARON DESC";
                grdLogRequest.DataSource = (DataView)Session["LogBakashot"];
                grdLogRequest.DataBind();
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }


    private void GetLogBakashot()
    {

        clRequest objRequest = new clRequest();
        DataView dvRequest;
        DataTable dtLogBakashot;
        int iRequestType, iMisparIshi;
        long lRequestNum;

        iRequestType = int.Parse(ddlRequestType.SelectedValue);
        lRequestNum = -1;
        if (clGeneral.IsNumeric(txtRequestNum.Text))
        {
            lRequestNum = long.Parse(txtRequestNum.Text);
        }
        iMisparIshi = 0;
        if (txtMisparIshi.Text.Length > 0)
        { iMisparIshi = int.Parse(txtMisparIshi.Text); }

        dtLogBakashot = objRequest.GetLogBakashot(iRequestType, iMisparIshi, lRequestNum, ddlMonth.SelectedValue,ddlTypeMessage.SelectedValue);

        dvRequest = new DataView(dtLogBakashot);

        Session["LogBakashot"] = dvRequest;


    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdLogRequest.Columns)
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



    protected void grdLogRequest_Sorting(object sender, GridViewSortEventArgs e)
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

            if (Session["LogBakashot"] == null)
            {
                GetLogBakashot();
            }
            ((DataView)Session["LogBakashot"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
            grdLogRequest.DataSource = (DataView)Session["LogBakashot"];
            grdLogRequest.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdLogRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort;
        if (e.Row.RowType == DataControlRowType.Header)
        {
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
            for (i = 0; i <= e.Row.Cells.Count - 1; i++)
            {
                sortHeader = e.Row.Cells[i].Controls[0];
                ((LinkButton)(sortHeader)).Style.Add("color", "white");
                ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
        {
            e.Row.Cells[COL_TAARICH].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_TEUR].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_SHAT_HATCHALA_SIDUR].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_SHAT_PEILUT].Attributes.Add("dir", "ltr");
            e.Row.Cells[COL_TARICH_IDKUN].Attributes.Add("dir", "ltr");
            if (e.Row.Cells[COL_SHAT_HATCHALA_SIDUR].Text == DateTime.MinValue.ToString("dd/MM/yyyy  HH:mm"))
            { e.Row.Cells[COL_SHAT_HATCHALA_SIDUR].Text = ""; }
        }

    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {
            autoComRequestNum.ContextKey = ddlMonth.SelectedValue;
            autoComMisparIshi.ContextKey = ddlMonth.SelectedValue + "|" + (txtRequestNum.Text.Length == 0 ? "0" : txtRequestNum.Text);
        }
    }
    protected void txtRequestNum_TextChanged(object sender, EventArgs e)
    {
        if (txtRequestNum.Text.Length > 0)
        {
            autoComMisparIshi.ContextKey = ddlMonth.SelectedValue + "|" + (txtRequestNum.Text.Length == 0 ? "0" : txtRequestNum.Text);
        }
    }
    protected void grdLogRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLogRequest.PageIndex = e.NewPageIndex;
        grdLogRequest.DataSource = (DataView)Session["LogBakashot"];
        grdLogRequest.DataBind();
    }
}

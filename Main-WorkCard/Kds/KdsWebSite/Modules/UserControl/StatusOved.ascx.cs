using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary.Security;
using KdsLibrary.BL;
using KdsLibrary.Utils;
using KdsLibrary;
using System.Data;

public partial class Modules_UserControl_StatusOved : System.Web.UI.UserControl
{
    protected string sElement;
    protected int iEmployeeId;
    protected string sMonth;

    protected void Page_Load(object sender, EventArgs e)
    {
             
    }

    public int EmployeeId
    {
        set
        {
            iEmployeeId = value;
        }
    }

    public string MonthToShow
    {
        set
        {
            sMonth = value;
        }
    }

     public string ElementId
    {
        set
        {
            sElement = value;
        }
    }

    public bool VisibleDivNetunim
    {
        set
        {
            divNetunim.Visible = value;
        }
    }

    public void ShowData()
    {
        clOvdim oOvdim = new clOvdim();
        DataTable dtPirteyOved;
        DateTime dTaarich;
      
        try
        {
            ViewState["Month"] = sMonth;
            ViewState["EmployeeId"] = iEmployeeId;
            ViewState["Element"] = sElement;
              if (sMonth.Length > 0 && iEmployeeId>0)
            {
                dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, sMonth);
            dtPirteyOved = oOvdim.GetPirteyOved(iEmployeeId, dTaarich);
            if (dtPirteyOved.Rows.Count > 0)
            {
                divNetunim.Visible = true;
                lblEmployeId.Text = dtPirteyOved.Rows[0]["MISPAR_ISHI"].ToString();
                lblFirstName.Text = dtPirteyOved.Rows[0]["SHEM_PRAT"].ToString();
                lblLastName.Text = dtPirteyOved.Rows[0]["SHEM_MISH"].ToString();
                lblEzor.Text = dtPirteyOved.Rows[0]["ezor"].ToString();
                lblGil.Text = dtPirteyOved.Rows[0]["gil"].ToString();
                lblIsuk.Text = dtPirteyOved.Rows[0]["isuk"].ToString();
                if (!string.IsNullOrEmpty(dtPirteyOved.Rows[0]["tchilat_avoda"].ToString()))
                {
                    lblDateStartWork.Text = ((DateTime)dtPirteyOved.Rows[0]["tchilat_avoda"]).ToShortDateString();
                }
                lblSnif.Text = dtPirteyOved.Rows[0]["snif_av"].ToString();
                lblMaamad.Text = dtPirteyOved.Rows[0]["maamad"].ToString();
                   
                ViewState["SortDirection"] = SortDirection.Descending;
                ViewState["SortExp"] = "TAARICH_HATCHALA";

                GetStatusOved();
            }
            }
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
            foreach (DataControlField dc in grdStatus.Columns)
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

    private void GetStatusOved()
    {
        DataTable dtStatus;
        clOvdim oOvdim = new clOvdim();
        DataView dvStatus;
        try
        {

            dtStatus = oOvdim.GetStatusToOved(iEmployeeId);
            dvStatus = new DataView(dtStatus);
            dvStatus.Sort = "TAARICH_HATCHALA DESC";

            grdStatus.DataSource = dvStatus;
            grdStatus.DataBind();
            Session["Status"] = dvStatus;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void grdStatus_Sorting(object sender, GridViewSortEventArgs e)
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

            if (Session["Status"] != null)
            {
                ((DataView)Session["Status"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
                grdStatus.DataSource = (DataView)Session["Status"];
                grdStatus.DataBind();
            }
            else
            {
                GetStatusOved();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort;
        try
        {
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

                for (i = 0; i < e.Row.Cells.Count; i++)
                {
                    sortHeader = e.Row.Cells[i].Controls[0];
                    ((LinkButton)(sortHeader)).Style.Add("color", "white");
                    ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");
                }
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
}

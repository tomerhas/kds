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

public partial class Modules_UserControl_PirteyOved : System.Web.UI.UserControl
{
    protected string sElement;
    protected int iEmployeeId;
    protected string sMonth;
    protected KdsSecurityLevel iSecurityPage;
    public const int COL_CODE = 0;
    public const int COL_TEUR = 1;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {


        }
        catch (Exception ex)
        {
            throw ex;
        }
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

    public KdsSecurityLevel SecurityPage
    {
        set
        {
            iSecurityPage = value;
        }
    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdPirteyOved.Columns)
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

    public void ShowData()
    {
      
        DateTime dTaarich;
        string[] arrDate;
        try
        {
            SetPirteyHitkashrut();
            ViewState["Month"] = sMonth;
            ViewState["EmployeeId"] = iEmployeeId;
            ViewState["Element"] = sElement;
            if (sMonth.Length > 0 && iEmployeeId > 0)
            {
                arrDate= sMonth.Split(char.Parse("/"));
                dTaarich = new DateTime(int.Parse(arrDate[1].ToString()), int.Parse(arrDate[0].ToString()), 1).AddMonths(1).AddDays(-1);
               divNetunim.Visible = true;

                ViewState["SortDirection"] = SortDirection.Ascending;
                ViewState["SortExp"] = "kod_natun";

                GetPirteyOved(dTaarich);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetPirteyOved(DateTime dTaarich)
    {  
          clOvdim oOvdim = new clOvdim();
        DataTable dtPirteyOved;
        DataView dvPirteyOved;
      
        try
        {
           
            dtPirteyOved = oOvdim.GetPirteyOvedAll(iEmployeeId, dTaarich);

            dvPirteyOved = new DataView(dtPirteyOved);

            dvPirteyOved.Sort = "KOD_NATUN ASC";

            txtTeurNatun.Text = "";
            txtCodeNatun.Text = "";
            grdHistoriatNatun.DataBind();
            if (dtPirteyOved.Rows.Count == 0)
                TitlePanel.Style["display"] = "none";
            else
                TitlePanel.Style["display"] = "block";
            grdPirteyOved.DataSource = dtPirteyOved;
            grdPirteyOved.DataBind();
            
            Session["PirteyOved"] = dvPirteyOved;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void grdPirteyOved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!(iSecurityPage == KdsSecurityLevel.ViewAll) && (e.Row.Cells[COL_CODE].Text == "11" || e.Row.Cells[COL_CODE].Text == "12"))
                 {
                    e.Row.Style.Add("display","none");
                }

                e.Row.Attributes.Add("OnClick", "javascript:CheckChangedDetails(" + (e.Row.RowIndex) + ");");
                e.Row.Style.Add("cursor", "pointer");

                if (e.Row.RowIndex == 0)
                {
                    e.Row.CssClass = "SelectedGridRow";
                    txtTeurNatun.Text = e.Row.Cells[1].Text;
                    txtCodeNatun.Text = e.Row.Cells[COL_CODE].Text;

                    GetHistory();
                }
                
            }

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


    private void SetPirteyHitkashrut()
    {
        clOvdim oOvdim = new clOvdim();
        DataTable dtPirteyHitkashrut;
        try
        {
            dtPirteyHitkashrut = oOvdim.GetPirteyHitkashrut(iEmployeeId);
            if (dtPirteyHitkashrut.Rows.Count > 0)
            {
                lblAddress.Text = dtPirteyHitkashrut.Rows[0]["KTOVET"].ToString();
                lblTelAvoda.Text = dtPirteyHitkashrut.Rows[0]["TELEFON_AVODA"].ToString();
                lblTelHome.Text = dtPirteyHitkashrut.Rows[0]["TELEFON_BAIT"].ToString();
                lblPhone.Text = dtPirteyHitkashrut.Rows[0]["TELEFON_NAID"].ToString();
                lblEmail.Text = dtPirteyHitkashrut.Rows[0]["EMAIL"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }


    protected void grdPirteyOved_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;
        DateTime dTaarich;
       
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

            if (Session["PirteyOved"] != null)
            {
                ((DataView)Session["PirteyOved"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
                grdPirteyOved.DataSource = (DataView)Session["PirteyOved"];
                grdPirteyOved.DataBind();
            }
            else
            {
                dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, ViewState["Month"].ToString());
                GetPirteyOved(dTaarich);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtCodeNatun_TextChanged(object sender, EventArgs e)
    {
        if (clGeneral.IsNumeric(txtCodeNatun.Text))
        {
            txtTeurNatun.Text = GetTeurNatun(int.Parse(txtCodeNatun.Text));
            GetHistory();
        }
    }

    protected void txtTeurNatun_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtTeurNatun.Text.Length > 0)
            {
                txtCodeNatun.Text = GetCodeNatun(txtTeurNatun.Text);
                GetHistory();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnBindHistory_Click(object sender, EventArgs e)
    {
        try
        {
            int iRowId;
            string sScript;

            iRowId = int.Parse(txtRowSelected.Value.ToString());

            for (int i = 0; i < grdPirteyOved.Rows.Count; i++)
            {
                grdPirteyOved.Rows[i].CssClass = "GridAltRow";
            }

            grdPirteyOved.Rows[iRowId].CssClass = "SelectedGridRow";
            txtCodeNatun.Text = grdPirteyOved.Rows[iRowId].Cells[COL_CODE].Text;
            txtTeurNatun.Text = grdPirteyOved.Rows[iRowId].Cells[COL_TEUR].Text;

            sScript = "document.getElementById('" + ViewState["Element"] + "_pnlContainerDetails').scrollTop = document.getElementById('" + ViewState["Element"] + "_grdPirteyOved').firstChild.childNodes(" + (iRowId + 1) + ").offsetTop-24;";

            ScriptManager.RegisterStartupScript(btnBindHistory, this.GetType(), "NatunScroll", sScript, true);

            GetHistory();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetHistory()
    {
        DateTime dTaarich;
        
        try
        {
            DataTable dtHistoryanNatun;
            clOvdim oOvdim = new clOvdim();
            if (clGeneral.IsNumeric(txtCodeNatun.Text))
            {
                dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, ViewState["Month"].ToString());
                dtHistoryanNatun = oOvdim.GetHostoriatNatunLeOved(int.Parse(ViewState["EmployeeId"].ToString()), dTaarich, int.Parse(txtCodeNatun.Text));
                grdHistoriatNatun.DataSource = dtHistoryanNatun;
                grdHistoriatNatun.DataBind();
            }
            else { ScriptManager.RegisterStartupScript(txtCodeNatun, this.GetType(), "errNatun", "alert('!קוד נתון לא חוקי');", true); }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private string GetTeurNatun(int iCodeNatun)
    {
        string sTeur = "";
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drNetunim;

        try
        {


            dt = oUtils.GetCombo(clGeneral.cProGetKodNatun, "");

            drNetunim = dt.Select("KOD_NATUN='" + iCodeNatun + "'", "KOD_NATUN asc");

            if (drNetunim.Length > 0)
            {
                sTeur = drNetunim[0]["TEUR_NATUN"].ToString();
            }

            return sTeur;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    private string GetCodeNatun(string sTeurNatun)
    {
        string sCode = "";
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drNetunim;

        try
        {


            dt = oUtils.GetCombo(clGeneral.cProGetKodNatun, "");

            drNetunim = dt.Select("TEUR_NATUN='" + Server.UrlDecode(sTeurNatun) + "'", "TEUR_NATUN asc");

            if (drNetunim.Length > 0)
            {
                sCode = drNetunim[0]["KOD_NATUN"].ToString();
            }

            return sCode;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
}

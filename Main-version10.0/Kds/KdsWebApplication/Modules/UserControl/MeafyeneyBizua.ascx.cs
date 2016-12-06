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

public partial class Modules_UserControl_MeafyeneyBizua : System.Web.UI.UserControl
{
    public const int COL_CODE = 0;
    public const int COL_TEUR = 1;
    public const int COL_ERECH = 4;
    public const int COL_ERECH_HISTORY = 2;
    public const int COL_KOD_INT = 6;
    protected string sElement;
    protected int iEmployeeId;
    protected string sMonth;
    protected bool bBrerotMechdal;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            
              
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
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

    public bool ShowBreratMechdal
    {
        set
        {
            bBrerotMechdal  = value;
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
        DateTime dTaarich,dMeTaarich;
        
        try
        {
            ViewState["Month"] = sMonth;
            ViewState["EmployeeId"] = iEmployeeId;
            ViewState["BrerotMechdal"] = bBrerotMechdal;
            ViewState["Element"] = sElement;
            if (sMonth.Length > 0 && iEmployeeId > 0)
            {
                dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, sMonth);
                dMeTaarich = dTaarich.AddDays(1).AddMonths(-1);
               
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
                   
                    ViewState["SortDirection"] = SortDirection.Ascending;
                    ViewState["SortExp"] = "KOD_MEAFYEN_INT";

                    GetMeafyeneyBitzua(dMeTaarich,dTaarich);

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetMeafyeneyBitzua(DateTime dMeTaarich,DateTime dAdTaarich)
    {
        DataTable dtMeafyeneyBitzua;
        int iBreratMechdal;
        DataView dvMeafyeneyBitzua;
        clOvdim oOvdim = new clOvdim();
       
        try
        {
            if (bBrerotMechdal == true)
            { iBreratMechdal = 1; }
            else { iBreratMechdal = 0; }


            dtMeafyeneyBitzua = oOvdim.GetMeafyeneyBitzuaLeOvedAll(iEmployeeId, dMeTaarich, dAdTaarich, iBreratMechdal);
            dtMeafyeneyBitzua.Columns.Add(new DataColumn("KOD_MEAFYEN_INT", typeof(System.Int32)));
            for (int i = 0; i < dtMeafyeneyBitzua.Rows.Count; i++)
            {
                dtMeafyeneyBitzua.Rows[i]["KOD_MEAFYEN_INT"] = int.Parse(dtMeafyeneyBitzua.Rows[i]["KOD_MEAFYEN"].ToString());
            }
            dvMeafyeneyBitzua = new DataView(dtMeafyeneyBitzua);

            dvMeafyeneyBitzua.Sort = "KOD_MEAFYEN_INT ASC";

            txtTeurMeafyen.Text = "";
            txtCodeMeafyen.Text = "";
            grdHistoriatMeafyen.DataBind();
            if (dtMeafyeneyBitzua.Rows.Count == 0)
                 TitlePanel.Style["display"] = "none"; 
            else
                TitlePanel.Style["display"] = "block";
            grdMeafyeneyBitzua.DataSource = dvMeafyeneyBitzua;
            grdMeafyeneyBitzua.DataBind();
            Session["MeafyeneyBitzua"] = dvMeafyeneyBitzua;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }


    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdMeafyeneyBitzua.Columns)
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

    protected void grdMeafyeneyBitzua_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string sErech;
        int iColSort;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // e.Row.Cells[COL_KOD_INT].Text = 
                if (((DataRowView)e.Row.DataItem).Row["Erech_Brirat_Mechdal"].ToString().Length > 0 && e.Row.Cells[COL_ERECH].Text != ((DataRowView)e.Row.DataItem).Row["Erech_Brirat_Mechdal"].ToString())
                {
                    sErech = "<span style='Display:none'> &nbsp;  &nbsp; " + ((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"];
                    sErech += "<br/>  &nbsp;  &nbsp; " + ((DataRowView)e.Row.DataItem).Row["Erech_Brirat_Mechdal"] + "</span>";
                    e.Row.Cells[COL_ERECH].Text = "<span style='cursor:pointer' onclick='ShowMeafyen(this)' id='OpenMeafyen'> + </span>" + e.Row.Cells[COL_ERECH].Text + "<br>" + sErech;
                }
                else if (((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"].ToString().Length > 0 && e.Row.Cells[COL_ERECH].Text != ((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"].ToString())
                {
                    sErech = "<span style='Display:none'>  &nbsp;   &nbsp; " + ((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"];
                    e.Row.Cells[COL_ERECH].Text = "<span style='cursor:pointer' onClick='ShowMeafyen(this)' id='OpenMeafyen'> + </span>" + e.Row.Cells[COL_ERECH].Text + "<br>" + sErech;
                }

                e.Row.Attributes.Add("OnClick", "javascript:CheckChanged(" + (e.Row.RowIndex) + ");");
                e.Row.Style.Add("cursor", "pointer");

                if (e.Row.RowIndex == 0)
                {
                    e.Row.CssClass = "SelectedGridRow";
                    txtTeurMeafyen.Text = e.Row.Cells[1].Text;
                    txtCodeMeafyen.Text = e.Row.Cells[0].Text;

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





    protected void grdMeafyeneyBitzua_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;
        DateTime dTaarich, dMeTaarich;
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

            if (Session["MeafyeneyBitzua"] != null)
            {
                ((DataView)Session["MeafyeneyBitzua"]).Sort = string.Concat(e.SortExpression, " ", sDirection);
                grdMeafyeneyBitzua.DataSource = (DataView)Session["MeafyeneyBitzua"];
                grdMeafyeneyBitzua.DataBind();
            }
            else {
               dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, ViewState["Month"].ToString());
               dMeTaarich = dTaarich.AddDays(1).AddMonths(-1);

               GetMeafyeneyBitzua(dMeTaarich,dTaarich);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnBindHistory_Click(object sender, EventArgs e)
    {
        int iRowId;
        string sScript;

        try
        {
            iRowId = int.Parse(txtRowSelected.Value.ToString());

            for (int i = 0; i < grdMeafyeneyBitzua.Rows.Count; i++)
            {
                grdMeafyeneyBitzua.Rows[i].CssClass = "GridAltRow";
            }

            grdMeafyeneyBitzua.Rows[iRowId].CssClass = "SelectedGridRow";
            txtCodeMeafyen.Text = grdMeafyeneyBitzua.Rows[iRowId].Cells[COL_CODE].Text;
            txtTeurMeafyen.Text = Server.HtmlDecode(grdMeafyeneyBitzua.Rows[iRowId].Cells[COL_TEUR].Text);

            //   sScript = "document.getElementById('" + ViewState["Element"] + "_pnlContainer').scrollTop = document.getElementById('" + ViewState["Element"] + "_grdMeafyeneyBitzua').firstChild.childNodes(" + (iRowId + 1) + ").offsetTop-24;";
            sScript = "document.getElementById('" + ViewState["Element"] + "_pnlContainer').scrollTop = document.getElementById('" + ViewState["Element"] + "_grdMeafyeneyBitzua').childNodes['1'].childNodes['" + (iRowId + 1) + "'].offsetTop-24;";
            ScriptManager.RegisterStartupScript(btnBindHistory, this.GetType(), "Scroll", sScript, true);
            GetHistory();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private void GetHistory()
    {
        DateTime dTaarich;
       
        try
            {
            DataTable dtMeafyeneyBitzua;
            clOvdim oOvdim = new clOvdim();
            if (clGeneral.IsNumeric(txtCodeMeafyen.Text))
            {
                dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, ViewState["Month"].ToString());
                dTaarich = dTaarich.AddDays(1).AddMonths(-1);
                dtMeafyeneyBitzua = oOvdim.GetHostoriatMeafyeneLeOved(int.Parse(ViewState["EmployeeId"].ToString()), dTaarich, int.Parse(txtCodeMeafyen.Text));
                grdHistoriatMeafyen.DataSource = dtMeafyeneyBitzua;
                grdHistoriatMeafyen.DataBind();
                }
            else{ScriptManager.RegisterStartupScript(txtCodeMeafyen, this.GetType(), "errName", "alert('!קוד מאפיין לא חוקי');", true); }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdHistoriatMeafyen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string sErech;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataRowView)e.Row.DataItem).Row["Erech_Brirat_Mechdal"].ToString().Length > 0 && e.Row.Cells[COL_ERECH_HISTORY].Text != ((DataRowView)e.Row.DataItem).Row["Erech_Brirat_Mechdal"].ToString())
                {
                    sErech = "<span style='Display:none'> &nbsp;  &nbsp; " + ((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"];
                    sErech += "<br/>  &nbsp;  &nbsp; " + ((DataRowView)e.Row.DataItem).Row["Erech_Brirat_Mechdal"] + "</span>";
                    e.Row.Cells[COL_ERECH_HISTORY].Text = "<span style='cursor:pointer' onClick='ShowMeafyen(this)' id='OpenMeafyen'> + </span>" + e.Row.Cells[COL_ERECH_HISTORY].Text + "<br>" + sErech;
                }
                else if (((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"].ToString().Length > 0 && e.Row.Cells[COL_ERECH_HISTORY].Text != ((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"].ToString())
                {
                    sErech = "<span style='Display:none'>  &nbsp;   &nbsp; " + ((DataRowView)e.Row.DataItem).Row["Erech_Mechdal_partany"];
                    e.Row.Cells[COL_ERECH_HISTORY].Text = "<span style='cursor:pointer' onClick='ShowMeafyen(this)' id='OpenMeafyen'> + </span>" + e.Row.Cells[COL_ERECH_HISTORY].Text + "<br>" + sErech;
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void txtCodeMeafyen_TextChanged(object sender, EventArgs e)
    {
        if (clGeneral.IsNumeric(txtCodeMeafyen.Text))
        {
            txtTeurMeafyen.Text=GetTeurMeafyen(int.Parse(txtCodeMeafyen.Text));
            GetHistory();
        }
    }

    protected void txtTeurMeafyen_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtTeurMeafyen.Text.Length > 0)
            {
                txtCodeMeafyen.Text=GetCodeMeafyen(txtTeurMeafyen.Text);
                GetHistory();
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private string GetTeurMeafyen(int iCodeMeafyen)
    {
        string sTeur = "";
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drMeafyenim;

        try
        {


            dt = oUtils.GetCombo(clGeneral.cProGetMeafyeneyBitsua, "");

            drMeafyenim = dt.Select("KOD_MEAFYEN_BITZUA='" + iCodeMeafyen + "'");

            if (drMeafyenim.Length > 0)
            {
                sTeur = drMeafyenim[0]["TEUR_MEAFYEN_BITZUA"].ToString();
            }

            return sTeur;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetCodeMeafyen(string sTeurMeafyen)
    {
        string sCode = "";
        DataTable dt;
        clUtils oUtils = new clUtils();
        DataRow[] drMeafyenim;

        try
        {


            dt = oUtils.GetCombo(clGeneral.cProGetMeafyeneyBitsua, "");

            drMeafyenim = dt.Select("TEUR_MEAFYEN_BITZUA='" + Server.UrlDecode(sTeurMeafyen) + "'");

            if (drMeafyenim.Length > 0)
            {
                sCode = drMeafyenim[0]["KOD_MEAFYEN_BITZUA"].ToString();
            }

            return sCode;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

}

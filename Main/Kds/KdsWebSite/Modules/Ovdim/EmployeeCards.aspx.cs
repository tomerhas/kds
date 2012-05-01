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
using KdsLibrary.UI;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.Security;
using KdsLibrary.Utils.Reports;
using System.Collections.Generic;
using KdsLibrary.UI.SystemManager;

public partial class Modules_Ovdim_EmployeeCards :KdsPage
{
    private const int Coll_lelo_divuach = 2;
    private const int Coll_status = 1;
    private string[] arrParams;
  
    protected override void CreateUser()
    {
        if (!Page.IsPostBack && Request.QueryString["EmpId"] == null)
            Session["arrParams"] = null;
     
        if (Request.QueryString["Key"] != null && Session["arrParams"] == null)
        {
            DriverStation.WSkds wsDriverStation = new DriverStation.WSkds();

            try
            {
                wsDriverStation.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                arrParams = wsDriverStation.getZihuiUser(Request.QueryString["Key"].ToString());
                Session["arrParams"] = arrParams;
                SetUserKiosk(int.Parse(arrParams[0].ToString()));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wsDriverStation.Dispose();
            }

        }
        else if (Session["arrParams"] != null && Request.QueryString["Key"] != null)
        {
            arrParams = (string[])Session["arrParams"];
            SetUserKiosk(int.Parse(arrParams[0].ToString()));
        }
        else if (Request.QueryString["EmpId"] != null && Session["arrParams"] != null)
        {
            arrParams = (string[])Session["arrParams"];
            txtId.Text = Request.QueryString["EmpId"].ToString();
            SetUserKiosk(int.Parse(Request.QueryString["EmpId"].ToString()));
        }
        else
        {
            Session["arrParams"] = null;
            base.CreateUser(); }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grdEmployee.RowCreated += new GridViewRowEventHandler(grdEmployee_RowCreated);
   }
    private void SetUserKiosk(int iMisparIshiKiosk)
    {

        LoginUser = LoginUser.GetLimitedUser(iMisparIshiKiosk.ToString());
        LoginUser.InjectEmployeeNumber(iMisparIshiKiosk.ToString());
        MasterPage mp = (MasterPage)Page.Master;
        mp.DisabledMenuAndToolBar = true;
        DisplayDivMessages = false;
        grdEmployee.PageSize = 13;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtParametrim = new DataTable();
        clUtils oUtils = new clUtils();
        MasterPage mp = (MasterPage)Page.Master;
        try
        {
            
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "רשימת כרטיסי עבודה לעובד";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
                SetFixedHeaderGrid(divNetunim.ClientID, mp.HeadPage);
                txtId.Focus();
                txtId.Attributes.Add("onfocus", "document.getElementById('" + txtId.ClientID + "').select();");
                txtName.Attributes.Add("onfocus", "document.getElementById('" + txtName.ClientID + "').select();");
                txtPageIndex.Value = "0";
             //   btnHidden.Style.Add("Display", "None");
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                clGeneral.LoadDateCombo(ddlMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
               
                SetDefault();
                hidFromEmda.Value = (LoginUser.IsLimitedUser && arrParams[2].ToString() == "1").ToString();
                  if (!bool.Parse(hidFromEmda.Value))
                {
                    divNetunim.Style.Add("overflow-y", "hidden");
                //    divNetunim.Style["overflow-y"] = "hidden";
                    grdEmployee.AllowPaging = false;
                     divNetunim.Style["height"] = "250px";
                }
                else
                {
                    divNetunim.Style["overflow-y"] = "hidden";
                    grdEmployee.PageSize = 8;
                    grdEmployee.AllowPaging = true;
                    txtId.Enabled = false;
                }
               
                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                if (iSecurity == KdsSecurityLevel.ViewAll) //|| iSecurity == KdsSecurityLevel.ViewOnlyEmployeeData)
                {
                    AutoCompleteExtenderByName.ContextKey = "";
                    AutoCompleteExtenderID.ContextKey = "";
                }
               else if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
                {
                    AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                    AutoCompleteExtenderByName.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                }
                else
                {
                    AutoCompleteExtenderByName.ContextKey = ""; // LoginUser.UserInfo.EmployeeNumber;
                    AutoCompleteExtenderID.ContextKey = "";// LoginUser.UserInfo.EmployeeNumber;
                    txtId.Enabled = false;
                    rdoId.Enabled = false;
                    rdoName.Enabled = false;
                }

                txtId.Text = LoginUser.UserInfo.EmployeeNumber;
                clOvdim oOvdim = new clOvdim();
                txtName.Text = oOvdim.GetOvedFullName(clGeneral.GetIntegerValue(LoginUser.UserInfo.EmployeeNumber));
                if (txtId.Text != "")
                    txtSnifUnit.Text = SetSnifName(int.Parse(txtId.Text));

                if (LoginUser.IsLimitedUser)
                {
                    if (Request.QueryString["WCardDate"] != null)
                    {
                        ddlMonth.SelectedValue = Request.QueryString["WCardDate"].ToString().Substring(3, 7);
                    }
                    btnExecute_Click(this, new EventArgs());
                }
            }
            checkHaveHarshaa();
        }
        catch(Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    private bool checkHaveHarshaa()
    {
        bool flag = true;
        if (txtId.Text.Length > 0)
        {
            btnExecute.ControlStyle.CssClass = "ImgButtonSearch";
            btnExecute.Enabled = true;
            if (AutoCompleteExtenderID.ContextKey.Length > 0)
            {
                txtName.Text = SetEmployeeName();
                if (txtName.Text != string.Empty)
                {
                    txtSnifUnit.Text = SetSnifName(int.Parse(txtId.Text));
                }
                else flag = false;
            }
        }
        else
        {
            btnExecute.ControlStyle.CssClass = "ImgButtonSearchDisable";
            btnExecute.Enabled = false;
            flag = false;
        }
        return flag;
    }
    private string SetSnifName(int iMisparIshi)
    {
        string sSnif="";
        string sUnit="";
        string sName = "";
        clOvdim oOvdim = new clOvdim();

        try
        {
           oOvdim.GetOvedSnifAndUnit(iMisparIshi, ref sSnif, ref sUnit);
           if (sSnif != string.Empty)
           {
               sName = string.Concat(sSnif, "/", sUnit);
           }
            return sName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string SetEmployeeName()
    {
        string sOvedName = "";
        DataTable dt;
        clOvdim oOvdim = new clOvdim();
        try
        {
            dt = oOvdim.GetOvdimToUser(txtId.Text, int.Parse(AutoCompleteExtenderID.ContextKey));
            if (dt.Rows.Count > 0)
            {
                sOvedName = dt.Rows[0]["OVED_NAME"].ToString();
            }
            return sOvedName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetDefault()
    {
        rdoMonth.Checked = true;
        ViewState["SortDirection"] = SortDirection.Descending;
        ViewState["SortExp"] = "taarich";
        txtId.Attributes.Add("onkeyup", "checkID();");
        rdoId.Attributes.Add("onclick", "SetTextBox();");
        rdoName.Attributes.Add("onclick", "SetTextBox();");
        txtName.Enabled = false;
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        DataView dv;
        try
        {
            if (checkHaveHarshaa()) // && txtId.Text.Trim() != "" && txtId.Text.Trim() != "0")
            {
                dv = ShowCards(ViewState["SortExp"].ToString(), ViewState["SortDirection"].ToString());//taarich","asc");
                grdEmployee.PageIndex = int.Parse(txtPageIndex.Value);

                grdEmployee.DataSource = dv;
                grdEmployee.DataBind();
                if (!bool.Parse(hidFromEmda.Value) && grdEmployee.Rows.Count > (grdEmployee.PageSize + 1))
                    divNetunim.Style["overflow-y"] = "scroll";
                else divNetunim.Style["overflow-y"] = "hidden";
                grdEmployee.Focus();
                divNetunim.Visible = true;
            }
            else divNetunim.Visible = false;
            if (txtId.Enabled == true || txtName.Enabled == true)
            {
                if (rdoId.Checked)
                {
                    txtName.Enabled = false;
                    txtId.Enabled = true;
                }
                else
                {
                    txtName.Enabled = true;
                    txtId.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnShowApproval_Click(object sender, EventArgs e)
    {
         string[] arrDate;
        DateTime dDateStart;
        try {
            arrDate = ddlMonth.Items[ddlMonth.Items.Count-1].Value.Split(char.Parse("/"));
            dDateStart = new DateTime(int.Parse(arrDate[1].ToString()), int.Parse(arrDate[0].ToString()), 1);
               
            Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
            ReportParameters.Add("P_MISPAR_ISHI", txtId.Text.ToString());
            ReportParameters.Add("P_KOD_ISHUR", "-1");
            ReportParameters.Add("P_STATUS","-1");
            ReportParameters.Add("P_STARTDATE", dDateStart.ToShortDateString());
            ReportParameters.Add("P_ENDDATE", DateTime.Now.ToShortDateString());
            ReportParameters.Add("P_FACTORCONFIRM", "-1");
            OpenReport(ReportParameters, (Button)sender, ReportName.IshurimLerashemet.ToString());
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void OpenReport(Dictionary<string, string> ReportParameters, Button btnScript, string sRdlName)
    {
        KdsReport _Report;
        KdsDynamicReport _KdsDynamicReport;

        _KdsDynamicReport = KdsDynamicReport.GetKdsReport();
        _Report = new KdsReport();
        _Report = _KdsDynamicReport.FindReport(sRdlName);
        Session["Report"] = _Report;

        Session["ReportParameters"] = ReportParameters;

        string sScript = "window.showModalDialog('" + this.PureUrlRoot + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "','','dialogwidth:850px;dialogheight:745px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
        ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "ReportViewer", sScript, true);

    }

    private DataView ShowCards(string sSortExp, string sSortDirection)
    {
        clOvdim oOvdim = new clOvdim();
        DataTable dt;
        DataView dv;
        if (txtId.Text.Length > 0)
        {
            int iMisparIshi = int.Parse(txtId.Text);
            string sMonth = ddlMonth.SelectedValue;

            try
            {
                if (rdoMonth.Checked)
                {
                    //כרטיסים של עובד לחודש נתון
                    dt = oOvdim.GetOvedCards(iMisparIshi, sMonth);
                }
                else
                {
                    //כרטיסים של עובד שעדיין לא הסתיים הטיפול בהם
                    dt = oOvdim.GetOvedCardsInTipul(iMisparIshi);
                }
                dv = new DataView(dt);

                if (ViewState["SortExp"].ToString() == "taarich")
                    ViewState["SortExp"] = "sDate";
             
                if (sSortDirection == SortDirection.Descending.ToString())
                    sSortDirection = "DESC";
                else
                    sSortDirection = "ASC";

                dv.Sort = string.Concat(sSortExp, " ", sSortDirection);
                Session["OvedCards"] = dv;
                return dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else { return null; }
       
    }

    protected void grdEmployee_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;
        if (e.SortExpression == "taarich")
            ViewState["SortExp"] = "taarich";
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
            sDirection = "DESC";
            ViewState["SortDirection"] = SortDirection.Descending;
        }
        if (e.SortExpression == "taarich")
            ViewState["SortExp"] = "sDate";
        else
            ViewState["SortExp"] = e.SortExpression;

        if (e.SortExpression.Length > 0)
        {
            ((DataView)Session["OvedCards"]).Sort = string.Concat(ViewState["SortExp"], " ", sDirection);
        }
        
        grdEmployee.DataSource = (DataView)Session["OvedCards"];         
        grdEmployee.DataBind();
    }

    protected void grdEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        clOvdim _Ovdim = new clOvdim();
        DataTable dt;
        try
        {
            int iColSort = 0;
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
                    ImageSort.ID = "imgDAscSort";
                    ImageSort.ImageUrl = "../../Images/AscSort.gif"; 
                    e.Row.Cells[iColSort].Controls.Add(ImageSort);
                }

                object sortHeader = null;
                int i;
                for (i = 0; i < e.Row.Cells.Count; i++)
                {
                    sortHeader = e.Row.Cells[i].Controls[0];
                    ((LinkButton)(sortHeader)).Style.Add("color", "white");
                    ((LinkButton)(sortHeader)).Style.Add("text-decoration", "none");

                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (LoginUser.IsLimitedUser)
                {
                    ((HyperLink)e.Row.Cells[0].Controls[0]).NavigateUrl= "WorkCard.aspx?EmpID=" + txtId.Text + "&WCardDate=" + DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "sDate").ToString()).ToShortDateString() + "&Page=1&dt=" + DateTime.Now;
                    ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:document.getElementById('divHourglass').style.display = 'block';"));
              
                }
                else
                {
                    ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenEmpWorkCard('", DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "sDate").ToString()).ToShortDateString(), "')"));
                }

                if (e.Row.Cells[Coll_lelo_divuach].Text == "+" )
                {
                    DateTime dTaarich = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "sDate").ToString());
                    e.Row.Cells[Coll_lelo_divuach].Text = "אין פעילות";
                    //if (bool.Parse(hidFromEmda.Value))
                    //    if (DateTime.Now >= dTaarich && DateTime.Now < dTaarich.AddDays(3))
                    //    {
                    //        ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("onclick", "");
                    //        ((HyperLink)e.Row.Cells[0].Controls[0]).Font.Bold = true;
                    //        ((HyperLink)e.Row.Cells[0].Controls[0]).Enabled = false; 
                    //    }
                }
                ////dt = _Ovdim.GetLastUpdate(int.Parse(txtId.Text), DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "sDate").ToString()));
                //////אם קיימות שגיאות וגם מישהו אחר עדכן את הכרטיס אז אין להציג עדכן
                ////if (dt.Rows.Count > 0 && e.Row.Cells[Coll_status].Text == "עדכן")// && e.Row.Cells[Coll_status].Text != "&nbsp;")
                ////{
                ////    e.Row.Cells[Coll_status].Text = "";
                ////}
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //*******Pager Functions*********/
    void grdEmployee_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            IGridViewPager gridPager = e.Row.FindControl("ucGridPager")
            as IGridViewPager;
            if (gridPager != null)
            {
                gridPager.PageIndexChanged += delegate(object pagerSender,
                    GridViewPageEventArgs pagerArgs)
                {
                   // if (int.Parse(Session["PageNum"].ToString()) != pagerArgs.NewPageIndex)
                  //  {
                    txtPageIndex.Value = pagerArgs.NewPageIndex.ToString();
                    ChangeGridPage(int.Parse(txtPageIndex.Value), grdEmployee,
                           (DataView)Session["OvedCards"], "SortDirection",
                           "SortExp");
                       
                  //  }
                  //  else ChangeGridPage(int.Parse(Session["PageNum"].ToString()), grdEmployee,
                  //        (DataView)Session["OvedCards"], "SortDirection",
                  //        "SortExp");
                };
            }
        }
    }
    private void ChangeGridPage(int pageIndex, GridView grid, DataView dataView,
                                string sortDirViewStateKey, string sortExprViewStateKey)
    {
     //   SetChangesOfGridInDataview(grid, ref dataView);
        grid.PageIndex = pageIndex;
        string sortExpr = String.Empty;
        SortDirection sortDir = SortDirection.Ascending;
        if (ViewState[sortExprViewStateKey] != null)
        {
            sortExpr = ViewState[sortExprViewStateKey].ToString();
            if (ViewState[sortDirViewStateKey] != null)
                sortDir = (SortDirection)ViewState[sortDirViewStateKey];
            dataView.Sort = String.Format("{0} {1}", sortExpr,
                ConvertSortDirectionToSql(sortDir));
        }
        grid.DataSource = dataView;
        grid.DataBind();
        
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }
    /********************/
    protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployee.PageIndex = e.NewPageIndex;
        txtPageIndex.Value = e.NewPageIndex.ToString();
        grdEmployee.DataSource = (DataView)Session["OvedCards"];
        grdEmployee.DataBind();
    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];

        if (sSortExp == "sDate")
            sSortExp = "taarich";
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdEmployee.Columns)
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


    
}

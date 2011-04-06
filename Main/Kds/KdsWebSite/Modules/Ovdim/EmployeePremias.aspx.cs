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
using KdsLibrary.UI.SystemManager;
using System.Collections.Generic;
using KdsLibrary;
using System.Diagnostics;
using KdsLibrary.BL;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Modules_Ovdim_EmployeePremias : KdsPage
{
    #region Fields

    private const string SAVED_PREMIAS = "Premias";
    private const string SAVED_PREMIAS_ERRORS = "PremiaErrors";
    private bool _refreshOnLoad;
    private const int Coll_Date = 6;

    #endregion

    #region Events
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ddStatuses.SelectedIndexChanged += 
            new EventHandler(ddStatuses_SelectedIndexChanged);
        ddMonths.SelectedIndexChanged +=
           new EventHandler(ddMonths_SelectedIndexChanged);
        btnExecute.Click += new EventHandler(btnExecute_Click);
        btnSearch.Click += new EventHandler(btnSearch_Click);
        grdPremias.SelectedIndexChanged += 
            new EventHandler(grdPremias_SelectedIndexChanged);
        grdPremias.RowDataBound += 
            new GridViewRowEventHandler(grdPremias_RowDataBound);
        grdPremias.PageIndexChanging += 
            new GridViewPageEventHandler(grdPremias_PageIndexChanging);        
        grdPremias.RowCreated += 
            new GridViewRowEventHandler(grdPremias_RowCreated);    
         btnUpdateGrid.Click += new EventHandler(btnUpdateGrid_Click);
        //btnUpdateGrid.Click += new EventHandler(btnUpdate_Click);
 
    }

    void grdPremias_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (grdPremias.SelectedIndex != -1)
        {
            txtPremiaMinutes.Enabled = true;
            btnUpdate.Attributes.Add("disabled", "false");
        }
        else
        {
            txtPremiaMinutes.Enabled = false;
            btnUpdate.Attributes.Add("disabled","true");
        }
    }

    //void grdPremias_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //        IGridViewPager gridPager = e.Row.FindControl("ucGridPager")
    //        as IGridViewPager;
    //        if (gridPager != null)
    //        {
    //            gridPager.PageIndexChanged += delegate(object pagerSender,
    //                GridViewPageEventArgs pagerArgs)
    //            {
    //                RememberPremiaValues();
    //                ChangeGridPage(pagerArgs.NewPageIndex);
    //            };
    //        }
    //    }
    //}

   
    void btnUpdateGrid_Click(object sender, EventArgs e)
    {
        DataRow[] drSelect;
        string sSQL = "";
        string dakot_old;
        Session[SAVED_PREMIAS_ERRORS] = null;
        DataTable dt = (DataTable)Session[SAVED_PREMIAS];
            grdPremias.Rows.Cast<GridViewRow>()
                           .ToList()
                           .ForEach
                           (
                            row =>
                            {
                                string shem_oved = "";
                                int mispar_ishi = 0;
                                float dakot = 0;

                                shem_oved = row.Cells[1].Text;
                                mispar_ishi = Int32.Parse(row.Cells[0].Text);
                                TextBox txtDakotPremia = 
                                    row.Cells[5].FindControl("txtDakotPremia") 
                                        as TextBox;
                                if (txtDakotPremia != null)
                                {
                                    if (!float.TryParse(txtDakotPremia.Text, out dakot))
                                    {
                                        dakot = -1;
                                    }
                                }

                                sSQL = string.Concat("MISPAR_ISHI='" + mispar_ishi + "'");
                                drSelect = dt.Select(sSQL);
                                dakot_old = drSelect[0]["Dakot_Premya"].ToString() =="0" ?"":drSelect[0]["Dakot_Premya"].ToString();
                                if (dakot_old  != txtDakotPremia.Text)
                                {

                                    int empID = 0;
                                    if (!Int32.TryParse(LoginUser.UserInfo.EmployeeNumber, out empID))
                                    {
                                        empID = -1;
                                    }

                                    if (empID > -1  && !clGeneral.UpdatePremiaForOved(
                                            ddStatuses.SelectedValue, mispar_ishi,
                                            DateTime.Parse("1/" + ddMonths.SelectedItem.Text), dakot == -1 ? 0:dakot, empID
                                          )
                                        )
                                    {
                                        if (Session[SAVED_PREMIAS_ERRORS] == null)
                                        {
                                            Session[SAVED_PREMIAS_ERRORS] =
                                                new List<string>();
                                        }
                                        (Session[SAVED_PREMIAS_ERRORS]
                                            as List<string>)
                                                .Add("There was an error updating premia for oved " + shem_oved);
                                    }
                                }
                            }
                           );
            if (grdPremias.PageCount > 1) ChangeGridPage(0);

            RefreshData();
     
    }
    void grdPremias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RememberPremiaValues();
        ChangeGridPage(e.NewPageIndex);
    }

    private void RememberPremiaValues()
    {
        grdPremias.Rows.Cast<GridViewRow>()
                       .ToList()
                       .ForEach
                        (
                            row =>
                            {                                
                                int mispar_ishi = 0;
                                int dakot = 0;
                                                                
                                mispar_ishi = Int32.Parse(row.Cells[0].Text);
                                TextBox txtDakotPremia = 
                                    row.Cells[5].FindControl("txtDakotPremia") 
                                        as TextBox;
                                if (txtDakotPremia != null)
                                {
                                    if (!Int32.TryParse(txtDakotPremia.Text, out dakot))
                                    {
                                        dakot = 0;
                                    }
                                }

                                DataTable dt = Session[SAVED_PREMIAS] as DataTable;
                                DataRow[] drs = null;

                                try
                                {
                                    drs = 
                                        dt.Select("Mispar_Ishi = " + mispar_ishi);
                                }
                                catch { }

                                if (drs != null)
                                {
                                    drs.ToList()
                                        .ForEach
                                        (
                                            dr =>
                                            {
                                                dr.BeginEdit();
                                                dr["Dakot_Premya"] = dakot;
                                                dr.EndEdit();
                                            }
                                        );
                                    dt.AcceptChanges();
                                    Session[SAVED_PREMIAS] = dt;
                                }
                            }
                           );
    }

    void grdPremias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort = 0;
        DateTime taarichIdkun;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();

            iColSort = GetCurrentColSort();
            lbl.Text = " ";
            e.Row.Cells[iColSort].Controls.Add(lbl);
            if ((SortDirection)ViewState["SortDirection"] == SortDirection.Descending)
            {
                System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                ImageSort.ID = "imgAscSort";
                ImageSort.ImageUrl = "../../Images/DescSort.gif";
                e.Row.Cells[iColSort].Controls.Add(ImageSort);
            }
            else
            {
                System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                ImageSort.ID = "imgDescSort";
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
            TextBox txtDakotPremia = 
                e.Row.FindControl("txtDakotPremia") as TextBox;
            if (txtDakotPremia != null)
            {
                txtDakotPremia.Text = 
                    (e.Row.DataItem as DataRowView)["Dakot_premya"].ToString();
                if (txtDakotPremia.Text == "0")
                {
                    txtDakotPremia.Text = "";
                }
            }
           
            if (e.Row.Cells[Coll_Date].Text != "&nbsp;" && e.Row.Cells[Coll_Date].Text != "")
            {
                taarichIdkun = DateTime.Parse(e.Row.Cells[Coll_Date].Text);
                e.Row.Cells[Coll_Date].Text = taarichIdkun.ToLongTimeString() + " " + taarichIdkun.ToShortDateString();
               
            }
        }
    }

    void btnSearch_Click(object sender, EventArgs e)
    {
        string id =txtId.Text;
        if (ListOvdim.Value.IndexOf(";" + id + ";") > -1)
            SearchOvedInGrid();
        else
           ScriptManager.RegisterStartupScript(btnSearch, btnSearch.GetType(), "err", " alert('מספר אישי לא קיים לפרמיה שנבחרה');", true);
   
    }

    void btnExecute_Click(object sender, EventArgs e)
    {
        ViewState["SortDirection"] = SortDirection.Ascending;
        ViewState["SortExp"] = "Mispar_Ishi";
        RefreshData();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {                   
        KdsLibrary.Exchange.ExchangeInfoServiceSoapClient exchangeSrv = new
                    KdsLibrary.Exchange.ExchangeInfoServiceSoapClient();
        List<string> groupsForUser =
            exchangeSrv.getUserPropertyByUserName(LoginUser.UserInfo.Username,
            "MemberOf").Split("|".ToCharArray()).ToList();

        if (!Debugger.IsAttached && !groupsForUser.Any(group => group.StartsWith("Kds_Premia", StringComparison.CurrentCultureIgnoreCase)))
        {
            //HttpContext.Current.Response.Redirect(String.Format("{0}/{1}",
            //    HttpContext.Current.Request.ApplicationPath,
            //    NotAuthorizedRedirectPage));
            HttpContext.Current.Response.Redirect(String.Format("{0}/{1}",
               "~",
               NotAuthorizedRedirectPage));
        }

        if (!IsPostBack)
        {
            ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
            PageHeader = "פרמיות ידניות";
            BindPremias(groupsForUser);
            BindPremiaDates();
            SetFields();
            Session[SAVED_PREMIAS_ERRORS] = null;
            ViewState["SortDirection"] = SortDirection.Ascending;
            ViewState["SortExp"] = "Mispar_Ishi";
            LoadMessages((DataList)Master.FindControl("lstMessages"));
        }
        Title = PageHeader;
        RegisterBodyOnloadEvent();
        if (_refreshOnLoad) RefreshData();

        //ddStatuses_SelectedIndexChanged(sender, e);
    }

    void ddStatuses_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddMonths.Enabled = ddStatuses.SelectedIndex > 0;
        rdoId.Enabled = ddStatuses.SelectedIndex > 0;
        rdoName.Enabled = ddStatuses.SelectedIndex > 0;
        txtId.Enabled = ddStatuses.SelectedIndex > 0;
        txtName.Enabled = ddStatuses.SelectedIndex > 0;        
        btnUpdateGrid.Enabled = ddStatuses.SelectedIndex > 0;       
      //  btnSearch.Enabled = ddStatuses.SelectedIndex > 0;
        btnExecute.Enabled = ddStatuses.SelectedIndex > 0;
        NikuySadot();
        //if (ddStatuses.SelectedIndex > 0)
        //{
        //    txtPremiaMinutes.Enabled = true;
        //    btnUpdate.Enabled = true;
        //}
        //else
        //{
        //    txtPremiaMinutes.Enabled = false;
        //    btnUpdate.Enabled = false;
        //}
     
    }
    void ddMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        NikuySadot();
    }
    private void NikuySadot()
    {
        divGrdPremyot.Style["display"] = "none";
        txtId.Text = "";
        txtName.Text = "";
        txtPremiaMinutes.Text = "";
        btnSearch.Enabled = false;
        btnUpdate.Attributes.Add("disabled", "true");
       
    }
    protected void vldEmpNotExists_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    #endregion

    #region Methods
        
    private void RegisterBodyOnloadEvent()
    {
        HtmlGenericControl body = null;
        if (Master != null)
            body = (HtmlGenericControl)Page.Master.FindControl("MasterBody");
        if (body != null) body.Attributes.Add("onload", "load()");
    }

    private void BindPremiaDates()
    {
        DataTable dtParametrim = new DataTable();
        clUtils oUtils = new clUtils();
        try
        {

            dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());

            ddMonths.DataSource =
                clGeneral.FillDateInDataTable(int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString())-1, DateTime.Today.AddMonths(-1), false);
            ddMonths.DataBind();
            if (!String.IsNullOrEmpty(Request.QueryString["month"]))
            {
                foreach (ListItem item in ddMonths.Items)
                {
                    if (item.Text.Equals(Request.QueryString["month"]))
                    {
                        ddMonths.SelectedIndex = ddMonths.Items.IndexOf(item);
                        _refreshOnLoad = true;
                        break;
                    }
                }
            }
        }
        catch(Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    private void BindPremias(List<string> groupsForUser)
    {
        string premiaCodes = string.Empty;
        DataTable dtPremiot = null;
        clUtils oUtils = new clUtils();
        foreach (string group in groupsForUser)
        {
            if (group.StartsWith("Kds_Premia", 
                StringComparison.CurrentCultureIgnoreCase))
            {
                premiaCodes += group.ToLower().Replace("kds_premia", "") + ",";
            }
        }

        if (premiaCodes.EndsWith(","))
        {
            premiaCodes = premiaCodes.Substring(0, premiaCodes.Length - 1);
        }

        if (Debugger.IsAttached && string.IsNullOrEmpty(premiaCodes))
        {
            dtPremiot = oUtils.GetPremiotDetailsForCodes(null);
        }
        else
        {
            dtPremiot = oUtils.GetPremiotDetailsForCodes(premiaCodes);
        }

        DataRow drPremiot = dtPremiot.NewRow();
        drPremiot["Kod_Premia"] = -1;
        drPremiot["Teur_Premia"] = "בחר פרמיה";
        dtPremiot.Rows.InsertAt(drPremiot, 0);

        ddStatuses.DataSource = dtPremiot;
        ddStatuses.DataBind();

        if (!String.IsNullOrEmpty(Request.QueryString["premia"]))
        {
            foreach (ListItem item in ddStatuses.Items)
            {
                if (item.Value.Equals(Request.QueryString["premia"]))
                {
                    ddStatuses.SelectedIndex = ddStatuses.Items.IndexOf(item);
                    _refreshOnLoad = true;
                    break;
                }
            }
        }
    }

    private void RefreshData()
    {
        DateTime dateTmp = new DateTime();
        string selectedPremia = ddStatuses.SelectedValue;
        clUtils oUtils = new clUtils();
        DateTime selectedMonth = DateTime.Parse(ddMonths.SelectedValue);
        DataTable dtSource =
            oUtils.GetOvdimForPremiaType(selectedPremia, selectedMonth);

        DataTable dtOvdim = new DataTable();
        dtOvdim.Columns.Add("Mispar_ishi");
        dtOvdim.Columns.Add("Shem");
        dtOvdim.Columns.Add("Maamad");
        dtOvdim.Columns.Add("Ezor");
        dtOvdim.Columns.Add("Snif");
        dtOvdim.Columns.Add("Dakot_premya");
        dtOvdim.Columns.Add("Taarich_Idkun");
        dtOvdim.AcceptChanges();

        DataTable dtOvedDetails = null;
        DataTable dtOvedPremyaDetails = null;
        DataRow drOved = null;
        ListOvdim.Value = ";";
        foreach (DataRow row in dtSource.Rows)
        {
            dtOvedDetails = 
                clOvdim.GetInstance().GetOvedDetails
                (
                    Int32.Parse(row["mispar_ishi"].ToString()), selectedMonth
                );

            dtOvedPremyaDetails =
                oUtils.GetPremiaYadanitForOved
                (
                    Int32.Parse(row["mispar_ishi"].ToString()), 
                    selectedMonth, 
                    Int32.Parse(selectedPremia)
                );

            drOved = dtOvdim.NewRow();

            drOved["Mispar_ishi"] = Int32.Parse(row["mispar_ishi"].ToString());
            drOved["Shem"] = dtOvedDetails.Rows[0]["FULL_NAME"].ToString();
            drOved["Maamad"] = dtOvedDetails.Rows[0]["TEUR_MAAMAD_HR"]
                                            .ToString();
            drOved["Ezor"] = dtOvedDetails.Rows[0]["TEUR_EZOR"].ToString();
            drOved["Snif"] = dtOvedDetails.Rows[0]["TEUR_SNIF_AV"].ToString();
            if (dtOvedPremyaDetails != null && 
                dtOvedPremyaDetails.Rows.Count > 0)
            {
                drOved["Dakot_premya"] = 
                    dtOvedPremyaDetails.Rows[0]["DAKOT_PREMYA"].ToString();
                dateTmp =   DateTime.Parse( dtOvedPremyaDetails.Rows[0]["Taarich_idkun_acharon"]
                                       .ToString());
                drOved["Taarich_Idkun"] = dateTmp.ToString(); // ToLongTimeString();// +" " + dateTmp.ToShortDateString();
                 
            }

            dtOvdim.Rows.Add(drOved);
            ListOvdim.Value += drOved["Mispar_ishi"] + ";";
        }

        Session[SAVED_PREMIAS] = dtOvdim;
        DataView dv = new DataView((DataTable)Session[SAVED_PREMIAS]);
        grdPremias.PageIndex = 0;
        dv.Sort = string.Concat(ViewState["SortExp"], " ",  (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ? "ASC":"DESC");
        grdPremias.DataSource = dv;// dtOvdim;
        grdPremias.DataBind();
        if (dtOvdim.Rows.Count > 0)
        {
            btnSearch.Enabled = true;
       //     btnUpdate.Enabled = true;
        }
        divGrdPremyot.Style["display"] = "inline";
      //  fsGrid.Attributes.Add("display","");
    //    grdPremias.Style["display"] = "inline";
    }
           
    private void SearchOvedInGrid()
    {
        int iPageIndex = 0;
        int iPos = 0;

        DataView dv;

        //Find Employee In Grid
        if (!String.IsNullOrEmpty(txtId.Text))
        {
            int iMisparIshi = int.Parse(txtId.Text);
            if (Session[SAVED_PREMIAS] != null)
            {
                dv = (Session[SAVED_PREMIAS] as DataTable).DefaultView;
                dv.Sort = "Mispar_Ishi";
                iPos = dv.Find(iMisparIshi) + 1;
                iPageIndex = iPos / grdPremias.PageSize;
                if ((iPos % grdPremias.PageSize) == 0)
                {
                    iPageIndex = iPageIndex - 1;
                    iPos = grdPremias.PageSize - 1;
                }
                else
                {
                    iPos = (iPos % grdPremias.PageSize) - 1;
                }
                if (iPageIndex >= 0)
                {
                    ChangeGridPage(iPageIndex);
                    grdPremias.SelectedIndex = iPos;
                }
                else
                {
                    grdPremias.SelectedIndex = -1;
                }
            }
            else
            {
                btnUpdate.Attributes.Add("disabled", "true");
       
                txtName.Text = "";
                txtId.Text = "";
                vldEmpNotExists.IsValid = false;
            }
        }

        if (grdPremias.SelectedIndex != -1)
        {
            TextBox txtDakotPremia =
                grdPremias.SelectedRow.FindControl("txtDakotPremia")
                    as TextBox;
            if (txtDakotPremia != null)
            {
                txtPremiaMinutes.Text = txtDakotPremia.Text;
            }

        }

        txtPremiaMinutes.Enabled = true;
        btnUpdate.Attributes.Add("disabled", "false");
       
    }

    
    [WebMethod]
    [ScriptMethod]
    public static string[] GetOvdimById(string prefixText, int count)
    {
        List<string> result = new List<string>();
        DataTable dt;

        if (HttpContext.Current.Session[SAVED_PREMIAS] != null)
        {
            int iMisparIshi = int.Parse(prefixText);
            dt = HttpContext.Current.Session[SAVED_PREMIAS] as DataTable;
            DataRow[] rows = dt.Select("Mispar_Ishi like '" + prefixText + "%'");
            
            rows.ToList()
                .ForEach
                (
                    row => result.Add(row["Mispar_Ishi"].ToString())
                );
        }

        return result.Count > count ? result.Take(count).ToArray() : result.ToArray();
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetOvdimByName(string prefixText, int count)
    {
        List<string> result = new List<string>();

        DataTable dt;

        if (HttpContext.Current.Session[SAVED_PREMIAS] != null)
        {
            dt = HttpContext.Current.Session[SAVED_PREMIAS] as DataTable;
            DataRow[] rows = null;
            try
            {
                rows = dt.Select("Shem like '" + prefixText + "%'");
            }
            catch { }

            rows.ToList()
                .ForEach
                (
                    row => result.Add(row["Shem"].ToString() + " (" + 
                        row["Mispar_Ishi"].ToString() + ")")
                );
        }

        return result.Count > count ? result.Take(count).ToArray() : result.ToArray();
    }
       

    private void SetFields()
    {
        rdoId.Checked = true;
        rdoId.Attributes.Add("onclick", "SetTextBox();");
        rdoName.Attributes.Add("onclick", "SetTextBox();");
    }
        
    private void ChangeGridPage(int newPageIndex)
    {        
        grdPremias.PageIndex = newPageIndex;
        grdPremias.DataSource = Session[SAVED_PREMIAS];
        grdPremias.DataBind();        
    }


    /*******************************************/
    protected void grdPremias_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;
        DataView dv = new DataView((DataTable)Session[SAVED_PREMIAS]);
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
        //if (e.SortExpression == "taarich")
        //    ViewState["SortExp"] = "sDate";
        //else
            ViewState["SortExp"] = e.SortExpression;

        if (e.SortExpression.Length > 0)
        {
           dv.Sort = string.Concat(ViewState["SortExp"], " ", sDirection);
        }
        grdPremias.PageIndex = 0;
        grdPremias.DataSource = dv;
        grdPremias.DataBind();
      
    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
 
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdPremias.Columns)
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
    #endregion

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Session[SAVED_PREMIAS_ERRORS] = null;

        if (!string.IsNullOrEmpty(txtPremiaMinutes.Text))
        {
            int mispar_ishi = 0;
            if (!Int32.TryParse(txtId.Text, out mispar_ishi))
            {
                mispar_ishi = -1;
            }
            float dakot = 0;
            if (!float.TryParse(txtPremiaMinutes.Text, out dakot))
            {
                dakot = -1;
            }
            string shem_oved = txtName.Text;
            int empID = 0;
            if (!Int32.TryParse(LoginUser.UserInfo.EmployeeNumber, out empID))
            {
                empID = -1;
            }
            if (mispar_ishi > -1 && dakot > -1 && !clGeneral.UpdatePremiaForOved(
                    ddStatuses.SelectedValue, mispar_ishi,
                    DateTime.Parse("1/" + ddMonths.SelectedItem.Text), dakot, empID) //DateTime.Today.Year, DateTime.Today.Month, 1
                )
            {
                if (Session[SAVED_PREMIAS_ERRORS] == null)
                {
                    Session[SAVED_PREMIAS_ERRORS] = new List<string>();
                }
                (Session[SAVED_PREMIAS_ERRORS] as List<string>)
                    .Add(
                        "There was an error updating premia for oved " + shem_oved
                     );
            }
        }
        if (grdPremias.PageCount > 1) ChangeGridPage(0);

        RefreshData();

    }

    //*******Pager Functions*********/
    void grdPremias_RowCreated(object sender, GridViewRowEventArgs e)
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
                    ChangeGridPage(pagerArgs.NewPageIndex, grdPremias,
                           new DataView((DataTable)Session[SAVED_PREMIAS]), "SortDirection",
                           "SortExp");
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
}

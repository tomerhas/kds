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
using KdsLibrary.BL;
using KdsLibrary.Utils;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.UI.SystemManager;
public partial class Modules_Errors_EmployeeDetails : KdsPage
{
    private DataTable dtMisparim = new DataTable();
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       grdOvedErrorCards.RowCreated += new GridViewRowEventHandler(grdEmployee_RowCreated);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int iMisparIshi;
        if (!(Page.IsPostBack))
        {
            ViewState["SortDirection"] = SortDirection.Descending;
            ViewState["SortExp"] = "taarich";
            lblMisparIshi.Text = (string)Request.QueryString["ID"];
            iMisparIshi = int.Parse(lblMisparIshi.Text);
            InputHiddenBack.Value = Session["Params"].ToString();
            SetOvedDetailsDB(iMisparIshi);
            SetOvedCards(iMisparIshi, "taarich", "ASC");           
            divPeriod.InnerText = string.Concat("פרטי העובד לתקופה: ", (string)Request.QueryString["FromDate"], " - ", (string)Request.QueryString["ToDate"]);
            LoadMessages((DataList)Master.FindControl("lstMessages"));
            
        }
        GetMisparimIshi();
        SetButtons();        
    }

    private void SetButtons()
    {
        int iMisparIshi = int.Parse(lblMisparIshi.Text);
        if (dtMisparim.Rows.Count <= 1)
        {
            DisableNextButton();
            DisablePrevButton();
        }

        if (int.Parse(dtMisparim.Rows[0][1].ToString())==iMisparIshi)
        {   //אם מוצג העובד הראשון, לא נאפשר לחיצה על עובד קודם
            DisablePrevButton();
        }

        if (int.Parse(dtMisparim.Rows[dtMisparim.Rows.Count-1][1].ToString())==iMisparIshi)
        {   //אם מוצג העובד האחרון, לא נאפשר לחיצה על עובד הבא
            DisableNextButton();
        }
    }
    
    private void SetOvedDetailsDB(int iMisparIshi)
    {
        try
        {
            clOvdim oOvdim = new clOvdim();
            DataTable dt = new DataTable();
            DateTime dFrom = new DateTime();
            DateTime dTo = new DateTime();
        
           dFrom = DateTime.Parse(Request.QueryString["FromDate"]);
           dTo = DateTime.Parse(Request.QueryString["ToDate"]);
           //dt = oOvdim.GetOvedDetails(iMisparIshi, dFrom);
           dt = oOvdim.GetOvedDetailsByTkufa(iMisparIshi, dFrom, dTo);

           if (dt.Rows.Count > 0)
            {

                lblMisparIshi.Text =  iMisparIshi.ToString();
                lblName.Text =  dt.Rows[0]["full_name"].ToString();
                lblEzor.Text =  dt.Rows[0]["teur_ezor"].ToString();
                lblSnif.Text =  dt.Rows[0]["teur_snif_av"].ToString();           
                lblMaamad.Text =  dt.Rows[0]["teur_maamad_hr"].ToString();
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void SetOvedDetails()
    {
        lblMisparIshi.Text = (string)Request.QueryString["ID"];
        lblName.Text = (string)Request.QueryString["Name"];
        lblSnif.Text = (string)Request.QueryString["Snif"];
        lblEzor.Text = (string)Request.QueryString["Ezor"];
        lblMaamad.Text = (string)Request.QueryString["Maamad"];
    }

    private void SetOvedCards(int iMisparIshi, string sSortExp, string sSortDirection)
    {
        clOvdim oOvdim = new clOvdim();       
        DataTable dt = new DataTable();
        DataView dv;
        DateTime from = new DateTime();
        DateTime to = new DateTime();
        string[] param;
        int iKodStatus=0;
        try
        {
            param = InputHiddenBack.Value.Split(';');
            //(string)Request.QueryString["FromDate"], " - ", (string)Request.QueryString["ToDate"]);
            from = DateTime.Parse((string)Request.QueryString["FromDate"]);      
            to = DateTime.Parse((string)Request.QueryString["ToDate"]);
            if (param[3] != "-1")
                iKodStatus = int.Parse(param[3]);
            dt = oOvdim.GetOvedErrorsCards(iMisparIshi, from, to, iKodStatus, param[4]);
            dv = new DataView(dt);
            if (sSortExp.Length > 0)
            {
                if (sSortExp.IndexOf(char.Parse(",")) > 0) { }
                // dv.Sort = string.Concat(Replace(sSortExp, ",", string.Concat(" " , sSortDirection, ",")) , " " , sSortDirection);
                else
                    dv.Sort = string.Concat(sSortExp, " ", sSortDirection);
            }
           
            grdOvedErrorCards.DataSource = dv;
            grdOvedErrorCards.DataBind();
            Session["OvedCards"] = dv;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetMisparimIshi()
    {

        DataRow dr;
        //string sMisparim="";
        ////string[] arrMisparimIshi;
        //string delimStr = ",";
        //char[] delimiter = delimStr.ToCharArray();
        int iCount;

        try
        {
            //SESSION--נשלוף מספרים אישיים מה 
            //sMisparim = Session["MisparimIshi"].ToString();
            //arrMisparimIshi = sMisparim.Split(delimiter);

            dtMisparim.Columns.Add("MisparIshiIndex", System.Type.GetType("System.Int32"));
            dtMisparim.Columns.Add("MisparIshi", System.Type.GetType("System.Int32"));
            //for (iCount = 0; iCount <= arrMisparimIshi.Length - 1; iCount++)
            for (iCount = 0; iCount <= ((DataTable)Session["MisparimIshi"]).Rows.Count - 1; iCount++)
             {
                dr = dtMisparim.NewRow();
                dr["MisparIshiIndex"] = iCount;
                //dr["MisparIshi"] = arrMisparimIshi[iCount];
                dr["MisparIshi"] = ((DataTable)Session["MisparimIshi"]).Rows[iCount]["mispar_ishi"];
                dtMisparim.Rows.Add(dr);
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdOvedErrorCards_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort = 0;

        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                 System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
                 iColSort = GetCurrentColSort();
                 lbl.Text = " ";
                 e.Row.Cells[iColSort].Controls.Add(lbl);
                 if ((SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
                 {
                    System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                    ImageSort.ID = "imgAscSort";
                    ImageSort.ImageUrl = "../../Images/AscSort.gif";
                    e.Row.Cells[iColSort].Controls.Add(ImageSort);
                 }
                 else
                 {
                    System.Web.UI.WebControls.Image ImageSort = new System.Web.UI.WebControls.Image();
                    ImageSort.ID = "imgDescSort";
                    ImageSort.ImageUrl = "../../Images/DescSort.gif";
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
                e.Row.Cells[4].Style["display"] = "none";
                break;
            case DataControlRowType.DataRow:
                e.Row.Cells[4].Style["display"] = "none";
                ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenEmpWorkCard('", e.Row.Cells[4].Text, "')"));
               // ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenEmpWorkCard('", e.Row.Cells[4].Text, "')"));
                break;
            default:
                break;
        }      
      
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            int iIndex = 1;
            MoveOved(iIndex);          
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        try
        {
            int iIndex = -1;
            MoveOved(iIndex);
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void grdOvedErrorCards_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;
        int iMisparIshi;
        if (ViewState["SortExp"].ToString() == "taarich")
            ViewState["SortExp"] = "teur_yom";
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
        ViewState["SortExp"] = e.SortExpression;
        if (ViewState["SortExp"].ToString() == "teur_yom")
            ViewState["SortExp"] = "taarich";
        grdOvedErrorCards.PageIndex = 0;
        iMisparIshi = int.Parse(lblMisparIshi.Text);
        SetOvedCards(iMisparIshi, (string)ViewState["SortExp"], sDirection);
        grdOvedErrorCards.DataBind();
    }
    private int GetCurrentColSort()
    {
        string sSortExp;
        if (ViewState["SortExp"].ToString() == "taarich")
            sSortExp = "teur_yom";
        else
            sSortExp = (string)ViewState["SortExp"];
        int iColNum = -1;
        try
        {
            foreach (DataControlField dc in grdOvedErrorCards.Columns)
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
    //protected void grdOvedErrorCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    string sDirection;
    //    int iMisparIshi= int.Parse(lblMisparIshi.Text);

    //    if ((SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
    //        sDirection = "ASC";
    //    else
    //        sDirection = "DESC";

    //    grdOvedErrorCards.PageIndex = e.NewPageIndex;
    //    SetOvedCards(iMisparIshi, (string)ViewState["SortExp"], sDirection);
    //}

    //protected void btnFirst_Click(object sender, EventArgs e)
    //{
    //    try
    //    {           
    //        MoveLastFirstOved(0);
    //    }
    //    catch (Exception ex)
    //    {
    //        clGeneral.BuildError(Page, ex.Message);
    //    }
    //}
    //protected void btnLast_Click(object sender, EventArgs e)
    //{
    //    try
    //    {          
    //        MoveLastFirstOved(dtMisparim.Rows.Count-1);
    //    }
    //    catch (Exception ex)
    //    {
    //        clGeneral.BuildError(Page, ex.Message);
    //    }
    //}

    private void MoveOved(int iIndex)
    {
        //הפרוצדורה מעבירה לעובד הקודם או לעובד הבא
        try
        {
            DataRow dr;
            int iCurrMisparIshi, iCurrIndex;
            int iNewMisparIshi;
            string sDirection;

            iCurrMisparIshi = int.Parse(lblMisparIshi.Text);

            //נמצא את הרשומה הנוכחית
            dtMisparim.PrimaryKey = new DataColumn[] { dtMisparim.Columns["MisparIshi"] };
            dr = dtMisparim.Rows.Find(iCurrMisparIshi.ToString());

            //נשלוף את האינדקס הנוכחי
            iCurrIndex = int.Parse(dr["MisparIshiIndex"].ToString());

            //נעמוד על האינדקס הבא
            dtMisparim.PrimaryKey = new DataColumn[] { dtMisparim.Columns["MisparIshiIndex"] };
            if (((iCurrIndex + iIndex) >= 0) && ((iCurrIndex + iIndex) <= dtMisparim.Rows.Count - 1))
            {
                dr = dtMisparim.Rows.Find((iCurrIndex + iIndex).ToString());

                //נשלוף את המספר האישי הבא
                iNewMisparIshi = int.Parse(dr["MisparIshi"].ToString());
                
                if ((SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
                    sDirection = "ASC";
                else
                    sDirection = "DESC";

                SetOvedCards(iNewMisparIshi, (string)ViewState["SortExp"], sDirection);
                SetOvedDetailsDB(iNewMisparIshi);            
            }

            //הגענו להתחלה
            if ((iCurrIndex + iIndex) <= 0)
            {
                //Disable prev button
                DisablePrevButton();                             
            }
            else
            {
                btnPrev.ControlStyle.CssClass = "ImgButtonOvedPrev";
                btnPrev.Enabled = true;
                btnPrev.Text = "";
            }

            //הגענו להתחלה
            if ((iCurrIndex + iIndex) >= dtMisparim.Rows.Count - 1)
            {
                //Disable next button
                DisableNextButton();
            }
            else
            {
                btnNext.ControlStyle.CssClass = "ImgButtonOvedNext";
                btnNext.Enabled = true;
                btnNext.Text = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void DisableNextButton()
    {
        btnNext.ControlStyle.CssClass = "ImgButtonOvedNextDisable";
        btnNext.Enabled = false;
        btnNext.Text = "העובד הבא";
    }

    private void DisablePrevButton()
    {
        btnPrev.ControlStyle.CssClass = "ImgButtonOvedPrevDisable";
        btnPrev.Enabled = false;
        btnPrev.Text = "העובד הקודם";
    }



    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        try
        {
             Session["Params"] = InputHiddenBack.Value;
             Response.Redirect("EmployeErrors.aspx?Back=true");
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnRefresh_OnClick(object sender, EventArgs e)
    {
        string dFrom, dTo;//, misIshi; 
        dFrom = Request.QueryString["FromDate"];
        dTo = Request.QueryString["ToDate"];

        Response.Redirect(string.Concat("EmployeeDetails.aspx?ID=", lblMisparIshi.Text, "&ToDate=", dTo, "&FromDate=", dFrom));   
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
                    ChangeGridPage(int.Parse(pagerArgs.NewPageIndex.ToString()), grdOvedErrorCards,
                           (DataView)Session["OvedCards"], "SortDirection",
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
    //private void MoveLastFirstOved(int iIndex)
    //{
    //    try
    //    {
    //        DataRow dr;
    //        //int iCurrMisparIshi, iCurrIndex;
    //        int iNewMisparIshi;

    //        //iCurrMisparIshi = int.Parse(lblMisparIshi.Text);

    //        ////נמצא את הרשומה הנוכחית
    //        //dtMisparim.PrimaryKey = new DataColumn[] { dtMisparim.Columns["MisparIshi"] };
    //        //dr = dtMisparim.Rows.Find(iCurrMisparIshi.ToString());

    //        ////נשלוף את האינדקס הנוכחי
    //        //iCurrIndex = int.Parse(dr["MisparIshiIndex"].ToString());

    //        //נעמוד על האינדקס הבא
    //        dtMisparim.PrimaryKey = new DataColumn[] { dtMisparim.Columns["MisparIshiIndex"] };
    //        dr = dtMisparim.Rows.Find((iIndex).ToString());

    //        //נשלוף את המספר האישי הבא
    //        iNewMisparIshi = int.Parse(dr["MisparIshi"].ToString());

    //        SetOvedCards(iNewMisparIshi);
    //        SetOvedDetailsDB(iNewMisparIshi);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}

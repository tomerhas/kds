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
using KdsLibrary.UI;
using System.Drawing;
using System.Text;
using KdsLibrary.UI.SystemManager;

public partial class Modules_Errors_EmployeErrors : KdsPage
{
    const int COL_MISPAR_ISHI = 0;
    const int COL_NAME = 1;
    const int COL_EZOR = 2;
    const int COL_SNIF = 3;
    const int COL_MAAMAD = 4;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        grdEmployee.RowCreated += new GridViewRowEventHandler(grdEmployee_RowCreated);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtParametrim = new DataTable();
        clUtils oUtils = new clUtils();
        try
        {
            if (!Page.IsPostBack)
            {

                if (Request.QueryString["Back"] != null)
                {
                    InputHiddenBack.Value = "true";
                    SlofParamsFromSession();
                }
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "רשימת עובדים בעלי כרטיסי עבודה לטיפול";
                SetFields();
                LoadMessages((DataList)Master.FindControl("lstMessages"));
                
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                for (int i = 0; i < dtParametrim.Rows.Count; i++)
                    Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());
            }
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message);
        }
    }
    private void SlofParamsFromSession()
    {
        string[] ParamsBack = Session["Params"].ToString().Split(';');
        InputHiddenBack.Attributes.Add("Ezor", ParamsBack[0]);
        InputHiddenBack.Attributes.Add("Snif", ParamsBack[1]);
        InputHiddenBack.Attributes.Add("Maamad", ParamsBack[2]);
        InputHiddenBack.Attributes.Add("From", ParamsBack[3]);
        InputHiddenBack.Attributes.Add("To", ParamsBack[4]);
    }
    private void SetDatesDefaults()
    {
        try
        {
            DateTime dDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            clnFromDate.Text = dDate.AddMonths(-10).ToShortDateString();
            clnToDate.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void SetFields()
    {
        try
        {
            //טעינת איזורים
            LoadEzorim();

            //נשמור ברירת מחדל של איזורים
            txtSnif.Attributes.Add("onchange", "GetMaamad();");
            if (int.Parse(ddlSite.SelectedValue) == -1)
            {
                AutoCompleteSnif.ContextKey = "0";
            }
            else
            {
                AutoCompleteSnif.ContextKey = ddlSite.SelectedValue;
            }
            btnMaamad.Style["Display"] = "none";
            btnRedirect.Style["Display"] = "none";
            txtRowSelected.Style["Display"] = "none";
            rdoId.Checked = true;         
            rdoId.Attributes.Add("onclick", "SetTextBox();");
            rdoName.Attributes.Add("onclick", "SetTextBox();");
            ViewState["SortDirection"] = SortDirection.Descending;
            ViewState["SortExp"] = "mispar_ishi";
            SetDatesDefaults();
            LoadMaamad();
            LoadOvdimGrid("", "ASC");
           // AutoCompleteExtenderID.OnClientHidden = "GetOvedName();";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadEzorim()
    {
        DataTable dt;
        clUtils oUtils = new clUtils();
         //object sender= new object();
         //EventArgs e = new EventArgs();
        try
        {
            dt = oUtils.GetEzorim();
            ddlSite.DataValueField = "code";
            ddlSite.DataTextField = "description";
            ddlSite.DataSource = dt;
            ddlSite.DataBind();

            if (InputHiddenBack.Value == "true"){
                ddlSite.SelectedValue = InputHiddenBack.Attributes["EZOR"];
                ddlSite_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            btnSearch.ControlStyle.CssClass = "ImgButtonSearchDisable";
            btnSearch.Enabled = false;
            //נשמור את קוד האיזור
            AutoCompleteSnif.ContextKey = ddlSite.SelectedValue;
            if (int.Parse(ddlSite.SelectedValue) == -1)
            {
                AutoCompleteSnif.ContextKey = "0";
            }
            if (InputHiddenBack.Value == "true")
                txtSnif.Text = InputHiddenBack.Attributes["SNIF"];
            else
                txtSnif.Text = "";
            LoadMaamad();
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message);
        }
    }

    private void LoadMaamad()
    {
        DataTable dt;
        clUtils oUtils = new clUtils();
        try
        {
            dt = oUtils.GetMaamad(GetKodHevra(txtSnif.Text));
            KdsLibrary.clGeneral.InsertNotSelectedOption(ref dt, 2, "שכירים");
            KdsLibrary.clGeneral.InsertNotSelectedOption(ref dt, 1, "חברים");
            KdsLibrary.clGeneral.InsertNotSelectedOption(ref dt, -1, "הכל");
            ddlMaamad.DataTextField = "description";
            ddlMaamad.DataValueField = "code";
            ddlMaamad.DataSource = dt;
            ddlMaamad.DataBind();

            if (InputHiddenBack.Value == "true")
            {
                ddlMaamad.SelectedValue = InputHiddenBack.Attributes["MAAMAD"];
                clnFromDate.Text = InputHiddenBack.Attributes["FROM"];
                clnToDate.Text = InputHiddenBack.Attributes["TO"];
            }
           
               
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnMaamad_Click(object sender, EventArgs e)
    {
        LoadMaamad();
        btnSearch.ControlStyle.CssClass = "ImgButtonSearchDisable";
        btnSearch.Enabled = false;
    }
    protected void ddlMaamad_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch.ControlStyle.CssClass = "ImgButtonSearchDisable";
        btnSearch.Enabled = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //חיפוש רשומה בגריד
        SearchOvedInGrid();
    }

    private void SearchOvedInGrid()
    {        
        int iPageIndex = 0;        
        int iPos = 0;
        
        DataView dv;
       
        try
         { //מציאת עובד בגריד
             if (txtId.Text != string.Empty)
             {
                 int iMisparIshi = int.Parse(txtId.Text);
                 //if (Session["MisparimIshi"].ToString().IndexOf(iMisparIshi.ToString()) > -1)
                 if (((DataTable)Session["MisparimIshi"]).Select("mispar_ishi=" + iMisparIshi.ToString()).Length > 0)
                 {
                     dv = GetMisparimIshiFromSession();
                     iPos = dv.Find(iMisparIshi) + 1;
                     iPageIndex = iPos / grdEmployee.PageSize;
                     if ((iPos % grdEmployee.PageSize) == 0)
                     {
                         iPageIndex = iPageIndex - 1;
                         iPos = grdEmployee.PageSize - 1;
                     }
                     else
                     {
                         iPos = (iPos % grdEmployee.PageSize) - 1;
                     }
                     grdEmployee.PageIndex = iPageIndex;

                     grdEmployee.DataSource = (DataView)Session["Ovdim_Details"];
                     grdEmployee.DataBind();

                     grdEmployee.Rows[iPos].BackColor = Color.FromArgb(206, 148, 15);//;.CornflowerBlue;
                     grdEmployee.Rows[iPos].ForeColor = Color.White;
                 }
                 else
                 {
                     txtName.Text = "";
                     txtId.Text = "";
                     vldEmpNotExists.IsValid = false;
                 }
                 SetNameAndId();
             }
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetNameAndId()
    {
        if (rdoId.Checked)
        {
            txtId.Enabled = true;
            txtName.Enabled = false;
        }
        else
        {
            txtId.Enabled = false;
            txtName.Enabled = true;
        }
    }
    private int GetKodSnif(string sSnif)
    {
        //מציאת קוד סניף
        string sTemp = "";
        string[] arr;

        arr = sSnif.Split(char.Parse("("));
        if (arr.Length > 1)
        {
            sTemp = arr[1];
            arr = sTemp.Split(char.Parse(")"));
            return int.Parse(arr[0]);
        }
        else
        {
            return 0;
        }
    }
    private int GetKodHevra(string sSnif)
    {      
        //מציאת קוד החברה- מתוך תיאור סניף
        string sTemp = "";
        string[] arr;
       
        arr = sSnif.Split(char.Parse("("));

        if (arr.Length > 1)
        {
            sTemp = arr[arr.Length - 1].Substring(0, arr[arr.Length - 1].Length - 1);
        }
        if (sTemp == string.Empty)
        {
            return 0;
        }
        else{
            return int.Parse(sTemp);}
    }
    protected void btnExecute_Click(object sender, EventArgs e)
    {                        
        try
        {               
           LoadOvdimGrid("","ASC");
        
           //AutoCompleteExtenderID.ContextKey = Session["MisparimIshi"].ToString();
           //AutoCompleteExtenderByName.ContextKey = Session["MisparimIshi"].ToString(); 
           AutoCompleteExtenderID.ContextKey = "1";
           AutoCompleteExtenderByName.ContextKey = "1"; 
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message);
        }
    }
    private void LoadOvdimGrid(string sSortExp, string sSortDirection)
    {
        DateTime dFrom = new DateTime();
        DateTime dTo = new DateTime();
        DataTable dt = new DataTable();
        clOvdim oOvdim = new clOvdim();
        DataView dv;
        int iKodSnif=0;
        int iKodMaamad=0;
        int iKodEzor = 0;
        int iKodHevra = 0;
        string[] arrMaamadHevraKeys;

        try
        {   //שליפת קוד איזור, קוד חברה, קוד סניף וקוד מעמד מהמסך         
            iKodEzor= int.Parse(ddlSite.SelectedValue);
            if (iKodEzor == -1) { iKodEzor = 0; }

            if ((txtSnif.Text) != string.Empty)
            {
                iKodSnif = GetKodSnif(txtSnif.Text);
                iKodHevra = GetKodHevra(txtSnif.Text);
            }
            if (ddlMaamad.SelectedIndex > 2)
            {
                arrMaamadHevraKeys = ddlMaamad.SelectedValue.Split(char.Parse("-"));
                iKodMaamad = int.Parse(arrMaamadHevraKeys[1]);
                iKodHevra = int.Parse(arrMaamadHevraKeys[0]);
            }else if (ddlMaamad.SelectedIndex ==1 || ddlMaamad.SelectedIndex ==2)
                    {
                        iKodMaamad = int.Parse(ddlMaamad.SelectedItem.Value);
                        iKodHevra = 0;
                    }
            dFrom = DateTime.Parse(clnFromDate.Text);
            dTo = DateTime.Parse(clnToDate.Text);
            dt = oOvdim.GetErrorOvdim(iKodHevra,iKodEzor, iKodSnif, iKodMaamad, dFrom, dTo);
            
            InsertMisparIshiToSession(dt);

            dv = new DataView (dt);
            if (sSortExp.Length > 0)
            {               
              dv.Sort = string.Concat(sSortExp, " ", sSortDirection); 
            }
           
            grdEmployee.DataSource = dv;
            grdEmployee.DataBind();
            Session["Ovdim_Details"] = dv;

            if (dv.Count == 0)
            {
                btnSearch.ControlStyle.CssClass = "ImgButtonSearchDisable";
                btnSearch.Enabled = false;
            }
            else
            {
                btnSearch.ControlStyle.CssClass = "ImgButtonSearch";
                btnSearch.Enabled = true;
            }
            Session["Params"] = ddlSite.SelectedValue + ";" + txtSnif.Text + ";" +
                        ddlMaamad.SelectedValue + ";" + clnFromDate.Text + ";" +
                        clnToDate.Text;
            FillPirteySinun();
            btnSearch.ControlStyle.CssClass = "ImgButtonSearch";
            btnSearch.Enabled = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillPirteySinun()
    {
        string ezor, snif, maamad;
        try
        {
            ezor = ddlSite.SelectedValue;
            if (ezor != "-1")
                ezor = ddlSite.SelectedItem.Text.Split('-')[1].Replace(" ", "");
            snif =txtSnif.Text;
            if (snif != "")
                snif = txtSnif.Text.Split(')')[0].Split('(')[1];

            maamad = ddlMaamad.SelectedValue;
            if (maamad != "-1")
                maamad = ddlMaamad.SelectedItem.Text.Split('(')[0];
            Session["PirteySinun"] = ezor + ";" + snif + ";" + maamad; // ddlSite.Text + ";" + txtSnif.Text + ";" + ddlMaamad.SelectedValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
      
    }
    private DataView GetMisparimIshiFromSession()
    {
        DataTable dtMisparim = new DataTable();
        DataRow dr;
        //string sMisparim = "";        
        //string delimStr = ",";
        //char[] delimiter = delimStr.ToCharArray();
        //string[] arrMisparimIshi;
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
            
            DataView dv = new DataView(dtMisparim);

            dv.Sort = string.Concat("MisparIshi", " ", "ASC");
            
            return dv;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InsertMisparIshiToSession(DataTable dt)
    {
        //StringBuilder sOvdimMisparIshi=new StringBuilder();
        
        try
        {
            ////נשמור את המספרים האישיים ב- SESSION
            //foreach (DataRow dr in dt.Rows)
            //{
            //    sOvdimMisparIshi.Append(dr["mispar_ishi"] + ",");
            //}

            //if (sOvdimMisparIshi.Length > 0)
            //{
            //    sOvdimMisparIshi.Remove( sOvdimMisparIshi.Length - 1,1);
            //}

            //Session["MisparimIshi"] = sOvdimMisparIshi.ToString();
            //AutoCompleteExtenderID.ContextKey = sOvdimMisparIshi.ToString();
            //AutoCompleteExtenderByName.ContextKey = sOvdimMisparIshi.ToString(); 
 
            Session["MisparimIshi"] = dt;
            AutoCompleteExtenderID.ContextKey = "1";
            AutoCompleteExtenderByName.ContextKey = "1";           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iColSort = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenOvedDetails(", e.Row.RowIndex, ")"));
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {           
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
        }
    }

    private int GetCurrentColSort()
    {
        string sSortExp = (string)ViewState["SortExp"];
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
    protected void grdEmployee_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sDirection;

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

        //LoadOvdimGrid(e.SortExpression, sDirection);
        ((DataView)Session["Ovdim_Details"]).Sort= string.Concat(e.SortExpression," ", sDirection);
        grdEmployee.DataSource = (DataView)Session["Ovdim_Details"];
        grdEmployee.DataBind();
    }

    protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        grdEmployee.PageIndex = e.NewPageIndex;      
        //LoadOvdimGrid("", "");
        grdEmployee.DataSource = (DataView)Session["Ovdim_Details"];
        grdEmployee.DataBind();
    }
    protected void grdEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnRedirect_Click(Object sender, CommandEventArgs e)
    {
        string sMisparIshi="";
        string sName = "";
        string sEzor = "";
        string sSnif = "";
        string sMaamad = "";
        string sToDate = "";
        string sFromDate = "";

        int iRowId=0;
        try
        {
            iRowId =int.Parse(txtRowSelected.Text);
            sMisparIshi = ((HyperLink)grdEmployee.Rows[iRowId].Controls[0].Controls[0]).Text;
            sName = (grdEmployee.Rows[iRowId].Cells[COL_NAME]).Text;
            sEzor = (grdEmployee.Rows[iRowId].Cells[COL_EZOR]).Text;
            sSnif= (grdEmployee.Rows[iRowId].Cells[COL_SNIF]).Text;
            sMaamad = (grdEmployee.Rows[iRowId].Cells[COL_MAAMAD]).Text;
            sToDate = clnToDate.Text;
            sFromDate = clnFromDate.Text;
           // Session["OvedDetails"] = sName + ";" + sEzor + ";" + sSnif + ";" + sMaamad;
            //Response.Redirect(string.Concat("EmployeeDetails.aspx?ID=", sMisparIshi,"&Name=",sName,"&Ezor=",sEzor,"&Snif=",sSnif,"&Maamad=",sMaamad,"&ToDate=",sToDate));
            Response.Redirect(string.Concat("EmployeeDetails.aspx?ID=", sMisparIshi,  "&ToDate=", sToDate, "&FromDate=", sFromDate));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void btnNext_Click(object sender, EventArgs e)
    //{
    //    grdEmployee.PageIndex  = Math.Min(grdEmployee.PageCount - 1, grdEmployee.PageIndex + 1);
    //    grdEmployee.DataBind();
    //}
    //protected void btnPrev_Click(object sender, EventArgs e)
    //{
    //    grdEmployee.PageIndex = Math.Min(0, grdEmployee.PageIndex - 1);
    //    grdEmployee.DataBind();
    //}
    //protected void btnFirst_Click(object sender, EventArgs e)
    //{
    //    grdEmployee.PageIndex = 0;
    //    grdEmployee.DataBind();
    //}
    //protected void btnLast_Click(object sender, EventArgs e)
    //{
    //    grdEmployee.PageIndex = grdEmployee.PageCount - 1;
    //    grdEmployee.DataBind();
    //}
    protected void vldEmpNotExists_ServerValidate(object source, ServerValidateEventArgs args)
    {
           
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
                    ChangeGridPage(pagerArgs.NewPageIndex, grdEmployee,
                       (DataView)Session["Ovdim_Details"], "SortDirection",
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

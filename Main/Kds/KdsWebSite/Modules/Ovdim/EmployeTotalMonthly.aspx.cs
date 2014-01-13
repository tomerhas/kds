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
using System.IO;
using System.Collections.Generic;
using KdsBatch.Reports;

public partial class Modules_Ovdim_EmployeTotalMonthly : KdsPage
{
    public const int COL_KIZUZ_MEAL_MICHSA = 3;
    public const int COL_YOM1 = 5;
    public const int COL_YOM11 = 15;
    private string[] arrParams;
   
    protected override void CreateUser()
    {
        if (!Page.IsPostBack && Request.QueryString["EmpId"] == null)
            Session["arrParams"]=null;
        if (Request.QueryString["Key"] != null && Session["arrParams"]==null) 
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
            txtEmpId.Text = Request.QueryString["EmpId"].ToString();
            SetUserKiosk(int.Parse(Request.QueryString["EmpId"].ToString()));
        }
        else { Session["arrParams"] = null;
            base.CreateUser(); }
    }

    private void SetUserKiosk(int iMisparIshiKiosk)
    {
        
        LoginUser = LoginUser.GetLimitedUser(iMisparIshiKiosk.ToString());
        LoginUser.InjectEmployeeNumber(iMisparIshiKiosk.ToString());
        MasterPage mp = (MasterPage)Page.Master;
        mp.DisabledMenuAndToolBar = true;
        DisplayDivMessages = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
           MasterPage mp = (MasterPage)Page.Master;
            mp.ImageExcelClick.Click += new ImageClickEventHandler(ImageExcelClick_Click);
            mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);

            if (!Page.IsPostBack)
            {
                PageHeader = "סיכום חודשי לעובד";
                DateHeader = DateTime.Today.ToShortDateString();
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";

                LoadMessages((DataList)Master.FindControl("lstMessages"));

                divNetunim.Visible = false;

                btnCalc.Style.Add("Display", "None");
                btnHidden.Style.Add("Display", "None");
                SetFixedHeaderGrid(pnlTotalMonthly.ClientID, mp.HeadPage);
                SetFixedHeaderGrid(pnlMonthlyComponents.ClientID, mp.HeadPage);

                txtEmpId.Text = LoginUser.UserInfo.EmployeeNumber;
                ViewState["UserId"] = LoginUser.UserInfo.EmployeeNumber;

                clOvdim oOvdim = new clOvdim();
                txtName.Text = oOvdim.GetOvedFullName(clGeneral.GetIntegerValue(LoginUser.UserInfo.EmployeeNumber));
                LoadDdlMonth();
                rdoId.Attributes.Add("onclick", "SetTextBox();");
                rdoName.Attributes.Add("onclick", "SetTextBox();");

                if (Request.QueryString["WCardDate"] != null)
                {
                    ddlMonth.SelectedValue = Request.QueryString["WCardDate"].ToString().Substring(3, 7);
                }

                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                if (iSecurity == KdsSecurityLevel.ViewAll) // || iSecurity == KdsSecurityLevel.ViewOnlyEmployeeData)
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
                    AutoCompleteExtenderByName.ContextKey = "";// LoginUser.UserInfo.EmployeeNumber;
                    AutoCompleteExtenderID.ContextKey = ""; // LoginUser.UserInfo.EmployeeNumber;
                    txtEmpId.Enabled = false;
                    rdoId.Enabled = false;
                    rdoName.Enabled = false;
                }
                txtName.Enabled = false;
                    
                btnShow_Click(this, new EventArgs());
                
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }


    }

     void LoadDdlMonth()
    {
        DataTable dtMonth;
         clOvdim oOvdim = new clOvdim();
         if (txtEmpId.Text.Length > 0 && clGeneral.IsNumeric(txtEmpId.Text))
         {
             //dtMonth = oOvdim.GetMonthsToOved(int.Parse(txtEmpId.Text));
             dtMonth = oOvdim.GetMonthsHuavarLesacharToOved(int.Parse(txtEmpId.Text));
             
             ddlMonth.DataSource = dtMonth;
             ddlMonth.DataBind();
             ddlMonth.Enabled = true;
         }
         else { ddlMonth.Enabled = false; }
       
    }

    void ImagePrintClick_Click(object sender, ImageClickEventArgs e)
    {
        Print();
    }

    void Print()
    {
        byte[] s;
        string sScript;
       // ReportModule Report = new ReportModule();// ReportModule.GetInstance();
        string sIp = "";
        string sPathFilePrint = ConfigurationManager.AppSettings["PathFileReportsTemp"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
        clReportOnLine oReportOnLine = new clReportOnLine("RikuzAvodaChodshi2", eFormat.PDF);

        if (Page.IsValid)
        {
            if (ViewState["BakashId"] != null)
            {
                oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", txtEmpId.Text));
                oReportOnLine.ReportParams.Add(new clReportParam("P_TAARICH", ViewState["Taarich"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_BAKASHA_ID", ViewState["BakashId"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_Tar_chishuv", ViewState["TarChishuv"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_sug_chishuv", ViewState["SugChishuv"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_Oved_5_Yamim", ViewState["WorkDay"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_SIKUM_CHODSHI", "1"));
                oReportOnLine.ReportParams.Add(new clReportParam("P_DT", DateTime.Now.ToString()));

                s = oReportOnLine.CreateFile();

                if (LoginUser.IsLimitedUser && arrParams[2].ToString() == "1")
                {
                    string sFileName, sPathFile;
                    FileStream fs;

                    sIp = "";// arrParams[1];
                    sFileName = "RikuzAvodaChodshi.pdf";
                    sPathFile = ConfigurationManager.AppSettings["PathFileReports"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
                    if (!Directory.Exists(sPathFile))
                        Directory.CreateDirectory(sPathFile);
                    fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
                    fs.Write(s, 0, s.Length);
                    fs.Flush();
                    fs.Close();

                    sScript = "PrintDoc('" + sIp + "' ,'" + sPathFilePrint + sFileName + "')";



                }
                else
                {

                    Session["BinaryResult"] = s;
                    Session["TypeReport"] = "PDF";
                    Session["FileName"] = "RikuzAvodaChodshi";

                    sScript = "window.showModalDialog('../../ModalShowPrint.aspx','','dialogwidth:800px;dialogheight:740px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;');";
                }

                MasterPage mp = (MasterPage)Page.Master;

                ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintPdf", sScript, true);
            }
        }
    }

    void ImageExcelClick_Click(object sender, ImageClickEventArgs e)
    {
        byte[] s;
        string sScript;
      //  ReportModule Report = new ReportModule();//  ReportModule.GetInstance();

        if (Page.IsValid)
        {
            if (ViewState["BakashId"] != null)
            {
                clReportOnLine oReportOnLine = new clReportOnLine("RikuzAvodaChodshi2", eFormat.EXCEL);
  
                oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", txtEmpId.Text));
                oReportOnLine.ReportParams.Add(new clReportParam("P_TAARICH", ViewState["Taarich"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_BAKASHA_ID", ViewState["BakashId"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_Tar_chishuv", ViewState["TarChishuv"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_sug_chishuv", ViewState["SugChishuv"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_Oved_5_Yamim", ViewState["WorkDay"].ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_SIKUM_CHODSHI", "1"));
                oReportOnLine.ReportParams.Add(new clReportParam("P_DT", DateTime.Now.ToString()));

                s = oReportOnLine.CreateFile();

                Session["BinaryResult"] = s;
                Session["TypeReport"] = "EXCEL";
                Session["FileName"] = "RikuzAvodaChodshi";

                sScript = "document.all.ctl00_KdsContent_iFramePrint.src='../../ShowPrint.aspx';";
                MasterPage mp = (MasterPage)Page.Master;

                ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintExcel", sScript, true);
            }
        }
    }

   
    protected void btnShow_Click(object sender, EventArgs e)
    {
        clOvdim oOvdim = new clOvdim();
        DateTime dTaarich, dTarChishuv;
        DataTable dtPirteyOved, dtHeadrut, dtRechivimChodshiym;
        long iBakashaId=0;
        int iSugChishuv;
        string sWorkDay="";

        dTarChishuv = DateTime.Today;
        try
        {
             if (ddlMonth.SelectedValue.Length > 0 && clGeneral.IsNumeric(txtEmpId.Text))
            {
                dTaarich = clGeneral.GetEndMonthFromStringMonthYear(1, ddlMonth.SelectedValue);

                ViewState["Taarich"] = dTaarich;
                dtPirteyOved = oOvdim.GetPirteyOved(int.Parse(txtEmpId.Text), dTaarich);

                if (dtPirteyOved.Rows.Count > 0)
                {
                    divNetunim.Visible = true;
                    btnPrint.Enabled = true;
                    btnPrint.CssClass = "ImgButtonSearch";

                    lblEmployeId.Text = dtPirteyOved.Rows[0]["MISPAR_ISHI"].ToString();
                    lblFirstName.Text = dtPirteyOved.Rows[0]["SHEM_PRAT"].ToString();
                    lblLastName.Text = dtPirteyOved.Rows[0]["SHEM_MISH"].ToString();
                    lblMonthYear.Text = dtPirteyOved.Rows[0]["month_year"].ToString();
                    lblMaamad.Text = dtPirteyOved.Rows[0]["maamad"].ToString();
                    lblEzor.Text = dtPirteyOved.Rows[0]["ezor"].ToString();
                     lblStationSalary.Text = dtPirteyOved.Rows[0]["tachanat_sachar"].ToString();
                       lblGil.Text = dtPirteyOved.Rows[0]["gil"].ToString();
                      lblIsuk.Text = dtPirteyOved.Rows[0]["isuk"].ToString();

                    sWorkDay = oOvdim.GetMeafyenLeOved(int.Parse(txtEmpId.Text), dTaarich, clGeneral.enMeafyeneyOved.YemeyAvoda);
                    if (int.Parse(sWorkDay) == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                    { lblWorkDay.Text = "5 ימים חודשי"; }
                    else if (int.Parse(sWorkDay) == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode())
                    { lblWorkDay.Text = "5 ימים יומי"; }
                    else if (int.Parse(sWorkDay) == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                    { lblWorkDay.Text = "6 ימים יומי"; }
                    else { lblWorkDay.Text = "6 ימים חודשי"; }

                    ViewState["WorkDay"] = lblWorkDay.Text;
                    KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                    if (iSecurity == KdsSecurityLevel.ViewAll)
                    {
                        lblEmployeId.Attributes.Add("onClick", "javascript:window.showModalDialog('NetuneyOvedModal.aspx?Id=" + lblEmployeId.Text + "&Month=" + ddlMonth.SelectedValue + "','','dialogwidth:970px;dialogheight:580px;dialogtop:130px;dialogleft:25px;status:no;resizable:yes;scroll:0;');");
                        lblEmployeId.CssClass = "link";
                    }

                    iSugChishuv = oOvdim.GetSugChishuv(int.Parse(txtEmpId.Text), dTaarich, ref iBakashaId, ref dTarChishuv);

                    lblCalcMonth.Text = dTarChishuv.ToShortDateString();

                    lblCalcType.Text = clGeneral.arrCalcType[iSugChishuv];
                    if (iSugChishuv == clGeneral.enSugChishiuv.Patuch.GetHashCode())
                    {
                        lblCalcType.Style["color"] = "red";
                        ScriptManager.RegisterStartupScript(btnShow, this.GetType(), "RunCalc", "document.getElementById('ctl00_KdsContent_btnShow').disabled=true;document.getElementById('divProgress').style.display='none';document.getElementById('DivCalc').style.display='block';document.getElementById('ctl00_KdsContent_btnCalc').click();", true);
                        Session["dtRikuz1To10"] = null;
                        Session["dtRikuz11To20"] = null;
                        Session["dtRikuz21To31"] = null;
                        grdTotalMonthly.DataSource = Session["dtRikuz1to10"];
                        grdTotalMonthly.DataBind();
                        grdMonthlyComponents.DataSource = null;
                        grdMonthlyComponents.DataBind();
                        grdAbsenceData.DataSource = null;
                        grdAbsenceData.DataBind();
                    }
                    else
                    {
                        lblCalcType.Style["color"] = "black";
                    }
                    ViewState["BakashId"] = iBakashaId;
                    ViewState["SugChishuv"] = lblCalcType.Text;
                    ViewState["TarChishuv"] = dTarChishuv;
                    ViewState["KodSugChishuv"] = iSugChishuv;

                    if (iSugChishuv != clGeneral.enSugChishiuv.Patuch.GetHashCode())
                    {
                        dtHeadrut = oOvdim.GetPirteyHeadrut(int.Parse(txtEmpId.Text), dTaarich, iBakashaId);
                        grdAbsenceData.DataSource = dtHeadrut;
                        grdAbsenceData.DataBind();

                        dtRechivimChodshiym = oOvdim.GetRechivimChodshiyim(int.Parse(txtEmpId.Text), dTaarich, iBakashaId, 1);
                        dtRechivimChodshiym = clGeneral.FilterDataTable(dtRechivimChodshiym, "Letzuga_Besikum_Chodshi=2");
                        grdMonthlyComponents.DataSource = dtRechivimChodshiym;
                        grdMonthlyComponents.DataBind();

                        ViewState["fErechRechiv45"] = GetSumErechRechiv45(dtRechivimChodshiym);
                        GetRikuzChodshi(float.Parse(ViewState["fErechRechiv45"].ToString()));

                        ViewState["ShowYom11"] = false;
                        ViewState["ShowDays"] = 1.10;
                        grdTotalMonthly.DataSource = Session["dtRikuz1to10"];
                        grdTotalMonthly.DataBind();
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    private float GetSumErechRechiv45(DataTable dtRechivimChodshiym)
    {
        object objRechiv45 = null;

        objRechiv45 = dtRechivimChodshiym.Compute("sum(erech_rechiv)", "kod_rechiv=" + clGeneral.enRechivim.SachGmulChisachon.GetHashCode().ToString());

        if (objRechiv45.Equals(System.DBNull.Value))
        {
            return 0;
        }
        else
        {
            return float.Parse(objRechiv45.ToString());
        }
    }

     protected void grdTotalMonthly_RowDataBound(object sender, GridViewRowEventArgs e) 
     {
         int iDay,iCount,I,J;
         string sDay;
         DateTime dTemp;
         string[] arrDate;
         if (e.Row.RowType == DataControlRowType.Header)
         {
             iCount=10;
             iDay = 0;
             if (double.Parse(ViewState["ShowDays"].ToString()) == 1.10)
             { iDay = 1; }
             if (double.Parse(ViewState["ShowDays"].ToString()) == 11.20)
             { iDay = 11; }
             if (double.Parse(ViewState["ShowDays"].ToString()) == 21.31)
             {
                 iDay = 21;
                 iCount = clGeneral.GetEndMonthFromStringMonthYear(1, ddlMonth.SelectedValue).Day - 20;
             }
             for (I = 0; I < iCount; I++)
             {
                 e.Row.Cells[COL_YOM1 + I].Style["text-align"] = "center";
                   arrDate = ddlMonth.SelectedValue.Split(char.Parse("/"));
                   dTemp = new DateTime(int.Parse(arrDate[1].ToString()), int.Parse(arrDate[0].ToString()), (I + iDay));
                 sDay = clGeneral.arrDays[(int)(dTemp.DayOfWeek)];
                 e.Row.Cells[COL_YOM1 + I].Text = (I + iDay).ToString() + "<br/>  " + sDay;
                 if (LoginUser.IsLimitedUser)
                 {
                     e.Row.Cells[COL_YOM1 + I].Attributes.Add("onClick", "javascript:window.location.href='WorkCard.aspx?EmpID=" + txtEmpId.Text + "&WCardDate=" + (I + iDay).ToString().PadLeft(2, (char)48) + "/" + ddlMonth.SelectedValue + "&Page=2&dt=" + DateTime.Now + "';");
                 }
                 else
                 {
                     e.Row.Cells[COL_YOM1 + I].Attributes.Add("onClick", "javascript:OpenEmpWorkCard(" + txtEmpId.Text + ",'" + (I + iDay).ToString().PadLeft(2, (char)48) + "/" + ddlMonth.SelectedValue + "');");
                 }
                 e.Row.Cells[COL_YOM1 + I].Style.Add("cursor", "pointer");
                 e.Row.Cells[COL_YOM1 + I].Style.Add("text-decoration", "underline");
             }
           /* if (!string.IsNullOrEmpty(((DataTable)Session["dtRikuz1To10"]).Compute("sum(KIZUZ_MEAL_MICHSA)","").ToString()))
             {
                 if (int.Parse(ViewState["KodSugChishuv"].ToString()) != clGeneral.enSugChishiuv.Patuch.GetHashCode())
                 {
                     KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                     if (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates || txtEmpId.Text.Trim()==LoginUser.UserInfo.EmployeeNumber)
                     {
                         e.Row.Cells[COL_KIZUZ_MEAL_MICHSA].Attributes.Add("onclick", "var OpenWin=window.showModalDialog('BakashatTashlum.aspx?Dt=Date()&MisparIshi=" + txtEmpId.Text + "&Taarich=" + ViewState["Taarich"] + "&BakashaId=" + ViewState["BakashId"] + "&UserId=" + ViewState["UserId"] + "&Tzuga=0" + "','bakasha','dialogHeight:480px;dialogWidth:470px;status:no;scroll:no;');");
                         e.Row.Cells[COL_KIZUZ_MEAL_MICHSA].Style.Add("cursor", "pointer");
                         e.Row.Cells[COL_KIZUZ_MEAL_MICHSA].Style.Add("text-decoration", "underline");
                     }
                 }
             } */
         }
         if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow )
         {
             if (double.Parse(ViewState["ShowDays"].ToString()) == 21.31)
             {
                 for (J = clGeneral.GetEndMonthFromStringMonthYear(1, ddlMonth.SelectedValue).Day + 1; J < 32; J++)
                 {
                     e.Row.Cells[COL_YOM1+J - 21].Style.Add("display", "none");
                 }
             }
         }
         if (e.Row.RowType != DataControlRowType.EmptyDataRow && !(bool.Parse(ViewState["ShowYom11"].ToString())))
         {
             e.Row.Cells[COL_YOM11].Style["Display"] = "none";
         }
     }


     protected void Day11to20_Click(object sender, ImageClickEventArgs e)
     {
         if (Session["dtRikuz11to20"].Equals(null))
         { GetRikuzChodshi(float.Parse(ViewState["fErechRechiv45"].ToString())); }

         ViewState["ShowYom11"] = false;
         ViewState["ShowDays"] = 11.20;
        
         grdTotalMonthly.DataSource = Session["dtRikuz11to20"];
         grdTotalMonthly.DataBind();
     }

     private void GetRikuzChodshi(float fErechRechiv45)
     {
         DataTable dtRikuz1To10, dtRikuz11To20, dtRikuz21To31;
         clOvdim oOvdim = new clOvdim();
         dtRikuz1To10 = new DataTable();
         dtRikuz11To20 = new DataTable();
         dtRikuz21To31 = new DataTable();
         DateTime dTaarich;
         try
         {
             Session["dtRikuz1To10"] = null;
             Session["dtRikuz11To20"] = null;
             Session["dtRikuz21To31"] = null;
             dTaarich = DateTime.Parse(ViewState["Taarich"].ToString());
           
             oOvdim.GetRikuzChodshi(int.Parse(txtEmpId.Text), dTaarich, long.Parse(ViewState["BakashId"].ToString()),1,fErechRechiv45, ref  dtRikuz1To10, ref  dtRikuz11To20, ref  dtRikuz21To31);
             Session["dtRikuz1To10"] = dtRikuz1To10;
             Session["dtRikuz11To20"] = dtRikuz11To20;
             Session["dtRikuz21To31"] = dtRikuz21To31;
            
         }
         catch (Exception ex)
         {
             clGeneral.BuildError(Page, ex.Message);
         }
     }

     protected void Day21to31_Click(object sender, ImageClickEventArgs e)
     {
         ViewState["ShowYom11"] = false;
         if (clGeneral.GetEndMonthFromStringMonthYear(1, ddlMonth.SelectedValue).Day == 31)
         { ViewState["ShowYom11"] = true; }
         ViewState["ShowDays"] = 21.31;
         grdTotalMonthly.DataSource = Session["dtRikuz21to31"];
         grdTotalMonthly.DataBind();
     }

     protected void Day1to10_Click(object sender, ImageClickEventArgs e)
     {
         ViewState["ShowYom11"] = false;
         ViewState["ShowDays"] = 1.10;
         grdTotalMonthly.DataSource = Session["dtRikuz1To10"];
         grdTotalMonthly.DataBind();
     }

     protected void btnCalc_Click(object sender, EventArgs e)
     {
         DateTime dTaarich;
         DataTable  dtHeadrut= new DataTable();
         DataTable dtRechivimChodshiym= new DataTable();;
         long iBakashaId;
         DataTable dtRikuz1To10 = new DataTable();
         DataTable dtRikuz11To20 = new DataTable();
         DataTable dtRikuz21To31 = new DataTable();
         DataTable dsRikuzAll = new DataTable();
         try
         {
             dTaarich = DateTime.Parse(ViewState["Taarich"].ToString());
             iBakashaId=long.Parse(ViewState["BakashId"].ToString());
             clOvdim oOvdim = new clOvdim();

             clCalculation objCalc = new clCalculation();
             objCalc.MainCalcOved(int.Parse(txtEmpId.Text), 0, dTaarich,1,ref   dtHeadrut, ref dtRechivimChodshiym, ref dtRikuz1To10,ref  dtRikuz11To20,ref dtRikuz21To31,ref dsRikuzAll);

             //MainCalc objMainCalc = new MainCalc();
             //objMainCalc.MainCalcOved(int.Parse(txtEmpId.Text), 0, dTaarich, 1, ref   dtHeadrut, ref dtRechivimChodshiym, ref dtRikuz1To10, ref  dtRikuz11To20, ref dtRikuz21To31, ref dsRikuzAll);
             
             grdAbsenceData.DataSource = dtHeadrut;
             grdAbsenceData.DataBind();

             grdMonthlyComponents.DataSource = dtRechivimChodshiym;
             grdMonthlyComponents.DataBind();

             Session["dtRikuz1To10"] = null;
             Session["dtRikuz11To20"] = null;
             Session["dtRikuz21To31"] = null;
           
             Session["dtRikuz1To10"] = dtRikuz1To10;
             Session["dtRikuz11To20"] = dtRikuz11To20;
             Session["dtRikuz21To31"] = dtRikuz21To31;

             ViewState["ShowYom11"] = false;
             ViewState["ShowDays"] = 1.10;
             grdTotalMonthly.DataSource = Session["dtRikuz1to10"];
             grdTotalMonthly.DataBind();
         }
         catch (Exception ex)
         { clGeneral.BuildError(Page, ex.Message); }
          
     }

     protected void btnHidden_OnClick(object sender, EventArgs e)
     {
         LoadDdlMonth();
         divNetunim.Visible = false;
         btnPrint.Enabled = false;
         btnPrint.CssClass = "ImgButtonSearchDisable";
         if (rdoId.Checked)
         {
             txtName.Enabled = false;
             txtEmpId.Enabled = true;
         }
         else
         {
             txtName.Enabled = true;
             txtEmpId.Enabled = false;
         }
     }

     protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
     {
         Session["dtRikuz1To10"] = null;
         Session["dtRikuz11To20"] = null;
         Session["dtRikuz21To31"] = null;
         ViewState["BakashId"] = null;
         divNetunim.Visible = false;
         btnPrint.Enabled = false;
         btnPrint.CssClass = "ImgButtonSearchDisable";
     }
}

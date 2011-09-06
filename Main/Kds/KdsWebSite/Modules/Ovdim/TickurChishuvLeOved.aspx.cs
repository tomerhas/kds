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

public partial class Modules_Ovdim_TickurChishuvLeOved : KdsPage
{
    public const int COL_YOM1 = 5;
    public const int COL_YOM11 = 15;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MasterPage mp = (MasterPage)Page.Master;
            mp.ImageExcelClick.Click += new ImageClickEventHandler(ImageExcelClick_Click);
            mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);
            ((ScriptManager)mp.FindControl("ScriptManagerKds")).AsyncPostBackTimeout = 360;

            if (!Page.IsPostBack)
            {
                PageHeader = "תחקור חישוב לעובד";
                DateHeader = DateTime.Today.ToShortDateString();
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";

                btnCalc.Style.Add("Display", "None");

                LoadMessages((DataList)Master.FindControl("lstMessages"));

                divNetunim.Visible = false;

                SetFixedHeaderGrid(pnlTotalMonthly.ClientID, mp.HeadPage);
                SetFixedHeaderGrid(pnlMonthlyComponents.ClientID, mp.HeadPage);

               if (ConfigurationManager.AppSettings["ShowRunError"]=="false")
                   btnInputData.Visible=false;

                rdoId.Attributes.Add("onclick", "SetTextBox();");
                rdoName.Attributes.Add("onclick", "SetTextBox();");

              
                AutoCompleteExtenderByName.ContextKey = "";
                AutoCompleteExtenderID.ContextKey = "";
           
                txtName.Enabled = false;

                Session["dtRikuz1To10"] = null;
                Session["dtRikuz11To20"] = null;
                Session["dtRikuz21To31"] = null;

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
        //DataTable dtMonth;
         clOvdim oOvdim = new clOvdim();
         DataTable dtParametrim = new DataTable();
         clUtils oUtils = new clUtils();
         if (txtEmpId.Text.Length > 0 && clGeneral.IsNumeric(txtEmpId.Text))
         {
             dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
             clGeneral.LoadDateCombo(ddlMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
             //clGeneral.LoadDateCombo(ddlMonth, 11);
             //dtMonth = oOvdim.GetMonthsToOved(int.Parse(txtEmpId.Text));
             //ddlMonth.DataSource = dtMonth;
             //ddlMonth.DataBind();
             ddlMonth.Enabled = true;
         }
         else { ddlMonth.Enabled = false; }
         LoadDdlRitzotChishuv();
       
    }

     void LoadDdlRitzotChishuv()
     {
         DataTable dtRitzot;
         ListItem Item;
         DateTime dTaarich;
         string[] arrDate;
         clOvdim oOvdim = new clOvdim();
         if (txtEmpId.Text.Length > 0 && clGeneral.IsNumeric(txtEmpId.Text) && ddlMonth.SelectedValue!="")
         {
             arrDate = (string[])ddlMonth.SelectedValue.ToString().Split(char.Parse("/"));

             dTaarich = new DateTime(int.Parse(arrDate[1].ToString()), int.Parse(arrDate[0].ToString()), 1);
             
              dtRitzot = oOvdim.GetRitzotChishuv(int.Parse(txtEmpId.Text), dTaarich);
              ddlRitzatChishuv.DataSource = dtRitzot;
              ddlRitzatChishuv.DataBind();
              ddlRitzatChishuv.Enabled = true;

              Item = new ListItem();
              Item.Text = "חישוב מקוון";
              Item.Value="0";
              ddlRitzatChishuv.Items.Add(Item);
         }
         else { ddlRitzatChishuv.Enabled = false; }
     }

     void ImagePrintClick_Click(object sender, ImageClickEventArgs e)
     {
         byte[] s;
         string sScript;
         ReportModule Report = ReportModule.GetInstance();

         if (Page.IsValid)
         {
             if (Session["dtRikuz1To10"] != null)
             {
                 if (ddlRitzatChishuv.SelectedItem.Text.LastIndexOf("-") == -1)
                 {
                     Report.AddParameter("P_MISPAR_ISHI", txtEmpId.Text);
                     Report.AddParameter("P_TAARICH", ViewState["Taarich"].ToString());
                     Report.AddParameter("P_Tar_chishuv", ViewState["TarChishuv"].ToString());
                     Report.AddParameter("P_sug_chishuv", ViewState["SugChishuv"].ToString());
                     Report.AddParameter("P_Oved_5_Yamim", ViewState["WorkDay"].ToString());
                     Report.AddParameter("P_SIKUM_CHODSHI", "0");
                     Report.AddParameter("P_DT_RIKUZ", ((DataSet)Session["dsRikuz"]));
                    
                     s = Report.CreateReport("/KdsReports/RikuzAvodaChodshiOnLine", eFormat.PDF, true);

                     Session["BinaryResult"] = s;
                     Session["TypeReport"] = "PDF";
                     Session["FileName"] = "RikuzAvodaChodshi";

                     sScript = "window.showModalDialog('../../modalshowprint.aspx','','dialogwidth:800px;dialogheight:750px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;');";
                     MasterPage mp = (MasterPage)Page.Master;

                     ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintPdf", sScript, true);

                 }
                 else if (ViewState["BakashId"] != null)
                 {

                     Report.AddParameter("P_MISPAR_ISHI", txtEmpId.Text);
                     Report.AddParameter("P_TAARICH", ViewState["Taarich"].ToString());
                     Report.AddParameter("P_BAKASHA_ID", ViewState["BakashId"].ToString());
                     Report.AddParameter("P_Tar_chishuv", ViewState["TarChishuv"].ToString());
                     Report.AddParameter("P_sug_chishuv", ViewState["SugChishuv"].ToString());
                     Report.AddParameter("P_Oved_5_Yamim", ViewState["WorkDay"].ToString());
                     Report.AddParameter("P_SIKUM_CHODSHI", "0");

                     s = Report.CreateReport("/KdsReports/RikuzAvodaChodshi", eFormat.PDF, true);

                     Session["BinaryResult"] = s;
                     Session["TypeReport"] = "PDF";
                     Session["FileName"] = "RikuzAvodaChodshi";

                     sScript = "window.showModalDialog('../../modalshowprint.aspx','','dialogwidth:800px;dialogheight:750px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;');";
                     MasterPage mp = (MasterPage)Page.Master;

                     ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintPdf", sScript, true);
                 }
             }
         }
     }

    void ImageExcelClick_Click(object sender, ImageClickEventArgs e)
    {
         byte[] s ;
         string sScript;
         ReportModule Report = ReportModule.GetInstance();

         if (Page.IsValid)
         {
             if (Session["dtRikuz1To10"] != null)
             {
                 if (ddlRitzatChishuv.SelectedItem.Text.LastIndexOf("-") == -1)
                 {
                     Report.AddParameter("P_MISPAR_ISHI", txtEmpId.Text);
                     Report.AddParameter("P_TAARICH", ViewState["Taarich"].ToString());
                     Report.AddParameter("P_Tar_chishuv", ViewState["TarChishuv"].ToString());
                     Report.AddParameter("P_sug_chishuv", ViewState["SugChishuv"].ToString());
                     Report.AddParameter("P_Oved_5_Yamim", ViewState["WorkDay"].ToString());
                     Report.AddParameter("P_SIKUM_CHODSHI", "0");
                     Report.AddParameter("P_DT_RIKUZ", ((DataSet)Session["dsRikuz"]));
                    
                     s = Report.CreateReport("/KdsReports/RikuzAvodaChodshiOnLine", eFormat.EXCEL, true);

                     Session["BinaryResult"] = s;
                     Session["TypeReport"] = "EXCEL";
                     Session["FileName"] = "RikuzAvodaChodshi";

                     sScript = "document.all.ctl00_KdsContent_iFramePrint.src='../../ShowPrint.aspx';";
                     MasterPage mp = (MasterPage)Page.Master;

                     ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintExcel", sScript, true);

                 }
                 else if (ViewState["BakashId"] != null)
                 {
                     Report.AddParameter("P_MISPAR_ISHI", txtEmpId.Text);
                     Report.AddParameter("P_TAARICH", ViewState["Taarich"].ToString());
                     Report.AddParameter("P_BAKASHA_ID", ViewState["BakashId"].ToString());
                     Report.AddParameter("P_Tar_chishuv", ViewState["TarChishuv"].ToString());
                     Report.AddParameter("P_sug_chishuv", ViewState["SugChishuv"].ToString());
                     Report.AddParameter("P_Oved_5_Yamim", ViewState["WorkDay"].ToString());
                     Report.AddParameter("P_SIKUM_CHODSHI", "0");
                     s = Report.CreateReport("/KdsReports/RikuzAvodaChodshi", eFormat.EXCEL, true);

                     Session["BinaryResult"] = s;
                     Session["TypeReport"] = "EXCEL";
                     Session["FileName"] = "RikuzAvodaChodshi";

                     sScript = "document.all.ctl00_KdsContent_iFramePrint.src='../../ShowPrint.aspx';";
                     MasterPage mp = (MasterPage)Page.Master;

                     ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintExcel", sScript, true);

                 }
             }
         }
    }

    protected void btnInputData_Click(object sender, EventArgs e)
    {
        string sError = "";
        DateTime dTaarich, dTarAd, dTarMe;
            bool nextStep = false;
            clOvdim oOvdim = new clOvdim();
            DataTable dtOvdim = new DataTable();
            DataRow[] drErrors;
            string sdate;
            try
            {
                dTaarich = clGeneral.GetDateTimeFromStringMonthYear(1, ddlMonth.SelectedValue);
                dTarMe = dTaarich;
                dTarAd = dTaarich.AddMonths(1).AddDays(-1);

                while (dTaarich <= dTarAd)
                {
                    clBatchManager btchMan = new clBatchManager(0);

                    try
                    {
                        nextStep = btchMan.MainInputData(int.Parse(txtEmpId.Text), dTaarich);

                        if (nextStep)
                        {
                            nextStep = btchMan.MainOvedErrors(int.Parse(txtEmpId.Text), dTaarich);
                        }
                        //else
                        //{
                        //    if (sError.Length > 0) sError += ", ";
                        //    sError += dTaarich.ToString("dd/MM/yyyy");
                        //}
                        dTaarich = dTaarich.AddDays(1);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        btchMan.Dispose();
                    }
                }

                dtOvdim = oOvdim.GetOvedErrorsCards(int.Parse(txtEmpId.Text), dTarMe, dTarAd);
                drErrors = dtOvdim.Select("status_card=0","taarich asc");
                if (drErrors.Length>0)
                    sError = DateTime.Parse(drErrors[0]["taarich"].ToString()).ToShortDateString();
                for (int i = 1; i < drErrors.Length; i++)
                {
                    sdate = DateTime.Parse(drErrors[i]["taarich"].ToString()).ToShortDateString();
                    if (sError.IndexOf(sdate)==-1)
                        sError += ", " + sdate;
                }

                if (sError.Length > 0)
                    ScriptManager.RegisterStartupScript(btnInputData, btnInputData.GetType(), "Err", "alert('ישנם תאריכים שגויים בחודש: " + sError + "');", true);
                else ScriptManager.RegisterStartupScript(btnInputData, btnInputData.GetType(), "Err", "alert('הרצת שינויי קלט ושגויים הסתיימה בהצלחה');", true);
               
            }
            catch (Exception ex)
            {
                clGeneral.BuildError(Page, ex.Message);
            }
           
    }
   
    protected void btnShow_Click(object sender, EventArgs e)
    {
        clOvdim oOvdim = new clOvdim();
        DateTime dTaarich, dTarChishuv;
        DataTable dtPirteyOved, dtHeadrut, dtRechivimChodshiym;
        string sWorkDay="";
        int iInstrStr;
       
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
                    if (ddlRitzatChishuv.SelectedItem.Text.LastIndexOf("-") == -1)
                    {
                        lblCalcType.Text = "מקוון";
                        lblTarChishuv.Text = DateTime.Today.ToShortDateString();
                        Session["dtRikuz1To10"] = null;
                        Session["dtRikuz11To20"] = null;
                        Session["dtRikuz21To31"] = null;

                        ScriptManager.RegisterStartupScript(btnShow, this.GetType(), "RunCalc", "document.getElementById('ctl00_KdsContent_btnShow').disabled=true;document.getElementById('divProgress').style.display='none';document.getElementById('DivCalc').style.display='block';document.getElementById('ctl00_KdsContent_btnCalc').click();document.getElementById('divProgress').style.display='block';", true);
                        
                        grdTotalMonthly.DataSource = Session["dtRikuz1to10"];
                        grdTotalMonthly.DataBind();
                        grdMonthlyComponents.DataSource = null;
                        grdMonthlyComponents.DataBind();
                        grdHeadrut.DataSource = null;
                        grdHeadrut.DataBind();
                        grdMachala.DataSource = null;
                        grdMachala.DataBind();
                      }
                    else
                    {
                        iInstrStr = ddlRitzatChishuv.SelectedItem.Text.LastIndexOf("-");
                        lblCalcType.Text = ddlRitzatChishuv.SelectedItem.Text.Substring((iInstrStr + 2), ddlRitzatChishuv.SelectedItem.Text.Length - iInstrStr - 2).ToString();
                        iInstrStr = ddlRitzatChishuv.SelectedItem.Text.LastIndexOf("(");
                        lblTarChishuv.Text = ddlRitzatChishuv.SelectedItem.Text.Substring((iInstrStr + 1), 10).ToString();
                  
                    }

                   
                  
                    ViewState["BakashId"] = ddlRitzatChishuv.SelectedValue;
                    ViewState["SugChishuv"] = lblCalcType.Text;
                    ViewState["TarChishuv"] = dTarChishuv;
                    if (ddlRitzatChishuv.SelectedItem.Text.LastIndexOf("-") != -1)
                    {
                        dtHeadrut = oOvdim.GetPirteyHeadrut(int.Parse(txtEmpId.Text), dTaarich, long.Parse(ddlRitzatChishuv.SelectedValue.ToString()));
                        grdHeadrut.DataSource = dtHeadrut;
                        grdHeadrut.DataBind();
                        
                        grdMachala.DataSource = dtHeadrut;
                        grdMachala.DataBind();

                        dtRechivimChodshiym = oOvdim.GetRechivimChodshiyim(int.Parse(txtEmpId.Text), dTaarich, long.Parse(ddlRitzatChishuv.SelectedValue.ToString()), 2);
                        dtRechivimChodshiym = clGeneral.FilterDataTable(dtRechivimChodshiym, "LETZUGA_BETIHKUR_RASHEMET=2");
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

    private   float GetSumErechRechiv45(DataTable dtRechivimChodshiym)
    {
        object objRechiv45=null;

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
         string[] arrDate;
         DateTime dTemp;
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
                 e.Row.Cells[COL_YOM1 + I].Attributes.Add("onClick", "javascript:OpenEmpWorkCard(" + txtEmpId.Text + ",'" + (I + iDay).ToString().PadLeft(2, (char)48) + "/" + ddlMonth.SelectedValue + "');");
                 e.Row.Cells[COL_YOM1 + I].Style.Add("cursor", "pointer");
                 e.Row.Cells[COL_YOM1 + I].Style.Add("text-decoration", "underline");
             }
         }
         if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow )
         {
             if (double.Parse(ViewState["ShowDays"].ToString()) == 21.31)
             {
                 for (J = clGeneral.GetEndMonthFromStringMonthYear(1, ddlMonth.SelectedValue).Day+1; J < 32; J++)
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
         if (Session["dtRikuz11to20"]==null)
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
           
             oOvdim.GetRikuzChodshi(int.Parse(txtEmpId.Text), dTaarich, long.Parse(ViewState["BakashId"].ToString()),2,fErechRechiv45,ref  dtRikuz1To10, ref  dtRikuz11To20, ref  dtRikuz21To31);
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

    
     protected void txtEmpId_TextChanged(object sender, EventArgs e)
     {
         DataTable dt;
         string sOvedName="";
         clOvdim oOvdim = new clOvdim();

         if (rdoId.Checked)
         {
             if ((txtEmpId.Text).Length == 0)
             {
                 btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                 btnShow.Enabled = false;
                 txtName.Text = "";
             }
             else if (!(clGeneral.IsNumeric(txtEmpId.Text)))
             {
                 btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                 btnShow.Enabled = false;
                 txtName.Text = "";
                
                 ScriptManager.RegisterStartupScript(txtEmpId, this.GetType(), "errName", "alert('!מספר אישי לא חוקי');", true);

             }
             else
             {
                 if (AutoCompleteExtenderID.ContextKey.Length > 0)
                 {
                     dt = oOvdim.GetOvdimToUser(txtEmpId.Text, int.Parse(AutoCompleteExtenderID.ContextKey));
                     if (dt.Rows.Count > 0)
                     {
                         sOvedName = dt.Rows[0]["OVED_NAME"].ToString();
                     }
                 }
                 else
                 {
                     sOvedName = oOvdim.GetOvedFullName(clGeneral.GetIntegerValue(txtEmpId.Text));
                 }

                 if (sOvedName.Length > 0 && sOvedName!="null")
                 {
                     txtName.Text = sOvedName;
                     btnShow.ControlStyle.CssClass = "ImgButtonSearch";
                     btnShow.Enabled = true;
                 }
                 else
                 {
                     btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                     btnShow.Enabled = false;
                     txtName.Text = "";
                    
                     ScriptManager.RegisterStartupScript(txtEmpId, this.GetType(), "errName", "alert('!מספר אישי לא קיים');", true);

                 }
             }
             LoadDdlMonth();


             divNetunim.Visible = false;

             if (rdoId.Checked)
             {
                 txtName.Enabled = false;
             }
             txtEmpId.Enabled = true;
         }
     }

     protected void txtName_TextChanged(object sender, EventArgs e)
     {
         DataTable dt; 
         string sMisparIshi="";
         try
         {
             
             clOvdim oOvdim = new clOvdim();
             if (rdoName.Checked && (txtName.Text).Length > 0)
             {
                 if (txtName.Text.IndexOf("(") == -1)
                 {
                     if (AutoCompleteExtenderID.ContextKey.Length > 0)
                     {
                         dt = oOvdim.GetOvdimToUserByName(txtName.Text, int.Parse(AutoCompleteExtenderID.ContextKey));
                         if (dt.Rows.Count > 0)
                         {
                             sMisparIshi = dt.Rows[0]["MISPAR_ISHI"].ToString();
                         }
                     }
                     else
                     {
                         sMisparIshi = oOvdim.GetOvedMisparIshi(txtName.Text);
                     }
                 }
                 else
                 {
                     sMisparIshi = (txtName.Text.Substring(txtName.Text.IndexOf("(") + 1)).Replace(")", "");
                 }

                 if (sMisparIshi.Length > 0 && sMisparIshi != "null")
                 {
                     txtEmpId.Text = sMisparIshi;
                     btnShow.ControlStyle.CssClass = "ImgButtonSearch";
                     btnShow.Enabled = true;
                 }
                 else
                 {
                     btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                     btnShow.Enabled = false;
                     txtEmpId.Text = "";
                    
                     ScriptManager.RegisterStartupScript(txtName, this.GetType(), "errName", "alert('!שם לא קיים');", true);
                     }

             }
             if (rdoName.Checked && txtName.Text.Length>0)
             {
                 txtEmpId.Enabled = false;
             }
             txtName.Enabled = true;
             if ((txtName.Text).Length == 0)
             {
                 btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                 btnShow.Enabled = false;
                 txtEmpId.Text = "";
             }
             divNetunim.Visible = false;
             LoadDdlMonth();
        
         }
         catch (Exception ex)
         { clGeneral.BuildError(Page, ex.Message); }
          

     }
     protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
     {
         LoadDdlRitzotChishuv();
     }

     protected void btnCalc_Click(object sender, EventArgs e)
     {
         DateTime dTaarich;
         DataTable dtHeadrut = new DataTable();
         DataTable dtRechivimChodshiym = new DataTable(); ;
         long iBakashaId;
         DataTable dtRikuz1To10 = new DataTable();
         DataTable dtRikuz11To20 = new DataTable();
         DataTable dtRikuz21To31 = new DataTable();
         DataTable dtAllRikuz = new DataTable();
         DataSet dsRikuz = new DataSet();
 
         try
         {
             dTaarich = DateTime.Parse(ViewState["Taarich"].ToString());
             iBakashaId = long.Parse(ViewState["BakashId"].ToString());
             clOvdim oOvdim = new clOvdim();

             clCalculation objCalc = new clCalculation();
             objCalc.MainCalcOved(int.Parse(txtEmpId.Text), 0, dTaarich, 2, ref   dtHeadrut, ref dtRechivimChodshiym, ref dtRikuz1To10, ref  dtRikuz11To20, ref dtRikuz21To31, ref dtAllRikuz);


             //MainCalc objMainCalc = new MainCalc();
             //objMainCalc.MainCalcOved(int.Parse(txtEmpId.Text), 0, dTaarich, 2, ref   dtHeadrut, ref dtRechivimChodshiym, ref dtRikuz1To10, ref  dtRikuz11To20, ref dtRikuz21To31, ref dtAllRikuz);
             

             grdHeadrut.DataSource = dtHeadrut;
             grdHeadrut.DataBind();
             dtHeadrut.TableName = "Headruyot";
             dsRikuz.Tables.Add(dtHeadrut);
             dtAllRikuz.TableName = "Rikuz";
             dsRikuz.Tables.Add(dtAllRikuz);
             dtRechivimChodshiym.TableName = "Chodshi";
             dsRikuz.Tables.Add(dtRechivimChodshiym);
             Session.Add("dsRikuz", dsRikuz);
            
             grdMachala.DataSource = dtHeadrut;
             grdMachala.DataBind();

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
}

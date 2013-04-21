using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.BL;
using KdsLibrary.Security;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using KdsLibrary.Controls;
using KdsLibrary.UI.SystemManager;
using Microsoft.Reporting.WebForms;
using KdsLibrary.DAL;
//using Egged.WebCustomControls;




public partial class Modules_Reports_ReportFilters : KdsPage
{
    private KdsDynamicReport _KdsDynamicReport;
    private KdsReport _Report;
    private PanelFilters _PanelFilters;
    private List<string> _ControlsList;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtParametrim = new DataTable();
        clUtils oUtils = new clUtils();
        UserId.Text =LoginUser.UserInfo.EmployeeNumber;
        try
        {
            if (!Page.IsPostBack)
            {
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                LoadMessages((DataList)Master.FindControl("lstMessages"));
                LoadKdsDynamicReport();
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                Param100.Value = dtParametrim.Rows[0]["ERECH_PARAM"].ToString();

            }
            FillFilterToForm();
            FillEnabledFilter();
            BtControlChanged.Style.Add("Display", "none");
            SetSecurityLevel();
            InitializeEvents();

        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            InitializeByReport();
            List<KdsFilter> ListOfFilters = Report.FilterList;
            ListOfFilters.ForEach(delegate(KdsFilter Filter)
            {
                if (Filter.BoxeType == KdsBoxeType.ListBoxExtended)
                {
                    ListBoxExtended CurrentControl = (ListBoxExtended)TdFilter.FindControl(Filter.ParameterName);
                    CurrentControl.AddAttributes();
                    if ((!Page.IsPostBack) && ((Report.NameReport == ReportName.Presence) ||
                                               (Report.NameReport == ReportName.IshurimLerashemet)))
                        CurrentControl.SetDefaultValue(LoginUser.UserInfo.EmployeeNumber.ToString());
                    if (CurrentControl.ListOfValues != "")
                    {
                        CurrentControl.List.Items.Clear();
                        foreach (string Item in CurrentControl.ListOfValues.Split(','))
                            CurrentControl.List.Items.Add(Item);
                    }
                }
            });
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }

    private void InitializeEvents()
    {
        try
        {
            switch (Report.NameReport)
            {
                
                case ReportName.HashvaatRizotChishuv:
                case ReportName.HashvaatChodsheyRizotChishuv:
                    Ritza.SelectedIndexChanged += new EventHandler(ddlRitza_SelectedIndexChanged);
                    break;
                case ReportName.ChafifotSidureyNihulTnua: 
                case ReportName.ReportNesiotKfulot:
                    Region.SelectedIndexChanged += new EventHandler(ddlEzor_SelectedIndexChanged);
                    break;
                //case ReportName.FindWorkerCard:
                //case ReportName.Presence:
                //case ReportName.Average:
                //    ((TextBox)TdFilter.FindControl("P_STARTDATE")).TextChanged += new EventHandler(EndDate_OnChanged);
                //    break;
            }
            //if (TdFilter.FindControl("P_STARTDATE") != null && TdFilter.FindControl("P_STARTDATE").GetType().Name == "TextBox")
            //    ((TextBox)TdFilter.FindControl("P_STARTDATE")).TextChanged += new EventHandler(EndDate_OnChanged);
                 
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private void InitializeByReport()
    {
        clUtils oUtils = new clUtils();
        DataTable dtMisRashemet = new DataTable();
        try
        {
            switch (Report.NameReport)
            {
                case ReportName.DriverWithoutTacograph:
                case ReportName.DriverWithoutSignature:
                    if (!Page.IsPostBack)
                    {
                        Ezor.Enabled = ((PageModule.SecurityLevel == KdsSecurityLevel.ViewAll) ||
                            (PageModule.SecurityLevel == KdsSecurityLevel.ViewOnlyEmployeeData));
                        Ezor.SelectedIndex = Ezor.Items.IndexOf(Ezor.Items.FindByValue(RegionOfWorker.ToString()));
                    }
                    break;
                case ReportName.Presence:
                    if (!Page.IsPostBack)
                    {
                        CtrlStartDate = "01/" + DateTime.Now.ToString("MM/yyyy");
                        CtrlEndDate = DateTime.Now.ToString("dd/MM/yyyy");// DateTime.Parse("01/" + DateTime.Now.AddMonths(1).ToString("MM/yyyy")).AddDays(-1).ToString("dd/MM/yyyy");
                    }
                    SetWorkerViewLevel();
                    break;
                case ReportName.IshurimLerashemet:
                    if (!Page.IsPostBack)
                        CtrlStartDate = DateTime.Now.AddMonths(-14).ToString("dd/MM/yyyy");
                    SetWorkerViewLevel();
                    break;
                case ReportName.FindWorkerCard:
                    if (!Page.IsPostBack)
                        CtrlStartDate = DateTime.Now.AddMonths(-14).ToString("dd/MM/yyyy");
                    break;
                //case ReportName.HashvaatRizotChishuv:
                //case ReportName.HashvaatChodsheyRizotChishuv:
                //    Ritza.SelectedIndexChanged += new EventHandler(ddlRitza_SelectedIndexChanged);
                //    break;
                case ReportName.IdkuneyRashemet:
                    Auto_P_MIS_RASHEMET.ContextKey = "6,0133," + CtrlTaarichCa.ToShortDateString(); //kod_natun=6; Erech=0133=רשם
                    Shaa.Items[0].Text = "00:01";
                    Shaa.Items[0].Value = "00:01";
                    dtMisRashemet = oUtils.getMispareiRashamot(6, "0133", CtrlTaarichCa, "");
                    if (dtMisRashemet.Rows.Count > 0)
                    {
                        MisRashamot.Value = ",";
                        for (int i = 0; i < dtMisRashemet.Rows.Count; i++)
                            MisRashamot.Value = MisRashamot.Value + dtMisRashemet.Rows[i]["MISPAR_ISHI"].ToString() + ",";
                    }

                    break;
                case ReportName.HityazvutBePundakimHitchashbenut:
                case ReportName.HityazvutBePundakimTlunot:
                case ReportName.HityazvutBePundakimKalkalit:
                case ReportName.ChafifotSidureyNihulTnua:
                case ReportName.ReportNesiotKfulot:
                    if (!Page.IsPostBack)
                    {
                        CtrlStartDate = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year).ToString("dd/MM/yyyy");
                        CtrlEndDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    break;
                
                    //if (!Page.IsPostBack)
                    //{
                    //    CtrlStartDate = DateTime.Parse("01/" + DateTime.Now.AddMonths(-1).Month + "/" + DateTime.Now.Year).ToString("dd/MM/yyyy");
                    //    CtrlEndDate = DateTime.Parse(CtrlStartDate).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
                    //}
                    ////Region.SelectedIndexChanged += new EventHandler(ddlEzor_SelectedIndexChanged);

                    break;
                //case ReportName.IdkuneyRashemetMasach4:
                //    Auto_P_MIS_RASHEMET.ContextKey = "6,0133," + CtrlTaarichCa.ToShortDateString(); //kod_natun=6; Erech=0133=רשם
                //    Shaa.Items[0].Text = "00:01";
                //    Shaa.Items[0].Value = "00:01";
                //    dtMisRashemet = oUtils.getMispareiRashamot(6, "0133", CtrlTaarichCa, "");
                //    if (dtMisRashemet.Rows.Count > 0)
                //    {
                //        MisRashamot.Value = ",";
                //        for (int i = 0; i < dtMisRashemet.Rows.Count; i++)
                //            MisRashamot.Value = MisRashamot.Value + dtMisRashemet.Rows[i]["MISPAR_ISHI"].ToString() + ",";
                //    }

                //    break;
                    
            }
            SetAutoCompleteExtender();
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }

    private void SetWorkerViewLevel()
    {
        if (!Page.IsPostBack)
            WorkerViewLevel.Items[0].Selected = true;
        if (WorkerViewLevel.Items.Count < 2)
        {
            WorkerViewLevel.Style.Add("Display", "none");
            WorkerViewLevelLabel.Style.Add("Display", "none");
        }
    }
    private void ddlRitza_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        clReport oReport = clReport.GetInstance();
        string riza = clGeneral.GetControlValue(Ritza).ToString();
        string rizaLeHashvaa = clGeneral.GetControlValue(RitzaLeHashvaa).ToString();
        if (riza != "-1")
        {
            if (Report.NameReport == ReportName.HashvaatRizotChishuv)
                dt = oReport.GetRizotChishuvSucceeded((string)(clGeneral.GetControlValue(Chodesh)));
            else if (Report.NameReport == ReportName.HashvaatChodsheyRizotChishuv)
                dt =  oReport.GetRizotChishuvLechodeshSucceeded((string)(clGeneral.GetControlValue(ChodeshLehashvaa)));

            RitzaLeHashvaa.Items.Clear();
            RitzaLeHashvaa.DataSource = dt;
            RitzaLeHashvaa.DataBind();
            for (int i = 0; i < RitzaLeHashvaa.Items.Count; i++)
            {
                if (RitzaLeHashvaa.Items[i].Value.ToString() == riza)
                    RitzaLeHashvaa.Items.RemoveAt(i);
            }
            if (riza != rizaLeHashvaa)
            {
                RitzaLeHashvaa.SelectedValue = rizaLeHashvaa;
            }
        }
    }

    private void EndDate_OnChanged(object sender, EventArgs e)
    {
        DateTime tarMe;
        try
        {
            if (CtrlStartDate != "")
            {
                tarMe =  DateTime.Parse(CtrlStartDate);
                if (tarMe >= DateTime.Parse("01/" + DateTime.Now.ToString("MM/yyyy")))
                    CtrlEndDate =  DateTime.Now.ToString("dd/MM/yyyy");
              //  else CtrlEndDate = DateTime.Parse("01/" + tarMe.ToString("MM/yyyy")).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private void ddlEzor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        clReport oReport = clReport.GetInstance();
        int p_ezor = int.Parse(clGeneral.GetControlValue(Region).ToString());

        dt = oReport.GetSnifimByEzor(p_ezor);

        Snif.Items.Clear();
        Snif.DataSource = dt;
        Snif.DataBind();

    }

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        string sScript;

        try
        {

            if (Page.IsValid)
            {
                PrepareReportParameters();
                if (Report.ProductionType == ProductionType.Normal)
                    sScript = "window.showModalDialog('ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + RdlName + "','','dialogwidth:1200px;dialogheight:800px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
                else // ProductionType.Heavy
                {
                    //Response.Redirect("BackgroundReport.aspx?Dt=" + DateTime.Now.ToString() + "&UserId=" + LoginUser.UserInfo.EmployeeNumber + "&ReportName=" + RdlName);
                    sScript = "window.showModalDialog('BackgroundReport.aspx?Dt=" + DateTime.Now.ToString() + "&UserId=" + LoginUser.UserInfo.EmployeeNumber + "&ReportName=" + RdlName + "&PageHeader=" + HttpUtility.UrlEncodeUnicode(Report.PageHeader) + "','','dialogwidth:450px;dialogheight:200px;center:yes;status:no;resizable:no;scroll:no;');";
                }
                ScriptManager.RegisterStartupScript(btnDisplay, btnDisplay.GetType(), "ReportViewer", sScript, true);
            }
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }

    }


    protected void BtControlChanged_Click(object sender, EventArgs e)
    {
        SetAutoCompleteExtender();
    }

    private DataTable GetDtOfListOfElement()
    {
        DataTable dt = null;
        clOvdim oOvdim = new clOvdim();
        clUtils oUtils = new clUtils();
        clReport oReport = clReport.GetInstance();
        switch (Report.NameReport)
        {
            case ReportName.HashvaatRizotChishuv: //m ??
                dt = oUtils.GetOvdimLeRitza(clGeneral.GetIntegerValue(clGeneral.GetControlValue(Ritza).ToString()), (string)clGeneral.GetControlValue(Maamad), (string)clGeneral.GetControlValue(Isuk), "");
                break;
            case ReportName.Average:
                dt = oReport.GetIdOfYameyAvoda(DateTime.Parse(EndMonth.ToString("dd/MM/yyyy")).AddDays(1).AddMonths(-7),EndMonth, (string)clGeneral.GetControlValue(CompanyId));
                break;
            case ReportName.HityazvutBePundakimTlunot:
            case ReportName.HityazvutBePundakimKalkalit:
                dt = oReport.GetOvdim(DateTime.Parse(CtrlStartDate),DateTime.Parse(CtrlEndDate));
                break;
        }
        return dt;
    }


    private void SetSecurityLevel()
    {
        if (WorkerID != null )   
            WorkerID.ContextKey = "";
//        if (MisparIshi != null)
//            MisparIshi.ContextKey = ""; 
        if ((PageModule.SecurityLevel == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (PageModule.SecurityLevel == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
        {
            MisparIshi.ContextKey = LoginUser.UserInfo.EmployeeNumber;
        }

    }

    private void LoadKdsDynamicReport()
    {
        _KdsDynamicReport = KdsDynamicReport.GetKdsReport();
        _Report = new KdsReport();
        _Report = _KdsDynamicReport.FindReport(RdlName);
        Session["Report"] = _Report;
        Session["Resources"] = (KdsSysManResources)_KdsDynamicReport.Resources;
        
    }
    private KdsReport Report
    {
        get
        {
            if (Session["Report"] == null)
                LoadKdsDynamicReport();
            _Report = (KdsReport)Session["Report"];
            return _Report;
        }
    }
    private KdsSysManResources Resources
    {
        get
        {
            if (Session["Report"] == null)
                LoadKdsDynamicReport();
            return (KdsSysManResources)Session["Resources"];
        }
    }


    private void SetAutoCompleteExtender()
    {
        try
        {
            switch (Report.NameReport)
            {
                case ReportName.FindWorkerCard:
                    Makat.ContextKey = CtrlStartDate + ";" + CtrlEndDate;
                    SidurNumber.ContextKey = CtrlStartDate + ";" + CtrlEndDate;
                    WorkerID.ContextKey = CtrlStartDate + ";" + CtrlEndDate;
                    break;
                case ReportName.Presence:
                case ReportName.IshurimLerashemet:
                    MisparIshi.ContextKey = WorkerViewLevel.SelectedValue + "," + LoginUser.UserInfo.EmployeeNumber + ","
                        + CtrlStartDate + "," + CtrlEndDate;
                    break;
                case ReportName.PremiotPresence:
                    MisparIshi.ContextKey = (string)clGeneral.GetControlValue(Premia) + "," + (string)clGeneral.GetControlValue(Period);
                    break;
                case ReportName.HashvaatRizotChishuv: //m
                    MisparIshi.ContextKey = (string)clGeneral.GetControlValue(Ritza) + ";" +
                                                                 (string)clGeneral.GetControlValue(Maamad) + ";" +
                                                                 (string)clGeneral.GetControlValue(Isuk);
                    break;
                case ReportName.Average:
                    MisparIshi.ContextKey = DateTime.Parse(EndMonth.ToString("dd/MM/yyyy")).AddDays(1).AddMonths(-6).ToShortDateString() + ";" + // StartMonth.ToString("dd/MM/yyyy") + ";" +
                                                                 EndMonth.ToString("dd/MM/yyyy") + ";" +
                                                                    clGeneral.GetControlValue(CompanyId);
                    break;
                case ReportName.HityazvutBePundakimTlunot:
                case ReportName.HityazvutBePundakimKalkalit:
                    MisparIshi.ContextKey = CtrlStartDate + ";" + CtrlEndDate;
                    break;
                case ReportName.IdkuneyRashemet:
                    Auto_P_MIS_RASHEMET.ContextKey = "6,0133," + CtrlTaarichCa.ToShortDateString();
                    break;
            }
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }

    }

    private void FillEnabledFilter()
    {
        if (Report.FilterList.Count == 0)
            DivDynamicFilter.Style.Add("Display", "none");
    }

    private void FillFilterToForm()
    {
        try
        {
                PageHeader = Report.PageHeader;
                _PanelFilters = new PanelFilters(Report, Resources, ScriptManager);
                Session["PanelFilters"] = _PanelFilters;
                TdFilter.Controls.Add(_PanelFilters);
                _PanelFilters.FillControls();
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    #region Private Properties

    private int RegionOfWorker
    {
        get
        {
            int region = 0;
            DataTable Dt = new DataTable();
            clOvdim oOvdim = new clOvdim();
            Dt = oOvdim.GetOvedDetails(clGeneral.GetIntegerValue(LoginUser.UserInfo.EmployeeNumber), DateTime.Now);
            region = (Dt.Rows.Count != 0) ? clGeneral.GetIntegerValue(Dt.Rows[0]["KOD_EZOR"].ToString()) : 1;
            return region;
        }
    }

    private string RdlName
    {
        get { return (Request.QueryString["RdlName"] == null) ? "Sample1" : Request.QueryString["RdlName"].ToString(); }
    }

    private Label WorkerViewLevelLabel
    {
        get { return (Label)TdFilter.FindControl("LBL_P_WORKERVIEWLEVEL"); }
    }

    private RadioButtonList WorkerViewLevel
    {
        get { return (RadioButtonList)TdFilter.FindControl("P_WORKERVIEWLEVEL"); }
    }
    private ListBoxExtended MisparIshi
    {
        get { return (ListBoxExtended)TdFilter.FindControl("P_MISPAR_ISHI"); }
    }

    private DateTime StartMonth
    {
        get
        {
            return DateTime.ParseExact("01/" + clGeneral.GetControlValue(TdFilter.FindControl("P_STARTDATE")), "dd/MM/yyyy", null);
        }
    }
    private DateTime EndMonth
    {
        get
        {
            return DateTime.ParseExact("01/" + clGeneral.GetControlValue(TdFilter.FindControl("P_ENDDATE")), "dd/MM/yyyy", null).AddMonths(1).AddDays(-1);
        }
    }
    private ListBoxExtended WorkerID 
    {
        get { return (ListBoxExtended)TdFilter.FindControl("P_WORKERID"); }
    }
        
    private ListBoxExtended SidurNumber
    {
        get { return (ListBoxExtended)TdFilter.FindControl("P_SIDURNUMBER"); }
    }

    private ListBoxExtended Makat 
    {
        get { return (ListBoxExtended)TdFilter.FindControl("P_MAKAT"); }
    }

    //private ListBoxExtended RishuyRechev
    //{
    //    get { return (ListBoxExtended)TdFilter.FindControl("P_RISHUYCAR"); }
    //}
    private DropDownList Premia
    {
        get { return (DropDownList)TdFilter.FindControl("P_KOD_PREMIA"); }
    }
    private DropDownList Period
    {
        get { return (DropDownList)TdFilter.FindControl("P_PERIOD"); }
    }
    private DateTime CtrlTaarichCa
    {
        get
        {
            string Day = ((TextBox)TdFilter.FindControl("P_TAARICH_CA")).Text;
            return DateTime.Parse(Day);
        }
    }

    private string CtrlStartDate
    {
        get
        {
            return ((TextBox)TdFilter.FindControl("P_STARTDATE")).Text  ;
        }
        set
        {
            ((TextBox)TdFilter.FindControl("P_STARTDATE")).Text = value; 
        }
    }
    private string CtrlEndDate
    {
        get
        {
            return ((TextBox)TdFilter.FindControl("P_ENDDATE")).Text;
        }
        set
        {
            ((TextBox)TdFilter.FindControl("P_ENDDATE")).Text = value;
        }
    }

    private DropDownList Chodesh
    {
        get { return (DropDownList)TdFilter.FindControl("P_CHODESH"); }
    }

    private DropDownList Ritza
    {
        get { return (DropDownList)TdFilter.FindControl("P_MIS_RITZA"); }
    }
    private Label lblRitza
    {
        get { return (Label)TdFilter.FindControl("LBL_P_MIS_RITZA"); }
    }
    private DropDownList RitzaLeHashvaa
    {
        get { return (DropDownList)TdFilter.FindControl("P_MIS_RITZA_LEHASVAA"); }
    }
    private Label lblRitzaLeHashvaa
    {
        get { return (Label)TdFilter.FindControl("LBL_P_MIS_RITZA_LEHASVAA"); }
    }
    private ListBox Maamad
    {
        get { return (ListBox)TdFilter.FindControl("P_MAMAD"); }
    }
    private ListBox Isuk
    {
        get { return (ListBox)TdFilter.FindControl("P_ISUK"); }
    }
    private DropDownList ChodeshLehashvaa
    {
        get { return (DropDownList)TdFilter.FindControl("P_CHODESH_LEHASHVAA"); }
    }
    private CheckBoxList Ezor
    {
        get { return (CheckBoxList)TdFilter.FindControl("P_EZOR"); }
    }
    private ListBox Snif
    {
        get { return (ListBox)TdFilter.FindControl("P_SNIF"); }
    }
    private DropDownList Region
    {
        get { return (DropDownList)TdFilter.FindControl("P_EZOR"); }
    }
    private DropDownList CompanyId
    {
        get { return (DropDownList)TdFilter.FindControl("P_COMPANYID"); }
    }

    private DropDownList Shaa
    {
        get { return (DropDownList)TdFilter.FindControl("P_SHAA"); }
    }
    private ListBox Kod_Yechida
    {
        get { return (ListBox)TdFilter.FindControl("P_KOD_YECHIDA"); }
    }
    


    private AjaxControlToolkit.AutoCompleteExtender Auto_P_MIS_RASHEMET
    {
        get { return (AjaxControlToolkit.AutoCompleteExtender)TdFilter.FindControl("AutoComplete_P_MIS_RASHEMET"); }
    }
    #endregion

    private void PrepareReportParameters()
    {
        Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
        Control FilterControl = new Control();
        try
        {
            //_Report = (KdsReport)Session["Report"];
            _PanelFilters = (PanelFilters)Session["PanelFilters"];
            _ControlsList = _PanelFilters.ControlsBoxes;
            _ControlsList.ForEach(delegate(string NameId)
            {
                FilterControl = TdFilter.FindControl(NameId);
                ReportParameters.Add(NameId, (string)clGeneral.GetControlValue(FilterControl));
            });
            AddSpecificReportParameters(Report, ref ReportParameters);
            Session["ReportParameters"] = ReportParameters;
        }
        catch (Exception ex)
        {
            KdsLibrary.clGeneral.BuildError(Page, ex.Message, true);
        }
    }
    private void AddSpecificReportParameters(KdsReport rpt, ref Dictionary<string, string> Params)
    {
        switch (rpt.NameReport)
        {
            case ReportName.DisregardDrivers:
                Params.Add("P_DISREGARDTYPE", "-1");
                break;
            case ReportName.DisregardDriversVisot:
                Params.Add("P_DISREGARDTYPE", "0" );
                break;
            case ReportName.IshurimLerashemet:
                Params.Add("P_PAGE_ADDRESS", PureUrlRoot + "/Modules/Ovdim/WorkCard.aspx?");
                Params.Add("P_WORKERID", LoginUser.UserInfo.EmployeeNumber.ToString());
                break;
            case ReportName.Presence:
//                Params.Add("P_WORKERVIEWLEVEL", ((int)PageModule.SecurityLevel).ToString());
                Params.Add("P_WORKERID", LoginUser.UserInfo.EmployeeNumber.ToString());
                break; 

        }
    }
    private void FillChildControls()
    {
        _PanelFilters = (PanelFilters)Session["PanelFilters"];
        List<string> ControlsList = _PanelFilters.ControlsBoxes;
        Control FilterControl = new Control();
        ControlsList.ForEach(delegate(string NameId)
        {
            FilterControl = TdFilter.FindControl(NameId);
        });

    }

    protected virtual ScriptManager ScriptManager
    {
        get
        {
            if (Master != null)
                return (ScriptManager)Master.FindControl("ScriptManagerKds");
            else
                return (ScriptManager)FindControl("ScriptManagerKds");
        }
    }


}


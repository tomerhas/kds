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
using KdsBatch;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;
using System.Collections.Specialized;
using KdsLibrary.UDT;
using KdsWorkFlow.Approvals;
using KdsLibrary.Utils.Reports;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using KdsLibrary.Utils;
public partial class Modules_Ovdim_WorkCard : KdsPage
{
    public int iMisparIshi;
    public DateTime dDateCard;
    clBatchManager oBatchManager;
    public const string URLZmaniNesiotPage = "~/Modules/Ovdim/ZmaniNesiot.aspx";

    private const int KOD_SIBA_LO_LETASHLUM = 20;
    private const string ERROR_IN_SITE = "החתמה באתר לא תקין";
    private const string PTOR = "פטור";
    private const string MISSING_SIBA = "חסרה החתמה";
    private const int MASACH_ID = 19;

    private const int ERR_DESCRIPTION = 1;
    private const int ERR_NUM = 2;
    private const int ERR_SIDUR_NUMBER = 3;
    private const int ERR_SIDUR_START = 4;
    private const int ERR_ACTIVITIY_START = 5;
    private const int ERR_ACTIVITIY_NUMBER = 6;

    private clGeneral.enCardStatus _StatusCard;
    private ApprovalRequest[] arrEmployeeApproval;
    private DataView dvSibotLedivuch;
    private DataTable dtIdkuneyRashemet;
    private DataTable dtSadotNosafim;
    private DataTable dtMeafyeneySidur;
    private bool bWorkCardWasUpdate;
    private bool bWorkCardWasUpdateRun; //מציין אם הפונקציה שבודקת אם כרטיס עודכן רצה
    private bool bInpuDataResult;
    private string[] arrParams;
    private int iMisparIshiKiosk;
    private DataTable dtPakadim;
    private bool bRashemet;
    private int iMisparIshiIdkunRashemet;
    private bool bParticipationAllowed;
 //   private bool bDisabledFrame;
   // private AsyncPostBackTrigger[] TriggerToAdd;
    public const int SIDUR_CONTINUE_NAHAGUT = 99500;
    public const int SIDUR_CONTINUE_NOT_NAHAGUT = 99501;  
    //private WorkCardObj _WorkCardBeforeChanges, _WorkCardAfterChanges;

    //private enum ErrorLevel
    //{
    //    errYomAvoda = 3,
    //    errSidur = 5,
    //    errPeilut = 7
    //}
    protected override void CreateUser()
    {
       
        //if (Request.QueryString["Page"]==null)
        //    EventLog.WriteEntry("kds","Page=null", EventLogEntryType.Error);
        //else
        //    EventLog.WriteEntry("kds", "page=" + Request.QueryString["Page"].ToString(), EventLogEntryType.Error);
      
        //if ((Session["arrParams"] == null))
        //{
        //    EventLog.WriteEntry("kds", "arrParam=null", EventLogEntryType.Error);
        //}
        //else
        //{
        //    EventLog.WriteEntry("kds", "arrParam=not null", EventLogEntryType.Error);
        //}
        //EventLog.WriteEntry("kds","src="+((string)Session["hidSource"]), EventLogEntryType.Error);
       
        if (((Session["arrParams"] != null) && (Request.QueryString["Page"] != null)) || ((((string)Session["hidSource"]) == "1") && (Session["arrParams"] != null)))
        {
            arrParams = (string[])Session["arrParams"];
            SetUserKiosk(arrParams);
            hidDriver.Value = "1";
        }
        else { base.CreateUser(); }
    }


    private void SetUserKiosk(string[] arrParamsKiosk)
    {
        iMisparIshiKiosk = int.Parse(arrParamsKiosk[0].ToString());

        LoginUser = LoginUser.GetLimitedUser(iMisparIshiKiosk.ToString());
        LoginUser.InjectEmployeeNumber(iMisparIshiKiosk.ToString());
        //MasterPage mp = (MasterPage)Page.Master;
        //mp.DisabledMenuAndToolBar = true;
    }
    protected override void OnLoad(EventArgs e)
    {
        // _secManager.AuthorizePage(this);

        base.OnLoad(e);
        if (!IsPostBack)
        {
            SetHeader(PageHeader);
            SetDateHeader(DateHeader);
            SetServiceRefference();
        }
    }
    protected void SetMeasherMistayeg()
    {
        //במידה ותאריך הכרטיס הוא התאריך של היום לא נאפשר את כפתורי מאשר מסתייג
        if (dDateCard.ToShortDateString().Equals(DateTime.Now.ToShortDateString())){        
            btnApprove.Disabled = true;
            btnNotApprove.Disabled = true;
        }
        else{                  
                if (iMisparIshi == int.Parse(LoginUser.UserInfo.EmployeeNumber)){                                   
                    //אם הגענו מעמדת נהג, נאפשר את מאשר מסתייג
                    //רק במידה ולא נעשה שינוי בכרטיס
                    btnApprove.Disabled = bWorkCardWasUpdate;
                    btnNotApprove.Disabled = bWorkCardWasUpdate;                                    
                }
                else{                                                                          
                        //אם הגענו מעמדת נהג, נאפשר את מאשר מסתייג
                        //רק במידה ולא נעשה שינוי בכרטיס
                        btnApprove.Disabled = true;
                        btnNotApprove.Disabled = true;                    
                }           
        }
       

        clGeneral.enMeasherOMistayeg oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)oBatchManager.oOvedYomAvodaDetails.iMeasherOMistayeg;
        switch (oMasherOMistayeg)
        {
            case clGeneral.enMeasherOMistayeg.ValueNull:
                if (btnApprove.Disabled)
                    btnApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled"); 
                if (btnNotApprove.Disabled)
                    btnNotApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled"); 
                if (!bRashemet)                
                    btnPrint.Enabled = false;                
                //if (iMisparIshi != int.Parse(LoginUser.UserInfo.EmployeeNumber))
                //{
                //    EnabledFrames(false);
                //    bDisabledFrame = true;
                //}
                //else
                //    EnabledFrames(true);                
                break;
            case clGeneral.enMeasherOMistayeg.Measher:
                if (btnApprove.Disabled)
                    btnApprove.Attributes.Add("class", "ImgButtonApprovalCheckedDisabled");
                else
                    btnApprove.Attributes.Add("class", "ImgButtonApprovalChecked");

                if (btnNotApprove.Disabled)
                    btnNotApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled");
                else
                    btnNotApprove.Attributes.Add("class", "ImgButtonApprovalRegular");
                break;
            case clGeneral.enMeasherOMistayeg.Mistayeg:
                 if (btnApprove.Disabled)
                     btnApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled");
                 else
                     btnApprove.Attributes.Add("class", "ImgButtonApprovalRegular");
                 if (btnNotApprove.Disabled)
                     btnNotApprove.Attributes.Add("class", "ImgButtonApprovalCheckedDisabled");
                 else
                     btnNotApprove.Attributes.Add("class", "ImgButtonApprovalChecked");
                break;

            default:
                if (btnApprove.Disabled)
                    btnApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled"); 
                else
                    btnApprove.Attributes.Add("class", "ImgButtonApprovalRegular"); 
                if (btnNotApprove.Disabled)
                    btnNotApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled"); 
                else
                    btnNotApprove.Attributes.Add("class", "ImgButtonApprovalRegular"); 
                btnPrint.Enabled = true;
                break;
        }
        hidMeasherMistayeg.Value = oMasherOMistayeg.GetHashCode().ToString();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        RenderPage();
        //if (bInpuDataResult)
        //{
        //    //מספר אישי
        //    SetControlColor(txtId, "black", "white");            
        //    // זמן נסיעות
        //    SetControlColor(ddlTravleTime, "black", "white");            
        //    // לינה 
        //    SetControlColor(ddlLina, "black", "white");                     
        //    //הלבשה
        //    SetControlColor(ddlHalbasha, "black", "white");     
        //    //סיבת השלמה ליום
        //    SetControlColor(ddlHashlamaReason, "black", "white");     
           
        //    switch (_StatusCard)
        //    {
        //        case clGeneral.enCardStatus.Valid:
        //            //אישורים
        //            SetApprovalInPage();
        //            break;
        //        case clGeneral.enCardStatus.Error:
        //            //שגיאות
        //            SetErrorInPage();
        //            break;
        //    }


        //    //התייצבות
        //    bParticipationAllowed = SetParticipation();
            
        //   // if (bParticipationAllowed){   
        //    if (!bWorkCardWasUpdateRun)
        //        bWorkCardWasUpdate = IsWorkCardWasUpdate();
        //   // }
        //    //bParticipationAllowed = SetParticipation();
        //    SetMeasherMistayeg();
            
        //    clGeneral.enMeasherOMistayeg oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)oBatchManager.oOvedYomAvodaDetails.iMeasherOMistayeg;
        //    //במידה והמשתמש הוא מנהל עם כפופים (לצפיה או לעדכון) וגם המספר האישי של הכרטיס שונה מממספר האישי של המשתמש שנכנס
        //    //או שהתאריך הוא תאריך של היום. לא נאפשר עדכון כרטיס
        //    KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
        //    if ((((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
        //        && (iMisparIshi != int.Parse(LoginUser.UserInfo.EmployeeNumber))) || ((dDateCard.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))))           
        //        EnabledFrames(false);            
        //    else
        //        if (!bDisabledFrame)                
        //            EnabledFrames(true);
                
                        
        //    btnHamara.Disabled = (!EnabledHamaraForDay());
        //    ddlTachograph.Enabled = EnabledTachograph();
        //    EnabledFields();
        //    SecurityManager.AuthorizePage(this,true);
        //    BindSibotLehashlamaLeyom();
        //    if ((!Page.IsPostBack) || (bool.Parse(ViewState["LoadNewCard"].ToString())))           
        //        CreateChangeAttributs();
            
        //    SetDDLToolTip();
        //    string sScript = "SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ");";
        //    ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);

        ////    InsertTriggersToUpdatePanel (upEmployeeDetails, TriggerToAdd);


        //}
        ////Before Load page, save field data for compare
        ////_WorkCardBeforeChanges = InitWorkCardObject();
    }

    protected void SetDDLToolTip(){
        clUtils.SetDDLToolTip(ddlLina);
        clUtils.SetDDLToolTip(ddlTravleTime);
        clUtils.SetDDLToolTip(ddlTachograph);
        clUtils.SetDDLToolTip(ddlHalbasha);
        clUtils.SetDDLToolTip(ddlHashlamaReason);
        clUtils.SetDDLToolTip(ddlFirstPart);
        clUtils.SetDDLToolTip(ddlSecPart);
    }
    //protected WorkCardObj InitWorkCardObject()
    //{
    //    //Fill WorkCard With all Page fields
    //    WorkCardObj _WorkCard = new WorkCardObj();
    //    try
    //    {
    //        _WorkCard.EmpID = iMisparIshi;
    //        _WorkCard.CardDate = dDateCard;
    //        _WorkCard.Lina = String.IsNullOrEmpty(ddlLina.SelectedValue) ? 0 : int.Parse(ddlLina.SelectedValue);
    //        _WorkCard.Halbasha = String.IsNullOrEmpty(ddlHalbasha.SelectedValue) ? 0 : int.Parse(ddlHalbasha.SelectedValue);


    //        return _WorkCard;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected bool IsRashemetProfile()
    {
        bool bRashemet = false;
        try
        {
            for (int i = 0; i < LoginUser.UserProfiles.Length; i++)
            {
                if ((LoginUser.UserProfiles[i].Role == clGeneral.enProfile.enRashemet.GetHashCode())
                     || (LoginUser.UserProfiles[i].Role == clGeneral.enProfile.enRashemetAll.GetHashCode())
                     || (LoginUser.UserProfiles[i].Role == clGeneral.enProfile.enSystemAdmin.GetHashCode()

                     ))
                {
                    bRashemet = true;
                    break;
                }
            }
            return bRashemet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetPageDefault()
    {
       // clnDate.Attributes.Add("onkeyup", "CheckIfCardExists();");
        txtId.Attributes.Add("onblur", "isUserIdValid();");
        txtId.Attributes.Add("onfocus", "document.getElementById('txtId').select();");
        //txtId.Attributes.Add("onkeydown", "BarCodeTest();");
        //txtId.Attributes.Add("onkeydown", "onTxtIdPress");
        txtId.Focus();
        txtName.Attributes.Add("onchange", "isUserNameValid();");
        btnRefreshOvedDetails.Attributes.Add("onfocus", "onButtonFocusIn(" + btnRefreshOvedDetails.ID + ")");
        btnRefreshOvedDetails.Attributes.Add("onfocusout","onButtonFocusOut(" + btnRefreshOvedDetails.ID + ")");
        btnUpdateCard.Attributes.Add("onfocus", "onButtonFocusIn(" + btnUpdateCard.ID + ")");
        btnUpdateCard.Attributes.Add("onfocusout", "onButtonFocusOut(" + btnUpdateCard.ID + ")");
        
        ErrorImage(imgIdErr, false);
        ErrorImage(imgTimeErr, false);
        ErrorImage(imgLinaErr, false);
        ErrorImage(imgHalbErr, false);
        ErrorImage(imgDayHaslamaErr, false);
        if (LoginUser.IsLimitedUser)
            btnDrvErrors.Style.Add("Display", "block");
        else
            btnDrvErrors.Style.Add("Display", "none");
        //btnRefreshOvedDetails.Enabled = false;
    }
    protected void SetIdkuneyRashemet()
    {
        try
        {
            ddlLina.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "LINA"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
            ddlTachograph.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "TACHOGRAF"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
            ddlTravleTime.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "BITUL_ZMAN_NESIOT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
            ddlHalbasha.Enabled = false;//(!clWorkCard.IsIdkunExists(clWorkCard.ErrorLevel.LevelYomAvoda, 5, 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));            
            ddlHashlamaReason.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "SIBAT_HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
            HashlamaForDayValue.Disabled = (clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
            Hamara.Disabled = (clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HAMARAT_SHABAT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));

            lstSidurim.dtIdkuneyRashemet = dtIdkuneyRashemet;
            Session["IdkuneyRashemet"] = lstSidurim.dtIdkuneyRashemet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool RunBatchFunctions()
    {
        bool bResult = true;
        clDefinitions _Defintion = new clDefinitions();
        bool bLoadNewCard=false;

        try
        {
            clOvedYomAvoda oOvedYomAvodaDetails = new clOvedYomAvoda(iMisparIshi, dDateCard);
            if (ViewState["LoadNewCard"] != null)
                bLoadNewCard = (bool.Parse(ViewState["LoadNewCard"].ToString()) == true);
            if (
                ((oOvedYomAvodaDetails.iStatus == clGeneral.enCardStatus.Calculate.GetHashCode()) && (!Page.IsPostBack) && (Request.QueryString["WCardUpdate"] == null))
                || ((Request.QueryString["WCardUpdate"]==null) && (oOvedYomAvodaDetails.iStatus == clGeneral.enCardStatus.Calculate.GetHashCode()))
                )     
                       
            {       
                oBatchManager.InitGeneralData();
                oBatchManager.CardStatus = clGeneral.enCardStatus.Calculate;
                bInpuDataResult = true;
                bResult = true;
            }
            else
            {
                //שינויי קלט
                if (!(hidExecInputChg.Value.Equals("0")))
                {
                    bInpuDataResult = oBatchManager.MainInputData(iMisparIshi, dDateCard);
                    if (!bInpuDataResult)
                        //שינויי קלט
                        bResult = false;
                }
                else { hidExecInputChg.Value = ""; }
                if (bResult)
                {
                    //שגויים
                    if ((hidErrChg.Value.Equals("")) || ((hidErrChg.Value.Equals("0"))))
                    {
                        bInpuDataResult = oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
                        bResult = bInpuDataResult;
                    }
                    else { hidErrChg.Value = "0"; } //נחזיר את הדגל כך שיקראו לשגויים בפעם הבאה }
                }
            }
            if (!bResult)
            {

                if ((Request.QueryString["Page"] != null) || ((Session["arrParams"] != null)))
                {
                    //string sScript = "alert('לא ניתן לעלות כרטיס עבודה'); window.location.href = '" + this.PureUrlRoot + "/Main.aspx';";
                    string sScript = "alert('לא ניתן לעלות כרטיס עבודה'); CloseWindow();";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "InputDataFailed", sScript, true);
                }
                else
                {
                    string sScript = "alert('לא ניתן לעלות כרטיס עבודה'); window.close();";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "InputDataFailed", sScript, true);
                }
            }
            return bResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void LoadPage()
    {
        DataTable dtLicenseNumbers = new DataTable();
        try
        {          
            ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
            //הרשאות לדף
            SetSecurityLevel();

            //אתחול פרמטרים
            SetEmployeeCardData();
            oBatchManager = new clBatchManager(iMisparIshi, dDateCard);
            SetPageDefault();
            bRashemet = LoginUser.IsRashemetProfile(LoginUser);
            Session["ProfileRashemet"] = bRashemet;
            hidRashemet.Value = bRashemet ? "1" : "0";
            hidFromEmda.Value = (LoginUser.IsLimitedUser && arrParams[2].ToString() == "1") ? "true" : "false";
            iMisparIshiIdkunRashemet = ((int.Parse)(LoginUser.UserInfo.EmployeeNumber)).Equals(iMisparIshi) ? iMisparIshi : 0;

            oBatchManager.iLoginUserId =int.Parse(LoginUser.UserInfo.EmployeeNumber);
            Session["LoginUserEmp"] = LoginUser.UserInfo.EmployeeNumber;
            //שינויי קלט ושגויים
            if (RunBatchFunctions())
            {
                //רוטינת שגויים             
                //Session["Errors"] = oBatchManager.dtErrors;
                //Session["Parameters"] = oBatchManager.oParam;
                //נתונים כללים שמגיעים מאובייקט שגויים ושינויי קלט
                SetGeneralData(oBatchManager);

                //איתחול ה- USERCONTROL
                InitSidurimUserControl();

                //טבלאות פענוח
                SetLookUpSidurim();
                SetImageForButtonValiditiy();

                ////אישורים               
                GetApproval();
                lstSidurim.btnHandler += new Modules_UserControl_ucSidurim.OnButtonClick(lstSidurim_btnHandler);
                //lstSidurim.btnReka +=new Modules_UserControl_ucSidurim.OnButtonClick(lstSidurim_btnReka);
                //if ((!Page.IsPostBack) || (bool.Parse(ViewState["LoadNewCard"].ToString())))
                if ((!Page.IsPostBack) || (hidRefresh.Value.Equals("1")))
                {
                    Session["Errors"] = oBatchManager.dtErrors;
                    Session["Parameters"] = oBatchManager.oParam;
                    dtIdkuneyRashemet = clWorkCard.GetIdkuneyRashemet(iMisparIshi, dDateCard);
                    if (!Page.IsPostBack)
                    {
                        btnUpdateCard.Attributes.Add("disabled", "true");
                        BindTachograph();
                        SetLookUpDDL();
                        ShowOvedCardDetails(iMisparIshi, dDateCard);
                        SetImageForButtonMeasherOMistayeg();
                        //dtPakadim = GetMasachPakadim();
                        //Session["Pakadim"] = dtPakadim;
                    }
                    //אם הגענו מעמדת נהג, נשמור 1 אחרת 0
                    hidSource.Value = ((Request.QueryString["Page"] != null) || (Session["arrParams"] != null)) ? "1" : " 0";
                    Session["hidSource"] = hidSource.Value;
                    if (dtPakadim == null)
                        if ((DataTable)Session["Pakadim"] == null)
                        {
                            dtPakadim = GetMasachPakadim();
                            Session["Pakadim"] = dtPakadim;
                        }
                        else
                            dtPakadim = (DataTable)Session["Pakadim"];

                    lstSidurim.dtPakadim = dtPakadim;
                    dtSadotNosafim = clWorkCard.GetSadotNosafimLesidur();
                    dtMeafyeneySidur = clWorkCard.GetMeafyeneySidur();
                    Session["SadotNosafim"] = dtSadotNosafim;
                    Session["MeafyeneySidur"] = dtMeafyeneySidur;

                    //עידכוני רשמת
                    SetIdkuneyRashemet();
                    RefreshEmployeeData(iMisparIshi, dDateCard);
                    //רק אם יש סידורים 
                    if (oBatchManager.htFullEmployeeDetails != null)
                    {
                        lstSidurim.DataSource = oBatchManager.htFullEmployeeDetails;
                        Session["Sidurim"] = oBatchManager.htFullEmployeeDetails;
                        if (oBatchManager.dtMashar == null)
                            dtLicenseNumbers = GetMasharData(oBatchManager.htFullEmployeeDetails);
                        else                        
                            dtLicenseNumbers = oBatchManager.dtMashar;

                        Session["Mashar"] = dtLicenseNumbers;
                        lstSidurim.Mashar = dtLicenseNumbers;
                    }
                    ViewState["LoadNewCard"] = true;
                    lstSidurim.ErrorsList = oBatchManager.dtErrors;
                    lstSidurim.SadotNosafim = dtSadotNosafim;
                    lstSidurim.MeafyeneySidur = dtMeafyeneySidur;
                    lstSidurim.dtPakadim = (DataTable)Session["Pakadim"];
                }
                else
                {
                    lstSidurim.CardDate = dDateCard;
                    lstSidurim.DataSource = (OrderedDictionary)Session["Sidurim"];
                    lstSidurim.Mashar = (DataTable)Session["Mashar"];
                    oBatchManager.dtErrors = (DataTable)Session["Errors"];
                    oBatchManager.oParam = (clParameters)Session["Parameters"];
                    dtPakadim = (DataTable)Session["Pakadim"];
                    lstSidurim.ErrorsList = (DataTable)Session["Errors"];
                    lstSidurim.SadotNosafim = (DataTable)Session["SadotNosafim"];
                    lstSidurim.MeafyeneySidur = (DataTable)Session["MeafyeneySidur"];
                    lstSidurim.dtPakadim = (DataTable)Session["Pakadim"];
                    lstSidurim.dtIdkuneyRashemet = (DataTable)Session["IdkuneyRashemet"];
                }
            }            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
    }
    protected void RenderPage()
    {
        if (bInpuDataResult)
        {
            //מספר אישי
            SetControlColor(txtId, "black", "white");
            // זמן נסיעות
            SetControlColor(ddlTravleTime, "black", "white");
            // לינה 
            SetControlColor(ddlLina, "black", "white");
            //הלבשה
            SetControlColor(ddlHalbasha, "black", "white");
            //סיבת השלמה ליום
            SetControlColor(ddlHashlamaReason, "black", "white");

            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Valid:
                    //אישורים
                    SetApprovalInPage();
                    break;
                case clGeneral.enCardStatus.Error:
                    //שגיאות
                    SetErrorInPage();
                    break;
            }


            //התייצבות
            bParticipationAllowed = SetParticipation();

            // if (bParticipationAllowed){   
            if (!bWorkCardWasUpdateRun)
                bWorkCardWasUpdate = IsWorkCardWasUpdate();
            // }
            //bParticipationAllowed = SetParticipation();
            SetMeasherMistayeg();

            clGeneral.enMeasherOMistayeg oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)oBatchManager.oOvedYomAvodaDetails.iMeasherOMistayeg;
            //במידה והמשתמש הוא מנהל עם כפופים (לצפיה או לעדכון) וגם המספר האישי של הכרטיס שונה מממספר האישי של המשתמש שנכנס
            //או שהתאריך הוא תאריך של היום. לא נאפשר עדכון כרטיס
            KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
            if ((((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
                && (iMisparIshi != int.Parse(LoginUser.UserInfo.EmployeeNumber))) || ((dDateCard.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))))
                EnabledFrames(false);
            else
             //   if (!bDisabledFrame)
                    EnabledFrames(true);


            btnHamara.Disabled = (!EnabledHamaraForDay());
            ddlTachograph.Enabled = EnabledTachograph();
            EnabledFields();
            SecurityManager.AuthorizePage(this, true);
            BindSibotLehashlamaLeyom();
            if ((!Page.IsPostBack) || (bool.Parse(ViewState["LoadNewCard"].ToString())))
                CreateChangeAttributs();

            SetDDLToolTip();
            string sScript = "SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ");";
            ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);            
        }
        //Before Load page, save field data for compare
        //_WorkCardBeforeChanges = InitWorkCardObject();
    }
    private bool HasVehicleTypeWithOutTachograph()
    {
        bool HasVehicleType = false;
        DataTable dt;
        DataRow[] dr;
        //יש לבדוק שלפחות אחד הרכבים המדווחים באותו תאריך אינו מדגם 64  (דגם שאינו מכיל טכוגרף). Vehicle_Type =64 במעל"ה.
        try
        {
           dt = (((DataTable)Session["Mashar"]));
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   dr = ((DataTable)Session["Mashar"]).Select("Vehicle_Type<>" + clGeneral.enVehicleType.NoTachograph.GetHashCode());
                   HasVehicleType = (dr.Length > 0);
               }
           }
           return HasVehicleType;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void AddAttribute(Control oObj)
    {
        try
        {
            string _ControlType = oObj.GetType().ToString();
            switch (_ControlType)
            {
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList _DDl = (DropDownList)oObj;
                    _DDl.Attributes.Add("OldV", _DDl.SelectedValue);
                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    CheckBox _Chk = (CheckBox)oObj;
                    _Chk.Attributes.Add("OldV", _Chk.Checked.GetHashCode().ToString());
                    break;
                case "System.Web.UI.HtmlControls.HtmlInputHidden":
                    HtmlInputHidden _HtmlInputBtn = (HtmlInputHidden)oObj;
                    _HtmlInputBtn.Attributes.Add("OldV", _HtmlInputBtn.Value);
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public static void BindTooltip(System.Web.UI.Page p)
    //{
    //    if (p == null || p.Form == null)
    //        return;
    //    BindTooltip(p.Form.Controls);
    //}


    //public static void BindTooltip(ControlCollection ControlList)
    //{
    //    string _ControlType;
    //    for (int i = 0; i < ControlList.Count; i++)
    //    {
    //        _ControlType = ControlList[i].GetType().ToString();
    //        switch (_ControlType)
    //        {
    //            case "System.Web.UI.WebControls.DropDownList":
    //                BindTooltip((DropDownList)ControlList[i]);
    //                break;
    //        }
    //    }
    //}

    protected void CreateChangeAttributs()
    {
        AddAttribute(ddlLina);
        AddAttribute(ddlTravleTime);
        AddAttribute(ddlTachograph);
        AddAttribute(ddlHalbasha);
        AddAttribute(ddlHashlamaReason);
        AddAttribute(HashlamaForDayValue);
        AddAttribute(Hamara);
        AddAttribute(ddlFirstPart);
        AddAttribute(ddlSecPart);
    }
    protected bool SetParticipation()
    {
        //התייצבות 
        bool bParticipationAllowed = ParticipationAllowed();

       // btnOpenParticipation.Disabled = (!bParticipationAllowed);
        if (bParticipationAllowed)
        {
            BindToDDL(dvSibotLedivuch, ddlFirstPart, true);
            BindToDDL(dvSibotLedivuch, ddlSecPart, true);
        }
        SetOneParticipation(lstSidurim.FirstParticipate, txtFirstPart, ddlFirstPart, 1);
        SetOneParticipation(lstSidurim.SecondParticipate, txtSecPart, ddlSecPart, 2);

        return bParticipationAllowed;
    }
    protected bool ParticipationAllowed()
    {
        bool _Allowed = false;
        clSidur _Sidur;
        clPeilut _Peilut;
        //1. סידור מפה
        //2. סידור מיוחד שמקורו במטלה (מזהים סידור מיוחד שמקורו במטלה לפי שבאחת הרשומות של הפעילויות של הסידור קיים ערך גדול מ- 0 
        if (oBatchManager.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < oBatchManager.htFullEmployeeDetails.Count; i++)
            {
                _Sidur = (clSidur)(oBatchManager.htFullEmployeeDetails[i]);
                if (!(_Sidur.bSidurMyuhad))
                {
                    _Allowed = true;
                    break;
                }
                else
                {
                    for (int j = 0; j < _Sidur.htPeilut.Count; j++)
                    {
                        _Peilut = (clPeilut)_Sidur.htPeilut[j];
                        if (_Peilut.lMisparMatala > 0)
                        {
                            _Allowed = true;
                            break;
                        }
                    }
                }
            }
        }
        return _Allowed;
    }
    protected void SetOneParticipation(clSidur oSidur, TextBox txtParticipation, DropDownList ddlParticipation, int iIndx)
    {
        int iKodLedivuch;
        //CustomValidator _CusVld;
        try
        {
            //if (txtParticipation.Text == string.Empty)
            //{
            //    txtParticipation.Parent.Controls.Add(AddTimeMaskedEditExtender(txtParticipation.ID, "99:99", "mskPart" + iIndx, AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Right));
            //    _CusVld = AddCustomValidator(txtParticipation.ID, "שעה לא תקינה, יש להקליד שעה בין 00:01 ל- 23:59", "cusPart" + iIndx, "chkPartHour", "");
            //    txtParticipation.Parent.Controls.Add(_CusVld);
            //    txtParticipation.Parent.Controls.Add(AddCallOutValidator(_CusVld.ID, "CusCallOutPart" + iIndx, ""));
            //}
            txtParticipation.Text = "";
            txtParticipation.ReadOnly = true;
            ddlParticipation.Enabled = false;
            if (oSidur != null)
            {
                if ((oSidur.iHachtamaBeatarLoTakin == clGeneral.enHityazvutErrorInSite.enHityazvutErrorInSite.GetHashCode())
                    && (oSidur.dShatHitiatzvut != null) && (oSidur.dShatHitiatzvut.Year > clGeneral.cYearNull))
                {
                    txtParticipation.Text = ERROR_IN_SITE;
                    //txtParticipation.ReadOnly = true;
                }
                else
                {
                    if ((oSidur.dShatHitiatzvut != null) && (oSidur.dShatHitiatzvut.Year > clGeneral.cYearNull))
                    {
                        txtParticipation.Text = oSidur.dShatHitiatzvut.ToShortTimeString();
                        //txtParticipation.ReadOnly = false;
                        //if (txtParticipation.Text != string.Empty)
                        //{
                        //    txtParticipation.Parent.Controls.Add(AddTimeMaskedEditExtender(txtParticipation.ID, "99:99", "mskPart" + iIndx, AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Right));
                        //    _CusVld = AddCustomValidator(txtParticipation.ID, "שעה לא תקינה, יש להקליד שעה בין 00:01 ל- 23:59", "cusPart" + iIndx, "chkPartHour", "");
                        //    txtParticipation.Parent.Controls.Add(_CusVld);
                        //    txtParticipation.Parent.Controls.Add(AddCallOutValidator(_CusVld.ID, "CusPart" + iIndx, ""));
                        //}
                    }
                    else
                    {
                        //אם אין שעת התייצבות ויש ערך 1 שדה פטור מהתייצבות, נציג את הצלל 'פטור'
                        if (oSidur.iPtorMehitiatzvut == clGeneral.enPtorHityazvut.PtorHityazvut.GetHashCode())
                        {
                            txtParticipation.Text = PTOR;
                            // txtParticipation.ReadOnly = true;
                        }
                    }
                }
                if (txtParticipation.Text == string.Empty)
                {
                    txtParticipation.Text = MISSING_SIBA;
                    ddlParticipation.Enabled = true;
                }
                iKodLedivuch = oSidur.iKodSibaLedivuchYadaniIn;
                ddlParticipation.SelectedValue = iKodLedivuch == 0 ? "-1" : iKodLedivuch.ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected AjaxControlToolkit.ValidatorCalloutExtender AddCallOutValidator
                                       (string sTargetControlId, string sID, string eClientId)
    {
        AjaxControlToolkit.ValidatorCalloutExtender vldExKisuyTor = new AjaxControlToolkit.ValidatorCalloutExtender();
        try
        {
            vldExKisuyTor.TargetControlID = sTargetControlId;
            vldExKisuyTor.PopupPosition = AjaxControlToolkit.ValidatorCalloutPosition.Left;
            vldExKisuyTor.ID = sID + eClientId;

            return vldExKisuyTor;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected CustomValidator AddCustomValidator(string sTargetControlId, string sMessage, string sID,
                                                      string sClientScriptFunction, string eClientID)
    {
        CustomValidator vldCustomValidator = new CustomValidator();
        try
        {
            vldCustomValidator.ClientValidationFunction = sClientScriptFunction;
            vldCustomValidator.Display = ValidatorDisplay.None;
            vldCustomValidator.ErrorMessage = sMessage;
            vldCustomValidator.ControlToValidate = sTargetControlId;
            vldCustomValidator.EnableClientScript = true;
            vldCustomValidator.ID = sID + eClientID;

            return vldCustomValidator;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected AjaxControlToolkit.MaskedEditExtender AddTimeMaskedEditExtender(string sTargetControlId,
                                                                            string sMask, string sMaskId,
                                                                            AjaxControlToolkit.MaskedEditType oMaskType,
                                                                            AjaxControlToolkit.MaskedEditShowSymbol oMaskEditSymbol)
    {
        AjaxControlToolkit.MaskedEditExtender oMaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
        try
        {
            oMaskTextBox.TargetControlID = sTargetControlId;
            oMaskTextBox.Mask = sMask;
            oMaskTextBox.MessageValidatorTip = true;
            //oMaskTextBox.OnFocusCssClass = "MaskedEditFocus";
            oMaskTextBox.OnInvalidCssClass = "MaskedEditError";
            oMaskTextBox.MaskType = oMaskType;
            oMaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
            oMaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.None;
            oMaskTextBox.DisplayMoney = oMaskEditSymbol;
            oMaskTextBox.ErrorTooltipEnabled = true;
            oMaskTextBox.ID = sMaskId;

            return oMaskTextBox;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void GetApproval()
    {
        //if (_StatusCard == clGeneral.enCardStatus.Valid)
        //{
            // btnCalcItem.Disabled = false;
            ApprovalManager _Approvals = new ApprovalManager(LoginUser);
            DataTable dt;
            
            dt = _Approvals.GetApprovalCodes();
            //נכניס ל USER CONTROL SIDURIM
            lstSidurim.dtApprovals = dt;
       // }
    }
    protected void SetApprovalInPage()
    {
        //נבדוק אם יש אישורים על שדה השלמה ליום
        //if (_StatusCard == clGeneral.enCardStatus.Valid)
        //{

            DataRow[] dr;
            
            bool bEnableApprove = false;
            //// btnCalcItem.Disabled = false;
            //ApprovalManager _Approvals = new ApprovalManager(LoginUser);
            //DataTable dt;
            //DataRow[] dr;

            //dt = _Approvals.GetApprovalCodes();
            ////נכניס ל USER CONTROL SIDURIM
            //lstSidurim.dtApprovals = dt;
            //אישורים לשדה השלמה
            dr = lstSidurim.dtApprovals.Select("mafne_lesade = 'Hashlama_Leyom'");
            if (dr.Length > 0)
            {
                string sAllApprovalDescription = "";
                if (CheckIfApprovalExists(FillApprovalKeys(dr), ref sAllApprovalDescription, ref bEnableApprove))
                {
                    ddlHashlamaReason.Attributes.Add("class", "ApprovalField");                    
                    imgDayHaslamaErr.Src = "../../Images/ApprovalSign.jpg";
                    imgDayHaslamaErr.Attributes.Add("ondblclick", "GetAppMsg(this)");
                    imgDayHaslamaErr.Attributes.Add("App", sAllApprovalDescription);
                    ErrorImage(imgDayHaslamaErr, true);
                    ddlHashlamaReason.Enabled = bEnableApprove;
                    btnHashlamaForDay.Disabled =!bEnableApprove;
                }
            }
        //}
    }

    protected bool CheckIfApprovalExists(int[] arrApprovalKey, ref string sAllApprovalDescription, ref bool bEnableApprove)
    {
        bool bApprovalExists = false;
        KdsWorkFlow.Approvals.WorkCard _WorkCardFlow;
        string sApprovalDescription = "";
        //במידה וקיים אישור נצבע בירוק את השדה ונאפשר העלאת חלון הסבר
        _WorkCardFlow = new WorkCard();
        for (int iCount = 0; iCount < arrApprovalKey.Length; iCount++)
        {
            if (_WorkCardFlow.HasApproval(arrEmployeeApproval, arrApprovalKey[iCount], ref sApprovalDescription, ref bEnableApprove))
            {
                sAllApprovalDescription = string.Concat(sAllApprovalDescription, (char)13, sApprovalDescription);
                bApprovalExists = true;
            }
        }
        return bApprovalExists;
    }
    private bool SetOneError(Control oObj, string sFieldName, string sImgId)
    {
        bool bErrorExists = false;
        DataSet dsFieldsErrors;
        DataView dvFieldsErrors;
        //string _ControlType = oObj.GetType().ToString();

        dsFieldsErrors = clDefinitions.GetErrorsForFields(bRashemet, oBatchManager.dtErrors, iMisparIshi, dDateCard, sFieldName);
        dvFieldsErrors = new DataView(dsFieldsErrors.Tables[0]);
        dvFieldsErrors.RowFilter = "SHOW_ERROR=1";

        if (dvFieldsErrors.Count > 0)
        {
            ((WebControl)oObj).Style.Add("background-color", "red");
            ((WebControl)oObj).Style.Add("color", "white");
            ((WebControl)oObj).Attributes.Add("ErrCnt", dvFieldsErrors.Count.ToString());
            ((WebControl)oObj).Attributes.Add("FName", sFieldName);
            ((WebControl)oObj).Attributes.Add("ImgId", sImgId);
            bErrorExists = true;
            //switch (_ControlType)
            //{
            //    case "System.Web.UI.WebControls.TextBox":                    
            //        ((TextBox)oObj).Style.Add("background-color", "red");
            //        ((TextBox)oObj).Style.Add("color", "white");  
            //        ((TextBox)oObj).Attributes.Add("ErrCnt", dvFieldsErrors.Count.ToString());
            //        ((TextBox)oObj).Attributes.Add("FName", sFieldName);
            //        ((TextBox)oObj).Attributes.Add("ImgId", sImgId);
            //        bErrorExists = true;
            //        break;
            //    case "System.Web.UI.WebControls.DropDownList":                    
            //        ((DropDownList)oObj).Style.Add("background-color", "red");
            //        ((DropDownList)oObj).Style.Add("color", "white"); 
            //        ((DropDownList)oObj).Attributes.Add("ErrCnt", dvFieldsErrors.Count.ToString());
            //        ((DropDownList)oObj).Attributes.Add("FName", sFieldName);
            //        ((DropDownList)oObj).Attributes.Add("ImgId", sImgId);
            //        bErrorExists = true;                        
            //        break;
            //}                      
        }
        else
        {
            SetControlColor(oObj, "black", "white");
            //((WebControl)oObj).Style.Add("background-color", "white");
            //((WebControl)oObj).Style.Add("color", "black");
            //switch (_ControlType)
            //{
            //    case "System.Web.UI.WebControls.TextBox":
            //        ((TextBox)oObj).Style.Add("background-color", "white");
            //        ((TextBox)oObj).Style.Add("color", "black");                   
            //        break;
            //    case "System.Web.UI.WebControls.DropDownList":
            //        ((DropDownList)oObj).Style.Add("background-color", "white");
            //        ((DropDownList)oObj).Style.Add("color", "black");                   
            //        break;
            //}
        }
        return bErrorExists;
    }
    protected void SetControlColor(Control oObj, string sColor, string sBackgroundColor)
    {        
        ((WebControl)oObj).Style.Add("color", sColor);
        ((WebControl)oObj).Style.Add("background-color", sBackgroundColor);
    }
    protected void ErrorImage(HtmlImage imgErr, bool bErrorExists)
    {
        if (bErrorExists)
            imgErr.Style.Add("Display", "block");
        else
            imgErr.Style.Add("Display", "none");
    }

    private void SetErrorInPage()
    {
        //הרוטינה צובעת באדום את השדות שהן שגיאות
        bool bErrorExists;

        try
        {
            //if (_StatusCard == clGeneral.enCardStatus.Error)
            //{
            //בדיקה אם עובד פעיל
            bErrorExists = (SetOneError(txtId, "mispar_ishi", "imgIdErr"));
            ErrorImage(imgIdErr, bErrorExists);

            //שגיאה 27 ו-150 בדיקת זמן נסיעות
            bErrorExists = SetOneError(ddlTravleTime, "Bitul_Zman_nesiot", "imgTimeErr");
            ErrorImage(imgTimeErr, bErrorExists);

            //בדיקת לינה - שגיאה 30
            bErrorExists = SetOneError(ddlLina, "Lina", "imgLinaErr");
            ErrorImage(imgLinaErr, bErrorExists);

            //בדיקת הלבשה - שגיאה 141 ו-36
            bErrorExists = SetOneError(ddlHalbasha, "Halbasha", "imgHalbErr");
            ErrorImage(imgHalbErr, bErrorExists);

            //סיבת השלמה ליום
            bErrorExists = SetOneError(ddlHashlamaReason, "Sibat_Hashlama_Leyom", "imgDayHaslamaErr");
            ErrorImage(imgDayHaslamaErr, bErrorExists);
            if (bErrorExists)
            {
                imgDayHaslamaErr.Src = "../../Images/ErrorSign.jpg";
                imgDayHaslamaErr.Attributes.Add("ondblclick", "GetErrorMessage(ddlHashlamaReason,1,'');");
            }
            else           
                //אם אין שגיאה, נבדוק אם יש אישור
                SetApprovalInPage();
            
            //ש לפתוח את מסך רכיבים מחושבים כאשר הכרטיס תקין בלבד
            // btnCalcItem.Disabled = true;
            //}
            //else
            //{
            //    ErrorImage(imgIdErr, false);
            //    ErrorImage(imgTimeErr, false);
            //    ErrorImage(imgLinaErr, false);
            //    ErrorImage(imgHalbErr, false);
            //    ErrorImage(imgDayHaslamaErr, false);
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable GetMasharData(OrderedDictionary htEmployeeDetails)
    {
        string sCarNumbers = "";
        DataTable dtLicenseNumber = new DataTable();
        clKavim oKavim = new clKavim();


        sCarNumbers = clDefinitions.GetMasharCarNumbers(htEmployeeDetails);

        if (sCarNumbers != string.Empty)
        {
            dtLicenseNumber = oKavim.GetMasharData(sCarNumbers);
        }
        return dtLicenseNumber;
    }

    private void SetGeneralData(clBatchManager oBatchManager)
    {
        //if (oBatchManager.CardStatus!= clGeneral.enCardStatus.Calculate) 
        //{
        if (oBatchManager.htFullEmployeeDetails == null)
            oBatchManager.InitGeneralData();
       
        //Check if oved musach
        //Get Employee Ishurim
        
        if ((!Page.IsPostBack) || (hidRefresh.Value.Equals("1")))
        {
            ApprovalFactory.RaiseEmployeeWorkDayApprovalCodes(dDateCard, iMisparIshi, 0, clWorkCard.IsOvedMusach(iMisparIshi, dDateCard));
            arrEmployeeApproval = ApprovalRequest.GetMatchingApprovalRequestsWithStatuses(iMisparIshi, dDateCard);
            Session["EmployeeApproval"] = arrEmployeeApproval;
        }
        else
        {
            arrEmployeeApproval = (ApprovalRequest[])Session["EmployeeApproval"];
        }
       // oBatchManager.InitGeneralData();
        
    }
    private void SetEmployeeCardData()
    {
        try
        {
            if (clnDate.Text != string.Empty)
            {
                dDateCard = DateTime.Parse(clnDate.Text);
            }
            else
            {
                if (Request.QueryString["WCardDate"] != null)
                {
                    dDateCard = DateTime.Parse((string)Request.QueryString["WCardDate"]);// new DateTime(2009, 5, 26);               
                }
                else
                {
                    dDateCard = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(-1);
                }
                clnDate.Text = dDateCard.ToShortDateString();
            }
            if (txtId.Text != string.Empty)
            {
                iMisparIshi = int.Parse(txtId.Text);
            }
            else
            {
                if (Request.QueryString["EmpID"] != null)
                {
                    iMisparIshi = int.Parse((string)Request.QueryString["EmpID"]);//74480;
                }
                else
                {
                    iMisparIshi = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                }
                txtId.Text = iMisparIshi.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int[] FillApprovalKeys(DataRow[] dr)
    {
        int[] arrApprovalKeys;
        int iArrControlSize = dr.Length;

        arrApprovalKeys = new int[iArrControlSize];
        for (int i = 0; i < iArrControlSize; i++)
        {
            arrApprovalKeys[i] = int.Parse(dr[i]["kod_ishur"].ToString());
        }

        return arrApprovalKeys;
    }
    //protected int[] FillApprovalKeysHashlamaForDay()
    //{
    //    int[] arrApprovalKeys;
    //    int iArrControlSize = 1;

    //    arrApprovalKeys = new int[iArrControlSize];
    //    arrApprovalKeys[0] = 39;

    //    return arrApprovalKeys;
    //}
    //private void SetParameters(DateTime dCardDate, int iSugYom)
    //{
    //    oParam = new clParameters(dCardDate, iSugYom);
    //}
    //private void AddPeilutyotTnuaData(ref DataTable dtSidurimPeiluot, DateTime dCardDate)
    //{        
    //    clKavim oKavim = new clKavim();
    //    clKavim.enMakatType oMakatType;
    //    DataRow[] drElementim;
    //    DataTable dtElementim;
    //    DataTable dtKavim;
    //    DataTable dtLicenseNumbers;
    //    DataRow[] drLicenseNumbers;
    //    int iKodElement;
    //    int iMakatValid=0;
    //    long lMakatNesia;
    //    long lOtoNo;

    //    //לכל פעילות נקרא לפרצדורות של התנועה על מנת לקבל תיאור המקט, סוג קו ודקות בהגדרה
    //    try
    //    {
    //        AddSidurimPeiluyotColumn(ref dtSidurimPeiluot);
    //        // dtLicenseNumbers = oKavim.GetMasharData();
    //        dtElementim = GetCtbElementim(dCardDate);
    //        foreach (DataRow dr in dtSidurimPeiluot.Rows)
    //        {
    //            //נבדוק אם סידור הוא מיוחד או רגיל                

    //            ////lOtoNo = (System.Convert.IsDBNull(dr["oto_no"])) ? 0 : long.Parse(dr["oto_no"].ToString());
    //            ////if (lOtoNo > 0)
    //            ////{
    //            ////    if (dtLicenseNumbers.Rows.Count > 0)
    //            ////    {
    //            ////        drLicenseNumbers = dtLicenseNumbers.Select("bus_number=" + lOtoNo.ToString());
    //            ////        if (drLicenseNumbers.Length > 0)
    //            ////        {
    //            ////            dr["license_number"] = (System.Convert.IsDBNull(drLicenseNumbers[0]["license_number"])) ? 0 : long.Parse(drLicenseNumbers[0]["license_number"].ToString());
    //            ////        }
    //            ////    }
    //            ////}
    //            ////lMakatNesia = (System.Convert.IsDBNull(dr["makat_nesia"])) ? 0 : long.Parse(dr["makat_nesia"].ToString());
    //            ////if (lMakatNesia > 0)
    //            ////{
    //            ////    oMakatType = (clKavim.enMakatType)oKavim.GetMakatType(lMakatNesia);
    //            ////    switch (oMakatType)
    //            ////    {
    //            ////        case clKavim.enMakatType.mKavShirut:
    //            ////            dtKavim = oKavim.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);
    //            ////            if (dtKavim.Rows.Count > 0)
    //            ////            {
    //            ////                dr["makat_description"] = dtKavim.Rows[0]["Description"].ToString();
    //            ////                dr["makat_shilut"] = dtKavim.Rows[0]["Shilut"].ToString();
    //            ////                dr["Shirut_type_Name"] = dtKavim.Rows[0]["SugShirutName"].ToString();
    //            ////            }
    //            ////            break;
    //            ////        case clKavim.enMakatType.mEmpty:
    //            ////            dtKavim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
    //            ////            if (dtKavim.Rows.Count > 0)
    //            ////            {
    //            ////                dr["makat_description"] = dtKavim.Rows[0]["Description"].ToString();
    //            ////                //dr["makat_shilut"] = dtKavim.Rows[0]["Shilut"].ToString();
    //            ////                dr["Shirut_type_Name"] = COL_TRIP_EMPTY;
    //            ////            }
    //            ////            break;
    //            ////        case clKavim.enMakatType.mNamak:
    //            ////            dtKavim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
    //            ////            if (dtKavim.Rows.Count > 0)
    //            ////            {
    //            ////                dr["makat_description"] = dtKavim.Rows[0]["Description"].ToString();
    //            ////                dr["makat_shilut"] = dtKavim.Rows[0]["Shilut"].ToString();
    //            ////                dr["Shirut_type_Name"] = COL_TRIP_NAMAK;
    //            ////            }
    //            ////            break;
    //            ////        case clKavim.enMakatType.mElement:
    //            ////            iKodElement = int.Parse(lMakatNesia.ToString().Substring(1, 2));
    //            ////            drElementim = dtElementim.Select(string.Concat("KOD_ELEMENT=", iKodElement));
    //            ////            if (drElementim.Length > 0)
    //            ////            {
    //            ////                dr["makat_description"] = drElementim[0]["teur_element"];
    //            ////                //sSugShirutName = COL_TRIP_ELEMENT;                                                                          
    //            ////            }
    //            ////            break;
    //            ////        default:
    //            ////            break;
    //            ////    }
    //            //}
    //        }
    //    }
    //    catch(Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void AddSidurimPeiluyotColumn(ref DataTable dtSidurimPeiluot)
    {
        try
        {
            DataColumn col = new DataColumn();

            col = new DataColumn("Makat_Description", System.Type.GetType("System.String"));
            dtSidurimPeiluot.Columns.Add(col);

            col = new DataColumn("Makat_Shilut", System.Type.GetType("System.String"));
            dtSidurimPeiluot.Columns.Add(col);

            col = new DataColumn("Shirut_type_Name", System.Type.GetType("System.String"));
            dtSidurimPeiluot.Columns.Add(col);

            col = new DataColumn("License_number", System.Type.GetType("System.Int64"));
            dtSidurimPeiluot.Columns.Add(col);

            col = new DataColumn("Sidur_Myuhad", System.Type.GetType("System.Boolean"));
            dtSidurimPeiluot.Columns.Add(col);

            col = new DataColumn("Sidur_Driver", System.Type.GetType("System.Boolean"));
            dtSidurimPeiluot.Columns.Add(col);

            col = new DataColumn("Sug_Sidur", System.Type.GetType("System.Int32"));
            dtSidurimPeiluot.Columns.Add(col);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void RefreshEmployeeData(int iMisparIshi, DateTime dDateCard)
    {
        try
        {
            //נעלה את תאריך עדכון אחרון ומעדכן אחרון
            ShowLastUpdateData(iMisparIshi, dDateCard);
            //Get LookUp Tables
            //dtLookUp = oUtils.GetLookUpTables();
            ////Set LookUp DDL
            //SetLookUpDDL();
            //נתונים ברמת יום עבודה
            //ShowOvedCardDetails(iMisparIshi, dDateCard);
            //נתוני עובד
            ShowEmployeeDetails(iMisparIshi, dDateCard);

            //נעלה את כל הרשימות בסידורים
            //SetLookUpSidurim();
            //Set SecurityLevel
            //SetSecurityLevel();
            //Get Meafyeny Ovdim
            //oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dDateCard);    
            //העלאת דף עובד וזמני נסיעות
            DefineNavigatePages(dDateCard);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool EnabledTachograph()
    {
     //. רמת סידור - מותר לעדכן רק
     //אם מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
    //2. רמת פעילות - יש לבדוק שלפחות אחד הרכבים המדווחים באותו תאריך אינו מדגם 64  (דגם שאינו מכיל טכוגרף). Vehicle_Type =64 במעל"ה.
        bool bTachographAllowed = true;

        if ((lstSidurim.IsAtLeastOneSidurNahagut) && (!HasCardPeiluyot()))
            bTachographAllowed = true;
        else
            bTachographAllowed = HasVehicleTypeWithOutTachograph() && lstSidurim.IsAtLeastOneSidurNahagut;

        return bTachographAllowed;
    }
    protected bool HasCardPeiluyot()
    {
        bool bHasPeiluyot = false;
        //מחזיר TRUE אם יש פעילויות בכרטיס
        if (oBatchManager.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < oBatchManager.htFullEmployeeDetails.Count; i++)
            {
                if (((KdsBatch.clSidur)(oBatchManager.htFullEmployeeDetails[0])).htPeilut.Count > 0)
                {
                    bHasPeiluyot=true;
                    break;
               }
            }

        }
        return bHasPeiluyot;
    }
    protected bool EnabledHamaraForDay()
    {
        bool bHamaraAllowed = true;

        try
        {
            int iKodMaamad = String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sKodMaamd) ? 0 : int.Parse(oBatchManager.oOvedYomAvodaDetails.sKodMaamd);
            //לא נאפשר המרה לעובד שלא קיים לו מאפיין 31          
            //לא נאפשר המרה לעובד מאגד תעבורה  
            //לא נאפשר המרה אם יום כרטיס העבודה הוא לא יום שבת או שבתון
            //לא נאפשר המרה אם לפחות אחד מהסידורים המיוחדים לא זכאי להמרה
            //לא נאפשר המרה אם לפחות אחד מהסידורים הוא לא מסוג נהגות או ניהול
            //לא נאפשר המרה ביום שישי  אלא אם כן אחד מהסידורים נגמר לאחר כניסת השבת
            //אסור לעובד שהוא מותאם ליום עבודה קצר. יודעים שעובד הוא מותאים ליום קצר אם יש לו ערך בקוד נתון 8 (קוד עובד מותאם) 
            //Pirtey_Ovdim. Kod_Natun=8 וגם יש לו ערך בקוד נתון 20 (זמן מותאמות) Pirtey_Ovdim. Kod_Natun=2

            //אסור אם כל הסידורים ביום אינם מסוג נהגות או  ניהול תנועה (מיוחד או מפה)
            //TB_Meafyeney_Sug_Sidur.Kod_Meafyen=3 (סקטור עבודה)   TB_Meafyeney_Sug_Sidur.Erech=
            //4  (נהגות) 5 (ניהול תנועה)
            //TB_Sidurim_Meyuchadim.Kod_Meafyen=3 (סקטור עבודה TB_Sidurim_Meyuchadim.Erech=
            //4  (נהגות) 5 (ניהול תנועה

            //
            bHamaraAllowed = ((oBatchManager.htFullEmployeeDetails != null) && 
                (oBatchManager.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEgged.GetHashCode())
                //&& (oBatchManager.oMeafyeneyOved.Meafyen31Exists)
                && (
                    (oBatchManager.oOvedYomAvodaDetails.sShabaton == "1") || (oBatchManager.oOvedYomAvodaDetails.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString())
                    || ((oBatchManager.oOvedYomAvodaDetails.sErevShishiChag.Equals("1")) && (lstSidurim.bAtLeastOneSidurInShabat))
                    || ((oBatchManager.oOvedYomAvodaDetails.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) && (lstSidurim.bAtLeastOneSidurInShabat))
                   )
                //&& ((iKodMaamad == clGeneral.enHrMaamad.PermanentSalariedEmployee.GetHashCode())
                //    || (iKodMaamad == clGeneral.enHrMaamad.SalariedEmployee12.GetHashCode())
                //    || (iKodMaamad.ToString().Substring(0, 1).Equals("1")))
                && ((!lstSidurim.bAtLeatOneSidurIsNOTNahagutOrTnua))

                && ((!(oBatchManager.oOvedYomAvodaDetails.bMutamutExists)) && (oBatchManager.oOvedYomAvodaDetails.iZmanMutamut == 0))
                && (!((clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HAMARAT_SHABAT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))))
                );


            return bHamaraAllowed;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool EnabledHashlamaForDay()
    {
        /* 1. אין לאפשר שינוי הערך עבור עובד מאגד תעבורה (Ovdim.Kod_Hevra=4895 )
       2. אסור לעדכן ביום שבתון/שבת.
       3. ביום שישי (רק ערך 6 מה- Oracle, לא ערב חג) - מותר רק לעובד 6 ימים (מזהים לפי ערך 61, 62) במאפיין 56 במאפייני עובדים.*/

        bool bEnabled = true;
        int iSidurDay = String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sSidurDay) ? 0 : int.Parse(oBatchManager.oOvedYomAvodaDetails.sSidurDay);
       
        //אין לאפשר עדכון עבור עובד שאינו מאגד (Ovdim.Kod_Hevra=580 ) מאגד תעבורה (Ovdim.Kod_Hevra=4895)
        //2. אסור לעדכן ביום שבתון/שבת/ערב חג.
        if ((oBatchManager.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode()) ||
            (oBatchManager.oOvedYomAvodaDetails.sShabaton == "1") || (iSidurDay == clGeneral.enDay.Shabat.GetHashCode()))
            return false;
             
        //. עובד 5 ימים - מותר בימים  א-ה כולל ערבי חג בימים אלה. זיהוי עובד 5 ימים לפי לפי ערך 51/52 במאפיין 56 במאפייני עובדים.
        if (((iSidurDay == clGeneral.enDay.Shabat.GetHashCode()) || (oBatchManager.oOvedYomAvodaDetails.sShabaton == "1") || ((iSidurDay == clGeneral.enDay.Shishi.GetHashCode())))
            && ((oBatchManager.oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (oBatchManager.oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
            return false;

        //. עובד 6 ימים - מותר בימים  א-ו כולל ערבי חג בימים אלה. זיהוי עובד 6 ימים לפי לפי ערך 61/62 במאפיין 56 במאפייני עובדים
        if ((iSidurDay== clGeneral.enDay.Shabat.GetHashCode()) || (oBatchManager.oOvedYomAvodaDetails.sShabaton == "1"))
            if ((((oBatchManager.oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (oBatchManager.oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()))))
                return false;
     
        if (oBatchManager.oOvedYomAvodaDetails.sMutamut != clGeneral.enMutaam.enMutaam1.GetHashCode().ToString())
            return false;
       
        return bEnabled;
    }
    protected void EnabledFrames(bool bEnabled)
    {
        tbLblWorkDay.Disabled = (!bEnabled);
        if (ddlTravleTime.Enabled){        
            ddlTravleTime.Enabled = bEnabled;
        }
        if (ddlTachograph.Enabled){        
            ddlTachograph.Enabled = bEnabled;
        }
        if (ddlHalbasha.Enabled){        
            ddlHalbasha.Enabled = bEnabled;
        }
        if (ddlLina.Enabled){        
            ddlLina.Enabled = bEnabled;
        }
        if (ddlHashlamaReason.Enabled){        
            ddlHashlamaReason.Enabled = bEnabled;
            btnHashlamaForDay.Disabled = (!bEnabled);
        }
        //if (!btnHashlamaForDay.Disabled)        
        //    btnHashlamaForDay.Disabled = (!bEnabled);
        
        //tbValWorkDay.Disabled = (!bEnabled);
        tblPart.Disabled = (!bEnabled);
        tbSidur.Disabled = (!bEnabled);
        //tbBtn.Disabled = (!bEnabled);
        btnFindSidur.Enabled = bEnabled;
        btnAddHeadrut.Enabled = bEnabled;
    }
    protected void EnabledFields()
    {
        bool bEnable = EnabledHashlamaForDay();
        //לא נאפשר את שדה השלמהליום אם לא התקיימו התנאים ואם לא היה עדכון רשמת
        if (!btnHashlamaForDay.Disabled)
        {
            btnHashlamaForDay.Disabled = ((!bEnable) || ((clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))));
        }
        if (ddlHashlamaReason.Enabled) //אם קבענו כבר שלא ניתן לעדכן לא נשנה את הערך
        {
            ddlHashlamaReason.Enabled = ((bEnable) && (!(clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, clWorkCard.ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "SIBAT_HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))));
        }
    }
    private void InitSidurimUserControl()
    {
        clSidur _FirstSidur = new clSidur();

        if (oBatchManager.htFullEmployeeDetails != null)
        {
            if (oBatchManager.htFullEmployeeDetails.Count > 0)
            {
                _FirstSidur = ((clSidur)oBatchManager.htFullEmployeeDetails[0]);
            }
        }
        lstSidurim.Param1 = oBatchManager.oParam.dSidurStartLimitHourParam1;           
        lstSidurim.Param80 = oBatchManager.oParam.dNahagutLimitShatGmar;
        lstSidurim.Param3 = oBatchManager.oParam.dSidurEndLimitHourParam3;
        lstSidurim.Param29 = oBatchManager.oParam.dStartHourForPeilut;
        lstSidurim.Param30 = oBatchManager.oParam.dEndHourForPeilut;
        lstSidurim.Param101 = oBatchManager.oParam.iHashlamaYomRagil;
        lstSidurim.Param102 = oBatchManager.oParam.iHashlamaShisi;
        lstSidurim.Param103 = oBatchManager.oParam.iHashlamaShabat;
        lstSidurim.Param242 = oBatchManager.oParam.dShatGmarNextDay;
        lstSidurim.Param244 = oBatchManager.oParam.dShatHatchalaNahagutNihulTnua; 
        lstSidurim.RefreshBtn = (hidRefresh.Value!=string.Empty) ? int.Parse(hidRefresh.Value) : 0;
     
        if ((dDateCard.DayOfWeek==System.DayOfWeek.Friday)) 
            lstSidurim.NumOfHashlama = oBatchManager.oParam.iHashlamaMaxShisi; //109
        else if(dDateCard.DayOfWeek==System.DayOfWeek.Saturday)
            lstSidurim.NumOfHashlama = oBatchManager.oParam.iHashlamaMaxShabat; //110
        else
            lstSidurim.NumOfHashlama = oBatchManager.oParam.iHashlamaMaxYomRagil; //108

        if (oBatchManager.htFullEmployeeDetails != null)
        {
            if (oBatchManager.htFullEmployeeDetails.Count > 0)
            {
                if (_FirstSidur.sErevShishiChag.Equals("1"))
                    lstSidurim.NumOfHashlama = oBatchManager.oParam.iHashlamaMaxShisi; //109
                else if (_FirstSidur.sShabaton.Equals("1"))
                    lstSidurim.NumOfHashlama = oBatchManager.oParam.iHashlamaMaxShabat; //110
            }
        }
        lstSidurim.Param41 = oBatchManager.oParam.iZmanChariga;
        lstSidurim.Param149 = oBatchManager.oParam.fOrechNesiaKtzaraEilat;
        lstSidurim.SugeySidur = oBatchManager.dtSugSidur;
        lstSidurim.MeafyenyOved = oBatchManager.oMeafyeneyOved;
        lstSidurim.KdsParameters = oBatchManager.oParam;
        lstSidurim.OvedYomAvoda = oBatchManager.oOvedYomAvodaDetails;
        lstSidurim.MeafyeneyElementim = GetMeafyeneyElementim();
        lstSidurim.CardDate = dDateCard;
        lstSidurim.MisparIshi = iMisparIshi;
        lstSidurim.EmployeeApproval = arrEmployeeApproval;
        lstSidurim.dtSidurim = oBatchManager.dtDetails;
        lstSidurim.UpEmpDetails = upEmployeeDetails;
        lstSidurim.KnisatShabat = oBatchManager.oParam.dKnisatShabat;
        lstSidurim.ProfileRashemet = bRashemet;
        lstSidurim.MisparIshiIdkunRashemet = iMisparIshiIdkunRashemet;
        lstSidurim.LoginUserId = (int.Parse(LoginUser.UserInfo.EmployeeNumber));
        lstSidurim.Param98 = oBatchManager.oParam.iMaxMinutsForKnisot;
        lstSidurim.MeasherOMistayeg =  (clGeneral.enMeasherOMistayeg)oBatchManager.oOvedYomAvodaDetails.iMeasherOMistayeg;

    }
    private DataTable GetMeafyeneyElementim()
    {
        clKavim oKavim = new clKavim();
        DataTable dtElementim = new DataTable();
        try
        {
            dtElementim = oKavim.GetMeafyeneyElementByKod(0, dDateCard);
            return dtElementim;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DefineZmaniNesiotNavigatePage(DateTime dDateCard)
    {
        string sMeafyenEmployeeValue = "";
        string sMeafyenEmployeeType = "";
        if (oBatchManager.oMeafyeneyOved.Meafyen51Exists)
        {
            sMeafyenEmployeeValue = oBatchManager.oMeafyeneyOved.sMeafyen51;
            sMeafyenEmployeeType = "51";
        }
        else
        {
            if (oBatchManager.oMeafyeneyOved.Meafyen61Exists)
            {
                sMeafyenEmployeeValue = oBatchManager.oMeafyeneyOved.sMeafyen61;
                sMeafyenEmployeeType = "61";
            }
        }
        if (sMeafyenEmployeeValue != string.Empty)
        { //אם קיים מאפיין 51 או 61, ניצור לינק לפתיחת דף זמני נסיעות, אחרת נכניס מלל זמני ניסעות
            HyperLink lnkZmaneyNesiot = new HyperLink();
            lnkZmaneyNesiot.ID = "lnkTravelTime";
            lnkZmaneyNesiot.Text = "זמן נסיעות:";
            lnkZmaneyNesiot.Style.Add("text-decoration", "underline");
            lnkZmaneyNesiot.Style.Add("cursor", "pointer");
            ddlTravleTime.Attributes.Add("MeafyenVal", sMeafyenEmployeeValue == string.Empty ? "" : sMeafyenEmployeeValue.Substring(0, 1));
            ddlTravleTime.Attributes.Add("OrgVal", oBatchManager.oOvedYomAvodaDetails.sBitulZmanNesiot);
            ddlTravleTime.Attributes.Add("onchange", "chkTravelTime();");
            lnkZmaneyNesiot.Attributes.Add("OnClick", "javascript:OpenZmaneiNessiot(" +
                                            iMisparIshi.ToString() + ",'" +
                                            dDateCard.ToShortDateString() + "'," +
                                            sMeafyenEmployeeType + "," +
                                            sMeafyenEmployeeValue + ")");
            //lnkZmaneyNesiot.Target = "_self";
            //lnkZmaneyNesiot.NavigateUrl = String.Format(String.Concat(URLZmaniNesiotPage, "?id={0}&date={1}&type={2}&value={3}"), iMisparIshi.ToString(), dDateCard.ToShortDateString(), sMeafyenEmployeeType, sMeafyenEmployeeValue);
            tdZmaniNesiot.Controls.Add(lnkZmaneyNesiot);
        }
        else
        {
            Label lblNesiot = new Label();
            lblNesiot.ID = "lblTravelTime";
            lblNesiot.Text = "זמן נסיעות:";
            tdZmaniNesiot.Controls.Add(lblNesiot);
            ddlTravleTime.Enabled = false;
        }
    }

    private void DefineNavigatePages(DateTime dDateCard)
    {
        DefineZmaniNesiotNavigatePage(dDateCard);
        //lnkId.NavigateUrl = String.Format(String.Concat(URLEmplyeeDataPage, "?id={0}&month={1}"), iMisparIshi.ToString(), dDateCard.ToShortDateString().Substring(3));        
    }

    private void BindTachograph()
    {
        //טעינת קומבו טכוגרף. אין ערכים מטבלה
        ListItem Item = new ListItem();

        Item.Text = "אין צורך בטכוגרף";
        Item.Value = "0";
        ddlTachograph.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "חסר טכוגרף";
        Item.Value = "1";
        ddlTachograph.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "צירף טכוגרף";
        Item.Value = "2";
        ddlTachograph.Items.Add(Item);
        clUtils.BindTooltip(ddlTachograph);
    }

    private void BindToDDL(string sTableName, DropDownList ddl, bool bAddEmptyItem)
    {
        ListItem Item = new ListItem();
        DataView dv = new DataView(oBatchManager.dtLookUp);

        dv.RowFilter = string.Concat("table_name='", sTableName, "'");
        ddl.DataTextField = "teur";
        ddl.DataValueField = "kod";
        ddl.ToolTip = ddl.SelectedValue;
        ddl.DataSource = dv;
        ddl.DataBind();

        if (bAddEmptyItem)
        {
            Item.Text = "";
            Item.Value = "-1";
            ddl.Items.Insert(0, Item);
        }

        clUtils.BindTooltip(ddl);
    }

    private void SetLookUpSidurim()
    {
        clUtils oUtils = new clUtils();
        DataView dv = new DataView(oBatchManager.dtLookUp);

        try
        {
            //חריגה
            dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbDivuchHarigaMeshaot, "'");
            lstSidurim.DDLChariga = dv;

            //סיבות לדיווח ידני
            dvSibotLedivuch = new DataView(oUtils.GetCtbSibotLedivuchYadani());
            lstSidurim.DDLSibotLedivuch = dvSibotLedivuch;


            //פיצול הפסקה
            dv = new DataView(oBatchManager.dtLookUp);
            dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbPitzulaHafsaka, "'");
            lstSidurim.DDLPitzulHafsaka = dv;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    protected void BindToDDL(DataView dv, DropDownList DDL, bool bAddEmptyItem)
    {
        ListItem Item = new ListItem();

        DDL.DataTextField = "teur_siba";
        DDL.DataValueField = "kod_siba";
        DDL.DataSource = dv;
        DDL.DataBind();

        if (bAddEmptyItem){        
            Item.Text = "";
            Item.Value = "-1";
            DDL.Items.Insert(0, Item);            
        }
        
        clUtils.BindTooltip(DDL);       
    }
    protected void BindSibotLehashlamaLeyom()
    {
        ListItem Item = new ListItem();
        DataView dv = new DataView(oBatchManager.dtLookUp);

       
        if ((!bRashemet) && (ddlHashlamaReason.Enabled==true)){        
            dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbSibotHashlamaLeyom, "'", " and (kod='", clGeneral.enShowOvedSibatHashlamaLeyom.enOvedKod5.GetHashCode(), "' or kod='", clGeneral.enShowOvedSibatHashlamaLeyom.enBitulHashlama.GetHashCode(), "')");
        }
        else{        
            dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbSibotHashlamaLeyom, "'");
        }

        ddlHashlamaReason.DataTextField = "teur";
        ddlHashlamaReason.DataValueField = "kod";
        ddlHashlamaReason.ToolTip = ddlHashlamaReason.SelectedValue;
        ddlHashlamaReason.DataSource = dv;
        ddlHashlamaReason.DataBind();

        Item.Text = "";
        Item.Value = "-1";
        ddlHashlamaReason.Items.Insert(0, Item);
        
        clUtils.BindTooltip(ddlHashlamaReason);
        ddlHashlamaReason.SelectedValue = oBatchManager.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString() == "0" ? "-1" : oBatchManager.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString();
    }
    private void SetLookUpDDL()
    {
        try
        {
            BindToDDL(clGeneral.cCtbZmaneyNesiaa, ddlTravleTime, false);
            BindToDDL(clGeneral.cCtbLina, ddlLina, false);
            BindToDDL(clGeneral.cCtbZmaneyHalbasha, ddlHalbasha, false);
            //BindToDDL(clGeneral.cCtbSibotHashlamaLeyom, ddlHashlamaReason, true);
            //BindSibotLehashlamaLeyom();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void SetSecurityLevel()
    {
        try
        {
            KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
            if (iSecurity == KdsSecurityLevel.ViewAll){           
                AutoCompleteExtenderByName.ContextKey = "";
                AutoCompleteExtenderID.ContextKey = "";
            }

            else if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
            {
                AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;//"77783"
                AutoCompleteExtenderByName.ContextKey = LoginUser.UserInfo.EmployeeNumber;
            }
            else{            
                AutoCompleteExtenderByName.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                txtId.Text = LoginUser.UserInfo.EmployeeNumber;
                txtId.Enabled = false;
            }
            hidGoremMeasher.Value = LoginUser.UserInfo.EmployeeNumber;
            Session["SecurityLevel"] = iSecurity;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

    }
    protected void btnCalcItem_Click(object sender, EventArgs e)
    {

    }
    private void SetImageForButtonValiditiy()
    {
       
            DataView dv;
            //string strImageUrl = "";        
            // btnCardStatus.Text = oBatchManager.oOvedYomAvodaDetails.sStatusCardDesc;

         _StatusCard = oBatchManager.CardStatus;
        if (bRashemet)
        {
            if (((clGeneral.enCardStatus)(oBatchManager.oOvedYomAvodaDetails.iStatus)) != (_StatusCard))
            {
                dv = new DataView(oBatchManager.dtLookUp);
                dv.RowFilter = string.Concat("table_name='", "ctb_status_kartis", "' and kod =", oBatchManager.CardStatus.GetHashCode());
                lblCardStatus.InnerText = dv[0]["teur"].ToString();
            }
            else
            {
                lblCardStatus.InnerText = oBatchManager.oOvedYomAvodaDetails.sStatusCardDesc;
            }
            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Error:
                    lblCardStatus.Attributes.Add("class", "ImgBtnCStatusErr");
                    //strImageUrl = "../../Images/btn-error.jpg";
                    break;
                case clGeneral.enCardStatus.Valid:
                    lblCardStatus.Attributes.Add("class", "ImgBtnCStatusValid");
                    //strImageUrl = "../../Images/btn-ok.jpg";
                    break;
                case clGeneral.enCardStatus.Calculate:
                    lblCardStatus.Attributes.Add("class", "ImgBtnCStatusValid");
                    //strImageUrl = "../../Images/btn-ok.jpg";
                    break;
            }            
        }
        lstSidurim.StatusCard = _StatusCard;

        //btnCardStatus.Attributes.Add("style", string.Concat("background-image: url(", strImageUrl, ")"));
    }
    private bool IsWorkCardWasUpdate()
    {
        bWorkCardWasUpdateRun = true;
        clOvdim _Ovdim = new clOvdim();
        int iMisparIshiTrail;
        int iMeadkenAcharon;
        //נבדוק אם הכרטיס עודכן ע"י גורם אנושי לא ע"י מערכת - קוד מעדכן אחרון גדול מאפס
        bool bWasUpdate = false;
        DataTable dt = _Ovdim.GetLastUpdate(iMisparIshi, dDateCard);
        foreach (DataRow dr in dt.Rows)
        {
            iMeadkenAcharon=0;
            if (!String.IsNullOrEmpty(dr["MEADKEN_ACHARON"].ToString()))             
                iMeadkenAcharon =  int.Parse(dr["MEADKEN_ACHARON"].ToString());
                
            iMisparIshiTrail=0;
            if  (!String.IsNullOrEmpty(dr["MISPAR_ISHI_TRAIL"].ToString()))                
                    iMisparIshiTrail = int.Parse(dr["MISPAR_ISHI_TRAIL"].ToString());                

            if (((iMeadkenAcharon >= 0) && (iMeadkenAcharon != iMisparIshi)) || ((iMisparIshiTrail >= 0) && (iMisparIshiTrail != iMisparIshi)))             
                bWasUpdate = true;
                        
        }
        return bWasUpdate;
    }
    private void SetImageForButtonMeasherOMistayeg()
    {
        //clOvdim _Ovdim = new clOvdim();
        string strImageUrlApprove = "";
        string strImageUrlNotApprove = "";
        clGeneral.enMeasherOMistayeg oMasherOMistayeg;

        //if (LoginUser.IsLimitedUser)
        //{
        //    strImageUrlApprove = "ImgButtonApprovalRegular";
        //    strImageUrlNotApprove = "ImgButtonApprovalRegular";
        //}
        //else
        //{
            oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)oBatchManager.oOvedYomAvodaDetails.iMeasherOMistayeg;
            switch (oMasherOMistayeg)
            {
                case clGeneral.enMeasherOMistayeg.Measher:
                    strImageUrlApprove = "ImgButtonApprovalChecked";
                    strImageUrlNotApprove = "ImgButtonApprovalRegular";
                    break;
                case clGeneral.enMeasherOMistayeg.Mistayeg:
                    strImageUrlApprove = "ImgButtonApprovalRegular";
                    strImageUrlNotApprove = "ImgButtonApprovalChecked";
                    break;
                case clGeneral.enMeasherOMistayeg.ValueNull:
                    strImageUrlApprove = "ImgButtonApprovalRegular";
                    strImageUrlNotApprove = "ImgButtonApprovalRegular";
                    break;
                default:
                    strImageUrlApprove = "ImgButtonApprovalRegular";
                    strImageUrlNotApprove = "ImgButtonApprovalRegular";
                    break;
            }
       // }
        btnApprove.Attributes.Add("class", strImageUrlApprove);
        btnNotApprove.Attributes.Add("class", strImageUrlNotApprove);
    }

    private void SetImageForButtonHashlamaForDay()
    {
        HashlamaForDayValue.Value = "0";
        string strImageUrl = "../../Images/allscreens-checkbox-empty.jpg";
        if (!String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sHashlamaLeyom))
        {
            if (int.Parse(oBatchManager.oOvedYomAvodaDetails.sHashlamaLeyom) > 0)
            {
                strImageUrl = "../../Images/allscreens-checkbox.jpg";
                HashlamaForDayValue.Value = "1";
            }
        }
        btnHashlamaForDay.Attributes.Add("style", string.Concat("background-image: url(", strImageUrl, ")"));
        //btnHashlamaForDay.Disabled = ((clGeneral.enEmployeeType)oBatchManager.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora);

    }
    private void SetImageForButtonHamaratShabat()
    {
        Hamara.Value = "0";
        string strImageUrl = "../../Images/allscreens-checkbox-empty.jpg";
        if (!String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sHamara))
        {
            if (int.Parse(oBatchManager.oOvedYomAvodaDetails.sHamara) > 0)
            {
                strImageUrl = "../../Images/allscreens-checkbox.jpg";
                Hamara.Value = "1";
            }
            else
            {
                strImageUrl = "../../Images/allscreens-checkbox-empty.jpg";
                Hamara.Value = "0";
            }
        }
        btnHamara.Attributes.Add("style", string.Concat("background-image: url(", strImageUrl, ")"));
        // sHamara.Disabled = ((clGeneral.enEmployeeType)oBatchManager.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora);
    }
    private void SetCardStatus()
    {
        //Show Card Status
        string sDayDesc;
        //if (oBatchManager.oOvedYomAvodaDetails.iStatus == clGeneral.enStatusTipul.HistayemTipul.GetHashCode())
        //{
        //    lblTipul.Text = String.Concat("הסתיים טיפול בכ", (char)34, "ע!");
        //}

        //סוג יום 
        if (!String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sSidurDay))
        {
            sDayDesc = clGeneral.arrDays[int.Parse(oBatchManager.oOvedYomAvodaDetails.sSidurDay) - 1];
            txtDay.Text = clGeneral.arrDays[int.Parse(oBatchManager.oOvedYomAvodaDetails.sSidurDay) - 1];
            if (oBatchManager.oOvedYomAvodaDetails.sDayTypeDesc != String.Empty)
            {
                sDayDesc = String.Concat(sDayDesc, "-", oBatchManager.oOvedYomAvodaDetails.sDayTypeDesc);
            }
            txtDay.Text = sDayDesc;
            txtDay.ToolTip = sDayDesc;
        }
        else
        {
            txtDay.Text = "";
            vldDay.IsValid = false;
        }
    }
    private void ShowOvedCardDetails(int iMisparIshi, DateTime dCardDate)
    {
        //SetImageForButtonValiditiy();
        //SetImageForButtonMeasherOMistayeg();

        //סטטוס טיפול כרטיס
        SetCardStatus();
        if (oBatchManager.oOvedYomAvodaDetails.OvedDetailsExists){
        //DDL
        ddlTachograph.SelectedValue = oBatchManager.oOvedYomAvodaDetails.sTachograf;//String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sTachograf) ? "-1" : oBatchManager.oOvedYomAvodaDetails.sTachograf;
        ddlHalbasha.SelectedValue = oBatchManager.oOvedYomAvodaDetails.sHalbasha;//String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sHalbasha) ? "-1" : oBatchManager.oOvedYomAvodaDetails.sHalbasha;
        ddlLina.SelectedValue = oBatchManager.oOvedYomAvodaDetails.sLina;//String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sLina) ? "-1" : oBatchManager.oOvedYomAvodaDetails.sLina;
        ddlTravleTime.SelectedValue = oBatchManager.oOvedYomAvodaDetails.sBitulZmanNesiot;//String.IsNullOrEmpty(oBatchManager.oOvedYomAvodaDetails.sBitulZmanNesiot) ? "-1" : oBatchManager.oOvedYomAvodaDetails.sBitulZmanNesiot;
        // ddlTravleTime.Enabled = ((clGeneral.enEmployeeType)oBatchManager.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEgged);
        // ddlLina.Enabled = ((clGeneral.enEmployeeType)oBatchManager.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEgged);
        //השלמה ליום
        SetImageForButtonHashlamaForDay();
        //EnabledFields();
        //המרה
        SetImageForButtonHamaratShabat();
        //סיבות להשלמה
       // ddlHashlamaReason.SelectedValue = oBatchManager.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString() == "0" ? "-1" : oBatchManager.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString();
        ddlHalbasha.Enabled = false;
            }
    }

    private void ShowEmployeeDetails(int iMisparIshi, DateTime dCardDate)
    {
        //נתוני עובד
        clOvdim _Ovdim = new clOvdim();
        DataTable dt = new DataTable();

        dt = _Ovdim.GetOvedDetails(iMisparIshi, dCardDate);
        if (dt.Rows.Count > 0)
        {
            //מעמד
            txtMaamad.Text = dt.Rows[0]["teur_maamad_hr"].ToString();
            //עיסוק
            txtIsuk.Text = dt.Rows[0]["teur_isuk"].ToString();
            //איזור
            txtArea.Text = dt.Rows[0]["teur_ezor"].ToString();
            //סניף
            txtSnif.Text = dt.Rows[0]["teur_snif_av"].ToString();
            //חברה
            txtCompany.Text = dt.Rows[0]["teur_hevra"].ToString();
            //שם
            txtName.Text = dt.Rows[0]["full_name"].ToString();
        }
    }

    private void ShowLastUpdateData(int iMisparIshi, DateTime dCardDate)
    {
        //נעלה את תאריך עדכון אחרון ומעדכן אחרון
        clOvdim oOvdim = new clOvdim();
        DataTable dt = new DataTable();
        try
        {
            dt = oOvdim.GetLastUpdateData(iMisparIshi, dCardDate);
            if (dt.Rows.Count > 0)
            {
                lblLastUpdateDate.InnerText = DateTime.Parse(dt.Rows[0]["last_date"].ToString()).ToShortDateString();
                lnkLastUpdateUser.InnerText = dt.Rows[0]["MEADKEN_ACHARON"].ToString().IndexOf("-") >= 0 ? "מערכת" : dt.Rows[0]["full_name"].ToString();
                LastUpdateUserId.Value = dt.Rows[0]["MEADKEN_ACHARON"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCardStatus_Click(object sender, EventArgs e)
    {

    }

    protected void txtId_TextChanged(object sender, EventArgs e)
    {
        oBatchManager.IsExecuteErrors = false;
        ViewState["LoadNewCard"] = false;
    }

    protected void txtName_TextChanged(object sender, EventArgs e)
    {
    }
    protected void clnDate_TextChanged(object sender, EventArgs e)
    {
        oBatchManager.IsExecuteErrors = false;
        ViewState["LoadNewCard"] = false;
    }
    protected void btnRefreshOvedDetails_Click(object sender, EventArgs e)
    {
       // OrderedDictionary htSidurim = (OrderedDictionary)(Session["Sidurim"]);
        if (bInpuDataResult)
        {
           
//            hidExecInputChg.Value = "0";
            if (hidSave.Value.Equals("1"))
            {
                RunBatchFunctions();
                hidSave.Value = "0";
            }
            SetImageForButtonMeasherOMistayeg();
            oBatchManager.IsExecuteErrors = false;
            ShowOvedCardDetails(iMisparIshi, dDateCard);
            //SetLookUpDDL();

            ViewState["LoadNewCard"] = true;
            lstSidurim.RefreshBtn = 0;
            hidRefresh.Value = "0";
          
            lstSidurim.ClearControl();
            lstSidurim.BuildPage();
            //lstSidurim.UpdateHashTableWithGridChanges(ref htSidurim);
            //lstSidurim.ClearControl();
            //lstSidurim.BuildPage();
            string sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ");";
            ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);
           
        }
    }

    protected void btnAddHeadrut_Click(object sender, EventArgs e)
    {
        hidSave.Value = "0";
        hidExecInputChg.Value = "0";
        RunBatchFunctions();
        ViewState["LoadNewCard"] = true;
        lstSidurim.RefreshBtn = 0;
        hidRefresh.Value = "0";
        SetImageForButtonMeasherOMistayeg();
        lstSidurim.ClearControl();
        lstSidurim.BuildPage();
    }
    protected void btnFindSidur_Click(object sender, EventArgs e)
    {
        hidSave.Value = "0";    
        hidExecInputChg.Value="0";
        RunBatchFunctions();
        ViewState["LoadNewCard"] = true;
        lstSidurim.RefreshBtn = 0;
        hidRefresh.Value = "0";
        SetImageForButtonMeasherOMistayeg();
        lstSidurim.ClearControl();
        lstSidurim.BuildPage();
    }
    protected DataTable GetSidurimAndPeilut(int iMisparIshi, DateTime dCardDate)
    {
        //Return Sidurim and Peilut for Employee
        DataTable dt;
        clOvdim oOvdim = new clOvdim();
        try
        {
            dt = oOvdim.GetSidurimAndPeilut(iMisparIshi, dCardDate);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetCtbElementim(DateTime dCardDate)
    {
        clUtils oUtils = new clUtils();
        DataTable dtElementim;
        try
        {
            dtElementim = oUtils.GetCtbElementim();
            return dtElementim;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCloseCard_Click(object sender, EventArgs e)
    {
        ViewState["LoadNewCard"] = false;
        btnShowMessage_Click(this, new EventArgs());
    }
    protected void btnPrintWithoutUpdate_click(object sender, EventArgs e)
    {
        PrintCard(sender,e);
      
        MPEPrintMsg.Hide();
    }
    protected void btnUpdatePrint_click(object sender, EventArgs e)
    {
        if (SaveCard())
        {
            RunBatchFunctions();
            lstSidurim.DataSource = oBatchManager.htFullEmployeeDetails;        
            lstSidurim.ErrorsList = oBatchManager.dtErrors;
            lstSidurim.ClearControl();
            lstSidurim.BuildPage();          
            PrintCard(sender, e);           
        }
        MPEPrintMsg.Hide();
    }
    protected void PrintCard(object sender, EventArgs e)
    {
        string urlBarcode,key;
        try
        {
            bWorkCardWasUpdate = IsWorkCardWasUpdate();
            key = "|" + iMisparIshi.ToString() + "|" + dDateCard.ToString("yyyyMMdd") + "|";
            urlBarcode = ClBarcode.GetUrlBarcode(key, 90, 90);
            // if (LoginUser.IsLimitedUser && arrParams[2].ToString() == "1")
            if (hidFromEmda.Value == "true")
            {
                string sScript = "";
                string sIp;
                string sPathFilePrint = @"\\\\" + System.Environment.MachineName + @"\\kdsPrints\\" + LoginUser.UserInfo.EmployeeNumber + @"\\";
                byte[] s;

                ReportModule Report = ReportModule.GetInstance();
                Report.AddParameter("P_MISPAR_ISHI", iMisparIshi.ToString());
                Report.AddParameter("P_TAARICH", dDateCard.ToShortDateString());
                Report.AddParameter("P_EMDA", "0");
                Report.AddParameter("P_SIDUR_VISA", IsSidurVisaExists().GetHashCode().ToString());
                Report.AddParameter("P_URL_BARCODE", urlBarcode);
                Report.AddParameter("P_TIKUN", bWorkCardWasUpdate ? "1" : "0");

                s = Report.CreateReport("/KdsReports/PrintWorkCard", eFormat.PDF, true);

                string sFileName, sPathFile;
                FileStream fs;
                sIp = "";// arrParams[1];
                sFileName = "WorkCard.pdf";
                sPathFile = ConfigurationManager.AppSettings["PathFileReports"] + LoginUser.UserInfo.EmployeeNumber + @"\";
                if (!Directory.Exists(sPathFile))
                    Directory.CreateDirectory(sPathFile);
                fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Flush();
                fs.Close();

                for (int i = 0; i < oBatchManager.dtErrors.Rows.Count; i++)
                {
                    if (oBatchManager.dtErrors.Rows[i]["check_num"].ToString().Trim() == "69")
                    {
                        sScript = "document.all('msgErrCar').style.display='block';";
                        break;
                    }
                }
                sScript += "PrintDoc('" + sIp + "' ,'" + sPathFilePrint + sFileName + "'); document.all('prtMsg').style.display='block'; setTimeout(\"document.all('prtMsg').style.display = 'none'; document.all('btnCloseCard').click()\", 5000);";
                ScriptManager.RegisterStartupScript(btnPrint, btnPrint.GetType(), "PrintPdf", sScript, true);
            }
            else
            {
                Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
                ReportParameters.Add("P_MISPAR_ISHI", iMisparIshi.ToString());
                ReportParameters.Add("P_TAARICH", dDateCard.ToShortDateString());
                ReportParameters.Add("P_EMDA", "0");
                ReportParameters.Add("P_SIDUR_VISA", IsSidurVisaExists().GetHashCode().ToString());
                ReportParameters.Add("P_URL_BARCODE", urlBarcode);
                ReportParameters.Add("P_TIKUN", bWorkCardWasUpdate ? "1" : "0");

                OpenReport(ReportParameters, (Button)sender, ReportName.PrintWorkCard.ToString());
            }
        }
        catch (Exception ex)
        {
           clGeneral.BuildError(this, ex.Message,true);
        }
    }
    
   
    protected void btnPrint_click(object sender, EventArgs e)
    {
        if (hidChanges.Value.ToLower() == "true")       
            btnShowPrintMsg_Click(sender, e);                    
        else
            PrintCard(sender, e);



        string sScript = "SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ");";
        ScriptManager.RegisterStartupScript(btnPrint, this.GetType(), "PrintCard", sScript, true);  
    }
    protected void btnConfirm_click(object sender, EventArgs e)
    {
        string sScript;
        int iSidurIndex;
        ModalPopupEx.Hide();
       
        if (SaveCard())
        {          
            if (!(hidSave.Value.Equals("1")))
            {
                if ((Request.QueryString["Page"] != null))
                {
                    //עמדת נהג
                    switch (int.Parse(Request.QueryString["Page"]))
                    {
                        case 1:
                            Response.Redirect("EmployeeCards.aspx?EmpID=" + iMisparIshi.ToString() + "&WCardDate=" + dDateCard.ToShortDateString());
                            break;
                        case 2:
                            Response.Redirect("EmployeTotalMonthly.aspx?EmpID=" + iMisparIshi.ToString() + "&WCardDate=" + dDateCard.ToShortDateString());
                            break;
                    }
                }
                else
                {
                    sScript = "window.close();";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "closeCard", sScript, true);
                }
            }
            else
            {//יכיל מספר סידור, תאריך ושעה של סידור
                string[] arrVal = hidSadotLSidur.Value.Split(char.Parse(","));
                if (arrVal.Length>1)
                    if (arrVal[0].Equals("1"))
                    {
                        oBatchManager.InitGeneralData();
                        lstSidurim.DataSource = oBatchManager.htFullEmployeeDetails;
                        Session["Sidurim"] = lstSidurim.DataSource;
                        lstSidurim.ClearControl();
                        lstSidurim.BuildPage();
                        iSidurIndex= FindSidurIndex(DateTime.Parse(arrVal[1]),int.Parse(arrVal[2]),  lstSidurim.DataSource);
                        sScript = "bScreenChanged=true; ExecSadotLsidur(" + iSidurIndex + ",true);";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ExecSadotNosafim", sScript, true);
                       
                    }
            
            }
        }
        //Response.Redirect("WorkCard.aspx?EmpID=" + iMisparIshi + "&WCardDate=" + dDateCard.ToShortDateString());
        //שינויי קלט
        //oBatchManager.MainInputData(iMisparIshi, dDateCard);
        //oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
        //lstSidurim.DataSource = oBatchManager.htEmployeeDetails;        
        //lstSidurim.ErrorsList = oBatchManager.dtErrors;
        //lstSidurim.ClearControl();
        //lstSidurim.BuildPage();  
    }
    protected int FindSidurIndex(DateTime dSidurDate,int iSidurNumber, OrderedDictionary htSidurim)
    {
        int iSidurIndex = 0;
        clSidur _Sidur = new clSidur();
        try
        {
            for (int iIndex = 0; iIndex < htSidurim.Count; iIndex++)
            {
                _Sidur = (clSidur)(htSidurim[iIndex]);
                if ((_Sidur.iMisparSidur.Equals(iSidurNumber)) && (_Sidur.dFullShatHatchala.Equals(dSidurDate)))
                {
                    iSidurIndex = iIndex;
                    break;
                }
            }
            return iSidurIndex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnPopUpd_click(object sender, EventArgs e)
    {
        //mpeUpd.Hide();

        if (SaveCard())
        {
            //          hidRefresh.Value = "1";
            //    Response.Redirect("WorkCard.aspx?EmpID=" + iMisparIshi + "&WCardDate=" + dDateCard.ToShortDateString() + "&WCardUpdate=true");
            //          ViewState["LoadNewCard"] = true;
            //          LoadPage();

            //          //hidExecInputChg.Value = "1";
            //          ////נתונים כללים שמגיעים מאובייקט שגויים ושינויי קלט
            //          lstSidurim.ClearControl();
            //          lstSidurim.BuildPage();  

            hidRefresh.Value = "1";
            RunBatchFunctions();

            SetImageForButtonMeasherOMistayeg();
            oBatchManager.IsExecuteErrors = false;
            ShowOvedCardDetails(iMisparIshi, dDateCard);
            SetImageForButtonValiditiy();
            //SetLookUpDDL();

            ViewState["LoadNewCard"] = true;
            lstSidurim.RefreshBtn = 1;
            hidRefresh.Value = "0";

            _StatusCard = oBatchManager.CardStatus;
            lstSidurim.StatusCard = _StatusCard;
            lstSidurim.Mashar = (oBatchManager.dtMashar == null) ? (DataTable)Session["Mashar"] : oBatchManager.dtMashar;
            lstSidurim.DataSource = oBatchManager.htFullEmployeeDetails;
            lstSidurim.ErrorsList = oBatchManager.dtErrors;
            Session["Sidurim"] = oBatchManager.htFullEmployeeDetails;
            Session["Errors"] = oBatchManager.dtErrors;
            lstSidurim.ClearControl();
            lstSidurim.BuildPage();
            string sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ");";
            ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);
        }
       
    }
    protected void btnShowMessage_Click(object sender, EventArgs e)
    {
        ModalPopupEx.Show();
    }
    protected void btnShowPrintMsg_Click(object sender, EventArgs e)
    {
        MPEPrintMsg.Show();
    }
    protected void btnCancel_click(object sender, EventArgs e)
    {
        ModalPopupEx.Hide();
    }
    //protected void btnErrCancel_Click(object sender, EventArgs e)
    //{
    //    //ViewState["LoadNewCard"] = false;
    //    MPEErrors.Hide();
    //   // InsertToShgiotMeusharot();

    //}
    //protected void btnErrApproval_Click(object sender, EventArgs e)
    //{
    //    //ViewState["LoadNewCard"] = false;
    //    ApproveError();
    //    MPEErrors.Hide();
    //}
    //protected void btnUpdateCard_click(object sender, EventArgs e)
    //{
    // //   btnShowMessage_Click(this, new EventArgs());  
    //    ViewState["LoadNewCard"] = false;        
    //    SaveCard();

    //}
    //protected void btnErrClose_Click(object sender, EventArgs e)
    //{
    //    MPEErrors.Hide();
    //}

    //void InsertTriggersToUpdatePanel(UpdatePanel upEmployeeDetails, AsyncPostBackTrigger[] TriggerToAdd)
    //{
    //    GridView _GridView;
    //    GridViewRow _GridRow;
    //    ImageButton _ImgReka;
    //  //  HyperLink _Knisot = new HyperLink();
    //    for (int iIndex = 0; iIndex < this.lstSidurim.DataSource.Count; iIndex++)
    //    {
    //        _GridView = ((GridView)this.FindControl("lstsidurim").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
    //        if (_GridView != null)
    //        {
    //            for (int iRowIndex = 0; iRowIndex < _GridView.Rows.Count; iRowIndex++)
    //            {
    //                _GridRow = _GridView.Rows[iRowIndex];
    //                if ((_GridRow.Cells[lstSidurim.COL_ADD_NESIA_REKA].Controls.Count) > 0)
    //                {
    //                    _ImgReka = ((ImageButton)_GridRow.Cells[lstSidurim.COL_ADD_NESIA_REKA].Controls[0]);
    //                    //if (_GridRow.Cells[4].Controls.Count > 0)
    //                    //{
    //                       // _Knisot = ((HyperLink)_GridRow.Cells[4].Controls[0]);

    //                        // _ImgReka = (ImageButton)_GridRow.FindControl(_GridRow.Cells[lstSidurim.COL_ADD_NESIA_REKA].Controls[0].ClientID);
    //                        upEmployeeDetails.Triggers.Add(AddTrigger(_ImgReka.UniqueID));
    //                    //}
    //                }
    //            }
    //        }
    //    }
    //    //return;
    //    //for (int i = 0; i < TriggerToAdd.Length; i++)
    //    //    upEmployeeDetails.Triggers.Add(TriggerToAdd[i]);            
    //}
    //void lstSidurim_btnReka(string strRekaID,bool bDummy)
    //{
    //    int iSize;
    //    if (TriggerToAdd == null)
    //    {
    //        TriggerToAdd = new AsyncPostBackTrigger [1];
    //        TriggerToAdd[0] = AddTrigger(strRekaID);
    //    }
    //    else
    //    {
    //        iSize = TriggerToAdd.Length;
    //        TriggerToAdd = (AsyncPostBackTrigger[])clGeneral.ResizeArray(TriggerToAdd, iSize + 1);
    //        TriggerToAdd[iSize] = AddTrigger(strRekaID);
    //    }       
    //}

    //AsyncPostBackTrigger AddTrigger(string strRekaID)
    //{
    //    AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();

    //    trigger.ControlID = strRekaID;
    //   // trigger.EventName = "OnClick";

    //    return trigger;        
    //}
   

    void lstSidurim_btnHandler(string strValue, bool bOpenUpdateBtn)
    {
        bInpuDataResult = true;
        if (bOpenUpdateBtn)
        {
            string sScript = "SetBtnChanges();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenUpdBtn", sScript, true);
        }
        //oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
        //lstSidurim.DataSource = oBatchManager.htEmployeeDetails;
        //lstSidurim.ErrorsList = oBatchManager.dtErrors;
        //if (btnUpdateCard.Enabled)
        //{
        //    SaveCard();
        //    oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
        //    lstSidurim.DataSource = oBatchManager.htEmployeeDetails;
        //    lstSidurim.ErrorsList = oBatchManager.dtErrors;
        //    Session["Errors"] = oBatchManager.dtErrors;
        //}
    }
    protected bool SaveCard()
    {
        //שמירת כרטיס עבודה
        try
        {
            //fill changes data for compare

            bool bResult = false;
            clWorkCard _WorkCard = new clWorkCard();
                

            COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd = new COLL_SIDURIM_OVDIM();
            COLL_SIDURIM_OVDIM oCollSidurimOvdimDel = new COLL_SIDURIM_OVDIM();
            COLL_OBJ_PEILUT_OVDIM oCollPeluyotOvdimDel = new COLL_OBJ_PEILUT_OVDIM();
            COLL_SIDURIM_OVDIM oCollSidurimOvdimIns = new COLL_SIDURIM_OVDIM();
            COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd = new COLL_OBJ_PEILUT_OVDIM();
            COLL_OBJ_PEILUT_OVDIM oCollPeluyotOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
            COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd = new COLL_YAMEY_AVODA_OVDIM();
            COLL_IDKUN_RASHEMET oCollIdkunRashemet = new COLL_IDKUN_RASHEMET();
            //COLL_IDKUN_RASHEMET oCollIdkunRashemetDel = new COLL_IDKUN_RASHEMET();
            //COLL_IDKUN_RASHEMET oCollIdkunRashemetIns = new COLL_IDKUN_RASHEMET();

            //נכניס שינויים ברמת יום עבודה
            FillYemayAvodaChanges(ref oCollYameyAvodaUpd);

            if (this.lstSidurim.DataSource != null)
            {
                //נכניס את השינויים של סידורים ופעילויות
                if (!FillSidurimChanges(ref oCollSidurimOvdimUpd, ref oCollPeilutOvdimUpd, 
                                        ref oCollSidurimOvdimIns, ref oCollSidurimOvdimDel,
                                        ref oCollPeluyotOvdimDel, ref oCollPeluyotOvdimIns))
                {
                    //נמצאו פעילויות זהות
                    string sScript = "alert('קיימות פעילויות זהות, לא ניתן לשמור נתונים' )";
                    ScriptManager.RegisterStartupScript(btnUpdateCard, this.GetType(), "Save", sScript, true);
                }
                else
                {
                    // - סידורים נבדוק שאין כפילויות
                    if (!HasMultiSidurim(ref oCollSidurimOvdimIns, ref oCollSidurimOvdimUpd))
                    {
                        FillIdkunRashemet(ref oCollIdkunRashemet);
                        //FillIdkunRashemet(ref oCollIdkunRashemet, ref oCollIdkunRashemetIns, ref oCollIdkunRashemetDel);
                        _WorkCard.SaveEmployeeCard(oCollYameyAvodaUpd, oCollSidurimOvdimUpd, oCollPeilutOvdimUpd,
                                                   oCollSidurimOvdimIns, oCollSidurimOvdimDel,oCollPeluyotOvdimDel,
                                                   oCollIdkunRashemet, oCollPeluyotOvdimIns);
                        bResult = true;
                        ((HtmlInputHidden)(this.FindControl("hidLvl2Chg"))).Value = "";
                    }
                    else
                    {
                        string sScript = "alert('קיימים סידורים עם אותו מספר סידור ואותה שעת התחלה, לא ניתן לשמור נתונים' )";
                        ScriptManager.RegisterStartupScript(btnUpdateCard, this.GetType(), "Save", sScript, true);
                    }
                }
                HttpRuntime.Cache.Remove(iMisparIshi.ToString() + dDateCard.ToShortDateString());
            }
            return bResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool HasMultiSidurim(ref COLL_SIDURIM_OVDIM oCollSidurimOvdimIns, ref COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd)
    {
        bool bError = false;
        int  iCount=0; //אם נמצא פעם אחת זה הסידור עצמו מעל ל-1 יש כפילות
        for (int i = 0; i < oCollSidurimOvdimIns.Count; i++)
        {
            iCount = 0;
            for (int j = 0; j < oCollSidurimOvdimUpd.Count; j++)
            {
                if ((oCollSidurimOvdimIns.Value[i].MISPAR_SIDUR == oCollSidurimOvdimUpd.Value[j].MISPAR_SIDUR) &&
                    (oCollSidurimOvdimIns.Value[i].SHAT_HATCHALA == oCollSidurimOvdimUpd.Value[j].NEW_SHAT_HATCHALA) &&
                    (oCollSidurimOvdimUpd.Value[j].BITUL_O_HOSAFA != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()))                
                {
                    iCount = iCount + 1;
                    if (iCount > 1)
                    {
                        bError = true;
                        break;
                    }
                }                
            }
        }
        return bError;
    }
    private bool FillObjIdkunRashemet(clSidur oSidur,  string sFieldName, int iMisparSidur,
                                      DateTime dShatHatchala, ref OBJ_IDKUN_RASHEMET _ObjIdkunRashemet)
                                       
    {
        int iPakadId = clUtils.GetPakadId(dtPakadim, sFieldName);        
        bool bChanged = false;

        switch (sFieldName)
        {
            case "SHAT_HATCHALA":
                if (!oSidur.dOldFullShatHatchala.Equals(oSidur.dFullShatHatchala))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "SHAT_GMAR":
                if (!oSidur.dOldFullShatGmar.Equals(oSidur.dFullShatGmar))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;   
            case "KOD_SIBA_LEDIVUCH_YADANI_IN":
                if (!oSidur.iOldKodSibaLedivuchYadaniIn.Equals(oSidur.iKodSibaLedivuchYadaniIn))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "KOD_SIBA_LEDIVUCH_YADANI_OUT":
                if (!oSidur.iOldKodSibaLedivuchYadaniOut.Equals(oSidur.iKodSibaLedivuchYadaniOut))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;   
            case "SHAT_HATCHALA_LETASHLUM":
                if (!oSidur.dOldFullShatHatchalaLetashlum.Equals(oSidur.dFullShatHatchalaLetashlum))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "SHAT_GMAR_LETASHLUM":
                if (!oSidur.dOldFullShatGmarLetashlum.Equals(oSidur.dFullShatGmarLetashlum))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;   
            case "CHARIGA":
                if (!oSidur.sOldChariga.Equals(oSidur.sChariga))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "PITZUL_HAFSAKA":
                if (!oSidur.sOldPitzulHafsaka.Equals(oSidur.sPitzulHafsaka))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break; 
            case "HASHLAMA":
                if (!oSidur.sOldHashlama.Equals(oSidur.sHashlama))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "OUT_MICHSA":
                if (!oSidur.sOldOutMichsa.Equals(oSidur.sOutMichsa))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "LO_LETASHLUM":
                if (!oSidur.iOldLoLetashlum.Equals(oSidur.iLoLetashlum))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break; 
        }

        if (bChanged)
        {
            _ObjIdkunRashemet.TAARICH = dDateCard;
            _ObjIdkunRashemet.MISPAR_ISHI = iMisparIshi;
            _ObjIdkunRashemet.MISPAR_SIDUR = iMisparSidur;
            _ObjIdkunRashemet.SHAT_HATCHALA = dShatHatchala;
            _ObjIdkunRashemet.NEW_SHAT_HATCHALA = dShatHatchala;
            _ObjIdkunRashemet.SHAT_YETZIA = DateTime.MinValue;
            _ObjIdkunRashemet.NEW_SHAT_YETZIA = DateTime.MinValue;
            _ObjIdkunRashemet.MISPAR_KNISA = 0;
            _ObjIdkunRashemet.GOREM_MEADKEN = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        }
        return bChanged;
    }
    private bool FillObjIdkunRashemet(clSidur oSidur, int iPeilutIndex, string sFieldName , int iMisparSidur,
                                      DateTime dShatHatchala, DateTime dShatYetiza, 
                                      int iMisparKnisa, ref OBJ_IDKUN_RASHEMET _ObjIdkunRashemet)
    {
        int iPakadId= clUtils.GetPakadId(dtPakadim, sFieldName);
        clPeilut _Peilut;
        _Peilut = (clPeilut)oSidur.htPeilut[iPeilutIndex];

        bool bChanged = false;
       
        switch  (sFieldName)
        {
            case "OTO_NO":
                if (!_Peilut.lOldOtoNo.Equals(_Peilut.lOtoNo))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "MAKAT_NO":
                if (!_Peilut.lOldMakatNesia.Equals(_Peilut.lMakatNesia))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "KISUY_TOR":
                if (!_Peilut.iOldKisuyTor.Equals(_Peilut.iKisuyTor))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "DAKOT_BAFOAL":
                if (!_Peilut.iOldDakotBafoal.Equals(_Peilut.iDakotBafoal))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "SHAT_YETIZA":
                 if (!_Peilut.dOldFullShatYetzia.Equals(_Peilut.dFullShatYetzia))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
        }
        
        if (bChanged)
        {
            _ObjIdkunRashemet.TAARICH = dDateCard;
            _ObjIdkunRashemet.MISPAR_ISHI = iMisparIshi;
            _ObjIdkunRashemet.MISPAR_SIDUR = iMisparSidur;
            _ObjIdkunRashemet.SHAT_HATCHALA = dShatHatchala;
            _ObjIdkunRashemet.NEW_SHAT_HATCHALA =  dShatHatchala;
            _ObjIdkunRashemet.SHAT_YETZIA = _Peilut.oPeilutStatus == clPeilut.enPeilutStatus.enNew ? _Peilut.dFullShatYetzia : _Peilut.dOldFullShatYetzia;
            _ObjIdkunRashemet.NEW_SHAT_YETZIA = _Peilut.oPeilutStatus == clPeilut.enPeilutStatus.enNew ? _Peilut.dFullShatYetzia : dShatYetiza;
            _ObjIdkunRashemet.MISPAR_KNISA = iMisparKnisa;
            _ObjIdkunRashemet.GOREM_MEADKEN = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        }
        return bChanged;
    }
    private bool FillObjIdkunRashemet(Control obj, int iPakadId, int iMisparSidur, DateTime dShatHatchala, DateTime dShatYetiza, int iMisparKnisa, ref OBJ_IDKUN_RASHEMET _ObjIdkunRashemet)
    {
        bool bChanged = false;
        string _ControlType = obj.GetType().ToString();
        //_ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();

        switch (_ControlType)
        {
            case "System.Web.UI.WebControls.TextBox":
                TextBox _Txt = (TextBox)obj;
                if (_Txt.Attributes["OldV"]=="0") _Txt.Attributes["OldV"]="";
                if (_Txt.Text=="0") _Txt.Text="";
                if (!(_Txt.Attributes["OldV"]).ToString().Equals(_Txt.Text)) 
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "System.Web.UI.HtmlControls.HtmlInputHidden":
                HtmlInputHidden _HtmlInputBtn = (HtmlInputHidden)obj;
                if (!(_HtmlInputBtn.Attributes["OldV"]).ToString().Equals(_HtmlInputBtn.Value))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "System.Web.UI.WebControls.DropDownList":
                DropDownList _DDL = (DropDownList)obj;
                if (!(_DDL.Attributes["OldV"]).ToString().Equals(_DDL.SelectedValue))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "System.Web.UI.WebControls.CheckBox":
                CheckBox _Chk = (CheckBox)obj;
                if (!((_Chk.Attributes["OldV"]).ToString().Equals(_Chk.Checked.GetHashCode().ToString())))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "System.Web.UI.HtmlControls.HtmlInputCheckBox":
                HtmlInputCheckBox _HtmlChk = (HtmlInputCheckBox)obj;
                if (!((_HtmlChk.Attributes["OldV"]).ToString().Equals(_HtmlChk.Checked.GetHashCode().ToString())))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
        }
        if (bChanged)
        {
            _ObjIdkunRashemet.TAARICH = dDateCard;
            _ObjIdkunRashemet.MISPAR_ISHI = iMisparIshi;
            _ObjIdkunRashemet.MISPAR_SIDUR = iMisparSidur;
            _ObjIdkunRashemet.SHAT_HATCHALA = dShatHatchala;
            _ObjIdkunRashemet.NEW_SHAT_HATCHALA = dShatHatchala;
            _ObjIdkunRashemet.SHAT_YETZIA = dShatYetiza;
            _ObjIdkunRashemet.NEW_SHAT_YETZIA = dShatYetiza;
            _ObjIdkunRashemet.MISPAR_KNISA = iMisparKnisa;
            _ObjIdkunRashemet.GOREM_MEADKEN = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        }
        return bChanged;
    }
    private void FillIdkunRashemet(ref COLL_IDKUN_RASHEMET oCollIdkunRashemet)
    {
        OBJ_IDKUN_RASHEMET _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
        try
        {
            if (FillObjIdkunRashemet(ddlLina, clUtils.GetPakadId(dtPakadim, "LINA"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }
            if (FillObjIdkunRashemet(ddlTachograph, clUtils.GetPakadId(dtPakadim, "TACHOGRAF"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }
            if (FillObjIdkunRashemet(ddlTravleTime, clUtils.GetPakadId(dtPakadim, "BITUL_ZMAN_NESIOT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }
            if (FillObjIdkunRashemet(ddlHalbasha, clUtils.GetPakadId(dtPakadim, "HALBASHA"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }
            if (FillObjIdkunRashemet(ddlHashlamaReason, clUtils.GetPakadId(dtPakadim, "SIBAT_HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }
            if (FillObjIdkunRashemet(HashlamaForDayValue, clUtils.GetPakadId(dtPakadim, "HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }
            if (FillObjIdkunRashemet(Hamara, clUtils.GetPakadId(dtPakadim, "HAMARAT_SHABAT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref _ObjIdkunRashemet))
            {
                oCollIdkunRashemet.Add(_ObjIdkunRashemet);
            }

            //נעבור על כל הסידורים ונבדוק אילו שדות השתנו
            FillIdkunRashemetSidurim(ref oCollIdkunRashemet);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FillIdkunRashemetSidurim(ref COLL_IDKUN_RASHEMET oCollIdkunRashemet)
    {
        GridView oGridView;
        TextBox _Txt;
        DropDownList _DDL;
        //HtmlInputCheckBox _Chk;
        Label _Lbl = new Label();
        HyperLink _HypLnk = new HyperLink();
        int iMisarSidur;
        DateTime dShatHatchala, dNewShatHatchala;
        OBJ_IDKUN_RASHEMET _objIdkunRashemet = new OBJ_IDKUN_RASHEMET();
        clSidur oSidur;
        for (int iIndex = 0; iIndex < this.lstSidurim.DataSource.Count; iIndex++)
        {
            try
            {
                _Lbl = (Label)this.FindControl("lstSidurim").FindControl("lblSidur" + iIndex);
            }
            catch (Exception ex)
            {
                _HypLnk = (HyperLink)this.FindControl("lstSidurim").FindControl("lblSidur" + iIndex);
                _Lbl = null;
            }

            if ((_Lbl != null) || (_HypLnk != null))
            {
                _objIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                iMisarSidur = (_Lbl == null) ? int.Parse(_HypLnk.Text) : int.Parse(_Lbl.Text);

                //שעת התחלה             
                _Txt = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSH" + iIndex)));

                _objIdkunRashemet.SHAT_HATCHALA = DateTime.Parse(_Txt.Attributes["OrgShatHatchala"]);
                if (_Txt.Text == string.Empty)
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.Parse("01/01/0001 00:00:00");
                else
                {//נבדוק אם השתנה התאריך
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = GetSidurNewDate(iMisarSidur, _Txt.Text); //DateTime.Parse(dDateCard.ToShortDateString() + " " + string.Concat(oTxt.Text, ":", oObjSidurimOvdimUpd.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                    if (_objIdkunRashemet.NEW_SHAT_HATCHALA.Second!=_objIdkunRashemet.SHAT_HATCHALA.Second)
                       _objIdkunRashemet.NEW_SHAT_HATCHALA = _objIdkunRashemet.NEW_SHAT_HATCHALA.AddSeconds(double.Parse(_objIdkunRashemet.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));                    
                }
                dNewShatHatchala = _objIdkunRashemet.NEW_SHAT_HATCHALA;
                dShatHatchala = _objIdkunRashemet.SHAT_HATCHALA;
               
                oSidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iIndex]);
                
                if (FillObjIdkunRashemet(oSidur, "SHAT_HATCHALA", iMisarSidur, dShatHatchala, ref  _objIdkunRashemet))   
                //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_HATCHALA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                {
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                    //_objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                    _objIdkunRashemet.UPDATE_OBJECT = 1;
                    oCollIdkunRashemet.Add(_objIdkunRashemet);
                }

                //שעת גמר

                //_Txt = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSG" + iIndex)));
                //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_GMAR"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                if (FillObjIdkunRashemet(oSidur, "SHAT_GMAR", iMisarSidur, dShatHatchala, ref  _objIdkunRashemet))                                       
                {
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                    //_objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                    _objIdkunRashemet.UPDATE_OBJECT = 1;
                    oCollIdkunRashemet.Add(_objIdkunRashemet);
                }
                //התייצבות
                if (lstSidurim.FirstParticipate != null)
                {
                    if ((lstSidurim.FirstParticipate.iMisparSidur == iMisarSidur)
                        && (lstSidurim.FirstParticipate.dFullShatHatchala == dShatHatchala))
                    {                        
                        //if (FillObjIdkunRashemet(ddlFirstPart, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_IN"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                        //{
                        if (FillObjIdkunRashemet(oSidur, "KOD_SIBA_LEDIVUCH_YADANI_IN", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                        {
                            _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                         //   _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                            _objIdkunRashemet.UPDATE_OBJECT = 1;
                            oCollIdkunRashemet.Add(_objIdkunRashemet);
                        }
                    }
                }

                if (lstSidurim.SecondParticipate != null)
                {
                    if ((lstSidurim.SecondParticipate.iMisparSidur == iMisarSidur)
                        && (lstSidurim.SecondParticipate.dFullShatHatchala == dShatHatchala))
                    {
                        //if (FillObjIdkunRashemet(ddlSecPart, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_IN"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                        //{
                        if (FillObjIdkunRashemet(oSidur, "KOD_SIBA_LEDIVUCH_YADANI_OUT", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                        {
                            _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                           // _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                            _objIdkunRashemet.UPDATE_OBJECT = 1;
                            oCollIdkunRashemet.Add(_objIdkunRashemet);
                        }
                    }
                }
                _DDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlResonIn" + iIndex);
                if (_DDL != null)
                {
                    //שעת התחלה לתשלום
                    //_Txt = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSHL" + iIndex)));
                    //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_HATCHALA_LETASHLUM"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur,  "SHAT_HATCHALA_LETASHLUM", iMisarSidur, dShatHatchala,ref _objIdkunRashemet))
                    {
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                       // _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }

                    //שעת גמר לתשלום
                    //_Txt = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSGL" + iIndex)));
                    //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_GMAR_LETASHLUM"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "SHAT_GMAR_LETASHLUM", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                        //_objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }

                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_IN"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    //{
                    if (FillObjIdkunRashemet(oSidur, "KOD_SIBA_LEDIVUCH_YADANI_IN", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlResonOut" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_OUT"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "KOD_SIBA_LEDIVUCH_YADANI_OUT", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlException" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "CHARIGA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "CHARIGA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                     
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlPHfsaka" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "PITZUL_HAFSAKA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "PITZUL_HAFSAKA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlHashlama" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "HASHLAMA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "HASHLAMA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_Chk = (HtmlInputCheckBox)this.FindControl("lstSidurim").FindControl("chkOutMichsa" + iIndex);
                    //if (FillObjIdkunRashemet(_Chk, clUtils.GetPakadId(dtPakadim, "OUT_MICHSA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "OUT_MICHSA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_Chk = (HtmlInputCheckBox)this.FindControl("lstSidurim").FindControl("chkLoLetashlum" + iIndex);
                    //if (FillObjIdkunRashemet(_Chk, clUtils.GetPakadId(dtPakadim, "LO_LETASHLUM"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "LO_LETASHLUM", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                       // _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                }

                //נעבור על כל הפעילויות ונבדוק אילו פעילויות השתנו
                oGridView = ((GridView)this.FindControl("lstsidurim").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
                if (oGridView != null)
                {
                    FillIdkunRashemetPeiluyot(oSidur,iMisarSidur, dShatHatchala, dNewShatHatchala, ref oGridView, ref oCollIdkunRashemet);
                }
            }
        }
    }

    private void FillIdkunRashemetPeiluyot(clSidur oSidur, int iMisparSidur, DateTime dSidurShatHatchala, DateTime dNewShatHatchala, ref GridView oGridView, ref COLL_IDKUN_RASHEMET oCollIdkunRashemet)
    {
        OBJ_IDKUN_RASHEMET _objIdkunRashemet = new OBJ_IDKUN_RASHEMET();
        GridViewRow oGridRow;
        TextBox _Txt, _TxtShatYetiza;
        DateTime dShatYetiza, dNewShatYetiza;
        int iMisparKnisa = 0;
        int iDayToAdd = 0;
        string[] arrKnisaVal;

        //נבדוק אם נעשו שינויים אם כן נעדכן את טבלת עדכוני רשמת
        for (int iRowIndex = 0; iRowIndex < oGridView.Rows.Count; iRowIndex++)
        {
            oGridRow = oGridView.Rows[iRowIndex];
            _TxtShatYetiza = ((TextBox)oGridRow.Cells[lstSidurim.COL_SHAT_YETIZA].Controls[0]);
            _Txt=((TextBox)oGridRow.Cells[lstSidurim.COL_DAY_TO_ADD].Controls[0]);
            iDayToAdd = int.Parse(_Txt.Text);
            dNewShatYetiza = DateTime.Parse(String.Concat(_TxtShatYetiza.Attributes["OrgDate"], " ", _TxtShatYetiza.Text));
            //אם שעת היציאה היא של כרטיס העבודה ובמקור זה היה היום הבא, נוריד יום

            dShatYetiza = DateTime.Parse(_TxtShatYetiza.Attributes["OrgDate"]);
            if (dShatYetiza.Year > clGeneral.cYearNull)
            {
                if ((dShatYetiza.AddDays(-1) == dDateCard)
                    && (iDayToAdd == 0))
                    dNewShatYetiza = dNewShatYetiza.AddDays(-1);
            }
            //אם שעת היציאה היא של כרטיס העבודה נבדוק אם צריך להוסיף יום
            if (DateTime.Parse(String.Concat(_TxtShatYetiza.Attributes["OrgDate"])) == dDateCard)
                dNewShatYetiza=dNewShatYetiza.AddDays(iDayToAdd);
           

            dShatYetiza = DateTime.Parse(_TxtShatYetiza.Attributes["OrgShatYetiza"]);
            
            arrKnisaVal = oGridRow.Cells[lstSidurim.COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);

            //_Txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_KISUY_TOR].Controls[0]);
            //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "KISUY_TOR"), iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            if (FillObjIdkunRashemet(oSidur, iRowIndex, "KISUY_TOR", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))                                                  
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }
           // if (FillObjIdkunRashemet(_TxtShatYetiza, clUtils.GetPakadId(dtPakadim, "SHAT_YETIZA"), iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            if (FillObjIdkunRashemet(oSidur, iRowIndex,"SHAT_YETIZA", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }

            //_Txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]);
            //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "OTO_NO"), iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            if (FillObjIdkunRashemet(oSidur, iRowIndex,"OTO_NO", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))                                      
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }
            //_Txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]);
            //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "MAKAT_NO"), iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            if (FillObjIdkunRashemet(oSidur, iRowIndex, "MAKAT_NO", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))  
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }
            //_Txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]);
            //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "DAKOT_BAFOAL"), iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            if (FillObjIdkunRashemet(oSidur, iRowIndex, "DAKOT_BAFOAL", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))              
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }
        }
    }
    private void FillPeilutUDT(bool bUpdate,int iCancelSidur,
                               DateTime dOldSidurShatHatchala, 
                               DateTime dNewSidurShatHatchala, 
                               ref clSidur oSidur,
                               ref clPeilut oPeilut,
                               ref OBJ_PEILUT_OVDIM oObjPeluyotOvdim,
                               ref GridViewRow oGridRow)
    {
        int iDayToAdd, iMisparKnisa;
        string sDayToAdd, sShatYetiza, sKisuyTor;
        TextBox oShatYetiza;
        DateTime dShatYetiza, dKisuyTor;
        Double dblKisuyTor;
        string[] arrKnisaVal;
        int iCancelPeilut = 0;

        oShatYetiza = ((TextBox)oGridRow.Cells[lstSidurim.COL_SHAT_YETIZA].Controls[0]);
        sDayToAdd = ((TextBox)oGridRow.Cells[lstSidurim.COL_DAY_TO_ADD].Controls[0]).Text;
        iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);

        oObjPeluyotOvdim.MISPAR_ISHI = iMisparIshi;
        oObjPeluyotOvdim.TAARICH = dDateCard;
        oObjPeluyotOvdim.MISPAR_SIDUR = oSidur.iMisparSidur;

        oObjPeluyotOvdim.SHAT_HATCHALA_SIDUR = bUpdate ? dOldSidurShatHatchala : dNewSidurShatHatchala;
        oObjPeluyotOvdim.NEW_SHAT_HATCHALA_SIDUR = dNewSidurShatHatchala;

        dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
        sShatYetiza = oShatYetiza.Text;

        if (oShatYetiza.Text.Equals(""))
            dShatYetiza = DateTime.Parse("01/01/0001 00:00");
        else{
            if (bUpdate)
            {
                if (dShatYetiza.Date.Year < clGeneral.cYearNull)
                    if (oShatYetiza.Text != string.Empty)
                    {
                        if (DateTime.Parse(oShatYetiza.Text).Hour > 0)
                            oShatYetiza.Attributes["OrgDate"] = dNewSidurShatHatchala.ToShortDateString();
                    }
                    else //שעת יציאה ריקה
                        sShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]).ToShortTimeString();

                dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"] + " " + sShatYetiza);

                if (dShatYetiza.Date == dDateCard.Date)
                    dShatYetiza = dShatYetiza.AddDays(iDayToAdd);
                else
                    if (dShatYetiza.Date.Year > clGeneral.cYearNull)
                    {
                        if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
                            dShatYetiza = dShatYetiza.AddDays(-1);
                    }

            }
            else  //הכנסה של פעילות חדשה
            {
                //if (oShatYetiza.Text == string.Empty)
                //    dShatYetiza = DateTime.Parse(dNewSidurShatHatchala.ToShortDateString()  + " 00:00:00");
                //else
                dShatYetiza = DateTime.Parse(dDateCard.ToShortDateString() + " " + sShatYetiza).AddDays(iDayToAdd);
            }
        }

        sKisuyTor = ((TextBox)oGridRow.Cells[lstSidurim.COL_KISUY_TOR].Controls[0]).Text;
        if (sKisuyTor != string.Empty)
        {
            dKisuyTor = DateTime.Parse(dShatYetiza.ToShortDateString() + " " + sKisuyTor);
            dblKisuyTor = (dShatYetiza - dKisuyTor).TotalMinutes;
            if (dblKisuyTor < 0)
                dblKisuyTor = 1440 + dblKisuyTor;
        }
        else
            dblKisuyTor = 0;

        oObjPeluyotOvdim.KISUY_TOR = (int)(dblKisuyTor);
        oObjPeluyotOvdim.NEW_SHAT_YETZIA = dShatYetiza;
        oObjPeluyotOvdim.SHAT_YETZIA = bUpdate ? DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]) : dShatYetiza;
        oObjPeluyotOvdim.OTO_NO = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]).Text);        
        oObjPeluyotOvdim.MAKAT_NESIA = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]).Text);
        oObjPeluyotOvdim.DAKOT_BAFOAL = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]).Text);
        arrKnisaVal = oGridRow.Cells[lstSidurim.COL_KNISA].Text.Split(",".ToCharArray());
        iMisparKnisa = int.Parse(arrKnisaVal[0]);
        oObjPeluyotOvdim.MISPAR_KNISA = iMisparKnisa;
        //נבדוק אם נעשה שינוי באחת משדות הפעילות
        oObjPeluyotOvdim.TAARICH_IDKUN_ACHARON = oPeilut.dCardLastUpdate;
        if (PeilutHasChanged(oPeilut, oGridRow))//נבדוק אם נעשה שינוי באחת משדות הפעילות                  
        {
            oObjPeluyotOvdim.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            oObjPeluyotOvdim.TAARICH_IDKUN_ACHARON = DateTime.Now;
            oObjPeluyotOvdim.UPDATE_OBJECT = 1;
        }
        if (bUpdate)
        {
            iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_CANCEL_PEILUT].Controls[0]).Text);
            oObjPeluyotOvdim.BITUL_O_HOSAFA = ((iCancelSidur == 1) || (iCancelPeilut == 1)) ? 1 : ((iCancelSidur == 2) || (iCancelPeilut == 2) ? 2 : oPeilut.iBitulOHosafa);
            if (((iCancelPeilut == 1) || (iCancelPeilut == 2)) && (oObjPeluyotOvdim.BITUL_O_HOSAFA == clGeneral.enBitulOHosafa.AddAutomat.GetHashCode()))
                oObjPeluyotOvdim.UPDATE_OBJECT = 1;
        }
        else
        {
            oObjPeluyotOvdim.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.AddByUser.GetHashCode();
            oObjPeluyotOvdim.UPDATE_OBJECT = 1;
        }
        //נעדכן את ה-HashTable בערכים המקוריים
        oPeilut.dFullShatYetzia = oObjPeluyotOvdim.NEW_SHAT_YETZIA;
        oPeilut.lOtoNo = oObjPeluyotOvdim.OTO_NO;
        oPeilut.lMakatNesia = oObjPeluyotOvdim.MAKAT_NESIA;
        oPeilut.iKisuyTor = oObjPeluyotOvdim.KISUY_TOR;
        oPeilut.iDakotBafoal = oObjPeluyotOvdim.DAKOT_BAFOAL;
    }
    //private void UpdateHashTableWithGridChanges(ref OrderedDictionary htEmployeeDetails)
    //{              
    //    GridView oGridView;
    //    clSidur oSidur;
    //    Label oLbl;
    //    HyperLink oHypLnk=new HyperLink();
    //    TextBox oTxt, oShatGmar, oDayToAdd;
    //    DropDownList oDDL;
    //    string sTmp;
    //    DateTime dSidurDate;
    //    int iCancelSidur;
    //    HtmlInputCheckBox oChk;
    //    for (int iIndex = 0; iIndex < this.lstSidurim.DataSource.Count; iIndex++)
    //    {            
    //        try
    //        {
    //            oLbl = (Label)this.FindControl("lstSidurim").FindControl("lblSidur" + iIndex);
    //        }
    //        catch (Exception ex)
    //        {
    //            oHypLnk = (HyperLink)this.FindControl("lstSidurim").FindControl("lblSidur" + iIndex);
    //            oLbl = null;
    //        }
    //        if ((oLbl != null) || (oHypLnk != null))
    //        {
    //            oSidur = (clSidur)((htEmployeeDetails)[iIndex]);

    //            oSidur.iMisparSidur = (oLbl == null ? int.Parse(oHypLnk.Text) : int.Parse(oLbl.Text));
    //            oTxt = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSH" + iIndex)));
    //            if (oTxt.Text == string.Empty)
    //            {
    //                oSidur.dFullShatHatchala = DateTime.Parse("01/01/0001 00:00:00");
    //                oSidur.sShatHatchala = "";
    //            }
    //            else
    //            {
    //               oLbl =  (Label)this.FindControl("lstSidurim").FindControl("lblDate" + iIndex);
    //               oSidur.dFullShatHatchala = DateTime.Parse(oLbl.Text + " " + oTxt.Text);
    //               oSidur.sShatHatchala = oTxt.Text;
    //            }

    //            oShatGmar = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSG" + iIndex)));
    //            //מספר ימים להוספה 0 אם יום נוכחי1 - יום הבא
    //            oDayToAdd = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtDayAdd" + iIndex)));
    //            sTmp = oShatGmar.Text;
    //            dSidurDate = DateTime.Parse(oShatGmar.Attributes["OrgDate"].ToString() + " " + sTmp);
    //            oSidur.sShatGmar = sTmp;
    //            if (sTmp != string.Empty)
    //                oSidur.dFullShatGmar = DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));
    //            else
    //                oSidur.dFullShatGmar = DateTime.Parse("01/01/0001 00:00:00");


    //            oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlResonIn" + iIndex);
    //            if (oDDL != null)//רק אם שונה מסידור רציפות
    //            {
    //                oSidur.iKodSibaLedivuchYadaniIn = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

    //                oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlResonOut" + iIndex);
    //                oSidur.iKodSibaLedivuchYadaniOut = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

    //                sTmp = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSHL" + iIndex))).Text;
    //                oSidur.dFullShatHatchalaLetashlum = clGeneral.GetDateTimeFromStringHour(sTmp, dDateCard);
    //                oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToShortTimeString();

    //                sTmp = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSGL" + iIndex))).Text;
    //                oSidur.dFullShatGmarLetashlum = clGeneral.GetDateTimeFromStringHour(sTmp, dDateCard);
    //                oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToShortTimeString();

    //                oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlException" + iIndex);
    //                oSidur.sChariga = oDDL.SelectedValue;

    //                oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlPHfsaka" + iIndex);
    //                oSidur.sPitzulHafsaka = oDDL.Attributes["OldV"].ToString();

    //                oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlHashlama" + iIndex);
    //                oSidur.sHashlama = oDDL.SelectedValue;
    //                if (int.Parse(oDDL.SelectedValue) == clGeneral.enSugHashlama.enNoHashlama.GetHashCode())
    //                    oSidur.iSugHashlama = clGeneral.enSugHashlama.enNoHashlama.GetHashCode();
    //                else
    //                    oSidur.iSugHashlama = clGeneral.enSugHashlama.enHashlama.GetHashCode();

    //                oChk = (HtmlInputCheckBox)this.FindControl("lstSidurim").FindControl("chkOutMichsa" + iIndex);
    //                oSidur.sOutMichsa = oChk.Checked ? "1" : "0";

    //                iCancelSidur = int.Parse(((TextBox)this.FindControl("lstSidurim").FindControl("lblSidurCanceled" + iIndex)).Text);

    //                oSidur.iBitulOHosafa = iCancelSidur;

    //                //לא לתשלום - במידה וסידור בוטל נעדכן בערך המקורי - ישאר ללא שינוי
    //                oChk = (HtmlInputCheckBox)this.FindControl("lstSidurim").FindControl("chkLoLetashlum" + iIndex);

    //                int iLoLetashlumOrgVal = int.Parse(oChk.Attributes["OrgVal"].ToString());
                                       
    //                if (iCancelSidur == 1)
    //                    oSidur.iLoLetashlum = iLoLetashlumOrgVal;
    //                else
    //                    oSidur.iLoLetashlum = oChk.Checked ? 1 : 0;

    //                if ((oSidur.iLoLetashlum == 1) && (iLoLetashlumOrgVal == 0))
    //                    oSidur.iKodSibaLoLetashlum = KOD_SIBA_LO_LETASHLUM;
    //                else
    //                    if ((oSidur.iLoLetashlum == 0) && (iLoLetashlumOrgVal == 1))
    //                        oSidur.iKodSibaLoLetashlum = 0;  


    //                //התייצבות                            
    //                if (lstSidurim.FirstParticipate != null)
    //                {
    //                    if ((lstSidurim.FirstParticipate.iMisparSidur == oSidur.iMisparSidur)
    //                        && (lstSidurim.FirstParticipate.dFullShatHatchala == oSidur.dFullShatHatchala))
    //                    {
    //                        if (!String.IsNullOrEmpty(ddlFirstPart.SelectedValue))                            
    //                            oSidur.iKodSibaLedivuchYadaniIn = ddlFirstPart.SelectedValue.Equals("-1") ? 0 : int.Parse(ddlFirstPart.SelectedValue); 
                            
    //                        if (!String.IsNullOrEmpty(txtFirstPart.Text) && (txtFirstPart.Text.IndexOf(":") > 0))                            
    //                            oSidur.dShatHitiatzvut = DateTime.Parse(dDateCard.ToShortDateString() + " " + txtFirstPart.Text);                            
    //                    }
    //                }
    //                if (lstSidurim.SecondParticipate != null)
    //                {
    //                    if ((lstSidurim.SecondParticipate.iMisparSidur ==  oSidur.iMisparSidur)
    //                        && (lstSidurim.SecondParticipate.dFullShatHatchala == oSidur.dFullShatHatchala))
    //                    {
    //                        if ((!String.IsNullOrEmpty(txtSecPart.Text)) && (txtSecPart.Text.IndexOf(":") > 0))                            
    //                            oSidur.dShatHitiatzvut  = DateTime.Parse(txtSecPart.Text);
                            
    //                        if (!String.IsNullOrEmpty(ddlSecPart.SelectedValue))                            
    //                           oSidur.iKodSibaLedivuchYadaniIn= ddlSecPart.SelectedValue.Equals("-1") ? 0 : int.Parse(ddlSecPart.SelectedValue);
    //                    }
    //                }
    //                if ((((System.Web.UI.HtmlControls.HtmlInputHidden)(this.FindControl("hidLvl2Chg"))).Value.ToString().Equals("1")))                    
    //                    oSidur.lMeadkenAcharon=int.Parse(LoginUser.UserInfo.EmployeeNumber);

    //                //אם יש פעילויות, נכניס גם אותן
    //                oGridView = ((GridView)this.FindControl("lstsidurim").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
    //                if (oGridView != null)                    
    //                    UpdateHashTablePeiltoyWithGridChanges(iIndex, ref oGridView, ref htEmployeeDetails);                    
    //            }                
    //        } 
    //    }
    //    Session["Sidurim"] = htEmployeeDetails;
    //}
    
    //private void UpdateHashTablePeiltoyWithGridChanges(int iSidurIndex, ref GridView oGridView, ref OrderedDictionary htEmployeeDetails)
    //{
    //    GridViewRow oGridRow;
    //    int iDayToAdd, iMisparKnisa;
    //    string sDayToAdd, sShatYetiza, sKisuyTor;
    //    TextBox oShatYetiza;
    //    DateTime dShatYetiza, dKisuyTor;
    //    Double dblKisuyTor;
    //    string[] arrKnisaVal;
    //    clPeilut _Peilut;
    //    clSidur _Sidur;
    //    try
    //    {
    //        _Sidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
    //        //פעילויות
    //        for (int iRowIndex = 0; iRowIndex < oGridView.Rows.Count; iRowIndex++)
    //        {
    //            _Peilut = (clPeilut)_Sidur.htPeilut[iRowIndex];
    //            oGridRow = oGridView.Rows[iRowIndex];
    //            oShatYetiza = ((TextBox)oGridRow.Cells[lstSidurim.COL_SHAT_YETIZA].Controls[0]);
    //            sDayToAdd = ((TextBox)oGridRow.Cells[lstSidurim.COL_DAY_TO_ADD].Controls[0]).Text;
    //            iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);
                
    //            _Peilut.dCardDate = dDateCard;
    //            _Peilut.iPeilutMisparSidur = _Sidur.iMisparSidur;                

    //            dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
    //            sShatYetiza = oShatYetiza.Text;
    //            if (dShatYetiza.Date.Year < clGeneral.cYearNull)
    //                if (oShatYetiza.Text != string.Empty)
    //                {
    //                    if (DateTime.Parse(oShatYetiza.Text).Hour > 0)
    //                        oShatYetiza.Attributes["OrgDate"] = _Sidur.dFullShatHatchala.ToShortDateString();
    //                }
    //                else //שעת יציאה ריקה
    //                    sShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]).ToShortTimeString();

    //            dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"] + " " + sShatYetiza);

    //            if (dShatYetiza.Date == dDateCard.Date)
    //                dShatYetiza = dShatYetiza.AddDays(iDayToAdd);
    //            else
    //                if (dShatYetiza.Date.Year > clGeneral.cYearNull)
    //                {
    //                    if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
    //                        dShatYetiza = dShatYetiza.AddDays(-1);
    //                }
    //            sKisuyTor = ((TextBox)oGridRow.Cells[lstSidurim.COL_KISUY_TOR].Controls[0]).Text;
    //            if (sKisuyTor != string.Empty)
    //            {
    //                dKisuyTor = DateTime.Parse(dShatYetiza.ToShortDateString() + " " + sKisuyTor);
    //                dblKisuyTor = (dShatYetiza - dKisuyTor).TotalMinutes;
    //                if (dblKisuyTor < 0)
    //                    dblKisuyTor = 1440 + dblKisuyTor;
    //            }
    //            else
    //                dblKisuyTor = 0;

    //            _Peilut.iKisuyTor = (int)(dblKisuyTor);
    //            _Peilut.dFullShatYetzia = dShatYetiza;
    //            _Peilut.sShatYetzia = oShatYetiza.Text;
    //            _Peilut.lMisparSiduriOto = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]).Text);
    //            _Peilut.lMakatNesia  = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]).Text);
    //            _Peilut.iDakotBafoal = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]).Text);
    //            arrKnisaVal = oGridRow.Cells[lstSidurim.COL_KNISA].Text.Split(",".ToCharArray());
    //            iMisparKnisa = int.Parse(arrKnisaVal[0]);
    //            _Peilut.iMisparKnisa = iMisparKnisa;                              
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}   
    private bool FillPeiluyotChanges(bool bInsert,int iSidurIndex, int iCancelSidur, DateTime dOldSidurShatHatchala,
                                     DateTime dNewSidurShatHatchala, ref COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd,
                                     ref COLL_OBJ_PEILUT_OVDIM oCollPeluyotOvdimDel,
                                     ref COLL_OBJ_PEILUT_OVDIM oCollPeluyotOvdimIns,
                                     ref GridView oGridView, ref clSidur oSidur)
    {
        bool bValid = true;
        GridViewRow oGridRow;
        OBJ_PEILUT_OVDIM oObjPeiluyotOvdim;      
        clPeilut _Peilut;
        int iCancelPeilut;

        OBJ_PEILUT_OVDIM oObjPeilyutOvdimDel;
        try
        {
            //פעילויות
            for (int iRowIndex = 0; iRowIndex < oGridView.Rows.Count; iRowIndex++)
            {
                _Peilut = (clPeilut)oSidur.htPeilut[iRowIndex];
                oGridRow = oGridView.Rows[iRowIndex];
                iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_CANCEL_PEILUT].Controls[0]).Text);

                //If new active and sidur or peilut was canceld, we will not add the active.
                if ((_Peilut.oPeilutStatus.Equals(clPeilut.enPeilutStatus.enNew)) && (iCancelPeilut == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode() || (iCancelSidur == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())))
                    continue;
                else
                {
                   
                    if (_Peilut.oPeilutStatus.Equals(clPeilut.enPeilutStatus.enNew))
                    {
                        //פעילות חדשה
                        oObjPeiluyotOvdim = new OBJ_PEILUT_OVDIM();
                        if (bInsert)
                            oObjPeiluyotOvdim.UPDATE_OBJECT = 1;
                        FillPeilutUDT(false, iCancelSidur, dOldSidurShatHatchala, dNewSidurShatHatchala, ref oSidur, ref _Peilut, ref oObjPeiluyotOvdim, ref oGridRow);
                        oCollPeluyotOvdimIns.Add(oObjPeiluyotOvdim);
                    }
                    else
                    {
                        //פעילות קיימת
                        oObjPeiluyotOvdim = new OBJ_PEILUT_OVDIM();
                        FillPeilutUDT(true, iCancelSidur, dOldSidurShatHatchala, dNewSidurShatHatchala, ref oSidur, ref _Peilut, ref oObjPeiluyotOvdim, ref oGridRow);

                        if (bInsert)
                            oObjPeiluyotOvdim.UPDATE_OBJECT = 1;

                        //אם פעילות בוטלה או סידור של הפעילות בוטל, נמחוק את הפעילות מטבלת פעילויות
                        if (oObjPeiluyotOvdim.BITUL_O_HOSAFA == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())
                        {
                            oObjPeilyutOvdimDel = new OBJ_PEILUT_OVDIM();
                            FillPeiluyotForDelete(ref  oObjPeilyutOvdimDel, ref  oObjPeiluyotOvdim);
                            oCollPeluyotOvdimDel.Add(oObjPeilyutOvdimDel);
                            oObjPeiluyotOvdim.UPDATE_OBJECT = 0;
                        }
                    }
                    //נבדוק אם מפתח כבר קיים
                    //אם קיים, נחזיר שגיאה ולא נאפשר שמירת נתונים
                   
                    if (!HasMultiPeiluyot(ref oCollPeilutOvdimUpd, ref oObjPeiluyotOvdim))
                        oCollPeilutOvdimUpd.Add(oObjPeiluyotOvdim);
                    else
                    {
                        bValid = false;
                        break;
                    }                   
                }
            }
            return bValid;
                //oShatYetiza = ((TextBox)oGridRow.Cells[lstSidurim.COL_SHAT_YETIZA].Controls[0]);
                //sDayToAdd = ((TextBox)oGridRow.Cells[lstSidurim.COL_DAY_TO_ADD].Controls[0]).Text;
                //iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);
                //oObjPeiluyotOvdimUpd = new OBJ_PEILUT_OVDIM();
                //oObjPeiluyotOvdimUpd.MISPAR_ISHI = iMisparIshi;
                //oObjPeiluyotOvdimUpd.TAARICH = dDateCard;
                //oObjPeiluyotOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;//int.Parse(((Label)(this.FindControl("lstSidurim").FindControl("lblSidur" + iSidurIndex))).Text);
                ////sTmp = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSH" + iSidurIndex))).Text;
                //oObjPeiluyotOvdimUpd.SHAT_HATCHALA_SIDUR = dOldSidurShatHatchala;
                //oObjPeiluyotOvdimUpd.NEW_SHAT_HATCHALA_SIDUR = dNewSidurShatHatchala;

                //dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
                //sShatYetiza = oShatYetiza.Text;
                //if (dShatYetiza.Date.Year < clGeneral.cYearNull)
                //    if (oShatYetiza.Text != string.Empty)
                //    {
                //        if (DateTime.Parse(oShatYetiza.Text).Hour > 0)
                //            oShatYetiza.Attributes["OrgDate"] = dNewSidurShatHatchala.ToShortDateString();
                //    }
                //    else //שעת יציאה ריקה
                //        sShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]).ToShortTimeString();

                //dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"] + " " + sShatYetiza);
               
                //if (dShatYetiza.Date == dDateCard.Date)                
                //    dShatYetiza = dShatYetiza.AddDays(iDayToAdd);                
                //else
                //    if (dShatYetiza.Date.Year > clGeneral.cYearNull)
                //    {
                //        if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
                //            dShatYetiza = dShatYetiza.AddDays(-1);
                //    }
                //sKisuyTor = ((TextBox)oGridRow.Cells[lstSidurim.COL_KISUY_TOR].Controls[0]).Text;
                //if (sKisuyTor != string.Empty)
                //{
                //    dKisuyTor = DateTime.Parse(dShatYetiza.ToShortDateString() + " " + sKisuyTor);
                //    dblKisuyTor = (dShatYetiza - dKisuyTor).TotalMinutes;
                //    if (dblKisuyTor < 0)
                //        dblKisuyTor = 1440 + dblKisuyTor;
                //}
                //else                
                //    dblKisuyTor = 0;
                

                //oObjPeiluyotOvdimUpd.KISUY_TOR = (int)(dblKisuyTor);
                //oObjPeiluyotOvdimUpd.NEW_SHAT_YETZIA = dShatYetiza;
                //oObjPeiluyotOvdimUpd.SHAT_YETZIA = DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]);
                //oObjPeiluyotOvdimUpd.OTO_NO = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]).Text);
                //oObjPeiluyotOvdimUpd.MAKAT_NESIA = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]).Text);
                //oObjPeiluyotOvdimUpd.DAKOT_BAFOAL = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]).Text);
                //arrKnisaVal = oGridRow.Cells[lstSidurim.COL_KNISA].Text.Split(",".ToCharArray());
                //iMisparKnisa = int.Parse(arrKnisaVal[0]);
                //oObjPeiluyotOvdimUpd.MISPAR_KNISA = iMisparKnisa;//String.IsNullOrEmpty(oGridRow.Cells[lstSidurim.COL_KNISA].Text) ? 0 : int.Parse(oGridRow.Cells[lstSidurim.COL_KNISA].Text);
                ////if ((((System.Web.UI.HtmlControls.HtmlInputHidden)(this.FindControl("hidLvl3Chg"))).Value.ToString().Equals("1")))
                ////נבדוק אם נעשה שינוי באחת משדות הפעילות
                //oObjPeiluyotOvdimUpd.TAARICH_IDKUN_ACHARON = _Peilut.dCardLastUpdate;
                //if (PeilutHasChanged(_Peilut, oGridRow))//נבדוק אם נעשה שינוי באחת משדות הפעילות                  
                //{
                //    oObjPeiluyotOvdimUpd.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                //    oObjPeiluyotOvdimUpd.TAARICH_IDKUN_ACHARON = DateTime.Now;
                //}
                ////else                
                ////    oObjPeiluyotOvdimUpd.TAARICH_IDKUN_ACHARON = _Peilut.dCardLastUpdate;
                
                //oObjPeiluyotOvdimUpd.UPDATE_OBJECT = 1;
                //iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[lstSidurim.COL_CANCEL_PEILUT].Controls[0]).Text);
                ////אם הפעילות הייתה מבוטלת וכרגע רוצים להפעיל אותה מחדש, ניתן ערך 2
                ////if (((_Peilut.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) || (_Peilut.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())) && (iCancelPeilut == 0))               
                ////    oObjPeiluyotOvdimUpd.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.AddByUser.GetHashCode();                
                ////else                
                //oObjPeiluyotOvdimUpd.BITUL_O_HOSAFA = ((iCancelSidur == 1) || (iCancelPeilut == 1)) ? 1 : ((iCancelSidur == 2) || (iCancelPeilut == 2) ? 2 : _Peilut.iBitulOHosafa);
                ////אם פעילות בוטלה או סידור של הפעילות בוטל, נמחוק את הפעילות מטבלת פעילויות
                //if (oObjPeiluyotOvdimUpd.BITUL_O_HOSAFA == 1)
                //{
                //    oObjPeilyutOvdimDel = new OBJ_PEILUT_OVDIM();
                //    FillPeiluyotForDelete(ref  oObjPeilyutOvdimDel, ref  oObjPeiluyotOvdimUpd);
                //    oCollPeluyotOvdimDel.Add(oObjPeilyutOvdimDel);
                //    oObjPeiluyotOvdimUpd.UPDATE_OBJECT = 0;
                //}

                ////נבדוק אם מפתח כבר קיים
                ////אם קיים, נחזיר שגיאה ולא נאפשר שמירת נתונים
                //if (!HasMultiPeiluyot(ref oCollPeilutOvdimUpd, ref oObjPeiluyotOvdimUpd))                
                //    oCollPeilutOvdimUpd.Add(oObjPeiluyotOvdimUpd);                
                //else
                //{
                //    bValid = false;
                //    break;
                //}
           // }
            //return bValid;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //private bool SidurHasChanged(clSidur oSidur, GridViewRow oGridRow)
    //{
    //}
    private bool PeilutHasChanged(clPeilut oPeilut, GridViewRow oGridRow)
    {
       // bool bChanged = false;
        TextBox _txt;
        DateTime _PeilutDate = new DateTime();

        //שעת יציאה
        _txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_SHAT_YETIZA].Controls[0]);
        if (((TextBox)oGridRow.Cells[lstSidurim.COL_DAY_TO_ADD].Controls[0]).Text == "1")
            _PeilutDate = DateTime.Parse(dDateCard.ToShortDateString() + " " + _txt.Text).AddDays(1);
        else
            _PeilutDate = DateTime.Parse(dDateCard.ToShortDateString() + " " + _txt.Text);

        if (_PeilutDate != oPeilut.dOldFullShatYetzia)
            return true;
        
      
        ////כיסוי תור
        _txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_KISUY_TOR].Controls[0]);
        if (_txt.Text == string.Empty){
             if (oPeilut.iOldKisuyTor!=0)
               return true;
        }
        else{
            if ((((_PeilutDate-DateTime.Parse(_PeilutDate.ToShortDateString() + " " +_txt.Text)) )).TotalMinutes != oPeilut.iOldKisuyTor)
               return true;
        }
        //מק"ט
        _txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_MAKAT].Controls[0]);
        if (_txt.Text != string.Empty)
        {
            if (long.Parse(_txt.Text) != oPeilut.lOldMakatNesia)
                return true;
        }
        else
        {
            if (oPeilut.lOldMakatNesia != 0)
                return true;
        }
        
        
        //דקות בפועל
        _txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_ACTUAL_MINUTES].Controls[0]);
        if (_txt.Text == string.Empty)
        {
            if (oPeilut.iOldDakotBafoal != 0)
                return true;
        }
        else
        {
            if (int.Parse(_txt.Text) != oPeilut.iOldDakotBafoal)
                return true;
        }

        //מספר רכב
        _txt = ((TextBox)oGridRow.Cells[lstSidurim.COL_CAR_NUMBER].Controls[0]);
        if (_txt.Text == string.Empty)
        {
            if (oPeilut.lOldOtoNo != 0)
                return true;
        }
        else
        {
            if (long.Parse(_txt.Text) != oPeilut.lOldOtoNo)
                return true;
        }

        //ביטול-הוספה
        bool bCancelPeilut = ((System.Web.UI.WebControls.WebControl)(((Button)(oGridRow.Cells[lstSidurim.COL_CANCEL].Controls[0])))).CssClass == "ImgCancel";
        //if ((((oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) || (oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())) && (!bCancelPeilut)) ||
         //   (((oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.AddAutomat.GetHashCode()) || (oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.AddByUser.GetHashCode())) && (bCancelPeilut)))
        if ((oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.AddAutomat.GetHashCode()) && (bCancelPeilut))
            return true;

        return false;
    }
    private bool HasMultiPeiluyot(ref COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd, ref OBJ_PEILUT_OVDIM oObjPeiluyotOvdimUpd)
    {
        bool bHasMultiPeilyot = false;
        //נבדוק אם יש פעילויות עם אותו מפתח
        try
        {
            for (int i = 0; i < oCollPeilutOvdimUpd.Count; i++)
            {
                if ((oCollPeilutOvdimUpd.Value[i].MISPAR_SIDUR == oObjPeiluyotOvdimUpd.MISPAR_SIDUR) &&
                    (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_HATCHALA_SIDUR == oObjPeiluyotOvdimUpd.NEW_SHAT_HATCHALA_SIDUR) &&
                    (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_YETZIA == oObjPeiluyotOvdimUpd.NEW_SHAT_YETZIA) &&
                    (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA == oObjPeiluyotOvdimUpd.MISPAR_KNISA) && 
                    (oCollPeilutOvdimUpd.Value[i].BITUL_O_HOSAFA!=clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()) && 
                    (oObjPeiluyotOvdimUpd.BITUL_O_HOSAFA!=clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()))
                {
                    bHasMultiPeilyot = true;
                }
            }
            return bHasMultiPeilyot;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DateTime GetSidurNewDate(int iSidurKey, string sSidurHour)
    {
        //מחזיר את  התארי המעודכן של הסידור
        //DataTable dtUpdateSidurim = (DataTable)Session["SidurimUpdated"];
        DataRow[] dr;
        DateTime dtDate= new DateTime();
        dr = ((DataTable)Session["SidurimUpdated"]).Select("sidur_number=" + iSidurKey + " and sidur_start_hour='" + sSidurHour + "'");
        if (dr.Length > 0)
            dtDate = DateTime.Parse(dr[0]["sidur_date"].ToString());


        return dtDate;
    }
    private bool FillSidurimChanges(ref COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd,
                                    ref COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd,
                                    ref COLL_SIDURIM_OVDIM oCollSidurimOvdimIns,
                                    ref COLL_SIDURIM_OVDIM oCollSidurimOvdimDel,
                                    ref COLL_OBJ_PEILUT_OVDIM oCollPeluyotOvdimDel,
                                    ref COLL_OBJ_PEILUT_OVDIM oCollPeluyotOvdimIns)
    {
        OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
        OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns;
        OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;
        try
        {
            //סידורים        
            int iCancelSidur = 0;
            Label oLbl = new Label();
            HyperLink oHypLnk = new HyperLink();
            DropDownList oDDL;
            HtmlInputCheckBox oChk;
            string sTmp;
            int iPitzulHafsakaOldValue=0;

            DateTime dSidurDate;
            GridView oGridView;
            TextBox oTxt, oShatGmar, oDayToAdd;
            bool bInsert = false;
            clSidur oSidur;
            bool bValid = true;
            string sSidurimThatChanged = "";
            //נעבור על כל הסידורים
            
                for (int iIndex = 0; iIndex < this.lstSidurim.DataSource.Count; iIndex++)
                {
                    oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                    try
                    {
                        oLbl = (Label)this.FindControl("lstSidurim").FindControl("lblSidur" + iIndex);
                    }
                    catch (Exception ex)
                    {
                        oHypLnk = (HyperLink)this.FindControl("lstSidurim").FindControl("lblSidur" + iIndex);
                        oLbl = null;
                    }
                    if ((oLbl != null) || (oHypLnk != null))
                    {
                        //oSidur = (clSidur)(oBatchManager.htEmployeeDetails[iIndex]);                      
                        oSidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iIndex]);    
                        oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
                        oObjSidurimOvdimUpd.MISPAR_ISHI = iMisparIshi;
                        oObjSidurimOvdimUpd.TAARICH = dDateCard;
                        oObjSidurimOvdimUpd.MISPAR_SIDUR = (oLbl == null ? int.Parse(oHypLnk.Text) : int.Parse(oLbl.Text));
                        oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
                        oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = oSidur.iShayahLeyomKodem;
                        oTxt = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSH" + iIndex)));
                        oObjSidurimOvdimUpd.SHAT_HATCHALA = DateTime.Parse(oTxt.Attributes["OrgShatHatchala"]);

                        if (oTxt.Text == string.Empty)
                        {
                            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = DateTime.Parse("01/01/0001 00:00:00");
                            oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = 0;
                        }
                        else
                        {//נבדוק אם השתנה התאריך
                            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = GetSidurNewDate(oObjSidurimOvdimUpd.MISPAR_SIDUR, oTxt.Text); //DateTime.Parse(dDateCard.ToShortDateString() + " " + string.Concat(oTxt.Text, ":", oObjSidurimOvdimUpd.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                            if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.Second != oObjSidurimOvdimUpd.SHAT_HATCHALA.Second)                                
                                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.AddSeconds(double.Parse(oObjSidurimOvdimUpd.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                            //אם תאריך הסידור גדול מתאריך כרטיס העבודה נסמן בסידור, שייך ליום קודם
                            if ((!oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.ToShortDateString().Equals(dDateCard.ToShortDateString())))
                                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = 1;
                            else
                                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = 0;
                        }

                        //אם השתנתה שעת ההתחלה של הסידור, נכניס סידור חדש ונמחק את הקודם
                        //כמו כן נעדכן את שעת התחלת הסידור לפעילויות שמקושרות לסידור
                        bInsert = ((oTxt.Text != (DateTime.Parse(oTxt.Attributes["OrgShatHatchala"].ToString()).ToShortTimeString())) &&
                                  (!((oTxt.Text == "") && (DateTime.Parse(oTxt.Attributes["OrgShatHatchala"].ToString()).Year<=clGeneral.cYearNull))));

                        oShatGmar = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSG" + iIndex)));
                        //מספר ימים להוספה 0 אם יום נוכחי1 - יום הבא
                        oDayToAdd = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtDayAdd" + iIndex)));
                        sTmp = oShatGmar.Text;
                        dSidurDate = DateTime.Parse(oSidur.dOldFullShatGmar.ToShortDateString() + " " + sTmp); //DateTime.Parse(oShatGmar.Attributes["OrgDate"].ToString() + " " + sTmp);
                       
                        if (sTmp != string.Empty)
                            oObjSidurimOvdimUpd.SHAT_GMAR = DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));
                        else
                            oObjSidurimOvdimUpd.SHAT_GMAR = DateTime.Parse("01/01/0001 00:00:00");
                      
                        
                        oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlResonIn" + iIndex);
                        if (oDDL != null)//רק אם שונה מסידור רציפות
                        {
                            oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

                            oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlResonOut" + iIndex);
                            oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

                            sTmp = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSHL" + iIndex))).Text;
                            if (sTmp == string.Empty)
                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = clGeneral.GetDateTimeFromStringHour(sTmp, DateTime.Parse("01/01/0001 00:00:00"));
                            else
                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = clGeneral.GetDateTimeFromStringHour(sTmp, DateTime.Parse(oSidur.dFullShatHatchala.ToShortDateString()));//DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp);

                            sTmp = ((TextBox)(this.FindControl("lstSidurim").FindControl("txtSGL" + iIndex))).Text;
                            if (sTmp==string.Empty)
                                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = clGeneral.GetDateTimeFromStringHour(sTmp, DateTime.Parse("01/01/0001 00:00:00"));//DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp); //TODO:
                            else
                             oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = clGeneral.GetDateTimeFromStringHour(sTmp, DateTime.Parse(oSidur.dFullShatGmar.ToShortDateString()));//DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp); //TODO:

                            oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlException" + iIndex);
                            oObjSidurimOvdimUpd.CHARIGA = int.Parse(oDDL.SelectedValue);

                            oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlPHfsaka" + iIndex);
                            iPitzulHafsakaOldValue =int.Parse(oSidur.sOldPitzulHafsaka);// int.Parse(oDDL.Attributes["OldV"].ToString());
                            oObjSidurimOvdimUpd.PITZUL_HAFSAKA = int.Parse(oDDL.SelectedValue);
                            //במידה והסידור הוא סידור מיוחד עם מאפיין 54 (שעון נוכחות) ובשדה פיצול הפסקה נבחר ערך "הפסקה ע"ח העובד" (KOD_PIZUL_HAFSAKA=3) יש לפצל  
                            // את הסידור 

                       
                            oDDL = (DropDownList)this.FindControl("lstSidurim").FindControl("ddlHashlama" + iIndex);
                            oObjSidurimOvdimUpd.HASHLAMA = int.Parse(oDDL.SelectedValue);
                            if (int.Parse(oDDL.SelectedValue) == clGeneral.enSugHashlama.enNoHashlama.GetHashCode())                            
                                oObjSidurimOvdimUpd.SUG_HASHLAMA = clGeneral.enSugHashlama.enNoHashlama.GetHashCode();
                            
                            else                            
                                oObjSidurimOvdimUpd.SUG_HASHLAMA = clGeneral.enSugHashlama.enHashlama.GetHashCode();
                            
                            oChk = (HtmlInputCheckBox)this.FindControl("lstSidurim").FindControl("chkOutMichsa" + iIndex);
                            oObjSidurimOvdimUpd.OUT_MICHSA = oChk.Checked ? 1 : 0;


                            iCancelSidur = int.Parse(((TextBox)this.FindControl("lstSidurim").FindControl("lblSidurCanceled" + iIndex)).Text);
                            //אם סידור היה מבוטל והמשתמש מחזיר לפעיל, נזין ערך 2
                            //if (((oSidur.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) || (oSidur.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())) && (iCancelSidur == 0))                       
                            //    oObjSidurimOvdimUpd.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.AddByUser.GetHashCode();                       
                            //else                        
                            oObjSidurimOvdimUpd.BITUL_O_HOSAFA = iCancelSidur;
                          
                            //לא לתשלום - במידה וסידור בוטל נעדכן בערך המקורי - ישאר ללא שינוי
                            oChk = (HtmlInputCheckBox)this.FindControl("lstSidurim").FindControl("chkLoLetashlum" + iIndex);
                            int iLoLetashlumOrgVal = int.Parse(oChk.Attributes["OrgVal"].ToString());

                            //נשמור את הערך ההתחלתי
                            oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = oSidur.iKodSibaLoLetashlum;
                            if (iCancelSidur == 1)                        
                                oObjSidurimOvdimUpd.LO_LETASHLUM = iLoLetashlumOrgVal;                        
                            else                        
                                oObjSidurimOvdimUpd.LO_LETASHLUM = oChk.Checked ? 1 : 0;


                            if ((oObjSidurimOvdimUpd.LO_LETASHLUM == 1) && (iLoLetashlumOrgVal == 0))                        
                                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = KOD_SIBA_LO_LETASHLUM;                        
                            else                       
                                if ((oObjSidurimOvdimUpd.LO_LETASHLUM == 0) && (iLoLetashlumOrgVal == 1))                            
                                    oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 0;                            
                        


                            //התייצבות
                            
                            if (lstSidurim.FirstParticipate != null)
                            {
                                if ((lstSidurim.FirstParticipate.iMisparSidur == oObjSidurimOvdimUpd.MISPAR_SIDUR)
                                    && (lstSidurim.FirstParticipate.dFullShatHatchala == oObjSidurimOvdimUpd.SHAT_HATCHALA))
                                {
                                    if (!String.IsNullOrEmpty(ddlFirstPart.SelectedValue))
                                    {
                                        oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = ddlFirstPart.SelectedValue.Equals("-1") ? 0 : int.Parse(ddlFirstPart.SelectedValue); // int.Parse(ddlFirstPart.SelectedValue);                                        
                                        if (oSidur.iKodSibaLedivuchYadaniIn!=oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN) 
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                    }
                                    if (!String.IsNullOrEmpty(txtFirstPart.Text) && (txtFirstPart.Text.IndexOf(":") > 0))
                                    {
                                        oObjSidurimOvdimUpd.SHAT_HITIATZVUT = DateTime.Parse(oSidur.dFullShatHatchala.ToShortDateString() + " " + txtFirstPart.Text);
                                        if (oSidur.dShatHitiatzvut!=oObjSidurimOvdimUpd.SHAT_HITIATZVUT)
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                    }
                                }
                            }


                            if (lstSidurim.SecondParticipate != null)
                            {
                                if ((lstSidurim.SecondParticipate.iMisparSidur == oObjSidurimOvdimUpd.MISPAR_SIDUR)
                                    && (lstSidurim.SecondParticipate.dFullShatHatchala == oObjSidurimOvdimUpd.SHAT_HATCHALA))
                                {
                                    if ((!String.IsNullOrEmpty(txtSecPart.Text)) && (txtSecPart.Text.IndexOf(":") > 0))
                                    {
                                        oObjSidurimOvdimUpd.SHAT_HITIATZVUT = DateTime.Parse(oSidur.dFullShatHatchala.ToShortDateString() + " " + txtSecPart.Text);
                                        if (oSidur.dShatHitiatzvut != oObjSidurimOvdimUpd.SHAT_HITIATZVUT)
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                    }
                                    if (!String.IsNullOrEmpty(ddlSecPart.SelectedValue))
                                    {
                                        oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = ddlSecPart.SelectedValue.Equals("-1") ? 0 : int.Parse(ddlSecPart.SelectedValue);//int.Parse(ddlSecPart.SelectedValue);
                                        if (oSidur.iKodSibaLedivuchYadaniIn != oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN)
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                    }
                                }
                            }
                            //}                      
                        }
                        sSidurimThatChanged = ((HtmlInputHidden)(this.FindControl("hidLvl2Chg"))).Value;
                        if (sSidurimThatChanged.IndexOf(iIndex.ToString())>-1)                                                   
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        
                        oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA = String.IsNullOrEmpty(oSidur.sMikumShaonKnisa) ? 0 : int.Parse(oSidur.sMikumShaonKnisa);
                        oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA = String.IsNullOrEmpty(oSidur.sMikumShaonYetzia) ? 0 : int.Parse(oSidur.sMikumShaonYetzia);
                        //שדות נוספים
                        oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA  = oSidur.iAchuzKnasLepremyatVisa;
                        oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = oSidur.iAchuzVizaBesikun;
                        oObjSidurimOvdimUpd.YOM_VISA = String.IsNullOrEmpty(oSidur.sVisa) ? 0 : int.Parse(oSidur.sVisa);
                        oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = oSidur.iMisparMusachOMachsan;
                        oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = oSidur.iMisparShiureyNehiga;
                        oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = oSidur.iSugHazmanatVisa;
                        oObjSidurimOvdimUpd.MIVTZA_VISA = oSidur.iMivtzaVisa;
                        oObjSidurimOvdimUpd.TAFKID_VISA = oSidur.iTafkidVisa;

                        if (oObjSidurimOvdimUpd.UPDATE_OBJECT==1)
                            oObjSidurimOvdimUpd.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);

                        oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);

                        if (iCancelSidur == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())
                        {
                            //אם סידור בוטל, נמחוק אותו מטבלת tb_sidurim_ovdim   
                            oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                            FillSidurimForDelete(ref oObjSidurimOvdimDel, ref oObjSidurimOvdimUpd);
                            oObjSidurimOvdimDel.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.BitulByUser.GetHashCode(); //^^^^
                            oObjSidurimOvdimDel.UPDATE_OBJECT = 0;  //^^^^^
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;                            
                            oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                        }
                        else
                        {
                            if ((bInsert))
                            {
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;
                                oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();

                                FillSidurimForInsert(ref oObjSidurimOvdimIns, ref oObjSidurimOvdimUpd, ref oSidur);
                                FillSidurimForDelete(ref oObjSidurimOvdimDel, ref oObjSidurimOvdimUpd);
                                oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
                                oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                            }
                        }
                        if (((oObjSidurimOvdimUpd.PITZUL_HAFSAKA == clGeneral.enShowPitzul.enOvedHafsaka.GetHashCode()) && (iPitzulHafsakaOldValue!=clGeneral.enShowPitzul.enOvedHafsaka.GetHashCode()) && (oSidur.bSidurMyuhad)) && (oSidur.bShaonNochachutExists))
                        {
                            oObjSidurimOvdimIns = SplitSidur(ref oObjSidurimOvdimUpd, ref oSidur);
                            oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
                        }
                        oSidur.dFullShatHatchala = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                        oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                        oSidur.iKodSibaLedivuchYadaniIn = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN;
                        oSidur.iKodSibaLedivuchYadaniOut = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT;
                        oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                        oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                        oSidur.sChariga = oObjSidurimOvdimUpd.CHARIGA.ToString();
                        oSidur.sPitzulHafsaka = oObjSidurimOvdimUpd.PITZUL_HAFSAKA.ToString();
                        oSidur.sHashlama = oObjSidurimOvdimUpd.HASHLAMA.ToString();
                        oSidur.sOutMichsa = oObjSidurimOvdimUpd.OUT_MICHSA.ToString();
                        oSidur.iLoLetashlum = oObjSidurimOvdimUpd.LO_LETASHLUM;

                        //אם יש פעילויות, נכניס גם אותן
                        oGridView = ((GridView)this.FindControl("lstsidurim").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
                        if (oGridView != null)
                        {
                            if (!(FillPeiluyotChanges(bInsert,iIndex, iCancelSidur, oObjSidurimOvdimUpd.SHAT_HATCHALA, 
                                  oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA, ref oCollPeilutOvdimUpd, 
                                  ref  oCollPeluyotOvdimDel, ref oCollPeluyotOvdimIns, ref oGridView, ref oSidur)))
                            {
                                bValid = false;
                                break;
                            }
                        }
                    }
                }
       
            return bValid;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //private void AddEmptyRowToPeilutGrid(int iSidurIndex)
    //{
    //    GridView _GridView;
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        //Update DataTable which bind to grid with new data
    //        _GridView = ((GridView)this.FindControl("lstsidurim").FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
    //        if (_GridView != null)
    //         {
    //             //בניית עמודות הטבלה
    //             lstSidurim.BuildDataTableColumns(ref dt);
    //             UpdatePeilutDataTable(iSidurIndex,ref dt, _GridView);
    //             AddEmptyLineToDataTable(ref dt);
    //             BindNewDataTableToGrid(ref dt, _GridView);
    //         }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    private void BindNewDataTableToGrid(ref DataTable dt, GridView _GridView)
    {
        DataView dv = new DataView(dt);
        _GridView.DataSource = dv;
        _GridView.DataBind();
    }
    private void AddEmptyLineToDataTable(ref DataTable dt)
    {
        DataRow drPeilutyot;

        drPeilutyot = dt.NewRow();
               
        //מציין האם מותר לבטל פעילות
        drPeilutyot["Add_Nesia_reka"] = "1";
        drPeilutyot["cancel_peilut_flag"] = 0;
        drPeilutyot["Kisuy_Tor"] = 0;
        drPeilutyot["Kisuy_Tor_map"] = 0;
        drPeilutyot["shat_yetzia"] = "";
        drPeilutyot["Makat_Description"] ="";
        drPeilutyot["makat_shilut"] = "";
        drPeilutyot["Shirut_type_Name"] = "";
        drPeilutyot["oto_no"] = 0;
        drPeilutyot["makat_nesia"] =0;
        drPeilutyot["dakot_bafoal"] = 0;
        drPeilutyot["imut_netzer"] = "לא" ;
        drPeilutyot["makat_type"] = 0;
        drPeilutyot["mazan_tichnun"] = 0;
        drPeilutyot["last_update"] = "";
        drPeilutyot["mazan_tashlum"] = 0;
        drPeilutyot["bitul_o_hosafa"] = 0;                
        drPeilutyot["knisa"] = 0;
        drPeilutyot["DayToAdd"] = "0";                
        drPeilutyot["license_number"] = 0;
        drPeilutyot["PeilutStatus"] = 0;// lstSidurim.enPeilutStatus.enValid.GetHashCode();
        dt.Rows.Add(drPeilutyot);
    }
  
    private OBJ_SIDURIM_OVDIM SplitSidur(ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref clSidur oSidur)
    {
        /*
          במידה והסידור הוא סידור מיוחד עם מאפיין 54 (שעון נוכחות) ובשדה פיצול הפסקה נבחר ערך "הפסקה ע"ח העובד" (KOD_PIZUL_HAFSAKA=3) יש לפצל  
             את הסידור באופן הבא:
            א.  בסידור המקורי (בו בחרנו  בשדה פיצול את הערך הפסקה ע"ח העובד):
        1.	יש למחוק את שעת הגמר ולהשאיר את הסידור ללא שעת גמר. 
        2.	יש לעדכן את שדה מיקום שעון יציאה ל- null.
           ב.  סידור נוסף: יש לפתוח רשומה חדשה של סידור עם הנתונים הבאים:
        1.	מספר סידור - זהה לסידור המקורי. 
        2.	שעת התחלה - ריקה.
        3.	שעת גמר - יש לעדכן לשעת הגמר המקורית של הסידור המקורי.
        4.	מיקום שעון יציאה - יש לעדכן לערך של מיקום שעון יציאה המקורי שהיה בסידור המקורי.
         */
        try
        {
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
            DateTime dSidurShatHatchala;
            
            /*       ב.  סידור נוסף: יש לפתוח רשומה חדשה של סידור עם הנתונים הבאים:
            1.	מספר סידור - זהה לסידור המקורי. 
            2.	שעת התחלה - ריקה.
            3.	שעת גמר - יש לעדכן לשעת הגמר המקורית של הסידור המקורי.
            4.	מיקום שעון יציאה - יש לעדכן לערך של מיקום שעון יציאה המקורי שהיה בסידור המקורי.*/

            FillSidurimForInsert(ref oObjSidurimOvdimIns, ref oObjSidurimOvdimUpd, ref oSidur);
            dSidurShatHatchala = DateTime.Parse("01/01/0001");
            oObjSidurimOvdimIns.SHAT_HATCHALA = dSidurShatHatchala;
            oObjSidurimOvdimIns.MIKUM_SHAON_KNISA = 0; 
            // א.  בסידור המקורי (בו בחרנו  בשדה פיצול את הערך הפסקה ע"ח העובד):
            //1.	יש למחוק את שעת הגמר
            //ולהשאיר את הסידור ללא שעת גמר. 
            //2.	יש לעדכן את שדה מיקום שעון יציאה ל- null.
            oObjSidurimOvdimUpd.SHAT_GMARIsNull = true;
            oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA = 0;
         
       
            return oObjSidurimOvdimIns;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //private void FillIdkuneyRashemetSidurimForInsert(ref OBJ_IDKUN_RASHEMET oObjSidurimOvdimIns, ref OBJ_IDKUN_RASHEMET oObjSidurimOvdimUpd)
    //{
    //    try
    //    {
    //        oObjSidurimOvdimIns.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
    //        oObjSidurimOvdimIns.TAARICH = oObjSidurimOvdimUpd.TAARICH;
    //        oObjSidurimOvdimIns.SHAT_HATCHALA = oObjSidurimOvdimUpd.SHAT_HATCHALA;
    //        oObjSidurimOvdimIns.MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
    //        oObjSidurimOvdimIns.GOREM_MEADKEN = oObjSidurimOvdimUpd.GOREM_MEADKEN;
    //        oObjSidurimOvdimIns.MISPAR_KNISA = 0;                       
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void FillIdkuneyRashemetSidurimForDelete(ref OBJ_IDKUN_RASHEMET oObjSidurimOvdimDel, ref OBJ_IDKUN_RASHEMET oObjSidurimOvdimUpd)
    //{
    //    try
    //    {
    //        oObjSidurimOvdimDel.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
    //        oObjSidurimOvdimDel.TAARICH = oObjSidurimOvdimUpd.TAARICH;
    //        oObjSidurimOvdimDel.SHAT_HATCHALA = oObjSidurimOvdimUpd.SHAT_HATCHALA;
    //        oObjSidurimOvdimDel.MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
    //        oObjSidurimOvdimDel.UPDATE_OBJECT = 1;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    private void FillPeiluyotForDelete(ref OBJ_PEILUT_OVDIM oObjPeilyutOvdimDel, ref OBJ_PEILUT_OVDIM oObjPeiluyotOvdimUpd)
    {
        try
        {
            oObjPeilyutOvdimDel.MISPAR_ISHI = oObjPeiluyotOvdimUpd.MISPAR_ISHI;
            oObjPeilyutOvdimDel.TAARICH = oObjPeiluyotOvdimUpd.TAARICH;
            oObjPeilyutOvdimDel.SHAT_HATCHALA_SIDUR = oObjPeiluyotOvdimUpd.SHAT_HATCHALA_SIDUR;
            oObjPeilyutOvdimDel.MISPAR_SIDUR = oObjPeiluyotOvdimUpd.MISPAR_SIDUR;
            oObjPeilyutOvdimDel.SHAT_YETZIA = oObjPeiluyotOvdimUpd.SHAT_YETZIA;
            oObjPeilyutOvdimDel.MISPAR_KNISA = oObjPeiluyotOvdimUpd.MISPAR_KNISA;
            oObjPeilyutOvdimDel.MAKAT_NESIA = oObjPeiluyotOvdimUpd.MAKAT_NESIA;
            oObjPeilyutOvdimDel.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            oObjPeilyutOvdimDel.BITUL_O_HOSAFA = 1;
            oObjPeilyutOvdimDel.UPDATE_OBJECT = 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillSidurimForDelete(ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
    {
        try
        {
            oObjSidurimOvdimDel.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
            oObjSidurimOvdimDel.TAARICH = oObjSidurimOvdimUpd.TAARICH;
            oObjSidurimOvdimDel.SHAT_HATCHALA = oObjSidurimOvdimUpd.SHAT_HATCHALA;
            oObjSidurimOvdimDel.MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
            oObjSidurimOvdimDel.MEADKEN_ACHARON =long.Parse(LoginUser.UserInfo.EmployeeNumber);
            //oObjSidurimOvdimDel.BITUL_O_HOSAFA = 1;
            oObjSidurimOvdimDel.UPDATE_OBJECT = 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FillSidurimForInsert(ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref clSidur oSidur)
    {
        try
        {
            oObjSidurimOvdimIns.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
            oObjSidurimOvdimIns.TAARICH = oObjSidurimOvdimUpd.TAARICH;
            oObjSidurimOvdimIns.SHAT_HATCHALA = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
            oObjSidurimOvdimIns.NEW_SHAT_HATCHALA = oObjSidurimOvdimUpd.SHAT_HATCHALA; //נשמר הפוך בגלל היסטוריה - בעקבות הוספת עדכון שגיאות מאושרות
            oObjSidurimOvdimIns.MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
            oObjSidurimOvdimIns.SHAT_GMAR = oObjSidurimOvdimUpd.SHAT_GMAR;
            oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_IN = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN;
            oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_OUT = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT;
            oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
            oObjSidurimOvdimIns.CHARIGA = oObjSidurimOvdimUpd.CHARIGA;
            oObjSidurimOvdimIns.PITZUL_HAFSAKA = oObjSidurimOvdimUpd.PITZUL_HAFSAKA;
            oObjSidurimOvdimIns.HAMARAT_SHABAT = oObjSidurimOvdimUpd.HAMARAT_SHABAT;
            oObjSidurimOvdimIns.HASHLAMA = oObjSidurimOvdimUpd.HASHLAMA;
            oObjSidurimOvdimIns.OUT_MICHSA = oObjSidurimOvdimUpd.OUT_MICHSA;
            oObjSidurimOvdimIns.LO_LETASHLUM = oObjSidurimOvdimUpd.LO_LETASHLUM;
            oObjSidurimOvdimIns.KOD_SIBA_LO_LETASHLUM = oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM;
            oObjSidurimOvdimIns.BITUL_O_HOSAFA = oObjSidurimOvdimUpd.BITUL_O_HOSAFA;
            oObjSidurimOvdimIns.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
            oObjSidurimOvdimIns.MIKUM_SHAON_KNISA = String.IsNullOrEmpty(oSidur.sMikumShaonKnisa) ? 0 : int.Parse(oSidur.sMikumShaonKnisa);
            oObjSidurimOvdimIns.MIKUM_SHAON_YETZIA = String.IsNullOrEmpty(oSidur.sMikumShaonYetzia) ? 0 : int.Parse(oSidur.sMikumShaonYetzia);
            //שדות נוספים
            oObjSidurimOvdimIns.ACHUZ_KNAS_LEPREMYAT_VISA=oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA;
            oObjSidurimOvdimIns.ACHUZ_VIZA_BESIKUN = oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN;
            oObjSidurimOvdimIns.YOM_VISA = oObjSidurimOvdimUpd.YOM_VISA;
            oObjSidurimOvdimIns.MISPAR_MUSACH_O_MACHSAN = oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN;
            oObjSidurimOvdimIns.MISPAR_SHIUREY_NEHIGA = oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA;
            oObjSidurimOvdimIns.SUG_HAZMANAT_VISA = oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA;
            oObjSidurimOvdimIns.MIVTZA_VISA = oObjSidurimOvdimUpd.MIVTZA_VISA;
            oObjSidurimOvdimIns.TAFKID_VISA = oObjSidurimOvdimUpd.TAFKID_VISA;
            oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM;

            oObjSidurimOvdimIns.UPDATE_OBJECT = 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillYemayAvodaChanges(ref COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd)
    {
        DropDownList _DDL;
        int iTravelTimeOrgVal;

        OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
        try
        {
            if ((((System.Web.UI.HtmlControls.HtmlInputHidden)(this.FindControl("hidLvl1Chg"))).Value.ToString().Equals("1")))
            {
                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = oBatchManager.oOvedYomAvodaDetails.iZmanNesiaHaloch;
                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = oBatchManager.oOvedYomAvodaDetails.iZmanNesiaHazor;
                oObjYameyAvodaUpd.TAARICH = dDateCard;
                oObjYameyAvodaUpd.MISPAR_ISHI = iMisparIshi;
                oObjYameyAvodaUpd.LINA = decimal.Parse(((DropDownList)this.FindControl("ddlLina")).SelectedValue);
                oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = decimal.Parse(((DropDownList)this.FindControl("ddlTravleTime")).SelectedValue);//decimal.Parse(((DropDownList)this.FindControl("ddlTravleTime")).SelectedValue == "-1" ? "0" : ((DropDownList)this.FindControl("ddlTravleTime")).SelectedValue);
                oObjYameyAvodaUpd.TACHOGRAF = ((DropDownList)this.FindControl("ddlTachograph")).SelectedValue == "-1" ? "" : ((DropDownList)this.FindControl("ddlTachograph")).SelectedValue;
                oObjYameyAvodaUpd.HALBASHA = decimal.Parse(((DropDownList)this.FindControl("ddlHalbasha")).SelectedValue); //decimal.Parse(((DropDownList)this.FindControl("ddlHalbasha")).SelectedValue == "-1" ? "0" : ((DropDownList)this.FindControl("ddlHalbasha")).SelectedValue); 
                oObjYameyAvodaUpd.HASHLAMA_LEYOM = decimal.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)(this.FindControl("HashlamaForDayValue"))).Value.ToString());
                oObjYameyAvodaUpd.HAMARAT_SHABAT = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)(this.FindControl("Hamara"))).Value.ToString());
                _DDL = (DropDownList)this.FindControl("ddlHashlamaReason");
                if ((_DDL.SelectedValue == "-1") || (_DDL.SelectedValue=="")) 
                    oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 0;
                else
                {
                    oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = int.Parse(_DDL.SelectedValue);
                }
                oObjYameyAvodaUpd.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                //נקבע את משתנה הלוך ושוב
                if (ddlTravleTime.Enabled)
                {
                    _DDL = (DropDownList)this.FindControl("ddlTravleTime");
                    iTravelTimeOrgVal = int.Parse(_DDL.Attributes["OrgVal"].ToString());
                    /*  במקרים הבאים יש לעדכן ב- DB את שדות Zman_Nesia_Haloch, Zman_Nesia_Hazor:
                    0 - אין מאפיין נסיעות
                    1 - זמן נסיעה לעבודה
                    2- זמן נסיעה מהעבודה
                    3 - זמן נסיעה לעבודה/מהעבודה
                    4 - לא זכאי לזמן נסיעות

                    א. שונה מ- 3 ל- 2 :  Zman_Nesia_Haloch ל- 0.
                    ב. שונה מ- 3 ל- 1
                    Zman_Nesia_Hazor ל- 0.
                    ג. שונה מ- 3 ל- 0
                    Zman_Nesia_Haloch ל- 0.
                    Zman_Nesia_Hazor ל- 0.
                    ד. שונה מ-1 ל- 0 או 2.
                    Zman_Nesia_Haloch ל- 0.
                    ה. שונה מ- 2 ל- 0 או 1.
                    Zman_Nesia_Hazor ל- 0.
                    ו. שונה מ- 1/2/3 ל- 4
                    Zman_Nesia_Haloch ל- 0.
                    Zman_Nesia_Hazor ל- 0.*/
                    //switch (iTravelTimeOrgVal)
                    //{
                    //    case 3:
                    //        //if (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 2)
                    //        //{
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                    //        //}
                    //        //if (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 1)
                    //        //{
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                    //        //}
                    //        //if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 0) || (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 4))
                    //        //if (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 4)
                    //        //{
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                    //        //}
                    //        break;
                    //    case 1:
                    //        //if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 2) || (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 0))
                    //        //{
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                    //        //}
                    //        //if (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 4)
                    //        //{
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                    //        //}
                    //        break;
                    //    case 2:
                    //        //if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 1) || (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 0))
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;

                    //        //if (oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 4)
                    //        //{
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                    //        //    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                    //        //}
                    //        break;
                    //}
                }
                oCollYameyAvodaUpd.Add(oObjYameyAvodaUpd);
            }
            //oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH
            //oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR
        }
        catch (Exception ex)
        {
            throw ex;
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

        string sScript = "window.showModalDialog('" + this.PureUrlRoot + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "','','dialogwidth:1200px;dialogheight:800px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
               
        ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "ReportViewer", sScript, true);

    }
    protected void btnDrvErrors_click(object sender, EventArgs e)
    {
        
    }
    protected void btnApprovalReport_click(object sender, EventArgs e)
    {
        Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
        ReportParameters.Add("P_MISPAR_ISHI", iMisparIshi.ToString());
        ReportParameters.Add("P_TAARICH", dDateCard.ToShortDateString());
        OpenReport(ReportParameters, (Button)sender, ReportName.IshurimLekartisAvoda.ToString());
    }
    protected void btnClock_click(object sender, EventArgs e)
    {
        Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
        ReportParameters.Add("P_MISPAR_ISHI", iMisparIshi.ToString());

        ReportParameters.Add("P_STARTDATE",dDateCard.AddDays(-DateTime.DaysInMonth(dDateCard.Year, dDateCard.Month)).ToShortDateString());
        ReportParameters.Add("P_ENDDATE", dDateCard.ToShortDateString());
       
        OpenReport(ReportParameters, (Button)sender, ReportName.Presence.ToString());
    }
   
    protected DataTable GetMasachPakadim()
    {
        clOvdim _ovdim = new clOvdim();
        dtPakadim = _ovdim.GetPakadIdForMasach(MASACH_ID);
        return dtPakadim;
    }
    protected bool IsSidurVisaExists()
    {
        clSidur _Sidur;
        bool bExists = false;
        if (oBatchManager.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < oBatchManager.htFullEmployeeDetails.Count; i++)
            {
                _Sidur = (clSidur)(oBatchManager.htFullEmployeeDetails[i]);
                if ((_Sidur.bSidurMyuhad) && (_Sidur.bSidurVisaKodExists))
                {
                    bExists = true;
                    break;
                }
            }
        }
        return bExists;
    }
    //public HtmlHead HeadPage
    //{
    //    get { return WCard; }
    //}
    //protected void grdErr_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    int iKodIshur;

    //    switch (e.Row.RowType)
    //    {
    //        case DataControlRowType.DataRow:
    //            iKodIshur = String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "KOD_ISHUR").ToString()) ? 0 : int.Parse(DataBinder.Eval(e.Row.DataItem, "KOD_ISHUR").ToString());
    //            ((Label)e.Row.Cells[3].Controls[1]).Text = "Key" + "|" + iKodIshur + "|" + int.Parse((DataBinder.Eval(e.Row.DataItem, "KOD_SHGIA").ToString()));
    //            break;
    //        default:
    //            break;

    //    }
    //    e.Row.Cells[3].Style["display"] = "none";
    //}
}

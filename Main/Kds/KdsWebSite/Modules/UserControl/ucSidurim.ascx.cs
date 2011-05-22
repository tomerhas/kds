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
using System.Collections.Specialized;
using KdsBatch;
using KdsLibrary.BL;
using KdsLibrary.UDT;
using KdsWorkFlow.Approvals;
using KdsLibrary.Security;
public partial class Modules_UserControl_ucSidurim : System.Web.UI.UserControl//, System.Web.UI.ICallbackEventHandler
{
    
    private OrderedDictionary _DataSource;
    private OrderedDictionary _CancledSidurim;
    private clMeafyenyOved _MeafyenyOved;
    private clParameters _KdsParameters;
    private clOvedYomAvoda _oOvedYomAvoda;
    //private DataView _dvPeilut;
    private DataView _dvChariga;
    private DataView _dvSibotLedivuch;
    private DataView _dvHashlama;
    private DataView _dvPitzulHafsaka;
    private DataTable _Mashar;   
    private DateTime _Param1;//מגבלת התחלת שעת התחלה
   // private DateTime _Param4;//מגבלת סיום שעת גמר - מפעילים
    private DateTime _Param93;//מגבלת סיום שעת התחלה
    private DateTime _Param3; //מגבלת סיום שעת גמר - סידור מנהל
    private DateTime _Param80; //מגבלת סיום שעת גמר - סידור נהגות
    private DateTime _Param29; //שעת התחלה ראשונה מותרת לפעילות בסידור
    private DateTime _Param30; //שעת התחלה אחרונה מותרת לפעילות בסידור
    private float _Param149; //149 - אורך נסיעה קצרה לאילת
    private int _Param101;//השלמה ליום רגיל
    private int _Param102;//השלמה ליום שישי
    private int _Param103;//השלמה ליום שבת
    //private int _Param108;
    //private int _Param109;
    //private int _Param110;
    private int _Param41;
    private int _Param98;
    private DateTime _Param242;
    private DateTime _Param244;
    private float _Param43;
    private float _Param42;
    private int _NumOfHashlama;
    private DateTime _CardDate;
    private DateTime _FullShatHatchala;
    private DateTime _FullOldShatHatchala;
    private bool _HasSaveCard = false;
    private DataTable _dtSugSidur;
    private DataTable _dtSidurim;
    private DataTable _ErrorsList;
    private DataTable _dtApprovals;
    private DataTable _dtIdkuneyRashemet;
    private DataTable _dtPakadim;
    private DataTable _SadotNosafim;
    private DataTable _MeafyeneySidur;
    private DataTable _dtMeafyeneyElementim;
    private DataTable dtUpdatedSidurim;
    private DateTime _KnisatShabat;
    private ApprovalRequest[] _EmployeeApproval;
    private clSidur _FirstParticipate;
    private clSidur _SecondParticipate;
    private bool _SidurNahagut;
    private bool _SidurRetzifut;    
    private int _MisparIshi;
    private int _LoginUserId;
    private int _MisparSidur;
    private int _SidurIndex;
    private int _RefreshBtn;
    private string _ShatHatchala;
    private clGeneral.enCardStatus _StatusCard;
    private UpdatePanel _UpEmpDetails;
    private clGeneral.enMeasherOMistayeg _MeasherOMistayeg;

    public const string NBSP ="&nbsp;";
    public const int _COL_DUMMY =0;
    public const int _COL_ADD_NESIA_REKA = 1;
    public const int _COL_KISUY_TOR = 2;
    public const int _COL_SHAT_YETIZA = 3;
    public const int _COL_LINE_DESCRIPTION = 4;
    public const int _COL_LINE = 5;
    public const int _COL_LINE_TYPE = 6;
    public const int _COL_MAKAT = 7;
    public const int _COL_DEF_MINUTES = 8; //MaazanTichnun
    public const int _COL_ACTUAL_MINUTES = 9;

    public const int _COL_CAR_NUMBER = 10;
    //public const int _COL_CAR_LICESNCE_NUMBER = 10;
   
    public const int _COL_NETZER = 11;
    public const int _COL_CANCEL = 12;
    public const int _COL_LAST_UPDATE = 13;
    public const int _COL_MAZAN_TASHLUM = 14;
    public const int _COL_CANCEL_PEILUT = 15;
    public const int _COL_KNISA = 16;
    public const int _COL_DAY_TO_ADD = 17;
    public const int _COL_KISUY_TOR_MAP = 18;
    public const int _COL_PEILUT_STATUS = 19; //מציין אם ברמת פעילות יש שגיאה או אישור או תקין  ללא אישורים. 
    
    private const int MAX_LEN_LINE_NUMBER = 8;
    private const int MAX_LEN_CAR_NUMBER = 5;
    private const int MAX_LEN_HOUR = 5;
    private const int MAX_LEN_ACTUAL_MINTUES = 4;
    public const int SIDUR_CONTINUE_NAHAGUT = 99500;
    public const int SIDUR_CONTINUE_NOT_NAHAGUT = 99501;    
    private const int COL_DATE = 16;
    private bool bSidurContinue;
    private bool bSidurNahagut;  
    //public bool bAtLeastOneSidurZakaiLeHamara = false;
    public bool bAtLeatOneSidurIsNOTNahagutOrTnua = false;
    public bool bAtLeastOneSidurInShabat = false;
    private bool _ProfileRashemet;
    private int _MisparIshiIdkunRashemet;
    private const string COL_TRIP_EMPTY = "ריקה";
    // Delegate declaration
    public delegate void OnButtonClick(string strValue, bool bOpenUpdateBtn);
    
    // Event declaration
    public event OnButtonClick btnHandler;
    //public event OnButtonClick btnReka;

    private enum enDayType
    {
        enRegular = 1,
        enShisi = 2,
        enShabat = 3
    }
    public enum enPeilutStatus
    {
        enError = 1, enValid = 0, enApproved = 2
    }

    //clBatchManager oBatchManager;
    //public int iMisparIshi;
    //public DateTime dDateCard;
    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    iMisparIshi = 78856;
    //    dDateCard = new DateTime(2009,12,22);
    //    oBatchManager = new clBatchManager(iMisparIshi, dDateCard);
    //    SetGeneralData(oBatchManager);
    //    if (oBatchManager.htEmployeeDetails != null)
    //    {
    //        _DataSource = oBatchManager.htEmployeeDetails;
    //    }
    //if (_DataSource != null)
    //    {
    //        BuildSidurim(_DataSource);
    //        SetHideParameters();

    //        //vldTst.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(IsShatHatchalaValid);
    //    }
    //}
    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    this.Page.MaintainScrollPositionOnPostBack = true;
    //}
    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    if (hidScrollPos.Value != string.Empty)
    //    {
    //        dvS.Style.Add("scrollTop", hidScrollPos.Value);
    //        //Set first sidur focus 
    //        string sScript = "SetScrollPosition();";//"document.getElementById('lstSidurim_dvS').scrollTop= Number(document.getElementById('lstSidurim_hidScrollPos').value) + 600";
    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "SidurFocus", sScript, true);
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (_DataSource != null)                                     
                BuildPage();            
        }
        catch (Exception ex)
        {
            if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode != -2147467259)
            {
                throw ex;
            }
        }
    }
    public void BuildPage()
    {
        try
        {
            if (_DataSource != null) 
            {
                BuildSidurim(_DataSource);
                SetHideParameters();

                
            }
        }
        catch (Exception ex)
        {
            if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode != -2147467259)
            {
                throw ex;
            }
        }
    }
    public clGeneral.enMeasherOMistayeg MeasherOMistayeg
    {
        get {return _MeasherOMistayeg; }
        set { _MeasherOMistayeg = value; }
    }
    public clGeneral.enCardStatus StatusCard
    {
        get { return _StatusCard; }        
        set { _StatusCard = value;}        
    }
    public bool ProfileRashemet
    {
        set
        {
            _ProfileRashemet = value;
        }
        get
        {
            return _ProfileRashemet;
        }
    }
    public int MisparIshiIdkunRashemet
    {
        set
        {
            _MisparIshiIdkunRashemet = value;
        }
        get
        {
            return _MisparIshiIdkunRashemet;
        }
    }
    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    //string sConn = (string)ConfigurationSettings.AppSettings["KDS_CONNECTION"];
    //    //string sCmd = "EXEC kdsadmin.pkg_errors.pro_get_oved_details";
    //    //DataTable dt;

    //    try
    //    {
    //        if (_HasSaveCard)
    //        {
    //            SaveCard();
    //        }
    //        else
    //        {
                
    //        }
           
           
    //      // CustomValidator vldShatHatchala = new CustomValidator();
    //      // vldShatHatchala.ErrorMessage = "יש להקליד שעת התחלה תקינה: ";
    //      // vldShatHatchala.ControlToValidate = "textTst";
    //      //// vldShatHatchala.ClientValidationFunction = "Test";
    //      // vldShatHatchala.ID = "aa";
    //      // vldShatHatchala.Display = ValidatorDisplay.None;
    //      // vldShatHatchala.EnableClientScript = true;
    //      // AjaxControlToolkit.ValidatorCalloutExtender vldExShatHatchala = new AjaxControlToolkit.ValidatorCalloutExtender();
    //      // vldExShatHatchala.TargetControlID = vldShatHatchala.ID;
    //      // vldExShatHatchala.PopupPosition = AjaxControlToolkit.ValidatorCalloutPosition.Left;
    //      // vldExShatHatchala.ID = "xx";
    //      // //vldShatHatchala.ClientValidationFunction = "ISShatHatchalaValid";
    //      // vldShatHatchala.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(IsShatHatchalaValid);
    //      // divTst.Controls.Add(vldShatHatchala);
    //      // divTst.Controls.Add(vldExShatHatchala);

    //       // dt = GetSidurimAndPeilut(74480, new DateTime(2009, 5, 26));
    //        //ListViewSidurim.DataSource = _DataSource;
    //        //ListViewSidurim.DataBind();
    //        //SqlDataSourceSidurim.SelectCommand = sCmd;
    //        //SqlDataSourceSidurim.ConnectionString = sConn;
    //        //SqlDataSourceSidurim.SelectParameters.Add("p_mispar_ishi", TypeCode.Int32, "74480");
    //        //SqlDataSourceSidurim.SelectParameters.Add("p_date",TypeCode.DateTime,new DateTime(2009,5,26).ToShortDateString());            
    //        //SqlDataSourceSidurim.DataSourceMode = SqlDataSourceMode.DataSet;
    //        //////SqlDataSource sdsSidurim = new SqlDataSource(sConn, sCmd);
    //        ////sdsSidurim.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
    //        //////sdsSidurim.ID = "SqlDataSourceSidurim";
    //        //////sdsSidurim.DataSourceMode = SqlDataSourceMode.DataSet;
    //        ////sdsSidurim.SelectParameters.Add("p_mispar_ishi",TypeCode.Int32,"74480");
    //        ////sdsSidurim.SelectParameters.Add("p_date",TypeCode.DateTime,new DateTime(2009,5,26).ToShortDateString());

    //        ////ListViewSidurim.DataSourceID = "SqlDataSourceSidurim";
           
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //public string GetCallbackResult()
    //{

    //    return _callBackStatus;
    //}

    //public void RaiseCallbackEvent(string eventArgument)
    //{

    //    // do your validation coding here

    //     _callBackStatus = "Valid";
    //}


    protected void SetHideParameters()
    {
        //Set General Parameters     
        hidParam29.Value = Param29.ToShortTimeString();     
        hidParam41.Value = Param41.ToString();
        hidParam98.Value = Param98.ToString();
        hidParam242.Value = Param242.ToShortTimeString();
        hidParam244.Value = Param244.ToShortTimeString(); 
        hidNumOfHashlama.Value = NumOfHashlama.ToString();
    }
    protected Panel CreatePanel(int iIndex, string sID, string sClass)
    {
        Panel pnl = new Panel();
        pnl.ID = sID + iIndex;
        pnl.Attributes.Add("class", sClass);

        return pnl;
    }
    protected TableCell CreateCellForPanel(int iIndex)
    {
        TableCell tCell = new TableCell();        
        TableRow tRow = new TableRow();
        
        tRow.ID = "Row" + iIndex;
        tCell.ID = "Cell" + iIndex;       
        tRow.Cells.Add(tCell);
        tbSidurim.Rows.Add(tRow);

        return tCell;
    }
    protected bool IsSadotNosafimLSidur(ref clSidur oSidur,DataTable dtMeafyenSidur, DataTable dtSadotNosafimLsidur)
    {
        bool bExists=false;
        int iSidurKey;
        string sErech;

        DataRow[] drSadot, drMeafyeney;
        try
        {
            //נחפש את מספר הסידור ב- TB_SADOT_NOSAFIM
            //אם קיים, נחזיר TRUE, אם לא קיים נבדוק את מאפייני הסידור
            drSadot = dtSadotNosafimLsidur.Select("mispar_sidur=" + oSidur.iMisparSidur);
            if (drSadot.Length > 0)            
                bExists = true;            
            else
            { //נבדוק את מאפייני הסידור
                if (oSidur.bSidurMyuhad)
                {
                    iSidurKey = oSidur.iMisparSidur;
                }
                else
                {
                    iSidurKey = oSidur.iSugSidurRagil;
                }
                //נשלוף את מאפייני הסידור
                drMeafyeney = dtMeafyenSidur.Select("sidur_key=" + iSidurKey);
                foreach (DataRow dr in drMeafyeney)
                {//אם קוד מאפיין קיים בטבלה, נחזיר TRUE
                    sErech = String.IsNullOrEmpty(dr["erech"].ToString()) ? "" : dr["erech"].ToString();
                    //if ((sErech == string.Empty) || (sErech.Equals("0")))
                    //{
                    //    drSadot = dtSadotNosafimLsidur.Select("kod_meafyen=" + dr["kod_meafyen"].ToString());
                    //}
                    //else
                    //{
                      //  drSadot = dtSadotNosafimLsidur.Select("kod_meafyen=" + dr["kod_meafyen"].ToString() + " and erech='" + sErech + "'");
                    drSadot = dtSadotNosafimLsidur.Select("kod_meafyen=" + dr["kod_meafyen"].ToString());
                    if (drSadot.Length > 0)
                    {
                        if ((drSadot[0]["erech"] == null) || (drSadot[0]["erech"].ToString().Equals("0")))
                        {
                            bExists = true;
                            break;
                        }
                        else
                        {
                            if (drSadot[0]["erech"].ToString().Equals(sErech))
                            {
                                bExists = true;
                                break;
                            }
                        }
                    }    
                    //}
                   
                }
            }//אם לא מצאנו מספר סידור או קודי מאפיין, נבדוק ברמת פעילות
            //- מבוטל - אם קיימת נסיעת אילת שהק"מ של הנסיעה גדול מפרמטר 149
            //שדה - מבוטל GetKavDetails.EilatTrip=1   וגם אורך הקו הוא גדול מהערך בפרמטר 149  (GetKavDetails.Km,GetNamakDetails.Km , GetRekaDetailsKm). 
          
            //סידור שיש בו לפחות פעילות אחת מסוג שירות שיש לה 71=Onatiut.
            if (!bExists)
            {
                bExists = IsOnatiyutInPeilutExists(ref oSidur);
            }
            return bExists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool IsOnatiyutInPeilutExists(ref clSidur oSidur)
    {
        //אם לא מצאנו מספר סידור או קודי מאפיין, נבדוק ברמת פעילות
        //סידור שיש בו לפחות פעילות אחת מסוג שירות שיש לה 71=Onatiut.
        bool bExists = false;
        clKavim.enMakatType _MakatType;
        try
        {
            clPeilut _Peilut;
            for (int i = 0; i < oSidur.htPeilut.Count; i++)
            {
                _Peilut = ((clPeilut)oSidur.htPeilut[i]);
                _MakatType = (clKavim.enMakatType)(_Peilut.iMakatType);
                if (_Peilut.iOnatiyut == clGeneral.enOnatiut.enOnatiut.GetHashCode() && (_MakatType== clKavim.enMakatType.mKavShirut)) 
                {
                    bExists = true;
                    break;
                }
            }
            return bExists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool IsSidurVisa(ref clSidur oSidur)
    {
        return ((oSidur.bSidurMyuhad) && (oSidur.bSidurVisaKodExists));   
    }
    //protected bool IsPeilutEilatExistsWithKm(ref clSidur oSidur)
    //{
    //    //אם לא מצאנו מספר סידור או קודי מאפיין, נבדוק ברמת פעילות
    //    //אם קיימת נסיעת אילת שהק"מ של הנסיעה גדול מפרמטר 149
    //    //שדה GetKavDetails.EilatTrip=1   וגם אורך הקו הוא גדול מהערך בפרמטר 149  (GetKavDetails.Km,GetNamakDetails.Km , GetRekaDetailsKm). 
    //    bool bExists = false;
    //    try
    //    {
    //        clPeilut _Peilut;
    //        for (int i = 0; i < oSidur.htPeilut.Count; i++)
    //        {
    //            _Peilut = ((clPeilut)oSidur.htPeilut[i]);
    //            if ((_Peilut.iEilatTrip == 1)) //&& (_Peilut.fKm > Param149))
    //            {
    //                bExists = true;
    //                break;
    //            }
    //        }
    //        return bExists;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void BuildSidurim(OrderedDictionary htFullEmployeeDetails)
    {            
        try
        {
            //יצירת כותרת הסידור
            CreateSidurHeader();
            //נבנה את הטבלה שתכיל את הנתוני הסידור העדכניים
            if ((!Page.IsPostBack) || (RefreshBtn.Equals(1)))
            {
                BuildUpdatedSidurimColumns(ref dtUpdatedSidurim);
                hidScrollPos.Value = "0"; //set scroll to top for new card
            }
            if ((OrderedDictionary)Session["Sidurim"]!= null)
            {
                htFullEmployeeDetails = (OrderedDictionary)Session["Sidurim"];
                for (int iIndex = 0; iIndex < htFullEmployeeDetails.Count; iIndex++)
                {
                    BuildSidurAndPeiluyot(ref htFullEmployeeDetails, iIndex);
                }
                if ((!Page.IsPostBack) || (RefreshBtn.Equals(1)))
                    Session["SidurimUpdated"]=dtUpdatedSidurim;
            }                           
        }
        catch (Exception ex)
        {
            if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode != -2147467259)
            {
                throw ex;
            }
        }
    }
    protected void BuildSidurAndPeiluyot(ref OrderedDictionary htFullEmployeeDetails, int iIndex)
    {
        AjaxControlToolkit.CollapsiblePanelExtender Ax;
        HtmlTable hTable;
        GridView grdPeiluyot;
        Panel pnlHeader, pnlContent;
        TableCell tCell;
        clSidur oSidur;
        DataView dvPeiluyot = new DataView();       
        UpdatePanel up = new UpdatePanel();
        bool bEnableSidur=false; // אם הכרטיס הוא ללא התייחסות וגורם המטפל שונה מבעל הכרטיס הנוכחי ולסידור אין מאפיין 99 לא נאפשר עדכון סידור ופעילויות
        try
        {
            oSidur = (clSidur)htFullEmployeeDetails[iIndex];
            MisparSidur = oSidur.iMisparSidur;
            ShatHatchala = oSidur.sShatHatchala;
            FullShatHatchala = oSidur.dFullShatHatchala;
            FullOldShatHatchala = oSidur.dOldFullShatHatchala;
            //Create Sidur
            hTable = BuildOneSidur(ref htFullEmployeeDetails, oSidur, iIndex, ref bEnableSidur);

            //Add sidur table to header panel
            pnlHeader = CreatePanel(iIndex, "pnlHeader", "CollapseHeader");
            pnlHeader.Controls.Add(hTable);

            tCell = CreateCellForPanel(iIndex);
            tCell.Controls.Add(pnlHeader);

            pnlContent = CreatePanel(iIndex, "pnlContent", "CollapseContent");
            //Add Panel
            tCell.Controls.Add(pnlContent);

            //Add grid   

            grdPeiluyot = BuildSidurPeiluyot(((KdsBatch.clSidur)(htFullEmployeeDetails[iIndex])).htPeilut, iIndex, ref dvPeiluyot);

            //Add Update Panel
            up = AddUpdatePanel();
            pnlContent.Controls.Add(up);
            up.ContentTemplateContainer.Controls.Add(grdPeiluyot);
                       
            grdPeiluyot.DataSource = dvPeiluyot;
            grdPeiluyot.DataBind();

            if (!bEnableSidur)
                grdPeiluyot.Enabled = false;
            //נוריד הוספת נסיעה ריקה, בפעילות אחרונה
            //if ((grdPeiluyot.Rows.Count > 0) && (htEmployeeDetails.Count-1==iIndex))
            //    if (grdPeiluyot.Rows[grdPeiluyot.Rows.Count - 1].Cells[_COL_ADD_NESIA_REKA].Controls.Count > 0)
            //    {
            //        grdPeiluyot.Rows[grdPeiluyot.Rows.Count - 1].Cells[_COL_ADD_NESIA_REKA].Controls.RemoveAt(0);                    
            //        grdPeiluyot.Rows[grdPeiluyot.Rows.Count - 1].Cells[_COL_ADD_NESIA_REKA].Attributes.Add("NesiaReka", "1");
            //    }
            
            // If Peilyot exists for sidur add collapsible panel                   
            if (oSidur.htPeilut.Count > 0)
            {
                Ax = CreateCollapsiblePanelExtender(iIndex, pnlHeader.ID, pnlContent.ID, oSidur.htPeilut.Count);
                tCell.Controls.Add(Ax);
            }                           
        }
        catch(Exception ex)
        {
            if (((System.Runtime.InteropServices.ExternalException)(ex)).ErrorCode != -2147467259)
            {
                throw ex;
            }
        }
    }
    //protected Button AddSidurHeadrutButton(int iIndex)
    //{
    //    Button btnSidur;
    //    btnSidur = AddInputButton("תיקון דיווח היעדרות", "ImgButtonUpdate");
    //    btnSidur.ID = "btnSidur" + iIndex;        
    //    btnSidur.Attributes.Add("onclick", string.Concat("FixSidurHeadrut", "(", iIndex, ")"));
    //    btnSidur.Click += new EventHandler(btnSidur_Click);

    //    return btnSidur;
    //    ////Add AsyncPostBackTrigger
    //    //AsyncPostBackTrigger AsncTrigger = AddAsyncTrigger("btnSidur");
    //    //AsncTrigger.ControlID = btnSidur.ID;
    //    ////UpEmpDetails.Triggers.Add(AsncTrigger);
    //}
    protected AjaxControlToolkit.CollapsiblePanelExtender CreateCollapsiblePanelExtender(int iIndex,
                                                                                         string sPnlHeader, string sPnlContent,
                                                                                         int iPeilutNumber)
    {
        AjaxControlToolkit.CollapsiblePanelExtender Ax = new AjaxControlToolkit.CollapsiblePanelExtender();
        Ax.ID = "Ax" + iIndex;
        Ax.Collapsed = false;
        Ax.CollapsedSize = 0;
        Ax.ExpandedSize = (iPeilutNumber * 35)+25;
        Ax.AutoCollapse = false;
        Ax.AutoExpand = false;
        Ax.ScrollContents = false;
        Ax.TargetControlID = sPnlContent;
        Ax.ExpandControlID = sPnlHeader;
        Ax.CollapseControlID = sPnlHeader;
        Ax.BehaviorID = "cPanel" + iIndex;
        return Ax;
    }        
    protected AsyncPostBackTrigger AddAsyncTrigger(string sControlId)
    {
        AsyncPostBackTrigger upTrigger  = new AsyncPostBackTrigger();        
        //upTrigger.ControlID = btnSidur;
        upTrigger.EventName = "OnCommand";
        return upTrigger;
    }
    protected UpdatePanel AddUpdatePanel()
    {
        UpdatePanel upPeilyot = new UpdatePanel();
        upPeilyot.RenderMode = UpdatePanelRenderMode.Inline;
        upPeilyot.UpdateMode = UpdatePanelUpdateMode.Conditional;   
        //upPeilyot.Triggers 
        return upPeilyot;
    }
    protected void FixGridHeader(string sNamePnlGrid)
    {
        ((System.Web.UI.Page)this.Parent.Parent.Parent.Parent).Header.Controls.Add(clUtils.SetFixedHeaderGrid(sNamePnlGrid));
        //((Page)((this.Parent).Parent)).Header.Controls.Add(clUtils.SetFixedHeaderGrid(sNamePnlGrid));
        //((Page)this.Parent).Header.Controls.Add(clUtils.SetFixedHeaderGrid(sNamePnlGrid));
    }
    protected HtmlGenericControl AddDiv(int iIndex)
    {
        HtmlGenericControl dynDiv = new HtmlGenericControl("DIV");
        dynDiv.ID = "pDiv" + iIndex;
       // dynDiv.Style.Add(HtmlTextWriterStyle.Overflow, "scroll");
        return dynDiv;
    }
    //protected void SetSidurParameters(DataRow dr)
    //{
    //    //אם סידור מיוחד, נכניס את פרמטר שעת התחלה מותרת ושעת גמר מותרת שמוגדרים לסידור
    //    //ונדרוס את הפרמטרים הכללים של שעת התחלה וגמר מותרת, כיוון שההגדרה לסידור חזקה יותר מההגדרה הכללית
    //    if (bool.Parse(dr["sidur_myuhad"].ToString()))
    //    {
    //        if (!String.IsNullOrEmpty(dr["shat_hatchala_muteret"].ToString()))
    //        {
    //            Param1 = DateTime.Parse(dr["shat_hatchala_muteret"].ToString());
    //        }
    //        if (!String.IsNullOrEmpty(dr["shat_gmar_muteret"].ToString()))
    //        {
    //            Param2 = DateTime.Parse(dr["shat_gmar_muteret"].ToString());
    //        }
    //    }       
    //}
    protected Button AddImageButton()
    {
        Button oImage = new Button();
        oImage.CssClass = "ImgCancel";//"ImgCancel"; //"~/Images/allscreens-cancle-btn.jpg";
        //oImage.CausesValidation = false;
        //oImage.Click += new ImageClickEventHandler(oImage_Click);
        return oImage;
    }
    protected AjaxControlToolkit.MaskedEditExtender AddTimeMaskedEditExtender(string sTargetControlId, int iIndex, 
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
            oMaskTextBox.OnFocusCssClass = "MaskedEditFocus";
            oMaskTextBox.OnInvalidCssClass = "MaskedEditError";
            oMaskTextBox.MaskType = oMaskType;
            oMaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
            oMaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.None;
            oMaskTextBox.DisplayMoney = oMaskEditSymbol;
            oMaskTextBox.ErrorTooltipEnabled = true;
            oMaskTextBox.ID = sTargetControlId + sMaskId + iIndex.ToString();
            
            return oMaskTextBox;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected AjaxControlToolkit.RoundedCornersExtender AddRoundedCorner(string sPanelHeaderId)
    {
        AjaxControlToolkit.RoundedCornersExtender RoundCorner = new AjaxControlToolkit.RoundedCornersExtender();
        RoundCorner.TargetControlID=sPanelHeaderId;
        RoundCorner.Radius=10;
        RoundCorner.Corners = AjaxControlToolkit.BoxCorners.All;
        RoundCorner.BorderColor = System.Drawing.Color.Gray;
        
        return RoundCorner;
    }
    public int COL_KISUY_TOR
    {
        get
        {
            return _COL_KISUY_TOR;
        }
    }
    public int COL_ADD_NESIA_REKA {
        get { return _COL_ADD_NESIA_REKA; }       
    }
    public int COL_SHAT_YETIZA
    {
        get
        {
            return _COL_SHAT_YETIZA;
        }
    }
    public int COL_CAR_NUMBER
    {
        get
        {
            return _COL_CAR_NUMBER;
        }
    }
    public int COL_MAKAT
    {
        get
        {
            return _COL_MAKAT;
        }
    }
    public int COL_DEF_MINUTES
    {
        get
        {
            return _COL_DEF_MINUTES;
        }
    }
    public int COL_ACTUAL_MINUTES
    {
        get
        {
            return _COL_ACTUAL_MINUTES;
        }
    }
    public int COL_CANCEL
    {
        get
        {
            return _COL_CANCEL;
        }
    }
    public int COL_CANCEL_PEILUT
    {
        get
        {
            return _COL_CANCEL_PEILUT;
        }
    }
    public int COL_DAY_TO_ADD
    {
        get
        {
            return _COL_DAY_TO_ADD;
        }
    }
    public int COL_KNISA
    {
        get
        {
            return _COL_KNISA;
        }
    }
    //private bool IsSidurMyuhad(string sMisparSidur)
    //{//נבדוק אם סידור הוא רגיל או מיוחד
    //    if (sMisparSidur.Length > 1)
    //        return (sMisparSidur.Substring(0, 2) == "99");
    //    else
    //    {
    //        return false;
    //    }
    //}
    //private bool IsSidurDriver(DataRow dr)
    //{//סידור נהגות - לפי ערך 5 במאפיין 3
    //   // bool bSidurMyuhad = (bool)dr["Sidur_Myuhad"];
    //    bool bSidurDriver = false;
    //    //if (bSidurMyuhad)
    //    //{
    //    //    bSidurDriver = String.IsNullOrEmpty(dr["sector_avoda"].ToString()) ? false : (int.Parse(dr["sector_avoda"].ToString()) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
    //    //}
    //    //else
    //    //{
    //    //}

    //    return bSidurDriver;
    //}
    //private  void IsShatHatchalaValid(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    //{   //תקינות שעת התחלה     
    //    DateTime dShatHatchala;
    //    DateTime dParam1= Param1;
    //    DateTime dParam2 = Param2;

    //    dShatHatchala = DateTime.Parse(string.Concat(CardDate.ToShortDateString()," " ,args.Value));
    //    args.IsValid = ((dShatHatchala >= dParam1) && (dShatHatchala <= dParam2));
    //}
    protected void FillDataTableRows(ref DataTable dtPeilutyot, OrderedDictionary htPeilut)
    {
         // populate it with your data
        DataRow drPeilutyot;
        clPeilut oPeilut;        
        DataRow[] drLicenseNumber;
        clGeneral.enBitulOHosafa _BitulOHosafa;
        clKavim _Kavim = new clKavim();
        try
        {
            for (int i = 0; i < htPeilut.Count; i++)
            {
                drPeilutyot = dtPeilutyot.NewRow();
                oPeilut = (clPeilut)htPeilut[i];
                //מציין האם מותר לבטל פעילות
                drPeilutyot["Add_Nesia_reka"] = "1";//oPeilut.iKisuyTor;
                drPeilutyot["cancel_peilut_flag"] = oPeilut.iMisparKnisa == 0 ? 1 : oPeilut.bKnisaNeeded ? 1 : 0;
                drPeilutyot["Kisuy_Tor"] = oPeilut.iKisuyTor;
                drPeilutyot["Kisuy_Tor_map"] = oPeilut.iKisuyTorMap;
                drPeilutyot["shat_yetzia"] = oPeilut.dFullShatYetzia;
                drPeilutyot["old_shat_yetzia"] = oPeilut.dOldFullShatYetzia;
                drPeilutyot["Makat_Description"] = oPeilut.sMakatDescription;
                drPeilutyot["makat_shilut"] = oPeilut.sShilut;
                drPeilutyot["Shirut_type_Name"] = oPeilut.sSugShirutName;
               
                drPeilutyot["oto_no"] = oPeilut.lOtoNo;
                drPeilutyot["old_oto_no"] = oPeilut.lOldOtoNo;
                drPeilutyot["makat_nesia"] = oPeilut.lMakatNesia;
                drPeilutyot["dakot_bafoal"] = oPeilut.iDakotBafoal == -1 ? "" : oPeilut.iDakotBafoal.ToString();
                drPeilutyot["imut_netzer"] = oPeilut.bImutNetzer ? "כן" : "לא";
                drPeilutyot["makat_type"] = _Kavim.GetMakatType(oPeilut.lMakatNesia);
                drPeilutyot["mazan_tichnun"] = oPeilut.iMazanTichnun;
                _BitulOHosafa = (clGeneral.enBitulOHosafa)oPeilut.iBitulOHosafa;
                drPeilutyot["last_update"] = (_BitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser) || (_BitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat) ? oPeilut.dCardLastUpdate.ToLongDateString() : "";
                drPeilutyot["mazan_tashlum"] = oPeilut.iMazanTashlum;
                drPeilutyot["bitul_o_hosafa"] = oPeilut.iBitulOHosafa;
                //נשמור פה את מספר הכניסה, סוג הכניסה ואם פעילות חייבת מספר רכב
                //3ואם זה אלמנט עם מאפיין 9
                drPeilutyot["knisa"] = oPeilut.iMisparKnisa + "," + oPeilut.bKnisaNeeded.GetHashCode().ToString() + "," + oPeilut.bBusNumberMustExists.GetHashCode() + "," + oPeilut.bElementIgnoreReka.GetHashCode(); 
                drPeilutyot["DayToAdd"] = "0";
                if (Mashar.Rows.Count > 0)
                {
                    drLicenseNumber = Mashar.Select("bus_number=" + oPeilut.lOtoNo);
                    if (drLicenseNumber.Length > 0)
                        drPeilutyot["license_number"] = long.Parse(drLicenseNumber[0]["license_number"].ToString());
                }
                else
                    drPeilutyot["license_number"] = 0;

                if ((!oPeilut.bBusNumberMustExists) && (int.Parse(drPeilutyot["makat_type"].ToString()) == clKavim.enMakatType.mElement.GetHashCode()))
                {
                    oPeilut.lOtoNo = 0;
                    oPeilut.lOldOtoNo = 0;
                    drPeilutyot["oto_no"] = 0;
                    drPeilutyot["old_oto_no"] =0;
                }
                drPeilutyot["PeilutStatus"] = enPeilutStatus.enValid.GetHashCode();                
                dtPeilutyot.Rows.Add(drPeilutyot);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }        
    }
    protected void BuildUpdatedSidurimColumns(ref DataTable dtUpdatedSidurim)
    {              
        DataColumn dcSidur;
        dtUpdatedSidurim = new DataTable();

        //ישמור נתונים לאחר עדכון
        dcSidur = new DataColumn();
        dcSidur.ColumnName = "sidur_number";
        dcSidur.DataType = System.Type.GetType("System.Int32");
        dtUpdatedSidurim.Columns.Add(dcSidur);

        dcSidur = new DataColumn();
        dcSidur.ColumnName = "sidur_org_start_hour";
        dcSidur.DataType = System.Type.GetType("System.String");
        dtUpdatedSidurim.Columns.Add(dcSidur);

        dcSidur = new DataColumn();
        dcSidur.ColumnName = "sidur_start_hour";
        dcSidur.DataType = System.Type.GetType("System.String");
        dtUpdatedSidurim.Columns.Add(dcSidur);

        dcSidur = new DataColumn();
        dcSidur.ColumnName = "sidur_date";
        dcSidur.DataType = System.Type.GetType("System.DateTime");
        dtUpdatedSidurim.Columns.Add(dcSidur);

        dcSidur = new DataColumn();
        dcSidur.ColumnName = "sidur_nahagut";
        dcSidur.DataType = System.Type.GetType("System.Int32");
        dtUpdatedSidurim.Columns.Add(dcSidur);

        dcSidur = new DataColumn();
        dcSidur.ColumnName = "sidur_nihul_tnua";
        dcSidur.DataType = System.Type.GetType("System.Int32");
        dtUpdatedSidurim.Columns.Add(dcSidur);
    }
    public void BuildDataTableColumns(ref DataTable dtPeilutyot)
    {
        DataColumn dcPeilut;
        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "Add_Nesia_reka";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "Kisuy_Tor";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "shat_yetzia";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "Makat_Description";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "makat_shilut";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);
        
        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "Shirut_type_Name";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);
                
        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "oto_no";
        dcPeilut.DataType = System.Type.GetType("System.Int64");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "license_number";
        dcPeilut.DataType = System.Type.GetType("System.Int64");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "makat_nesia";
        dcPeilut.DataType = System.Type.GetType("System.Int64");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "dakot_bafoal";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "mazan_tichnun";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "imut_netzer";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "makat_type";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "cancel_peilut_flag";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "last_update";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "mazan_tashlum";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "bitul_o_hosafa";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "knisa";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "DayToAdd";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "Kisuy_Tor_Map";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "PeilutStatus";
        dcPeilut.DataType = System.Type.GetType("System.Int32");
        dtPeilutyot.Columns.Add(dcPeilut);
       // grdPeiluyot.Columns.Add(boundGridField);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "Update";
        dcPeilut.DataType = System.Type.GetType("System.Int32");

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "old_shat_yetzia";
        dcPeilut.DataType = System.Type.GetType("System.String");
        dtPeilutyot.Columns.Add(dcPeilut);

        dcPeilut = new DataColumn();
        dcPeilut.ColumnName = "old_oto_no";
        dcPeilut.DataType = System.Type.GetType("System.Int64");
        dtPeilutyot.Columns.Add(dcPeilut);
      
    }
    protected bool IsElementTime(long lMakatNesia)
    {

        //מחזיר אמת אם אלמנט הוא מסוג אלמנט זמן, ערך 1 במאפיין 4
        DataRow[] dr;
        long lKodElement;
        bool bElementTime = false;
     
        try
        {           
            lKodElement = GetKodElement(lMakatNesia);
            dr = MeafyeneyElementim.Select("kod_element=" + lKodElement.ToString() + " and kod_meafyen=" + clGeneral.enMeafyenElementim.Meafyen4.GetHashCode().ToString() + " and erech='" + clGeneral.enMeafyenElementim4.ElementTime.GetHashCode().ToString() + "'");

            bElementTime = (dr.Length > 0);

            return bElementTime;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool IsMeafyenExistsInElement(long lMakatNesia, long lMeafyenNum, string sValue)
    {

        //מחזיר אמת אם קיים לאלמנט ערך במאפיין נתון
        DataRow[] dr;
        long lKodElement;
        bool bElementExists = false;

        try
        {
            lKodElement = GetKodElement(lMakatNesia);
            dr = MeafyeneyElementim.Select("kod_element=" + lKodElement.ToString() + " and kod_meafyen=" + lMeafyenNum.ToString() + " and erech='" + sValue + "'");

            bElementExists = (dr.Length > 0);

            return bElementExists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected long GetKodElement(long lMakatNesia)
    {
        long lKodElement;

        lKodElement = long.Parse(lMakatNesia.ToString().PadRight(8, (char)48));
        lKodElement = (lKodElement % 10000000);
        lKodElement = (lKodElement / 100000);

        return lKodElement;
    }
    protected DataView ConvertHashPeilutToDataView(OrderedDictionary htPeilut)
    {
        DataTable dtPeilutyot = new DataTable();
        DataView dvPeiluyot;

        try
        {
            //בניית עמודות הטבלה
            BuildDataTableColumns(ref dtPeilutyot);
            //הכנסת שורות
            FillDataTableRows(ref dtPeilutyot, htPeilut);

            return dvPeiluyot = new DataView(dtPeilutyot);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected GridView BuildSidurPeiluyot(OrderedDictionary htPeilut, int iIndex, ref DataView dvPeiluyot)
    {
        GridView grdPeiluyot = new GridView();
        BoundField boundGridField;
        TemplateField tGridField = new TemplateField();
        enControlToAdd [] arrControlToAdd;        
        int iArrControlSize=0;
        
        GridControls oGridControls;

        try
        {
            //נבנה DataView עם הנתונים
            dvPeiluyot = ConvertHashPeilutToDataView(htPeilut);

            grdPeiluyot.GridLines = GridLines.None;
            grdPeiluyot.ID = iIndex.ToString().PadLeft(3, char.Parse("0"));
            grdPeiluyot.ShowHeader = true;
            grdPeiluyot.AllowPaging = false;
            grdPeiluyot.AutoGenerateColumns = false;
            grdPeiluyot.AllowSorting = true;
            grdPeiluyot.Width = Unit.Pixel(915);
            grdPeiluyot.AlternatingRowStyle.CssClass = "WCard_AltItemRow";
            grdPeiluyot.RowStyle.CssClass = "WCard_GridRow";//"WCard_GridRow";//"GridRow";
            grdPeiluyot.ShowFooter = false;
            
            //רווח
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "");
            iArrControlSize = 0;
            //Initialize the HeaderText field value.
            arrControlToAdd = new enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = enControlToAdd.TextBox;
            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "cancel_peilut_flag", iIndex);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.ItemStyle.Width = Unit.Pixel(230);
            tGridField.ItemStyle.BackColor = System.Drawing.Color.White;
            grdPeiluyot.Columns.Add(tGridField);

            //הוספת נסיעה ריקה
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "ריקה");
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer_left";
            // tGridField.ItemStyle.Width = Unit.Pixel(30);
            grdPeiluyot.Columns.Add(tGridField);

            //כיסוי תור
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "כיסוי תור");
            //Initialize the HeaderText field value.
            arrControlToAdd = new enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = enControlToAdd.TextBox;
            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "Kisuy_Tor", iIndex);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.SortExpression = "iKisuyTor";
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer";
            tGridField.ItemStyle.Width = Unit.Pixel(40);
            grdPeiluyot.Columns.Add(tGridField);

            //שעת יציאה
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "ש. יציאה");
            arrControlToAdd = new enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = enControlToAdd.TextBox;

            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "shat_yetzia", iIndex);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.SortExpression = "shat_yetzia";
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer";
            tGridField.ItemStyle.Width = Unit.Pixel(35);
            grdPeiluyot.Columns.Add(tGridField);

            //תיאור
            boundGridField = new BoundField();
            boundGridField.HeaderText = "תיאור";
            boundGridField.DataField = "Makat_Description";
            boundGridField.HeaderStyle.CssClass = "wcard_header";
            boundGridField.FooterStyle.CssClass = "wcard_footer";
            boundGridField.ItemStyle.Width = Unit.Pixel(180);
            grdPeiluyot.Columns.Add(boundGridField);

            //קו
            boundGridField = new BoundField();
            boundGridField.HeaderText = "קו";
            boundGridField.DataField = "makat_shilut";
            boundGridField.HeaderStyle.CssClass = "wcard_header";
            boundGridField.FooterStyle.CssClass = "wcard_footer";
            boundGridField.ItemStyle.Width = Unit.Pixel(40);
            grdPeiluyot.Columns.Add(boundGridField);

            //סוג
            boundGridField = new BoundField();
            boundGridField.HeaderText = "סוג";
            boundGridField.DataField = "Shirut_type_Name";
            boundGridField.HeaderStyle.CssClass = "wcard_header";
            boundGridField.FooterStyle.CssClass = "wcard_footer";
            boundGridField.ItemStyle.Width = Unit.Pixel(50);
            grdPeiluyot.Columns.Add(boundGridField);

            //מספר מק"ט
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "מק" + (char)34 + "ט");
            arrControlToAdd = new enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = enControlToAdd.TextBox;
            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "makat_nesia", iIndex);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.SortExpression = "makat_nesia";
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer";
            tGridField.ItemStyle.Width = Unit.Pixel(50);
            grdPeiluyot.Columns.Add(tGridField);

            //דקות הגדרה
            boundGridField = new BoundField();
            boundGridField.HeaderText = "ד. הגדרה";
            boundGridField.DataField = "mazan_tichnun";
            boundGridField.HeaderStyle.CssClass = "wcard_header";
            boundGridField.FooterStyle.CssClass = "wcard_footer";
            boundGridField.ItemStyle.Width = Unit.Pixel(55);
            grdPeiluyot.Columns.Add(boundGridField);

            //דקות בפועל
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "ד. בפועל");
            arrControlToAdd = new enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = enControlToAdd.TextBox;
            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "dakot_bafoal", iIndex);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.SortExpression = "dakot_bafoal";
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer";
            tGridField.ItemStyle.Width = Unit.Pixel(40);
            grdPeiluyot.Columns.Add(tGridField);

            //מספר רכב
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "מספר רכב");
            arrControlToAdd = new enControlToAdd[iArrControlSize + 1];
            arrControlToAdd[iArrControlSize] = enControlToAdd.TextBox;
            oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "oto_no", iIndex);
            tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
            tGridField.SortExpression = "oto_no";
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer";
            tGridField.ItemStyle.Width = Unit.Pixel(60);
            grdPeiluyot.Columns.Add(tGridField);

            //נצר
            boundGridField = new BoundField();
            boundGridField.HeaderText = "נצ" + (char)34 + "ר";
            boundGridField.DataField = "imut_netzer";
            boundGridField.HeaderStyle.CssClass = "wcard_header";
            boundGridField.FooterStyle.CssClass = "wcard_footer";
            boundGridField.ItemStyle.Width = Unit.Pixel(50);
            grdPeiluyot.Columns.Add(boundGridField);


            //בטל
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "פעיל");
            tGridField.HeaderStyle.CssClass = "wcard_header";
            tGridField.FooterStyle.CssClass = "wcard_footer_left";
           
            grdPeiluyot.Columns.Add(tGridField);
            grdPeiluyot.RowDataBound += new GridViewRowEventHandler(grdPeiluyot_RowDataBound);


            //עמודה נסתרת
            boundGridField = new BoundField();
            boundGridField.DataField = "last_update";           
            boundGridField.ReadOnly = true;
            grdPeiluyot.Columns.Add(boundGridField);

            //הגדרה לגמר
            boundGridField = new BoundField();
            boundGridField.HeaderText = "";
            boundGridField.DataField = "mazan_tashlum";           
            grdPeiluyot.Columns.Add(boundGridField);

            //מציין אם פעילות בוטלה
            boundGridField = new BoundField();
            boundGridField.HeaderText = "";
            boundGridField.DataField = "bitul_o_hosafa";           
            grdPeiluyot.Columns.Add(boundGridField);

            //מספר כניסה
            boundGridField = new BoundField();
            boundGridField.HeaderText = "";
            boundGridField.DataField = "knisa";            
            grdPeiluyot.Columns.Add(boundGridField);

            //מספר ימים להוסיף - שעת גמר           
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "");            
            grdPeiluyot.Columns.Add(tGridField);


            //כיסוי תור מהמפה           
            boundGridField = new BoundField();
            boundGridField.DataField = "Kisuy_Tor_map";            
            boundGridField.ReadOnly = true;
            grdPeiluyot.Columns.Add(boundGridField);

            //סטטוס פעילות - ברמת שגיאה יש שגיאה/אישור/תקין           
            iArrControlSize = 0;
            tGridField = new TemplateField();
            tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "");
            grdPeiluyot.Columns.Add(tGridField);

           
            return grdPeiluyot;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool HasNoPremmisionToAddPeilut(clSidur oSidur)
    {
        bool bNotValid = false;

        bNotValid = (((((oSidur.bSidurMyuhad) && (oSidur.sNoPeilotKod == clGeneral.enMeafyenSidur46.enAddPeilutNotAllowed.GetHashCode().ToString()))
        ||
        ((oSidur.iMisparSidur == SIDUR_CONTINUE_NAHAGUT) || (oSidur.iMisparSidur == SIDUR_CONTINUE_NOT_NAHAGUT))))
        ||
        ((oSidur.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) || (oSidur.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()))
        );

        return bNotValid;
    }
    protected void CreateAddPeilutCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bEnableSidur)
    {
        try
        {
            //hCell = CreateTableCell("70px", "", "");
            if (!(HasNoPremmisionToAddPeilut(oSidur)))
            {
                ImageButton imgAddPeilut = new ImageButton();
                imgAddPeilut.ID = "imgAddPeilut" + iIndex;
                imgAddPeilut.ImageUrl = "~/images/plus.jpg";
               // imgAddPeilut.Attributes.Add("OnClientClick", "AddPeilut(" + iIndex + "); MovePanel(" + iIndex + ");");
                //imgAddPeilut.Attributes.Add("onclick", "document.getElementById('lstSidurim_hidScrollPos').value=document.getElementById('lstSidurim_dvS').scrollTop+',' + document.getElementById('lstSidurim_dvS').scrollLeft;  hidExecInputChg.value = '0'; hidErrChg.value = '1'; MovePanel(" + iIndex + ");");
                imgAddPeilut.Attributes.Add("onclick", "hidExecInputChg.value = '0'; hidErrChg.value = '1'; MovePanel(" + iIndex + ");");
                imgAddPeilut.Attributes.Add("SdrInd", iIndex.ToString());
                imgAddPeilut.CausesValidation = false;
                imgAddPeilut.Click += new ImageClickEventHandler(imgAddPeilut_Click);
                if (!bEnableSidur)
                    imgAddPeilut.Enabled = false; 
                hCell = CreateTableCell("43px", "", "");
                hCell.Controls.Add(imgAddPeilut);
            }
            else
                hCell = CreateTableCell("53px", "", "");           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    long GetMakatEndForReaka(ref int iSidurIndex, ref int iPeilutIndex, ref long lCarNum )
    {      
        GridView _NextSidur= new GridView();
        GridView _CurrSidur = new GridView();
        GridViewRow _NextPeilut;
        bool bFound = false;
        bool bExists = false;
        bool bCanAddReka = false;
        bool bLastPeilut = IsLastPeilutInSidur(iSidurIndex,iPeilutIndex);
        long lMakatEnd=0;
        long lMakat = 0;
        
        //int iMazanTichnun;
        TextBox _txt;
        clKavim _Kavim= new clKavim();
        string[] arrKnisa;

        
        _CurrSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
        _NextPeilut = _CurrSidur.Rows[iPeilutIndex];
        //iMazanTichnun = int.Parse(((TextBox)_NextPeilut.Cells[_COL_DEF_MINUTES].Controls[0]).Text);
        if (((TextBox)_NextPeilut.Cells[_COL_CAR_NUMBER].Controls[0]).Text == string.Empty)
            lCarNum = 0;
        else
           lCarNum = long.Parse(((TextBox)_NextPeilut.Cells[_COL_CAR_NUMBER].Controls[0]).Text);

        //string sShatYetiza = ((TextBox)_NextPeilut.Cells[_COL_SHAT_YETIZA].Controls[0]).Text;
        //DateTime dPeilutDate = DateTime.Parse(((TextBox)_NextPeilut.Cells[_COL_SHAT_YETIZA].Controls[0]).Attributes["OrgDate"] + " " + sShatYetiza);

        if (bLastPeilut)
        {
            iSidurIndex = iSidurIndex + 1;            
            _NextSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
            _txt = ((TextBox)(this.FindControl("txtSH" + iSidurIndex)));
            if (_txt == null) //לא קיימים סידורים
                return 0;
            else
            {
                while (!bFound)
                {
                    if ((_NextSidur != null) && ((((TextBox)_NextSidur.Rows[0].Cells[15].Controls[0])).Text!="1"))
                        bFound = true;
                    else
                    {
                        iSidurIndex = iSidurIndex + 1;
                        _NextSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
                    }
                }
            }
        }
        else {
            _NextSidur = _CurrSidur;           
        }

        if (bLastPeilut)
        {
            iPeilutIndex = 0;
            _NextPeilut = _NextSidur.Rows[0];
        }
        else
        {
            iPeilutIndex = iPeilutIndex + 1;
            _NextPeilut = _NextSidur.Rows[iPeilutIndex];
        }



        while ((_NextPeilut != null) && (lMakatEnd == 0) && (bExists == false) && (_NextSidur != null) && ((((TextBox)_NextSidur.Rows[0].Cells[15].Controls[0])).Text != "1"))
        {
            try
            {
                bCanAddReka = ((ImageButton)_NextPeilut.Cells[_COL_ADD_NESIA_REKA].Controls[0]).Attributes["NesiaReka"].Equals("1");
            }
            catch (Exception ex)
            {
                try
                {
                    bCanAddReka = _NextPeilut.Cells[_COL_ADD_NESIA_REKA].Attributes["NesiaReka"].Equals("1");
                }
                catch (Exception exx)
                {
                    bCanAddReka = false;
                }
            }

            string sCancelPeilut = ((TextBox)(_NextPeilut.Cells[_COL_CANCEL_PEILUT].Controls[0])).Text;
            if ((bCanAddReka) && (!sCancelPeilut.Equals("1")))
            {
                //sPeilutShatYetiza = sPeilutShatYetiza.concat(NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].getAttribute("OrgDate") + ' ' + NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value) + '|';
                lMakatEnd = long.Parse(((TextBox) _NextPeilut.Cells[_COL_MAKAT].Controls[0]).Text);
                if (lCarNum != long.Parse(((TextBox) _NextPeilut.Cells[_COL_CAR_NUMBER].Controls[0]).Text))
                    lCarNum = 0;
               
            }
            else
            {                    
                    _txt = ((TextBox)(_NextPeilut.Cells[_COL_MAKAT].Controls[0]));
                    if ((_txt.Text != String.Empty) && (!sCancelPeilut.Equals("1")))
                    {
                        lMakat = long.Parse(_txt.Text);
                        arrKnisa = _NextPeilut.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
                        // 3אלמנט ללא מאפיין 9                   
                        if (((_Kavim.GetMakatType(lMakat)) == clKavim.enMakatType.mElement.GetHashCode()) && ((arrKnisa[3]) == "0"))
                            bExists = true;
                        else
                        {
                            //sPeilutShatYetiza = sPeilutShatYetiza.concat(NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].getAttribute("OrgDate") + ' ' + NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value) + '|';
                            iPeilutIndex = iPeilutIndex + 1;
                            if (iPeilutIndex < _NextSidur.Rows.Count)
                                _NextPeilut = _NextSidur.Rows[iPeilutIndex];
                            else
                                _NextPeilut = null;

                            if (_NextPeilut == null)
                            {   //אם הגענו לסוף הסידור נעבור לסידור הבא
                                iSidurIndex = iSidurIndex + 1;
                                bFound = false;
                                _NextSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
                                while ((_NextSidur != null) && (!bFound))
                                {
                                    if (((((TextBox)_NextSidur.Rows[0].Cells[15].Controls[0])).Text != "1"))
                                    {
                                        bFound = true;
                                        _NextPeilut = _NextSidur.Rows[0]; //נעמוד על הפעילות הראשונה בסידור שמצאנו
                                        iPeilutIndex = 0;
                                    }
                                    else
                                    {
                                        iSidurIndex = iSidurIndex + 1;
                                        _NextSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        iPeilutIndex = iPeilutIndex + 1;
                        if (iPeilutIndex < _NextSidur.Rows.Count)
                            _NextPeilut = _NextSidur.Rows[iPeilutIndex];
                        else
                            _NextPeilut = null;

                        if (_NextPeilut == null)
                        {   //אם הגענו לסוף הסידור נעבור לסידור הבא
                            iSidurIndex = iSidurIndex + 1;
                            bFound = false;
                            _NextSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
                            while ((_NextSidur != null) && (!bFound))
                            {
                                if (((((TextBox)_NextSidur.Rows[0].Cells[15].Controls[0])).Text != "1"))
                                {
                                    bFound = true;
                                    _NextPeilut = _NextSidur.Rows[0]; //נעמוד על הפעילות הראשונה בסידור שמצאנו
                                    iPeilutIndex = 0;
                                }
                                else
                                {
                                    iSidurIndex = iSidurIndex + 1;
                                    _NextSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
                                }
                            }
                        }
                    }
                
            }
            
        }
        return lMakatEnd;
    }
    bool IsLastPeilutInSidur(int iSidurIndex, int iPeilutIndex)
    {
        bool bLastPeilut = false;
        GridView oGridView;
        
        try 
        {
             oGridView = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
             if (oGridView != null)             
                bLastPeilut = (iPeilutIndex + 1 == oGridView.Rows.Count);                 
             
            return bLastPeilut;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InsertPeilutReaka(int iSidurIndex, int iPeilutIndex, long lMakat, long lCarNum,
                                   DataTable _NesiaDetails)
    {
        int iMazanTichnun, iPos;
        GridView _CurrSidur;
        GridViewRow _NextPeilut;
        clSidur _Sidur = new clSidur();
        clPeilut _Peilut = new clPeilut();
        clKavim _Kavim = new clKavim();
        int iSidurNumber = 0;
        DateTime dSidurShatHatchala = new DateTime();
        DateTime dPeilutDate = CardDate;
        DateTime dPeilutShatYetiza;
        try
        {
            _CurrSidur = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
            _NextPeilut = _CurrSidur.Rows[iPeilutIndex];
            iMazanTichnun = int.Parse(_NextPeilut.Cells[_COL_DEF_MINUTES].Text);
            string sShatYetiza = ((TextBox)_NextPeilut.Cells[_COL_SHAT_YETIZA].Controls[0]).Text;
            
            if (sShatYetiza.Equals(""))
                dPeilutShatYetiza = DateTime.Parse("01/01/0001 00:00:00");
             else
                {
                if (((TextBox)_NextPeilut.Cells[_COL_DAY_TO_ADD].Controls[0]).Text.Equals("1"))
                    dPeilutDate=dPeilutDate.AddDays(1);

                dPeilutShatYetiza = DateTime.Parse(dPeilutDate.ToShortDateString() + " " + sShatYetiza);

                dPeilutShatYetiza = dPeilutShatYetiza.AddMinutes(iMazanTichnun);
                }
            //_CurrSidur = ((GridView)this.FindControl(iSidurIndexNew.ToString().PadLeft(3, char.Parse("0"))));
            //_NextPeilut = _CurrSidur.Rows[iPeilutIndexNew];
                        
            iSidurNumber = GetSidurKey(iSidurIndex, ref dSidurShatHatchala);
            for (int iIndex = 0; iIndex < _DataSource.Count; iIndex++)
            {
                _Sidur = (clSidur)(_DataSource[iIndex]);
                if ((_Sidur.iMisparSidur.Equals(iSidurNumber)) && (_Sidur.dFullShatHatchala.Equals(dSidurShatHatchala)))
                    break;
            }
           
            _Peilut.oPeilutStatus = clPeilut.enPeilutStatus.enNew;
            _Peilut.dFullShatYetzia = dPeilutShatYetiza;
            _Peilut.dOldFullShatYetzia = dPeilutShatYetiza;
            _Peilut.sShatYetzia = (sShatYetiza==string.Empty) ? "" : dPeilutShatYetiza.ToShortTimeString(); 
            _Peilut.lMakatNesia = lMakat;
            _Peilut.lOldOtoNo = lCarNum;
            _Peilut.lOtoNo = lCarNum;
            _Peilut.iMakatType = _Kavim.GetMakatType(lMakat);
            _Peilut.iMazanTashlum = String.IsNullOrEmpty(_NesiaDetails.Rows[0]["mazantashlum"].ToString()) ? 0 : int.Parse(_NesiaDetails.Rows[0]["mazantashlum"].ToString());
            _Peilut.iMazanTichnun = String.IsNullOrEmpty(_NesiaDetails.Rows[0]["mazantichnun"].ToString()) ? 0 : int.Parse(_NesiaDetails.Rows[0]["mazantichnun"].ToString());
            _Peilut.sMakatDescription = _NesiaDetails.Rows[0]["description"].ToString();
            _Peilut.sSugShirutName = COL_TRIP_EMPTY;

            iPos = FindPeilutPos(dPeilutShatYetiza, _Sidur.htPeilut);
            if (iPos == _Sidur.htPeilut.Count)
            {
                _Peilut.dFullShatYetzia = DateTime.Parse("01/01/0001 00:00");
                _Peilut.dOldFullShatYetzia = _Peilut.dFullShatYetzia;
                _Peilut.sShatYetzia ="";
                _Sidur.htPeilut.Add(_Sidur.htPeilut.Count + 1, _Peilut);
            }
            else
            {
                if (iPos > _Sidur.htPeilut.Count)
                    //מקרה שנכנסת פעילות עם שעה בסוף הסידור
                    _Sidur.htPeilut.Add(_Sidur.htPeilut.Count + 1, _Peilut);
                else
                //נכניס את הפעילות באמצע
                  ChangePeiluyotIndex(iPos, ref _Sidur.htPeilut, ref _Peilut);
            }
            Session["Sidurim"] = _DataSource;
           
            ClearControl();
            BuildPage();
           
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void ChangePeiluyotIndex(int iPos, ref OrderedDictionary htPeilyout, ref clPeilut oPeilutNew)
    {
        clPeilut _Peilut;
        OrderedDictionary htPeilyoutNew = new OrderedDictionary();
        int iIndexNew=0;
        try{
            for (int i = 0; i < htPeilyout.Count; i++)
            {
                if (i == iPos)
                {
                    htPeilyoutNew.Add(i, oPeilutNew);
                    i = i - 1;
                    iPos = -1;
                }
                else
                {
                    _Peilut = (clPeilut)htPeilyout[i];
                    htPeilyoutNew.Add(iIndexNew, _Peilut);
                }
                iIndexNew = iIndexNew + 1;                
            }
            htPeilyout = htPeilyoutNew;
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private int FindPeilutPos(DateTime dPeilutShatYetiza, OrderedDictionary htPeiluyot)
    {
        //נמצא את המיקום שבו נכניס את הפעילות החדשה
        int iPos=0;
        bool bFirst = false; //מציין אם המיקום הוא בפעילות הראשונה
        bool bFound = false; //חייבים לעבור על כל הפעילויות כיוון שפעילויות שנכנסו ע"י הוספת פעילויות הן לא ממויינות ויכולות להיות בעלות אותה שעה של הריקה החדשה
        clPeilut _Peilut;
        for (int i = 0; i < htPeiluyot.Count; i++)
        {
            _Peilut = (clPeilut)htPeiluyot[i];
            if (_Peilut.dFullShatYetzia == dPeilutShatYetiza)
            {
                iPos = htPeiluyot.Count;
                break;
            }
            if (_Peilut.dFullShatYetzia > dPeilutShatYetiza)
            {
                if (!bFound)
                {
                    bFound = true;
                    iPos = i;
                    if (iPos == 0)
                        bFirst = true;
                }
            }
        }

        if ((iPos == 0) && (!bFirst))
            iPos = htPeiluyot.Count+1; //אם לא מצאנו, יש להכניס את הפעילות בסוף עם שעה מלאה
        return iPos;
    }
    private bool AddNesiaReka(int iSidurIndexOrg, int iPeilutIndexOrg, long lMakatEnd, ref long lMakat, ref DataTable _NesiaDetails)
         //int iMisparSidur, string sSidurShatHatchala, long lMakatStart, long lMakatEnd, string sShatYetiza,
           //                   string sPeilutDate, int iMazanTichnun, long lCarNum, string sPeilutShatYetiza)
    {
      
        DataTable _Peiluyot;
        DataRow[] dr;
        long xPosStart=0;
        long xPosEnd=0;        
        clKavim _Kavim = new clKavim();
        int iResult = 0;
        bool bFound = false;
        long lMakatStart = 0;
        long lRepresentMakat8 = 0;
        GridView _CurrSidur;
        GridViewRow _NextPeilut;
        //DateTime dPeilutShatYetiza = DateTime.Parse(sPeilutDate + " " + sShatYetiza);

        try
        {
            _CurrSidur = ((GridView)this.FindControl(iSidurIndexOrg.ToString().PadLeft(3, char.Parse("0"))));
            _NextPeilut = _CurrSidur.Rows[iPeilutIndexOrg];
            //iMazanTichnun = int.Parse(((TextBox)_NextPeilut.Cells[_COL_DEF_MINUTES].Controls[0]).Text);           
            lMakatStart  = long.Parse(((TextBox)_NextPeilut.Cells[_COL_MAKAT].Controls[0]).Text);
            

            _Peiluyot = (DataTable)HttpRuntime.Cache.Get(MisparIshi.ToString() + CardDate.ToShortDateString());
            dr = _Peiluyot.Select("makat8=" + lMakatStart);
            if (dr.Length > 0)
                xPosStart = String.IsNullOrEmpty(dr[0]["xy_moked_siyum"].ToString()) ? 0 : long.Parse(dr[0]["xy_moked_siyum"].ToString());
            dr = _Peiluyot.Select("makat8=" + lMakatEnd);
            if (dr.Length > 0)
                xPosEnd = String.IsNullOrEmpty(dr[0]["xy_moked_tchila"].ToString()) ? 0 : long.Parse(dr[0]["xy_moked_tchila"].ToString());


            _NesiaDetails = _Kavim.GetRekaDetailsByXY(CardDate, xPosStart, xPosEnd, out iResult);

            //נמצאה ריקה מתאימה
            if (_NesiaDetails.Rows.Count > 0)
            {
                if (iResult == 0)
                {
                    bFound = true;
                    lMakat = String.IsNullOrEmpty(_NesiaDetails.Rows[0]["makat8"].ToString()) ? 0 : long.Parse((_NesiaDetails.Rows[0]["makat8"].ToString()));
                    lRepresentMakat8 = String.IsNullOrEmpty(_NesiaDetails.Rows[0]["representmakat8"].ToString()) ? 0 : long.Parse((_NesiaDetails.Rows[0]["representmakat8"].ToString()));
                    if ((lMakat == 0) && (lRepresentMakat8 > 0))
                        lMakat = lRepresentMakat8;

                    //InsertNesiaRekaToDB(int.Parse(Session["LoginUserEmp"].ToString()), iMisparIshi, sCardDate, iMisparSidur, sSidurShatHatchala, lMakat, lCarNum, dPeilutShatYetiza, sPeilutShatYetiza);
                    //HttpRuntime.Cache.Remove(iMisparIshi.ToString() + DateTime.Parse(sCardDate).ToShortDateString());
                }
            }
           
            return bFound ; 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    void imgAddReka_Click(object sender, ImageClickEventArgs e)
    {
        long lMakatEnd;
        long lMakat = 0;
        long lCarNum = 0;
        int iSidurIndex = int.Parse(((ImageButton)sender).Attributes["SdrInd"]);
        int iPeilutIndex = int.Parse(((ImageButton)sender).Attributes["PeilutInd"]);
        int iSidurIndexOrg = iSidurIndex;
        int iPeilutIndexOrg = iPeilutIndex;
        string sScript;
        bool bOpenUpdateBtn = false;
        

        //נמצא את מספר המק"ט הבא שביניהם תכנס הנסיעה הריקה
        lMakatEnd = GetMakatEndForReaka(ref iSidurIndex, ref iPeilutIndex, ref lCarNum);
        if (lMakatEnd == 0)
        {
            sScript = "alert(' לא ניתן להשלים נסיעה ריקה');";
            ScriptManager.RegisterStartupScript((ImageButton)sender, sender.GetType(), "AddReka", sScript, true);
        }
        else
        {
            DataTable _NesiaDetails = new DataTable();
            if (AddNesiaReka(iSidurIndexOrg, iPeilutIndexOrg, lMakatEnd, ref lMakat, ref _NesiaDetails))
            {              
                OrderedDictionary hashSidurimPeiluyot = DataSource;
                UpdateHashTableWithGridChanges(ref hashSidurimPeiluyot);               
                InsertPeilutReaka(iSidurIndexOrg, iPeilutIndexOrg, lMakat, lCarNum, _NesiaDetails);
                bOpenUpdateBtn = true;
            }
            else            
            {
                sScript = "alert('לא נמצאה ריקה מתאימה');";
                ScriptManager.RegisterStartupScript((ImageButton)sender, sender.GetType(), "GetRekaFromTnua", sScript, true);
            }
        }

        //נציין כאילו שינוי הקלט עבדו בהצלחה
        if (btnHandler != null)
            btnHandler(string.Empty, bOpenUpdateBtn);
    }
    void imgAddPeilut_Click(object sender, ImageClickEventArgs e)
    {        
        AddEmptyRowToPeilutGrid(int.Parse(((ImageButton)sender).Attributes["SdrInd"]));
        //נציין כאילו שינוי הקלט עבדו בהצלחה
        if (btnHandler != null)
            btnHandler(string.Empty, true);

        //string sScript = "SetScrollPosition();";//"document.getElementById('lstSidurim_dvS').scrollTop= Number(document.getElementById('lstSidurim_hidScrollPos').value) + 600";
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "SaveScrollPos", sScript, true);
    }
    protected Image AddImage(string sImageUrl,string sImageId, string sOnClickScript)
    {
        Image imgCollapse = new Image();
        imgCollapse.ImageUrl = sImageUrl;
        imgCollapse.ID = sImageId;
        imgCollapse.Attributes.Add("onclick", sOnClickScript);

        return imgCollapse;
    }
    protected HyperLink AddHyperLink(string sId, string sText, string sOnClickScript)
    {
        HyperLink lnkSidur = new HyperLink();
        lnkSidur.Text = sText;
        lnkSidur.ID = sId;
        lnkSidur.Style.Add("text-decoration", "underline");
        lnkSidur.Style.Add("cursor", "pointer");         
        lnkSidur.Attributes.Add("onclick", sOnClickScript);
        return lnkSidur;
    }
    protected Label AddLabel(string sId, string sText, string sOnClickScript, int iWidth)
    {
        Label lbl = new Label();
        lbl.Text = sText;
        lbl.ID = sId;
        lbl.Width = Unit.Pixel(iWidth);
        lbl.Attributes.Add("onclick", sOnClickScript);
        return lbl;
    }
    protected void CreateSidurCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex)
    {
        try
        {
            Image imgErr = new Image();
            imgErr.ID = "imgSdr" + iIndex;

            LiteralControl lDummy = new LiteralControl();
            lDummy.Text = " ";
            hCell = CreateTableCell("95px", "", "");
            //אם לסידור יש פעילויות, נציג IMG COLLAPSE 
            if (oSidur.htPeilut.Count > 0){
                hCell.Controls.Add(AddImage("~/images/collapse_blue_big.jpg", "cImgS" + iIndex, "ChgImg(" + iIndex + ")"));
            }
          
            hCell.Controls.Add(lDummy);

            DataRow[] dr = dtApprovals.Select("mafne_lesade='mispar_sidur'");
            //נבדוק אם לסידור יש אפשרות להוסיף שדות, אם כן נציג אותו כלינק
            if (IsSadotNosafimLSidur(ref oSidur, MeafyeneySidur, SadotNosafim))
            {
                HyperLink lnkSidur = new HyperLink();                              
                lnkSidur = AddHyperLink("lblSidur" + iIndex,oSidur.iMisparSidur.ToString(),"AddSadotLsidur(" + iIndex + ");");
               // lnkSidur.Attributes.Add("OnClick", "RefreshBtn();");
                lnkSidur.ToolTip = oSidur.sSidurDescription;
                lnkSidur.Attributes.Add("SidurVisa", IsSidurVisa(ref oSidur) ? "1":"0");
                lnkSidur.Attributes.Add("Sidur93", oSidur.iElement1Hova.ToString());
                lnkSidur.Attributes.Add("Sidur94", oSidur.iElement2Hova.ToString());
                lnkSidur.Attributes.Add("Sidur95", oSidur.iElement3Hova.ToString());
                
                hCell.Controls.Add(lnkSidur);
                                
                switch (_StatusCard)
                {
                    case clGeneral.enCardStatus.Error:
                        if (SetOneError(lnkSidur, hCell, "Mispar_sidur", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ID))
                        {
                            imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                            imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                            imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + lnkSidur.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");

                            hCell.Controls.Add(lDummy);
                            hCell.Controls.Add(imgErr);
                            hCell.Style.Add("background-color", "white");
                            hCell.Style.Add("color", "black");
                        }
                        else
                        {
                            //אישורים נראה בכל מקרה גם כשהכרטיס שגוי                           
                            if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref hCell))
                                lnkSidur.Style.Add("color", "white");               
                           
                        }
                        
                        //נבדוק שגם אין שגיאות ברמת שדות נוספים
                        if (IsErrorInAdditionFields(ErrorsList, ref oSidur))                            
                            lnkSidur.Style.Add("color", "red");                        
                        
                        break;
                    case clGeneral.enCardStatus.Valid:
                        //DataRow[] dr = dtApprovals.Select("mafne_lesade='mispar_sidur'");
                        CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref hCell);
                        break;
                }                                                 
            }
            else
            {
                Label lbl = new Label();
                lbl = AddLabel("lblSidur" + iIndex, oSidur.iMisparSidur.ToString(), "MovePanel(" + iIndex + ");", 40);
                lbl.ToolTip = oSidur.sSidurDescription;
                lbl.Attributes.Add("SidurVisa", IsSidurVisa(ref oSidur) ? "1" : "0");
                lbl.Attributes.Add("Sidur93", oSidur.iElement1Hova.ToString());
                lbl.Attributes.Add("Sidur94", oSidur.iElement2Hova.ToString());
                lbl.Attributes.Add("Sidur95", oSidur.iElement3Hova.ToString());
                
                hCell.Controls.Add(lbl);

               // DataRow[] dr = dtApprovals.Select("mafne_lesade='mispar_sidur'");
                
                switch (_StatusCard)
                {
                    case clGeneral.enCardStatus.Error:
                        if (SetOneError(lbl, hCell, "Mispar_sidur", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ID))
                        {
                            imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                            imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                            imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + lbl.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");
                            hCell.Controls.Add(lDummy);
                            hCell.Controls.Add(imgErr);
                            hCell.Style.Add("background-color", "white");
                            hCell.Style.Add("color", "black"); 
                        }
                        else
                            CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref hCell);
                        break;
                    case clGeneral.enCardStatus.Valid:
                        //DataRow[] dr = dtApprovals.Select("mafne_lesade='mispar_sidur'");                        
                        CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref hCell);
                        break;                    
                }                                
            }            
            hCell.Style.Add("valign", "top");
            hCell.Style.Add("border-left", "solid 1px gray");
            hCell.Style.Add("border-right", "solid 1px gray");            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CreateAddDayToShatGmarHiddenCell(ref HtmlTableCell hCell, int iIndex, clSidur oSidur)
    {
        hCell = CreateTableCell("20px", "", "");
        hCell.Style.Add("Display", "none");
        TextBox txt = new TextBox();
        txt.ID = "txtDayAdd" + iIndex;
        txt.Text = ((oSidur.dFullShatGmar.ToShortDateString() == oSidur.dSidurDate.ToShortDateString()) || (oSidur.dFullShatGmar.Year<clGeneral.cYearNull)) ? "0" : "1";
        hCell.Controls.Add(txt);
    }
    protected void CreateCancelFlagHiddenCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex)
    {
        hCell = CreateTableCell("0px", "", "");
        hCell.Style.Add("Display", "none");
        TextBox txt = new TextBox();
        txt.ID = "lblSidurCanceled" + iIndex;
        txt.Text = oSidur.iBitulOHosafa.ToString();
        hCell.Controls.Add(txt);
    }
    protected void CreateNahagutHiddenCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex)
    {
        hCell = CreateTableCell("0px", "", "");
        hCell.Style.Add("Display", "none");
        Label lbl = new Label();
        lbl.ID = "lblSidurNahagut" + iIndex;
        lbl.Text = bSidurNahagut.GetHashCode().ToString();
        hCell.Controls.Add(lbl);
    }
    protected void CreateSidurDateHiddenCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex)
    {
        hCell = CreateTableCell("0px", "", "");
        hCell.Style.Add("Display", "none");
        Label lbl = new Label();
        lbl.ID = "lblDate" + iIndex;
        lbl.Text = oSidur.dFullShatHatchala.ToShortDateString();
        hCell.Controls.Add(lbl);
    }
    protected void CreateShatHatchalaMutereHiddentCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex)
    {
        hCell = CreateTableCell("0px", "", "");               
        hCell.Style.Add("Display", "none");
        Label lbl = new Label();               
        lbl.ID = "lblShatHatchalaMuteret" + iIndex;
        lbl.Text = String.IsNullOrEmpty(oSidur.sShatHatchalaMuteret) ? "" : DateTime.Parse(oSidur.sShatHatchalaMuteret).ToShortTimeString();
        hCell.Controls.Add(lbl);       
    }
    protected void CreateShatGmarMutereHiddentCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex)
    {
        //שעת גמר מותרת לסידור, ערך מאפיין 8
        hCell = CreateTableCell("0px", "", "");                
        hCell.Style.Add("Display", "none");
        Label lbl = new Label();                
        lbl.ID = "lblShatGmarMuteret" + iIndex;
        lbl.Text = String.IsNullOrEmpty(oSidur.sShatGmarMuteret) ? "" : DateTime.Parse(oSidur.sShatGmarMuteret).ToShortTimeString(); 
        hCell.Controls.Add(lbl);
    }
    protected void CreateCancelCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive, bool bEnableSidur)
    {
        hCell = CreateTableCell("35px", "", "");
        Button btnImage = new Button();
        btnImage.ID = "imgCancel" + iIndex;
        btnImage.CssClass = bSidurActive ? "ImgChecked" : "ImgCancel";
        if ((!_ProfileRashemet) || (!bEnableSidur)){        
            btnImage.Attributes.Add("disabled", "true");
        }        
        //btnImage.OnClientClick = "if (!ChangeStatusSidur(this.id)) {return false;} else {return true;} ";
        btnImage.OnClientClick = "return ChangeStatusSidur(this.id);";
        btnImage.ToolTip = bSidurActive ? "" : oSidur.dTaarichIdkunAcharon.ToLongDateString();
        //btnImage.Click += new ImageClickEventHandler(btnImage_Click);       
        hCell.Controls.Add(btnImage);
    }
    protected void CreateLoLetashlumCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {
        bool bEnable;
        HtmlInputCheckBox chkBox = new HtmlInputCheckBox();
        chkBox.ID = "chkLoLetashlum" + iIndex;
        chkBox.Checked = (oSidur.iLoLetashlum == 1);
        bEnable = (((IsLoLetashlum(ref oSidur))) && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "LO_LETASHLUM"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
        chkBox.Disabled = ((!(bSidurActive)) || (!bEnable));
        chkBox.Attributes.Add("onclick", "MovePanel(" + iIndex + ");SetBtnChanges();SetLvlChg(2," + iIndex + ");");
        chkBox.Attributes.Add("OrgVal", oSidur.iOldLoLetashlum.ToString());
        chkBox.Attributes.Add("OrgEnabled", bEnable ? "1" : "0");   
       // AddAttribute(chkBox, "OldV", chkBox.Checked.GetHashCode().ToString());

        Image imgRemark = new Image();
        imgRemark.ImageUrl = "../../Images/questionbtn.jpg";
        imgRemark.Attributes.Add("onclick", "ShowRemark(" + iIndex + ");MovePanel(" + iIndex + ");");
        if (!chkBox.Checked)
        {
            hCell = CreateTableCell("70px", "", "");
            imgRemark.Style.Add("display", "none");
        }
        else
        {
            hCell = CreateTableCell("57px", "", "");
        }
        hCell.Controls.Add(chkBox);              
        hCell.Controls.Add(imgRemark);

        hCell.Style.Add("border-left", "solid 1px gray");        
    }
    protected void CreateOutMichsaCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {
        bool bEnabled;
        bool bEnableApproval = false;
        hCell = CreateTableCell("64px", "", "");
        HtmlInputCheckBox chkBox = new HtmlInputCheckBox();
        chkBox.ID = "chkOutMichsa" + iIndex;
        chkBox.Checked = (oSidur.sOutMichsa == "1");
        bEnabled = (IsOutMichsaAllowed(ref oSidur)) && ((!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "OUT_MICHSA"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
        chkBox.Disabled = (!((bEnabled) && (bSidurActive)));
        chkBox.Attributes.Add("OrgEnabled", bEnabled ? "1" : "0");
        chkBox.Attributes.Add("onclick", "MovePanel(" + iIndex + ");SetBtnChanges();SetLvlChg(2," + iIndex + ");");
        //AddAttribute(chkBox, "OldV", chkBox.Checked.GetHashCode().ToString());

        string sAllApprovalDescription = "";
        DataRow[] dr = dtApprovals.Select("mafne_lesade='Out_michsa'");
       
        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                //שגיאות לשדה
                if (!SetOneError(chkBox, hCell, "Out_michsa", ref oSidur, iIndex.ToString(), ""))
                {
                    if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription, ref bEnableApproval))
                    {
                        Image imgApp;
                        imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgAOutMichsa" + iIndex, "GetAppMsg(this)", "MovePanel(" + iIndex + ");", sAllApprovalDescription);
                        chkBox.Attributes.Add("class", "ApprovalField");
                        hCell.Controls.Add(imgApp);
                        LiteralControl lDummy = new LiteralControl();
                        lDummy.Text = " ";
                        hCell.Controls.Add(lDummy);
                    }
                }
                break;
            case clGeneral.enCardStatus.Valid:
                //string sAllApprovalDescription = "";
                //DataRow[] dr = dtApprovals.Select("mafne_lesade='Out_michsa'");
                if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription,ref bEnableApproval))
                {
                    Image imgApp;
                    imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgAOutMichsa" + iIndex, "GetAppMsg(this)", "MovePanel(" + iIndex + ");", sAllApprovalDescription);
                    chkBox.Attributes.Add("class", "ApprovalField");
                    hCell.Controls.Add(imgApp);
                    LiteralControl lDummy = new LiteralControl();
                    lDummy.Text = " ";
                    hCell.Controls.Add(lDummy);
                    if (!chkBox.Disabled)                    
                        chkBox.Disabled = (!bEnableApproval);                    
                }
                break;

        }
        hCell.Controls.Add(chkBox);
        hCell.Style.Add("border-left", "solid 1px gray");
    }
    public void UpdateHashTableWithGridChanges(ref OrderedDictionary htFullEmployeeDetails)
    {
        GridView oGridView;
        clSidur oSidur;
        Label oLbl;
        HyperLink oHypLnk = new HyperLink();
        TextBox oTxt, oShatGmar, oDayToAdd;
        DropDownList oDDL;
        string sTmp;
        DateTime dSidurDate;
        int iCancelSidur;
        HtmlInputCheckBox oChk;
        for (int iIndex = 0; iIndex < this.DataSource.Count; iIndex++)
        {
            try
            {
                oLbl = (Label)this.FindControl("lblSidur" + iIndex);
            }
            catch (Exception ex)
            {
                oHypLnk = (HyperLink)this.FindControl("lblSidur" + iIndex);
                oLbl = null;
            }
            if ((oLbl != null) || (oHypLnk != null))
            {
                oSidur = (clSidur)((htFullEmployeeDetails)[iIndex]);

                oSidur.iMisparSidur = (oLbl == null ? int.Parse(oHypLnk.Text) : int.Parse(oLbl.Text));
                oTxt = ((TextBox)(this.FindControl("txtSH" + iIndex)));
                if (oTxt.Text == string.Empty)
                {
                    oSidur.dFullShatHatchala = DateTime.Parse("01/01/0001 00:00:00");
                    oSidur.sShatHatchala = "";
                }
                else
                {
                    oLbl = (Label)this.FindControl("lblDate" + iIndex);
                    oSidur.dFullShatHatchala = DateTime.Parse(oLbl.Text + " " + oTxt.Text);
                    oSidur.sShatHatchala = oTxt.Text;
                }

                oShatGmar = ((TextBox)(this.FindControl("txtSG" + iIndex)));
                //מספר ימים להוספה 0 אם יום נוכחי1 - יום הבא
                oDayToAdd = ((TextBox)(this.FindControl("txtDayAdd" + iIndex)));
                sTmp = oShatGmar.Text;
                dSidurDate = DateTime.Parse(oSidur.dOldFullShatGmar.ToShortDateString() + " " + sTmp);//DateTime.Parse(oShatGmar.Attributes["OrgDate"].ToString() + " " + sTmp);
                oSidur.sShatGmar = sTmp;
                if (sTmp != string.Empty)
                    oSidur.dFullShatGmar = DateTime.Parse(_CardDate.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));
                else
                    oSidur.dFullShatGmar = DateTime.Parse("01/01/0001 00:00:00");


                oDDL = (DropDownList)this.FindControl("ddlResonIn" + iIndex);
                if (oDDL != null)//רק אם שונה מסידור רציפות
                {
                    oSidur.iKodSibaLedivuchYadaniIn = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

                    oDDL = (DropDownList)this.FindControl("ddlResonOut" + iIndex);
                    oSidur.iKodSibaLedivuchYadaniOut = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

                    sTmp = ((TextBox)(this.FindControl("txtSHL" + iIndex))).Text;
                    oSidur.dFullShatHatchalaLetashlum = clGeneral.GetDateTimeFromStringHour(sTmp, DateTime.Parse(oSidur.dFullShatHatchalaLetashlum.ToShortDateString()));
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToShortTimeString();

                    sTmp = ((TextBox)(this.FindControl("txtSGL" + iIndex))).Text;
                    oSidur.dFullShatGmarLetashlum = clGeneral.GetDateTimeFromStringHour(sTmp, DateTime.Parse(oSidur.dFullShatGmarLetashlum.ToShortDateString()));
                    oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToShortTimeString();

                    oDDL = (DropDownList)this.FindControl("ddlException" + iIndex);
                    oSidur.sChariga = oDDL.SelectedValue;

                    oDDL = (DropDownList)this.FindControl("ddlPHfsaka" + iIndex);
                    oSidur.sPitzulHafsaka = oDDL.SelectedValue.Equals("-1") ? "0" : oDDL.SelectedValue; //oDDL.Attributes["OldV"].ToString();

                    oDDL = (DropDownList)this.FindControl("ddlHashlama" + iIndex);
                    oSidur.sHashlama = oDDL.SelectedValue;
                    if (int.Parse(oDDL.SelectedValue) == clGeneral.enSugHashlama.enNoHashlama.GetHashCode())
                        oSidur.iSugHashlama = clGeneral.enSugHashlama.enNoHashlama.GetHashCode();
                    else
                        oSidur.iSugHashlama = clGeneral.enSugHashlama.enHashlama.GetHashCode();

                    oChk = (HtmlInputCheckBox)this.FindControl("chkOutMichsa" + iIndex);
                    oSidur.sOutMichsa = oChk.Checked ? "1" : "0";

                    iCancelSidur = int.Parse(((TextBox)this.FindControl("lblSidurCanceled" + iIndex)).Text);

                    oSidur.iBitulOHosafa = iCancelSidur;

                    //לא לתשלום - במידה וסידור בוטל נעדכן בערך המקורי - ישאר ללא שינוי
                    oChk = (HtmlInputCheckBox)this.FindControl("chkLoLetashlum" + iIndex);

                    int iLoLetashlumOrgVal = int.Parse(oChk.Attributes["OrgVal"].ToString());

                    if (iCancelSidur == 1)
                        oSidur.iLoLetashlum = iLoLetashlumOrgVal;
                    else
                        oSidur.iLoLetashlum = oChk.Checked ? 1 : 0;

                    if ((oSidur.iLoLetashlum == 1) && (iLoLetashlumOrgVal == 0))
                        oSidur.iKodSibaLoLetashlum = 20;
                    else
                        if ((oSidur.iLoLetashlum == 0) && (iLoLetashlumOrgVal == 1))
                            oSidur.iKodSibaLoLetashlum = 0;

                    //DropDownList _DDL;
                    //TextBox _HitTextBox;
                    ////התייצבות                            
                    //if (FirstParticipate != null)
                    //{
                    //    _DDL = ((DropDownList)this.FindControl("ddlFirstPart"));
                    //    _HitTextBox = ((TextBox)this.FindControl("txtFirstPart"));
                    //    if ((FirstParticipate.iMisparSidur == oSidur.iMisparSidur)
                    //        && (FirstParticipate.dFullShatHatchala == oSidur.dFullShatHatchala))
                    //    {
                    //        if (!String.IsNullOrEmpty(_DDL.SelectedValue))
                    //            oSidur.iKodSibaLedivuchYadaniIn = _DDL.SelectedValue.Equals("-1") ? 0 : int.Parse(_DDL.SelectedValue);

                    //        if (!String.IsNullOrEmpty(_HitTextBox.Text) && (_HitTextBox.Text.IndexOf(":") > 0))
                    //            oSidur.dShatHitiatzvut = DateTime.Parse(_CardDate.ToShortDateString() + " " + _HitTextBox.Text);
                    //    }
                    //}
                    //if (SecondParticipate != null)
                    //{
                    //    _DDL = ((DropDownList)this.FindControl("ddlSecPart"));
                    //    _HitTextBox = ((TextBox)this.FindControl("txtFirstPart"));
                    //    if ((SecondParticipate.iMisparSidur == oSidur.iMisparSidur)
                    //        && (SecondParticipate.dFullShatHatchala == oSidur.dFullShatHatchala))
                    //    {
                    //        if ((!String.IsNullOrEmpty(_HitTextBox.Text)) && (_HitTextBox.Text.IndexOf(":") > 0))
                    //            oSidur.dShatHitiatzvut = DateTime.Parse(_HitTextBox.Text);

                    //        if (!String.IsNullOrEmpty(_DDL.SelectedValue))
                    //            oSidur.iKodSibaLedivuchYadaniIn = _DDL.SelectedValue.Equals("-1") ? 0 : int.Parse(_DDL.SelectedValue);
                    //    }
                    //}                   
                    //אם יש פעילויות, נכניס גם אותן
                    oGridView = ((GridView)this.FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
                    if (oGridView != null)
                        UpdateHashTablePeiltoyWithGridChanges(iIndex, iCancelSidur, ref oGridView, ref htFullEmployeeDetails);
                }
            }
        }
        Session["Sidurim"] = htFullEmployeeDetails;
    }
    private void UpdateHashTablePeiltoyWithGridChanges(int iSidurIndex,int iCancelSidur, ref GridView oGridView, ref OrderedDictionary htEmployeeDetails)
    {
        GridViewRow oGridRow;
        int iDayToAdd, iMisparKnisa;
        string sDayToAdd, sShatYetiza, sKisuyTor;
        TextBox oShatYetiza;
        DateTime dShatYetiza, dKisuyTor;
        Double dblKisuyTor;
        string[] arrKnisaVal;
        clPeilut _Peilut;
        clSidur _Sidur;
      
        try
        {
            _Sidur = (clSidur)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
            //פעילויות
            for (int iRowIndex = 0; iRowIndex < oGridView.Rows.Count; iRowIndex++)
            {
                _Peilut = (clPeilut)_Sidur.htPeilut[iRowIndex];
                oGridRow = oGridView.Rows[iRowIndex];
                oShatYetiza = ((TextBox)oGridRow.Cells[COL_SHAT_YETIZA].Controls[0]);
                sDayToAdd = ((TextBox)oGridRow.Cells[COL_DAY_TO_ADD].Controls[0]).Text;
                iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);

                _Peilut.dCardDate = _CardDate;
                _Peilut.iPeilutMisparSidur = _Sidur.iMisparSidur;

                dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
                sShatYetiza = oShatYetiza.Text;
                if (dShatYetiza.Date.Year < clGeneral.cYearNull)
                    if (oShatYetiza.Text != string.Empty)
                    {
                        if ((DateTime.Parse(oShatYetiza.Text).Hour > 0) || (DateTime.Parse(oShatYetiza.Text).Minute > 0))
                            oShatYetiza.Attributes["OrgDate"] = _Sidur.dFullShatHatchala.ToShortDateString();
                    }
                    else //שעת יציאה ריקה
                        sShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]).ToShortTimeString();

                dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"] + " " + sShatYetiza);

                if (dShatYetiza.Date == _CardDate.Date)
                    dShatYetiza = dShatYetiza.AddDays(iDayToAdd);
                else
                    if (dShatYetiza.Date.Year > clGeneral.cYearNull)
                    {
                        if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
                            dShatYetiza = dShatYetiza.AddDays(-1);
                    }
                sKisuyTor = ((TextBox)oGridRow.Cells[COL_KISUY_TOR].Controls[0]).Text;
                if (sKisuyTor != string.Empty)
                {
                    dKisuyTor = DateTime.Parse(dShatYetiza.ToShortDateString() + " " + sKisuyTor);
                    dblKisuyTor = (dShatYetiza - dKisuyTor).TotalMinutes;
                    if (dblKisuyTor < 0)
                        dblKisuyTor = 1440 + dblKisuyTor;
                }
                else
                    dblKisuyTor = 0;

               
                _Peilut.lOtoNo = ((TextBox)(oGridRow.Cells[COL_CAR_NUMBER].Controls[0])).Text == "" ? 0 : long.Parse(((TextBox)(oGridRow.Cells[COL_CAR_NUMBER].Controls[0])).Text);
                _Peilut.iKisuyTor = (int)(dblKisuyTor);
                
                _Peilut.dFullShatYetzia = dShatYetiza;
                _Peilut.sShatYetzia = oShatYetiza.Text;              
                _Peilut.lMakatNesia = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[COL_MAKAT].Controls[0]).Text);
                _Peilut.iDakotBafoal = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[COL_ACTUAL_MINUTES].Controls[0]).Text);
                
                int iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[COL_CANCEL_PEILUT].Controls[0]).Text);
                _Peilut.iBitulOHosafa = ((iCancelSidur == 1) || (iCancelPeilut == 1)) ? 1  : _Peilut.iBitulOHosafa;
                
                _Peilut.iKisuyTorMap = oGridRow.Cells[_COL_KISUY_TOR_MAP].Text == string.Empty ? 0 : int.Parse(oGridRow.Cells[_COL_KISUY_TOR_MAP].Text);
 
                arrKnisaVal = oGridRow.Cells[COL_KNISA].Text.Split(",".ToCharArray());
                iMisparKnisa = int.Parse(arrKnisaVal[0]);
                _Peilut.iMisparKnisa = iMisparKnisa;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }   
    private void AddEmptyRowToPeilutGrid(int iSidurIndex)
    {
       // GridView _GridView;
        //DataTable dt = new DataTable();
        try
        {
            //Update DataTable which bind to grid with new data
          
            //_GridView = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
            //if (_GridView != null)
            //{
                //בניית עמודות הטבלה               
            OrderedDictionary hashSidurimPeiluyot = DataSource;
            UpdateHashTableWithGridChanges(ref hashSidurimPeiluyot);
            AddEmptyPeilutToHashTable(iSidurIndex); 
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private int GetSidurKey(int iSidurIndex, ref DateTime dSidurShatHatchala)
    {   GridView _GridView;
        int iSidurNumber=0;
        Label _Lbl;
        TextBox oTxt;
        HyperLink _HypLnk = new HyperLink();       
        if (_DataSource != null)
        {
            //נוציא את מספר הסידור
            _GridView = ((GridView)this.FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
            if (_GridView != null)
            {
                try
                {
                    _Lbl = (Label)this.FindControl("lblSidur" + iSidurIndex);
                }
                catch (Exception ex)
                {
                    _HypLnk = (HyperLink)this.FindControl("lblSidur" + iSidurIndex);
                    _Lbl = null;
                }
                iSidurNumber = (_Lbl == null ? int.Parse(_HypLnk.Text) : int.Parse(_Lbl.Text));
                oTxt = ((TextBox)(this.FindControl("txtSH" + iSidurIndex)));
                _Lbl = (Label)this.FindControl("lblDate" + iSidurIndex);
                dSidurShatHatchala = DateTime.Parse(_Lbl.Text + " " + oTxt.Text);                
            }
        }
        return iSidurNumber;
    }
    private void AddEmptyPeilutToHashTable(int iSidurIndex)
    {
        //הוספת פעילות ריקה ל- HASHTABLE
        clSidur _Sidur = new clSidur(); 
        clPeilut _Peilut = new clPeilut();               
        int iSidurNumber=0;
        DateTime dSidurShatHatchala= new DateTime();

        iSidurNumber = GetSidurKey(iSidurIndex, ref dSidurShatHatchala);        
        //for (int iIndex = 0; iIndex < _DataSource.Count; iIndex++)
        //{
        //    _Sidur = (clSidur)(_DataSource[iIndex]);
        //    if ((_Sidur.iMisparSidur.Equals(iSidurNumber)) && (_Sidur.dFullShatHatchala.Equals(dSidurShatHatchala)) && (_Sidur.iBitulOHosafa!= clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()))
        //            break;
        //}
         _Sidur = (clSidur)(_DataSource[iSidurIndex]);
         _Sidur.htPeilut.Add(_Sidur.htPeilut.Count+1, _Peilut);
         _Peilut.oPeilutStatus = clPeilut.enPeilutStatus.enNew;
         Session["Sidurim"] = _DataSource;
         
         ClearControl();
         BuildPage();
    }
    //private void GetPeilyotTnuaDetails(ref clPeilut _Peilut)
    //{
    //    string sCacheKey = MisparIshi + CardDate.ToShortDateString();
    //    clKavim _Kavim = new clKavim();
    //    DataTable _Peilyout;
    //    DataRow[] _PeilyotDetails;
    //    try
    //    {
    //        _Peilyout = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
    //        _PeilyotDetails = _Peilyout.Select("makat8=" + _Peilut.lMakatNesia.ToString());
    //        if (_PeilyotDetails.Length>0)
    //            _Peilut

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void UpdatePeilutDataTable(int iSidurIndex, ref DataTable dt, GridView _GridView)
    //{
    //    DataRow drPeilutyot;
    //    clSidur _Sidur;
    //    clPeilut _Peilut;
    //    DateTime dShatYetiza, dKisuyTor;
    //    String sShatYetiza, sKisuyTor, sDayToAdd;
    //    int iDayToAdd,iSidurNumber;
    //    double dblKisuyTor;
    //    TextBox oShatYetiza;
    //    GridViewRow oGridRow;
    //    clKavim _Kavim = new clKavim();
    //    DataRow[] drLicenseNumber;
    //    DateTime dNewSidurShatHatchala, dOldSidurShatHatchala;
    //    TextBox oTxt;
    //    Label oLbl;
    //    HyperLink oHypLnk = new HyperLink();
    //    try
    //    {
    //        try
    //        {
    //            oLbl = (Label)this.FindControl("lblSidur" + iSidurIndex);
    //        }
    //        catch (Exception ex)
    //        {
    //            oHypLnk = (HyperLink)this.FindControl("lblSidur" + iSidurIndex);
    //            oLbl = null;
    //        }
    //        iSidurNumber = (oLbl == null ? int.Parse(oHypLnk.Text) : int.Parse(oLbl.Text));
    //        oTxt = ((TextBox)(this.FindControl("txtSH" + iSidurIndex)));
    //        dOldSidurShatHatchala = DateTime.Parse(oTxt.Attributes["OrgShatHatchala"]);
    //        if (oTxt.Text == string.Empty)
    //            dNewSidurShatHatchala = DateTime.Parse("01/01/0001 00:00:00");
    //        else
    //        {//נבדוק אם השתנה התאריך
    //            dNewSidurShatHatchala = GetSidurNewDate(iSidurNumber, oTxt.Text); 
    //            dNewSidurShatHatchala = dNewSidurShatHatchala.AddSeconds(double.Parse(dOldSidurShatHatchala.Second.ToString().PadLeft(2, (char)48)));
    //        }

    //        _Sidur = (clSidur)(_DataSource[iSidurIndex]);
    //        for (int iRowIndex = 0; iRowIndex < _GridView.Rows.Count; iRowIndex++)
    //        {
    //            oGridRow = _GridView.Rows[iRowIndex];
    //            _Peilut = (clPeilut)_Sidur.htPeilut[iRowIndex];
    //            drPeilutyot = dt.NewRow();

    //            //הוספת נסיעה ריקה
    //            drPeilutyot["Add_Nesia_reka"] = "1";
    //            //מציין האם מותר לבטל פעילות
    //            drPeilutyot["cancel_peilut_flag"] = _Peilut.iMisparKnisa == 0 ? 1 : _Peilut.bKnisaNeeded ? 1 : 0;
    //            //כיסוי תור ושעת יציאה
    //            sDayToAdd = ((TextBox)oGridRow.Cells[COL_DAY_TO_ADD].Controls[0]).Text;
    //            iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);
    //            oShatYetiza = ((TextBox)oGridRow.Cells[COL_SHAT_YETIZA].Controls[0]);
    //            dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
    //            sShatYetiza = oShatYetiza.Text;
    //            if (dShatYetiza.Date.Year < clGeneral.cYearNull)
    //                if (oShatYetiza.Text != string.Empty)
    //                {
    //                    if (DateTime.Parse(oShatYetiza.Text).Hour > 0)
    //                        oShatYetiza.Attributes["OrgDate"] = dNewSidurShatHatchala.ToShortDateString();
    //                }
    //                else //שעת יציאה ריקה
    //                    sShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgShatYetiza"]).ToShortTimeString();

    //            dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"] + " " + sShatYetiza);

    //            if (dShatYetiza.Date == CardDate)
    //                dShatYetiza = dShatYetiza.AddDays(iDayToAdd);
    //            else
    //                if (dShatYetiza.Date.Year > clGeneral.cYearNull)
    //                {
    //                    if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
    //                        dShatYetiza = dShatYetiza.AddDays(-1);
    //                }
    //            sKisuyTor = ((TextBox)oGridRow.Cells[COL_KISUY_TOR].Controls[0]).Text;
    //            if (sKisuyTor != string.Empty)
    //            {
    //                dKisuyTor = DateTime.Parse(dShatYetiza.ToShortDateString() + " " + sKisuyTor);
    //                dblKisuyTor = (dShatYetiza - dKisuyTor).TotalMinutes;
    //                if (dblKisuyTor < 0)
    //                    dblKisuyTor = 1440 + dblKisuyTor;
    //            }
    //            else
    //                dblKisuyTor = 0;

    //            drPeilutyot["Kisuy_Tor"] = dblKisuyTor;
    //            drPeilutyot["Kisuy_Tor_map"] = _Peilut.iKisuyTorMap;
    //            drPeilutyot["shat_yetzia"] = dShatYetiza;
    //            drPeilutyot["oto_no"] = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[COL_CAR_NUMBER].Controls[0]).Text);
    //            drPeilutyot["makat_nesia"] = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[COL_MAKAT].Controls[0]).Text);
    //            drPeilutyot["dakot_bafoal"] = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[COL_ACTUAL_MINUTES].Controls[0]).Text);
    //            drPeilutyot["Makat_Description"] = oGridRow.Cells[_COL_LINE_DESCRIPTION].Text == NBSP ? "" : oGridRow.Cells[_COL_LINE_DESCRIPTION].Text;
    //            drPeilutyot["makat_shilut"] = oGridRow.Cells[_COL_LINE].Text == NBSP ? "" : oGridRow.Cells[_COL_LINE].Text;
    //            drPeilutyot["Shirut_type_Name"] = oGridRow.Cells[_COL_LINE_TYPE].Text == NBSP ? "" : oGridRow.Cells[_COL_LINE_TYPE].Text; 
    //            drPeilutyot["imut_netzer"] = oGridRow.Cells[_COL_NETZER].Text;
    //            drPeilutyot["makat_type"] = _Kavim.GetMakatType(long.Parse(drPeilutyot["makat_nesia"].ToString()));
    //            drPeilutyot["mazan_tichnun"] = oGridRow.Cells[_COL_DEF_MINUTES].Text;
    //            drPeilutyot["mazan_tashlum"] = oGridRow.Cells[_COL_MAZAN_TASHLUM].Text;
    //            drPeilutyot["knisa"] = oGridRow.Cells[_COL_KNISA].Text;
    //            drPeilutyot["last_update"] = oGridRow.Cells[_COL_LAST_UPDATE].Text;
    //            drPeilutyot["DayToAdd"] = ((TextBox)(oGridRow.Cells[_COL_DAY_TO_ADD].Controls[0])).Text;
    //            drPeilutyot["bitul_o_hosafa"] = oGridRow.Cells[_COL_CANCEL_PEILUT].Text;               
    //            drLicenseNumber = Mashar.Select("bus_number=" + drPeilutyot["oto_no"].ToString());
    //            if (drLicenseNumber.Length > 0)                
    //                drPeilutyot["license_number"] = long.Parse(drLicenseNumber[0]["license_number"].ToString());
                

    //            //drPeilutyot["PeilutStatus"] = e.Row.Cells[_COL_PEILUT_STATUS];   
    //            dt.Rows.Add(drPeilutyot);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex; //פעילויות            
    //    }
    //}
    //private void BindNewDataTableToGrid(ref DataTable dt, GridView _GridView)
    //{
    //    DataView dv = new DataView(dt);
    //    _GridView.DataSource = dv;
    //    _GridView.DataBind();
    //}
    //private void AddEmptyLineToDataTable(ref DataTable dt)
    //{
    //    DataRow drPeilutyot;

    //    drPeilutyot = dt.NewRow();

    //    //מציין האם מותר לבטל פעילות
    //    drPeilutyot["Add_Nesia_reka"] = "1";
    //    drPeilutyot["cancel_peilut_flag"] = 0;
    //    drPeilutyot["Kisuy_Tor"] = 0;
    //    drPeilutyot["Kisuy_Tor_map"] = 0;
    //    drPeilutyot["shat_yetzia"] = "00:00";
    //    drPeilutyot["Makat_Description"] = "";
    //    drPeilutyot["makat_shilut"] = "";
    //    drPeilutyot["Shirut_type_Name"] = "";
    //    drPeilutyot["oto_no"] = 0;
    //    drPeilutyot["makat_nesia"] = 0;
    //    drPeilutyot["dakot_bafoal"] = 0;
    //    drPeilutyot["imut_netzer"] = "לא";
    //    drPeilutyot["makat_type"] = 0;
    //    drPeilutyot["mazan_tichnun"] = 0;
    //    drPeilutyot["last_update"] = "";
    //    drPeilutyot["mazan_tashlum"] = 0;
    //    drPeilutyot["bitul_o_hosafa"] = 0;
    //    drPeilutyot["knisa"] = "0,0,0,0";
    //    drPeilutyot["DayToAdd"] = "0";
    //    drPeilutyot["license_number"] = 0;
    //    drPeilutyot["PeilutStatus"] = 0;// lstSidurim.enPeilutStatus.enValid.GetHashCode();
    //    dt.Rows.Add(drPeilutyot);
    //}
    private DateTime GetSidurNewDate(int iSidurKey, string sSidurHour)
    {
        //מחזיר את  התארי המעודכן של הסידור
        //DataTable dtUpdateSidurim = (DataTable)Session["SidurimUpdated"];
        DataRow[] dr;
        DateTime dtDate = new DateTime();
        dr = ((DataTable)Session["SidurimUpdated"]).Select("sidur_number=" + iSidurKey + " and sidur_start_hour='" + sSidurHour + "'");
        if (dr.Length > 0)
            dtDate = DateTime.Parse(dr[0]["sidur_date"].ToString());


        return dtDate;
    }
    protected DropDownList BuildHashlamaDLL(bool bEnabled)
    {
               
        DropDownList ddl = new DropDownList();
        ListItem item = new ListItem();
        item.Text = "אין השלמה";
        item.Value = "0";
        ddl.Items.Add(item);

        item = new ListItem();
        item.Text = "השלמה לשעה";
        item.Value = "1";
        ddl.Items.Add(item);

        if ((ProfileRashemet) || ((MisparIshi) != LoginUserId) || (!bEnabled)){        
            item = new ListItem();
            item.Text = "השלמה לשעתיים";
            item.Value = "2";
            ddl.Items.Add(item);
        }
        return ddl;
    }
    protected void CreateHashlamaCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive, DataRow[] drSugSidur)
    {
        bool bEnabled;
        string sErrorMsg, sClientScriptFunction, sID;
        Image imgErr = new Image();
        imgErr.ID = "imgHash" + iIndex;
 
        CustomValidator vldHashlamaNumber;
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut;

        hCell = CreateTableCell("113px", "", "");
        bEnabled = ((IsHashlamaAllowed(ref oSidur, drSugSidur))  && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "HASHLAMA"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));

        DropDownList ddl;
        
        ddl = BuildHashlamaDLL(bEnabled);
        ddl.ID = "ddlHashlama" + iIndex;
        ddl.SelectedValue = oSidur.sHashlama;
        ddl.Style.Add("width", "82px");
        ddl.Enabled = ((bEnabled) && (bSidurActive));
        ddl.Attributes.Add("OrgEnabled", ddl.Enabled ? "1" : "0");
        ddl.Attributes.Add("onchange", "SetBtnChanges();SetLvlChg(2,"+iIndex+");");
        ddl.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
        //AddAttribute(ddl, "OldV", ddl.SelectedValue);

        //string sAllApprovalDescription = "";
        //DataRow[] dr = dtApprovals.Select("mafne_lesade='hashlama'");
        //if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription))
        //{
        //    Image imgApp;
        //    imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgAHashlama" + iIndex, "GetAppMsg(this)", "closePanel(" + iIndex + ");", sAllApprovalDescription);
        //    ddl.Attributes.Add("class", "ApprovalField");
        //    ddl.Enabled = false;
        //    hCell.Controls.Add(imgApp);
        //}
        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                if (SetOneError(ddl, hCell, "Hashlama", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ClientID))
                {
                    imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                    imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                    imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + ddl.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");
                    hCell.Controls.Add(imgErr);
                }
                else
                    SetIsurim(ddl, iIndex, "hashlama", "imgAHashlama", ref oSidur, ref hCell);
                break;
            case clGeneral.enCardStatus.Valid:
                SetIsurim(ddl, iIndex, "hashlama", "imgAHashlama", ref oSidur, ref hCell);
                //string sAllApprovalDescription = "";
                //DataRow[] dr = dtApprovals.Select("mafne_lesade='hashlama'");
                //if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription))
                //{
                //    Image imgApp;
                //    imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgAHashlama" + iIndex, "GetAppMsg(this)", "closePanel(" + iIndex + ");", sAllApprovalDescription);
                //    ddl.Attributes.Add("class", "ApprovalField");
                //    ddl.Enabled = false;
                //    hCell.Controls.Add(imgApp);
                //}
                break;
        }                
        
       
        sErrorMsg = "משך הסידור שווה או גדול מזמן ההשלמה הנבחר ";
        sClientScriptFunction = "chkHashlama";
        sID = "vldHashlama" + iIndex;
        vldHashlamaNumber = AddCustomValidator(ddl.ID, sErrorMsg, sID, sClientScriptFunction,"", iIndex.ToString());
        vldExtenderCallOut = AddCallOutValidator(sID, "vldCallOutHashlama" + iIndex, "", AjaxControlToolkit.ValidatorCalloutPosition.Right);
        hCell.Controls.Add(ddl);
        hCell.Controls.Add(vldHashlamaNumber);
        hCell.Controls.Add(vldExtenderCallOut);
        hCell.Style.Add("border-left", "solid 1px gray");
        clUtils.BindTooltip(ddl);
        clUtils.SetDDLToolTip(ddl);
      
    }

    private void SetIsurim(DropDownList ddl, int iIndex, string sFieldName, string oImagName, ref clSidur oSidur, ref HtmlTableCell hCell)
    {
        bool bEnableApproval=false;
        string sAllApprovalDescription = "";      
        DataRow[] dr = dtApprovals.Select("mafne_lesade='" + sFieldName + "'");
        if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription, ref bEnableApproval))
        {
            Image imgApp;
            imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", oImagName + iIndex, "GetAppMsg(this)", "MovePanel(" + iIndex + ");", sAllApprovalDescription);
            ddl.Attributes.Add("class", "ApprovalField");
            ddl.Enabled = bEnableApproval;
            hCell.Controls.Add(imgApp);
        }
    }
    //protected void CreateHamaraCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive, DataRow[] drSugSidur)
    //{
    //    bool bErrorExists, bEnabled;
    //    string sErrorMsg;

    //    hCell = CreateTableCell("28px", "", "");
    //    CheckBox chkBox = new CheckBox();
    //    chkBox.ID = "chkHamara" + iIndex;
    //    chkBox.Checked = (oSidur.sHamaratShabat == "1");
    //    bEnabled = IsHamaraAllowed(ref oSidur, drSugSidur);
    //    bErrorExists = clWorkCard.IsErrorExists(ErrorsList, clBatchManager.enErrors.errHamaratShabatNotValid.GetHashCode(), oSidur.iMisparIshi, DateTime.Parse(oSidur.sSidurDate), oSidur.iMisparSidur, oSidur.sShatHatchala, out sErrorMsg);
    //    if (bErrorExists)
    //    {
    //        chkBox.Attributes.Add("style", "background-color: red ");
    //        chkBox.Attributes.Add("Err", sErrorMsg);
    //        chkBox.Attributes.Add("ondblclick", "GetErrorMessage(this)"); 
    //    }
    //    chkBox.Enabled = ((bEnabled) && (bSidurActive));
    //    chkBox.Attributes.Add("OrgEnabled", bEnabled.GetHashCode().ToString());
    //    chkBox.Attributes.Add("onclick", "SetBtnChanges();");
    //    chkBox.Attributes.Add("onclick", "closePanel(" + iIndex + ");");
    //    hCell.Controls.Add(chkBox);
    //    hCell.Style.Add("border-left", "solid 1px #3F3F3F");
    //}
    protected void CreateDDLPitzulHafsakaCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive, ref OrderedDictionary htEmployeeDetails)
    {
        bool bEnablePitzul;
        bool bSidurMychadShaon = false;
        Image imgErr = new Image();
        int iMikumShaonKnisa = String.IsNullOrEmpty(oSidur.sMikumShaonKnisa) ? 0 : int.Parse(oSidur.sMikumShaonKnisa);
        int iMikumShaonYetiza = String.IsNullOrEmpty(oSidur.sMikumShaonYetzia) ? 0 : int.Parse(oSidur.sMikumShaonYetzia);

        hCell = CreateTableCell("103px", "", "");
        bEnablePitzul = IsPitzulHafsakaAllowed(ref oSidur,ref bSidurMychadShaon);

        DropDownList ddl = new DropDownList();
        ddl.ID = "ddlPHfsaka" + iIndex;
        ddl.DataTextField = "teur";
        ddl.DataValueField = "kod";
        if ((bSidurMychadShaon) && (iMikumShaonKnisa > 0) && (iMikumShaonYetiza > 0) && (!MeafyenyOved.Meafyen41Exists))
        {
            _dvPitzulHafsaka.RowFilter = string.Concat("table_name='", clGeneral.cCtbPitzulaHafsaka, "'", " and (kod='", clGeneral.enShowPitzul.enLoZakaiLepitzul.GetHashCode(), "' or kod='", clGeneral.enShowPitzul.enOvedHafsaka.GetHashCode(), "')");
            ddl.DataSource = _dvPitzulHafsaka;
        }
        else
        {
            _dvPitzulHafsaka.RowFilter = string.Concat("table_name='", clGeneral.cCtbPitzulaHafsaka, "'");
            ddl.DataSource = _dvPitzulHafsaka;
        }


        ddl.DataBind();
        ddl.SelectedValue = oSidur.sPitzulHafsaka;        
        ddl.Style.Add("width", "95px");       
        ddl.Attributes.Add("onchange", "SetBtnChanges();SetLvlChg(2,"+iIndex+"); chkPitzulHafsaka(" + iIndex + ",false)");
        ddl.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");        
        //AddAttribute(ddl, "OldV",ddl.SelectedValue);

        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                if (SetOneError(ddl, hCell, "Pitzul_hafsaka", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ClientID))
                {
                    imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                    imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                    imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + ddl.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");
                    hCell.Controls.Add(imgErr);
                }
                break;
        }

        bool bOrgEnable = ((bEnablePitzul) && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "PITZUL_HAFSAKA"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
        ddl.Attributes.Add("OrgEnabled", bOrgEnable ? "1" : "0");
        ddl.Enabled = ((bSidurActive) && (bOrgEnable));
       
        hCell.Controls.Add(ddl);
        hCell.Style.Add("border-left", "solid 1px gray");
        clUtils.BindTooltip(ddl);
        clUtils.SetDDLToolTip(ddl);
    }
    protected void CreateDDLExceptionCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {
        string sCharigaType = "0";
        bool bEnabled;
        Image imgErr = new Image();
        imgErr.ID = "imgCh" + iIndex;
        hCell = CreateTableCell("115px", "", "");
        DropDownList ddl = new DropDownList();
        ddl.ID = "ddlException" + iIndex;           
        ddl.DataTextField = "teur";
        ddl.DataValueField = "kod";
        ddl.DataSource = _dvChariga;            
        ddl.DataBind();
        ddl.SelectedValue = oSidur.sChariga;
        ddl.Style.Add("width", "85px");
        
        bEnabled = IsExceptionAllowed(ref oSidur, ref sCharigaType);
        ddl.Attributes.Add("ChrigaType", sCharigaType);
        ddl.Enabled = ((bEnabled) && (bSidurActive) && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "CHARIGA"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
        ddl.Attributes.Add("OrgEnabled", ddl.Enabled ? "1":"0");
        ddl.Attributes.Add("onchange", "SetBtnChanges();SetLvlChg(2,"+iIndex+");ChkCharigaVal(" + iIndex  + ");");
        ddl.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");

       // AddAttribute(ddl, "OldV", ddl.SelectedValue);

        
        //string sAllApprovalDescription = "";
        //DataRow[] dr = dtApprovals.Select("mafne_lesade='Chariga'");
        //if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription))
        //{
        //    Image imgApp;
        //    imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgAChariga" + iIndex, "GetAppMsg(this)", "closePanel(" + iIndex + ");", sAllApprovalDescription);
        //    //ddl.Attributes.Add("class", "ApprovalField");
        //    ddl.Enabled = false;

        //    //ddl.Attributes.Add("class", "ApprovalField",);
        //    //imgApp = AddImage("../../Images/ApprovalSign.jpg", "imgAChariga", "");
        //    //imgApp.Attributes.Add("ondblclick", "GetAppMsg(this)");
        //    //imgApp.Attributes.Add("onClick", "closePanel(" + iIndex + ");");
        //    //imgApp.Attributes.Add("App", sAllApprovalDescription);
        //    hCell.Controls.Add(imgApp);
        //}
        //נבדוק אם יש שגיאות על שדה חריגה
        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                if (SetOneError(ddl, hCell, "Chariga", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ClientID))
                {
                    imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                    imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                    imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + ddl.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");
                    hCell.Controls.Add(imgErr);
                }
                else
                    //נבדוק אם יש אישורים על שדה חריגה
                    SetIsurim(ddl, iIndex, "Chariga", "imgAChariga", ref oSidur, ref hCell);
                break;
            case clGeneral.enCardStatus.Valid:
                {
                    //נבדוק אם יש אישורים על שדה חריגה
                    SetIsurim(ddl, iIndex, "Chariga", "imgAChariga", ref oSidur, ref hCell);
                    break;
                }
        }        
        hCell.Controls.Add(ddl);
        hCell.Style.Add("border-left", "solid 1px gray");
        clUtils.BindTooltip(ddl);
        clUtils.SetDDLToolTip(ddl);
    }

    protected Image CreateImgForApp(string sImgUrl, 
                                    string sId, string sOnDblClickFunction, string sOnClickFunction,
                                    string sAllApprovalDescription)
    {
        Image imgApp;
        
        imgApp = AddImage(sImgUrl, sId, "");
        imgApp.Attributes.Add("ondblclick", sOnDblClickFunction);
        imgApp.Attributes.Add("onClick", sOnClickFunction);
        imgApp.Attributes.Add("App", sAllApprovalDescription);
        return imgApp;
    }

    protected void CreateShatGmarLetashlumCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {
        bool bIdkunRashemet;
        bool bOrgEnable;
        AjaxControlToolkit.MaskedEditExtender oMaskedEditExtender;
        AjaxControlToolkit.ValidatorCalloutExtender vldExShatGmarLetashlum;
        CustomValidator vldShatGmarLetashlum;
        hCell = CreateTableCell("70px", "", "");
       
        TextBox oTextBox = new TextBox();
        oTextBox.ID = "txtSGL" + iIndex;
        oTextBox.Text = oSidur.sShatGmarLetashlum;       
        bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "SHAT_GMAR_LETASHLUM"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0);
        bOrgEnable = ((IsSidurShaon(ref oSidur)) && (!bIdkunRashemet));
        oTextBox.Enabled = ((bSidurActive) && (bOrgEnable));
        oTextBox.Width = Unit.Pixel(40);
       // oTextBox.Height = Unit.Pixel(20);
        oTextBox.CausesValidation = true;
        oTextBox.MaxLength = MAX_LEN_HOUR;
        oTextBox.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
        oTextBox.Attributes.Add("onkeypress", "SetBtnChanges();SetLvlChg(2,"+iIndex+");");
        oTextBox.Attributes.Add("onblur", "SidurTimeChanged(" + iIndex + ");");
        oTextBox.Attributes.Add("OrgEnabled", bOrgEnable ? "1" : "0");
        //AddAttribute(oTextBox, "OldV", oTextBox.Text);
        hCell.Controls.Add(oTextBox);
        oMaskedEditExtender = AddTimeMaskedEditExtender(oTextBox.ID, iIndex, "99:99", "SGPMask", AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Left);
        hCell.Controls.Add(oMaskedEditExtender);
        vldShatGmarLetashlum = AddCustomValidator(oTextBox.ID, "", "vldSGL" + iIndex, "ISSHLValid", "");
        vldExShatGmarLetashlum = AddCallOutValidator(vldShatGmarLetashlum.ID, "vldExSGL" + iIndex, "", AjaxControlToolkit.ValidatorCalloutPosition.Left);
        hCell.Controls.Add(vldShatGmarLetashlum);
        hCell.Controls.Add(vldExShatGmarLetashlum);

        hCell.Style.Add("border-left", "solid 1px gray");
        
        DataRow[] dr = dtApprovals.Select("mafne_lesade='Shat_Gmar_Letashlum'");
        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                if (!SetOneError(oTextBox, hCell, "Shat_Gmar_Letashlum", ref oSidur, iIndex.ToString(), ""))
                {
                    //אם לא נמצאה שגיאה נבדוק אישורים
                    CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                }
                break;
            case clGeneral.enCardStatus.Valid:
                {
                    CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                    break;
                }
        }
    }
    protected void CreateShatHatchalaLetashlumCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {
        bool bIdkunRashemet;
        bool bOrgEnabled;
        AjaxControlToolkit.MaskedEditExtender oMaskedEditExtender;
        AjaxControlToolkit.ValidatorCalloutExtender vldExShatHatchalaLetashlum;
        CustomValidator vldShatHatchalaLetashlum;
        hCell = CreateTableCell("73px", "", "");
       
        TextBox oTextBox = new TextBox();
        oTextBox.ID = "txtSHL" + iIndex;
        oTextBox.Text = oSidur.sShatHatchalaLetashlum;
        oTextBox.Width = Unit.Pixel(37);
       // oTextBox.Height = Unit.Pixel(20);
        oTextBox.CausesValidation = true;
        bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "SHAT_HATCHALA_LETASHLUM"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0);
        bOrgEnabled=((IsSidurShaon(ref oSidur)) && (!bIdkunRashemet));
        oTextBox.Enabled = ((bSidurActive) && (bOrgEnabled));
        oTextBox.MaxLength = MAX_LEN_HOUR;
        oTextBox.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
        oTextBox.Attributes.Add("onkeypress", "SetBtnChanges();SetLvlChg(2,"+iIndex+");");
        oTextBox.Attributes.Add("onblur", "SidurTimeChanged(" + iIndex + ");");
        oTextBox.Attributes.Add("OrgEnabled", bOrgEnabled ? "1" : "0");

        //AddAttribute(oTextBox, "OldV", oTextBox.Text);
        hCell.Controls.Add(oTextBox);
        oMaskedEditExtender = AddTimeMaskedEditExtender(oTextBox.ID, iIndex, "99:99", "SHPMask", AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Left);
        hCell.Controls.Add(oMaskedEditExtender);
       
        vldShatHatchalaLetashlum = AddCustomValidator(oTextBox.ID, "", "vldSHL" + iIndex, "ISSHLValid", "");
        vldExShatHatchalaLetashlum = AddCallOutValidator(vldShatHatchalaLetashlum.ID, "vldExSHL" + iIndex, "", AjaxControlToolkit.ValidatorCalloutPosition.Left);
        hCell.Controls.Add(vldShatHatchalaLetashlum);
        hCell.Controls.Add(vldExShatHatchalaLetashlum);

        hCell.Style.Add("border-left", "solid 1px gray");

        DataRow[] dr = dtApprovals.Select("mafne_lesade='Shat_Hatchala_Letashlum'");
        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                if (!SetOneError(oTextBox, hCell, "Shat_Hatchala_Letashlum", ref oSidur, iIndex.ToString(), ""))
                {
                    //אם לא נמצאה שגיאה נבדוק אישורים
                    CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                }
                break;
            case clGeneral.enCardStatus.Valid:
                {
                    CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                    break;
                }
        }
    }
    protected void CreateDDLResonOutCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {
        bool OrgEnable;
        hCell = CreateTableCell("100px", "", "");
        DropDownList ddl = new DropDownList();
        ListItem Item = new ListItem();
        Image imgErr = new Image();
        imgErr.ID = "imgSibaOut" + iIndex;
        ddl.ID = "ddlResonOut" + iIndex;
        ddl.DataTextField = "teur_siba";
        ddl.DataValueField = "kod_siba";
        ddl.DataSource = _dvSibotLedivuch;
        ddl.DataBind();
        Item.Text = "";
        Item.Value = "-1";
        ddl.Items.Insert(0, Item);

        ddl.SelectedValue = oSidur.iKodSibaLedivuchYadaniOut.ToString();
        ddl.Style.Add("width", "75px");        
        ddl.Attributes.Add("onchange", "SetBtnChanges();SetLvlChg(2,"+iIndex+");");
        ddl.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
        OrgEnable = ((IsSidurShaon(ref oSidur)) && (IsMikumShaonEmpty(oSidur.sMikumShaonYetzia)) && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_OUT"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
        ddl.Enabled = ((bSidurActive) && (OrgEnable));
        ddl.Attributes.Add("OrgEnabled", OrgEnable ? "1" : "0");
        //AddAttribute(ddl, "OldV", ddl.SelectedValue);
        
      
        //    string sAllApprovalDescription = "";
        //    DataRow[] dr = dtApprovals.Select("mafne_lesade='KOD_SIBA_LEDIVUCH_YADANI_OUT'");
        //    if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription))
        //    {
        //        Image imgApp;
        //        imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgASibaOut" + iIndex, "GetAppMsg(this)", "closePanel(" + iIndex + ");", sAllApprovalDescription);
        //        ddl.Attributes.Add("class", "ApprovalField");
        //        ddl.Enabled = false;
        //        hCell.Controls.Add(imgApp);

        //    }
        ////נבדוק אם יש שגיאות על שדה כניסה
        switch (_StatusCard)
        {
            case clGeneral.enCardStatus.Error:
                if (SetOneError(ddl, hCell, "KOD_SIBA_LEDIVUCH_YADANI_OUT", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ClientID))
                {
                    imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                    imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                    imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + ddl.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");
                    hCell.Controls.Add(imgErr);
                }
                else
                    //נבדוק אם יש אישורים על שדה כניסה
                    SetIsurim(ddl, iIndex, "KOD_SIBA_LEDIVUCH_YADANI_OUT", "imgASibaOut", ref oSidur, ref hCell);
                break;
            case clGeneral.enCardStatus.Valid:
                {
                    //נבדוק אם יש אישורים על שדה כניסה
                    SetIsurim(ddl, iIndex, "KOD_SIBA_LEDIVUCH_YADANI_OUT", "imgASibaOut", ref oSidur, ref hCell);
                    break;
                }
        }
        hCell.Controls.Add(ddl);
        hCell.Style.Add("border-left", "solid 1px gray");
        clUtils.BindTooltip(ddl);
        clUtils.SetDDLToolTip(ddl);
    }
    private void CreateDDLResonInCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {       
        hCell = CreateTableCell("91px", "", "");
        DropDownList ddl = new DropDownList();
        ListItem Item = new ListItem();
        Image imgErr = new Image();
        bool bOrgEnable;
        imgErr.ID = "imgSibaIn" + iIndex;
        try
        {
            ddl.ID = "ddlResonIn" + iIndex;
            ddl.DataTextField = "teur_siba";
            ddl.DataValueField = "kod_siba";
            ddl.DataSource = _dvSibotLedivuch;
            ddl.DataBind();
            Item.Text = "";
            Item.Value = "-1";
            ddl.Items.Insert(0, Item);
            ddl.SelectedValue = (IsSidurShaon(ref oSidur)) ? oSidur.iKodSibaLedivuchYadaniIn.ToString() : "-1";
            
            ddl.Style.Add("width", "78px");           
            ddl.Attributes.Add("ToolTip", ddl.SelectedValue);
            ddl.Attributes.Add("onchange", "SetBtnChanges();SetLvlChg(2," + iIndex + ");");
            ddl.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
            bOrgEnable = ((IsSidurShaon(ref oSidur)) && (IsMikumShaonEmpty(oSidur.sMikumShaonKnisa)) && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_IN"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
            ddl.Enabled = ((bSidurActive) && (bOrgEnable));
            ddl.Attributes.Add("OrgEnabled", bOrgEnable ? "1" : "0");
            // AddAttribute(ddl, "OldV", ddl.SelectedValue);
           
            ////נבדוק אם יש אישורים על שדה כניסה
            //string sAllApprovalDescription = "";
            //DataRow[] dr = dtApprovals.Select("mafne_lesade='KOD_SIBA_LEDIVUCH_YADANI_IN'");
            //if (CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref sAllApprovalDescription))
            //{
            //    Image imgApp;
            //    imgApp = CreateImgForApp("../../Images/ApprovalSign.jpg", "imgASibaIn" + iIndex, "GetAppMsg(this)", "closePanel(" + iIndex + ");", sAllApprovalDescription);
            //    ddl.Attributes.Add("class", "ApprovalField");
            //    ddl.Enabled = false;                       
            //    hCell.Controls.Add(imgApp);
            //}
            //נבדוק אם יש שגיאות על שדה כניסה
            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Error:
                    if (SetOneError(ddl, hCell, "KOD_SIBA_LEDIVUCH_YADANI_IN", ref oSidur, iIndex.ToString(), "lstSidurim_" + imgErr.ClientID))
                    {
                        imgErr.ImageUrl = "../../Images/ErrorSign.jpg";
                        imgErr.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
                        imgErr.Attributes.Add("ondblClick", "GetErrorMessage(lstSidurim_" + ddl.ClientID + "," + 2 + ",'" + iIndex.ToString() + "')");
                        hCell.Controls.Add(imgErr);
                    }
                    else
                        //נבדוק אם יש אישורים על שדה כניסה
                        SetIsurim(ddl, iIndex, "KOD_SIBA_LEDIVUCH_YADANI_IN", "imgASibaIn", ref oSidur, ref hCell);                    
                    break;
                case clGeneral.enCardStatus.Valid:
                    {
                        //נבדוק אם יש אישורים על שדה כניסה
                        SetIsurim(ddl, iIndex, "KOD_SIBA_LEDIVUCH_YADANI_IN", "imgASibaIn", ref oSidur, ref hCell);
                        break;
                    }
            }    
            hCell.Controls.Add(ddl);
            hCell.Style.Add("border-left", "solid 1px gray");
            clUtils.BindTooltip(ddl);
            clUtils.SetDDLToolTip(ddl);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool IsLoLetashlum(ref clSidur oSidur)
    {
        bool bValid;

        if  (((oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkAtFridayWithoutPremission.GetHashCode())
               || (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkAtSaturdayWithoutPremission.GetHashCode())
               || (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.SidurInNonePremissionHours.GetHashCode()))
                && (oSidur.sChariga.Equals("0")))
        {
            bValid = false;
        }
        else
        {
             bValid = true;
        }
        return bValid;
    }
    private bool IsAccessToSidurNotShaon(ref clSidur oSidur)
    {
        //אפשר שינוי שעת התחלה/גמר של סידורים שאינם נוכחות רק לרשמת/רשמת על/מנהל מע
        bool bEnable = false;

        if ((IsSidurShaon(ref oSidur)) || (IsSidurMeafyen27(ref oSidur)) || (IsSidurHeadrut(ref oSidur)))        
            bEnable = true;        
        else        
            bEnable = ProfileRashemet;


        return bEnable;
    }
    private bool IsSidurHeadrut(ref clSidur oSidur)
    {
        return ((oSidur.bSidurMyuhad) && (oSidur.bHeadrutTypeKodExists));
    }
    private bool IsSidurMeafyen27(ref clSidur oSidur)
    {      
       return ((oSidur.bSidurMyuhad) && (oSidur.bNitanLedaveachAdTaarichExists));
    }
    private bool IsSidurShaon(ref clSidur oSidur)
    {
        return ((oSidur.bSidurMyuhad) && (oSidur.bShaonNochachutExists));
    }
    private bool IsMikumShaonEmpty(string sMikumShaon)
    {
        return ((String.IsNullOrEmpty(sMikumShaon)) || (sMikumShaon.Equals("0")));
    }
    private void SetSidurParticipate(ref clSidur oSidur)
    {
        try
        {
            clGeneral.enHityazvut _Participate;

            _Participate = (clGeneral.enHityazvut)oSidur.iNidreshetHitiatzvut;
            switch (_Participate)
            {
                case clGeneral.enHityazvut.enFirstHityatzvut:
                    FirstParticipate = oSidur;
                    break;
                case clGeneral.enHityazvut.enSecondHityatzvut:
                    SecondParticipate = oSidur;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex; 
        }
    }
  
    private void FillUpdateSidurimTable(ref clSidur oSidur, DataRow[] drSugSidur){
        DataRow drSidur;
        //מבנה שיכיל בסוף את התאריך המועדכן של הסידור
        drSidur = dtUpdatedSidurim.NewRow();
        drSidur["sidur_number"] = oSidur.iMisparSidur;
        drSidur["sidur_org_start_hour"] = oSidur.dFullShatHatchala.ToShortTimeString();
        drSidur["sidur_start_hour"] = oSidur.dFullShatHatchala.ToShortTimeString();
        drSidur["sidur_date"] = oSidur.dFullShatHatchala;
        drSidur["sidur_nahagut"] = IsSidurNahagut(ref oSidur, drSugSidur).GetHashCode();
        drSidur["sidur_nihul_tnua"] = IsSidurNihul(ref oSidur, drSugSidur).GetHashCode();
        dtUpdatedSidurim.Rows.Add(drSidur);
       
    }
    private HtmlTable BuildOneSidur(ref OrderedDictionary htEmployeeDetails, clSidur oSidur, int iIndex, ref bool bEnableSidur)
    {
        HtmlTable hTable = new HtmlTable();
        HtmlTableRow hRow = new HtmlTableRow();
        HtmlTableCell hCell = new HtmlTableCell();            
        DataRow[] drSugSidur;
        bool bSidurNahagutOrTnua;
        try
        {
            //נשלוף את מאפייני סוג הסידור ( סידורים רגילים(
            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, CardDate, SugeySidur);
            SetSidurNahagutStatus(drSugSidur, oSidur);            
            //התייצבות
            SetSidurParticipate(ref oSidur);
            //יצירת טבלת סידורים
            hTable.ID = "tblSidurim" + iIndex;                        
            hTable.CellPadding = 1;
            hTable.CellSpacing = 0;               
            hTable.Style.Add("height", "20px");
          
            bSidurContinue = ((oSidur.iMisparSidur == SIDUR_CONTINUE_NAHAGUT) || (oSidur.iMisparSidur == SIDUR_CONTINUE_NOT_NAHAGUT));
                                                      
            hTable.Rows.Add(hRow);

            //אם לסידור אין מאפיין 99 והסידור הוא ללא התייחסות, לא נאפשר  את עידכון הסידור
            bEnableSidur = IsEnableSidur(ref oSidur, drSugSidur);  

            bSidurNahagutOrTnua=(IsSidurNahagut(ref oSidur, drSugSidur) || (IsSidurNihul(ref oSidur, drSugSidur)));//IsSidurNahagutOrTnua(ref oSidur, drSugSidur);
            //נבדוק אם אחד מהסידורים הוא סידור נהגות או ניהול תנועה
            if (!bAtLeatOneSidurIsNOTNahagutOrTnua){
                bAtLeatOneSidurIsNOTNahagutOrTnua = (!bSidurNahagutOrTnua);
            }

            if ((oSidur.sErevShishiChag.Equals("1")) || (oSidur.sSidurDay.Equals(clGeneral.enDay.Shishi.GetHashCode().ToString())))
            {
                if (!bAtLeastOneSidurInShabat){                
                   bAtLeastOneSidurInShabat = IsSidurInShabat(ref oSidur);
                }
            }
           
            bool bSidurActive = ((oSidur.iBitulOHosafa != clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) && (oSidur.iBitulOHosafa != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()));
            
            //נמלא את הDATATABLE שתכיל בסוף את הנתונים העדכניים
            if ((!Page.IsPostBack) || (RefreshBtn.Equals(1)))
                FillUpdateSidurimTable(ref oSidur, drSugSidur);
            
            //הוספת פעילות            
            CreateAddPeilutCell(oSidur, ref hCell, iIndex, bEnableSidur);
            hTable.Rows[0].Cells.Add(hCell);

            //מספר סידור    
            CreateSidurCell(oSidur, ref hCell, iIndex);          
            hTable.Rows[0].Cells.Add(hCell);
            
            //שעת התחלה
            CreateShatHatchalaCell(oSidur, ref hCell, iIndex, bSidurActive);
            hTable.Rows[0].Cells.Add(hCell);

            //שעת גמר
            CreateShatGmarCell(oSidur, ref hCell, iIndex, drSugSidur, bSidurActive);          
            hTable.Rows[0].Cells.Add(hCell);


            if (bSidurContinue)
            {   //סידור רציפות
                CreateSidurNahagutCell(ref hCell, iIndex);
                hTable.Rows[0].Cells.Add(hCell);
            }
            else
            {
                //סיבת אי החתמת כניסה                
                CreateDDLResonInCell(oSidur, ref hCell, iIndex, bSidurActive);               
                hTable.Rows[0].Cells.Add(hCell);

                CreateDDLResonOutCell(oSidur, ref hCell, iIndex, bSidurActive);
                //סיבת אי החתמת יציאה               
                hTable.Rows[0].Cells.Add(hCell);

                //שעת התחלה לתשלום
                CreateShatHatchalaLetashlumCell(oSidur, ref hCell, iIndex, bSidurActive);             
                hTable.Rows[0].Cells.Add(hCell);

                //שעת גמר לתשלום
                CreateShatGmarLetashlumCell(oSidur, ref hCell, iIndex, bSidurActive);             
                hTable.Rows[0].Cells.Add(hCell);

                //חריגה
                CreateDDLExceptionCell(oSidur, ref hCell, iIndex, bSidurActive);                
                hTable.Rows[0].Cells.Add(hCell);

                //פיצול הפסקה
                CreateDDLPitzulHafsakaCell(oSidur, ref hCell, iIndex, bSidurActive, ref htEmployeeDetails);               
                hTable.Rows[0].Cells.Add(hCell);

                //קומבו השלמה
                CreateHashlamaCell(oSidur, ref hCell, iIndex, bSidurActive, drSugSidur);                
                hTable.Rows[0].Cells.Add(hCell);

                //מחוץ למכסה
                CreateOutMichsaCell(oSidur, ref hCell, iIndex, bSidurActive);
                hTable.Rows[0].Cells.Add(hCell);

                //לא לתשלום
                CreateLoLetashlumCell(oSidur, ref hCell, iIndex, bSidurActive);               
                hTable.Rows[0].Cells.Add(hCell);

                //פעיל
                CreateCancelCell(oSidur, ref hCell, iIndex, bSidurActive, bEnableSidur);
                hTable.Rows[0].Cells.Add(hCell);
             }       
                //עמודות נסתרות
                //שעת התחלה מותרת לסידור, ערך מאפיין 7     
                CreateShatHatchalaMutereHiddentCell(oSidur, ref hCell, iIndex);               
                hTable.Rows[0].Cells.Add(hCell);

                CreateShatGmarMutereHiddentCell(oSidur, ref hCell, iIndex);
                hTable.Rows[0].Cells.Add(hCell);

                //תאריך
                CreateSidurDateHiddenCell(oSidur, ref hCell, iIndex);
                hTable.Rows[0].Cells.Add(hCell);

                //מציין אם סידור נהגות
                CreateNahagutHiddenCell(oSidur, ref hCell, iIndex);                
                hTable.Rows[0].Cells.Add(hCell);

                //מציין אם בוטלה רשומה
                CreateCancelFlagHiddenCell(oSidur, ref hCell, iIndex);               
                hTable.Rows[0].Cells.Add(hCell);

                //מספר ימים להוספה 0לשעת הגמר  יום נוכחי או 1 יום הבא
                CreateAddDayToShatGmarHiddenCell(ref hCell, iIndex, oSidur);
                hTable.Rows[0].Cells.Add(hCell);
           
                //אם הכרטיס הוא ללא התייחסות וגם המספר של הגורם המטפל בכרטיס הוא לא בעל הכרטיס הנוכחי והסידור הוא ללא מאפיין 99, נחסום את הסידור לעריכה
                if (!bEnableSidur)
                    hTable.Disabled = true;
                
                
            return hTable;
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool IsEnableSidur(ref clSidur oSidur, DataRow[] drSugSidur)
    {
        bool bEnable = true;
        bool bRashaiLedavech=false;

        if (oSidur.bSidurMyuhad) //סידור מיוחד
            bRashaiLedavech = oSidur.bRashaiLedaveachExists;
        else //סידור מפה
            if (drSugSidur.Length > 0)
            {
                bRashaiLedavech = true;
                bRashaiLedavech = (drSugSidur[0]["RASHAI_LEDAVEACH"].ToString() == "1");
            }
            else //אינו סידור מיוחד ואינו סידור מפה
                bRashaiLedavech = false;

        //אם הכרטיס הוא ללא התייחסות והמספר שאישי של הגורם שנכנס שונה מהמספר האישי של הכרטיס
        //לא נאפשר עדכון סידור אם לסידור לא קיים מאפיין 99
        if ((MeasherOMistayeg == clGeneral.enMeasherOMistayeg.ValueNull) && (!bRashaiLedavech) && (LoginUserId != MisparIshi))
            bEnable = false;

        return bEnable;
    }
    protected void AddAttribute(Control oObj, string sAttrName, string sAttrValue)
    {
        string _ControlType = oObj.GetType().ToString();
        switch (_ControlType)
        {
            case "System.Web.UI.WebControls.TextBox":
                TextBox _Txt = (TextBox)oObj;
                _Txt.Attributes.Add(sAttrName, sAttrValue);//_Txt.Text
                break;
            case "System.Web.UI.WebControls.DropDownList":
                DropDownList _DDl = (DropDownList)oObj;
                _DDl.Attributes.Add(sAttrName, sAttrValue);//_DDl.SelectedValue
                break;
            case "System.Web.UI.WebControls.CheckBox":
                CheckBox _Chk = (CheckBox)oObj;
                _Chk.Attributes.Add(sAttrName, sAttrValue);//_Chk.Checked.GetHashCode().ToString()
                break;
            case "System.Web.UI.HtmlControls.HtmlInputCheckBox":
                HtmlInputCheckBox _HtmlChk = (HtmlInputCheckBox)oObj;
                _HtmlChk.Attributes.Add(sAttrName, sAttrValue);//_Chk.Checked.GetHashCode().ToString()
                break;       
        }
    }
    //protected bool ChkIfSidurNahagut(DataRow[] drSugSidur, clSidur oSidur)
    //{
    //    bool bSidurDriver = false;

    //    try
    //    {
    //        //אם לפחות אחד מהסידורים הוא סידור נהגות נעדכן את ה PROPERTY ךTRUE
    //        //סידור מיוחד
    //        if (oSidur.bSidurMyuhad)
    //        {
    //            if (oSidur.bSidurNahagut)
    //            {
    //                bSidurDriver = true;
    //            }
    //        }
    //        else
    //        {
    //            //סידור רגיל
    //            if (drSugSidur.Length > 0)
    //            {
    //                bSidurDriver = String.IsNullOrEmpty(drSugSidur[0]["sector_avoda"].ToString()) ? false : (int.Parse(drSugSidur[0]["sector_avoda"].ToString()) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
    //            }
    //        }
    //        return bSidurDriver;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void SetSidurNahagutStatus(DataRow[] drSugSidur, clSidur oSidur)
    {
        //bool bSidurDriver;

        //אם לפחות אחד מהסידורים הוא סידור נהגות נעדכן את ה PROPERTY ךTRUE
        //סידור מיוחד
        bSidurNahagut = IsSidurNahagut(ref oSidur, drSugSidur);//ChkIfSidurNahagut(drSugSidur, oSidur);
        if (bSidurNahagut){        
            IsAtLeastOneSidurNahagut = true;
        }
        //if (oSidur.bSidurMyuhad)
        //{
        //    if (oSidur.bSidurNahagut)
        //    {
        //        IsSidurNahagut = true;
        //    }
        //}
        //else
        //{
        //    //סידור רגיל
        //    if (drSugSidur.Length > 0)
        //    {
        //        bSidurDriver = String.IsNullOrEmpty(drSugSidur[0]["sector_avoda"].ToString()) ? false : (int.Parse(drSugSidur[0]["sector_avoda"].ToString()) == clGeneral.enSectorAvoda.Nahagut.GetHashCode());
        //        if (bSidurDriver)
        //        {
        //            IsSidurNahagut = true;
        //        }
        //    }
        //}
    }
    private enDayType GetDayType(ref clSidur oSidur, DateTime dCardDate)
    {
        enDayType _DayType = enDayType.enRegular;
        //מחזיר סוג יום] רגיל,שישי או שבת
        try
        {
            if ((oSidur.sErevShishiChag.Equals("1")) || ((dCardDate.DayOfWeek.GetHashCode() + 1)==6))
                _DayType = enDayType.enShisi;
            
            if ((oSidur.sShabaton.Equals("1")) || ((dCardDate.DayOfWeek.GetHashCode() + 1)==7))
              _DayType = enDayType.enShabat;
                       
            return _DayType;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected bool IsHashlamaAllowed(ref clSidur oSidur, DataRow[] drSugSidur)       
    {
        bool bHashlamaAllowed = true;

        //לחסום את השדה לסידורי מטלה
        if (oSidur.iMisparSidur < 1000)
        {
            bHashlamaAllowed = false;
        }
        //לא נאפשר השלמה לעובד מאגד תעבורה
        if (OvedYomAvoda.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
        {
            bHashlamaAllowed = false;
        }
        //סידור מיוחד - לסידור מאפיין 40        
        if (oSidur.bSidurMyuhad)
        {
            if (!oSidur.bHashlamaKodExists)
            {
                bHashlamaAllowed = false;
            }
        }
        else //סידור רגיל  //40 (זכאי להשלמה עבור הסידור) בטבלת מאפיינים סידורים מיוחדים. 
        {
            if ((drSugSidur.Length) > 0)
            {
                int iHashlamaAllowed = String.IsNullOrEmpty(drSugSidur[0]["ZAKAY_LEHASHLAMA_AVUR_SIDUR"].ToString()) ? 0 : int.Parse(drSugSidur[0]["ZAKAY_LEHASHLAMA_AVUR_SIDUR"].ToString());
                if (!(iHashlamaAllowed > 0))
                {
                    bHashlamaAllowed = false;
                }
            }
        }
        if (bHashlamaAllowed)
            if (IsSidurTimeBigOrEquallToHashlamaTime(ref oSidur))
                bHashlamaAllowed = false;
        
        ////נבדוק את סוג היום, רגיל, שישי או שבת, לפי סוג היום נבדוק האם זמן הסידור קטן מפרמטרים 101, 103,102 בהתאמה
        ////רק אם קטן, נאפשר השלמה
        //if (!SidurTimeIsLessThanParameterHashlama(ref  oSidur))
        //{
        //    bHashlamaAllowed = false;
        //}
        return bHashlamaAllowed;                      
    }
    protected bool IsSidurTimeBigOrEquallToHashlamaTime(ref clSidur oSidur)
    {
        float fSidurTime;
        bool bSidurTimeBigger = false;

        if ((oSidur.sShatHatchala == "") || (oSidur.sShatGmar == ""))        
            bSidurTimeBigger = true;        
        else
        {
            fSidurTime = clDefinitions.GetSidurTimeInMinuts(oSidur);
            bSidurTimeBigger = (fSidurTime / 60 >= clGeneral.enSugHashlama.enHashlama.GetHashCode());
        }
       
        return bSidurTimeBigger;       
    }
    protected bool SidurTimeIsLessThanParameterHashlama(ref clSidur oSidur)
    {
        bool bLess = false;
        enDayType _DayType;
        float fSidurTime;

        //נבדוק את סוג היום, רגיל, שישי או שבת, לפי סוג היום נבדוק האם זמן הסידור קטן מפרמטרים 101, 103,102 בהתאמה
        //רק אם קטן, נאפשר השלמה
        _DayType = GetDayType(ref oSidur, CardDate);
         
        fSidurTime = clDefinitions.GetSidurTimeInMinuts(oSidur);
        switch (_DayType)
        {
            case enDayType.enRegular:
                bLess = fSidurTime < Param101;
                //hidDayType.Value = enDayType.enRegular.GetHashCode().ToString();
                break;
            case enDayType.enShisi:                
                bLess = fSidurTime < Param102;
                //hidDayType.Value = enDayType.enShisi.GetHashCode().ToString();
                break;
            case enDayType.enShabat:
                bLess = fSidurTime < Param103;
                //hidDayType.Value = enDayType.enShabat.GetHashCode().ToString();
                break;
        }
        return bLess;
    }
    protected bool IsSidurNotMyuchadNochechut(ref clSidur oSidur, DataRow[] drSugSidur)
    {
        //במקרה של סידור שאינו מיוחד עם מאפיין 54 (שעון נוכחות)
        bool bSidurNochechot=false;
        if (!oSidur.bSidurMyuhad)
            if (drSugSidur.Length > 0)
                bSidurNochechot = (!String.IsNullOrEmpty(drSugSidur[0]["SHAON_NOCHACHUT"].ToString()));


        return bSidurNochechot;
    }
    protected bool IsPitzulHafsakaAllowed(ref clSidur oSidur,  ref bool bSidurMyuchadShaon)
    {
        bool bPitzulHafsakaAllowed = false;
        //DataRow[] drSugSidur;
        int iPitzulHafasaka = String.IsNullOrEmpty(oSidur.sPitzulHafsaka)? 0 : int.Parse(oSidur.sPitzulHafsaka);
        bool bSidurShaonMyuchad = IsSidurShaon(ref oSidur);
        int iMikumShaonKnisa = String.IsNullOrEmpty(oSidur.sMikumShaonKnisa) ? 0 : int.Parse(oSidur.sMikumShaonKnisa);
        int iMikumShaonYetiza = String.IsNullOrEmpty(oSidur.sMikumShaonYetzia) ? 0 : int.Parse(oSidur.sMikumShaonYetzia);
        //נשלוף את מאפייני סוג הסידור ( סידורים רגילים(
       // drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, CardDate, SugeySidur);
        //bool bNotMyuchadSidurShaon = IsSidurNotMyuchadNochechut(ref oSidur, drSugSidur);
        //if ((iPitzulHafasaka > 0) && (bNotMyuchadSidurShaon))        
        //    bPitzulHafsakaAllowed = true;        
        //else {
        

        //במקרה של סידור רגיל או מיוחד שאין לו מאפיין 54 (שעון נוכחות) או מיוחד שיש לו מאפיין 54 (שעון נוכחות) וגם לעובד יש מאפיין 41 (זכאי לפיצול בתפקיד) רק בתנאי והמסך עולה עם ערך גדול מ- 0.
        //במקרה של סידור מיוחד עם מאפיין 54 (שעון נוכחות) ולעובד אין מאפיין 41 (זכאי לפיצול בתפקיד) וקיים ערך בשדה Mikum_shaon_knisa וגם בשדה Mikum_shaon_yetzia -  השדה זמין תמיד ויש להציג בו רק את הערכים הבאים:
        //א. ב. "לא זכאי לפיצול" (KOD_PIZUL_HAFSAKA=0)
        //ב. "הפסקה ע"ח העובד
        //(KOD_PIZUL_HAFSAKA=3) 

        bPitzulHafsakaAllowed = ((bSidurShaonMyuchad && (iMikumShaonKnisa > 0) && (iMikumShaonYetiza > 0) && (!MeafyenyOved.Meafyen41Exists)) || ((iPitzulHafasaka > 0) && (oSidur.bSidurMyuhad) && (!oSidur.bShaonNochachutExists)) || ((bSidurShaonMyuchad) && (iPitzulHafasaka > 0) && (MeafyenyOved.Meafyen41Exists)) || ((!oSidur.bSidurMyuhad)) && (iPitzulHafasaka > 0));
            //במקרה של סידור מיוחד עם מאפיין 54 (שעון נוכחות) השדה זמין תמיד ויש להציג בו רק את הערכים הבאים:
            //ב. "הפסקה ע"ח העובד
            // (KOD_PIZUL_HAFSAKA=3) 
        bSidurMyuchadShaon =(bSidurShaonMyuchad); // במידה וTRUE, נאפשררק שני ערכים בקומבו 
        //} 
       
        //if ((iPitzulHafasaka > 0) && (iPitzulHafasaka!=2)) 
        //{
        //    bPitzulHafsakaAllowed = true;
        //}
        //else
        //{
        //    if (iPitzulHafasaka==2)
        //    {
        //        //הסידור עליו מסומן הפיצול והסידור הבא אחריו הם מסוג נהגות. 
        //        if (_SidurNahagut) //סידור נוכחי
        //        {
        //            clSidur oSidurNext;
        //            if (htEmployeeDetails.Count - 1 > iIndex)
        //            {
        //                oSidurNext = (clSidur)htEmployeeDetails[iIndex + 1];
        //                //נשלוף את מאפייני סוג הסידור ( סידורים רגילים(
        //                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidurNext.iSugSidurRagil, CardDate, SugeySidur);
        //                bPitzulHafsakaAllowed = (ChkIfSidurNahagut(drSugSidur, oSidurNext));
        //            }
        //        }
        //    }
        //}
        return bPitzulHafsakaAllowed;
    }
    protected bool IsOutMichsaAllowed(ref clSidur oSidur)
    {
        bool bOutMichsaAllowed= false;

        //לא נאפשר שינוי מחוץ למכסה לעובד מאגד תעבורה
        //נאפשר עדכון מחוץ למכסה רק לעובדים שהם לא מאגד תעבורה ושהסידור מיוחד ויש לו ערך 1 במאפיין 25
        if ((OvedYomAvoda.iKodHevra) != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode() && (oSidur.bSidurMyuhad) && (oSidur.sZakayMichutzLamichsa == clGeneral.enMeafyenSidur25.enZakai.GetHashCode().ToString()))
        {
            bOutMichsaAllowed = true;
        }        
        
        return bOutMichsaAllowed;
    }
    protected bool IsSidurZakaiLehamara(ref clSidur oSidur)
    {
        return (!((oSidur.bSidurMyuhad) && (oSidur.sZakaiLehamara == clGeneral.enMeafyen32.enLoZakai.GetHashCode().ToString())));
    }

    protected bool IsSidurNihul(ref clSidur oSidur, DataRow[] drSugSidur)
    {
        bool bSidurNihul = false;
        //מחזיר אם סידור ניהול
        if (oSidur.bSidurMyuhad)
            bSidurNihul = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
        else
        {
            if (drSugSidur.Length > 0)
                bSidurNihul = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
        }

        return bSidurNihul;
    }
    protected bool IsSidurNahagut(ref clSidur oSidur, DataRow[] drSugSidur)
    {
        bool bSidurNahagut = false;
        //מחזיר אם סידור נהגות
        if (oSidur.bSidurMyuhad)        
            bSidurNahagut = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());        
        else
        {
            if (drSugSidur.Length > 0)            
                bSidurNahagut = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());                                           
        }
        return bSidurNahagut;
    }
    //protected bool IsSidurNahagutOrTnua(ref clSidur oSidur, DataRow[] drSugSidur)
    //{
    //    bool bSidurNahagut = false;
        
    //    //if (oSidur.bSidurMyuhad)
    //    //{
    //    //    bSidurNahagut = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString() || (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString()));
    //    //}
    //    //else
    //    //{
    //    //    if (drSugSidur.Length > 0)
    //    //    {
    //    //        bSidurNahagut = (((drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
    //    //                       || (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString())));
    //    //    }
    //    //}
    //    return bSidurNahagut;
    //}
    protected bool IsSidurInShabat(ref clSidur oSidur)
    {
        return oSidur.dFullShatGmar > KnisatShabat;
    }
    //protected bool IsHamaraAllowed(ref clSidur oSidur, DataRow[] drSugSidur)
    //{
    //    bool bHamaraAllowed = true;
    //    try
    //    {
    //        ////לא נאפשר המרה לעובד שלא קיים לו מאפיין 31
    //        //if (!MeafyenyOved.Meafyen31Exists)
    //        //{
    //        //    bHamaraAllowed = false;
    //        //}
    //        ////לא נאפשר המרה לעובד מאגד תעבורה
    //        //if (OvedYomAvoda.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
    //        //{
    //        //    bHamaraAllowed = false;
    //        //}

    //        //לא נאפשר המרה לעובד שיש לו מאפיין 32 עם ערך 2 (לא זכאי להמרה) - סידור מיוחד
    //        if (oSidur.bSidurMyuhad)
    //        {
    //            if (oSidur.sZakaiLehamara == clGeneral.enMeafyen32.enLoZakai.GetHashCode().ToString())
    //            {
    //                bHamaraAllowed = false;
    //            }
    //        }
    //        else
    //        {
    //            //בסידורים רגילים נאפשר המרה רק לסידורים מסוג נהגות וניהול תנועה
    //            if (drSugSidur.Length > 0)
    //            {
    //                if ((drSugSidur[0]["sector_avoda"].ToString() != clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
    //                    && (drSugSidur[0]["sector_avoda"].ToString() != clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString()))
    //                {
    //                    bHamaraAllowed = false;
    //                }                    
    //            }
    //        }
    //        return bHamaraAllowed;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    public bool IsExceptionAllowed(ref clSidur oSidur, ref string sCharigaType)
    {
        bool bExceptionAllowed = false;

        //DateTime dShatHatchalaLetashlum= new DateTime();
        //DateTime dShatGmarLetashlum= new DateTime();
        //clBatchManager oBatchManager = new clBatchManager();
       
        //נאפשר חריגה רק במידה ושעת ההתחלה שהוזנה פחות שעת מאפיין שעת התחלה גדול מפרמטר 41
        try
        {
            //נקרא את מאפייני שעת גמר ושעת התחלה לתשלום           
            //ברמת הסידור - רק לסידורים מיוחדים שיש להם ערך 1 (זכאי) במאפיין 35 (זכאות לחריגה משעות כניסה ויציאה

            if ((oSidur.bSidurMyuhad) && (oSidur.sZakaiLeChariga == clGeneral.enMeafyenSidur35.enCharigaZakai.GetHashCode().ToString()))
            {
                /* ברמת העובד -
                   א. עובד עם מאפיינים מתאימים ליום העבודה- 
                   יום חול -  מאפיינים 3 ו-  4 עבור יום חול (לכל העובדים)
                   ערב שישי/ערב חג  - מאפיינים 5 ו- 6 
                   עבור ערב שבת/שבתון  - מאפיינים 7 ו- 8 
                   1. שעת התחלה קטנה מהערך במאפיין  בשדה שעת התחלה לתשלום או שעת גמר גדולה מהערך בשדה במאפיין שעת גמר לתשלום (המאפיין המתאים לפי סוג היום) - אם שניהם לא מתקיימים, לחסום את השדה.
                */

                //oBatchManager.GetOvedShatHatchalaGmar(oSidur.dFullShatGmar, MeafyenyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);
                if (!String.IsNullOrEmpty(oSidur.sShatHatchala))
                {
                    if (oSidur.dFullShatHatchala < oSidur.dFullShatHatchalaLetashlum)
                    {
                        /*אם תנאי 1 מתקיים, בודקים
                            אם המרווח בין החתמת כניסה/יציאה לשעת התחלה/גמר לתשלום מאפייני שעת התחלה/גמר מותרת שווה או גדול מהערך המוגדר בפרמטר 41 (זמן חריגה התחלה / גמר על חשבון העובד). יש לבדוק את מאפייני שעת התחלה/גמר מותרת בהתאם לסוג היום.
                            כמובן יש לבדוק שהמרווח הוא מ"הכיוון הנכון", כלומר עבור עובד שאמור להתחיל לעבוד בשעה 07:00, שעת התחלה לתשלום היא 07:20 החתים שעון בשעה 07:20, כמובן שאסור לבקש חריגה משעת התחלה. בדיקה דומה יש לבצע עבור בקשה חריגה משעת גמר. במידה וביקשו ערך שגוי, יש להציג הודעה מתאימה ב- pop-up : "אין חריגה משעת התחלה או משעת גמר 
                        */
                        if ((oSidur.dFullShatHatchalaLetashlum - oSidur.dFullShatHatchala).TotalMinutes >= KdsParameters.iZmanChariga)
                        {
                            bExceptionAllowed = true;
                            sCharigaType = clGeneral.enCharigaValue.CharigaKnisa.GetHashCode().ToString();//חריגה רק מהתחלה
                        }
                    }
                }
                if (!String.IsNullOrEmpty(oSidur.sShatGmar))
                {
                    if (oSidur.dFullShatGmar > oSidur.dFullShatGmarLetashlum)
                    {
                        if ((oSidur.dFullShatGmar - oSidur.dFullShatGmarLetashlum).TotalMinutes >= KdsParameters.iZmanChariga)
                        {

                            if (bExceptionAllowed)
                                sCharigaType = clGeneral.enCharigaValue.CharigaKnisaYetiza.GetHashCode().ToString();  //חריגה  ומגמר מהתחלה                          
                            else
                            {
                                bExceptionAllowed = true;
                                sCharigaType = clGeneral.enCharigaValue.CharigaYetiza.GetHashCode().ToString();   //חריגה מגמר
                            }
                        }
                    }
                }


                //if (!bExceptionAllowed)
                //{
                //יום שישי או ערב חג
                //ביום שישי/ערב חג לעובד ללא מאפיינים 5 ו- 6 וגם TB_Sidurim_Ovedim.KOD_SIBA_LO_LETASHLUM=5   (עבודה בשישי ללא הרשאה).
                if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                {
                    if ((!MeafyenyOved.Meafyen5Exists) && (!MeafyenyOved.Meafyen6Exists) && (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkAtFridayWithoutPremission.GetHashCode()))
                    {
                        bExceptionAllowed = true;
                        sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
                    }
                }
                //ביום שבת/שבתון לעובד ללא מאפיינים 7 ו- 8 וגם TB_Sidurim_Ovedim.KOD_SIBA_LO_LETASHLUM=4  (עבודה בשבתון ללא הרשאה).
                if ((oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oSidur.sShabaton.Equals("1")))
                {
                    if ((!MeafyenyOved.Meafyen7Exists) && (!MeafyenyOved.Meafyen8Exists) && (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkAtSaturdayWithoutPremission.GetHashCode()))
                    {
                        bExceptionAllowed = true;
                        sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
                    }
                }
                //סידור מחוץ לשעות מותרות לעובד).
                if (oSidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.SidurInNonePremissionHours.GetHashCode())
                {
                    bExceptionAllowed = true;
                    sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
                }
                //}
            }
            else
            {
                sCharigaType = clGeneral.enCharigaValue.CharigaAvodaWithoutPremmision.GetHashCode().ToString();
            }
        

            return bExceptionAllowed;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool HasSaveCard
    {
        set
        {
            _HasSaveCard = value;
        }
        get
        {
            return _HasSaveCard;
        }        
    }
    protected bool IsIdkunExists(int iMisparIshiIdkunRashemet,bool bProfileRashemet, clWorkCard.ErrorLevel oLevel, int iPakadId, int iMisparSidur, DateTime dShatHatchala, DateTime dShatYetiza, int iMisparKnisa)
    {
        return clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet,bProfileRashemet, oLevel, iPakadId, iMisparSidur, dShatHatchala, dShatYetiza, iMisparKnisa, ref _dtIdkuneyRashemet);
    }
    protected void CreateShatHatchalaCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex, bool bSidurActive)
    {        
        TextBox oTextBox;        
        CustomValidator vldShatHatchala;
        AjaxControlToolkit.ValidatorCalloutExtender vldExShatHatchala;
        AjaxControlToolkit.MaskedEditExtender oMaskedEditExtender;
        String sShatHatchalaMutert="";
        String sShatGmarMutert = "";
        String sMessage;
        bool bSidurMustDisabled=false;
        try
        {
            hCell = CreateTableCell("51px", "", "");

            oTextBox = new TextBox();
            oTextBox.ID = "txtSH" + iIndex;
            oTextBox.Text = oSidur.sShatHatchala;
            oTextBox.Width = Unit.Pixel(40);
            //oTextBox.Height = Unit.Pixel(20);
            oTextBox.CausesValidation = true;
            oTextBox.MaxLength = MAX_LEN_HOUR;
            bSidurMustDisabled = ((!(IsMikumShaonEmpty(oSidur.sMikumShaonKnisa))) || (bSidurContinue) || (!IsAccessToSidurNotShaon(ref oSidur)) || (IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "SHAT_HATCHALA"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0)));
            oTextBox.ReadOnly = ((bSidurMustDisabled) || (!bSidurActive));
           
            oTextBox.Attributes.Add("OrgShatHatchala",oSidur.dOldFullShatHatchala.ToString());//oSidur.dFullShatHatchala.ToString());
            //oTextBox.Attributes.Add("FullSH", oSidur.dFullShatHatchala.ToString());
            oTextBox.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
            oTextBox.Attributes.Add("onkeypress", "SetBtnChanges();SetLvlChg(2,"+iIndex+");");
            oTextBox.Attributes.Add("onchange", "changeStartHour("+ iIndex +"); SidurTimeChanged(" + iIndex + ");");
            oTextBox.Attributes.Add("OrgEnabled", bSidurMustDisabled ? "0" : "1");
            oTextBox.ToolTip = "תאריך תחילת הסידור הוא: " + oSidur.dFullShatHatchala.ToShortDateString();
          
            //AddAttribute(oTextBox,"OldV",oSidur.dOldFullShatHatchala.ToShortTimeString());//oTextBox.Text);
            hCell.Controls.Add(oTextBox);

            DataRow[] dr = dtApprovals.Select("mafne_lesade='Shat_hatchala'");
            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Valid:
                    //במידה והכרטיס תקין נבדוק אישורים                    
                    CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                    break;
                case clGeneral.enCardStatus.Error:
                    if (!SetOneError(oTextBox, hCell, "Shat_Hatchala", ref oSidur, iIndex.ToString(), ""))
                    {
                        //אם לא נמצאה שגיאה, נבדוק אם יש אישור
                        //אישורים                       
                        CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                    }
                    break;
            }
            if ((oTextBox.ReadOnly) && (oTextBox.Style["background-color"] != "red") && ((oTextBox.Style["background-color"] != "green")))
            {
                oTextBox.Style.Add("color", "gray");
                oTextBox.Style.Add("backgroung", "white");
            }
            oMaskedEditExtender = AddTimeMaskedEditExtender(oTextBox.ID, iIndex, "99:99", "SHMask", AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Left);
            //oTextBox.Attributes.Add("onfocus", "SetFocus('lstSidurim_" + oMaskedEditExtender.ClientID + "_ClientState');");
            hCell.Controls.Add(oMaskedEditExtender);
            
            SetSidurStartHourParameters(oSidur, ref sShatHatchalaMutert, ref sShatGmarMutert);
            sShatHatchalaMutert = String.IsNullOrEmpty(sShatHatchalaMutert) ? "" : (DateTime.Parse(sShatHatchalaMutert)).ToShortTimeString();
            sShatGmarMutert = String.IsNullOrEmpty(sShatGmarMutert) ? "" : (DateTime.Parse(sShatGmarMutert)).ToShortTimeString();
            sMessage = "יש להקליד שעת התחלה תקינה: " + sShatHatchalaMutert + " " + "עד" + " " + sShatGmarMutert;
            vldShatHatchala = AddCustomValidator(oTextBox.ID,sMessage, "vldSHatchala" + iIndex,"ChkStartHour","");
            hCell.Controls.Add(vldShatHatchala);
            vldExShatHatchala = AddCallOutValidator(vldShatHatchala.ID, "vldExSHatchala" + iIndex, "", AjaxControlToolkit.ValidatorCalloutPosition.Left);
            hCell.Controls.Add(vldExShatHatchala);

            //sMessage = "שעת ההתחלה אינה יכולה להיות גדולה או שווה לשעת הגמר";
            //vldShatHatchala = AddCustomValidator(oTextBox.ID, sMessage, "vldSidurHour" + iIndex, "IsSHBigSG","");
            //hCell.Controls.Add(vldShatHatchala);
            //vldExShatHatchala = AddCallOutValidator(vldShatHatchala.ID, "vldESdrHour" + iIndex, "");
            //hCell.Controls.Add(vldExShatHatchala);
          
          
        
            hCell.Style.Add("border-left", "solid 1px gray");
            //if (iIndex > 0)
            //{
            //    sMessage = "שעת ההתחלה שהוקלדה גורמת לחפיפת זמנים עם הסידור הקודם";
            //    vldShatHatchala = AddCustomValidator(oTextBox.ID, sMessage, "vldSdrPrvHour" + iIndex, "IsSHGreaterPrvSG","");
            //    hCell.Controls.Add(vldShatHatchala);
            //    vldExShatHatchala = AddCallOutValidator(vldShatHatchala.ID, "vldExSdPrvHour" + iIndex, "");
            //    hCell.Controls.Add(vldExShatHatchala);
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool  CheckIfApprovalExists(int[] arrApprovalKey, ref clSidur oSidur, ref HtmlTableCell _hCell)
    {
        KdsWorkFlow.Approvals.WorkCard _WorkCardFlow;
        string sAllApprovalDescription = "";
        string sApprovalDescription = "";
        bool bEnableApprove = false;
        bool ApprovalExist=false;
        //במידה וקיים אישור נצבע בירוק את השדה ונאפשר העלאת חלון הסבר
        _WorkCardFlow = new WorkCard();
        for (int iCount = 0; iCount < arrApprovalKey.Length; iCount++)
        {
            if (_WorkCardFlow.HasApproval(EmployeeApproval, oSidur.iMisparSidur, oSidur.dFullShatHatchala, arrApprovalKey[iCount], ref sApprovalDescription, ref bEnableApprove))
            {
                //_hCell.Attributes.Add("class", "ApprovalField");
                _hCell.Style.Add("background-color", "green");
                _hCell.Style.Add("color", "white"); 
                _hCell.Attributes.Add("App", sAllApprovalDescription = string.Concat(sAllApprovalDescription, (char)13, sApprovalDescription));
                _hCell.Attributes.Add("ondblclick", "GetAppMsg(this)");
                ApprovalExist = true;
            }
        }
        return ApprovalExist;
    }
    protected void CheckIfApprovalExists(int[] arrApprovalKey, ref clSidur oSidur, ref TextBox _TextBox)
    {
        KdsWorkFlow.Approvals.WorkCard _WorkCardFlow;
        string sAllApprovalDescription = "";
        string sApprovalDescription = "";
        //במידה וקיים אישור נצבע בירוק את השדה ונאפשר העלאת חלון הסבר
        _WorkCardFlow = new WorkCard();
        bool bEnableApprove=false;
        try
        {
            for (int iCount = 0; iCount < arrApprovalKey.Length; iCount++)
            {
                if (_WorkCardFlow.HasApproval(EmployeeApproval, oSidur.iMisparSidur, oSidur.dFullShatHatchala, arrApprovalKey[iCount], ref sApprovalDescription, ref bEnableApprove))
                {
                    _TextBox.Attributes.Add("class", "ApprovalField");
                    _TextBox.Style.Add("background-color", "green");
                    _TextBox.Style.Add("color", "white");                               
                    _TextBox.Attributes.Add("App", sAllApprovalDescription = string.Concat(sAllApprovalDescription, (char)13, sApprovalDescription));
                    _TextBox.Attributes.Add("ondblclick", "GetAppMsg(this)");
                    _TextBox.Enabled = bEnableApprove;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void SetFieldColor(ref Control oObj, string sColor, string sBackColor)
    //{
    //    string sControlType = oObj.GetType().ToString();
    //    switch (sControlType)
    //    {
    //        case "System.Web.UI.WebControls.TextBox":
    //            ((TextBox)oObj).Style.Add("background-color", sBackColor);
    //            ((TextBox)oObj).Style.Add("color", sColor);               
    //            break;
    //        case "System.Web.UI.WebControls.DropDownList":
    //            ((DropDownList)oObj).Style.Add("background-color", sBackColor);
    //            ((DropDownList)oObj).Style.Add("color", sColor);                
    //            break;
    //        case "System.Web.UI.WebControls.CheckBox":
    //            hCell.Style.Add("background-color", sBackColor);
    //            hCell.Style.Add("color", sColor);                
    //            break;
    //        case "System.Web.UI.WebControls.Label":
    //            hCell.Style.Add("background-color", sBackColor);
    //            hCell.Style.Add("color", sColor);              
    //            break;
    //        case "System.Web.UI.WebControls.HyperLink":
    //            hCell.Style.Add("background-color", sBackColor);
    //            hCell.Style.Add("color", sColor);              
    //            break;
    //    }                
    //}
    protected bool CheckIfApprovalExists(int[] arrApprovalKey, int iMisaprSidur, DateTime dFullShatHatchala, DateTime dShatYetiza, long lMakatNesia, ref TextBox _TextBox)
    {
        KdsWorkFlow.Approvals.WorkCard _WorkCardFlow;
        string sAllApprovalDescription = "";
        string sApprovalDescription = "";
        bool bApprove = false;
        bool bEnableApprove = false;

        //במידה וקיים אישור נצבע בירוק את השדה ונאפשר העלאת חלון הסבר
        _WorkCardFlow = new WorkCard();
        try
        {
            for (int iCount = 0; iCount < arrApprovalKey.Length; iCount++)
            {
                if (_WorkCardFlow.HasApproval(EmployeeApproval, iMisaprSidur, dFullShatHatchala, dShatYetiza, lMakatNesia, arrApprovalKey[iCount], ref sApprovalDescription, ref bEnableApprove))
                {
                    _TextBox.Attributes.Add("class", "ApprovalField");
                    _TextBox.Attributes.Add("App", sAllApprovalDescription = string.Concat(sAllApprovalDescription, (char)13, sApprovalDescription));
                    _TextBox.Attributes.Add("ondblclick", "GetAppMsg(this)");
                    _TextBox.Enabled = bEnableApprove;
                    bApprove = true;
                }
            }
            return bApprove;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool CheckIfApprovalExists(int[] arrApprovalKey, ref clSidur oSidur, ref string sAllApprovalDescription, ref bool bEnableApprovel)
    {
        bool bApprovalExists = false;
        KdsWorkFlow.Approvals.WorkCard _WorkCardFlow;       
        string sApprovalDescription = "";
       
       
        //במידה וקיים אישור נצבע בירוק את השדה ונאפשר העלאת חלון הסבר
        _WorkCardFlow = new WorkCard();
        for (int iCount = 0; iCount < arrApprovalKey.Length; iCount++)
        {
            if (_WorkCardFlow.HasApproval(EmployeeApproval, oSidur.iMisparSidur, oSidur.dFullShatHatchala, arrApprovalKey[iCount], ref sApprovalDescription, ref bEnableApprovel))
            {
                bApprovalExists = true;
                sAllApprovalDescription = string.Concat(sAllApprovalDescription, (char)13, sApprovalDescription);            
            }
        }
        return bApprovalExists;
    }
    protected void SetFieldStyle(Control oObj, HtmlTableCell hCell, int iLevel, int iErrorCount, string sControlType,
                                 string sBackColor, string sColor, string sPeilutKey, string sFieldName, string sImgId)        
    {
        //if (oObj is HtmlControl)
        //{
        //    ((HtmlControl)oObj).Style.Add("background-color", sBackColor);
        //    ((HtmlControl)oObj).Style.Add("color", sColor);
        //    if (iErrorCount > 0)
        //    {
        //        ((HtmlControl)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
        //        ((HtmlControl)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
        //        ((HtmlControl)oObj).Attributes.Add("FName", sFieldName);
        //        ((HtmlControl)oObj).Attributes.Add("ImgId", sImgId);
        //    }
        //}
        //else
        //{
        //    ((WebControl)oObj).Style.Add("background-color", sBackColor);
        //    ((WebControl)oObj).Style.Add("color", sColor);
        //    if (iErrorCount > 0)
        //    {
        //        ((WebControl)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
        //        ((WebControl)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
        //        ((WebControl)oObj).Attributes.Add("FName", sFieldName);
        //        ((WebControl)oObj).Attributes.Add("ImgId", sImgId);
        //    }
        //}
        switch (sControlType)
        {
            case "System.Web.UI.WebControls.TextBox":
                //((TextBox)oObj).Style.Add("background-color", sBackColor);
                //((TextBox)oObj).Style.Add("color", sColor);
                if (iErrorCount > 0)
                {
                    ((TextBox)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
                    ((TextBox)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
                    ((TextBox)oObj).Attributes.Add("FName", sFieldName);
                    ((TextBox)oObj).Attributes.Add("ImgId", sImgId);
                    ((TextBox)oObj).Style.Add("background-color", sBackColor);
                    ((TextBox)oObj).Style.Add("color", sColor);
                }
                break;
            case "System.Web.UI.WebControls.DropDownList":
                ((DropDownList)oObj).Style.Add("background-color", sBackColor);
                ((DropDownList)oObj).Style.Add("color", sColor);
                if (iErrorCount > 0)
                {
                    ((DropDownList)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
                    ((DropDownList)oObj).Attributes.Add("ImgId", sImgId);
                    ((DropDownList)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
                    ((DropDownList)oObj).Attributes.Add("FName", sFieldName);
                }
                break;
            case "System.Web.UI.WebControls.CheckBox":
                hCell.Style.Add("background-color", sBackColor);
                hCell.Style.Add("color", sColor);
                if (iErrorCount > 0)
                {
                    ((CheckBox)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
                    ((CheckBox)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
                    ((CheckBox)oObj).Attributes.Add("FName", sFieldName);
                    ((CheckBox)oObj).Attributes.Add("ImgId", sImgId);
                }
                break;
            case "System.Web.UI.WebControls.Label":
                hCell.Style.Add("background-color", sBackColor);
                hCell.Style.Add("color", sColor);
                if (iErrorCount > 0)
                {
                    ((Label)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
                    ((Label)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
                    ((Label)oObj).Attributes.Add("FName", sFieldName);
                    ((Label)oObj).Attributes.Add("ImgId", sImgId);
                }
                break;
            case "System.Web.UI.WebControls.HyperLink":
                hCell.Style.Add("background-color", sBackColor);
                hCell.Style.Add("color", sColor);
                if (iErrorCount > 0)
                {
                    ((HyperLink)oObj).Attributes.Add("ErrCnt", iErrorCount.ToString());
                    ((HyperLink)oObj).Attributes.Add("ondblClick", "GetErrorMessage(this," + iLevel + ",'" + sPeilutKey + "')");
                    ((HyperLink)oObj).Attributes.Add("FName", sFieldName);
                    ((HyperLink)oObj).Attributes.Add("ImgId", sImgId);
                }
                break;
        }         
    }
    protected bool SetOneError(Control oObj, HtmlTableCell hCell, string sFieldName, ref clSidur oSidur, string sPeilutKey, string sImgId)
    {
        bool bErrorExists = false; ;
        DataSet dsFieldsErrors;
        DataView dvFieldsErrors;
        String _ContolType = oObj.GetType().ToString();

        dsFieldsErrors = clDefinitions.GetErrorsForFields(ProfileRashemet,ErrorsList, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dOldFullShatHatchala, sFieldName);
        dvFieldsErrors = new DataView(dsFieldsErrors.Tables[0]);
        dvFieldsErrors.RowFilter = "SHOW_ERROR=1";

        if (dvFieldsErrors.Count > 0){        
            SetFieldStyle(oObj,hCell, 2, dvFieldsErrors.Count, _ContolType, "red", "white", sPeilutKey, sFieldName, sImgId);
            bErrorExists = true;
        }
        else{        
            SetFieldStyle(oObj,hCell, 2, dvFieldsErrors.Count, _ContolType, "white", "black", sPeilutKey,sFieldName, sImgId);           
        }
        return bErrorExists;
    }  
    protected bool SetOneError(Control oObj,HtmlTableCell hCell, int iMisparIshi,DateTime dCardDate, 
                               int iMisparSidur,DateTime dFullShatHatchala , DateTime dPeilutShatYetiza,
                               int iMisparKnisa,string sPeilutKey, string sFieldName)
    {
        DataSet dsFieldsErrors;
        DataView dvFieldsErrors;
        String _ContolType = oObj.GetType().ToString();
        bool bError = false;

        dsFieldsErrors = clDefinitions.GetErrorsForFields(ProfileRashemet, ErrorsList, iMisparIshi, dCardDate, iMisparSidur, dFullShatHatchala, dPeilutShatYetiza, iMisparKnisa, sFieldName);
        dvFieldsErrors = new DataView(dsFieldsErrors.Tables[0]);
        dvFieldsErrors.RowFilter = "SHOW_ERROR=1";

        if (dvFieldsErrors.Count > 0){        
            SetFieldStyle(oObj, hCell, 3, dvFieldsErrors.Count, _ContolType, "red", "white", sPeilutKey, sFieldName, "");
            bError = true;
        }
        else
        {
            SetFieldStyle(oObj, hCell, 3, dvFieldsErrors.Count, _ContolType, "white", "black", sPeilutKey, sFieldName, "");
        }

        return bError;
    }
    protected bool IsErrorInAdditionFields(DataTable ErrorsList, ref clSidur oSidur)
                                          
    {
        bool bErrExists = false;
        DataRow[] drResults;
        try
        {
            drResults = ErrorsList.Select("mispar_ishi=" + oSidur.iMisparIshi + " and taarich=Convert('" + oSidur.dSidurDate.ToShortDateString() + "', 'System.DateTime')" + " and mispar_sidur=" + oSidur.iMisparSidur + " and shat_hatchala = '" + oSidur.dFullShatHatchala + "' and sadot_nosafim=1");
            bErrExists = (drResults.Length > 0);
            return bErrExists;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    //protected void SetSidurHourError(clSidur oSidur,ref TextBox oTextBox, int iError1Key, int iError2Key)
    //{
        //SetOneError(oTextBox, "Shat_Gmar", oSidur.iMisparSidur, oSidur.sShatHatchala);
        //string sErrorMsg;
        //bool bErrorExists;
        //bErrorExists = clWorkCard.IsErrorExists(ErrorsList,iError1Key , oSidur.iMisparIshi, DateTime.Parse(oSidur.sSidurDate), oSidur.iMisparSidur, oSidur.sShatHatchala, out sErrorMsg);
        //if (bErrorExists)
        //{
        //    oTextBox.Attributes.Add("style", "background-color: red;color:white ");
        //    oTextBox.Attributes.Add("Err", sErrorMsg);
        //    oTextBox.Attributes.Add("ondblClick", "GetErrorMessage(this)"); 
        //}
        //bErrorExists = clWorkCard.IsErrorExists(ErrorsList, iError2Key, oSidur.iMisparIshi, DateTime.Parse(oSidur.sSidurDate), oSidur.iMisparSidur, oSidur.sShatHatchala, out sErrorMsg);
        //if (bErrorExists)
        //{
        //    oTextBox.Attributes.Add("style", "background-color: red;color:white ");
        //    oTextBox.Attributes.Add("Err", sErrorMsg);
        //    oTextBox.Attributes.Add("ondblClick", "GetErrorMessage(this)"); 
        //}
   // }
    protected void CreateSidurNahagutCell(ref HtmlTableCell hCell, int iIndex)
    {
        hCell = CreateTableCell("1240px", "", "");
        Label lbl = new Label();
        lbl.ID = "lblSidurContinue" + iIndex;
        lbl.Text = "סידור רציפות - החישוב לתשלום ייעשה ברמה חודשית";
        lbl.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
        lbl.EnableViewState = false;
        hCell.Controls.Add(lbl);
    }
    protected void EnabledField(bool bEnabled,ref HtmlTableCell hCell,Control oObj)
    {
        SetFieldStyle(oObj, hCell, 0, 0, oObj.GetType().ToString(), "", bEnabled ? "black" : "gray", "", "", "");
    }
    protected void CreateShatGmarCell(clSidur oSidur, ref HtmlTableCell hCell, int iIndex,DataRow[] drSugSidur, bool bSidurActive)
    {
        TextBox oTextBox;      
        string sMessage;
        CustomValidator vldShatGmar;
        AjaxControlToolkit.MaskedEditExtender oMaskedEditExtender;
        AjaxControlToolkit.ValidatorCalloutExtender vldExSG;
        bool bOrgEnable;
        try
        {
            hCell = CreateTableCell("51px", "", "");
            oTextBox = new TextBox();
            oTextBox.ID = "txtSG" + iIndex;
            oTextBox.Text = oSidur.sShatGmar;
            oTextBox.Width = Unit.Pixel(40);          
            oTextBox.CausesValidation = true;
            oTextBox.MaxLength = MAX_LEN_HOUR;
            bOrgEnable = ((((IsMikumShaonEmpty(oSidur.sMikumShaonYetzia))) && (IsAccessToSidurNotShaon(ref oSidur)) && (!bSidurContinue) && (!IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelSidur, clUtils.GetPakadId(dtPakadim, "SHAT_GMAR"), oSidur.iMisparSidur, oSidur.dFullShatHatchala, DateTime.MinValue, 0))));
            oTextBox.ReadOnly = ((!bOrgEnable) || (!bSidurActive));

            oTextBox.Attributes.Add("onclick", "MovePanel(" + iIndex + ");");
            oTextBox.Attributes.Add("onChange", "SetDay('1|" + iIndex + "');SidurTimeChanged(" + iIndex + ");MovePanel(" + iIndex + ");");
            oTextBox.Attributes.Add("onkeypress", "SetBtnChanges();SetLvlChg(2," + iIndex + ");");
            
            //AddAttribute(oTextBox, "OrgDate", oSidur.dOldFullShatGmar.ToString());           
            //AddAttribute(oTextBox, "OldV", oTextBox.Text);
            //oTextBox.Attributes["OrgDate"] = oSidur.dOldFullShatGmar.ToShortDateString();
            oTextBox.ToolTip = "תאריך גמר הסידור הוא: " + oSidur.dFullShatGmar.ToShortDateString();
            oTextBox.Attributes.Add("OrgEnabled", bOrgEnable ? "1" : "0");
          
            
            DataRow[] dr = dtApprovals.Select("mafne_lesade='Shat_gmar'");
            
            switch (_StatusCard)
            {                             
                case clGeneral.enCardStatus.Error:
                    if (!SetOneError(oTextBox, hCell, "Shat_Gmar", ref oSidur, iIndex.ToString(), ""))
                    {
                        //אם לא נמצאה שגיאה נבדוק אישורים
                        CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                    }
                    break;
                case clGeneral.enCardStatus.Valid:
                    {
                        CheckIfApprovalExists(FillApprovalKeys(dr), ref oSidur, ref oTextBox);
                        break;
                    }
            }
            if ((oTextBox.ReadOnly) && (oTextBox.Style["background-color"] != "red") && (oTextBox.Style["background-color"] != "green"))
            {
                oTextBox.Style.Add("color", "gray");
                oTextBox.Style.Add("backgroung", "white");
            }

            oMaskedEditExtender = AddTimeMaskedEditExtender(oTextBox.ID, iIndex, "99:99", "SGMask", AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Left);
            hCell.Controls.Add(oMaskedEditExtender);
            hCell.Style.Add("border-left", "solid 1px gray");
            hCell.Controls.Add(oTextBox);

            //SetSidurEndHourParameters(oSidur, ref sShatGmarMutertStart, ref sShatGmarMutertEnd, drSugSidur);

            //sShatGmarMutertStart = String.IsNullOrEmpty(sShatGmarMutertStart) ? "" : (DateTime.Parse(sShatGmarMutertStart)).ToShortTimeString();
            //sShatGmarMutertEnd = String.IsNullOrEmpty(sShatGmarMutertEnd) ? "" : (DateTime.Parse(sShatGmarMutertEnd)).ToShortTimeString();
            
            //sMessage =  "יש להקליד שעת גמר תקינה: " + sShatGmarMutertStart + " " + "עד" + " " + sShatGmarMutertEnd;            
            sMessage = "";
            vldShatGmar = AddCustomValidator(oTextBox.ID, sMessage, "vldSG" + iIndex, "ISSGValid", "");
            vldExSG = AddCallOutValidator(vldShatGmar.ID, "vldExSG" + iIndex, "", AjaxControlToolkit.ValidatorCalloutPosition.Left);
            hCell.Controls.Add(vldShatGmar);
            hCell.Controls.Add(vldExSG);


            //sMessage = "שעת הגמר אינה יכולה להיות קטנה או שווה לשעת ההתחלה";
            //vldShatGmar = AddCustomValidator(oTextBox.ID, sMessage, "vldSdHours" + iIndex, "IsSHBigSG", "");           
            //vldExSG=AddCallOutValidator(vldShatGmar.ID, "vldExSdHours" + iIndex, "");
            //hCell.Controls.Add(vldShatGmar);
            //hCell.Controls.Add(vldExSG);

            //sMessage = "שעת הגמר שהוקלדה גורמת לחפיפת זמנים עם הסידור הבא";
            //vldShatGmar = AddCustomValidator(oTextBox.ID, sMessage, "vldSdNxtHour" + iIndex, "IsEHourBigSHour", "");
            //vldExSG = AddCallOutValidator(vldShatGmar.ID, "vldExCmpSdNxtHour" + iIndex, "");
            //hCell.Controls.Add(vldShatGmar);
            //hCell.Controls.Add(vldExSG);

          
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
        try
        {
            arrApprovalKeys = new int[iArrControlSize];
            for (int i = 0; i < iArrControlSize; i++)
            {
                arrApprovalKeys[i] = int.Parse(dr[i]["kod_ishur"].ToString());
            }

            return arrApprovalKeys;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected int[] FillApprovalKeysShatHatchala()
    //{
    //    int[] arrApprovalKeys;
    //    int iArrControlSize = 4;

    //    arrApprovalKeys = new int[iArrControlSize];
    //    arrApprovalKeys[0] = 1;
    //    arrApprovalKeys[1] = 101;
    //    arrApprovalKeys[2] = 3;
    //    arrApprovalKeys[3] = 301;

    //    return arrApprovalKeys;
    //}
    //protected int[] FillApprovalKeysShatGmar()
    //{
    //    int[] arrApprovalKeys;
    //    int iArrControlSize = 4;

    //    arrApprovalKeys = new int[iArrControlSize];
    //    arrApprovalKeys[0] = 1;
    //    arrApprovalKeys[1] = 102;
    //    arrApprovalKeys[2] = 3;
    //    arrApprovalKeys[3] = 302;

    //    return arrApprovalKeys;
    //}
    //protected int[] FillApprovalKeysChariga()
    //{
    //    int[] arrApprovalKeys;
    //    int iArrControlSize = 2;

    //    arrApprovalKeys = new int[iArrControlSize];
    //    arrApprovalKeys[0] = 2;
    //    arrApprovalKeys[1] = 4;
        
    //    return arrApprovalKeys;
    //}
    protected void SetSidurStartHourParameters(clSidur oSidur, ref string sShatHatchalaMutert, ref string sShatGmarMutert)
    {
        //מחזיר את מגבלות שעת ההתחלה
        sShatHatchalaMutert = Param1.ToShortTimeString();
        sShatGmarMutert = Param93.ToShortTimeString();
        if (oSidur.bSidurMyuhad)
        {
            if (!String.IsNullOrEmpty(oSidur.sShatHatchalaMuteret))
            {
                sShatHatchalaMutert = oSidur.sShatHatchalaMuteret;
            }            
            if (!String.IsNullOrEmpty(oSidur.sShatGmarMuteret))
            {
                sShatGmarMutert = oSidur.sShatGmarMuteret;
            }            
        }
    }
    protected void SetSidurEndHourParameters(clSidur oSidur, ref string sShatGmarMutertStart, ref string sShatGmarMutertEnd, DataRow[] drSugSidur)
    {
        //מחזיר את מגבלות שעת גמר
        //פרמטרים כללים
        sShatGmarMutertStart = Param1.ToShortTimeString();
        if (oSidur.bSidurMyuhad)             
        {
            if (!String.IsNullOrEmpty(oSidur.sShatGmarMuteret))
            {
                sShatGmarMutertEnd = oSidur.sShatGmarMuteret;
            }
            else
            {
                if (oSidur.bSidurNahagut)
                {
                    sShatGmarMutertEnd = Param80.ToShortTimeString();
                }
                else
                {
                    sShatGmarMutertEnd = Param3.ToShortTimeString();
                }
            }
        }
        else// סידור רגיל
        {
            if (!oSidur.bSidurMyuhad)
            {
                //אם סידור נהגות
                if (drSugSidur.Length > 0)
                {
                    if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                    {
                        sShatGmarMutertEnd = Param80.ToShortTimeString();
                    }
                    else
                    {
                        sShatGmarMutertEnd = Param3.ToShortTimeString();
                    }
                }
                else
                {
                    sShatGmarMutertEnd = Param3.ToShortTimeString();
                }
            }
        }
        
        //אם קיימים ערכים של סידור הם חזקים יותר מפרמטרים כללים

    }
    //protected void  btnImage_Click(object sender, EventArgs e)
    //{

    //}
    protected void CreateSidurHeader()
    {
        HtmlTableRow hRow = new HtmlTableRow();
        HtmlTableCell hCell;
       
        try
        {       
            tbSidurimHeader.Attributes.Add("class", "Grid");
            tbSidurimHeader.Attributes.Add("width", "985px");   
            tbSidurimHeader.CellPadding=1;
            tbSidurimHeader.CellSpacing=1;
            tbSidurimHeader.EnableViewState = false;

            hRow.Attributes.Add("class", "WCard_collapse_header");            
            tbSidurimHeader.Rows.Add(hRow);

            hCell = CreateTableCell("32px", "CellListView", "הוסף");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("69px", "CellListView", "מספר סידור");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("47px",  "CellListView", "התחלה");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("46px",  "CellListView", "גמר");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("83px", "CellListView", "אי החתמה התחל'");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("87px", "CellListView", "אי החתמה גמר");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("58px", "CellListView", "תש' התחלה");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("57px", "CellListView", "תש' גמר");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("100px", "CellListView", "חריגה");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("92px", "CellListView", "פיצול/ הפסקה");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("97px", "CellListView", "השלמה");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("46px", "CellListView", "מ.ל.");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("49px", "CellListView", "לא לתש'");
            hRow.Controls.Add(hCell);

            hCell = CreateTableCell("23px", "CellListView", "פעיל");
            hRow.Controls.Add(hCell);
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected AjaxControlToolkit.AlwaysVisibleControlExtender AddAlwaysVisibleControlExtender(string ID)
    {
        AjaxControlToolkit.AlwaysVisibleControlExtender oAlwaysVisibleControl = new AjaxControlToolkit.AlwaysVisibleControlExtender();
        //oAlwaysVisibleControl.ID = "AlwaysVisibleControl";
        oAlwaysVisibleControl.TargetControlID = ID;
        oAlwaysVisibleControl.VerticalSide = AjaxControlToolkit.VerticalSide.Top;
        //oAlwaysVisibleControl.VerticalOffset =  "10";
        oAlwaysVisibleControl.HorizontalSide = AjaxControlToolkit.HorizontalSide.Right;
        //oAlwaysVisibleControl.HorizontalOffset = "10";
        //oAlwaysVisibleControl.ScrollEffectDuration = ".1";
        return oAlwaysVisibleControl;
    }
    protected HtmlTableCell CreateTableCell(string sWidth,string sClass, string sText)
    {
        HtmlTableCell hCell = new HtmlTableCell();
        string sHeight = "20px";
        if (!String.IsNullOrEmpty(sHeight))
        {
            hCell.Attributes.Add("Height", sHeight);
        }
        if (!String.IsNullOrEmpty(sWidth))
        {
            hCell.Attributes.Add("width", sWidth);           
        }
        if (!String.IsNullOrEmpty(sClass))
        {
            hCell.Attributes.Add("class", sClass);
        }
        if (!String.IsNullOrEmpty(sText))
        {
            hCell.InnerHtml = sText;
        }
        //hCell.Style.Add("border-left", "solid 1px #3F3F3F");
        return hCell;
    }
    protected TableHeaderCell CreateTableCell(string sWidth, string sClass, string sText,int? iTest)
    {
        TableHeaderCell hCell = new TableHeaderCell();
        string sHeight = "20px";
        if (!String.IsNullOrEmpty(sHeight))
        {
            hCell.Attributes.Add("Height", sHeight);
        }
        if (!String.IsNullOrEmpty(sWidth))
        {
            hCell.Attributes.Add("width", sWidth);
        }
        if (!String.IsNullOrEmpty(sClass))
        {
            hCell.Attributes.Add("class", sClass);
        }
        if (!String.IsNullOrEmpty(sText))
        {
            hCell.Text = sText;
        }
       // hCell.Style.Add("border-left", "solid 1px #3F3F3F");
        return hCell;
    }
    protected void oImage_Click(object sender, EventArgs e)
    {

    }
    //protected bool IsMktToEnable(long lMakatNumber)
    //{
    //    bool bMktToEnable = true;

    //    if (lMakatNumber != 0)
    //        if (lMakatNumber.ToString().Length >= 0)
    //            bMktToEnable = (!int.Parse(lMakatNumber.ToString().Substring(0, 1)).Equals(5));               

        
    //    return bMktToEnable;
    //}

    protected void grdPeiluyot_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        int iMakatType;
        clKavim.enMakatType oMakatType;
        bool bEnabled, bSidurActive, bPeilutActive;        
        clKavim oKavim=new clKavim();
        int iSidurIndex=0;
        int iBitulOHosafa=0;
        bool bElementHachanatMechona;       
        long lMakatNumber; 
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    SetPeilutStatus(e);

                    iSidurIndex = int.Parse(e.Row.ClientID.Substring("lstSidurim_".Length, 3));
                    //bSidurActive = ((KdsBatch.clSidur)(this.DataSource[int.Parse(e.Row.ClientID.Substring("lstSidurim_".Length, 3))])).iBitulOHosafa == 0;
                    iBitulOHosafa = ((KdsBatch.clSidur)(this.DataSource[iSidurIndex])).iBitulOHosafa;
                    bSidurActive = ((iBitulOHosafa != clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) && (iBitulOHosafa != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()));
                    iBitulOHosafa = (int)DataBinder.Eval(e.Row.DataItem, "bitul_o_hosafa");
                    bPeilutActive = ((iBitulOHosafa != clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) && (iBitulOHosafa != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()));
                    lMakatNumber = long.Parse(((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0])).Text);
                    bElementHachanatMechona = (IsElementHachanatMechona(lMakatNumber));
                   // bMktTypeToEnable = IsMktToEnable(lMakatNumber);


                    TextBox oTxt = new TextBox();
                    oTxt.Text = ((!bPeilutActive).GetHashCode().ToString());
                    e.Row.Cells[_COL_CANCEL_PEILUT].Controls.Add(oTxt);

                    //עמודה שתכיל 0/1 האם ניתן לבטל פעילות או לא
                    ((TextBox)(e.Row.Cells[_COL_DUMMY].Controls[0])).Style["Display"] = "none";

                    SetShatYetiza(e, bSidurActive, bPeilutActive, iSidurIndex, bElementHachanatMechona);

                    iMakatType =  int.Parse(DataBinder.Eval(e.Row.DataItem, "makat_type").ToString());
                    oMakatType = (clKavim.enMakatType)iMakatType;
                    bEnabled = ((oMakatType == clKavim.enMakatType.mKavShirut) || (oMakatType==clKavim.enMakatType.mNamak));
                    
                    //הוספת נסיעה ריקה
                    SetAddNesiaRekaColumn(e, bSidurActive, bPeilutActive, bElementHachanatMechona, oMakatType, iSidurIndex);

                    //כיסוי תור
                    SetKisuyTor(e, bEnabled, bSidurActive, bPeilutActive, iSidurIndex, bElementHachanatMechona);

                    //תיאור
                    SetDescription(e,iSidurIndex, lMakatNumber,ref oKavim);

                    //מספר רכב          
                    SetCarNumber(e, bSidurActive, bPeilutActive, iSidurIndex, bElementHachanatMechona, oMakatType);

                    //מספר מקט
                    SetMakatNumber(e, bSidurActive, bPeilutActive, iSidurIndex, bElementHachanatMechona, ref oMakatType);

                    //דקות הגדרה
                    SetDefMinutes(e, ref oKavim);

                    //דקות בפועל
                    SetActualMinutes(e, bSidurActive, bPeilutActive, oMakatType, bElementHachanatMechona);

                    //פעיל            
                    SetCancelColumn(e, bSidurActive, bPeilutActive, bElementHachanatMechona );

                    SetDayToAdd(e, iSidurIndex);


                    //Remove view state for readonly fields
                    e.Row.Cells[_COL_LINE_DESCRIPTION].EnableViewState = false;
                    e.Row.Cells[_COL_LINE_TYPE].EnableViewState = false;
                    e.Row.Cells[_COL_LINE].EnableViewState = false;
                    //e.Row.Cells[_COL_CAR_LICESNCE_NUMBER].EnableViewState = false;
                    e.Row.Cells[_COL_DEF_MINUTES].EnableViewState = false;
                    e.Row.Cells[_COL_NETZER].EnableViewState = false;
                    e.Row.Cells[_COL_DAY_TO_ADD].EnableViewState = false;
                    e.Row.Cells[_COL_KISUY_TOR_MAP].EnableViewState = false;
                   
                    break;
                case DataControlRowType.Header:
                    for (int i = 0; i < ((GridView)sender).Columns.Count; i++)
                    {
                        e.Row.Cells[i].EnableViewState = false;
                    }
                    break;
                case DataControlRowType.Footer:
                   // btn=AddInputButton("הוסף/חפש פעילות", "ImgButtonUpdate");
                   // e.Row.Cells[_COL_LINE_DESCRIPTION].Controls.Add(btn);
                   //((Button)e.Row.Cells[_COL_LINE_DESCRIPTION].Controls[0]).Attributes.Add("onclick", "AddPeilut(" + e.Row.ClientID.Substring("lstSidurim_".Length, 3) + ")");
                    //e.Row.Cells[_COL_LINE_TYPE].Controls.Add(AddInputButton("שדות נוספים לסידור", "ImgButtonUpdate"));
                    //((Button)e.Row.Cells[_COL_LINE_TYPE].Controls[0]).Attributes.Add("onclick", "(" + e.Row.ClientID.Substring("lstSidurim_".Length, 3) + ")");
                    break;
            }
            e.Row.Cells[_COL_LAST_UPDATE].Style["display"] = "none";
            e.Row.Cells[_COL_MAZAN_TASHLUM].Style["display"] = "none";
            e.Row.Cells[_COL_CANCEL_PEILUT].Style["display"] = "none";
            e.Row.Cells[_COL_KNISA].Style["display"] = "none";
            e.Row.Cells[_COL_DAY_TO_ADD].Style["display"] = "none";
            e.Row.Cells[_COL_KISUY_TOR_MAP].Style["display"] = "none";
            e.Row.Cells[_COL_PEILUT_STATUS].Style["display"] = "none";
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetDayToAdd(GridViewRowEventArgs e, int iSidurIndex)
    {
        TextBox _TextBox = new TextBox();
        DateTime dtShatYetiza;
        dtShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "shat_yetzia").ToString());

        e.Row.Cells[_COL_DAY_TO_ADD].Controls.Add(_TextBox);
        ((TextBox)e.Row.Cells[_COL_DAY_TO_ADD].Controls[0]).ID = "txtDayToAdd";        
        ((TextBox)e.Row.Cells[_COL_SHAT_YETIZA].Controls[0]).Attributes.Add("onChange", "SetDay('2|" + e.Row.ClientID + "|" + iSidurIndex + "');");
        ((TextBox)e.Row.Cells[_COL_SHAT_YETIZA].Controls[0]).ToolTip = "תאריך שעת היציאה הוא: " + dtShatYetiza.ToShortDateString();
        ((TextBox)e.Row.Cells[_COL_DAY_TO_ADD].Controls[0]).Text = ((dtShatYetiza.Date==CardDate.Date) || (dtShatYetiza.Year<clGeneral.cYearNull)) ? "0" : "1";
    
    }
    protected void SetPeilutStatus(GridViewRowEventArgs e )
    {
        TextBox _TextBox = new TextBox();
      
        e.Row.Cells[_COL_PEILUT_STATUS].Controls.Add(_TextBox);
       // ((TextBox)e.Row.Cells[_COL_PEILUT_STATUS].Controls[0]).Text = 

    }
    protected Button AddInputButton(string sText,string sCssClass)
    {
        try
        {
            Button _Button = new Button();
            _Button.Text = sText;
            _Button.Attributes.Add("class", sCssClass);
            //e.Row.Cells[iCol].Controls.Add(_Button);
            // oControl.Controls.Add(_Button);
            // _Button.Click += new EventHandler(_Button_Click);
            _Button.CausesValidation = false;
            _Button.Style.Add("height", "25px");
            //_Button.Click += new  ClickEventHandler(_Button_Click); 
            //        ((Button)e.Row.Cells[iCol].Controls[0]).Attributes.Add("onclick", sFunctionName + "(" + e.Row.ClientID.Substring("lstSidurim_".Length, 3) + ")");
            return _Button;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected DataRow BuildSidurDataRow(string[] arrItems)
    //{
    //    DataRow dr;

    //    //dtSidurim.NewRow();
    //    //dtSidurim[""]
    //}
    protected void AddItemToSidurimDataTable(string sItem)
    {
       // string[] arrItems;
       //// arrItems = sItem.Split[char.Parse(",")];

       // if (arrItems != null)
       // {
       //    // arrItems[clGeneral.enNetuneyPeilut.KisuyTorShaa];
       // }

    }
    protected void _Button_Click(object sender, EventArgs e)
    {    
        if (hidItmAddKey.Value != string.Empty)
        {
           
        //    AddItemToSidurimDataTable(_DataSource);
        }
    }
    protected bool IsElementHachanatMechona(long lMakatNesia)
    {
        bool bHachanatMechona = false;
        int iElementType;
        if (lMakatNesia != 0)
        {
            if (lMakatNesia.ToString().Length >= 3)
            {
                iElementType = int.Parse(lMakatNesia.ToString().Substring(0, 3));
                bHachanatMechona = (iElementType == clGeneral.enElementHachanatMechona.Element701.GetHashCode()) || (iElementType == clGeneral.enElementHachanatMechona.Element711.GetHashCode() || (iElementType == clGeneral.enElementHachanatMechona.Element712.GetHashCode()));
            }
        }
        return bHachanatMechona;
    }
    protected bool IsMakatHasActualMinPremmision(clKavim.enMakatType oMakatType, int iMisparKnisa, int iKnisaType)
    {
        bool bEnabled = false;
        try
        {
            if ((oMakatType == clKavim.enMakatType.mKavShirut) || (oMakatType == clKavim.enMakatType.mNamak) || (oMakatType == clKavim.enMakatType.mEmpty))
                if ((iMisparKnisa == 0) || ((iMisparKnisa > 0) && (iKnisaType == 1)))
                    bEnabled = true;
            
            return bEnabled;
        }            
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetActualMinutes(GridViewRowEventArgs e, bool bSidurActive, bool bPeilutActive,clKavim.enMakatType oMakatType,bool bElementHachanatMechona)
    {
        string sID, sMessage, sTargetControlId, sClientScriptFunction;
        TextBox oTxt = ((TextBox)(e.Row.Cells[_COL_ACTUAL_MINUTES].Controls[0]));
        AjaxControlToolkit.FilteredTextBoxExtender oFilterTextBox;
        CustomValidator vldCustomValidator;
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut;
        bool bIdkunRashemet;
        DateTime dShatYetiza;
        int iMisparKnisa, iKnisaType;
        string[] arrKnisaVal; //מספר כניסה ןסוג כניסה
        
        try
        {
            oTxt.CausesValidation = true;
            oTxt.MaxLength = MAX_LEN_ACTUAL_MINTUES;
            oTxt.Width = Unit.Pixel(40);
            oTxt.ID = e.Row.ClientID + "ActualMinutes";
            dShatYetiza = DateTime.Parse(CardDate.ToShortDateString() + " " + ((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0])).Text);
            arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);//int.Parse(e.Row.Cells[_COL_KNISA].Text);
            iKnisaType = int.Parse(arrKnisaVal[1]);
            bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelPeilut, clUtils.GetPakadId(dtPakadim, "DAKOT_BAFOAL"), MisparSidur, DateTime.Parse(CardDate.ToShortDateString() + " " + ShatHatchala), dShatYetiza, iMisparKnisa);
            bool bEnabled = (!bIdkunRashemet);
            

            oTxt.Enabled = ((!bElementHachanatMechona) && (bSidurActive) && (bPeilutActive) && (!bIdkunRashemet) && (IsMakatHasActualMinPremmision(oMakatType, iMisparKnisa, iKnisaType)));
            oTxt.Attributes.Add("OrgEnabled", oTxt.Enabled.GetHashCode().ToString());
            oTxt.Attributes.Add("onfocus", "SetFocus('" + e.Row.ClientID + "'," + _COL_ACTUAL_MINUTES + ");");
            oTxt.Attributes.Add("onkeypress", "SetBtnChanges();");
            //AddAttribute(oTxt, "OldV", oTxt.Text);
            sTargetControlId = oTxt.ID;
            sID = "defMin";
            oFilterTextBox = AddFilterTextBoxExtender(sTargetControlId, sID, "0123456789", AjaxControlToolkit.FilterModes.ValidChars, AjaxControlToolkit.FilterTypes.Numbers, e);
            e.Row.Cells[_COL_ACTUAL_MINUTES].Controls.Add(oFilterTextBox);

            //Add CustomeValidator
            if ((iMisparKnisa > 0) && (iKnisaType == 1))
                sMessage = "יש להקליד ערך בין 0 דקות לבין " + Param98.ToString() + " דקות";
                else
                sMessage = "יש להקליד ערך בין 0 דקות לבין " + (e.Row.Cells[_COL_MAZAN_TASHLUM].Text) + " דקות";
            sID = "vldAMin";
            sClientScriptFunction = "IsAMinValid";
            vldCustomValidator = AddCustomValidator(sTargetControlId, sMessage, sID, sClientScriptFunction, e.Row.ClientID, e.Row.ClientID);
            e.Row.Cells[_COL_ACTUAL_MINUTES].Controls.Add(vldCustomValidator);

            //Add Ajax CallOutCustomeValidator
            sTargetControlId = ((CustomValidator)(e.Row.Cells[_COL_ACTUAL_MINUTES].Controls[2])).ID;
            sID = "vldExActualMinutes";
            vldExtenderCallOut = AddCallOutValidator(sTargetControlId, sID, e.Row.ClientID, AjaxControlToolkit.ValidatorCalloutPosition.Right);
            e.Row.Cells[_COL_ACTUAL_MINUTES].Controls.Add(vldExtenderCallOut);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetDescription(GridViewRowEventArgs e, int iSidurIndex, long lMakatNumber, ref clKavim oKavim)
    {
        clKavim.enMakatType _MakatType;
        string[] arrKnisaVal;
        arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
        int iMisparKnisa = int.Parse(arrKnisaVal[0]);

        e.Row.Cells[_COL_LINE_DESCRIPTION].Width = Unit.Pixel(300);
        _MakatType = ((clKavim.enMakatType)oKavim.GetMakatType(lMakatNumber));
        if ((_MakatType == clKavim.enMakatType.mKavShirut) && (iMisparKnisa==0))
        {
            HyperLink lnkKnisa = new HyperLink();
            lnkKnisa = AddHyperLink("lblKnisa" + iSidurIndex + lMakatNumber, e.Row.Cells[_COL_LINE_DESCRIPTION].Text, "AddHosafatKnisot(" + iSidurIndex + "," + e.Row.ClientID + ");");
            e.Row.Cells[_COL_LINE_DESCRIPTION].Controls.Add(lnkKnisa);
        }
        //e.Row.Cells[_COL_LINE_DESCRIPTION].ToolTip = e.Row.Cells[_COL_LINE_DESCRIPTION].Text;
    }
    protected void SetDefMinutes(GridViewRowEventArgs e, ref clKavim oKavim)
    {
        long lmakat;
        //int iMisparKnisa = int.Parse(e.Row.Cells[_COL_KNISA].Text);
        clKavim.enMakatType _MakatType;
        string[] arrKnisaVal;
        int iMisparKnisa;
        arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
        iMisparKnisa = int.Parse(arrKnisaVal[0]);
        int iTime;
        try
        {
            //עבור שירות (מק"ט שירות שאינו כניסה  (mispar_knisa=0, ריקה, נמ"ק לקבלת נתון זה יש לפנות לתנועה:
            //GetKavDetails.MazanTichnun  (עבור קו שירות) GetRekaDetails.MazanTichnun   (עבור ריקה) GetNamakDetails.MazanTichnun   (עבור נמ"ק)  
            //עבור אלמנט (כולל ויסות 700xxxxx) - יש להציג 0.

            lmakat = long.Parse(((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0])).Text);
            //iMisparKnisa = int.Parse(e.Row.Cells[_COL_KNISA].Text);
            _MakatType = ((clKavim.enMakatType)oKavim.GetMakatType(lmakat));
           
            //אם אלמנט זמן מסוג ריקה
            //אלמנט מסוג ריקה (מאפיין 23 = 1)  - להציג 
            //בעמודה ערך מפוזיציות 4-6 * פרמטר 43.
            if (((_MakatType == clKavim.enMakatType.mElement)
                  && (IsMeafyenExistsInElement(lmakat,clGeneral.enMeafyenElementim.Meafyen23.GetHashCode(), 
                   clGeneral.enMeafyenElementim23.ElementTimeNesiaReka.GetHashCode().ToString()))))
            {
                iTime =int.Parse(lmakat.ToString().Substring(3,3));
                e.Row.Cells[_COL_DEF_MINUTES].Text = ((int)System.Math.Round((iTime * _Param43))).ToString();
                e.Row.Cells[_COL_DEF_MINUTES].ToolTip = "הגדרה לגמר היא " + e.Row.Cells[_COL_MAZAN_TASHLUM].Text + " דקות ";
            }
            else{
                ////אם אלמנט זמן מסוג נסיעה מלאה
                //// אלמנט מסוג נסיעה מלאה (מאפיין 35 = 1) - 
                ////להציג בעמודה ערך מפוזיציות 4-6 * פרמטר 42.

                //if (((_MakatType == clKavim.enMakatType.mElement)
                //      && (IsMeafyenExistsInElement(lmakat,
                //                                   clGeneral.enMeafyenElementim.Meafyen35.GetHashCode(),
                //                                   clGeneral.enMeafyenElementim35.ElementTimeNesiaMelea.GetHashCode().ToString()))))
                //{
                //    iTime = int.Parse(lmakat.ToString().Substring(3, 3));
                //    e.Row.Cells[_COL_DEF_MINUTES].Text = ((int)(iTime * _Param42)).ToString();
                //    e.Row.Cells[_COL_DEF_MINUTES].ToolTip = "הגדרה לגמר היא " + e.Row.Cells[_COL_MAZAN_TASHLUM].Text + " דקות ";
                //}
                //else
                //{
                    if ((_MakatType == clKavim.enMakatType.mElement) || (_MakatType == clKavim.enMakatType.mVisut) || (_MakatType == clKavim.enMakatType.mVisa) || (((_MakatType == clKavim.enMakatType.mKavShirut) && (iMisparKnisa != 0))))            
                     e.Row.Cells[_COL_DEF_MINUTES].Text = "0";     
       
                   else
                      {
                        if (((_MakatType == clKavim.enMakatType.mKavShirut) && (iMisparKnisa == 0)) || ((_MakatType == clKavim.enMakatType.mEmpty) || (_MakatType == clKavim.enMakatType.mNamak)))
                        {                
                            e.Row.Cells[_COL_DEF_MINUTES].Width = Unit.Pixel(55);
                            e.Row.Cells[_COL_DEF_MINUTES].ToolTip = "הגדרה לגמר היא " + e.Row.Cells[_COL_MAZAN_TASHLUM].Text + " דקות ";
                        }                                        
                    }     
                }                       
          }       

        //}
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetAddNesiaRekaColumn(GridViewRowEventArgs e, bool bSidurActive, bool bPeilutActive, bool bElementHachanatMechona, clKavim.enMakatType oMakatType, int iSidurIndex)
    {
        try
        {
            int iMisparKnisa;
            string[] arrKnisaVal;
            arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);
            int iLastRow =((System.Data.DataRowView)(e.Row.DataItem)).Row.Table.Rows.Count==e.Row.RowIndex+1 ? 1 :0;
            //if (((oMakatType == clKavim.enMakatType.mEmpty)) || (oMakatType == clKavim.enMakatType.mNamak) || ((oMakatType == clKavim.enMakatType.mKavShirut) && (iMisparKnisa == 0)))
            //{
                //Image imgAddReka = new Image();
                //imgAddReka.ID = "AddReka" + e.Row.ClientID;
                //imgAddReka.ImageUrl = "~/images/plus.jpg";
                //imgAddReka.Attributes.Add("onclick", "AddNesiaReka(" + e.Row.ClientID + "," + iSidurIndex + "," + iLastRow + ");");
                ImageButton imgAddReka = new ImageButton();
                imgAddReka.Click += new ImageClickEventHandler(imgAddReka_Click);
                if (((oMakatType == clKavim.enMakatType.mEmpty)) || (oMakatType == clKavim.enMakatType.mNamak) || ((oMakatType == clKavim.enMakatType.mKavShirut) && (iMisparKnisa == 0)))
                {
                    imgAddReka.Attributes.Add("NesiaReka", "1");
                    imgAddReka.Attributes.Add("SdrInd", iSidurIndex.ToString());
                    imgAddReka.Attributes.Add("PeilutInd", e.Row.RowIndex.ToString());
                }
                else                
                    imgAddReka.Style.Add("visibility","hidden");
                
                imgAddReka.CausesValidation = false;
                imgAddReka.ImageUrl = "~/images/plus.jpg";
                imgAddReka.ID = "AddReka" + e.Row.ClientID;
              //  imgAddReka.ID = "AddReka" + e.Row.RowIndex.ToString();
                e.Row.Cells[_COL_ADD_NESIA_REKA].Controls.Add(imgAddReka);
                //if (btnReka != null)
                //    btnReka(((System.Web.UI.Control)(e.Row)).ClientID + "_" + "AddReka" + e.Row.RowIndex.ToString(), false);
           // }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetCancelColumn(GridViewRowEventArgs e, bool bSidurActive, bool bPeilutActive, bool bElementHachanatMechona)
    {
      Button oCancelButton;
      //bool bPeilutActive;

      try
      {
          oCancelButton = AddImageButton();
          oCancelButton.OnClientClick = "if (!ChangeStatusPeilut(" + e.Row.ClientID + ",0,0,0"  + ")) {return false;} else {return true;} ";
        //  bPeilutActive = (((System.Data.DataRowView)(e.Row.DataItem)).Row["bitul_o_hosafa"].ToString() == "0");
          oCancelButton.CssClass = bPeilutActive ? "ImgChecked" : "ImgCancel";
          //oCancelButton.Attributes.Add("cancel", (!bPeilutActive).GetHashCode().ToString());
          e.Row.Cells[_COL_CANCEL].Controls.Add(oCancelButton);
          oCancelButton = ((Button)(e.Row.Cells[_COL_CANCEL].Controls[0]));

          oCancelButton.ID = e.Row.ClientID + "CancelPeilut";
          bool bDisabled = (((((TextBox)(e.Row.Cells[_COL_DUMMY].Controls[0])).Text != "1")) || (!bSidurActive) || (!_ProfileRashemet) || (bElementHachanatMechona));

          if (bDisabled)
          {
              oCancelButton.Attributes.Add("disabled", "true");
              oCancelButton.Attributes.Add("OrgEnabled", "0");
          }
          else
          {
              oCancelButton.Attributes.Add("OrgEnabled", "1");
          }
         
          oCancelButton.ToolTip = e.Row.Cells[_COL_LAST_UPDATE].Text == "&nbsp;" ? "" : e.Row.Cells[_COL_LAST_UPDATE].Text;
      }
      catch (Exception ex)
      {
          throw ex;
      }
    }
    protected void SetMakatNumber(GridViewRowEventArgs e, bool bSidurActive, bool bPeilutActive, int iSidurIndex, 
                                 bool bElementHachanatMechona,  ref clKavim.enMakatType oMakatType)
    {
        string sID, sMessage, sTargetControlId, sClientScriptFunction;
        TextBox oTxt;
        AjaxControlToolkit.FilteredTextBoxExtender oFilterTextBox;
        CustomValidator vlMakatNumber;
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut;
        DateTime dShatYetiza, dOldShatYetiza;        
        HtmlTableCell hCell = new HtmlTableCell();
        bool bIdkunRashemet=false;
        int iMisparKnisa;

        try
        {
            dOldShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "old_shat_yetzia").ToString());
            oTxt = ((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0]));
            oTxt.CausesValidation = true;
            oTxt.Attributes.Add("onchange", "chkMkt(" + e.Row.Cells[_COL_MAKAT].ClientID + "," + e.Row.Cells[_COL_MAKAT].ClientID + ");");
            
            oTxt.Attributes.Add("onkeypress", "SetBtnChanges();");
            oTxt.Attributes.Add("onfocus", "SetFocus('" + e.Row.ClientID + "'," + _COL_MAKAT + ");");
            oTxt.MaxLength = MAX_LEN_LINE_NUMBER;
            oTxt.Width = Unit.Pixel(70);
            oTxt.Attributes.Add("OrgMakat", oTxt.Text);            
            oTxt.ID = e.Row.ClientID + "MakatNumber";

            //AddAttribute(oTxt, "OldV", oTxt.Text);
            sTargetControlId = ((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0])).ID;
            sID = "vldFilterMakatNumber";
            oFilterTextBox = AddFilterTextBoxExtender(sTargetControlId, sID, "0123456789", AjaxControlToolkit.FilterModes.ValidChars, AjaxControlToolkit.FilterTypes.Numbers, e);
            e.Row.Cells[_COL_MAKAT].Controls.Add(oFilterTextBox);
            //Add CustomeValidator
            sMessage = "מספר מק'ט שגוי";
            sID = "vldMakatNum";
            sClientScriptFunction = "";
            vlMakatNumber = AddCustomValidator(sTargetControlId, sMessage, sID, sClientScriptFunction, e.Row.ClientID, e.Row.ClientID);
            e.Row.Cells[_COL_MAKAT].Controls.Add(vlMakatNumber);

            //Add Ajax CallOutCustomeValidator
            sTargetControlId = ((CustomValidator)(e.Row.Cells[_COL_MAKAT].Controls[2])).ID;
            sID = "vldExMakatNum";
            vldExtenderCallOut = AddCallOutValidator(sTargetControlId, sID, e.Row.ClientID, AjaxControlToolkit.ValidatorCalloutPosition.Right);
            vldExtenderCallOut.BehaviorID = "vldMakatNumBeh" + e.Row.ClientID;
            e.Row.Cells[_COL_MAKAT].Controls.Add(vldExtenderCallOut);
            //dShatYetiza = DateTime.Parse(CardDate.ToShortDateString() + " " + ((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0])).Text);
           
            dShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "shat_yetzia").ToString());
            string sPeilutKey = string.Concat(((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0])).ClientID, "|", e.Row.Cells[_COL_KNISA].ClientID, "|", iSidurIndex, "|", e.Row.ClientID);
            string[] arrKnisaVal;
            arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);

            bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelPeilut, clUtils.GetPakadId(dtPakadim, "MAKAT_NO"), MisparSidur, FullShatHatchala, dShatYetiza, iMisparKnisa);
            bool bEnabled = ((!bIdkunRashemet) && (!bElementHachanatMechona) && (oMakatType != clKavim.enMakatType.mVisa) && (oMakatType != clKavim.enMakatType.mVisut));
            ((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0])).Attributes.Add("OrgEnabled", bEnabled.GetHashCode().ToString());
            ((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0])).Enabled = ((bSidurActive) && (bPeilutActive) && (bEnabled));            
            
            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Error:
                    if (SetOneError(oTxt, hCell, MisparIshi, CardDate, MisparSidur, _FullOldShatHatchala, dOldShatYetiza, iMisparKnisa, sPeilutKey, "Makat_nesia"))
                    {
                        ((TextBox)e.Row.Cells[_COL_PEILUT_STATUS].Controls[0]).Text = enPeilutStatus.enError.GetHashCode().ToString();
                    }
                    break;
                //case clGeneral.enCardStatus.Valid:
                //    //במידה והכרטיס תקין נבדוק אישורים
                //    DataRow[] dr = dtApprovals.Select("mafne_lesade='Makat_nesia'");
                //    DateTime dSidurShatHatchala = new DateTime();
                //    dSidurShatHatchala = DateTime.Parse(string.Concat(CardDate.ToShortDateString(), " ", ShatHatchala));
                //    if (CheckIfApprovalExists(FillApprovalKeys(dr), MisparSidur, dSidurShatHatchala, dShatYetiza, long.Parse(oTxt.Text), ref oTxt))
                //    {
                //        ((TextBox)e.Row.Cells[_COL_PEILUT_STATUS].Controls[0]).Text = enPeilutStatus.enApproved.GetHashCode().ToString();
                //    }
                //    break;
            }            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetCarNumber(GridViewRowEventArgs e, bool bSidurActive, bool bPeilutActive, int iSidurIndex,
                                bool bElementHachanatMechona,
                                clKavim.enMakatType oMakatType)
    {
        string sID, sMessage, sTargetControlId, sClientScriptFunction;
        TextBox oTxt;
        AjaxControlToolkit.FilteredTextBoxExtender oFilterTextBox;
        CustomValidator vldCarNumber;
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut;
        DateTime dShatYetiza, dOldShatYetiza;
        bool  bIdkunRashemet=false;
        String sPeilutKey;
        HtmlTableCell hCell = new HtmlTableCell();
       
        //int iMisparKnisa;

        try
        {
            oTxt = ((TextBox)(e.Row.Cells[_COL_CAR_NUMBER].Controls[0]));
            oTxt.CausesValidation = true;

            oTxt.ID = e.Row.ClientID + "CarNumber";
            sTargetControlId = oTxt.ID;
            oTxt.MaxLength = MAX_LEN_CAR_NUMBER;
            oTxt.Width = Unit.Pixel(60);
            //oTxt.Attributes.Add("onclick", "ChkOto(" + e.Row.Cells[_COL_CAR_NUMBER].ClientID + ")");

            //oTxt.Attributes.Add("onkeypress", "ChkOto(" + e.Row.Cells[_COL_CAR_NUMBER].ClientID + ")");
           // oTxt.Attributes.Add("onkeyup", "CarKeyUp(" + e.Row.Cells[_COL_CAR_NUMBER].ClientID + ")");
            //oTxt.Attributes.Add("onchange", "CopyOtoNum(" + e.Row.Cells[_COL_CAR_NUMBER].ClientID + ")");
            oTxt.Attributes.Add("onchange", "SetBtnChanges();");
            oTxt.Attributes.Add("onkeyup", "ChkOto(" + e.Row.Cells[_COL_CAR_NUMBER].ClientID + ");SetBtnChanges();");
          //  oTxt.Attributes.Add("onchange", "CopyOtoNum(" + e.Row.Cells[_COL_CAR_NUMBER].ClientID + ")");
            oTxt.Attributes.Add("onfocus", "SetFocus('" + e.Row.ClientID + "'," + _COL_CAR_NUMBER + ");");
            oTxt.ToolTip = (DataBinder.Eval(e.Row.DataItem, "license_number").ToString());
            AddAttribute(oTxt, "OldV",DataBinder.Eval(e.Row.DataItem, "old_oto_no").ToString());//AddAttribute(oTxt, "OldV", oTxt.Text);

            sID = "vldFilCarNum";
            oFilterTextBox = AddFilterTextBoxExtender(sTargetControlId, sID, "0123456789", AjaxControlToolkit.FilterModes.ValidChars, AjaxControlToolkit.FilterTypes.Numbers, e);
            e.Row.Cells[_COL_CAR_NUMBER].Controls.Add(oFilterTextBox);

            //Add CustomeValidator
            sMessage = "מספר רכב שגוי";
            sID = "vldCarNum";
            sClientScriptFunction = "Test";
            vldCarNumber = AddCustomValidator(sTargetControlId, sMessage, sID, sClientScriptFunction, e.Row.ClientID, e.Row.ClientID);
            e.Row.Cells[_COL_CAR_NUMBER].Controls.Add(vldCarNumber);

            //Add Ajax CallOutCustomeValidator
            sTargetControlId = ((CustomValidator)(e.Row.Cells[_COL_CAR_NUMBER].Controls[2])).ID;
            sID = "vldExCarNumber";
            vldExtenderCallOut = AddCallOutValidator(sTargetControlId, sID, e.Row.ClientID, AjaxControlToolkit.ValidatorCalloutPosition.Right);
            vldExtenderCallOut.BehaviorID = "vldCarNumBehv" + e.Row.ClientID;
            e.Row.Cells[_COL_CAR_NUMBER].Controls.Add(vldExtenderCallOut);

            //Check if Error Exists
            oTxt = ((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0]));
            sPeilutKey = string.Concat(oTxt.ClientID, "|", e.Row.Cells[_COL_KNISA].ClientID, "|", iSidurIndex, "|", e.Row.ClientID);
            dShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "shat_yetzia").ToString());
            dOldShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "old_shat_yetzia").ToString());
            oTxt = ((TextBox)(e.Row.Cells[_COL_CAR_NUMBER].Controls[0]));
            string[] arrKnisaVal;
            arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
            int iMisparKnisa = int.Parse(arrKnisaVal[0]);
            bool bBusMustOtoNumber = arrKnisaVal[2]=="1"? true : false;
            //אם לא קיים מאפיין 11
            //לא דרוש מספר רכב, נחסום ונמחוק את השדהS
            //רק לאלמנטים
            if ((!bBusMustOtoNumber) && (oMakatType== clKavim.enMakatType.mElement)){
                ((TextBox)(e.Row.Cells[_COL_CAR_NUMBER].Controls[0])).Text = "";
                //((TextBox)(e.Row.Cells[_COL_CAR_NUMBER].Controls[0])).Attributes["OldV"] = "";
            }
                
            long lMakat = long.Parse(((TextBox)(e.Row.Cells[_COL_MAKAT].Controls[0])).Text);
            oTxt.Attributes.Add("MustOtoNum", IsPeilutMustOtoNumber(lMakat, bBusMustOtoNumber).GetHashCode().ToString()); 
            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Error:
                    if (SetOneError(oTxt, hCell, MisparIshi, CardDate, MisparSidur, _FullOldShatHatchala, dOldShatYetiza, iMisparKnisa, sPeilutKey, "Oto_no"))
                    {
                        ((TextBox)e.Row.Cells[_COL_PEILUT_STATUS].Controls[0]).Text = enPeilutStatus.enError.GetHashCode().ToString();
                    }
                    break;
            }
            //iMisparKnisa = int.Parse(e.Row.Cells[_COL_KNISA].Text);
            bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelPeilut, clUtils.GetPakadId(dtPakadim, "OTO_NO"), MisparSidur, FullShatHatchala, dShatYetiza, iMisparKnisa);
            oTxt = ((TextBox)(e.Row.Cells[_COL_CAR_NUMBER].Controls[0]));
            bool bEnabled = ((!bIdkunRashemet) && (!bElementHachanatMechona) && ((((bBusMustOtoNumber) && (oMakatType == clKavim.enMakatType.mElement)) || (oMakatType != clKavim.enMakatType.mElement))));
            oTxt.Attributes.Add("OrgEnabled", bEnabled.GetHashCode().ToString());
            oTxt.Enabled = ((bSidurActive) && (bPeilutActive) && (bEnabled));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void SetShatYetiza(GridViewRowEventArgs e, bool bSidurActive, bool bPeilutActive, int iSidurIndex, bool bElementHachanatMechona)
    {
        int iKisuyTor, iKisuyTorMap;
        string sTmp, sID, sMessage, sTargetControlId, sClientScriptFunction, sShatYetiza;
        TextBox oTxt;
        AjaxControlToolkit.MaskedEditExtender oMaskEx;
        CustomValidator vldCustomValidator;
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut;
        bool bIdkunRashemet;
        DateTime dShatYetiza, dOldShatYetiza;
        int iMisparKnisa;
        HtmlTableCell hCell = new HtmlTableCell();
        string[] arrKnisaVal;
        
        try
        {
            //חישוב כיסוי תור
            oTxt = ((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0]));
            dShatYetiza = DateTime.Parse(oTxt.Text);
            arrKnisaVal = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);
            oTxt.Width = Unit.Pixel(40);

            dOldShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "old_shat_yetzia").ToString());
            //AddAttribute(oTxt, "OldV", dOldShatYetiza.ToShortTimeString());//dShatYetiza.ToShortTimeString());
            iKisuyTor = String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "kisuy_tor").ToString()) ? 0 : int.Parse(DataBinder.Eval(e.Row.DataItem, "kisuy_tor").ToString());
            iKisuyTorMap = String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "kisuy_tor_map").ToString()) ? 0 : int.Parse(DataBinder.Eval(e.Row.DataItem, "kisuy_tor_map").ToString());
            sShatYetiza = DataBinder.Eval(e.Row.DataItem, "shat_yetzia").ToString();
            if ((!String.IsNullOrEmpty(sShatYetiza)) && (dShatYetiza.Year>clGeneral.cYearNull))
            {
                sTmp = DateTime.Parse(sShatYetiza).ToShortTimeString();
                oTxt.Text = sTmp;
                oTxt.Attributes.Add("onkeypress", "SetBtnChanges();"); //SetLvlChg(3);
                if (iKisuyTor == 0){                
                    ((TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0])).Text = "";
                }
                else{                
                    ((TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0])).Text = dShatYetiza.AddMinutes(-iKisuyTor).ToShortTimeString();
                    //dShatYetiza.AddMinutes(-iKisuyTorMap).ToShortTimeString();
                }
                e.Row.Cells[_COL_KISUY_TOR_MAP].Text = iKisuyTorMap.ToString();
                oTxt.Attributes.Add("OrgShatYetiza", dOldShatYetiza.ToString());//dShatYetiza.ToString());
                AddAttribute(oTxt, "OrgDate", dOldShatYetiza.ToShortDateString());//dShatYetiza.ToShortDateString());               
            }
            else
            {
                oTxt.Text = "";
                oTxt.Attributes.Add("OrgShatYetiza", sShatYetiza);//sShatYetiza);
                AddAttribute(oTxt, "OrgDate", "01/01/0001");  
                ((TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0])).Text = "";
            }

            oTxt.CausesValidation = true;
            bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, 
                                           clWorkCard.ErrorLevel.LevelPeilut, clUtils.GetPakadId(dtPakadim, "SHAT_YETIZA"), MisparSidur,
                                           FullShatHatchala, dShatYetiza, iMisparKnisa);

            bool bEnabled = ((!bIdkunRashemet) && (!bElementHachanatMechona) && (iMisparKnisa==0));
            oTxt.Attributes.Add("OrgEnabled", bEnabled.GetHashCode().ToString());
            //oTxt.Enabled = ((bSidurActive) && (bPeilutActive) && (!bIdkunRashemet));
            oTxt.ID = e.Row.ClientID + "ShatYetiza";
            //oTxt.Attributes.Add("onblur", "SetDay('" + oTxt.ClientID + "');");
            sTargetControlId = oTxt.ID;
            oMaskEx = AddTimeMaskedEditExtender(sTargetControlId, e.Row.RowIndex, "99:99", "PeilutShatYetiza", 
                                                AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Left);
            e.Row.Cells[_COL_SHAT_YETIZA].Controls.Add(oMaskEx);

            //Add CustomeValidator
            sMessage = "";//"הוקלד ערך שגוי. יש להקליד שעת יציאה בין " + Param29 + " עד " + Param30 ;
            sID = "vldPeilutShatYetiza";
            sClientScriptFunction = "ChkExitHour";
            vldCustomValidator = AddCustomValidator(sTargetControlId, sMessage, sID, 
                                                    sClientScriptFunction, e.Row.ClientID, e.Row.ClientID);
            e.Row.Cells[_COL_SHAT_YETIZA].Controls.Add(vldCustomValidator);

            //Add Ajax CallOutCustomeValidator
            sTargetControlId = ((CustomValidator)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[2])).ID;
            sID = "vldExShatYetiza";
            vldExtenderCallOut = AddCallOutValidator(sTargetControlId, sID, e.Row.ClientID,
                                                      AjaxControlToolkit.ValidatorCalloutPosition.Left);
            e.Row.Cells[_COL_SHAT_YETIZA].Controls.Add(vldExtenderCallOut);
            string sPeilutKey = string.Concat(oTxt.ClientID, "|", e.Row.Cells[_COL_KNISA].ClientID, "|", iSidurIndex, "|", e.Row.ClientID);

           
            DataRow[] dr = dtApprovals.Select("mafne_lesade='Shat_yetzia'");
            DateTime dSidurShatHatchala = new DateTime();
                       
            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Error:
                    if (SetOneError(oTxt, hCell, MisparIshi, CardDate, MisparSidur, _FullOldShatHatchala, dOldShatYetiza, iMisparKnisa, sPeilutKey, "Shat_yetzia"))
                        ((TextBox)e.Row.Cells[_COL_PEILUT_STATUS].Controls[0]).Text = enPeilutStatus.enError.GetHashCode().ToString();
                    else
                    {
                        dSidurShatHatchala = DateTime.Parse(string.Concat(CardDate.ToShortDateString(), " ", ShatHatchala));
                        CheckIfApprovalExists(FillApprovalKeys(dr), MisparSidur, _FullOldShatHatchala, dOldShatYetiza, 0, ref oTxt);
                    }
                    break;
                case clGeneral.enCardStatus.Valid:
                    //במידה והכרטיס תקין נבדוק אישורים                  
                    {
                        dSidurShatHatchala = DateTime.Parse(string.Concat(CardDate.ToShortDateString(), " ", ShatHatchala));
                        CheckIfApprovalExists(FillApprovalKeys(dr), MisparSidur, _FullOldShatHatchala, dOldShatYetiza, 0, ref oTxt);
                    }
                    break;
            }
            oTxt.Enabled = ((bSidurActive) && (bPeilutActive) && (bEnabled));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void SetKisuyTor(GridViewRowEventArgs e, bool bEnabled, bool bSidurActive, bool bPeilutActive, int iSidurIndex, bool bElementHachanatMechona)
    {
        AjaxControlToolkit.MaskedEditExtender oMaskEx;
        CustomValidator vldCustomValidator;
        AjaxControlToolkit.ValidatorCalloutExtender vldExtenderCallOut;
        TextBox oTxt;
        string sTargetControlId,  sID,sClientScriptFunction;
        DateTime dShatYetiza, dOldShatYetiza;
        HtmlTableCell hCell = new HtmlTableCell();
        bool bIdkunRashemet = false;
        int iMisparKnisa;

        try
        {
            dOldShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "old_shat_yetzia").ToString());
            oTxt = (TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0]);

            //oTxt.Enabled = ((bEnabled) && (bSidurActive) && (bPeilutActive) && (!bIdkunRashemet));
            oTxt.CausesValidation = true;
            oTxt.ID = e.Row.ClientID + "KisoyTor";
            oTxt.Width = Unit.Pixel(40);
            oTxt.Attributes.Add("OrgEnabled", bEnabled.GetHashCode().ToString());
            oTxt.Attributes.Add("onkeypress", "SetBtnChanges();");
            //AddAttribute(oTxt, "OldV", oTxt.Text);
         //   AddAttribute(oTxt, "OldTorMapV", e.Row.Cells[_COL_KISUY_TOR_MAP].Text);
            e.Row.Cells[_COL_KISUY_TOR_MAP].Text = e.Row.Cells[_COL_KISUY_TOR_MAP].Text;
            //Add MaskTextBox
            sTargetControlId = ((TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0])).ID;
            oMaskEx = AddTimeMaskedEditExtender(sTargetControlId, e.Row.RowIndex, "99:99", "PeilutKisuyTor", AjaxControlToolkit.MaskedEditType.Time, AjaxControlToolkit.MaskedEditShowSymbol.Left);
            e.Row.Cells[_COL_KISUY_TOR].Controls.Add(oMaskEx);

            //Add CustomeValidator
            //sMessage = "כיסוי התור שהוקלד הינו מעל המותר. מותר עד " + ((TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0])).Text;
            sID = "vldKisuyTor";
            sClientScriptFunction = "ChkKisyT";
            vldCustomValidator = AddCustomValidator(sTargetControlId, "", sID, sClientScriptFunction, e.Row.ClientID, e.Row.ClientID);
            e.Row.Cells[_COL_KISUY_TOR].Controls.Add(vldCustomValidator);
            

            //Add Ajax CallOutCustomeValidator
            sTargetControlId = ((CustomValidator)(e.Row.Cells[_COL_KISUY_TOR].Controls[2])).ID;
            sID = "vldExKisuyTor";
            vldExtenderCallOut = AddCallOutValidator(sTargetControlId, sID, e.Row.ClientID, AjaxControlToolkit.ValidatorCalloutPosition.Left);
            e.Row.Cells[_COL_KISUY_TOR].Controls.Add(vldExtenderCallOut);

            //Check if Error Exists
            oTxt = ((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0]));
            dShatYetiza = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "shat_yetzia").ToString());
            oTxt = (TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0]);

            string sPeilutKey = string.Concat(((TextBox)(e.Row.Cells[_COL_SHAT_YETIZA].Controls[0])).ClientID, "|", e.Row.Cells[_COL_KNISA].ClientID, "|", iSidurIndex, "|", e.Row.ClientID);
            string[] arrKnisaVal;
            arrKnisaVal  = e.Row.Cells[_COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);
            if (bEnabled)
                bEnabled = (iMisparKnisa == 0); //נאפשר כיסוי תו רק לנסיעות נמק ושירות שהכניסה היא מעל ל-0

            switch (_StatusCard)
            {
                case clGeneral.enCardStatus.Error:
                    if (SetOneError(oTxt, hCell, MisparIshi, CardDate, MisparSidur, _FullOldShatHatchala, dOldShatYetiza, iMisparKnisa, sPeilutKey, "KISUY_TOR"))
                    {                    
                        ((TextBox)e.Row.Cells[_COL_PEILUT_STATUS].Controls[0]).Text = enPeilutStatus.enError.GetHashCode().ToString();
                    }
                    break;
            }
            bIdkunRashemet = IsIdkunExists(_MisparIshiIdkunRashemet, _ProfileRashemet, clWorkCard.ErrorLevel.LevelPeilut, clUtils.GetPakadId(dtPakadim, "KISUY_TOR"), MisparSidur, FullShatHatchala, dShatYetiza, iMisparKnisa);
            oTxt = (TextBox)(e.Row.Cells[_COL_KISUY_TOR].Controls[0]);
            if (oTxt.Text.Equals(0))            
                oTxt.Text = "";
            
            int iKisuyTorMap = String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "kisuy_tor_map").ToString()) ? 0 : int.Parse(DataBinder.Eval(e.Row.DataItem, "kisuy_tor_map").ToString());
                //if (iKisuyTorMap==0)
                //    oTxt.Text = "";
            //}
            oTxt.Enabled = ((!bElementHachanatMechona) && (bEnabled) && (bSidurActive) && (bPeilutActive) && (!bIdkunRashemet) && (iKisuyTorMap>0));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool IsPeilutMustOtoNumber(long lMakat, bool bBusMustOtoNumber)
    {
        clKavim _Kavim = new clKavim();
        clKavim.enMakatType _MakatType;
        _MakatType = ((clKavim.enMakatType)_Kavim.GetMakatType(lMakat));
        //פעילויות שדורשות רכב: קו שירות, נמ"ק, ריקה ואלמנט עם מאפיין 11
        return ((_MakatType == clKavim.enMakatType.mKavShirut) || (_MakatType == clKavim.enMakatType.mNamak) || (_MakatType == clKavim.enMakatType.mEmpty) || (_MakatType == clKavim.enMakatType.mVisut) || ((_MakatType == clKavim.enMakatType.mElement) && (bBusMustOtoNumber)));

    }
    protected void btnSidur_Click(object sender, EventArgs e)
    {
        ClearControl();
        BuildPage();       
    }
    protected void btnDummy_Click(object sender, ImageClickEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    void vldCarNumber_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }
    protected RegularExpressionValidator AddRegularExpressionValidator(string sValidationExpression , string sMessage,string sID, GridViewRowEventArgs e)
    {
        RegularExpressionValidator oValidator =  new RegularExpressionValidator();
        oValidator.ControlToValidate = sID;
        oValidator.ErrorMessage = sMessage;
        oValidator.ID = sID + e.Row.ClientID;
        oValidator.Display = ValidatorDisplay.None;
        oValidator.ValidationExpression =sValidationExpression;
        return oValidator;
      
    }

    protected AjaxControlToolkit.FilteredTextBoxExtender AddFilterTextBoxExtender(
                                            string sTargetControlId, string sID,string sValidChars, 
                                            AjaxControlToolkit.FilterModes oFilterMode,
                                            AjaxControlToolkit.FilterTypes oFilterType,        
                                            GridViewRowEventArgs e)
    {
        AjaxControlToolkit.FilteredTextBoxExtender oFilterTextBox = new AjaxControlToolkit.FilteredTextBoxExtender();
        oFilterTextBox.TargetControlID = sTargetControlId;
        oFilterTextBox.FilterType = oFilterType;
        oFilterTextBox.FilterMode = oFilterMode;
        oFilterTextBox.ValidChars = sValidChars;          
        oFilterTextBox.ID = sID + e.Row.ClientID;
       
        return oFilterTextBox;
        
    }
    protected AjaxControlToolkit.ValidatorCalloutExtender AddCallOutValidator
                                        (string sTargetControlId,string sID, string eClientId,
                                        AjaxControlToolkit.ValidatorCalloutPosition oPosition)
    {
        AjaxControlToolkit.ValidatorCalloutExtender vldExKisuyTor = new AjaxControlToolkit.ValidatorCalloutExtender();
        try
        {
            vldExKisuyTor.TargetControlID = sTargetControlId;
            vldExKisuyTor.PopupPosition = oPosition;//AjaxControlToolkit.ValidatorCalloutPosition.Right;
            vldExKisuyTor.ID = sID + eClientId;
            
            return vldExKisuyTor;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected CustomValidator AddCustomValidator(string sTargetControlId, string sMessage, string sID,
                                                   string sClientScriptFunction, string eClientID, string sIndex)
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
        vldCustomValidator.Attributes.Add("index", sIndex);
        vldCustomValidator.ValidateEmptyText = false;
        vldCustomValidator.Style.Add("dir","rtl");
            
        
        //vldKisuyTor.Text = ((TextBox)(e.Row.Cells[COL_KISUY_TOR].Controls[0])).Text;

        return vldCustomValidator;

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
            vldCustomValidator.Style.Add("dir", "rtl");
            return vldCustomValidator;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public clSidur SecondParticipate
    {
        set
        {
            _SecondParticipate = value;
        }
        get
        {
            return _SecondParticipate;
        }
    }

    public clSidur FirstParticipate
    {
        set
        {
            _FirstParticipate = value;
        }
        get
        {
            return _FirstParticipate;
        }
    }

    public bool IsAtLeastOneSidurNahagut
    {
        set
        {
            _SidurNahagut = value;
        }
        get
        {
            return _SidurNahagut;
        }
    }
    public bool IsSidurRetzifut
    {
        set
        {
            _SidurRetzifut = value;
        }
        get
        {
            return _SidurRetzifut;
        }
    }
    public DataTable Mashar
    {
        set
        {
            _Mashar = value;
        }
        get
        {
            return _Mashar;
        }
    }
  
    //protected void BuildSidurim(DataTable dtSidurim)
    //{
    //    AjaxControlToolkit.AccordionPane Ap = new AjaxControlToolkit.AccordionPane();        
    //    System.Web.UI.HtmlControls.HtmlTable hTable;        
    //    GridView grdPeiluyot = new GridView();      
    //    dvPeilut = new DataView(dtSidurim);
        

    //    int iPrevSidur=0;
    //    int i = 1;
    //    try
    //    {
    //        foreach (DataRow dr in dtSidurim.Rows)
    //        {
    //            if ((int.Parse(dr["mispar_sidur"].ToString())) != iPrevSidur)
    //            {                    
    //                Ap = new AjaxControlToolkit.AccordionPane();
    //                Ap.ID = "AccordionPane" + i;
    //                i++;
    //                MyAccordion.Controls[0].Controls.Add(Ap);
                  
    //                hTable = BuildOneSidur(dr);
    //                Ap.HeaderContainer.Controls.Add(hTable);
    //                iPrevSidur = int.Parse(dr["mispar_sidur"].ToString());

    //                grdPeiluyot = BuildSidurPeiluyot(dvPeilut, dr);
    //                //dvPeilut.RowFilter = "mispar_sidur=" + dr["mispar_sidur"].ToString();

    //                //grdPeiluyot = new GridView();
    //               // grdPeiluyot.AutoGenerateColumns = true;

    //                //grdPeiluyot.DataSource = dvPeilut;
    //                //grdPeiluyot.DataBind();
    //                Ap.ContentContainer.Controls.Add(grdPeiluyot);
    //            }                
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}
    
    protected void ListViewSidurim_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ListViewSidurim_DataBound(object sender, EventArgs e)
    {
        
    }

    protected void ListViewSidurim_ItemDataBound(object sender,ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            // you would use your actual data item type here, not "object" 
            object o = (object)dataItem;
        }         
    }

    public clParameters KdsParameters
    {
        set
        {
            _KdsParameters = value;
        }
        get
        {
            return _KdsParameters;
        }
    }
    public clOvedYomAvoda OvedYomAvoda
    {
        set
        {
            _oOvedYomAvoda = value;
        }
        get
        {
            return _oOvedYomAvoda;
        }
    }
    public clMeafyenyOved MeafyenyOved 
    {
       set
       {
           _MeafyenyOved = value;
       }
       get
       {
           return _MeafyenyOved;
       }    
    }

    public OrderedDictionary DataSource
    {
        set
        {
            _DataSource = value;
        }
        get
        {
            return _DataSource;
        }
    }
    public OrderedDictionary CancledSidurim
    {
        set
        {
            _CancledSidurim = value;
        }
        get
        {
            return _CancledSidurim;
        }
    }

    public DataView DDLChariga
    {
        set
        {
            _dvChariga = value;
        }
        get
        {
            return _dvChariga;
        }
    }

    public DataView DDLPitzulHafsaka
    {
        set
        {
            _dvPitzulHafsaka = value;
        }
        get
        {
            return _dvPitzulHafsaka;
        }
    }

    public DataView DDLSibotLedivuch
    {
        set
        {
            _dvSibotLedivuch = value;
        }
        get
        {
            return _dvSibotLedivuch;
        }
    }

    public DataView DDLHashlama
    {
        set
        {
            _dvHashlama = value;
        }
        get
        {
            return _dvHashlama;
        }
    }
    public float Param42
    {//פקטור נסיעות שירות בין גמר לתכנון,
        set
        {
            _Param42 = value;
        }
        get
        {
            return _Param42;
        }
    }
    public float Param43
    {// פקטור נסיעות ריקות בין גמר לתכנון 
        set
        {
            _Param43 = value;
        }
        get
        {
            return _Param43;
        }
    }
    public DateTime Param93
    {//מגבלת התחלה שעת התחלה
        set
        {
            _Param93 = value;
        }
        get
        {
            return _Param93;
        }
    }
    public DateTime Param1
    {//מגבלת סיום שעת התחלה
        set
        {
            _Param1 = value;
        }
        get
        {
            return _Param1;
        }
    }
    //public DateTime Param4
    //{//מגבלת סיום שעת גמר - מפעילים
    //    set
    //    {
    //        _Param4 = value;
    //    }
    //    get
    //    {
    //        return _Param4;
    //    }
    //}
    public DateTime Param3
    {//סידור מנהל - מגבלת סיום שעת גמר 
        set
        {
            _Param3 = value;
        }
        get
        {
            return _Param3;
        }
    }
    public DateTime Param29
    {
        //שעת התחלה ראשונה מותרת לפעילות
        set
        {
            _Param29 = value;
        }
        get
        {
            return _Param29;
        }
    }
    public DateTime Param30
    {
        //שעת התחלה אחרונה מותרת לפעילות
        set
        {
            _Param30 = value;
        }
        get
        {
            return _Param30;
        }
    }
    public DateTime Param80
    {//מגבלת סיום שעת גמר - סידור נהגות
        set
        {
            _Param80 = value;
        }
        get
        {
            return _Param80;
        }
    }
    public float Param149
    {//149 - אורך נסיעה קצרה לאילת
        set
        {
            _Param149 = value;
        }
        get
        {
            return _Param149;
        }
    }
    public int Param101
    {
        set
        {
            _Param101 = value;
        }
        get
        {
            return _Param101;
        }
    }
    public int Param102
    {
        set
        {
            _Param102 = value;
        }
        get
        {
            return _Param102;
        }
    }
    public int Param103
    {
        set
        {
            _Param103 = value;
        }
        get
        {
            return _Param103;
        }
    }
    public int Param41
    {
        set
        {
            _Param41 = value;
        }
        get
        {
            return _Param41;
        }
    }
    public int Param98
    {
        set
        {
            _Param98 = value;
        }
        get
        {
            return _Param98;
        }
    }
    public DateTime Param242
    {
        set
        {
            _Param242 = value;
        }
        get
        {
            return _Param242;
        }
    }
    public DateTime Param244
    {
        set
        {
            _Param244 = value;
        }
        get
        {
            return _Param244;
        }
    }
    //public int Param108
    //{
    //    set
    //    {
    //        _Param108 = value;
    //    }
    //    get
    //    {
    //        return _Param108;
    //    }
    //}
    //public int Param109
    //{
    //    set
    //    {
    //        _Param109 = value;
    //    }
    //    get
    //    {
    //        return _Param109;
    //    }
    //}
    //public int Param110
    //{
    //    set
    //    {
    //        _Param110 = value;
    //    }
    //    get
    //    {
    //        return _Param110;
    //    }
    //}
    public int NumOfHashlama
    {
        set 
        {
            _NumOfHashlama = value;     
        }
        get
        {
            return _NumOfHashlama;
        }
    }
    public DateTime CardDate
    {
        set
        {
            _CardDate = value;
        }
        get
        {
            return _CardDate;
        }
    }
    public int MisparIshi
    {
        set
        {
            _MisparIshi = value;
        }
        get
        {
            return _MisparIshi;
        }
    }
    public int LoginUserId
    {
        set
        {
            _LoginUserId = value;
        }
        get
        {
            return _LoginUserId;
        }
    }
    public int SidurIndex
    {
        set
        {
            _SidurIndex = value;
        }
        get
        {
            return _SidurIndex;
        }
    }
    public int RefreshBtn
    {
        set
        {
            _RefreshBtn = value;
        }
        get
        {
            return _RefreshBtn;
        }
    }  
    public DateTime FullShatHatchala
    {
        set { _FullShatHatchala = value; }
        get { return _FullShatHatchala; }
        
    }
    public DateTime FullOldShatHatchala
    {
        set { _FullOldShatHatchala = value; }
        get { return _FullOldShatHatchala; }

    }
    public string ShatHatchala
    {
        set
        {
            _ShatHatchala = value;
        }
        get
        {
            return _ShatHatchala;
        }
    }

    public int MisparSidur
    {
        set
        {
            _MisparSidur = value;
        }
        get
        {
            return _MisparSidur;
        }
    }
    public DataTable dtSidurim
    {
        set
        {
            _dtSidurim = value;
        }
        get
        {
            return _dtSidurim;
        }
    }
    public DataTable SugeySidur
    {
        set
        {
            _dtSugSidur = value;
        }
        get
        {
            return _dtSugSidur;
        }
    }
    public DataTable ErrorsList
    {
        set
        {
            _ErrorsList = value;
        }
        get
        {
            return _ErrorsList;
        }
    }
    public DataTable dtApprovals
    {
        set
        {
            _dtApprovals = value;
        }
        get
        {
            return _dtApprovals;
        }
    }
    public DataTable dtIdkuneyRashemet
    {
        set {_dtIdkuneyRashemet = value;}
        get { return _dtIdkuneyRashemet; }
    }
    public DataTable dtPakadim
    {
        set { _dtPakadim = value; }
        get { return _dtPakadim; }
    }
    
    public DataTable SadotNosafim
    {
        set { _SadotNosafim = value; }
        get { return _SadotNosafim; }
    }
    public DataTable MeafyeneySidur
    {
        set { _MeafyeneySidur = value; }
        get { return _MeafyeneySidur; }
    }
    public DateTime KnisatShabat
    {
        set { _KnisatShabat = value;}
        get { return _KnisatShabat; }
    }
    public UpdatePanel UpEmpDetails
    {
        set
        {
            _UpEmpDetails = value;
        }
        get
        {
            return _UpEmpDetails;
        }
    }
    public DataTable MeafyeneyElementim
    {
        set
        {
            _dtMeafyeneyElementim = value;
        }
        get
        {
            return _dtMeafyeneyElementim;
        }
    }
    public ApprovalRequest[] EmployeeApproval
    {
        set
        {
            _EmployeeApproval = value;
        }
        get
        {
            return _EmployeeApproval;
        }
    }
    public class GridViewTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;
        //A variable to hold the column name.
        string _columnName;
        enControlToAdd[] _arrControlToAdd;
        string _sMessage;
        string _sControlID;
        string _sClientValidationFunction;
        string _sTargetID;
        int _iIndex = 0;

        //Constructor where we define the template type and column name.
        public GridViewTemplate(GridControls oGridControls)
        {
            try
            {
                //Stores the template type.
                _templateType = oGridControls.Type;

                //Stores the column name.
                _columnName = oGridControls.ColName;
                _arrControlToAdd = oGridControls.gcControlType;
                _sClientValidationFunction = oGridControls.ClientValidationFunction;
                _sMessage = oGridControls.Message;
                _sTargetID = oGridControls.TargetID;
                _sControlID = oGridControls.sControlID;
                _iIndex = oGridControls.Index;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GridViewTemplate(ListItemType type, string colname)
        {
            //Stores the template type.
            _templateType = type;

            //Stores the column name.
            _columnName = colname;           
        }

        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            //enControlToAdd oControlToAdd;
            TextBox tbItem;
            //AjaxControlToolkit.MaskedEditExtender MaskTextBox;
            ImageButton Image;
            string sTargetId = "";
            //CustomValidator vldCustom;

            try
            {
                switch (_templateType)
                {
                    case ListItemType.Header:
                        //Creates a new label control and add it to the container.
                        Label lbl = new Label();            //Allocates the new label object.
                        lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                        container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                        break;
                    case ListItemType.Item:
                        //Creates a new text box control and add it to the container.
                        if (_arrControlToAdd.Length > 0)
                        {
                            foreach (enControlToAdd Item in _arrControlToAdd)
                            {
                                switch (Item)
                                {
                                    case enControlToAdd.TextBox:
                                        tbItem = AddTextBox(_columnName);
                                        tbItem.DataBinding += new EventHandler(tbItem_DataBinding);
                                        container.Controls.Add(tbItem);
                                        sTargetId = tbItem.ClientID;
                                        break;
                                    case enControlToAdd.MaskedTimeEditExtender:
                                        //if (!String.IsNullOrEmpty(sTargetId))
                                        //{
                                        //    MaskTextBox = AddTimeMaskedEditExtender(sTargetId);
                                        //    container.Controls.Add(MaskTextBox);
                                        //}
                                        break;
                                    case enControlToAdd.MaskedNumberEditExtender:
                                        //if (!String.IsNullOrEmpty(sTargetId))
                                        //{
                                        //    MaskTextBox = AddNumberMaskedEditExtender(sTargetId);
                                        //    container.Controls.Add(MaskTextBox);
                                        //}
                                        break;
                                    case enControlToAdd.ImageButton:
                                        Image = AddImageButton();
                                        container.Controls.Add(Image);
                                        break;
                                    case enControlToAdd.CustomValidator:
                                        //if (!String.IsNullOrEmpty(sTargetId))
                                        //{
                                        //    vldCustom = AddCustomValidator(sTargetId);
                                        //    container.Controls.Add(vldCustom);
                                        //}
                                        break;
                                    //case enControlToAdd.label:
                                    //    AddLabel();
                                }
                            }
                        }
                        //Creates a column with size 4.                    
                        //Adds the newly created textbox to the container.
                        break;
                    case ListItemType.EditItem:
                        //As, I am not using any EditItem, I didnot added any code here.
                        break;
                    case ListItemType.Footer:
                        CheckBox chkColumn = new CheckBox();
                        chkColumn.ID = "Chk" + _columnName;
                        container.Controls.Add(chkColumn);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        void tbItem_DataBinding(object sender, EventArgs e)
        {
            TextBox txtdata = (TextBox)sender;
            GridViewRow container = (GridViewRow)txtdata.NamingContainer;

            try
            {
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);

                if (dataValue != DBNull.Value)
                {
                    txtdata.Text = dataValue.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected TextBox AddTextBox(string _columnName)
        {
            TextBox tb1 = new TextBox();                            //Allocates the new text box object.
            //tb1.ID = "txt" + _columnName+_iIndex;
            //Attaches the data binding event.
            tb1.Columns = 4;
            return tb1;
        }
        protected AjaxControlToolkit.MaskedEditExtender AddTimeMaskedEditExtender(string sTargetControlId)
        {
            try
            {
                AjaxControlToolkit.MaskedEditExtender MaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
                MaskTextBox.TargetControlID = sTargetControlId;
                MaskTextBox.Mask = "99:99";
                MaskTextBox.MessageValidatorTip = true;
                MaskTextBox.OnFocusCssClass = "MaskedEditFocus";
                MaskTextBox.OnInvalidCssClass = "MaskedEditError";
                MaskTextBox.MaskType = AjaxControlToolkit.MaskedEditType.Time;
                MaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
                MaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.Left;
                MaskTextBox.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left;
                MaskTextBox.ErrorTooltipEnabled = true;

                return MaskTextBox;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected AjaxControlToolkit.MaskedEditExtender AddNumberMaskedEditExtender(string sTargetControlId)
        {
           
            AjaxControlToolkit.MaskedEditExtender MaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
            MaskTextBox.TargetControlID = sTargetControlId;
            MaskTextBox.Mask = "999999999";
            MaskTextBox.MessageValidatorTip = true;
            MaskTextBox.OnFocusCssClass = "MaskedEditFocus";
            MaskTextBox.OnInvalidCssClass = "MaskedEditError";
            MaskTextBox.MaskType = AjaxControlToolkit.MaskedEditType.Number;
            MaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
            MaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.ErrorTooltipEnabled = true;

            return MaskTextBox;
        }
        protected CustomValidator AddCustomValidator(string sTargetId)
        {
            CustomValidator oVld = new CustomValidator();

            oVld.ErrorMessage = _sMessage;
            oVld.ControlToValidate = sTargetId;
            oVld.ID = _sControlID;
            oVld.Display = ValidatorDisplay.None;
            oVld.EnableClientScript = true;
            oVld.ClientValidationFunction = _sClientValidationFunction;
            return oVld;
        }
        protected ImageButton AddImageButton()
        {
            ImageButton oImage = new ImageButton();
            oImage.ImageUrl = "~/Images/allscreens-cancle-btn.jpg";
            oImage.Click += new ImageClickEventHandler(oImage_Click);             
            return oImage;
        }
        protected void AddLabel()
        {
        }
        protected void oImage_Click(object sender, EventArgs e)
        {
            
        }
    }

    public enum enControlToAdd
    {
        TextBox,
        MaskedTimeEditExtender,
        MaskedNumberEditExtender,
        label,
        ImageButton,
        CustomValidator,
        CustomValidatorExtender
    }

    public class GridControls
    {
        private enControlToAdd[] _gcControlType;
        private string _sMessage;
        private string _sControlID;
        private string _sClientValidationFunction;
        private string _TargetID;
        private ListItemType _Type;
        private string _sColName;
        private int _iIndex;        
        //private DataTable _Mashar;
        //private DateTime _CardDate;
       
        public GridControls(enControlToAdd[] gcControlType, string sMessage,string sControlID,
                            string sClientValidationFunction, string sTargetID,
                            ListItemType Type, string sColName, int iIndex)            
        {
            try
            {
                _gcControlType = gcControlType;
                _sMessage = sMessage;
                _sControlID = sControlID;
                _sClientValidationFunction = sClientValidationFunction;
                _TargetID = sTargetID;
                _Type = Type;
                _sColName = sColName;
                _iIndex = iIndex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public enControlToAdd[] gcControlType
        {
            set
            {
                _gcControlType = value;
            }
            get
            {
                return _gcControlType;
            }
        }
        public string Message
        {
            set
            {
                _sMessage = value;
            }
            get
            {
                return _sMessage;
            }
        }
        public string sControlID
        {
            set
            {
                _sControlID = value;
            }
            get
            {
                return _sControlID;
            }
        }
        public string  ClientValidationFunction
        {
            set
            {
                _sClientValidationFunction = value;
            }
            get
            {
                return _sClientValidationFunction;
            }
        }
        public string TargetID
        {
            set
            {
                _TargetID = value;
            }
            get
            {
                return _TargetID;
            }
        }
        public ListItemType Type
        {
            set
            {
                _Type = value;
            }
            get
            {
                return _Type;
            }
        }
        public string ColName
        {
            set
            {
                _sColName = value;
            }
            get
            {
                return _sColName;
            }
        }
        public int Index
        {
            set
            {
                _iIndex = value;
            }
            get
            {
                return _iIndex;
            }
        }
       
    }

    //protected void textTst_TextChanged(object sender, EventArgs e)
    //{

    //}
    //protected void vldTst_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = false;
    //}
   
    protected void txtDummy_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtDummy_TextChanged1(object sender, EventArgs e)
    {
    }
    public void ClearControl()
    {
        tbSidurim.Controls.Clear();
        tbSidurimHeader.Controls.Clear();
    }


    
}

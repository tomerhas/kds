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
//using KdsBatch.InputData;
//using KdsBatch;
using KdsWorkFlow.Approvals;
//using KdsBatch.Entities;
//using KdsBatch.Errors;
using System.IO;
using System.Threading.Tasks;
using KdsLibrary.Utils.Reports;
using System.Collections.Generic;
using System.Timers;
using KDSCommon.DataModels;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Logs;
using KDSCommon.DataModels.Exceptions;


public partial class Modules_Test :Page
{
  
    //private clMeafyenyOved _MeafyenyOved;
    //private clParameters _KdsParameters;
    //private clOvedYomAvoda _oOvedYomAvoda;
    ////private DataView _dvPeilut;
    //private DataView _dvChariga;
    //private DataView _dvSibotLedivuch;
    //private DataView _dvHashlama;
    //private DataView _dvPitzulHafsaka;
    //private DataTable _Mashar;
    //private DateTime _Param1;//מגבלת התחלת שעת התחלה
    //private DateTime _Param93;//מגבלת סיום שעת התחלה
    //private DateTime _Param3; //מגבלת סיום שעת גמר - סידור מנהל
    //private DateTime _Param80; //מגבלת סיום שעת גמר - סידור נהגות
    //private DateTime _Param29; //שעת התחלה ראשונה מותרת לפעילות בסידור
    //private DateTime _Param30; //שעת התחלה אחרונה מותרת לפעילות בסידור
    //private DateTime _CardDate;
    //private bool _HasSaveCard = false;
    //private DataTable _dtSugSidur;
    //private DataTable _dtMeafyeneyElementim;
    //private bool _SidurNahagut;
    //private clBatchManager oBatchManager;
    public int iMisparIshi;
    public DateTime dDateCard;
      public int _sug_bakasha;
      System.Timers.Timer _timer = new System.Timers.Timer(5000); 
     public void Test()
     {
         
     }

     //void OnTimerAwake(object sender, EventArgs e)
     //{
     //    //DataTable dt;
     //    clRequest oRequest= new clRequest();
     //    //try{
     //        // dt= oRequest.
     //        _timer.Stop();
        
       
     //       if (!oRequest.CheckTahalichEnd(_sug_bakasha))
     //       {
     //           _timer.Start();
     //       }

          
     //    //}
     //   //catch
     //   //{
          
     //   //    throw ex;
     //   //}
     //}

    protected void SleepUntillProccessEnd(int sug_bakasha)
    {

        _sug_bakasha = sug_bakasha;
        
        _timer.Start();

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt;
        clBatch oBatch = new clBatch();

        dt = oBatch.GetErrorsActive();
        //chkList.DataSource = dt; 
        //chkList.DataBind();

        //CheckBoxList1.DataSource = dt;
        //CheckBoxList1.DataTextField = "teur_shgia";
        //CheckBoxList1.DataValueField = "kod_shgia";
        //CheckBoxList1.DataBind(); 
        //long dateNumber = 1297380023295;
        //long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        //_timer = new System.Timers.Timer(5000);
        //_timer.Interval = 5000;
        //_timer.Tick += OnTimerAwake;
        //_timer.Enabled = true;
        //_timer.Elapsed += OnTimerAwake;
        //SleepUntillProccessEnd(1);
        //_sug_bakasha = 1;
        //_timer.Start();
    //    OnTimerAwake(sender, e);
      //  SleepUntillProccessEnd(1);
       
        //txtId.Attributes.Add("onfocus", "alert('')");
        //DateTime convertedDate = DateTime.SpecifyKind(DateTime.Parse("26/08/2011"), DateTimeKind.Utc); 
        //var kind = convertedDate.Kind; // will equal DateTimeKind.Utc Now, once the system knows its in UTC time, you can just call ToLocalTime:

        //DateTime dt = convertedDate.ToLocalTime(); 

        //DateTime d1 = new DateTime(1970, 1, 1);
        //long d2 = DateTime.UtcNow.Ticks;
        
        //TimeSpan ts = new TimeSpan(d2 - d1.Ticks); 

       // //DataTable dtTest = new DataTable();
       // //clUtils oUtils = new clUtils();
       // clBatchManager oBatchManager;
       // //dtTest = oUtils.GetSnifAv(1);
       // //SD.dtSnifim = dtTest;
       // DataTable dtLicenseNumbers = new DataTable();
       // dDateCard = DateTime.Parse("03/08/2009");
       // //ServicePath = "~/Modules/WebServices/wsGeneral.asmx";

       // oBatchManager = new clBatchManager(74220, DateTime.Parse("03/08/2009"));
       // if (!oBatchManager.IsExecuteErrors)
       // {
       //     oBatchManager.InitGeneralData();
       // }
       // SD.Param1 = oBatchManager.oParam.dSidurStartLimitHourParam1;
       // SD.Param93 = oBatchManager.oParam.dSidurLimitShatGmar;
       // SD.Param80 = oBatchManager.oParam.dNahagutLimitShatGmar;
       // SD.Param3 = oBatchManager.oParam.dSidurEndLimitHourParam3;
       // SD.Param29 = oBatchManager.oParam.dStartHourForPeilut;
       // SD.Param30 = oBatchManager.oParam.dEndHourForPeilut;
       // SD.SugeySidur = oBatchManager.dtSugSidur;
       // SD.MeafyenyOved = oBatchManager.oMeafyeneyOved;
       // SD.KdsParameters = oBatchManager.oParam;
       // SD.OvedYomAvoda = oBatchManager.oOvedYomAvodaDetails;
       // SD.MeafyeneyElementim = GetMeafyeneyElementim();
       // ////SetSecurityLevel();
       // //SetLookUpSidurim();
       // SD.DataSource = oBatchManager.htEmployeeDetails;
       // dtLicenseNumbers = GetMasharData(oBatchManager.htEmployeeDetails);
       
       // SD.Mashar = dtLicenseNumbers;
       // //oBatchManager.MainInputData(iMisparIshi, dDateCard);
       // //if (!Page.IsPostBack)
       // //{
          //  BuildPage();
       // //}

       ////////// oTxt.Text = "vered";
       //////////// oTableCell.Controls.Add(oTxt);
       ////////// Page.Controls.Add(oTxt);
    }
    private string GetMasharCarNumbers(OrderedDictionary htEmployeeDetails)
    {
        string sCarNumbers = "";
        PeilutDM oPeilut;
        SidurDM oSidur;

        //נשרשר את כל מספרי הרכב, כדי לפנות למש"ר עם פחות נתונים
        for (int i = 0; i < htEmployeeDetails.Count; i++)
        {
            oSidur = (SidurDM)htEmployeeDetails[i];
            for (int j = 0; j < oSidur.htPeilut.Count; j++)
            {
                oPeilut = (PeilutDM)oSidur.htPeilut[j];
                sCarNumbers += oPeilut.lOtoNo.ToString() + ",";
            }
        }

        if (sCarNumbers.Length > 0)
        {
            sCarNumbers = sCarNumbers.Substring(0, sCarNumbers.Length - 1);
        }
        return sCarNumbers;
    }
    private DataTable GetMasharData(OrderedDictionary htEmployeeDetails)
    {
        string sCarNumbers;
        DataTable dtLicenseNumber = new DataTable();

        sCarNumbers = GetMasharCarNumbers(htEmployeeDetails);

        if (sCarNumbers != string.Empty)
        {
            var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
            dtLicenseNumber = kavimDal.GetMasharData(sCarNumbers);
        }
        return dtLicenseNumber;
    }
    protected void SetSecurityLevel()
    {
        //KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
        //if (iSecurity == KdsSecurityLevel.ViewAll)
        //{
        //    //AutoCompleteExtenderByName.ContextKey = "";
        //    //AutoCompleteExtenderID.ContextKey = "";
        //}

        ////else if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
        ////{
        ////AutoCompleteExtenderID.ContextKey = "77783";//LoginUser.UserInfo.EmployeeNumber;
        ////AutoCompleteExtenderByName.ContextKey = "77783";//LoginUser.UserInfo.EmployeeNumber;
        ////}
        ////else
        ////{
        ////    //AutoCompleteExtenderByName.ContextKey = LoginUser.UserInfo.EmployeeNumber;
        ////    AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;
        ////    txtId.Text = LoginUser.UserInfo.EmployeeNumber;
        ////    txtId.Enabled = false;
        ////    //                rdoId.Enabled = false;
        ////    //              rdoName.Enabled = false;
        ////}

    }
    private DataTable GetMeafyeneyElementim()
    {
        DataTable dtElementim = new DataTable();
        try
        {
            var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
            dtElementim = kavimDal.GetMeafyeneyElementByKod(0, dDateCard);
            return dtElementim;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void SetLookUpSidurim()
    {
        //clUtils oUtils = new clUtils();
        //DataView dv = new DataView(oBatchManager.dtLookUp);
        //DataView dvSibotLedivuch;
        //try
        //{
        //    //חריגה
        //    dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbDivuchHarigaMeshaot, "'");
        //    SD.DDLChariga = dv;

        //    //סיבות לדיווח ידני
        //    dvSibotLedivuch = new DataView(oUtils.GetCtbSibotLedivuchYadani());
        //    SD.DDLSibotLedivuch = dvSibotLedivuch;

        //    //פיצול הפסקה
        //    dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbPitzulaHafsaka, "'");
        //    SD.DDLPitzulHafsaka = dv;

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    private void BuildPage()
    {
        //  HtmlTableCell oTableCell = new HtmlTableCell();
        HtmlTable hTable = new HtmlTable();
        HtmlTableRow hRow = new HtmlTableRow();
        HtmlTableCell hCell = new HtmlTableCell();

        hTable.Rows.Add(hRow);

        TextBox oTxt = new TextBox();
        oTxt.ID = "txtTest";
        oTxt.Text = "vered";
        
        ListItem Item = new ListItem();
        DropDownList ddl = new DropDownList();

        Item.Text = "";
        Item.Value = "-1";
        ddl.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "אין צורך בטכוגרף";
        Item.Value = "0";
        ddl.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "חסר טכוגרף";
        Item.Value = "1";
        ddl.Items.Add(Item);
        Item = new ListItem();
        Item.Text = "צירף טכוגרף";
        Item.Value = "2";
        ddl.ID = "ddlVered";
        ddl.Items.Add(Item);

        Button btnImage = new Button();
        btnImage.ID = "imgCancel1";
       
        //btnImage.PostBackUrl = "~/Images/allscreens-cancle-btn.jpg";
        btnImage.OnClientClick = "if (!ChangeStatusSidur(this.id)) {return false;} else {return true;} ";
        //btnImage.Click += new ClickEventHandler(btnImage_Click);
        btnImage.Text = "1";
        btnImage.Attributes.Add("cancel", "0");

        hCell.Controls.Add(oTxt);
       // hCell.Controls.Add(ddl);
        hCell.Controls.Add(btnImage);
        
        //lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
                     
        hTable.Rows[0].Cells.Add(hCell);
        pnlHeader.Controls.Add(hTable);



        GridView grdPeiluyot = new GridView();
        //BoundField boundGridField;
        TemplateField tGridField = new TemplateField();
        GridControls.enControlToAdd[] arrControlToAdd;
        int iArrControlSize = 0;
        DataTable dtTest;
        clUtils oUtils = new clUtils();

        dtTest = oUtils.GetSnifAv(1);

        GridControls oGridControls;

       
        grdPeiluyot.EnableViewState = true;
        grdPeiluyot.GridLines = GridLines.None;
      //  grdPeiluyot.ID = "grdPeiluyot" + iIndex;
        grdPeiluyot.ShowHeader = true;
        grdPeiluyot.AllowPaging = false;
        grdPeiluyot.AutoGenerateColumns = false;
        grdPeiluyot.AllowSorting = true;
        grdPeiluyot.Width = Unit.Pixel(900);
       
        tGridField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, "");
        iArrControlSize = 0;
        //Initialize the HeaderText field value.
        arrControlToAdd = new GridControls.enControlToAdd[iArrControlSize + 1];
        arrControlToAdd[iArrControlSize] = GridControls.enControlToAdd.TextBox;
        oGridControls = new GridControls(arrControlToAdd, "", "", "", "", ListItemType.Item, "code", 0);
        tGridField.ItemTemplate = new GridViewTemplate(oGridControls);
        tGridField.ItemStyle.Width = Unit.Pixel(120);
        grdPeiluyot.RowDataBound += new GridViewRowEventHandler(grdPeiluyot_RowDataBound);
        grdPeiluyot.Columns.Add(tGridField);

        grdPeiluyot.DataSource = dtTest;
        grdPeiluyot.DataBind();
        pnlHeader.Controls.Add(grdPeiluyot);
       
    }
    protected void grdPeiluyot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //(((TextBox)(e.Row.Cells[0].Controls[0]))).Text = "vered";

        }
    }
    protected void btnImage_Click(object sender, EventArgs e)
    {

    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    clKavim oKavim = new clKavim();
    //    int iRes;

    //    Response.Write(DateTime.Now);
    //    //for (int i = 0; i <= 67; i++)
    //    //{


    //    oKavim.GetSidurDetailsFromTnua(1012,DateTime.Now, out iRes);
    //       // oKavim.GetKavimDetailsFromTnua(45714132, DateTime.Now, out iRes);//45714132,68966641
    //   // }
    //    Response.Write(string.Concat(" - " , DateTime.Now));
    //    //oKavim.ExecuteTnuaReka(68966641, DateTime.Now, out iRes);
    //    //oKavim.ExecuteTnuaNamak(80101041, DateTime.Parse("15/04/2007"),out iRes);
    //}
    protected void Button2_Click(object sender, EventArgs e)
    {
        //pnlHeader.Controls.Clear();
        //BuildPage();
      //  clKavim oKavim = new clKavim();
      //  int iRes;
        //clCardErrors oErr = new clCardErrors();
        //clInputData oInputData = new clInputData();
       // oKavim.GetSidurAndPeiluyotFromTnua(12417, DateTime.Parse("03/08/2009"), out iRes);
        //clBatchManager oBatchManager = new clBatchManager(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
        //oBatchManager.MainInputData(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
        //oBatchManager.MainOvedErrors(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));

        

        //oKavim.GetKavimDetailsFromTnuaDS(45714132, DateTime.Now, out iRes);
        //oKavim.GetSidurDetailsFromTnua(62063, DateTime.Parse("04/05/2009"), out iRes);
        //oInputData.MainInputData(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
        //oErr.MainOvedErrors(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));

        MainCalc objMainCalc = new MainCalc();
        objMainCalc.MainCalcTest(DateTime.Parse(clnFromDate.Text), int.Parse(txtId.Text));
             
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //clCalculation objCalc = new clCalculation();
        //objCalc.MainCalcTest(DateTime.Parse(clnFromDate.Text), int.Parse(txtId.Text));

       //MainCalc objMainCalc = new MainCalc();
       //objMainCalc.MainCalcTest(DateTime.Parse(clnFromDate.Text), int.Parse(txtId.Text));
             
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        MainCalc objMainCalc = new MainCalc(456,1);
        objMainCalc.PremiaCalc();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //clInputData oInputData = new clInputData();
        //oInputData.MainInputData(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
        string[] arrDate;
        arrDate = clnFromDate.Text.Split(char.Parse("/"));
        DateTime a;
        a = new DateTime(2010, 10, 03, 19, 20, 0);
       clBatchManager oBatchManager = new clBatchManager();
        
        oBatchManager.MainOvedErrorsNew(int.Parse(txtId.Text), new DateTime(int.Parse(arrDate[2]), int.Parse(arrDate[1]), int.Parse(arrDate[0])));
        //oBatchManager.MainInputData(int.Parse(txtId.Text), new DateTime(int.Parse(arrDate[2]), int.Parse(arrDate[1]), int.Parse(arrDate[0])));
      //  oBatchManager.MainOvedErrors(int.Parse(txtId.Text), new DateTime(int.Parse(arrDate[2]), int.Parse(arrDate[1]), int.Parse(arrDate[0])));
        oBatchManager.MainOvedErrorsNew(int.Parse(txtId.Text), new DateTime(int.Parse(arrDate[2]), int.Parse(arrDate[1]), int.Parse(arrDate[0])));
        
    }
    protected void BtAddSelected_Click(object sender, EventArgs e) 
    {

    }
    protected void btnWorkCard_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Modules/Ovdim/WorkCard.aspx");
        //Response.Redirect("~/Modules/Ovdim/WorkCard.aspx?MisparIshi=74480&CardDate=29/11/2009");
        Response.Redirect("~/Modules/Ovdim/WorkCard.aspx?EmpID=77840&WCardDate=12/09/2010");
       // Server.MapPath("~/Modules/Ovdim/WorkCard.aspx?EmpID=74220&WCardDate=26/05/2009");
    }
    protected void btnApproval_click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtId.Text.Trim()))
        {
            //ApprovalFactory.RaiseAllWorkDayApprovalCodes(DateTime.Parse(clnFromDate.Date), 11000);
            Server.ScriptTimeout = 3600;
         //   ApprovalFactory.ApprovalsEndOfDayProcess(DateTime.Parse(clnFromDate.Date), false);
        }
        //else
        //    ApprovalFactory.RaiseEmployeeWorkDayApprovalCodes(DateTime.Parse(clnFromDate.Date),
        //        int.Parse(txtId.Text), 10000, chkGarage.Checked);
    }
    protected void btnInputAndErrorsBatch_click(object sender, EventArgs e)
    {
        long lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(
            clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess,
            "", 0);
      //  KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcess,
           // KdsBatch.BatchExecutionType.All, DateTime.Parse(clnFromDate.Date), lRequestNum, true);

    }

    protected void btnImportCompare_Click(object sender, EventArgs e)
    {
        KdsBatch.SystemCompare.CompareImporter import = new KdsBatch.SystemCompare.CompareImporter();
        import.ExecuteImport();
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

        public GridControls(enControlToAdd[] gcControlType, string sMessage,string sControlID,
                            string sClientValidationFunction, string sTargetID,
                            ListItemType Type, string sColName, int iIndex)            
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
        //tbSidurim.Controls.Clear();
        //tbSidurimHeader.Controls.Clear();
    }
    public class GridViewTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;
        //A variable to hold the column name.
        string _columnName;
        GridControls.enControlToAdd[] _arrControlToAdd;
        string _sMessage;
        string _sControlID;
        string _sClientValidationFunction;
        string _sTargetID;
        int _iIndex = 0;

        //Constructor where we define the template type and column name.
        public GridViewTemplate(GridControls oGridControls)
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
            AjaxControlToolkit.MaskedEditExtender MaskTextBox;
            ImageButton Image;
            string sTargetId = "";
            CustomValidator vldCustom;

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
                        foreach (GridControls.enControlToAdd Item in _arrControlToAdd)
                        {
                            switch (Item)
                            {
                                case GridControls.enControlToAdd.TextBox:
                                    tbItem = AddTextBox(_columnName);
                                    tbItem.DataBinding += new EventHandler(tbItem_DataBinding);
                                    container.Controls.Add(tbItem);
                                    sTargetId = tbItem.ClientID;
                                    break;
                                case GridControls.enControlToAdd.MaskedTimeEditExtender:
                                    //if (!String.IsNullOrEmpty(sTargetId))
                                    //{
                                    //    MaskTextBox = AddTimeMaskedEditExtender(sTargetId);
                                    //    container.Controls.Add(MaskTextBox);
                                    //}
                                    break;
                                case GridControls.enControlToAdd.MaskedNumberEditExtender:
                                    //if (!String.IsNullOrEmpty(sTargetId))
                                    //{
                                    //    MaskTextBox = AddNumberMaskedEditExtender(sTargetId);
                                    //    container.Controls.Add(MaskTextBox);
                                    //}
                                    break;
                                case GridControls.enControlToAdd.ImageButton:
                                    Image = AddImageButton();
                                    container.Controls.Add(Image);
                                    break;
                                case GridControls.enControlToAdd.CustomValidator:
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


        void tbItem_DataBinding(object sender, EventArgs e)
        {
            TextBox txtdata = (TextBox)sender;
            GridViewRow container = (GridViewRow)txtdata.NamingContainer;

            object dataValue = DataBinder.Eval(container.DataItem, _columnName);

            if (dataValue != DBNull.Value)
            {
                txtdata.Text = dataValue.ToString();
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
            AjaxControlToolkit.MaskedEditExtender MaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
            MaskTextBox.TargetControlID = sTargetControlId;
            MaskTextBox.Mask = "99:99";
            MaskTextBox.MessageValidatorTip = true;
            MaskTextBox.OnFocusCssClass = "MEFocus";
            MaskTextBox.OnInvalidCssClass = "MEError";
            MaskTextBox.MaskType = AjaxControlToolkit.MaskedEditType.Time;
            MaskTextBox.InputDirection = AjaxControlToolkit.MaskedEditInputDirection.RightToLeft;
            MaskTextBox.AcceptNegative = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left;
            MaskTextBox.ErrorTooltipEnabled = true;

            return MaskTextBox;
        }
        protected AjaxControlToolkit.MaskedEditExtender AddNumberMaskedEditExtender(string sTargetControlId)
        {
            AjaxControlToolkit.MaskedEditExtender MaskTextBox = new AjaxControlToolkit.MaskedEditExtender();
            MaskTextBox.TargetControlID = sTargetControlId;
            MaskTextBox.Mask = "999999999";
            MaskTextBox.MessageValidatorTip = true;
            MaskTextBox.OnFocusCssClass = "MEFocus";
            MaskTextBox.OnInvalidCssClass = "MEError";
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        
        clTransferToHilan objTran = new clTransferToHilan();

        objTran.Transfer(0, int.Parse(txtId.Text));
     }



    protected void rdoTst_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ButtonShinuyim_Click(object sender, EventArgs e)
    {
        //EntitiesDal oDal = new EntitiesDal();
       // DataTable dt = oDal.getOvdimForShguim();
       // string sMisparim = "77319";
       // DateTime dTaarich = DateTime.Parse("25/08/2011");
        // string[] iMisparim = sMisparim.Split(',');
        // clBatchManager oBatchManager = new clBatchManager();

         //HafelShguim(26506, DateTime.Parse("15/02/2011"));
         //oBatchManager.MainOvedErrors(26506, DateTime.Parse("15/02/2011"));

        // ServiceLocator.Current.GetInstance<ILogBakashot>().InsertErrorToLog(lRequestNum, "E", 0, "RunCalcBatchProcess " + iNumProcess + ": " + ex.Message);
      //   clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now, "Start MainOvedErrors");
         //foreach (DataRow dr in dt.Rows) //int i = 0; i < iMisparim.Length; i++)
         //{
         //   // HafelShguim(int.Parse(dr["MISPAR_ISHI"].ToString()), DateTime.Parse(dr["taarich"].ToString()));
         //   //  oBatchManager.MainOvedErrors(int.Parse(dr["MISPAR_ISHI"].ToString()), DateTime.Parse(dr["taarich"].ToString()));
         //    oBatchManager.MainOvedErrorsNew(int.Parse(dr["MISPAR_ISHI"].ToString()), DateTime.Parse(dr["taarich"].ToString()));
         //}
      //   clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now, "End MainOvedErrors");

     //    clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now, "Start HafelShguim");
         //foreach (DataRow dr in dt.Rows) //int i = 0; i < iMisparim.Length; i++)
         //{
         //    HafelShguim(int.Parse(dr["MISPAR_ISHI"].ToString()), DateTime.Parse(dr["taarich"].ToString()));
         //  //  oBatchManager.MainOvedErrors(int.Parse(dr["MISPAR_ISHI"].ToString()), DateTime.Parse(dr["taarich"].ToString()));
         //}
        // clLogBakashot.InsertErrorToLog(0, 0, "I", 0, DateTime.Now, "End HafelShguim");
         }

    private void HafelShguim(int mispar_ishi,DateTime taarich)
    {
        //EntitiesDal oDal = new EntitiesDal();
        CardStatus _CardStatus;
        bool bHaveShgiotLetzuga = false;
        string sArrKodShgia;
      //  DataTable dt = new DataTable();
        try
        {
           // GlobalData.InitGlobalData(taarich);

            //Day oDay = new Day(mispar_ishi, taarich,true);// new Day(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text), true);
            //oDay.btchRequest = 0;
            //if (oDay.oOved.bOvedDetailsExists)
                    //{
            //    try
            //    {
            //        foreach (Sidur oSidur in oDay.Sidurim)
            //        {
            //        //Parallel.ForEach(oDay.Sidurim, oSidur =>
            //        //{
            //            if (oSidur.bIsSidurLeBdika)
            //            {
            //                //Parallel.ForEach(oSidur.Peiluyot, oPeilut =>
            //                //{
            //                //    oPeilut.Run(oDay);

            //                //});
            //                foreach (Peilut oPeilut in oSidur.Peiluyot)
            //                {
            //                    oPeilut.Run(oDay);
            //                }
            //                oSidur.Run(oDay);
            //            }
            //        }//);
            //        oDay.Run(oDay);
            //    }
            //    catch (Exception ex)
            //    {
            //        oDay.bSuccsess = false;
            //    }
               
            //    oDal.DeleteErrorsFromTbShgiot(oDay.oOved.iMisparIshi, oDay.dCardDate);

            //    sArrKodShgia = "";
            //    oDay.RemoveShgiotMeusharotFromDt(ref sArrKodShgia, oDay);
            //    if (sArrKodShgia.Length > 0)
            //    {
            //        sArrKodShgia = sArrKodShgia.Substring(0, sArrKodShgia.Length - 1);
            //        bHaveShgiotLetzuga = oDal.CheckShgiotLetzuga(sArrKodShgia);
            //    }
            //    if (oDay.CardErrors.Count > 0)
            //    {
            //        oDal.InsertErrorsToTbShgiot(oDay);
            //        _CardStatus = CardStatus.Error;
            //    }
            //    else
            //    {
            //        _CardStatus = CardStatus.Valid;
            //    }
            //    if (_CardStatus.GetHashCode() != oDay.iStatus)
            //    {
            //        oDal.UpdateCardStatus(oDay.oOved.iMisparIshi, oDay.dCardDate, _CardStatus, oDay.iUserId);
            //    }

            //    oDal.UpdateRitzatShgiotDate(oDay.oOved.iMisparIshi, oDay.dCardDate, bHaveShgiotLetzuga);
            //   // dt = oDay.CardErrors.ToDataTable();
            //}
            // return oDay.bSuccsess;
        }
        catch (Exception ex)
        {
           // clLogBakashot.InsertErrorToLog(0, iMisparIshi, "E", 0, Date, "MainOvedErrors: " + ex.Message);
            //  return false;
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        DataSet dsSidur=new DataSet();
        int iResult;
        double dTotalTime;
        DateTime dTime = DateTime.Now;

        //5095, DateTime.Parse("03/10/2010")
        var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
        dsSidur = kavimDal.GetSidurAndPeiluyotFromTnua(67409, DateTime.Parse("14/12/2010"), null, out iResult);
         dTotalTime = (DateTime.Now - dTime).TotalSeconds;
        lblTimeNoVisut.Text = dTotalTime.ToString();
        dTime = DateTime.Now;
        dsSidur = kavimDal.GetSidurAndPeiluyotFromTnua(67409, DateTime.Parse("14/12/2010"), 1, out iResult);
        dTotalTime = (DateTime.Now - dTime).TotalSeconds;
        lblTimeWithVisut.Text = dTotalTime.ToString();       
    }


    protected void btnMakat_Click(object sender, EventArgs e)
    {
      //  clTkinutMakatim objMakat = new clTkinutMakatim();
       // objMakat.CheckTkinutMakatim(DateTime.Parse(clnFromDate.Text));

        wsBatch oBatch = new wsBatch();
        oBatch.RunTkinutMakatim(DateTime.Parse(clnFromDate.Text));
    }

    //protected void btnRefreshMakatim_Click(object sender, EventArgs e)
    //{
    //    List<string[]> FilesName;
    //    string[] patterns = new string[3];
    //    string path, FileNameOld;
    //    string[] files;
    //    try
    //    {
    //        FilesName = new List<string[]>();
    //        patterns[0] = "BZAY"; patterns[1] = "BZAS"; patterns[2] = "BZAP";
    //        path = ConfigurationSettings.AppSettings["PathFileReports"] +"MF\\";
    //        if (Directory.Exists(path))
    //        {
    //            foreach (string pattern in patterns)
    //            {
    //                FilesName.Add(Directory.GetFiles(path, pattern + "*.txt", SearchOption.TopDirectoryOnly));
    //            }

    //            for(int i=0;i<FilesName.Count;i++)
    //            {
    //                files = FilesName[i];
    //                foreach (string file in files)
    //                {
    //                    switch (i)
    //                    {
    //                        case 0:
    //                            KdsBatch.History.TaskDay oTaskY = new KdsBatch.History.TaskDay(file, ';');
    //                            oTaskY.Run();
    //                            break;
    //                        case 1:
    //                            KdsBatch.History.TaskSidur oTaskS = new KdsBatch.History.TaskSidur(file, ';');
    //                            oTaskS.Run();
    //                            break;
    //                        case 2:
    //                            KdsBatch.History.TaskPeilut oTaskP = new KdsBatch.History.TaskPeilut(file, ';');
    //                            oTaskP.Run();
    //                            break;
    //                    }


    //                    FileNameOld = file.Replace(".TXT", ".old");
    //                    File.Copy(file, FileNameOld);
    //                    File.Delete(file);
    //                }
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        clGeneral.LogError(ex);
    //    }
    //}

    protected void btnRefreshMakatim_Click(object sender, EventArgs e)
    {
        try
        {
            KdsBatch.TaskManager.Utils clUtils = new KdsBatch.TaskManager.Utils();
           //++ clUtils.RunBakaratSDRN();
           //++ clUtils.RunRetroSpectSDRN();
            clUtils.RefreshKnisot(DateTime.Parse(clnFromDate.Text));
        //clTkinutMakatim objMakat = new clTkinutMakatim();
        // objMakat.(DateTime.Parse(clnFromDate.Text));

        //wsBatch oBatch = new wsBatch();
        //oBatch.RunTkinutMakatim(DateTime.Parse(clnFromDate.Text));
    }
        catch (Exception ex)
        {
            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(255, "E", 0, "",   ex);
               
           // var excep = clLogBakashot.SetError(0, "E", 0, null, ex);//--
           // ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(excep.Log.RequestId, excep.Log.SugHodaa,excep.Log.KodYeshut,ex.Message );  //--
            //++clGeneral.LogError(ex);
        }
        
    }

    protected void btnPremyot_Click(object sender, EventArgs e)
    {

        KdsBatch.TaskManager.Utils clUtils = new KdsBatch.TaskManager.Utils();

        clUtils.RunCalculationPremyot();
    }


    protected void btnShguimBatch_click(object sender, EventArgs e)
    {
      wsBatch oBatch = new wsBatch();
        DateTime dTime = DateTime.Now.AddDays(-1);

            //clBatch oBatch = new clBatch();
            long lRequestNum = 0;
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "RunShguimOfSdrn", -12);
                //oBatch.RunShinuimVeShguim(lRequestNum, dTime, clGeneral.enCalcType.ShinuyimVeSghuimPremiot.GetHashCode(), clGeneral.BatchExecutionType.All.GetHashCode()); // KdsBatch.BatchRequestSource.ImportProcess.GetHashCode(), KdsBatch.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);
                //KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcess, KdsBatch.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);
            }
            catch (Exception ex)
            {
                throw new Exception("RunShguimOfSdrn:" + ex.Message);
            }
          }

    protected void btnShlifatRikuz_click(object sender, EventArgs e)
    {
        //clReport BlReport = new clReport();
        //byte[] x;
        //byte[] buffer;
        //string path;
        //FileInfo info;
        //ReportModule _RptModule = ReportModule.GetInstance();
        //FileStream fs;
        //System.IO.Stream st;
        //long num;
        //try
        //{
        //    x = BlReport.getRikuzPdf( 281,DateTime.Parse("01/08/2010") ,5950);
        //   // st = BlReport.getRikuzPdf(329, DateTime.Parse("01/08/2010"), 5950);
        //    path = ConfigurationSettings.AppSettings["PathFileReports"];
        //    //if (!Directory.Exists(path))
        //    //    Directory.CreateDirectory(path);
        //   // Report = new KdsBatch.Reports.clReport(
        //    //info = _RptModule.CreateOutputFile(path, "xxxx");
        //    fs = new FileStream (path+"xxx1.pdf", FileMode.Create, FileAccess.Write);
        //    ////num = st.Length;
        //    ////buffer = new Byte[num];
        //    ////int bytesRead = st.Read(buffer, 0, (int)num);

        //    ////while (bytesRead > 0)
        //    ////{
        //    ////    fs.Write(buffer, 0, bytesRead);
        //    ////    bytesRead = st.Read(buffer, 0, (int)num);
        //    ////}
        //    ////st.Close();
        //    ////fs.Close();


        //    fs.Write(x, 0, x.Length);
        //    fs.Flush();
        //    fs.Close(); 

        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("RunShguimOfSdrn:" + ex.Message);
        //}
    }
}



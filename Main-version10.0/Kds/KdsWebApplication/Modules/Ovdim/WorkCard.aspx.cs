﻿using System;
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
using KdsWorkFlow.Approvals;
using KdsLibrary.Utils.Reports;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using KdsLibrary.Utils;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using KDSCommon.Helpers;

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

  //  private CardStatus _StatusCard;
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
    private bool bMenahelBankShaot;
   //** private bool bWcIsUsed;
   // private bool bNextCardErrorNotFound;
    private int iMisparIshiIdkunRashemet;
    private bool bParticipationAllowed; 
   // private AsyncPostBackTrigger[] TriggerToAdd;
    public const int SIDUR_CONTINUE_NAHAGUT = 99500;
    public const int SIDUR_CONTINUE_NOT_NAHAGUT = 99501;
    public const int SIDUR_HITYAZVUT = 99200;
    private bool bAddSidur;
   
    private WorkCardResult _wcResult;
    
    
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
       
        if (((Session["arrParams"] != null) && (Request.QueryString["Page"] != null)) || ((((string)Session["hidSource"]) != "0") && (Session["arrParams"] != null)))
        {
            arrParams = (string[])Session["arrParams"];
            SetUserKiosk(arrParams);
            hidDriver.Value = "1";
            SD.DriverStation = true;
        }
        else { base.CreateUser(); }
    }

    //protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    //{
    //    ScriptManagerKds.AsyncPostBackErrorMessage = e.Exception.ToString(); 
    //}

    private void SetUserKiosk(string[] arrParamsKiosk)
    {
        iMisparIshiKiosk =  int.Parse(arrParamsKiosk[0].ToString());

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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if ((Session["Pakadim"] == null) && (bInpuDataResult))
            SD.UnloadCard(btnRefreshOvedDetails);
        else
            RenderPage();
    }

    protected bool IsSidurLoLetashlumAndLoHitychasut(ref SidurDM _Sidur)
    {
        return ((_Sidur.iLoLetashlum > 0) && (_Sidur.iKodSibaLoLetashlum == clGeneral.enLoLetashlum.WorkCardWithoutHityachasut.GetHashCode()));
    }
    protected bool IsSidurMatala(ref SidurDM _Sidur)
    {
       // זיהוי סידור שמקורו במטלה לפי שבאחת הרשומות של הפעילויות 
       // בסידור 0< TB_peilut_Ovdim. Mispar_matala
        bool bSidurMatala = false;
        PeilutDM _Peilut;
        for (int i = 0; i < _Sidur.htPeilut.Count; i++)
        {
            _Peilut = ((PeilutDM)_Sidur.htPeilut[i]);
            if (_Peilut.lMisparMatala > 0)
            {
                bSidurMatala = true;
                break;
            }
        }
        return bSidurMatala;
    }
    protected bool IsSidurimLoLetashlumAndLoHitychasut()
    {
        //לא קיים סידור המפה או סידור מיוחד שמקורו במטלה שמסומנים כלא לתשלום עם קוד סיבה 16
        //במידה והפונקציה מחזירה אמת, נאפשר מאשר מסתייג
        //bool bAllSidurimLoLetashlum = true;
        bool isOk = true;
        SidurDM _Sidur;
        if (_wcResult.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
            {
                _Sidur = ((SidurDM)_wcResult.htFullEmployeeDetails[i]);

                if ((_Sidur.bSidurMyuhad && IsSidurMatala(ref _Sidur)) || (!_Sidur.bSidurMyuhad && _Sidur.iSugSidurRagil != clGeneral.enSugSidur.SugSidur73.GetHashCode()))//אם סידור מיוחד שמקורו במטלה או סידור מפה
                {
                    isOk = false;
                    if (IsSidurLoLetashlumAndLoHitychasut(ref _Sidur))// סידור לא לתשלום עם סיבה 16
                    {
                        return true;
                    }
                }

                //if (_Sidur.bSidurMyuhad)
                ////אם סידור מיוחד, נבדוק שאם מקורו במטלה, אז הוא מסומן כלא לתשלום
                //{
                //    if (!IsSidurLoLetashlumAndLoHitychasut(ref _Sidur))
                //    {
                //        //אם סידור התגלה כסידור לתשלום או לא לתשלום אבל לא בגלל סיבה 16, נבדוק שמקור הסידור הוא לא במטלה
                //        //במידה ומקורו במטלה נחזיר שגוי
                //        if (IsSidurMatala(ref _Sidur))
                //        {
                //            bAllSidurimLoLetashlum = false;
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                //    //אם סידור מפה נבדוק שהוא מסומן כלא לתשלום
                //    if (_Sidur.iSugSidurRagil != clGeneral.enSugSidur.SugSidur73.GetHashCode())
                //    {
                //        if (!IsSidurLoLetashlumAndLoHitychasut(ref _Sidur))
                //        {
                //            bAllSidurimLoLetashlum = false;
                //            break;
                //        }
                //    }
                //}
            }
        }
        return isOk;
     }
    protected bool IsSidurMatalaNotValidExists()
    {
        bool bIsSidurMatalaNotValidExists = false;
        SidurDM _Sidur;
        for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
        {
            _Sidur = ((SidurDM)_wcResult.htFullEmployeeDetails[i]);
            if ((_Sidur.iMisparSidur >= 1) && (_Sidur.iMisparSidur <= 999))
            {
                bIsSidurMatalaNotValidExists = true;
                break;
            }
        }
        return bIsSidurMatalaNotValidExists;
    }
     protected void SetMeasherMistayeg(bool bChishuvShachar)
     {
         bool bParam252 = false;
        // bool bWorkCardEmpty = false;
         bool bCalculate = (_wcResult.oOvedYomAvodaDetails.iStatus == CardStatus.Calculate.GetHashCode());
         //כרטיס בטווח של 72 שעות פרמטר 263 יחסום את מאשר מסתייג
         bool bParam263 = false;
         //במידה ותאריך הכרטיס הוא התאריך של היום לא נאפשר את כפתורי מאשר מסתייג
         if (dDateCard.ToShortDateString().Equals(DateTime.Now.ToShortDateString())){        
             btnApprove.Disabled = true;
             btnNotApprove.Disabled = true;
         }
         else
         {
             //פרמטר 252 - אם הכרטיס מעל 45 יום, מאשר מסתייג יהיה חסום
             //אם בחישוב שכר - מאשר מסתייג יהיה חסום   
             //אם הועבר לשכר - מאשר מסתייג יהיה חסום
             //אז מאשר מסתייג יהיה חסום - 99200  אם מספר הימים קטן מ72 שעות (פרמטר 263?) וגם אין סידורים או שיש סידור אחד והוא התייצבות 
            
             bParam252 = clDefinitions.GetDiffDays(dDateCard, DateTime.Now) + 1 > _wcResult.oParam.iValidDays;
             int iDays = clDefinitions.GetDiffDays(dDateCard, DateTime.Now);
             bParam263 = (iDays <= _wcResult.oParam.iDaysToViewWorkCard);//טווח של 72 שעות

             //if (oBatchManager.htFullEmployeeDetails == null)
             //    bWorkCardEmpty = true;
             //else
             //   bWorkCardEmpty = ((iDays <= oBatchManager.oParam.iDaysToViewWorkCard) && ((oBatchManager.htFullEmployeeDetails.Count == 0) || ((oBatchManager.htFullEmployeeDetails.Count == 1) && (((SidurDM)oBatchManager.htFullEmployeeDetails[0]).iMisparSidur == SIDUR_HITYAZVUT))));

            //** if ((iMisparIshi == int.Parse(LoginUser.UserInfo.EmployeeNumber)) && (!bChishuvShachar) && (!bWcIsUsed) && (!bParam252) && (!((bParam263) && (CardIsEmpty(oBatchManager)))))
             if ((iMisparIshi == int.Parse(LoginUser.UserInfo.EmployeeNumber)) && (!bChishuvShachar)  && (!bParam252) && (!((bParam263) && (CardIsEmpty(oBatchManager)))))
             {                                   
                 //אם הגענו מעמדת נהג, נאפשר את מאשר מסתייג
                 //רק במידה ולא נעשה שינוי בכרטיס    
                 // או שנעשה שינוי בכרטיס וקיים סידור מפה או סידורים המיוחדים (שמקורם במטלה) מסומנים כלא לתשלום עם קוד סיבה 16 או שלא קיים כלל סידור מפה או מיוחד (שמקורו במטלה             
                 bool bSidurimLoLetashlum = IsSidurimLoLetashlumAndLoHitychasut();
                 btnApprove.Disabled = (bWorkCardWasUpdate) && (!bSidurimLoLetashlum);
                 btnNotApprove.Disabled = (bWorkCardWasUpdate) && (!bSidurimLoLetashlum);
                 
                 //במידה ומתקיימים התנאים הבאים נחסום את כפתור מאשר
                 if (btnApprove.Disabled==false)                
                    btnApprove.Disabled = DisabledMeasherBtn();
             }
            else{                                                                                                  
                    btnApprove.Disabled = true;
                    btnNotApprove.Disabled = true;                    
                }           
         }
         // btnPrint.Enabled = true;  
         //btnPrint.Attributes.Remove("disabled");
         //btnPrint.Attributes.Add("disabled", "");
         //btnPrint.Attributes.Add("class","btnWorkCardPrint");
         EnableCtl(btnPrint, false, "btnWorkCardPrint");
         clGeneral.enMeasherOMistayeg oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)_wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
         switch (oMasherOMistayeg)
         {
             case clGeneral.enMeasherOMistayeg.ValueNull:
                 if ((!bRashemet) && (clDefinitions.GetDiffDays(dDateCard, DateTime.Now) + 1 <= _wcResult.oParam.iValidDays))
                 {
                     //btnPrint.Enabled = false;    
                     //btnPrint.Attributes.Remove("disabled");
                     //btnPrint.Attributes.Add("disabled", "disabled");
                     //btnPrint.Attributes.Add("class", "btnWorkCardPrintDis");
                     EnableCtl(btnPrint, true, "btnWorkCardPrintDis");
                 }               
                 break;
             //case clGeneral.enMeasherOMistayeg.Measher:               
             //    if (!btnApprove.Disabled)
             //    {
             //        btnApprove.Attributes.Add("class", "ImgButtonApprovalChecked");
             //        btnNotApprove.Attributes.Add("class", "ImgButtonDisApprovalRegularDisabled"); 
             //    }               
             //    break;
             //case clGeneral.enMeasherOMistayeg.Mistayeg:                
             //    if (!btnNotApprove.Disabled)
             //    {
             //        btnNotApprove.Attributes.Add("class", "ImgButtonDisApproveChecked");
             //        btnApprove.Attributes.Add("class", "ImgButtonApprovalRegularDisabled"); 
             //    }
             //    break;

             default:                
                 if (!btnApprove.Disabled) 
                     btnApprove.Attributes.Add("class", "ImgButtonApprovalRegular"); 
                 if (!btnNotApprove.Disabled)
                     btnNotApprove.Attributes.Add("class", "ImgButtonDisApprovalRegular"); 
                 //btnPrint.Enabled = true;

                 //btnPrint.Attributes.Remove("disabled");
                 //btnPrint.Attributes.Add("disabled", "");
                 //btnPrint.Attributes.Add("class", "btnWorkCardPrint");
                 EnableCtl(btnPrint, false, "btnWorkCardPrint");
                 break;
         }
         SetImageForButtonMeasherOMistayeg();
         hidMeasherMistayeg.Value = oMasherOMistayeg.GetHashCode().ToString();
     }
     protected void EnableCtl(Button bCtl, bool bValue, string sClass)
     {
        //bCtl.Attributes.Add("disabled", bValue.ToString());
        bCtl.Enabled = (!bValue);
        bCtl.Attributes.Add("class", sClass);
     }
     protected bool IsPeilutEilatExist()
     {
         bool bPeilutEilat = false;

         SidurDM _Sidur;
         for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
         {
             _Sidur = ((SidurDM)_wcResult.htFullEmployeeDetails[i]);
             if (SD.IsOnatiyutInPeilutExists(ref _Sidur))
             {
                 bPeilutEilat = true;
                 break;
             }
         }
         return bPeilutEilat;
     }
     protected bool IsCarNumberErrorExists()
     {
         bool bErrExists = false;

         if (_wcResult.dtErrors!=null)
             if (_wcResult.dtErrors.Rows.Count > 0)
                 bErrExists = (_wcResult.dtErrors.Select("check_num=69").Length > 0);

         return bErrExists;
     }
     protected bool DisabledMeasherBtn()
     {
         //1.  קיים בכרטיס סידור ויזה - זיהוי סידור ויזה לפי  TB_SIDURIM_OVDIM.SECTOR_VISA <> null.
         //2.  קיימת שגיאה על מספר רכב - קיימת לפחות שגיאה אחת על מספר רכב (שגיאה 69).
         //3.  קיימת נסיעת אילת - קיימת לפחות פעילות אחת מסוג שירות ( (Mispar_knisa=0 שיש לה 71=Onatiut והיא מסוג אילת (לפי פניה 
         //    לקטלוג נסיעות וקבלת ערך 1  בשדה GetKavDetails.EilatTrip=1 . תנאים זהים להצגת לינק על מספר סידור במקרה של נסיעת אילת.
         //4.  קיים סידור מטלה שלא תורגמה למספר סידור תקני - מספר סידור בין הספרות 1 ל- 999 (רק אם קיים מק"ט תקני ניתן לתרגם את 
         //    המטלה למספר סידור תיקני). 
         //5.  "כרטיס עבודה ריק" - שני מקרים שונים: א. כרטיס ריק לחלוטין מסידורים (למעט 99200) ב. בכרטיס לפחות סידור מיוחד (מתחיל  
         //     בספרות 99) אחד ללא מאפיין 90 והסידור לא מקורו במטלה. זיהוי סידור שמקורו במטלה לפי שבאחת הרשומות של הפעילויות 
         //     בסידור 0< TB_peilut_Ovdim. Mispar_matala.
         bool bDisable = false;

         if (_wcResult.htFullEmployeeDetails == null)
             bDisable = true;
         else
         {
             //|| (IsCarNumberErrorExists())
             bDisable = (((_wcResult.htFullEmployeeDetails.Count == 0) || CardIsEmpty(oBatchManager))
                        || (IsSidurVisa())
                        || (IsSidurMatalaNotValidExists())
                        || (IsPeilutEilatExist())                        
                        || IsSidurChosem()
                        || (IsCarNumberErrorExists()));
         }
        

         return bDisable;
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
   
     //protected bool IsRashemetProfile()
     //{
     //    bool bRashemet = false;
     //    try
     //    {
     //        for (int i = 0; i < LoginUser.UserProfiles.Length; i++)
     //        {
     //            if ((LoginUser.UserProfiles[i].Role == clGeneral.enProfile.enRashemet.GetHashCode())
     //                 || (LoginUser.UserProfiles[i].Role == clGeneral.enProfile.enRashemetAll.GetHashCode())
     //                 || (LoginUser.UserProfiles[i].Role == clGeneral.enProfile.enSystemAdmin.GetHashCode())
                     
     //                )
     //            {
     //                bRashemet = true;
     //                break;
     //            }
     //        }
     //        return bRashemet;
     //    }
     //    catch (Exception ex)
     //    {
     //        throw ex;
     //    }
     //}
    
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
        
         clnDate.Attributes.Add("onclick", "SetRefreshBtn(true);");
         clnDate.Attributes.Add("OnChangeCalScript", "SetRefreshBtn(true);");

         ErrorImage(imgIdErr, false);
         ErrorImage(imgTimeErr, false);
         ErrorImage(imgLinaErr, false);
         ErrorImage(imgHalbErr, false);
         ErrorImage(imgDayHaslamaErr, false);
         if (LoginUser.IsLimitedUser)
         {
            // btnDrvErrors.Style.Add("Display", "block");
             lnkLastUpdateUser.Disabled = true;            
             lblId.Style.Add("Display", "block");
             lnkId.Style.Add("Display", "none");
         }
         else
         {
           //  btnDrvErrors.Style.Add("Display", "none");
             lnkLastUpdateUser.Disabled = false;
             lblId.Style.Add("Display", "none");
             lnkId.Style.Add("Display", "block");
         }
         //btnRefreshOvedDetails.Enabled = false;
     }
     protected void SetIdkuneyRashemet()
     {
         try
         {
             ddlLina.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "LINA"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
             ddlTachograph.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "TACHOGRAF"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
             ddlTravleTime.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "BITUL_ZMAN_NESIOT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
             ddlHalbasha.Enabled = IsHalbasha();//(!clWorkCard.IsIdkunExists(clWorkCard.ErrorLevel.LevelYomAvoda, 5, 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));            
             ddlHashlamaReason.Enabled = (!clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "SIBAT_HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
             HashlamaForDayValue.Disabled = (clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));
             Hamara.Disabled = (clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HAMARAT_SHABAT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet));

             SD.dtIdkuneyRashemet = dtIdkuneyRashemet;
             Session["IdkuneyRashemet"] = SD.dtIdkuneyRashemet;
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     private void FillWorkCardResult(WorkCardResult wcResult, int misparIshi, DateTime cardDate)
     {
         int iLast = 0;
         OrderedDictionary htSpecialEmployeeDetails, htFullEmployeeDetails;
         var ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
         var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);

         //WorkCardResult WCResult = new WorkCardResult() { Succeeded = false };
         //if (ovedDetails.Rows.Count > 0)
         //{
             wcResult.htEmployeeDetails = ovedManager.GetEmployeeDetails(false,ovedDetails, cardDate, misparIshi, out iLast, out htSpecialEmployeeDetails, out htFullEmployeeDetails);
             wcResult.htFullEmployeeDetails = htFullEmployeeDetails;
             wcResult.Succeeded = true;
           
         //}
         var cacheAged = ServiceLocator.Current.GetInstance<IKDSAgedQueueParameters>();
         wcResult.oParam = cacheAged.GetItem(cardDate);

        bool byNum = cardDate < wcResult.oParam.dParam319 ? true : false;
        wcResult.dtMashar = GetMasharData(wcResult.htEmployeeDetails, byNum);

        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
         wcResult.dtLookUp = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables);
         wcResult.dtSugSidur = cache.GetCacheItem<DataTable>(CachedItems.SugeySidur);


         wcResult.oOvedYomAvodaDetails = ovedManager.CreateOvedDetails(misparIshi, cardDate);
         wcResult.oMeafyeneyOved = ovedManager.CreateMeafyenyOved(misparIshi, cardDate);
         wcResult.dtDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);

         
         
     }

     private void SetParameters(DateTime dCardDate)
     {
         
         var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
         var dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
         var dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
         int iSugYom = DateHelper.GetSugYom(dtYamimMeyuchadim, dCardDate, dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

         var cacheAge = ServiceLocator.Current.GetInstance<IKDSAgedQueueParameters>();
         var param = cacheAge.GetItem(dCardDate);
         if (param == null)
         {
             IParametersManager paramManager = ServiceLocator.Current.GetInstance<IParametersManager>();
             var oParam = paramManager.CreateClsParametrs(dCardDate, iSugYom);
             cacheAge.Add(oParam, dCardDate);
         }

     }
     protected bool RunBatchFunctions()
     {
         bool bResult = true;
         clDefinitions _Defintion = new clDefinitions();
         bool bLoadNewCard=false;
        // bool bSaveChange = true;
         try
         {
             IOvedManager ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
             var oOvedYomAvodaDetails = ovedManager.CreateOvedDetails(iMisparIshi, dDateCard);
            
             if (ViewState["LoadNewCard"] != null)
                 bLoadNewCard = (bool.Parse(ViewState["LoadNewCard"].ToString()) == true);
             if ((hidChanges.Value.ToLower() != "true") &&
                 (((oOvedYomAvodaDetails.iStatus == CardStatus.Calculate.GetHashCode() || oOvedYomAvodaDetails.iBechishuvSachar == clGeneral.enBechishuvSachar.bsActive.GetHashCode()) && (!Page.IsPostBack) && (Request.QueryString["WCardUpdate"] == null))
                    || ((Request.QueryString["WCardUpdate"] == null) && (oOvedYomAvodaDetails.iStatus == CardStatus.Calculate.GetHashCode() || oOvedYomAvodaDetails.iBechishuvSachar == clGeneral.enBechishuvSachar.bsActive.GetHashCode()))
                 ))     
             // || oOvedYomAvodaDetails.iBechishuvSachar == clGeneral.enBechishuvSachar.bsActive.GetHashCode()     
             {       
                 // oBatchManager.InitGeneralData();
                 _wcResult.CardStatus = (CardStatus)oOvedYomAvodaDetails.iStatus;
                 ViewState["CardStatus"] = (CardStatus)oOvedYomAvodaDetails.iStatus;
                 bInpuDataResult = true;
                 bResult = true;
             }
             else
             {
                 if (oOvedYomAvodaDetails.iBechishuvSachar != clGeneral.enBechishuvSachar.bsActive.GetHashCode())
                 {
                     //if (bWcIsUsed && (CardStatus)oOvedYomAvodaDetails.iStatus == CardStatus.Error)
                     //    bSaveChange = false;
                     
                 //שינויי קלט
                 if (!(hidExecInputChg.Value.Equals("0")))
                 {
                     var result = oBatchManager.MainInputDataNew(iMisparIshi, dDateCard);
                     bInpuDataResult = result.IsSuccess;
                     if (!bInpuDataResult)
                         //שינויי קלט
                         bResult = false;

                    
                     /**********************/
                     //bInpuDataResult = oBatchManager.MainInputData(iMisparIshi, dDateCard);
                     //if (!bInpuDataResult)
                     //    //שינויי קלט
                     //    bResult = false;
                 }
                 else { hidExecInputChg.Value = ""; }
                 if (bResult)
                 {
                     //שגויים
                     if ((hidErrChg.Value.Equals("")) || ((hidErrChg.Value.Equals("0"))))
                     {
                         //New code for errors
                         var result = oBatchManager.MainOvedErrorsNew(iMisparIshi, dDateCard);
                         bResult = result.IsSuccess;
                         ViewState["CardStatus"] = result.CardStatus;
                         Session["Errors"] = result.Errors;
                         _wcResult.dtErrors = result.Errors;
                         _wcResult.CardStatus = result.CardStatus;
                        
                        //This is the old code for validiating new
                         //bInpuDataResult = oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
                         //bResult = bInpuDataResult;
                         //ViewState["CardStatus"] = oBatchManager.CardStatus;
                         //Session["Errors"] = oBatchManager.dtErrors;
                     }
                         else
                         {
                         hidErrChg.Value = "0";
                         _wcResult.CardStatus = (CardStatus)ViewState["CardStatus"];
                     } //נחזיר את הדגל כך שיקראו לשגויים בפעם הבאה }
                 }
             }
             }
             if (!bResult)
             {
                 //FreeWC();
                 if ((Request.QueryString["Page"] != null) || ((Session["arrParams"] != null)))
                 {
                     //string sScript = "alert('לא ניתן לעלות כרטיס עבודה'); window.location.href = '" + this.PureUrlRoot + "/Main.aspx';";
                     string sScript = "alert('לא ניתן לעלות כרטיס עבודה'); CloseWindow();";
                     ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "InputDataFailed", sScript, true);
                 }
                 else
                 {
                     string sScript = "alert('לא ניתן לעלות כרטיס עבודה'); window.close();";
                     ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "InputDataFailed", sScript, true);
                 }   
             }

             if (bResult) 
                 FillWorkCardResult(_wcResult, iMisparIshi, dDateCard);
             return bResult;
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
     //private void FreeWC()
     //{
     //    wsGeneral oGeneral = new wsGeneral();
     //    oGeneral.FreeWC(int.Parse(txtId.Text), clnDate.Text, int.Parse(hidMiMeadkenOL.Value));
     //}

     protected bool SetNextErrorCardDate()
     {
         string sNextErrorCardDate;
         bool bFound = false;

         clWorkCard _WorkCard = new clWorkCard();

         sNextErrorCardDate = clWorkCard.GetNextErrorCard(int.Parse(txtId.Text), DateTime.Parse(clnDate.Text)).ToShortDateString();
         bFound = (!(sNextErrorCardDate.Equals(clnDate.Text)));
         //if (bFound)
         //    FreeWC();
         clnDate.Text = sNextErrorCardDate;        
         return bFound;
     }
     protected void SetRashemetVars(bool bRashemet, bool bMenahelBankShaot)
     {
         Session["ProfileRashemet"] = bRashemet;
         Session["ProfileMenahelBankShaot"] = bMenahelBankShaot;
         hidRashemet.Value = bRashemet ? "1" : "0";
         hidMenahelBank.Value = bMenahelBankShaot ? "1" : "0";
         //כפתור מסך עדכונים לעובד
         btnDrvErrors.Style.Add("Display", "block");  
         //כפתור השגוי הבא יוצג רק לרשמת/מנהל מערכת        
         if (bRashemet || bMenahelBankShaot)
         {         
             btnNextErrCard.Style.Add("Display", "block");
             //btnNextCard.Style.Add("Display", "block");
             //btnPrevCard.Style.Add("Display", "block");
         }
         else
         {           
             btnNextErrCard.Style.Add("Display", "none");
             //btnNextCard.Style.Add("Display", "none");
             //btnPrevCard.Style.Add("Display", "none");
         }  
     }
     protected void SetDriverSource()
     {             
         if (Request.QueryString["Page"]!=null)
         {
           if (Request.QueryString["Page"].ToString() == "2") //עמדת נהג דרך חישוב חודשי
                 hidSource.Value = "2";
           if (Request.QueryString["Page"].ToString() == "1") //עמדת נהג דרך כרטיסי עבודה
                 hidSource.Value = "1";  
         }       
         else
         {            
             if (Session["arrParams"] != null)
                 hidSource.Value = "1"; //עמדת נהג
             else
                 hidSource.Value = "0"; // PC
         }        
     }
     protected void SetUpdateBtnVisibility(string sDisabled)
     {
         btnUpdateCard.Attributes.Add("disabled", sDisabled);        
         hidUpdateBtn.Value = sDisabled;   
         if (sDisabled.Equals("true"))
             btnUpdateCard.Attributes.Add("class", "btnWorkCardUpadteDis");
         else
             btnUpdateCard.Attributes.Add("class", "btnWorkCardUpadte");

     }
     protected void SetToolTipForID()
     {
         string sToolTip = "";
         if (_wcResult.oMeafyeneyOved.IsMeafyenExist(3))
             sToolTip = "שעת התחלה מותרת (3): " + DateHelper.ConvertToValidHour(_wcResult.oMeafyeneyOved.GetMeafyen(3).Value) + "\n";
         else
             sToolTip = "שעת התחלה מותרת (3): 0 \n";

        
         if (_wcResult.oMeafyeneyOved.IsMeafyenExist(4))
             sToolTip = sToolTip + "שעת גמר מותרת (4): " + DateHelper.ConvertToValidHour(_wcResult.oMeafyeneyOved.GetMeafyen(4).Value);
         else
             sToolTip = sToolTip + "שעת גמר מותרת (4): 0 ";


         txtId.ToolTip = sToolTip;
     }
     protected void SetNextPrevBtn()
     {//מעמדת נהג לא נציג את כפתורי הבא/קודם
         if (hidSource.Value =="0")
         {
             btnNextCard.Style.Add("Display", "block");
             btnPrevCard.Style.Add("Display", "block");
         }
         else
         {
             btnNextCard.Style.Add("Display", "none");
             btnPrevCard.Style.Add("Display", "none");
         }
     }
     protected void UnloadPage()
     {        
        
     }
     protected void LoadPage()
     {
         DataTable dtLicenseNumbers = new DataTable();
         clWorkCard _WorkCard = new clWorkCard();
         try
         {
           
             ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
             //אם נלחץ השגוי הבא, נמצא את התאריך של הכרטיס הבא השגוי
             if (hidNextErrCard.Value.Equals("1"))
             {
                 if (!SetNextErrorCardDate())
                     //לא נמצא השגוי הבא
                     hidNextErrCard.Value = "2";
                 else
                     hidNextErrCard.Value = "0";
             }
                           

             //הרשאות לדף
             SetSecurityLevel();

             //אתחול פרמטרים
             SetEmployeeCardData();
             hidPratim.Value = iMisparIshi.ToString() + "|" + dDateCard.ToShortDateString();
             //**int iMiMeadkenOL = _WorkCard.SaveWCInUsed(iMisparIshi, dDateCard, int.Parse(LoginUser.UserInfo.EmployeeNumber.ToString()));
             //if (iMiMeadkenOL == 0)
             //    bWcIsUsed = false;
             //else
             //{
             //    if (iMiMeadkenOL == int.Parse(LoginUser.UserInfo.EmployeeNumber))
             //        bWcIsUsed = false;
             //    else bWcIsUsed = true;
             //}
             //if (iMiMeadkenOL == 0)
             //    hidMiMeadkenOL.Value = LoginUser.UserInfo.EmployeeNumber;
             //else
             //**    hidMiMeadkenOL.Value = iMiMeadkenOL.ToString();

             oBatchManager = new clBatchManager();
             SetPageDefault();
             bRashemet =  LoginUser.IsRashemetProfile(LoginUser);
             bMenahelBankShaot = LoginUser.IsMenahelBankShaot(LoginUser);

             SetRashemetVars(bRashemet, bMenahelBankShaot);
             hidFromEmda.Value = (LoginUser.IsLimitedUser && arrParams[2].ToString() == "1") ? "true" : "false";
             iMisparIshiIdkunRashemet = ((int.Parse)(LoginUser.UserInfo.EmployeeNumber)).Equals(iMisparIshi) ? iMisparIshi : 0;

           //  oBatchManager.iLoginUserId =int.Parse(LoginUser.UserInfo.EmployeeNumber);
             Session["LoginUserEmp"] = LoginUser.UserInfo.EmployeeNumber;
            
             _wcResult = new WorkCardResult() { Succeeded = true };
             SetParameters(dDateCard);
             //שינויי קלט ושגויים
             if (RunBatchFunctions())
             {
                 
                 //רוטינת שגויים                            
                 //נתונים כללים שמגיעים מאובייקט שגויים ושינויי קלט
                 //SetGeneralData(oBatchManager);

                 //set tool tip for txtId
                 SetToolTipForID();

                 //איתחול ה- USERCONTROL
                 InitSidurimUserControl();

                 //טבלאות פענוח
                 SetLookUpSidurim();
                 SetImageForButtonValiditiy();

                 ////אישורים               
                // GetApproval();
                 SD.btnHandler += new Modules_UserControl_ucSidurim.OnButtonClick(SD_btnHandler);
                 //SD.btnReka +=new Modules_UserControl_ucSidurim.OnButtonClick(SD_btnReka);
                 //if ((!Page.IsPostBack) || (bool.Parse(ViewState["LoadNewCard"].ToString())))
                 if ((!Page.IsPostBack) || (hidRefresh.Value.Equals("1")))
                 {                     
                     Session["OvedYomAvodaDetails"] = _wcResult.oOvedYomAvodaDetails;
                     Session["Errors"] = _wcResult.dtErrors;
                     Session["Parameters"] = _wcResult.oParam;
                     Session["MeafyenyOved"] = _wcResult.oMeafyeneyOved;
                     dtIdkuneyRashemet = clWorkCard.GetIdkuneyRashemet(iMisparIshi, dDateCard);
                     btnUpdateCard.Attributes.Add("disabled", hidUpdateBtn.Value);                  
                     if (!Page.IsPostBack)
                     {
                         SetUpdateBtnVisibility("true");                                           
                         BindTachograph();
                         SetLookUpDDL();
                         ShowOvedCardDetails(iMisparIshi, dDateCard);
                         SetImageForButtonMeasherOMistayeg();
                     }
                     //אם הגענו מעמדת נהג, נשמור 1 אחרת 0
                     SetDriverSource();
                     SetNextPrevBtn();
                     //hidSource.Value = ((Request.QueryString["Page"] != null) || (Session["arrParams"] != null)) ? "1" : " 0";
                     Session["hidSource"] = hidSource.Value;
                     if (dtPakadim == null)
                         if ((DataTable)Session["Pakadim"] == null)
                         {
                             dtPakadim = GetMasachPakadim();
                             Session["Pakadim"] = dtPakadim;
                         }
                         else
                             dtPakadim = (DataTable)Session["Pakadim"];

                     SD.dtPakadim = dtPakadim;
                     dtSadotNosafim = clWorkCard.GetSadotNosafimLesidur();
                     dtMeafyeneySidur = clWorkCard.GetMeafyeneySidur();
                     Session["SadotNosafim"] = dtSadotNosafim;
                     Session["MeafyeneySidur"] = dtMeafyeneySidur;

                     //עידכוני רשמת
                     SetIdkuneyRashemet();
                     RefreshEmployeeData(iMisparIshi, dDateCard);
                     //רק אם יש סידורים 
                     Session["Sidurim"] = _wcResult.htFullEmployeeDetails;
                        

                     if (_wcResult.htFullEmployeeDetails != null)
                     {
                         SD.DataSource = _wcResult.htFullEmployeeDetails;

                         Session["Mashar"] = _wcResult.dtMashar;
                         SD.Mashar = _wcResult.dtMashar;
                     }
                   
                     ViewState["LoadNewCard"] = true;
                     SD.ErrorsList = _wcResult.dtErrors;
                     SD.SadotNosafim = dtSadotNosafim;
                     SD.MeafyeneySidur = dtMeafyeneySidur;
                     SD.dtPakadim = (DataTable)Session["Pakadim"];
                 }
                 else
                 {
                     SD.CardDate = dDateCard;
                     SD.DataSource = (OrderedDictionary)Session["Sidurim"];
                     SD.Mashar = (DataTable)Session["Mashar"];
                    
                     _wcResult.dtErrors = (DataTable)Session["Errors"];
                     _wcResult.oParam = (clParametersDM)Session["Parameters"];
                     dtPakadim = (DataTable)Session["Pakadim"];
                     SD.ErrorsList = (DataTable)Session["Errors"];
                     SD.SadotNosafim = (DataTable)Session["SadotNosafim"];
                     SD.MeafyeneySidur = (DataTable)Session["MeafyeneySidur"];
                     SD.dtPakadim = (DataTable)Session["Pakadim"];
                     SD.dtIdkuneyRashemet = (DataTable)Session["IdkuneyRashemet"];                              
                 }
             }          
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
     //protected void Page_Init(object sender, EventArgs e)
     //{
     //    this.MaintainScrollPositionOnPostBack = true;
     //}
     protected void Page_Load(object sender, EventArgs e)
     { 
         LoadPage();  
     }
     protected bool CardIsEmpty(clBatchManager oBatchManager)
     {
         //מחזיר אמת אם הכרטיס ריק או שקיים לו סידור אחד שהוא סידור התייצבות
         return ((_wcResult.htFullEmployeeDetails.Count == 0) || ((_wcResult.htFullEmployeeDetails.Count == 1) && (((SidurDM)_wcResult.htFullEmployeeDetails[0]).iMisparSidur == SIDUR_HITYAZVUT)));
     }
     protected bool DisabledCard()
     {
       int iDays;    
       //  בפתיחת כרטיס עבודה עם TB_YAMEY_AVODA_OVDIM.STATUS=2  (הועבר לשכר) והגורם שפתח את הכרטיס אינו רשמת/רשמת על/מנהל מערכת - יש להציג את הכרטיס כ- disable ולא לאפשר עדכון בכרטיס (ללא תלות במי ומאיפה פתחו את הכרטיס).   
       //4. בפתיחת כרטיס עבודה ללא סידורים ו- sysdate הוא בטווח תאריך כרטיס עבודה + 2 והגורם שפתח את הכרטיס אינו רשמת/רשמת על/מנהל מערכת- יש להציג את הכרטיס כ- disable ולא לאפשר עדכון בכרטיס (ללא תלות מאיפה פתחו את הכרטיס). 
       //TimeSpan ts = DateTime.Now-oBatchManager.CardDate;
       //int iDays = ts.Days; //ההפרש בימים בין התאריך של הכרטיס לתאריך של היום
       iDays = clDefinitions.GetDiffDays(dDateCard, DateTime.Now);
       return (((_wcResult.oOvedYomAvodaDetails.iStatus == CardStatus.Calculate.GetHashCode()) && (!bRashemet) && (!bMenahelBankShaot))
             || ((iDays <= _wcResult.oParam.iDaysToViewWorkCard) && (!bRashemet) && (!bMenahelBankShaot) && CardIsEmpty(oBatchManager))
             || (WorkCardWasUpdateAndDriver(bWorkCardWasUpdate)));
     }
     protected bool WorkCardWasUpdateAndDriver(bool bWorkCardWasUpdate)
     {
     //עדכון כרטיס ע"י עובד -  אם המ.א ב-  User   LogIn שווה למ.א בכרטיס העבודה יש לחסום עדכון כרטיס עבודה במקרים הבאים:
     //א. גורם אחר התחיל לטפל בכרטיס: מזהים שגורם אחר נגע בכרטיס אם קיימת רשומה למ.א ותאריך כרטיס עבודה בטבלה 
     //  TB_MEADKEN_ACHARON (טבלה זו מכילה רשומות עבור מ.א+תאריך בהם בוצע עדכון לא ע"י המ.א אישי של כרטיס העבודה).
     //ב.  תאריך כרטיס עבודה קטן מ - sysdate(פרמטר 252 ) פחות 45 יום.
         //TimeSpan ts = DateTime.Now-oBatchManager.CardDate;
         //int iDays = ts.Days; //ההפרש בימים בין התאריך של הכרטיס לתאריך של היום
         int iDays;
         iDays = clDefinitions.GetDiffDays(dDateCard, DateTime.Now);
         bool bWorkCardWasUpdateAndDriverInsert=false;

         if ((iMisparIshi == int.Parse(LoginUser.UserInfo.EmployeeNumber)) && ((bWorkCardWasUpdate) || (iDays + 1 > _wcResult.oParam.iValidDays)))
             bWorkCardWasUpdateAndDriverInsert = true;

         return bWorkCardWasUpdateAndDriverInsert;
     }
     protected void RenderPage()
     {
         bool bCalculateAndNotRashemet=false;
         string[] sPeilutDetails;
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

             switch (_wcResult.CardStatus)
             {
                 case CardStatus.Valid:
                     //אישורים
                  //   SetApprovalInPage(); /vered 29/4/2012
                     break;
                 case CardStatus.Error:
                     //שגיאות
                     SetErrorInPage();
                     break;
             }

             //מיקום תמונה של תאריכון
             if (Request.UrlReferrer != null) //התחברות מהבית
             {
                 string sDomain = clGeneral.AsDomain(Request.UrlReferrer.ToString()) + Request.ApplicationPath;
                 clnDate.ImageUrl = sDomain + "Images/B_calander.png";
             }
             //התייצבות
             bParticipationAllowed = SetParticipation();

             // if (bParticipationAllowed){   
             if (!bWorkCardWasUpdateRun)
                 bWorkCardWasUpdate = IsWorkCardWasUpdate();
             // }
             //bParticipationAllowed = SetParticipation();
            
             bool bChishuvShachar = _wcResult.oOvedYomAvodaDetails.iBechishuvSachar.Equals(clGeneral.enBechishuvSachar.bsActive.GetHashCode());
             //במידה והכרטיס בסטטוס הועבר לשכר והמשתמש הוא לא רשמת על.רשמת או מנהל מערכת,  נחסום את כל הכרטיס 
             //או שהכרטיס הוא ללא סידורים והתאריך הכרטיס הוא של היום + 2 והמשתמש הוא לא רשמת\רשמת על\מנהל מערכת
             if (DisabledCard())            
                 bCalculateAndNotRashemet = true;
             SetMeasherMistayeg(bChishuvShachar);             
             clGeneral.enMeasherOMistayeg oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)_wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
             //במידה והמשתמש הוא מנהל עם כפופים (לצפיה או לעדכון) וגם המספר האישי של הכרטיס שונה מממספר האישי של המשתמש שנכנס
             //או שהתאריך הוא תאריך של היום. לא נאפשר עדכון כרטיס
             KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
             if ((((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates && (iMisparIshi != int.Parse(LoginUser.UserInfo.EmployeeNumber)))
                  || ((dDateCard.ToShortDateString().Equals(DateTime.Now.ToShortDateString())))))
                  || (bChishuvShachar) || (bCalculateAndNotRashemet) 
                  || (WorkCardWasUpdateAndDriver(bWorkCardWasUpdate)))
                 EnabledFrames(false, (bChishuvShachar || bCalculateAndNotRashemet ));
             else
                 EnabledFrames(true, (bChishuvShachar || bCalculateAndNotRashemet ));
           

             btnHamara.Disabled = (!EnabledHamaraForDay());
             ddlTachograph.Enabled = (EnabledTachograph() && (!bChishuvShachar));
             EnabledFields();
             SecurityManager.AuthorizePage(this, true);
             BindSibotLehashlamaLeyom();
             if ((!Page.IsPostBack) || (bool.Parse(ViewState["LoadNewCard"].ToString())))
                 CreateChangeAttributs();

             SetDDLToolTip();
             string sScript = SendScript(bChishuvShachar, bCalculateAndNotRashemet);
             ViewState["PrintWcFromEmda"] = null;
             if (bAddSidur)
                 sScript = sScript + "SetNewSidurFocus(" + (SD.DataSource.Count - 1).ToString() + ");";                         
             if (SD.AddPeilut != null)
             {
                 sPeilutDetails = SD.AddPeilut.Split(char.Parse("|"));
                 if (sPeilutDetails[0].Equals("1"))                    
                     sScript = sScript + "SetNewPeilutFocus('" + SD.GetPeilutClientKey(sPeilutDetails) + "');";
                
             }                    

             bAddSidur = false;
             SD.AddPeilut = "";
             btnUpdateCard.Attributes.Add("disabled", hidUpdateBtn.Value);
             if (hidUpdateBtn.Value == "false") 
                 sScript = sScript + "$get('btnUpdateCard').className ='btnWorkCardUpadte';";
             //btnUpdateCard.Attributes.Add("class", "btnWorkCardUpadte");  
             else
                 sScript = sScript + "$get('btnUpdateCard').className ='btnWorkCardUpadteDis';";               
                // btnUpdateCard.Attributes.Add("class", "btnWorkCardUpadteDis");  


             ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);

             //if (Session["LoginUserEmp"] == null)
             //    UnloadCard();
         }
         //Before Load page, save field data for compare
         //_WorkCardBeforeChanges = InitWorkCardObject();             

     }
     //private void UnloadCard(Control ctl)
     //{
     //    ScriptManager.RegisterStartupScript(ctl, this.GetType(), "UnloadCard", "alert('זמן ההתחברות הסתיים, יש להכנס מחדש לכרטיס העבודה'); window.close();", true);
     //    //ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "UnloadCard", "alert('זמן ההתחברות הסתיים, יש להכנס מחדש לכרטיס העבודה'); window.close();", true);
     //}
     private string SendScript(bool bChishuvShachar, bool bCalculateAndNotRashemet)
     {
         string sScript = "";
         if ((bChishuvShachar) || (bCalculateAndNotRashemet))
         {
             sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",true);";
             if (bChishuvShachar)
                 sScript = sScript + ChisuvShacharMsg();// " alert('זמנית לא ניתן להפיק כרטיס עבודה זה. אנא נסה במועד מאוחר י
             //else if (bWcIsUsed && (ViewState["PrintWcFromEmda"] == null || ViewState["PrintWcFromEmda"].ToString() != "true"))
             //    sScript = sScript + WCIsUsedMsg();
         }
         else
         {
             sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",false);";
         }
         return sScript;
     }
     //private string GetPeilutClientKey(string[] sPeilutDetails)
     //{
     //   int iPeilutIndex;
     //   iPeilutIndex = ((SidurDM)SD.DataSource[int.Parse(sPeilutDetails[1])]).htPeilut.Count+1 ;
     //   return "SD_" + (sPeilutDetails[1]).PadLeft(3, char.Parse("0")) + "_ctl" + iPeilutIndex.ToString().PadLeft(2, char.Parse("0")) + "_SD_" + (sPeilutDetails[1]).PadLeft(3, char.Parse("0")) + "_ctl" + iPeilutIndex.ToString().PadLeft(2, char.Parse("0")) + "ShatYetiza";
     //}
     private string ChisuvShacharMsg()
     {
         return  " alert('זמנית לא ניתן להפיק כרטיס עבודה זה. אנא נסה במועד מאוחר יותר');"; 
     }
     //private string WCIsUsedMsg()
     //{
     //    string name;
     //    clOvdim oOvdim = new clOvdim();
     //    name = oOvdim.GetOvedFullName(int.Parse(hidMiMeadkenOL.Value));
     //    return " alert('כרטיס נמצא בעדכון משתמש: " + name + ", לצורך ביצוע עדכונים בכרטיס אנא נסה במועד מאוחר יותר');";
     //}
     private bool HasVehicleTypeWithOutTachograph()
     {
         bool HasVehicleType = false;
         DataTable dt;
         DataRow[] dr;
         //יש לבדוק שלפחות אחד הרכבים המדווחים באותו תאריך אינו מדגם 64  (דגם שאינו מכיל טכוגרף). Vehicle_Type =64 במעל"ה.
         try
         {
             if (_wcResult.htFullEmployeeDetails != null)
             {
                bool byNum = dDateCard < _wcResult.oParam.dParam319 ? true : false;
                dt = GetMasharData(_wcResult.htFullEmployeeDetails, byNum); //SD.Mashar;
                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         dr = dt.Select("Vehicle_Type<>" + clGeneral.enVehicleType.NoTachograph.GetHashCode());
                         HasVehicleType = (dr.Length > 0);
                     }
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
         SetOneParticipation(SD.FirstParticipate, txtFirstPart, ddlFirstPart, 1);
         SetOneParticipation(SD.SecondParticipate, txtSecPart, ddlSecPart, 2);

         return bParticipationAllowed;
     }
     protected bool ParticipationAllowed()
     {
         bool _Allowed = false;
         SidurDM _Sidur;
         PeilutDM _Peilut;
         //1. סידור מפה
         //2. סידור מיוחד שמקורו במטלה (מזהים סידור מיוחד שמקורו במטלה לפי שבאחת הרשומות של הפעילויות של הסידור קיים ערך גדול מ- 0 
         if (_wcResult.htFullEmployeeDetails != null)
         {
             for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
             {
                 _Sidur = (SidurDM)(_wcResult.htFullEmployeeDetails[i]);
                 if (!(_Sidur.bSidurMyuhad))
                 {
                     _Allowed = true;
                     break;
                 }
                 else
                 {
                     for (int j = 0; j < _Sidur.htPeilut.Count; j++)
                     {
                         _Peilut = (PeilutDM)_Sidur.htPeilut[j];
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
     protected void SetOneParticipation(SidurDM oSidur, TextBox txtParticipation, DropDownList ddlParticipation, int iIndx)
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
                     && (oSidur.dShatHitiatzvut != null) && (oSidur.dShatHitiatzvut.Year > DateHelper.cYearNull))
                 {
                     txtParticipation.Text = ERROR_IN_SITE;
                     //txtParticipation.ReadOnly = true;
                 }
                 else
                 {
                     if ((oSidur.dShatHitiatzvut != null) && (oSidur.dShatHitiatzvut.Year > DateHelper.cYearNull))
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
             //oMaskTextBox.OnFocusCssClass = "MEFocus";
             oMaskTextBox.OnInvalidCssClass = "MEError";
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
     //protected void GetApproval()
     //{
     //    //if (_StatusCard == enCardStatus.Valid)
     //    //{
     //        // btnCalcItem.Disabled = false;
     //        ApprovalManager _Approvals = new ApprovalManager(LoginUser);
     //        DataTable dt;
            
     //        dt = _Approvals.GetApprovalCodes();
     //        //נכניס ל USER CONTROL SIDURIM
     //        SD.dtApprovals = dt;
     //   // }
     //}
     //protected void SetApprovalInPage()
     //{
     //    //נבדוק אם יש אישורים על שדה השלמה ליום
     //    //if (_StatusCard == enCardStatus.Valid)
     //    //{

     //        DataRow[] dr;
            
     //        bool bEnableApprove = false;
     //        //// btnCalcItem.Disabled = false;
     //        //ApprovalManager _Approvals = new ApprovalManager(LoginUser);
     //        //DataTable dt;
     //        //DataRow[] dr;

     //        //dt = _Approvals.GetApprovalCodes();
     //        ////נכניס ל USER CONTROL SIDURIM
     //        //SD.dtApprovals = dt;
     //        //אישורים לשדה השלמה
     //        dr = SD.dtApprovals.Select("mafne_lesade = 'Hashlama_Leyom'");
     //        if (dr.Length > 0)
     //        {
     //            string sAllApprovalDescription = "";
     //            if (CheckIfApprovalExists(FillApprovalKeys(dr), ref sAllApprovalDescription, ref bEnableApprove))
     //            {
     //                ddlHashlamaReason.Attributes.Add("class", "ApprovalField");                    
     //                imgDayHaslamaErr.Src = "../../Images/ApprovalSign.jpg";
     //                imgDayHaslamaErr.Attributes.Add("ondblclick", "GetAppMsg(this)");
     //                imgDayHaslamaErr.Attributes.Add("App", sAllApprovalDescription);
     //                ErrorImage(imgDayHaslamaErr, true);
     //                ddlHashlamaReason.Enabled = bEnableApprove;
     //                btnHashlamaForDay.Disabled =!bEnableApprove;
     //            }
     //        }
     //    //}
     //}

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

         dsFieldsErrors = clDefinitions.GetErrorsForFields((bRashemet || bMenahelBankShaot), _wcResult.dtErrors, iMisparIshi, dDateCard, sFieldName);
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
             //if (_StatusCard == enCardStatus.Error)
             //{
             //בדיקה אם עובד פעיל
             bErrorExists = (SetOneError(txtId, "mispar_ishi", "imgIdErr"));
             ErrorImage(imgIdErr, bErrorExists);

             //שגיאה 27 ו-150 בדיקת זמן נסיעות
             bErrorExists = SetOneError(ddlTravleTime, "Bitul_Zman_nesiot", "imgTimeErr");
             ErrorImage(imgTimeErr, bErrorExists);
             if (bErrorExists)
                 tdZmanNesiotErr.Style["display"] = "block";
             else
                 tdZmanNesiotErr.Style["display"] = "none";
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
                 imgDayHaslamaErr.Src = "../../Images/!.png";
                 imgDayHaslamaErr.Attributes.Add("ondblclick", "GetErrorMessage(ddlHashlamaReason,1,'');");
             }
            // else           
                 //אם אין שגיאה, נבדוק אם יש אישור
                // SetApprovalInPage(); //vered 29/4/2012
            
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
     private DataTable GetMasharData(OrderedDictionary htEmployeeDetails,bool ByNumOto)
     {
         string sCarNumbers = "";
         DataTable dtLicenseNumber = new DataTable();


        if (ByNumOto)//cardDate < inputData.oParam.dParam319)
        {
            sCarNumbers = ServiceLocator.Current.GetInstance<IKavimManager>().GetMasharCarNumbers(htEmployeeDetails);
            dtLicenseNumber = sCarNumbers != string.Empty ? ServiceLocator.Current.GetInstance<IKavimDAL>().GetMasharData(sCarNumbers) : null;
        }
        else
        {
            sCarNumbers = ServiceLocator.Current.GetInstance<IKavimManager>().GetMasharLicenseNumbers(htEmployeeDetails);
            dtLicenseNumber = sCarNumbers != string.Empty ? ServiceLocator.Current.GetInstance<IKavimDAL>().GetMasharDataByLicense(sCarNumbers) : null;
        }

        //sCarNumbers = ServiceLocator.Current.GetInstance<IKavimManager>().GetMasharCarNumbers(htEmployeeDetails);

        // if (sCarNumbers != string.Empty)
        // {
        //     var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
        //     dtLicenseNumber = kavimDal.GetMasharData(sCarNumbers);
        // }
         return dtLicenseNumber;
     }

     //private void SetGeneralData(clBatchManager oBatchManager)
         //{
     //    //if (oBatchManager.CardStatus!= enCardStatus.Calculate) 
     //    //{
     //    if (_wcResult.htFullEmployeeDetails == null)
     //        oBatchManager.InitGeneralData();
       
     //    //Check if oved musach
     //    //Get Employee Ishurim
     //    //vered 29/4/2012
     //    //if ((!Page.IsPostBack) || (hidRefresh.Value.Equals("1")))
     //    //{
     //    //    ApprovalFactory.RaiseEmployeeWorkDayApprovalCodes(dDateCard, iMisparIshi, 0, clWorkCard.IsOvedMusach(iMisparIshi, dDateCard));
     //    //    arrEmployeeApproval = ApprovalRequest.GetMatchingApprovalRequestsWithStatuses(iMisparIshi, dDateCard);
     //    //    Session["EmployeeApproval"] = arrEmployeeApproval;
     //    //}
     //    //else
     //    //{
     //    //    arrEmployeeApproval = (ApprovalRequest[])Session["EmployeeApproval"];
     //    //}
     //    //vered 29/4/2012
     //   // oBatchManager.InitGeneralData();
        
     //}
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
     //    enMakatType oMakatType;
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
     //            ////    oMakatType = (enMakatType)StaticBL.GetMakatType(lMakatNesia);
     //            ////    switch (oMakatType)
     //            ////    {
     //            ////        case enMakatType.mKavShirut:
     //            ////            dtKavim = oKavim.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);
     //            ////            if (dtKavim.Rows.Count > 0)
     //            ////            {
     //            ////                dr["makat_description"] = dtKavim.Rows[0]["Description"].ToString();
     //            ////                dr["makat_shilut"] = dtKavim.Rows[0]["Shilut"].ToString();
     //            ////                dr["Shirut_type_Name"] = dtKavim.Rows[0]["SugShirutName"].ToString();
     //            ////            }
     //            ////            break;
     //            ////        case enMakatType.mEmpty:
     //            ////            dtKavim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
     //            ////            if (dtKavim.Rows.Count > 0)
     //            ////            {
     //            ////                dr["makat_description"] = dtKavim.Rows[0]["Description"].ToString();
     //            ////                //dr["makat_shilut"] = dtKavim.Rows[0]["Shilut"].ToString();
     //            ////                dr["Shirut_type_Name"] = COL_TRIP_EMPTY;
     //            ////            }
     //            ////            break;
     //            ////        case enMakatType.mNamak:
     //            ////            dtKavim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
     //            ////            if (dtKavim.Rows.Count > 0)
     //            ////            {
     //            ////                dr["makat_description"] = dtKavim.Rows[0]["Description"].ToString();
     //            ////                dr["makat_shilut"] = dtKavim.Rows[0]["Shilut"].ToString();
     //            ////                dr["Shirut_type_Name"] = COL_TRIP_NAMAK;
     //            ////            }
     //            ////            break;
     //            ////        case enMakatType.mElement:
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

         if ((SD.IsAtLeastOneSidurNahagut) && (!HasCardPeiluyot()))
             bTachographAllowed = true;
         else
             bTachographAllowed = HasVehicleTypeWithOutTachograph() && SD.IsAtLeastOneSidurNahagut;

         return bTachographAllowed;
     }
     protected bool HasCardPeiluyot()
     {
         bool bHasPeiluyot = false;
         //מחזיר TRUE אם יש פעילויות בכרטיס
         if (_wcResult.htFullEmployeeDetails != null)
         {
             for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
             {
                 if (((SidurDM)(_wcResult.htFullEmployeeDetails[0])).htPeilut.Count > 0)
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
             int iKodMaamad = String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sKodMaamd) ? 0 : int.Parse(_wcResult.oOvedYomAvodaDetails.sKodMaamd);
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
             bHamaraAllowed = (((_wcResult.htFullEmployeeDetails != null) && (_wcResult.htFullEmployeeDetails.Count > 0)) && 
                 (_wcResult.oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEgged.GetHashCode())
                 //&& (oBatchManager.oMeafyeneyOved.Meafyen31Exists)
                 && (
                     (_wcResult.oOvedYomAvodaDetails.sShabaton == "1") || (_wcResult.oOvedYomAvodaDetails.sSidurDay == enDay.Shabat.GetHashCode().ToString())
                     || ((_wcResult.oOvedYomAvodaDetails.sErevShishiChag.Equals("1")) && (SD.bAtLeastOneSidurInShabat))
                     || ((_wcResult.oOvedYomAvodaDetails.sSidurDay == enDay.Shishi.GetHashCode().ToString()) && (SD.bAtLeastOneSidurInShabat))
                    )
                 //&& ((iKodMaamad == clGeneral.enHrMaamad.PermanentSalariedEmployee.GetHashCode())
                 //    || (iKodMaamad == clGeneral.enHrMaamad.SalariedEmployee12.GetHashCode())
                 //    || (iKodMaamad.ToString().Substring(0, 1).Equals("1")))
                 && ((SD.bAtLeatOneSidurIsNOTNahagutOrTnua))
                 //אם יש ערך במותאם וגם הזמן גדול מאפס לא נאפשר המרה
                 && (!(((_wcResult.oOvedYomAvodaDetails.bMutamutExists)) && (_wcResult.oOvedYomAvodaDetails.iZmanMutamut > 0)))
                 && (!((clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HAMARAT_SHABAT"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))))
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
        int iSidurDay = String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sSidurDay) ? 0 : int.Parse(_wcResult.oOvedYomAvodaDetails.sSidurDay);
       
        //אין לאפשר עדכון עבור עובד שאינו מאגד (Ovdim.Kod_Hevra=580 ) מאגד תעבורה (Ovdim.Kod_Hevra=4895)
        //2. אסור לעדכן ביום שבתון/שבת/ערב חג.
        if ((_wcResult.oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode()) ||
            (_wcResult.oOvedYomAvodaDetails.sShabaton == "1") || (iSidurDay == enDay.Shabat.GetHashCode()))
            return false;
             
        //. עובד 5 ימים - מותר בימים  א-ה כולל ערבי חג בימים אלה. זיהוי עובד 5 ימים לפי לפי ערך 51/52 במאפיין 56 במאפייני עובדים.
        if (((iSidurDay == enDay.Shabat.GetHashCode()) || (_wcResult.oOvedYomAvodaDetails.sShabaton == "1") || ((iSidurDay == enDay.Shishi.GetHashCode())))
            && ((_wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (_wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
            return false;

        //. עובד 6 ימים - מותר בימים  א-ו כולל ערבי חג בימים אלה. זיהוי עובד 6 ימים לפי לפי ערך 61/62 במאפיין 56 במאפייני עובדים
        if ((iSidurDay== enDay.Shabat.GetHashCode()) || (_wcResult.oOvedYomAvodaDetails.sShabaton == "1"))
            if ((((_wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (_wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()))))
                return false;
     
        if (_wcResult.oOvedYomAvodaDetails.sMutamut != clGeneral.enMutaam.enMutaam1.GetHashCode().ToString())
            return false;
       
        return bEnabled;
    }
    protected void EnabledFrames(bool bEnabled,bool bDisabledTabs)
    {
        //bDisabledTabs נתונים ליום עבודה,מתייחס לטאבים של נתוני עובד, התייצבות 
       // tbLblWorkDay.Disabled = (!bEnabled);
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
        //tbSidur.Disabled = (!bEnabled);
        SD.SidurimTable.Enabled = bEnabled;
        //tbBtn.Disabled = (!bEnabled);
        btnFindSidur.Enabled = bEnabled;
        btnAddMyuchad.Enabled = bEnabled;
        btnAddHeadrut.Enabled = bEnabled;

        if (bDisabledTabs)
        {
            tbTabs.Style.Add("disabled", "true");           
        }
    }
    protected void EnabledFields()
    {
        bool bEnable = EnabledHashlamaForDay();
        //לא נאפשר את שדה השלמהליום אם לא התקיימו התנאים ואם לא היה עדכון רשמת
        if (!btnHashlamaForDay.Disabled)
        {
            btnHashlamaForDay.Disabled = ((!bEnable) || ((clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))));
        }
        if (ddlHashlamaReason.Enabled) //אם קבענו כבר שלא ניתן לעדכן לא נשנה את הערך
        {
            ddlHashlamaReason.Enabled = ((bEnable) && (!(clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "SIBAT_HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))));
        }
    }
    private void InitSidurimUserControl()
    {
        SidurDM _FirstSidur =null;

        if (_wcResult.htFullEmployeeDetails != null)
        {
            if (_wcResult.htFullEmployeeDetails.Count > 0)
            {
                _FirstSidur = ((SidurDM)_wcResult.htFullEmployeeDetails[0]);
            }
        }
        SD.Param1 = _wcResult.oParam.dSidurStartLimitHourParam1;           
        SD.Param80 = _wcResult.oParam.dNahagutLimitShatGmar;
        SD.Param3 = _wcResult.oParam.dSidurEndLimitHourParam3;
        SD.Param29 = _wcResult.oParam.dStartHourForPeilut;
        SD.Param30 = _wcResult.oParam.dEndHourForPeilut;
        SD.Param101 = _wcResult.oParam.iHashlamaYomRagil;
        SD.Param102 = _wcResult.oParam.iHashlamaShisi;
        SD.Param103 = _wcResult.oParam.iHashlamaShabat;
        SD.Param242 = _wcResult.oParam.dShatGmarNextDay;
        SD.Param244 = _wcResult.oParam.dShatHatchalaNahagutNihulTnua;
        SD.Param276 = _wcResult.oParam.dShatHatchalaGrira;
        SD.Param252 = _wcResult.oParam.iValidDays;
        SD.Param319= _wcResult.oParam.dParam319;
        SD.RefreshBtn = (hidRefresh.Value!=string.Empty) ? int.Parse(hidRefresh.Value) : 0;
        
     
        if ((dDateCard.DayOfWeek==System.DayOfWeek.Friday)) 
            SD.NumOfHashlama = _wcResult.oParam.iHashlamaMaxShisi; //109
        else if(dDateCard.DayOfWeek==System.DayOfWeek.Saturday)
            SD.NumOfHashlama = _wcResult.oParam.iHashlamaMaxShabat; //110
        else
            SD.NumOfHashlama = _wcResult.oParam.iHashlamaMaxYomRagil; //108

        if (_wcResult.htFullEmployeeDetails != null)
        {
            if (_wcResult.htFullEmployeeDetails.Count > 0)
            {
                if (_FirstSidur.sErevShishiChag.Equals("1"))
                    SD.NumOfHashlama = _wcResult.oParam.iHashlamaMaxShisi; //109
                else if (_FirstSidur.sShabaton.Equals("1"))
                    SD.NumOfHashlama = _wcResult.oParam.iHashlamaMaxShabat; //110
            }
        }
        SD.Param41 = _wcResult.oParam.iZmanChariga;
        SD.Param149 = _wcResult.oParam.fOrechNesiaKtzaraEilat;
        SD.SugeySidur = _wcResult.dtSugSidur;
        SD.MeafyenyOved = _wcResult.oMeafyeneyOved;
        SD.KdsParameters = _wcResult.oParam;
        SD.OvedYomAvoda = _wcResult.oOvedYomAvodaDetails;
        SD.MeafyeneyElementim = GetMeafyeneyElementim();
        SD.CardDate = dDateCard;
        SD.MisparIshi = iMisparIshi;
        //SD.EmployeeApproval = arrEmployeeApproval; //vered 29/4/2012
        SD.dtSidurim = _wcResult.dtDetails;
        SD.UpEmpDetails = upEmployeeDetails;
        SD.KnisatShabat = _wcResult.oParam.dKnisatShabat;
        SD.ProfileRashemet = bRashemet;
        SD.ProfileMenahelBankShaot = bMenahelBankShaot;
        SD.MisparIshiIdkunRashemet = iMisparIshiIdkunRashemet;
        SD.LoginUserId = (int.Parse(LoginUser.UserInfo.EmployeeNumber));
        SD.Param98 = _wcResult.oParam.iMaxMinutsForKnisot;
        SD.Param42 = _wcResult.oParam.fFactor;
        SD.Param43 = _wcResult.oParam.fFactorNesiotRekot;
        SD.MeasherOMistayeg =  (clGeneral.enMeasherOMistayeg)_wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
        
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
    private void DefineZmaniNesiotNavigatePage(DateTime dDateCard)
    {
        string sMeafyenEmployeeValue = "";
        string sMeafyenEmployeeType = "";
        if (_wcResult.oMeafyeneyOved.IsMeafyenExist(51))
        {
            sMeafyenEmployeeValue = _wcResult.oMeafyeneyOved.GetMeafyen(51).Value;
            sMeafyenEmployeeType = "51";
        }
        else
        {
            if (_wcResult.oMeafyeneyOved.IsMeafyenExist(61))
            {
                sMeafyenEmployeeValue = _wcResult.oMeafyeneyOved.GetMeafyen(61).Value;
                sMeafyenEmployeeType = "61";
            }
        }
        if (sMeafyenEmployeeValue != string.Empty)
        { //אם קיים מאפיין 51 או 61, ניצור לינק לפתיחת דף זמני נסיעות, אחרת נכניס מלל זמני ניסעות
            HyperLink lnkZmaneyNesiot = new HyperLink();
            lnkZmaneyNesiot.ID = "lnkTravelTime";
            lnkZmaneyNesiot.Text = "זמן נסיעות";
            lnkZmaneyNesiot.Style.Add("text-decoration", "underline");
            lnkZmaneyNesiot.Style.Add("cursor", "pointer");
            ddlTravleTime.Attributes.Add("MeafyenVal", sMeafyenEmployeeValue == string.Empty ? "" : sMeafyenEmployeeValue.Substring(0, 1));
            ddlTravleTime.Attributes.Add("OrgVal", _wcResult.oOvedYomAvodaDetails.sBitulZmanNesiot);
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
            //lblNesiot.ID = "lblTravelTime";
            lblNesiot.Text = "זמן נסיעות";
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
        DataView dv = new DataView(_wcResult.dtLookUp);

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
        DataView dv = new DataView(_wcResult.dtLookUp);

        try
        {
              var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
            //חריגה
            dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbDivuchHarigaMeshaot, "'");
            SD.DDLChariga = dv;

            //סיבות לדיווח ידני
            dvSibotLedivuch = new DataView(cache.GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani));
            SD.DDLSibotLedivuch = dvSibotLedivuch;


            //פיצול הפסקה
            dv = new DataView(_wcResult.dtLookUp);
            dv.RowFilter = string.Concat("table_name='", clGeneral.cCtbPitzulaHafsaka, "'");
            SD.DDLPitzulHafsaka = dv;
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
        DataView dv = new DataView(_wcResult.dtLookUp);

       
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
        if (_wcResult.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString() == "0")
            ddlHashlamaReason.SelectedValue = "-1";
        else
        {
            for (int i = 0; i < ddlHashlamaReason.Items.Count; i++)
            {
                if (ddlHashlamaReason.Items[i].Value.Equals(_wcResult.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString()))
                    ddlHashlamaReason.SelectedValue = _wcResult.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString();

            }            
        }
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
            // btnCardStatus.Text = _wcResult.oOvedYomAvodaDetails.sStatusCardDesc;
            if (_wcResult.oOvedYomAvodaDetails != null && _wcResult.oOvedYomAvodaDetails.bOvedDetailsExists)
            {
                //_StatusCard = oBatchManager.CardStatus;
                if (bRashemet || bMenahelBankShaot)
                {
                    if (((CardStatus)(_wcResult.oOvedYomAvodaDetails.iStatus)) != (_wcResult.CardStatus))
                    {
                        dv = new DataView(_wcResult.dtLookUp);
                        dv.RowFilter = string.Concat("table_name='", "ctb_status_kartis", "' and kod =", _wcResult.CardStatus.GetHashCode());
                        lblCardStatus.InnerText = dv[0]["teur"].ToString();
                    }
                    else
                    {
                        lblCardStatus.InnerText = _wcResult.oOvedYomAvodaDetails.sStatusCardDesc;
                    }
                    switch (_wcResult.CardStatus)
                    {
                        case CardStatus.Error:
                            tdCardStatus.Attributes.Add("class", "CardStatusError");
                            tdCardStatus2.Attributes.Add("class", "CardStatusError");
                            //strImageUrl = "../../Images/btn-error.jpg";
                            break;
                        case CardStatus.Valid:
                            tdCardStatus.Attributes.Add("class", "CardStatusValid");
                            tdCardStatus2.Attributes.Add("class", "CardStatusValid");
                            //strImageUrl = "../../Images/btn-ok.jpg";
                            break;
                        case CardStatus.Calculate:
                            tdCardStatus.Attributes.Add("class", "CardStatusCalculate");
                            tdCardStatus2.Attributes.Add("class", "CardStatusCalculate");
                            //strImageUrl = "../../Images/btn-ok.jpg";
                            break;
                    }
                }
                SD.StatusCard = _wcResult.CardStatus;
            }
            else
            {
               tdCardStatus.Attributes.Add("class","");
               tdCardStatus2.Attributes.Add("class", "");
               lblCardStatus.InnerText = "";
            }

        //btnCardStatus.Attributes.Add("style", string.Concat("background-image: url(", strImageUrl, ")"));
    }
    private bool IsWorkCardWasUpdate()
    {
        bWorkCardWasUpdateRun = true;
        clOvdim _Ovdim = new clOvdim();
        //int iMisparIshiTrail;
        //int iMeadkenAcharon;
        //נבדוק אם הכרטיס עודכן ע"י גורם אנושי לא ע"י מערכת - קוד מעדכן אחרון גדול מאפס
        bool bWasUpdate = false;
        bWasUpdate=_Ovdim.IsCardWasUpdated(iMisparIshi, dDateCard);
        //DataTable dt = _Ovdim.GetLastUpdate(iMisparIshi, dDateCard);
        //foreach (DataRow dr in dt.Rows)
        //{
        //    iMeadkenAcharon=0;
        //    if (!String.IsNullOrEmpty(dr["MEADKEN_ACHARON"].ToString()))             
        //        iMeadkenAcharon =  int.Parse(dr["MEADKEN_ACHARON"].ToString());
                
        //    iMisparIshiTrail=0;
        //    if  (!String.IsNullOrEmpty(dr["MISPAR_ISHI_TRAIL"].ToString()))                
        //            iMisparIshiTrail = int.Parse(dr["MISPAR_ISHI_TRAIL"].ToString());                

        //    if (((iMeadkenAcharon >= 0) && (iMeadkenAcharon != iMisparIshi)) || ((iMisparIshiTrail >= 0) && (iMisparIshiTrail != iMisparIshi)))             
        //        bWasUpdate = true;
                        
        //}
        return bWasUpdate;
    }
    private void SetImageForButtonMeasherOMistayeg()
    {
        //clOvdim _Ovdim = new clOvdim();
        string strImageUrlApprove = "";
        string strImageUrlNotApprove = "";
        clGeneral.enMeasherOMistayeg oMasherOMistayeg;

        if (_wcResult.oOvedYomAvodaDetails != null && _wcResult.oOvedYomAvodaDetails.bOvedDetailsExists)
        {
            oMasherOMistayeg = (clGeneral.enMeasherOMistayeg)_wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
          //  EventLog.WriteEntry("Kds", "oMasherOMistayeg: " + oMasherOMistayeg, EventLogEntryType.Error);
            switch (oMasherOMistayeg)
            {
                case clGeneral.enMeasherOMistayeg.Measher:
                    strImageUrlApprove = "ImgButtonApprovalChecked";
                    strImageUrlNotApprove = "ImgButtonDisApprovalCheckedDisabled";//"ImgButtonDisApprovalRegular";
                    break;
                case clGeneral.enMeasherOMistayeg.Mistayeg:
                    strImageUrlApprove = "ImgButtonApprovalRegularDisabled";//"ImgButtonApprovalRegular";
                    strImageUrlNotApprove = "ImgButtonDisApproveChecked";
                    break;
                case clGeneral.enMeasherOMistayeg.ValueNull:
                    strImageUrlApprove = "ImgButtonApprovalRegularDisabled";//"ImgButtonApprovalRegular";
                    strImageUrlNotApprove = "ImgButtonDisApprovalCheckedDisabled";//"ImgButtonDisApprovalRegular";
                    break;
                default:
                    strImageUrlApprove = "ImgButtonApprovalRegular";
                    strImageUrlNotApprove = "ImgButtonDisApprovalRegular";
                    break;
            }
            if (!Page.IsPostBack) 
                Session["MeasherMistyeg"] = _wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
        }
        else
        {
          //  EventLog.WriteEntry("Kds", "oMasherOMistayeg ELSE " ,EventLogEntryType.Error);
            strImageUrlApprove = "ImgButtonApprovalRegularDisabled";
            strImageUrlNotApprove = "ImgButtonDisApprovalCheckedDisabled";
        }
       // }
        btnApprove.Attributes.Add("class", strImageUrlApprove);
        btnNotApprove.Attributes.Add("class", strImageUrlNotApprove);
    }

    private void SetImageForButtonHashlamaForDay()
    {
        HashlamaForDayValue.Value = "0";
        string strImageUrl = "../../Images/allscreens-checkbox-empty.jpg";
        if (!String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sHashlamaLeyom))
        {
            if (int.Parse(_wcResult.oOvedYomAvodaDetails.sHashlamaLeyom) > 0)
            {
                strImageUrl = "../../Images/allscreens-checkbox.jpg";
                HashlamaForDayValue.Value = "1";
            }
        }
        btnHashlamaForDay.Attributes.Add("style", string.Concat("background-image: url(", strImageUrl, ")"));
        //btnHashlamaForDay.Disabled = ((clGeneral.enEmployeeType)_wcResult.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora);

    }
    private void SetImageForButtonHamaratShabat()
    {
        Hamara.Value = "0";
        string strImageUrl = "../../Images/allscreens-checkbox-empty.jpg";
        if (!String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sHamara))
        {
            if (int.Parse(_wcResult.oOvedYomAvodaDetails.sHamara) > 0)
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
        // sHamara.Disabled = ((clGeneral.enEmployeeType)_wcResult.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora);
    }
    private void SetCardStatus()
    {
        //Show Card Status
        string sDayDesc;
        //if (_wcResult.oOvedYomAvodaDetails.iStatus == clGeneral.enStatusTipul.HistayemTipul.GetHashCode())
        //{
        //    lblTipul.Text = String.Concat("הסתיים טיפול בכ", (char)34, "ע!");
        //}

        //סוג יום 
        if (!String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sSidurDay))
        {
            sDayDesc = clGeneral.arrDays[int.Parse(_wcResult.oOvedYomAvodaDetails.sSidurDay) - 1];
            txtDay.Text = clGeneral.arrDays[int.Parse(_wcResult.oOvedYomAvodaDetails.sSidurDay) - 1];
            if (_wcResult.oOvedYomAvodaDetails.sDayTypeDesc != String.Empty)
            {
                sDayDesc = String.Concat(sDayDesc, "-", _wcResult.oOvedYomAvodaDetails.sDayTypeDesc);
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
        if (_wcResult.oOvedYomAvodaDetails != null && _wcResult.oOvedYomAvodaDetails.bOvedDetailsExists)
        {
        //DDL
        ddlTachograph.SelectedValue = _wcResult.oOvedYomAvodaDetails.sTachograf;//String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sTachograf) ? "-1" : _wcResult.oOvedYomAvodaDetails.sTachograf;
        ddlHalbasha.SelectedValue = _wcResult.oOvedYomAvodaDetails.sHalbasha;//String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sHalbasha) ? "-1" : _wcResult.oOvedYomAvodaDetails.sHalbasha;
        hidPrevHalbasha.Value = ddlHalbasha.SelectedValue;
        ddlLina.SelectedValue = _wcResult.oOvedYomAvodaDetails.sLina;//String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sLina) ? "-1" : _wcResult.oOvedYomAvodaDetails.sLina;
        ddlTravleTime.SelectedValue = _wcResult.oOvedYomAvodaDetails.sBitulZmanNesiot;//String.IsNullOrEmpty(_wcResult.oOvedYomAvodaDetails.sBitulZmanNesiot) ? "-1" : _wcResult.oOvedYomAvodaDetails.sBitulZmanNesiot;
        // ddlTravleTime.Enabled = ((clGeneral.enEmployeeType)_wcResult.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEgged);
        // ddlLina.Enabled = ((clGeneral.enEmployeeType)_wcResult.oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEgged);
        //השלמה ליום
        SetImageForButtonHashlamaForDay();
        //EnabledFields();
        //המרה
        SetImageForButtonHamaratShabat();
        //סיבות להשלמה
       // ddlHashlamaReason.SelectedValue = _wcResult.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString() == "0" ? "-1" : _wcResult.oOvedYomAvodaDetails.iSibatHashlamaLeyom.ToString();
        ddlHalbasha.Enabled = IsHalbasha();
       }
    }
    private bool IsHalbasha()
    {
        return _wcResult.oMeafyeneyOved.IsMeafyenExist(44);
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
            dt = oOvdim.GetLastUpdator(iMisparIshi, dCardDate); //oOvdim.GetLastUpdateData(iMisparIshi, dCardDate);
            if (dt.Rows.Count > 0)
            {
                lblLastUpdateDate.InnerText = DateTime.Parse(dt.Rows[0]["UpdateDate"].ToString()).ToShortDateString();// DateTime.Parse(dt.Rows[0]["last_date"].ToString()).ToShortDateString();
                lnkLastUpdateUser.InnerText = dt.Rows[0]["FullName"].ToString(); // dt.Rows[0]["MEADKEN_ACHARON"].ToString().IndexOf("-") >= 0 ? "מערכת" : dt.Rows[0]["full_name"].ToString();
                LastUpdateUserId.Value = dt.Rows[0]["IDNUM"].ToString(); // dt.Rows[0]["MEADKEN_ACHARON"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   

    protected void txtId_TextChanged(object sender, EventArgs e)
    {
      //  oBatchManager.IsExecuteErrors = false;
        ViewState["LoadNewCard"] = false;
    }

  
    protected void clnDate_TextChanged(object sender, EventArgs e)
    {
     //   oBatchManager.IsExecuteErrors = false;
        ViewState["LoadNewCard"] = false;
    }
    protected void btnRefreshOvedDetails_Click(object sender, EventArgs e)
    {
        bool bCalculateAndNotRashemet = false;
        bool bChishuvShachar = false;

        if (bInpuDataResult)
        {          
            if (hidSave.Value.Equals("1"))
            {
                RunBatchFunctions();
                hidSave.Value = "0";
            }
            SetImageForButtonMeasherOMistayeg();
        //    oBatchManager.IsExecuteErrors = false;
            ShowOvedCardDetails(iMisparIshi, dDateCard);
         
            ViewState["LoadNewCard"] = true;
            SD.RefreshBtn = 0;
            hidRefresh.Value = "0";
          
            SD.ClearControl();
            SD.BuildPage();

            if (DisabledCard())
                bCalculateAndNotRashemet = true;

            // string sScript;// = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",false);";
            bChishuvShachar = _wcResult.oOvedYomAvodaDetails.iBechishuvSachar.Equals(clGeneral.enBechishuvSachar.bsActive.GetHashCode());
            string sScript = SendScript(bChishuvShachar, bCalculateAndNotRashemet);
            ViewState["PrintWcFromEmda"] = null;
            //if ((bChishuvShachar) || (bCalculateAndNotRashemet))
            //{
            //    sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",true);";
            //    if (bChishuvShachar)
            //        sScript = sScript + ChisuvShacharMsg(); //"alert('זמנית לא ניתן להפיק כרטיס עבודה זה. אנא נסה במועד מאוחר יותר');"; 
            //}
            //else
            //    sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",false);";
            ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);           
        }
    }
    protected void btnResonOutIn_Click(object sender, EventArgs e)
    {
        string[] arrSidurDetails;
        int iSidurIndexThatChanged=0;
        arrSidurDetails = hidSdrInd.Value.Split(char.Parse(","));
        int iSidurIndex = int.Parse(arrSidurDetails[0]);
        int iKnisaYetiza = int.Parse(arrSidurDetails[1]);

        SD.SwitchShatGmatHatchala(iSidurIndex, iKnisaYetiza, ref iSidurIndexThatChanged);
            
    }
    protected void btnAddHeadrut_Click(object sender, EventArgs e)
    {
        hidSave.Value = "0";
        hidExecInputChg.Value = "0";
        RunBatchFunctions();
        ViewState["LoadNewCard"] = true;
        SD.RefreshBtn = 0;
        hidRefresh.Value = "0";
        hidUpdateBtn.Value = "true";
        SetImageForButtonMeasherOMistayeg();     
        SD.ClearControl();
        SD.BuildPage();
    }
    protected void btnAddSpecialSidur_Click(object sender, EventArgs e)
    {   //הוספת סידור מיוחד     
        if (Session["Parameters"] == null)
           SD.UnloadCard(btnRefreshOvedDetails);
        else
        {
            SD.AddNewSidur();
            string sScript = "$get('SD_lblSidur" + (SD.DataSource.Count - 1).ToString() + "').focus();";
            ScriptManager.RegisterStartupScript(btnAddMyuchad, this.GetType(), "AddSidur", sScript, true);
            bAddSidur = true;
        }
    }
    protected void btnFindSidur_Click(object sender, EventArgs e)
    {
        hidSave.Value = "0";    
        hidExecInputChg.Value="0";
        RunBatchFunctions();
        ViewState["LoadNewCard"] = true;
        SD.RefreshBtn = 0;
        hidRefresh.Value = "0";
        hidUpdateBtn.Value = "true";
        SetImageForButtonMeasherOMistayeg();     
        SD.ClearControl();
        SD.BuildPage();
       // DefineZmaniNesiotNavigatePage(dDateCard);
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
            RefreshScreen();
            PrintCard(sender, e);  
            //RunBatchFunctions();
            //PrintCard(sender, e);  
            //SD.DataSource = oBatchManager.htFullEmployeeDetails;        
            //SD.ErrorsList = oBatchManager.dtErrors;
            //SD.ClearControl();
            //SD.BuildPage();           
        }
        MPEPrintMsg.Hide();
    }
    public void PrintCard_2(object sender, EventArgs e)
    {
        
        string urlBarcode,key;
        try
        {
            int iOldMeasherMistayeg = int.Parse(Session["MeasherMistyeg"].ToString());
            clGeneral.enMeasherOMistayeg oMeasherMistayeg = (clGeneral.enMeasherOMistayeg)_wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
            bWorkCardWasUpdate = IsWorkCardWasUpdate();
            key = "|" + iMisparIshi.ToString() + "|" + dDateCard.ToString("yyyyMMdd") + "|";
            urlBarcode = ConfigurationManager.AppSettings["WsBarcode"].ToString() +"&text=" + key;

            //arrParams[2].ToString()=="0") התחברות מהבית
            //לכן רק אם לא התחברנו מהבית ונדפיס ישירות, אחרת נפתח PDF
            if (hidFromEmda.Value == "true") 
            {
              
                ViewState["PrintWcFromEmda"] = "true";
                string sScript = "";
                string sIp;
                string sPathFilePrint = ConfigurationManager.AppSettings["PathFileReportsTemp"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
                byte[] s;
                clReportOnLine oReportOnLine = new clReportOnLine("PrintWorkCard", eFormat.PDF);

                //EventLog.WriteEntry("Kds", "PathFileReportsTemp: " + sPathFilePrint, EventLogEntryType.Error);
                oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", iMisparIshi.ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_TAARICH", dDateCard.ToShortDateString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_EMDA", "0"));
                oReportOnLine.ReportParams.Add(new clReportParam("P_SIDUR_VISA", IsSidurVisaExists().GetHashCode().ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_URL_BARCODE", urlBarcode));
                    //במידה ויש ערך בטבלת מעדכן אחרון והכרטיס עלה ללא התייחסות ולחצו על מאשר או מסתייג נשלח תיקון 0, שלא היה תיקון
                    if ((iOldMeasherMistayeg == clGeneral.enMeasherOMistayeg.ValueNull.GetHashCode()) && (oMeasherMistayeg != clGeneral.enMeasherOMistayeg.ValueNull))
                    oReportOnLine.ReportParams.Add(new clReportParam("P_TIKUN", "0"));

                    else
                    oReportOnLine.ReportParams.Add(new clReportParam("P_TIKUN", "1"));
                
                oReportOnLine.ReportParams.Add(new clReportParam("P_DT", DateTime.Now.ToString()));

                s = oReportOnLine.CreateFile();
                string sFileName, sPathFile;
                FileStream fs;
                sIp = "";// arrParams[1];
                sFileName = "WorkCard.pdf";

                sPathFile = ConfigurationManager.AppSettings["PathFileReports"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
               // EventLog.WriteEntry("Kds", "sPathFile: " + sPathFile, EventLogEntryType.Error);
                if (!Directory.Exists(sPathFile))
                {
                    Directory.CreateDirectory(sPathFile);
                }
                
                fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Flush();
                fs.Close();
                //EventLog.WriteEntry("Kds", "oBatchManager.dtErrors: " + oBatchManager.dtErrors, EventLogEntryType.Error);
                if (_wcResult.dtErrors != null)
                {
                    for (int i = 0; i < _wcResult.dtErrors.Rows.Count; i++)
                    {
                        if (_wcResult.dtErrors.Rows[i]["check_num"].ToString().Trim() == "69")
                        {
                            sScript = "document.all('msgErrCar').style.display='block';";
                            break;
                        }
                    }
                }
               
              //  EventLog.WriteEntry("kds", Page.Title + ": path = " + sPathFilePrint + sFileName, EventLogEntryType.Error);
                //EventLog.WriteEntry("kds", "sIp" + sIp, EventLogEntryType.Error);
                sScript += "PrintDoc('" + sIp + "' ,'" + sPathFilePrint + sFileName + "'); document.all('prtMsg').style.display='block'; setTimeout(\"document.all('prtMsg').style.display = 'none'; document.all('btnCloseCard').click()\", 5000); ";
                //FreeWC();
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
                //if (bWorkCardWasUpdate)
                //{
                    if ((iOldMeasherMistayeg == clGeneral.enMeasherOMistayeg.ValueNull.GetHashCode()) && (oMeasherMistayeg != clGeneral.enMeasherOMistayeg.ValueNull))
                        ReportParameters.Add("P_TIKUN", "0");
                    else
                        ReportParameters.Add("P_TIKUN", "1");
                //}
                //else
                //    ReportParameters.Add("P_TIKUN", "0");

                
                OpenReport(ReportParameters, (Button)sender, ReportName.PrintWorkCard.ToString());
            }
        }
        catch (Exception ex)
        {
            EventLog.WriteEntry("Kds", "PrintCard Faild: " + ex.Message, EventLogEntryType.Error);
            throw ex;
        }
    }

    public void PrintCard(object sender, EventArgs e)
    {

        string urlBarcode, key;
        try
        {
            int iOldMeasherMistayeg = int.Parse(Session["MeasherMistyeg"].ToString());
            clGeneral.enMeasherOMistayeg oMeasherMistayeg = (clGeneral.enMeasherOMistayeg)_wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg;
            bWorkCardWasUpdate = IsWorkCardWasUpdate();
            key = "|" + iMisparIshi.ToString() + "|" + dDateCard.ToString("yyyyMMdd") + "|";
            urlBarcode = ConfigurationManager.AppSettings["WsBarcode"].ToString() + "&text=" + key;

            //arrParams[2].ToString()=="0") התחברות מהבית
            //לכן רק אם לא התחברנו מהבית ונדפיס ישירות, אחרת נפתח PDF
            if (hidFromEmda.Value == "true")
            {

                ViewState["PrintWcFromEmda"] = "true";
                string sScript = "";
                string sIp;
                string sPathFilePrint = ConfigurationManager.AppSettings["PathFileReportsTemp"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
                byte[] s;
                clReportOnLine oReportOnLine = new clReportOnLine("PrintWorkCard", eFormat.PDF);

                //EventLog.WriteEntry("Kds", "PathFileReportsTemp: " + sPathFilePrint, EventLogEntryType.Error);
                oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", iMisparIshi.ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_TAARICH", dDateCard.ToShortDateString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_EMDA", "0"));
                oReportOnLine.ReportParams.Add(new clReportParam("P_SIDUR_VISA", IsSidurVisaExists().GetHashCode().ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_URL_BARCODE", urlBarcode));
                //במידה ויש ערך בטבלת מעדכן אחרון והכרטיס עלה ללא התייחסות ולחצו על מאשר או מסתייג נשלח תיקון 0, שלא היה תיקון
                if ((iOldMeasherMistayeg == clGeneral.enMeasherOMistayeg.ValueNull.GetHashCode()) && (oMeasherMistayeg != clGeneral.enMeasherOMistayeg.ValueNull))
                    oReportOnLine.ReportParams.Add(new clReportParam("P_TIKUN", "0"));

                else
                    oReportOnLine.ReportParams.Add(new clReportParam("P_TIKUN", "1"));

                oReportOnLine.ReportParams.Add(new clReportParam("P_DT", DateTime.Now.ToString()));

                s = oReportOnLine.CreateFile();
                string sFileName, sPathFile;
                FileStream fs;
                sIp = "";// arrParams[1];
                sFileName = "WorkCard.pdf";

                sPathFile = ConfigurationManager.AppSettings["PathFileReports"] + LoginUser.UserInfo.EmployeeNumber + @"\\";
                // EventLog.WriteEntry("Kds", "sPathFile: " + sPathFile, EventLogEntryType.Error);
                if (!Directory.Exists(sPathFile))
                {
                    Directory.CreateDirectory(sPathFile);
                }

                fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Flush();
                fs.Close();
                //EventLog.WriteEntry("Kds", "oBatchManager.dtErrors: " + oBatchManager.dtErrors, EventLogEntryType.Error);
                if (_wcResult.dtErrors != null)
                {
                    for (int i = 0; i < _wcResult.dtErrors.Rows.Count; i++)
                    {
                        if (_wcResult.dtErrors.Rows[i]["check_num"].ToString().Trim() == "69")
                        {
                            sScript = "document.all('msgErrCar').style.display='block';";
                            break;
                        }
                    }
                }

                //  EventLog.WriteEntry("kds", Page.Title + ": path = " + sPathFilePrint + sFileName, EventLogEntryType.Error);
                //EventLog.WriteEntry("kds", "sIp" + sIp, EventLogEntryType.Error);
                sScript += "PrintDoc('" + sIp + "' ,'" + sPathFilePrint + sFileName + "'); document.all('prtMsg').style.display='block'; setTimeout(\"document.all('prtMsg').style.display = 'none'; document.all('btnCloseCard').click()\", 5000); ";
                //FreeWC();
                ScriptManager.RegisterStartupScript(btnPrint, btnPrint.GetType(), "PrintPdf", sScript, true);
            }
            else
            {

                clReportOnLine oReportOnLine = new clReportOnLine(ReportName.PrintWorkCard.ToString(), eFormat.PDF);
               

                oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", iMisparIshi.ToString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_TAARICH", dDateCard.ToShortDateString()));
                oReportOnLine.ReportParams.Add(new clReportParam("P_EMDA", "0"));
                oReportOnLine.ReportParams.Add(new clReportParam("P_SIDUR_VISA", IsSidurVisaExists().GetHashCode().ToString()));


                oReportOnLine.ReportParams.Add(new clReportParam("P_URL_BARCODE", urlBarcode));
                //if (bWorkCardWasUpdate)
                //{
                if ((iOldMeasherMistayeg == clGeneral.enMeasherOMistayeg.ValueNull.GetHashCode()) && (oMeasherMistayeg != clGeneral.enMeasherOMistayeg.ValueNull))
                    oReportOnLine.ReportParams.Add(new clReportParam("P_TIKUN", "0"));
                else
                    oReportOnLine.ReportParams.Add(new clReportParam("P_TIKUN", "1"));
                //}
                //else
                //    ReportParameters.Add("P_TIKUN", "0");


                OpenReportFile(oReportOnLine, btnPrint, ReportName.PrintWorkCard.ToString(), eFormat.PDF);
            }
        }
        catch (Exception ex)
        {
            EventLog.WriteEntry("Kds", "PrintCard Faild: " + ex.Message, EventLogEntryType.Error);
            throw ex;
        }
    }

    protected void btnPrint_click(object sender, EventArgs e)
    {
        //string sCloseAllBtn = "";
        bool bChishuvShachar = false;
        bool  bCalculateAndNotRashemet = false;
        SetImageForButtonMeasherOMistayeg();
        hidExecInputChg.Value = "";
        if (hidChanges.Value.ToLower() == "true")
            btnShowPrintMsg_Click(sender, e);
        else
            PrintCard(sender, e);

        //if (_wcResult.oOvedYomAvodaDetails.iBechishuvSachar.Equals(clGeneral.enBechishuvSachar.bsActive.GetHashCode()))
        //    sCloseAllBtn = "true";
        //else
        //    sCloseAllBtn = "false";

        if (DisabledCard())
            bCalculateAndNotRashemet = true;

        // string sScript;// = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",false);";
        bChishuvShachar = _wcResult.oOvedYomAvodaDetails.iBechishuvSachar.Equals(clGeneral.enBechishuvSachar.bsActive.GetHashCode());
        string sScript = SendScript(bChishuvShachar, bCalculateAndNotRashemet);
        //string sScript = "SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + "," + sCloseAllBtn + ");";
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
                            Response.Redirect("EmployeeCards.aspx?EmpID=" + iMisparIshi.ToString() + "&WCardDate=" + dDateCard.ToShortDateString(), false);
                            break;
                        case 2:
                            Response.Redirect("EmployeTotalMonthly.aspx?EmpID=" + iMisparIshi.ToString() + "&WCardDate=" + dDateCard.ToShortDateString(), false);
                            break;
                    }
                }
                else
                {
                    sScript = "window.close();";
                    //FreeWC();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "closeCard", sScript, true);
                }
            }
            else
            {//יכיל מספר סידור, תאריך ושעה של סידור
                string[] arrVal = hidSadotLSidur.Value.Split(char.Parse(","));
                if (arrVal.Length>1)
                    if (arrVal[0].Equals("1"))
                    {
                        _wcResult = new WorkCardResult();
                        FillWorkCardResult(_wcResult,iMisparIshi, dDateCard);
                       // oBatchManager.InitGeneralData();
                        SD.DataSource = _wcResult.htFullEmployeeDetails;
                        Session["Sidurim"] = SD.DataSource;
                        SD.ClearControl();
                        SD.BuildPage();
                        iSidurIndex= FindSidurIndex(DateTime.Parse(arrVal[1]),int.Parse(arrVal[2]),  SD.DataSource);
                        sScript = "bScreenChanged=true; ExecSadotLsidur(" + iSidurIndex + ",true);";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ExecSadotNosafim", sScript, true);
                       
                    }            
              }
        }
        //Response.Redirect("WorkCard.aspx?EmpID=" + iMisparIshi + "&WCardDate=" + dDateCard.ToShortDateString());
        //שינויי קלט
        //oBatchManager.MainInputData(iMisparIshi, dDateCard);
        //oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
        //SD.DataSource = oBatchManager.htEmployeeDetails;        
        //SD.ErrorsList = oBatchManager.dtErrors;
        //SD.ClearControl();
        //SD.BuildPage();  
    }
    protected int FindSidurIndex(DateTime dSidurDate,int iSidurNumber, OrderedDictionary htSidurim)
    {
        int iSidurIndex = 0;
        SidurDM _Sidur =null;
        try
        {
            for (int iIndex = 0; iIndex < htSidurim.Count; iIndex++)
            {
                _Sidur = (SidurDM)(htSidurim[iIndex]);
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
    protected void RefreshScreen()
    {
        hidRefresh.Value = "1";
        //hidChanges.Value = "";
        RunBatchFunctions();

        SetImageForButtonMeasherOMistayeg();
       // oBatchManager.IsExecuteErrors = false;
        ShowOvedCardDetails(iMisparIshi, dDateCard);
        SetImageForButtonValiditiy();
        DefineZmaniNesiotNavigatePage(dDateCard);
        //SetLookUpDDL();

        ViewState["LoadNewCard"] = true;
        SD.RefreshBtn = 1;
        hidRefresh.Value = "0";

        //_StatusCard = oBatchManager.CardStatus;
        SD.bAtLeatOneSidurIsNOTNahagutOrTnua = false;
        SD.IsAtLeastOneSidurNahagut = false;
        SD.StatusCard = _wcResult.CardStatus;
        SD.Mashar = (_wcResult.dtMashar == null) ? (DataTable)Session["Mashar"] : _wcResult.dtMashar;
        SD.DataSource = _wcResult.htFullEmployeeDetails;
        SD.ErrorsList = _wcResult.dtErrors;
        Session["Sidurim"] = _wcResult.htFullEmployeeDetails;
        Session["Errors"] = _wcResult.dtErrors;
        SD.ClearControl();
        SD.BuildPage();
        string sScript = "document.getElementById('divHourglass').style.display = 'none'; SetSidurimCollapseImg();HasSidurHashlama();EnabledSidurimListBtn(" + tbSidur.Disabled.ToString().ToLower() + ",false);";
        ScriptManager.RegisterStartupScript(btnRefreshOvedDetails, this.GetType(), "ColpImg", sScript, true);
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
            //          SD.ClearControl();
            //          SD.BuildPage();  

            RefreshScreen();
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
    //    for (int iIndex = 0; iIndex < this.SD.DataSource.Count; iIndex++)
    //    {
    //        _GridView = ((GridView)this.FindControl("SD").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
    //        if (_GridView != null)
    //        {
    //            for (int iRowIndex = 0; iRowIndex < _GridView.Rows.Count; iRowIndex++)
    //            {
    //                _GridRow = _GridView.Rows[iRowIndex];
    //                if ((_GridRow.Cells[SD.COL_ADD_NESIA_REKA].Controls.Count) > 0)
    //                {
    //                    _ImgReka = ((ImageButton)_GridRow.Cells[SD.COL_ADD_NESIA_REKA].Controls[0]);
    //                    //if (_GridRow.Cells[4].Controls.Count > 0)
    //                    //{
    //                       // _Knisot = ((HyperLink)_GridRow.Cells[4].Controls[0]);

    //                        // _ImgReka = (ImageButton)_GridRow.FindControl(_GridRow.Cells[SD.COL_ADD_NESIA_REKA].Controls[0].ClientID);
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
    //void SD_btnReka(string strRekaID,bool bDummy)
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
   

    void SD_btnHandler(string strValue, bool bOpenUpdateBtn)
    {
        bInpuDataResult = true;
        if ((bOpenUpdateBtn) || (hidUpdateBtn.Value=="false"))
        {            
            string sScript = "SetBtnChanges();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenUpdBtn", sScript, true);
           // hidUpdateBtn.Value = "true";
            DefineZmaniNesiotNavigatePage(dDateCard);
          //  SD.HasSaveCard = false;
        }
        //oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
        //SD.DataSource = oBatchManager.htEmployeeDetails;
        //SD.ErrorsList = oBatchManager.dtErrors;
        //if (btnUpdateCard.Enabled)
        //{
        //    SaveCard();
        //    oBatchManager.MainOvedErrors(iMisparIshi, dDateCard);
        //    SD.DataSource = oBatchManager.htEmployeeDetails;
        //    SD.ErrorsList = oBatchManager.dtErrors;
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

            if (this.SD.DataSource != null)
            {
                //נכניס את השינויים של סידורים ופעילויות
                if (!FillSidurimChanges(ref oCollSidurimOvdimUpd, ref oCollPeilutOvdimUpd, 
                                        ref oCollSidurimOvdimIns, ref oCollSidurimOvdimDel,
                                        ref oCollPeluyotOvdimDel, ref oCollPeluyotOvdimIns))
                {
                    //נמצאו פעילויות זהות
                    SetUpdateBtnVisibility("true");
                    string sScript = "alert('קיימת פעילות בשעת היציאה שדיווחת, יש לתקן את השעה' )";
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
                        SetUpdateBtnVisibility("true");
                        hidChanges.Value = "false";                        
                        //hidUpdateBtn.Value = "true";
                       // SD.HasSaveCard = false;
                    }
                    else
                    {
                        SetUpdateBtnVisibility("true");
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
    private bool FillObjIdkunRashemet(SidurDM oSidur,  string sFieldName, int iMisparSidur,
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
                if (int.Parse((ConfigurationSettings.AppSettings["WriteToLog"]))==1)
                    EventLog.WriteEntry("kds", "Rashemet shat_gmar_old: "+ oSidur.dOldFullShatGmarLetashlum + " shat_gmar_new: " + oSidur.dFullShatGmarLetashlum + " mispar-ishi: " + oSidur.iMisparIshi );
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
                if (int.Parse((ConfigurationSettings.AppSettings["WriteToLog"])) == 1)
                    EventLog.WriteEntry("kds", "Rashemet LoLetashlum_old: " + oSidur.iOldLoLetashlum + " LoLetashlum_new: " + oSidur.iLoLetashlum + " mispar-ishi: " + oSidur.iMisparIshi);
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
    private bool FillObjIdkunRashemet(SidurDM oSidur, int iPeilutIndex, string sFieldName , int iMisparSidur,
                                      DateTime dShatHatchala, DateTime dShatYetiza, 
                                      int iMisparKnisa, ref OBJ_IDKUN_RASHEMET _ObjIdkunRashemet)
    {
        int iPakadId= clUtils.GetPakadId(dtPakadim, sFieldName);
        PeilutDM _Peilut;
        _Peilut = (PeilutDM)oSidur.htPeilut[iPeilutIndex];

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
            case "LICENSE_NUMBER":
                if (!_Peilut.lOldLicenseNumber.Equals(_Peilut.lLicenseNumber))
                {
                    _ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                    _ObjIdkunRashemet.PAKAD_ID = iPakadId;
                    bChanged = true;
                }
                break;
            case "Makat_nesia":
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
            case "Shat_yetzia":
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
            _ObjIdkunRashemet.SHAT_YETZIA = _Peilut.oPeilutStatus == PeilutDM.enPeilutStatus.enNew ? _Peilut.dFullShatYetzia : _Peilut.dOldFullShatYetzia;
            _ObjIdkunRashemet.NEW_SHAT_YETZIA = _Peilut.oPeilutStatus == PeilutDM.enPeilutStatus.enNew ? _Peilut.dFullShatYetzia : dShatYetiza;
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
                if (int.Parse((ConfigurationSettings.AppSettings["WriteToLog"])) == 1)
                    EventLog.WriteEntry("kds", "Rashemet DDL_old: " + _DDL.Attributes["OldV"].ToString() + " DDL_new: " + _DDL.SelectedValue + " mispar-ishi: " + iMisparIshi);
                
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
        TextBox _TxtSidur = new TextBox();
        DropDownList _DDL;
        //HtmlInputCheckBox _Chk;
        Label _Lbl = new Label();
        HyperLink _HypLnk = new HyperLink();
        int iMisarSidur;
        DateTime dShatHatchala, dNewShatHatchala;
        OBJ_IDKUN_RASHEMET _objIdkunRashemet = new OBJ_IDKUN_RASHEMET();
        SidurDM oSidur;
        for (int iIndex = 0; iIndex < this.SD.DataSource.Count; iIndex++)
        {
            try
            {
                _Lbl = (Label)this.FindControl("SD").FindControl("lblSidur" + iIndex);
            }           
            catch (Exception ex)
            {
                _Lbl = null;
                try
                {
                    _HypLnk = (HyperLink)this.FindControl("SD").FindControl("lblSidur" + iIndex);
                }
                catch (Exception ex1)
                {
                   _TxtSidur = ((TextBox)(this.FindControl("SD").FindControl("lblSidur" + iIndex)));
                   _HypLnk = null;
                }
            }

            if ((_Lbl != null) || (_HypLnk != null) || (_TxtSidur!=null))
            {
                _objIdkunRashemet = new OBJ_IDKUN_RASHEMET();
                iMisarSidur = (_Lbl == null) ? (_HypLnk == null ? (_TxtSidur.Text=="" ? 0 : int.Parse(_TxtSidur.Text)) : int.Parse(_HypLnk.Text)) : int.Parse(_Lbl.Text);

                oSidur = (SidurDM)(((OrderedDictionary)Session["Sidurim"])[iIndex]);

                //שעת התחלה             
                _Txt = ((TextBox)(this.FindControl("SD").FindControl("txtSH" + iIndex)));

                if (oSidur.oSidurStatus == SidurDM.enSidurStatus.enNew)
                {
                    if (_Txt.Text == string.Empty)
                    {
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.Parse("01/01/0001 00:00:00");
                        _objIdkunRashemet.SHAT_HATCHALA = _objIdkunRashemet.NEW_SHAT_HATCHALA;
                        oSidur.dFullShatHatchalaLetashlum = _objIdkunRashemet.NEW_SHAT_HATCHALA;
                        oSidur.dFullShatGmarLetashlum = _objIdkunRashemet.NEW_SHAT_HATCHALA;
                    }
                    else
                    {
                        _objIdkunRashemet.SHAT_HATCHALA = oSidur.dFullShatHatchala;
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = oSidur.dFullShatHatchala;
                        //oSidur.dFullShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                        //oSidur.dFullShatGmarLetashlum = oSidur.dFullShatHatchala;
                    }
                }
                else
                    _objIdkunRashemet.SHAT_HATCHALA = DateTime.Parse(_Txt.Attributes["OrgShatHatchala"]);

                if (_Txt.Text == string.Empty)
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.Parse("01/01/0001 00:00:00");
                else
                {//נבדוק אם השתנה התאריך
                    if (oSidur.oSidurStatus != SidurDM.enSidurStatus.enNew)
                    {
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = GetSidurNewDate(iMisarSidur, _Txt.Text); //DateTime.Parse(dDateCard.ToShortDateString() + " " + string.Concat(oTxt.Text, ":", oObjSidurimOvdimUpd.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                        if (_objIdkunRashemet.NEW_SHAT_HATCHALA.Second != _objIdkunRashemet.SHAT_HATCHALA.Second)
                            _objIdkunRashemet.NEW_SHAT_HATCHALA = _objIdkunRashemet.NEW_SHAT_HATCHALA.AddSeconds(double.Parse(_objIdkunRashemet.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                    }
                }
                dNewShatHatchala = _objIdkunRashemet.NEW_SHAT_HATCHALA;
                dShatHatchala = _objIdkunRashemet.SHAT_HATCHALA;
               
                
                
                if (FillObjIdkunRashemet(oSidur, "SHAT_HATCHALA", iMisarSidur, dShatHatchala, ref  _objIdkunRashemet))   
                //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_HATCHALA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                {
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                    //_objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                    _objIdkunRashemet.UPDATE_OBJECT = 1;
                    oCollIdkunRashemet.Add(_objIdkunRashemet);
                }

                //שעת גמר

                //_Txt = ((TextBox)(this.FindControl("SD").FindControl("txtSG" + iIndex)));
                //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_GMAR"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                if (FillObjIdkunRashemet(oSidur, "SHAT_GMAR", iMisarSidur, dShatHatchala, ref  _objIdkunRashemet))                                       
                {
                    _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                    //_objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                    _objIdkunRashemet.UPDATE_OBJECT = 1;
                    oCollIdkunRashemet.Add(_objIdkunRashemet);
                }
                //התייצבות
                if (SD.FirstParticipate != null)
                {
                    if ((SD.FirstParticipate.iMisparSidur == iMisarSidur)
                        && (SD.FirstParticipate.dFullShatHatchala == dShatHatchala))
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

                if (SD.SecondParticipate != null)
                {
                    if ((SD.SecondParticipate.iMisparSidur == iMisarSidur)
                        && (SD.SecondParticipate.dFullShatHatchala == dShatHatchala))
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
                _DDL = (DropDownList)this.FindControl("SD").FindControl("ddlResonIn" + iIndex);
                if (_DDL != null)
                {
                    //שעת התחלה לתשלום
                    //_Txt = ((TextBox)(this.FindControl("SD").FindControl("txtSHL" + iIndex)));
                    //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "SHAT_HATCHALA_LETASHLUM"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur,  "SHAT_HATCHALA_LETASHLUM", iMisarSidur, dShatHatchala,ref _objIdkunRashemet))
                    {
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                       // _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }

                    //שעת גמר לתשלום
                    //_Txt = ((TextBox)(this.FindControl("SD").FindControl("txtSGL" + iIndex)));
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
                    //_DDL = (DropDownList)this.FindControl("SD").FindControl("ddlResonOut" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "KOD_SIBA_LEDIVUCH_YADANI_OUT"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "KOD_SIBA_LEDIVUCH_YADANI_OUT", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("SD").FindControl("ddlException" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "CHARIGA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "CHARIGA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                     
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("SD").FindControl("ddlPHfsaka" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "PITZUL_HAFSAKA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "PITZUL_HAFSAKA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_DDL = (DropDownList)this.FindControl("SD").FindControl("ddlHashlama" + iIndex);
                    //if (FillObjIdkunRashemet(_DDL, clUtils.GetPakadId(dtPakadim, "HASHLAMA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "HASHLAMA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_Chk = (HtmlInputCheckBox)this.FindControl("SD").FindControl("chkOutMichsa" + iIndex);
                    //if (FillObjIdkunRashemet(_Chk, clUtils.GetPakadId(dtPakadim, "OUT_MICHSA"), iMisarSidur, dShatHatchala, DateTime.MinValue, 0, ref _objIdkunRashemet))
                    if (FillObjIdkunRashemet(oSidur, "OUT_MICHSA", iMisarSidur, dShatHatchala, ref _objIdkunRashemet))
                    {                    
                        _objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                      //  _objIdkunRashemet.SHAT_HATCHALA = dShatHatchala; //orgin
                        _objIdkunRashemet.UPDATE_OBJECT = 1;
                        oCollIdkunRashemet.Add(_objIdkunRashemet);
                    }
                    //_Chk = (HtmlInputCheckBox)this.FindControl("SD").FindControl("chkLoLetashlum" + iIndex);
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
                oGridView = ((GridView)this.FindControl("SD").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
                if (oGridView != null)
                {
                    FillIdkunRashemetPeiluyot(oSidur,iMisarSidur, dShatHatchala, dNewShatHatchala, ref oGridView, ref oCollIdkunRashemet);
                }
            }
        }
    }

    private void FillIdkunRashemetPeiluyot(SidurDM oSidur, int iMisparSidur, DateTime dSidurShatHatchala, DateTime dNewShatHatchala, ref GridView oGridView, ref COLL_IDKUN_RASHEMET oCollIdkunRashemet)
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
            _TxtShatYetiza = ((TextBox)oGridRow.Cells[SD.COL_SHAT_YETIZA].Controls[0]);
            _Txt=((TextBox)oGridRow.Cells[SD.COL_DAY_TO_ADD].Controls[0]);
            iDayToAdd = int.Parse(_Txt.Text);
            dNewShatYetiza = DateTime.Parse(String.Concat(_TxtShatYetiza.Attributes["OrgDate"], " ", _TxtShatYetiza.Text));
            //אם שעת היציאה היא של כרטיס העבודה ובמקור זה היה היום הבא, נוריד יום

            dShatYetiza = DateTime.Parse(_TxtShatYetiza.Attributes["OrgDate"]);
            if (dShatYetiza.Year > DateHelper.cYearNull)
            {
                if ((dShatYetiza.AddDays(-1) == dDateCard)
                    && (iDayToAdd == 0))
                    dNewShatYetiza = dNewShatYetiza.AddDays(-1);
            }
            //אם שעת היציאה היא של כרטיס העבודה נבדוק אם צריך להוסיף יום
            if (DateTime.Parse(String.Concat(_TxtShatYetiza.Attributes["OrgDate"])) == dDateCard)
                dNewShatYetiza=dNewShatYetiza.AddDays(iDayToAdd);
           

            dShatYetiza = DateTime.Parse(_TxtShatYetiza.Attributes["OrgShatYetiza"]);
            
            arrKnisaVal = oGridRow.Cells[SD.COL_KNISA].Text.Split(",".ToCharArray());
            iMisparKnisa = int.Parse(arrKnisaVal[0]);

            //_Txt = ((TextBox)oGridRow.Cells[SD.COL_KISUY_TOR].Controls[0]);
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
            if (FillObjIdkunRashemet(oSidur, iRowIndex, "Shat_yetzia", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }

            //_Txt = ((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]);
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

            if (FillObjIdkunRashemet(oSidur, iRowIndex, "LICENSE_NUMBER", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }
            //_Txt = ((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]);
            //if (FillObjIdkunRashemet(_Txt, clUtils.GetPakadId(dtPakadim, "MAKAT_NO"), iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))
            if (FillObjIdkunRashemet(oSidur, iRowIndex, "Makat_nesia", iMisparSidur, dNewShatHatchala, dNewShatYetiza, iMisparKnisa, ref _objIdkunRashemet))  
            {
                //_objIdkunRashemet.NEW_SHAT_HATCHALA = dNewShatHatchala;
                //_objIdkunRashemet.SHAT_HATCHALA = dSidurShatHatchala;
                //_objIdkunRashemet.SHAT_YETZIA = dShatYetiza;
                //_objIdkunRashemet.NEW_SHAT_YETZIA = dNewShatYetiza;
                _objIdkunRashemet.UPDATE_OBJECT = 1;
                oCollIdkunRashemet.Add(_objIdkunRashemet);
            }
            //_Txt = ((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]);
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
                               ref SidurDM oSidur,
                               ref PeilutDM oPeilut,
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

        oShatYetiza = ((TextBox)oGridRow.Cells[SD.COL_SHAT_YETIZA].Controls[0]);
        sDayToAdd = ((TextBox)oGridRow.Cells[SD.COL_DAY_TO_ADD].Controls[0]).Text;
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
                if (dShatYetiza.Date.Year < DateHelper.cYearNull)
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
                    if (dShatYetiza.Date.Year > DateHelper.cYearNull)
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

        sKisuyTor = ((TextBox)oGridRow.Cells[SD.COL_KISUY_TOR].Controls[0]).Text;
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
        oObjPeluyotOvdim.OTO_NO = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]).Text);
        oObjPeluyotOvdim.LICENSE_NUMBER = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_CAR_LICESNCE_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_CAR_LICESNCE_NUMBER].Controls[0]).Text);
        oObjPeluyotOvdim.MAKAT_NESIA = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]).Text);
        oObjPeluyotOvdim.DAKOT_BAFOAL = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]).Text);
        arrKnisaVal = oGridRow.Cells[SD.COL_KNISA].Text.Split(",".ToCharArray());
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
            iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[SD.COL_CANCEL_PEILUT].Controls[0]).Text);
            oObjPeluyotOvdim.BITUL_O_HOSAFA = ((iCancelSidur == 1) || (iCancelPeilut == 1)) ? 1 : ((iCancelSidur == 2) || (iCancelPeilut == 2) ? 2 : oPeilut.iBitulOHosafa);
            if (((iCancelPeilut == 1) || (iCancelPeilut == 2)) && (oObjPeluyotOvdim.BITUL_O_HOSAFA == clGeneral.enBitulOHosafa.AddAutomat.GetHashCode()))            
                oObjPeluyotOvdim.UPDATE_OBJECT = 1;
        }
        else
        {
            oObjPeluyotOvdim.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.AddByUser.GetHashCode();
            oObjPeluyotOvdim.UPDATE_OBJECT = 1;
            oObjPeluyotOvdim.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
        }
        //נעדכן את ה-HashTable בערכים המקוריים
        oPeilut.dFullShatYetzia = oObjPeluyotOvdim.NEW_SHAT_YETZIA;
        oPeilut.lOtoNo = oObjPeluyotOvdim.OTO_NO;
        oPeilut.lLicenseNumber = oObjPeluyotOvdim.LICENSE_NUMBER;
        oPeilut.lMakatNesia = oObjPeluyotOvdim.MAKAT_NESIA;
        oPeilut.iKisuyTor = oObjPeluyotOvdim.KISUY_TOR;
        oPeilut.iDakotBafoal = oObjPeluyotOvdim.DAKOT_BAFOAL;
    }
    //private void UpdateHashTableWithGridChanges(ref OrderedDictionary htEmployeeDetails)
    //{              
    //    GridView oGridView;
    //    SidurDM oSidur;
    //    Label oLbl;
    //    HyperLink oHypLnk=new HyperLink();
    //    TextBox oTxt, oShatGmar, oDayToAdd;
    //    DropDownList oDDL;
    //    string sTmp;
    //    DateTime dSidurDate;
    //    int iCancelSidur;
    //    HtmlInputCheckBox oChk;
    //    for (int iIndex = 0; iIndex < this.SD.DataSource.Count; iIndex++)
    //    {            
    //        try
    //        {
    //            oLbl = (Label)this.FindControl("SD").FindControl("lblSidur" + iIndex);
    //        }
    //        catch (Exception ex)
    //        {
    //            oHypLnk = (HyperLink)this.FindControl("SD").FindControl("lblSidur" + iIndex);
    //            oLbl = null;
    //        }
    //        if ((oLbl != null) || (oHypLnk != null))
    //        {
    //            oSidur = (SidurDM)((htEmployeeDetails)[iIndex]);

    //            oSidur.iMisparSidur = (oLbl == null ? int.Parse(oHypLnk.Text) : int.Parse(oLbl.Text));
    //            oTxt = ((TextBox)(this.FindControl("SD").FindControl("txtSH" + iIndex)));
    //            if (oTxt.Text == string.Empty)
    //            {
    //                oSidur.dFullShatHatchala = DateTime.Parse("01/01/0001 00:00:00");
    //                oSidur.sShatHatchala = "";
    //            }
    //            else
    //            {
    //               oLbl =  (Label)this.FindControl("SD").FindControl("lblDate" + iIndex);
    //               oSidur.dFullShatHatchala = DateTime.Parse(oLbl.Text + " " + oTxt.Text);
    //               oSidur.sShatHatchala = oTxt.Text;
    //            }

    //            oShatGmar = ((TextBox)(this.FindControl("SD").FindControl("txtSG" + iIndex)));
    //            //מספר ימים להוספה 0 אם יום נוכחי1 - יום הבא
    //            oDayToAdd = ((TextBox)(this.FindControl("SD").FindControl("txtDayAdd" + iIndex)));
    //            sTmp = oShatGmar.Text;
    //            dSidurDate = DateTime.Parse(oShatGmar.Attributes["OrgDate"].ToString() + " " + sTmp);
    //            oSidur.sShatGmar = sTmp;
    //            if (sTmp != string.Empty)
    //                oSidur.dFullShatGmar = DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));
    //            else
    //                oSidur.dFullShatGmar = DateTime.Parse("01/01/0001 00:00:00");


    //            oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlResonIn" + iIndex);
    //            if (oDDL != null)//רק אם שונה מסידור רציפות
    //            {
    //                oSidur.iKodSibaLedivuchYadaniIn = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

    //                oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlResonOut" + iIndex);
    //                oSidur.iKodSibaLedivuchYadaniOut = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

    //                sTmp = ((TextBox)(this.FindControl("SD").FindControl("txtSHL" + iIndex))).Text;
    //                oSidur.dFullShatHatchalaLetashlum = DateHelper.GetDateTimeFromStringHour(sTmp, dDateCard);
    //                oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToShortTimeString();

    //                sTmp = ((TextBox)(this.FindControl("SD").FindControl("txtSGL" + iIndex))).Text;
    //                oSidur.dFullShatGmarLetashlum = DateHelper.GetDateTimeFromStringHour(sTmp, dDateCard);
    //                oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToShortTimeString();

    //                oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlException" + iIndex);
    //                oSidur.sChariga = oDDL.SelectedValue;

    //                oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlPHfsaka" + iIndex);
    //                oSidur.sPitzulHafsaka = oDDL.Attributes["OldV"].ToString();

    //                oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlHashlama" + iIndex);
    //                oSidur.sHashlama = oDDL.SelectedValue;
    //                if (int.Parse(oDDL.SelectedValue) == clGeneral.enSugHashlama.enNoHashlama.GetHashCode())
    //                    oSidur.iSugHashlama = clGeneral.enSugHashlama.enNoHashlama.GetHashCode();
    //                else
    //                    oSidur.iSugHashlama = clGeneral.enSugHashlama.enHashlama.GetHashCode();

    //                oChk = (HtmlInputCheckBox)this.FindControl("SD").FindControl("chkOutMichsa" + iIndex);
    //                oSidur.sOutMichsa = oChk.Checked ? "1" : "0";

    //                iCancelSidur = int.Parse(((TextBox)this.FindControl("SD").FindControl("lblSidurCanceled" + iIndex)).Text);

    //                oSidur.iBitulOHosafa = iCancelSidur;

    //                //לא לתשלום - במידה וסידור בוטל נעדכן בערך המקורי - ישאר ללא שינוי
    //                oChk = (HtmlInputCheckBox)this.FindControl("SD").FindControl("chkLoLetashlum" + iIndex);

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
    //                if (SD.FirstParticipate != null)
    //                {
    //                    if ((SD.FirstParticipate.iMisparSidur == oSidur.iMisparSidur)
    //                        && (SD.FirstParticipate.dFullShatHatchala == oSidur.dFullShatHatchala))
    //                    {
    //                        if (!String.IsNullOrEmpty(ddlFirstPart.SelectedValue))                            
    //                            oSidur.iKodSibaLedivuchYadaniIn = ddlFirstPart.SelectedValue.Equals("-1") ? 0 : int.Parse(ddlFirstPart.SelectedValue); 
                            
    //                        if (!String.IsNullOrEmpty(txtFirstPart.Text) && (txtFirstPart.Text.IndexOf(":") > 0))                            
    //                            oSidur.dShatHitiatzvut = DateTime.Parse(dDateCard.ToShortDateString() + " " + txtFirstPart.Text);                            
    //                    }
    //                }
    //                if (SD.SecondParticipate != null)
    //                {
    //                    if ((SD.SecondParticipate.iMisparSidur ==  oSidur.iMisparSidur)
    //                        && (SD.SecondParticipate.dFullShatHatchala == oSidur.dFullShatHatchala))
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
    //                oGridView = ((GridView)this.FindControl("SD").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
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
    //    SidurDM _Sidur;
    //    try
    //    {
    //        _Sidur = (SidurDM)(((OrderedDictionary)Session["Sidurim"])[iSidurIndex]);
    //        //פעילויות
    //        for (int iRowIndex = 0; iRowIndex < oGridView.Rows.Count; iRowIndex++)
    //        {
    //            _Peilut = (clPeilut)_Sidur.htPeilut[iRowIndex];
    //            oGridRow = oGridView.Rows[iRowIndex];
    //            oShatYetiza = ((TextBox)oGridRow.Cells[SD.COL_SHAT_YETIZA].Controls[0]);
    //            sDayToAdd = ((TextBox)oGridRow.Cells[SD.COL_DAY_TO_ADD].Controls[0]).Text;
    //            iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);
                
    //            _Peilut.dCardDate = dDateCard;
    //            _Peilut.iPeilutMisparSidur = _Sidur.iMisparSidur;                

    //            dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
    //            sShatYetiza = oShatYetiza.Text;
    //            if (dShatYetiza.Date.Year < DateHelper.cYearNull)
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
    //                if (dShatYetiza.Date.Year > DateHelper.cYearNull)
    //                {
    //                    if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
    //                        dShatYetiza = dShatYetiza.AddDays(-1);
    //                }
    //            sKisuyTor = ((TextBox)oGridRow.Cells[SD.COL_KISUY_TOR].Controls[0]).Text;
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
    //            _Peilut.lMisparSiduriOto = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]).Text);
    //            _Peilut.lMakatNesia  = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]).Text);
    //            _Peilut.iDakotBafoal = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]).Text);
    //            arrKnisaVal = oGridRow.Cells[SD.COL_KNISA].Text.Split(",".ToCharArray());
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
                                     ref GridView oGridView, ref SidurDM oSidur)
    {
        bool bValid = true;
        GridViewRow oGridRow;
        OBJ_PEILUT_OVDIM oObjPeiluyotOvdim;      
        PeilutDM _Peilut;
        int iCancelPeilut;

        OBJ_PEILUT_OVDIM oObjPeilyutOvdimDel;
        try
        {
            //פעילויות
            for (int iRowIndex = 0; iRowIndex < oGridView.Rows.Count; iRowIndex++)
            {
                _Peilut = (PeilutDM)oSidur.htPeilut[iRowIndex];
                oGridRow = oGridView.Rows[iRowIndex];
                iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[SD.COL_CANCEL_PEILUT].Controls[0]).Text);
                
                _Peilut.iBitulOHosafa = iCancelPeilut;
                //If new active and sidur or peilut was canceld, we will not add the active.
                if ((_Peilut.oPeilutStatus.Equals(PeilutDM.enPeilutStatus.enNew)) && (iCancelPeilut == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode() || (iCancelSidur == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())))
                    continue;
                else
                {
                   
                    if (_Peilut.oPeilutStatus.Equals(PeilutDM.enPeilutStatus.enNew))
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
                //oShatYetiza = ((TextBox)oGridRow.Cells[SD.COL_SHAT_YETIZA].Controls[0]);
                //sDayToAdd = ((TextBox)oGridRow.Cells[SD.COL_DAY_TO_ADD].Controls[0]).Text;
                //iDayToAdd = String.IsNullOrEmpty(sDayToAdd) ? 0 : int.Parse(sDayToAdd);
                //oObjPeiluyotOvdimUpd = new OBJ_PEILUT_OVDIM();
                //oObjPeiluyotOvdimUpd.MISPAR_ISHI = iMisparIshi;
                //oObjPeiluyotOvdimUpd.TAARICH = dDateCard;
                //oObjPeiluyotOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;//int.Parse(((Label)(this.FindControl("SD").FindControl("lblSidur" + iSidurIndex))).Text);
                ////sTmp = ((TextBox)(this.FindControl("SD").FindControl("txtSH" + iSidurIndex))).Text;
                //oObjPeiluyotOvdimUpd.SHAT_HATCHALA_SIDUR = dOldSidurShatHatchala;
                //oObjPeiluyotOvdimUpd.NEW_SHAT_HATCHALA_SIDUR = dNewSidurShatHatchala;

                //dShatYetiza = DateTime.Parse(oShatYetiza.Attributes["OrgDate"]);
                //sShatYetiza = oShatYetiza.Text;
                //if (dShatYetiza.Date.Year < DateHelper.cYearNull)
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
                //    if (dShatYetiza.Date.Year > DateHelper.cYearNull)
                //    {
                //        if (iDayToAdd == 0) //נוריד יום- iDayToAdd=0 אם תאריך היציאה שונה מתאריך הכרטיס, כלומר הוא של היום הבא ורוצים לשנות ליום נוכחי
                //            dShatYetiza = dShatYetiza.AddDays(-1);
                //    }
                //sKisuyTor = ((TextBox)oGridRow.Cells[SD.COL_KISUY_TOR].Controls[0]).Text;
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
                //oObjPeiluyotOvdimUpd.OTO_NO = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]).Text);
                //oObjPeiluyotOvdimUpd.MAKAT_NESIA = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]).Text) ? 0 : long.Parse(((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]).Text);
                //oObjPeiluyotOvdimUpd.DAKOT_BAFOAL = String.IsNullOrEmpty(((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]).Text) ? 0 : int.Parse(((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]).Text);
                //arrKnisaVal = oGridRow.Cells[SD.COL_KNISA].Text.Split(",".ToCharArray());
                //iMisparKnisa = int.Parse(arrKnisaVal[0]);
                //oObjPeiluyotOvdimUpd.MISPAR_KNISA = iMisparKnisa;//String.IsNullOrEmpty(oGridRow.Cells[SD.COL_KNISA].Text) ? 0 : int.Parse(oGridRow.Cells[SD.COL_KNISA].Text);
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
                //iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[SD.COL_CANCEL_PEILUT].Controls[0]).Text);
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
    //private bool SidurHasChanged(SidurDM oSidur, GridViewRow oGridRow)
    //{
    //}
    private bool PeilutHasChanged(PeilutDM oPeilut, GridViewRow oGridRow)
    {
       // bool bChanged = false;
        TextBox _txt;
        DateTime _PeilutDate = new DateTime();

        //שעת יציאה
        _txt = ((TextBox)oGridRow.Cells[SD.COL_SHAT_YETIZA].Controls[0]);
        if (((TextBox)oGridRow.Cells[SD.COL_DAY_TO_ADD].Controls[0]).Text == "1")
            _PeilutDate = DateTime.Parse(dDateCard.ToShortDateString() + " " + _txt.Text).AddDays(1);
        else
            _PeilutDate = DateTime.Parse(dDateCard.ToShortDateString() + " " + _txt.Text);

        if (_PeilutDate != oPeilut.dOldFullShatYetzia)
            return true;
        
      
        ////כיסוי תור
        _txt = ((TextBox)oGridRow.Cells[SD.COL_KISUY_TOR].Controls[0]);
        if (_txt.Text == string.Empty){
             if (oPeilut.iOldKisuyTor!=0)
               return true;
        }
        else{
            if ((((_PeilutDate-DateTime.Parse(_PeilutDate.ToShortDateString() + " " +_txt.Text)) )).TotalMinutes != oPeilut.iOldKisuyTor)
               return true;
        }
        //מק"ט
        _txt = ((TextBox)oGridRow.Cells[SD.COL_MAKAT].Controls[0]);
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
        _txt = ((TextBox)oGridRow.Cells[SD.COL_ACTUAL_MINUTES].Controls[0]);
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
        _txt = ((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]);
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

        //מספר רישוי
        _txt = ((TextBox)oGridRow.Cells[SD.COL_CAR_LICESNCE_NUMBER].Controls[0]);
        if (_txt.Text == string.Empty)
        {
            if (oPeilut.lOldLicenseNumber != 0)
                return true;
        }
        else
        {
            if (long.Parse(_txt.Text) != oPeilut.lOldLicenseNumber)
                return true;
        }
        //_txt = ((TextBox)oGridRow.Cells[SD.COL_CAR_NUMBER].Controls[0]);
        //if (_txt.Text == string.Empty)
        //{
        //    if (oPeilut.lOldLicenseNumber != 0)
        //        return true;
        //}
        //else
        //{
        //    if (long.Parse(_txt.Text) != oPeilut.lOldLicenseNumber)
        //        return true;
        //}

        //ביטול-הוספה
        bool bCancelPeilut = ((System.Web.UI.WebControls.WebControl)(((Button)(oGridRow.Cells[SD.COL_CANCEL].Controls[0])))).CssClass == "ImgCancel";
        //int iCancelPeilut = int.Parse(((TextBox)oGridRow.Cells[SD.COL_CANCEL_PEILUT].Controls[0]).Text);
        //if ((((oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) || (oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())) && (!bCancelPeilut)) ||
         //   (((oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.AddAutomat.GetHashCode()) || (oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.AddByUser.GetHashCode())) && (bCancelPeilut)))
        if (((oPeilut.iBitulOHosafa == clGeneral.enBitulOHosafa.AddAutomat.GetHashCode()) && (bCancelPeilut)))
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
            TextBox oTxt, oShatGmar, oDayToAdd ;
            TextBox oTxtSidur= new TextBox();
            bool bInsert = false;
            SidurDM oSidur;
            bool bValid = true;
            string sSidurimThatChanged = "";
            int iMS = 0;
            //נעבור על כל הסידורים
            
                for (int iIndex = 0; iIndex < this.SD.DataSource.Count; iIndex++)
                {
                    oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                    try
                    {
                        oLbl = (Label)this.FindControl("SD").FindControl("lblSidur" + iIndex);
                    }                  
                    catch (Exception ex)
                    {
                        oLbl = null;
                        try
                        {
                            oHypLnk = (HyperLink)this.FindControl("SD").FindControl("lblSidur" + iIndex); 
                        }
                        catch (Exception ex1)
                        {
                            oTxtSidur = ((TextBox)(this.FindControl("SD").FindControl("lblSidur" + iIndex)));
                            oHypLnk = null;
                        }
                    }
                    if ((oLbl != null) || (oHypLnk != null) || (oTxtSidur!=null))
                    {
                        //oSidur = (SidurDM)(oBatchManager.htEmployeeDetails[iIndex]);                      
                        oSidur = (SidurDM)(((OrderedDictionary)Session["Sidurim"])[iIndex]);    
                        oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
                        oObjSidurimOvdimUpd.MISPAR_ISHI = iMisparIshi;
                        oObjSidurimOvdimUpd.TAARICH = dDateCard;
                        oObjSidurimOvdimUpd.MISPAR_SIDUR = (oLbl == null ? (oHypLnk == null ? (oTxtSidur.Text=="" ? 0 : int.Parse(oTxtSidur.Text)) : int.Parse(oHypLnk.Text)) : int.Parse(oLbl.Text)); 
                        oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
                        oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = oSidur.iShayahLeyomKodem;
                        oObjSidurimOvdimUpd.MENAHEL_MUSACH_MEADKEN = oSidur.iMenahelMusachMeadken;
                        oTxt = ((TextBox)(this.FindControl("SD").FindControl("txtSH" + iIndex)));
                        oObjSidurimOvdimUpd.SHAT_HATCHALA = DateTime.Parse(oTxt.Attributes["OrgShatHatchala"]);

                        if (oTxt.Text == string.Empty)
                        {
                            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = DateTime.Parse("01/01/0001 00:00:" + iMS.ToString().PadLeft(2,(char)48));
                            oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = 0;
                            iMS = iMS + 1;
                        }
                        else
                        {//נבדוק אם השתנה התאריך
                            if (oSidur.oSidurStatus == SidurDM.enSidurStatus.enNew)
                            {
                                //אם סידור חדש, ניקח את השעה מהאובייקט סידור המעודכן
                                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oSidur.dFullShatHatchala;
                                oObjSidurimOvdimUpd.SHAT_HATCHALA = oSidur.dFullShatHatchala;
                            }
                            else
                            {
                                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = GetSidurNewDate(oObjSidurimOvdimUpd.MISPAR_SIDUR, oTxt.Text); //DateTime.Parse(dDateCard.ToShortDateString() + " " + string.Concat(oTxt.Text, ":", oObjSidurimOvdimUpd.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                                if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.Second != oObjSidurimOvdimUpd.SHAT_HATCHALA.Second)
                                    oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.AddSeconds(double.Parse(oObjSidurimOvdimUpd.SHAT_HATCHALA.Second.ToString().PadLeft(2, (char)48)));
                            }
                            //אם תאריך הסידור גדול מתאריך כרטיס העבודה נסמן בסידור, שייך ליום קודם
                            if ((!oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.ToShortDateString().Equals(dDateCard.ToShortDateString())))
                                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = 1;
                            else
                                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = 0;
                            
                        }

                        //אם השתנתה שעת ההתחלה של הסידור, נכניס סידור חדש ונמחק את הקודם
                        //כמו כן נעדכן את שעת התחלת הסידור לפעילויות שמקושרות לסידור
                        bInsert = (((oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA != (DateTime.Parse(oTxt.Attributes["OrgShatHatchala"].ToString()))) &&
                                  (!((oTxt.Text == "") && (DateTime.Parse(oTxt.Attributes["OrgShatHatchala"].ToString()).Year <= DateHelper.cYearNull))))
                                  || (oSidur.oSidurStatus == SidurDM.enSidurStatus.enNew));
                        //bInsert = (((oTxt.Text != (DateTime.Parse(oTxt.Attributes["OrgShatHatchala"].ToString()).ToShortTimeString())) &&
                        //          (!((oTxt.Text == "") && (DateTime.Parse(oTxt.Attributes["OrgShatHatchala"].ToString()).Year<=DateHelper.cYearNull))))
                        //          || (oSidur.oSidurStatus==SidurDM.enSidurStatus.enNew));

                        oShatGmar = ((TextBox)(this.FindControl("SD").FindControl("txtSG" + iIndex)));
                        //מספר ימים להוספה 0 אם יום נוכחי1 - יום הבא
                        oDayToAdd = ((TextBox)(this.FindControl("SD").FindControl("txtDayAdd" + iIndex)));
                        sTmp = oShatGmar.Text;
                        dSidurDate = DateTime.Parse(oSidur.dOldFullShatGmar.ToShortDateString() + " " + sTmp); //DateTime.Parse(oShatGmar.Attributes["OrgDate"].ToString() + " " + sTmp);
                       
                        if (sTmp != string.Empty)
                            oObjSidurimOvdimUpd.SHAT_GMAR = DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));
                        else
                            oObjSidurimOvdimUpd.SHAT_GMAR = DateTime.Parse("01/01/0001 00:00:00");
                      
                        
                        oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlResonIn" + iIndex);
                        if (oDDL != null)//רק אם שונה מסידור רציפות
                        {
                            oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

                            oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlResonOut" + iIndex);
                            oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT = oDDL.SelectedValue.Equals("-1") ? 0 : int.Parse(oDDL.SelectedValue);

                            sTmp = ((TextBox)(this.FindControl("SD").FindControl("txtSHL" + iIndex))).Text;
                            if (sTmp == string.Empty)
                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateHelper.GetDateTimeFromStringHour(sTmp, DateTime.Parse("01/01/0001 00:00:00"));
                            else
                            {
                                oDayToAdd = ((TextBox)(this.FindControl("SD").FindControl("txtDayAddSHL" + iIndex)));
                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));
                            }

                            //sTmp = ((TextBox)(this.FindControl("SD").FindControl("txtSHL" + iIndex))).Text;
                            //if (sTmp == string.Empty)
                            //    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateHelper.GetDateTimeFromStringHour(sTmp, DateTime.Parse("01/01/0001 00:00:00"));
                            //else
                            //    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateHelper.GetDateTimeFromStringHour(sTmp, DateTime.Parse(oSidur.dFullShatHatchala.ToShortDateString()));//DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp);

                            sTmp = ((TextBox)(this.FindControl("SD").FindControl("txtSGL" + iIndex))).Text;
                            if (sTmp==string.Empty)
                                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = DateHelper.GetDateTimeFromStringHour(sTmp, DateTime.Parse("01/01/0001 00:00:00"));//DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp); //TODO:
                            else
                                {
                                    oDayToAdd = ((TextBox)(this.FindControl("SD").FindControl("txtDayAddSGL" + iIndex)));                                  
                                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = DateTime.Parse(dDateCard.ToShortDateString() + " " + sTmp).AddDays(int.Parse(oDayToAdd.Text));           
                                }

                            oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlException" + iIndex);
                            oObjSidurimOvdimUpd.CHARIGA = int.Parse(oDDL.SelectedValue);

                            oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlPHfsaka" + iIndex);
                            iPitzulHafsakaOldValue = oSidur.sOldPitzulHafsaka!=string.Empty ? int.Parse(oSidur.sOldPitzulHafsaka): 0;// int.Parse(oDDL.Attributes["OldV"].ToString());
                            oObjSidurimOvdimUpd.PITZUL_HAFSAKA = int.Parse(oDDL.SelectedValue);
                            //במידה והסידור הוא סידור מיוחד עם מאפיין 54 (שעון נוכחות) ובשדה פיצול הפסקה נבחר ערך "הפסקה ע"ח העובד" (KOD_PIZUL_HAFSAKA=3) יש לפצל  
                            // את הסידור 

                       
                            oDDL = (DropDownList)this.FindControl("SD").FindControl("ddlHashlama" + iIndex);
                            oObjSidurimOvdimUpd.HASHLAMA = int.Parse(oDDL.SelectedValue);
                            if (int.Parse(oDDL.SelectedValue) == clGeneral.enSugHashlama.enNoHashlama.GetHashCode())                            
                                oObjSidurimOvdimUpd.SUG_HASHLAMA = clGeneral.enSugHashlama.enNoHashlama.GetHashCode();                            
                            else                            
                                oObjSidurimOvdimUpd.SUG_HASHLAMA = clGeneral.enSugHashlama.enHashlama.GetHashCode();
                            
                            oChk = (HtmlInputCheckBox)this.FindControl("SD").FindControl("chkOutMichsa" + iIndex);
                            oObjSidurimOvdimUpd.OUT_MICHSA = oChk.Checked ? 1 : 0;


                            iCancelSidur = int.Parse(((TextBox)this.FindControl("SD").FindControl("lblSidurCanceled" + iIndex)).Text);
                            //אם סידור היה מבוטל והמשתמש מחזיר לפעיל, נזין ערך 2
                            //if (((oSidur.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) || (oSidur.iBitulOHosafa == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode())) && (iCancelSidur == 0))                       
                            //    oObjSidurimOvdimUpd.BITUL_O_HOSAFA = clGeneral.enBitulOHosafa.AddByUser.GetHashCode();                       
                            //else                        
                            oObjSidurimOvdimUpd.BITUL_O_HOSAFA = iCancelSidur;
                          
                            //לא לתשלום - במידה וסידור בוטל נעדכן בערך המקורי - ישאר ללא שינוי
                            oChk = (HtmlInputCheckBox)this.FindControl("SD").FindControl("chkLoLetashlum" + iIndex);
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
                            
                            if (SD.FirstParticipate != null)
                            {
                                if ((SD.FirstParticipate.iMisparSidur == oObjSidurimOvdimUpd.MISPAR_SIDUR)
                                    && (SD.FirstParticipate.dFullShatHatchala == oObjSidurimOvdimUpd.SHAT_HATCHALA))
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


                            if (SD.SecondParticipate != null)
                            {
                                if ((SD.SecondParticipate.iMisparSidur == oObjSidurimOvdimUpd.MISPAR_SIDUR)
                                    && (SD.SecondParticipate.dFullShatHatchala == oObjSidurimOvdimUpd.SHAT_HATCHALA))
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
                            //נבדוק אם לא קיים סידור באותו מפתח שאנחנו רוצים לבטל, כלומר שני סידורים עם אותו מפתח
                            //במידה ויש, לא נבטל את הסידור על מנת שלא יבוטל הסידור האחר                                                     
                            if ((HasSidurWithKey(oObjSidurimOvdimUpd)) && (oSidur.oSidurStatus.Equals(SidurDM.enSidurStatus.enNew)))
                                oObjSidurimOvdimDel.UPDATE_OBJECT = 100;  //^^^^^
                            else
                                oObjSidurimOvdimDel.UPDATE_OBJECT = 0;  //^^^^^
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;
                            oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                            if (oSidur.oSidurStatus.Equals(SidurDM.enSidurStatus.enNew))
                            {//אם ביטלו סידור חדש, נשווה את הערכים הישנים לחדשים כדי שלא ייכנסו רשומות לעדכוני רשמת
                                oSidur.dOldFullShatHatchala = oSidur.dFullShatHatchala;
                                oSidur.dOldFullShatGmar = oSidur.dFullShatGmar;
                                oSidur.iOldKodSibaLedivuchYadaniIn = oSidur.iKodSibaLedivuchYadaniIn;
                                oSidur.iOldKodSibaLedivuchYadaniOut = oSidur.iKodSibaLedivuchYadaniOut;
                                oSidur.iOldLoLetashlum = oSidur.iLoLetashlum;
                                oSidur.dOldFullShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum;
                                oSidur.dOldFullShatGmarLetashlum = oSidur.dFullShatGmarLetashlum;
                                oSidur.sChariga = oSidur.sOldChariga;
                                oSidur.sPitzulHafsaka = oSidur.sOldPitzulHafsaka;
                                oSidur.sHashlama = oSidur.sOldHashlama;
                                oSidur.sOutMichsa = oSidur.sOldOutMichsa;
                                oSidur.iLoLetashlum = oSidur.iOldLoLetashlum;
                                //remove fro hashtable
                                //RemoveSidurFromHashTable(iIndex);
                            }
                           }
                        else
                        {
                            if ((bInsert))
                            {
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;
                                oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();

                                FillSidurimForInsert(ref oObjSidurimOvdimIns, ref oObjSidurimOvdimUpd, ref oSidur);
                                oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
                                if (!oSidur.oSidurStatus.Equals(SidurDM.enSidurStatus.enNew)){
                                    FillSidurimForDelete(ref oObjSidurimOvdimDel, ref oObjSidurimOvdimUpd);                                
                                    oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                                }
                            }
                        }
                        if (((oObjSidurimOvdimUpd.PITZUL_HAFSAKA == clGeneral.enShowPitzul.enOvedHafsaka.GetHashCode()) && (iPitzulHafsakaOldValue!=clGeneral.enShowPitzul.enOvedHafsaka.GetHashCode()) && (oSidur.bSidurMyuhad)) && (oSidur.bShaonNochachutExists))
                        {
                            oObjSidurimOvdimIns = SplitSidur(ref oObjSidurimOvdimUpd, ref oSidur);
                            oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
                        }
                        oSidur.dFullShatHatchala = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                        oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                        //if (oSidur.oSidurStatus == SidurDM.enSidurStatus.enNew)
                        //{
                        //    oSidur.dOldFullShatGmar = oSidur.dFullShatGmar;
                        //    oSidur.dOldFullShatHatchala = oSidur.dFullShatHatchala;
                        //}
                        if (!((iCancelSidur == clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()) && (oSidur.oSidurStatus == SidurDM.enSidurStatus.enNew)))
                        {//במצב של הכנסת סידור חדש וביטולו, לא נעדכן את הערכים
                            oSidur.iKodSibaLedivuchYadaniIn = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN;
                            oSidur.iKodSibaLedivuchYadaniOut = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT;
                            oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                            oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                            oSidur.sChariga = oObjSidurimOvdimUpd.CHARIGA.ToString();
                            oSidur.sPitzulHafsaka = oObjSidurimOvdimUpd.PITZUL_HAFSAKA.ToString();
                            oSidur.sHashlama = oObjSidurimOvdimUpd.HASHLAMA.ToString();
                            oSidur.sOutMichsa = oObjSidurimOvdimUpd.OUT_MICHSA.ToString();
                            oSidur.iLoLetashlum = oObjSidurimOvdimUpd.LO_LETASHLUM;
                        }
                      
                        //אם יש פעילויות, נכניס גם אותן
                        oGridView = ((GridView)this.FindControl("SD").FindControl(iIndex.ToString().PadLeft(3, char.Parse("0"))));
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
           //oCollPeilutOvdimUpd.Sort();
            return bValid;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool HasSidurWithKey(OBJ_SIDURIM_OVDIM objSidurimOvdim)
    {
        SidurDM _Sidur;
        OrderedDictionary _Sidurim=(OrderedDictionary)Session["Sidurim"];
        bool bHasSidurWithKey = false;
        int iCount = 0;
        
        //מחפש אם קיים סידור עם אותו מפתח
        for (int iIndex = 0; iIndex < _Sidurim.Count; iIndex++)
        {
            _Sidur = (SidurDM)_Sidurim[iIndex];
            if ((_Sidur.iMisparSidur == objSidurimOvdim.MISPAR_SIDUR) && (_Sidur.dFullShatHatchala == objSidurimOvdim.NEW_SHAT_HATCHALA))
            {
                iCount = iCount + 1;
                if (iCount == 2)
                {
                    bHasSidurWithKey = true;
                    break;
                }
                
            }
        }
        return bHasSidurWithKey;
    }

    //private void RemoveSidurFromHashTable(int iIndex)
    //{
    //    oBatchManager.htEmployeeDetails.RemoveAt(iIndex);
    //    Session["Sidurim"] = oBatchManager.htEmployeeDetails;
    //}
    //private void AddEmptyRowToPeilutGrid(int iSidurIndex)
    //{
    //    GridView _GridView;
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        //Update DataTable which bind to grid with new data
    //        _GridView = ((GridView)this.FindControl("SD").FindControl(iSidurIndex.ToString().PadLeft(3, char.Parse("0"))));
    //        if (_GridView != null)
    //         {
    //             //בניית עמודות הטבלה
    //             SD.BuildDataTableColumns(ref dt);
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
        drPeilutyot["license_number"] = 0;
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
        drPeilutyot["PeilutStatus"] = 0;// SD.enPeilutStatus.enValid.GetHashCode();
        dt.Rows.Add(drPeilutyot);
    }
  
    private OBJ_SIDURIM_OVDIM SplitSidur(ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref SidurDM oSidur)
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

    private void FillSidurimForInsert(ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref SidurDM oSidur)
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
            oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
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
            oObjSidurimOvdimIns.SUG_HASHLAMA = oObjSidurimOvdimUpd.SUG_HASHLAMA;
            oObjSidurimOvdimIns.ACHUZ_KNAS_LEPREMYAT_VISA=oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA;
            oObjSidurimOvdimIns.ACHUZ_VIZA_BESIKUN = oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN;
            oObjSidurimOvdimIns.YOM_VISA = oObjSidurimOvdimUpd.YOM_VISA;
            oObjSidurimOvdimIns.MISPAR_MUSACH_O_MACHSAN = oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN;
            oObjSidurimOvdimIns.MISPAR_SHIUREY_NEHIGA = oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA;
            oObjSidurimOvdimIns.SUG_HAZMANAT_VISA = oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA;
            oObjSidurimOvdimIns.MIVTZA_VISA = oObjSidurimOvdimUpd.MIVTZA_VISA;
            oObjSidurimOvdimIns.TAFKID_VISA = oObjSidurimOvdimUpd.TAFKID_VISA;
            oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM;
            oObjSidurimOvdimIns.MENAHEL_MUSACH_MEADKEN = oObjSidurimOvdimUpd.MENAHEL_MUSACH_MEADKEN;
            if (!oSidur.bSidurMyuhad) 
                oObjSidurimOvdimIns.SUG_SIDUR = oSidur.iSugSidurRagil;
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
                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = _wcResult.oOvedYomAvodaDetails.iZmanNesiaHaloch;
                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = _wcResult.oOvedYomAvodaDetails.iZmanNesiaHazor;
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
    


    //protected void OpenReport(Dictionary<string, string> ReportParameters, Button btnScript, string sRdlName)
    //{
    //    KdsReport _Report;
    //    KdsDynamicReport _KdsDynamicReport;
    //    string sDomain = "";
    //    _KdsDynamicReport = KdsDynamicReport.GetKdsReport();
    //    _Report = new KdsReport();
    //    _Report = _KdsDynamicReport.FindReport(sRdlName);

    //    KdsLibrary.BL.clReport rep = new KdsLibrary.BL.clReport();
    //    DataTable dt = rep.GetReportDetails(((ReportName)Enum.Parse(typeof(ReportName), sRdlName)).GetHashCode());
    //    if(dt!=null && dt.Rows.Count>0)
    //    {
    //        DataRow dr = dt.Rows[0];
    //        //_Report.PageHeader = dr["PageHeader"].ToString();
    //        _Report.RSVersion = dr["RS_VERSION"].ToString();
    //        _Report.URL_CONFIG_KEY = dr["URL_CONFIG_KEY"].ToString();
    //        _Report.SERVICE_URL_CONFIG_KEY = dr["SERVICE_URL_CONFIG_KEY"].ToString();
    //        _Report.EXTENSION = dr["EXTENSION"].ToString();
    //        //_Report.RdlName = RdlName;
    //    }

    //    Session["Report"] = _Report;
    //    Session["ReportParameters"] = ReportParameters;

    //    sDomain = clGeneral.AsDomain(Request.UrlReferrer.ToString()) + Request.ApplicationPath;
    //    EventLog.WriteEntry("kds", "Url: " + sDomain);
    //    string sScript = "window.showModalDialog('" + sDomain + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "','','dialogwidth:1200px;dialogheight:800px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
               
    //    ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "ReportViewer", sScript, true);

    //}
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
    protected void btnClock_click_2(object sender, EventArgs e)
    {
        Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
        string ReportNameStr = ReportName.Presence.ToString();
        if (_wcResult.oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
            ReportNameStr = ReportName.PresenceAllSidurim.ToString();
        
        ReportParameters.Add("P_MISPAR_ISHI", iMisparIshi.ToString());
        ReportParameters.Add("P_STARTDATE",dDateCard.AddDays(-DateTime.DaysInMonth(dDateCard.Year, dDateCard.Month)).ToShortDateString());
        ReportParameters.Add("P_ENDDATE", DateTime.Now.ToShortDateString());

        OpenReport(ReportParameters, (Button)sender, ReportNameStr);
    }


    protected void btnClock_click(object sender, EventArgs e)
    {
        Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
        string ReportNameStr = ReportName.Presence.ToString();
        if (_wcResult.oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
            ReportNameStr = ReportName.PresenceAllSidurim.ToString();
        clReportOnLine oReportOnLine = new clReportOnLine(ReportNameStr, eFormat.PDF);
        DateTime FromDate = DateTime.Parse("01/" + dDateCard.Month.ToString().PadLeft(2 ,(char)48) + "/" + dDateCard.Year);
        DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);
        ToDate = ToDate < DateTime.Now ? ToDate : DateTime.Now;
        oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", iMisparIshi.ToString()));
        oReportOnLine.ReportParams.Add(new clReportParam("P_STARTDATE", FromDate.ToShortDateString()));
        oReportOnLine.ReportParams.Add(new clReportParam("P_ENDDATE", ToDate.ToShortDateString()));

        //  OpenReport(ReportParameters, (Button)sender, ReportNameStr);

        OpenReportFile(oReportOnLine, btnClock, ReportNameStr, eFormat.PDF);
    }

    //public void OpenReportFile(clReportOnLine oReportOnLine, Button btnScript, string sRdlName)
    //{
    //    byte[] s;
    //    string sScript;
        
    //    s = oReportOnLine.CreateFile();
    //    Session["BinaryResult"] = s;
    //    Session["TypeReport"] = "PDF";
    //    Session["FileName"] = sRdlName;


    //   // sScript = "window.showModalDialog('../../ModalShowPrint.aspx');";
    //    sScript = "window.showModalDialog('../../ModalShowPrint.aspx','','dialogwidth:800px;dialogheight:850px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;');";
    //    ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "PrintPdf", sScript, true);
 

    ////    sIp = "";// arrParams[1];
       
    //}
    protected DataTable GetMasachPakadim()
    {
        clOvdim _ovdim = new clOvdim();
        dtPakadim = _ovdim.GetPakadIdForMasach(MASACH_ID);
        return dtPakadim;
    }
    protected bool IsSidurChosem()
    {   
        //בכרטיס לפחות סידור מיוחד   
        //אחד ללא מאפיין 90 והסידור לא מקורו במטלה. זיהוי סידור שמקורו במטלה לפי שבאחת הרשומות של הפעילויות 
        // בסידור 0< TB_peilut_Ovdim. Mispar_matala.    

        SidurDM _Sidur;
        bool bExists = false;
        if (_wcResult.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
            {
                _Sidur = (SidurDM)(_wcResult.htFullEmployeeDetails[i]);
                if ((_Sidur.bSidurMyuhad) && (!_Sidur.bSidurLoChosemExists))
                {
                    if (!IsSidurMatala(ref _Sidur))
                    {
                        bExists = true;
                        break;
                    }
                }
            }
        }
        return bExists;
    }
    protected bool IsSidurVisa()
    {   //נכון גם לפני וגם אחרי שינויי קלט
        SidurDM _Sidur;
        bool bExists = false;
        if (_wcResult.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
            {
                _Sidur = (SidurDM)(_wcResult.htFullEmployeeDetails[i]);
                if (_Sidur.iSectorVisa>0)
                {
                    bExists = true;
                    break;
                }
            }
        }
        return bExists;
    }
    protected bool IsSidurVisaExists()
    {
        //מאפיין 45 - נכון רק לאחר שינויי קלט
        SidurDM _Sidur;
        bool bExists = false;
        if (_wcResult.htFullEmployeeDetails != null)
        {
            for (int i = 0; i < _wcResult.htFullEmployeeDetails.Count; i++)
            {
                _Sidur = (SidurDM)(_wcResult.htFullEmployeeDetails[i]);
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

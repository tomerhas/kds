<%@ Page Language="C#" AutoEventWireup="true" Inherits="Modules_Ovdim_WorkCard" Buffer="true" Trace ="false" Codebehind="WorkCard.aspx.cs" %>    
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc" Src="~/Modules/UserControl/ucSidurim.ascx" TagName="ucSidurim" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="WCard">
    <title>כרטיס עבודה</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../../StyleSheet.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
    <script src='../../js/jquery-1.11.1.min.js' type='text/javascript'></script>  
    <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
    <script src="../../Js/WorkCard.js" type="text/javascript"></script>
    <script src="../../Js/String.js" type="text/javascript"></script>
    <script src='../../js/jquery-ui.min.js' type='text/javascript'></script>  
  <%--  <script src='../../js/KdsErrorMessage.js' type='text/javascript'></script>  --%>
     
    <link type='text/css' href='../../css/jquery-ui.min.css' rel='stylesheet'/>
</head>
<body dir="rtl" style="margin:0px"> <%--  <body dir="rtl" style="margin:auto; width:1010px; height:680px">--%>
    <form id="frmWorkCard" runat="server" > 
        <%--  <div id="dialog" title="שגיאת מערכת" >
             <div >משתמש יקר ארעה שגיאה במערכת. אנא פנה למנהל המערכת.</div>
             <br />
             <a href="#" onclick="ToggleDisplay()">פרטי השגיאה:</a>
             <div id="dialogContent" dir="ltr" style="width: 380px; height: 200px; overflow-y: scroll;overflow-x:scroll;display: none"></div>
         </div>   --%>
    <asp:ScriptManager runat="server" ID="ScriptManagerKds" EnablePartialRendering="true" >     <%--   OnAsyncPostBackError="ScriptManager_AsyncPostBackError"--%>    

        <Scripts>            
            <asp:ScriptReference Name="MicrosoftAjax.js" />
            <asp:ScriptReference Name="MicrosoftAjaxWebForms.js" />
            <asp:ScriptReference Name="AjaxControlToolkit.Common.Common.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.Compat.Timer.Timer.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.Animation.Animations.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.ExtenderBase.BaseScripts.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.Animation.AnimationBehavior.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.PopupExtender.PopupBehavior.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.AutoComplete.AutoCompleteBehavior.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.ValidatorCallout.ValidatorCalloutBehavior.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.MaskedEdit.MaskedEditBehavior.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.MaskedEdit.MaskedEditValidator.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.FilteredTextBox.FilteredTextBoxBehavior.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.CollapsiblePanel.CollapsiblePanelBehavior.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.Compat.DragDrop.DragDropScripts.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.DragPanel.FloatingBehavior.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.DynamicPopulate.DynamicPopulateBehavior.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.RoundedCorners.RoundedCornersBehavior.js"
                Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.DropShadow.DropShadowBehavior.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
            <asp:ScriptReference Name="AjaxControlToolkit.ModalPopup.ModalPopupBehavior.js" Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" />
        </Scripts>
    </asp:ScriptManager>
 
    <asp:UpdateProgress runat="server" ID="Progress1" DisplayAfter="0">
        <ProgressTemplate>
            <div id="divProgress" class="Progress" style="text-align: right; position: absolute;
                 left: 50%; top: 48%; z-index: 1000;" >
                <img src="../../Images/progress.gif" alt="" style="width: 120px; height:120px;" />              
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> 
    </div>  
   <center>       
       <asp:UpdatePanel ID="upEmployeeDetails" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
         <table class="WorkCardTable1" style="width: 100%">
        <tr>                           
            <td class="WorkCardTable1Label" align="right" style="width:25%">כרטיס עבודה</td>                                            
            <td class="WorkCardTable1Label" align="left" style="width:25%">מעדכן אחרון:</td>                                            
            <td class="WorkCardTable1Label" align="right" style="width:30%">
                <a href="#" runat="server" id="lnkLastUpdateUser" target="_self" onclick="ShowLastUpdateUser();"></a>                
                <input type="hidden" runat="server" id="LastUpdateUserId" />
            </td>
            <td class="WorkCardTable1Label" align="left">תאריך עדכון אחרון:</td>                               
            <td class="WorkCardTable1Label" align="right"><label id="lblLastUpdateDate" runat="server"></label></td>                    
        </tr>
      </table>     
    
         <table width="100%" class="WorkCardTable2" cellspacing="0">
            <tr>
              <td width="89%">                                      
                <table width="100%" cellpadding="0" cellspacing="0">
                   <tr>
                    <td style="width: 8%">
                        <a href="#" runat="server" id="lnkId" onclick="ShowEmployeeDetails();" target="_self">מספר אישי:</a>  
                        <label  runat="server" id="lblId" class = "WorkCardTable1Label">מספר אישי:</label>                                                           
                    </td>
                    <td style="width: 1%"><img src="../../Images/!.png" ondblclick="GetErrorMessage(txtId,1,'');" runat="server" id="imgIdErr"/></td>
                    <td style="width: 7%">                                                                                      
                        <asp:TextBox ID="txtId" runat="server" CssClass="WorkCardTextBox" AutoComplete="Off" 
                            dir="rtl" Style="width: 50px;" TabIndex="1" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderID" runat="server" CompletionInterval="0" 
                            CompletionSetCount="25" UseContextKey="true" TargetControlID="txtId" MinimumPrefixLength="1"
                            ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx"
                            EnableCaching="true" CompletionListCssClass="ACLst"
                            FirstRowSelected="true" CompletionListHighlightedItemCssClass="ACLstItmSel"
                            CompletionListItemCssClass="ACLstItmE" OnClientHidden="onClientHiddenHandler_getID" OnClientItemSelected="onClientItemSelected_getID">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td style="width: 4%" class = "WorkCardTable1Label">שם:</td>                                                
                    <td style="width: 15%">
                        <asp:TextBox ID="txtName" CssClass="WorkCardTextBox" runat="server" AutoComplete="Off"
                            Style="width: 110px;"  ></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderByName" runat="server" CompletionInterval="0"
                            CompletionSetCount="12" UseContextKey="true" TargetControlID="txtName" MinimumPrefixLength="1"
                            ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx"
                            EnableCaching="true" CompletionListCssClass="ACLst"
                            FirstRowSelected="true" CompletionListHighlightedItemCssClass="ACLstItmSel"
                            CompletionListItemCssClass="ACLstItmE" OnClientHidden="onClientHiddenHandler_getName">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td style="width: 188px" class = "WorkCardTable1Label">מעמד:</td>                                
                    <td style="width: 21%;height:45px; " class="WorkCardTdBorder" align="right">
                        <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtMaamad" ReadOnly="true"
                            Style="width: 173px;"></asp:TextBox>
                    </td>
                    <td style="width: 5%" class = "WorkCardTable1Label">תאריך:</td>
                    <td dir="ltr" align="right" style="width: 15%">                                                                                                       
                        <KdsCalendar:KdsCalendar runat="server" ID="clnDate" TextBoxCssClass="WorkCardTextBox"  OnChangeCalScript="CheckIfCardExists();" CalenderTabIndex="1"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" TextBoxWidth="90px"></KdsCalendar:KdsCalendar>
                        <asp:CustomValidator runat="server" ID="vldDay" ErrorMessage="לא נמצא כרטיס לתאריך זה"
                            ControlToValidate="clnDate" Display="None"></asp:CustomValidator>
                        <cc1:ValidatorCalloutExtender runat="server" ID="vldEx" BehaviorID="vldExBehavior"
                            TargetControlID="vldDay" Width="200px" PopupPosition="Left">
                        </cc1:ValidatorCalloutExtender>    
                    </td>
                    <td style="width: 3%" class="WorkCardTable1Label">יום:</td>
                    <td style="width: 9%" class="WorkCardTdBorder">
                        <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtDay" Style="width: 70px;"> </asp:TextBox>
                    </td>                                           
                    <td>
                        <input type="hidden" runat="server" id="hidMeasherMistayeg" />  
                        <input type="hidden" id="hidRefresh" runat="server"/>
                        <asp:Button runat="server" ID="btnRefreshOvedDetails" Text="הצג" OnClientClick="RefreshBtn();"  OnClick="btnRefreshOvedDetails_Click"
                            CausesValidation="false" CssClass="ImgButtonShow"  TabIndex="2" Style="height: 30px; width:54px"/>
                            <input type="hidden" runat="server" id="hidSave"/>                                                                                                                                               
                    </td>                                            
                   </tr>                                        
                </table>                                                       
              </td>
              <td runat="server" id="tdCardStatus" style="width: 11%" rowspan="2">              
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr valign="bottom">
                        <td colspan="2" align="center">
                            <label runat="server" id="lblCardStatus" class="ImgButtonCardStatus"></label>                                            
                        </td>
                    </tr>                                                                     
                </table>                 
            </td>
           </tr>            
         </table>  
              <input type="hidden" runat="server" name="hidMiMeadkenOL" id="hidMiMeadkenOL"/>  
              <input type="hidden" runat="server" name="hidPratim" id="hidPratim"/>    
        </ContentTemplate>
        <Triggers>                                                                                                                                
            <asp:AsyncPostBackTrigger ControlID="btnConfirm" />                                                                    
            <asp:AsyncPostBackTrigger ControlID="btnAddHeadrut" />
            <asp:AsyncPostBackTrigger ControlID="btnFindSidur" />                                    
            <asp:AsyncPostBackTrigger ControlID="btnAddMyuchad" />
            <asp:AsyncPostBackTrigger ControlID="btnApprovalReport" />
            <asp:AsyncPostBackTrigger ControlID="btnClock" />
            <asp:AsyncPostBackTrigger ControlID="btnUpdateCard"/> 
            <asp:AsyncPostBackTrigger ControlID="btnUpdatePrint" />      
            <asp:AsyncPostBackTrigger ControlID="btnPrintWithoutUpdate" />     
            <asp:AsyncPostBackTrigger ControlID="btnCancel" /> 
            <asp:AsyncPostBackTrigger ControlID="SD" />   
            <asp:AsyncPostBackTrigger ControlID="btnRefreshOvedDetails" /> 
            <asp:AsyncPostBackTrigger ControlID="btnNextErrCard" />  
            <asp:AsyncPostBackTrigger ControlID="btnNextCard" /> 
            <asp:AsyncPostBackTrigger ControlID="btnPrevCard" />                                                                                                                                                                                                                                                                                                                                                                                                                     
        </Triggers>
      </asp:UpdatePanel>           
      <asp:UpdatePanel ID="upGeneralDetails" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>         
                <table width="100%" id="tbTabs" runat="server" class="WorkCardTable2" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td width="8px" runat="server" id="tdZmanNesiotErr" style="display:none;" ><img id="imgTimeErr"  runat="server" src="../../Images/!.png" ondblclick="GetErrorMessage(ddlTravleTime,1,'');" /></td>
                        <td id="tdZmaniNesiot" runat="server" width="70px" class = "WorkCardTable1Label"></td> 
                        <td  class="WorkCardTabs" >
                            <asp:DropDownList runat="server" ID="ddlTravleTime" CssClass="WorkCardDDLZmanNesiot"  onchange="SetBtnChanges();SetLvlChg(1,0);"
                                ondblclick="GetErrorMessage(this,1,'');" >
                            </asp:DropDownList>
                        </td>
                        <td width="117px" class="WorkCardTabs"> 
                            <input runat="server" type="button" id="btnPlus3" name="btnOpenParticipation1"
                                class="ImgButtonShowPlus"  onclick="OpenDiv('divParticipation', this.id);" />
                            <label style="font-weight: bold;cursor:pointer;	" onclick="OpenDiv('divParticipation', this.id);"> התייצבות </label>                                      
                        </td>                                             
                        <td width="150px" class="WorkCardTabs">
                            <input type="button" id="btnPlus2" name="btnOpenNetunimLeYom1" class="ImgButtonShowPlus"
                                 onclick="OpenDiv('divNetunimLeYom', this.id);" />
                            <label style="font-weight: bold;cursor:pointer;	" onclick="OpenDiv('divNetunimLeYom', this.id);">נתונים ליום עבודה</label>                                             
                        </td>                      
                        <td width="110px" class="WorkCardTabs"> 
                            <input type="button" id="btnPlus1" name="btnOpenEmployeeDetails1" class="ImgButtonShowPlus"
                                 onclick="OpenDiv('divEmployeeDetails', this.id);" />
                            <label style="font-weight: bold;cursor:pointer;" onclick="OpenDiv('divEmployeeDetails', this.id);"> פרטי העובד</label>                           
                        </td>   
                        <td width="1px" class="WorkCardTdBorder"></td>
                        <td width="48px"></td>
                        <td width="15px">
                               <asp:Button runat="server" ID="btnPrevCard" CssClass="btnPrevDay"  OnClientClick="RefreshBtn();SetNewDate(-1);" OnClick="btnRefreshOvedDetails_Click"
                                CausesValidation="false" Height="25px" />                                    
                        </td>
                        <td align="right" width="25px">
                            <asp:Button runat="server" ID="btnNextCard" CssClass="btnNextDay"  OnClientClick="RefreshBtn();SetNewDate(1);" OnClick="btnRefreshOvedDetails_Click"
                                CausesValidation="false" Height="25px" />                                    
                        </td>
                        <td width="105px" align="right" >
                            <asp:Button runat="server" ID="btnNextErrCard" CssClass="btnNextError" Text="שגוי הבא" OnClientClick="hidNextErrCard.value='1'; RefreshBtn(); " OnClick="btnRefreshOvedDetails_Click"
                                CausesValidation="false" Height="25px" />
                                    <input type="hidden" runat="server" id="hidNextErrCard"/>                                                                            
                        </td>
                        <td width="87" align="right" class="WorkCardTdBorder"></td>                                                    
                        <td width="55px" align="right" ></td>    
                        <td width="111px" id="tdCardStatus2" runat="server"></td>                                                    
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>                                                                                                                                
                <asp:AsyncPostBackTrigger ControlID="btnConfirm" />                                                                    
                <asp:AsyncPostBackTrigger ControlID="btnAddHeadrut" />
                <asp:AsyncPostBackTrigger ControlID="btnFindSidur" />                                    
                <asp:AsyncPostBackTrigger ControlID="btnAddMyuchad" />
                <asp:AsyncPostBackTrigger ControlID="btnApprovalReport" />
                <asp:AsyncPostBackTrigger ControlID="btnClock" />
                <asp:AsyncPostBackTrigger ControlID="btnUpdateCard"/> 
                <asp:AsyncPostBackTrigger ControlID="btnUpdatePrint" />      
                <asp:AsyncPostBackTrigger ControlID="btnPrintWithoutUpdate" />     
                <asp:AsyncPostBackTrigger ControlID="btnCancel" /> 
                <asp:AsyncPostBackTrigger ControlID="SD" />   
                <asp:AsyncPostBackTrigger ControlID="btnRefreshOvedDetails" />                                                                                                                                                                                                                                                                                                                                                                         
            </Triggers>
      </asp:UpdatePanel>
      <table width="100%" id="tbEmpDetails" runat="server" class="WorkCardTable2" style="display:none">
           <tr id="trTab1">
               <td>
                    <div style="display: none; width: 97%;" id="divEmployeeDetails">
                        <asp:UpdatePanel ID="upEmployee" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>        
                                <fieldset class="FilterFieldSet" >                        
                                <table width="100%" cellpadding="1">
                                    <tr>
                                        <td width="25%">
                                            עיסוק:
                                            <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtIsuk" Width="150px"
                                                ReadOnly="true"> </asp:TextBox>
                                        </td>
                                        <td width="25%">
                                            חברה:
                                            <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtCompany" Width="150px"
                                                ReadOnly="true"> </asp:TextBox>
                                        </td>
                                        <td width="25%">
                                            סניף:
                                            <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtSnif" Width="150px"
                                                ReadOnly="true"> </asp:TextBox>
                                        </td>
                                        <td width="25%">
                                            איזור:
                                            <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtArea" Width="150px"
                                                ReadOnly="true"> </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>                                
                                </fieldset>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRefreshOvedDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                 </td>
           </tr>
      </table>
      <asp:UpdatePanel ID="upCardDetails" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>            
            <table width="100%" cellpadding="1" id="tblPart" runat="server" class="WorkCardTable1" style="display:none">
                <tr>
                  <td colspan="5">
                    <div id="divNetunimLeYom" runat="server" style="display: none; width: 100%;">
                      <fieldset class="FilterFieldSet">                      
                        <table width="100%" cellpadding="1" runat="server" id="tbValWorkDay" class="WorkCardTable1">
                          <tr>         
                            <td width="50px">טכוגרף:</td>               
                            <td width="110px">
                                <asp:DropDownList runat="server" ID="ddlTachograph"  CssClass="WCSidurDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"  width="110px" > </asp:DropDownList>                           
                            </td>
                            <td width="40px">לינה:</td>
                            <td width="8px"><img id="imgLinaErr" runat="server" src="../../Images/!.png" ondblclick="GetErrorMessage(ddlLina,1,'');" /></td>
                            <td width="110px">
                                <asp:DropDownList runat="server"
                                 ID="ddlLina" CssClass="WCSidurDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"
                                    ondblclick="GetErrorMessage(this,1);" width="100px">
                                </asp:DropDownList>
                            </td>
                            <td width="40px">המרה:</td>
                            <td width="40px">
                                <input type="button" runat="server" id="btnHamara" name="btnHamara" class="ImgButtonCheckBoxEmpty" onclick="CheckButton(this,Hamara);SetLvlChg(1,0);"  />                                        
                                <input type="hidden" runat="server" id="Hamara" />
                            </td>
                            <td width="50px">הלבשה:</td>
                            <td width="8px"><img id="imgHalbErr" runat="server" src="../../Images/!.png" ondblclick="GetErrorMessage(ddlHalbasha,1,'');" /></td>
                            <td width="130px">
                                <asp:DropDownList runat="server" ID="ddlHalbasha" CssClass="WCSidurDDL" onchange="SetBtnChanges();SetLvlChg(1,0);ChangeHalbashaSelect();" 
                                    ondblclick="GetErrorMessage(this,1,'');" width="130px">
                                </asp:DropDownList>
                            </td>   
                            <td width="70px">השלמה ליום</td>      
                            <td width="8px"><img id="imgDayHaslamaErr" runat="server" src="../../Images/!.png" ondblclick="GetErrorMessage(ddlHashlamaReason,1,'');" /></td>                         
                            <td align="center" width="50px">
                                <input type="button" runat="server" id="btnHashlamaForDay" name="btnHashlamaForDay"
                                    class="ImgButtonCheckBox" onclick="CheckButton(this,HashlamaForDayValue);SetLvlChg(1,0);" />
                                <input type="hidden" runat="server" id="HashlamaForDayValue" />
                            </td>             
                            <td width="90px">סיבה להשלמה:</td>                                                        
                            <td><asp:DropDownList runat="server" ID="ddlHashlamaReason" CssClass="WCSidurDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"  width="160px"></asp:DropDownList></td>
                            <td></td>
                        </tr>                         
                        </table>
                    </fieldset>
                   </div>     
                  </td>
                </tr>
                <tr>
                    <td colspan="5">
                       <div style="display: none" id="divParticipation">  
                            <fieldset class="FilterFieldSet" >                             
                            <table width="100%" cellpadding="0" border="0">
                                <tr>                                               
                                    <td style="width:220px">
                                        התייצבות 1:
                                        <asp:TextBox runat="server" ID="txtFirstPart" CssClass="WorkCardTextBox" Width="122px" > </asp:TextBox>                                                                                                       
                                    </td>
                                    <td style="width: 285px">
                                        סיבת אי התייצבות:
                                        <asp:DropDownList runat="server" ID="ddlFirstPart" CssClass="WCSidurDDL" style="width:168px" onchange="SetBtnChanges();SetLvlChg(1,0);"></asp:DropDownList>                                                    
                                    </td>
                                    <td style="width:210px">
                                        התייצבות 2:
                                        <asp:TextBox runat="server" ID="txtSecPart" CssClass="WorkCardTextBox" Width="122px" onchange="SetBtnChanges();SetLvlChg(1,0);"> </asp:TextBox>                                                    
                                    </td>
                                    <td  style="width: 285px">
                                        סיבת אי התייצבות:
                                        <asp:DropDownList runat="server" ID="ddlSecPart" CssClass="WCSidurDDL" style="width:168px" onchange="SetBtnChanges();SetLvlChg(1,0);"></asp:DropDownList>                                                    
                                    </td>
                                </tr>
                            </table>  
                            </fieldset> 
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefreshOvedDetails" />
            <asp:AsyncPostBackTrigger ControlID="btnAddHeadrut" />           
            <asp:AsyncPostBackTrigger ControlID="btnFindSidur" />                                        
            <asp:AsyncPostBackTrigger ControlID="btnUpdateCard" />   
            <asp:AsyncPostBackTrigger ControlID="btnNextErrCard" />  
            <asp:AsyncPostBackTrigger ControlID="btnNextCard" /> 
            <asp:AsyncPostBackTrigger ControlID="btnPrevCard" />        
        </Triggers>
    </asp:UpdatePanel>               
      <asp:UpdatePanel ID="upCollpase" runat="server" RenderMode="Inline" UpdateMode="Always">                                
        <ContentTemplate>
        <div style="text-align: right; overflow: auto; height:500px;">
            <table width="100%" id="tbSidur" runat="server" cellpadding="0" cellspacing="0">
                <tr>                                     
                    <td width="100%">
                        <div id="divSidur" style="text-align: right; overflow:hidden;height:500px;" >                                        
                            <uc:ucSidurim runat="server" ID="SD"/> 
                            <input type="hidden" runat="server" id="hidErrChg" /> 
                            <input type="hidden" runat="server" id="hidExecInputChg" />                                        
                        </div>    
                    </td>
                </tr>
            </table>
        </div>   
                                
        <table width="100%" class="WorkCardTableBottom" cellspacing="0">
            <tr class="WorkCardTableBottomTrTop">
                <td width="100%">
                    <table width="100%">
                        <tr>                                    
                            <td style="width:108px">
                            <asp:Button Text="דיווח היעדרות"  style="width: 107px;" align="right"  ID="btnAddHeadrut" runat="server" OnClick="btnAddHeadrut_Click"
                                        CausesValidation="false" OnClientClick='return AddSidurHeadrut();' CssClass="btnWorkCardAddMap" />                                 
                            </td>  
                            <td style="width: 115px;" align="right"><asp:Button Text="הוסף סידור מפה" ID="btnFindSidur" runat="server" Style="width: 114px;" CssClass="btnWorkCardAddMap" CausesValidation="false" OnClientClick='return AddSidur();' OnClick="btnFindSidur_Click"/></td>
                            <td style="width: 127px;" align="right"><asp:Button Text="הוסף סידור מיוחד" ID="btnAddMyuchad" runat="server" Style="width: 124px;" CssClass="btnWorkCardAddSpecial" CausesValidation="false" OnClientClick="$get('hidExecInputChg').value ='0';$get('hidUpdateBtn').value='false'; return true;" OnClick="btnAddSpecialSidur_Click"/></td>
                            <td style="width: 242px;"></td>                                                        
                            <td style="width: 120px;">                                                                         
                                <asp:Button Text="עדכן כרטיס" ID="btnUpdateCard" runat="server"   Style="width: 150px; height: 33px;" CausesValidation="false" OnClientClick="return ChkCardVld();" OnClick="btnPopUpd_click"  />
                            </td>   
                            <td style="width: 90px;">                                            
                               <input type="button"  value="מאשר" ID="btnApprove" runat="server" onclick="btnMeasherOrMistayeg_onclick(1);" />
                            </td>
                            <td style="width: 197px;" align="right">
                               <input type="button"  value="מסתייג" ID="btnNotApprove" runat="server"  onclick="btnMeasherOrMistayeg_onclick(0);"/>   
                            </td>                                                                               
                            <td align="right" style="display:none"><asp:Button ID="btnResonOutIn" runat="server"  CausesValidation="false" OnClick="btnResonOutIn_Click"/></td>                                    
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="WorkCardTableBottomTrBottom">    
                <td width="100%">
                    <table width="100%">
                        <tr>                                                                                                                                                                                   
                            <td style="width: 128px">
                                <input type="button" value="רכיבים מחושבים" name="btnCalcItem" id="btnCalcItem" runat="server" 
                                    onclick="ShowRecivimCalculation();" class="btnWorkCardCalculate" style="width: 128px;" />   
                            </td>
                            <td  style="width: 90px">
                                <input type="hidden" runat="server" id="hidChanges" />
                                <input type="hidden" value="" id="btnHidClose" runat="server" causesvalidation="false" />                                         
                                <asp:Button Text="שגיאות לעובד" ID="btnDrvErrors" runat="server" CssClass="btnWorkCardAddMap" Style="width: 102px; " OnClientClick="return ShowDrvErr();"  CausesValidation="false" />                                                                           
                                <asp:Button ID="btnShowMessage" runat="server" OnClick="btnShowMessage_Click" Style="display: none;" />
                                <cc1:ModalPopupExtender ID="ModalPopupEx" DropShadow="false" X="300" Y="200" PopupControlID="paCloseMsg"
                                    TargetControlID="btnHidClose" CancelControlID="btnCancel"  OnCancelScript="CloseWindow();" runat="server" BehaviorID="MPClose" backgroundcssclass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel runat="server" Style="display: none" ID="paCloseMsg" CssClass="WorkCardPanelMessage" Width="480px" Height="180px" > 
                                    <table width="480px">
                                        <tr class="WorkCardPanelMessageBorder">
                                            <td colspan="2" width="480px" height="33px"  class="WorkCardPanelMessageHeader">
                                               <asp:Label ID="lblHeaderMessage" runat="server"  width="450px" >סגירת כרטיס</asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="WorkCardPanelMessageBorder">
                                            <td width="480px" height="100px" colspan="2">
                                                <br />
                                                <br />
                                                <asp:Label ID="lblMessage" runat="server" Width="90%"></asp:Label>
                                                <br />
                                                האם ברצונך לעדכן את השינויים שביצעת?<br/>
                                                <br>
                                                </br>
                                                </br>
                                            </td>
                                        </tr>
                                        <tr class="WorkCardPanelMessageHeader">
                                            <td width="380px" align="left">
                                                <asp:Button ID="btnConfirm" runat="server" Text="עדכון שינויים בכרטיס" CssClass="btnWorkCardLongUpdate"
                                                    Width="170px" OnClick="btnConfirm_click" CausesValidation="false" OnClientClick= 'return PageValidity(2);'/>
                                            </td>
                                            <td align="left">
                                                  <asp:Button runat="server" ID="btnCancel" Text="סגור ללא עדכון" CssClass="btnWorkCardUpdate" CausesValidation="false" OnClientClick="DisabledButtons(2); CloseWindow();return false;" />                                    
                                            </td>
                                        </tr>
                                    </table>                                                                                                  
                                </asp:Panel>
                                    
                                <asp:Button ID="btnShowPrintMsg" runat="server" OnClick="btnShowPrintMsg_Click" Style="display: none;" />
                                <cc1:ModalPopupExtender ID="MPEPrintMsg" DropShadow="false" X="300" Y="200" PopupControlID="pnlPrint"
                                    TargetControlID="btnShowPrintMsg" CancelControlID="btnCancel"  OnCancelScript="CloseWindow();" runat="server" BehaviorID="MPPrint" backgroundcssclass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel runat="server" Style="display: none" ID="pnlPrint" CssClass="WorkCardPanelMessage" Width="480px" Height="175px"> 
                                    <table width="480px">
                                        <tr class="WorkCardPanelMessageBorder">
                                            <td colspan="2" width="480px" height="33px"  class="WorkCardPanelMessageHeader">
                                               <asp:Label ID="Label3" runat="server" >הדפסת כרטיס</asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="WorkCardPanelMessageBorder">
                                            <td width="480px" height="100px" colspan="2">
                                                <br />
                                                <br />
                                                 <asp:Label ID="Label5" runat="server" Width="90%"></asp:Label>
                                                <br />
                                                 האם ברצונך לעדכן את השינויים שביצעת לפני הדפסת הכרטיס?<br/>
                                                <br>
                                                </br>
                                                </br>
                                            </td>
                                        </tr>
                                        <tr class="WorkCardPanelMessageHeader">
                                            <td width="380px" align="left">
                                                 <asp:Button ID="btnUpdatePrint" runat="server" Text="עדכון שינויים בכרטיס" CssClass="btnWorkCardLong"
                                                    Width="150px" OnClick="btnUpdatePrint_click" CausesValidation="false" OnClientClick= 'return PageValidity(1);'/>
                                            </td>
                                            <td align="left">
                                                 <asp:Button runat="server" ID="btnPrintWithoutUpdate" Text="הדפס ללא עדכון" CssClass="btnWorkCardLong" Width="150px" CausesValidation="false" OnClick="btnPrintWithoutUpdate_click" onClientClick='return DisabledButtons(1);'/>                                
                                            </td>
                                        </tr>
                                    </table>                                            
                                </asp:Panel>
                            </td>      
                            <td style="width: 90px">
                                <asp:Button Text="שעונים" ID="btnClock" runat="server" CssClass="btnWorkCardClocks" Style="width: 88px;" OnClick="btnClock_click" CausesValidation="false" />                                           
                            </td>
                            <td style="width: 30px">
                                <asp:Button ID="btnPrint" runat="server"  CausesValidation="false" onclick ="btnPrint_click" OnClientClick= 'return DisabledShinuyKelet();' />                                          
                            </td>                                                                       
                            <td align="right">
                                <asp:Button Text="סגור כרטיס" ID="btnCloseCard" runat="server" Style="width: 89px;" 
                                        CssClass="btnWorkCardCloseCard" CausesValidation="false" OnClientClick='return CheckChanges();' />  
                                
                            </td>  
                            <td>
                                <asp:Button Text="דוח אישורים" ID="btnApprovalReport" runat="server" CssClass="ImgButtonShow" Style="width: 90px; height: 25px; display:none;" OnClick="btnApprovalReport_click" CausesValidation="false" />                    
                            </td> 
                        </tr>
                        </table>
                    </td>                                                            
            </tr>
        </table>
       </ContentTemplate>      
       <Triggers>                                                                                                                                               
           <asp:AsyncPostBackTrigger ControlID="SD" />                                                          
        </Triggers>             
      </asp:UpdatePanel>      
   </center>
        <asp:Button ID="btnErrors" runat="server" CssClass="ImgButtonUpdate" CausesValidation="false"  Style="display: none;" />           
        <cc1:ModalPopupExtender ID="MPEErrors" DropShadow="false" CancelControlID="btnErrClose" BehaviorID="bMpeErr"
            X="200" Y="100" PopupControlID="paErrorMessage" TargetControlID="btnErrors" runat="server">
        </cc1:ModalPopupExtender>
        <asp:Panel runat="server" Style="display: none" ID="paErrorMessage" CssClass="WorkCardPanelMessageError" Width="520px" height="300px">                       
            <table border="0" width="520px" >
                <tr style="height:40px">
                    <td class="WorkCardErrMsg" width="520px"><asp:Label ID="lblErrors" runat="server" >פירוט שגיאה</asp:Label></td>
                </tr>
                <tr style="height: 40px">
                   <td class="WorkCardPanelTopTableMessage" id="Td1" align="right" width="520px"><asp:Label ID="Label8" runat="server">תיאור השגיאה</asp:Label></td>                      
                </tr>
                <tr style="height: 170px" valign="top">                    
                   <td id="tbErr" align="right"></td>                      
                </tr>
            </table>
            <table style="height: 31px; width: 520px">
                <tr style="height: 31px" class="WorkCardTopBorder">
                    <td width="500px" align="left" >
                        <asp:Button ID="btnErrClose" runat="server" Text="סגור" CssClass="btnWorkCardCloseWin"
                            Width="75px" Height="30px"  CausesValidation="false" />
                            <input type="hidden" runat="server" id="hErrKey" width="0px" />
                    </td>                                                        
                </tr>              
            </table>
        </asp:Panel>      
        <asp:Button ID="btnApp" runat="server" CssClass="ImgButtonUpdate" CausesValidation="false" Style="display: none;" />            
        <cc1:ModalPopupExtender ID="MPEApp" DropShadow="false" CancelControlID="btnAppClose"
            X="300" Y="200" PopupControlID="pnlApp" TargetControlID="btnApp" runat="server">
        </cc1:ModalPopupExtender>
        <asp:Panel runat="server" Style="display: none" ID="pnlApp" CssClass="PanelMessage" Width="350px" Height="150px">        
            <asp:Label ID="Label1" runat="server" Width="100%" CssClass="AppMsg">פירוט בקשה לאישור</asp:Label>
            <table border="1" width="100%">
                <tr>
                    <td style="background-color: white; height: 80px">
                        <label class="ErrDetailsMsg" runat="server" id="lblApp"></label>                    
                    </td>
                </tr>
            </table>
            <table style="height: 30px; width: 300px" border="1">
                <tr>
                    <td width="50px"></td>                
                    <td>
                        <asp:Button ID="btnAppClose" runat="server" Text="סגור" CssClass="ImgButtonEndUpdate" Width="100px" Height="25px" CausesValidation="false" />                            
                    </td>
                </tr>
            </table>
        </asp:Panel>        
        <asp:Button ID="btnRemark" runat="server" CausesValidation="false" Style="display: none;" />            
        <cc1:ModalPopupExtender ID="MPERemark" DropShadow="false" CancelControlID="btnRmkClose" X="50" Y="250" PopupControlID="pnlRemark" TargetControlID="btnRemark" runat="server"> </cc1:ModalPopupExtender>                   
        <asp:Panel runat="server" Style="display: none;" ID="pnlRemark" CssClass="WorkCardPanelMessage" Width="500px" height="180px" >                        
            <asp:Label ID="Label4" runat="server" width="100%" Height="30px" CssClass="WorkCardErrMsg">הערה</asp:Label>
            <table style="height: 100px; width: 500px;">   
                <tr><td style="height: 15px; Width:500px"></td></tr>                              
                <tr>
                    <td id="tblRmk" style="Width:500px"></td>                                                                                    
                </tr>
             </table>
             <hr />
             <table style="width: 500px;" cellpadding="0" cellspacing="0">     
                <tr>                                   
                    <td width="500px" align="left" valign="bottom">
                        <input type="hidden" runat="server" id="Hidden1" />
                        <asp:Button ID="btnRmkClose" runat="server" Text="סגור" CssClass="btnWorkCardCloseWin"
                            Width="80px" Height="30px" CausesValidation="false" />
                    </td>                                       
                </tr>              
            </table>                        
      </asp:Panel>     
        <input type="button" ID="btnCopy" runat="server" style="display: none;" />
        <cc1:ModalPopupExtender ID="MPECopy" dropshadow="false" X="300" Y="180" PopupControlID="paCopy"
           TargetControlID="btnCopy"  runat="server" behaviorid="pBehvCopy" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel runat="server" Style="display: none" ID="paCopy" CssClass="WorkCardPanelMessage" Width="450px">          
            <table width="450px">
                <tr class="WorkCardPanelMessageBorder">
                    <td colspan="2" width="450px" height="33px"  class="WorkCardPanelMessageHeader">
                        <asp:Label ID="Label7" runat="server" Width="100%">העתקת מספר רכב</asp:Label>
                        <input type="hidden" id="hidCarKey" />        
                        <br />                                                    
                    </td>
                </tr>
                <tr class="WorkCardPanelMessageBorder">
                    <td width="455px" height="100px" colspan="2">                        
                       <asp:Label ID="lblCarNumQ" runat="server" Width="100%"></asp:Label>    
                       <br/>                                               
                    </td>
                </tr>
                <tr class="WorkCardPanelMessageHeader">
                    <td width="380px" align="left">
                        <input type="button" ID="btnYes" runat="server" value="כן" onclick="btnCopyOtoNum(1)" CausesValidation="false" class="btnWorkCardCloseWin" style="width:80px" />       
                    </td>
                    <td align="left">
                       <input type="button" ID="btnNo"  runat="server" onclick="btnCopyOtoNum(0)"  value="לא" CausesValidation="false" class="btnWorkCardCloseWin" style="width:80px"/>       
                    </td>
                </tr>
            </table>               
        </asp:Panel>        
        <asp:Panel runat="server" Style="display: none" ID="prtMsg" CssClass="PanelMessage" Width="350px" Height="100px">                            
            <asp:Label ID="Label6" runat="server" Width="97%" BackColor="#696969" ForeColor="White">הדפסת כרטיס</asp:Label>
            <br />
            <br />
            <br />
                   כרטיס העבודה נשלח להדפסה אנא המתן
            <br/>
                  <label id="msgErrCar" runat="server" style="display:none">חסר מספר רכב, כרטיס עבודה לא יועבר לתשלום </label>       
            <br/>
     </asp:Panel>  
        
        
           
        <input type="hidden" runat="server" id="hidGoremMeasher" />
        <input type="hidden" runat="server" id="hidSource" />
        <input type="hidden" runat="server" id="hidLvl1Chg" />
        <input type="hidden" runat="server" id="hidLvl2Chg" />
        <input type="hidden" runat="server" id="hidLvl3Chg" />     
        <input type="hidden" runat="server" id="hidRashemet" />  
        <input type="hidden" runat="server" id="hidMenahelBank" />  
        <input type="hidden" runat="server" id="hidFromEmda" />  
        <input type="hidden" runat="server" id="hidSadotLSidur" />
        <input type="hidden" runat="server" id="hidDriver"/>
        <input type="hidden" runat="server" id="hidUpdateBtn"/>
        <input type="hidden" runat="server" id="hidSdrInd"/>
        <input type="hidden" runat="server" id="hidPrevHalbasha"/>     
         
    </form>   
    <script language="vbscript" type="text/vbscript">
        sub ShiftTab()
           set WSHShell=CreateObject("WScript.Shell")        
           WSHShell.SendKeys "+{Tab}"
        end sub
    </script>
    
    <script language="javascript" type="text/javascript">
        var iCount=0;
   
        var SIDUR_CONTINUE_NAHAGUT=<%= SIDUR_CONTINUE_NAHAGUT %>;
        var SIDUR_CONTINUE_NOT_NAHAGUT=<%= SIDUR_CONTINUE_NOT_NAHAGUT %>;
        document.onkeydown = KeyCheck; 
        function KeyCheck(){  
          var KeyID = event.keyCode; 
           switch(KeyID){            
              case 13: //Enter           
                 if ((document.activeElement.id!='btnRefreshOvedDetails') &&  (document.activeElement.id!='btnUpdateCard')){  
                     if (($get("txtId").value).length>5)
                     {
                         //FreeWC();
                         SetBarCode();
                     }
                     else{              
                         event.returnValue=false;
                         event.cancel = true;
                     }
                 }                 
                 break;  
              case 107: //+
                 event.keyCode=9;
                 break;
              case 109: //-                   
                   ShiftTab();                                       
                   break;
              case 110: //. //123-f12
                   if ($get("btnUpdateCard").disabled==false)
                       $get("btnUpdateCard").focus();
              
                   break;
             case 38://up 
                  GoToNextPrevLine (KeyID,document.activeElement.id);                
                  break;
             case 40://down       
                  GoToNextPrevLine (KeyID,document.activeElement.id);                 
                  break;
             }                
         }
         function GoToNextPrevLine(Direction,ElementName){
            SetFocusToNextPrevPeilutField(Direction,ElementName,'MakatNumber');
            SetFocusToNextPrevPeilutField(Direction,ElementName,'CarNumber');  
            SetFocusToNextPrevPeilutField(Direction,ElementName,'ActualMinutes'); 
            SetFocusToNextPrevPeilutField(Direction,ElementName,'ShatYetiza'); 
            SetFocusToNextPrevPeilutField(Direction,ElementName,'KisoyTor'); 
            SetFocusToNextPrevSidur(Direction,ElementName,'txtSG');
            SetFocusToNextPrevSidur(Direction,ElementName,'txtSH');
         }
         function SetFocusToNextPrevSidur(Direction,ElementName,FieldName)
         {
            var iPos=ElementName.indexOf(FieldName);
            var NextSidurName,PeilutName; 
            if (iPos>-1)
            {
               var SidurNum = ElementName.substr(ElementName.indexOf('SD_')+8);
               var _GridPeilut; 
               if (Direction==40)
               {  //down             
                    //נמצא את הסידור הבא                    
                    SidurNum = Number(SidurNum) + 1;                                         
                    while (($get("SD_txtSH".concat(SidurNum))!=null) && ($get("SD_txtSH".concat(SidurNum)).isDisabled==true)){                                                                  
                        SidurNum = Number(SidurNum) + 1;                             
                    }

                    if ($get("SD_"+FieldName.concat(SidurNum))!=null)
                        if ($get("SD_"+FieldName.concat(SidurNum)).isDisabled==false)
                            $get(("SD_"+FieldName).concat(SidurNum)).focus();                     
                 }
                 else
                 {//up      
                    SidurNum = Number(SidurNum) - 1;       
                    while ($get("SD_"+FieldName.concat(SidurNum))!=null)   
                    {                                                  
                    if ($get("SD_"+FieldName.concat(SidurNum))!=null)
                        if ($get("SD_"+FieldName.concat(SidurNum)).isDisabled==false)
                        {
                            $get(("SD_"+FieldName).concat(SidurNum)).focus();
                            break;
                        }                                                    
                    SidurNum = Number(SidurNum) - 1;   
                    }                                                                                                                                                                                      
                }                                                
            }//end if pos           
         }//end function        
         function SetFocusToNextPrevPeilutField(Direction,ElementName,FieldName){            
            var iPos=ElementName.indexOf(FieldName);
            var NextSidurName; 
            if (iPos>-1){
               var PeilutNum=ElementName.substr(ElementName.indexOf('ctl')+3,2);
               var SidurNum = ElementName.substr(ElementName.indexOf('SD_')+3,3);
               NextSidurName = "SD_" + padLeft(SidurNum.toString(),'0',3);     
               if (Direction==38) //up
                  PeilutNum=Number(PeilutNum)-1;
               else //down
                  PeilutNum=Number(PeilutNum)+1;

               var NextPeilutName = ElementName.substr(0,10) + padLeft(PeilutNum.toString(),'0',2) + ElementName.substr(12,11) + padLeft(PeilutNum.toString(),'0',2) +FieldName;
               
               if ($get(NextPeilutName)==null)
               {
                if (Direction==38){ //up
                   SidurNum = Number(SidurNum) - 1;
                   NextSidurName = "SD_" + padLeft(SidurNum.toString(),'0',3); 
                   if ($get(NextSidurName)==null)
                     PeilutNum=0;
                   else      
                     PeilutNum = Number($get(NextSidurName).firstChild.childNodes.length);
                }
                else //down
                {
                  SidurNum = Number(SidurNum) + 1;
                  PeilutNum = 2;
                  NextSidurName = "SD_" + padLeft(SidurNum.toString(),'0',3);       
                }                                                
                 NextPeilutName  = NextSidurName + "_ctl" + padLeft(PeilutNum.toString(),'0',2) + "_"   + NextSidurName + "_ctl" + padLeft(PeilutNum.toString(),'0',2) +FieldName;
                 
               }                              
                while (($get(NextPeilutName)!=null) && ($get(NextPeilutName).isDisabled==true) && ($get(NextSidurName)!=null))
                   {
                    //  נעבור לפעילות הבאה
                    if (Direction==38) //up
                        PeilutNum=Number(PeilutNum)-1;
                    else //down
                        PeilutNum=Number(PeilutNum)+1;
                   
                   NextPeilutName  = ElementName.substr(0,3) +  padLeft(SidurNum.toString(),'0',3) + "_ctl" + padLeft(PeilutNum.toString(),'0',2) + ElementName.substr(11,3) +  padLeft(SidurNum.toString(),'0',3) + "_ctl" + padLeft(PeilutNum.toString(),'0',2) +FieldName;
                   if (PeilutNum==0){             
                        SidurNum=SidurNum-1;            
                        _GridPeilut = $get("SD_" + padLeft(Number(SidurNum), '0', 3));   
                        if (_GridPeilut!=null){      
                           PeilutNum = Number(_GridPeilut.rows.length+1);
                           NextPeilutName  =  GetPeilutName(ElementName,SidurNum);// ElementName.substr(0,3) +  padLeft(SidurNum.toString(),'0',3) + "_ctl" + padLeft(PeilutNum.toString(),'0',2) + ElementName.substr(0,3) +  padLeft(SidurNum.toString(),'0',3) + "_ctl" + padLeft(PeilutNum.toString(),'0',2) +FieldName;  
                           SetFocusToNextPrevPeilutField(Direction,NextPeilutName,FieldName);
                           break;
                        }
                        else
                          break;
                    }
                    if ($get(NextPeilutName)==null)
                    {
                        if (Direction==38){ //up
                            SidurNum = Number(SidurNum) - 1;
                            NextSidurName = "SD_" + padLeft(SidurNum.toString(),'0',3);         
                            if ($get(NextSidurName)==null)
                               PeilutNum=0;
                              else
                               PeilutNum = Number($get(NextSidurName).firstChild.childNodes.length);
                        }
                        else //down
                        {
                            SidurNum = Number(SidurNum) + 1;
                            PeilutNum = 2;
                            NextSidurName = "SD_" + padLeft(SidurNum.toString(),'0',3);         
                        }
                         NextPeilutName  =  NextSidurName + "_ctl" + padLeft(PeilutNum.toString(),'0',2) + "_"   + NextSidurName + "_ctl" + padLeft(PeilutNum.toString(),'0',2) +FieldName;                              
                    }
                   }                 
                   if ($get(NextPeilutName)!=null)                  
                      if (!$get(NextPeilutName).isDisabled)
                      try{
                           $get(NextPeilutName).focus();//                   
                          }
                      catch(err)
                      {
                      }
               }                      
         }
         function chkIfBarCode(){
            var KeyID = event.keyCode;
            if (KeyID == 124){ //42
               iCount=iCount+1;
               if (iCount==3){
                  SetBarCode();
                  iCount=0;
               }
            }
         }
         function SetBarCode()
         {   
           var sKey = $get("txtId").value.split("|");                     
           $get("txtId").value =sKey[1];
           $get("clnDate").value = String(sKey[2]).substr(6,2) + "/" +  String(sKey[2]).substr(4,2) + "/" + String(sKey[2]).substr(0,4);                
           wsGeneral.SaveToBarcodeTable(Number($get("txtId").value), $get("clnDate").value,null);
           $get("btnRefreshOvedDetails").click();          
         }
         function btnMeasherOrMistayeg_onclick(value)
         {               
            SetMeasher(value); 
            //if ($get('hidFromEmda').value =='true') 
            //{  
            //    $get("btnPrint").disabled = false;                
            //    $get("btnPrint").className="btnWorkCardPrint";
            //    document.all('btnPrint').click(); 
            //}            
         }   
         
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(pageLoaded);
        prm.add_beginRequest(beginRequest);
        var postbackElement; 

        function beginRequest(sender, args) {
            postbackElement = args.get_postBackElement();
        }

        function pageLoaded(sender, args) {
            var updatedPanels = args.get_panelsUpdated();
            if (typeof(postbackElement) == "undefined"){
                return;
            }
            
            if (Number($get("SD_hidScrollPos").value)==0)
              $get("SD_dvS").scrollTop = Number($get("SD_hidScrollPos").value);            
            else
                $get("SD_dvS").scrollTop = Number($get("SD_hidScrollPos").value) + 100;            
        }   
        function ChangeHalbashaSelect()
        {
          if ($get("ddlHalbasha").value=="0")
             $get("ddlHalbasha").value = $get("hidPrevHalbasha").value;
           else
             $get("hidPrevHalbasha").value=$get("ddlHalbasha").value;
        }
        function PageValidity(MsgType)
        {
            DisabledButtons(MsgType);            
            ChkCardVld();            
            return true;
        }
        function DisabledButtons(MsgType){
            var t=setTimeout(function()
                            {
                             if (MsgType==1)
                             {
                                 $('[id$=btnUpdatePrint]').attr('disabled', 'true');
                                 $('[id$=btnPrintWithoutUpdate]').attr('disabled', 'true');
                             }
                             else{
                                  $('[id$=btnConfirm]').attr('disabled', 'true');
                                  $('[id$=btnCancel]').attr('disabled', 'true');
                             }
                            },150);
            return true;
        }

        //window.onbeforeunload = function () {
        //    window.returnValue= $get("txtId").value +'|'+ $get("clnDate").value +'|'+ $get("hidMiMeadkenOL").value;  
        //};
    </script>
</body>
</html>


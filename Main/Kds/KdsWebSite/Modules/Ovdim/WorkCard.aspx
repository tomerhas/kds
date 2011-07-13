<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkCard.aspx.cs" Inherits="Modules_Ovdim_WorkCard" Buffer="true" Trace ="false" %>    
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
    <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
    <script src="../../Js/WorkCard.js" type="text/javascript"></script>
    <script src="../../Js/String.js" type="text/javascript"></script>
    <script src='../../js/jquery.js' type='text/javascript'></script>
    
</head>
<body dir="rtl" style="margin:0px">
    <form id="frmWorkCard" runat="server" >    
    <asp:ScriptManager runat="server" ID="ScriptManagerKds" EnablePartialRendering="true" >
        
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
        <fieldset class="FilterFieldSet" style="width: 1000px; height: 677px;">
            <table class="HeaderBody" style="font-size: 14px; width: 100%">
                <tr>
                    <td style="width: 1%;"></td>                    
                    <td align="right" style="width: 50%">כרטיס עבודה</td>                                            
                    <td style="width: 10%;" align="left">מעדכן אחרון:</td>                                            
                    <td style="width: 15%;" align="right">
                        | <a href="#" runat="server" id="lnkLastUpdateUser" target="_self" onclick="ShowLastUpdateUser();">
                        </a>
                        <input type="hidden" runat="server" id="LastUpdateUserId" />
                    </td>
                    <td style="width: 14%;">תאריך עדכון אחרון:</td>                               
                    <td style="color:gray"><label id="lblLastUpdateDate" runat="server"></label></td>                    
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td width="90%">
                        <fieldset class="FilterFieldSet" style="width: 98%; height: 50px">
                            <legend>כרטיס עבודה עבור:</legend>
                            <asp:UpdatePanel ID="upEmployeeDetails" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%" cellpadding="0">
                                        <tr>
                                            <td style="width: 10%">
                                                <a href="#" runat="server" id="lnkId" onclick="ShowEmployeeDetails();" target="_self">מספר אישי:</a>                                                    
                                            </td>
                                            <td style="width: 1%"><img src="../../Images/ErrorSign.jpg" ondblclick="GetErrorMessage(txtId,1,'');" runat="server" id="imgIdErr"/></td>
                                            <td style="width: 10%">                                                                                      
                                                <asp:TextBox ID="txtId" runat="server" CssClass="WorkCardTextBox" AutoComplete="Off" 
                                                    dir="rtl" Style="width: 60px;" OnTextChanged="txtId_TextChanged" TabIndex="1"  ></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderID" runat="server" CompletionInterval="0" 
                                                    CompletionSetCount="25" UseContextKey="true" TargetControlID="txtId" MinimumPrefixLength="1"
                                                    ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx"
                                                    EnableCaching="true" CompletionListCssClass="autocomplete_completionListElement"
                                                    FirstRowSelected="true" CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientHidden="onClientHiddenHandler_getID" OnClientItemSelected="onClientItemSelected_getID">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td style="width: 4%">שם:</td>                                                
                                            <td style="width: 15%">
                                                <asp:TextBox ID="txtName" CssClass="WorkCardTextBox" runat="server" AutoComplete="Off"
                                                    Style="width: 100px;" onblur="GetOvedMisparIshiByName();" OnTextChanged="txtName_TextChanged" ></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderByName" runat="server" CompletionInterval="0"
                                                    CompletionSetCount="12" UseContextKey="true" TargetControlID="txtName" MinimumPrefixLength="1"
                                                    ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx"
                                                    EnableCaching="true" CompletionListCssClass="autocomplete_completionListElement"
                                                    FirstRowSelected="true" CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientHidden="onClientHiddenHandler_getName">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td style="width: 5%">מעמד:</td>                                
                                            <td style="width: 15%">
                                                <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtMaamad" ReadOnly="true"
                                                    Style="width: 85px;"> </asp:TextBox>
                                            </td>
                                            <td style="width: 5%">תאריך:</td>
                                            <td dir="ltr" align="right" style="width: 18%">                                                                                                       
                                                <KdsCalendar:KdsCalendar runat="server" ID="clnDate" OnChangeCalScript="CheckIfCardExists();" CalenderTabIndex="1"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" TextBoxWidth="70px"></KdsCalendar:KdsCalendar>
                                                <asp:CustomValidator runat="server" ID="vldDay" ErrorMessage="לא נמצא כרטיס לתאריך זה"
                                                    ControlToValidate="clnDate" Display="None"></asp:CustomValidator>
                                                <cc1:ValidatorCalloutExtender runat="server" ID="vldEx" BehaviorID="vldExBehavior"
                                                    TargetControlID="vldDay" Width="200px" PopupPosition="Left">
                                                </cc1:ValidatorCalloutExtender>    
                                            </td>
                                            <td style="width: 3%">יום:</td>
                                            <td style="width: 10%">
                                                <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtDay" Style="width: 80px;"> </asp:TextBox>
                                            </td>                                           
                                            <td>
                                              <input type="hidden" id="hidRefresh" runat="server"/>
                                              <asp:Button runat="server" ID="btnRefreshOvedDetails" Text="הצג" OnClientClick="RefreshBtn();" OnClick="btnRefreshOvedDetails_Click"
                                                    CausesValidation="false" CssClass="ImgButtonShow" Height="25px" TabIndex="2"/>
                                                    <input type="hidden" runat="server" id="hidSave"/>                                              
                                            </td>                                            
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
                                    <asp:AsyncPostBackTrigger ControlID="lstSidurim" />   
                                    <asp:AsyncPostBackTrigger ControlID="btnRefreshOvedDetails" />                                                                                                                                                                                                                                                                                     
                                </Triggers>
                            </asp:UpdatePanel>
                        </fieldset>
                    </td>
                    <td style="width: 10%" rowspan="2">
                        <asp:UpdatePanel ID="upApproveButton" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr valign="bottom">
                                        <td colspan="2" align="center">
                                           <label runat="server" id="lblCardStatus" class="ImgButtonCardStatus"></label>                                            
                                        </td>
                                    </tr>                                                                     
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRefreshOvedDetails"/>
                                <asp:AsyncPostBackTrigger ControlID="btnAddHeadrut"/>
                                <asp:AsyncPostBackTrigger ControlID="btnFindSidur"/>
                                <asp:AsyncPostBackTrigger ControlID="btnUpdateCard"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>            
            <table width="100%">
                <tr>
                    <td width="105px">
                        <input type="button" id="btnPlus1" name="btnOpenEmployeeDetails1" class="ImgButtonShowPlus"
                            style="width: 20px; height: 20px" value="+" onclick="OpenDiv('divEmployeeDetails', this.id);" />
                        <label style="font-weight: bold;"> פרטי העובד</label>                           
                    </td>
                    <td style="width:20px"></td>
                    <td td width="130px">
                     <input type="button" id="btnPlus2" name="btnOpenNetunimLeYom1" class="ImgButtonShowPlus"
                            style="width: 20px; height: 20px" value="+" onclick="OpenDiv('divNetunimLeYom', this.id);" />
                        <label style="font-weight: bold;">נתונים ליום עבודה</label>                                             
                    </td>
                     <td style="width:20px"></td>
                    <td>
                        <input runat="server" type="button" id="btnPlus3" name="btnOpenParticipation1"
                                    class="ImgButtonShowPlus" style="width: 20px; height: 20px" value="+" onclick="OpenDiv('divParticipation', this.id);" />
                                <label style="font-weight: bold;">התייצבות </label>                                      
                    </td>
                </tr>
            </table>
            <table width="100%" id="tbEmpDetails" runat="server">
                <tr>
                    <td>
                        <div style="display: none; width: 97%;" id="divEmployeeDetails">
                            <asp:UpdatePanel ID="upEmployee" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <fieldset class="FilterFieldSet" style="width: 955px;">
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
                    <div  id="divNetunimLeYom" runat="server" style="display: none; width: 97%; " >
                      <fieldset class="FilterFieldSet" style="width: 955px;">
                        <table width="100%" cellpadding="1" runat="server" id="tbLblWorkDay" >
                            <tr>
                                <td width="8px"><img id="imgTimeErr" runat="server" src="../../Images/ErrorSign.jpg" ondblclick="GetErrorMessage(ddlTravleTime,1,'');" /></td>
                                <td id="tdZmaniNesiot" runat="server" width="148px"></td>                                
                                <td width="120px">טכוגרף:</td>
                                <td width="8px"><img id="imgLinaErr" runat="server" src="../../Images/ErrorSign.jpg" ondblclick="GetErrorMessage(ddlLina,1,'');" /></td>
                                <td width="100px">לינה:</td>
                                <td width="50px">המרה:</td>
                                <td width="8px"><img id="imgHalbErr" runat="server" src="../../Images/ErrorSign.jpg" ondblclick="GetErrorMessage(ddlHalbasha,1,'');" /></td>
                                <td width="120px">הלבשה:</td>
                                <td width="70px">השלמה ליום</td>                                
                                <td width="8px"><img id="imgDayHaslamaErr" runat="server" src="../../Images/ErrorSign.jpg" ondblclick="GetErrorMessage(ddlHashlamaReason,1,'');" /></td>
                                <td>סיבה להשלמה:</td>                                                                 
                            </tr>
                        </table>
                        <table width="100%" cellpadding="1" runat="server" id="tbValWorkDay">
                            <tr>
                                <td width="150px">
                                    <asp:DropDownList runat="server" ID="ddlTravleTime" CssClass="WorkCardDDL"  onchange="SetBtnChanges();SetLvlChg(1,0);"
                                        ondblclick="GetErrorMessage(this,1,'');" width="160px" >
                                    </asp:DropDownList>
                                </td>
                                <td width="110px">
                                    <asp:DropDownList runat="server" ID="ddlTachograph"  CssClass="WorkCardDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"  width="110px" >
                                    </asp:DropDownList>
                                </td>
                                <td width="110px">
                                    <asp:DropDownList runat="server" ID="ddlLina" CssClass="WorkCardDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"
                                        ondblclick="GetErrorMessage(this,1);" width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td width="40px">
                                    <input type="button" runat="server" id="btnHamara" name="btnHamara" class="ImgButtonCheckBoxEmpty" onclick="CheckButton(this,Hamara);SetLvlChg(1,0);" width="30px" />                                        
                                    <input type="hidden" runat="server" id="Hamara" />
                                </td>
                                <td width="130px">
                                    <asp:DropDownList runat="server" ID="ddlHalbasha" CssClass="WorkCardDDL" onchange="SetBtnChanges();SetLvlChg(1,0);" 
                                        ondblclick="GetErrorMessage(this,1,'');" width="130px">
                                    </asp:DropDownList>
                                </td>
                                
                                <td align="center" width="50px">
                                    <input type="button" runat="server" id="btnHashlamaForDay" name="btnHashlamaForDay"
                                        class="ImgButtonCheckBox" onclick="CheckButton(this,HashlamaForDayValue);SetLvlChg(1,0);" width="40px" />
                                    <input type="hidden" runat="server" id="HashlamaForDayValue" />
                                </td>                                                             
                                <td><asp:DropDownList runat="server" ID="ddlHashlamaReason" CssClass="WorkCardDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"  width="180px"></asp:DropDownList></td>
                                <td>
                                    <input type="button" value="רכיבים מחושבים" name="btnCalcItem" id="btnCalcItem" runat="server" 
                                        onclick="ShowRecivimCalculation();" class="ImgButtonShow" style="width: 120px; height: 25px" />                                       
                                </td>
                            </tr>
                         
                        </table>
                        </fieldset>
                      </div>     
                    <table width="100%" cellpadding="1" id="tblPart" runat="server">
                        <tr>
                            <td colspan="5">
                                <div style="display: none" id="divParticipation">
                                    <fieldset class="FilterFieldSet" style="width: 955px;">
                                        <table width="97%" cellpadding="0">
                                            <tr>                                               
                                                <td style="width: 23.25%">
                                                    התייצבות ראשונה:
                                                    <asp:TextBox runat="server" ID="txtFirstPart" CssClass="WorkCardTextBox" Width="100px" > </asp:TextBox>                                                                                                       
                                                </td>
                                                <td style="width: 26.75%">
                                                    סיבת אי התייצבות:
                                                    <asp:DropDownList runat="server" ID="ddlFirstPart" CssClass="WorkCardDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"></asp:DropDownList>                                                    
                                                </td>
                                                <td style="width: 23%">
                                                    התייצבות שניה:
                                                    <asp:TextBox runat="server" ID="txtSecPart" CssClass="WorkCardTextBox" Width="100px" onchange="SetBtnChanges();SetLvlChg(1,0);"> </asp:TextBox>                                                    
                                                </td>
                                                <td style="width: 27%">
                                                    סיבת אי התייצבות:
                                                    <asp:DropDownList runat="server" ID="ddlSecPart" CssClass="WorkCardDDL" onchange="SetBtnChanges();SetLvlChg(1,0);"></asp:DropDownList>                                                    
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
                </Triggers>
            </asp:UpdatePanel>            
            <asp:UpdatePanel ID="upCollpase" runat="server" RenderMode="Inline" UpdateMode="Always">                                
               <ContentTemplate>
                    <div style="text-align: right; overflow: auto">
                        <table width="100%" id="tbSidur" runat="server" cellpadding="0" cellspacing="0" >
                            <tr>                                     
                                <td width="100%">
                                    <div id="divSidur" style="text-align: right; overflow:hidden;">                                        
                                      <uc:ucSidurim runat="server" ID="lstSidurim"/> 
                                      <input type="hidden" runat="server" id="hidErrChg" /> 
                                      <input type="hidden" runat="server" id="hidExecInputChg" />                                        
                                   </div>    
                                </td>
                            </tr>
                        </table>
                      </div>   
                                            
                    <table width="100%" border="1">
                        <tr>
                            <td style="width: 10%">
                                <asp:Button Text="דיווח היעדרות" ID="btnAddHeadrut" runat="server"  Style="width: 160px; height: 25px" OnClick="btnAddHeadrut_Click"
                                 CssClass="ImgButtonUpdate" CausesValidation="false" OnClientClick='return AddSidurHeadrut();' />
                            </td>  
                            <td align="right"><asp:Button Text="הוסף סידור מפה" ID="btnFindSidur" runat="server" Style="width: 150px; height: 25px; " CssClass="ImgButtonUpdate" CausesValidation="false" OnClientClick='return AddSidur();' OnClick="btnFindSidur_Click"/></td>
                            <td align="right"><asp:Button Text="הוסף סידור מיוחד" ID="btnAddMyuchad" runat="server" Style="width: 150px; height: 25px; " CssClass="ImgButtonUpdate" CausesValidation="false" OnClientClick="$get('hidExecInputChg').value ='0';return true;" OnClick="btnAddSpecialSidur_Click"/></td>
                            <td  style="width: 80%" align="left">                                            
                                <input type="button"  value="מאשר" ID="btnApprove" runat="server" width="70px" height="35px"  onclick="btnMeasherOrMistayeg_onclick(1);" />
                                <input type="button"  value="מסתייג" ID="btnNotApprove" runat="server" width="70px" height="30px"  onclick="btnMeasherOrMistayeg_onclick(0);" />   
                            </td>                            
                       </tr>
                    </table>
           </ContentTemplate>             
         </asp:UpdatePanel>  

    <asp:UpdatePanel ID="upCloseCard" runat="server" UpdateMode="Always">
        <ContentTemplate>    
            <table style="width:980px;height:30px" cellpadding="1">
                <tr>
                    <td style="width: 70%">                        
                       <table style="width: 100%">
                            <tr>
                                 <td style="width: 50%" align="right">
                                    <input type="hidden" runat="server" id="hidChanges" />
                                    <input type="hidden" value="" id="btnHidClose" runat="server" causesvalidation="false" />                                         
                                    <asp:Button Text="סגור כרטיס" ID="btnCloseCard" runat="server" Style="width: 100px; 
                                        height: 25px" CssClass="ImgButtonShow" CausesValidation="false" OnClientClick='return CheckChanges();' />
                                    <asp:Button Text="הדפס כרטיס" ID="btnPrint" runat="server" Style="width: 100px;
                                        height: 25px" CssClass="ImgButtonShow" CausesValidation="false" onclick ="btnPrint_click" OnClientClick='return SetChgFlag();' />    
                                    <asp:Button ID="btnShowMessage" runat="server" OnClick="btnShowMessage_Click" Style="display: none;" />
                                    <cc1:ModalPopupExtender ID="ModalPopupEx" DropShadow="false" X="300" Y="200" PopupControlID="paCloseMsg"
                                        TargetControlID="btnHidClose" CancelControlID="btnCancel"  OnCancelScript="CloseWindow();" runat="server" BehaviorID="MPClose">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel runat="server" Style="display: none" ID="paCloseMsg" CssClass="PanelMessage" Width="350px" Height="130px">                            
                                        <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">סגירת כרטיס</asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblMessage" runat="server" Width="90%"></asp:Label>
                                        <br />
                                        האם ברצונך לעדכן את השינויים שביצעת?<br/>
                                        <br>
                                        </br>
                                        <asp:Button ID="btnConfirm" runat="server" Text="עדכון שינויים בכרטיס" CssClass="ImgButtonMake"
                                            Width="150px" OnClick="btnConfirm_click" CausesValidation="false" OnClientClick= 'return ChkCardVld();'/>
                                        <asp:Button runat="server" ID="btnCancel" Text="סגור ללא עדכון" CssClass="ImgButtonMake" Width="150px" CausesValidation="false" OnClientClick="CloseWindow();return false;" />                                
                                    </asp:Panel>
                                    
                                    <asp:Button ID="btnShowPrintMsg" runat="server" OnClick="btnShowPrintMsg_Click" Style="display: none;" />
                                    <cc1:ModalPopupExtender ID="MPEPrintMsg" DropShadow="false" X="300" Y="200" PopupControlID="pnlPrint"
                                        TargetControlID="btnShowPrintMsg" CancelControlID="btnCancel"  OnCancelScript="CloseWindow();" runat="server" BehaviorID="MPPrint">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel runat="server" Style="display: none" ID="pnlPrint" CssClass="PanelMessage" Width="350px" Height="130px">                            
                                        <asp:Label ID="Label3" runat="server" Width="97%" BackColor="#696969" ForeColor="White">הדפסת כרטיס</asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label5" runat="server" Width="90%"></asp:Label>
                                        <br />
                                             האם ברצונך לעדכן את השינויים שביצעת לפני הדפסת הכרטיס?<br/>
                                        <br>
                                        </br>
                                        <asp:Button ID="btnUpdatePrint" runat="server" Text="עדכון שינויים בכרטיס" CssClass="ImgButtonMake"
                                            Width="150px" OnClick="btnUpdatePrint_click" CausesValidation="false" OnClientClick= 'return ChkCardVld();'/>
                                        <asp:Button runat="server" ID="btnPrintWithoutUpdate" Text="הדפס ללא עדכון" CssClass="ImgButtonMake" Width="150px" CausesValidation="false" OnClick="btnPrintWithoutUpdate_click" />                                
                                    </asp:Panel>
                                 </td>
                                 <td>                                                                         
                                    <asp:Button Text="עדכן כרטיס" ID="btnUpdateCard" runat="server"  CssClass="ImgButtonShow" Style="width: 150px; height: 25px" CausesValidation="false" OnClientClick="return ChkCardVld();" OnClick="btnPopUpd_click" />                                                                                                               
                                 </td>      
                            </tr>  
                       </table>                     
                     </td>         
                    <td style="width: 10%">
                        <asp:Button Text="שגיאות" ID="btnDrvErrors" runat="server" CssClass="ImgButtonShow" Style="width: 90px; height: 25px" OnClientClick="return ShowDrvErr();"  CausesValidation="false" />                    
                    </td>  
                    <td style="width: 10%">
                        <asp:Button Text="דוח אישורים" ID="btnApprovalReport" runat="server" CssClass="ImgButtonShow" Style="width: 90px; height: 25px; display:none;" OnClick="btnApprovalReport_click" CausesValidation="false" />                    
                    </td>
                    <td style="width: 10%" align="left">
                        <asp:Button Text="שעונים" ID="btnClock" runat="server" CssClass="ImgButtonShow" Style="width: 80px; height: 25px" OnClick="btnClock_click" CausesValidation="false" />                                           
                    </td>                            
                </tr>
            </table>    
           </ContentTemplate>
       </asp:UpdatePanel>   
    </fieldset>
   </center>
        <asp:Button ID="btnErrors" runat="server" CssClass="ImgButtonUpdate" CausesValidation="false"
            Style="display: none;" />
        <cc1:ModalPopupExtender ID="MPEErrors" DropShadow="false" CancelControlID="btnErrClose" BehaviorID="bMpeErr"
            X="300" Y="200" PopupControlID="paErrorMessage" TargetControlID="btnErrors" runat="server">
        </cc1:ModalPopupExtender>
        <asp:Panel runat="server" Style="display: none" ID="paErrorMessage" CssClass="PanelMessage" Width="450px" height="250px">            
            <asp:Label ID="lblErrors" runat="server" Width="100%" CssClass="ErrMsg">פירוט שגיאה</asp:Label>
            <table border="1" width="100%" >
                <tr>
                    <td id="tbErr" style="background-color: white; height: 180px"></td>                      
                </tr>
            </table>
            <table style="height: 30px; width: 500px">
                <tr>
                    <td width="10px"></td>                
                    <td width="100px">
                        <asp:Button ID="btnErrClose" runat="server" Text="סגור" CssClass="ImgButtonEndUpdate"
                            Width="100px" Height="25px" CausesValidation="false" />
                    </td>                    
                    <td><input type="hidden" runat="server" id="hErrKey" width="0px" /></td>                    
                </tr>
            </table>
        </asp:Panel>      
    <asp:Button ID="btnApp" runat="server" CssClass="ImgButtonUpdate" CausesValidation="false"
        Style="display: none;" />
    <cc1:ModalPopupExtender ID="MPEApp" DropShadow="false" CancelControlID="btnAppClose"
        X="300" Y="200" PopupControlID="pnlApp" TargetControlID="btnApp" runat="server">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" Style="display: none" ID="pnlApp" CssClass="PanelMessage"
        Width="350px" Height="150px">
        <asp:Label ID="Label1" runat="server" Width="100%" CssClass="AppMsg">פירוט בקשה לאישור</asp:Label>
        <table border="1" width="100%">
            <tr>
                <td style="background-color: white; height: 80px">
                    <label class="ErrDetailsMsg" runat="server" id="lblApp"></label>                    
                </td>
            </tr>
        </table>
        <table style="height: 30px; width: 300px">
            <tr>
                <td width="50px">
                </td>
                <td>
                    <asp:Button ID="btnAppClose" runat="server" Text="סגור" CssClass="ImgButtonEndUpdate"
                        Width="100px" Height="25px" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>        
    <asp:Button ID="btnRemark" runat="server" CausesValidation="false" Style="display: none;" />            
        <cc1:ModalPopupExtender ID="MPERemark" DropShadow="false" CancelControlID="btnRmkClose" X="50" Y="250" PopupControlID="pnlRemark" TargetControlID="btnRemark" runat="server">            
        </cc1:ModalPopupExtender>
        <asp:Panel runat="server" Style="display: none;" ID="pnlRemark" CssClass="PanelMessage" Width="200px" height="100px">            
            <asp:Label ID="Label4" runat="server" Width="100%" CssClass="RemarkMsg">הערה</asp:Label>
            <table border="1" width="100%" >
                <tr>
                    <td id="tblRmk" style="background-color: white; height: 30px"></td>                      
                </tr>
            </table>
            <table style="height: 30px; width: 500px">
                <tr>
                    <td width="10px"></td>                
                    <td width="100px">
                        <asp:Button ID="btnRmkClose" runat="server" Text="סגור" CssClass="ImgButtonEndUpdate"
                            Width="80px" Height="20px" CausesValidation="false" />
                    </td>                    
                    <td><input type="hidden" runat="server" id="Hidden1" width="0px" /></td>                    
                </tr>
            </table>            
      </asp:Panel>     
       <input type="button" ID="btnCopy" runat="server" style="display: none;" />
      <cc1:ModalPopupExtender ID="MPECopy" dropshadow="false" X="500" Y="280" PopupControlID="paCopy"
         TargetControlID="btnCopy"  runat="server" behaviorid="pBehvCopy" BackgroundCssClass="modalBackground">
      </cc1:ModalPopupExtender>
      <asp:Panel runat="server" Style="display: none" ID="paCopy" CssClass="modalPopup" Width="350px">
        <asp:Label ID="Label2" runat="server" Width="100%" BackColor="#696969" ForeColor="White">העתקת מספר רכב</asp:Label>
        <input type="hidden" id="hidCarKey" />        
        <br />
            <asp:Label ID="lblCarNumQ" runat="server" Width="100%"></asp:Label>    
        <br />
        <br />
        <input type="button" ID="btnYes" runat="server" value="כן" onclick="btnCopyOtoNum(1)" CausesValidation="false" class="ImgButtonEndUpdate" style="width:80px" />       
        <input type="button" ID="btnNo"  runat="server" onclick="btnCopyOtoNum(0)"  value="לא" CausesValidation="false" class="ImgButtonEndUpdate" style="width:80px"/>       
     </asp:Panel>   
     
     <asp:Panel runat="server" Style="display: none" ID="prtMsg" CssClass="PanelMessage" Width="350px" Height="100px">                            
            <asp:Label ID="Label6" runat="server" Width="97%" BackColor="#696969" ForeColor="White">הדפסת כרטיס</asp:Label>
            <br />
            <br />
            <br />
                   כרטיס העבודה נשלח להדפסה אנא המתן
            <br/>
                  <label id="msgErrCar" runat="server" style="display:none"  >חסר מספר רכב, כרטיס עבודה לא יועבר לתשלום </label>       
            <br/>
     </asp:Panel>  
         
    <input type="hidden" runat="server" id="hidGoremMeasher" />
    <input type="hidden" runat="server" id="hidSource" />
    <input type="hidden" runat="server" id="hidLvl1Chg" />
    <input type="hidden" runat="server" id="hidLvl2Chg" />
    <input type="hidden" runat="server" id="hidLvl3Chg" />
    <input type="hidden" runat="server" id="hidMeasherMistayeg" />   
    <input type="hidden" runat="server" id="hidRashemet" />  
    <input type="hidden" runat="server" id="hidFromEmda" />  
    <input type="hidden" runat="server" id="hidSadotLSidur" />
    <input type="hidden" runat="server" id="hidDriver"/>
    </form>   
    <script language="javascript" type="text/javascript">
         
//       frmWorkCard.submit =SaveScrollPositionSubmit();
        var SIDUR_CONTINUE_NAHAGUT=<%= SIDUR_CONTINUE_NAHAGUT %>;var SIDUR_CONTINUE_NOT_NAHAGUT=<%= SIDUR_CONTINUE_NOT_NAHAGUT %> 
        document.onkeydown = KeyCheck; 
        function KeyCheck(){  
          var KeyID = event.keyCode; 
           switch(KeyID){            
              case 13: //Enter           
                 if ((document.activeElement.id!='btnRefreshOvedDetails') &&  (document.activeElement.id!='btnUpdateCard')){  
                     if ((document.getElementById("txtId").value).length>5)
                         SetBarCode();
                     else{              
                         event.returnValue=false;
                         event.cancel = true;
                         }
                 }                 
                 break;  
              case 107:
                 event.keyCode=9;
                 break;
              case 110: //. //123-f12
              if (document.getElementById("btnUpdateCard").disabled==false)              
                    document.getElementById("btnUpdateCard").focus();
              
                 break;
             }                
         }
         function SetBarCode()
         {
           var sKey = document.getElementById("txtId").value.split("*");                     
           document.getElementById("txtId").value =sKey[1];
           document.getElementById("clnDate").value = String(sKey[2]).substr(6,2) + "/" +  String(sKey[2]).substr(4,2) + "/" + String(sKey[2]).substr(0,4);                
           document.getElementById("btnRefreshOvedDetails").click();          
         }
         function btnMeasherOrMistayeg_onclick(value)
         {       
            SetMeasher(value); 
            if (document.getElementById('hidFromEmda').value =='true') 
                {
                    document.getElementById("btnPrint").disabled=false;
                    document.all('btnPrint').click(); 
                } 
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
            
            $get("lstSidurim_dvS").scrollTop = $get("lstSidurim_hidScrollPos").value;            
         }   
        
    </script>
</body>
</html>


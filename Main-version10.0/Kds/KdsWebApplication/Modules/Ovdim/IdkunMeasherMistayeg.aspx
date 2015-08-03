<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeBehind="IdkunMeasherMistayeg.aspx.cs"  Title="Untitled Page" Inherits="KdsWebApplication.Modules.Ovdim.IdkunMeasherMistayeg"  %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">

<script src='../../js/jquery.js' type='text/javascript'></script>
    <script src='../../js/jquery.simplemodal.1.4.4.min.js' type='text/javascript'></script>      
    <script src='../../js/basic.js' type='text/javascript'></script>
    <link type='text/css' href='../../css/basic.css' rel='stylesheet' media='screen' />
    <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 128px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server"> 
   <script type="text/javascript" language="javascript">
       var oTxtId = "<%=txtId.ClientID%>";
       var oTxtName = "<%=txtName.ClientID%>";
       var flag = false;
       var userId = iUserId;
   </script>     
    <div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> 
</div>  
<div id="divSinun" runat="server"  onkeydown="if (event.keyCode==107) {event.keyCode=9; return event.keyCode }">
       <asp:UpdatePanel ID="upRdoId" runat="server" RenderMode="Inline" UpdateMode="Always">
                        <ContentTemplate> 
      <fieldset class="FilterFieldSet" style="width:950px;height:80px">
        <legend>רשימת כרטיסי עבודה עבור</legend> 
        <table cellpadding="2" cellspacing="0" border="0" style="margin-top:4px"  >
            <tr>
                <td>
                 
                        <table>
                            <tr>                                
                                <td class="InternalLabel" style="width:90px">                                       
                                  <asp:RadioButton runat="server" Checked="true" ID="rdoId"   EnableViewState="true" GroupName="grpSearch" Text="מספר אישי:"    > </asp:RadioButton>                                        
                                </td>
                                                         
                                <td style="width:120px;">
                                        <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" dir="rtl" onchange="GetOvedNameById();"    MaxLength="5" style="width:100px;" TabIndex="1" ></asp:TextBox>                            
                                        <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                            TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                            EnableCaching="true"  CompletionListCssClass="ACLst"
                                            CompletionListHighlightedItemCssClass="ACLstItmSel"
                                            CompletionListItemCssClass="ACLstItmE"   
                                            OnClientHidden="SimunExtendeIdClose"  OnClientShowing="SimunExtendeOpen"  >                        
                                        </cc1:AutoCompleteExtender>                            
                                </td>                
                                <td class="style1">                    
                                  <asp:RadioButton runat="server" ID="rdoName" EnableViewState="true" GroupName="grpSearch" Text="שם:" > </asp:RadioButton>
                                </td>   
                                
                                <td style="width:220px;">
                                        <asp:TextBox ID="txtName" runat="server" AutoComplete="Off" style="width:180px;" TabIndex="2"   onchange="GetOvedIdByName();" ></asp:TextBox>
                                        <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                                    TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="ACLst" 
                                                    CompletionListHighlightedItemCssClass="ACLstItmSel"
                                                    CompletionListItemCssClass="ACLstItmE"
                                                    OnClientHidden="SimunExtendeNameClose"  OnClientShowing="SimunExtendeOpen"  >                                                                                     
                                        </cc1:AutoCompleteExtender>  
                                </td>
                                  <td class="InternalLabel" style="width:40px">
                                        תאריך:
                                    </td>
                                    <td align="right" dir="ltr"  style="width:160px">  
                                      <KdsCalendar:KdsCalendar runat="server" ID="clnTaarich" CalenderTabIndex="4"  AutoPostBack="false" OnChangeCalScript="onChange_FromDate();"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>           
                                       <asp:CustomValidator runat="server" id="vldTaarich"   ControlToValidate="clnTaarich" ErrorMessage=""  Display="None"    ></asp:CustomValidator>
                                       <cc1:ValidatorCalloutExtender runat="server" ID="vldExFrom" BehaviorID="vldExFromDate"   TargetControlID="vldTaarich" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                                      <%--<wccEgged:wccCalendar runat="server" ID="clnTaarich" BasePath="../../EggedFramework" AutoPostBack="false" Width="110px" dir="rtl"></wccEgged:wccCalendar>--%>                                                                      
                                    </td>     
                                <td>
                                   <asp:Button Text="הצג" ID="btnExecute" runat="server" TabIndex="5" 
                                CssClass ="ImgButtonSearch" autopostback="true" OnClientClick="return CheckNetunimExist();" OnClick="btnExecute_Click"
                                Width="62px" onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" />    
                                </td>                       
                            </tr>        
                        </table>  
                      
                </td>                               
            </tr>
           </table>   
       
     </fieldset>    
    <br />
    <br />

    <fieldset id="fsPratim" runat="server" style="width:400px;height:400px; display:none">   
         <legend>פרטי סטטוס כרטיס</legend>  
          <br />
        
                      <table >
                          <tr>
                              <td style="width:80px" >מספר אישי:</td>
                              <td style="width:200px" >
                                  <br />
                                  <asp:Label ID="lblMis" runat="server"></asp:Label>
                                  <br />
                                  <br />
                              </td>
                       
                         </tr>
                          <tr>
                              <td  class="bold">תאריך:</td>
                               <td >
                                   <br />
                                    <a id="lnkTaarich" href="#" runat="server"  onclick="OpenEmpWorkCard()"></a>
                                
                                 <%--  <asp:Label ID="lblTaarich" runat="server"></asp:Label>--%>
                                   <br />
                                   <br />
                              </td>
                          </tr>
                          <tr>
                              <td  class="bold">סטטוס:</td>
                              <td  >
                                  <br />
                                  <asp:DropDownList ID="ddlstatus" runat="server" Enabled="false" Width="150px"></asp:DropDownList>
                                  <br />
                                  <br />
                              </td>
                              </tr>
                          <tr>
                              <td  class="bold">
                                  <br />
                                  סיבה:
                                  <br />
                              </td>
                               <td >
                                   <br />
                                   <asp:TextBox ID="txtSiba" runat="server" Enabled="false" Width="300px"  ></asp:TextBox>
                                   <br />
                              </td>
                          </tr>
              
                          <tr align="left" dir="ltr" >
                              <td  colspan="2" align="left" dir="ltr" >
                                   
                                 <br />
                                 <br />
                                 <br />
                                    <asp:Button Text="עדכן" ID="btnUpdate" runat="server" TabIndex="5"  Enabled="false"
                                        CssClass ="ImgButtonSearch" autopostback="true"  OnClientClick="return CheckNetunimNOtEmpty();" OnClick="btnUpdate_Click"  
                                        Width="62px" onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" />      
                              </td>
                          </tr>
                           <tr align="right"  >
                              <td  colspan="2" align="right" dir="ltr" >
                                   
                                 <br />
                                    <label style="font-weight:bold"><bold>
                                        בסיום עדכון:<br />
                                        1. חובה לפתוח את כרטיס העבודה לבדיקת תקינות הכרטיס. <br />
                                        2. אם תאריך הוא לחודש מוקדם משני חודשי רטרו, חובה  <br />
                                           &nbsp; &nbsp;  .("לפתוח את הכרטיס" (לבצע בו עדכון  
                                        </bold>
                                    </label> 
                              </td>
                          </tr>
                      </table>   
                 

           </fieldset>       
                <input type="hidden" id="Params" name="Params"  runat="server" />
            </ContentTemplate>
</asp:UpdatePanel>   
 </div>      
   <script language="javascript" type="text/javascript">
       window.onload = function () {
           SetTextBox();
       }
       function CheckNetunimExist(){
           if (document.getElementById("ctl00_KdsContent_txtId").value == "" ||  document.getElementById('ctl00_KdsContent_clnTaarich').value =="")
               return false;
           else return true;
       }
       function CheckNetunimNOtEmpty() {
           if (document.getElementById("ctl00_KdsContent_txtSiba").value == "") {
               alert("חובה להקליד סיבה");
               return false;
           }
           else return true;
       }
       function SetTextBox() {

           var rdo = document.getElementById("ctl00_KdsContent_rdoId");
           if (rdo.checked) {
               document.getElementById("ctl00_KdsContent_txtId").disabled = false;
               document.getElementById("ctl00_KdsContent_txtName").disabled = true;
           }
           else {
               document.getElementById("ctl00_KdsContent_txtName").disabled = false;
               document.getElementById("ctl00_KdsContent_txtId").disabled = true;
           }
       }

           function onChange_FromDate() {
               //  debugger;
               var Param100 = document.getElementById("ctl00_KdsContent_Params").attributes["Param100"].value;//($('[id$=Params]')[0]).attributes["Param100"].value;// document.getElementById("ctl00_KdsContent_Params").attributes("Param100").value;
               var StartDateSplit = document.getElementById('ctl00_KdsContent_clnTaarich').value.split('/');
               var StartDate = new Date(StartDateSplit[2], StartDateSplit[1] - 1, StartDateSplit[0], 0, 0, 0, 0);
               var minDate = new Date();
               minDate.setDate(1);
               minDate.setMonth(minDate.getMonth() - Param100);
               minDate.setHours(0);
               minDate.setMinutes(0);
               minDate.setSeconds(0);
               minDate.setMilliseconds(0);

               if (StartDate.getTime() < minDate.getTime()) {
                   var sBehaviorId = 'vldExFromDate';
                   document.getElementById("ctl00_KdsContent_vldTaarich").errormessage = " לא ניתן להזין תאריך מעבר ל " + Param100 + " חודשים אחורה";
                   $find(sBehaviorId)._ensureCallout();
                   $find(sBehaviorId).show(true);
                   document.getElementById("ctl00_KdsContent_btnExecute").disabled = true;
               }
               else
                   document.getElementById("ctl00_KdsContent_btnExecute").disabled = false;

           }
           function continue_click() {

               // SetTextBox();
           }

           function OpenEmpWorkCard() {
              

                var EmpId = $('#<%=lblMis.ClientID %>').html();
               var WCardDate = $('#<%=lnkTaarich.ClientID %>').html();
               var sQuryString = "?EmpID=" + EmpId + "&WCardDate=" + WCardDate + "&dt=" + Date();
               document.getElementById("divHourglass").style.display = 'block';
               var ReturnWin = window.showModalDialog('WorkCard.aspx' + sQuryString, window, "dialogHeight: 680px; dialogWidth: 1010px; scroll: no;status: 1;");

               document.getElementById("divHourglass").style.display = 'none';
               if (ReturnWin == '' || ReturnWin == 'undefined' || ReturnWin == undefined)
                   ReturnWin = false;

             
            return ReturnWin;
        }
   </script>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeErrors.aspx.cs" Inherits="Modules_Errors_EmployeErrors" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src='../../js/jquery.js' type='text/javascript'></script>
    <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
<script type="text/javascript">

$(document).ready(function(){
    EnlargeFieldSetsForIE8("0", null, 80);    
});
document.onkeydown = ChangeKeyCode;
function ChangeKeyCode()
{ if (event.keyCode == 107)  event.keyCode = 9;   }
</script>
    <style type="text/css">
        .style1
        {
            color: Black;
            font-family: Arial;
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server"> 
   <script type="text/javascript" language="javascript">
       var oTxtId = "<%=txtId.ClientID%>";
       var oTxtName = "<%=txtName.ClientID%>";
       var flag = false;
   </script>     
    <fieldset class="FilterFieldSet" >          
        <legend>חיתוך לפי</legend>
         
        <table class="FilterTable" cellpadding="0" cellspacing="2" dir="rtl"  width="970px" >
            <tr>
                <td class="InternalLabel" style="width:40px">
                    איזור:
                </td>
                <td style="width:160px">
                    <asp:UpdatePanel ID="upSite" runat="server" RenderMode="Inline" >
                        <ContentTemplate> 
                            <asp:DropDownList ID="ddlSite" runat="server"  style="width:110px" TabIndex="1"
                                AutoPostBack="true" onselectedindexchanged="ddlSite_SelectedIndexChanged" ></asp:DropDownList>
                        </ContentTemplate>  
                      <%--    <Triggers>
                 
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />                                                                 
                       </Triggers>   --%>                    
                    </asp:UpdatePanel>                                   
                </td>
                <td class="InternalLabel" style="width:40px">
                    סניף:
                </td>
                <td style="width:320px">
                    <asp:UpdatePanel ID="upSnif" runat="server" RenderMode="Inline" UpdateMode="Conditional"   >
                        <ContentTemplate>                         
                            <asp:TextBox ID="txtSnif" runat="server" AutoComplete="Off" TabIndex="2" dir="rtl" style="width:300px" ></asp:TextBox>
                            
                            <cc1:AutoCompleteExtender id="AutoCompleteSnif"  runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  DelimiterCharacters=";" 
                                TargetControlID="txtSnif" MinimumPrefixLength="1" ServiceMethod="GetSnifim" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"
                               OnClientHidden="CheckSnif" >                                
                            </cc1:AutoCompleteExtender>
                            <asp:button id="btnMaamad" runat="server" onclick="btnMaamad_Click"  />
                            <input type="hidden" id="txtCurrSnifKod" runat="server" />
                       </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSite" /> 
                        <%--    <asp:AsyncPostBackTrigger ControlID="btnSearch" />   --%>                                                                                                           
                       </Triggers>   
                    </asp:UpdatePanel>                    
                </td>
                <td class="InternalLabel" style="width:40px">
                    מעמד:
                </td>
                <td>
                   <asp:UpdatePanel ID="upMaamad" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:DropDownList ID="ddlMaamad" runat="server" TabIndex="3"  OnSelectedIndexChanged="ddlMaamad_SelectedIndexChanged" style="width:250px" AutoPostBack="true" ></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnMaamad" />                                                                    
                            <asp:AsyncPostBackTrigger ControlID="ddlSite" /> 
                           <%-- <asp:AsyncPostBackTrigger ControlID="btnSearch" /> --%>                                                                
               
                        </Triggers>  
                   </asp:UpdatePanel>         
                </td>
               </tr>
        </table>                 
        <table class="FilterTable" width="970px" cellpadding="0">
            <tr>
                <td class="InternalLabel" style="width:40px">
                    מיום:
                </td>
                <td align="right" dir="ltr"  style="width:160px">  
                  <KdsCalendar:KdsCalendar runat="server" ID="clnFromDate" CalenderTabIndex="4"  AutoPostBack="false" OnChangeCalScript="onChange_FromDate();"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>           
                   <asp:CustomValidator runat="server" id="vldFrom"   ControlToValidate="clnFromDate" ErrorMessage=""  Display="None"    ></asp:CustomValidator>
                   <cc1:ValidatorCalloutExtender runat="server" ID="vldExFrom" BehaviorID="vldExFromDate"   TargetControlID="vldFrom" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                  <%--<wccEgged:wccCalendar runat="server" ID="clnFromDate" BasePath="../../EggedFramework" AutoPostBack="false" Width="110px" dir="rtl"></wccEgged:wccCalendar>--%>                                                                      
                </td>  
                <td class="InternalLabel" style="width:40px">
                    עד יום:
                </td> 
                <td align="right" dir="ltr" style="width:150px">  
                    <KdsCalendar:KdsCalendar runat="server"  ID="clnToDate" CalenderTabIndex="5"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                             
                    <%--<wccEgged:wccCalendar runat="server" ID="clnToDate" BasePath="../../EggedFramework" AutoPostBack="false" Width="110px" dir="rtl"></wccEgged:wccCalendar>--%>                                                                                                                      
                </td>               
                <td>
                    <asp:UpdatePanel ID="upExecute" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                         <ContentTemplate>
                            <asp:Button Text="הצג" ID="btnExecute" runat="server"  CssClass ="ImgButtonSearch" autopostback="true" onclick="btnExecute_Click" TabIndex="6"
                            onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" />                            
                            <input type="hidden" id="hidMisparIshi" runat="server" />                                                            
                       </ContentTemplate>                                              
                    </asp:UpdatePanel >          
                </td>                    
                <td>                                       
                   <asp:CustomValidator id="vldCmpDates" runat="server" ClientValidationFunction="CheckDates"  ErrorMessage="תאריך סיום קטן מתאריך התחלה" Display="Dynamic"    ></asp:CustomValidator>
                </td>
            </tr>                   
         </table>                
   </fieldset>
      
   <fieldset class="FilterFieldSet"> 
        <legend >חיפוש לפי</legend>      
        <table class="FilterTable">
            <tr>
                <td class="InternalLabel" style="width:80px">
                    <asp:RadioButton runat="server" Checked="true" ID="rdoId"  GroupName="grpSearch" Text="מספר אישי"  > </asp:RadioButton>
                </td>
                <td style="width:200px;">
                <asp:UpdatePanel ID="upId" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" onchange="GetOvedNameById();" MaxLength="5" dir="rtl"  TabIndex="7"  ></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0"  UseContextKey="true"  
                                TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetOvdimById" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"  CompletionSetCount="25"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement" 
                                 OnClientHidden="SimunExtendeIdClose"  OnClientShowing="SimunExtendeOpen"  >                                   
                            </cc1:AutoCompleteExtender>                              
                       </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />    
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />                                                                 
                       </Triggers> 
                 </asp:UpdatePanel>           
                </td>                
                <td class="style1">
                     <asp:RadioButton runat="server" ID="rdoName" GroupName="grpSearch" Text="שם"   > </asp:RadioButton>
                </td>               
                <td style="width:200px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional"  >
                        <ContentTemplate> 
                            <asp:TextBox ID="txtName" runat="server" AutoComplete="Off"  onchange="GetOvedIdByName();"  style="width:200px" TabIndex="8" ></asp:TextBox>
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                        TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                        CompletionListItemCssClass="autocomplete_completionListItemElement" 
                                       OnClientHidden="SimunExtendeNameClose"  OnClientShowing="SimunExtendeOpen"  >                                 
                            </cc1:AutoCompleteExtender> 
                         </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />                                                                    
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" /> 
                       </Triggers> 
                 </asp:UpdatePanel>       
                </td>
                <td colspan="5">
                    <asp:UpdatePanel ID="upSearch" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:button ID="btnSearch" runat="server" text="חפש"  TabIndex="9"  
                                CssClass ="ImgButtonSearch" onclick="btnSearch_Click"
                                onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" /> 
                        </ContentTemplate>
                         <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddlSite" />    
                           <asp:AsyncPostBackTrigger ControlID="btnMaamad" /> 
                            <asp:AsyncPostBackTrigger ControlID="ddlMaamad" />    
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />                                                                                               
                       </Triggers> 
                    </asp:UpdatePanel>                                    
                </td>                
            </tr>     
            <tr>
                <td colspan="9"> 
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:CustomValidator ID="vldEmpNotExists" CssClass="ErrorMessage" 
                                        runat="server" Display="Dynamic" ErrorMessage="עובד לא נמצא בתחום!" 
                                        onservervalidate="vldEmpNotExists_ServerValidate" >
                            </asp:CustomValidator>
                         </ContentTemplate>
                         <Triggers>                                                                                               
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" /> 
                         </Triggers> 
                     </asp:UpdatePanel> 
                  </td>                                         
            </tr>                 
        </table>
   </fieldset>
   <div>
        <label class="SubTitleLabel">תוצאות חיפוש:</label>
   </div>
   <table> 
    <tr>
        <td>            
            <asp:UpdatePanel ID="udGrid" runat="server" RenderMode="Inline" UpdateMode="Always" >
                 <ContentTemplate> 
                    <asp:Button ID="btnRedirect" runat="server"  OnCommand="btnRedirect_Click"  />
                    <asp:textbox  ID="txtRowSelected" runat="server"  />

                    <asp:GridView ID="grdEmployee" runat="server" AllowSorting="true" 
                         AllowPaging="true" PageSize="6" AutoGenerateColumns="false" CssClass="Grid"  
                         Width="960px" EmptyDataText="לא נמצאו נתונים!"  
                         OnRowDataBound="grdEmployee_RowDataBound" Height="70px" OnSorting="grdEmployee_Sorting" OnPageIndexChanging="grdEmployee_PageIndexChanging">
                        <Columns>
                            <asp:HyperLinkField DataTextField="mispar_ishi"   ItemStyle-CssClass="ItemRow"   HeaderStyle-CssClass="GridHeader"  Text="מספר אישי"  HeaderText="מספר אישי" SortExpression="mispar_ishi" NavigateUrl="#"  ItemStyle-Width="100px"  />                                          
                            <asp:BoundField DataField="full_name" HeaderText="שם" SortExpression="full_name" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"   />
                            <asp:BoundField DataField="teur_ezor" HeaderText="איזור" SortExpression="teur_ezor" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                            <asp:BoundField DataField="teur_snif_av" HeaderText="סניף" SortExpression="teur_snif_av" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                            <asp:BoundField DataField="teur_maamad_hr" HeaderText="מעמד" SortExpression="teur_maamad_hr" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                            <asp:BoundField DataField="status_key" HeaderText="שגויים"  SortExpression="status_key" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                          <%--  <asp:BoundField DataField="measher_o_mistayeg_key" HeaderText="מסתייגים" SortExpression="measher_o_mistayeg_key"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField DataField="status_tipul_key" HeaderText="ממתינים לאישור" SortExpression="status_tipul_key"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-HorizontalAlign="Center"/>
                        </Columns>
                        <AlternatingRowStyle CssClass="GridAltRow"  />
                        <RowStyle CssClass="GridRow"   />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                        <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/>                                                    
                        <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                        </PagerTemplate>  
                    </asp:GridView>
                 </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnExecute" />    
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers> 
             </asp:UpdatePanel>      
        </td>     
    </tr>           
   </table>
   <input type="hidden" id="Params" name="Params"  runat="server" />
   <input type="hidden" id="InputHiddenBack" name="InputHiddenBack" value="false" runat="server" />
   <script language="javascript" type="text/javascript">
    function window.onload()
    {
       SetTextBox();
    }
    function SetTextBox() {

        var rdo = document.getElementById("ctl00_KdsContent_rdoId");
        if (rdo.checked)
        {           
         document.getElementById("ctl00_KdsContent_txtId").disabled=false;           
         document.getElementById("ctl00_KdsContent_txtName").disabled=true;
        }
        else{         
            document.getElementById("ctl00_KdsContent_txtName").disabled=false;           
            document.getElementById("ctl00_KdsContent_txtId").disabled=true;
        }
        document.getElementById("ctl00_KdsContent_txtSnif").focus();
    }
    function CheckSnif()
    {
        var sSnifName = document.getElementById("ctl00_KdsContent_txtSnif").value;

        //document.getElementById("ctl00_KdsContent_btnSearch").disabled = true;
       if (sSnifName!='')
       {
           if (sSnifName.indexOf(')')==-1) 
           {
               alert('סניף לא נמצא');
              // document.getElementById("ctl00_KdsContent_txtSnif").value='';
               document.getElementById("ctl00_KdsContent_txtSnif").focus();
           } else document.getElementById("ctl00_KdsContent_btnMaamad").click();  
       }   
    }
    function GetMaamad()
    { 
        document.getElementById("ctl00_KdsContent_btnMaamad").click();        
    }
    
    function CheckDates(src,args)
    {
      var StartDateString = document.getElementById('ctl00_KdsContent_clnFromDate').value;
     var EndDateString = document.getElementById('ctl00_KdsContent_clnToDate').value;
 
     var StartDateSplit = StartDateString.split('/');
     var EndDateSplit = EndDateString.split('/');

     var StartDate = new Date  (StartDateSplit[2],StartDateSplit[1],StartDateSplit[0],0,0,0,0);
     var EndDate = new Date  (EndDateSplit[2],EndDateSplit[1],EndDateSplit[0],0,0,0,0);
     
     args.IsValid = (StartDate <= EndDate);
 }
 function getMassege() {
     var Param100 = document.getElementById("ctl00_KdsContent_Params").attributes("Param100").value;
     return "jjj";
   //  
 }
    function onChange_FromDate() {
      
        var Param100 = document.getElementById("ctl00_KdsContent_Params").attributes("Param100").value;
        var StartDateSplit = document.getElementById('ctl00_KdsContent_clnFromDate').value.split('/');
        var StartDate = new Date(StartDateSplit[2], StartDateSplit[1]-1, StartDateSplit[0], 0, 0, 0, 0);
        var minDate = new Date();
        minDate.setDate(1);
        minDate.setMonth(minDate.getMonth() - Param100);
        minDate.setHours(0);
        minDate.setMinutes(0);
        minDate.setSeconds(0);
        minDate.setMilliseconds(0);

        if (StartDate.getTime() < minDate.getTime()) {
            var sBehaviorId = 'vldExFromDate';
            document.getElementById("ctl00_KdsContent_vldFrom").errormessage = " לא ניתן להזין תאריך מעבר ל " + Param100  + " חודשים אחורה";
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            document.getElementById("ctl00_KdsContent_btnExecute").disabled = true;
        }
        else 
            document.getElementById("ctl00_KdsContent_btnExecute").disabled = false;
          
    }
 
    function onClientHiddenHandler_getID(sender, eventArgs)
    {
     GetOvedName(document.getElementById("ctl00_KdsContent_txtId"));
    }     
    
    function onClientHiddenHandler_getName(sender, eventArgs)
    {
     //GetOvedMisparIshi(document.getElementById("ctl00_KdsContent_txtName"));
     var iMisparIshi, iPos;
     var sOvedName=document.getElementById("ctl00_KdsContent_txtName").value;  
     if (sOvedName!='')
      {  
         iPos = sOvedName.indexOf('(');
         if (iPos==-1)
         {           
          //  alert('שם לא נמצא');                        
//            document.getElementById("ctl00_KdsContent_txtId").value='';
//            document.getElementById("ctl00_KdsContent_txtName").value='';
//            document.getElementById("ctl00_KdsContent_txtName").select();
         }
         else{
            iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
            document.getElementById("ctl00_KdsContent_txtId").value=iMisparIshi;
            document.getElementById("ctl00_KdsContent_txtName").value=sOvedName.substr(0,iPos-1);
            }
       }     
    }
      
     function GetOvedMisparIshiByName()
     {
      GetOvedMisparIshi(document.getElementById("ctl00_KdsContent_txtName"));
     } 
   
    function GetOvedMisparSucc(result)
    {
        if (result==''){
            alert('שם לא נמצא');                                    
            document.getElementById("ctl00_KdsContent_txtName").value='';
            document.getElementById("ctl00_KdsContent_txtName").select();
        }
        else{
            document.getElementById("ctl00_KdsContent_txtId").value=result;
        }
    }
    
    function GetOvedNameSucceeded(result) {
     if (result==''){
        alert('מספר אישי לא קיים');                        
        document.getElementById("ctl00_KdsContent_txtId").select();
     }
     else{
         document.getElementById("ctl00_KdsContent_txtName").value=result;
     }
    }
  
    function OpenOvedDetails(RowIndex)
    {
      
//         var RowSelection,sQuryString,sLeft;
         
         //RowSelection=eval(document.all.ctl00_KdsContent_grdEmployee.children(0).children(RowIndex+1));
         //this.CommandArgument = RowSelection.children(0).children(0).innerHTML;          
         //RowSelection=eval(document.all.ctl00_KdsContent_grdEmployee.children(0).children(RowIndex+1).firstChild.innerText);
         document.getElementById("ctl00_KdsContent_txtRowSelected").value=RowIndex;//RowSelection;
         var btn= document.getElementById("ctl00_KdsContent_btnRedirect");   
         btn.click();
         
//         sQuryString="?dt=" + Date();
//         sQuryString = sQuryString + "&OvedId=" + RowSelection.children(0).children(0).innerHTML;
//         
//         sLeft=(document.body.clientWidth/2)-400;
//         window.open("EmployeeDetails.aspx" + sQuryString,"","dialogwidth:960px;dialogheight:690px;dialogtop:" + (document.body.clientWidth/10) + "px;dialogleft:" + sLeft + "px;status:no")
     }

     function continue_click() {
        // SetTextBox();
     }  
   </script>
</asp:Content>


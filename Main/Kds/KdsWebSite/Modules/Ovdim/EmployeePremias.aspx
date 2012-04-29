<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
AutoEventWireup="true" CodeFile="EmployeePremias.aspx.cs" 
Inherits="Modules_Ovdim_EmployeePremias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src='../../js/jquery.js' type='text/javascript'></script>
<script src='../../js/jquery.simplemodal.js' type='text/javascript'></script>
<script src='../../js/basic.js' type='text/javascript'></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
   <script type="text/javascript" language="javascript">
       var oTxtId = "<%=txtId.ClientID%>";
       var oTxtName = "<%=txtName.ClientID%>";
       var flag = false;
       var userId = 0;
</script>
    <fieldset class="FilterFieldSet">          
        <legend>פרמיה לעדכון</legend>
        <asp:UpdatePanel ID="upTypes" runat="server" RenderMode="Inline">
            <ContentTemplate> 
                <table class="FilterTable" style="width:50%;"> 
                    <tr>
                        <td class="InternalLabel" style="width:80px;">
                            סוג פרמיה:
                        </td>
                        <td>
                            <asp:DropDownList runat="server"
                                AutoPostBack="true" ID="ddStatuses" 
                                DataValueField="Kod_Premia" 
                                DataTextField="Teur_Premia"
                                  >
                            </asp:DropDownList>
                        </td>
                        <td class="InternalLabel" style="width:40px;">
                            חודש:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" 
                                AutoPostBack="true" ID="ddMonths" Enabled="false" DataTextField="month" DataValueField="Value">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button Text="הצג" ID="btnExecute"  Enabled="false"
                                runat="server" CssClass="ImgButtonSearch" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                <asp:AsyncPostBackTrigger ControlID="btnExecute" />
                <%--<asp:AsyncPostBackTrigger ControlID="grdPremias" />--%>
            </Triggers>
        </asp:UpdatePanel>        
    </fieldset>
    <fieldset > 
        <legend>חיפוש לפי</legend>      
        <table class="FilterTable" style="width:100%;"> 
            <tr>
                <td class="InternalLabel" style="width:80px;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:RadioButton runat="server" ID="rdoId" 
                                Checked="true" GroupName="grpSearch" 
                                Text="מספר אישי">
                            </asp:RadioButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                        </Triggers> 
                   </asp:UpdatePanel>
                </td>
                <td style="width:200px;">
                    <asp:UpdatePanel ID="upId" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:TextBox ID="txtId" runat="server" MaxLength="5" onchange="GetOvedNameById();" onfocus="this.select();" AutoComplete="Off" dir="rtl">
                            </asp:TextBox> 
                            <cc1:AutoCompleteExtender
                                id="AutoCompleteExtenderID" runat="server" CompletionInterval="100"
                                CompletionSetCount="12"   UseContextKey="true" TargetControlID="txtId"   MinimumPrefixLength="1" 
                                ServiceMethod="GetOvdimById"
                                EnableCaching="true" 
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"
                                OnClientHidden="SimunExtendeIdClose"  OnClientShowing="SimunExtendeOpen"  >   
                            </cc1:AutoCompleteExtender>     
                       </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                       </Triggers> 
                 </asp:UpdatePanel>           
                </td>                
                <td class="InternalLabel" style="width:40px;">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:RadioButton runat="server" ID="rdoName" 
                                GroupName="grpSearch" Text="שם">
                            </asp:RadioButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                        </Triggers> 
                   </asp:UpdatePanel>
                </td>               
                <td style="width:200px">
                    <asp:UpdatePanel ID="upName" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:TextBox ID="txtName" runat="server"    onchange="GetOvedIdByName();" onfocus="this.select();"
                                style="width:200px;" AutoComplete="Off">
                               <%-- onblur="if(this.value != ''){onClientHiddenHandler_getName(this,null);}"--%>
                            </asp:TextBox>
                            <cc1:AutoCompleteExtender 
                                id="AutoCompleteExtenderByName" runat="server"                                 CompletionInterval="100" 
                                CompletionSetCount="12"                                                        UseContextKey="true" TargetControlID="txtName"                                 MinimumPrefixLength="1" 
                                ServiceMethod="GetOvdimByName" 
                                EnableCaching="true" 
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"  
                                 OnClientHidden="SimunExtendeNameClose"  OnClientShowing="SimunExtendeOpen"  >      
                            </cc1:AutoCompleteExtender> 
                             <%--   OnClientHidden="onClientHiddenHandler_getName"--%>
                         </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" /> 
                       </Triggers> 
                 </asp:UpdatePanel>                         
                </td>                
                <td colspan="5">
                    <asp:UpdatePanel ID="upSearch" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:Button ID="btnSearch" runat="server" Enabled="false"
                                Text="חפש" CssClass ="ImgButtonSearch" />
                        </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />
                       </Triggers> 
                    </asp:UpdatePanel>                                    
                </td> 
                <td class="InternalLabel" style="width:80px;">
                    <asp:Label runat="server" ID="lblPremiaMinutes" 
                        Text="דקות פרמיה">
                    </asp:Label>
                </td>
                <td style="width:50px">
                    <asp:UpdatePanel ID="upPremiaMinutes" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:TextBox ID="txtPremiaMinutes" runat="server"  MaxLength="4"
                                AutoComplete="Off" style="width:50px;">
                            </asp:TextBox>
                         </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" /> 
                       </Triggers> 
                 </asp:UpdatePanel>                         
                </td>
                <td colspan="5">
                     <asp:UpdatePanel ID="upUpdate" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>  
                            <asp:Button ID="btnUpdate" runat="server"  
                                Text="עדכן" CssClass="ImgButtonSearch" 
                                
                                OnClientClick="return Onclick_UpdatePremiaMinutes();" onclick="btnUpdate_Click" 
                               />
                        </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                       </Triggers> 
                    </asp:UpdatePanel>                                  
                </td>               
            </tr>     
            <tr>
                <td colspan="9"> 
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" 
                        RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:CustomValidator ID="vldEmpNotExists" 
                            CssClass="ErrorMessage" runat="server" 
                            Display="Dynamic" 
                            ErrorMessage="עובד לא נמצא בתחום!" 
                            onservervalidate="vldEmpNotExists_ServerValidate">
                            </asp:CustomValidator>
                         </ContentTemplate>
                         <Triggers>            
                            <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                         </Triggers> 
                     </asp:UpdatePanel> 
                  </td>                                         
            </tr>           
        </table>
   </fieldset>
   
    <asp:UpdatePanel ID="udGrid" runat="server" 
                RenderMode="Inline" UpdateMode="Always">
                 <ContentTemplate> 
                 <div id="divGrdPremyot" runat="server" style="display:none" >
   <fieldset id="fsGrid" runat="server"  class="FilterFieldSet">
    <legend>רשימת עובדים זכאים לפרמיה</legend>
     
                        <table width="920px">
                            <tr>
                            <td>            
                  
                            <asp:GridView ID="grdPremias" runat="server" 
                                AllowSorting="true" AllowPaging="true" PageSize="6"                            AutoGenerateColumns="false" CssClass="Grid"  
                                Width="920px" EmptyDataText="לא נמצאו נתונים!" 
                                Height="70px"
                                HeaderStyle-CssClass="GridHeader"
                                HeaderStyle-ForeColor="White"  OnSorting="grdPremias_Sorting"
                                DataKeyNames="Mispar_Ishi,Shem,Maamad,Ezor,Snif,Dakot_premya,Taarich_Idkun">
                                <Columns>                             
                                    <asp:BoundField HeaderText="מ.א." 
                                        DataField="Mispar_Ishi" SortExpression="Mispar_Ishi" />
                                    <asp:BoundField HeaderText="שם" 
                                        DataField="Shem" SortExpression="Shem" />
                                    <asp:BoundField HeaderText="מעמד" 
                                        DataField="Maamad" SortExpression="Maamad" />
                                    <asp:BoundField HeaderText="אזור" 
                                        DataField="Ezor" SortExpression="Ezor" />
                                    <asp:BoundField HeaderText="סניף" 
                                        DataField="Snif" ItemStyle-Width="150" SortExpression="Snif" />
                                    <asp:TemplateField HeaderText="דקות פרמיה"  SortExpression="Dakot_Premya"                                        ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDakotPremia" MaxLength="4" 
                                            runat="server" Width="80"/>
                                         <%-- <asp:CustomValidator runat="server" id="vldDakot" ControlToValidate="txtDakotPremia" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                            <cc1:ValidatorCalloutExtender runat="server" ID="exvdakot" BehaviorID="vldExtndrDakot"  TargetControlID="vldDakot" Width="180px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>  --%>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="תאריך עדכון" 
                                        ItemStyle-Width="150" DataField="Taarich_Idkun"    DataFormatString="{0:dd/MM/yyyy}" 
                                        HtmlEncode="false" SortExpression="Taarich_Idkun" />
                                </Columns>
                                <AlternatingRowStyle CssClass="GridAltRow" />
                                <RowStyle CssClass="GridRow" />
                                <PagerStyle CssClass="GridPager" 
                                            HorizontalAlign="Center" />   
                                <EmptyDataRowStyle CssClass="GridEmptyData" 
                                            height="20px" Wrap="False"/>
                                <SelectedRowStyle CssClass="SelectedGridRowNoIndent" />
                                <PagerTemplate>
                                     <kds:GridViewPager runat="server" 
                                        ID="ucGridPager" />
                                </PagerTemplate>
                            </asp:GridView>
                             <div style="float:left;margin-left:10px;margin-top:10px;">
                          <asp:Button ID="btnUpdateGrid" runat="server" 
                            Text="עדכן" CssClass="ImgButtonSearch" OnClientClick="if (!CheckExistanceOfValidValues()) { alert('ישנם ערכי דקות פרמיה בלתי חוקיים'); return false; }" />
                         </div>   
                    </td>     
                </tr>    
            </table>
   </fieldset>
   </div>  
    <input type="hidden" id="ListOvdim" name="ListOvdim"  runat="server" />
    </ContentTemplate>
             <%--    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnExecute" />    
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers> --%>
               
 </asp:UpdatePanel> 
   
  
    
    <script language="javascript" type="text/javascript">
  
    function load()
    {
        window.moveTo(0,0);
        window.resizeTo(screen.availWidth,screen.availHeight);
        
       SetTextBox();
    }
    
    function SetTextBox()
    {
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
    }
    
    function CheckExistanceOfValidValues() {
         var invalidRows = 0;
         var $gridTable = $('#ctl00_KdsContent_grdPremias');
         var rows = $gridTable.find('tbody > tr');
         var slicedRows = rows.slice(1, rows.length - 2);
         slicedRows.each(function() {
             var cells = $(this).find('td');
             var cellDakot = cells.get(5);
             $(cellDakot).find('input').each(function() {
                 if ($(this).val() != '') {
                     if (!IsDecimal($(this).val())) {
                         invalidRows++;
                     }
                 }
             });
         });
         if(invalidRows >0) {            
            return false;
         }
         else {
            return true;
         }
    }

//    function GetOvedNameById() {
//        
//        GetOvedName(document.getElementById("ctl00_KdsContent_txtId"));
//    }             
//    function onClientHiddenHandler_getID(sender, eventArgs)
//    {
//     GetOvedName(document.getElementById("ctl00_KdsContent_txtId"));
//    }     
    
//    function onClientHiddenHandler_getName(sender, eventArgs)
//    {
//     var iMisparIshi, iPos;
//     var sOvedName = document.getElementById("ctl00_KdsContent_txtName").value;
//     var name;   
//     if(sOvedName != ""){
//        if (sOvedName.indexOf('(')>-1)
//            name = sOvedName.split(')')[0];
//     GetOvedMisparIshiByName(arrOvedName[0]);
//     }
//     if (sOvedName!='')
//      {  
//         iPos = sOvedName.indexOf('(');
//         if (iPos==-1)
//         {           

//         }
//         else{
//            iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
//            document.getElementById("ctl00_KdsContent_txtId").value=iMisparIshi;
//            document.getElementById("ctl00_KdsContent_txtName").value = sOvedName.substr(0, iPos - 1);
//            wsGeneral.GetDakotPremiya(iMisparIshi,
//                                   "01/" + document.getElementById("ctl00_KdsContent_ddMonths").value,
//                                   document.getElementById("ctl00_KdsContent_ddStatuses").value,
//                                   GetDakotPremiyaSucc);
//            }
//       }     
//    }
      
     function GetOvedMisparIshiByName()
     {
         var sOvedName = document.getElementById("ctl00_KdsContent_txtName").value;
        // var name;   
         if(sOvedName != ""){
            if (sOvedName.indexOf(')')>-1)
                sOvedName = sOvedName.split('(')[0];
            wsGeneral.GetOvedMisparIshi(trim(sOvedName), GetOvedMisparSucc); 
      }
     } 
             
    function GetOvedMisparSucc(result) {
           if (result == '' || result == "null" || result == null  ) {
           
                alert('שם לא נמצא');                                    
            document.getElementById("ctl00_KdsContent_txtName").select();
            document.getElementById("ctl00_KdsContent_txtId").value = "";
            document.getElementById("ctl00_KdsContent_txtPremiaMinutes").value = "";
            document.getElementById("ctl00_KdsContent_btnUpdate").disabled = true;
            document.getElementById("ctl00_KdsContent_btnSearch").disabled = true;
        }
        else  {
            document.getElementById("ctl00_KdsContent_txtId").value = result;
            wsGeneral.GetDakotPremiya(result,
                                   "01/" + document.getElementById("ctl00_KdsContent_ddMonths").value,
                                   document.getElementById("ctl00_KdsContent_ddStatuses").value,
                                   GetDakotPremiyaSucc);
            document.getElementById("ctl00_KdsContent_btnUpdate").disabled = false;
            document.getElementById("ctl00_KdsContent_btnSearch").disabled = false;
        }
    }
    
    function GetOvedNameSucceeded(result) {
       
        if (result == '' || result == "null" || result == null) {
            alert('מספר אישי לא קיים');
            document.getElementById("ctl00_KdsContent_txtName").value = '';
            document.getElementById("ctl00_KdsContent_txtId").select();
            document.getElementById("ctl00_KdsContent_txtPremiaMinutes").value = "";
            document.getElementById("ctl00_KdsContent_btnUpdate").disabled = true;
            document.getElementById("ctl00_KdsContent_btnSearch").disabled = true;
         }
         else{
             document.getElementById("ctl00_KdsContent_txtName").value = result;
             wsGeneral.GetDakotPremiya(document.getElementById("ctl00_KdsContent_txtId").value,
                                   "01/" + document.getElementById("ctl00_KdsContent_ddMonths").value,
                                   document.getElementById("ctl00_KdsContent_ddStatuses").value,
                                   GetDakotPremiyaSucc);
             document.getElementById("ctl00_KdsContent_btnUpdate").disabled = false;
             document.getElementById("ctl00_KdsContent_btnSearch").disabled = false;
         }
    }
    function GetDakotPremiyaSucc(result){
        document.getElementById("ctl00_KdsContent_txtPremiaMinutes").value = result;
    }
    function Onclick_UpdatePremiaMinutes() {
        var dakot = document.getElementById("ctl00_KdsContent_txtPremiaMinutes").value;
        var txt_id = document.getElementById("ctl00_KdsContent_txtId").value;
        if (trim(txt_id) != "") {
            if (!IsNumeric(dakot) || trim(dakot) == "") {
                alert("יש להזין ערך מספרי ושלם בלבד");
                document.getElementById("ctl00_KdsContent_txtPremiaMinutes").focus();
                return false;
            }
        }
        else {
            alert("יש לבחור מספר אישי לעידכון");
            document.getElementById("ctl00_KdsContent_txtPremiaMinutes").focus();
            return false;
        }
   
    }
     
    function continue_click() {
        var rdo = document.getElementById("ctl00_KdsContent_rdoId");
        if (rdo.checked) {

            document.getElementById("ctl00_KdsContent_txtId").disabled = false;
            document.getElementById("ctl00_KdsContent_txtName").disabled = true;
            // document.getElementById("ctl00_KdsContent_txtId").select();
        }
        else {
            document.getElementById("ctl00_KdsContent_txtName").disabled = false;
            document.getElementById("ctl00_KdsContent_txtId").disabled = true;
            //    document.getElementById("ctl00_KdsContent_txtName").select();
        }
    }
//    function onchange_ddStatuses() {
//        document.getElementById("ctl00_KdsContent_ddMonths").selectedIndex = 0;
//        document.getElementById("ctl00_KdsContent_txtId").value = "";
//        document.getElementById("ctl00_KdsContent_txtName").value = "";
//        document.getElementById("ctl00_KdsContent_txtPremiaMinutes").value = "";
//        //document.getElementById("ctl00_KdsContent_divGrdPremyot").style.display = "none";
//        $("#ctl00_KdsContent_divGrdPremyot").css("display", "none");
    //    }


    function onchange_txtDakot(row) {
        //debugger;
        var vld = document.getElementById(row.id + "_vldDakot");
        var dakotNochechut = document.getElementById(row.id + "_txtDakotPremia").value;

        if (!trim(dakotNochechut) == "")
            if (!IsNumeric(dakotNochechut)) {
              //  vld.errormessage = "יש להזין ערך מספרי ושלם בלבד";
                //  ShowValidatorCalloutExtender(row.id + "_vldExtndrDakot");
                alert("יש להזין ערך מספרי ושלם בלבד");
                document.getElementById(row.id + "_txtDakotPremia").focus();
                document.getElementById(row.id + "_txtDakotPremia").value = "";
               
            }
        }

//        function ShowValidatorCalloutExtender(sBehaviorId) {
//            $find(sBehaviorId)._ensureCallout();
//            $find(sBehaviorId).show(true);
//        }
   </script>
</asp:Content>
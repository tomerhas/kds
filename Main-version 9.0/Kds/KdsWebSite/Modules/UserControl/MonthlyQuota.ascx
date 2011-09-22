<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthlyQuota.ascx.cs" Inherits="Modules_UserControl_MonthlyQuota" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />
    <script src="../../Js/ListBoxExtended.js" type="text/javascript"></script>


    <asp:UpdatePanel ID="upFilter" runat="server">
     <ContentTemplate>
     <table>
        <tr>
        <td>
            <fieldset runat="server" id="DemandFieldSet"  > 
                <table class="FilterTable" >
                <tr><td></td>
                    <td id="Td1" width="195px" runat="server" >
                            <asp:RadioButton Checked="true"  ID="rdoDemandsInTreatment" runat="server" GroupName="grpDemandType" Text="בקשות לטיפול" > </asp:RadioButton>
                    </td>
                    <td id="Td2" width="170px" runat="server">
                            <asp:RadioButton ID="rdoTreatedDemands" runat="server" GroupName="grpDemandType" Text="בקשות שטופלו" > </asp:RadioButton></td>
                    <td width="220px"  >
                        חודש:&nbsp;<asp:DropDownList runat="server" ID="ddlMonth" style="width:103px;"></asp:DropDownList>
                    </td>
                    <td align="left"><asp:Button Text="הצג" ID="btnDisplay" runat="server" CssClass="ImgButtonSearch" onclick="btnDisplay_Click" />
                </tr>
                </table>
             </fieldset>
             
        </td>
        </tr>
        <tr><td height="2px"  ></td></tr>
        <tr><td>
        <div ID="DivQuotaDetails" runat="server">
            <fieldset > 
                <table class="FilterTable" >
                    <tr><td> </td>
                        <td width="200px" > &nbsp;סה&quot;כ מכסה חודשית:  &nbsp;
                        <asp:Label BorderStyle="Inset" BorderWidth="1px" runat="server"  ID="txtMonthlyQuota" style="width:60px"  ></asp:Label>
                        </td>
                        <td  width="160px" >
                        מכסה שחולקה:&nbsp;
                        <asp:Label BorderStyle="Inset" BorderWidth="1px" runat="server" ID="txtSharedQuota"  style="width:50px"></asp:Label>
                        </td>
                        <td >
                        יתרה:&nbsp; 
                        <asp:Label BorderStyle="Inset" BorderWidth="1px"  runat="server" ID="txtBalanceQuota"  style="width:50px"  ></asp:Label>
                    </td></tr>
                </table></fieldset>
        </div>
        </td></tr>
        
        <tr><td>
            <fieldset > 
                <table  class="FilterTable">
                    <tr>
                        <td>
                            <asp:Panel ID="PnlSearchWorker" Enabled="false"  runat="server"  >
                                <table><tr>
                                    <td >
                                    <asp:TextBox ID="TxtUserID" runat="server" style="display:none;"></asp:TextBox> 
                                    <asp:TextBox ID="TxtStatusIsuk" runat="server" style="display:none;"></asp:TextBox> 
                                    </td>
                                    <td width="100px" >
                                        <asp:RadioButton runat="server" OnClick="InitTextBox()" Checked="true" ID="rdoId" GroupName="grpSearch" Text="מספר אישי:"  > </asp:RadioButton>&nbsp; 
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtId" runat="server"  dir="rtl" Width="100px" onChange="GetOveNameBydMisparIshi();" ></asp:TextBox>                            
                                        <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" FirstRowSelected="true" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                            TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetWorkersDetailsOfHoursApproval" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                            EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select" 
                                            CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientHidden="onClientHiddenHandler_getID" >                               
                                        </cc1:AutoCompleteExtender>                              
                                    </td>
                                    <td width="12px"></td>                
                                    <td width="60px">
                                         <asp:RadioButton runat="server" OnClick="InitTextBox()" ID="rdoName" GroupName="grpSearch" Text="שם:" > </asp:RadioButton>&nbsp; 
                                    </td>               
                                    <td >
                                        <asp:TextBox ID="txtName" runat="server" style="width:100px" onChange="GetOvedMisparIshiByName();" ></asp:TextBox>
                                        <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                                    TargetControlID="txtName" MinimumPrefixLength="1" FirstRowSelected="true" ServiceMethod="GetWorkersDetailsOfHoursApproval" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientHidden="onClientHiddenHandler_getName">                               
                                        </cc1:AutoCompleteExtender> 
                                    </td>
                                 </tr></table>
                            </asp:Panel>
                        </td>
                        <td align="left">
                            <asp:button ID="btnSearchWorker" runat="server" text="חפש" onclick= "btnSearchWorker_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td></tr></table>
   </ContentTemplate>
 </asp:UpdatePanel>  
 <asp:UpdatePanel ID="upGridIshurim" runat="server"  UpdateMode="Conditional">
     <ContentTemplate>
       <div ID="DivData" runat="server"  style="display:none"><table width="100%">
       <tr><td>
           <asp:GridView ID="grdIshurim" runat="server" AllowPaging="true" 
                AllowSorting="true" AutoGenerateColumns="false" CssClass="Grid" 
                EmptyDataText="לא נמצאו נתונים!" HeaderStyle-CssClass="GridHeader" 
                onDataBound="grdIshurim_RowBound" onrowdatabound="grdIshurim_RowDataBound" 
                OnRowCreated="grdIshurim_RowCreated"
                onpageindexchanging="grdIshurim_PageIndexChanging"
                PageSize="8" SelectedIndex="0">
               <Columns>
                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                       HeaderStyle-Width="100px" HeaderStyle-Wrap="true">
                       <HeaderTemplate>
                           <table align="center">
                               <tr>
                                   <td align="center" colspan="2">מאושר</td>
                               </tr>
                               <tr>
                                   <td>
                                       <asp:LinkButton ID="HplApprovalAll" runat="server" CssClass="GridHeaderLinked"  OnClick="HplApprovalAll_Click"
                                           Text="סמן הכל"></asp:LinkButton>
                                   </td>
                                   <td>
                                       <asp:LinkButton ID="HplApprovalClear" runat="server" CssClass="GridHeaderLinked"  OnClick="HplApprovalClear_Click"
                                           Text="נקה"></asp:LinkButton>
                                   </td>
                               </tr>
                           </table>
                       </HeaderTemplate>
                       <ItemTemplate>
                       <table align="center"><tr><td align="center" colspan="3">
                            <asp:RadioButton ID="rdoApproved" AutoPostBack="true" runat="server" oncheckedchanged="StatusIshur_Click"  GroupName="GrpStatusIshur" />
                       </td></tr></table>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                       HeaderStyle-Width="50px" HeaderStyle-Wrap="true" HeaderText="לא מאושר">
                       <ItemTemplate>
                        <table align="center"><tr><td align="center">
                           <asp:RadioButton ID="rdoNotApproved" AutoPostBack="true" runat="server" oncheckedchanged="StatusIshur_Click"
                               GroupName="GrpStatusIshur" />
                               </td></tr></table>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="kod_status_ishur" ItemStyle-CssClass="ItemRow" />
                   <asp:BoundField DataField="RAMA" ItemStyle-CssClass="ItemRow" />
                   <asp:BoundField DataField="mispar_ishi" HeaderText="מ.א." ItemStyle-CssClass="ItemRow"    />
                   <asp:BoundField DataField="Shem" ItemStyle-Width="110px" HeaderText="שם" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="Agaf" ItemStyle-Width="110px" HeaderText="אגף" ItemStyle-CssClass="ItemRow" />
                   <asp:BoundField DataField="snif_av" ItemStyle-Width="110px" HeaderText="סניף" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="maamad" ItemStyle-Width="110px" HeaderText="מעמד" ItemStyle-CssClass="ItemRow" />
                   <asp:BoundField DataField="isuk" ItemStyle-Width="110px" HeaderText="עיסוק" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="TAARICH" HeaderText="חודש"  ItemStyle-CssClass="ItemRow" DataFormatString="{0:MM/yyyy}"  />
                   <asp:BoundField DataField="Mevukash"  HeaderText="מבוקש" ItemStyle-CssClass="ItemRow"   />
                   <asp:BoundField DataField="siba" HeaderText="סיבה" ItemStyle-CssClass="ItemRow" ItemStyle-Width="140px"  />
                   <asp:BoundField DataField="Meushar_MenahelYashir" HeaderStyle-Width="40px" HeaderStyle-Wrap="true" HeaderText="מאושר מ.ישיר" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="Meushar_Agafit" HeaderStyle-Width="40px" HeaderStyle-Wrap="true" HeaderText="מאושר אגפית" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="UavarLeIshurVaad" HeaderStyle-Width="60px"  HeaderStyle-Wrap="true" HeaderText="הועבר לאישור  ו. פנים" ItemStyle-CssClass="ItemRow"   />
                   <asp:BoundField DataField="UsharAlVaad" HeaderStyle-Width="60px" HeaderStyle-Wrap="true" HeaderText="אושר ע&quot;י ו. פנים" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="Bakasha_ID" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="KOD_ISHUR" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="TAARICH" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="MISPAR_SIDUR" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="SHAT_HATCHALA" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="SHAT_YETZIA" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="MISPAR_KNISA" ItemStyle-CssClass="ItemRow"  />
                   <asp:BoundField DataField="OriginalStatusIshur" ItemStyle-CssClass="ItemRow"  />
               </Columns>
               <AlternatingRowStyle CssClass="GridAltRow" />
               <RowStyle CssClass="GridRow" />
               <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
               <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" />
                <PagerTemplate>
                    <kds:GridViewPager runat="server" ID="ucGridPager" />
                </PagerTemplate>
           </asp:GridView>
        </td></tr></table></div>
   </ContentTemplate> 
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnDisplay" />                                                                    
    </Triggers>               
</asp:UpdatePanel>
        
       <div ID="DivDataQuotaUpdate" runat="server"  style="display:none">
            <table width="100%"   >
            <tr>
                <td id="TdBtnUpdate" runat="server" style="width:59%;">
                <asp:Button Text="בצע" ID="btnUpdate" runat="server" CssClass="ImgButtonSearch" 
                        onclick="btnUpdate_Click" />
                </td>
                <td>
                    <div runat="server" ID="DivQuotaSumAndBalance">
                        <table >
                            <tr>
                                <td>
                                    סה&quot;כ מאושר
                                    <asp:TextBox BorderStyle="Inset" BorderWidth="1px" runat="server" ID="TxtSumMevukash" style="width:40px" ></asp:TextBox>
                                </td>
                                <td align="center" dir="ltr" lang="he">
                                    <asp:TextBox BorderStyle="Inset" BorderWidth="1px" runat="server" ID="TxtQuotaBalanceGrid" style="width:40px" ></asp:TextBox>
                                    יתרה פנויה
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td><asp:TextBox runat="server" ID= "TxtSelectedRow" style="display:none;"></asp:TextBox></td>
                <td><asp:TextBox runat="server" ID="TxtConfirmedValueUnitReturned" style="display:none;"></asp:TextBox></td>
                <td><asp:TextBox runat="server" ID="TxtValueUnitToConfirmReturned" style="display:none;"></asp:TextBox></td>
                <td><asp:TextBox runat="server" ID="TxtConfirmedValueVaadReturned" style="display:none;"></asp:TextBox></td>
                <td><asp:Button runat="server" ID="BtnUpdateRow"  style="display:none" onclick="BtnUpdateRow_Click" /></td>
            </tr>
            </table>
        </div>
    <script type="text/javascript">

        function onClientHiddenHandler_getID(sender, eventArgs) {
            GetOvedName(($get("<%=txtId.ClientID %>")));
        }

        function onClientHiddenHandler_getName(sender, eventArgs) {
            var iMisparIshi, iPos;
            var sOvedName = ($get("<%=txtName.ClientID %>")).value;
            if (sOvedName != '') {
                iPos = sOvedName.indexOf('(');
                if (iPos == -1) {
                }
                else {
                    iMisparIshi = sOvedName.substr(iPos + 1, sOvedName.length - iPos - 2);
                    ($get("<%=txtId.ClientID %>")).value = iMisparIshi;
                    ($get("<%=txtName.ClientID %>")).value = sOvedName.substr(0, iPos - 1);
                }
            }
        }
        function GetOveNameBydMisparIshi() {
            GetOvedName(($get("<%=txtId.ClientID %>")));
        }

        function GetOvedMisparIshiByName() {
            GetOvedMisparIshi(($get("<%=txtName.ClientID %>")));
        }

        function GetOvedMisparSucc(result) {
            if ((result == '') || (result == 'null')) {
                alert('שם לא נמצא');
                ($get("<%=txtName.ClientID %>")).value = '';
                ($get("<%=txtName.ClientID %>")).select();
                EnableButton("btnSearchWorker", false);
            }
            else {
                ($get("<%=txtId.ClientID %>")).value = result;
                EnableButton("btnSearchWorker", true);
            }
        }
        function GetOvedNameSucceeded(result) {
            if ((result == '') || (result == 'null')) {
                alert('מספר אישי לא קיים');
                ($get("<%=txtId.ClientID %>")).value = '';
                ($get("<%=txtId.ClientID %>")).select();
                EnableButton("btnSearchWorker", false);
            }
            else {
                ($get("<%=txtName.ClientID %>")).value = result;
                EnableButton("btnSearchWorker", true);
            }
        }

        function EnableButton(NameOfButton, boolDecision) {
            var but = SetNameOfObject(($get("<%=txtName.ClientID %>")), NameOfButton);
            var objBut = document.getElementById(but);
            objBut.disabled = !(boolDecision);
            objBut.className = boolDecision ? "ImgButtonSearch" : "ImgButtonSearchDisable";
        }

        function DisablePanelQuota(boolDecision) {
            if (boolDecision) {
                ($get("<%=DivQuotaDetails.ClientID %>")).style.display = "none";
                ($get("<%=DivQuotaSumAndBalance.ClientID %>")).style.display = "none";
            }
            else {
                ($get("<%=DivQuotaDetails.ClientID %>")).style.display = "block";
                ($get("<%=DivQuotaSumAndBalance.ClientID %>")).style.display = "block";
            }
            SetAutoCompleteExtender()
        }
        function SetAutoCompleteExtender() {
            var DemandType = ($get("<%=rdoDemandsInTreatment.ClientID %>").checked) ? 1 : 2;
            var MisparIshi = ($get("<%=TxtUserID.ClientID %>")).value;
            var Month = ($get("<%=ddlMonth.ClientID %>")).value;
            var StatusIsuk = ($get("<%=TxtStatusIsuk.ClientID %>")).value;
            var AutoCompleteExtender = DemandType + ',' + MisparIshi + ',' + Month + ',' + StatusIsuk; //+ ',' + WorkerType;
            $find('ctl00_KdsContent_UcMonthlyQuota_AutoCompleteExtenderID').set_contextKey('mispar_ishi,' + AutoCompleteExtender);
            $find('ctl00_KdsContent_UcMonthlyQuota_AutoCompleteExtenderByName').set_contextKey('Shem,' + AutoCompleteExtender);
        }

        function InitTextBox() {
            ($get("<%=txtId.ClientID %>")).value = '';
            ($get("<%=txtName.ClientID %>")).value = '';
            var FilterKod = ($get("<%=rdoId.ClientID %>")).checked;
            if (FilterKod == true) {
                ($get("<%=txtId.ClientID %>")).disabled = false;
                ($get("<%=txtName.ClientID %>")).disabled = true;
            }
            else {
                ($get("<%=txtId.ClientID %>")).disabled = true;
                ($get("<%=txtName.ClientID %>")).disabled = false;
            }
        }
        function OpenDialogBakashatTashlum(Mispar_Ishi, BakashaId, Month) {
            var ddlMonth = "01/" + Month; // document.getElementById("ctl00_KdsContent_ddlMonth").value;
            var OpenWin = window.showModalDialog("BakashatTashlum.aspx?Tzuga=1&MisparIshi=" + Mispar_Ishi +
                    "&Editable=1&Taarich=" + ddlMonth + "&BakashaId=" + BakashaId
                    , "bakasha", "dialogHeight:320px;dialogWidth:450px;status:no;scroll:no;");

        }

        function OpenDialogApprovalMonthlyQuota(Level, RowId, Mispar_Ishi, QuotaDemand, ConfirmedValueUnit, UnitValueMovedToVaad, ConfirmedValueVaad, Unit, Month) {
            ($get("<%=TxtSelectedRow.ClientID %>")).value = RowId;
            var ConfirmedValueUnitReturned = ($get("<%=TxtConfirmedValueUnitReturned.ClientID %>")) ;
            var ValueUnitToConfirmReturned = ($get("<%=TxtValueUnitToConfirmReturned.ClientID %>")) ;
            var ConfirmedValueVaadReturned = ($get("<%=TxtConfirmedValueVaadReturned.ClientID %>"));
            var now = new Date();

            var Balance = ($get("<%=TxtQuotaBalanceGrid.ClientID %>")).value;
            var Returnvalue = window.showModalDialog("ApprovalMonthlyQuota.aspx?MisparIshi=" + Mispar_Ishi +
                        "&Level=" + Level + "&QuotaDemand=" + QuotaDemand +
                        "&ConfirmedValueUnit=" + ConfirmedValueUnit + "&UnitValueMovedToVaad=" + escape(UnitValueMovedToVaad) +
                        "&ConfirmedValueVaad=" + ConfirmedValueVaad + "&Balance=" + Balance +
                        "&Unit=" + escape(Unit) + "&Month=" + Month + "&Date=" + now 
                        , "Approval", "dialogHeight:250px;dialogWidth:450px;status:no;scroll:no;");
            if (Returnvalue != '-1') {
                ConfirmedValueUnitReturned.value = Returnvalue.split(",")[0];
                ValueUnitToConfirmReturned.value = Returnvalue.split(",")[1];
                ConfirmedValueVaadReturned.value = Returnvalue.split(",")[2];
                var BtnUpdateRow = ($get("<%=BtnUpdateRow.ClientID %>")) ;
                BtnUpdateRow.click();
            }
        }
    </script>
 
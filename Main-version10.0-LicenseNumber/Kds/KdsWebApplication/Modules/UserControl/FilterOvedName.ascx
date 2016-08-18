<%@ Control Language="C#" AutoEventWireup="true" Inherits="Modules_UserControl_FilterOvedName" Codebehind="FilterOvedName.ascx.cs" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />
 
<asp:UpdatePanel ID="upId" runat="server" RenderMode="Inline" UpdateMode="Always" >
    <ContentTemplate> 
            <fieldset class="FilterFieldSet"> 
                    <legend style="background-color:White" dir="rtl"> סינון ע"פ שם </legend>      
                    <table class="FilterTable">
                        <tr>
                            <td class="InternalLabel" style="width:80px">
                                <asp:RadioButton runat="server" Checked="true" ID="rdoId" OnClick="InitTextBox()"  GroupName="grpSearch" Text="מספר אישי"  > </asp:RadioButton>
                            </td>
                            <td>
                                       <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" dir="rtl" ></asp:TextBox>                            
                                          <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                            TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                            EnableCaching="true"  CompletionListCssClass="ACLst"
                                            CompletionListHighlightedItemCssClass="ACLstItmSel"
                                            CompletionListItemCssClass="ACLstItmE" OnClientHidden="onClientHiddenHandler_getID" >                               
                                        </cc1:AutoCompleteExtender>                              
                                       <br /> 
                            </td>                
                            <td class="InternalLabel" style="width:80px">
                                 <asp:RadioButton runat="server" ID="rdoName" OnClick="InitTextBox()"  GroupName="grpSearch" Text="שם" > </asp:RadioButton>
                            </td>               
                            <td style="width:200px">
                               <asp:TextBox ID="txtName" runat="server" AutoComplete="Off" style="width:230px"  ></asp:TextBox>
                                <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                            TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                            EnableCaching="true"  CompletionListCssClass="ACLst"
                                            CompletionListHighlightedItemCssClass="ACLstItmSel"
                                            CompletionListItemCssClass="ACLstItmE" OnClientHidden="onClientHiddenHandler_getName" >                               
                                </cc1:AutoCompleteExtender> 
                              </td>
                            <td width="400px">
                                        <asp:CompareValidator ID="vldId" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="הערך בשדה מספר אישי לא תקין!" Type="Integer"  Operator="DataTypeCheck"  ControlToValidate="txtId"></asp:CompareValidator>                
                                <asp:CompareValidator ID="vldName" runat="server" Display="Static" CssClass="ErrorMessage" ErrorMessage="הערך בשדה שם לא תקין!" Type="String"   Operator="DataTypeCheck"  ControlToValidate="txtName"></asp:CompareValidator>
                            </td>
                         </tr>     
                    </table>
               </fieldset>
    </ContentTemplate>
 </asp:UpdatePanel>
  <script language="javascript" type="text/javascript">
       var txtId;
       var txtName;
       var FilterKod;
//    window.onload = function Load()
//    {
//        debugger

//        txtName = $get("<%=txtName.ClientID %>");
//        FilterKod = $get("<%=rdoId.ClientID %>"); 
//    }
    function onClientHiddenHandler_getID(sender, eventArgs) {
        txtId = $get("<%=txtId.ClientID %>");
        GetOvedName(txtId);
    }
    
    function onClientHiddenHandler_getName(sender, eventArgs)
    {
     var iMisparIshi, iPos;
     var sOvedName = txtName.value;
     txtId = $get("<%=txtId.ClientID %>");
     txtName = $get("<%=txtName.ClientID %>");
     if (sOvedName != '')
      {  
         iPos = sOvedName.indexOf('(');
         if (iPos !=-1)
         {
            iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
            txtId.value = iMisparIshi;
            txtName.value = sOvedName.substr(0, iPos - 1);
            }
       }
   }

   function GetOvedNameSucceeded(result) {
       txtId = $get("<%=txtId.ClientID %>");
       txtName = $get("<%=txtName.ClientID %>");
       if (result == '') {
           alert('מספר אישי לא קיים');
           txtId.select();
       }
       else {
           txtName.value = result;
       }
   }

   function InitTextBox() {
       txtId = $get("<%=txtId.ClientID %>");
       txtName = $get("<%=txtName.ClientID %>");
       txtId.value = '';
       txtName.value = '';
       var Filter = FilterKod.checked;
       txtId.disabled = !Filter;
       txtName.disabled = Filter;
   }

 </script>
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Modules_Batches_CreateConstantReports" Codebehind="CreateConstantReports.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../Js/GeneralFunction.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="KdsContent" Runat="Server">
   <br />
 <table width="340px" cellpadding="6" border="0">
    <tr>
          <td align="right">
           חודש: &nbsp; 
          <asp:DropDownList ID="ddlToMonth"  runat="server" Width="100px"> </asp:DropDownList>
         </td>
     </tr>
  
    <tr>
         <td  align="right">
           תאור: &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server"  CssClass="ErrorMessage"  ErrorMessage="חובה להגדיר תיאור!" ControlToValidate="txtDescription"></asp:RequiredFieldValidator><br />
         <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" TextMode="MultiLine" Width="320" Height="100px"></asp:TextBox>
       </td>
    </tr>
      <tr>
        <td align="left"> 
            <asp:UpdatePanel ID="upButton" runat="server"  UpdateMode="Conditional">
                <ContentTemplate>
                 <asp:Button Text="הפעל" ID="btnRun" runat="server" CssClass="ImgButtonSearch" 
                        onclick="btnRun_Click"  /> 
                </ContentTemplate>
            </asp:UpdatePanel>
         </td>
    </tr>
 </table>
<asp:UpdatePanel ID="upMessage" runat="server">
  <ContentTemplate>
     <asp:Button  ID="btnShowMessage" runat="server" onclick="btnShowMessage_Click"  style="display:none;"/> 
        <cc1:ModalPopupExtender ID="ModalPopupEx" OkControlID="btnShowMessage" CancelControlID="btnConfirm" 
                DropShadow="false" X="400" Y="200" PopupControlID="paMessage" TargetControlID="btnShowMessage"  runat="server" >
        </cc1:ModalPopupExtender>
       <asp:Panel runat="server" style="display:none"   ID="paMessage" CssClass="PanelMessage"  >
        <asp:Label ID="lblHeaderMessage" runat ="server" Width="97%" BackColor="#696969"></asp:Label>
        <br /><br />
        <asp:Label ID="lblMessage" runat ="server" Width="90%"></asp:Label>
       <br />  <br />
        <asp:Button ID="btnConfirm" runat="server" Text="אישור" CssClass="ImgButtonMake" onclick="btnConfirm_Click"/>
     </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
 <script language="javascript" type="text/javascript">


     function RunReports(iRequestId, month, iUserId) {
     //    alert(iRequestId);
         wsBatch.CreateReprts(iRequestId, month, iUserId); //,RunErrorsSucceeded );
      }

//     function RunErrorsSucceeded(result) {
//         if (result == 'OK') {
//             alert('הסתיים בהצלחה');
//         }
//     }
 </script> 
</asp:Content>
﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RunErrorsBatch.aspx.cs" Inherits="Modules_Batches_RunErrorsBatch" Title="הרצה גורפת לזיהוי שגויים" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../Js/GeneralFunction.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
   <br />
 <table width="340px" cellpadding="6" border="0">
    <tr>
         <td></td>
     </tr>
  
    <tr>
         <td  align="right">
           תאור: &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server"  CssClass="ErrorMessage"  ErrorMessage="חובה להגדיר תיאור לריצה!" ControlToValidate="txtDescription"></asp:RequiredFieldValidator><br />
         <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" TextMode="MultiLine" Width="320" Height="100px"></asp:TextBox>
       </td>
    </tr>
      <tr>
        <td align="left"> 
            <asp:UpdatePanel ID="upButton" runat="server"  UpdateMode="Conditional">
                <ContentTemplate>
                 <asp:Button Text="הפעל ריצה גורפת" ID="btnRun" runat="server" Width="134px" CssClass="ImgButtonSearch" 
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


     function RunErrors(iRequestId) 
     {
         wsBatch.InputDataAndErrors(iRequestId); //,RunErrorsSucceeded );
      }

//     function RunErrorsSucceeded(result) {
//         if (result == 'OK') {
//             alert('הסתיים בהצלחה');
//         }
//     }
 </script> 
</asp:Content>


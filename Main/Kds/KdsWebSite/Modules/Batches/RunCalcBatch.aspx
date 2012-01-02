<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RunCalcBatch.aspx.cs" Inherits="Modules_Batches_RunCalcBatch" Title="הרצת חישוב" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">   
    <br />
 <table width="420px" cellpadding="6" border="0">
    <tr>
         <td colspan="4"></td>
     </tr>
    <tr>
        <td>מעמד:</td>
        <td></td>
        <td style="width:70px"><asp:CheckBox ID="chkFriends" runat="server"  Checked="true"   /> &nbsp; חברים</td>
        <td style="width:70px"><asp:CheckBox ID="chkSalarieds" runat="server" Checked="true"  />  &nbsp; שכירים </td>
        <td  colspan="2"><asp:CustomValidator ID="vldStatus" CssClass="ErrorMessage" runat="server" Display="Dynamic" ErrorMessage="חובה לבחור לפחות מעמד אחד!" ClientValidationFunction="CheckStatus"></asp:CustomValidator></td>
    </tr>
     <tr>
        <td colspan="2" valign="top">עד חודש:</td>
        <td colspan="2"><asp:DropDownList ID="ddlToMonth"  runat="server" Width="100px">
            </asp:DropDownList><asp:RequiredFieldValidator ID="vldMonth" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="חובה לבחור חודש!" ControlToValidate="ddlToMonth"></asp:RequiredFieldValidator>
        </td>
        <td colspan="2"></td>
    </tr>
      <tr>
       <td colspan="3"><asp:RadioButton ID="chkTest" runat="server" Checked="true" GroupName="rdoTest" /> &nbsp; ריצת טסט</td>
       
        <td colspan="2"><asp:RadioButton ID="chkNoTest" runat="server" GroupName="rdoTest" /> &nbsp;  ריצה בפועל</td>
        <td ></td>
    </tr>
 <tr>
         <td colspan="6" align="right">
         תאור: &nbsp; <asp:RequiredFieldValidator ID="vldDescription" Display="Dynamic" CssClass="ErrorMessage"  runat="server"     ErrorMessage="חובה להגדיר תיאור לריצה!" ControlToValidate="txtDescription"></asp:RequiredFieldValidator><br />
         <asp:TextBox ID="txtDescription" runat="server"  MaxLength="100"   TextMode="MultiLine" Width="320" Height="100px" ></asp:TextBox></td>
     </tr>
      <tr>
        <td colspan="3"><asp:CheckBox ID="chkRunAll" runat="server"  />&nbsp; ריצה גורפת</td>
         <td></td>
         <td align="left"> 
            <asp:UpdatePanel ID="upButton" runat="server">
                <ContentTemplate>
                 <asp:Button Text="הפעל" ID="btnRun" runat="server" CssClass="ImgButtonSearch" 
                        onclick="btnRun_Click"  /> 
                  
                </ContentTemplate>
            </asp:UpdatePanel>
         </td>
         <td style="width:65px">
         
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
 function  CheckStatus(oSrc, args)
 {
  if (!document.getElementById("ctl00_KdsContent_chkFriends").checked && !document.getElementById("ctl00_KdsContent_chkSalarieds").checked)
  {args.IsValid = false;}
  else { args.IsValid = true; }
}

function RunCalc(iRequestId, dChodesh, sMaamad, bRitzatTest, bRitzaGoreft) {
    wsBatch.CalcBatchParallel(iRequestId, dChodesh, sMaamad, bRitzatTest, bRitzaGoreft); //, RunCalcSucceeded);
}






//function RunCalcSucceeded(result)
// {
//    if (result == 'OK')
//     {
//        alert('הסתיים בהצלחה');
//    }
//}
 </script> 
 
</asp:Content>


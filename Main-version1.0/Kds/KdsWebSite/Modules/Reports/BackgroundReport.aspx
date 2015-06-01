<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackgroundReport.aspx.cs" Inherits="Modules_Reports_BackgroundReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <base target="_self" />
    <title runat="server" id="TitleWindow">פרטי הפקת הדו"ח</title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
    <link type='text/css' href='../../css/basic.css' rel='stylesheet' media='screen' />
    <script type="text/javascript" src="../../Js/GeneralFunction.js"></script>
    </head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true">
     <Services>
     <asp:ServiceReference Path="~/Modules/WebServices/wsBatch.asmx" />
     </Services>
     </asp:ScriptManager>        
   
    <div>
    <asp:UpdatePanel ID="upMessage" runat="server" UpdateMode="Always">
          <ContentTemplate>
    <table dir="rtl" border="0" class="InternalLabel">
    <tr><td style="width:30px" rowspan="4"></td>
        <td colspan="2" align="center">הדו&quot;ח יופק ברקע ויישמר במערכת לצפייה .בהמשך תתקבל הודעה בסיום הפקת הדו&quot;ח</td>
        <td style="width:30px" rowspan="4"></td>
    </tr>
    <tr><td colspan="2" align="right" style="text-decoration:underline">פרטי הפקה</td></tr>
    <tr><td  colspan="2" align="right">
        <table border="0">
            <tr>
            <td rowspan="3"></td>
                <td style="width:20%"><asp:Label ID="Label1" runat="server" Text="תיאור הדו&quot;ח:" />  </td>
                <td  style="width:80%"><asp:TextBox runat="server" ID="TxtDescription" Width="95%" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="סוג קובץ" />  </td>
                <td><asp:RadioButton runat="server" Width="100px" ID="rdPdfType" GroupName="FileType" Text="Pdf" Checked="true"    />
                    <asp:RadioButton runat="server" Width="100px" ID="rdExcelType" GroupName="FileType" Text="Excel-אקסל"   />
                </td>
            </tr>
            <tr> 
            <td colspan="3" >
                <div runat="server" id="DivSendMail" colspan="3" style="display:none;width:100%">
                    <asp:CheckBox ID="CkSendInEmail" runat="server" Text="העתק לדואר אלקטרוני" /> 
                </div> 
            </td>
            </tr>
        </table>
    </td></tr>
        <tr>
            <td align="center" height="30px">
                 <asp:Button Text="הפק" ID="btnSend" runat="server" CssClass="ImgButtonSearch" onclick="btnSend_Click" CausesValidation="false"/> 
            </td>
            <td align="center"  height="30px">
                 <asp:Button Text="בטל"  ID="btnCancel" OnClientClick="window.close();" runat="server" CssClass="ImgButtonSearch"  /> 
            </td>
        </tr>
    </table>
     <asp:Button  ID="btnShowMessage" runat="server"  style="display:none;" CausesValidation="false"/> 
        <cc1:ModalPopupExtender ID="ModalPopupEx" DropShadow="false" OkControlID="btnShowMessage"  CancelControlID="btnConfirm" 
                PopupControlID="paMessage" TargetControlID="btnShowMessage"  runat="server" >
        </cc1:ModalPopupExtender>
       <asp:Panel runat="server" style="display:none"   ID="paMessage" CssClass="PanelMessage"  >
        <asp:Label ID="lblHeaderMessage" runat ="server" Width="97%" BackColor="#696969"></asp:Label>
        <br /><br />
        <asp:Label ID="lblMessage" runat ="server" Width="90%"></asp:Label>
       <br />  <br />
        <asp:Button ID="btnConfirm" runat="server" Text="אישור" CssClass="ImgButtonMake" OnClientClick="window.close();"/>
     </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
        <script type="text/javascript" language="javascript">
            function RunReports(iRequestId) {
                wsBatch.CreateHeavyReports(iRequestId);
            }
        </script>
    </div>
    </form>
</body>
</html>
<script language="javascript"  type="text/javascript" >

    document.onkeydown = KeyCheck;

    function KeyCheck() {
        var KeyID = event.keyCode;
        if (KeyID ==13){ //Enter              
                event.returnValue = false;
                event.cancel = true;
        }
    }     

</script>


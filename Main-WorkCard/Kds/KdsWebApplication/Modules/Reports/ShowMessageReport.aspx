<%@ Page Language="C#" AutoEventWireup="true" Inherits="Modules_Reports_ShowMessageReport" Codebehind="ShowMessageReport.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link type='text/css' href='../../css/basic.css' rel='stylesheet' media='screen' />

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <base target="_self" />
    <title runat="server" id="TitleWindow">הודעה על דוחו''ת מוכנים</title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../../Js/GeneralFunction.js"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <table dir="rtl" border="0" class="InternalLabel">
    <tr><td style="width:30px" rowspan="4"></td>
        <td colspan="2" align="center">קיימים דוחו''ת שביקשת להפיק, כדי לפתוח אותם לחץ על פתח</td>
        <td style="width:30px" rowspan="4"></td>
    </tr>
        <tr>
            <td align="center" height="30px">
              <br />
               <asp:Button Text="פתח" ID="btnSend" runat="server" CssClass="ImgButtonSearch" OnClientClick="btnOpen_Click();" /> 
            </td>
            <td align="center"  height="30px">
             <br />
                 <asp:Button Text="בטל"  ID="btnCancel" OnClientClick="window_onunload();" runat="server" CssClass="ImgButtonSearch" /> 
            </td>
        </tr>
    </table>
    <br />
    </div>
    </form>
</body>
</html>
<script language="javascript">
    function window_onunload() {
        window.returnValue = "close";
        window.close();
    }

    function btnOpen_Click() {
        window.returnValue = "open";
        window.close();
    }
</script>


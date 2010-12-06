<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowReport.aspx.cs" Inherits="Modules_Reports_ShowReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title runat="server" id="TitleWindow">דו"חות</title>
    <link id="Link1" runat="server" href="../../StyleSheet.css" type="text/css" rel="stylesheet" />
    </head>
<body dir="rtl">
    <form id="form1" runat="server" style="margin:0px">
        <table width="100%"  border="1" cellpadding="0" cellspacing="0" >
            <tr>
               <td width="20px" ></td>
                <td id="ErrorMsg" class="ErrorMessage" runat="server" style="display:block"/>
                <td width="20px" ></td>
            </tr>
            <tr>
                <td colspan="3"  align="center" >
                    <rsweb:ReportViewer ID="RptViewer" runat="server" Height="680px"  />
                </td>
            </tr>
        </table>
        <div style="left:40px;top:10px;position:absolute; z-index:1000">
          <asp:Button ID="CloseWindow" CssClass="ImgButtonSearch" runat="server" Text="סגור" OnClientClick="window.close()" />
        </div>
    </form>

</body>
</html>

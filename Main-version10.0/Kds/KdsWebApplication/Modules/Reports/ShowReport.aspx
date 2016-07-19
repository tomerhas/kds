<%@ Page Language="C#" AutoEventWireup="true" Inherits="Modules_Reports_ShowReport" Codebehind="ShowReport.aspx.cs" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb"  %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
--%>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <base target="_self" />
    <title runat="server" id="TitleWindow">דו"חות</title>
    <link id="Link1" runat="server" href="../../StyleSheet.css" type="text/css" rel="stylesheet" />

     <style type="text/css">
         .auto-style1 {
             height: 148px;
         }
         .auto-style2 {
             color: Red;
             font-size: 12px;
             height: 148px;
         }
     </style>

    </head>
<body dir="rtl">
    <form id="form1" runat="server" style="margin:0px">
    

        <table width="100%"  border="1" cellpadding="0" cellspacing="0" >
            <tr>
               <td width="20px" class="auto-style1" ></td>
                <td id="ErrorMsg" class="auto-style2" runat="server" style="display:block"/>
                <td width="20px" class="auto-style1" ></td>
            </tr>
            <tr>
                <td colspan="3"  align="center" >
<%--                    <rsweb:ReportViewer ID="RptViewer" runat="server" Height="680px"  />--%>
                   
                    <rsweb:ReportViewer ID="RptViewer" runat="server" Height="680px" InteractivityPostBackMode="SynchronousOnDrillthrough" ClientIDMode="Static">
                    </rsweb:ReportViewer>
                    
                </td>
            </tr>
        </table>
        <div style="left:40px;top:10px;position:absolute; z-index:1000">
        <asp:Label ID="lblRsVersion" runat="server" />
          <asp:Button ID="CloseWindow" CssClass="ImgButtonSearch" runat="server" Text="סגור" OnClientClick="window.close()" />
            <asp:scriptmanager id="ScriptManager1" runat="server" enablepagemethods="true" />
        </div>
    </form>

</body>
</html>

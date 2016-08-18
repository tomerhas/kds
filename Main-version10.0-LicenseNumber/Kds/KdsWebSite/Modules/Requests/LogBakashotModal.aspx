<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogBakashotModal.aspx.cs" Inherits="Modules_Requests_LogBakashotModal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucLogBakashot.ascx" TagName="ucLogBakashot"  %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
 <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
      <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>

    <title>לוג בקשות</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true"   >        
       <Services >
            <asp:ServiceReference  Path="~/Modules/WebServices/wsGeneral.asmx" />
       </Services>
   </asp:ScriptManager>
    <div style="width:990px;text-align:center"  >
      <uc:ucLogBakashot runat="server" ID="ucLogBakashot"></uc:ucLogBakashot>
    </div>
    </form>
</body>
</html>

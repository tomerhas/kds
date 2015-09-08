<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Modules_Errors_test" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox id="a" runat="server"></asp:TextBox>
        <asp:TextBox ID="b" runat="server"></asp:TextBox>
    </div>
        <cc1:AutoCompleteExtender  ID="AutoCompleteExtender1" runat="server">
        </cc1:AutoCompleteExtender>

    </form>
</body>
</html>

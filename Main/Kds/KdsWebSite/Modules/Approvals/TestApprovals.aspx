<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestApprovals.aspx.cs" Inherits="Modules_Approvals_Test" %>
<%@ Register Namespace="Egged.WebCustomControls" Assembly="Egged.WebCustomControls" TagPrefix="wccEgged" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnApproval" Text="Test Approval" />
        <asp:Button runat="server" ID="btnMail" Text="Test Mails" />
        
        <table>
            <tr>
                <td>
                    Mispar Ishi
                </td>
                <td>
                    Kod Ishur
                </td>
                <td>
                    Taarich
                </td>
                <td>
                    Mispar Sidur
                </td>
                <td>
                    Shat Hatchala
                </td>
                <td>
                    Shat Yetzia
                </td>
                <td>
                    Mispar Knisa
                </td>
              
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtMisparIshi" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtKodIshur" runat="server"></asp:TextBox>
                </td>
                <td>
                    <wccEgged:wccCalendar runat="server" BasePath="../../EggedFramework" ID="calTaarich"></wccEgged:wccCalendar>
                </td>
                <td>
                    <asp:TextBox ID="txtMisparSidur" runat="server"></asp:TextBox>
                </td>
                <td>
                    <wccEgged:wccCalendar runat="server" ID="calShatHatchala" BasePath="../../EggedFramework"></wccEgged:wccCalendar>
                </td>
                <td>
                    <wccEgged:wccCalendar runat="server" ID="calShatYetzia" BasePath="../../EggedFramework"></wccEgged:wccCalendar>
                </td>
                <td>
                    <asp:TextBox ID="txtMisparKnisa" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnHR" Text="Measher Rashi" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtGoremRashi"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Kod Tafkid
                </td>
                <td>
                    <asp:Label runat="server" ID="lblKodTafkid"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Kod Sug Ishur
                </td>
                <td>
                    <asp:Label runat="server" ID="lblSugIshur"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Meakev tashlum
                </td>
                <td>
                    <asp:Label runat="server" ID="lblMeakevTashlum"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Sug Peilut
                </td>
                <td>
                    <asp:Label runat="server" ID="lblSugPeilut"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel runat="server" GroupingText="Kod Natun Oved">
                        <asp:Table runat="server" ID="tblKodNatun">
                        </asp:Table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="HR SP Input">
                        <asp:Table runat="server" ID="tblHrInput">
                        </asp:Table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

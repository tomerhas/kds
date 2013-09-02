<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PresenceReport.aspx.cs" Inherits="Modules_Reports_PresenceReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
     <iframe id="frmPrint" src="../../ShowPrint.aspx" height="450px" width="98%"></iframe>
     
     
</asp:Content>

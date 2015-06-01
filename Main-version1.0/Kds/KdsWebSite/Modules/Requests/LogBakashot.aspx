<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LogBakashot.aspx.cs" Inherits="Modules_Requests_LogBakashot" %>

<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucLogBakashot.ascx" TagName="ucLogBakashot"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
  <uc:ucLogBakashot runat="server" ID="ucLogBakashot"></uc:ucLogBakashot>
</asp:Content>


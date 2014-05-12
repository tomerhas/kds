<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Modules_Requests_LogBakashot" Codebehind="LogBakashot.aspx.cs" %>

<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucLogBakashot.ascx" TagName="ucLogBakashot"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
  <uc:ucLogBakashot runat="server" ID="ucLogBakashot"></uc:ucLogBakashot>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Modules_Ovdim_MonthlyQuotaAgafit" Codebehind="MonthlyQuotaAgafit.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc" TagName="MonthlyQuota" Src="~/Modules/UserControl/MonthlyQuota.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <script src="../../Js/SystemManager.js" type="text/javascript"></script>
    
<script src='../../js/jquery.js' type='text/javascript'></script>
<script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
    <asp:UpdatePanel ID="upFilter" runat="server">
        <ContentTemplate>
            <table>
                <tr><td>
                    <uc:MonthlyQuota ID="UcMonthlyQuota" runat="server" />
                </td></tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>  
 </asp:Content>
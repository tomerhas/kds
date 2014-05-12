<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_GridViewPager" Codebehind="GridViewPager.ascx.cs" %>
<asp:Label runat="server" ID="lblGoto" Text="עבור לדף " />
<asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true">
</asp:DropDownList>
<asp:Button Text="ראשון" CssClass="PagerButton" CommandName="Page" CommandArgument="First" runat="server"
     ID="btnFirst" />
<asp:Button Text="קודם" CssClass="PagerButton" CommandName="Page" CommandArgument="Prev" runat="server"
     ID="btnPrevious" />
<asp:Button Text="הבא" CssClass="PagerButton" CommandName="Page" CommandArgument="Next" runat="server"
     ID="btnNext" />
<asp:Button Text="אחרון" CssClass="PagerButton" CommandName="Page" CommandArgument="Last" runat="server"
     ID="btnLast" />
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ReportsList.aspx.cs" Inherits="Modules_Reports_ReportsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" runat="Server">
    <div id="divNetunim" runat="server" style="text-align: right;">
        <br />
        <div runat="server" id="pnlgrdReports" style="height: 350px; width: 435px; overflow-y:scroll;
            direction: ltr;">
            <asp:GridView ID="grdReports" runat="server" GridLines="None" ShowHeader="true" CssClass="Grid"
                AllowPaging="false" AutoGenerateColumns="false" AllowSorting="true" Width="418px"
                EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader" OnRowDataBound="grdReports_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="SHEM_DOCH_BAKOD" />
                    <asp:HyperLinkField DataTextField="TEUR_DOCH" NavigateUrl="#" HeaderStyle-HorizontalAlign="Right"
                        HeaderText="שם דו''ח" ItemStyle-Width="500px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" />
                </Columns>
                <AlternatingRowStyle CssClass="GridAltRow" />
                <RowStyle CssClass="GridRow" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <EmptyDataRowStyle CssClass="GridEmptyData" Height="20px" Wrap="False" />
                
            </asp:GridView>
        </div>
    </div>
    <br />

    <script language="javascript" type="text/javascript">
   
    </script>

</asp:Content>

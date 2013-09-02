<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataEntryNavigation.aspx.cs" Inherits="SystemManager_DataEntryNavigation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
 
 <div id="divNetunim" runat="server" style="text-align: right;">
       
        
            <asp:GridView ID="grdNavigation" runat="server" GridLines="None" ShowHeader="true" CssClass="Grid"
                AllowPaging="false" AutoGenerateColumns="false" AllowSorting="true" Width="300px"
                EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader" 
                OnRowDataBound="grdNavigation_RowDataBound">
                <Columns>
                    <asp:HyperLinkField DataTextField="TableTitle" 
                        HeaderStyle-HorizontalAlign="Right" Target="_self"
                        HeaderText="טבלה" ItemStyle-Width="300px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" />
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTableName" 
                            Text=<%# Eval("TableName") %>></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="GridAltRow" />
                <RowStyle CssClass="GridRow" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <EmptyDataRowStyle CssClass="GridEmptyData" Height="20px" Wrap="False" />
                
            </asp:GridView>
 </div>      
    
</asp:Content>


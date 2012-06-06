<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPeiluyot.ascx.cs" Inherits="Modules_UserControl_ucPeiluyot" %>
<link href="../StyleSheet.css" type="text/css" rel="stylesheet" />  

 
<div runat="server" ID="divPeiluyot" style="Height:350px;Width:867px;overflow:auto;direction:rtl;">
     <asp:GridView ID="grdPeiluyot" runat="server"  GridLines="None"  ShowHeader="true"
           CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="true"
           Width="550px" 
           EmptyDataRowStyle-CssClass="GridHeader"   >
        <Columns>
            <asp:BoundField DataField="kisuy_tor" HeaderText="כיסוי תור" SortExpression="kisuy_tor" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="115px"  />
            <asp:BoundField DataField="shat_yetzia" HeaderText="שעת יציאה" SortExpression="shat_yetzia" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="115px"  DataFormatString="{0:HH:MM}" HtmlEncodeFormatString="true"/>                    
        </Columns> 
        <RowStyle CssClass="GridAltRow"   />
     </asp:GridView>
</div>
     
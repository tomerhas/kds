<%@ Control Language="C#" AutoEventWireup="true" Inherits="Modules_UserControl_StatusOved" Codebehind="StatusOved.ascx.cs" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />
 
 <asp:UpdatePanel ID="UpStatus" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
     <ContentTemplate>
         <div id="divNetunim" runat="server"  style="text-align:right;width:945px">
          <br />
            <span class="TitleLable">פרטי העובד:</span><br />
           
           <table width="945px"  class="Grid" cellpadding="1" cellspacing="0">
                <tr  class="GridHeader" >
                    <td class="ItemRow">מספר אישי</td>
                    <td class="ItemRow">שם משפחה</td>
                    <td class="ItemRow">שם פרטי</td>
                    <td class="ItemRow" style="width:80px;">מעמד</td>
                     <td class="ItemRow" style="width:180px;">עיסוק</td>
                     <td class="ItemRow" style="width:180px;">סניף/יחידה</td>
                    <td class="ItemRow" style="width:60px;">אזור</td>
                    <td class="ItemRow">קוד גיל</td>
                    <td class="ItemRow" style="width:110px;">ת. תחילת עבודה</td>
                  
                </tr>
                <tr class="GridAltRow">
                    <td class="ItemRow"><asp:Label ID="lblEmployeId" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblLastName" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                   <td class="ItemRow"><asp:Label ID="lblMaamad" runat="server"></asp:Label></td>
                     <td class="ItemRow"><asp:Label ID="lblIsuk" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblSnif" runat="server"></asp:Label></td>
                     <td class="ItemRow"> <asp:Label ID="lblEzor" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblGil" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblDateStartWork" runat="server"></asp:Label></td>
                </tr>
            </table>
            <br />
              <span class="TitleLable">סטטוס:</span><br />
          
                <div runat="server" ID="pnlgrdStatus" style="Height:120px;Width:567px;overflow:auto;direction:rtl;">
                    <asp:GridView ID="grdStatus" runat="server"  GridLines="None"  ShowHeader="true"
                           CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="true"
                           Width="550px"   EmptyDataText="לא נמצאו נתונים!" 
                        EmptyDataRowStyle-CssClass="GridHeader" onsorting="grdStatus_Sorting"  onrowdatabound="grdStatus_RowDataBound">
                        <Columns>
                             <asp:BoundField DataField="TAARICH_HATCHALA" HeaderText="מתאריך" SortExpression="TAARICH_HATCHALA" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="115px"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                            <asp:BoundField DataField="TAARICH_SIYUM" HeaderText="עד תאריך" SortExpression="TAARICH_SIYUM" ItemStyle-Width="115px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                            <asp:BoundField DataField="TEUR_STATUS" HeaderText="תאור  סטטוס" SortExpression="TEUR_STATUS" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         </Columns> 
                        <RowStyle CssClass="GridAltRow"   />
                     </asp:GridView>
                 </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel> 
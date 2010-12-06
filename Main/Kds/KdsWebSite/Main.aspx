<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">   
    <table width="95%" cellspacing="8px" border="0">
        <tr>
           <td colspan="2" class="SubTitleLabel"><asp:Label id="lblHellowUser" runat="server"></asp:Label></td>
        </tr>
          <tr>
            <td colspan="2" class="opening_head">ברוך הבא למערכת קדם שכר  - מערכת נוכחות ופעילויות</td>
        </tr>
        <tr>
            <td class="opening_date" valign="top">
                <table>
                    <tr><td style="color:White;font-size:17px;">תאריך</td></tr>
                     <tr><td style="padding-top:4px"><asp:Label id="lblDate" runat="server"></asp:Label></td></tr>
                </table>
            </td>
            <td rowspan="2"  valign="top">
               <table class="opening-quickmenu">
                    <tr >
                    <td style="color:White;font-size:17px;">תפריט ניווט מהיר</td></tr>
                     <tr>
                        <td>
                            <table cellspacing="4px" border="0" cellpadding="1">
                                 <tr><td colspan="6" style="height:6px;"></td></tr>
                                <tr>
                                     <td style="width:85px;"></td>
                                    <td> <asp:Button  Text=" עדכון &#10; כרטיס&#10;עבודה  " ID="btnUpdWorkCard"  runat="server" CssClass="opening-btn"  /></td>
                                     <td style="width:9px;"></td>
                                    <td> <asp:Button Text="סיכום&#10; חודשי&#10;לעובד   " ID="btnMonthlySum" runat="server" CssClass="opening-btn" PostBackUrl="~/Modules/Ovdim/EmployeTotalMonthly.aspx" /></td>
                                    <td style="width:9px;"></td>
                                    <td> <asp:Button Text=" תחקור&#10; חישוב&#10; לעובד  " ID="btnInquiryCalc" runat="server" CssClass="opening-btn"  PostBackUrl="~/Modules/Ovdim/TickurChishuvLeOved.aspx"/></td>
                               </tr>
                                <tr><td colspan="6" style="height:6px;"></td></tr>
                             </table>
                        </td>
                     </tr>
                     </table>
                   <table class="opening-quickmenu" style="margin-top:8px" runat="server" id="tblLogTahalichim">
                     <tr><td style="color:White;font-size:17px;">לוג תהליכים</td></tr>
                     <tr>
                        <td>
                            <table cellspacing="4px" border="0" cellpadding="1"  width="100%">
                                 <tr><td colspan="2" style="height:5px;"></td></tr>
                                <tr>
                                     <td style="width:5px;" rowspan="2"></td>
                                    <td align="right" valign="top">
                                        <asp:Panel ID="pnlProcessLog"  height="130px" width="637px" dir="ltr"  runat="server" ScrollBars="Auto">   
                                        <asp:GridView ID="grdProcessLog" runat="server"  GridLines="none" CssClass="Grid"   
                                                AllowPaging="false" AutoGenerateColumns="false"  Width="620px" 
                                                onrowdatabound="grdProcessLog_RowDataBound"  >
                                            <Columns>
                                                <asp:BoundField DataField="Teur_Tahalich" HeaderText="תהליך"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                                <asp:BoundField DataField="Teur_Peilut_Be_Tahalich" HeaderText="פעילות בתהליך" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="75px"/>
                                                <asp:BoundField DataField="Taarich" HeaderText="תאריך תקלה" HtmlEncodeFormatString="true" ItemStyle-CssClass="ItemRow" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="GridHeader" />
                                                <asp:BoundField DataField="Teur_Takala" HeaderText="תיאור תקלה" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                                <asp:BoundField DataField="Teur_Tech" HeaderText="תיאור טכני" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                             </Columns> 
                                            <AlternatingRowStyle CssClass="GridAltRow"  />
                                            <RowStyle CssClass="GridRow"   />
                                         </asp:GridView>
                                       </asp:Panel>  
                                    </td>
                               </tr>
                            </table>
                        </td>
                     </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="opening-mes" valign="top">
                <table>
                    <tr><td style="color:White;font-size:17px;">הודעות מערכת</td></tr>
                     <tr><td>
                         <asp:DataList ID="lstMessages" runat="server" Width="220px">
                            <ItemTemplate><br /> > <asp:Label ID="lblMessage" runat="server"><%#DataBinder.Eval(Container.DataItem, "Melel_Hodaa")%></asp:Label></ItemTemplate>
                         </asp:DataList>
                     </td></tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
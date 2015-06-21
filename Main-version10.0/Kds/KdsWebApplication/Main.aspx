<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="_Main" Codebehind="Main.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">   
    <table width="99%" cellspacing="6px" border="0">
         <tr>
            <td colspan="2" >

                <asp:UpdatePanel ID="UpdatePanelValidation" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                    <div>
                        <asp:ValidationSummary ID="vldGeneral" DisplayMode="BulletList"  runat="server" CssClass="ErrorMessage"  ValidationGroup="ErrGeneral"/>
                    </div>
                    <div>
                        <asp:Panel ID="pnlImpersonate" runat="server" Visible="false">
                            <asp:Label ID="Label1" runat="server" Text="מספר אישי להתחזות:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtImpersonate"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidTxtImperNum_1" runat="server" ControlToValidate="txtImpersonate"
                                Text="The field cannot be empty" 
                                ValidationGroup="impersonate"></asp:RequiredFieldValidator>
                        <%--      <asp:RangeValidator ID="ValidTxtImperNum_2" runat="server" ControlToValidate="txtImpersonate" 
                                ValidationGroup="impersonate" MinimumValue="0" MaximumValue="9999999"
                                Text="The field must be number" ></asp:RangeValidator>--%>
                            <asp:Button runat="server" ID="btnImpersonate" Text="החלף מ.א." ValidationGroup="impersonate" />
                        </asp:Panel>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr> 
        <tr style="height:20px">
           <td  ></td>
        </tr>
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
                        <td align="center">
                            <table cellspacing="4px" border="0" cellpadding="1">
                                 <tr><td colspan="6" style="height:6px;"></td></tr>
                                <tr>
                                     <td> <asp:Button Text=" כרטיסי עבודה לעובד " ID="btnUpdWorkCard" runat="server" CssClass="opening-btn" PostBackUrl="~/Modules/Ovdim/EmployeeCards.aspx" /></td>
                                     <td style="width:9px;"></td>
                                    <td> <asp:Button Text= " סיכום חודשי " ID="btnMonthlySum" runat="server" CssClass="opening-btn" PostBackUrl="~/Modules/Ovdim/EmployeTotalMonthly.aspx" /></td>
                                    <td style="width:9px;"></td>
                                    <td> <asp:Button Text=" דו''ח נוכחות " ID="btnInquiryCalc" runat="server" CssClass="opening-btn"  PostBackUrl="~/Modules/Reports/ReportFilters.aspx?RdlName=Presence"/></td>
                               </tr>
                                <tr><td colspan="6" style="height:6px;"></td></tr>
                             </table>
                        </td>
                     </tr>
                     </table>
                     
                     <div class="opening-quickmenu" style="margin-top:8px">
                   <table  runat="server" id="tblLogTahalichim">
                     <tr><td style="color:White;font-size:17px;">לוג תהליכים</td></tr>
                     <tr>
                        <td>
                            <table cellspacing="4px" border="0" cellpadding="1"  width="100%">
                                 <tr><td colspan="2" style="height:5px;"></td></tr>
                                <tr>
                                     <td style="width:2px;" rowspan="2"></td>
                                    <td align="right" valign="top">
                                        <asp:Panel ID="pnlProcessLog"  height="125px" Width="697px" dir="ltr"  runat="server" ScrollBars="Auto">   
                                        <asp:GridView ID="grdProcessLog" runat="server"  GridLines="none" CssClass="Grid"   
                                                AllowPaging="false" AutoGenerateColumns="false" Width="680px"
                                                onrowdatabound="grdProcessLog_RowDataBound"  >
                                            <Columns>
                                                <asp:BoundField DataField="Teur_Tahalich" HeaderText="תהליך"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-Font-Size="Smaller"/>
                                                <asp:BoundField DataField="Teur_Peilut_Be_Tahalich" HeaderText="פעילות בתהליך" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-Font-Size="Smaller"/>
                                                <asp:BoundField DataField="Taarich" HeaderText="תאריך תקלה" HtmlEncodeFormatString="true" ItemStyle-CssClass="ItemRow" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-CssClass="GridHeader" ItemStyle-Font-Size="Smaller"/>
                                                <asp:BoundField DataField="Teur_Takala" HeaderText="תיאור תקלה" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="200px" ItemStyle-Font-Size="Smaller"/>
                                                <asp:BoundField DataField="Teur_Tech" HeaderText="תיאור טכני" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-Font-Size="Smaller" />
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
                </table></div>
            </td>
        </tr>
        <tr>
            <td class="opening-mes" valign="top">
                <table>
                    <tr><td style="color:White;font-size:17px;">הודעות מערכת</td></tr>
                     <tr><td>
                         <asp:DataList ID="lstMessages" runat="server" Width="220px">
                            <ItemTemplate><br />  <asp:Label ID="lblMessage" runat="server"><%#DataBinder.Eval(Container.DataItem, "Melel_Hodaa")%></asp:Label></ItemTemplate>
                         </asp:DataList>
                     </td></tr>
                </table>
            </td>
        </tr>
    </table>
   <input type="hidden" runat="server" id="HiddenLoginPage" value="Login Page Loaded Ok" name="HiddenLoginPage" />   
<script language="javascript" type="text/javascript">
    function OpenEmpWorkCard() {
        var EmpId = <%=sUserId%>;
         var sQuryString = "?EmpID=" + EmpId + "&WCardDate=" + document.getElementById("ctl00_KdsContent_lblDate").innerHTML + "&dt=" + Date();
         var ReturnWin = window.showModalDialog('Modules/Ovdim/WorkCard.aspx' + sQuryString, window, "dialogHeight: 680px; dialogWidth: 1010px;;status: 1;");
        if (ReturnWin == '' || ReturnWin == 'undefined') ReturnWin = false;
        return ReturnWin;
    }  
</script>
</asp:Content>
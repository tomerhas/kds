<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HachtamotHityzvut.aspx.cs" Inherits="KdsWebApplication.Modules.Ovdim.HachtamotHityzvut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>החתמות התייצבות </title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
    <script src="../../Js/GeneralFunction.js" ></script>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upMain" ChildrenAsTriggers="true">
    <ContentTemplate>
   <div align="center">
       <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Bold="true" Visible="false" Text="לא נמצאו התייצבויות לעובד!" ></asp:Label>
        <br />
       <table style="width:90%;" cellspacing="0">
        <tr>
            <td align="center" style="width:100%;">
                <asp:Label ID="LabelHeader" runat ="server"   Font-Size="Larger"  Font-Bold="true" Width="100%" Height="25px" ></asp:Label>
              
            </td>
        </tr>
         
         <tr>
            <td style="width:100%;">
                <asp:GridView runat="server" ID="grdHityazvut" AutoGenerateColumns="false"  
                    EmptyDataText="לא נמצאו נתונים!" AllowPaging="true" PageSize="6" 
                    HeaderStyle-CssClass="WorkCardRechivimGridHeader" CssClass="WorkCardRechivimGridRow"
                     OnRowDataBound="grdHityazvut_RowDataBound" 
                    HeaderStyle-ForeColor="White" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Mispar_Sidur" HeaderText="מספר סידור" ItemStyle-CssClass="WorkCardRechivimGridRow" />
                         <asp:BoundField DataField="TEUR_SIDUR_MEYCHAD" HeaderText="תאור סידור" ItemStyle-CssClass="WorkCardRechivimGridRow"/>
                         <asp:BoundField DataField="SHAT_HATCHALA" HeaderText="שעת החתמה" DataFormatString="{0:HH:mm}" ItemStyle-CssClass="WorkCardRechivimGridRow"/>
                         <asp:BoundField DataField="TEUR_MIKUM_YECHIDA" HeaderText="מיקום החתמה" ItemStyle-CssClass="WorkCardRechivimGridRow"/>

                          <asp:TemplateField HeaderText="התייצבות ראשונה"    ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="WorkCardRechivimGridRow"   >
                             <ItemTemplate>
                                    <asp:Image runat="server" ID="imgFirst" Visible="false"   ImageUrl="~/Images/tick.png" />
                             </ItemTemplate>           
                         </asp:TemplateField>  

                          <asp:TemplateField HeaderText="התייצבות שניה"   ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="WorkCardRechivimGridRow"  >
                             <ItemTemplate>
                                    <asp:Image runat="server" ID="imgSec" Visible="false"   ImageUrl="~/Images/tick.png" />
                             </ItemTemplate>           
                         </asp:TemplateField>  

                        <asp:BoundField DataField="First_SHAT_HITIATZVUT" ItemStyle-Width="0px"/>
                        <asp:BoundField DataField="Second_SHAT_HITIATZVUT" ItemStyle-Width="0px"/>
                    </Columns>
                    <AlternatingRowStyle CssClass="WorkCardRechivimGridRow"  />
                    <RowStyle CssClass="WorkCardRechivimGridRow"   />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                    <EmptyDataRowStyle CssClass="WorkCardRechivimGridRow" height="20px" Wrap="False"/> 
                </asp:GridView>
            </td>
        </tr>
        </table>
        <br />
        <table style="width:90%;">
            <tr>
                <td style="width:100%;" align="right">
                    <input type="button" class="btnWorkCardCloseWin" style="width:75px;Height:30px" value="סגור" onclick="javascript:window.close();" />
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

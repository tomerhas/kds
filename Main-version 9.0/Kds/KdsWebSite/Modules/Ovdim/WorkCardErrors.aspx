<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkCardErrors.aspx.cs" Inherits="Modules_Ovdim_WorkCardErrors" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>שגיאות ברטיס עבודה</title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
    <script src="../../Js/GeneralFunction.js" ></script>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upMain" ChildrenAsTriggers="true">
    <ContentTemplate>
    <div align="center">
        <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Bold="true" Visible="false" Text="נא לרענן את כרטיס העבודה" ></asp:Label>
        <br />
        <table style="width:90%;" cellspacing="0">
         <tr>
            <td align="center" style="width:100%;">
                <asp:Label ID="lblHeaderMessage" runat ="server" CssClass="GridHeader" Width="100%" Height="20px" >שגיאות כלליות ליום עבודה</asp:Label>        
            </td>
         </tr>   
        <tr>
            <td style="width:100%;">
                <asp:GridView runat="server" ID="grdWorkDayErrors" AutoGenerateColumns="false"
                     EmptyDataText="לא נמצאו נתונים!" AllowPaging="true" PageSize="6" 
                     HeaderStyle-CssClass="GridHeader"
                     HeaderStyle-ForeColor="White" Width="100%"  >
                    <Columns>
                        <asp:BoundField ItemStyle-Width="40%" DataField="TEUR" HeaderText="שדה" />
                        <asp:BoundField ItemStyle-Width="60%" DataField="Teur_Shgia" HeaderText="תאור השגיאה" />
                    </Columns>
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                    <RowStyle CssClass="GridRow"   />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                    <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/> 
                </asp:GridView>
            </td>
        </tr>
        </table>
        <br />
        <table style="width:90%;" cellspacing="0">
        <tr>
            <td align="center" style="width:100%;">
                <asp:Label ID="Label1" runat ="server" CssClass="GridHeader" Width="100%" Height="20px" >שגיאות בתוך סידורים</asp:Label>
            </td>
        </tr>
        
         <tr>
            <td style="width:100%;">
                <asp:GridView runat="server" ID="grdSidurErrors" AutoGenerateColumns="false"  
                    EmptyDataText="לא נמצאו נתונים!" AllowPaging="true" PageSize="6" 
                    HeaderStyle-CssClass="GridHeader"
                    HeaderStyle-ForeColor="White" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Mispar_Sidur" HeaderText="סידור" />
                        <asp:BoundField DataField="Shat_Hatchala" HeaderText="ש. התחלה" DataFormatString="{0:HH:mm}" />
                        
                        <asp:BoundField DataField="Shat_yetzia" HeaderText="ש. יציאה" DataFormatString="{0:HH:mm dd/MM/yyyy}" />
                        <asp:BoundField DataField="TEUR" HeaderText="שדה" />
                        <asp:BoundField DataField="Teur_Shgia" HeaderText="תאור השגיאה" />
                    </Columns>
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                    <RowStyle CssClass="GridRow"   />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                    <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/> 
                </asp:GridView>
            </td>
        </tr>
        </table>
        <br />
        <table style="width:90%;">
            <tr>
                <td style="width:100%;" align="right">
                    <input type="button" class="ImgButtonSearch" value="סגור" onclick="javascript:window.close();" />
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rechivim.aspx.cs" Inherits="Modules_Ovdim_Rechivim" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <base target="_self" />  
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
   <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>  
   <script type="text/javascript">
       function window.onload() {
           if (window.dialogArguments != undefined) {
               if (window.dialogArguments.document.getElementById("divHourglass") != null)
                   window.dialogArguments.document.getElementById("divHourglass").style.display = 'none';
           }
       }
 </script>
</head>
<body dir="rtl">
     <form id="Rechivim" runat="server">
     <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
     </asp:ScriptManager>
    <div>
    <br />
      <table width="450px" border="0" cellspacing="0" cellpadding="0">
       <tr>
          <td  valign="top" align="center" rowspan="4">
             <asp:Label runat="server" ID="lblPageHeader"   width="450px"  CssClass = "GridHeader">רכיבים מחושבים לסידור וליום עבודה</asp:Label>
              <div runat="server" id="pnlRechivim" style="height: 350px; width:450px; overflow:auto;direction: rtl;">
                <asp:GridView ID="grdRechivim" runat="server" GridLines="none" 
                      CssClass="GridAltRow"  EmptyDataText="לא נמצאו נתונים!" 
                    AllowPaging="false" AutoGenerateColumns="false"  
                    HeaderStyle-CssClass="WCard_collapse_header" ShowHeader="true" ShowFooter="false"
                      onrowdatabound="grdRechivim_RowDataBound">
                    <EmptyDataRowStyle CssClass="GridHeader" />
                    <Columns>
                      
                        <asp:BoundField DataField="TEUR_RECHIV" HeaderText="תאור רכיב" 
                            ItemStyle-CssClass="ItemRow" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="ItemRow" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mispar_sidur" HeaderText="מספר סידור" 
                            ItemStyle-CssClass="ItemRow" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="ItemRow" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="shat_hatchala" DataFormatString="{0: hh:mm}" 
                            HeaderText="שעת התחלה" ItemStyle-CssClass="ItemRow" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="ItemRow" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="erech_rechiv" HeaderText="תוצאות חישוב" 
                            ItemStyle-CssClass="ItemRowLast" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="ItemRowLast" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="kod_rechiv" />
                        <asp:BoundField DataField="row_type" />
                    </Columns>
                    <EmptyDataRowStyle CssClass="GridEmptyData" height="20px"/> 
                 </asp:GridView>
               </div>
          </td>
       </tr>
      </table>
    </div>
    </form>
</body>

</html>

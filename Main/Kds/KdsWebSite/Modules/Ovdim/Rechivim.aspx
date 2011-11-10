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
<body dir="rtl" class="WorkCardRechivim">
     <form id="Rechivim" runat="server" >
     <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
     </asp:ScriptManager>
    <div>
    <br />
    <br />    
      <table border="0" width="637px" >
       <tr style="height:395px">
          <td valign="top" align="center" rowspan="4" width="637px">
             <asp:Label runat="server" ID="lblPageHeader" class="WorkCardRechivimHeader"  width="637px" Height="36px" >רכיבים מחושבים לסידור וליום עבודה</asp:Label>
              <div runat="server" id="pnlRechivim" style="height: 395px; width:637px; overflow:auto;direction: rtl;">
                <asp:GridView ID="grdRechivim" runat="server" GridLines="none" width="637px"
                    CssClass="WorkCardRechivimGridRow"  EmptyDataText="לא נמצאו נתונים!" 
                    AllowPaging="false" AutoGenerateColumns="false"  
                    HeaderStyle-CssClass="WorkCardRechivimGridHeader" ShowHeader="true" ShowFooter="false"
                    onrowdatabound="grdRechivim_RowDataBound">
                    <EmptyDataRowStyle CssClass="WorkCardRechivimGridRow" />
                    <Columns>                      
                        <asp:BoundField DataField="TEUR_RECHIV" HeaderText="תאור רכיב" 
                            ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="WorkCardRechivimGridRow" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mispar_sidur" HeaderText="מספר סידור" 
                            ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="WorkCardRechivimGridRow" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="shat_hatchala" DataFormatString="{0: hh:mm}" 
                            HeaderText="שעת התחלה" ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="WorkCardRechivimGridRow" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="erech_rechiv" HeaderText="תוצאות חישוב" 
                            ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle CssClass="WorkCardRechivimGridRow" HorizontalAlign="Center" />
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

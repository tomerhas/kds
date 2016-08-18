<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BakashatTashlum.aspx.cs" Inherits="Modules_Ovdim_BakashatTashlum" Title="בקשה לתשלום שעות נוספות מעל המכסה" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title runat="server" id="TitleWindow">בקשה לתשלום שעות נוספות מעל המכסה</title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
    <script src="../../Js/GeneralFunction.js" ></script>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    
    <asp:Label ID="lblHeaderMessage" runat ="server" CssClass="GridHeader" Width="100%" Height="20px" >בקשה לתשלום שעות נוספות מעל המכסה</asp:Label>
        <br />
     <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
     </asp:ScriptManager>
      <asp:UpdatePanel ID="upBody" runat="server">
         <ContentTemplate>
            <table width="100%" border="0">
            <tr>
            <td width="5px" rowspan="8"></td>
            <td>
            <div id="DivEmployeeDetails" runat="server" visible="false">
                מ.א.:<asp:Label ID="TxtMisparIshi"  runat="server" Width="150px"></asp:Label>
                שם:<asp:Label ID="TxtFullName"   runat="server" Width="100px"></asp:Label>
                חודש:<asp:Label ID="TxtMonth"  runat="server" Width="60px"></asp:Label>
            </div>
            </td></tr>
            <tr>
            <td>
             <table width="350px" class="Grid" border="0" cellpadding="1" cellspacing="0" >
                 <tr class="GridHeader">
                    <td class="ItemRow"></td>
                    <td class="ItemRow">מכסה</td>
                    <td class="ItemRow"> ש"נ שקוזזו מעבר למכסה</td>
                </tr>
                 <tr class="GridAltRow" id="trMeshekChol" runat="server">
                     <td class="ItemRow" >משק ומינהל  - חול</td>
                    <td class="ItemRow"><asp:Label ID="lblMichsaMeshekChol" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblKizuzMeshekChol" runat="server"></asp:Label></td>
                </tr>
                  <tr class="GridAltRow" id="trNihulChol" runat="server">
                    <td class="ItemRow">ניהול תנועה - חול</td>
                    <td class="ItemRow"><asp:Label ID="lblMichsaNihulChol" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblKizuzNihulChol" runat="server"></asp:Label></td>
                </tr>
               
                 <tr class="GridAltRow" id="trMeshekShabat" runat="server">
                    <td class="ItemRow">משק ומינהל - שבת </td>
                    <td class="ItemRow"><asp:Label ID="lblMichsaMeshekShabat" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblKizuzMeshekShabat" runat="server"></asp:Label></td>
                </tr>
                  <tr class="GridAltRow" id="trNihulShabat" runat="server">
                    <td class="ItemRow">ניהול תנועה - שבת</td>
                    <td class="ItemRow"><asp:Label ID="lblMichsaNihulShabat" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblKizuzNihulShabat" runat="server"></asp:Label></td>
                </tr>
            </table>
            </td>
            </tr>
            <tr>
            <td><br />
            <table width="250px" class="Grid" border="0" cellpadding="1" cellspacing="0" >
                 <tr class="GridHeader">
                    <td class="ItemRow">סוג שעות נוספות</td>
                    <td class="ItemRow">  שעות שקוזזו</td>
                </tr>
                <tr class="GridAltRow" runat="server" id="trShaot125">
                    <td class="ItemRow">125%</td>
                    <td class="ItemRow"><asp:Label ID="lblShaot125" runat="server"></asp:Label></td>
                </tr>
                  <tr class="GridAltRow" runat="server" id="trShaot150">
                    <td class="ItemRow">150%</td>
                    <td class="ItemRow"><asp:Label ID="lblShaot150" runat="server"></asp:Label></td>
                </tr>
                <tr class="GridAltRow" runat="server" id="trShaot200">
                    <td class="ItemRow">200%</td>
                    <td class="ItemRow"><asp:Label ID="lblShaot200" runat="server"></asp:Label></td>
                </tr>
              
                 <tr class="GridAltRow">
                    <td class="ItemRow">סה"כ שעות שקוזזו:</td>
                    <td class="ItemRow"><asp:Label ID="lblSumShaot" runat="server"></asp:Label></td>
                </tr>
            </table>
                <br />
                  
            </td>
            </tr>
            <tr><td>
                <div runat="server" id="DivTextEditable">
                <table>
                    <tr>
                        <td>מבקש תשלום מחוץ למכסה עבור:</td>
                        <td width="260px">
                          <asp:TextBox ID="txtShaot" runat="server"  MaxLength="20"   Width="90"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="vldReqShaot" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="חובה למלא שעות לתשלום!" ControlToValidate="txtShaot"></asp:RequiredFieldValidator>
                          <asp:CompareValidator ID="vldShaot" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="הערך בשדה שעות לא תקין!" Type="Double" Operator="DataTypeCheck"  ControlToValidate="txtShaot"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        סיבה:
                        </td>
                        <td>
                          <asp:TextBox ID="txtSiba" runat="server"  MaxLength="50"   Width="200" Rows="3" TextMode="MultiLine" ></asp:TextBox><br />
                          <asp:RequiredFieldValidator ID="vldSiba" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="חובה למלא סיבה!" ControlToValidate="txtSiba"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                             <asp:Label runat="server" ID="lblPrevLevelShaotMeushar" Text="מחוץ למכסה מאושר ע''י רמה קודמת:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPrevLevelShaotMeushar"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                            <td valign="top">
                                <asp:Label runat="server" ID="lblShaotMeushar" Text="מחוץ למכסה מאושר:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtShaotMeushar" runat="server"  MaxLength="20"   Width="90"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="חובה למלא שעות מכסה מאושר!" ControlToValidate="txtShaotMeushar"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="הערך בשדה שעות לא תקין!" Type="Double" Operator="DataTypeCheck"  ControlToValidate="txtShaotMeushar"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="לא ניתן לאשר ערך הגדול מערך המבוקש ולא ניתן לאשר ערך שלילי!" ControlToValidate="txtShaotMeushar" Type="Double"  MinimumValue="0" ></asp:RangeValidator>
                            </td>
                        </tr>
                 </table>
                </div>
                
            </tr>
            
            <tr>
                <td align="center">
                 <asp:UpdatePanel ID="upButton" runat="server">
                    <ContentTemplate> 
                     <asp:Button Text="שלח בקשה" ID="btnSend" runat="server" CssClass="ImgButtonSearch" onclick="btnSend_Click" /> 
                        <br> 
                     </br>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
             </tr>
            </td></tr>                     
            </table>
         </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript">
  
</script>
</html>

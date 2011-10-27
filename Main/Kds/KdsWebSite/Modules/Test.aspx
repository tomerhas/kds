<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Modules_Test" AsyncTimeout=10000  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucSidurim.ascx" TagName="ucSidurim"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />    
    <script src="../../Js/GeneralFunction.js" type="text/javascript">
    
    </script>
    <script type="text/vbscript" language="vbscript">
   
       
    </script>
    <script type="text/javascript" language="javascript "> 
     function ChangeStatusSidur(id)
    {           
      debugger
      document.getElementById(id).value="0"; 
      document.getElementById(id).src = "../../Images/allscreens-checkbox.jpg"; 
      return false;
    }
//     Sys.Application.add_init(AppInit);
// 
//    function AppInit() {
//      wsGeneral.GetRSSReader(OnSuccess, OnFailure);
//    }
// 
//    function OnSuccess(result) {
//      // Remove the .loading CSS from the div, to remove the 
//      //  progress indicator background.
//      Sys.UI.DomElement.removeCssClass($get('RSSBlock'), 'loading');
//     
//      // Fill the div with the HTML generated from the user control.
//      $get('RSSBlock').innerHTML = result;
//    }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true">        
   </asp:ScriptManager>
    <div>       
        </div>
   
        
       <asp:Panel ID="pnlHeader" runat="server"></asp:Panel>
        <table runat="server" id="tblTest">
            <tr>               
               <td>תאריך</td>
                <td>
                 <KdsCalendar:KdsCalendar runat="server" ID="clnFromDate"  AutoPostBack="false"  dir="rtl" onfocus="this.select();" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                            
                
<%--                  <wccEgged:wccCalendar runat="server" ID="clnFromDate" BasePath="../EggedFramework" AutoPostBack="false" Width="110px" dir="rtl"></wccEgged:wccCalendar>                                                                      --%>
                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional"  >
                        <ContentTemplate> 
                    <asp:ListBox Width="100%" Height="150px" ID="LstBxOfElement" runat="server"></asp:ListBox>
                </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="BtAddSelected" />    
               </Triggers>
                 </asp:UpdatePanel> 

                </td>
            </tr>    
            <tr>   
                <td>
                <asp:Button Width="30px" ID="BtAddSelected" runat="server" Text=">" 
                        onclick="BtAddSelected_Click" />
                    מספר אישי
                </td> 
                <td>
                    <asp:TextBox runat="server" ID="txtId"></asp:TextBox>                      
                </td>
                <td>
                    <asp:CheckBox runat="server" Text="עובד מוסך" ID="chkGarage" />
                </td>
            </tr>    
            <tr>
                <td>
                 <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                        Text="חישוב חדש" />
                 </td>       
            
                <td>
                 <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                        Text="רוטינת שינוי קלט" />
                 </td> 
                <td>
                 <asp:Button ID="Button1" runat="server"
                        Text="חישוב רכיבים" onclick="Button1_Click" />
                 </td>  
                 <td>
                 <asp:Button ID="Button4" runat="server"
                        Text="העברה לשכר" onclick="Button4_Click"  />
                 </td> 
                 <td>
                 <asp:Button ID="btnWorkCard" runat="server"
                        Text="כרטיס עבודה" onclick="btnWorkCard_Click" />
                 </td> 
                 <td>
                    <asp:Button ID="btnApproval" runat="server"
                        Text="חפש אישורים" OnClick="btnApproval_click" />
                 </td> 
                 <td>
                    <asp:Button ID="btnInputAndErrorsBatch" runat="server"
                        Text="שגיאות ונתוני קלט מתהליך לילה" OnClick="btnInputAndErrorsBatch_click" />
                 </td> 
                         
            </tr>     
            <tr>
                <td>
                <asp:Button ID="btnImportCompare" runat="server"
                    Text="קליטת קבצים להשוואה" OnClick="btnImportCompare_Click" />
                </td>
                <td>
                  <asp:Button ID="Button5" runat="server"
                        Text="טסט תנועה" onclick="Button5_Click"  /><br />
                        <asp:Label id="lblTimeNoVisut" runat="server" ></asp:Label> ללא ויסות<br />
                         <asp:Label id="lblTimeWithVisut" runat="server"></asp:Label> כולל ויסות
                 </td> 
            </tr> 
            </table> 
            <table>   
            <tr>
               <td dir="ltr" align="right" >                                            
<%--                    <wccEgged:wccCalendar runat="server" ID="clnDate" CssClass ="WorkCardTextBox" 
                        BasePath="../EggedFramework" AutoPostBack="false"   OnChangeScript=""
                          ></wccEgged:wccCalendar>--%>                                                                                        
                </td>
                <td>           
                    
                </td>
                 <td>
                 <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
                        Text="חישוב פרימיות" />
                 <asp:Button ID="ButtonShinuyim" runat="server" onclick="ButtonShinuyim_Click" 
                        Text="שגיאות" />
                 </td>      
            </tr>
         </table>  
          <asp:RadioButton runat="server" AutoPostBack="true" ID="rdoTst" Text="בדיקה" 
                oncheckedchanged="rdoTst_CheckedChanged"  />          
    </form>
    
    
</body>


</html>

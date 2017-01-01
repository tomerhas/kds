﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Modules_Test" AsyncTimeout=10000 Async="true"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/ucSidurim.ascx" TagName="ucSidurim"%>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
     <script src='../../js/jquery.js' type='text/javascript'></script>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />    
    <script src="../../Js/GeneralFunction.js" type="text/javascript">
    
    </script>
     <style type="text/css">  
        .block {  
            height:150px;  
            width:200px;  
            border:1px solid aliceblue;  
            overflow-y:scroll;  
        }  
    </style> 

    <script type="text/vbscript" language="vbscript">
   
       
    </script>
    <script type="text/javascript" language="javascript ">
//        $(document).ready(function () {
//            $("button").click(function () {
//                $("div").load('txtId.txt');
//            });
//        });

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
       <%-- <asp:GridView ID="grdEmployee" runat="server" AllowSorting="true" 
                AllowPaging="true" PageSize="8" AutoGenerateColumns="false" CssClass="Grid"  
                Width="950px" EmptyDataText="לא נמצאו נתונים!" ShowHeader="true" 
                OnRowDataBound="grdEmployee_RowDataBound" OnSorting="grdEmployee_Sorting" OnPageIndexChanging="grdEmployee_PageIndexChanging">                                 
            <Columns>                                                                           
                <asp:BoundField DataField="status" HeaderText="דרוש עדכון" SortExpression="status"  ItemStyle-Font-Size="Larger"  ItemStyle-CssClass="ItemRow"  HeaderStyle-Width="150px" HeaderStyle-CssClass="GridHeader"   />
                <asp:BoundField DataField="kartis_without_peilut" HeaderText="ללא דיווח"  ItemStyle-Font-Size="Larger" SortExpression="kartis_without_peilut" ItemStyle-CssClass="ItemRow" HeaderStyle-Width="145px"  HeaderStyle-CssClass="GridHeader"  />
                <asp:BoundField DataField="measher_o_mistayeg_key" HeaderText="מסתייג/מאשר/ללא התייחסות"  ItemStyle-Font-Size="Larger"  SortExpression="measher_o_mistayeg_key"  HeaderStyle-Width="250px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                <asp:BoundField DataField="status_tipul_key" HeaderText="ממתין לאישור"  ItemStyle-Font-Size="Larger" SortExpression="status_tipul_key" ItemStyle-CssClass="ItemRow"   HeaderStyle-Width="150px" HeaderStyle-CssClass="GridHeader"  />
            <%--     <asp:BoundField DataField="status_tipul_key" HeaderText="ממתין לחישוב שכר" SortExpression="status_tipul_key" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />                            
          </Columns>
            <AlternatingRowStyle CssClass="GridAltRow" Height="25px" />
            <RowStyle CssClass="GridRow" Height="25px" />
            <PagerStyle CssClass="GridPagerLarge" HorizontalAlign="Center"  />                          
            <EmptyDataRowStyle CssClass="GridEmptyData" height="10px" Wrap="False"/>                                                            
        </asp:GridView>   
    </div>--%>
  <%--  <uc:ucTestCB runat="server" ID="ucTestCB" />--%>
      <%-- <div id="dddlcb" runat="server">
           <asp:Label ID="lblddl" runat="server"  width="160px"></asp:Label>  
           <asp:ImageButton ID="imgddl" runat="server" ImageUrl="../../Images/icon-print.jpg" />
           <div class="block">  
             <asp:CheckBoxList ID="CheckBoxList1" runat="server" >     </asp:CheckBoxList> 
          </div>
       </div>--%>

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
               <td>
                    <asp:Button ID="btnShguimBatch" runat="server"
                        Text="שגויים batch" OnClick="btnShguimBatch_click" />
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
                    <asp:Button ID="Button7" runat="server" onclick="btnShlifatRikuz_click" 
                        Text="ריכוז" />
                </td>
                 <td>
                 <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
                        Text="חישוב פרמיות" />

                 <asp:Button ID="ButtonShinuyim" runat="server" onclick="ButtonShinuyim_Click" 
                        Text="שגיאות" />
                 </td>    
                  <td>
                 <asp:Button ID="btnPremyot" runat="server"
                        Text=" חדש - פרמיות חישוב" onclick="btnPremyot_Click"  />
                 </td> 
                   <td>
                 <asp:Button ID="btnMakat" runat="server" onclick="btnMakat_Click" 
                        Text="תקינות  מקטים" />
                 </td>  
                 <td>   
                 <asp:Button ID="btnRefreshMakatim" runat="server" onclick="btnRefreshMakatim_Click" 
                        Text="רענן מקטים" />
                 </td>         
            </tr>
         </table>  
          <asp:RadioButton runat="server" AutoPostBack="true" ID="rdoTst" Text="בדיקה" 
                oncheckedchanged="rdoTst_CheckedChanged"  />          
    </form>
    
    <div><h2>Let AJAX change this text</h2></div>
    <button>Change Content</button>


</body>


</html>
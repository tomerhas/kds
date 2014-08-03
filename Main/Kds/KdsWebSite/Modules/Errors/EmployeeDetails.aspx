<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeDetails.aspx.cs" Inherits="Modules_Errors_EmployeeDetails" Title="Untitled Page" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 127px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
<div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> 
</div>  
     <div class="TitleLable" runat="server" id="divPeriod"></div>
        
     <asp:UpdatePanel ID="upOvedDetails" runat="server" RenderMode="Inline" UpdateMode="Conditional">
      <ContentTemplate> 
         <table cellpadding="3" cellspacing="0" border="1" style="width:940px;height:30px;border-color:Gray;" >
            <tr class="OvedDetailsHeader">            
                <td>מספר אישי</td><td>שם</td><td>איזור</td><td>סניף</td><td>מעמד</td>                                                              
            </tr>
            <tr class="InternalLabel">
                <td><a id="linkMisparIshi" href="#" runat="server" onclick="openNetuneyOved()"><asp:Label runat="server" ID="lblMisparIshi">
                </asp:Label></a></td>                
                <td><asp:Label runat="server" ID="lblName"></asp:Label></td>                
                <td><asp:Label runat="server" ID="lblEzor"></asp:Label></td>                
                <td><asp:Label runat="server" ID="lblSnif"></asp:Label></td>                
                <td><asp:Label runat="server" ID="lblMaamad"></asp:Label></td>                                    
            </tr>
         </table>
        </ContentTemplate>   
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNext" />                                                                    
            <asp:AsyncPostBackTrigger ControlID="btnPrev" />                                                                        
       </Triggers>               
       </asp:UpdatePanel>               
     <br />
     <div class="TitleLable">כרטיסי עבודה לטיפול:</div>
     <table class="InternalLabel"> 
       <tr>
        <td>            
            <asp:UpdatePanel ID="udGrid" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                 <ContentTemplate>                    
                    <asp:GridView ID="grdOvedErrorCards" runat="server" AllowSorting="true" 
                         AllowPaging="true" PageSize="8" AutoGenerateColumns="false" 
                         Width="940px" EmptyDataText="לא נמצאו נתונים!" 
                         OnRowDataBound="grdOvedErrorCards_RowDataBound"  OnSorting="grdOvedErrorCards_Sorting" >
                        <Columns>                
                            <asp:HyperLinkField DataTextField="teur_yom" ItemStyle-CssClass="ItemRow"   HeaderStyle-CssClass="GridHeader"   HeaderText="תאריך" SortExpression="teur_yom" NavigateUrl="#"  ItemStyle-Width="220px"   />                                                                 
                            <asp:BoundField DataField="status_key"  SortExpression="status_key" HeaderText="שגוי"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="measher_o_mistayeg_key" SortExpression="measher_o_mistayeg_key" HeaderText="מסתייג"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="status_tipul_key" SortExpression="status_tipul_key" HeaderText="ממתין לאישור"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-HorizontalAlign="Center"/>                            
                            <asp:BoundField DataField="taarich" SortExpression="taarich" HeaderText=""  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-HorizontalAlign="Center"/>
                        </Columns>
                        <AlternatingRowStyle CssClass="GridAltRow"  />
                        <RowStyle CssClass="GridRow"   />
                         <PagerStyle CssClass="GridPagerLarge" HorizontalAlign="Center"  />                               
                        <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                        </PagerTemplate>   
                    </asp:GridView>                    
                 </ContentTemplate> 
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNext" />                                                                    
                    <asp:AsyncPostBackTrigger ControlID="btnPrev" />                                                                                            
               </Triggers>               
             </asp:UpdatePanel>      
        </td>             
    </tr>           
   </table>   
   <br />
   <table class="InternalLabel">
    <tr>                     
        <td class="style1">
            <asp:UpdatePanel ID="upPrev" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate> 
                    <asp:Button ID="btnPrev" runat="server" CssClass ="ImgButtonOvedPrev" autopostback="true" onclick="btnPrev_Click" />                            
                </ContentTemplate>    
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNext" />                                                                                                                                                                          
                </Triggers>                
            </asp:UpdatePanel>    
        </td>
        <td style="width:610px">
            <asp:UpdatePanel ID="upNext" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                <ContentTemplate> 
                    <asp:Button ID="btnNext" runat="server" CssClass ="ImgButtonOvedNext" autopostback="true" onclick="btnNext_Click"  />                            
                </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPrev" />                                                                                                                                                                          
                </Triggers>   
            </asp:UpdatePanel>        
        </td>
        
         <td style="width:200px" align="left">
             <asp:Button id="btnRefresh" runat="server" OnClick="btnRefresh_OnClick" class="ImgButtonSearch" Width="120px" Text="רענן" />                           
        </td>                
    </tr> 
    <tr>
        <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button id="btnBack" runat="server" OnClick="btnBack_OnClick" class="ImgButtonSearch" Width="130px" Text="חזרה לדף הראשי" />
             <%-- <asp:Button ID="btnBack" runat="server" Width="120px" CssClass="ImgButtonSearch" Text="חזרה לדף הראשי" OnClientClick="btnBack_OnClick()"    />  --%>
        </td>
    </tr> 
   </table>
     <input type="hidden" id="InputHiddenBack" name="InputHiddenBack" value="false" runat="server" />
      
   <script language="javascript" type="text/javascript" >
        function OpenEmpWorkCard(RowDate)
        {
            var EmpId = document.getElementById("ctl00_KdsContent_lblMisparIshi").innerHTML;
            var WCardDate = RowDate;
            var sQuryString = "?EmpID=" + EmpId + "&WCardDate=" + WCardDate + "&dt=" + Date();
            document.getElementById("divHourglass").style.display = 'block';
            var ReturnWin = window.showModalDialog('../Ovdim/WorkCard.aspx' + sQuryString, window, "dialogHeight:680px; dialogWidth: 1010px;scroll:no;;status: 1;");
            if (ReturnWin == '' || ReturnWin == 'undefined') ReturnWin = false;
            //   debugger;
            document.getElementById("ctl00_KdsContent_btnRefresh").click();  
            document.getElementById("divHourglass").style.display = 'none';
            return ReturnWin;
        }

        function openNetuneyOved() {
            var EmpId = document.getElementById("ctl00_KdsContent_lblMisparIshi").innerHTML;
            var Month = (new Date()).format("MM/yyyy");
            window.showModalDialog("../Ovdim/NetuneyOvedModal.aspx?Id=" + EmpId + "&Month=" + Month , window ,"dialogwidth:970px;dialogheight:580px;"); //dialogtop:130px;dialogleft:25px;status:no;resizable:yes;scroll:0;");
        }

     
//        function btnBack_OnClick() {
//            window.location.href = "EmployeErrors.aspx?Back=true";
//        }
   </script>
     
   </asp:Content>


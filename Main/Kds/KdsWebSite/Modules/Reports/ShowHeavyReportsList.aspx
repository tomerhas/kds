<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShowHeavyReportsList.aspx.cs" Inherits="Modules_Reports_ShowHeavyReportsList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">

<asp:UpdatePanel ID="upDivNetunim" runat="server" RenderMode="Inline">
    <ContentTemplate> 
        <%-- <iframe height="700px" width="100%">--%>
         <div id="divNetunim" runat="server"  style="text-align:right;">
             <br /> 
             <div runat="server" ID="pnlgrdRequest" style="Height:460px;Width:978px;overflow:auto;direction:ltr;">
                <asp:GridView ID="grdRequest" runat="server"  GridLines="None"  ShowHeader="true"
                       CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="true"
                       Width="945px"   EmptyDataText="לא נמצאו נתונים!" 
                     EmptyDataRowStyle-CssClass="GridHeader"
                     onrowdatabound="grdRequest_RowDataBound">
                    <Columns>
                         <asp:BoundField DataField="BAKASHA_ID"    HeaderText="מספר בקשה"  ItemStyle-Width="50px" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                         <asp:BoundField DataField="TEUR"          HeaderText="תאור"       ItemStyle-Width="430px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                         <asp:BoundField DataField="ZMAN_HATCHALA" HeaderText="תחילת הפקה" ItemStyle-Width="110px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncodeFormatString="true"/>
                         <asp:BoundField DataField="ZMAN_Siyum"    HeaderText="סיום הפקה" ItemStyle-Width="110px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"   DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncodeFormatString="true"/>
                         <asp:BoundField DataField="SHEM_TIKIYA"  ItemStyle-Width="0px" />
                         <asp:BoundField DataField="EXTENSION_TYPE"  ItemStyle-Width="0px"  />                     
                       
                         <asp:TemplateField HeaderText="קובץ"  ItemStyle-Width="40px"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  >
                             <ItemTemplate>
                               <asp:HyperLink  ID="imgButton" runat="server" NavigateUrl="#" ></asp:HyperLink>
                             </ItemTemplate>           
                         </asp:TemplateField>  
                      </Columns> 
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                        <RowStyle CssClass="GridRow"   />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                        <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/>  
                 </asp:GridView>
             </div>
        </div>
         <%--   </iframe>--%>

                  <br /> 
    </ContentTemplate>
</asp:UpdatePanel>

<script language="javascript" type="text/javascript">
    function OpenKovez(url) {
        window.open(url);
      //  var a = "http://www.ynet.co.il/home/0,7340,L-8,00.html";
      //  window.showModalDialog(url, '', 'dialogwidth:1200px;dialogheight:800px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');
 }
   </script> 
</asp:Content>



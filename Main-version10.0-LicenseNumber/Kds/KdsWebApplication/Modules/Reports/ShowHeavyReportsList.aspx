<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Modules_Reports_ShowHeavyReportsList" Codebehind="ShowHeavyReportsList.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">

<asp:UpdatePanel ID="upDivNetunim" runat="server" RenderMode="Inline">
    <ContentTemplate> 
        <%-- <iframe height="700px" width="100%">--%>
         <div id="divNetunim" runat="server"  style="text-align:right;">
             <br /> 
             <div runat="server" ID="pnlgrdReports" style="Height:460px;Width:978px;overflow:auto;direction:rtl;">
                <asp:GridView ID="grdReports" runat="server"  GridLines="None"  ShowHeader="true"
                       CssClass="Grid"   AllowPaging="true" PageSize="11" AutoGenerateColumns="false"   AllowSorting="true"
                       Width="978px"   EmptyDataText="לא נמצאו נתונים!" 
                     EmptyDataRowStyle-CssClass="GridHeader"
                     onrowdatabound="grdReports_RowDataBound" >
                    <Columns>
                         <asp:BoundField DataField="BAKASHA_ID"    HeaderText="מספר בקשה"  ItemStyle-Width="59px" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                         <asp:BoundField DataField="TEUR"          HeaderText="תאור"       ItemStyle-Width="440px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                         <asp:BoundField DataField="ZMAN_HATCHALA" HeaderText="תחילת הפקה" ItemStyle-Width="198px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncodeFormatString="true"/>
                         <asp:BoundField DataField="ZMAN_Siyum"    HeaderText="סיום הפקה" ItemStyle-Width="198px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"   DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncodeFormatString="true"/>
                         <asp:BoundField DataField="SHEM_TIKIYA"  ItemStyle-Width="0px" />
                         <asp:BoundField DataField="EXTENSION_TYPE"  ItemStyle-Width="0px"  />                     
                       
                         <asp:TemplateField HeaderText="קובץ"  ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  >
                             <ItemTemplate>
                               <asp:HyperLink  ID="imgButton" runat="server" NavigateUrl="#" ></asp:HyperLink>
                             </ItemTemplate>           
                         </asp:TemplateField>  
                      </Columns> 
                      <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                        </PagerTemplate>
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



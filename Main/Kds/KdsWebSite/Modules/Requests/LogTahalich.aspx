<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LogTahalich.aspx.cs" Inherits="Modules_Requests_LogTahalich" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">

   <fieldset dir="rtl"  style="text-align:right;width:965px" > 
       <legend>סינון לפי</legend>  <br />    
     <table  border="0"  style="text-align:right;width:965px">
        <tr valign="top">
        <td class="InternalLabel">
                    מתאריך:
                </td> 
                <td align="right" style="width:140px;">     
                   <KdsCalendar:KdsCalendar runat="server" ID="clnFromDate"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>           
                 
                </td> 
                 <td style="width:10px"></td>
            <td class="InternalLabel">
           עד תאריך:
                </td>
                 <td style="width:140px;">
                 <KdsCalendar:KdsCalendar runat="server" ID="clnToDate"   AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>  <br />         
                 <asp:CompareValidator ID="vldComDate" runat="server" ErrorMessage="עד תאריך קטן ממתאריך"  CssClass="ErrorMessage" Operator="GreaterThanEqual" ControlToCompare="clnFromDate" ControlToValidate="clnToDate" Display="Dynamic"></asp:CompareValidator>
                </td>
                 <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:50px">
                    תהליך:
                </td>
                <td align="right">                  
                 <asp:DropDownList ID="ddlProcess" runat="server"></asp:DropDownList>
                 </td>  
                 <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:50px">
                    סטטוס:
                </td>
                <td align="right">                  
                 <asp:DropDownList ID="ddlStatus" runat="server">
                  </asp:DropDownList>
                 </td>  
                
                <td style="width:20px"></td>
                <td> 
                    <asp:UpdatePanel ID="upBtnShow" runat="server" RenderMode="Inline"  >
                      <ContentTemplate> 
                            <asp:button ID="btnShow" runat="server" text="הצג" CssClass ="ImgButtonSearch"  onclick="btnShow_Click"  />
                     </ContentTemplate>
                  </asp:UpdatePanel> 
            </td> 
        </tr>                
    </table>  

 </fieldset>

<asp:UpdatePanel ID="upDivNetunim" runat="server" RenderMode="Inline">
    <ContentTemplate> 
         <div id="divNetunim" runat="server" dir="rtl"  style="text-align:right;width:965px">
             <br /> 
                <asp:GridView ID="grdLogTahalich" runat="server"  GridLines="None"  ShowHeader="true"
                       CssClass="Grid"   AllowPaging="true" AutoGenerateColumns="false"   AllowSorting="true"
                       Width="965px"   EmptyDataText="לא נמצאו נתונים!"   PageSize="15" 
                     EmptyDataRowStyle-CssClass="GridHeader" onsorting="grdLogTahalich_Sorting" 
                     onrowdatabound="grdLogTahalich_RowDataBound" 
                 onpageindexchanging="grdLogTahalich_PageIndexChanging">
                    <Columns>
                         <asp:BoundField DataField="TAARICH" HeaderText="תאריך התחלה" SortExpression="TAARICH" DataFormatString="{0:dd/MM/yyyy  HH:mm:ss}" HtmlEncodeFormatString="true"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                          <asp:BoundField DataField="TEUR_TAHALICH" HeaderText="תהליך" SortExpression="TEUR_TAHALICH"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:BoundField DataField="TEUR_PEILUT_BE_TAHALICH" HeaderText="פעילות בתהליך" SortExpression="TEUR_PEILUT_BE_TAHALICH"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:BoundField DataField="TEUR_STATUS_BAKASHA" HeaderText="סטטוס" SortExpression="TEUR_STATUS_BAKASHA" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader"  />
                          <asp:BoundField DataField="TAARICH_SGIRA" HeaderText="תאריך סיום" SortExpression="TAARICH_SGIRA" DataFormatString="{0:dd/MM/yyyy  HH:mm:ss}" HtmlEncodeFormatString="true"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:BoundField DataField="TEUR_TAKALA" HeaderText="תאור התקלה" SortExpression="TEUR_TAKALA" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:BoundField DataField="TEUR_TECH" HeaderText="תאור טכני" SortExpression="TEUR_TECH" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Left"/>        
                      </Columns> 
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                        <RowStyle CssClass="GridRow"   />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                        <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/>  
                 </asp:GridView>
         
        </div>
                   
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PirteyOved.ascx.cs" Inherits="Modules_UserControl_PirteyOved" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />

<asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
 <ContentTemplate>
    <div id="divNetunim" runat="server"  style="text-align:right;width:945px">
        <br />
            <span class="TitleLable">פרטי התקשרות:</span><br />
           
            <table width="945px"  class="Grid" cellpadding="1" cellspacing="0">
                <tr  class="GridHeader" >
                    <td class="ItemRow" style="width:250px;">כתובת</td>
                    <td class="ItemRow" style="width:100px;">טל' עבודה</td>
                    <td class="ItemRow" style="width:100px;">טל' בית</td>
                    <td class="ItemRow" style="width:100px;">נייד</td>
                    <td class="ItemRow">אימייל</td>
                </tr>
                <tr class="GridAltRow"  align="center">
                    <td class="ItemRow"><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblTelAvoda" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblTelHome" runat="server"></asp:Label></td>
                   <td class="ItemRow"><asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                     <td class="ItemRow" ><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                </tr>
            </table>
          <br />
             <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate> 
                      <div runat="server" ID="pnlContainerDetails" style="Height:120px;Width:947px;overflow:auto;direction:ltr; position: relative;">
                        <asp:GridView ID="grdPirteyOved" runat="server"  GridLines="None"  ShowHeader="true"
                               CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="true"
                               Width="930px"   EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader" 
                              onrowdatabound="grdPirteyOved_RowDataBound" onsorting="grdPirteyOved_Sorting">
                            <Columns>
                                <asp:BoundField DataField="kod_natun" HeaderText="קוד נתון"  ItemStyle-Width="145px"  SortExpression="kod_natun" HeaderStyle-CssClass="GridHeader"  ItemStyle-CssClass="ItemRow"/>
                                <asp:BoundField DataField="teur_natun" HeaderText="תאור נתון"  SortExpression="teur_natun" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="165px" ItemStyle-CssClass="ItemRow" />
                                <asp:BoundField DataField="ME_TAARICH" HeaderText="מתאריך" SortExpression="ME_TAARICH" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="115px"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                <asp:BoundField DataField="AD_TAARICH" HeaderText="עד תאריך" SortExpression="AD_TAARICH" ItemStyle-Width="115px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                <asp:BoundField DataField="kod_erech" HeaderText="ערך" SortExpression="kod_erech" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                <asp:BoundField DataField="teur_erech" HeaderText=" תאור ערך" SortExpression="teur_erech" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                             </Columns> 
                            <RowStyle CssClass="GridAltRow"   />
                               
                         </asp:GridView>
                              <asp:Button runat="server" style="display:none"  ID="btnBindHistory" 
                                onclick="btnBindHistory_Click" />
                                <input type="hidden" runat="server" id="txtRowSelected" /> 
        
                      </div>
                    </ContentTemplate>
               </asp:UpdatePanel>  
               
              <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline"  UpdateMode="Conditional">
                <ContentTemplate>     
                        <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" 
                            TargetControlID="ContentPanel"
                            ExpandControlID="TitlePanel" 
                            CollapseControlID="TitlePanel" 
                            Collapsed="True"
                            TextLabelID="Label1" 
                            CollapsedSize="0"
                             ExpandedText="הסתר נתונים הסטוריים לנתון..." 
                            CollapsedText="הצג נתונים היסטוריים לנתון..."
                            ImageControlID="Image1" 
                            ExpandedImage="~/images/collapse_blue.jpg" 
                            CollapsedImage="~/images/expand_blue.jpg"
                            SuppressPostBack="True" 
                            ExpandDirection="Vertical"      
                            ScrollContents="false">
                        </cc1:CollapsiblePanelExtender>
                        <asp:Panel ID="TitlePanel" runat="server" CssClass="collapsePanel" Width="210px"> 
                               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg"/>&nbsp;&nbsp;
                               <asp:Label ID="Label1" runat="server" Font-Underline="true">הצג נתונים היסטוריים לנתון...</asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="ContentPanel" runat="server" Width="700px" Height="0px" style="overflow: hidden;">
                            <table border="0"  >
                                <tr>
                                    <td> <span class="TitleLable"> נתונים היסטוריים לנתון:</span> </td>
                                    <td></td>
                                      <td class="InternalLabel">קוד נתון:</td>
                                             <td>  
                                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                                    <ContentTemplate> 
                                                    <asp:TextBox ID="txtCodeNatun" runat="server" AutoComplete="Off" dir="rtl"  AutoPostBack="true"
                                                        Width="60px"  ontextchanged="txtCodeNatun_TextChanged" EnableViewState="true"></asp:TextBox>                            
                                                    <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                        TargetControlID="txtCodeNatun" MinimumPrefixLength="1" ServiceMethod="GetTeurByKod" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"   EnableViewState="true"
                                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                        CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientItemSelected="ItemSelectedDetails" >                               
                                                    </cc1:AutoCompleteExtender>   
                                                                                     
                                                   </ContentTemplate>
                                             </asp:UpdatePanel> 
                                           </td>
                                          <td width="10px"></td>
                                          <td class="InternalLabel">תאור:</td>
                                          <td>  
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline"  >
                                                    <ContentTemplate> 
                                                        <asp:TextBox ID="txtTeurNatun" runat="server" AutoComplete="Off" dir="rtl"
                                                            Width="180px" AutoPostBack="true" ontextchanged="txtTeurNatun_TextChanged" EnableViewState="true"  ></asp:TextBox>                            
                                                        <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                            TargetControlID="txtTeurNatun" MinimumPrefixLength="1" ServiceMethod="GetNatunByTeur" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                            EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"  EnableViewState="true"
                                                            CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                            CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientItemSelected="ItemSelectedDetails" >                               
                                                        </cc1:AutoCompleteExtender>                              
                                                   </ContentTemplate>
                                              </asp:UpdatePanel>
                                            </td>
                                </tr>
                                  <tr>
                                    <td colspan="7">
                                    <asp:UpdatePanel ID="upGridHistory" runat="server" RenderMode="Inline">
                                      <ContentTemplate> 
                                         <div runat="server" ID="pnlgrdHistoria" style="Height:120px;Width:567px;overflow:auto;direction:ltr;">
                                            <asp:GridView ID="grdHistoriatNatun" runat="server"  GridLines="None"  ShowHeader="true"
                                                   CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="false"
                                                   Width="550px"   EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader">
                                                <Columns>
                                                     <asp:BoundField DataField="ME_TAARICH" HeaderText="מתאריך" SortExpression="ME_TAARICH" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="90px"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                                    <asp:BoundField DataField="AD_TAARICH" HeaderText="עד תאריך" SortExpression="AD_TAARICH" ItemStyle-Width="90px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                                    <asp:BoundField DataField="kod_erech" HeaderText="ערך" SortExpression="kod_erech" ItemStyle-Width="80px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                                    <asp:BoundField DataField="teur_erech" HeaderText="תאור ערך" SortExpression="teur_erech" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                                                 </Columns> 
                                                <RowStyle CssClass="GridAltRow"   />
                                             </asp:GridView>
                                         </div>
                                         </ContentTemplate>
                                     </asp:UpdatePanel>
                                </td>
                            </tr>
                       </table>
                   
                   </asp:Panel>  
              </ContentTemplate>
            </asp:UpdatePanel>     
          </div>
    </ContentTemplate>
</asp:UpdatePanel> 
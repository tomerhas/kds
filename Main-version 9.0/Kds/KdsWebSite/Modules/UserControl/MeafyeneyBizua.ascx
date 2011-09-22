<%@ Control  Language="C#" AutoEventWireup="true" CodeFile="MeafyeneyBizua.ascx.cs" Inherits="Modules_UserControl_MeafyeneyBizua"  %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />
 
   <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
     <ContentTemplate>
         <div id="divNetunim" runat="server"  style="text-align:right;width:945px">
          <br />
            <span class="TitleLable">פרטי העובד:</span><br />
           
            <table width="945px"  class="Grid" cellpadding="1" cellspacing="0">
                <tr  class="GridHeader" >
                    <td class="ItemRow">מספר אישי</td>
                    <td class="ItemRow">שם משפחה</td>
                    <td class="ItemRow">שם פרטי</td>
                    <td class="ItemRow" style="width:80px;">מעמד</td>
                     <td class="ItemRow" style="width:180px;">עיסוק</td>
                     <td class="ItemRow" style="width:180px;">סניף/יחידה</td>
                    <td class="ItemRow" style="width:60px;">אזור</td>
                    <td class="ItemRow">קוד גיל</td>
                    <td class="ItemRow" style="width:110px;">ת. תחילת עבודה</td>
                  
                </tr>
                <tr class="GridAltRow">
                    <td class="ItemRow"><asp:Label ID="lblEmployeId" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblLastName" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                   <td class="ItemRow"><asp:Label ID="lblMaamad" runat="server"></asp:Label></td>
                     <td class="ItemRow"><asp:Label ID="lblIsuk" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblSnif" runat="server"></asp:Label></td>
                     <td class="ItemRow"> <asp:Label ID="lblEzor" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblGil" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblDateStartWork" runat="server"></asp:Label></td>
                </tr>
            </table>
            <br />
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate> 
                     <span class="TitleLable">מאפייני ביצוע:</span><br />
                     <div runat="server" ID="pnlContainer" style="Height:185px;Width:947px;overflow:auto;direction:ltr; position: relative;">
                        <asp:GridView ID="grdMeafyeneyBitzua" runat="server"  GridLines="None"  ShowHeader="true"
                               CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="true"
                               Width="930px"   EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader" 
                              onrowdatabound="grdMeafyeneyBitzua_RowDataBound" onsorting="grdMeafyeneyBitzua_Sorting">
                            <Columns>
                                <asp:BoundField DataField="kod_meafyen" HeaderText="קוד"  ItemStyle-Width="145px"  SortExpression="kod_meafyen" HeaderStyle-CssClass="GridHeader"  ItemStyle-CssClass="ItemRow"/>
                                <asp:BoundField DataField="teur_MEAFYEN_BITZUA" HeaderText="תאור"  SortExpression="teur_MEAFYEN_BITZUA" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="165px" ItemStyle-CssClass="ItemRow" />
                                <asp:BoundField DataField="ME_TAARICH" HeaderText="מתאריך" SortExpression="ME_TAARICH" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="115px"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                <asp:BoundField DataField="AD_TAARICH" HeaderText="עד תאריך" SortExpression="AD_TAARICH" ItemStyle-Width="115px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                <asp:BoundField DataField="Erech_ishi" HeaderText="ערך מאפיין" SortExpression="value_Erech_ishi" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                <asp:BoundField DataField="YECHIDA" HeaderText="יחידה" SortExpression="YECHIDA" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                             </Columns> 
                            <RowStyle CssClass="GridAltRow"   />
                               
                         </asp:GridView>
                      </div>
                        <asp:Button runat="server" style="display:none"  ID="btnBindHistory" 
                                onclick="btnBindHistory_Click" />
                                <input type="hidden" runat="server" id="txtRowSelected" /> 
                          
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
                             ExpandedText="הסתר נתונים הסטוריים למאפיין..." 
                            CollapsedText="הצג נתונים היסטוריים למאפיין..."
                            ImageControlID="Image1" 
                            ExpandedImage="~/images/collapse_blue.jpg" 
                            CollapsedImage="~/images/expand_blue.jpg"
                            SuppressPostBack="True" 
                            ExpandDirection="Vertical"      
                            ScrollContents="false">
                        </cc1:CollapsiblePanelExtender>
                        <asp:Panel ID="TitlePanel" runat="server" CssClass="collapsePanel" Width="220px"  > 
                               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg"/>&nbsp;&nbsp;
                               <asp:Label ID="Label1" runat="server" Font-Underline="true">הצג נתונים היסטוריים למאפיין...</asp:Label>
                        </asp:Panel>
                         <asp:Panel ID="ContentPanel" runat="server" Width="700px" Height="0px" style="overflow: hidden;">
                            <table border="0" >
                                <tr>
                                    <td> <span class="TitleLable"> נתונים היסטוריים למאפיין:</span> </td>
                                    <td></td>
                                      <td class="InternalLabel">קוד:</td>
                                             <td>  
                                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" >
                                                    <ContentTemplate> 
                                                    <asp:TextBox ID="txtCodeMeafyen" runat="server" AutoComplete="Off" dir="rtl"  AutoPostBack="true"
                                                        Width="70px"   ontextchanged="txtCodeMeafyen_TextChanged"  EnableViewState="true" ondbclick="this.select();"></asp:TextBox>                            
                                                    <cc1:AutoCompleteExtender    id="AutoCompleteCodeMeafyen" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                        TargetControlID="txtCodeMeafyen" MinimumPrefixLength="1" ServiceMethod="GetMeafyenyeBitzuaCode" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                        EnableCaching="false"  CompletionListCssClass="autocomplete_completionListElement"    EnableViewState="true"
                                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                        CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientItemSelected="ItemSelected">                               
                                                    </cc1:AutoCompleteExtender>   
                                                                                     
                                                   </ContentTemplate>
                                             </asp:UpdatePanel> 
                                           </td>
                                          <td width="10px"></td>
                                          <td class="InternalLabel">תאור:</td>
                                          <td>  
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                                                    <ContentTemplate> 
                                                        <asp:TextBox ID="txtTeurMeafyen" runat="server" AutoComplete="Off" dir="rtl"
                                                            Width="180px" AutoPostBack="true" ontextchanged="txtTeurMeafyen_TextChanged" EnableViewState="true"  ></asp:TextBox>                            
                                                        <cc1:AutoCompleteExtender id="AutoCompleteTeurMeafyen" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                            TargetControlID="txtTeurMeafyen" MinimumPrefixLength="1" ServiceMethod="GetMeafyenyeBitzuaTeur" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                            EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"  EnableViewState="true"
                                                            CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                            CompletionListItemCssClass="autocomplete_completionListItemElement"  OnClientItemSelected="ItemSelected">                               
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
                                            <asp:GridView ID="grdHistoriatMeafyen" runat="server"  GridLines="None"  ShowHeader="true"
                                                   CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="false"
                                                   Width="550px"   EmptyDataText="לא נמצאו נתונים!" 
                                                EmptyDataRowStyle-CssClass="GridHeader"  onrowdatabound="grdHistoriatMeafyen_RowDataBound">
                                                <Columns>
                                                     <asp:BoundField DataField="ME_TAARICH" HeaderText="מתאריך" SortExpression="ME_TAARICH" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="115px"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                                    <asp:BoundField DataField="AD_TAARICH" HeaderText="עד תאריך" SortExpression="AD_TAARICH" ItemStyle-Width="115px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                                                    <asp:BoundField DataField="Erech_ishi" HeaderText="ערך מאפיין" SortExpression="Erech_ishi" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                                    <asp:BoundField DataField="YECHIDA" HeaderText="יחידה" SortExpression="YECHIDA" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
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

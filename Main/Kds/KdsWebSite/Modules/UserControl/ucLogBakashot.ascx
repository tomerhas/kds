<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLogBakashot.ascx.cs" Inherits="Modules_UserControl_ucLogBakashot" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

   <link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />

   <fieldset dir="rtl"  style="text-align:right;width:965px" > 
       <legend>סינון לפי</legend>  <br />    
     <table  border="0"  style="text-align:right;width:965px">
        <tr valign="top">
        <td class="InternalLabel" style="width:30px">
                    חודש:
                </td> 
                <td align="right">     
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server"  RenderMode="Inline">
                        <ContentTemplate>                
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="80px"  AutoPostBack="true"
                                onselectedindexchanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                          </ContentTemplate>
                      </asp:UpdatePanel> 
                </td> 
                 <td style="width:10px"></td>
            <td class="InternalLabel" style="width:80px">
           מספר בקשה:
                </td>
                 <td style="width:60px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server"  RenderMode="Inline">
                        <ContentTemplate> 
                            <asp:TextBox ID="txtRequestNum" runat="server" AutoComplete="Off" dir="rtl"
                                Width="50px" EnableViewState="true"  AutoPostBack="true" 
                                ontextchanged="txtRequestNum_TextChanged" ></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="autoComRequestNum" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"
                                TargetControlID="txtRequestNum" MinimumPrefixLength="1" ServiceMethod="GetRequestList" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="false"  CompletionListCssClass="autocomplete_completionListElement"  EnableViewState="true"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"  >                               
                            </cc1:AutoCompleteExtender>   
                       </ContentTemplate>
                  </asp:UpdatePanel> 
                       <asp:CompareValidator ID="vldRequestNum" runat="server" Display="Static" ErrorMessage="מספר בקשה לא חוקי" Type="Integer" CssClass="ErrorMessage" Operator="DataTypeCheck" ControlToValidate="txtRequestNum"></asp:CompareValidator>                           
                </td>
                 <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:80px">
                    סוג בקשה:
                </td>
                <td align="right">                  
                 <asp:DropDownList ID="ddlRequestType" runat="server"></asp:DropDownList>
                 </td>  
                 <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:80px">
                    סוג הודעה:
                </td>
                <td align="right">                  
                 <asp:DropDownList ID="ddlTypeMessage" runat="server">
                    <asp:ListItem Value="0" Text="הכל"></asp:ListItem>
                    <asp:ListItem Value="E" Text="שגיאה"></asp:ListItem>
                    <asp:ListItem Value="I" Text="אינפורמטיבית"></asp:ListItem>
                    <asp:ListItem Value="W" Text="התראה"></asp:ListItem>
                 </asp:DropDownList>
                 </td>  
                  <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:80px">
                        מספר אישי:
                </td>
                 <td  style="width:70px">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server"  RenderMode="Inline">
                        <ContentTemplate> 
                            <asp:TextBox ID="txtMisparIshi" runat="server" AutoComplete="Off" dir="rtl"
                                Width="65px" EnableViewState="true"  ></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="autoComMisparIshi" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"
                                TargetControlID="txtMisparIshi" MinimumPrefixLength="1" ServiceMethod="GetOvdimFromLogBakashot" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="false"  CompletionListCssClass="autocomplete_completionListElement"  EnableViewState="true"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"  >                               
                            </cc1:AutoCompleteExtender>   
                       </ContentTemplate>
                  </asp:UpdatePanel> 
                       <asp:CompareValidator ID="vldMisparIshi" runat="server" Display="Static" ErrorMessage="מספר אישי לא חוקי" Type="Integer" CssClass="ErrorMessage" Operator="DataTypeCheck" ControlToValidate="txtMisparIshi"></asp:CompareValidator>                           
               
                </td>
                <td style="width:40px"></td>
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
                <asp:GridView ID="grdLogRequest" runat="server"  GridLines="None"  ShowHeader="true"
                       CssClass="Grid"   AllowPaging="true" AutoGenerateColumns="false"   AllowSorting="true"
                       Width="965px"   EmptyDataText="לא נמצאו נתונים!"   PageSize="7" 
                     EmptyDataRowStyle-CssClass="GridHeader" onsorting="grdLogRequest_Sorting" 
                     onrowdatabound="grdLogRequest_RowDataBound" 
                 onpageindexchanging="grdLogRequest_PageIndexChanging">
                    <Columns>
                         <asp:BoundField DataField="sug_hodaa" HeaderText="סוג הודעה" SortExpression="sug_hodaa" ItemStyle-Width="65px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                          <asp:BoundField DataField="bakasha_id" HeaderText="מספר בקשה" SortExpression="bakasha_id, TAARICH_IDKUN_ACHARON" ItemStyle-Width="50px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:BoundField DataField="TEUR_SUG_BAKASHA" HeaderText="סוג בקשה" SortExpression="TEUR_SUG_BAKASHA" ItemStyle-Width="60px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-BorderWidth="1px"  />
                         <asp:BoundField DataField="TEUR_HODAA" HeaderText="תאור" SortExpression="TEUR_HODAA" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="TAARICH_IDKUN_ACHARON" HeaderText="תאריך עדכון" SortExpression="TAARICH_IDKUN_ACHARON" ItemStyle-Width="80px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy  HH:mm:ss}" HtmlEncodeFormatString="true"/>
                         <asp:BoundField DataField="KOD_YESHUT" HeaderText="קוד ישות" SortExpression="KOD_YESHUT" ItemStyle-CssClass="ItemRow" ItemStyle-Width="50px" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="mispar_ishi" HeaderText="מ. אישי" SortExpression="mispar_ishi" ItemStyle-CssClass="ItemRow" ItemStyle-Width="50px" HeaderStyle-CssClass="GridHeader" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="taarich" HeaderText="תאריך" SortExpression="taarich" ItemStyle-Width="75px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"   DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                          <asp:BoundField DataField="MISPAR_SIDUR" HeaderText="מספר סידור" SortExpression="MISPAR_SIDUR" ItemStyle-CssClass="ItemRow" ItemStyle-Width="80px" HeaderStyle-CssClass="GridHeader" />
                         <asp:BoundField DataField="SHAT_HATCHALA_SIDUR" HeaderText="שעת התחלת סידור" SortExpression="SHAT_HATCHALA_SIDUR" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" DataFormatString="{0:dd/MM/yyyy  HH:mm}" HtmlEncodeFormatString="true" ItemStyle-BorderWidth="1px" ItemStyle-Width="80px" />
                         <asp:BoundField DataField="SHAT_YETZIA" HeaderText="שעת יציאת פעילות" SortExpression="SHAT_YETZIA" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" DataFormatString="{0:dd/MM/yyyy  HH:mm}" HtmlEncodeFormatString="true"  ItemStyle-Width="80px" />
                     
                      </Columns> 
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                        <RowStyle CssClass="GridRow"   />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                        <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/>  
                        <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                        </PagerTemplate>  
                 </asp:GridView>
         
        </div>
                   
    </ContentTemplate>
</asp:UpdatePanel>
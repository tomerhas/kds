<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewRikuzim.aspx.cs" Inherits="Modules_Rikuzim_ViewRikuzim" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">


    <div>
        <table>
            <tr>
                <td colspan="2">
                     <fieldset class="FilterFieldSet" style="width:950px"> 
                            <legend>סינון לפי</legend>      <br />
                            <table style="margin-top:4px;height:30px" border="0">
                            <tr valign="top">
                                <td class="InternalLabel" style="width:88px">
                                מספר בקשה:
                                    </td>
                                        <td style="width:100px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  RenderMode="Inline">
                                            <ContentTemplate> 
                                                <asp:TextBox ID="txtRequestNum" runat="server" AutoComplete="Off" dir="rtl"
                                                    Width="80px" EnableViewState="true"  ></asp:TextBox>                            
                                                <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"
                                                    TargetControlID="txtRequestNum" MinimumPrefixLength="1" ServiceMethod="GetRequestList" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"  EnableViewState="true"
                                                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement"  >                               
                                                </cc1:AutoCompleteExtender>   
                                            </ContentTemplate>
                                        </asp:UpdatePanel> 
                                            <asp:CompareValidator ID="vldRequestNum" runat="server" Display="Static" ErrorMessage="מספר בקשה לא חוקי" Type="Integer" CssClass="ErrorMessage" Operator="DataTypeCheck" ControlToValidate="txtRequestNum"></asp:CompareValidator>                           
                                    </td>

                                    <td style="width:60px"></td>
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
                </td>
            </tr>
            <tr>
                 <td align="right">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                   <ContentTemplate> 
                     <div id="divNetunim" runat="server" onscroll="FreezeHeader(this)" dir="rtl" style="text-align:right;width:965px;overflow-x:hidden;">
                        <asp:GridView ID="grdRikuzim" runat="server" AllowSorting="true" 
                                   AutoGenerateColumns="false" PageSize="9" CssClass="Grid"  
                                 Width="200px" EmptyDataText="לא נמצאו נתונים!" ShowHeader="true" 
                                 OnRowDataBound="grdRikuzim_RowDataBound" >                                 
                                <Columns>  
                                     <asp:HyperLinkField DataTextField="MISPAR_ISHI" ItemStyle-CssClass="ItemRow"   ItemStyle-Font-Size="Larger" HeaderStyle-CssClass="GridHeader"  HeaderStyle-Width="250px"  HeaderText="מספר אישי" SortExpression="taarich" NavigateUrl="#"  />                                                                                                    
                                     <asp:BoundField DataField="TAARICH" ItemStyle-CssClass="ItemRow" />
                                </Columns>
                                <AlternatingRowStyle CssClass="GridAltRow" Height="25px" />
                                <RowStyle CssClass="GridRow" Height="25px" />
                                <PagerStyle CssClass="GridPagerLarge" HorizontalAlign="Center"  />                          
                                <EmptyDataRowStyle CssClass="GridEmptyData" height="10px" Wrap="False"/> 
                               <%-- <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                                </PagerTemplate> --%>                                                  
                         </asp:GridView>
                        </div> 
                         <input type="hidden" runat="server" id="txtPageIndex" value="0"/>  
                     </ContentTemplate>
                     <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnExecute" />                     
                     </Triggers> --%>
                  </asp:UpdatePanel>       
               </td>
                <td>
                      <iframe runat="server" id="iFramePrint" style="display:none" height="5px" width="5px"></iframe>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
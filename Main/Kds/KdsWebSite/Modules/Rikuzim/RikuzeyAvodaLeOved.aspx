<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AsyncTimeout="1500"  AutoEventWireup="true" CodeFile="RikuzeyAvodaLeOved.aspx.cs" Inherits="Modules_Ovdim_RikuzeyAvodaLeOved" Title="ריכוזי עבודה" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
  
   <script type="text/javascript" language="javascript">
       var oTxtId = "<%=txtEmpId.ClientID%>";
       var oTxtName = "<%=txtName.ClientID%>";
       var flag = false;
       var userId = iUserId;
</script>
<div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:2000;width:150px" >
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> 
</div>  
    <fieldset class="FilterFieldSet" style="width:950px"> 
       <legend> פרטי עובד </legend>   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  RenderMode="Inline">
          <ContentTemplate>    
              <table style="margin-top:4px">
                <tr>
                    <td class="InternalLabel" style="width:90px">
                        <asp:RadioButton runat="server" Checked="true" ID="rdoId"  EnableViewState="true" GroupName="grpSearch" Text="מספר אישי:"  > </asp:RadioButton>
                    </td>
                    <td dir="rtl">
                            <asp:TextBox ID="txtEmpId" runat="server" AutoComplete="Off" dir="rtl"  onchange="GetOvedNameById();"
                                Width="55px"  EnableViewState="true" onfocus="this.select();"></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                TargetControlID="txtEmpId" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"  FirstRowSelected="true"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"  
                               OnClientHidden="SimunExtendeIdClose"  OnClientShowing="SimunExtendeOpen"  >  
                            </cc1:AutoCompleteExtender>                        
                    </td>
                    <td style="width:10px"></td>
                    <td class="InternalLabel" style="width:60px">                       
                         <asp:RadioButton runat="server" ID="rdoName" EnableViewState="true" GroupName="grpSearch" Text="שם:" > </asp:RadioButton>
                    </td>   
                    <td style="width:120px">
                            <asp:TextBox ID="txtName" runat="server"  onchange="GetOvedIdByName();"  AutoComplete="Off" style="width:120px" onfocus="this.select();" EnableViewState="true"  ></asp:TextBox>
                          
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                        TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"  EnableViewState="true"
                                           CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select" 
                                        CompletionListItemCssClass="autocomplete_completionListItemElement"
                                       OnClientHidden="SimunExtendeNameClose"  OnClientShowing="SimunExtendeOpen"  >   
                            </cc1:AutoCompleteExtender> 
                    </td>
                    <td style="width:10px"></td>
                    <td  class="InternalLabel">חודש:</td>
                    <td> 
                             <asp:DropDownList id="ddlMonth" runat="server" AutoPostBack="false"  Width="45px" ></asp:DropDownList>
                    
                    </td>
                    <td style="width:10px"></td>
                    <td  class="InternalLabel"> שנה:</td>
                    <td>
                        <asp:DropDownList id="ddlYears" runat="server"   Width="80px" ></asp:DropDownList>
                    </td>
                    <td style="width:10px"></td>
                    <td> 
                        <asp:button ID="btnShow" runat="server"  text="הצג" CssClass ="ImgButtonSearch"  onclick="btnShow_Click" OnClientClick=" if (document.getElementById('ctl00_KdsContent_txtEmpId').value.length == 0) {alert('יש להזין מספר אישי'); return false;} else {return true;}" />
                       <%--     <asp:Button ID="btnHidden" runat="server" OnClick="btnHidden_OnClick"  />--%>
                    </td>
                 </tr>                
            </table>  

   </ContentTemplate>
  </asp:UpdatePanel> 
 </fieldset>
 <br />
   <table  width="952px">
        <tr>
            <td align="right">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                   <ContentTemplate> 
                     <div id="divNetunim" runat="server" onscroll="FreezeHeader(this)" dir="rtl" style="text-align:right;width:965px;overflow-x:hidden;">
                        <asp:GridView ID="grdRikuzim" runat="server" AllowSorting="true" 
                                   AutoGenerateColumns="false" PageSize="9" CssClass="Grid"  
                                 Width="950px" EmptyDataText="לא נמצאו נתונים!" ShowHeader="true" 
                                 OnRowDataBound="grdRikuzim_RowDataBound" OnSorting="grdRikuzim_Sorting" >                                 
                               <Columns>                                                            
                                     <asp:BoundField DataField="BAKASHA_ID" SortExpression="BAKASHA_ID" ItemStyle-CssClass="WorkCardHosafatSidurGridItm"  />
                                     <asp:BoundField DataField="TAARICH"  SortExpression="TAARICH"  HeaderText="חודש"  ItemStyle-Width="150px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:MM/yyyy}" HtmlEncodeFormatString="true"/>
                                     <asp:BoundField DataField="SUG_CHISHUV"   SortExpression="SUG_CHISHUV"       HeaderText="סוג ריצה"       ItemStyle-Width="100px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                     <asp:BoundField DataField="TAARICH_HAAVARA_LESACHAR"  SortExpression="TAARICH_HAAVARA_LESACHAR"   HeaderText="תאריך העברה לשכר" ItemStyle-Width="150px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncodeFormatString="true"/>                                  
                                     <asp:TemplateField HeaderText="PDF"  ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  >
                                         <ItemTemplate>
                                               <asp:HyperLink  ID="imgButton" runat="server"   NavigateUrl="#" ></asp:HyperLink>
                                          </ItemTemplate>           
                                     </asp:TemplateField>     
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
        </tr>
     </table>

  
     <iframe runat="server" id="iFramePrint" style="display:none" height="5px" width="5px"></iframe>
<script language="javascript" type="text/javascript">
    function continue_click() {
            document.getElementById("ctl00_KdsContent_btnShow").disabled = false;
        }
        function onclick_ShowReport(mispar_ishi, bakasha_id, taarich) {
            var sQueryString = "?EmpID=" + mispar_ishi + "&bakashaID=" + bakasha_id + "&taarich=" + taarich;
            document.all.ctl00_KdsContent_iFramePrint.src = "ShowRikuz.aspx" + sQueryString;
       //     window.open('ShowRikuz.aspx' + sQueryString);
           // window.showModalDialog('ShowRikuz.aspx' + sQueryString, '', 'dialogwidth:900px;dialogheight:680px;dialogtop:280px;dialogleft:480px;status:no;resizable:yes;');
        }
   </script>

</asp:Content>



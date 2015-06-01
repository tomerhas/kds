<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Modules_Requests_MaakavBakashot" Codebehind="MaakavBakashot.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
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
                                EnableCaching="true"  CompletionListCssClass="ACLst"  EnableViewState="true"
                                CompletionListHighlightedItemCssClass="ACLstItmSel"
                                CompletionListItemCssClass="ACLstItmE"  >                               
                            </cc1:AutoCompleteExtender>   
                       </ContentTemplate>
                  </asp:UpdatePanel> 
                       <asp:CompareValidator ID="vldRequestNum" runat="server" Display="Static" ErrorMessage="מספר בקשה לא חוקי" Type="Integer" CssClass="ErrorMessage" Operator="DataTypeCheck" ControlToValidate="txtRequestNum"></asp:CompareValidator>                           
                </td>
                 <td style="width:20px"></td>
                 <td class="InternalLabel" style="width:80px">
                    סוג בקשה:
                </td>
                <td align="right">                  
                 <asp:DropDownList ID="ddlRequestType" runat="server"></asp:DropDownList>
                 </td>  
                 <td style="width:20px"></td>
                <td class="InternalLabel" style="width:40px">
                    חודש:
                </td> 
                <td align="right">                    
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList>
                </td> 
                 <td style="width:20px"></td>
                 <td class="InternalLabel" style="width:50px">
                        סטטוס:
                </td>
                 <td  style="width:90px">
                  <asp:DropDownList ID="ddlStatus" runat="server"  Width="100px"></asp:DropDownList>
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

<asp:UpdatePanel ID="upDivNetunim" runat="server"  RenderMode="Inline">
    <ContentTemplate> 
         <div id="divNetunim" runat="server"   style="text-align:right;"    >
             <br /> 
             <div runat="server" ID="pnlgrdRequest" style="Height:460px;Width:950px;overflow:auto;direction:ltr;">
                <asp:GridView ID="grdRequest" runat="server"  GridLines="None"  ShowHeader="true" 
                       CssClass="Grid"  AllowPaging="true"  PageSize="10"  AutoGenerateColumns="false"   AllowSorting="true"
                       Width="950px"   EmptyDataText="לא נמצאו נתונים!" 
                     EmptyDataRowStyle-CssClass="GridHeader" onsorting="grdRequest_Sorting" 
                     onrowdatabound="grdRequest_RowDataBound">
                    <Columns>
                         <asp:BoundField DataField="TEUR_STATUS_BAKASHA" HeaderText="סטטוס" SortExpression="TEUR_STATUS_BAKASHA" ItemStyle-Width="45px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:HyperLinkField  DataTextField="bakasha_id" NavigateUrl="#"  HeaderText="מספר בקשה" SortExpression="bakasha_id" ItemStyle-Width="50px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" />
                         <asp:BoundField DataField="TEUR_SUG_BAKASHA" HeaderText="סוג בקשה" SortExpression="TEUR_SUG_BAKASHA" ItemStyle-Width="80px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                         <asp:BoundField DataField="TEUR" HeaderText="תאור" SortExpression="TEUR" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" ItemStyle-BorderWidth="1px"/>
                         <asp:BoundField DataField="ZMAN_HATCHALA" HeaderText="זמן תחילת ריצה" SortExpression="ZMAN_HATCHALA" ItemStyle-Width="80px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy  HH:mm:ss}" HtmlEncodeFormatString="true"/>
                         <asp:BoundField DataField="ZMAN_Siyum" HeaderText="זמן סיום ריצה" SortExpression="ZMAN_Siyum" ItemStyle-Width="80px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"   DataFormatString="{0:dd/MM/yyyy  HH:mm:ss}" HtmlEncodeFormatString="true"/>
                          <asp:BoundField DataField="USER_NAME" HeaderText="שם מפעיל הבקשה" SortExpression="USER_NAME" ItemStyle-CssClass="ItemRow" ItemStyle-Width="90px" HeaderStyle-CssClass="GridHeader" ItemStyle-BorderWidth="1px"/>
                         <asp:BoundField DataField="Taarich_Haavara_Lesachar" HeaderText=" תאריך העברה לשכר" SortExpression="Taarich_Haavara_Lesachar" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" DataFormatString="{0:dd/MM/yyyy  HH:mm}" HtmlEncodeFormatString="true"  ItemStyle-Width="80px" />
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
        </div>
                  <br /> 
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" type="text/javascript">
function OpenLogBakashot(RowIndex)
    {

        var RowSelection, sQuryString, sLeft, returnWin;

     RowSelection = eval(document.all.ctl00_KdsContent_grdRequest.children(0).children(RowIndex + 1));
       
       sQuryString="?dt=" + Date();
       sQuryString = sQuryString + "&BakashaId=" + RowSelection.children(1).children(0).innerHTML;
     
         sLeft=(document.body.clientWidth/2)-400;
         returnWin = window.showModalDialog("LogBakashotModal.aspx" + sQuryString, "", "dialogwidth:1000px;dialogheight:700px;dialogtop:" + (document.body.clientWidth / 10) + "px;dialogleft:" + sLeft + "px;status:no");      
    }
   </script> 
</asp:Content>


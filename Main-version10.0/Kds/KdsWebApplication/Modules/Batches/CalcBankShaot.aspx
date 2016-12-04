<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeBehind="CalcBankShaot.aspx.cs" Inherits="KdsWebApplication.Modules.Batches.CalcBankShaot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">

    <div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:2000;width:150px" >
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" />
</div> 
<asp:UpdatePanel ID="upDivNetunim" runat="server" RenderMode="Inline">
    <ContentTemplate> 
       <fieldset class="FilterFieldSet" style="width:930px">
                <legend>הרצת חישוב בנק שעות</legend> 
               <br />
                <table>
                    <tr>
                        <td>מתקן:</td>
                        <td>
                            <asp:TextBox ID="txtMitkan" runat="server" MaxLength="6"  dir="rtl" style="width:100px;"></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderMitkan" runat="server" CompletionInterval="0"   CompletionSetCount="25" UseContextKey="true"  
                            TargetControlID="txtMitkan" MinimumPrefixLength="1" ServiceMethod="GetYechidotBankMeshek" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                            EnableCaching="true"  CompletionListCssClass="ACLst"
                            CompletionListHighlightedItemCssClass="ACLstItmSel"
                            CompletionListItemCssClass="ACLstItmE">                        
                            </cc1:AutoCompleteExtender>                                                  
                        </td>              
                        <td style="width:5px"></td>
                        <td>חודש:</td>
                        <td><asp:DropDownList ID="ddlBank"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="SetContextKey"  Width="100px"></asp:DropDownList></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td >
                              <asp:Button Text="הפעל חישוב בנק" ID="btnBank" Width="120px"   runat="server" CssClass="ImgButtonSearch" CausesValidation="false" onclick="btnRunBank_Click"  /> 
                        </td>
                    </tr>
                 </table>
            <br />
          </fieldset>
    </ContentTemplate>
</asp:UpdatePanel> 


</asp:Content>


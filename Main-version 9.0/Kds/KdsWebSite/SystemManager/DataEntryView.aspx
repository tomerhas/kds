<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataEntryView.aspx.cs" Inherits="DataEntryView" Title="Untitled Page" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="../Js/SystemManager.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
     <asp:Button ID="btnRefresh" runat="server" CssClass="Hidden" />
    <asp:UpdatePanel ID="upMessage" runat="server" RenderMode="Inline">
     <ContentTemplate>
    <asp:HiddenField ID="hdQueryString" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Button Text="הוסף" ID="btnNew" runat="server"  
                    CssClass="ImgButtonSearch"   /> 
                    
            </td>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td colspan="2">
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                    AllowPaging="true" AllowSorting="true" 
                    EmptyDataText="לא נמצאו נתונים!" 
                    CssClass="Grid" 
                    HeaderStyle-CssClass="GridHeader"
                    HeaderStyle-ForeColor="White" 
                    PageSize="12">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="ItemRow">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="lbDelete" ImageUrl="~/Images/delete.png" CommandName="Select"  OnClick="PromptDelete" />
                                <ajaxToolkit:ConfirmButtonExtender ID="cbDelete" runat="server"
                                    TargetControlID="lbDelete" DisplayModalPopupID="mpConfirm" />
                                <ajaxToolkit:ModalPopupExtender ID="mpConfirm" runat="server"
                                    TargetControlID="lbDelete"
                                    PopupControlID="pnlConfirm"
                                    OkControlID="OkConfButton" 
                                    CancelControlID="CancelConfButton"  />
                                <asp:Panel runat="server" ID="pnlConfirm" CssClass="PanelMessage" Width="200px" style="display:none;position:static;" >
                                    <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">מחיקה</asp:Label>
                                    <br /> <br /> 
                                        <asp:Label runat="server" ID="lblPrompt"></asp:Label>
                                     <br /><br /> 
                                    <asp:Button ID="OkConfButton" runat="server" Text="OK" CssClass="ImgButtonMake" CausesValidation="false" OnClick="PromptDelete" />
                                    <asp:Button ID="CancelConfButton" runat="server" CssClass="ImgButtonMake" CausesValidation="false" Text="Cancel" />
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="ItemRow">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSelect" runat="server" CommandName="Select"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                    <RowStyle CssClass="GridRow"   />
                    <SelectedRowStyle CssClass="SelectedGridRowNoIndent" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                    <PagerTemplate>
                         <kds:GridViewPager runat="server" ID="ucGridPager" />
                    </PagerTemplate>
                    <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/> 
             </asp:GridView>
            
            </td>
        </tr>
    </table>
    </ContentTemplate></asp:UpdatePanel>
    <script type="text/javascript">
        function refresh(){
            $get("<%=btnRefresh.ClientID %>").click();
        }
        
        function success(){
            return true;
        }
    </script>
</asp:Content>

